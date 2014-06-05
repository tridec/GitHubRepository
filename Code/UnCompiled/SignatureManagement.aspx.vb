Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports VANS.MdwsDemo.dao.soap
Imports VANS.MdwsDemo.domain
Imports VANS.us.vacloud.devmdws

Public Class SignatureManagement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'login check
        Dim loginchk = New clsLoginCheck()
        If (Not loginchk.LoggedIn) Then
            Response.Redirect("~/login.aspx")
        End If

        If Not loginchk.Admin Then
            Response.Redirect("HomeMain.aspx")
        End If

        If Not Page.IsPostBack Then

        End If
    End Sub

    Private Sub rgdSignature_DeleteCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgdSignature.DeleteCommand
        Dim gdi As GridDataItem = DirectCast(e.Item, GridDataItem)

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "SignatureDelete"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@SignatureID", DbType.Int32, gdi.GetDataKeyValue("SignatureID"))
        db.ExecuteNonQuery(objCommand)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "MyScript", "alert('" & gdi("Title").Text & " has been deleted" & "');", True)

        rgdSignature.Rebind()
    End Sub

    Private Sub rgdSignature_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgdSignature.NeedDataSource
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "SignatureSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        rgdSignature.DataSource = db.ExecuteDataSet(objCommand)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        pnlSignatureEdit.Visible = True
        pnlSignatureList.Visible = False
        lblSignatureEditTitle.Text = "Add Signature"
        reSignature.Content = ""
        txtTitle.Text = ""
        chkDefaultEnabled.Checked = False
    End Sub

    Private Sub rgdSignature_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgdSignature.UpdateCommand
        pnlSignatureEdit.Visible = True
        pnlSignatureList.Visible = False
        lblSignatureEditTitle.Text = "Edit Signature"
        chkDefaultEnabled.Checked = False

        Dim gdi As GridDataItem = DirectCast(e.Item, GridDataItem)
        lblSignatureID.Text = gdi.GetDataKeyValue("SignatureID")

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "SignatureSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.AddInParameter(objCommand, "@SignatureID", DbType.Int32, lblSignatureID.Text)
        Using datareader As IDataReader = db.ExecuteReader(objCommand)
            If datareader.Read Then
                txtTitle.Text = datareader("Title")
                chkDefaultEnabled.Checked = datareader("DefaultEnabled") * -1
                reSignature.Content = datareader("Message")
            End If
        End Using

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        pnlSignatureEdit.Visible = False
        pnlSignatureList.Visible = True
        lblSignatureID.Text = ""
        chkDefaultEnabled.Checked = False
    End Sub

    Private Sub ResetDefault()
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "SignatureDefaultResetUpdate"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)
        db.ExecuteNonQuery(objCommand)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        Dim blnInsert As Boolean
        If Val(lblSignatureID.Text) = 0 Then
            blnInsert = True
        End If

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlCommand As String = "SignatureInsertUpdate"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlCommand)

        If chkDefaultEnabled.Checked = True Then
            'Reset previous default enabled.
            ResetDefault()
        End If

        If blnInsert = True Then
            db.AddOutParameter(objCommand, "@InsertSignatureID", SqlDbType.Int, 100)
        Else
            db.AddInParameter(objCommand, "@SignatureID", DbType.Int32, lblSignatureID.Text)
        End If

        db.AddInParameter(objCommand, "@DefaultEnabled", DbType.Int32, chkDefaultEnabled.Checked * -1)
        db.AddInParameter(objCommand, "@Title", DbType.String, txtTitle.Text)
        db.AddInParameter(objCommand, "@Message", DbType.String, reSignature.Content)
        db.ExecuteNonQuery(objCommand)

        If blnInsert = True Then
            lblSignatureID.Text = db.GetParameterValue(objCommand, "@InsertSignatureID")
        End If

        pnlSignatureEdit.Visible = False
        pnlSignatureList.Visible = True
        rgdSignature.Rebind()

        'Display popup message for insert or update
        If blnInsert = True Then
            'New record created
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "MyScript", "alert('" & txtTitle.Text & " has been created." & "');", True)
        Else
            'Record updated with name
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "MyScript", "alert('" & txtTitle.Text & " has been updated" & "');", True)
        End If

        lblSignatureID.Text = ""
    End Sub

End Class