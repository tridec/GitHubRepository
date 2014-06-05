Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Public Class TemplateByClinicPrintPreview
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim intcount As Integer = 0

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
            dt.Columns.Add(New DataColumn("AppointmentDate", GetType(Date)))
            dt.Columns.Add(New DataColumn("AppointmentTime", GetType(String)))
            dt.Columns.Add(New DataColumn("ClinicName", GetType(String)))

            If Val(Request("ClinicID")) = 0 Then
                Response.Redirect("TemplateSearch.aspx")
            Else
                lblClinicID.Text = Request("ClinicID")
            End If

            SetClinicName()
            LoadSignature()
        End If
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

    Public Sub SetClinicName()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "ClinicSpecificSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@ClinicID", DbType.Int32, lblClinicID.Text)
        Using Datareader As IDataReader = db.ExecuteReader(objCommand)
            Do While Datareader.Read
                lblClinicName.Text = Datareader("Name")
                lblLegend.Text = "05/23/2014:0930 " & lblClinicName.Text
            Loop
        End Using

    End Sub

    Public Sub rlvTemplate_NeedDataSource(sender As Object, e As Telerik.Web.UI.RadListViewNeedDataSourceEventArgs)
        'Dim rlvTemplate As RadListView = DirectCast(sender, RadListView)

        'Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        'Dim sqlcommand As String = "TemplateSpecificSelect"
        'Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        'db.AddInParameter(objCommand, "@TemplateID", DbType.String, DirectCast(rlvTemplate.NamingContainer, RadListViewDataItem).GetDataKeyValue("TemplateID"))

        Dim rlvTemplate As RadListView = DirectCast(sender, RadListView)

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "ClinicTemplateAssignedSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@ClinicID", DbType.Int32, lblClinicID.Text)
        rlvTemplate.DataSource = db.ExecuteDataSet(objCommand)

        Using datareader As IDataReader = db.ExecuteReader(objCommand)
            If Not datareader.Read Then
                pnlTemplate.Visible = False
            End If
        End Using

    End Sub

    Private Sub lbnBack_Click(sender As Object, e As System.EventArgs) Handles lbnBack.Click
        Response.Redirect("TemplateSearch.aspx")
    End Sub

    'Private Sub rlvAppointments_ItemDataBound(sender As Object, e As Telerik.Web.UI.RadListViewItemEventArgs) Handles rlvAppointments.ItemDataBound
    '    Dim dr As DataRow = dt.NewRow()
    '    Dim dtDate As Date = Date.Today.AddDays(intcount).ToShortDateString
    '    intcount -= 1
    '    Dim lblLegend As Label = e.Item.FindControl("lblLegend")
    '    lblLegend.Text = dtDate.ToShortDateString & ":" & IIf(intcount Mod 2 = 0, "0800", "1030") & " " & lblClinicName.Text

    '    dr("AppointmentDate") = dtDate
    '    dr("AppointmentTime") = IIf(intcount Mod 2 = 0, "0800", "1030")
    '    dr("ClinicName") = lblClinicName.Text
    '    dt.Rows.Add(dr)
    'End Sub

    'Private Sub rlvAppointments_NeedDataSource(sender As Object, e As Telerik.Web.UI.RadListViewNeedDataSourceEventArgs) Handles rlvAppointments.NeedDataSource
    '    Dim rlvTemplate As RadListView = DirectCast(sender, RadListView)

    '    Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommand As String = "ClinicTemplateAssignedSelect"
    '    Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
    '    db.AddInParameter(objCommand, "@ClinicID", DbType.Int32, lblClinicID.Text)

    '    Using datareader As IDataReader = db.ExecuteReader(objCommand)
    '        Do While datareader.Read
    '            lblClinicName.Text = datareader("Name")
    '        Loop
    '    End Using


    '    rlvTemplate.DataSource = db.ExecuteDataSet(objCommand)
    'End Sub


    'Private Sub rlvAppointments_PreRender(sender As Object, e As System.EventArgs) Handles rlvAppointments.PreRender
    '    rgdAppointments.DataSource = dt
    '    rgdAppointments.DataBind()
    'End Sub

    Private Sub rgdAppointments_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdAppointments.NeedDataSource
        Dim dt As New DataTable
        dt.Columns.Add("AppointmentDate")
        dt.Columns.Add("AppointmentTime")
        dt.Columns.Add("ClinicName")
        Dim dr As DataRow = dt.NewRow()
        dr("AppointmentDate") = "05/23/2014"
        dr("AppointmentTime") = "0930"
        dr("ClinicName") = lblClinicName.Text
        dt.Rows.Add(dr)
        rgdAppointments.DataSource = dt

    End Sub
End Class