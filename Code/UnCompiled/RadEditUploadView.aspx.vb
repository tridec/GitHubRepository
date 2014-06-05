Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Partial Public Class RadEditUploadView
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' login check
        Dim loginchk = New clsLoginCheck()
        'Don't include no Cache will break https downloads
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not loginchk.LoggedIn Then
            Response.Redirect("login.aspx")
        End If

        If Not Page.IsPostBack Then
            Try
                Dim intRadEditUploadID As Integer = Convert.ToInt32(Request.QueryString("RadEditUploadId"))
                If intRadEditUploadID > 0 Then

                    Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
                    Dim sqlcommand As String = "RadEditUploadView"
                    Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)

                    If intRadEditUploadID <> 0 Then
                        db.AddInParameter(objCommand, "@RadEditUploadID", DbType.Int32, intRadEditUploadID)
                    End If

                    Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
                    If datareader.Read Then
                        Response.ContentType = datareader("MimeType").ToString()
                        Response.AddHeader("Content-Disposition", "attachment; filename=""" + datareader("Filename") + """")
                        Response.OutputStream.Write(datareader("Image"), 0, datareader("FileSize"))
                        Response.Flush()
                        Response.Close()
                    End If
                    datareader.Close()
                End If
            Catch
            End Try
        End If
    End Sub

End Class