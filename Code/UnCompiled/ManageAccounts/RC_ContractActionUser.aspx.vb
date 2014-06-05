Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports Telerik.Web.UI
Imports RC.data.CollectionClasses
Imports RC.data.EntityClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports SD.LLBLGen.Pro.DQE
Imports RC.data.HelperClasses
Imports RC.data

Public Class RC_ContractActionUser
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'login check
        Dim loginchk = New clsLoginCheck()
        If (Not loginchk.LoggedIn) Then
            Response.Redirect("../login.aspx")
        End If

        'Node Check
        loginchk.GetNodePermission(Val(Request("NODEID")))
        If Not loginchk.HasView Then
            If Not loginchk.HasModuleAdmin Then
                Response.Redirect("../Blank.aspx")
            End If
        End If
        ' Dim objLoginCheck As New clsLoginCheck

        If Not IsPostBack Then

            'RESOLVED: MIGRATION PERMISSIONS
            'If Not (clsRCContractActionFunctions.hasSpecialAdminPermissions(Session("LoginID")) Or clsCheckPermissions.HasSuperGroupManagePermission(Session("LoginID"))) Then
            'If Not (clsRCContractActionFunctions.hasSpecialAdminPermissions(Session("LoginID"))) Then
            If Not User.IsInRole("Admin") Then
                If Not loginchk.HasModuleAdmin Then
                    Response.Redirect("RC_ContractActionList.aspx?NodeID=" & Val(Request("NodeID")))
                End If
            End If
            'End If

            'Admin
            loadLists(0, lstAdminUsers, lstAdminUsersAll, "ALL", True)

            'Headquarters
            loadLists(RCRegionIDs.Headquarters, lstHeadquarters, lstHeadquartersAll, "ALL")

            'North East
            loadLists(RCRegionIDs.North_East, lstNorthEast, lstNorthEastAll, "ALL")

            'North West
            loadLists(RCRegionIDs.North_West, lstNorthWest, lstNorthWestAll, "ALL")

            'South West
            loadLists(RCRegionIDs.South_West, lstSouthWest, lstSouthWestAll, "ALL")

            'South East
            loadLists(RCRegionIDs.South_East, lstSouthEast, lstSouthEastAll, "ALL")

        End If

    End Sub

    Private Sub loadLists(ByVal RCRegionID As Integer, ByVal lstCurrent As RadListBox, ByVal lstAll As RadListBox, ByVal SortBy As String, Optional ByVal Admin As Boolean = False)

        'get all Region member LoginIDs
        Dim lgenUsersCollection As New RccontractActionUserCollection
        If Admin Then
            lgenUsersCollection.GetMulti(New PredicateExpression(RccontractActionUserFields.Admin = 1))
        Else
            lgenUsersCollection.GetMulti(New PredicateExpression(RccontractActionUserFields.RcregionId = RCRegionID))
        End If

        'make list off all Region member LoginIDs
        Dim LoginIDs As New List(Of Integer)
        For Each E As RccontractActionUserEntity In lgenUsersCollection
            LoginIDs.Add(getValue(E.LoginId))
        Next

        'Get users for that region
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "uspSelRCContractActionListUser"
        Dim objcommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objcommand, "@RCRegionID", DbType.Int32, RCRegionID)
        db.AddInParameter(objcommand, "@ListType", DbType.String, "Region")
        db.AddInParameter(objcommand, "@SortType", DbType.String, SortBy)

        Dim datareader As SqlDataReader = db.ExecuteReader(objcommand)

        lstCurrent.DataSource = datareader
        lstCurrent.DataBind()


        'get all user LoginIDs sorted by last name that aren't assigned to the region already
        Dim db2 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand2 As String = "uspSelRCContractActionListUser"
        Dim objcommand2 As DbCommand = db2.GetStoredProcCommand(sqlcommand2)
        db2.AddInParameter(objcommand2, "@RCRegionID", DbType.Int32, RCRegionID)
        db2.AddInParameter(objcommand2, "@ListType", DbType.String, "All")
        db2.AddInParameter(objcommand2, "@SortType", DbType.String, SortBy)

        Dim datareader2 As SqlDataReader = db2.ExecuteReader(objcommand2)

        lstAll.DataSource = datareader2
        lstAll.DataBind()

    End Sub

    Private Sub setSortLabel(ByVal Sender As LinkButton, ByRef label As Label)
        Sender.Font.Overline = False
        If Sender.CommandName <> "ALL" Then
            Sender.Font.Overline = True
        End If

        label.Text = Sender.CommandName

    End Sub

    'Private Sub loadAdminLists()

    '    'get all Admin LoginIDs
    '    Dim lgenAdminUsers As New RccontractActionUserCollection
    '    lgenAdminUsers.GetMulti(New PredicateExpression(RccontractActionUserFields.Admin = 1))

    '    'make list off all Admin LoginIDs
    '    Dim LoginIDs As New List(Of Integer)
    '    For Each E As RccontractActionUserEntity In lgenAdminUsers
    '        LoginIDs.Add(getValue(E.LoginId))
    '    Next

    '    'get all user LoginIDs sorted by last name
    '    Dim lgenAllUsers As New UserInfoCollection
    '    lgenAllUsers.GetMulti(Nothing, 0, New SortExpression(UserInfoFields.LastName Or SortOperator.Ascending))

    '    'create default view
    '    Dim UsersView As EntityView(Of UserInfoEntity)
    '    UsersView = lgenAllUsers.DefaultView

    '    'bind all Admins to list box
    '    UsersView.Filter = New PredicateExpression(New FieldCompareRangePredicate(UserInfoFields.LoginId, LoginIDs))
    '    lstAdminUsers.DataSource = UsersView
    '    lstAdminUsers.DataBind()

    '    'bind all users, except admins, to list box
    '    UsersView.Filter = New PredicateExpression(Not (New FieldCompareRangePredicate(UserInfoFields.LoginId, LoginIDs)))
    '    lstAdminUsersAll.DataSource = UsersView
    '    lstAdminUsersAll.DataBind()

    'End Sub

