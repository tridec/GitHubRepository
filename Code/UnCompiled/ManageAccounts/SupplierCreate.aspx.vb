Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.DirectoryServices
Imports System.Web.Security

Partial Public Class SupplierCreate
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
            LoadNewSupplierRole()

            Dim rtsAdmin As RadTabStrip = Master.FindControl("rtsAdmin")
            rtsAdmin.Tabs(0).Selected = True

        End If
    End Sub


    Protected Sub LoadNewSupplierRole()
        'load roles
        If Not rlbUserRoles Is Nothing Then
            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommand As String = "RoleSelect"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
            db.AddInParameter(objCommand, "@RoleTypeID", DbType.Int32, 1)
            Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
            rlbUserRoles.DataSource = datareader
            rlbUserRoles.DataBind()

            rlbUserRoles.FindItemByText("Member").Checked = True
            rlbUserRoles.FindItemByText("Member").Enabled = False
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

    Protected Sub btnResetNewUser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnResetNewUser.Click
        'Show user fields again and hide completion text
        pnlAll.Visible = True
        pnlComplete.Visible = False

        ' clear all new user fields
        UserName.Text = ""
        Password.Text = ""
        Email.Text = ""
        lblCreateStatus.Text = ""
        ' Clear Roles.
        For Each rolebox As Telerik.Web.UI.RadListBoxItem In rlbUserRoles.Items
            rolebox.Checked = False
        Next
        ' Insert New User Information into database 
        txtFirstName.Text = ""
        txtLastName.Text = ""

        ' hide succsss and show save button
        btnSaveNewUser.Visible = True
        lblNewUserSaved.Visible = False
        btnResetNewUser.Visible = False


    End Sub


    Protected Sub btnSaveNewUser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveNewUser.Click

        Try
            ' Add User.
            Dim createstatus As New System.Web.Security.MembershipCreateStatus
            Dim newUser As MembershipUser = Membership.CreateUser(UserName.Text, Password.Text, Email.Text, "What is your favorite sports team?", UserName.Text + Date.Now, True, createstatus)
            lblCreateStatus.Text = createstatus.ToString()
            Dim UserID As String = newUser.ProviderUserKey.ToString()

            ' Add Roles.
            For Each rolebox As Telerik.Web.UI.RadListBoxItem In rlbUserRoles.Items
                If rolebox.Checked = True Then
                    Roles.AddUserToRole(UserName.Text, rolebox.Text)
                End If
            Next

            If Not Roles.IsUserInRole(UserName.Text, "Member") Then
                Roles.AddUserToRole(UserName.Text, "Member")
            End If

            'set email url
            Dim emURL As String
            emURL = System.Configuration.ConfigurationManager.AppSettings("AdminURL").ToString()

            ' Insert New User Information into database 
            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommand As String = "PersonUpdate"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
            db.AddInParameter(objCommand, "@UserId", DbType.String, UserID)
            db.AddInParameter(objCommand, "@FirstName", DbType.String, txtFirstName.Text)
            db.AddInParameter(objCommand, "@LastName", DbType.String, txtLastName.Text)
            db.ExecuteNonQuery(objCommand)

            btnSaveNewUser.Visible = False
            lblNewUserSaved.Visible = True
            btnResetNewUser.Visible = True

            'Collect info for the user creating the account
            Dim strName As String = ""
            Dim strPhone As String = ""
            Dim dbPOC As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommandPOC As String = "PersonUserSelect"
            Dim objCommandPOC As DbCommand = dbPOC.GetStoredProcCommand(sqlcommandPOC)

            Dim m As MembershipUser = Membership.GetUser()
            m.ProviderUserKey.ToString()

            dbPOC.AddInParameter(objCommandPOC, "@UserId", DbType.String, m.ProviderUserKey.ToString())
            Dim datareader As SqlDataReader = dbPOC.ExecuteReader(objCommandPOC)
            If datareader.HasRows Then
                Do While datareader.Read()

                    If Not (datareader("FirstName") Is System.DBNull.Value) Then
                        strName = datareader("FirstName")
                    Else
                        strName = ""
                    End If
                    If Not (datareader("LastName") Is System.DBNull.Value) Then
                        strName = strName + " " + datareader("LastName")
                    End If
                    If Not (datareader("PhoneNumber") Is System.DBNull.Value) Then
                        strPhone = datareader("PhoneNumber")
                    Else
                        strPhone = ""
                    End If

                Loop
            End If
            datareader.Close()

            Dim objEmail = New clsEmail()
            objEmail.EmailFrom = m.Email
            objEmail.EmailTo = Email.Text
            objEmail.EmailHTML = True
            objEmail.EmailSubject = "New Account"
            objEmail.EmailBody = "A new account has been created for you.<br/><br/> Your User Name is:&nbsp;&nbsp;" + UserName.Text + "<br/><br/> Please go to the following URL to login:&nbsp;<a href='" + emURL + "'>" + emURL + "</a><br/><br/>Questions/Comments - Please contact:<br/><br/>Name:&nbsp;" + strName + "<br/>Email:&nbsp;" + m.Email + "<br/>Phone:&nbsp;" + strPhone + "<br/><br/>Thank You"

            objEmail.RecordTypeId = 1
            objEmail.SendEmail()

            Dim objEmail2 = New clsEmail()
            objEmail2.EmailFrom = m.Email
            objEmail2.EmailTo = Email.Text
            objEmail2.EmailHTML = True
            objEmail2.EmailSubject = "New Account Password"
            objEmail2.EmailBody = "The password below is to be used with the user name sent in a previous email.<br/><br/>Password:&nbsp;" + Password.Text + "<br/><br/>Special Instructions&nbsp;-<br/>&nbsp;&nbsp;1.Change Password<br/>&nbsp;&nbsp;2.Answer Security Question<br/><br/>Questions/Comments - Please contact:<br/><br/>Name:&nbsp;" + strName + "<br/>Email:&nbsp;" + m.Email + "<br/>Phone:&nbsp;" + strPhone + "<br/><br/>Thank You"

            objEmail2.RecordTypeId = 1
            objEmail2.SendEmail()

            'Hide Input fields and show completion text
            pnlAll.Visible = False
            pnlComplete.Visible = True

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "User Account", _
            String.Format("alert('{0}');", lblCreateStatus.Text), True)
        End Try

    End Sub

End Class