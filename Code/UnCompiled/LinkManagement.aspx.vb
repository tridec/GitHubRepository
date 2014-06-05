Imports Telerik.Web.UI
Imports System.Data
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary
Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data.Common
Imports System.Data.SqlClient

Partial Public Class LinkManagement
    Inherits System.Web.UI.Page

    Private intLinkId As Integer
    Private intNodeId As Integer
    Private strURL As String
    Private strTitle As String
    Private strDescription As String

    Private Sub LoadData()
        'Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        'Dim sqlcommand As String = "LinksSelect"
        'Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        'db.AddInParameter(objCommand, "@NodeId", SqlDbType.Int, Val(lblNodeID.Text))
        'Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        'RadGrid1.DataSource = datareader
        'RadGrid1.DataBind()

        'Dim db2 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        'Dim sqlcommand2 As String = "LinkCategoriesSelect"
        'Dim objCommand2 As DbCommand = db2.GetStoredProcCommand(sqlcommand2)
        'db2.AddInParameter(objCommand2, "@NodeId", SqlDbType.Int, Val(lblNodeID.Text))
        'Dim datareader2 As SqlDataReader = db2.ExecuteReader(objCommand2)
        'RadGrid2.DataSource = datareader2
        'RadGrid2.DataBind()

    End Sub


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' login check
        Dim loginchk = New clsLoginCheck()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not loginchk.LoggedIn Then
            Response.Redirect("Login.aspx")
        End If
        If Not IsPostBack Then
            Dim intNodeId As Integer = 0
            If Not Request("NODEID") Is Nothing Then
                intNodeId = Request("NODEID")
            Else
                intNodeId = 0
            End If

            lblNodeID.Text = intNodeId

            loginchk.GetNodePermission(Val(intNodeId))

            'If loginchk.HasView() Then
            '    Response.Redirect("Blank.aspx")
            'End If

            If Not loginchk.HasView() Then
                Response.Redirect("Blank.aspx")
            ElseIf loginchk.HasModuleAdmin() Then 'Hide Nothing
                'Don't have to do anything
            ElseIf loginchk.HasModuleEdit() Then 'Hide Nothing
                'Don't have to do anything
            ElseIf loginchk.HasView() Then
                'Hide Add/Edit/Delete and Categories list
                'Hiding the Add Button
                For Each cmditm As GridCommandItem In RadGrid1.MasterTableView.GetItems(GridItemType.CommandItem)
                    If Not (loginchk.HasModuleAdmin Or loginchk.HasModuleEdit) And loginchk.HasView Then
                        Dim btn1 As Button = CType(cmditm.FindControl("AddNewRecordButton"), Button)
                        btn1.Visible = False
                        Dim lnkbtn1 As LinkButton = CType(cmditm.FindControl("InitInsertButton"), LinkButton)
                        lnkbtn1.Visible = False
                    End If
                Next
                'Hiding the Edit Column
                Dim editColumn As GridColumn = RadGrid1.MasterTableView.GetColumnSafe("EditColumn1")
                If Not editColumn Is Nothing Then
                    editColumn.Visible = False
                End If
                'Hiding the Delete Column
                Dim deleteColumn As GridColumn = RadGrid1.MasterTableView.GetColumnSafe("DeleteColumn1")
                If Not deleteColumn Is Nothing Then
                    deleteColumn.Visible = False
                End If
                pnlTable3.Visible = False
            Else ' must have permissions on the NODE to view the page
                Response.Redirect("Blank.aspx")
            End If

        End If

    End Sub

    Protected Sub LoadCategory(ByRef ddlCategory As DropDownList, ByRef lblCategoryID As Label)
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "LinkCategoriesSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@NodeId", SqlDbType.Int, Val(lblNodeID.Text))
        'db.AddInParameter(objCommand, "@NodeID", DbType.Int32, 0)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        ddlCategory.DataSource = datareader
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Please Select", "0"))
        If Not (lblCategoryID Is Nothing Or lblCategoryID.Text.Length = 0) Then
            ddlCategory.SelectedValue = Val(lblCategoryID.Text)
        Else
            ddlCategory.SelectedValue = "0"
        End If
    End Sub


    'Private Sub RadGrid1_UpdateCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.UpdateCommand


    '    Dim dbUpdate As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommandUpdate As String = "LinksUpdate"

    '    Dim objCommandUpdate As DbCommand = dbUpdate.GetStoredProcCommand(sqlcommandUpdate)
    '    dbUpdate.AddInParameter(objCommandUpdate, "@LinkID", DbType.Int32, intLinkId)
    '    dbUpdate.AddInParameter(objCommandUpdate, "@NodeID", DbType.String, intNodeId)
    '    dbUpdate.AddInParameter(objCommandUpdate, "@Title", DbType.String, strTitle)
    '    dbUpdate.AddInParameter(objCommandUpdate, "@Description", DbType.String, strDescription)


    'End Sub


    Private Sub SqlDataSource1_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSource1.Selecting
        e.Command.Parameters("@NodeID").Value = Request("NODEID")
    End Sub
    

    Private Sub SqlDataSource2_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSource2.Selecting
        e.Command.Parameters("@NodeID").Value = Request("NODEID")
    End Sub


    Private Sub RadGrid1_DeleteCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.DeleteCommand
        'Dim txtLinkID As TextBox = e.Item.FindControl("LinkID")
        'Dim intLink As Integer
        'intLink = RadGrid1.SelectedValue
        Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
        Dim intLinkID As Integer = item("LinkID").Text


        If Not intLinkID = 0 Then
            'remove item from datbase
            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommand As String = "LinksDelete"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)

            db.AddInParameter(objCommand, "@LinkID", DbType.Int32, intLinkID)
            db.ExecuteNonQuery(objCommand)
        End If

    End Sub



    Private Sub RadGrid2_DeleteCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.DeleteCommand
        Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
        Dim intCategoryID As Integer = item("CategoryID").Text

        If Not intCategoryID = 0 Then
            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommand As String = "LinkCategoriesDelete"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)

            db.AddInParameter(objCommand, "@CategoryID", DbType.Int32, intCategoryID)
            db.ExecuteNonQuery(objCommand)
        End If
    End Sub



    'Private Sub RadGrid1_EditCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.EditCommand
    '    Dim editForm As GridItem = DirectCast(e.Item, GridItem)
    '    Dim ddlCategory As DropDownList = editForm.FindControl("ddlCategory")
    '    Dim lblCategoryID As Label = editForm.FindControl("lblCategoryID")
    '    If Not ddlCategory Is Nothing Then
    '        LoadCategory(ddlCategory, lblCategoryID)
    '    End If
    '    'Dim ddlCategory As DropDownList = e.Item.FindControl("ddlCategory")
    '    'Dim lblCategoryID As Label = e.Item.FindControl("lblCategoryID")
    '    'LoadCategory(ddlCategory, lblCategoryID)
    'End Sub

    Private Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If TypeOf e.Item Is GridEditFormItem AndAlso e.Item.IsInEditMode Then

            Dim editForm As GridEditFormItem = DirectCast(e.Item, GridEditFormItem)
            'load
            Dim ddlCategory As DropDownList = editForm.FindControl("ddlCategory")
            Dim lblCategoryID As Label = editForm.FindControl("lblCategoryID")
            If Not ddlCategory Is Nothing Then
                LoadCategory(ddlCategory, lblCategoryID)
            End If
        End If
    End Sub

    'Private Sub RadGrid1_ItemDeleted(ByVal source As Object, ByVal e As Telerik.Web.UI.GridDeletedEventArgs) Handles RadGrid1.ItemDeleted
    '    Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
    '    Dim intLinkID As Integer = item("LinkID").Text

    'End Sub



    Private Sub RadGrid1_UpdateCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.UpdateCommand

        Dim txtLinkID As TextBox = e.Item.FindControl("LinkID")
        Dim txtNodeID As TextBox = New TextBox
        txtNodeID.text = Request("NODEID")
        Dim txtURL As TextBox = e.Item.FindControl("URL")
        Dim txtTitle As TextBox = e.Item.FindControl("Title")
        Dim txtDescription As TextBox = e.Item.FindControl("Description")
        Dim ddlCategory As DropDownList = e.Item.FindControl("ddlCategory")

        'update database
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "LinksUpdate"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)


        If txtLinkID.Text = "" Then
            db.AddInParameter(objCommand, "@NodeID", DbType.Int32, Val(txtNodeID.Text))
            db.AddInParameter(objCommand, "@URL", DbType.String, txtURL.Text)
            db.AddInParameter(objCommand, "@Title", DbType.String, txtTitle.Text)
            db.AddInParameter(objCommand, "@Description", DbType.String, txtDescription.Text)
            If Not ddlCategory Is Nothing Then
                If ddlCategory.SelectedValue > 0 Then
                    db.AddInParameter(objCommand, "@CategoryID", DbType.Int32, Val(ddlCategory.SelectedValue))
                End If
            End If 

            db.ExecuteNonQuery(objCommand)
        Else

            db.AddInParameter(objCommand, "@LinkID", DbType.Int32, Val(txtLinkID.Text))
            db.AddInParameter(objCommand, "@NodeID", DbType.Int32, Val(txtNodeID.Text))
            db.AddInParameter(objCommand, "@URL", DbType.String, txtURL.Text)
            db.AddInParameter(objCommand, "@Title", DbType.String, txtTitle.Text)
            db.AddInParameter(objCommand, "@Description", DbType.String, txtDescription.Text)

            If Not ddlCategory Is Nothing Then
                If ddlCategory.SelectedValue > 0 Then
                    db.AddInParameter(objCommand, "@CategoryID", DbType.Int32, Val(ddlCategory.SelectedValue))
                End If
            End If

            db.ExecuteNonQuery(objCommand)
        End If

    End Sub



    Private Sub RadGrid2_UpdateCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.UpdateCommand
        Dim txtCategoryID As TextBox = e.Item.FindControl("txtCategoryID")
        Dim txtNodeID As TextBox = New TextBox
        txtNodeID.text = Request("NODEID")
        Dim txtTitle As TextBox = e.Item.FindControl("txtCategoryTitle")

        'update database
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "LinkCategoriesUpdate"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)


        If txtCategoryID.Text = "" Then
            db.AddInParameter(objCommand, "@NodeID", DbType.Int32, Val(txtNodeID.Text))
            db.AddInParameter(objCommand, "@Title", DbType.String, txtTitle.Text)

            db.ExecuteNonQuery(objCommand)
        Else

            db.AddInParameter(objCommand, "@CategoryID", DbType.Int32, Val(txtCategoryID.Text))
            db.AddInParameter(objCommand, "@NodeID", DbType.Int32, Val(txtNodeID.Text))
            db.AddInParameter(objCommand, "@Title", DbType.String, txtTitle.Text)

            db.ExecuteNonQuery(objCommand)
        End If

    End Sub
End Class