Imports System.IO
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common

Partial Public Class DocumentAdd
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
                lblNodeID.Text = Request("NODEID")
            Else
                lblNodeID.Text = 0
            End If

            If Not Request("DocumentID") Is Nothing Then
                If Not Request("Version") Is Nothing Then
                    LoadData(Request("DocumentID"), True)
                Else
                    LoadData(Request("DocumentID"), False)
                End If
            End If

            'Set Permissions
            loginchk.GetNodePermission(lblNodeID.Text)

            If Not loginchk.HasView() And Not (loginchk.HasModuleAdmin() Or loginchk.HasModuleEdit) Then
                Response.Redirect("Blank.aspx")
            End If

        End If
    End Sub
    Private Sub LoadData(ByVal documentID As String, ByVal blnNewVersion As Boolean)
        'Get Document Details
        Dim docDetails As New clsDocument(documentID)
        lblVersionID.Text = docDetails.Version
        txtDescription.Text = docDetails.DocumentDescription
        txtTitle.Text = docDetails.DocumentTitle
        lblDocumentUploadID.Text = docDetails.DocumentUploadID
        'cbxPublish.Checked = docDetails.Publish
        'lblPublishValue.Text = docDetails.Publish

        If Not blnNewVersion Then
            btnSave.Visible = True
            rulUpload.Enabled = False
            btnUpload.Enabled = False
        End If

    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click

        If rulUpload.UploadedFiles.Count > 0 And rulUpload.InvalidFiles.Count = 0 Then

            'Set Version to 1 for New File
            If lblVersionID.Text = "" Then
                lblVersionID.Text = 1
            Else
                lblVersionID.Text = Int32.Parse(lblVersionID.Text) + 1
            End If

            lblWarning.Text = ""

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
            Dim dbUpload As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommandUpload As String = "DocumentUploadInsert"
            Dim objCommandUpload As DbCommand = dbUpload.GetStoredProcCommand(sqlcommandUpload)
            dbUpload.AddInParameter(objCommandUpload, "@Filename", DbType.String, strFileName)
            dbUpload.AddInParameter(objCommandUpload, "@Image", DbType.Binary, Docbuffer)
            dbUpload.AddInParameter(objCommandUpload, "@Filesize", DbType.Int32, intDocLen)
            dbUpload.AddInParameter(objCommandUpload, "@Extension", DbType.String, strDocExt)
            dbUpload.AddInParameter(objCommandUpload, "@MimeType", DbType.String, strDocMimeType)
            dbUpload.AddOutParameter(objCommandUpload, "@InsertDocumentUploadID", SqlDbType.Int, 100)
            dbUpload.ExecuteNonQuery(objCommandUpload)

            Dim intDocumentUploadID As Integer
            If Not dbUpload.GetParameterValue(objCommandUpload, "@InsertDocumentUploadID") Is System.DBNull.Value Then
                intDocumentUploadID = dbUpload.GetParameterValue(objCommandUpload, "@InsertDocumentUploadID")
            End If

            Dim intDocumentID As Integer
            intDocumentID = SaveFileDetails(intDocumentUploadID)

            lblWarning.Text = "Upload Successful.  "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Upload Successful", _
            String.Format("alert('{0}');", "Upload Successful"), True)

            'Perform Audit
            Dim objAudit As New clsAudit
            objAudit.RecordId = intDocumentID
            objAudit.ModuleTypeAuditId = clsModuleTypeAudit.DocumentList
            If Val(lblVersionID.Text) = 1 Then
                objAudit.AuditActionId = clsAuditAction.Created
            Else
                objAudit.AuditActionId = clsAuditAction.NewVersion
            End If
            objAudit.SaveAction()

            'If cbxPublish.Checked Then
            '    objAudit.AuditActionId = clsAuditAction.Published
            '    objAudit.SaveAction()
            'End If

            'For Add New Version - Close and return to form
            If lblVersionID.Text <> "1" Then
                Response.Redirect("ManageDocuments.aspx?NodeID=" + lblNodeID.Text)
            Else
                'For First Version - Clear form and allow for additional uploads
                lblVersionID.Text = ""
                txtTitle.Text = ""
                txtDescription.Text = ""
                'cbxPublish.Checked = False
            End If

        ElseIf rulUpload.InvalidFiles.Count > 0 Then
            'lblWarning.Text = "Invalid File or File Type for Upload.  Please try again"

            Exit Sub

        End If
    End Sub
    Private Function SaveFileDetails(ByVal intDocumentUploadID As Integer)
        'If the DocumentUploadID is 0 then use the previous one
        If intDocumentUploadID = 0 Then
            intDocumentUploadID = Val(lblDocumentUploadID.Text)
        End If

        'If title is blank, use file name
        If txtTitle.Text = "" Then
            txtTitle.Text = rulUpload.UploadedFiles.Item(0).GetName().ToString
        End If

        Dim intDocumentID As Integer

        If Not Request("DocumentID") Is Nothing Then
            Dim docUpdate As New clsDocument(Val(Request("DocumentID")))
            docUpdate.NodeId = lblNodeID.Text
            docUpdate.DocumentTitle = txtTitle.Text
            docUpdate.DocumentDescription = txtDescription.Text
            docUpdate.Version = Val(lblVersionID.Text)
            'If cbxPublish.Checked Then
            '    docUpdate.Publish = 1
            'Else
            docUpdate.Publish = 0
            'End If
            docUpdate.DocumentUploadID = intDocumentUploadID
            docUpdate.Update()

            intDocumentID = Request("DocumentID")
        Else
            Dim docUpdate As New clsDocument
            docUpdate.NodeId = lblNodeID.Text
            docUpdate.DocumentTitle = txtTitle.Text
            docUpdate.DocumentDescription = txtDescription.Text
            docUpdate.Version = Val(lblVersionID.Text)
            docUpdate.DocumentUploadID = intDocumentUploadID
            'If cbxPublish.Checked Then
            '    docUpdate.Publish = 1
            'Else
            docUpdate.Publish = 0
            'End If

            intDocumentID = docUpdate.Insert()

        End If

        Return intDocumentID
    End Function

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
        Response.Redirect("ManageDocuments.aspx?NodeID=" + lblNodeID.Text)
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim intDocumentID As Integer

        If rulUpload.Enabled = False And txtTitle.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Title String Empty", String.Format("alert('{0}');", "Please enter a title for the document."), True)
            Exit Sub
        End If

        intDocumentID = SaveFileDetails(0)

        'Perform Audit
        Dim objAudit As New clsAudit
        objAudit.RecordId = intDocumentID
        objAudit.ModuleTypeAuditId = clsModuleTypeAudit.DocumentList
        objAudit.AuditActionId = clsAuditAction.Updated
        objAudit.SaveAction()

        'If cbxPublish.Checked Then
        '    objAudit.AuditActionId = clsAuditAction.Published
        '    objAudit.SaveAction()
        'Else
        '    'Save as Unpublished if it was previously published.
        '    If lblPublishValue.Text = "1" Then
        '        objAudit.AuditActionId = clsAuditAction.UnPublished
        '        objAudit.SaveAction()
        '    End If
        'End If

        Response.Redirect("ManageDocuments.aspx?NodeID=" + lblNodeID.Text)

    End Sub
    Public Sub validateRULServerValidate(ByVal source As Object, ByVal e As ServerValidateEventArgs) Handles CustomValidatorRUL.ServerValidate
        e.IsValid = (rulUpload.InvalidFiles.Count = 0)
        'CType(e.Item.FindControl("lblWarning"), Label).Text = "Invalid File, File Type, or File Size for Upload.  Please try again"
        If e.IsValid = False Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Invalid File Type", _
             String.Format("alert('{0}');", "Invalid file for upload (.htm, .html, .pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .mpp, .zip, or .vsd and less than 100MB)"), True)
            'e.Canceled = True
            Exit Sub
        End If
    End Sub
    'Public Sub checkRequiredUpload(ByVal source As Object, ByVal e As ServerValidateEventArgs) Handles CustomValidatorRULRequired.ServerValidate

    '    e.IsValid = (rulUpload.UploadedFiles.Count = 1)

    'End Sub
End Class
