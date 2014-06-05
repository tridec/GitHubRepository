Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Public Class CompanyAdmin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        'login check
        Dim loginchk = New clsLoginCheck()
        If (Not loginchk.LoggedIn) Then
            Response.Redirect("../login.aspx")
        End If

        'Node Check
        loginchk.GetNodePermission(Val(Request("NODEID")))
        If Not loginchk.HasView Then
            If Not loginchk.HasModuleAdmin Then
                Response.Redirect("../Blank.aspx")
            End If
        End If
    End Sub

    Private Sub btnAddCompany_Click(sender As Object, e As System.EventArgs) Handles btnAddCompany.Click      
        'show edit panel
        ShowPanel()
        'set title
        lblCompanyTitle.Text = "Company Insert"
    End Sub

    Private Sub rgCompany_UpdateCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgCompany.UpdateCommand
        'show edit panel
        ShowPanel()
        'this opens the edit insert panel from the edit link in the grid
        If (e.CommandSource.GetType.ToString = "System.Web.UI.WebControls.LinkButton") Then
            'set title
            lblCompanyTitle.Text = "Company Edit"
            'this loads the Panel info if it is an edit
            LoadCompany(source, e)
        End If
    End Sub

    Private Sub LoadCompany(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs)
        'Get the Panel Info
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "CompanySelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@CompanyID", DbType.Int32, rgCompany.MasterTableView.DataKeyValues(e.Item.ItemIndex)("CompanyID"))
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)
        If datareader.HasRows Then
            datareader.Read()
            lblCompanyID.Text = datareader("CompanyID")
            txtCompanyName.Text = datareader("CompanyName")
        End If
        datareader.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        'Get the Panel Info
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "CompanyInsertUpdate"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@CompanyID", DbType.Int32, Val(lblCompanyID.Text))
        db.AddInParameter(objCommand, "@CompanyName", DbType.String, txtCompanyName.Text)
        db.ExecuteNonQuery(objCommand)
        'Clears the form if previous data
        ClearForm()
        'Hide edit panel
        HidePanel()
        'rebind grid
        rgCompany.Rebind()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        'Clears the form if previous data
        ClearForm()
        'Hide edit panel
        HidePanel()
    End Sub

    Private Sub ShowPanel()
        pnlInsertEdit.Visible = True
        btnAddCompany.Visible = False
    End Sub

    Private Sub HidePanel()
        pnlInsertEdit.Visible = False
        btnAddCompany.Visible = True
    End Sub

    Private Sub ClearForm()
        lblCompanyTitle.Text = ""
        lblCompanyID.Text = ""
        txtCompanyName.Text = ""
    End Sub
End Class