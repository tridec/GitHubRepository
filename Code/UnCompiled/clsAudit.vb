Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Public Class clsAudit
    Inherits System.Web.UI.Page

    Private intRecordId As Integer = 0
    Private intAuditActionId As Integer = 0
    Private intModuleTypeAuditId As Integer = 0
    Private intVersion As Integer = 0
    Private strAffectedUserID As String = ""

    Public Property RecordId()
        Get
            Return intRecordId
        End Get
        Set(ByVal Value)
            intRecordId = Value
        End Set
    End Property

    Public Property AuditActionId()
        Get
            Return intAuditActionId
        End Get
        Set(ByVal Value)
            intAuditActionId = Value
        End Set
    End Property

    Public Property ModuleTypeAuditId()
        Get
            Return intModuleTypeAuditId
        End Get
        Set(ByVal Value)
            intModuleTypeAuditId = Value
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

    Public Property AffectedUserID()
        Get
            Return strAffectedUserID
        End Get
        Set(ByVal Value)
            strAffectedUserID = Value
        End Set
    End Property

    Function SaveAction() As Boolean
        ' Save the  Action to Audit Table 
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "AuditInsert"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        Dim m As MembershipUser = Membership.GetUser()

        db.AddInParameter(objCommand, "@RecordId", DbType.Int32, RecordId)
        db.AddInParameter(objCommand, "@ModuleTypeAuditId", DbType.Int32, ModuleTypeAuditId)
        If Not String.Equals(AffectedUserID, "", StringComparison.OrdinalIgnoreCase) Then
            db.AddInParameter(objCommand, "@AffectedUserID", DbType.String, strAffectedUserID)
        End If

        ' User performing Action 
        db.AddInParameter(objCommand, "@ActionUserId", DbType.String, m.ProviderUserKey.ToString())
        db.AddInParameter(objCommand, "@AuditActionId", DbType.Int32, AuditActionId)
        db.AddInParameter(objCommand, "@Version", DbType.Int32, Version)
        db.ExecuteNonQuery(objCommand)
        Return True
    End Function
End Class