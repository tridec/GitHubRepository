Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.IO

Partial Public Class RadEditPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' login check
        Dim loginchk = New clsLoginCheck()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not loginchk.LoggedIn Then
            Response.Redirect("Login.aspx")
        End If

        If Not IsPostBack Then
            If Not Request("NODEID") Is Nothing Then
                lblNodeId.Text = Request("NODEID")
            Else
                lblNodeId.Text = 0
            End If

            loginchk.GetNodePermission(Val(lblNodeId.Text))

            If Not loginchk.HasView() Then ' Currently NO View ONLY permission
                Response.Redirect("Blank.aspx")
            ElseIf loginchk.HasModuleAdmin() Then
                'Admin has full rights
            ElseIf loginchk.HasModuleEdit() Then ' Only edit permissions
                'Disallow Publishing and modifying Published Records
                cbxPublish.Visible = False
                lblEditOnly.Text = "True"
            ElseIf loginchk.HasView() Then
                Response.Redirect("ContentPage.aspx" & "?NodeId=" & lblNodeId.Text)
            Else ' must have edit or Admin permissions on the NODE to view the page
                Response.Redirect("Blank.aspx")
            End If


            Dim intVersion As Integer = 0

            LoadVersionDropDown()
            LoadData(intVersion)
        End If
    End Sub

    Private Sub LoadEditorTools()
        'add a new Toolbar dynamically
        Dim dynamicToolbar As New EditorToolGroup()
        reContentText.Tools.Add(dynamicToolbar)

        'Remove toolbar if its already been added
        For Each group As Telerik.Web.UI.EditorToolGroup In reContentText.Tools
            Dim tool As EditorTool = group.FindTool("Uploaded Images")
            group.Tools.Remove(tool)
        Next

        'add a custom dropdown and set its items and dimension attributes
        Dim ddn As New EditorDropDown("Uploaded Images")
        ddn.Text = "Uploaded Images"

        'Set the popup width and height
        ddn.Attributes("width") = "110px"
        ddn.Attributes("popupwidth") = "240px"
        ddn.Attributes("popupheight") = "100px"

        'Get all uploaded images
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "RadEditUploadSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)

        db.AddInParameter(objCommand, "@RadEditId", DbType.Int32, Int32.Parse(lblNodeId.Text))

        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        If datareader.HasRows Then
            Do While datareader.Read()
                'Add items
                Dim imagePath As String = "RadEditUploadView.aspx?RadEditUploadId=" & datareader("RadEditUploadID")
                Dim strLinkInfo As String = "<img src='" & imagePath & "' alt='" & datareader("title") & "'>"
                ddn.Items.Add(datareader("filename"), strLinkInfo)
            Loop
        End If
        datareader.Close()

        'Add tool to toolbar
        dynamicToolbar.Tools.Add(ddn)

        Dim ddn2 As New EditorDropDown("Documents")


        'Set the popup width and height
        ddn2.Attributes("width") = "110px"
        ddn2.Attributes("popupwidth") = "240px"
        ddn2.Attributes("popupheight") = "100px"


        'Get all uploaded images
        db = DatabaseFactory.CreateDatabase("VOAConnectionString")
        sqlcommand = "DocumentPublishedSelect"
        objCommand = db.GetStoredProcCommand(sqlcommand)

        Dim datareader2 As SqlDataReader = db.ExecuteReader(objCommand)

        If datareader2.HasRows Then
            Do While datareader2.Read()
                'Add items
                Dim documentPath As String = "Documentview.aspx?DocumentUploadID=" & datareader2("DocumentUploadID")
                Dim docLinkInfo As String = "<a href='" & documentPath & "' target=""_new""> " + datareader2("DocumentTitle") + " </a><br />"
                ddn2.Items.Add(datareader2("DocumentTitle"), docLinkInfo)
            Loop
        End If

        datareader2.Close()
        dynamicToolbar.Tools.Add(ddn2)

        'If the Editor is active, add the Tab script
        Dim scriptText As String = ""
        scriptText &= "Telerik.Web.UI.Editor.CommandList.InsertTab = function(commandName, editor, oTool)"
        scriptText &= " { "
        scriptText &= " setTimeout(function() "
        scriptText &= " { "
        scriptText &= " $get(editor.get_id() + '_ModesWrapper').focus(); "
        scriptText &= " }, 0); "
        scriptText &= " }; "

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Script", scriptText, True)

    End Sub

    Private Sub LoadVersionDropDown()
        ddlVersion.Visible = True

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "RadEditSelectAll"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@NodeId", SqlDbType.Int, lblNodeId.Text)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        ddlVersion.DataSource = datareader
        ddlVersion.DataBind()
    End Sub

    Private Sub LoadData(ByVal version As Integer)
        'lookup and set title and content data
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String
        Dim objCommand As DbCommand

        MakeFieldsReadOnly()

        If version > 0 Then
            'Load data for specific version
            sqlcommand = "RadEditSelectVersion"
            objCommand = db.GetStoredProcCommand(sqlcommand)
            db.AddInParameter(objCommand, "@Version", SqlDbType.Int, version)
        Else
            'Load either the published version or the most recent version
            sqlcommand = "RadEditSelect"
            objCommand = db.GetStoredProcCommand(sqlcommand)
        End If
        db.AddInParameter(objCommand, "@NodeId", SqlDbType.Int, lblNodeId.Text)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        If datareader.HasRows Then
            datareader.Read()
            lblRadEditId.Text = datareader("RadEditId")
            lblTitleText.Text = datareader("TitleText")
            lblContentText.Text = datareader("ContentText")
            lblVersion.Text = datareader("Version")
            cbxPublish.Checked = datareader("Publish")
            ddlVersion.SelectedValue = datareader("Version")
        Else
            ddlVersion.Visible = False
            btnNewVersion.Visible = False
        End If
        datareader.Close()

        'Set Publish Permission
        If cbxPublish.Checked And lblEditOnly.Text = "True" Then
            btnEdit.Visible = False
        End If

    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        lblTitleText.Visible = False
        lblContentText.Visible = False
        btnEdit.Visible = False
        txtTitleText.Visible = True
        reContentText.Visible = True
        btnSave.Visible = True
        btnCancel.Visible = True
        cbxPublish.Enabled = True
        txtTitleText.Text = lblTitleText.Text
        reContentText.Content = lblContentText.Text
        pnlUpload.Visible = True
        btnNewVersion.Visible = False
        txtTitle.Text = ""


        LoadEditorTools()

        'If New Version - hide the New Version button
        If lblRadEditId.Text.Length <= 0 Then
            btnNewVersion.Visible = False
        End If


    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        'Is this a new record or an update?
        Dim blnNewRecord As Boolean
        If lblRadEditId.Text.Length <= 0 Then
            blnNewRecord = True
        End If

        'Is this a new version?
        Dim blnNewVersion As Boolean
        If lblNewVersion.Text = "True" Then
            blnNewVersion = True
        End If

        'Check for Title
        lblWarning.Text = ""
        If txtTitleText.Text = "" Then
            lblWarning.Text = "Please enter Page Title"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Invalid Title", _
            String.Format("alert('{0}');", "Please enter Page Title"), True)
            Exit Sub
        End If

        'Save data, and Update 
        lblTitleText.Visible = True
        lblContentText.Visible = True
        btnEdit.Visible = True
        txtTitleText.Visible = False
        reContentText.Visible = False
        btnSave.Visible = False
        btnCancel.Visible = False
        lblTitleText.Text = txtTitleText.Text
        lblContentText.Text = reContentText.Content

        'Determine Version
        Dim intVersion As Integer
        If lblVersion.Text = "" Or "0" Then
            intVersion = 1
        Else
            intVersion = Int32.Parse(lblVersion.Text)
        End If

        ' save the info to the database insert or update
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "RadEditUpdate"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        If lblRadEditId.Text.Length > 0 Then
            db.AddInParameter(objCommand, "@RadEditID", SqlDbType.Int, Val(lblRadEditId.Text))
        End If
        db.AddInParameter(objCommand, "@NodeID", SqlDbType.Int, Val(lblNodeId.Text))
        db.AddInParameter(objCommand, "@TitleText", DbType.String, txtTitleText.Text)
        db.AddInParameter(objCommand, "@ContentText", DbType.String, reContentText.Content)
        db.AddInParameter(objCommand, "@Version", DbType.Int32, intVersion)
        If cbxPublish.Checked Then
            UnpublishContent()
            db.AddInParameter(objCommand, "@Publish", DbType.Int32, 1)
        End If
        db.AddOutParameter(objCommand, "@InsertRadEditID", SqlDbType.Int, 100)
        db.ExecuteNonQuery(objCommand)

        lblRadEditId.Text = db.GetParameterValue(objCommand, "@InsertRadEditID")

        LoadVersionDropDown()
        LoadData(intVersion)

        'Perform Audit
        Dim objAudit As New clsAudit
        objAudit.RecordId = Val(lblRadEditId.Text)
        objAudit.ModuleTypeAuditId = clsModuleTypeAudit.ContentPage
        If blnNewRecord And Not blnNewVersion Then
            objAudit.AuditActionId = clsAuditAction.Created
        Else
            If blnNewVersion Then
                objAudit.AuditActionId = clsAuditAction.NewVersion
            Else
                objAudit.AuditActionId = clsAuditAction.Updated
            End If
        End If
        objAudit.SaveAction()

        'Perform Audit for Publish if it has been newly checked
        If lblPublish.Text = "True" Then
            If cbxPublish.Checked Then
                objAudit.AuditActionId = clsAuditAction.Published
                objAudit.SaveAction()
            Else
                objAudit.AuditActionId = clsAuditAction.UnPublished
                objAudit.SaveAction()
            End If
        End If


    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        MakeFieldsReadOnly()
        LoadData(0)
    End Sub

    Private Sub ddlVersion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlVersion.SelectedIndexChanged
        'Determine if in Edit
        Dim blnEdit As Boolean = False
        If reContentText.Visible = True Then
            blnEdit = True
        End If

        'Load the new data
        LoadData(ddlVersion.SelectedValue)

        'Reset to Edit if true
        If blnEdit Then
            btnEdit_Click(sender, e)
        End If

    End Sub

    Private Sub btnNewVersion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewVersion.Click
        'Determine New Version - The Version will not be saved until the saved button is clicked

        'Get the latest version
        Dim intVersion As Integer
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "RadEditSelectAll"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@NodeId", SqlDbType.Int, lblNodeId.Text)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)

        If datareader.HasRows() Then
            datareader.Read()
            intVersion = datareader("Version")
        End If
        datareader.Close()

        If intVersion = 0 Then
            intVersion = 1
        Else
            intVersion += 1
        End If

        lblRadEditId.Text = ""

        'Set Publish Permission
        If cbxPublish.Checked And lblEditOnly.Text = "True" Then
            cbxPublish.Checked = False
        End If

        lblVersion.Text = intVersion
        lblNewVersion.Text = True
        btnEdit_Click(sender, e)
    End Sub
    Sub ChangePublish()
        lblPublish.Text = "True"

    End Sub
    Private Sub MakeFieldsReadOnly()
        lblTitleText.Visible = True
        lblContentText.Visible = True
        btnEdit.Visible = True
        txtTitleText.Visible = False
        reContentText.Visible = False
        btnSave.Visible = False
        btnCancel.Visible = False
        cbxPublish.Enabled = False
        btnNewVersion.Visible = True
        pnlUpload.Visible = False
    End Sub

    Private Sub UnpublishContent()
        Dim db1 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand1 As String = "RadEditUnPublish"
        Dim objCommand1 As DbCommand = db1.GetStoredProcCommand(sqlcommand1)
        db1.AddInParameter(objCommand1, "@NodeID", SqlDbType.Int, Val(lblNodeId.Text))
        db1.ExecuteNonQuery(objCommand1)
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If rulUpload.UploadedFiles.Count > 0 And rulUpload.InvalidFiles.Count = 0 Then
            Dim intDocLen As Integer
            Dim strDocExt As String
            Dim strDocMimeType As String
            Dim strFileName As String

            'Stream object used for reading the contents of the Uploading Documnet
            Dim objStream As Stream

            intDocLen = rulUpload.UploadedFiles.Item(0).InputStream.Length
            'buffer to hold Document Contents
            Dim Docbuffer As Byte() = New Byte(intDocLen) {}
            'Grab the Content of the Uploaded Document

            'InputStream:
            'Gets a Stream object which points to an uploaded Document; 
            'to prepare for reading the contents of the file.
            objStream = rulUpload.UploadedFiles.Item(0).InputStream

            'Store the Content of the Documnet in a buffer
            'This buffer will be stored in the Database
            objStream.Read(Docbuffer, 0, intDocLen)

            strDocExt = rulUpload.UploadedFiles.Item(0).GetExtension().ToString
            strDocMimeType = rulUpload.UploadedFiles.Item(0).ContentType.ToString
            strFileName = rulUpload.UploadedFiles.Item(0).GetName().ToString

            'Add Uploaded Documnet to Database as Binary
            'You have to change the connection string
            Dim dbUpload As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommandUpload As String = "RadEditUploadInsert"
            Dim objCommandUpload As DbCommand = dbUpload.GetStoredProcCommand(sqlcommandUpload)
            dbUpload.AddInParameter(objCommandUpload, "@RadEditId", DbType.Int32, Val(lblNodeId.Text))
            dbUpload.AddInParameter(objCommandUpload, "@Filename", DbType.String, strFileName)
            ' default title to filename if title not populated
            If txtTitle.Text = "" Then
                txtTitle.Text = strFileName
            End If
            dbUpload.AddInParameter(objCommandUpload, "@Title", DbType.String, txtTitle.Text)
            ' no description
            dbUpload.AddInParameter(objCommandUpload, "@Description", DbType.String, "")
            dbUpload.AddInParameter(objCommandUpload, "@Image", DbType.Binary, Docbuffer)
            dbUpload.AddInParameter(objCommandUpload, "@Filesize", DbType.Int32, intDocLen)
            dbUpload.AddInParameter(objCommandUpload, "@Extension", DbType.String, strDocExt)
            dbUpload.AddInParameter(objCommandUpload, "@MimeType", DbType.String, strDocMimeType)

            Dim m2 As MembershipUser = Membership.GetUser()
            If Not m2 Is Nothing Then
                dbUpload.AddInParameter(objCommandUpload, "@RadEditUploadUserId", DbType.String, m2.ProviderUserKey.ToString())
            End If

            dbUpload.ExecuteNonQuery(objCommandUpload)

            'Log to Audit
            Dim objAudit As New clsAudit
            objAudit.RecordId = Val(lblRadEditId.Text)
            objAudit.ModuleTypeAuditId = clsModuleTypeAudit.ContentPage
            objAudit.AuditActionId = clsAuditAction.UploadImage
            objAudit.SaveAction()

            lblWarning.Text = "Upload Successful.  Image is now available in the Content Image Dropdown"

            LoadEditorTools()
            txtTitle.Text = ""
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Invalid File Type", _
            String.Format("alert('{0}');", "Invalid file for upload (File must be type JPG and less than 150KB). Please try again"), True)
            lblWarning.Text = "Invalid File or File Type for Upload (File must be type JPG and less than 150KB).  Please try again"
            Exit Sub

        End If
    End Sub
End Class