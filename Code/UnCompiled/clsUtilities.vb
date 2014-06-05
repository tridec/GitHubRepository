Imports System.ComponentModel

Public Class clsUtilities
    Function GetFizeSize(ByVal filesize As Double) As String
        Dim strSize As String = ""

        If filesize > 1048576 Then
            filesize = filesize / 1048576
            filesize = Math.Round(filesize, 2)
            strSize = filesize.ToString + " MB"
        ElseIf filesize > 1024 Then
            filesize = filesize / 1024
            filesize = Math.Round(filesize, 2)
            strSize = filesize.ToString + " KB"
        End If

        Return strSize
    End Function

    Function getCurrentFiscalYear() As Integer
        Dim dtNow As DateTime = DateTime.Now
        Dim intFiscalYear As Integer = dtNow.Year
        Dim intMonth As Integer = Convert.ToInt32(dtNow.Month.ToString)

        If (intMonth >= 9 And intMonth <= 12) Then
            intFiscalYear += 1
        End If
        Return intFiscalYear
    End Function

    Public Shared Function ConvertTo(Of T)(list As IList(Of T)) As DataTable
        Dim table As DataTable = CreateTable(Of T)()
        Dim entityType As Type = GetType(T)
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(entityType)

        For Each item As T In list
            Dim row As DataRow = table.NewRow()

            For Each prop As PropertyDescriptor In properties
                row(prop.Name) = If(prop.GetValue(item), DBNull.Value)
            Next

            table.Rows.Add(row)
        Next

        Return table
    End Function

    Public Shared Function CreateTable(Of T)() As DataTable
        Dim entityType As Type = GetType(T)
        Dim table As New DataTable(entityType.Name)
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(entityType)

        For Each prop As PropertyDescriptor In properties
            table.Columns.Add(prop.Name, If(Nullable.GetUnderlyingType(prop.PropertyType), prop.PropertyType))
        Next

        Return table
    End Function
End Class
