Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports VANS.MdwsDemo.dao.soap
Imports VANS.us.vacloud.devmdws
Imports VANS.MdwsDemo.domain

Public Class TemplateAddEdit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'login check
        Dim loginchk = New clsLoginCheck()
        If (Not loginchk.LoggedIn) Then
            Response.Redirect("~/login.aspx")
        End If

        If Not loginchk.Admin Then
            Response.Redirect("HomeMain.aspx")
        End If

        If Not Page.IsPostBack Then
            If Val(Request("TemplateID")) = 0 Then
                lblTitle.Text = "Add New Template"
                lblTemplateID.Text = 0
            Else
                'txtTemplateMessage.Text = "This is an existing template."
                lblTemplateID.Text = Request("TemplateID")
                lblTitle.Text = "Edit Template"
                LoadDescription()
                btnDelete.Visible = True
            End If

            If Request.UrlReferrer Is Nothing Then
                lblReferURL.Text = "/TemplateList.aspx"
            ElseIf Request.UrlReferrer.AbsolutePath.ToString = "/TemplateList.aspx" Then
                lblReferURL.Text = "/TemplateList.aspx"
            ElseIf Request.UrlReferrer.AbsolutePath.ToString = "/TemplateSearch.aspx" Then
                lblReferURL.Text = "/TemplateSearch.aspx"
            Else
                lblReferURL.Text = "/TemplateList.aspx"
            End If


        End If
    End Sub

    Private Sub LoadDescription()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "TemplateSpecificSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@TemplateID", DbType.Int32, lblTemplateID.Text)
        Using datareader As IDataReader = db.ExecuteReader(objCommand)
            If datareader.Read Then
                txtTemplateMessage.Text = datareader("Message")
            End If
        End Using

    End Sub

    'Public Sub chkHeader_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim chkHeader As CheckBox = DirectCast(sender, CheckBox)
    '    Dim groupHeader As GridGroupHeaderItem = CType(chkHeader.NamingContainer, GridGroupHeaderItem)
    '    Dim children As GridItem() = groupHeader.GetChildItems()

    '    CheckChildren(chkHeader.Checked, children)
    'End Sub

    'Private Sub CheckParents(ByVal parent As Object)
    '    Dim strstring As String = parent.ToString

    'End Sub

    'Private Sub CheckChildren(ByVal blnChecked As Boolean, ByVal children As GridItem())
    '    For Each child In children
    '        If TypeOf child Is GridDataItem Then
    '            Dim chkClinic As CheckBox = child.FindControl("chkClinic")
    '            If blnChecked = True Then
    '                chkClinic.Checked = True
    '            Else
    '                chkClinic.Checked = False
    '            End If
    '        ElseIf TypeOf child Is GridGroupHeaderItem Then
    '            Dim chkHeader As CheckBox = child.FindControl("chkHeader")
    '            If blnChecked = True Then
    '                chkHeader.Checked = True
    '            Else
    '                chkHeader.Checked = False
    '            End If
    '            CheckChildren(blnChecked, CType(child, GridGroupHeaderItem).GetChildItems)
    '        End If
    '    Next
    'End Sub

    Protected Sub chkEnabledDefault_UpdateEnabled(sender As Object, e As EventArgs)
        Dim chkEnabledDefault As CheckBox = DirectCast(sender, CheckBox)
        Dim itm As GridDataItem = DirectCast(chkEnabledDefault.NamingContainer, GridDataItem)
        Dim int As Integer = itm("TemplateClinicID").Text

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "TemplateClinicUpdate"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@TemplateClinicID", DbType.Int32, itm("TemplateClinicID").Text)
        db.AddInParameter(objCommand, "@DefaultEnabled", DbType.Int32, chkEnabledDefault.Checked * -1)
        db.ExecuteNonQuery(objCommand)

    End Sub

    Private Sub rgdClinicsAssigned_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdClinicsAssigned.NeedDataSource
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "TemplateClinicAssignedSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@TemplateID", DbType.Int32, lblTemplateID.Text)
        rgdClinicsAssigned.DataSource = db.ExecuteDataSet(objCommand)
    End Sub

    Private Sub rgdClinicsAvailable_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdClinicsAvailable.NeedDataSource
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "TemplateClinicAvailableSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@TemplateID", DbType.Int32, lblTemplateID.Text)
        rgdClinicsAvailable.DataSource = db.ExecuteDataSet(objCommand)
    End Sub

    'Private Sub btnRemove_Click(sender As Object, e As System.EventArgs) Handles btnRemove.Click
    '    'Shouldn't need this until a template has been saved.
    '    If lblTemplateID.Text > 0 Then
    '        'Iterate Grid remove checked items
    '        For Each item As GridDataItem In rgdClinicsAssigned.Items
    '            Dim chkClinic As CheckBox = item.FindControl("chkClinic")
    '            If chkClinic.Checked = True Then
    '                Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '                Dim sqlCommand As String = "TemplateClinicDelete"
    '                Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
    '                db.AddInParameter(objCommand, "@TemplateClinicID", DbType.Int32, item("TemplateClinicID").Text)
    '                db.ExecuteNonQuery(objCommand)
    '            End If
    '        Next

    '        rgdClinicsAssigned.Rebind()
    '        rgdClinicsAvailable.Rebind()
    '    End If
    'End Sub

    Private Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(lblReferURL.Text)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Private Sub Save()
        'Insert or Update
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "TemplateInsertUpdate"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@Message", DbType.String, txtTemplateMessage.Text)

        Dim blnInsert As Boolean

        If lblTemplateID.Text = 0 Then
            blnInsert = True
        End If

        If blnInsert = True Then
            db.AddOutParameter(objCommand, "@InsertTemplateID", SqlDbType.Int, 100)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Template has been created", String.Format("alert('{0}');", "The Template as been created."), True)
        Else
            db.AddInParameter(objCommand, "@TemplateID", DbType.Int32, lblTemplateID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Template has been saved", String.Format("alert('{0}');", "The Template as been saved."), True)
        End If

        db.ExecuteNonQuery(objCommand)

        If blnInsert = True Then
            lblTemplateID.Text = db.GetParameterValue(objCommand, "@InsertTemplateID")
            btnDelete.Visible = True
        End If
    End Sub

    'Private Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click

    '    'Check if This template has been created first
    '    If lblTemplateID.Text = 0 Then
    '        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '        Dim sqlCommand As String = "TemplateInsertUpdate"
    '        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
    '        db.AddInParameter(objCommand, "@Message", DbType.String, txtTemplateMessage.Text)
    '        db.AddOutParameter(objCommand, "@InsertTemplateID", SqlDbType.Int, 100)
    '        db.ExecuteNonQuery(objCommand)
    '        lblTemplateID.Text = db.GetParameterValue(objCommand, "@InsertTemplateID")
    '        btnDelete.Visible = True
    '        ScriptManager.RegisterStartupScript(Me,Me.GetType(), "Template has been created", String.Format("alert('{0}');", "The Template as been created."), True)
    '    End If

    '    For Each item As GridDataItem In rgdClinicsAvailable.Items
    '        Dim chkClinic As CheckBox = item.FindControl("chkClinic")
    '        If chkClinic.Checked = True Then
    '            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '            Dim sqlCommand As String = "TemplateClinicInsert"
    '            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
    '            db.AddInParameter(objCommand, "@ClinicID", DbType.Int32, item("ClinicID").Text)
    '            db.AddInParameter(objCommand, "@TemplateID", DbType.Int32, lblTemplateID.Text)
    '            db.ExecuteNonQuery(objCommand)
    '        End If
    '    Next

    '    rgdClinicsAssigned.Rebind()
    '    rgdClinicsAvailable.Rebind()
    'End Sub

    Private Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "TemplateDelete"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@TemplateID", DbType.String, lblTemplateID.Text)
        db.ExecuteNonQuery(objCommand)
        Response.Redirect(lblReferURL.Text)

    End Sub

    Private Sub rgdClinicsAvailable_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgdClinicsAvailable.UpdateCommand
        'Check if This template has been created first
        If lblTemplateID.Text = 0 Then
            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlCommand As String = "TemplateInsertUpdate"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
            db.AddInParameter(objCommand, "@Message", DbType.String, txtTemplateMessage.Text)
            db.AddOutParameter(objCommand, "@InsertTemplateID", SqlDbType.Int, 100)
            db.ExecuteNonQuery(objCommand)
            lblTemplateID.Text = db.GetParameterValue(objCommand, "@InsertTemplateID")
            btnDelete.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Template has been created", String.Format("alert('{0}');", "The Template as been created."), True)
        End If

        Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)

        Dim db2 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand2 As String = "TemplateClinicInsert"
        Dim objCommand2 As DbCommand = db2.GetStoredProcCommand(sqlCommand2)
        db2.AddInParameter(objCommand2, "@ClinicID", DbType.Int32, itm("ClinicID").Text)
        db2.AddInParameter(objCommand2, "@TemplateID", DbType.Int32, lblTemplateID.Text)
        db2.ExecuteNonQuery(objCommand2)


        rgdClinicsAssigned.Rebind()
        rgdClinicsAvailable.Rebind()
    End Sub

    Private Sub rgdClinicsAssigned_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgdClinicsAssigned.UpdateCommand
        'Shouldn't need this until a template has been saved.
        If lblTemplateID.Text > 0 Then
            'Iterate Grid remove checked items
            Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)

            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlCommand As String = "TemplateClinicDelete"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
            db.AddInParameter(objCommand, "@TemplateClinicID", DbType.Int32, itm("TemplateClinicID").Text)
            db.ExecuteNonQuery(objCommand)

            rgdClinicsAssigned.Rebind()
            rgdClinicsAvailable.Rebind()
        End If
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As System.EventArgs) Handles btnPreview.Click
        Save()
        Response.Redirect("TemplatePrintPreview.aspx?TemplateID=" & lblTemplateID.Text)

    End Sub
End Class