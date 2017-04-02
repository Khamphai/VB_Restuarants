Imports System.Data
Imports System.Data.SqlClient

Public Class frmLogin
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Conn As SqlConnection
        Dim Cmd As New SqlCommand

        Try
            Conn = getConnect()
            Conn.Open()
            Cmd = Conn.CreateCommand
            Cmd.CommandText = "SELECT emp_id, emp_name, emp_pwd, emp_level FROM employees WHERE emp_id='" & TextBox1.Text & "'AND emp_pwd='" & TextBox2.Text & "'"

            Dim da As New SqlDataAdapter(Cmd.CommandText, Conn)
            Dim dt As New DataTable("restuarant")
            da.Fill(dt)

            Dim dr As SqlDataReader = Cmd.ExecuteReader

            Try

                If dr.Read = False Then
                    MessageBox.Show("Authentication Failed...")
                Else
                    'MsgBox("jjg")
                    Dim i As Integer = dt.Rows.Count - 1
                    If dt.Rows(i)(3) = "Sale" Or dt.Rows(i)(3) = "Admin" Or dt.Rows(i)(3) = "Cashier" Then
                        'MsgBox("jjd")
                        If frmMain.formRember = 1 Then
                            Me.Close()
                            frmSale.Show()
                            frmSale.Label2.Text = dt.Rows(i)(0)
                            frmSale.Label3.Text = dt.Rows(i)(1)
                        ElseIf frmMain.formRember = 2 Then
                            Me.Close()
                            frmReserve.Show()
                            frmReserve.Label16.Text = dt.Rows(i)(0)
                            frmReserve.Label17.Text = dt.Rows(i)(1)
                        ElseIf frmMain.formRember = 3 Then
                            Me.Close()
                            frmInvoice.Show()
                            frmInvoice.Label3.Text = dt.Rows(i)(0)
                            frmInvoice.Label5.Text = dt.Rows(i)(1)
                        ElseIf frmMain.formRember = 4 Then
                            Me.Close()
                            frmRecieve.Show()
                            frmRecieve.Label3.Text = dt.Rows(i)(0)
                            frmRecieve.Label5.Text = dt.Rows(i)(1)
                        ElseIf frmMain.formRember = 5 Then
                            Me.Close()
                            frmOrder.Show()
                            frmOrder.Label4.Text = dt.Rows(i)(0)
                            frmOrder.Label6.Text = dt.Rows(i)(1)
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try

            Conn.Close()

        Catch ex As Exception
            MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
        End Try
    End Sub

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Close()
        'frmMain.Show()
        Me.Hide()
        frmMain.WindowState = FormWindowState.Normal
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()
    End Sub
End Class