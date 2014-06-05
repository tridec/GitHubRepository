Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Web.UI.Page
Imports System.DirectoryServices
Imports System.Collections
Imports System

Partial Public Class UserInformation
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
   
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'login check
        Dim loginchk = New clsLoginCheck()
        If (Not loginchk.LoggedIn) Then
            Response.Redirect("~/login.aspx")
        End If

      

        If Not Page.IsPostBack Then
            'LoadDropDowns()
            LoadUserInfo()
            'CheckForVendorEntry()
            'LoadBusinessTypeData()

        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        ' Save user data and revert back to read-only view
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "PersonUpdate"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        ' Maybe modify to use the login class
        Dim m As MembershipUser = Membership.GetUser()
        m.ProviderUserKey.ToString()
        db.AddInParameter(objCommand, "@UserId", DbType.String, m.ProviderUserKey.ToString())
        db.AddInParameter(objCommand, "@FirstName", DbType.String, txtFirstName.Text)
        db.AddInParameter(objCommand, "@LastName", DbType.String, txtLastName.Text)
        'db.AddInParameter(objCommand, "@Title", DbType.String, txtTitle.Text)
        'db.AddInParameter(objCommand, "@CompanyID", DbType.Int32, ddlCompany.SelectedValue)
        'db.AddInParameter(objCommand, "@UserRegionID", DbType.Int32, ddlRegion.SelectedValue)
        'db.AddInParameter(objCommand, "@Address1", DbType.String, txtAddress1.Text)
        'db.AddInParameter(objCommand, "@Address2", DbType.String, txtAddress2.Text)
        'db.AddInParameter(objCommand, "@City", DbType.String, txtCity.Text)
        'If ddlState.SelectedValue > 0 Then
        '    db.AddInParameter(objCommand, "@StateId", DbType.Int32, ddlState.SelectedValue)
        'Else
        '    db.AddInParameter(objCommand, "@StateId", DbType.Int32, 0)
        'End If
        'db.AddInParameter(objCommand, "@ZipCode", DbType.String, txtZipCode.Text)
        'db.AddInParameter(objCommand, "@PhoneNumber", DbType.String, txtPhone.Text)
        'db.AddInParameter(objCommand, "@AltPhone", DbType.String, txtAltPhone.Text)
        'db.AddInParameter(objCommand, "@FaxNumber", DbType.String, txtFax.Text)
        db.ExecuteNonQuery(objCommand)

        'Redirect New User first time after saving information
        'If hfNewUser.Value = "True" Then
        '    If Not (Session("VAi2") Is Nothing) Then
        '        Response.Redirect(Session("VAi2").ToString)
        '    Else
        '        Response.Redirect("default.aspx")
        '    End If
        'End If
        'rapUserInfoView.Visible = True
        'rapUserInfoEdit.Visible = False


        LoadUserInfo()

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        ' don't save and go back to read-only view
        rapUserInfoView.Visible = True
        rapUserInfoEdit.Visible = False
        LoadUserInfo()
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        rapUserInfoView.Visible = False
        rapUserInfoEdit.Visible = True
        LoadUserInfo()
    End Sub

    'Protected Sub LoadDropDowns()
    '    Dim dbState As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommandState As String = "StateSelect"
    '    Dim objCommandState As DbCommand = dbState.GetStoredProcCommand(sqlcommandState)
    '    Dim datareaderState As SqlDataReader = dbState.ExecuteReader(objCommandState)
    '    ddlState.DataSource = datareaderState
    '    ddlState.DataBind()
    '    ddlState.Items.Insert(0, New ListItem("Please Select", "0"))
    '    ddlState.SelectedValue = "0"

    '    Dim dbRegion As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommandRegion As String = "UserRegionSelect"
    '    Dim objCommandRegion As DbCommand = dbRegion.GetStoredProcCommand(sqlcommandRegion)
    '    Dim datareaderRegion As SqlDataReader = dbRegion.ExecuteReader(objCommandRegion)
    '    ddlRegion.DataSource = datareaderRegion
    '    ddlRegion.DataBind()
    '    ddlRegion.Items.Insert(0, New ListItem("Please Select", "0"))
    '    ddlRegion.SelectedValue = "0"

    '    Dim dbCompany As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommandCompany As String = "CompanySelect"
    '    Dim objCommandCompany As DbCommand = dbCompany.GetStoredProcCommand(sqlcommandCompany)
    '    Dim datareaderCompany As SqlDataReader = dbCompany.ExecuteReader(objCommandCompany)
    '    ddlCompany.DataSource = datareaderCompany
    '    ddlCompany.DataBind()
    '    ddlCompany.Items.Insert(0, New ListItem("Please Select", "0"))
    '    ddlCompany.SelectedValue = "0"






    'End Sub

    Protected Sub LoadUserInfo()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "PersonUserSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        Dim m As MembershipUser = Membership.GetUser()
        db.AddInParameter(objCommand, "@UserId", DbType.String, m.ProviderUserKey.ToString())
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        lblUserNameValue.Text = m.UserName
        lblUserDisplay.Text = "(" + m.UserName + ")"
        If datareader.HasRows Then
            Do While datareader.Read()
                If (datareader("PersonID") Is System.DBNull.Value) Then
                    ' USER INFO NOT FILLED IN - OPEN INFO and enable EDIT
                    rapUserInfoView.Visible = False
                    rapUserInfoEdit.Visible = True
                    rtsUserInfo.SelectedIndex = 0
                    rtsUserInfo.Tabs.Item(1).Enabled = False
                    btnCancel.Enabled = False
                    hfNewUser.Value = "True"

                Else
                    lblUserNameEditValue.Text = datareader("UserName")
                    lblUserNameViewValue.Text = datareader("UserName")

                    lblFirstNameViewValue.Text = datareader("FirstName")
                    txtFirstName.Text = datareader("FirstName")

                    lblLastNameViewValue.Text = datareader("LastName")
                    txtLastName.Text = datareader("LastName")

                    'lblTitleViewValue.Text = datareader("Title")
                    'txtTitle.Text = datareader("Title")

                    'lblCompanyNameViewValue.Text = datareader("CompanyName")
                    'If Not (ddlCompany.Items.FindByValue(datareader("CompanyID")) Is Nothing) Then
                    '    ddlCompany.SelectedValue = datareader("CompanyID")
                    'End If

                    'lblRegionViewValue.Text = datareader("Region")
                    'If Not (ddlRegion.Items.FindByValue(datareader("UserRegionID")) Is Nothing) Then
                    '    ddlRegion.SelectedValue = datareader("UserRegionID")
                    'End If

                    lblEmailViewValue.Text = datareader("Email")
                    txtEmail.Text = datareader("Email")
                    txtEmail.Enabled = False

                    'lblAddress1ViewValue.Text = datareader("Address1")
                    'txtAddress1.Text = datareader("Address1")

                    'lblAddress2ViewValue.Text = datareader("Address2")
                    'txtAddress2.Text = datareader("Address2")

                    'lblCityViewValue.Text = datareader("City")
                    'txtCity.Text = datareader("City")

                    'lblStateViewValue.Text = datareader("StateName")
                    'ddlState.SelectedValue = datareader("StateID")

                    'lblZipCodeViewValue.Text = datareader("ZipCode")
                    'txtZipCode.Text = datareader("ZipCode")

                    'lblPhoneViewValue.Text = datareader("PhoneNumber")
                    'txtPhone.Text = datareader("PhoneNumber")

                    'lblAltPhoneViewValue.Text = datareader("AltPhone")
                    'txtAltPhone.Text = datareader("AltPhone")

                    'lblFaxViewValue.Text = datareader("FaxNumber")
                    'txtFax.Text = datareader("FaxNumber")

                    lblUserDisplay.Text = datareader("FirstName") + " " + lblUserDisplay.Text
                    lblUserDisplay.Text = ", " + lblUserDisplay.Text
                    lblUserDisplay.Text = datareader("LastName") + lblUserDisplay.Text

                    rtsUserInfo.Tabs.Item(1).Enabled = True
                End If
            Loop


        End If
        datareader.Close()
    End Sub

    Protected Sub rtsUserInfo_TabClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTabStripEventArgs) Handles rtsUserInfo.TabClick
        If rtsUserInfo.SelectedTab.Text = "Account Information" Then
            Dim m As MembershipUser = Membership.GetUser()
            m.ProviderUserKey.ToString()
            lblUserNameValue.Text = m.UserName
            rpvAccountInfo.Focus()
        End If
        If rtsUserInfo.SelectedTab.Text = "Contact Information" Then
            rpvContactInfo.Focus()
        End If
    End Sub

    Protected Sub btnUpdateSecurity_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdateSecurity.Click
        Dim password, newSecurityQuestion, newSecurityAnswer As String

        Dim success As Boolean = False
        password = txtCurrentPassword.Text
        newSecurityQuestion = ddlQuestion.Text
        newSecurityAnswer = Me.txtAnswer.Text
        success = Membership.GetUser(User.Identity.Name).ChangePasswordQuestionAndAnswer(password, newSecurityQuestion, newSecurityAnswer)

        If success = True Then
            lblSuccess.Text = "* Your security question has been updated."
        Else
            lblSuccess.Text = "* Your current password is incorrect."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Invalid File Type", _
            String.Format("alert('{0}');", "Your current password is incorrect"), True)
        End If
    End Sub

    Private Sub ChangePassword1_ChangePasswordError(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChangePassword1.ChangePasswordError
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Invalid Password Change", _
        String.Format("alert('{0}');", "Password Incorrect or New Password Invalid"), True)
    End Sub

    'Protected Sub CheckForVendorEntry()
    '    'check for entries to see if logged in user has vendor related items. if no, then allow cage code editing.
    '    Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommand As String = "ectosIDIQSelectByUser"
    '    Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
    '    Dim m As MembershipUser = Membership.GetUser()
    '    m.ProviderUserKey.ToString()
    '    db.AddInParameter(objCommand, "@UserID", DbType.String, m.ProviderUserKey.ToString())
    '    db.AddInParameter(objCommand, "@ContractStatus", DbType.Int32, 0)
    '    Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
    '    If Not datareader.HasRows Then
    '        tdCCRCageCodeEdit.Visible = True
    '        tdCCRCageCodeView.Visible = False
    '    Else
    '        tdCCRCageCodeEdit.Visible = False
    '        tdCCRCageCodeView.Visible = True
    '    End If
    '    datareader.Close()
    'End Sub


    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    ' login check
    '    Dim loginchk = New clsLoginCheck()
    '    Response.Cache.SetCacheability(HttpCacheability.NoCache)

    '    If Not loginchk.LoggedIn Then
    '        Response.Redirect("~/login.aspx")
    '    End If

    '    If Not Page.IsPostBack Then
    '        LoadData()
    '    End If
    'End Sub

    'Private Sub LoadData()

    '    Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommand As String = "PersonUserSelect"
    '    Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
    '    ' Maybe modify to use the login class
    '    Dkim m As MembershipUser = Membership.GetUser()
    '    m.ProviderUserKey.ToString()
    '    lblEmailViewValue.Text = m.Email
    '    db.AddInParameter(objCommand, "@UserId", DbType.String, m.ProviderUserKey.ToString())
    '    Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
    '    lblUserDisplay.Text = m.UserName
    '    lblUserDisplay.Text = "(" + m.UserName + ")" + "  "


    '    If datareader.HasRows Then
    '        Do While datareader.Read()
    '            If Not (datareader("CompanyName") Is System.DBNull.Value) Then
    '                lblCompanyNameViewValue.Text = datareader("CompanyName")
    '            Else
    '                lblCompanyNameViewValue.Text = ""
    '            End If
    '            'If Not (datareader("DeptOrgName") Is System.DBNull.Value) Then
    '            '    lblDeptOrgNameViewValue.Text = datareader("DeptOrgName")
    '            'Else
    '            '    lblDeptOrgNameViewValue.Text = ""
    '            'End If
    '            If Not (datareader("FirstName") Is System.DBNull.Value) Then
    '                lblFirstNameViewValue.Text = datareader("FirstName")
    '                lblUserDisplay.Text = lblUserDisplay.Text + datareader("FirstName") + " "
    '            Else
    '                lblFirstNameViewValue.Text = ""
    '            End If
    '            If Not (datareader("LastName") Is System.DBNull.Value) Then
    '                lblLastNameViewValue.Text = datareader("LastName")
    '                lblUserDisplay.Text = lblUserDisplay.Text + datareader("LastName") + " "
    '            Else
    '                lblLastNameViewValue.Text = ""
    '            End If
    '            If Not (datareader("Title") Is System.DBNull.Value) Then
    '                lblTitleViewValue.Text = datareader("Title")
    '            Else
    '                lblTitleViewValue.Text = ""
    '            End If
    '            If Not (datareader("Address1") Is System.DBNull.Value) Then
    '                lblAddress1ViewValue.Text = datareader("Address1")
    '            Else
    '                lblAddress1ViewValue.Text = ""
    '            End If
    '            If Not (datareader("City") Is System.DBNull.Value) Then
    '                lblCityViewValue.Text = datareader("City")
    '            Else
    '                lblCityViewValue.Text = ""
    '            End If
    '            If Not (datareader("StateName") Is System.DBNull.Value) Then
    '                lblStateViewValue.Text = datareader("StateName")
    '            Else
    '                lblStateViewValue.Text = ""
    '            End If
    '            If Not (datareader("ZipCode") Is System.DBNull.Value) Then
    '                lblZipCodeViewValue.Text = datareader("ZipCode")
    '            Else
    '                lblZipCodeViewValue.Text = ""
    '            End If
    '            If Not (datareader("PhoneNumber") Is System.DBNull.Value) Then
    '                lblPhoneViewValue.Text = datareader("PhoneNumber")
    '            Else
    '                lblPhoneViewValue.Text = ""
    '            End If
    '        Loop
    '    End If

    '    datareader.Close()
    'End Sub
End Class