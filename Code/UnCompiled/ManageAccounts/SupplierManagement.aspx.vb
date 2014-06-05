Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.DirectoryServices
Imports System.Web.Security

Partial Public Class SupplierManagement
    Inherits System.Web.UI.Page

    Dim datareader As SqlDataReader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        'login check
        Dim loginchk = New clsLoginCheck()

        If Not loginchk.LoggedIn Then
            Response.Redirect("../login.aspx")
        End If

        'Only Admins 
        If Not User.IsInRole("Admin") And Not User.IsInRole("Contractor Admin") Then
            If User.IsInRole("Member") Then
                Response.Redirect("../MemberHome.aspx")
            Else
                Response.Redirect("../Blank.aspx")
            End If

        End If

        If Not IsPostBack Then
            If Not Request("RoleName") Is Nothing Then
                lblRoleName.Text = Request("RoleName")
            End If
            LoadRoles()
        End If
    End Sub

    Protected Sub LoadRoles()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "RoleSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@RoleTypeID", DbType.Int32, 1)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        ddlRoleSearch.DataSource = datareader
        ddlRoleSearch.DataBind()
        ddlRoleSearch.Items.Insert(0, New ListItem("All", ""))

        If Not lblRoleName.Text = "" Then
            ddlRoleSearch.SelectedValue = lblRoleName.Text
        Else
            ddlRoleSearch.SelectedIndex = 0
        End If
    End Sub

    Private Sub LoadUsersList()

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "UserManagementListSelect"
        Dim objcommand As DbCommand = db.GetStoredProcCommand(sqlcommand)

        If Not ddlRoleSearch.SelectedIndex = 0 Then
            db.AddInParameter(objcommand, "@RoleName", DbType.String, ddlRoleSearch.SelectedValue)
        End If
        'add disabled checkbox
        If cbShowDisabled.Checked Then
            db.AddInParameter(objcommand, "@ShowOnlyDisabled", DbType.Int32, 1)
        End If
        If Not txtSearchTerm.Text = "" Then
            db.AddInParameter(objcommand, "@Text", DbType.String, txtSearchTerm.Text)
            db.AddInParameter(objcommand, "@Type", DbType.Int32, ddlSearchType.SelectedValue)
        End If

        ' db.AddInParameter(objcommand, "@SuppliersOnly", DbType.String, "1")
        datareader = db.ExecuteReader(objcommand)
        RadGrid1.DataSource = datareader
    End Sub

    Private Sub RadGrid1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadGrid1.DataBound
        datareader.Close()
    End Sub

    '''''Disabled temporarily...will be put back when time allows JM (1)
    'Protected Sub LoadRolesFilter()
    '    Dim allRoles As String() = Roles.GetAllRoles()
    '     Get the list of roles in the system and how many users belong to each role

    '    Dim strRoleFilterSelected = ddlRoleFilter.SelectedValue
    '    ddlRoleFilter.Items.Clear()
    '    For Each roleName As String In allRoles
    '        ddlRoleFilter.Items.Insert(0, New ListItem(roleName, roleName))
    '    Next

    '    ddlRoleFilter.Items.Insert(0, New ListItem("Any Role", "Any Role"))
    '    If Not (ddlRoleFilter.Items.FindByText(strRoleFilterSelected) Is Nothing) Then
    '        ddlRoleFilter.SelectedValue = strRoleFilterSelected
    '    Else
    '        ddlRoleFilter.SelectedValue = "Any Role"
    '    End If
    '    LoadUsersList()
    '    LoadMemberList()
    '    RadGrid1.DataBind()
    '    rgdMember.DataBind()
    'End Sub


    'Private Sub ddlRoleFilter_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRoleFilter.SelectedIndexChanged (1)
    '    RadGrid1.CurrentPageIndex = 0
    '    LoadUsersList()
    '    RadGrid1.DataBind()
    'End Sub

    Private Sub RadGrid1_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        LoadUsersList()
    End Sub

    Private Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound

        ''Adds the Edit link to the first columns and appends the personid per row
        'Dim lblEdit As Label = e.Item.FindControl("lblEdit")
        'If Not lblEdit Is Nothing Then
        '    lblEdit.Text = "<a href='SupplierManagementUpdate.aspx?UserName=" + lblUserName.Text + "'>Edit</a>"
        'End If

        Dim lblUserName As Label = e.Item.FindControl("lblUserName")
        If TypeOf e.Item Is GridNestedViewItem Then
            lblUserName = e.Item.FindControl("lblUserNameValue")
            Dim lblUserRoles As Label = e.Item.FindControl("lblUserRoles")
            If Not lblUserRoles Is Nothing Then
                'Dim strUserRoles() As String = Roles.GetRolesForUser(lblUserName.Text)
                Dim RoleNames As String = ""

                For Each role In Roles.GetRolesForUser(lblUserName.Text)
                    If Not role.EndsWith("Case") Then
                        If Not RoleNames = "" Then
                            RoleNames = RoleNames + ", " + role
                        Else
                            RoleNames = RoleNames + role
                        End If
                    End If
                Next
                If RoleNames = "" Then
                    lblUserRoles.Text = "NONE"
                Else
                    lblUserRoles.Text = RoleNames
                End If


                'If strUserRoles.Length = 0 Then
                '    lblUserRoles.Text = "NONE"
                'Else
                '    lblUserRoles.Text = ""
                '    For i = 0 To strUserRoles.Length - 1
                '        If i > 0 Then
                '            lblUserRoles.Text = lblUserRoles.Text + ",&nbsp;"
                '        End If
                '        lblUserRoles.Text = lblUserRoles.Text + strUserRoles(i)
                '    Next
                'End If
            End If

            'Load First Time Federal Contactor
            Dim lblContractor As Label = e.Item.FindControl("lblContractor")
            If Not lblContractor Is Nothing Then
                Dim lblContractorViewVal As Label = e.Item.FindControl("lblContractorViewVal")
                If lblContractor.Text = "1" Then
                    lblContractorViewVal.Text = "Yes"
                ElseIf lblContractor.Text = "0" Then
                    lblContractorViewVal.Text = "No"
                Else
                    lblContractorViewVal.Text = " "
                End If
            End If

            'Load First Teim VA Contractor
            Dim lblVAContractor As Label = e.Item.FindControl("lblVAContractor")
            If Not lblVAContractor Is Nothing Then
                Dim lblVAContractorViewVal As Label = e.Item.FindControl("lblVAContractorViewVal")
                If lblVAContractor.Text = "1" Then
                    lblVAContractorViewVal.Text = "Yes"
                ElseIf lblVAContractor.Text = "0" Then
                    lblVAContractorViewVal.Text = "No"
                Else
                    lblVAContractorViewVal.Text = " "
                End If
            End If

            'Loads businesstype data in the nested table view
            Dim MembershipUser As MembershipUser = Membership.GetUser(lblUserName.Text)
            Dim UserID As String = MembershipUser.ProviderUserKey.ToString()

            Dim db2 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommand2 As String = "PersonBusinessTypeSelect"
            Dim objCommand2 As DbCommand = db2.GetStoredProcCommand(sqlcommand2)
            db2.AddInParameter(objCommand2, "@UserID", DbType.String, UserID)
            Dim datareader2 As SqlDataReader = db2.ExecuteReader(objCommand2)

            Dim lblBusinessTypeViewValue As Label = e.Item.FindControl("lblBusinessTypeViewValue")
            While datareader2.Read
                If Not lblBusinessTypeViewValue Is Nothing Then
                    lblBusinessTypeViewValue.Text += datareader2("BusinessType") + "<br />"
                End If
            End While
            datareader2.Close()

        End If
    End Sub

    Protected Sub RadTabStrip1_TabClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTabStripEventArgs) Handles RadTabStrip1.TabClick
        If RadTabStrip1.SelectedTab.Text = "Supplier Accounts" Then
            'LoadRolesFilter() (1)
        End If
    End Sub

    Protected Sub btnActiveDeactive_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btnActivateDeactivate As Button = sender
        If btnActivateDeactivate.Text = "Deactivate" Then
            btnActivateDeactivate.Text = "Activate"
        Else
            btnActivateDeactivate.Text = "Deactivate"
        End If
    End Sub

    Public Sub btnSearchSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearchSubmit.Click

        RadGrid1.MasterTableView.CurrentPageIndex = 0
        RadGrid1.Rebind()

    End Sub

    Public Sub btnSearchClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchClear.Click
        txtSearchTerm.Text = ""
        'ddlRoleSearch.SelectedIndex = 0
        RadGrid1.Rebind()
    End Sub

    Private Sub RadGrid1_EditCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.EditCommand
        Dim lblUserName As Label = e.Item.FindControl("lblUserName")
        Session("UserName") = lblUserName.Text
        Response.Redirect("SupplierManagementUpdate.aspx")
    End Sub

    Private Sub cbShowDisabled_CheckedChanged(sender As Object, e As System.EventArgs) Handles cbShowDisabled.CheckedChanged
        RadGrid1.Rebind()
    End Sub

    Private Sub ddlRoleSearch_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlRoleSearch.SelectedIndexChanged
        RadGrid1.Rebind()
    End Sub
End Class