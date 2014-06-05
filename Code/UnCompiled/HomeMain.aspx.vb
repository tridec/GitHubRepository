Imports Telerik.Web.UI
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary
Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data.Common
Imports System.Net.Configuration

Partial Public Class HomeMain
    Inherits System.Web.UI.Page
    Private objLoginCheck As clsLoginCheck
    Dim datareaderSearch As SqlDataReader
    Dim strURL As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        '  **** enable nodeid passed in to be checked and set as the selected item in the tree.
        objLoginCheck = New clsLoginCheck()

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        '***** remove admin check to verify tree permissions work and make it work for 
        ' make sure this code works if admin and not member
        If Not objLoginCheck.LoggedIn Or Not (objLoginCheck.Member Or objLoginCheck.Admin) Then 'And Not objLoginCheck.Admin
            Response.Redirect("login.aspx")
        End If

        If Page.User.Identity IsNot Nothing AndAlso TypeOf Page.User.Identity Is FormsIdentity Then
            If Not Page.IsPostBack Then

                'Try
                '    Dim firstNode As RadTreeNode = ControlTree.Nodes.Item(0)

                '    If firstNode IsNot Nothing Then
                '        If firstNode.Attributes("Dest").Contains("?") Then
                '            ContentPane.ContentUrl = firstNode.Attributes("Dest").Trim + "&NodeId=" + firstNode.Value
                '        Else
                '            ContentPane.ContentUrl = firstNode.Attributes("Dest").Trim + "?NodeId=" + firstNode.Value
                '        End If
                '    Else
                '        ContentPane.ContentUrl = "blank.aspx"
                '    End If
                'Catch

                'End Try
            End If


        End If
        If Not IsPostBack Then

            If User.Identity.IsAuthenticated Then
                LoadLoginID()
            End If

            'Set Tree Nodes, 1 - User Management, 2 - Appointment List, 3 - Template Management, 4 - Appointment List (Member), 5 - Signature Management
            If User.IsInRole("Admin") Then
                ControlTree.Nodes.FindNodeByValue("4").Remove()
            ElseIf User.IsInRole("AppointmentUser") Or User.IsInRole("AppointmentAdmin") Then
                ControlTree.Nodes.FindNodeByValue("1").Remove()
                ControlTree.Nodes.FindNodeByValue("3").Remove()
                ControlTree.Nodes.FindNodeByValue("4").Remove()
                ControlTree.Nodes.FindNodeByValue("5").Remove()
            Else
                ControlTree.Nodes.FindNodeByValue("1").Remove()
                ControlTree.Nodes.FindNodeByValue("2").Remove()
                ControlTree.Nodes.FindNodeByValue("3").Remove()
                ControlTree.Nodes.FindNodeByValue("5").Remove()
            End If

            'Dim strRequestedPage As String = Request("requestedpage")
            'If Not strRequestedPage Is Nothing Then
            '    ContentPane.ContentUrl = Server.UrlDecode(strRequestedPage)

            'End If
        End If

    End Sub


#Region "Load Tree Nodes"

    Private Sub ControlTree_NodeClick1(sender As Object, e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles ControlTree.NodeClick
        Select Case e.Node.Value
            Case 1
                ContentPane.ContentUrl = "~/ManageAccounts/SupplierManagement.aspx"
            Case 2
                ContentPane.ContentUrl = "~/AppointmentsList.aspx"
            Case 3
                ContentPane.ContentUrl = "~/TemplateList.aspx"
            Case 4
                ContentPane.ContentUrl = "~/AppointmentsListMember.aspx"
            Case 5
                ContentPane.ContentUrl = "~/SignatureManagement.aspx"
        End Select

    End Sub

    Protected Sub ControlTree_NodeDataBound(sender As Object, e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles ControlTree.NodeDataBound

    End Sub


#End Region
    Protected Sub LoadLoginID()

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "PersonUserSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@UserID", DbType.Guid, Membership.GetUser().ProviderUserKey)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        If datareader.HasRows Then
            While datareader.Read
                Session("LoginID") = datareader("LoginID")
            End While
        End If
        datareader.Close()

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