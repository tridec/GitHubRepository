Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Public Class AppointmentPrintPreview
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'login check
        Dim loginchk = New clsLoginCheck()
        If (Not loginchk.LoggedIn) Then
            Response.Redirect("~/login.aspx")
        End If

        If Not (loginchk.Admin Or loginchk.HasRole("AppointmentAdmin") Or loginchk.HasRole("AppointmentUser")) Then
            Response.Redirect("~/HomeMain.aspx")
        End If

        If Not Page.IsPostBack Then
            If Request.UrlReferrer Is Nothing Then
                lblReferURL.Text = "/AppointmentsList.aspx"
            Else
                lblReferURL.Text = Request.UrlReferrer.AbsolutePath.ToString
            End If

            If Val(Request("AppointmentGroupID")) = 0 Or Session("PatientDetails") Is Nothing Or Session("AppointmentsGroup") Is Nothing Then
                Response.Redirect(lblReferURL.Text)
            Else
                lblAppointmentGroupID.Text = Request("AppointmentGroupID")
            End If

            lblDate.Text = Date.Now.Date

            LoadComments()
            LoadDemographicInfo()
        End If

        If Session("PatientDetails") Is Nothing Or Session("AppointmentsGroup") Is Nothing Then
            Response.Redirect("/AppointmentsList.aspx")
        End If
    End Sub

    Private Sub LoadComments()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "AppointmentGroupSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@AppointmentGroupID", DbType.String, lblAppointmentGroupID.Text)
        Using datareader As IDataReader = db.ExecuteReader(objCommand)
            If datareader.Read Then
                If datareader("Comment") IsNot DBNull.Value Then
                    lblComments.Text = datareader("Comment")
                End If
                If lblComments.Text = "" Then
                    lblComments.Text = "None"
                End If
                If datareader("SignatureMessage") IsNot DBNull.Value Then
                    lblSignature.Text = datareader("SignatureMessage")
                End If
            End If
        End Using

    End Sub

    Private Sub LoadDemographicInfo()
        Dim objPatient As clsPatient = Session("PatientDetails")
        'Dim objAppointment As clsAppointment = Session("AppointmentDetails")
        'Dim lstAppointments As List(Of clsAppointment) = Session("AppointmentsGroup")

        lblTopMessage.Text = "You have the following appointment(s) scheduled:"

        lblNameAddressHeader.Text = objPatient.Name & "<br />"
        lblNameAddressHeader.Text += objPatient.Address & "<br />"
        lblNameAddressHeader.Text += objPatient.City & ", " & objPatient.State & ", " & objPatient.ZIP

        lblPatientNameValue.Text += objPatient.Name & ","
    End Sub

    Public Sub rlvTemplate_NeedDataSource(sender As Object, e As Telerik.Web.UI.RadListViewNeedDataSourceEventArgs)
        Dim rlvTemplate As RadListView = DirectCast(sender, RadListView)

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "AppointmentTemplatePrintPreviewSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@AppointmentGroupID", DbType.Int32, lblAppointmentGroupID.Text)
        db.AddInParameter(objCommand, "@VANSAppointmentID", DbType.String, DirectCast(rlvTemplate.NamingContainer, RadListViewDataItem).GetDataKeyValue("VANSAppointmentID"))

        rlvTemplate.DataSource = db.ExecuteDataSet(objCommand)
    End Sub

    Private Sub lbnBack_Click(sender As Object, e As System.EventArgs) Handles lbnBack.Click
        Response.Redirect(lblReferURL.Text)
    End Sub

    Private Sub rlvAppointments_NeedDataSource(sender As Object, e As Telerik.Web.UI.RadListViewNeedDataSourceEventArgs) Handles rlvAppointments.NeedDataSource
        rlvAppointments.DataSource = Session("AppointmentsGroup")
    End Sub

    Private Sub rgdAppointments_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdAppointments.NeedDataSource
        rgdAppointments.DataSource = Session("AppointmentsGroup")
    End Sub

    Private Sub SaveDate()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "AppointmentGroupUpdateDate"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@AppointmentGroupID", DbType.Int32, lblAppointmentGroupID.Text)
        db.ExecuteNonQuery(objCommand)
    End Sub

    Private Sub lbnEmail_Click(sender As Object, e As System.EventArgs) Handles lbnEmail.Click
        SaveDate()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "MyScript", "alert('Letter has been released to patient for viewing.');", True)
    End Sub

    Private Sub lbnPrint_Click(sender As Object, e As System.EventArgs) Handles lbnPrint.Click
        SaveDate()
    End Sub

    Private Sub rlvAppointments_PreRender(sender As Object, e As System.EventArgs) Handles rlvAppointments.PreRender
        For Each itm As RadListViewDataItem In rlvAppointments.Items
            If DirectCast(itm.FindControl("rlvTemplate"), RadListView).Items.Count = 0 Then
                itm.Visible = False
            End If
        Next
    End Sub
End Class