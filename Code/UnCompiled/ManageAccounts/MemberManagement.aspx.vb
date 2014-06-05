Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.DirectoryServices
Imports System.Web.Security

Partial Public Class MemberManagement
    Inherits System.Web.UI.Page

    Dim bUserCheck As Boolean = False
    Dim datareader As SqlDataReader
    Dim intItemIndex As Integer
    Dim blnPage As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        'login check
        Dim loginchk = New clsLoginCheck()

        'Only Admins 
        If Not User.IsInRole("Admin") And Not User.IsInRole("Contractor Admin") Then
            Response.Redirect("../Blank.aspx")
        End If

        If Not IsPostBack Then

            If Not Request("RoleName") Is Nothing Then
                lblRoleName.Text = Request("RoleName")
            End If

            Dim intVersion As Integer = 0
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

    Private Sub LoadMemberList()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "UserManagementListSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)

        If Not ddlRoleSearch.SelectedIndex = 0 Then
            db.AddInParameter(objCommand, "@RoleName", DbType.String, ddlRoleSearch.SelectedValue)
        End If

        If Not txtSearchTerm.Text = "" Then
            db.AddInParameter(objCommand, "@Text", DbType.String, txtSearchTerm.Text)
            db.AddInParameter(objCommand, "@Type", DbType.Int32, ddlSearchType.SelectedValue)
        End If

        db.AddInParameter(objCommand, "@MembersOnly", DbType.String, "1")
        datareader = db.ExecuteReader(objCommand)
        rgdMember.DataSource = datareader

    End Sub

    Private Sub rgdMember_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgdMember.DataBound
        'If Not Request.QueryString("UserName") <> "" Then
        datareader.Close()
        'End If
    End Sub

    Private Sub rgdMember_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdMember.NeedDataSource
        LoadMemberList()
    End Sub

    Private Sub rgdMember_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgdMember.ItemDataBound
        Dim lblPersonID As Label = e.Item.FindControl("lblPersonID")

        Dim lblEdit As Label = e.Item.FindControl("lblEdit")
        If Not lblEdit Is Nothing Then
            lblEdit.Text = "<a href='MemberManagementUpdate.aspx?PersonID=" + lblPersonID.Text + "'>Edit</a>"
        End If

        If TypeOf e.Item Is GridNestedViewItem Then
            Dim lblUserName As Label = e.Item.FindControl("lblUserNameValue")
            Dim lblUserRoles As Label = e.Item.FindControl("lblUserRoles")
            If Not lblUserRoles Is Nothing Then
                Dim strUserRoles() As String = Roles.GetRolesForUser(lblUserName.Text)
                If strUserRoles.Length = 0 Then
                    lblUserRoles.Text = "NONE"
                Else
                    lblUserRoles.Text = ""
                    For i = 0 To strUserRoles.Length - 1
                        If i > 0 Then
                            lblUserRoles.Text = lblUserRoles.Text + ",&nbsp;"
                        End If
                        lblUserRoles.Text = lblUserRoles.Text + strUserRoles(i)
                    Next
                End If
            End If
        End If
    End Sub

    Public Sub btnSearchSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearchSubmit.Click
        rgdMember.MasterTableView.CurrentPageIndex = 0
        rgdMember.Rebind()

    End Sub

    Private Sub rgdMember_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgdMember.PreRender
        If Not True Then  'this code at this time will never be used  left for reference when correct paging is fixed

            'Determine how many pages (maxPageSize) the radGrid will have
            Dim maxPageSize As Integer
            Dim ds As DataSet
            Dim loginchk As New clsLoginCheck()

            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommand As String = "PersonUserSelect"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)

            db.AddInParameter(objCommand, "@RoleName", DbType.String, "Member")
            db.AddInParameter(objCommand, "@Text", DbType.String, "")
            db.AddInParameter(objCommand, "@Type", DbType.Int32, 1)
            ds = db.ExecuteDataSet(objCommand)
            maxPageSize = ds.Tables(0).Rows.Count / 20

            If Request.QueryString("UserName") <> "" And Session("SelectedIndex") <> "Clear" Then

                Dim strUserName = Request.QueryString("UserName")

                Dim item As GridItem
                Dim intPage As Integer
                While blnPage <> 2 And intPage <= maxPageSize
                    For Each item In rgdMember.MasterTableView.Items
                        If item.ItemType = GridItemType.Item Or item.ItemType = GridItemType.AlternatingItem Then
                            If strUserName = item.OwnerTableView.DataKeyValues(item.ItemIndex)("UserName") Then
                                item.Selected = True
                                item.Focus()
                                item.Selected = False
                                intItemIndex = item.ItemIndex
                                blnPage = 2
                                Exit While
                            Else
                                blnPage = 1
                            End If

                        End If

                    Next

                    'Calculate which page the row is o
                    If blnPage = 1 Then
                        intItemIndex += 20
                    End If
                    intPage = Convert.ToInt32(Decimal.Truncate(intItemIndex / 20))

                    rgdMember.MasterTableView.CurrentPageIndex = intPage

                    'LoadMemberList()
                    '                    rgdMember.Rebind()

                    'datareader.Close()
                End While
            End If
        End If
    End Sub

    Private Sub rgdMember_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgdMember.PageIndexChanged
        Session("SelectedIndex") = "Clear"
    End Sub

    Private Sub btnSearchClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchClear.Click
        txtSearchTerm.Text = ""
        ddlRoleSearch.SelectedIndex = 0
        rgdMember.Rebind()
    End Sub
End Class