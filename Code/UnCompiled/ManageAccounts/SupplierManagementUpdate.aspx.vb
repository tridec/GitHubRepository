Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.DirectoryServices
Imports System.Web.Security

Partial Public Class SupplierManagementUpdate
    Inherits System.Web.UI.Page
    Dim objVANS As New clsVANS

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        'login check
        Dim loginchk = New clsLoginCheck()

        'Only Admins 
        If Not User.IsInRole("Admin") And Not User.IsInRole("Contractor Admin") Then
            Response.Redirect("../Blank.aspx")
        End If

        If Not IsPostBack Then
            If Not Session("UserName") = "" Then
                lblUserName.Text = Session("UserName")
                lblVewNodeTree.Text = "<a   href='UserAccessReview.aspx?UserName=" + lblUserName.Text + "&ParentPage=1'>View User Access</a>"
            Else
                'redirtect to vendor list no session variable
                Response.Redirect("SupplierManagement.aspx")
            End If

            LoadUserInfo(lblUserName.Text)
            'default tab load Teams
            LoadRoles(4, 1)

            Session("UserName") = ""
            Session("LinkUser") = ""

            Dim rtsAdmin As RadTabStrip = Master.FindControl("rtsAdmin")
            rtsAdmin.Tabs(0).Selected = True
        End If


    End Sub

    Protected Sub LoadUserInfo(ByVal strUserName As String)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Load User Information
        ''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim MembershipUser As MembershipUser = Membership.GetUser(strUserName)
        Dim UserID As String = MembershipUser.ProviderUserKey.ToString()


        lblUserName.Text = strUserName


        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "PersonUserSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@UserId", DbType.String, UserID)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)

        If datareader.HasRows Then
            Do While datareader.Read()
                If Not (datareader("Email") Is System.DBNull.Value) Then
                    'lblEmailVal.Text = datareader("Email")
                    txtEmail.Text = datareader("Email")
                End If

                lblUserNameEditValue.Text = datareader("UserName")

                txtFirstName.Text = datareader("FirstName")

                txtLastName.Text = datareader("LastName")

                txtEmail.Text = datareader("Email")
                txtEmail.Enabled = False

                If Not (datareader("VistaUserID") Is System.DBNull.Value) Then
                    pnlExistingUser.Visible = True
                    pnlFindUser.Visible = False
                    lblVistaUserValue.Text = datareader("VistaUserName")
                Else
                    pnlExistingUser.Visible = False
                    pnlFindUser.Visible = True
                End If

                If Not (datareader("IsLockedOut") Is System.DBNull.Value) Then
                    rblIsLockedOut.SelectedValue = datareader("IsLockedOut")
                End If
                If Not (datareader("IsApproved") Is System.DBNull.Value) Then
                    rblIsAuthorized.SelectedValue = datareader("IsApproved")
                    'hold values to see if email submission is needed on update
                    lblIsAuthorizedOld.Text = datareader("IsApproved")
                End If
                If Not (datareader("NewRequest") Is System.DBNull.Value) Then
                    'Will be populated with yes or no
                    lblEditFormNewRequest.Text = datareader("NewRequest")
                    'Do we need code to go with this label??????????????????????????????????????????????????????????????????????????????????????????????????????
                End If
            Loop
        End If
        datareader.Close()
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

        Dim userRoles As String() = Roles.GetRolesForUser(lblUserName.Text)
        For Each role As String In userRoles
            If Not rlbUserRoles.FindItemByText(role) Is Nothing Then
                rlbUserRoles.FindItemByText(role).Checked = True
            End If
        Next

        'If the role Member is checked, do not let them uncheck it
        If rlbUserRoles.FindItemByText("Member").Checked = True Then
            rlbUserRoles.FindItemByText("Member").Enabled = False
        End If

    End Sub

    Private Sub btnUserAccountUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUserAccountUpdate.Click

        'Update the users information (btnUserAccountUpdate)
        If Not lblUserName Is Nothing Then
            Dim MembershipUser As MembershipUser = Membership.GetUser(lblUserName.Text)
            Dim UserID As String = MembershipUser.ProviderUserKey.ToString()
            'Dim lblMessage As Label = e.Item.FindControl("lblMessage")

            If Not txtEmail Is Nothing Then
                Try
                    MembershipUser.Email = txtEmail.Text
                    MembershipUser.IsApproved = rblIsAuthorized.SelectedValue
                    Membership.UpdateUser(MembershipUser)
                    If rblIsLockedOut.SelectedValue = False Then
                        MembershipUser.UnlockUser()
                    End If
                    ' Add Roles.
                    If Not rlbUserRoles Is Nothing Then
                        For Each rolebox As Telerik.Web.UI.RadListBoxItem In rlbUserRoles.Items
                            If rolebox.Checked = True Then
                                If Roles.IsUserInRole(lblUserName.Text, rolebox.Text) = False Then
                                    Roles.AddUserToRole(lblUserName.Text, rolebox.Text)
                                End If
                            Else
                                If Roles.IsUserInRole(lblUserName.Text, rolebox.Text) = True Then
                                    Roles.RemoveUserFromRole(lblUserName.Text, rolebox.Text)
                                End If
                            End If
                        Next
                    End If

                    'Make sure they have the role Member
                    If Not Roles.IsUserInRole(lblUserName.Text, "Member") Then
                        Roles.AddUserToRole(lblUserName.Text, "Member")
                    End If

                    ' update database 
                    Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
                    Dim sqlcommand As String = "PersonUpdate"
                    Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)

                    db.AddInParameter(objCommand, "@UserId", DbType.String, MembershipUser.ProviderUserKey.ToString())
                    db.AddInParameter(objCommand, "@FirstName", DbType.String, txtFirstName.Text)
                    db.AddInParameter(objCommand, "@LastName", DbType.String, txtLastName.Text)
                    If pnlFindUser.Visible = True Then
                        If rgdPerson.SelectedItems.Count > 0 Then
                            Dim gdi As GridDataItem = rgdPerson.SelectedItems(0)
                            db.AddInParameter(objCommand, "@VistaUserID", DbType.Int32, gdi("localPID").Text)
                            db.AddInParameter(objCommand, "@VistaUserName", DbType.String, gdi("name").Text)
                            lblVistaUserValue.Text = gdi("name").Text
                        End If
                    End If
                    db.ExecuteNonQuery(objCommand)

                Catch ex As Exception
                    lblMessage.Text = "Update Failure: " + ex.Message
                    If ex.Message = "The E-mail supplied is invalid." Then
                        lblMessage.Visible = False
                        lblEmailDuplicate.Visible = True
                        lblEmailDuplicate.Text = "Duplicate Email"
                    End If
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UpdateError", String.Format("alert('{0}');", lblEmailDuplicate.Text), True)
                    Exit Sub
                End Try

                If pnlFindUser.Visible = True Then
                    If rgdPerson.SelectedItems.Count > 0 Then
                        pnlFindUser.Visible = False
                        pnlExistingUser.Visible = True
                    End If
                End If

            Else

            End If

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UpdateError", String.Format("alert('{0}');", "User " + lblUserName.Text + " has been updated"), True)

            If lblIsAuthorizedOld.Text = "False" And rblIsAuthorized.Text = "True" Then
                'Send email saying they have been granted access
                Dim clsEmail1 = New clsEmail()
                clsEmail1.EmailTo = txtEmail.Text
                clsEmail1.EmailHTML = True
                clsEmail1.EmailSubject = "Access Granted"
                Dim strURL As String = System.Configuration.ConfigurationManager.AppSettings("AdminURL").ToString()
                Dim strBody As String = "Dear " + txtFirstName.Text + " " + txtLastName.Text + ",<BR/><BR/>"
                strBody += "Your access request has been approved, you may now use your "
                strBody += "account, with username "
                strBody += lblUserName.Text
                strBody += ", to login login at:<BR/>"
                strBody += "<a href='" + strURL + "'>" + strURL + "<a/>."
                clsEmail1.EmailBody = strBody
                clsEmail1.SendEmail()
            End If

        Else ' username error message

        End If

    End Sub

    Private Sub btnUserAccountCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUserAccountCancel.Click
        Response.Redirect("SupplierManagement.aspx")   'Return the user to the MemberManagement page
        'Supplier management needs to be coded to handle username being passed''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        'Reset the users password and send it to them in an email 
        Dim strPassword As String = ""
        Dim RegexObj As Regex = New Regex("^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).*$")

        'Loop to generate passwords and verify them against the reg ex in the webconfig to make sure the password is valid
        Dim blnValue As Boolean = False
        While blnValue = False
            strPassword = Membership.GeneratePassword(8, 0)
            If RegexObj.IsMatch(strPassword) Then 'compares string to regex
                blnValue = True
            Else
                blnValue = False
            End If
        End While

        Dim username As String = lblUserNameEditValue.Text
        Dim mu As MembershipUser = Membership.Providers("PasswordReset").GetUser(username, False)
        mu.ChangePassword(mu.ResetPassword(), strPassword)

        Dim objEmail = New clsEmail()
        objEmail.EmailTo = txtEmail.Text
        objEmail.EmailHTML = True
        objEmail.EmailSubject = "Account Password Reset"
        Dim strBody As String = ""
        strBody = "A new password has been generated for your account.<br/><br/> Please use the following password to login  " + strPassword + "<br/><br/>Thank You"
        'strBody += ""
        objEmail.EmailBody = strBody
        objEmail.RecordTypeId = 1
        objEmail.SendEmail()
        Dim strEmailSent As String = "An email has been sent to " + username + " with a new randomly generated password."
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "EmailSent", String.Format("alert('{0}');", strEmailSent), True)
    End Sub

    Private Sub LoadBlankGrid(ByVal rgdGrid As RadGrid)
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim colsNum As Integer = 3
        Dim rowsNum As Integer = 0
        Dim colName As String = "Column"

        For j As Integer = 1 To colsNum
            dt.Columns.Add([String].Format("{0}{1}", colName, j))
        Next

        For i As Integer = 1 To rowsNum
            dr = dt.NewRow()

            For k As Integer = 1 To colsNum
                dr([String].Format("{0}{1}", colName, k)) = [String].Format("{0}{1} Row{2}", colName, k, i)
            Next
            dt.Rows.Add(dr)
        Next

        rgdGrid.DataSource = dt
    End Sub

    Private Sub btnFind_Click(sender As Object, e As System.EventArgs) Handles btnFind.Click

        Session("LinkUser") = Nothing

        If connectVANS() = False Then
            rgdPerson.Rebind()
            Exit Sub
        End If

        Dim tgdPA As us.vacloud.devmdws.TaggedPatientArrays = objVANS.match(txtSSN.Text)
        If objVANS.ErrorMessage <> "" Then
            rgdPerson.Rebind()
            Exit Sub
        End If

        Session("LinkUser") = tgdPA.arrays(0).patients

        objVANS.disconnect()
        rgdPerson.Rebind()

    End Sub

    Private Function connectVANS() As Boolean
        objVANS.parentPage = HttpContext.Current.Handler
        If objVANS.connectVANS = False Then
            Return False
        End If

        Return True
    End Function

    Private Sub rgdPerson_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdPerson.NeedDataSource
        If Not Session("LinkUser") Is Nothing Then
            rgdPerson.DataSource = Session("LinkUser")
        Else
            'LoadBlankGrid
            LoadBlankGrid(rgdPerson)
        End If
    End Sub

    Private Sub btnResetLink_Click(sender As Object, e As System.EventArgs) Handles btnResetLink.Click
        Dim MembershipUser As MembershipUser = Membership.GetUser(lblUserName.Text)
        Dim UserID As String = MembershipUser.ProviderUserKey.ToString()

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "PersonVistaResetUpdate"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@UserId", DbType.String, UserID)
        db.ExecuteNonQuery(objCommand)

        Session("LinkUser") = Nothing
        pnlExistingUser.Visible = False
        pnlFindUser.Visible = True
        lblVistaUserValue.Text = ""
        txtSSN.Text = ""
        rgdPerson.Rebind()
    End Sub
End Class