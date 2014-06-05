
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports VANS.us.vacloud.devmdws

Namespace MdwsDemo.dao.soap
    Public Class VistaDao
        Private _emrSvc As EmrSvc

        Public Sub New()
            _emrSvc = New EmrSvc()
            _emrSvc.CookieContainer = New System.Net.CookieContainer()
        End Sub

        Public ReadOnly Property EmrSvcSession() As System.Net.CookieContainer
            Get
                Return _emrSvc.CookieContainer
            End Get
        End Property

        'Public Function getClinics() As TaggedHospitalLocationArray
        '    Dim Array As TaggedHospitalLocationArray = _emrSvc.getClinics("")
        '    Return Array

        'End Function


        Public Function getSitesFile() As RegionArray
            _
            Dim regions As RegionArray = _emrSvc.getVHA()
            If regions.fault IsNot Nothing Then
                Throw New ApplicationException(regions.fault.message)
            End If
            For Each visn As RegionTO In regions.regions
                ' makes for a user friendly dropdown
                visn.name = "VISN " + visn.id + " - " + visn.name
            Next
            Return regions
        End Function

        Public Function addDataSource(sitecode As String, siteName As String, hostname As String, brokerPort As String, visn As String) As SiteTO
            Dim result As SiteTO = _emrSvc.addDataSource(sitecode, siteName, hostname, brokerPort, "HIS", "VISTA", _
             visn)
            If result.fault IsNot Nothing Then
                Throw New ApplicationException(result.fault.message)
            End If
            Return result
        End Function

        Public Function connect(sitecode As String) As String
            Dim result As DataSourceArray = _emrSvc.connect(sitecode)
            If result.fault IsNot Nothing Then
                Throw New ApplicationException(result.fault.message)
            End If
            Return result.items(0).welcomeMessage
        End Function

        Public Function login(accessCode As String, verifyCode As String) As UserTO
            Dim result As UserTO = _emrSvc.login(accessCode, verifyCode, "OR CPRS GUI CHART")
            If result.fault IsNot Nothing Then
                Throw New ApplicationException(result.fault.message)
            End If
            Return result
        End Function

        Public Sub visit(securityPhrase As String, visitSite As String, userSite As String, userName As String, duz As String, ssn As String, _
         permissionString As String)
            Dim result As TaggedTextArray = _emrSvc.visit(securityPhrase, visitSite, userSite, userName, duz, ssn, _
             permissionString)
            If result.fault IsNot Nothing Then
                Throw New ApplicationException(result.fault.message)
            End If
            If result.results IsNot Nothing AndAlso result.results.Length > 0 Then
                For Each tt As TaggedText In result.results
                    If tt IsNot Nothing AndAlso tt.fault IsNot Nothing Then
                        Throw New ApplicationException(tt.fault.message)
                    End If
                Next
            End If
        End Sub

        Public Function [select](dfn As String) As PatientTO
            Dim result As PatientTO = _emrSvc.[select](dfn)
            If result.fault IsNot Nothing Then
                Throw New ApplicationException(result.fault.message)
            End If
            Return result
        End Function

        Public Sub setupMultiSiteQuery(securityPhrase As String)
            Dim result As SiteArray = _emrSvc.setupMultiSiteQuery(securityPhrase)
            If result.fault IsNot Nothing Then
                Throw New ApplicationException(result.fault.message)
            End If
        End Sub

        Public Function getAllMeds() As IList(Of MedicationTO)
            Dim allMeds As TaggedMedicationArrays = _emrSvc.getAllMeds()
            Dim result As IList(Of MedicationTO) = New List(Of MedicationTO)()

            If allMeds.fault IsNot Nothing Then
                Throw New ApplicationException(allMeds.fault.message)
            End If
            For Each tma As TaggedMedicationArray In allMeds.arrays
                If tma IsNot Nothing AndAlso tma.fault IsNot Nothing Then
                    Throw New ApplicationException(tma.fault.message)
                End If
                If tma Is Nothing OrElse tma.count = 0 OrElse tma.meds Is Nothing Then
                    Continue For
                End If
                For Each med As MedicationTO In tma.meds
                    result.Add(med)
                Next
            Next
            Return result
        End Function

        Public Sub disconnect()
            If _emrSvc IsNot Nothing Then
                Try
                    _emrSvc.disconnect()
                    ' we don't ever want to care about this function
                Catch generatedExceptionName As Exception
                End Try
            End If
        End Sub

        Public Function match(target As String) As IList(Of PatientTO)
            Dim tpas As TaggedPatientArrays = _emrSvc.match(target)

            If tpas Is Nothing OrElse tpas.arrays Is Nothing OrElse tpas.arrays.Length = 0 Then
                Throw New ApplicationException("Nothing returned!")
            End If
            If tpas.fault IsNot Nothing Then
                Throw New ApplicationException(tpas.fault.message)
            End If
            If tpas.arrays(0) Is Nothing OrElse tpas.arrays(0).count = 0 OrElse tpas.arrays(0).patients Is Nothing OrElse tpas.arrays(0).patients.Length = 0 Then
                Throw New ApplicationException("No patients returned for that search...")
            End If

            Dim patients As IList(Of PatientTO) = New List(Of PatientTO)()

            For Each patient As PatientTO In tpas.arrays(0).patients
                patients.Add(patient)
            Next

            Return patients
        End Function
    End Class
End Namespace

'=======================================================
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik
'Facebook: facebook.com/telerik
'=======================================================
