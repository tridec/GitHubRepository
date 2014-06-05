Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports VANS.MdwsDemo.dao.soap
Imports VANS.MdwsDemo.domain
Imports VANS.us.vacloud.devmdws


Public Class AppointmentsTest
    Inherits System.Web.UI.Page
    Dim emrsvcs As New VANS.us.vacloud.devmdws.EmrSvc

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub rgdClinics_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdClinics.NeedDataSource
        emrsvcs.CookieContainer = New System.Net.CookieContainer()
        emrsvcs.connect(System.Configuration.ConfigurationManager.AppSettings("SiteID"))
        Dim user As VANS.us.vacloud.devmdws.UserTO = emrsvcs.login("1programmer", "programmer1", "OR CPRS GUI CHART")

        Dim tgdHLA As VANS.us.vacloud.devmdws.TaggedHospitalLocationArray = emrsvcs.getClinics(System.Configuration.ConfigurationManager.AppSettings("SiteID"))
        rgdClinics.DataSource = tgdHLA.locations
        emrsvcs.disconnect()
    End Sub

    Private Sub rgdHospitals_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdHospitals.NeedDataSource
        'emrsvcs.CookieContainer = New System.Net.CookieContainer()
        emrsvcs.connect(System.Configuration.ConfigurationManager.AppSettings("SiteID"))
        Dim user As VANS.us.vacloud.devmdws.UserTO = emrsvcs.login("1programmer", "programmer1", "OR CPRS GUI CHART")

        Dim tgdHLA As VANS.us.vacloud.devmdws.TaggedHospitalLocationArray = emrsvcs.getHospitalLocations(System.Configuration.ConfigurationManager.AppSettings("SiteID"), "1")
        rgdHospitals.DataSource = tgdHLA.locations
        emrsvcs.disconnect()
    End Sub

    Private Sub rgdLocations_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdLocations.NeedDataSource
        'emrsvcs.CookieContainer = New System.Net.CookieContainer()
        emrsvcs.connect(System.Configuration.ConfigurationManager.AppSettings("SiteID"))
        Dim user As VANS.us.vacloud.devmdws.UserTO = emrsvcs.login("1programmer", "programmer1", "OR CPRS GUI CHART")

        Dim tgdHLA As VANS.us.vacloud.devmdws.TaggedHospitalLocationArray = emrsvcs.getLocations(System.Configuration.ConfigurationManager.AppSettings("SiteID"), "1")
        rgdLocations.DataSource = tgdHLA.locations
        emrsvcs.disconnect()
    End Sub

    Private Sub rgdSpecialties_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdSpecialties.NeedDataSource
        'emrsvcs.CookieContainer = New System.Net.CookieContainer()
        emrsvcs.connect(System.Configuration.ConfigurationManager.AppSettings("SiteID"))
        Dim user As VANS.us.vacloud.devmdws.UserTO = emrsvcs.login("1programmer", "programmer1", "OR CPRS GUI CHART")

        Dim tgdT As VANS.us.vacloud.devmdws.TaggedText = emrsvcs.getSpecialties()
        rgdSpecialties.DataSource = tgdT.taggedResults
        emrsvcs.disconnect()
    End Sub

    Private Sub rgdTeams_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdTeams.NeedDataSource
        'emrsvcs.CookieContainer = New System.Net.CookieContainer()
        emrsvcs.connect(System.Configuration.ConfigurationManager.AppSettings("SiteID"))
        Dim user As VANS.us.vacloud.devmdws.UserTO = emrsvcs.login("1programmer", "programmer1", "OR CPRS GUI CHART")
        Dim tgdT As VANS.us.vacloud.devmdws.TaggedText = emrsvcs.getTeams()
        rgdTeams.DataSource = tgdT.taggedResults
        emrsvcs.disconnect()
    End Sub
End Class