Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Public Class Migration
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnUsers_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUsers.Click
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "MigrateUsersSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        Dim datareader As SqlDataReader
        datareader = db.ExecuteReader(objCommand)
        If datareader.HasRows Then
            While datareader.Read
                ' do something here
                Dim strusername As String = datareader("username")
                Dim stremail As String = datareader("email")

                'strDeptOrgName = datareader("DeptOrgName")
                'strUserTitle = datareader("Title")
                'strFirstName = datareader("FirstName")
                'strLastName = datareader("LastName")
                'strEmail = datareader("Email")
                'strCity = datareader("City")
                'strZipCode = datareader("ZipCode")
                'strAddress = datareader("Address1")
                'strPhone = datareader("PhoneNumber")
                'strCompanyName = datareader("CompanyName")
                'strStateVal = datareader("StateID")
                'strUserName = datareader("UserName")



                lblUsers.Text = lblUsers.Text + "<BR/>Create:  " & strusername
                Try
                    Dim createstatus As New System.Web.Security.MembershipCreateStatus
                    Dim newUser As MembershipUser = Membership.CreateUser(strusername, "ZAQ!zaq1", stremail, "Where do you work?", "VOA", True, createstatus)
                    lblUsers.Text = lblUsers.Text + "  Status:  " & createstatus.ToString()
                    Dim UserID As String = newUser.ProviderUserKey.ToString()
                    'newUser.Comment = comment.Text
                    'Membership.UpdateUser(newUser)

                    ' Add Roles.
                    Roles.AddUserToRole(strusername, "Member")

                    'disable account if IMSG or previously disabled
                    If (datareader("disabled") = 1 Or (stremail.ToLower.Contains("imsg") = True)) Then
                        newUser.IsApproved = False
                        Membership.UpdateUser(newUser)
                    End If

                    ' Insert New User Information into database 
                    Dim db1 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
                    Dim sqlcommand1 As String = "MigratePersonUpdate"
                    Dim objCommand1 As DbCommand = db1.GetStoredProcCommand(sqlcommand1)
                    db1.AddInParameter(objCommand1, "@UserId", DbType.String, UserID)
                    db1.AddInParameter(objCommand1, "@FirstName", DbType.String, datareader("FirstName"))
                    db1.AddInParameter(objCommand1, "@LastName", DbType.String, datareader("LastName"))
                    db1.AddInParameter(objCommand1, "@Title", DbType.String, datareader("Title"))
                    db1.AddInParameter(objCommand1, "@PhoneNumber", DbType.String, datareader("PhoneNumber"))
                    db1.AddInParameter(objCommand1, "@AltPhone", DbType.String, datareader("CellPhone"))
                    db1.AddInParameter(objCommand1, "@FaxNumber", DbType.String, datareader("FaxNumber"))
                    db1.AddInParameter(objCommand1, "@Address1", DbType.String, datareader("Address1"))
                    db1.AddInParameter(objCommand1, "@Address2", DbType.String, datareader("Address2"))
                    db1.AddInParameter(objCommand1, "@City", DbType.String, datareader("City"))
                    db1.AddInParameter(objCommand1, "@StateId", DbType.Int32, datareader("StateId"))
                    db1.AddInParameter(objCommand1, "@ZipCode", DbType.String, datareader("ZipCode"))
                    'db1.AddInParameter(objCommand1, "@CompanyName", DbType.String, datareader("CompanyName"))
                    db1.AddInParameter(objCommand1, "@CompanyId", DbType.Int32, datareader("CompanyID"))
                    db1.AddInParameter(objCommand1, "@LoginID", DbType.Int32, datareader("LoginID"))
                    db1.ExecuteNonQuery(objCommand1)

                    lblUsers.Text = lblUsers.Text + "  :Information Saved:  "


                Catch ex As Exception
                    lblUsers.Text = lblUsers.Text + "<BR/>" + datareader("username") & "Message:" & ex.Message.ToString
                End Try


            End While
        End If
        datareader.Close()
    End Sub

    Protected Sub btnTree_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTree.Click
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "MigrateTree"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.ExecuteNonQuery(objCommand)
        lblTree.Text = "Tree structure imported"
    End Sub

    Protected Sub btnRoles_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRoles.Click
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "MigrateRoles"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        Dim datareader As SqlDataReader
        datareader = db.ExecuteReader(objCommand)
        If datareader.HasRows Then
            While datareader.Read
                Try
                    Roles.CreateRole(datareader("Name"))
                    lblRoles.text = lblRoles.text & datareader("Name") & ":  role was added.<BR/>"

                    'Save RoleType of the new role
                    Dim db1 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
                    Dim sqlcommand1 As String = "RoleDataInsert"
                    Dim objCommand1 As DbCommand = db.GetStoredProcCommand(sqlcommand1)
                    db1.AddInParameter(objCommand1, "@RoleName", DbType.String, datareader("Name"))
                    db1.AddInParameter(objCommand1, "@RoleTypeID", DbType.Int32, 1)
                    db1.AddInParameter(objCommand1, "@RoleCategoryID", DbType.Int32, 4)
                    db1.ExecuteNonQuery(objCommand1)
                Catch ex As Exception
                    Dim strErrorMsg As String = ex.Message
                    lblRoles.Text = lblRoles.Text & datareader("Name") & ":  ERROR" & strErrorMsg & "<BR/>"
                End Try
            End While
        End If
    End Sub

    Protected Sub btnRoleUsers_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRoleUsers.Click
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "MigrateUserRoles"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        Dim datareader As SqlDataReader
        datareader = db.ExecuteReader(objCommand)
        If datareader.HasRows Then
            While datareader.Read
                Try
                    If Roles.IsUserInRole(datareader("UserName"), datareader("Name")) = False Then
                        Roles.AddUserToRole(datareader("UserName"), datareader("Name"))
                        lblRoleUsers.Text = lblRoleUsers.Text & "Add " & datareader("UserName") & " to " & datareader("Name") & "<BR/>"
                    Else
                        lblRoleUsers.Text = lblRoleUsers.Text & "Already in Role " & datareader("UserName") & " : " & datareader("Name") & "<BR/>"
                    End If

                Catch ex As Exception
                    Dim strErrorMsg As String = ex.Message
                    lblRoleUsers.Text = lblRoleUsers.Text & datareader("UserName") & " : " & datareader("Name") & ":  ERROR" & strErrorMsg & "<BR/>"
                End Try
            End While
        End If
    End Sub

    Protected Sub btnTreeRoles_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTreeRoles.Click
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "MigrateTreeRoles"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.ExecuteNonQuery(objCommand)
        lblTreeRoles.Text = "Role Permissons added To Tree"
    End Sub

    Private Sub btnTreeUsers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTreeUsers.Click
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "MigrateTreeUsers"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.ExecuteNonQuery(objCommand)
        lblTreeUsers.Text = "User Permissons added To Tree"
    End Sub

    Protected Sub btnDocuments_Click(sender As Object, e As EventArgs) Handles btnDocuments.Click
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "MigrateDocuments"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.ExecuteNonQuery(objCommand)
        lblDocuments.Text = "Documents, Uploads, Document History and Document Audit Actions Migrated"
    End Sub
End Class