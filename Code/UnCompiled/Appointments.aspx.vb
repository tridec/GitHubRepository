Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class Appointments
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim loginchk = New clsLoginCheck()

        If (Not loginchk.LoggedIn) Then
            Response.Redirect("~/login.aspx")
        End If

        If loginchk.Admin Then
            lblIsEdit.Text = "True"
            pnlComments.Visible = True
            reSignature.Visible = True
            lblSignatureValue.Visible = False
        ElseIf loginchk.HasRole("AppointmentAdmin") Then
            lblIsEdit.Text = "False"
            pnlComments.Visible = True
            reSignature.Visible = True
            lblSignatureValue.Visible = False
        ElseIf loginchk.HasRole("AppointmentUser") Then
            lblIsEdit.Text = "False"
            pnlComments.Visible = False
            reSignature.Visible = False
            lblSignatureValue.Visible = True
            btnResetSignature.Visible = False
        Else
            Response.Redirect("~/HomeMain.aspx")
        End If

        If Session("AppointmentsGroup") Is Nothing Or Session("PatientDetails") Is Nothing Then
            Response.Redirect("AppointmentsList.aspx")
        End If

        If Not Page.IsPostBack Then
            LoadSignaturesDDL()

            Dim objPatient As clsPatient = Session("PatientDetails")
            Dim objAppointments() As clsAppointment = Session("AppointmentsGroup")

            If Session("AppointmentsGroup") Is Nothing Or Session("PatientDetails") Is Nothing Then
                Response.Redirect("AppointmentsList.aspx")
            Else
                lblAppointmentGroupID.Text = objAppointments(0).AppointmentGroupID
                lblTitle.Text += " - " & lblAppointmentGroupID.Text
            End If

            lblPatientNameValue.Text = objPatient.Name
            lblPatientAddressValue.Text = objPatient.Address
            lblCityStateZipValue.Text = objPatient.City & ", " & objPatient.State & ", " & objPatient.ZIP

            LoadLetterData()

            'If Request("PrintView") <> "" Then
            '    Dim strPrint As String = Request("PrintView")
            'End If
        End If
    End Sub

    Private Sub LoadSignaturesDDL()
        ddlSignature.Items.Clear()
        ddlSignature.SelectedValue = Nothing

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "SignatureSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        ddlSignature.DataSource = db.ExecuteDataSet(objCommand)
        ddlSignature.DataBind()
        ddlSignature.Items.Insert(0, New ListItem("None", "0"))

        ddlSignature.SelectedValue = 0

        'Set Default Selected. If there is already a signature saved this will be overwritten in LoadLetterData()
        Using Datareader As IDataReader = db.ExecuteReader(objCommand)
            Do While Datareader.Read
                If Datareader("DefaultEnabled") = 1 Then
                    ddlSignature.SelectedValue = Datareader("SignatureID")
                    reSignature.Content = Datareader("Message")
                End If
            Loop
        End Using
    End Sub

    Private Sub LoadLetterData()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "AppointmentGroupSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@AppointmentGroupID", DbType.String, lblAppointmentGroupID.Text)
        Using datareader As IDataReader = db.ExecuteReader(objCommand)
            If datareader.Read Then
                If datareader("DateSent") IsNot DBNull.Value Then
                    lblSendDateValue.Text = datareader("DateSent")
                    btnDelete.Visible = False
                End If
                If datareader("Comment") IsNot DBNull.Value Then
                    txtComments.Text = datareader("Comment")
                End If

                If datareader("SignatureMessage") IsNot DBNull.Value Then
                    lblSignatureValue.Text = datareader("SignatureMessage")
                    lblSignatureValueHidden.Text = datareader("SignatureMessage")
                    reSignature.Content = datareader("SignatureMessage")
                    lblSignatureMessageOriginal.Text = datareader("SignatureMessage")
                End If

                If datareader("SignatureID") Is DBNull.Value Then
                    'If Null then load the default enabled as its never been updated.
                ElseIf ddlSignature.Items.FindByValue(datareader("SignatureID")) Is Nothing Then
                    'If there is a Signature ID but cannot find it in the list then its been deleted. Readd it.
                    lblSignatureIDOriginal.Text = datareader("SignatureID")
                    ddlSignature.Items.Add(New ListItem(datareader("Title"), datareader("SignatureID")))
                    ddlSignature.SelectedValue = datareader("SignatureID")
                Else
                    'Else set it to what its supposed to be.
                    lblSignatureIDOriginal.Text = datareader("SignatureID")
                    ddlSignature.SelectedValue = datareader("SignatureID")
                End If

            End If
        End Using
    End Sub

    'Protected Function getChecked(intAppointmentTemplateID As Integer, intDefaultEnabled As Integer) As Boolean
    '    If lblAppointmentGroupID.Text > 0 Then
    '        If intAppointmentTemplateID > 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Else
    '        If intDefaultEnabled <> 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    End If
    'End Function

    Private Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        Session("AppointmentsGroup") = ""
        Response.Redirect("~/AppointmentsList.aspx")
    End Sub

    Private Sub DeleteAppointmentTemplates()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "AppointmentTemplateDelete"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@AppointmentGroupID", DbType.Int32, lblAppointmentGroupID.Text)
        db.ExecuteNonQuery(objCommand)

    End Sub

    Private Sub rgdAppointments_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdAppointments.NeedDataSource
        rgdAppointments.DataSource = Session("AppointmentsGroup")
    End Sub

    Public Sub rlvTemplate_NeedDataSource(sender As Object, e As Telerik.Web.UI.RadListViewNeedDataSourceEventArgs)
        Dim rlvTemplate As RadListView = DirectCast(sender, RadListView)


        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "AppointmentTemplateSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@VANSClinicID", DbType.Int32, DirectCast(rlvTemplate.NamingContainer, RadListViewDataItem).GetDataKeyValue("ClinicID"))

        rlvTemplate.DataSource = db.ExecuteDataSet(objCommand)
    End Sub

    Private Function ValidatePage() As Boolean
        For Each item As RadListViewItem In rlvAppointments.Items
            Dim imgExpandCollapse As HtmlImage = item.FindControl("imgExpandCollapse")
            Dim rlvTemplate As RadListView = item.FindControl("rlvTemplate")
            If rlvTemplate.Visible = False Then
                rlvTemplate.Visible = True
                imgExpandCollapse.Src = "../images/chevronup.png"
            End If
        Next

        Page.Validate()
        If Page.IsValid = False Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub SaveAppointment()

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "AppointmentGroupInsertUpdate"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)

        If pnlComments.Visible = True Then
            db.AddInParameter(objCommand, "@Comment", DbType.String, txtComments.Text)
        End If

        Dim strMesssage As String
        If reSignature.Visible = True Then
            strMesssage = reSignature.Content
        Else
            strMesssage = lblSignatureValueHidden.Text
        End If

        db.AddInParameter(objCommand, "@SignatureMessage", DbType.String, strMesssage)
        db.AddInParameter(objCommand, "@SignatureID", DbType.Int32, ddlSignature.SelectedValue)

        db.AddInParameter(objCommand, "@AppointmentGroupID", DbType.Int32, lblAppointmentGroupID.Text)

        db.ExecuteNonQuery(objCommand)

        DeleteAppointmentTemplates()
        InsertAppointmentTemplates()
        LoadLetterData()
        'rlvAppointments.Rebind()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "MyScript", "alert('Letter information has been saved.');", True)
    End Sub

    Private Sub InsertAppointmentTemplates()

        For Each AppointmentItem As RadListViewDataItem In rlvAppointments.Items
            Dim strVANSAppointmentID As String = rlvAppointments.Items(AppointmentItem.DataItemIndex).GetDataKeyValue("VANSAppointmentID")
            Dim rlvTemplate As RadListView = AppointmentItem.FindControl("rlvTemplate")

            For Each TemplateItem As RadListViewDataItem In rlvTemplate.Items
                Dim chkAppointment As CheckBox = TemplateItem.FindControl("chkAppointment")
                If chkAppointment.Checked = True Then

                    Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
                    Dim sqlcommand As String = "AppointmentTemplateInsert"
                    Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
                    db.AddInParameter(objCommand, "@AppointmentGroupID", DbType.Int32, lblAppointmentGroupID.Text)
                    db.AddInParameter(objCommand, "@TemplateID", DbType.Int32, DirectCast(TemplateItem.FindControl("lblTemplateID"), Label).Text)
                    db.AddInParameter(objCommand, "@TemplateMessage", DbType.String, IIf(lblIsEdit.Text = "True", DirectCast(TemplateItem.FindControl("txtTemplateMessage"), TextBox).Text, DirectCast(TemplateItem.FindControl("lblTemplateMessageHidden"), Label).Text))
                    db.AddInParameter(objCommand, "@VANSAppointmentID", DbType.String, strVANSAppointmentID)
                    db.ExecuteNonQuery(objCommand)
                End If
            Next
        Next
    End Sub

    Private Function clearChecked() As Boolean
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "AppointmentTemplateGroupSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@AppointmentGroupID", DbType.Int32, lblAppointmentGroupID.Text)
        Using datareader As IDataReader = db.ExecuteReader(objCommand)
            If datareader.Read Then
                Return True
            End If
        End Using
        Return False
    End Function

    Public Sub rlvTemplate_PreRender(sender As Object, e As System.EventArgs)
        If Not Page.IsPostBack Then
            Dim rlvTemplate As RadListView = DirectCast(sender, RadListView)

            'If its been saved before, clear all checkboxes that were default enabled.
            If clearChecked() = True Then
                For Each itm As RadListViewItem In rlvTemplate.Items
                    'clear checkboxes if its been saved before as they are currently set by "Template Default Enabled"
                    Dim chkAppointment As CheckBox = itm.FindControl("chkAppointment")
                    chkAppointment.Checked = False
                Next
            End If

            'Show No Available Templates Label
            If rlvTemplate.Items.Count = 0 Then
                Dim lblNoTemplates As Label = rlvTemplate.Parent.FindControl("lblNoTemplates")
                lblNoTemplates.Visible = True

            End If

            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommand As String = "AppointmentTemplateSpecificSelect"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
            db.AddInParameter(objCommand, "@AppointmentGroupID", DbType.Int32, lblAppointmentGroupID.Text)
            db.AddInParameter(objCommand, "@VANSAppointmentID", DbType.String, DirectCast(rlvTemplate.NamingContainer, RadListViewDataItem).GetDataKeyValue("VANSAppointmentID"))
            Using datareader As IDataReader = db.ExecuteReader(objCommand)
                Do While datareader.Read
                    For Each itm As RadListViewItem In rlvTemplate.Items
                        'clear checkboxes if its been saved before as they are currently set by "Template Default Enabled"
                        Dim lblAppointmentTemplateID As Label = itm.FindControl("lblAppointmentTemplateID")

                        'If Item is not already set, then check it.
                        Dim lblTemplateID As Label = itm.FindControl("lblTemplateID")
                        If lblTemplateID.Text = datareader("TemplateID") Then
                            Dim txtTemplateMessage As TextBox = itm.FindControl("txtTemplateMessage")
                            Dim lblTemplateMessage As Label = itm.FindControl("lblTemplateMessage")

                            lblAppointmentTemplateID.Text = datareader("AppointmentTemplateID")
                            lblTemplateMessage.Text = datareader("TemplateMessage")
                            txtTemplateMessage.Text = datareader("TemplateMessage")
                            Dim chkAppointment As CheckBox = itm.FindControl("chkAppointment")
                            chkAppointment.Checked = True
                        End If
                    Next
                Loop
            End Using
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        If ValidatePage() = False Then
            Exit Sub
        End If
        SaveAppointment()
        LoadSignaturesDDL()
        LoadLetterData()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "AppointmentAllDataDelete"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@AppointmentGroupID", DbType.Int32, lblAppointmentGroupID.Text)
        db.ExecuteNonQuery(objCommand)
        Session("AppointmentsGroup") = ""
        Response.Redirect("AppointmentsList.aspx")
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As System.EventArgs) Handles btnPreview.Click
        If ValidatePage() = False Then
            Exit Sub
        End If
        SaveAppointment()
        Response.Redirect("AppointmentPrintPreview.aspx?AppointmentGroupID=" & lblAppointmentGroupID.Text)
    End Sub

    Private Sub rlvAppointments_NeedDataSource(sender As Object, e As Telerik.Web.UI.RadListViewNeedDataSourceEventArgs) Handles rlvAppointments.NeedDataSource
        rlvAppointments.DataSource = Session("AppointmentsGroup")
    End Sub

    Public Sub lbnExpand_Click(sender As Object, e As System.EventArgs)
        Dim lbnExpand As LinkButton = DirectCast(sender, LinkButton)
        Dim rlvTemplate As RadListView = lbnExpand.FindControl("rlvTemplate")
        Dim imgExpandCollapse As HtmlImage = lbnExpand.FindControl("imgExpandCollapse")

        rlvTemplate.Visible = Not rlvTemplate.Visible
        If rlvTemplate.Visible = True Then
            imgExpandCollapse.Src = "../images/chevronup.png"
            'lbnExpand.Text = "-"
        Else

            imgExpandCollapse.Src = "../images/chevrondown.png"
            'lbnExpand.Text = "+"
        End If
    End Sub

    Private Sub ddlSignature_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSignature.SelectedIndexChanged
        If ddlSignature.SelectedValue = 0 Then
            reSignature.Content = ""
        ElseIf ddlSignature.SelectedValue = Val(lblSignatureIDOriginal.Text) Then
            reSignature.Content = lblSignatureMessageOriginal.Text
            lblSignatureValue.Text = lblSignatureMessageOriginal.Text
            lblSignatureValueHidden.Text = lblSignatureMessageOriginal.Text
        Else
            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlCommand As String = "SignatureSelect"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
            db.AddInParameter(objCommand, "@SignatureID", DbType.Int32, ddlSignature.SelectedValue)
            Using datareader As IDataReader = db.ExecuteReader(objCommand)
                If datareader.Read Then
                    reSignature.Content = datareader("Message")
                    lblSignatureValue.Text = datareader("Message")
                    lblSignatureValueHidden.Text = datareader("Message")
                End If
            End Using
        End If

    End Sub

    Private Sub btnResetSignature_Click(sender As Object, e As System.EventArgs) Handles btnResetSignature.Click
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "SignatureMessageSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@SignatureID", DbType.Int32, ddlSignature.SelectedValue)
        Using datareader As IDataReader = db.ExecuteReader(objCommand)
            If datareader.Read Then
                reSignature.Content = datareader("Message")
            End If
        End Using
        If ddlSignature.SelectedValue = 0 Then
            reSignature.Content = ""
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "MyScript", "alert('Signature reset.');", True)
    End Sub
End Class