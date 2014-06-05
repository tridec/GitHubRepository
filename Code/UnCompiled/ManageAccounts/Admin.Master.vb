Imports Telerik.Web.UI
Public Class Admin
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim currentTab As RadTab = rtsAdmin.FindTabByUrl(Request.Url.PathAndQuery)
        If currentTab IsNot Nothing Then
            currentTab.Selected = True
        End If
    End Sub

    Protected Sub lbLogout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbLogout.Click
        Dim loginchk = New clsLoginCheck()
        If loginchk.LoggedIn = True Then
            'logout non MEMBER
            FormsAuthentication.SignOut()
            Roles.DeleteCookie()
            FormsAuthentication.RedirectToLoginPage()
        Else
            If Convert.ToInt32(Request.QueryString("PageId")) = -2 Then
                Response.Redirect("login.aspx?PageId=-2")
            Else
                Response.Redirect("login.aspx")
            End If

        End If

        '    'logout non MEMBER
        '    FormsAuthentication.SignOut()
        '    Roles.DeleteCookie()
        '    FormsAuthentication.RedirectToLoginPage()
    End Sub

End Class