
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary
Imports System.Data.Common



Public Class clsCheckStatus
    Private intIdeaStatusId As Integer
    Private intPhase As Integer
    Private strIdeaStatus As String

    Public Property IdeaStatusId()
        Get
            Return intIdeaStatusId
        End Get
        Set(ByVal Value)
            intIdeaStatusId = Value
        End Set
    End Property
    Public Property IdeaStatus()
        Get
            Return strIdeaStatus
        End Get
        Set(ByVal Value)
            strIdeaStatus = Value
        End Set
    End Property
    Public Property Phase()
        Get
            Return intPhase
        End Get
        Set(ByVal Value)
            intPhase = Value
        End Set
    End Property

    Public Function CheckSupplierIdeaStatus(ByVal intIdeaApplicationId As Integer) As Integer

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "IdeaReviewStatusSupplierSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@IdeaApplicationId", DbType.Int32, intIdeaApplicationId)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)

        While datareader.Read
            IdeaStatusId = datareader("IdeaStatusID")
            IdeaStatus = datareader("IdeaStatusName")
        End While
        datareader.Close()

        Return IdeaStatusId
    End Function

    Public Function CheckAdminIdeaStatus(ByVal intIdeaApplicationId As Integer) As Integer

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "IdeaReviewStatusSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        db.AddInParameter(objCommand, "@IdeaApplicationId", DbType.Int32, intIdeaApplicationId)
        Dim datareader As SqlDataReader = db.ExecuteReader(objCommand)

        While datareader.Read
            IdeaStatusId = datareader("IdeaStatusID")
            IdeaStatus = datareader("IdeaStatusName")
            'set the Phase too
            If IdeaStatusId <= 4 Then
                Phase = 1
            End If
            If IdeaStatusId >= 5 Then
                Phase = 2
            End If
        End While
        datareader.Close()

        Return IdeaStatusId
    End Function

    Public Function DeterminePhase(ByVal intIdeaApplicationId As Integer) As Integer

        Dim intStatus As Integer
        intStatus = CheckAdminIdeaStatus(intIdeaApplicationId)

        If intStatus <= 4 Then
            intPhase = 1
        End If

        If intStatus >= 5 Then
            intPhase = 2
        End If

        Return intPhase
    End Function

End Class

