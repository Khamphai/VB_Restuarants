Imports System.Data
Imports System.Data.SqlClient

Public Class frmDrink
    Dim tcondition As String

    Private Sub frmDrink_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call datagirdshow()
        Me.WindowState = FormWindowState.Maximized
        Panel1.Location = New System.Drawing.Point((Me.Width / 2) - (Panel1.Width / 2), (Me.Height / 2) - (Panel1.Height / 2))


    End Sub

    'Function Show Data in DataGridView
    Sub datagirdshow()
        Dim Conn As SqlConnection
        Dim Cmd As New SqlCommand

        Try
            Conn = getConnect()
            Conn.Open()
            Cmd = Conn.CreateCommand
            Cmd.CommandText = "Select * From drinks order by cast(right(drink_id,len(drink_id)-1) as int) asc"
            Dim da As New SqlDataAdapter(Cmd.CommandText, Conn)
            Dim dt As New DataTable("Restuarant")
            da.Fill(dt)
            DataGridView1.DataSource = dt
            Conn.Close()
            DataGridView1.Columns(0).HeaderText = "ລະຫັດເຄື່ອງດື່ມ"
            DataGridView1.Columns(1).HeaderText = "ຊື່ເຄື່ອງດື່ມ"
            DataGridView1.Columns(2).HeaderText = "ຈຳນວນ"
            DataGridView1.Columns(3).HeaderText = "ລາຄາ"
            DataGridView1.Columns(4).HeaderText = "ປະເພດ"

        Catch ex As Exception
            MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Conn As SqlConnection
        Dim Cmd As New SqlCommand
        Try
            Conn = getConnect()
            Conn.Open()
            Cmd = Conn.CreateCommand
            Cmd.CommandText = "select right(drink_id,len(drink_id)-1) from drinks order by cast(right(drink_id,len(drink_id)-1) as int) asc"
            Dim da As New SqlDataAdapter(Cmd.CommandText, Conn)
            Dim dt As New DataTable("restuarant")
            da.Fill(dt)
            Conn.Close()
            Dim i As Integer = dt.Rows.Count - 1
            TextBox1.Text = "D" & Val(dt.Rows(i)(0)) + 1
        Catch ex As Exception
            TextBox1.Text = "D1"
            'MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
        End Try

        tcondition = "New"
        If Button1.Text = "New" Then
            Button1.Text = "Cancel"
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            TextBox4.Enabled = True
            ComboBox1.Enabled = True
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            ComboBox1.Text = "------"
        Else
            Button1.Text = "New"
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            TextBox4.Enabled = False
            ComboBox1.Enabled = False
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            ComboBox1.Text = "------"
        End If

        Try
            Conn = getConnect()
            Conn.Open()
            Cmd = Conn.CreateCommand
            Cmd.CommandText = "select * from drinktype"
            Dim da As New SqlDataAdapter(Cmd.CommandText, Conn)
            Dim dt As New DataTable("restuarant")
            da.Fill(dt)
            Conn.Close()
            ComboBox1.Items.Clear()
            For i = 0 To dt.Rows.Count - 1 Step 1
                ComboBox1.Items.Add(dt.Rows(i)(1))
            Next

        Catch ex As Exception
            MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        tcondition = "Update"
        If TextBox1.Text.Length > 0 And Button2.Text = "Update" Then
            Button2.Text = "Cancel"
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            TextBox4.Enabled = True
            ComboBox1.Enabled = True
        Else
            Button2.Text = "Update"
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            TextBox4.Enabled = False
            ComboBox1.Enabled = False
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim conn As New SqlConnection
        Dim cmd As New SqlCommand
        If tcondition = "New" Then
            Try
                conn = getConnect()
                conn.Open()
                cmd = conn.CreateCommand
                cmd.CommandText = "INSERT INTO drinks(
                                                        [drink_ID],
                                                        [drink_name],
                                                        [drink_qtt],
                                                        [drink_price],
                                                        [drinktype_ID]
                                                        ) VALUES(
                                                        N'" & TextBox1.Text & "',
                                                        N'" & TextBox2.Text & "',
                                                        N'" & TextBox3.Text & "',
                                                        N'" & TextBox4.Text & "',
                                                        (select drinktype_id from drinktype where drinktype_name = N'" & ComboBox1.Text & "'))"
                Dim da As New SqlDataAdapter(cmd.CommandText, conn)
                Dim dt As New DataTable("ComputerShop")
                da.Fill(dt)
                DataGridView1.DataSource = dt
                conn.Close()
                Call datagirdshow()
                Call cllear()
            Catch ex As Exception
                MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
            End Try
        ElseIf tcondition = "Update" Then
            Try
                conn = getConnect()
                conn.Open()
                cmd = conn.CreateCommand
                cmd.CommandText = "UPDATE drinks SET 
                                    drink_name = N'" & TextBox2.Text & "',
                                    drink_qtt = N'" & TextBox3.Text & "',
                                    drink_price = N'" & TextBox4.Text & "',
                                    drinktype_id = N'" & ComboBox1.Text & "'
                                    where drink_ID = N'" & TextBox1.Text & "'"
                Dim da As New SqlDataAdapter(cmd.CommandText, conn)
                Dim dt As New DataTable("restuarant")
                da.Fill(dt)
                DataGridView1.DataSource = dt
                conn.Close()
                Call datagirdshow()
                Call cllear()
            Catch ex As Exception
                MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
            End Try
        End If
    End Sub


    Sub cllear()
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        ComboBox1.Enabled = False
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        Button2.Text = "Update"
        Button1.Text = "New"
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim Conn As SqlConnection
        Dim Cmd As New SqlCommand

        Try
            Conn = getConnect()
            Conn.Open()
            Cmd = Conn.CreateCommand
            Cmd.CommandText = "delete from drinks where drink_ID = '" & TextBox1.Text & "'"
            'If MessageBox.Show("ຕ້ອງການລຶບຂໍ້ມູນແທ້ບໍ່?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Dim da As New SqlDataAdapter(Cmd.CommandText, Conn)
                Dim dt As New DataTable("restuarant")
                da.Fill(dt)
                DataGridView1.DataSource = dt
            'End If
            Conn.Close()
            Call datagirdshow()
            Call cllear()
        Catch ex As Exception
            MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Close()
    End Sub

    Private Sub DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

        Try
            Dim cindex As Integer
            cindex = DataGridView1.CurrentRow.Index
            TextBox1.Text = DataGridView1.Item(0, cindex).Value.ToString()
            TextBox2.Text = DataGridView1.Item(1, cindex).Value.ToString()
            TextBox3.Text = DataGridView1.Item(2, cindex).Value.ToString()
            TextBox4.Text = DataGridView1.Item(3, cindex).Value.ToString()
            'ComboBox1.Text = DataGridView1.Item(4, cindex).Value.ToString()


            Dim Conn As SqlConnection
            Dim Cmd As New SqlCommand
            Conn = getConnect()
            Conn.Open()
            Cmd = Conn.CreateCommand
            Cmd.CommandText = "Select drinktype_name from drinktype where drinktype_id = '" & DataGridView1.Item(4, cindex).Value & "'"
            Dim da As New SqlDataAdapter(Cmd.CommandText, Conn)
            Dim dt As New DataTable("Restuarant")
            da.Fill(dt)
            ComboBox1.Text = dt.Rows(0)(0)

        Catch ex As Exception
            MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
        End Try
    End Sub
End Class