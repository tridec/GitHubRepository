Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common

Public Class clsPageLog
    Inherits System.Web.UI.Page

    Private strFullURL As String = ""
    Private strPageName As String = ""
    Private strQueryString As String = ""
    Private intNodeId As Integer = 0

    Public Property FullURL()
        Get
            Return strFullURL
        End Get
        Set(ByVal Value)
            strFullURL = Value
        End Set
    End Property
    Public Property PageName()
        Get
            Return strPageName
        End Get
        Set(ByVal Value)
            strPageName = Value
        End Set
    End Property
    Public Property QueryString()
        Get
            Return strQueryString
        End Get
        Set(ByVal Value)
            strQueryString = Value
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

    Public Sub New()

        FullURL = Context.Request.Url.ToString().Trim(" ")
        PageName = GetPageName(FullURL)
        QueryString = Context.Request.ServerVariables("QUERY_STRING")
        NodeId = Val(Context.Request("NodeID"))

        ' save the info to the database insert or update
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "PageLogInsert"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        Dim m As MembershipUser = Membership.GetUser()
        ' don't log if no UserId - Not Logged in 
        If Not m Is Nothing Then
            db.AddInParameter(objCommand, "@UserId", DbType.String, m.ProviderUserKey.ToString())
            ' Add ProxyId here
            db.AddInParameter(objCommand, "@FullURL", DbType.String, FullURL)
            db.AddInParameter(objCommand, "@PageName", DbType.String, PageName)
            db.AddInParameter(objCommand, "@QueryString", DbType.String, QueryString)
            db.AddInParameter(objCommand, "@NodeId", DbType.Int32, NodeId)
            db.ExecuteNonQuery(objCommand)
        End If

    End Sub

    Private Function GetPageName(ByVal strURL As String)
        Dim sPath As String = System.Web.HttpContext.Current.Request.Url.AbsolutePath
        Dim oInfo As New System.IO.FileInfo(sPath)
        Dim sRet As String = oInfo.Name
        Return sRet
    End Function

End Class
