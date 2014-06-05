Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.DirectoryServices
Imports System.Web.Security

Partial Public Class MemberCreate
    Inherits System.Web.UI.Page

    'Private intPersonID As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' login check
        Dim loginchk = New clsLoginCheck()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        ' redirect if not logged in or not admin
        If (Not loginchk.LoggedIn) Then
            Response.Redirect("../login.aspx")
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
                Response.Redirect("../Blank.aspx")
            ElseIf Not loginchk.HasModuleAdmin() Then ' must have Admin permissions on the NODE
                Response.Redirect("../Blank.aspx")

            End If
            Dim intVersion As Integer = 0

            ClearNewMemberInfo()
            CheckRequestVariable()
            'LoadRolesFilter() (1)
            'LoadMemberFields()

        End If
    End Sub
    Protected Sub CheckRequestVariable()
        If Not Request("AccessRequestID") Is Nothing Then
            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommand As String = "AccessRequestEmailSelect"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
            db.AddInParameter(objCommand, "@AccessRequestId", DbType.Int32, Request("AccessRequestID"))
            Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
            If datareader.HasRows Then
                Do While datareader.Read()
                    txtAddMember.Text = datareader("Email")
                Loop
            End If
            datareader.Close()

            LoadUserData()
        End If
    End Sub

    'Protected Sub LoadReviewerType(ByRef ddlReviewerType As DropDownList, ByRef lblReviewerTypeID As Label)
    '    Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommand As String = "IdeaReviewerTypeSelect"
    '    Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
    '    Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
    '    ddlReviewerType.DataSource = datareader
    '    ddlReviewerType.DataBind()
    '    If Not (lblReviewerTypeID Is Nothing Or lblReviewerTypeID.Text.Length = 0) Then
    '        ddlReviewerType.SelectedValue = Val(lblReviewerTypeID.Text)
    '    Else
    '        ddlReviewerType.SelectedValue = "1"
    '    End If
    'End Sub

    'Private Sub LoadMemberFields()
    'load reviewer type ddl
    'this function can populate with user selection of a value is pulled from the database to pass in
    'lblEmpty.Text = ""
    'If Not ddlReviewerTypeNew Is Nothing Then
    '    LoadReviewerType(ddlReviewerTypeNew, lblEmpty)
    'End If

    ''load roles
    'If Not rlbUserRolesNew Is Nothing Then
    '    Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommand As String = "RoleSelect"
    '    Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
    '    db.AddInParameter(objCommand, "@RoleTypeID", DbType.Int32, 1)
    '    Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
    '    rlbUserRolesNew.DataSource = datareader
    '    rlbUserRolesNew.DataBind()

    '    rlbUserRolesNew.FindItemByText("Member").Checked = True
    '    rlbUserRolesNew.FindItemByText("Member").Enabled = False

    'End If

    ''Load Person Challenges
    'If Not rlbChallengeNew Is Nothing Then

    '    Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommand As String = "IdeaChallengeSelect"
    '    Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
    '    Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)

    '    rlbChallengeNew.DataSource = datareader
    '    rlbChallengeNew.DataBind()

    'End If
    'End Sub



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

    'Function CheckRole(ByVal rlbRole As RadListBox)
    '    If rlbRole.FindItemByText("Admin").Checked = True Then
    '        If (rlbRole.FindItemByText("ContractOfficer").Checked = True Or rlbRole.FindItemByText("IdeaManager").Checked = True Or rlbRole.FindItemByText("IdeaReviewer").Checked = True Or rlbRole.FindItemByText("IdeaReader").Checked = True) Then
    '            Return False
    '        Else
    '            Return True
    '        End If
    '    ElseIf rlbRole.FindItemByText("ContractOfficer").Checked = True Then
    '        If (rlbRole.FindItemByText("IdeaManager").Checked = True Or rlbRole.FindItemByText("IdeaReviewer").Checked = True Or rlbRole.FindItemByText("IdeaReader").Checked = True) Then
    '            Return False
    '        Else
    '            Return True
    '        End If
    '    ElseIf rlbRole.FindItemByText("IdeaManager").Checked = True Then
    '        If (rlbRole.FindItemByText("IdeaReviewer").Checked = True Or rlbRole.FindItemByText("IdeaReader").Checked = True) Then
    '            Return False
    '        Else
    '            Return True
    '        End If
    '    End If
    '    Return True
    'End Function

    Protected Sub ClearNewMemberInfo()
        lblDeptOrgNameVal.Text = ""
        lblTitleVal.Text = ""
        lblFirstNameVal.Text = ""
        lblLastNameVal.Text = ""
        lblEmailVal.Text = ""
        lblCityVal.Text = ""
        lblZipCodeVal.Text = ""
        lblAddress1Val.Text = ""
        lblPhoneVal.Text = ""
        lblCompanyNameVal.Text = ""
        lblStateVal.Text = ""
        lblNewMemberUserName.Text = ""
        lblNoUser.Visible = False
        lblDupEmail.Visible = False
        lblDupUserName.Visible = False
        lblDupBoth.Visible = False
        pnlInfo.Visible = False
        txtAddMember.Text = ""
    End Sub

    Private Sub LoadUserData()
        'Clear User Data 
        lblDeptOrgNameVal.Text = ""
        lblTitleVal.Text = ""
        lblFirstNameVal.Text = ""
        lblLastNameVal.Text = ""
        lblEmailVal.Text = ""
        lblCityVal.Text = ""
        lblZipCodeVal.Text = ""
        lblAddress1Val.Text = ""
        lblPhoneVal.Text = ""
        lblCompanyNameVal.Text = ""
        lblStateVal.Text = ""
        lblNewMemberUserName.Text = ""
        lblNoUser.Visible = False
        lblDupEmail.Visible = False
        lblDupUserName.Visible = False
        lblDupBoth.Visible = False
        pnlInfo.Visible = False
        Dim bfound As Boolean = False

        Dim objADSUser As New clsADSUser
        objADSUser.GetUserByEmail(txtAddMember.Text)

        If objADSUser.UserFound = True Then
            bfound = True
            lblNewMemberUserName.Text = objADSUser.ADUserName
            lblDeptOrgNameVal.Text = objADSUser.DeptOrgName
            lblTitleVal.Text = objADSUser.UserTitle
            lblFirstNameVal.Text = objADSUser.FirstName
            lblLastNameVal.Text = objADSUser.LastName
            lblEmailVal.Text = objADSUser.Email
            lblCityVal.Text = objADSUser.City
            lblZipCodeVal.Text = objADSUser.ZipCode
            lblAddress1Val.Text = objADSUser.Address
            lblPhoneVal.Text = objADSUser.Phone
            lblCompanyNameVal.Text = objADSUser.CompanyName
            lblStateVal.Text = objADSUser.StateVal

            Dim bInSystem As Boolean = False
            'check if username is in system
            Dim objUN As New clsASPUser
            objUN.GetUserByName(lblNewMemberUserName.Text)

            If objUN.UserFound Then
                'Username is already in the system
                pnlInfo.Visible = False
                pnlNoUser.Visible = True
                lblDupUserName.Visible = True
                txtAddMember.Text = lblEmailVal.Text
                bInSystem = True
            End If

            'check if email is in system
            Dim objEm As New clsASPUser
            objEm.GetUserByEmail(lblEmailVal.Text)

            If objEm.UserFound Then
                'email is already in the system
                pnlInfo.Visible = False
                pnlNoUser.Visible = True
                lblDupEmail.Visible = True
                txtAddMember.Text = lblEmailVal.Text
                bInSystem = True
            End If

            If (objEm.UserFound) And (objUN.UserFound) Then
                ClientScript.RegisterStartupScript(Me.GetType(), "CreateUserError", String.Format("alert('{0}');", "The Email Entered and matching UserName already exists in the system."), True)
                lblPersonID.Text = objUN.PersonID.ToString
                btnEditExistingUser.Visible = True
                pnlEditExistingUser.Visible = True
                lblDupBoth.Visible = True
                lblDupEmail.Visible = False
                lblDupUserName.Visible = False
            ElseIf objEm.UserFound Then
                ClientScript.RegisterStartupScript(Me.GetType(), "CreateUserError", String.Format("alert('{0}');", "The email Entered already exists in the system."), True)
                lblPersonID.Text = objEm.PersonID.ToString
                btnEditExistingUser.Visible = True
                pnlEditExistingUser.Visible = True
            ElseIf objUN.UserFound Then
                ClientScript.RegisterStartupScript(Me.GetType(), "CreateUserError", String.Format("alert('{0}');", "The User Name Entered already exists in the system."), True)
                lblPersonID.Text = objUN.PersonID.ToString
                btnEditExistingUser.Visible = True
                pnlEditExistingUser.Visible = True
            Else
                lblPersonID.Text = "0"
                btnEditExistingUser.Visible = False
                pnlEditExistingUser.Visible = False
            End If


            If bInSystem = False Then
                pnlInfo.Visible = True
                pnlNoUser.Visible = False
                'pnlPassword.Visible = True
                txtAddMember.Text = lblEmailVal.Text
            End If
        Else
            pnlInfo.Visible = False
            pnlNoUser.Visible = True
            lblNoUser.Visible = True
            ClientScript.RegisterStartupScript(Me.GetType(), "CreateUserError", String.Format("alert('{0}');", "No user was found in Active Directory with this email address."), True)
            txtAddMember.Text = lblEmailVal.Text
            lblPersonID.Text = "0"
            btnEditExistingUser.Visible = False
            pnlEditExistingUser.Visible = False
        End If

    End Sub

    Private Sub btnAddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        LoadUserData()
    End Sub

    Private Sub btnSaveMember_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveMember.Click
        
        Dim objADE As New clsADSUser
        objADE.GetUserByEmail(lblEmailVal.Text)
        objADE.CreateUser()
        Dim UserID As String = objADE.UserId
        Dim intPersonID As Integer = objADE.PersonID

        Dim muUser As MembershipUser = Membership.GetUser(objADE.ADUserName)

        'Save auth status
        muUser.IsApproved = rblIsAuthorizedNewMember.SelectedValue
        Membership.UpdateUser(muUser)

        'Find PersonID

        '' Add Roles.
        'For Each rolebox As Telerik.Web.UI.RadListBoxItem In rlbUserRolesNew.Items
        '    If rolebox.Checked = True Then
        '        If Roles.IsUserInRole(lblNewMemberUserName.Text, rolebox.Text) = False Then
        '            Roles.AddUserToRole(lblNewMemberUserName.Text, rolebox.Text)
        '        End If
        '    End If
        'Next

        'set email url
        Dim emURL As String = ""
        emURL = System.Configuration.ConfigurationManager.AppSettings("AdminURL").ToString()

        '' Add Challenges by UserID.
        'For Each challengebox As Telerik.Web.UI.RadListBoxItem In rlbChallengeNew.Items
        '    If challengebox.Checked = True Then
        '        Dim dbPerson As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        '        Dim sqlcommandPerson As String = "PersonChallengeInsert"
        '        Dim objCommandPerson As DbCommand = dbPerson.GetStoredProcCommand(sqlcommandPerson)
        '        dbPerson.AddInParameter(objCommandPerson, "@UserId", DbType.String, UserID)
        '        dbPerson.AddInParameter(objCommandPerson, "@ChallengeID", DbType.Int32, challengebox.Value)
        '        dbPerson.ExecuteNonQuery(objCommandPerson)
        '    End If
        'Next

        '' Save reviewer type 
        'Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        'Dim sqlcommand As String = "PersonReviewerTypeUpdate"
        'Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        'db.AddInParameter(objCommand, "@UserId", DbType.String, UserID)
        'If Not ddlReviewerTypeNew Is Nothing Then
        '    If ddlReviewerTypeNew.SelectedValue > 0 Then
        '        db.AddInParameter(objCommand, "@IdeaReviewerTypeID", DbType.Int32, ddlReviewerTypeNew.SelectedValue)
        '    Else
        '        db.AddInParameter(objCommand, "@IdeaReviewerTypeID", DbType.Int32, 1)
        '    End If
        'End If
        'db.ExecuteNonQuery(objCommand)

        pnlSuccess.Visible = True
        pnlInfo.Visible = False

        'Hide Input fields and show completion text
        lblDisplayUserName.Text = "User " + lblNewMemberUserName.Text + " has been been added to the system."
        Response.Redirect("MemberManagementUpdate.aspx" + "?PersonID=" + intPersonID.ToString)
        lblDisplayUserName.Visible = True
        pnlSearch.Visible = False

    End Sub

    Private Sub btnComplete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnComplete.Click
        txtAddMember.Text = ""
        pnlSearch.Visible = True
        pnlSuccess.Visible = False
    End Sub

    Private Sub btnUserAccountCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUserAccountCancel.Click
        pnlInfo.Visible = False
        ClearNewMemberInfo()
    End Sub

    Private Sub btnEditExistingUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditExistingUser.Click
        If Not lblPersonID.Text = "0" Then
            Response.Redirect("MemberManagementUpdate.aspx" + "?PersonID=" + lblPersonID.Text)
        End If
    End Sub
End Class