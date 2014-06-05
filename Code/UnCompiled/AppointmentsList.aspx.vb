Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports VANS.MdwsDemo.dao.soap
Imports VANS.MdwsDemo.domain
Imports VANS.us.vacloud.devmdws

Public Class AppointmentsList
    Inherits System.Web.UI.Page
    Private _mySession As MySession
    'Dim emrsvcs As New VANS.us.vacloud.devmdws.EmrSvc
    Dim objVANS As New clsVANS

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
            txtBeginDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
            rgdAppointments.MasterTableView.FilterExpression = "([AppointmentDate] >='" & txtBeginDate.Text & "') "

            Session("AppointmentsGroup") = Nothing
            Session("selectedItems") = Nothing

            If Not Session("PatientSearch") Is Nothing Then
                txtSSN.Text = Session("PatientSearch")
            End If

            If Not Session("PatientDetails") Is Nothing Then
                pnlPatient.Visible = False
                pnlAppointments.Visible = True
                lblName.Text = DirectCast(Session("PatientDetails"), clsPatient).Name
                lblSelectedPatientID.Text = DirectCast(Session("PatientDetails"), clsPatient).VANSPatientID
                setAppointments()

                lbnTitle.Visible = True
                lblTitle.Visible = False
                lblTitleName.Text = " > " & lblName.Text
            Else
                pnlPatient.Visible = True
                pnlAppointments.Visible = False

                lblName.Text = ""
                lblSelectedPatientID.Text = ""

                lbnTitle.Visible = False
                lblTitle.Visible = True
                lblTitleName.Text = ""
            End If

        End If
    End Sub

    Private Sub LoadBlankGrid(ByVal rgdGrid As RadGrid)
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim colsNum As Integer = 3
        Dim rowsNum As Integer = 0
        Dim colName As String = "Column"

        For j As Integer = 1 To colsNum
            dt.Columns.Add([String].Format("{0}{1}", colName, j))
        Next

        For i As Integer = 1 To rowsNum
            dr = dt.NewRow()

            For k As Integer = 1 To colsNum
                dr([String].Format("{0}{1}", colName, k)) = [String].Format("{0}{1} Row{2}", colName, k, i)
            Next
            dt.Rows.Add(dr)
        Next

        rgdGrid.DataSource = dt
    End Sub

    Private Function connectVANS() As Boolean
        objVANS.parentPage = HttpContext.Current.Handler
        If objVANS.connectVANS = False Then
            Return False
        End If

        Return True
    End Function

    Private Sub btnFind_Click(sender As Object, e As System.EventArgs) Handles btnFind.Click
        txtBeginDate.Text = ""
        txtEndDate.Text = ""
        Session("AllAppointments") = Nothing
        Session("PatientDetails") = Nothing
        Session("Patients") = Nothing
        Session("AppointmentsWithoutLetters") = Nothing
        Session("AppoinmentsGroup") = Nothing
        Session("PatientSearch") = txtSSN.Text

        If connectVANS() = False Then
            rgdPerson.Rebind()
            Exit Sub
        End If

        Dim tgdPA As us.vacloud.devmdws.TaggedPatientArrays = objVANS.match(txtSSN.Text)
        If objVANS.ErrorMessage <> "" Then
            rgdPerson.Rebind()
            Exit Sub
        End If

        Session("Patients") = tgdPA.arrays(0).patients

        'lblPID.Text += tgdPA.arrays(0).patients(0).localPid
        'lblPIDHid.Text = tgdPA.arrays(0).patients(0).localPid
        'lblName.Text += tgdPA.arrays(0).patients(0).name
        objVANS.disconnect()
        rgdPerson.Rebind()

    End Sub

    Private Sub rgdAppointments_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgdAppointments.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim gdi As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim strArray As String() = Split(gdi("VANSAppointmentID").Text, ";")
            ' Dim strArrayTime As String() = Split(gdi("text").Text, "::")
            'gdi("AppointmentDate").Text = DateTime.Parse(gdi("AppointmentDate").Text)


            gdi("ClinicID").Text = strArray(2)
            'gdi("Time").Text = strArrayTime(1)

        End If
    End Sub

    Private Sub rgdAppointments_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdAppointments.NeedDataSource
        If Not Session("AppointmentsWithoutLetters") Is Nothing Then
            If chkShowAll.Checked Then
                rgdAppointments.DataSource = Session("AllAppointments")
            Else
                rgdAppointments.DataSource = Session("AppointmentsWithoutLetters")
            End If
        Else
            'LoadBlankGrid
            LoadBlankGrid(rgdAppointments)
        End If
    End Sub

    Private Sub rgdPerson_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdPerson.NeedDataSource
        If Not Session("Patients") Is Nothing Then
            rgdPerson.DataSource = Session("Patients")
        Else
            'LoadBlankGrid
            LoadBlankGrid(rgdPerson)
        End If

    End Sub

    Private Sub rgdPerson_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgdPerson.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim gdi As GridDataItem = DirectCast(e.Item, GridDataItem)
            If Not Session("PatientDetails") Is Nothing Then
                If gdi("localpid").Text = DirectCast(Session("PatientDetails"), clsPatient).VANSPatientID Then
                    'Fix Page Index to work on sorting
                    lblSelectedPatientID.Text = gdi("localpid").Text
                    e.Item.Selected = True
                End If
            End If
            'Dim gdi As GridDataItem = DirectCast(e.Item, GridDataItem)
            'Dim strDOB As String = gdi("dob").Text.Remove(8)
            'gdi("Dob").Text = Mid(strDOB, 5, 2) & "/" & Right(strDOB, 2) & "/" & Left(strDOB, 4)

        End If
    End Sub

    Private Sub rgdPerson_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgdPerson.SelectedIndexChanged
        Dim gdi As GridDataItem = rgdPerson.SelectedItems(0)

        Session("AllAppointments") = Nothing
        Session("PatientDetails") = Nothing
        Session("AppointmentsWithoutLetters") = Nothing
        Session("AppoinmentsGroup") = Nothing
        chkShowAll.Checked = True

        lblSelectedPatientID.Text = gdi("localPID").Text
        txtBeginDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
        rgdAppointments.MasterTableView.FilterExpression = "([AppointmentDate] >='" & txtBeginDate.Text & "') "
        txtEndDate.Text = ""

        If connectVANS() = False Then
            Exit Sub
        End If

        objVANS.selectPatient(gdi("localPID").Text)
        If objVANS.ErrorMessage <> "" Then
            Exit Sub
        End If

        'Dim tgdAA As us.vacloud.devmdws.TaggedAppointmentArrays = objVANS.getAppointments()
        'If objVANS.ErrorMessage <> "" Then
        '    Exit Sub
        'End If

        Dim objAppointments As clsAppointment() = objVANS.getAppointments()
        If objVANS.ErrorMessage <> "" Then
            Exit Sub
        End If

        Dim ptoPatient As VANS.us.vacloud.devmdws.PatientTO = objVANS.getDemographics()
        If objVANS.ErrorMessage <> "" Then
            Exit Sub
        End If

        Session("AllAppointments") = objAppointments
        setAppointments()

        lblName.Text = gdi("Name").Text

        Dim objPatient As New clsPatient

        objPatient.VANSPatientID = gdi("LocalPID").Text
        objPatient.Name = gdi("Name").Text
        objPatient.Address = ptoPatient.homeAddress.streetAddress1 & " " & ptoPatient.homeAddress.streetAddress2 & " " & ptoPatient.homeAddress.streetAddress3
        objPatient.City = ptoPatient.homeAddress.city
        objPatient.State = ptoPatient.homeAddress.state
        objPatient.ZIP = ptoPatient.homeAddress.zipcode
        objPatient.PageIndex = rgdPerson.CurrentPageIndex

        'Save sort order in object
        If rgdPerson.MasterTableView.SortExpressions.Count > 0 Then
            objPatient.SortName = rgdPerson.MasterTableView.SortExpressions(0).FieldName
            objPatient.SortOrder = rgdPerson.MasterTableView.SortExpressions(0).SortOrder
        End If

        Session("PatientDetails") = objPatient

        pnlAppointments.Visible = True
        pnlPatient.Visible = False

        lbnTitle.Visible = True
        lblTitle.Visible = False
        lblTitleName.Text = " > " & lblName.Text

        objVANS.disconnect()
        rgdAppointments.Rebind()
        rgdGroupedAppointments.Rebind()
    End Sub

    Private Sub setAppointments()
        Dim lstAppointmentsWithoutLetters = New List(Of clsAppointment)(DirectCast(Session("AllAppointments"), clsAppointment()))
        Dim lstAppointmentsWithLetters As New List(Of clsAppointment)

        'This will get the list of appointments in our database that have letters and then remove those from the vans list we get from them.
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "AppointmentSelectAllByPatient"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@PatientID", DbType.Int32, Val(lblSelectedPatientID.Text))
        Using datareader As IDataReader = db.ExecuteReader(objCommand)
            Do While datareader.Read
                For Each appt In lstAppointmentsWithoutLetters
                    If appt.VANSAppointmentID = datareader("VANSAppointmentID") Then
                        lstAppointmentsWithLetters.Add(appt)
                    End If
                Next
            Loop
        End Using

        For Each appt In lstAppointmentsWithLetters
            lstAppointmentsWithoutLetters.Remove(appt)
        Next

        Session("AppointmentsWithoutLetters") = lstAppointmentsWithoutLetters
        'Session("AppointmentsWithLetters") = lstAppointmentsWithLetters
    End Sub

    'Private Sub btnVisits_Click(sender As Object, e As System.EventArgs) Handles btnVisits.Click

    '    emrsvcs.CookieContainer = New System.Net.CookieContainer()
    '    emrsvcs.connect(System.Configuration.ConfigurationManager.AppSettings("SiteID"))
    '    emrsvcs.login("1programmer", "programmer1", "OR CPRS GUI CHART")
    '    emrsvcs.select(Session("PID"))
    '    Dim tgdVA As us.vacloud.devmdws.TaggedVisitArray = emrsvcs.getVisits(txtFromDate.Text, txtToDate.Text)
    '    rgdVisits.DataSource = tgdVA.visits
    '    rgdVisits.DataBind()

    '    emrsvcs.disconnect()
    'End Sub

    Private Sub rgdPerson_PreRender(sender As Object, e As System.EventArgs) Handles rgdPerson.PreRender
        If Not Page.IsPostBack Then
            If Not Session("PatientDetails") Is Nothing Then
                Dim objPatient As clsPatient = Session("PatientDetails")
                If objPatient.SortName <> "" Then
                    Dim gseSort As GridSortExpression = New GridSortExpression
                    gseSort.FieldName = objPatient.SortName
                    gseSort.SortOrder = objPatient.SortOrder
                    rgdPerson.MasterTableView.SortExpressions.AddSortExpression(gseSort)
                End If
                If objPatient.PageIndex > 0 Then
                    rgdPerson.CurrentPageIndex = objPatient.PageIndex
                End If
                rgdPerson.Rebind()
            End If
        End If
    End Sub

    Private Function getChecked() As Boolean
        For Each itm As GridDataItem In rgdAppointments.Items
            Dim chkAppointment As CheckBox = itm.FindControl("chkAppointment")
            If chkAppointment.Checked = True Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub btnCreateLetter_Click(sender As Object, e As System.EventArgs) Handles btnCreateLetter.Click
        'Insert in to AppoitnmentGroup

        If Session("selectedItems") IsNot Nothing Then
            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlCommand As String = "AppointmentGroupInsertUpdate"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)

            db.AddInParameter(objCommand, "@PatientID", DbType.Int32, lblSelectedPatientID.Text)
            db.AddOutParameter(objCommand, "@InsertAppointmentGroupID", SqlDbType.Int, 100)
            db.ExecuteNonQuery(objCommand)

            Dim intAppointmentGroupID As Integer = db.GetParameterValue(objCommand, "@InsertAppointmentGroupID")

            Dim selectedItems As List(Of KeyValuePair(Of String, String)) = DirectCast(Session("selectedItems"), List(Of KeyValuePair(Of String, String)))

            For Each itm In selectedItems
                'Dim chkAppointment As CheckBox = itm.FindControl("chkAppointment")

                'If chkAppointment.Checked = True Then
                'Insert new entries for each checked appointment in the grid.
                Dim db2 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
                Dim sqlCommand2 As String = "AppointmentInsert"
                Dim objCommand2 As DbCommand = db2.GetStoredProcCommand(sqlCommand2)
                db2.AddInParameter(objCommand2, "@VANSAppointmentID", DbType.String, itm.Key)
                db2.AddInParameter(objCommand2, "@AppointmentGroupID", DbType.Int32, intAppointmentGroupID)
                db2.AddInParameter(objCommand2, "@VANSClinicID", DbType.Int32, itm.Value)
                db2.ExecuteNonQuery(objCommand2)
                'End If
            Next

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "MyScript", "alert('Letter group " & intAppointmentGroupID & " has been created');", True)
            setAppointments()
            rgdAppointments.Rebind()
            rgdGroupedAppointments.Rebind()

        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "MyScript", "alert('No Appointments have been selected or are available.');", True)
        End If
        Session("selectedItems") = Nothing

    End Sub

    Private Sub btnFilter_Click(sender As Object, e As System.EventArgs) Handles btnFilter.Click
        Try
            If txtBeginDate.Text <> "" And txtEndDate.Text <> "" Then
                rgdAppointments.MasterTableView.FilterExpression = "([AppointmentDate] >='" & CDate(txtBeginDate.Text).ToString("MM/dd/yyyy") & "' AND [AppointmentDate] <= '" & CDate(txtEndDate.Text).ToString("MM/dd/yyyy") & "') "
            ElseIf txtBeginDate.Text <> "" Then
                rgdAppointments.MasterTableView.FilterExpression = "([AppointmentDate] >='" & CDate(txtBeginDate.Text).ToString("MM/dd/yyyy") & "') "
            ElseIf txtEndDate.Text <> "" Then
                rgdAppointments.MasterTableView.FilterExpression = "([AppointmentDate] <= '" & CDate(txtEndDate.Text).ToString("MM/dd/yyyy") & "') "
            Else
                rgdAppointments.MasterTableView.FilterExpression = ""
            End If

        Catch ex As Exception
        End Try

        Session("selectedItems") = Nothing
        rgdAppointments.MasterTableView.Rebind()
        rgdGroupedAppointments.Rebind()
        btnFilter.Focus()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As System.EventArgs) Handles btnReset.Click
        Session("selectedItems") = Nothing
        txtBeginDate.Text = ""
        txtEndDate.Text = ""
        rgdAppointments.MasterTableView.FilterExpression = ""
        rgdAppointments.Rebind()
        rgdGroupedAppointments.Rebind()
        btnReset.Focus()

    End Sub

    Private Sub rgdGroupedAppointments_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdGroupedAppointments.NeedDataSource
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "AppointmentGroupSelectByPatient"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@PatientID", DbType.Int32, Val(lblSelectedPatientID.Text))
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

        If chkPreviousLetters.Checked Then
            rgdGroupedAppointments.MasterTableView.FilterExpression = ""
        Else
            rgdGroupedAppointments.MasterTableView.FilterExpression = "([BeginDate] >='" & Date.Today & "') "
        End If


        rgdGroupedAppointments.DataSource = dsGroup
        'rgdGroupedAppointments.DataSource = Session("AppointmentsWithLetters")
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


            Session("AppointmentsGroup") = objAppointment
            Response.Redirect("Appointments.aspx")
        End If
    End Sub

    Private Sub rgdGroupedAppointments_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgdGroupedAppointments.ItemDataBound
        If TypeOf e.Item Is GridNestedViewItem Then
            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlCommand As String = "AppointmentSelectAllByGroup"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
            db.AddInParameter(objCommand, "@AppointmentGroupID", DbType.Int32, e.Item.DataItem("AppointmentGroupID"))

            'Used to get all the dates in the list so Begin/End dates can be set.
            'lstNested = New List(Of Date)
            Dim lstAllAppointments As clsAppointment() = Session("AllAppointments")
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

            'lstNested.Sort()

            'If lstNested.Count > 0 Then
            '    Dim itm As GridDataItem = DirectCast(e.Item, GridNestedViewItem).ParentItem
            '    itm("BeginDate").Text = lstNested(0)
            '    itm("EndDate").Text = lstNested(lstNested.Count - 1)
            'End If

        End If
    End Sub

    Private Function getDate(ByVal strVANSAppointmentID As String) As Date
        Dim lstAllAppointments As clsAppointment() = Session("AllAppointments")

        For Each appt In lstAllAppointments
            If appt.VANSAppointmentID = strVANSAppointmentID Then
                Return appt.AppointmentDate
            End If
        Next
    End Function

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

    Private Sub lbnTitle_Click(sender As Object, e As System.EventArgs) Handles lbnTitle.Click
        Session("AllAppointments") = Nothing
        Session("AppointmentsWithoutLetters") = Nothing
        Session("PatientDetails") = Nothing
        Session("selectedItems") = Nothing
        lblSelectedPatientID.Text = ""
        txtBeginDate.Text = ""
        txtEndDate.Text = ""
        rgdAppointments.Rebind()
        rgdGroupedAppointments.Rebind()

        lblName.Text = ""
        pnlAppointments.Visible = False
        pnlPatient.Visible = True

        lbnTitle.Visible = False
        lblTitle.Visible = True
        lblTitleName.Text = ""

        If Session("Patients") IsNot Nothing Then
            rgdPerson.Rebind()
        End If

        rgdPerson.SelectedIndexes.Clear()
        chkPreviousLetters.Checked = False
        chkShowAll.Checked = False
    End Sub

    Public Sub chkAppointment_CheckedChanged(sender As Object, e As System.EventArgs)
        Dim chkAppointment As CheckBox = DirectCast(sender, CheckBox)

        Dim selectedItems As List(Of KeyValuePair(Of String, String))
        If Session("selectedItems") Is Nothing Then
            selectedItems = New List(Of KeyValuePair(Of String, String))
        Else
            selectedItems = CType(Session("selectedItems"), List(Of KeyValuePair(Of String, String)))
        End If

        Dim gdi As GridDataItem = chkAppointment.Parent.Parent

        If chkAppointment.Checked Then
            If Not selectedItems.Contains(New KeyValuePair(Of String, String)(gdi("VANSAppointmentID").Text, gdi("clinicid").Text)) Then
                selectedItems.Add(New KeyValuePair(Of String, String)(gdi("VANSAppointmentID").Text, gdi("clinicid").Text))
            End If
        Else
            selectedItems.Remove(New KeyValuePair(Of String, String)(gdi("VANSAppointmentID").Text, gdi("clinicid").Text))
        End If

        Session("selectedItems") = selectedItems

    End Sub

    Private Sub rgdAppointments_PreRender(sender As Object, e As System.EventArgs) Handles rgdAppointments.PreRender
        If Session("selectedItems") IsNot Nothing Then
            Dim selectedItems As List(Of KeyValuePair(Of String, String)) = CType(Session("selectedItems"), List(Of KeyValuePair(Of String, String)))
            For Each kvpItem In selectedItems
                For Each itm As GridDataItem In rgdAppointments.Items
                    If itm("VANSAppointmentID").Text = kvpItem.Key Then
                        Dim chkAppoinment As CheckBox = itm.FindControl("chkAppointment")
                        chkAppoinment.Checked = True
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub chkPreviousLetters_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkPreviousLetters.CheckedChanged
        rgdGroupedAppointments.Rebind()
    End Sub

    Private Sub chkShowAll_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkShowAll.CheckedChanged
        Session("selectedItems") = Nothing
        rgdAppointments.Rebind()
    End Sub
End Class