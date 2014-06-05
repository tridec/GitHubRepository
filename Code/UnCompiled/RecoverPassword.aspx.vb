
Partial Class RecoverPassword
    Inherits System.Web.UI.Page

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        Dim loginchk = New clsLoginCheck()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

    End Sub

    Protected Sub SubmitButton_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub PasswordRecovery1_AnswerLookupError(ByVal sender As Object, ByVal e As System.EventArgs) Handles PasswordRecovery1.AnswerLookupError
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Invalid User Information", _
        String.Format("alert('{0}');", "Your answer could not be verified. Please try again. "), True)
    End Sub

    Private Sub PasswordRecovery1_UserLookupError(ByVal sender As Object, ByVal e As System.EventArgs) Handles PasswordRecovery1.UserLookupError
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Invalid User Information", _
        String.Format("alert('{0}');", "We were unable to access your information. Please try again."), True)
    End Sub
End Class
