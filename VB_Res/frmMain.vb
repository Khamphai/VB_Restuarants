Public Class frmMain
    Public formRember As Integer
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Close()
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Panel1.Location = New System.Drawing.Point((Me.Width / 2) - (Panel1.Width / 2), (Me.Height / 2) - (Panel1.Height / 2))
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        formRember = 1
        frmLogin.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        formRember = 5
        frmLogin.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        formRember = 2
        frmLogin.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        formRember = 3
        frmLogin.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        formRember = 4
        frmLogin.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

    End Sub
End Class