Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports VANS.MdwsDemo.dao.soap
Imports VANS.MdwsDemo.domain
Imports VANS.us.vacloud.devmdws

Public Class TemplateList
    Inherits System.Web.UI.Page
    'Dim emrsvcs As New VANS.us.vacloud.devmdws.EmrSvc
    Dim objVANS As New clsVANS

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

        End If
    End Sub

    Private Sub rgdTemplate_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgdTemplate.ItemCreated
        If TypeOf e.Item Is GridFilteringItem Then
            Dim filteringItem As GridFilteringItem = CType(e.Item, GridFilteringItem)
            Dim box As TextBox = CType(filteringItem("Message").Controls(0), TextBox)
            box.Width = Unit.Pixel(300)
        End If
    End Sub

    Private Sub rgdTemplate_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgdTemplate.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = DirectCast(e.Item, GridDataItem)

            dataItem("TemplateID").Text = "<a href='TemplateAddEdit.aspx?TemplateID=" & dataItem("TemplateID").Text & "'>" & dataItem("TemplateID").Text & "</a>"

            'If dataItem("Status").Text = "New" Then
            '    dataItem.BackColor = Drawing.Color.LightGreen
            'End If
        End If
    End Sub

    Private Sub rgdTemplate_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdTemplate.NeedDataSource
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "TemplateSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        rgdTemplate.DataSource = db.ExecuteDataSet(objCommand)
    End Sub

    'Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlStatus.SelectedIndexChanged
    '    For Each gdi As GridDataItem In rgdTemplate.Items
    '        Select Case ddlStatus.SelectedValue
    '            Case 1
    '                If gdi("Status").Text = "Approved" Then
    '                    gdi.Display = True
    '                Else
    '                    gdi.Display = False
    '                End If
    '            Case 2
    '                If gdi("Status").Text = "Unapproved" Then
    '                    gdi.Display = True
    '                Else
    '                    gdi.Display = False
    '                End If
    '            Case 3
    '                gdi.Display = True
    '        End Select
    '    Next

    'End Sub

    Private Function connectVANS() As Boolean
        objVANS.parentPage = HttpContext.Current.Handler
        If objVANS.connectVANS = False Then
            Return False
        End If

        Return True
    End Function

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Response.Redirect("TemplateSearch.aspx")
    End Sub

    Private Sub btnAddTemplate_Click(sender As Object, e As System.EventArgs) Handles btnAddTemplate.Click
        Response.Redirect("TemplateAddEdit.aspx?TemplateID=0")
    End Sub

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

    End Sub
End Class