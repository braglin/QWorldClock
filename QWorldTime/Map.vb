Public Class Map
    Public thisItem As String
    Dim KeyName(4) As String
    Dim KeyValues(4) As String

    Private Sub Map_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each ctl As Control In Me.GroupBox1.Controls
            AddHandler ctl.Click, AddressOf SelectCountry
        Next
        Key()
    End Sub

    Private Sub selectCountry(sender As Object, e As EventArgs)
        For Each x As Control In GroupBox1.Controls
            If Not x.Name.Contains("PictureBox") Then
                If Not sender.ToString.Contains(x.Text) Then
                    x.BackColor = SystemColors.Control
                Else
                    x.BackColor = Color.YellowGreen
                    Label13.Text = x.Text
                End If
            End If
        Next
    End Sub


#Region "INI"
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

    Private Sub save(sender As Object, e As EventArgs) Handles Button1.Click
        writeIni(Application.StartupPath + "\qworldtime.clk", "Item" + thisItem.ToString, "Country", Label13.Text)
        writeIni(Application.StartupPath + "\qworldtime.clk", "Item" + thisItem.ToString, "Title", TextBox1.Text)
        Application.Restart()
        Close()
    End Sub
End Class