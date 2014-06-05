Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports VANS.MdwsDemo.dao.soap
Imports VANS.MdwsDemo.domain
Imports VANS.us.vacloud.devmdws

Public Class AppointmentsListMember
    Inherits System.Web.UI.Page
    Dim objVANS As New clsVANS
    Dim blnUserLink As Boolean = True

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'login check
        Dim loginchk = New clsLoginCheck()
        If (Not loginchk.LoggedIn) Then
            Response.Redirect("~/login.aspx")
        End If

        If Not Page.IsPostBack Then
            'LoadData()
        End If

    End Sub

    Private Sub LoadUser()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "PersonUserSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@UserId", DbType.String, Membership.GetUser.ProviderUserKey.ToString())
        Using Datareader As IDataReader = db.ExecuteReader(objCommand)
            Do While Datareader.Read
                If Not (Datareader("VistaUserID") Is DBNull.Value) Then
                    lblSelectedPatientID.Text = Datareader("VistaUserID")
                    lblName.Text = Datareader("VistaUserName")
                Else
                    blnUserLink = False
                End If
            Loop
        End Using


    End Sub

    Private Sub LoadData()
        'Do Person Select to find localPID
        LoadUser()

        If blnUserLink = False Then
            'Display panel that states this user hasn't been linked to vista, please contact an administrator.
            pnlWarning.Visible = True
            pnlAppointments.Visible = False
            Exit Sub
        Else
            pnlWarning.Visible = False
            pnlAppointments.Visible = True
        End If

        Session("AllAppointmentsMember") = Nothing
        Session("PatientDetailsMember") = Nothing
        Session("AppointmentsGroupMember") = Nothing

        If connectVANS() = False Then
            Exit Sub
        End If

        objVANS.selectPatient(lblSelectedPatientID.Text)
        If objVANS.ErrorMessage <> "" Then
            Exit Sub
        End If

        Dim objAppointments As clsAppointment() = objVANS.getAppointments()
        If objVANS.ErrorMessage <> "" Then
            Exit Sub
        End If

        Dim ptoPatient As VANS.us.vacloud.devmdws.PatientTO = objVANS.getDemographics()
        If objVANS.ErrorMessage <> "" Then
            Exit Sub
        End If

        Session("AllAppointmentsMember") = objAppointments
        'setAppointments()

        Dim objPatient As New clsPatient

        objPatient.VANSPatientID = ptoPatient.localPid
        objPatient.Name = ptoPatient.name
        objPatient.Address = ptoPatient.homeAddress.streetAddress1 & " " & ptoPatient.homeAddress.streetAddress2 & " " & ptoPatient.homeAddress.streetAddress3
        objPatient.City = ptoPatient.homeAddress.city
        objPatient.State = ptoPatient.homeAddress.state
        objPatient.ZIP = ptoPatient.homeAddress.zipcode

        Session("PatientDetailsMember") = objPatient

        objVANS.disconnect()
        rgdGroupedAppointments.Rebind()
    End Sub

    Private Function connectVANS() As Boolean
        objVANS.parentPage = HttpContext.Current.Handler
        If objVANS.connectVANS = False Then
            Return False
        End If

        Return True
    End Function

    Private Function getDate(ByVal strVANSAppointmentID As String) As Date
        If Session("AllAppointmentsMember") Is Nothing Then
            Response.Redirect("HomeMain.aspx")
        End If

        Dim lstAllAppointments As clsAppointment() = Session("AllAppointmentsMember")

        For Each appt In lstAllAppointments
            If appt.VANSAppointmentID = strVANSAppointmentID Then
                Return appt.AppointmentDate
            End If
        Next
    End Function

    Private Sub rgdGroupedAppointments_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgdGroupedAppointments.ItemDataBound
        If TypeOf e.Item Is GridNestedViewItem Then
            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlCommand As String = "AppointmentSelectAllByGroup"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
            db.AddInParameter(objCommand, "@AppointmentGroupID", DbType.Int32, e.Item.DataItem("AppointmentGroupID"))

            'Used to get all the dates in the list so Begin/End dates can be set.
            'lstNested = New List(Of Date)
            Dim lstAllAppointments As clsAppointment() = Session("AllAppointmentsMember")
            Dim rgdNestedItems As RadGrid = e.Item.FindControl("rgdNestedItems")
            Dim ds As DataSet = db.ExecuteDataSet(objCommand)
            For Each dt As DataTable In ds.Tables
                'dt.Columns.Add("CurrentStatus")
                'dt.Columns.Add("type")
                'dt.Columns.Add("AppointmentDate")
                'dt.Columns.Add("clinic")
                'dt.Columns.Add("AppointmentTime")
                'dt.Columns.Add("ClinicID")
                For Each row As DataRow In dt.Rows
                    For Each appt In lstAllAppointments
                        If appt.VANSAppointmentID = row("VANSAppointmentID") Then
                            row("CurrentStatus") = appt.CurrentStatus
                            row("type") = appt.Type
                            row("AppointmentDate") = appt.AppointmentDate
                            row("clinic") = appt.ClinicName
                            row("AppointmentTime") = appt.AppointmentTime
                            row("ClinicID") = appt.ClinicID
                        End If
                    Next
                Next
            Next


            rgdNestedItems.DataSource = ds
            rgdNestedItems.DataBind()

        End If
    End Sub

    Private Sub rgdGroupedAppointments_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdGroupedAppointments.NeedDataSource
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "AppointmentGroupSelectByPatient"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@PatientID", DbType.Int32, Val(lblSelectedPatientID.Text))
        db.AddInParameter(objCommand, "@Member", DbType.Int32, 1)
        Dim dsGroup As DataSet = db.ExecuteDataSet(objCommand)
        For Each Table As DataTable In dsGroup.Tables

            For Each dr As DataRow In Table.Rows
                Dim lstNested As New List(Of Date)
                Dim db2 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
                Dim sqlCommand2 As String = "AppointmentSelectAllByGroup"
                Dim objCommand2 As DbCommand = db2.GetStoredProcCommand(sqlCommand2)
                db2.AddInParameter(objCommand2, "@AppointmentGroupID", DbType.Int32, dr("AppointmentGroupID"))
                Using datareader As IDataReader = db2.ExecuteReader(objCommand2)
                    While datareader.Read()
                        'Used to find the Begin and End Date ranges.
                        lstNested.Add(getDate(datareader("VANSAppointmentID")))
                    End While
                End Using
                lstNested.Sort()

                dr("BeginDate") = lstNested(0).ToShortDateString
                dr("EndDate") = lstNested(lstNested.Count - 1).ToShortDateString
            Next
        Next

        rgdGroupedAppointments.DataSource = dsGroup
    End Sub


    Public Sub rgdNestedItems_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs)
        '    If TypeOf e.Item Is GridDataItem Then
        '        Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)

        '        Dim lstAllAppointments As List(Of AppointmentTO) = Session("AllAppointments")

        '        For Each appt In lstAllAppointments
        '            If appt.VANSAppointmentid = itm("VANSAppointmentID").Text Then
        '                itm("CurrentStatus").Text = appt.currentStatus
        '                itm("type").Text = appt.type
        '                itm("timestamp").Text = appt.timestamp
        '                'lstNested.Add(itm("timestamp").Text)
        '                itm("clinic").Text = appt.clinic.name
        '                itm("AppointmentTime").Text = appt.text
        '                itm("ClinicID").Text = appt.clinic.id
        '            End If
        '        Next

        '    End If
    End Sub

    Private Sub rgdGroupedAppointments_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgdGroupedAppointments.UpdateCommand
        If TypeOf e.Item Is GridDataItem Then
            Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)

            Dim nestedItem As GridNestedViewItem = DirectCast(itm.ChildItem, GridNestedViewItem)
            Dim rgdNestedItems As RadGrid = nestedItem.FindControl("rgdNestedItems")

            Dim objAppointment(rgdNestedItems.Items.Count - 1) As clsAppointment

            Dim intCount As Integer = 0
            For Each itmNested As GridDataItem In rgdNestedItems.Items
                objAppointment(intCount) = New clsAppointment
                objAppointment(intCount).AppointmentGroupID = itm("AppointmentGroupID").Text
                objAppointment(intCount).VANSAppointmentID = itmNested("VANSAppointmentID").Text
                objAppointment(intCount).CurrentStatus = itmNested("CurrentStatus").Text
                objAppointment(intCount).Type = itmNested("type").Text
                objAppointment(intCount).AppointmentTime = itmNested("AppointmentTime").Text
                objAppointment(intCount).AppointmentDate = itmNested("AppointmentDate").Text
                objAppointment(intCount).ClinicName = itmNested("Clinic").Text
                objAppointment(intCount).ClinicID = Val(itmNested("ClinicID").Text)
                intCount += 1
            Next
            Session("AppointmentsGroupMember") = objAppointment
            Response.Redirect("AppointmentPrintPreviewMember.aspx?AppointmentGroupID=" & itm("AppointmentGroupID").Text)
        End If
    End Sub

    Private Sub tmrLoad_Tick(sender As Object, e As System.EventArgs) Handles tmrLoad.Tick
        LoadData()
        tmrLoad.Enabled = False
    End Sub
End Class