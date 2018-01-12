Public Class Main
    Dim KeyName(4) As String
    Dim KeyValues(4) As String
    Public Country As String
    Dim itemcount As String = 0

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ShowInTaskbar = False
        Me.Width = 306
        Me.Height = 74
        Me.Hide()
        Key()

        For Each Line In (System.IO.File.ReadLines(Application.StartupPath + "\qworldtime.clk"))
            If Line.StartsWith("[Item") Then
                KeyVClear()
                Dim item As String = Replace(Line, "[", "")
                item = Replace(item, "]", "")
                itemcount = Replace(item, "Item", "")
                '  MsgBox("item: " + item + vbNewLine + "count: " + itemcount)
                If Line.Contains("[Item" + itemcount.ToString) Then
                    ReadINIFile(Application.StartupPath + "\qworldtime.clk", item, KeyName, KeyValues)
                    '
                    If KeyValues(3) <> "" Then
                        Try
                            Dim NewClock As New Clock
                            NewClock.thisitem = itemcount
                            NewClock.Country = KeyValues(3)
                            NewClock.Label1.Text = KeyValues(4)
                            NewClock.Show()
                            NewClock.Label1.Text = KeyValues(4)
                            If (KeyValues(1) <> "") Then
                                NewClock.Location = New Point(KeyValues(1), KeyValues(2))
                            Else
                                NewClock.Location = New Point((Screen.PrimaryScreen.Bounds.Width - (Me.Width / 2)), (Screen.PrimaryScreen.Bounds.Height - (Me.Height)))
                            End If
                            ' MsgBox(KeyValues(6))
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                    End If
                    itemcount += 1
                End If
            End If
        Next
    End Sub

    'Notify Icon
    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick
        ContextMenuStrip1.Show(MousePosition.X, MousePosition.Y)
    End Sub
    'Exit
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        Try
            Dim NewClock As New Clock
            NewClock.thisitem = itemcount
            NewClock.Country = "Central Brazilian Standard Time"
            NewClock.Label1.Text = "Brazil"
            NewClock.Show()

            If (KeyValues(1) <> "") Then
                NewClock.Location = New Point(KeyValues(1), KeyValues(2))
            Else
                NewClock.Location = New Point((Screen.PrimaryScreen.Bounds.Width - (Me.Width / 2)), (Screen.PrimaryScreen.Bounds.Height - (Me.Height)))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        itemcount += 1
    End Sub


#Region "INI"
    Private Declare Unicode Function GetPrivateProfileString Lib "kernel32" _
Alias "GetPrivateProfileStringW" (ByVal lpApplicationName As String, _
ByVal lpKeyName As String, ByVal lpDefault As String, _
ByVal lpReturnedString As String, ByVal nSize As Int32, _
ByVal lpFileName As String) As Int32
    Private Declare Unicode Function WritePrivateProfileString Lib "kernel32" _
Alias "WritePrivateProfileStringW" (ByVal lpApplicationName As String, _
ByVal lpKeyName As String, ByVal lpString As String, _
ByVal lpFileName As String) As Int32

    Public Sub Key()
        KeyName(1) = "X"
        KeyName(2) = "Y"
        KeyName(3) = "Country"
        KeyName(4) = "Title"
    End Sub
    Public Sub KeyVClear()
        KeyValues(1) = ""
        KeyValues(2) = ""
        KeyValues(3) = ""
        KeyValues(4) = ""
    End Sub

    Public Overloads Sub ReadINIFile(ByVal INIPath As String, _
 ByVal SectionName As String, ByVal KeyName As String(), _
 ByRef KeyValue As String())
        Dim Length As Integer
        Dim strData As String
        strData = Space$(10000)
        For i As Integer = 1 To KeyName.Length - 1
            If KeyName(i) <> "" Then
                'This will read the ini file using Section Name and Key 
                Length = GetPrivateProfileString(SectionName, KeyName(i), KeyValue(i), _
                strData, strData.Length, LTrim(RTrim((INIPath))))
                If Length > 0 Then
                    KeyValue(i) = strData.Substring(0, Length)
                Else
                    KeyValue(i) = ""
                End If
            End If
        Next
    End Sub

    Public Sub writeIni(ByVal iniFileName As String, ByVal Section As String, ByVal ParamName As String, ByVal ParamVal As String)
        Dim Result As Integer
        Result = WritePrivateProfileString(Section, ParamName, ParamVal, iniFileName)
    End Sub
#End Region
End Class