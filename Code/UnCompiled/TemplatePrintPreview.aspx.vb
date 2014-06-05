Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Public Class TemplatePrintPreview
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'login check
        Dim loginchk = New clsLoginCheck()
        If (Not loginchk.LoggedIn) Then
            Response.Redirect("~/login.aspx")
        End If

        If Not loginchk.Admin Then
            Response.Redirect("HomeMain.aspx")
        End If

        If Not Page.IsPostBack Then

            lblDate.Text = Date.Now.Date

            If Val(Request("TemplateID")) > 0 Then
                LoadMessage()
                LoadSignature()
                lblReferURL.Text = "/TemplateAddEdit.aspx?TemplateID=" & Request("TemplateID")
            Else
                Response.Redirect("/TemplateList.aspx")
            End If


        End If
    End Sub

    Private Sub LoadMessage()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "TemplateSpecificSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@TemplateID", DbType.Int32, Request("TemplateID"))
        Using datareader As IDataReader = db.ExecuteReader(objCommand)
            If datareader.Read Then
                lblClinicMessage.Text = datareader("Message")
            Else
                Response.Redirect("/TemplateList.aspx")
            End If
        End Using
    End Sub

    Private Sub LoadSignature()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "SignatureMessageDefaultSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        Using datareader As IDataReader = db.ExecuteReader(objCommand)
            If datareader.Read Then
                lblSignature.Text = datareader("Message")
            End If
        End Using
    End Sub

    Private Sub rgdAppointments_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdAppointments.NeedDataSource
        Dim dt As New DataTable()
        dt.Columns.Add("CurrentStatus")
        dt.Columns.Add("Type")
        dt.Columns.Add("AppointmentDate")
        dt.Columns.Add("AppointmentTime")
        dt.Columns.Add("ClinicName")

        Dim dr As DataRow = dt.NewRow()
        dr("CurrentStatus") = "FUTURE"
        dr("Type") = "REGULAR"
        dr("AppointmentDate") = "05/23/2014"
        dr("AppointmentTime") = "0800"
        dr("ClinicName") = "CLINICNAME"
        dt.Rows.Add(dr)

        rgdAppointments.DataSource = dt
    End Sub

    Private Sub lbnBack_Click(sender As Object, e As System.EventArgs) Handles lbnBack.Click
        Response.Redirect(lblReferURL.Text)
    End Sub
End Class