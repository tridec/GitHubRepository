Partial Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim loginchk = New clsLoginCheck()
        ' new session cookie code.
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        '' needed security abandon session every time
        '' If Not Page.User.Identity.IsAuthenticated Then
        'If Not (Page.Request.Cookies("ASP.NET_SessionId") Is Nothing) Then
        '    Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddYears(-30)
        '    Response.Cookies("ASP.NET_SessionId").Secure = True
        'End If
        'Session.Abandon()
        ''End If

        'If loginchk.LoggedIn Then
        '    Response.Redirect("default.aspx")
        'Else
        'End If

        'NEW Secuirty code to flush the client session cookie and redirect the first time
        If Not IsPostBack AndAlso (Request.Cookies("__LOGINCOOKIE__") Is Nothing OrElse Request.Cookies("__LOGINCOOKIE__").Value = "") Then
            'At this point, we do not know if the session ID that we have is a new
            'session ID or if the session ID was passed by the client. 
            'Update the session ID.

            Session.Abandon()
            Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))

            'To make sure that the client clears the session ID cookie, respond to the client to tell 
            'it that we have responded. To do this, set another cookie.
            AddRedirCookie()
            Response.Redirect(Request.RawUrl)
        Else
            If loginchk.LoggedIn Then
                If Convert.ToInt32(Request.QueryString("PageId")) = -2 Then
                    Response.Redirect("HomeMain.aspx?PageId=-2")
                Else
                    Response.Redirect("HomeMain.aspx")
                End If

            End If
        End If
        'NEW Secuirty code to flush the client session cookie and redirect the first time - double checks the redirect time is less than 5 sec
        If Not IsPostBack Then
            'Make sure that someone is not trying to spoof.
            Try
                Dim ticket As FormsAuthenticationTicket = FormsAuthentication.Decrypt(Request.Cookies("__LOGINCOOKIE__").Value)
                If ticket Is Nothing OrElse ticket.Expired = True Then
                    Throw New Exception()
                End If
                RemoveRedirCookie()
            Catch
                'If someone is trying to spoof, do it again.
                AddRedirCookie()
                Response.Redirect(Request.RawUrl)
            End Try
            If Convert.ToInt32(Request.QueryString("PageId")) = -2 Then
                Login1.DestinationPageUrl = "~/HomeMain.aspx?PageId=-2"
            End If
        End If
        'displays changing sessions and cookies
        'Response.Write("Session.SessionID=" + Session.SessionID + "<br/>")
        'Response.Write("Cookie ASP.NET_SessionId=" + Request.Cookies("ASP.NET_SessionId").Value + "<br/>")

    End Sub

    Private Sub Login1_LoggingIn(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LoginCancelEventArgs) Handles Login1.LoggingIn
        'Login1.FailureAction = LoginFailureAction.RedirectToLoginPage
    End Sub

    Private Sub Login1_LoginError(ByVal sender As Object, ByVal e As System.EventArgs) Handles Login1.LoginError
        Dim MembershipUser As MembershipUser = Membership.GetUser(Login1.UserName.ToString)
        If Not MembershipUser Is Nothing Then
            If MembershipUser.IsLockedOut = True Then
                Login1.FailureText = "Account is Locked Out <BR> Please contact your Systems Administrator for assistance"
                Login1.FailureAction = LoginFailureAction.Refresh
            End If

            If MembershipUser.IsApproved = False Then
                Login1.FailureText = "Account is Disabled <BR> Please contact your Systems Administrator for assistance"
                Login1.FailureAction = LoginFailureAction.Refresh
            End If
        End If
        'Display the failure message in a client-side alert box
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "LoginError", _
                    String.Format("alert('{0}');", Login1.FailureText.Replace("'", "\'").Replace("<BR>", "\n")), True)
    End Sub

    Private Sub RemoveRedirCookie()
        Response.Cookies.Add(New HttpCookie("__LOGINCOOKIE__", ""))
    End Sub

    Private Sub AddRedirCookie()
        Dim ticket As New FormsAuthenticationTicket(1, "Test", DateTime.Now, DateTime.Now.AddSeconds(5), False, "")
        Dim encryptedText As String = FormsAuthentication.Encrypt(ticket)
        Response.Cookies.Add(New HttpCookie("__LOGINCOOKIE__", encryptedText))
    End Sub

End Class