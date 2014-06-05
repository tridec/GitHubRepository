Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.DirectoryServices
Imports System.Web.Security

Partial Public Class MemberManagementUpdate
    Inherits System.Web.UI.Page

    Dim bUserCheck As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim loginchk = New clsLoginCheck()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Session("SelectedIndex") = ""

        If Not IsPostBack Then
            Dim intPersonID As Integer = Val(Request("PersonID"))
            SetUserName(intPersonID)
            CheckUser()
            LoadUserInfo()
        End If

    End Sub

    Protected Sub SetUserName(ByVal intPersonID As Integer)
        'returns the username based in the personid passed to the page
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "PersonByPersonIDSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@PersonID", DbType.Int32, intPersonID)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        If datareader.HasRows Then
            While datareader.Read
                lblUserNameVal.Text = datareader("UserName")
            End While
        End If
        datareader.Close()
    End Sub

    Protected Sub LoadUserInfo()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Load User Information
        ''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim MembershipUser As MembershipUser = Membership.GetUser(lblUserNameVal.Text)
        Dim UserID As String = MembershipUser.ProviderUserKey.ToString()

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "PersonUserSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@UserId", DbType.String, UserID)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)

        If datareader.HasRows Then
            Do While datareader.Read()
                If Not (datareader("Email") Is System.DBNull.Value) Then
                    lblEmailVal.Text = datareader("Email")
                    txtEmail.Text = datareader("Email")
                End If
                If Not (datareader("UserName") Is System.DBNull.Value) Then
                    lblUserNameVal.Text = datareader("UserName")
                End If
                If Not (datareader("CompanyName") Is System.DBNull.Value) Then
                    lblCompanyNameVal.Text = datareader("CompanyName")
                End If
                If Not (datareader("DeptOrgName") Is System.DBNull.Value) Then
                    lblDeptOrgNameVal.Text = datareader("DeptOrgName")
                End If
                If Not (datareader("FirstName") Is System.DBNull.Value) Then
                    lblFirstNameVal.Text = datareader("FirstName")
                End If
                If Not (datareader("LastName") Is System.DBNull.Value) Then
                    lblLastNameVal.Text = datareader("LastName")
                End If
                If Not (datareader("Title") Is System.DBNull.Value) Then
                    lblTitleVal.Text = datareader("Title")
                End If
                If Not (datareader("Address1") Is System.DBNull.Value) Then
                    lblAddress1Val.Text = datareader("Address1")
                End If
                If Not (datareader("City") Is System.DBNull.Value) Then
                    lblCityVal.Text = datareader("City")
                End If
                If Not (datareader("StateName") Is System.DBNull.Value) Then
                    lblStateVal.Text = datareader("StateName")
                End If
                If Not (datareader("ZipCode") Is System.DBNull.Value) Then
                    lblZipCodeVal.Text = datareader("ZipCode")
                End If
                If Not (datareader("PhoneNumber") Is System.DBNull.Value) Then
                    lblPhoneVal.Text = datareader("PhoneNumber")
                End If
                If Not (datareader("IdeaReviewerTypeID") Is System.DBNull.Value) Then
                    lblReviewerTypeID.Text = datareader("IdeaReviewerTypeID")
                End If
                If Not (datareader("IsApproved") Is System.DBNull.Value) Then
                    rblIsAuthorizedMember.SelectedValue = datareader("IsApproved")
                End If
            Loop
        End If
        datareader.Close()

        ''Call Load of Reviewer Drop Down List
        'If Not ddlReviewerType Is Nothing Then
        '    LoadReviewerType(ddlReviewerType, lblReviewerTypeID)
        'End If

        ''Load Person Challenges
        'If Not rlbChallenge Is Nothing Then

        '    Dim db1 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        '    Dim sqlcommand1 As String = "IdeaChallengeSelect"
        '    Dim objCommand1 As DbCommand = db1.GetStoredProcCommand(sqlcommand1)
        '    Dim datareader1 As SqlDataReader = db1.ExecuteReader(objCommand1)

        '    rlbChallenge.DataSource = datareader1
        '    rlbChallenge.DataBind()

        '    Dim db2 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        '    Dim sqlcommand2 As String = "PersonChallengeSelect"
        '    Dim objCommand2 As DbCommand = db2.GetStoredProcCommand(sqlcommand2)
        '    db2.AddInParameter(objCommand2, "@UserID", DbType.String, UserID)
        '    Dim datareader2 As SqlDataReader = db2.ExecuteReader(objCommand2)

        '    While datareader2.Read
        '        If Not rlbChallenge.FindItemByValue(datareader2("ChallengeID")) Is Nothing Then
        '            rlbChallenge.FindItemByValue(datareader2("ChallengeID")).Checked = True
        '        End If
        '    End While
        '    datareader2.Close()
        'End If

        ''Load Requiring Activities
        'If Not rlbRequiringActivity Is Nothing Then
        '    Dim db1 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        '    Dim sqlcommand1 As String = "AcqRequestRequiringActivitySelect"
        '    Dim objCommand1 As DbCommand = db1.GetStoredProcCommand(sqlcommand1)
        '    Dim datareader1 As SqlDataReader = db1.ExecuteReader(objCommand1)

        '    rlbRequiringActivity.DataSource = datareader1
        '    rlbRequiringActivity.DataBind()

        '    Dim db2 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        '    Dim sqlcommand2 As String = "PersonRequiringActivitySelect"
        '    Dim objCommand2 As DbCommand = db2.GetStoredProcCommand(sqlcommand2)
        '    db2.AddInParameter(objCommand2, "@UserID", DbType.String, UserID)
        '    Dim datareader2 As SqlDataReader = db2.ExecuteReader(objCommand2)

        '    While datareader2.Read
        '        If Not rlbRequiringActivity.FindItemByValue(datareader2("AcqRequestRequiringActivityID")) Is Nothing Then
        '            rlbRequiringActivity.FindItemByValue(datareader2("AcqRequestRequiringActivityID")).Checked = True
        '        End If
        '    End While

        '    datareader2.Close()


        'End If

        ''Load Major initiatives
        'If Not rlbMI Is Nothing Then

        '    Dim db1 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        '    Dim sqlcommand1 As String = "AcqRequestVAInitiativesSelectAll"
        '    Dim objCommand1 As DbCommand = db1.GetStoredProcCommand(sqlcommand1)
        '    Dim datareader1 As SqlDataReader = db1.ExecuteReader(objCommand1)

        '    rlbMI.DataSource = datareader1
        '    rlbMI.DataBind()

        '    Dim db2 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        '    Dim sqlcommand2 As String = "PersonInitiativeSelect"
        '    Dim objCommand2 As DbCommand = db2.GetStoredProcCommand(sqlcommand2)
        '    db2.AddInParameter(objCommand2, "@UserID", DbType.String, UserID)
        '    Dim datareader2 As SqlDataReader = db2.ExecuteReader(objCommand2)

        '    While datareader2.Read
        '        If Not rlbMI.FindItemByValue(datareader2("AcqRequestVAInitiativesID")) Is Nothing Then
        '            rlbMI.FindItemByValue(datareader2("AcqRequestVAInitiativesID")).Checked = True
        '        End If
        '    End While
        '    datareader2.Close()
        'End If

        ''Load Person Topics
        'If Not rlbTopic Is Nothing Then

        '    Dim db1 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        '    Dim sqlcommand1 As String = "InnovationTopicSelect"

        '    Dim objCommand1 As DbCommand = db1.GetStoredProcCommand(sqlcommand1)
        '    Dim datareader1 As SqlDataReader = db1.ExecuteReader(objCommand1)

        '    rlbTopic.DataSource = datareader1
        '    rlbTopic.DataBind()

        '    Dim db2 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        '    Dim sqlcommand2 As String = "PersonTopicSelect"
        '    Dim objCommand2 As DbCommand = db2.GetStoredProcCommand(sqlcommand2)
        '    db2.AddInParameter(objCommand2, "@UserID", DbType.String, UserID)
        '    Dim datareader2 As SqlDataReader = db2.ExecuteReader(objCommand2)

        '    While datareader2.Read
        '        If Not rlbTopic.FindItemByValue(datareader2("InnovationTopicID")) Is Nothing Then
        '            rlbTopic.FindItemByValue(datareader2("InnovationTopicID")).Checked = True
        '        End If
        '       End While
        '    datareader2.Close()
        'End If

        ''Load Innovation Panel list box
        'If Not rlbInnovationPanel Is Nothing Then

        '    Dim db1 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        '    Dim sqlcommand1 As String = "InternalInnovationPanelSelect"
        '    Dim objCommand1 As DbCommand = db1.GetStoredProcCommand(sqlcommand1)
        '    db1.AddInParameter(objCommand1, "@Active", DbType.Int32, 0)
        '    Dim datareader1 As SqlDataReader = db1.ExecuteReader(objCommand1)

        '    rlbInnovationPanel.DataSource = datareader1
        '    rlbInnovationPanel.DataBind()

        '    Dim db2 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        '    Dim sqlcommand2 As String = "PersonInternalInnovationPanelSelect"
        '    Dim objCommand2 As DbCommand = db2.GetStoredProcCommand(sqlcommand2)
        '    db2.AddInParameter(objCommand2, "@UserID", DbType.String, UserID)
        '    Dim datareader2 As SqlDataReader = db2.ExecuteReader(objCommand2)

        '    While datareader2.Read
        '        If Not rlbInnovationPanel.FindItemByValue(datareader2("InternalInnovationPanelID")) Is Nothing Then
        '            rlbInnovationPanel.FindItemByValue(datareader2("InternalInnovationPanelID")).Checked = True
        '        End If
        '    End While
        '    datareader2.Close()
        'End If

        'Load Proposal types
        If Not rlbProposalsType Is Nothing Then

            Dim db1 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommand1 As String = "ProposalsTypeSelect"
            Dim objCommand1 As DbCommand = db1.GetStoredProcCommand(sqlcommand1)
            db1.AddInParameter(objCommand1, "@Admin", DbType.Int32, 1)
            db1.AddInParameter(objCommand1, "@ActiveOnly", DbType.Int32, 0)
            Dim datareader1 As SqlDataReader = db1.ExecuteReader(objCommand1)

            rlbProposalsType.DataSource = datareader1
            rlbProposalsType.DataBind()

            Dim db2 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommand2 As String = "PersonProposalsTypeSelect"
            Dim objCommand2 As DbCommand = db2.GetStoredProcCommand(sqlcommand2)
            db2.AddInParameter(objCommand2, "@UserID", DbType.String, UserID)
            Dim datareader2 As SqlDataReader = db2.ExecuteReader(objCommand2)

            While datareader2.Read
                If Not rlbProposalsType.FindItemByValue(datareader2("ProposalsTypeID")) Is Nothing Then
                    rlbProposalsType.FindItemByValue(datareader2("ProposalsTypeID")).Checked = True
                End If
            End While
            datareader2.Close()
        End If

        LoadRoles(4, 1)
        pnlReviewer.Visible = False
        pnlMajorInitiative.Visible = False
        pnlInnovationReviewer.Visible = False
        pnlRequiringActivity.Visible = False
        pnlInternalInnovationPanel.Visible = False
        pnlProposalsType.Visible = False

    End Sub

    Protected Sub CheckUser()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Active Directory Search
        ''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objAD As New clsADSUser
        objAD.GetUserByName(lblUserNameVal.Text)

        If objAD.UserFound Then
            Dim objADE As New clsADSUser
            objADE.GetUserByEmail(objAD.Email)
            If Not String.Equals(objAD.ADUserName, objADE.ADUserName, StringComparison.OrdinalIgnoreCase) Then
                objADE.InvalidateEmail()
            End If
            objAD.Update()
            txtEmail.Text = objAD.Email
            lblUserNameVal.Text = objAD.ADUserName
        Else 'AD not found
            Dim objADE2 As New clsADSUser
            objADE2.GetUserByEmail(objAD.NetEmail)
            If objADE2.UserFound Then
                objADE2.Update()
                txtEmail.Text = objADE2.Email
                lblUserNameVal.Text = objADE2.ADUserName
            Else
                'Invalid User(Deactivate/Notify Admin)
                Dim strErrorMessage As String = lblUserNameVal.Text
                Dim strArray As String() = Split(strErrorMessage, "\")
                If strArray.Length > 1 Then
                    'Error message to let the user know the current username has no record in active directory
                    ClientScript.RegisterStartupScript(Me.GetType(), "User not found", String.Format("alert('{0}');", "No Matching Active Directory Information for " + strArray(0) + "\\" + strArray(1) + ""), True)
                Else
                    ClientScript.RegisterStartupScript(Me.GetType(), "User not found", String.Format("alert('{0}');", "No Matching Active Directory Information for " + strErrorMessage + ""), True)
                End If
            End If
        End If
    End Sub

    Protected Sub LoadRoles(ByVal CategoryID As Integer, ByVal RoleTypeID As Integer)

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "RolesByCategorySelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@RoleCategoryID", DbType.Int32, CategoryID)
        db.AddInParameter(objCommand, "@RoleTypeID", DbType.Int32, RoleTypeID)
        Dim datareader2 As SqlDataReader = db.ExecuteReader(objCommand)
        rlbUserRoles.DataSource = datareader2
        rlbUserRoles.DataBind()

        Dim userRoles As String() = Roles.GetRolesForUser(lblUserNameVal.Text)
        For Each role As String In userRoles
            If Not rlbUserRoles.FindItemByText(role) Is Nothing Then
                rlbUserRoles.FindItemByText(role).Checked = True
            End If
        Next

        If Not rlbUserRoles.FindItemByText("Member") Is Nothing Then
            rlbUserRoles.FindItemByText("Member").Checked = True
            rlbUserRoles.FindItemByText("Member").Enabled = False
        End If

    End Sub

    Protected Sub LoadReviewerType(ByRef ddlReviewerType As DropDownList, ByRef lblReviewerTypeID As Label)
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "IdeaReviewerTypeSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        ddlReviewerType.DataSource = datareader
        ddlReviewerType.DataBind()
        If Not (lblReviewerTypeID Is Nothing Or lblReviewerTypeID.Text.Length = 0) Then
            ddlReviewerType.SelectedValue = Val(lblReviewerTypeID.Text)
        Else
            ddlReviewerType.SelectedValue = "1"
        End If
    End Sub

    Private Sub rtsRoles_TabClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTabStripEventArgs) Handles rtsRoles.TabClick
        'Hide all pnls
        pnlReviewer.Visible = False
        pnlMajorInitiative.Visible = False
        pnlInnovationReviewer.Visible = False
        pnlRequiringActivity.Visible = False
        pnlInternalInnovationPanel.Visible = False
        pnlProposalsType.Visible = False

        'If rtsRoles.SelectedTab.Text = "Idea Portal" Then
        '    LoadRoles(1, 1)
        '    pnlReviewer.Visible = True
        'End If
        'If rtsRoles.SelectedTab.Text = "Customer Portal" Then
        '    LoadRoles(2, 1)
        '    pnlRequiringActivity.Visible = True
        'End If
        'If rtsRoles.SelectedTab.Text = "ECTOS" Then
        '    LoadRoles(3, 1)
        'End If
        If rtsRoles.SelectedTab.Text = "Other" Then
            LoadRoles(4, 1)
        End If
        'If rtsRoles.SelectedTab.Text = "Innovation Portal" Then
        '    LoadRoles(5, 1)
        '    pnlInnovationReviewer.Visible = True
        'End If
        'If rtsRoles.SelectedTab.Text = "Major Initiative" Then
        '    LoadRoles(6, 1)
        '    pnlMajorInitiative.Visible = True
        'End If
        'If rtsRoles.SelectedTab.Text = "Acquisition Tracker" Then
        '    LoadRoles(7, 1)
        'End If
        'If rtsRoles.SelectedTab.Text = "Internal Innovations" Then
        '    LoadRoles(8, 1)
        '    pnlInternalInnovationPanel.Visible = True
        'End If
        If rtsRoles.SelectedTab.Text = "Proposals" Then
            LoadRoles(9, 1)
            pnlProposalsType.Visible = True
        End If
        tdSubmit.Focus()
    End Sub

    Private Sub btnSaveAuth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveAuth.Click
        Dim MembershipUser As MembershipUser = Membership.GetUser(lblUserNameVal.Text)

        MembershipUser.IsApproved = rblIsAuthorizedMember.SelectedValue
        Membership.UpdateUser(MembershipUser)

        Dim strMessage As String = "Authorization status has been saved."
        ClientScript.RegisterStartupScript(Me.GetType(), "AddRoleError", String.Format("alert('{0}');", strMessage), True)
        tdRoles.Focus()
    End Sub

    Private Sub btnSaveRole_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveRole.Click
        'This button click handles the saving of roles and (challenges/reviewer type if on the idea portal tab)
        If rtsRoles.SelectedTab.Text = "Idea Portal" Then
            Dim MembershipUser As MembershipUser = Membership.GetUser(lblUserNameVal.Text)
            Dim UserID As String = MembershipUser.ProviderUserKey.ToString()

            ' Update reviewer type 
            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommand As String = "PersonReviewerTypeUpdate"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
            'Dim ddlReviewerType As DropDownList = e.Item.FindControl("ddlReviewerType")
            db.AddInParameter(objCommand, "@UserId", DbType.String, UserID)
            If Not ddlReviewerType Is Nothing Then
                If ddlReviewerType.SelectedValue > 0 Then
                    db.AddInParameter(objCommand, "@IdeaReviewerTypeID", DbType.Int32, ddlReviewerType.SelectedValue)
                Else
                    db.AddInParameter(objCommand, "@IdeaReviewerTypeID", DbType.Int32, 1)
                End If
            End If
            db.ExecuteNonQuery(objCommand)

            ' Delete Challenges by UserID, delete challenges before adding back changes
            Dim dbDel As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommandDel As String = "PersonChallengeDelete"
            Dim objCommandDel As DbCommand = dbDel.GetStoredProcCommand(sqlcommandDel)
            dbDel.AddInParameter(objCommandDel, "@UserId", DbType.String, UserID)
            dbDel.ExecuteNonQuery(objCommandDel)

            ' Add Challenges by UserID.
            'Dim rlbChallenge2 As Telerik.Web.UI.RadListBox = e.Item.FindControl("rlbChallenge")
            For Each challengebox As Telerik.Web.UI.RadListBoxItem In rlbChallenge.Items
                If challengebox.Checked = True Then
                    Dim dbPerson As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
                    Dim sqlcommandPerson As String = "PersonChallengeInsert"
                    Dim objCommandPerson As DbCommand = dbPerson.GetStoredProcCommand(sqlcommandPerson)
                    dbPerson.AddInParameter(objCommandPerson, "@UserId", DbType.String, UserID)
                    dbPerson.AddInParameter(objCommandPerson, "@ChallengeID", DbType.Int32, challengebox.Value)
                    dbPerson.ExecuteNonQuery(objCommandPerson)
                End If
            Next

            ' Update Roles
            If CheckRoleIP(rlbUserRoles) Then
                SaveRoles()
            Else
                'Don't save alert user
                Dim strMessage2 As String = "Roles cannot be saved because conflicting roles have been selected together."
                ClientScript.RegisterStartupScript(Me.GetType(), "AddRoleError", String.Format("alert('{0}');", strMessage2), True)
            End If
        ElseIf rtsRoles.SelectedTab.Text = "Customer Portal" Then
            Dim MembershipUser As MembershipUser = Membership.GetUser(lblUserNameVal.Text)
            Dim UserID As String = MembershipUser.ProviderUserKey.ToString()

            'Delete Requiring Activities by UserID
            Dim dbDel As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommandDel As String = "PersonRequiringActivityDelete"
            Dim objCommandDel As DbCommand = dbDel.GetStoredProcCommand(sqlcommandDel)
            dbDel.AddInParameter(objCommandDel, "@UserId", DbType.String, UserID)
            dbDel.ExecuteNonQuery(objCommandDel)

            'Add Requiring Activities checked
            For Each activitybox As Telerik.Web.UI.RadListBoxItem In rlbRequiringActivity.Items
                If activitybox.Checked = True Then
                    Dim dbPerson As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
                    Dim sqlcommandPerson As String = "PersonRequiringActivityInsert"
                    Dim objCommandPerson As DbCommand = dbPerson.GetStoredProcCommand(sqlcommandPerson)
                    dbPerson.AddInParameter(objCommandPerson, "@UserId", DbType.String, UserID)
                    dbPerson.AddInParameter(objCommandPerson, "@AcqRequestRequiringActivityID", DbType.Int32, activitybox.Value)
                    dbPerson.ExecuteNonQuery(objCommandPerson)
                End If
            Next


            SaveRoles()
        ElseIf rtsRoles.SelectedTab.Text = "ECTOS" Then
            SaveRoles()
        ElseIf rtsRoles.SelectedTab.Text = "Innovation Portal" Then
            Dim MembershipUser As MembershipUser = Membership.GetUser(lblUserNameVal.Text)
            Dim UserID As String = MembershipUser.ProviderUserKey.ToString()

            ' Delete Topic by UserID, delete Topics before adding back Topics
            Dim dbDel As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommandDel As String = "PersonTopicDelete"
            Dim objCommandDel As DbCommand = dbDel.GetStoredProcCommand(sqlcommandDel)
            dbDel.AddInParameter(objCommandDel, "@UserId", DbType.String, UserID)
            dbDel.ExecuteNonQuery(objCommandDel)

            ' Add Topics by UserID.
            For Each topicbox As Telerik.Web.UI.RadListBoxItem In rlbTopic.Items
                If topicbox.Checked = True Then
                    Dim dbPerson As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
                    Dim sqlcommandPerson As String = "PersonTopicInsert"
                    Dim objCommandPerson As DbCommand = dbPerson.GetStoredProcCommand(sqlcommandPerson)
                    dbPerson.AddInParameter(objCommandPerson, "@UserId", DbType.String, UserID)
                    dbPerson.AddInParameter(objCommandPerson, "@InnovationTopicID", DbType.Int32, topicbox.Value)
                    dbPerson.ExecuteNonQuery(objCommandPerson)
                End If
            Next

            SaveRoles()
        ElseIf rtsRoles.SelectedTab.Text = "Major Initiative" Then
            Dim MembershipUser As MembershipUser = Membership.GetUser(lblUserNameVal.Text)
            Dim UserID As String = MembershipUser.ProviderUserKey.ToString()

            ' Delete Initiative by UserID, delete Initiative before adding back Initiative
            Dim dbDel As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommandDel As String = "PersonInitiativeDelete"
            Dim objCommandDel As DbCommand = dbDel.GetStoredProcCommand(sqlcommandDel)
            dbDel.AddInParameter(objCommandDel, "@UserId", DbType.String, UserID)
            dbDel.ExecuteNonQuery(objCommandDel)

            ' Add Initiative by UserID.
            For Each topicbox As Telerik.Web.UI.RadListBoxItem In rlbMI.Items
                If topicbox.Checked = True Then
                    Dim dbPerson As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
                    Dim sqlcommandPerson As String = "PersonInitiativeInsert"
                    Dim objCommandPerson As DbCommand = dbPerson.GetStoredProcCommand(sqlcommandPerson)
                    dbPerson.AddInParameter(objCommandPerson, "@UserId", DbType.String, UserID)
                    dbPerson.AddInParameter(objCommandPerson, "@AcqRequestVAInitiativesID", DbType.Int32, topicbox.Value)
                    dbPerson.ExecuteNonQuery(objCommandPerson)
                End If
            Next
            SaveRoles()
        ElseIf rtsRoles.SelectedTab.Text = "Proposals" Then
            Dim MembershipUser As MembershipUser = Membership.GetUser(lblUserNameVal.Text)
            Dim UserID As String = MembershipUser.ProviderUserKey.ToString()

            ' Delete Initiative by UserID, delete Initiative before adding back Initiative
            Dim dbDel As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommandDel As String = "PersonProposalsTypeDelete"
            Dim objCommandDel As DbCommand = dbDel.GetStoredProcCommand(sqlcommandDel)
            dbDel.AddInParameter(objCommandDel, "@UserId", DbType.String, UserID)
            dbDel.ExecuteNonQuery(objCommandDel)

            ' Add Initiative by UserID.
            For Each topicbox As Telerik.Web.UI.RadListBoxItem In rlbProposalsType.Items
                If topicbox.Checked = True Then
                    Dim dbPerson As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
                    Dim sqlcommandPerson As String = "PersonProposalsTypeInsert"
                    Dim objCommandPerson As DbCommand = dbPerson.GetStoredProcCommand(sqlcommandPerson)
                    dbPerson.AddInParameter(objCommandPerson, "@UserId", DbType.String, UserID)
                    dbPerson.AddInParameter(objCommandPerson, "@ProposalsTypeID", DbType.Int32, topicbox.Value)
                    dbPerson.ExecuteNonQuery(objCommandPerson)
                End If
            Next
            SaveRoles()
        ElseIf rtsRoles.SelectedTab.Text = "Other" Then
            If CheckRoleOther(rlbUserRoles) Then
                SaveRoles()
            Else
                'Don't save alert user
                Dim strMessage3 As String = "User cannot be saved with role admin due to other conflicted roles previously set."
                ClientScript.RegisterStartupScript(Me.GetType(), "AddRoleError", String.Format("alert('{0}');", strMessage3), True)

            End If
        ElseIf rtsRoles.SelectedTab.Text = "Internal Innovations" Then
            Dim MembershipUser As MembershipUser = Membership.GetUser(lblUserNameVal.Text)
            Dim UserID As String = MembershipUser.ProviderUserKey.ToString()

            ' Delete Innovation Panel by UserID, delete Innovation Panel before adding back
            Dim dbDel As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommandDel As String = "PersonInternalInnovationPanelDelete"
            Dim objCommandDel As DbCommand = dbDel.GetStoredProcCommand(sqlcommandDel)
            dbDel.AddInParameter(objCommandDel, "@UserId", DbType.String, UserID)
            dbDel.ExecuteNonQuery(objCommandDel)

            ' Add Innovation Panel by UserID.
            For Each topicbox As Telerik.Web.UI.RadListBoxItem In rlbInnovationPanel.Items
                If topicbox.Checked = True Then
                    Dim dbPerson As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
                    Dim sqlcommandPerson As String = "PersonInternalInnovationPanelInsert"
                    Dim objCommandPerson As DbCommand = dbPerson.GetStoredProcCommand(sqlcommandPerson)
                    dbPerson.AddInParameter(objCommandPerson, "@UserId", DbType.String, UserID)
                    dbPerson.AddInParameter(objCommandPerson, "@InternalInnovationPanelID", DbType.Int32, topicbox.Value)
                    dbPerson.ExecuteNonQuery(objCommandPerson)
                End If
            Next
            SaveRoles()
        Else
            SaveRoles()
        End If

        Dim strMessage As String = "Roles have been saved."
        ClientScript.RegisterStartupScript(Me.GetType(), "AddRoleError", String.Format("alert('{0}');", strMessage), True)
        tdRoles.Focus()

    End Sub

    Public Sub SaveRoles()
        If Not rlbUserRoles Is Nothing Then
            For Each rolebox As Telerik.Web.UI.RadListBoxItem In rlbUserRoles.Items
                If rolebox.Checked = True Then
                    If Roles.IsUserInRole(lblUserNameVal.Text, rolebox.Text) = False Then
                        Roles.AddUserToRole(lblUserNameVal.Text, rolebox.Text)
                    End If
                Else
                    If Roles.IsUserInRole(lblUserNameVal.Text, rolebox.Text) = True Then
                        Roles.RemoveUserFromRole(lblUserNameVal.Text, rolebox.Text)
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub btnChangeEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangeEmail.Click
        EmailChange()
        txtEmail.Focus()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        bUserCheck = True
        pnlWarning.Visible = False
        btnChangeEmail.Enabled = True
        txtEmail.Enabled = True
        EmailChange()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        pnlWarning.Visible = False
        btnChangeEmail.Enabled = True
        txtEmail.Enabled = True
        txtEmail.Text = ""
    End Sub

    Public Sub EmailChange()
        '''''''''''''''''''''''''''''''''''
        'Create object of user being edited
        '''''''''''''''''''''''''''''''''''
        Dim ObjAd As New clsADSUser ' user you click to edit
        ObjAd.GetUserByName(lblUserNameVal.Text)

        Dim objADE As New clsADSUser 'user based on email typed in
        objADE.GetUserByEmail(txtEmail.Text)

        If objADE.UserFound Then
            If objADE.NetUserFound Then

                If String.Equals(lblUserNameVal.Text, objADE.ADUserName, StringComparison.OrdinalIgnoreCase) Then
                    objADE.NetUserName = ObjAd.NetUserName
                    objADE.Update()
                    lblUserNameVal.Text = objADE.ADUserName
                Else
                    'warn .net user found - talk to client for approval
                    If bUserCheck = False Then
                        ClientScript.RegisterStartupScript(Me.GetType(), "ADSearchError", String.Format("alert('{0}');", "In order to complete this action another user will be inactivated.  Confirmation is necessary to disable this conflicting user."), True)
                        pnlWarning.Visible = True
                        btnChangeEmail.Enabled = False
                        txtEmail.Enabled = False
                        'e.Canceled = True
                        Exit Sub
                    Else
                        Dim objAD2 As New clsADSUser
                        objAD2.GetUserByName(objADE.ADUserName)

                        objADE.InvalidateEmail()

                        'check ADUserName
                        If objAD2.NetUserFound Then
                            objAD2.InvalidateUserName()
                        End If
                        Dim strtest As String = objADE.Email
                        objADE.NetUserName = ObjAd.NetUserName
                        objADE.Update()
                        lblUserNameVal.Text = objADE.ADUserName
                    End If
                End If

            Else 'email not found in .net
                'check if ADE UserName is in .net
                If String.Equals(lblUserNameVal.Text, objADE.ADUserName, StringComparison.OrdinalIgnoreCase) Then
                    objADE.NetUserName = ObjAd.NetUserName
                    objADE.Update()
                    lblUserNameVal.Text = objADE.ADUserName
                Else
                    Dim objAD3 As New clsADSUser
                    objAD3.GetUserByName(objADE.ADUserName)

                    If objAD3.NetUserFound Then
                        If bUserCheck = False Then
                            ClientScript.RegisterStartupScript(Me.GetType(), "ADSearchError", String.Format("alert('{0}');", "In order to complete this action another user will be inactivated.  Confirmation is necessary to disable this conflicting user."), True)
                            pnlWarning.Visible = True
                            btnChangeEmail.Enabled = False
                            txtEmail.Enabled = False
                            'e.Canceled = True
                            Exit Sub
                        Else
                            objAD3.InvalidateUserName()
                        End If
                    End If

                    objAD3.NetUserName = ObjAd.NetUserName
                    objAD3.Update()
                    lblUserNameVal.Text = objAD3.ADUserName
                End If
            End If
        Else
            'email supplied is not in active directory
            ClientScript.RegisterStartupScript(Me.GetType(), "ADSearchError", String.Format("alert('{0}');", "The Email Entered is not in Active Directory"), True)
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Reload newly saved user data from the person table to the edit form
        ''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim MembershipUser As MembershipUser = Membership.GetUser(lblUserNameVal.Text)
        Dim UserID As String = MembershipUser.ProviderUserKey.ToString()

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "PersonUserSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@UserId", DbType.String, UserID)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)

        If datareader.HasRows Then
            Do While datareader.Read()
                If Not (datareader("Email") Is System.DBNull.Value) Then
                    lblEmailVal.Text = datareader("Email")
                End If
                If Not (datareader("UserName") Is System.DBNull.Value) Then
                    lblUserNameVal.Text = datareader("UserName")
                End If
                If Not (datareader("CompanyName") Is System.DBNull.Value) Then
                    lblCompanyNameVal.Text = datareader("CompanyName")
                End If
                If Not (datareader("DeptOrgName") Is System.DBNull.Value) Then
                    lblDeptOrgNameVal.Text = datareader("DeptOrgName")
                End If
                If Not (datareader("FirstName") Is System.DBNull.Value) Then
                    lblFirstNameVal.Text = datareader("FirstName")
                End If
                If Not (datareader("LastName") Is System.DBNull.Value) Then
                    lblLastNameVal.Text = datareader("LastName")
                End If
                If Not (datareader("Title") Is System.DBNull.Value) Then
                    lblTitleVal.Text = datareader("Title")
                End If
                If Not (datareader("Address1") Is System.DBNull.Value) Then
                    lblAddress1Val.Text = datareader("Address1")
                End If
                If Not (datareader("City") Is System.DBNull.Value) Then
                    lblCityVal.Text = datareader("City")
                End If
                If Not (datareader("StateName") Is System.DBNull.Value) Then
                    lblStateVal.Text = datareader("StateName")
                End If
                If Not (datareader("ZipCode") Is System.DBNull.Value) Then
                    lblZipCodeVal.Text = datareader("ZipCode")
                End If
                If Not (datareader("PhoneNumber") Is System.DBNull.Value) Then
                    lblPhoneVal.Text = datareader("PhoneNumber")
                End If
            Loop
        End If
        datareader.Close()

        ClientScript.RegisterStartupScript(Me.GetType(), "ADSearchError", String.Format("alert('{0}');", "User email and information have been saved to match email: " + txtEmail.Text + "."), True)
    End Sub

    Function CheckRoleIP(ByVal rlbRole As RadListBox)

        If Roles.IsUserInRole(lblUserNameVal.Text, "Admin") = False Then
            If rlbRole.FindItemByText("ContractOfficer").Checked = True Then
                If (rlbRole.FindItemByText("IdeaManager").Checked = True Or rlbRole.FindItemByText("IdeaReviewer").Checked = True Or rlbRole.FindItemByText("IdeaReader").Checked = True) Then
                    Return False
                Else
                    Return True
                End If
            ElseIf rlbRole.FindItemByText("IdeaManager").Checked = True Then
                If (rlbRole.FindItemByText("IdeaReviewer").Checked = True Or rlbRole.FindItemByText("IdeaReader").Checked = True) Then
                    Return False
                Else
                    Return True
                End If
            End If
        Else
            If (rlbRole.FindItemByText("ContractOfficer").Checked = True Or rlbRole.FindItemByText("IdeaManager").Checked = True Or rlbRole.FindItemByText("IdeaReviewer").Checked = True Or rlbRole.FindItemByText("IdeaReader").Checked = True) Then
                Return False
            Else
                Return True
            End If
        End If

        Return True
    End Function

    Function CheckRoleOther(ByVal rlbRole As RadListBox)
        If rlbRole.FindItemByText("Admin").Checked Then

            If Roles.IsUserInRole(lblUserNameVal.Text, "ContractOfficer") = True Then
                Return False
            ElseIf Roles.IsUserInRole(lblUserNameVal.Text, "IdeaManager") = True Then
                Return False
            ElseIf Roles.IsUserInRole(lblUserNameVal.Text, "IdeaReviewer") = True Then
                Return False
            ElseIf Roles.IsUserInRole(lblUserNameVal.Text, "IdeaReader") = True Then
                Return False
            Else
                Return True
            End If
        End If
        Return True
    End Function

    Private Sub btnReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        'Response.Redirect("MemberManagement.aspx?UserName=" + lblUserNameVal.Text)   'Return the user to the MemberManagement page
        Response.Redirect("MemberManagement.aspx")
    End Sub
End Class