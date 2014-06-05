Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports Telerik.Web.UI

Public Class UserAccessReview
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim loginchk As New clsLoginCheck

        If (Not loginchk.LoggedIn) Then
            Response.Redirect("~/login.aspx")
        End If

        'Only Admins 
        If Not User.IsInRole("Admin") And Not User.IsInRole("Contractor Admin") Then
            Response.Redirect("../Blank.aspx")
        End If

        If Not IsPostBack Then
            lblUserName.Text = Request("UserName")
            LoadNodes()
            loadUser()
        End If
    End Sub
    Private Sub loadUser()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "PersonUserSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        Dim MembershipUser As MembershipUser = Membership.GetUser(lblUserName.Text)
        db.AddInParameter(objCommand, "@UserId", DbType.String, MembershipUser.ProviderUserKey.ToString) '<-- userid of node permission to view
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        If datareader.HasRows Then
            datareader.Read()
            lblUserValue.Text = datareader("LastName") + ", " & datareader("FirstName") + " (" + lblUserName.Text + ")"
        End If
        datareader.Close()

    End Sub
    Private Sub LoadNodes()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "UserAccessReviewSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        Dim MembershipUser As MembershipUser = Membership.GetUser(lblUserName.Text)
        db.AddInParameter(objCommand, "@UserId", DbType.String, MembershipUser.ProviderUserKey.ToString) '<-- userid of node permission to view
        If Roles.IsUserInRole(lblUserName.Text, "Admin") Then
            db.AddInParameter(objCommand, "@Admin", DbType.Int32, 1)
        End If

        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        ControlTree.DataSource = datareader
        ControlTree.DataBind()

        ControlTree.ExpandAllNodes()
    End Sub

    Private Sub ControlTree_NodeClick(sender As Object, e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles ControlTree.NodeClick

        Dim strPermissions As String = ""
        If Not Roles.IsUserInRole(lblUserName.Text, "Admin") Then
            Dim NodeId As String = ControlTree.SelectedValue
            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommand As String = "NodePermissionDisplaySelect"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
            Dim MembershipUser As MembershipUser = Membership.GetUser(lblUserName.Text)
            db.AddInParameter(objCommand, "@UserId", DbType.String, MembershipUser.ProviderUserKey.ToString())
            db.AddInParameter(objCommand, "@NodeId", SqlDbType.Int, NodeId)
            Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
            If datareader.HasRows Then
                While datareader.Read
                    If datareader("HasUserPermission") = 1 Then
                        If strPermissions = "" Then
                            strPermissions = "Has User Permission"
                        Else
                            strPermissions = strPermissions + "\r\n Has User Permission"
                        End If
                    End If
                    If Not datareader("RoleName") Is System.DBNull.Value Then
                        If strPermissions = "" Then
                            If datareader("RoleName").ToString.EndsWith("Case") Then
                                strPermissions = " Case Role Permission Granted"
                            Else
                                strPermissions = " " & datareader("RoleName")
                            End If

                        Else
                            If datareader("RoleName").ToString.EndsWith("Case") Then
                                '    strPermissions = " Case Role Permission Granted"
                                'Else
                                strPermissions = strPermissions + "\r\n " & datareader("RoleName")
                            End If
                        End If
                    End If
                End While
            End If
            datareader.Close()
        Else
            strPermissions = "Admin"
        End If

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "AccessPermissions", String.Format("alert('{0}');", "User has the following role(s) granting them view permission:\r\n\" + strPermissions), True)
    End Sub

    Private Sub btnReturn_Click(sender As Object, e As System.EventArgs) Handles btnReturn.Click
        If Request("ParentPage") = 1 Then
            Session("UserName") = lblUserName.Text
            Response.Redirect("SupplierManagementUpdate.aspx")
        Else
            Response.Redirect("SupplierManagement.aspx")
        End If
    End Sub
End Class