#Region " Admin "

    Private Sub Admin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Admin_ALL.Click, Admin_A.Click, Admin_B.Click, Admin_C.Click, Admin_D.Click, Admin_E.Click, Admin_F.Click, Admin_G.Click, Admin_H.Click, Admin_I.Click, Admin_J.Click, Admin_K.Click, Admin_L.Click, Admin_M.Click, Admin_N.Click, Admin_O.Click, Admin_P.Click, Admin_Q.Click, Admin_R.Click, Admin_S.Click, Admin_T.Click, Admin_U.Click, Admin_V.Click, Admin_W.Click, Admin_X.Click, Admin_Y.Click, Admin_Z.Click

        setSortLabel(sender, lblAdmin_SortCommand)
        loadLists(0, lstAdminUsers, lstAdminUsersAll, lblAdmin_SortCommand.Text, True)

    End Sub

    Private Sub btnAdminAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdminAdd.Click
        If lstAdminUsersAll.SelectedValue <> "" Then
            addUser(lstAdminUsersAll.SelectedValue, 0, 1)
            loadLists(0, lstAdminUsers, lstAdminUsersAll, lblAdmin_SortCommand.Text, True)
        End If
    End Sub

    Private Sub btnAdminRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdminRemove.Click
        If lstAdminUsers.SelectedValue <> "" Then
            removeUser(lstAdminUsers.SelectedValue, 0, 1)
            loadLists(0, lstAdminUsers, lstAdminUsersAll, lblAdmin_SortCommand.Text, True)
        End If
    End Sub

#End Region

#Region " Headquarters "

    Private Sub HQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HQ_ALL.Click, HQ_A.Click, HQ_B.Click, HQ_C.Click, HQ_D.Click, HQ_E.Click, HQ_F.Click, HQ_G.Click, HQ_H.Click, HQ_I.Click, HQ_J.Click, HQ_K.Click, HQ_L.Click, HQ_M.Click, HQ_N.Click, HQ_O.Click, HQ_P.Click, HQ_Q.Click, HQ_R.Click, HQ_S.Click, HQ_T.Click, HQ_U.Click, HQ_V.Click, HQ_W.Click, HQ_X.Click, HQ_Y.Click, HQ_Z.Click

        setSortLabel(sender, lblHQ_SortCommand)
        loadLists(RCRegionIDs.Headquarters, lstHeadquarters, lstHeadquartersAll, lblHQ_SortCommand.Text)

    End Sub

    Private Sub btnHeadquartersAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHeadquartersAdd.Click
        If lstHeadquartersAll.SelectedValue <> "" Then
            addUser(lstHeadquartersAll.SelectedValue, RCRegionIDs.Headquarters)
            loadLists(RCRegionIDs.Headquarters, lstHeadquarters, lstHeadquartersAll, lblHQ_SortCommand.Text)
        End If
    End Sub

    Private Sub btnHeadquartersRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHeadquartersRemove.Click
        If lstHeadquarters.SelectedValue <> "" Then
            removeUser(lstHeadquarters.SelectedValue, RCRegionIDs.Headquarters)
            loadLists(RCRegionIDs.Headquarters, lstHeadquarters, lstHeadquartersAll, lblHQ_SortCommand.Text)
        End If
    End Sub

