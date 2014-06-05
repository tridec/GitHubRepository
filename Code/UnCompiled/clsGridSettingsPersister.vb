﻿Imports System
Imports System.Collections
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Telerik.Web
Imports Telerik.Web.UI


Public Class clsGridSettingsPersister

    Private gridInstance As RadGrid

    Public Sub New(ByVal gridInstance As RadGrid)
        MyBase.New()
        Me.gridInstance = gridInstance
    End Sub

    'this method should be called on Render
    Public Function SaveSettings() As String
        Dim gridSettings() As Object = New Object((7) - 1) {}
        'Save groupBy
        Dim groupByExpressions As GridGroupByExpressionCollection = gridInstance.MasterTableView.GroupByExpressions
        Dim groupExpressions() As Object = New Object((groupByExpressions.Count) - 1) {}
        Dim count As Integer = 0
        For Each expression As GridGroupByExpression In groupByExpressions
            groupExpressions(count) = CType(expression, IStateManager).SaveViewState
            count = (count + 1)
        Next
        gridSettings(0) = groupExpressions
        'Save sort expressions
        gridSettings(1) = CType(gridInstance.MasterTableView.SortExpressions, IStateManager).SaveViewState
        'Save columns order
        Dim columnsLength As Integer = (gridInstance.MasterTableView.Columns.Count + gridInstance.MasterTableView.AutoGeneratedColumns.Length)
        Dim columnOrder() As Pair = New Pair(columnsLength - 1) {}

        Dim allColumns As ArrayList = New ArrayList(columnsLength)
        allColumns.AddRange(gridInstance.MasterTableView.Columns)
        allColumns.AddRange(gridInstance.MasterTableView.AutoGeneratedColumns)
        Dim i As Integer = 0
        For Each column As GridColumn In allColumns
            Dim p As Pair = New Pair
            p.First = column.OrderIndex
            p.Second = column.HeaderStyle.Width
            columnOrder(i) = p
            i = (i + 1)
        Next
        gridSettings(2) = columnOrder
        'Save filter expression
        gridSettings(3) = CType(gridInstance.MasterTableView.FilterExpression, Object)

        'Save the current records per page
        gridSettings(4) = gridInstance.MasterTableView.PageSize

        'Save the current page index
        gridSettings(5) = gridInstance.MasterTableView.CurrentPageIndex


        Dim formatter As LosFormatter = New LosFormatter
        Dim writer As StringWriter = New StringWriter
        formatter.Serialize(writer, gridSettings)
        Return writer.ToString
    End Function

    'this method should be called on PageInit
    Public Sub LoadSettings(ByVal settings As String)
        Dim formatter As LosFormatter = New LosFormatter
        Dim reader As StringReader = New StringReader(settings)
        Dim gridSettings() As Object = CType(formatter.Deserialize(reader), Object())
        'Load groupBy
        Dim groupByExpressions As GridGroupByExpressionCollection = Me.gridInstance.MasterTableView.GroupByExpressions
        groupByExpressions.Clear()
        Dim groupExpressionsState() As Object = CType(gridSettings(0), Object())
        Dim count As Integer = 0
        For Each obj As Object In groupExpressionsState
            Dim expression As GridGroupByExpression = New GridGroupByExpression
            CType(expression, IStateManager).LoadViewState(obj)
            groupByExpressions.Add(expression)
            count = (count + 1)
        Next
        'Load sort expressions
        Me.gridInstance.MasterTableView.SortExpressions.Clear()
        CType(Me.gridInstance.MasterTableView.SortExpressions, IStateManager).LoadViewState(gridSettings(1))
        'Load columns order
        Dim columnsLength As Integer = (Me.gridInstance.MasterTableView.Columns.Count + Me.gridInstance.MasterTableView.AutoGeneratedColumns.Length)
        Dim columnOrder() As Pair = CType(gridSettings(2), Pair())
        If (columnsLength = columnOrder.Length) Then
            Dim allColumns As ArrayList = New ArrayList(columnsLength)
            allColumns.AddRange(Me.gridInstance.MasterTableView.Columns)
            allColumns.AddRange(Me.gridInstance.MasterTableView.AutoGeneratedColumns)
            Dim i As Integer = 0
            For Each column As GridColumn In allColumns
                column.OrderIndex = CType(columnOrder(i).First, Integer)
                column.HeaderStyle.Width = CType(columnOrder(i).Second, Unit)
                i = (i + 1)
            Next
        End If
        'Load filter expression
        Me.gridInstance.MasterTableView.FilterExpression = CType(gridSettings(3), String)

        'Load Page Size
        Me.gridInstance.MasterTableView.PageSize = CType(gridSettings(4), Integer)

        'Load Current Page Index
        Me.gridInstance.MasterTableView.CurrentPageIndex = CType(gridSettings(5), Integer)

    End Sub
    Public Function SaveFilterText(ByVal e As Telerik.Web.UI.GridCommandEventArgs) As Object
        Dim arrFilterSettings(Me.gridInstance.MasterTableView.RenderColumns.Length - 2)() As String
        Dim i As Integer = 0

        Dim filterPair As Pair = DirectCast(e.CommandArgument, Pair)

        If Not filterPair.First = "NoFilter" Then
            For Each filteritem As GridFilteringItem In Me.gridInstance.MasterTableView.GetItems(GridItemType.FilteringItem)
                For Each col As GridColumn In Me.gridInstance.MasterTableView.RenderColumns
                    Try
                        Dim txtbx As TextBox = DirectCast(filteritem(col.UniqueName).Controls(0), TextBox)
                        Dim strtxt As String = txtbx.Text.ToString()
                        arrFilterSettings(i) = New String() {col.UniqueName, strtxt}
                        i += 1
                    Catch ex As Exception

                    End Try
                Next
            Next
        End If

        Return arrFilterSettings
    End Function

    Public Sub LoadFilterText(ByVal objfilterSettings As Object)
        Try
            'Put Filter text back in
            If Not objfilterSettings Is Nothing Then
                Dim arrFilterSettings(Me.gridInstance.MasterTableView.RenderColumns.Length - 2)() As String
                arrFilterSettings = objfilterSettings

                Dim i As Integer = 0
                For Each filteritem As GridFilteringItem In Me.gridInstance.MasterTableView.GetItems(GridItemType.FilteringItem)
                    For Each col As GridColumn In Me.gridInstance.MasterTableView.RenderColumns
                        i = 0
                        While (i < arrFilterSettings.Length - 1)
                            If (arrFilterSettings(i) IsNot Nothing) Then
                                If arrFilterSettings(i)(0).ToString = col.UniqueName Then
                                    Dim txtbx As TextBox = DirectCast(filteritem(col.UniqueName).Controls(0), TextBox)
                                    txtbx.Text = arrFilterSettings(i)(1).ToString
                                End If

                            End If
                            i += 1
                        End While
                    Next
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class

