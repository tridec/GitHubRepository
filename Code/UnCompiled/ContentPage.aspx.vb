Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data

Partial Public Class ContentPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim loginchk = New clsLoginCheck()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ' redirect if not logged in or not admin
        If (Not loginchk.LoggedIn) Then
            Response.Redirect("login.aspx")
        End If
        If Not IsPostBack Then
            Dim intNodeId As Integer = 0
            If Not Request("NODEID") Is Nothing Then
                intNodeId = Request("NODEID")
            Else
                intNodeId = 0
            End If
            loginchk.GetNodePermission(Val(intNodeId))
            ' Currently NO View ONLY permission
            If Not loginchk.HasView() Then
                Response.Redirect("Blank.aspx")
            Else
                'Not sure How this would happen
            End If
            If Not Request("NODEID") Is Nothing Then
                LoadData(Request("NODEID"))
            Else
                lblContentText.Text = "Content not available at this time."
            End If
        End If
    End Sub

    Private Sub LoadData(ByVal intNodeId As Integer)
        'lookup and set title and content data
        Dim intRadEditID As Integer
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "RadEditSelectPublic"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@NodeId", SqlDbType.Int, intNodeId)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        If datareader.HasRows Then
            datareader.Read()
            intRadEditID = datareader("RadEditID")
            lblTitleText.Text = datareader("TitleText")
            lblContentText.Text = datareader("ContentText")

            'Write to Audit
            Dim objAudit As New clsAudit
            objAudit.RecordId = intRadEditID
            objAudit.ModuleTypeAuditId = clsModuleTypeAudit.ContentPage
            objAudit.AuditActionId = clsAuditAction.Viewed
            objAudit.SaveAction()

        Else
            lblContentText.Text = "Content not available at this time."
        End If
        datareader.Close()

    End Sub
End Class