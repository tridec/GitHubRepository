Imports System.Data.Common
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Microsoft.Practices.EnterpriseLibrary.Data

Partial Public Class MemberApprove
    Inherits System.Web.UI.Page
    Dim datareader As SqlDataReader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        'login check
        Dim loginchk = New clsLoginCheck()
        If (Not loginchk.LoggedIn) Then
            Response.Redirect("../login.aspx")
        End If

        ' redirect if not logged in or not admin
        loginchk.GetNodePermission(Val(Request("NODEID")))
        ' Currently NO View ONLY permission
        If Not loginchk.HasView() Then
            Response.Redirect("../Blank.aspx")
        ElseIf Not loginchk.HasModuleAdmin() Then ' must have Admin permissions on the NODE
            Response.Redirect("../Blank.aspx")
        End If
    End Sub
    Private Sub LoadData()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "AccessApproveSelect"
        Dim objcommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objcommand, "@FilterType", DbType.String, ddlFilterType.SelectedValue)
        datareader = db.ExecuteReader(objcommand)
        rdgAccessRequest.DataSource = datareader
    End Sub

    Private Sub rdgAccessRequest_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdgAccessRequest.DataBound
        datareader.Close()
    End Sub

    Private Sub rdgAccessRequest_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rdgAccessRequest.ItemDataBound
        If Not TypeOf e.Item Is GridEditFormItem Then
            Dim lblProcess As Label = e.Item.FindControl("lblProcess")
            Dim btnProcess As Button = e.Item.FindControl("btnProcess")

            If ddlFilterType.SelectedValue = 1 Then
                rdgAccessRequest.Columns.FindByDataField("ProcessedDate").Visible = False
            Else
                rdgAccessRequest.Columns.FindByDataField("ProcessedDate").Visible = True
            End If

            If Not lblProcess Is Nothing Then
                If lblProcess.Text = "Yes" Then
                    btnProcess.Visible = False
                    btnProcess.Enabled = False
                ElseIf lblProcess.Text = "No" Then
                    btnProcess.Visible = True
                    btnProcess.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub rdgAccessRequest_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rdgAccessRequest.NeedDataSource
        LoadData()
    End Sub

    Private Sub rdgAccessRequest_UpdateCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rdgAccessRequest.UpdateCommand
        Dim lblAccessRequestID As Label = e.Item.FindControl("lblAccessRequestID")

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "AccessApproveUpdate"
        Dim objcommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objcommand, "@AccessRequestID", DbType.Int32, lblAccessRequestID.Text)
        db.ExecuteNonQuery(objcommand)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "User Processed", String.Format("alert('{0}');", "Record Updated"), True)

    End Sub

    Private Sub ddlFilterType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFilterType.SelectedIndexChanged
        rdgAccessRequest.Rebind()
    End Sub
End Class