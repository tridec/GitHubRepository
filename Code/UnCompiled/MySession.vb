Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports VANS.MdwsDemo.dao.soap
Imports VANS.us.vacloud.devmdws

Namespace MdwsDemo.domain
    Public Class MySession
        Public VisitorPassword As String = "This is a sample BSE pass phrase"
        Public Property SelectedSite() As String
            Get
                Return m_SelectedSite
            End Get
            Set(value As String)
                m_SelectedSite = value
            End Set
        End Property
        Private m_SelectedSite As String
        Public Property VistaDao() As VistaDao
            Get
                Return m_VistaDao
            End Get
            Set(value As VistaDao)
                m_VistaDao = value
            End Set
        End Property
        Private m_VistaDao As VistaDao
        Public Property User() As UserTO
            Get
                Return m_User
            End Get
            Set(value As UserTO)
                m_User = value
            End Set
        End Property
        Private m_User As UserTO
        Public Property Patient() As PatientTO
            Get
                Return m_Patient
            End Get
            Set(value As PatientTO)
                m_Patient = value
            End Set
        End Property
        Private m_Patient As PatientTO
        Public Property SitesFile() As RegionArray
            Get
                Return m_SitesFile
            End Get
            Set(value As RegionArray)
                m_SitesFile = value
            End Set
        End Property
        Private m_SitesFile As RegionArray
        ''' <summary>
        ''' Use for storing things between calls
        ''' </summary>
        Public Property TempHolder() As Object
            Get
                Return m_TempHolder
            End Get
            Set(value As Object)
                m_TempHolder = value
            End Set
        End Property
        Private m_TempHolder As Object
    End Class
End Namespace
