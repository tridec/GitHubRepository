Public Class clsAppointment
    Inherits System.Web.UI.Page

    Private intAppointmentGroupID As Integer = 0
    Private strVANSAppointmentID As String = ""
    Private intAppointmentID As Integer = 0
    Private strClinicName As String = ""
    Private intClinicID As Integer = 0
    Private strClinicLocation As String = ""
    Private dtAppointmentDate As DateTime
    Private strTime As String = ""
    Private strCurrentStatus As String = ""
    Private strType As String = ""

    Public Property AppointmentGroupID As Integer
        Get
            Return intAppointmentGroupID
        End Get
        Set(value As Integer)
            intAppointmentGroupID = value
        End Set
    End Property

    Public Property ClinicLocation() As String
        Get
            Return strClinicLocation
        End Get
        Set(value As String)
            strClinicLocation = value
        End Set
    End Property

    Public Property VANSAppointmentID() As String
        Get
            Return strVANSAppointmentID
        End Get
        Set(ByVal value As String)
            strVANSAppointmentID = value
        End Set
    End Property

    Public Property AppointmentID() As Integer
        Get
            Return intAppointmentID
        End Get
        Set(ByVal value As Integer)
            intAppointmentID = value
        End Set
    End Property

    Public Property ClinicName() As String
        Get
            Return strClinicName
        End Get
        Set(ByVal value As String)
            strClinicName = value
        End Set
    End Property

    Public Property ClinicID() As Integer
        Get
            Return intClinicID
        End Get
        Set(ByVal value As Integer)
            intClinicID = value
        End Set
    End Property

    Public Property AppointmentDate() As DateTime
        Get
            Return dtAppointmentDate
        End Get
        Set(ByVal value As DateTime)
            dtAppointmentDate = value
        End Set
    End Property

    Public Property AppointmentTime() As String
        Get
            Return strTime
        End Get
        Set(ByVal value As String)
            strTime = value
        End Set
    End Property

    Public Property CurrentStatus() As String
        Get
            Return strCurrentStatus
        End Get
        Set(ByVal value As String)
            strCurrentStatus = value
        End Set
    End Property

    Public Property Type() As String
        Get
            Return strType
        End Get
        Set(ByVal value As String)
            strType = value
        End Set
    End Property


End Class
