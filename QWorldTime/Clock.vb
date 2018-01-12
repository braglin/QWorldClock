Public Class Clock
    Dim KeyName(4) As String
    Dim KeyValues(4) As String
    Public Country As String
    Dim itemcount As String = 0
    Public thisItem As String = 0

    Private Sub clockLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ShowInTaskbar = False
        Timer1.Enabled = True

        'Dim timezone = TimeZoneInfo.GetSystemTimeZones
        'For Each x As TimeZoneInfo In timezone
        '    RichTextBox1.Text += x.Id + vbNewLine + vbNewLine
        'Next
    End Sub

    Private Sub showMenu(sender As Object, e As EventArgs) Handles Button1.Click
        ContextMenuStrip1.Show(MousePosition.X, MousePosition.Y)
    End Sub

    Private Sub changeLocation(sender As Object, e As EventArgs) Handles ChangeToolStripMenuItem.Click
        Map.Show()
        Map.thisitem = thisItem
    End Sub

    Private Sub countTime(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label2.Text = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(Country)).DateTime.ToString("HH:mm:ss tt")
        Label3.Text = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(Country)).Month.ToString + "-" + TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(Country)).Day.ToString + "-" + TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(Country)).Year.ToString 'Format(Date.Today.Month, "00") & "-" & Format(Date.Today.Day, "00") & "-" & Date.Today.Year
    End Sub

    Private Sub closeClock(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Dim sSection As String
        Dim sKey As String
        Dim sFileName As String
        sSection = "Item" + thisItem.ToString
        sFileName = Application.StartupPath + "\qworldtime.clk"
        If Len(Trim(sKey)) <> 0 Then
            WritePrivateProfileString(sSection, sKey, vbNullString, sFileName)
        Else
            WritePrivateProfileString(sSection, sKey, vbNullString, sFileName)
        End If
        Me.Close()
    End Sub


#Region "Move Form and Resize (form and font)"
    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point
    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown, Label1.MouseDown, Label2.MouseDown, Label3.MouseDown
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.NoMove2D
            MoveForm_MousePosition = e.Location
        End If
    End Sub
    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove, Label1.MouseMove, Label2.MouseMove, Label3.MouseMove
        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub
    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp, Label1.MouseUp, Label2.MouseUp, Label3.MouseUp
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
            writeIni(Application.StartupPath + "\qworldtime.clk", "Item" + thisItem.ToString, "X", Me.Location.X)
            writeIni(Application.StartupPath + "\qworldtime.clk", "Item" + thisItem.ToString, "Y", Me.Location.Y)
        End If
    End Sub
#End Region
#Region "Write INI"
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
    Public Sub writeIni(ByVal iniFileName As String, ByVal Section As String, ByVal ParamName As String, ByVal ParamVal As String)
        Dim Result As Integer
        Result = WritePrivateProfileString(Section, ParamName, ParamVal, iniFileName)
    End Sub
#End Region
End Class
