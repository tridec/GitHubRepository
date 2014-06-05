Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class ManageDocuments
    Inherits System.Web.UI.Page

    Dim datareader As SqlDataReader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' login check
        Dim loginchk = New clsLoginCheck()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not loginchk.LoggedIn Then
            Response.Redirect("Login.aspx")
        End If

        If Not IsPostBack Then
            If Not Request("NODEID") Is Nothing Then
                lblNodeID.Text = Request("NODEID")
            Else
                lblNodeID.Text = 0
            End If

            'Set Permissions
            loginchk.GetNodePermission(Val(lblNodeID.Text))

            If Not loginchk.HasView() Then ' Currently NO View ONLY permission
                Response.Redirect("Blank.aspx")
            ElseIf loginchk.HasModuleAdmin() Then
                'Admin has full rights
            ElseIf loginchk.HasModuleEdit() Then ' Only edit permissions
                'Disallow Publishing and modifying Published Records
                btnPublish.Visible = False
                lblEditOnly.Text = "True"
            ElseIf loginchk.HasView() Then
                'If the ONLY have view permissions - hide all functionality except view.
                btnAddDocument.Visible = False
                btnAddVersion.Visible = False
                btnDelete.Visible = False
                btnPublish.Visible = False
                btnUpdate.Visible = False
                btnUnPublish.Visible = False
            Else ' must have edit or Admin permissions on the NODE to view the page
                Response.Redirect("Blank.aspx")
            End If

            'LoadData()
        End If

    End Sub

    Private Sub rdgDocuments_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rdgDocuments.NeedDataSource
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "DocumentSelectAll"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@NodeId", SqlDbType.Int, lblNodeID.Text)
        datareader = db.ExecuteReader(objCommand)
        rdgDocuments.DataSource = datareader
    End Sub

    Private Sub rdgDocuments_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdgDocuments.DataBound
        datareader.Close()
    End Sub

    Private Sub btnAddDocument_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddDocument.Click
        Response.Redirect("DocumentAdd.aspx?NodeID=" + lblNodeID.Text)
    End Sub

    Private Sub btnPublish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPublish.Click
        lblWarning.Text = ""
        Dim intDocument As Integer
        intDocument = Val(lblRowID.Value)

        If intDocument <= 0 Then
            lblWarning.Text = "Please select a Document"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Invalid Document Select", _
            String.Format("alert('{0}');", "Please select a Document"), True)
            Exit Sub
        End If

        'Update File Details
        Dim docUpdate As New clsDocument(intDocument)
        docUpdate.Publish = 1
        docUpdate.Update()

        'Perform Audit
        Dim objAudit As New clsAudit
        objAudit.RecordId = intDocument
        objAudit.ModuleTypeAuditId = clsModuleTypeAudit.DocumentList
        objAudit.AuditActionId = clsAuditAction.Published
        objAudit.SaveAction()

        'LoadData()
        rdgDocuments.Rebind()
        lblRowID.Value = ""
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        lblWarning.Text = ""
        Dim intDocument As Integer
        intDocument = Val(lblRowID.Value)

        If intDocument <= 0 Then
            lblWarning.Text = "Please select a Document"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Invalid Document Select", _
            String.Format("alert('{0}');", "Please select a Document"), True)
            Exit Sub
        End If

        'Update File Details
        Dim docUpdate As New clsDocument(intDocument)
        docUpdate.Delete()

        'Perform Audit
        Dim objAudit As New clsAudit
        objAudit.RecordId = intDocument
        objAudit.ModuleTypeAuditId = clsModuleTypeAudit.DocumentList
        objAudit.AuditActionId = clsAuditAction.Deleted
        objAudit.SaveAction()

        'LoadData()
        rdgDocuments.Rebind()
        lblRowID.Value = ""
    End Sub

    Private Sub btnAddVersion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddVersion.Click
        lblWarning.Text = ""
        Dim intDocument As Integer
        intDocument = Val(lblRowID.Value)

        If intDocument <= 0 Then
            lblWarning.Text = "Please select a Document"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Invalid Document Select", _
            String.Format("alert('{0}');", "Please select a Document"), True)
            Exit Sub
        End If

        Response.Redirect("DocumentAdd.aspx?DocumentID=" + intDocument.ToString + "&NodeID=" + lblNodeID.Text + "&Version=New")
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        lblWarning.Text = ""
        Dim intDocument As Integer
        intDocument = Val(lblRowID.Value)

        If intDocument <= 0 Then
            lblWarning.Text = "Please select a Document"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Invalid Document Select", _
            String.Format("alert('{0}');", "Please select a Document"), True)
            Exit Sub
        End If

        Response.Redirect("DocumentAdd.aspx?DocumentID=" + intDocument.ToString + "&NodeID=" + lblNodeID.Text)
    End Sub

    Private Sub rdgDocuments_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rdgDocuments.ItemDataBound

        If TypeOf e.Item Is GridDataItem Then
            Dim gridItem As GridDataItem = CType(e.Item, GridDataItem)
            Dim lblDocUploadID As Label = e.Item.FindControl("lblDocUploadIDValue")

            'If user has only Edit permissions - disable selection of Published records
            If lblEditOnly.Text = "True" Then
                Dim strPublish As String = e.Item.Cells(5).Text
                Dim cbxSelect As CheckBox = DirectCast(e.Item.FindControl("chkSelected"), CheckBox)
                If strPublish = "Yes" Then
                    cbxSelect.Enabled = False
                    'e.Item.Enabled = False
                    inputSelect.Value += e.Item.ItemIndex.ToString + ":"
                End If
            End If

            'add javascript call to checkbox so that when checkbox is clicked, it will call the javascript to uncheck all boxes except the one that was checked.
            For Each column As GridColumn In rdgDocuments.MasterTableView.RenderColumns
                If (TypeOf column Is GridTemplateColumn) Then
                    DirectCast(e.Item.FindControl("chkSelected"), CheckBox).Attributes.Add("onclick", "javascript:UncheckMostCheckboxes('" & DirectCast(e.Item.FindControl("chkSelected"), CheckBox).ClientID & "'," & lblDocUploadID.Text & ")")
                End If
                'If (TypeOf column Is GridClientSelectColumn) Then
                ''this line will show a tooltip based on the CustomerID data field
                'gridItem(column.UniqueName).ToolTip = ("Select Document: " + gridItem.OwnerTableView.DataKeyValues(gridItem.ItemIndex)("DocumentTitle").ToString)
                ''This is in case you wish to display the column name instead of data field.
                ''gridItem[column.UniqueName].ToolTip = "Tooltip: " + column.UniqueName;
            Next
        End If
    End Sub

    Private Sub btnUnPublish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUnPublish.Click
        lblWarning.Text = ""
        Dim intDocument As Integer
        intDocument = Val(lblRowID.Value)

        If intDocument <= 0 Then
            lblWarning.Text = "Please select a Document"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Invalid Document Select", _
            String.Format("alert('{0}');", "Please select a Document"), True)
            Exit Sub
        End If

        'Update File Details
        Dim docUpdate As New clsDocument(intDocument)
        docUpdate.Publish = 0
        docUpdate.Update()

        'Perform Audit
        Dim objAudit As New clsAudit
        objAudit.RecordId = intDocument
        objAudit.ModuleTypeAuditId = clsModuleTypeAudit.DocumentList
        objAudit.AuditActionId = clsAuditAction.UnPublished
        objAudit.SaveAction()

        'LoadData()
        rdgDocuments.Rebind()
        lblRowID.Value = ""
    End Sub

   
End Class