Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.DirectoryServices
Imports System.Web.Security

Partial Public Class RoleManagement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        'login check
        Dim loginchk = New clsLoginCheck()

        'Only Admins 
        If Not User.IsInRole("Admin") And Not User.IsInRole("Contractor Admin") Then
            Response.Redirect("../Blank.aspx")
        End If

        If Not IsPostBack Then
            Dim intVersion As Integer = 0

            Dim rtsAdmin As RadTabStrip = Master.FindControl("rtsAdmin")
            rtsAdmin.Tabs(0).Selected = True
        End If
    End Sub

    Private Sub rgRoles_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgRoles.NeedDataSource
        ' Create a DataTable and define its columns
        Dim RoleList As New DataTable()
        RoleList.Columns.Add("Role Name")
        RoleList.Columns.Add("User Count")
        RoleList.Columns.Add("RoleDataID")
        RoleList.Columns.Add("RoleDescription")
        Dim allRoles As String() = Roles.GetAllRoles()

        ' Get the list of roles in the system and how many users belong to each role
        For Each roleName As String In allRoles
            Dim numberOfUsersInRole As Integer = Roles.GetUsersInRole(roleName).Length
            Dim intRoleCategory As Integer = 0
            Dim intRoleData As Integer = 0
            Dim strRoleDescription As String = ""
            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommand As String = "RoleTypeSelectbyName"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
            db.AddInParameter(objCommand, "@RoleName", DbType.String, roleName)
            Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
            If datareader.HasRows Then
                Do While datareader.Read()
                    If Not (datareader("RoleCategoryID") Is System.DBNull.Value) Then
                        intRoleCategory = datareader("RoleCategoryID")
                    End If
                    If Not (datareader("RoleDataID") Is System.DBNull.Value) Then
                        intRoleData = datareader("RoleDataID")
                    End If
                    If Not (datareader("RoleDescription") Is System.DBNull.Value) Then
                        strRoleDescription = datareader("RoleDescription")
                    End If
                Loop
            End If
            datareader.Close()

            'Contractor Admin users do not have permission to any of the Federal roles.
            If Not User.IsInRole("Admin") Then
                If roleName = "Federal Program Manager" Or roleName = "Federal Project Lead" Or roleName = "Federal" Or roleName = "Division Chief" Or roleName = "Core Team" Or roleName = "COR" Or roleName = "Admin" Then
                    'Do Nothing
                Else
                    Dim roleRow As String() = {roleName, numberOfUsersInRole.ToString(), intRoleData, strRoleDescription} 'intRoleType, intRoleCategory,
                    RoleList.Rows.Add(roleRow)
                End If
            Else
                Dim roleRow As String() = {roleName, numberOfUsersInRole.ToString(), intRoleData, strRoleDescription} 'intRoleType, intRoleCategory,
                RoleList.Rows.Add(roleRow)
            End If
        Next

        ' Bind the DataTable to the GridView
        rgRoles.DataSource = RoleList
        ' Clears form field after a role was successfully added. Alternative to redirect technique I often use.
        txtNewRole.Text = ""
    End Sub

    Private Sub rgRoles_UpdateCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgRoles.UpdateCommand

        Dim lblRoleName As Label = e.Item.FindControl("lblRoleName")
        Dim txtRoleDescription As TextBox = e.Item.FindControl("txtRoleDescription")

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "RoleDataUpdate"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@RoleName", DbType.String, lblRoleName.Text)
        db.AddInParameter(objCommand, "@RoleDescription", DbType.String, txtRoleDescription.Text)
        db.ExecuteNonQuery(objCommand)

        rgRoles.Rebind()
    End Sub

    Private Sub rgRoles_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgRoles.ItemDataBound

        If Not TypeOf e.Item Is GridEditFormItem Then
            Dim lblRoleName As Label = e.Item.FindControl("lblRoleName")
            Dim btnDelete As Button = e.Item.FindControl("btnDeleteRole")
            Dim lblUserCount As Label = e.Item.FindControl("lblUserCount")

            If Not lblUserCount Is Nothing Then
                If lblUserCount.Text > 0 Then
                    btnDelete.Visible = False
                End If
            End If

            If Not lblRoleName Is Nothing Then
                ' and other coded to roles to keep them from being deleted
                If lblRoleName.Text = "Admin" Or lblRoleName.Text = "Member" Then
                    btnDelete.Enabled = False
                    btnDelete.Visible = False
                End If
            End If
        End If

        If TypeOf e.Item Is GridDataItem Then

            Dim lblUserCount As Label = e.Item.FindControl("lblUserCount")
            Dim lblRoleName As Label = e.Item.FindControl("lblRoleName")
            Dim lblViewPermissions As Label = e.Item.FindControl("lblViewPermissions")
            Dim intCount As Integer = Val(lblUserCount.Text)

            If Not intCount = 0 Then
                'lblUserCount.Text = "<a href='MemberManagement.aspx?RoleName=" + lblRoleName.Text + "'>" + intCount.ToString + "</a>"
                lblUserCount.Text = intCount.ToString
            End If
            Dim strRoleName As String = lblRoleName.Text.Replace(" ", "_")
            lblViewPermissions.Text = "<a href=""RoleAccessReview.aspx?RoleName=" & strRoleName & """>View Permissions</a>"
        End If

        If TypeOf e.Item Is GridNestedViewItem Then
            Dim lblUsers As Label = e.Item.FindControl("lblUsers")
            Dim lblRoleNameNested As Label = e.Item.FindControl("lblRoleNameNested")
            If Not lblRoleNameNested Is Nothing Then
                'Roles.GetUsersInRole(lblRoleNameNested.Text)
                For Each Name In Roles.GetUsersInRole(lblRoleNameNested.Text)
                    If Not lblUsers.Text = "" Then
                        lblUsers.Text = lblUsers.Text + ",<br/>" + Name
                    Else
                        lblUsers.Text = Name
                    End If

                Next
            End If

        End If

    End Sub

    Private Sub rgRoles_DeleteCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgRoles.DeleteCommand
        ' currently not enabled
        Dim lblRoleName As Label = e.Item.FindControl("lblRoleName")
        Dim lblRoleDataID As Label = e.Item.FindControl("lblRoleDataID")

        Try
            Roles.DeleteRole(lblRoleName.Text)
            ConfirmationMessage.InnerText = "Role '" + lblRoleName.Text + "' was deleted."

            'Delete the matching roledata row
            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommand As String = "RoleDataDelete"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
            db.AddInParameter(objCommand, "@RoleDataId", DbType.Int32, lblRoleDataID.Text)
            db.ExecuteNonQuery(objCommand)
        Catch ex As Exception
            ConfirmationMessage.InnerText = lblRoleName.Text + " : " + ex.Message
        End Try
        'Display the failure message in a client-side alert box
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "DeleteRoleError", String.Format("alert('{0}');", ConfirmationMessage.InnerText.Replace("'", "\'")), True)
        rgRoles.Rebind()
    End Sub

    Protected Sub RadTabStrip1_TabClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTabStripEventArgs) Handles RadTabStrip1.TabClick
        If RadTabStrip1.SelectedTab.Text = "Roles" Then
            rgRoles.Rebind()
        End If
    End Sub

    Private Sub btnAddRole_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRole.Click
        'roles cannot contain _ as  " " is converted to _ when passed through the url
        'Roles ending with Case are created as new cases are entered 
        If Not txtNewRole.Text.Contains("_") And Not txtNewRole.Text.Trim.EndsWith("Case", StringComparison.OrdinalIgnoreCase) Then
            Try
                Roles.CreateRole(txtNewRole.Text)
                ConfirmationMessage.InnerText = "The new role was added."

                'Save RoleType of the new role
                Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
                Dim sqlcommand As String = "RoleDataInsert"
                Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
                db.AddInParameter(objCommand, "@RoleName", DbType.String, txtNewRole.Text)
                db.AddInParameter(objCommand, "@RoleTypeID", DbType.Int32, 1)
                db.AddInParameter(objCommand, "@RoleCategoryID", DbType.Int32, 4)
                db.AddInParameter(objCommand, "@RoleDescription", DbType.String, txtDescription.Text)
                db.ExecuteNonQuery(objCommand)

                rgRoles.Rebind()
            Catch ex As Exception
                'capture ex message and change to more simple text to be more user friendly
                'captured exception trimmed various lengths to allow for valid string comparison.  Exception contained invalid character to compare.
                Dim strErrorMsg As String = ex.Message

                If Left(strErrorMsg, 43) = "The parameter 'roleName' must not be empty." Then
                    ConfirmationMessage.InnerText = "Must enter a role name to add."
                ElseIf Left(strErrorMsg, 77) = "The parameter 'roleName' is too long: it must not exceed 256 chars in length." Then
                    ConfirmationMessage.InnerText = "The role name is too long."
                ElseIf Left(strErrorMsg, 49) = "The parameter 'roleName' must not contain commas." Then
                    ConfirmationMessage.InnerText = "The role cannot contain commas."
                Else
                    ConfirmationMessage.InnerText = strErrorMsg
                End If

            End Try
            Dim strMessage As String = ConfirmationMessage.InnerText.Trim.Replace("'", "\'").Replace(vbCr, " ").Replace(vbLf, " ")
            'Display the failure message in a client-side alert box
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "AddRoleError", String.Format("alert('{0}');", strMessage), True)
            pnlNewRole.Visible = False
            txtDescription.Text = ""
            btnCreateRole.Visible = True
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "AddRoleError", String.Format("alert('{0}');", "Role Name cannot contain  ""_"" or end with ""Case""."), True)
        End If

    End Sub

    Private Sub btnCreateRole_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateRole.Click
        pnlNewRole.Visible = True
        btnCreateRole.Visible = False

    End Sub

    Private Sub btnCancelRole_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelRole.Click
        pnlNewRole.Visible = False
        btnCreateRole.Visible = True
        txtDescription.Text = ""
        txtNewRole.Text = ""
    End Sub
End Class