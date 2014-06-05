Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Public Class EmailLog
    Inherits System.Web.UI.Page
    'Dim rgEmailDataReader As SqlDataReader
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        'login check
        Dim loginchk = New clsLoginCheck()

        'Only Admins 
        If Not User.IsInRole("Admin") And Not User.IsInRole("Contractor Admin") Then
            Response.Redirect("../Blank.aspx")
        End If

        If Not IsPostBack Then

            Dim intVersion As Integer = 0
            txtSelectEmailSentDate.Text = System.DateTime.Now.ToShortDateString()
            lblddlDaysVal.Text = ddlDays.SelectedValue
            lblSelectEmailSentDateVal.Text = txtSelectEmailSentDate.Text

        End If
    End Sub

    Private Sub sqlDatasource1_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles sqlDatasource1.Selecting
        e.Command.Parameters("@EndDate").Value = CDate(lblSelectEmailSentDateVal.Text).AddDays(lblddlDaysVal.Text)
        e.Command.Parameters("@StartDate").Value = CDate(lblSelectEmailSentDateVal.Text).AddDays(-CInt(lblddlDaysVal.Text))
    End Sub

    Private Sub btn508_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn508.Click
        If btn508.Text = "Keyboard Accessible View" Then
            btn508.Text = "Interactive View"
        Else
            btn508.Text = "Keyboard Accessible View"
        End If
        pnl508.Visible = Not pnl508.Visible
        rgEmail.ShowGroupPanel = Not rgEmail.ShowGroupPanel
        rgEmail.AllowFilteringByColumn = Not rgEmail.AllowFilteringByColumn
        rgEmail.MasterTableView.GroupByExpressions.Clear()
        rgEmail.MasterTableView.FilterExpression = ""
        rgEmail.Rebind()
    End Sub

    Private Sub btnGroupBy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGroupBy.Click
        Dim expression As GridGroupByExpression = New GridGroupByExpression
        Dim gridGroupByField As GridGroupByField = New GridGroupByField
        If ddlGroup.SelectedIndex < 1 Then
            rgEmail.MasterTableView.GroupByExpressions.Clear()
        Else
            If rgEmail.MasterTableView.GroupByExpressions.Count > 0 Then
                rgEmail.MasterTableView.GroupByExpressions.Clear()
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

            rgEmail.MasterTableView.GroupByExpressions.Insert(0, expression)
        End If
        rgEmail.Rebind()
    End Sub

    Private Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        If ddlFilter.SelectedIndex < 1 Then
            rgEmail.MasterTableView.FilterExpression = ""
            'clear all filter expressions also
        Else
            If ddlFilter.SelectedValue = "SentDate" Then
                Dim dtStartFilter As System.DateTime
                Dim dtEndFilter As System.DateTime
                Try
                    dtStartFilter = CDate(txtFilter.Text + " 00:00:00.0000000")
                    dtEndFilter = CDate(txtFilter.Text + " 23:59:59.9999999")
                    rgEmail.MasterTableView.FilterExpression = "([" & ddlFilter.SelectedValue & "] >= '" & dtStartFilter & "' AND [" & ddlFilter.SelectedValue & "] <= '" & dtEndFilter & "') "
                Catch ex As Exception
                    rgEmail.MasterTableView.FilterExpression = "([" & ddlFilter.SelectedValue & "] = " & txtFilter.Text & ") "
                End Try
            Else
                rgEmail.MasterTableView.FilterExpression = "([" & ddlFilter.SelectedValue & "] LIKE '%" & txtFilter.Text & "%') "
            End If
        End If
        rgEmail.MasterTableView.Rebind()
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        lblddlDaysVal.Text = ddlDays.SelectedValue
        lblSelectEmailSentDateVal.Text = txtSelectEmailSentDate.Text
        rgEmail.MasterTableView.CurrentPageIndex = 0
        rgEmail.MasterTableView.Rebind()
    End Sub

    Protected Sub Export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExportButton.Click
        ConfigureExport()

        If (Not IgnoreCheckBox.Checked) Then
            rgEmail.Rebind()
        End If
        rgEmail.ExportSettings.OpenInNewWindow = True
        rgEmail.MasterTableView.Caption = Now.ToString
        rgEmail.MasterTableView.ExportToExcel()
    End Sub

    Public Sub ConfigureExport()
        rgEmail.ExportSettings.ExportOnlyData = True
        rgEmail.ExportSettings.IgnorePaging = IgnoreCheckBox.Checked
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        rgEmail.MasterTableView.FilterExpression = ""
        rgEmail.Rebind()
    End Sub
End Class