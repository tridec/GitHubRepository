Imports System.DirectoryServices.ActiveDirectory
Imports System.DirectoryServices
Imports System.Object
Imports System.MarshalByRefObject
Imports System.ComponentModel
Imports System.DirectoryServices.PropertyCollection
Imports System.Text
Imports System.Reflection
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Public Class clsASPUser
    Inherits System.Web.UI.Page

    Private bUserFound As Boolean = False
    Private bIsApproved As Boolean = False
    Private strUserID As String
    Private strUserName As String = ""
    Private strDeptOrgName As String = ""
    Private strUserTitle As String = ""
    Private strFirstName As String = ""
    Private strLastName As String = ""
    Private strEmail As String = ""
    Private strCity As String = ""
    Private strZipCode As String = ""
    Private strAddress As String = ""
    Private strPhone As String = ""
    Private strCompanyName As String = ""
    Private strStateVal As String = ""
    Private intPersonID As Integer

    Public Property PersonID() As Integer
        Get
            Return intPersonID
        End Get
        Private Set(ByVal value As Integer)
            intPersonID = value
        End Set
    End Property

    Public Property UserFound() As Boolean
        Get
            Return bUserFound
        End Get
        Set(ByVal Value As Boolean)
            bUserFound = Value
        End Set
    End Property

    Public Property IsApproved() As Boolean
        Get
            Return bIsApproved
        End Get
        Set(ByVal Value As Boolean)
            bIsApproved = Value
        End Set
    End Property

    Public Property UserId() As String
        Get
            Return strUserID
        End Get
        Set(ByVal Value As String)
            strUserID = Value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return strEmail
        End Get
        Set(ByVal Value As String)
            strEmail = Value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return strUserName
        End Get
        Set(ByVal Value As String)
            strUserName = Value
        End Set
    End Property

    Public Property DeptOrgName() As String
        Get
            Return strDeptOrgName
        End Get
        Set(ByVal Value As String)
            strDeptOrgName = Value
        End Set
    End Property

    Public Property UserTitle() As String
        Get
            Return strUserTitle
        End Get
        Set(ByVal Value As String)
            strUserTitle = Value
        End Set
    End Property

    Public Property FirstName() As String
        Get
            Return strFirstName
        End Get
        Set(ByVal Value As String)
            strFirstName = Value
        End Set
    End Property

    Public Property LastName() As String
        Get
            Return strLastName
        End Get
        Set(ByVal Value As String)
            strLastName = Value
        End Set
    End Property

    Public Property City() As String
        Get
            Return strCity
        End Get
        Set(ByVal Value As String)
            strCity = Value
        End Set
    End Property

    Public Property ZipCode() As String
        Get
            Return strZipCode
        End Get
        Set(ByVal Value As String)
            strZipCode = Value
        End Set
    End Property

    Public Property Address() As String
        Get
            Return strAddress
        End Get
        Set(ByVal Value As String)
            strAddress = Value
        End Set
    End Property

    Public Property Phone() As String
        Get
            Return strPhone
        End Get
        Set(ByVal Value As String)
            strPhone = Value
        End Set
    End Property

    Public Property CompanyName() As String
        Get
            Return strCompanyName
        End Get
        Set(ByVal Value As String)
            strCompanyName = Value
        End Set
    End Property

    Public Property StateVal() As String
        Get
            Return strStateVal
        End Get
        Set(ByVal Value As String)
            strStateVal = Value
        End Set
    End Property
    Public Sub GetUserByName(ByVal strUserNameInput As String)
        UserFound = False
        Dim MembershipUser As MembershipUser = Membership.GetUser(strUserNameInput)
        If Not MembershipUser Is Nothing Then
            UserFound = True
            ''''''''''Faster to do all of this in one query?
            UserId = MembershipUser.ProviderUserKey.ToString
            'Email = MembershipUser.Email.ToString
            'UserName = MembershipUser.UserName
            'IsApproved = MembershipUser.IsApproved

            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommand As String = "PersonUserSelect"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
            db.AddInParameter(objCommand, "@UserID", DbType.String, UserId)
            Dim datareader As SqlDataReader
            datareader = db.ExecuteReader(objCommand)
            If datareader.HasRows Then
                While datareader.Read
                    intPersonID = datareader("PersonID")
                    strDeptOrgName = datareader("DeptOrgName")
                    strUserTitle = datareader("Title")
                    strFirstName = datareader("FirstName")
                    strLastName = datareader("LastName")
                    strEmail = datareader("Email")
                    strCity = datareader("City")
                    strZipCode = datareader("ZipCode")
                    strAddress = datareader("Address1")
                    strPhone = datareader("PhoneNumber")
                    strCompanyName = datareader("CompanyName")
                    strStateVal = datareader("StateID")
                    strUserName = datareader("UserName")
                End While
            End If
            datareader.Close()

        Else
            UserId = ""
            Email = ""
            UserName = ""
        End If

    End Sub

    Public Sub GetUserByEmail(ByRef strEmailInput As String)
        UserFound = False
        Dim strUserNameInput As String = Membership.GetUserNameByEmail(strEmailInput)
        Dim MembershipUser As MembershipUser = Nothing
        If Not strUserNameInput Is Nothing Then
            MembershipUser = Membership.GetUser(strUserNameInput)
        End If
        If Not MembershipUser Is Nothing Then
            UserFound = True

            UserId = MembershipUser.ProviderUserKey.ToString
            'NetEmail = MembershipUser.Email.ToString
            'NetUserName = MembershipUser.UserName
            'NetIsApproved = MembershipUser.IsApproved
            Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
            Dim sqlcommand As String = "PersonUserSelect"
            Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
            db.AddInParameter(objCommand, "@UserID", DbType.String, UserId)
            Dim datareader As SqlDataReader
            datareader = db.ExecuteReader(objCommand)
            If datareader.HasRows Then
                While datareader.Read
                    intPersonID = datareader("PersonID")
                    strDeptOrgName = datareader("DeptOrgName")
                    strUserTitle = datareader("Title")
                    strFirstName = datareader("FirstName")
                    strLastName = datareader("LastName")
                    strEmail = datareader("Email")
                    strCity = datareader("City")
                    strZipCode = datareader("ZipCode")
                    strAddress = datareader("Address1")
                    strPhone = datareader("PhoneNumber")
                    strCompanyName = datareader("CompanyName")
                    strStateVal = datareader("StateID")
                    strUserName = datareader("UserName")
                End While
            End If
            datareader.Close()
        Else
            UserId = ""
            Email = ""
            UserName = ""
        End If


    End Sub

    Public Sub GetUserByLoginID(ByRef strLoginID As String)
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "PersonByLoginIdSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@LoginId", SqlDbType.Int, strLoginID)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        Do While datareader.Read()
            If datareader.HasRows Then
                strFirstName = datareader("FirstName")
                strLastName = datareader("LastName")
                strEmail = datareader("Email")
            End If
        Loop
        datareader.Close()
    End Sub

    Public Sub GetUserByID(ByRef strUserID As String)

        UserFound = True

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "PersonUserSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@UserID", DbType.String, strUserID)
        Dim datareader As SqlDataReader
        datareader = db.ExecuteReader(objCommand)
        If datareader.HasRows Then
            While datareader.Read
                intPersonID = datareader("PersonID")
                strDeptOrgName = datareader("DeptOrgName")
                strUserTitle = datareader("Title")
                strFirstName = datareader("FirstName")
                strLastName = datareader("LastName")
                strEmail = datareader("Email")
                strCity = datareader("City")
                strZipCode = datareader("ZipCode")
                strAddress = datareader("Address1")
                strPhone = datareader("PhoneNumber")
                strCompanyName = datareader("CompanyName")
                strStateVal = datareader("StateID")
                strUserName = datareader("UserName")
            End While
        End If
        datareader.Close()

    End Sub
End Class