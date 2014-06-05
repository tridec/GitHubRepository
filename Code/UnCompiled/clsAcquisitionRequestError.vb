Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Public Class clsAcquisitionRequestError
    Inherits System.Web.UI.Page
    Private intAcqRequestID As Integer = 0
    Private strErrorDescription As String = ""
    Private strSystemErrorMessage As String = ""

    Public Property AcqRequestID()
        Get
            Return intAcqRequestID
        End Get
        Set(ByVal value)
            intAcqRequestID = value
        End Set
    End Property

    Public Property ErrorDescription()
        Get
            Return strErrorDescription
        End Get
        Set(ByVal value)
            strErrorDescription = value
        End Set
    End Property

    Public Property SystemErrorMessage()
        Get
            Return strSystemErrorMessage
        End Get
        Set(ByVal value)
            strSystemErrorMessage = value
        End Set
    End Property

    Function SaveError() As Boolean
        'save the logged in user, AcqRequestId, SystemErrorMessage, and ErrorDescription
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "AcqRequestErrorInsert"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        Dim m As MembershipUser = Membership.GetUser()
        db.AddInParameter(objCommand, "@AcqRequestID", DbType.Int32, AcqRequestID)
        db.AddInParameter(objCommand, "@ErrorDescription", DbType.String, ErrorDescription)
        db.AddInParameter(objCommand, "@UserID", DbType.String, m.ProviderUserKey.ToString())
        db.AddInParameter(objCommand, "@SystemErrorMessage", DbType.String, SystemErrorMessage)
        db.ExecuteNonQuery(objCommand)

        'set status to error for the request ID in the request table
        sqlCommand = "AcqRequestStatusUpdate"
        objCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@LoggedInUserID", DbType.String, m.ProviderUserKey.ToString)
        db.AddInParameter(objCommand, "@AcqRequestID", DbType.Int32, AcqRequestID)
        db.AddInParameter(objCommand, "@AcqRequestStatusID", DbType.Int32, 6)
        db.ExecuteNonQuery(objCommand)

        Return True
    End Function
End Class
