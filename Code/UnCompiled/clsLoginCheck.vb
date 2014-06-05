Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Public Class clsLoginCheck
    Inherits System.Web.UI.Page

    Private bLoggedin As Boolean = False
    Private bAdmin As Boolean = False
    Private bMember As Boolean = False
    Private intNodeId As Integer = 0
    Private bHasView As Boolean = False
    Private bHasModify As Boolean = False
    Private bHasSetPermission As Boolean = False
    Private bHasModuleEdit As Boolean = False
    Private bHasModuleAdmin As Boolean = False

    Public Property NodeId()
        Get
            Return intNodeId
        End Get
        Set(ByVal Value)
            intNodeId = Value
        End Set
    End Property
    Public ReadOnly Property LoggedIn()
        Get
            Return bLoggedin
        End Get
    End Property
    Public ReadOnly Property Member()
        Get
            Return bMember
        End Get
    End Property
    Public ReadOnly Property Admin()
        Get
            Return bAdmin
        End Get
    End Property
    Public ReadOnly Property HasView()
        Get
            Return bHasView
        End Get
    End Property
    Public ReadOnly Property HasModify()
        Get
            Return bHasModify
        End Get
    End Property
    Public ReadOnly Property HasSetPermission()
        Get
            Return bHasSetPermission
        End Get
    End Property
    Public ReadOnly Property HasModuleEdit()
        Get
            Return bHasModuleEdit
        End Get
    End Property
    Public ReadOnly Property HasModuleAdmin()
        Get
            Return bHasModuleAdmin
        End Get
    End Property

    Public Sub New()
        Dim objURLScrub As New clsURLScrub
        Dim bURLError As Boolean = False
        objURLScrub.URLScrub(Context.Request.ServerVariables("QUERY_STRING"))
        If User.Identity.IsAuthenticated Then
            Dim objPageLog As New clsPageLog
            bLoggedin = True
            If Roles.IsUserInRole("Admin") Then
                bAdmin = True
            End If

            If Roles.IsUserInRole("Member") Then
                bMember = True
                '    'logout MEMBER
                '    FormsAuthentication.SignOut()
                '    Roles.DeleteCookie()
                '    FormsAuthentication.RedirectToLoginPage()
            Else
                Roles.AddUserToRole(Membership.GetUser().UserName.ToString, "Member")
                bMember = True
            End If
        Else
            If Not (Context.Request.Url.ToString.IndexOf("login.aspx", StringComparison.OrdinalIgnoreCase) > 0) And Not (Context.Request.Url.ToString.IndexOf("RecoverPassword.aspx", StringComparison.OrdinalIgnoreCase) > 0) And Not (Context.Request.Url.ToString.IndexOf("RecoverUserName.aspx", StringComparison.OrdinalIgnoreCase) > 0) Then
                FormsAuthentication.SignOut()
                Roles.DeleteCookie()
                FormsAuthentication.RedirectToLoginPage()
            End If
        End If
    End Sub ' New

    Public Sub New(ByVal NodeId As Integer)
        MyBase.New()
        GetNodePermission(NodeId)
    End Sub ' New

    Public Sub GetNodePermission(ByVal NodeId As Integer)
        Me.NodeId = NodeId
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "NodeIdPermissionSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        If bLoggedin Then
            ' Admin Role members have full access to everything
            If bAdmin Then
                bMember = True
                bHasView = True
                bHasModify = True
                bHasSetPermission = True
                bHasModuleEdit = True
                bHasModuleAdmin = True
            Else
                Dim m As MembershipUser = Membership.GetUser()
                m.ProviderUserKey.ToString()
                db.AddInParameter(objCommand, "@UserId", DbType.String, m.ProviderUserKey.ToString())
                If NodeId > 0 Then
                    db.AddInParameter(objCommand, "@NodeId", SqlDbType.Int, NodeId)
                    Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
                    If datareader.HasRows Then
                        Do While datareader.Read()
                            bHasView = (1 = datareader("HasView"))
                            bHasModify = (1 = datareader("HasModify"))
                            bHasSetPermission = (1 = datareader("HasSetPermission"))
                            bHasModuleEdit = (1 = datareader("HasModuleEdit"))
                            bHasModuleAdmin = (1 = datareader("HasModuleAdmin"))
                        Loop
                    Else ' no permissions found
                        bHasView = False
                        bHasModify = False
                        bHasSetPermission = False
                        bHasModuleEdit = False
                        bHasModuleAdmin = False
                    End If
                    datareader.Close()
                Else ' no NodeId set
                    bHasView = False
                    bHasModify = False
                    bHasSetPermission = False
                    bHasModuleEdit = False
                    bHasModuleAdmin = False
                End If
            End If
        Else ' not logged it
            bHasView = False
            bHasModify = False
            bHasSetPermission = False
            bHasModuleEdit = False
            bHasModuleAdmin = False
        End If
    End Sub

    Function HasIdeaPermission(ByVal IdeaId As Integer) As Boolean

    End Function
    Function HasRole(ByVal strRoleName As String) As Boolean
        Return Roles.IsUserInRole(strRoleName)
    End Function
End Class
