
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Public Class clsNodeInfo
    Inherits System.Web.UI.Page

    Private strNodeName As String
    Private intNodeId As Integer = 0
    Private intParentId As Integer = 0
    

    Public Property NodeId()
        Get
            Return intNodeId
        End Get
        Set(ByVal Value)
            intNodeId = Value
        End Set
    End Property

    Public Property ParentId()
        Get
            Return intParentId
        End Get
        Set(ByVal Value)
            intParentId = Value
        End Set
    End Property
    
    Public ReadOnly Property NodeName()
        Get
            Return strNodeName
        End Get
    End Property

    Public Sub GetNode(ByVal NodeId As Integer)
        Me.NodeId = NodeId
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "ControlTreeNodeSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@NodeId", SqlDbType.Int, NodeId)

        Dim objDR As SqlDataReader
        objDR = db.ExecuteReader(objCommand)

        If objDR.HasRows Then
            objDR.Read()
            strNodeName = objDR("NodeName")
            intParentId = objDR("ParentID")
        End If
        objDR.Close()
    End Sub

End Class
