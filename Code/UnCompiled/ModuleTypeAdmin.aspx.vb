Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports Telerik.Web.UI
Imports System.Data.SqlClient

Partial Public Class ModuleTypeAdmin
    Inherits System.Web.UI.Page
    Dim datareader As SqlDataReader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' login check
        Dim loginchk = New clsLoginCheck()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        ' redirect if not logged in or not admin
        If (Not loginchk.LoggedIn) Then
            Response.Redirect("login.aspx")
        End If
        If Not IsPostBack Then
            Dim intNodeId As Integer = 0
            If Not Request("NODEID") Is Nothing Then
                intNodeId = Request("NODEID")
            Else
                intNodeId = 0
            End If
            loginchk.GetNodePermission(intNodeId)
            ' Currently NO View ONLY permission
            If Not loginchk.HasView() Then
                Response.Redirect("Blank.aspx")
            ElseIf Not loginchk.HasModuleAdmin() Then ' must have Admin permissions on the NODE
                Response.Redirect("Blank.aspx")

            End If
            Dim intVersion As Integer = 0

            'LoadModuleType()
            
        End If
    End Sub


    Private Sub rgModuleType_DeleteCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgModuleType.DeleteCommand
        ' database connection
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        ' sql stored procedure Name as a string
        Dim sqlcommand As String = "ModuleTypeDelete"
        ' database command object that excecutes the stored procedure
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        Dim lblModuleTypeId As Label = e.Item.FindControl("ModuleTypeIdLabel")


        Dim intModuleTypeId As Integer = Val(lblModuleTypeId.Text)
        db.AddInParameter(objCommand, "@ModuleTypeId", DbType.Int32, intModuleTypeId)
        ' execute the command object
        db.ExecuteNonQuery(objCommand)

        'Perform Audit
        Dim objAudit As New clsAudit
        objAudit.RecordId = Val(lblModuleTypeId.Text)
        objAudit.ModuleTypeAuditId = clsModuleTypeAudit.ModuleTypeAdmin
        objAudit.AuditActionId = clsAuditAction.Deleted
        objAudit.SaveAction()


    End Sub


    Private Sub rgModuleType_InsertCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgModuleType.InsertCommand
        ' database connection
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        ' sql stored procedure Name as a string
        Dim sqlcommand As String = "ModuleTypeInsert"
        ' database command object that excecutes the stored procedure
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        'find the text box controls
        Dim txtModuleName As TextBox = e.Item.FindControl("txtModuleName")
        Dim txtModuleURL As TextBox = e.Item.FindControl("txtModuleURL")
        Dim txtModuleDesc As TextBox = e.Item.FindControl("txtModuleDescription")
        Dim cbIsPublic As CheckBox = e.Item.FindControl("cbIsPublicValue")
        Dim retValParam As Integer

        'double check to make sure that controls were found
        If Not txtModuleName Is Nothing Then
            db.AddInParameter(objCommand, "@ModuleName", DbType.String, txtModuleName.Text)
        End If
        If Not txtModuleURL Is Nothing Then
            db.AddInParameter(objCommand, "@ModuleURL", DbType.String, txtModuleURL.Text)
        End If
        If Not txtModuleDesc Is Nothing Then
            db.AddInParameter(objCommand, "@ModuleDescription", DbType.String, txtModuleDesc.Text)
        End If
        If Not cbIsPublic Is Nothing Then
            If cbIsPublic.Checked = True Then
                db.AddInParameter(objCommand, "@IsPublic", DbType.Int32, 1)
            Else
                db.AddInParameter(objCommand, "@IsPublic", DbType.Int32, 0)
            End If
        End If
        db.AddOutParameter(objCommand, "@InsertModuleTypeID", SqlDbType.Int, 100)

        ' execute the command object
        db.ExecuteNonQuery(objCommand)

        retValParam = db.GetParameterValue(objCommand, "@InsertModuleTypeID")

        Dim objAudit As New clsAudit
        objAudit.RecordId = Val(retValParam)
        objAudit.ModuleTypeAuditId = clsModuleTypeAudit.ModuleTypeAdmin
        objAudit.AuditActionId = clsAuditAction.Created
        objAudit.SaveAction()


    End Sub

    Private Sub rgModuleType_UpdateCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgModuleType.UpdateCommand
        ' database connection
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        ' sql stored procedure Name as a string
        Dim sqlcommand As String = "ModuleTypeUpdate"
        ' database command object that excecutes the stored procedure
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        'find the text box controls
        Dim txtModuleName As TextBox = e.Item.FindControl("txtModuleName")
        Dim txtModuleURL As TextBox = e.Item.FindControl("txtModuleURL")
        Dim txtModuleDesc As TextBox = e.Item.FindControl("txtModuleDescription")
        Dim cbIsPublic As CheckBox = e.Item.FindControl("cbIsPublicValue")
        Dim lblModuleTypeId As Label = e.Item.FindControl("lblModuleTypeId")

        'double check to make sure that controls were found
        If Not lblModuleTypeId Is Nothing Then
            db.AddInParameter(objCommand, "@ModuleTypeId", DbType.Int32, Val(lblModuleTypeId.Text))
        End If
        If Not txtModuleName Is Nothing Then
            db.AddInParameter(objCommand, "@ModuleName", DbType.String, txtModuleName.Text)
        End If
        If Not txtModuleURL Is Nothing Then
            db.AddInParameter(objCommand, "@ModuleURL", DbType.String, txtModuleURL.Text)
        End If
        If Not txtModuleDesc Is Nothing Then
            db.AddInParameter(objCommand, "@ModuleDescription", DbType.String, txtModuleDesc.Text)
        End If
        If Not cbIsPublic Is Nothing Then
            If cbIsPublic.Checked = True Then
                db.AddInParameter(objCommand, "@IsPublic", DbType.Int32, 1)
            Else
                db.AddInParameter(objCommand, "@IsPublic", DbType.Int32, 0)
            End If

        End If

        Dim objAudit As New clsAudit
        objAudit.RecordId = Val(lblModuleTypeId.Text)
        objAudit.ModuleTypeAuditId = clsModuleTypeAudit.ModuleTypeAdmin
        objAudit.AuditActionId = clsAuditAction.Updated
        objAudit.SaveAction()

        ' execute the command object
        db.ExecuteNonQuery(objCommand)
    End Sub
    Private Sub LoadModuleType()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "ModuleTypeAdminSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        datareader = db.ExecuteReader(objCommand)
        rgModuleType.DataSource = datareader
        'rgModuleType.DataBind()
    End Sub
    Private Sub rgModuleType_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgModuleType.ItemDataBound

        If TypeOf e.Item Is GridDataItem Then
            Dim gridItem As GridDataItem = CType(e.Item, GridDataItem)
            Dim lblIsPublic As Label = e.Item.FindControl("IsPublicLabel")
            Dim cbIsPublic As CheckBox = e.Item.FindControl("cbIsPublic")
            Dim lblModuleTypeLabel As Label = e.Item.FindControl("ModuleTypeIdLabel")
            Dim btnDelete As LinkButton = e.Item.FindControl("btnDelete")
            'omit delete button for special fields
            If Val(lblModuleTypeLabel.Text) = 1 Or Val(lblModuleTypeLabel.Text) = 2 Or Val(lblModuleTypeLabel.Text) = 7 Or Val(lblModuleTypeLabel.Text) = 8 Or Val(lblModuleTypeLabel.Text) = 12 Or Val(lblModuleTypeLabel.Text) = 13 Then

                btnDelete.Enabled = False
                btnDelete.Visible = False
            Else
                btnDelete.Enabled = True
                btnDelete.Visible = True
            End If

            If Val(lblIsPublic.Text) = 1 Then
                cbIsPublic.Checked = True
            Else
                cbIsPublic.Checked = False
            End If

        End If

        If TypeOf e.Item Is GridEditFormItem AndAlso e.Item.IsInEditMode Then
            Dim editForm As GridEditFormItem = DirectCast(e.Item, GridEditFormItem)
            Dim lblIsPublic As Label = editForm.FindControl("lblIsPublicValue")
            Dim cbIsPublic As CheckBox = editForm.FindControl("cbIsPublicValue")

            If Val(lblIsPublic.Text) = 1 Then
                cbIsPublic.Checked = True
            Else
                cbIsPublic.Checked = False
            End If
        End If

    End Sub

    Private Sub rgModuleType_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgModuleType.NeedDataSource
        LoadModuleType()
    End Sub

    Private Sub rgModuleType_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgModuleType.DataBound
        datareader.Close()
    End Sub
End Class