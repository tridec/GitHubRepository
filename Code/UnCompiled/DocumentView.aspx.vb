Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Partial Public Class DocumentView
    Inherits System.Web.UI.Page
    Dim bIdMatch As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Don't include no Cache will break https downloads
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim loginchk = New clsLoginCheck()
        If Not loginchk.LoggedIn Then
            Response.Redirect("login.aspx")
        End If

        If Not Page.IsPostBack Then

            Try
                Dim intDocumentUploadID As Integer = Convert.ToInt32(Request.QueryString("DocumentUploadId"))

                If intDocumentUploadID > 0 Then

                    'Get Version Info based on DocumentUploadID
                    Dim intDocumentID As Integer
                    Dim intVersion As Integer
                    Dim docDetails As New clsDocument()
                    docDetails.GetVersion(intDocumentUploadID)
                    intDocumentID = docDetails.DocumentId
                    intVersion = docDetails.Version

                    'Only do this if user is logged in
                    If bIdMatch = False Then
                        'Set Permissions
                        loginchk.GetNodePermission(docDetails.NodeId)

                        If Not loginchk.HasView() Then ' Currently NO View ONLY permission
                            Response.Redirect("Blank.aspx")
                        End If

                        'Perform Audit
                        Dim objAudit As New clsAudit
                        objAudit.RecordId = intDocumentID
                        objAudit.ModuleTypeAuditId = clsModuleTypeAudit.DocumentList
                        objAudit.AuditActionId = clsAuditAction.Viewed
                        objAudit.Version = intVersion
                        objAudit.SaveAction()

                    End If

                    'Get the Document Stream to view
                    Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
                    Dim sqlcommand As String = "DocumentUploadView"
                    Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)

                    If intDocumentUploadID <> 0 Then
                        db.AddInParameter(objCommand, "@DocumentUploadID", DbType.Int32, intDocumentUploadID)
                    End If

                    Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
                    Dim filesize As Integer
                    If datareader.Read Then
                        filesize = datareader("FileSize")
                        Response.ContentType = datareader("MimeType").ToString()
                        Response.AddHeader("Content-Disposition", "attachment; filename=""" + datareader("Filename") + """")
                        Response.OutputStream.Write(datareader("Image"), 0, filesize)
                        Response.Flush()
                        Response.Close()
                    End If
                    datareader.Close()
                End If
            Catch excException As Exception

            End Try
        End If
    End Sub

End Class