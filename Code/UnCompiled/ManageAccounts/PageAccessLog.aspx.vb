Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.IO
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Upload
Imports System.Data.SqlClient
Public Class PageAccessLog
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'login check
        Dim loginchk = New clsLoginCheck()

        'Only Admins 
        If Not User.IsInRole("Admin") And Not User.IsInRole("Contractor Admin") Then
            Response.Redirect("../Blank.aspx")
        End If

        If Not Page.IsPostBack Then
            Dim intVersion As Integer = 0
            txtSelectPageAccessDate.Text = System.DateTime.Now.ToShortDateString()
            lblddlDaysVal.Text = ddlDays.SelectedValue
            lblSelectPageAccessDateVal.Text = txtSelectPageAccessDate.Text
        End If
    End Sub
    Private Sub sqlDatasource1_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles sqlDatasource1.Selecting


        e.Command.Parameters("@EndDate").Value = CDate(lblSelectPageAccessDateVal.Text).AddDays(lblddlDaysVal.Text)
        e.Command.Parameters("@StartDate").Value = CDate(lblSelectPageAccessDateVal.Text).AddDays(-CInt(lblddlDaysVal.Text))

    End Sub

    Private Sub btn508_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn508.Click
        If btn508.Text = "Keyboard Accessible View" Then
            btn508.Text = "Interactive View"
        Else
            btn508.Text = "Keyboard Accessible View"
        End If
        pnl508.Visible = Not pnl508.Visible

        rgdPageLog.ShowGroupPanel = Not rgdPageLog.ShowGroupPanel
        rgdPageLog.AllowFilteringByColumn = Not rgdPageLog.AllowFilteringByColumn
        rgdPageLog.MasterTableView.GroupByExpressions.Clear()
        rgdPageLog.MasterTableView.FilterExpression = ""
        rgdPageLog.Rebind()
    End Sub

    Private Sub btnGroupBy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGroupBy.Click
        Dim expression As GridGroupByExpression = New GridGroupByExpression
        Dim gridGroupByField As GridGroupByField = New GridGroupByField
        If ddlGroup.SelectedIndex < 1 Then
            rgdPageLog.MasterTableView.GroupByExpressions.Clear()
        Else
            If rgdPageLog.MasterTableView.GroupByExpressions.Count > 0 Then
                rgdPageLog.MasterTableView.GroupByExpressions.Clear()
            End If
            'SelectFields values (appear in header)
            gridGroupByField = New GridGroupByField
            gridGroupByField.FieldName = ddlGroup.SelectedValue
            gridGroupByField.HeaderText = ddlGroup.SelectedItem.Text
            gridGroupByField.FieldAlias = ddlGroup.SelectedValue

            gridGroupByField.HeaderValueSeparator = " for current group: "
            gridGroupByField.FormatString = "<strong>{0}</strong>"

            expression.SelectFields.Insert(0, gridGroupByField)
            expression.GroupByFields.Insert(0, gridGroupByField)

            rgdPageLog.MasterTableView.GroupByExpressions.Insert(0, expression)
        End If
        rgdPageLog.Rebind()
    End Sub

    Private Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        If ddlFilter.SelectedIndex < 1 Then
            rgdPageLog.MasterTableView.FilterExpression = ""
            'clear all filter expressions also
        Else
           If ddlFilter.SelectedValue = "AccessDate" Then
                Dim dtStartFilter As System.DateTime
                Dim dtEndFilter As System.DateTime
                Try
                    dtStartFilter = CDate(txtFilter.Text + " 00:00:00.0000000")
                    dtEndFilter = CDate(txtFilter.Text + " 23:59:59.9999999")
                    rgdPageLog.MasterTableView.FilterExpression = "([" & ddlFilter.SelectedValue & "] >= '" & dtStartFilter & "' AND [" & ddlFilter.SelectedValue & "] <= '" & dtEndFilter & "') "
                Catch ex As Exception
                    rgdPageLog.MasterTableView.FilterExpression = "([" & ddlFilter.SelectedValue & "] = " & txtFilter.Text & ") "
                End Try
            Else

                rgdPageLog.MasterTableView.FilterExpression = "([" & ddlFilter.SelectedValue & "] LIKE '%" & txtFilter.Text & "%') "
            End If
        End If

        rgdPageLog.MasterTableView.Rebind()
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        lblddlDaysVal.Text = ddlDays.SelectedValue
        lblSelectPageAccessDateVal.Text = txtSelectPageAccessDate.Text
        rgdPageLog.MasterTableView.CurrentPageIndex = 0
        rgdPageLog.MasterTableView.Rebind()
    End Sub

    Protected Sub Export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExportButton.Click
        ConfigureExport()

        If (Not IgnoreCheckBox.Checked) Then
            rgdPageLog.Rebind()
        End If
        rgdPageLog.ExportSettings.OpenInNewWindow = True
        rgdPageLog.MasterTableView.Caption = Now.ToString
        rgdPageLog.MasterTableView.ExportToExcel()
    End Sub

    Public Sub ConfigureExport()
        rgdPageLog.ExportSettings.ExportOnlyData = True
        rgdPageLog.ExportSettings.IgnorePaging = IgnoreCheckBox.Checked
    End Sub
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        rgdPageLog.MasterTableView.FilterExpression = ""
        rgdPageLog.Rebind()
        'Response.Redirect("PageLog.aspx")
    End Sub
End Class