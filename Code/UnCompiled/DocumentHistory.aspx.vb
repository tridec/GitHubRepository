
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Partial Public Class DocumentHistory
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' login check
        Dim loginchk = New clsLoginCheck()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not loginchk.LoggedIn Then
            Response.Redirect("Login.aspx")
        End If

        If Not IsPostBack Then
            If Not Request("DocumentID") Is Nothing Then
                LoadData(Val(Request("DocumentID")))
            End If


        End If
    End Sub
    Private Sub LoadData(ByVal intDocumentId As Integer)
        Dim db1 As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand1 As String = "DocumentHistoryVersion"
        Dim objCommand1 As DbCommand = db1.GetStoredProcCommand(sqlcommand1)
        db1.AddInParameter(objCommand1, "@DocumentID", SqlDbType.Int, intDocumentId)
        Dim datareader1 As SqlDataReader = db1.ExecuteReader(objCommand1)
        rdgVersion.DataSource = datareader1
        rdgVersion.DataBind()

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "AuditSelectRecord"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@RecordID", SqlDbType.Int, intDocumentId)
        db.AddInParameter(objCommand, "@ModuleTypeAuditID", SqlDbType.Int, clsModuleTypeAudit.DocumentList)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        rdgDocuments.DataSource = datareader
        rdgDocuments.DataBind()

    End Sub


    Private Sub btnReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        Response.Redirect("ManageDocuments.aspx?NodeID=" + Request("NodeID"))
    End Sub
End Class