#End Region

#Region " North East "

    Private Sub NE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NE_ALL.Click, NE_A.Click, NE_B.Click, NE_C.Click, NE_D.Click, NE_E.Click, NE_F.Click, NE_G.Click, NE_H.Click, NE_I.Click, NE_J.Click, NE_K.Click, NE_L.Click, NE_M.Click, NE_N.Click, NE_O.Click, NE_P.Click, NE_Q.Click, NE_R.Click, NE_S.Click, NE_T.Click, NE_U.Click, NE_V.Click, NE_W.Click, NE_X.Click, NE_Y.Click, NE_Z.Click

        setSortLabel(sender, lblNE_SortCommand)
        loadLists(RCRegionIDs.North_East, lstNorthEast, lstNorthEastAll, lblNE_SortCommand.Text)

    End Sub

    Private Sub btnNorthEastAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNorthEastAdd.Click
        If lstNorthEastAll.SelectedValue <> "" Then
            addUser(lstNorthEastAll.SelectedValue, RCRegionIDs.North_East)
            loadLists(RCRegionIDs.North_East, lstNorthEast, lstNorthEastAll, lblNE_SortCommand.Text)
        End If
    End Sub

    Private Sub btnNorthEastRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNorthEastRemove.Click
        If lstNorthEast.SelectedValue <> "" Then
            removeUser(lstNorthEast.SelectedValue, RCRegionIDs.North_East)
            loadLists(RCRegionIDs.North_East, lstNorthEast, lstNorthEastAll, lblNE_SortCommand.Text)
        End If
    End Sub

#End Region

#Region " North West "

    Private Sub NW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NW_ALL.Click, NW_A.Click, NW_B.Click, NW_C.Click, NW_D.Click, NW_E.Click, NW_F.Click, NW_G.Click, NW_H.Click, NW_I.Click, NW_J.Click, NW_K.Click, NW_L.Click, NW_M.Click, NW_N.Click, NW_O.Click, NW_P.Click, NW_Q.Click, NW_R.Click, NW_S.Click, NW_T.Click, NW_U.Click, NW_V.Click, NW_W.Click, NW_X.Click, NW_Y.Click, NW_Z.Click

        setSortLabel(sender, lblNW_SortCommand)
        loadLists(RCRegionIDs.North_West, lstNorthWest, lstNorthWestAll, lblNW_SortCommand.Text)

    End Sub

    Private Sub btnNorthWestAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNorthWestAdd.Click
        If lstNorthWestAll.SelectedValue <> "" Then
            addUser(lstNorthWestAll.SelectedValue, RCRegionIDs.North_West)
            loadLists(RCRegionIDs.North_West, lstNorthWest, lstNorthWestAll, lblNW_SortCommand.Text)
        End If
    End Sub

    Private Sub btnNorthWestRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNorthWestRemove.Click
        If lstNorthWest.SelectedValue <> "" Then
            removeUser(lstNorthWest.SelectedValue, RCRegionIDs.North_West)
            loadLists(RCRegionIDs.North_West, lstNorthWest, lstNorthWestAll, lblNW_SortCommand.Text)
        End If
    End Sub

#End Region

