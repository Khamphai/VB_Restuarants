Public Class frmRecieve
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Close()
        'frmMain.Show()
        Me.Hide()
        frmMain.WindowState = FormWindowState.Normal
    End Sub

    Private Sub frmRecieve_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
    End Sub
End Class