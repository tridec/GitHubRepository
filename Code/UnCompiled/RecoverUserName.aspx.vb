Partial Public Class RecoverUserName
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim loginchk = New clsLoginCheck()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

    End Sub

    Protected Sub SubmitButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SubmitButton.Click
        ' check the db for a username based on email

        Dim username As String = Membership.GetUserNameByEmail(txtEmailAddress.Text)
        If username Is Nothing Then ' if user not found display error
            SuccessFailureText.Text = "Your email address was not found."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Invalid User Information", _
            String.Format("alert('{0}');", "Your email address was not found."), True)
        Else
            Dim EmailAddress As String = txtEmailAddress.Text
            Dim strLink As String = System.Configuration.ConfigurationManager.AppSettings("SupplierURL").ToString()
            Dim clsEmail = New clsEmail()
            clsEmail.EmailTo = EmailAddress
            clsEmail.EmailSubject = "Your Username"
            clsEmail.EmailBody = "Your username for is " & username & ". Please go to the following URL to login.  " + strLink + " "
            clsEmail.SendEmail()
            SuccessFailureText.Text = "Your username has been sent to your registered email address."
        End If

    End Sub
End Class