#Region " South West "

    Private Sub SW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SW_ALL.Click, SW_A.Click, SW_B.Click, SW_C.Click, SW_D.Click, SW_E.Click, SW_F.Click, SW_G.Click, SW_H.Click, SW_I.Click, SW_J.Click, SW_K.Click, SW_L.Click, SW_M.Click, SW_N.Click, SW_O.Click, SW_P.Click, SW_Q.Click, SW_R.Click, SW_S.Click, SW_T.Click, SW_U.Click, SW_V.Click, SW_W.Click, SW_X.Click, SW_Y.Click, SW_Z.Click

        setSortLabel(sender, lblSW_SortCommand)
        loadLists(RCRegionIDs.South_West, lstSouthWest, lstSouthWestAll, lblSW_SortCommand.Text)

    End Sub

    Private Sub btnSouthWestAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSouthWestAdd.Click
        If lstSouthWestAll.SelectedValue <> "" Then
            addUser(lstSouthWestAll.SelectedValue, RCRegionIDs.South_West)
            loadLists(RCRegionIDs.South_West, lstSouthWest, lstSouthWestAll, lblSW_SortCommand.Text)
        End If
    End Sub

    Private Sub btnSouthWestRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSouthWestRemove.Click
        If lstSouthWest.SelectedValue <> "" Then
            removeUser(lstSouthWest.SelectedValue, RCRegionIDs.South_West)
            loadLists(RCRegionIDs.South_West, lstSouthWest, lstSouthWestAll, lblSW_SortCommand.Text)
        End If
    End Sub

#End Region

#Region " South East "

    Private Sub SE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SE_ALL.Click, SE_A.Click, SE_B.Click, SE_C.Click, SE_D.Click, SE_E.Click, SE_F.Click, SE_G.Click, SE_H.Click, SE_I.Click, SE_J.Click, SE_K.Click, SE_L.Click, SE_M.Click, SE_N.Click, SE_O.Click, SE_P.Click, SE_Q.Click, SE_R.Click, SE_S.Click, SE_T.Click, SE_U.Click, SE_V.Click, SE_W.Click, SE_X.Click, SE_Y.Click, SE_Z.Click

        setSortLabel(sender, lblSE_SortCommand)
        loadLists(RCRegionIDs.South_East, lstSouthEast, lstSouthEastAll, lblSE_SortCommand.Text)

    End Sub

    Private Sub btnSouthEastAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSouthEastAdd.Click
        If lstSouthEastAll.SelectedValue <> "" Then
            addUser(lstSouthEastAll.SelectedValue, RCRegionIDs.South_East)
            loadLists(RCRegionIDs.South_East, lstSouthEast, lstSouthEastAll, lblSE_SortCommand.Text)
        End If
    End Sub

    Private Sub btnSouthEastRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSouthEastRemove.Click
        If lstSouthEast.SelectedValue <> "" Then
            removeUser(lstSouthEast.SelectedValue, RCRegionIDs.South_East)
            loadLists(RCRegionIDs.South_East, lstSouthEast, lstSouthEastAll, lblSE_SortCommand.Text)
        End If
    End Sub

#End Region

    Private Sub addUser(ByVal LoginID As Integer, ByVal RCRegionID As Integer, Optional ByVal Admin As Integer = 0)

        Dim NewEntity As New RccontractActionUserEntity
        NewEntity.LoginId = LoginID
        NewEntity.RcregionId = RCRegionID
        NewEntity.Admin = Admin
        NewEntity.Save()

    End Sub

    Private Sub removeUser(ByVal LoginID As Integer, ByVal RCRegionID As Integer, Optional ByVal Admin As Integer = 0)

        Dim filter As New PredicateExpression(RccontractActionUserFields.LoginId = LoginID)

        If RCRegionID > 0 Then
            filter.AddWithAnd(RccontractActionUserFields.RcregionId = RCRegionID)
        Else
            filter.AddWithAnd(RccontractActionUserFields.RcregionId = 0 Or RccontractActionUserFields.RcregionId = DBNull.Value)
        End If

        If Admin > 0 Then
            filter.AddWithAnd(RccontractActionUserFields.Admin = Admin)
        Else
            filter.AddWithAnd(RccontractActionUserFields.Admin = 0 Or RccontractActionUserFields.Admin = DBNull.Value)
        End If

        Dim collection As New RccontractActionUserCollection
        collection.DeleteMulti(filter)

    End Sub

    Private Function getValue(ByVal value As Object)

        If value Is Nothing Then
            Return 0
        Else
            Return value
        End If

    End Function

    Enum RCRegionIDs As Integer

        North_East = 1
        North_West = 2
        South_East = 3
        South_West = 4
        Headquarters = 5

    End Enum

    'Private Sub btnReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReturn.Click
    '    Response.Redirect("RC_ContractActionList.aspx?NodeID=" & Val(Request("NodeID")))
    'End Sub

End Class