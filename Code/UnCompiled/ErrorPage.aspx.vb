Public Partial Class ErrorPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objURLScrub As New clsURLScrub
        objURLScrub.URLScrub(Context.Request.ServerVariables("QUERY_STRING"))
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

    End Sub
End Class