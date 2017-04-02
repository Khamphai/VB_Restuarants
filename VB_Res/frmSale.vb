Imports System.Data
Imports System.Data.SqlClient

Public Class frmSale

    Private Sub frmSale_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Label8.Text = Today()
        Label17.Text = TimeOfDay()

        '------------------------
        'ລະຫັດການຂາຍ
        '------------------------
        Dim Conn As SqlConnection
        Dim Cmd As New SqlCommand
        Try
            Conn = getConnect()
            Conn.Open()
            Cmd = Conn.CreateCommand
            Cmd.CommandText = "select right(sale_id,len(sale_id)-1) from sales order by cast(right(sale_id,len(sale_id)-1) as int) asc"
            Dim da As New SqlDataAdapter(Cmd.CommandText, Conn)
            Dim dt As New DataTable("restuarant")
            da.Fill(dt)
            Conn.Close()
            Dim i As Integer = dt.Rows.Count - 1
            Label5.Text = "S" & Val(dt.Rows(i)(0)) + 1
        Catch ex As Exception
            Label5.Text = "S1"
            'MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
        End Try

        '---------------------------
        'ລະຫັດລາຍລະອຽດການຂາຍ
        '---------------------------
        Try
            Conn = getConnect()
            Conn.Open()
            Cmd = Conn.CreateCommand
            Cmd.CommandText = "select right(sale_de_id,len(sale_de_id)-2) from sale_detail order by cast(right(sale_de_id,len(sale_de_id)-2) as int) asc"
            Dim da As New SqlDataAdapter(Cmd.CommandText, Conn)
            Dim dt As New DataTable("restuarant")
            da.Fill(dt)
            Conn.Close()
            Dim i As Integer = dt.Rows.Count - 1
            Label19.Text = "SD" & Val(dt.Rows(i)(0)) + 1
        Catch ex As Exception
            Label19.Text = "SD1"
            'MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        'frmMain.Show()
        frmMain.WindowState = FormWindowState.Normal
    End Sub

    Private Sub ComboBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedValueChanged

        Dim Conn As SqlConnection
        Dim Cmd As New SqlCommand
        Try
            Conn = getConnect()
            Conn.Open()
            Cmd = Conn.CreateCommand
            If ComboBox1.Text = "ອາຫານ" Then
                Cmd.CommandText = "SELECT * FROM foodtype"
            ElseIf ComboBox1.Text = "ເຄື່ອງດື່ມ" Then
                Cmd.CommandText = "SELECT * FROM drinktype"
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

    Private Sub ComboBox2_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedValueChanged

        Dim Conn As SqlConnection
        Dim Cmd As New SqlCommand
        Try
            Conn = getConnect()
            Conn.Open()
            Cmd = Conn.CreateCommand
            If ComboBox1.Text = "ອາຫານ" Then
                Cmd.CommandText = "SELECT * FROM foods WHERE foodtype_id=(SELECT foodtype_id FROM foodtype WHERE foodtype_name=N'" & ComboBox2.Text & "')"
            ElseIf ComboBox1.Text = "ເຄື່ອງດື່ມ" Then
                Cmd.CommandText = "SELECT * FROM drinks WHERE drinktype_id=(SELECT drinktype_id FROM drinktype WHERE drinktype_name=N'" & ComboBox2.Text & "')"
            End If

            Dim da As New SqlDataAdapter(Cmd.CommandText, Conn)
            Dim dt As New DataTable("restuarant")
            da.Fill(dt)
            Conn.Close()
            ComboBox3.Items.Clear()
            For i = 0 To dt.Rows.Count - 1 Step 1
                ComboBox3.Items.Add(dt.Rows(i)(1))
            Next

        Catch ex As Exception
            MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
        End Try

    End Sub

    Private Sub ComboBox3_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedValueChanged
        Dim Conn As SqlConnection
        Dim Cmd As New SqlCommand
        Try
            Conn = getConnect()
            Conn.Open()
            Cmd = Conn.CreateCommand
            If ComboBox1.Text = "ອາຫານ" Then
                Cmd.CommandText = "SELECT * FROM foods WHERE food_name=N'" & ComboBox3.Text & "'"
            ElseIf ComboBox1.Text = "ເຄື່ອງດື່ມ" Then
                Cmd.CommandText = "SELECT * FROM drinks WHERE drink_name=N'" & ComboBox3.Text & "'"
            End If

            Dim da As New SqlDataAdapter(Cmd.CommandText, Conn)
            Dim dt As New DataTable("restuarant")
            da.Fill(dt)
            Conn.Close()

            Dim i As Integer = dt.Rows.Count - 1
            TextBox2.Text = dt.Rows(i)(3)
            'TextBox3.Text = dt.Rows(i)(2)

        Catch ex As Exception
            MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label17.Text = TimeOfDay()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.TextLength > 0 Then
            If TextBox2.TextLength > 0 And TextBox3.TextLength > 0 Then
                DataGridView1.Rows.Add(New String() {ComboBox3.Text, TextBox2.Text, TextBox3.Text, Val(TextBox2.Text) * Val(TextBox3.Text), ComboBox2.Text})
                TextBox2.Clear()
                TextBox3.Clear()
            Else
                MsgBox("ກະລຸນາປ້ອນຈຳນວນສິນຄ້າ")
            End If
        Else
            MsgBox("ກະລຸນາປ້ອນເລກໂຕະ")
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

            Try
                conn = getConnect()
                conn.Open()
                cmd = conn.CreateCommand
                cmd.CommandText = "INSERT INTO sales(
                                                [sale_ID],
                                                [sale_de_ID],
                                                [table_ID],
                                                [cus_ID],
                                                [Emp_ID],
                                                [Cur_ID],
                                                [Res_ID],
                                                [Dates],
                                                [Times],
                                                [Paid_Status]
                                                ) VALUES(
                                                '" & Label5.Text & "',
                                                '" & Label19.Text & "',
                                                '" & TextBox1.Text & "',
                                                NULL,
                                                '" & Label2.Text & "',
                                                NULL, 
                                                NULL,
                                                '" & Label8.Text & "',
                                                '" & Label17.Text & "',
                                                'NO')"

                Dim da As New SqlDataAdapter(cmd.CommandText, conn)
                Dim dt As New DataTable("ComputerShop")
                da.Fill(dt)
                conn.Close()

            Catch ex As Exception
                'MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
            End Try


            For i = 0 To DataGridView1.RowCount - 1 Step 1

                Try
                    conn = getConnect()
                    conn.Open()
                    cmd = conn.CreateCommand
                    cmd.CommandText = "INSERT INTO sale_detail(
                                                            [sale_de_id],
                                                            [description],
                                                            [sale_qtt],
                                                            [Des_Price],
                                                            [amount],
                                                            [table_no],
                                                            [types])
                                                            VALUES(
                                                            '" & Label19.Text & "',
                                                            N'" & DataGridView1.Rows(i).Cells(0).Value & "',
                                                            '" & DataGridView1.Rows(i).Cells(2).Value & "',
                                                            '" & DataGridView1.Rows(i).Cells(1).Value & "',
                                                            '" & DataGridView1.Rows(i).Cells(3).Value & "',                                                           
                                                            '" & TextBox1.Text & "',
                                                            N'" & DataGridView1.Rows(i).Cells(4).Value & "')"
                    Dim da As New SqlDataAdapter(cmd.CommandText, conn)
                    Dim dt As New DataTable("restuarant")
                    da.Fill(dt)
                    conn.Close()

                Catch ex As Exception
                    'MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
                End Try
            Next
            DataGridView1.Rows.Clear()
            MsgBox("Data Were Saved!")
        Else
            MsgBox("Please Enter Data!!!")
        End If


    End Sub

End Class