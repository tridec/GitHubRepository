Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports Telerik.Web.UI
Public Class RoleAccessReview
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
            lblRoleName.Text = Request("RoleName").Replace("_", " ")
            LoadNodes()
        End If
    End Sub
  
    Private Sub LoadNodes()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "RoleAccessReviewSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@RoleName", DbType.String, lblRoleName.Text)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        ControlTree.DataSource = datareader
        ControlTree.DataBind()
        ControlTree.ExpandAllNodes()
        If Not ControlTree.Nodes.Count > 0 Then
            lblNoAccess.Visible = True
        End If
    End Sub


    Private Sub btnReturn_Click(sender As Object, e As System.EventArgs) Handles btnReturn.Click
       
        Response.Redirect("RoleManagement.aspx")

    End Sub
End Class