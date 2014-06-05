Public Partial Class NoTimeout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objlogin = New clsLoginCheck
        ' always refresh code 
        Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) - 60))

    End Sub

End Class