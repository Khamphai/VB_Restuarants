Imports System.Data
Imports System.Data.SqlClient

Public Class frmOrder
    Dim tcondition As String
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Close()
        'frmMain.Show()
        Me.Hide()
        frmMain.WindowState = FormWindowState.Normal
    End Sub

    Private Sub ComboBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedValueChanged
        Dim Conn As SqlConnection
        Dim Cmd As New SqlCommand
        Try

            Conn = getConnect()
            Conn.Open()
            Cmd = Conn.CreateCommand
            If ComboBox1.Text = "ເຄື່ອງດື່ມ" Then
                Cmd.CommandText = "SELECT * FROM drinks"
            ElseIf ComboBox1.Text = "ເຄື່ອງປຸງ" Then
                tcondition = "A"
                Cmd.CommandText = "SELECT * FROM ingredients"
            ElseIf ComboBox1.Text = "ວັດຖຸດິບ" Then
                tcondition = "B"
                Cmd.CommandText = "SELECT * FROM materials"
            End If

            Dim da As New SqlDataAdapter(Cmd.CommandText, Conn)
            Dim dt As New DataTable("restuarant")
            da.Fill(dt)
            Conn.Close()
            ComboBox2.Items.Clear()
            For i = 0 To dt.Rows.Count - 1 Step 1
                ComboBox2.Items.Add(dt.Rows(i)(1))
            Next

        Catch ex As Exception
            MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
        End Try
    End Sub


    Private Sub frmOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        TextBox2.Text = Today()
        TextBox3.Text = TimeOfDay()

        '------------------------
        'ລະຫັດການຂາຍ
        '------------------------
        Dim Conn As SqlConnection
        Dim Cmd As New SqlCommand
        Try
            Conn = getConnect()
            Conn.Open()
            Cmd = Conn.CreateCommand
            Cmd.CommandText = "select right(order_id,len(order_id)-2) from Orders order by cast(right(order_id,len(order_id)-2) as int) asc"
            Dim da As New SqlDataAdapter(Cmd.CommandText, Conn)
            Dim dt As New DataTable("restuarant")
            da.Fill(dt)
            Conn.Close()
            Dim i As Integer = dt.Rows.Count - 1
            TextBox1.Text = "OR" & Val(dt.Rows(i)(0)) + 1
        Catch ex As Exception
            TextBox1.Text = "OR1"
            'MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
        End Try

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        TextBox3.Text = TimeOfDay()
    End Sub

    'ເພີ້ມ
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If TextBox4.TextLength > 0 Then
                Dim a As Integer = 0
                Dim b As Integer

                For i = 0 To DataGridView1.RowCount - 1 Step 1
                    If ComboBox2.Text = DataGridView1.Item(0, i).Value Then
                        a = 1
                        b = i
                    End If
                Next
                If a = 1 Then
                    DataGridView1.Item(1, b).Value = Val(DataGridView1.Item(1, b).Value) + Val(TextBox4.Text)
                Else
                    DataGridView1.Rows.Add(New String() {ComboBox2.Text, TextBox4.Text, ComboBox3.Text})
                End If
                TextBox4.Clear()
            Else
                MsgBox("ກະລຸນາປ້ອນຈຳນວນ")
            End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            DataGridView1.Rows.Remove(DataGridView1.CurrentRow)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim conn As New SqlConnection
        Dim cmd As New SqlCommand

        If DataGridView1.RowCount > 1 Then

            For i = 0 To DataGridView1.RowCount - 2 Step 1
                Try
                    conn = getConnect()
                    conn.Open()
                    cmd = conn.CreateCommand
                    cmd.CommandText = "INSERT INTO Orders VALUES(
                                                            '" & TextBox1.Text & "',
                                                            '" & TextBox2.Text & "',
                                                            '" & TextBox3.Text & "',
                                                            N'" & Label4.Text & "',
                                                            N'" & DataGridView1.Rows(i).Cells(0).Value & "',
                                                            N'" & DataGridView1.Rows(i).Cells(1).Value & "',
                                                            N'" & DataGridView1.Rows(i).Cells(2).Value & "')"
                    Dim da As New SqlDataAdapter(cmd.CommandText, conn)
                    Dim dt As New DataTable("ComputerShop")
                    da.Fill(dt)
                    conn.Close()

                Catch ex As Exception

                End Try
            Next
            DataGridView1.Rows.Clear()
            MsgBox("Data Were Saved!")
        Else
            MsgBox("Please Enter Data!!!")
        End If

    End Sub
End Class