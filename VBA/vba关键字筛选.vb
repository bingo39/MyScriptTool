Sub FilterByKeywords()
    Dim ws As Worksheet
    Dim wsOutput As Worksheet
    Dim rng As Range
    Dim cell As Range
    Dim criteria As Variant
    Dim filteredData As Range
    Dim lastRow As Long
    Dim lastCol As Long
    Dim outputRow As Long
    Dim i As Long
    
    ' 设置工作表和范围
    Set ws = ThisWorkbook.Sheets("Sheet1") ' 更改为你需要筛选的工作表名称
    lastRow = ws.Cells(ws.Rows.Count, "C").End(xlUp).Row ' 获取数据的最后一行
    lastCol = ws.Cells(1, ws.Columns.Count).End(xlToLeft).Column ' 获取数据的最后一列
    Set rng = ws.Range(ws.Cells(1, 1), ws.Cells(lastRow, lastCol)) ' 设置数据范围，可以调整为实际数据范围
    
    ' 创建或清空Sheet2
    On Error Resume Next
    Set wsOutput = ThisWorkbook.Sheets("Sheet2")
    On Error GoTo 0
    
    If wsOutput Is Nothing Then
        Set wsOutput = ThisWorkbook.Sheets.Add(After:=ThisWorkbook.Sheets(ThisWorkbook.Sheets.Count))
        wsOutput.Name = "Sheet2"
    Else
        wsOutput.Cells.Clear ' 清空现有内容
    End If
    
    ' 设置关键字
    criteria = Array("职院", "职业学院", "学院", "高职", "职业技术学院")
    
    ' 清除已有筛选
    ws.AutoFilterMode = False
    
    ' 设置输出的起始行
    outputRow = 1
    wsOutput.Cells(outputRow, 1).Value = "筛选结果" ' 输出标题
    
    ' 将原表头复制到Sheet2
    ws.Rows(1).Copy Destination:=wsOutput.Rows(1)
    outputRow = outputRow + 1 ' 移动到下一行
    
    ' 使用高级筛选
    For i = LBound(criteria) To UBound(criteria)
        ' 应用筛选条件
        'Criteria1是筛选器，&符号是连接符；
            'ps：Criteria1 = *职院*
        rng.AutoFilter Field:=3, Criteria1:="*" & criteria(i) & "*", Operator:=xlOr
        
        ' 获取筛选后的数据范围
        On Error Resume Next
        Set filteredData = rng.SpecialCells(xlCellTypeVisible)
        On Error GoTo 0
        
        ' 如果有筛选结果，将整行数据复制到Sheet2
        If Not filteredData Is Nothing Then
            For Each cell In filteredData
                ' 仅复制整行数据（跳过标题行）
                If cell.Row > 1 Then
                    ws.Rows(cell.Row).Copy Destination:=wsOutput.Rows(outputRow)
                    outputRow = outputRow + 1 ' 更新输出行
                End If
            Next cell
        End If
    Next i
    
    ' 清除筛选
    ws.AutoFilterMode = False
    
    ' 提示用户
    MsgBox "筛选完成，结果已输出到 'Sheet2'！"
End Sub




'备注
    ' Range.Cells.FormulaLocal :获取到的数据集
    'Range.AutoFilter: 方法用于在指定的Range 对象上应用或清除自动筛选。它允许你根据特定条件筛选数据，只显示满足条件的行
        ' 参数说明:
        ' Field:指定要筛选的列的序号(从1开始)。
        ' Criteria1:第一个筛选条件。可以是文本、数字、日期或一个数组。
        ' Operator:指定筛选条件的操作符，例如 xlAnd, xlOr, xlTop10Items 等。
        ' Criteria2:第二个筛选条件，用于与 Criteria1 结合形成复合条件。
        ' SubField:用于指定要筛选的字段类型(例如，地理位置的“人口”字段或股票的“交易量”字段)。
        ' VisibleDropDown:一个布尔值，指定是否显示筛选下拉箭头。
