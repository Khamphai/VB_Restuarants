Public Class frmReserve

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Close()
        'frmMain.Show()
        Me.Hide()
        frmMain.WindowState = FormWindowState.Normal
    End Sub

    Private Sub frmReserve_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
    End Sub
End Class