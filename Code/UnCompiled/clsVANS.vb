Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Configuration.ConfigurationManager
Imports VANS.us.vacloud.devmdws

Public Class clsVANS
    Inherits System.Web.UI.Page
    Private emrsvcs As VANS.us.vacloud.devmdws.EmrSvc
    Private strErrorMessage As String = ""
    Private currentPage As Page

    Public Property parentPage()
        Get
            Return currentPage
        End Get
        Set(value)
            currentPage = value
        End Set
    End Property

    Public Property ErrorMessage()
        Get
            Return strErrorMessage
        End Get
        Set(ByVal Value)
            strErrorMessage = Value
        End Set
    End Property

    Public Sub New()
        emrsvcs = New VANS.us.vacloud.devmdws.EmrSvc()
        emrsvcs.CookieContainer = New System.Net.CookieContainer()
    End Sub

    Public Function connectVANS() As Boolean
        If connect() = True Then
            If login() = True Then
                Return True
            End If
        End If

        Return False

    End Function

    Public Sub displayError()
        ScriptManager.RegisterStartupScript(parentPage, parentPage.GetType(), "MyScript", "alert('" & strErrorMessage & "');", True)
    End Sub

    Public Function connect() As Boolean
        Dim result As DataSourceArray = emrsvcs.connect(AppSettings("SiteID"))

        If result.fault IsNot Nothing Then
            strErrorMessage = "Could not connect to MDWS (connect). Please contact Systems Administrator."
            errorDisconnect()
            Return False
        End If

        Return True
        'Return result.items(0).welcomeMessage
    End Function

    Public Function login() As Boolean
        Dim result As UserTO = emrsvcs.login(AppSettings("SiteUser"), AppSettings("Sitepwd"), AppSettings("SiteContext"))

        If Not result.fault Is Nothing Then
            strErrorMessage = "Could not connect to MDWS (login). Please contact Systems Administrator."
            errorDisconnect()
            Return False
        End If

        Return True
    End Function


    Public Function match(strSSN As String) As TaggedPatientArrays
        Dim result As TaggedPatientArrays = emrsvcs.match(strSSN)

        If result.count = 0 Then
            strErrorMessage = "No Matches Found."
            errorDisconnect()
        ElseIf result.fault IsNot Nothing Then
            strErrorMessage = "Error connecting to MDWS (match). Please contact Systems Administrator."
            errorDisconnect()
        End If

        Return emrsvcs.match(strSSN)
    End Function

    Public Function selectPatient(strLocalPID As String) As PatientTO
        Dim result As PatientTO = emrsvcs.select(strLocalPID)

        If result.fault IsNot Nothing Then
            strErrorMessage = "Error connecting to MDWS (select). Please contact Systems Administrator."
            errorDisconnect()
        End If

        Return result
    End Function

    Public Function getAppointments() As clsAppointment()
        Dim result As TaggedAppointmentArrays = emrsvcs.getAppointments()

        If result.arrays(0).count = 0 Then
            strErrorMessage = "No Appointments Found."
            errorDisconnect()
        ElseIf Not result.fault Is Nothing Then
            strErrorMessage = "Error connecting to MDWS (getAppointments). Please contact Systems Administrator."
            errorDisconnect()
        End If

        Dim objAppointments(result.arrays(0).count - 1) As clsAppointment
        Dim intCount As Integer = 0
        Dim intResizeCount As Integer = 0

        For Each itm In result.arrays(0).appts
            If Not (itm.clinic.name.Contains("-X") Or itm.clinic.name.Contains("-x")) Then
                objAppointments(intCount) = New clsAppointment

                Dim strYear As String
                Dim strMonth As String
                Dim strDay As String
                Dim strTime As String

                strYear = Left(itm.timestamp, 4)
                strMonth = Mid(itm.timestamp, 5, 2).PadLeft(2, "0")
                strDay = Mid(itm.timestamp, 7, 2).PadLeft(2, "0")
                strTime = Mid(itm.timestamp, 10, 4)

                'itm.text = strTime
                'itm.timestamp = strMonth & "/" & strDay & "/" & strYear

                objAppointments(intCount).VANSAppointmentID = itm.id
                objAppointments(intCount).AppointmentDate = strMonth & "/" & strDay & "/" & strYear
                objAppointments(intCount).AppointmentTime = strTime
                objAppointments(intCount).ClinicID = itm.clinic.id
                objAppointments(intCount).ClinicName = itm.clinic.name
                objAppointments(intCount).CurrentStatus = itm.currentStatus
                objAppointments(intCount).Type = itm.type

                intCount += 1
            Else
                intResizeCount += 1
            End If
        Next

        'Resize array for items that aren't added
        Array.Resize(objAppointments, objAppointments.Count - intResizeCount)

        Return objAppointments
    End Function

    Public Function getDemographics() As PatientTO
        Dim result As PatientTO = emrsvcs.getDemographics()

        If Not result.fault Is Nothing Then
            strErrorMessage = "Error connecting to MDWS (getDemographics). Please contact Systems Administrator."
            errorDisconnect()
        End If

        Return result
    End Function

    Public Function getClinics() As TaggedHospitalLocationArray
        Dim result As TaggedHospitalLocationArray = emrsvcs.getClinics(AppSettings("SiteID"))

        If Not result.fault Is Nothing Then
            strErrorMessage = "Error connecting to MDWS (getClinics). Please contact Systems Administrator."
            errorDisconnect()
        End If

        Return result
    End Function

    Public Sub disconnect()
        emrsvcs.disconnect()
    End Sub

    Public Sub errorDisconnect()
        displayError()
        emrsvcs.disconnect()
    End Sub

End Class
