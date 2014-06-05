Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common

Public Class FolderList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' login check
        Dim loginchk = New clsLoginCheck()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not loginchk.LoggedIn Then
            Response.Redirect("Login.aspx")
        End If

        If Not IsPostBack Then
            If Not Request("NODEID") Is Nothing Then
                LoadNodes(Request("NODEID"))
            End If
        End If
    End Sub

    Private Function GetControlTreeNodes(ByVal parentId As String) As SqlDataReader
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "ControlTreeUserSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        ' Maybe modify to use the login class
        Dim m As MembershipUser = Membership.GetUser()
        m.ProviderUserKey.ToString()
        db.AddInParameter(objCommand, "@UserId", DbType.String, m.ProviderUserKey.ToString())
        If Len(parentId) = 0 Then
            db.AddInParameter(objCommand, "@ParentId", SqlDbType.Int, DBNull.Value)
        Else
            db.AddInParameter(objCommand, "@ParentId", SqlDbType.Int, parentId)
        End If
        If Roles.IsUserInRole("Admin") Then
            db.AddInParameter(objCommand, "@Admin", SqlDbType.Int, 1)
        Else
            db.AddInParameter(objCommand, "@Admin", SqlDbType.Int, 0)
        End If
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        Return datareader
    End Function

    'load nodes of the ParentId passed in ParentId = "" loads  Root Nodes 
    Private Sub LoadNodes(ByVal parentId As String)
        Dim data As SqlDataReader = GetControlTreeNodes(parentId)
        Dim ModuleTypeId As Int32
        If data.HasRows Then
            Do While data.Read()
                Dim strNodeName As String = data("NodeName")
                Dim intNodeID As Integer = Val(data("NodeId"))
                Dim intRootNodeId As Integer = Val(data("RootNodeId"))
                Dim intModuleTypeID As Integer = Val(data("ModuleTypeId"))
                Dim strDest As String = ""
                Dim strTarget As String = "_self"

                If intModuleTypeID = 1 Then ' internal URL link
                    If Not (data("URL") Is System.DBNull.Value) And data("URL").ToString.Length > 0 Then
                        strDest = data("URL").trim + "?NodeId=" + data("NodeId").ToString
                    Else
                        strDest = "~/Blank.aspx"
                    End If
                ElseIf ModuleTypeId = 2 Then 'external url link
                    If Not (data("URL") Is System.DBNull.Value) And data("URL").ToString.Length > 0 Then
                        strDest = data("URL").trim
                    Else
                        strDest = "~/Blank.aspx"
                    End If
                    ' Open New window
                    strTarget = "_blank"
                Else
                    strDest = data("ModuleURL").trim + "?NodeId=" + data("NodeId").ToString
                End If
                lblFolderList.Text = lblFolderList.Text & "<a href='" & strDest & "' target='" & strTarget & "'>" & strNodeName & "</a><br><br>"
            Loop
            data.Close()
        Else
        End If
    End Sub

End Class