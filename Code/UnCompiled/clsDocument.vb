
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Public Class clsDocument
    Private intDocumentId As Integer
    Private intNodeId As Integer
    Private strDocumentTitle As String
    Private strDocumentDescription As String
    Private intVersion As Integer
    Private intPublish As Integer
    Private dtUploadDate As DateTime
    Private strUploadUserID As Guid
    Private intDocumentUploadId As Integer
    Private intDeleted As Integer
    Private strDeletedUserID As Guid
    Private dtDeletedDate As DateTime
    Private strUserId As Guid

    Public Property DocumentId()
        Get
            Return intDocumentId
        End Get
        Set(ByVal Value)
            intDocumentId = Value
        End Set
    End Property

    Public Property NodeId()
        Get
            Return intNodeId
        End Get
        Set(ByVal Value)
            intNodeId = Value
        End Set
    End Property

    Public Property DocumentTitle()
        Get
            Return strDocumentTitle
        End Get
        Set(ByVal Value)
            strDocumentTitle = Value
        End Set
    End Property

    Public Property DocumentDescription()
        Get
            Return strDocumentDescription
        End Get
        Set(ByVal Value)
            strDocumentDescription = Value
        End Set
    End Property

    Public Property Version()
        Get
            Return intVersion
        End Get
        Set(ByVal Value)
            intVersion = Value
        End Set
    End Property

    Public Property Publish()
        Get
            Return intPublish
        End Get
        Set(ByVal Value)
            intPublish = Value
        End Set
    End Property

    Public Property UploadDate()
        Get
            Return dtUploadDate
        End Get
        Set(ByVal Value)
            dtUploadDate = Value
        End Set
    End Property

    Public Property UploadUserID()
        Get
            Return strUploadUserID
        End Get
        Set(ByVal Value)
            strUploadUserID = Value
        End Set
    End Property

    Public Property DocumentUploadID()
        Get
            Return intDocumentUploadId
        End Get
        Set(ByVal Value)
            intDocumentUploadId = Value
        End Set
    End Property

    Public Property Deleted()
        Get
            Return intDeleted
        End Get
        Set(ByVal Value)
            intDeleted = Value
        End Set
    End Property

    Public Property DeletedDate()
        Get
            Return dtDeletedDate
        End Get
        Set(ByVal Value)
            dtDeletedDate = Value
        End Set
    End Property

    Public Property DeletedUserID()
        Get
            Return strDeletedUserID
        End Get
        Set(ByVal Value)
            strDeletedUserID = Value
        End Set
    End Property

    Public Property UserID()
        Get
            Return strUserId
        End Get
        Set(ByVal Value)
            strUserId = Value
        End Set
    End Property
    Sub New()
        MyBase.New()
    End Sub
    Sub New(ByVal DocumentID As Integer)
        MyBase.New()
        intDocumentId = DocumentID

        'Get the current values from the database
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "DocumentSelectAll"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@DocumentId", SqlDbType.Int, DocumentID)
        Dim dr As SqlDataReader = db.ExecuteReader(objCommand)

        If dr.HasRows Then
            dr.Read()
            NodeId = dr("NodeId")
            DocumentTitle = dr("DocumentTitle")
            DocumentDescription = dr("DocumentDescription")
            Version = dr("Version")
            Publish = dr("Publish")
            UploadDate = dr("UploadDate")
            UploadUserID = dr("UploadUserID")
            DocumentUploadID = dr("DocumentUploadId")
            Deleted = dr("Deleted")
            If Not dr("DeletedUserId") Is DBNull.Value Then
                DeletedUserID = dr("DeletedUserID")
            End If
            If Not dr("DeletedDate") Is DBNull.Value Then
                DeletedDate = dr("DeletedDate")
            End If

        End If
        dr.Close()

    End Sub

    Sub Update()

        'Get Membership Information
        Dim m2 As MembershipUser = Membership.GetUser()

        'Update the database
        Dim dbUpload As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommandUpload As String = "DocumentUpdate"
        Dim objCommandUpload As DbCommand = dbUpload.GetStoredProcCommand(sqlcommandUpload)
        dbUpload.AddInParameter(objCommandUpload, "@DocumentID", DbType.String, intDocumentId)
        dbUpload.AddInParameter(objCommandUpload, "@NodeID", DbType.String, intNodeId)
        dbUpload.AddInParameter(objCommandUpload, "@DocumentTitle", DbType.String, strDocumentTitle)
        dbUpload.AddInParameter(objCommandUpload, "@DocumentDescription", DbType.String, strDocumentDescription)
        dbUpload.AddInParameter(objCommandUpload, "@Version", DbType.Int32, intVersion)
        dbUpload.AddInParameter(objCommandUpload, "@Publish", DbType.Int32, intPublish)
        dbUpload.AddInParameter(objCommandUpload, "@UploadDate", DbType.DateTime, dtUploadDate)
        dbUpload.AddInParameter(objCommandUpload, "@UploadUserID", DbType.Guid, strUploadUserID)
        dbUpload.AddInParameter(objCommandUpload, "@DocumentUploadID", DbType.Int32, intDocumentUploadId)
        dbUpload.AddInParameter(objCommandUpload, "@Deleted", DbType.Int32, intDeleted)
        If Not dtDeletedDate.Date = "12:00:00 AM" Then
            dbUpload.AddInParameter(objCommandUpload, "@DeletedDate", DbType.DateTime, dtDeletedDate)
        End If

        dbUpload.AddInParameter(objCommandUpload, "@DeletedUserID", DbType.Guid, strDeletedUserID)
        dbUpload.AddInParameter(objCommandUpload, "@UserId", DbType.Guid, m2.ProviderUserKey)
        dbUpload.ExecuteNonQuery(objCommandUpload)
    End Sub

    Function Insert()
        Dim intDocumentID As Integer

        'Get Membership Information
        Dim m2 As MembershipUser = Membership.GetUser()

        'Insert new Record
        Dim dbUpload As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommandUpload As String = "DocumentUpdate"
        Dim objCommandUpload As DbCommand = dbUpload.GetStoredProcCommand(sqlcommandUpload)
        dbUpload.AddInParameter(objCommandUpload, "@NodeID", DbType.String, intNodeId)
        dbUpload.AddInParameter(objCommandUpload, "@DocumentTitle", DbType.String, strDocumentTitle)
        dbUpload.AddInParameter(objCommandUpload, "@DocumentDescription", DbType.String, strDocumentDescription)
        dbUpload.AddInParameter(objCommandUpload, "@Version", DbType.Int32, intVersion)
        dbUpload.AddInParameter(objCommandUpload, "@Publish", DbType.Int32, intPublish)
        dbUpload.AddInParameter(objCommandUpload, "@UploadUserID", DbType.String, m2.ProviderUserKey.ToString())
        dbUpload.AddInParameter(objCommandUpload, "@UploadDate", DbType.DateTime, Now())
        dbUpload.AddInParameter(objCommandUpload, "@DocumentUploadID", DbType.Int32, intDocumentUploadId)
        dbUpload.AddInParameter(objCommandUpload, "@UserId", DbType.String, m2.ProviderUserKey.ToString())
        dbUpload.AddOutParameter(objCommandUpload, "@InsertDocumentID", SqlDbType.Int, 100)
        dbUpload.ExecuteNonQuery(objCommandUpload)

        If Not dbUpload.GetParameterValue(objCommandUpload, "@InsertDocumentID") Is System.DBNull.Value Then
            intDocumentID = dbUpload.GetParameterValue(objCommandUpload, "@InsertDocumentID")
        End If

        Return intDocumentId
    End Function

    Sub Delete()

        'Get Membership Information
        Dim m2 As MembershipUser = Membership.GetUser()

        'Set Deleted Fields
        intDeleted = 1
        dtDeletedDate = Now
        strDeletedUserID = m2.ProviderUserKey
        intPublish = 0

        'Update the database
        Dim dbUpload As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommandUpload As String = "DocumentUpdate"
        Dim objCommandUpload As DbCommand = dbUpload.GetStoredProcCommand(sqlcommandUpload)
        dbUpload.AddInParameter(objCommandUpload, "@DocumentID", DbType.String, intDocumentId)
        dbUpload.AddInParameter(objCommandUpload, "@NodeID", DbType.String, intNodeId)
        dbUpload.AddInParameter(objCommandUpload, "@DocumentTitle", DbType.String, strDocumentTitle)
        dbUpload.AddInParameter(objCommandUpload, "@DocumentDescription", DbType.String, strDocumentDescription)
        dbUpload.AddInParameter(objCommandUpload, "@Version", DbType.Int32, intVersion)
        dbUpload.AddInParameter(objCommandUpload, "@Publish", DbType.Int32, intPublish)
        dbUpload.AddInParameter(objCommandUpload, "@UploadDate", DbType.DateTime, dtUploadDate)
        dbUpload.AddInParameter(objCommandUpload, "@UploadUserID", DbType.Guid, strUploadUserID)
        dbUpload.AddInParameter(objCommandUpload, "@DocumentUploadID", DbType.Int32, intDocumentUploadId)
        dbUpload.AddInParameter(objCommandUpload, "@Deleted", DbType.Int32, intDeleted)
        dbUpload.AddInParameter(objCommandUpload, "@DeletedDate", DbType.DateTime, dtDeletedDate)
        dbUpload.AddInParameter(objCommandUpload, "@DeletedUserID", DbType.Guid, strDeletedUserID)
        dbUpload.AddInParameter(objCommandUpload, "@UserId", DbType.Guid, m2.ProviderUserKey)
        dbUpload.ExecuteNonQuery(objCommandUpload)
    End Sub

    Sub GetVersion(ByVal intDocumentUploadID As Integer)
        'Get the current values from the database
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "DocumentSelectVersion"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@DocumentUploadId", SqlDbType.Int, intDocumentUploadId)
        Dim dr As SqlDataReader = db.ExecuteReader(objCommand)

        If dr.HasRows Then
            dr.Read()
            DocumentId = dr("DocumentID")
            NodeId = dr("NodeId")
            DocumentTitle = dr("DocumentTitle")
            DocumentDescription = dr("DocumentDescription")
            Version = dr("Version")
            Publish = dr("Publish")
            UploadDate = dr("UploadDate")
            UploadUserID = dr("UploadUserID")
            DocumentUploadID = dr("DocumentUploadId")
            Deleted = dr("Deleted")
            If Not dr("DeletedUserId") Is DBNull.Value Then
                DeletedUserID = dr("DeletedUserID")
            End If
            If Not dr("DeletedDate") Is DBNull.Value Then
                DeletedDate = dr("DeletedDate")
            End If

        End If
        dr.Close()
    End Sub

End Class
