Public Class clsPatient
    Inherits System.Web.UI.Page

    Private strVANSPatientID As String = ""
    Private intPatientID As Integer = 0
    Private strName As String = ""
    Private strAddress As String = ""
    Private strCity As String = ""
    Private strState As String = ""
    Private strZIP As String = ""
    Private intPageIndex As Integer = 0
    Private strSortName As String = ""
    Private gsoPatient As GridSortOrder

    Public Property SortOrder() As GridSortOrder
        Get
            Return gsoPatient
        End Get
        Set(ByVal value As GridSortOrder)
            gsoPatient = value
        End Set
    End Property

    Public Property SortName() As String
        Get
            Return strSortName
        End Get
        Set(ByVal value As String)
            strSortName = value
        End Set
    End Property

    Public Property PageIndex() As Integer
        Get
            Return intPageIndex
        End Get
        Set(ByVal value As Integer)
            intPageIndex = value
        End Set
    End Property

    Public Property VANSPatientID() As String
        Get
            Return strVANSPatientID
        End Get
        Set(ByVal value As String)
            strVANSPatientID = value
        End Set
    End Property

    Public Property PatientID() As String
        Get
            Return intPatientID
        End Get
        Set(ByVal value As String)
            intPatientID = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return strName
        End Get
        Set(ByVal value As String)
            strName = value
        End Set
    End Property

    Public Property Address() As String
        Get
            Return strAddress
        End Get
        Set(ByVal value As String)
            strAddress = value
        End Set
    End Property

    Public Property City() As String
        Get
            Return strCity
        End Get
        Set(ByVal value As String)
            strCity = value
        End Set
    End Property

    Public Property State() As String
        Get
            Return strState
        End Get
        Set(ByVal value As String)
            strState = value
        End Set
    End Property

    Public Property ZIP() As String
        Get
            Return strZIP
        End Get
        Set(ByVal value As String)
            strZIP = value
        End Set
    End Property

End Class
