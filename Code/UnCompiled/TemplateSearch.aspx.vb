
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports VANS.us.vacloud.devmdws

Public Class TemplateSearch
    Inherits System.Web.UI.Page
    Dim objVANS As New clsVANS

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'login check
        Dim loginchk = New clsLoginCheck()
        If (Not loginchk.LoggedIn) Then
            Response.Redirect("~/login.aspx")
        End If

        If Not Page.IsPostBack Then
            TemplateLoad()
        End If
    End Sub

    Private Sub rgdTemplatesAssigned_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgdTemplatesAssigned.ItemDataBound
        'If TypeOf e.Item Is GridDataItem Then
        '    Dim dataItem As GridDataItem = DirectCast(e.Item, GridDataItem)

        '    dataItem("TemplateID").Text = "<a href='TemplateAddEdit.aspx'>" & dataItem("TemplateID").Text & "</a>"

        '    If dataItem("Status").Text = "New" Then
        '        dataItem.BackColor = Drawing.Color.LightGreen
        '    End If
        'End If
    End Sub

    Private Sub TemplateLoad()
        Dim dt As New DataTable()
        rgdTemplatesAssigned.DataSource = dt

        Dim dt2 As New DataTable()
        rgdTemplatesAvailable.DataSource = dt
    End Sub

    Private Sub rgdClinics_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdClinics.NeedDataSource
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "ClinicSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        rgdClinics.DataSource = db.ExecuteDataSet(objCommand)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Response.Redirect("TemplateList.aspx")
    End Sub

    Private Sub rgdClinics_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgdClinics.SelectedIndexChanged

        Dim gdi As GridDataItem = rgdClinics.SelectedItems(0)
        lblClinicID.Text = gdi("ClinicID").Text
        rgdTemplatesAssigned.Rebind()
        rgdTemplatesAvailable.Rebind()
    End Sub

    Private Sub rgdTemplatesAssigned_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdTemplatesAssigned.NeedDataSource
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "ClinicTemplateAssignedSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@ClinicID", DbType.Int32, lblClinicID.Text)
        rgdTemplatesAssigned.DataSource = db.ExecuteDataSet(objCommand)
    End Sub

    Private Sub rgdTemplatesAvailable_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdTemplatesAvailable.NeedDataSource
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "ClinicTemplateAvailableSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@ClinicID", DbType.Int32, lblClinicID.Text)
        rgdTemplatesAvailable.DataSource = db.ExecuteDataSet(objCommand)
    End Sub

    Private Sub btnAddTemplate_Click(sender As Object, e As System.EventArgs) Handles btnAddTemplate.Click
        Response.Redirect("TemplateAddEdit.aspx?TemplateID=0")
    End Sub

    Protected Sub chkEnabledDefault_UpdateEnabled(sender As Object, e As EventArgs)
        Dim chkEnabledDefault As CheckBox = DirectCast(sender, CheckBox)
        Dim itm As GridDataItem = DirectCast(chkEnabledDefault.NamingContainer, GridDataItem)
        Dim int As Integer = itm("TemplateClinicID").Text

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "TemplateClinicUpdate"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@TemplateClinicID", DbType.Int32, itm("TemplateClinicID").Text)
        db.AddInParameter(objCommand, "@DefaultEnabled", DbType.Int32, chkEnabledDefault.Checked * -1)
        db.ExecuteNonQuery(objCommand)

    End Sub

    Private Sub rgdTemplatesAssigned_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgdTemplatesAssigned.UpdateCommand

        Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "TemplateClinicDelete"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@TemplateClinicID", DbType.Int32, itm("TemplateClinicID").Text)
        db.ExecuteNonQuery(objCommand)


        rgdTemplatesAvailable.Rebind()
        rgdTemplatesAssigned.Rebind()
    End Sub

    Private Sub rgdTemplatesAvailable_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgdTemplatesAvailable.UpdateCommand
        Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "TemplateClinicInsert"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@ClinicID", DbType.Int32, lblClinicID.Text)
        db.AddInParameter(objCommand, "@TemplateID", DbType.Int32, itm("TemplateID").Text)
        db.ExecuteNonQuery(objCommand)

        rgdTemplatesAvailable.Rebind()
        rgdTemplatesAssigned.Rebind()
    End Sub

    Private Function connectVANS() As Boolean
        objVANS.parentPage = HttpContext.Current.Handler
        If objVANS.connectVANS = False Then
            Return False
        End If

        Return True
    End Function

    Private Sub btnUpdateClinics_Click(sender As Object, e As System.EventArgs) Handles btnUpdateClinics.Click
        If connectVANS() = False Then
            Exit Sub
        End If

        Dim tgdClinics As TaggedHospitalLocationArray = objVANS.getClinics()
        If objVANS.ErrorMessage <> "" Then
            Exit Sub
        End If

        For Each itmClinic In tgdClinics.locations
            If Not (itmClinic.name.Contains("-X") Or itmClinic.name.Contains("-x")) Then
                Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
                Dim sqlCommand As String = "ClinicInsertUpdate"
                Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
                db.AddInParameter(objCommand, "@AskForCheckIn", DbType.Int32, itmClinic.askForCheckIn * -1)
                db.AddInParameter(objCommand, "@Bed", DbType.String, itmClinic.bed)
                db.AddInParameter(objCommand, "@Building", DbType.String, itmClinic.building)
                db.AddInParameter(objCommand, "@ClinicDisplayStartTime", DbType.String, itmClinic.clinicDisplayStartTime)
                db.AddInParameter(objCommand, "@Department", DbType.String, itmClinic.department.text)
                db.AddInParameter(objCommand, "@Facility", DbType.String, itmClinic.facility)
                db.AddInParameter(objCommand, "@Floor", DbType.String, itmClinic.floor)
                db.AddInParameter(objCommand, "@Name", DbType.String, itmClinic.name)
                db.AddInParameter(objCommand, "@Phone", DbType.String, itmClinic.phone)
                db.AddInParameter(objCommand, "@PhysicalLocation", DbType.String, itmClinic.physicalLocation)
                db.AddInParameter(objCommand, "@Room", DbType.String, itmClinic.room)
                db.AddInParameter(objCommand, "@Service", DbType.String, itmClinic.service.text)
                db.AddInParameter(objCommand, "@Specialty", DbType.String, itmClinic.specialty.text)
                db.AddInParameter(objCommand, "@Status", DbType.String, itmClinic.status)
                db.AddInParameter(objCommand, "@Type", DbType.String, itmClinic.type)
                db.AddInParameter(objCommand, "@VANSClinicID", DbType.Int32, itmClinic.id)
                db.ExecuteNonQuery(objCommand)
            End If
        Next

        objVANS.disconnect()

        rgdClinics.Rebind()
        TemplateLoad()
        rgdTemplatesAssigned.Rebind()
        rgdTemplatesAvailable.Rebind()

    End Sub

    Private Sub btnPreview_Click(sender As Object, e As System.EventArgs) Handles btnPreview.Click
        If rgdClinics.SelectedItems.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "MyScript", "alert('Please select a clinic to preview.');", True)
        Else
            Dim gdi As GridDataItem = rgdClinics.SelectedItems(0)
            Response.Redirect("TemplateByClinicPrintPreview.aspx?ClinicID=" & gdi("ClinicID").Text)
        End If
    End Sub
End Class