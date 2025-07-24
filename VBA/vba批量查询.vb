VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "Sheet1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = True
Private Sub CommandButton1_Click()
Dim 表1行, 表2行, 行, 列 As Integer
Dim 矢量 As Boolean
'清理表3数据
Sheet3.Range("A2:C200").Clear
行 = 2

'筛选条件查询
For 表1行 = 2 To Sheet1.Rows.Count
矢量 = False
'查询区域循环
    For 表2行 = 2 To Sheet2.Rows.Count
        If Sheet1.Cells(表1行, "B") = Sheet2.Cells(表2行, "B") Then
        矢量 = True
        Exit For
        End If
    If Cells(表2行, "B") = "" Then 表2行 = Rows.Count
    Next 表2行

'输入区域循环
    If 矢量 = False Then
        For 列 = 1 To 3
        Sheet3.Cells(行, 列) = Sheet1.Cells(表1行, 列)
        Next 列
        
    Else
        行 = 行 - 1
    End If
行 = 行 + 1
If Cells(表1行, "B") = "" Then 表1行 = Rows.Count
Next 表1行
End Sub
