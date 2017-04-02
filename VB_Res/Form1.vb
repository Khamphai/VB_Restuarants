Imports System.Data
Imports System.Data.SqlClient


Public Class Form1
    Dim tcondition As String

    'Function Form Load program
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            Cmd.CommandText = "Select * From employees"
            Dim da As New SqlDataAdapter(Cmd.CommandText, Conn)
            Dim dt As New DataTable("Restuarant")
            da.Fill(dt)
            DataGridView1.DataSource = dt
            Conn.Close()
            DataGridView1.Columns(0).HeaderText = "ລະຫັດພະນັກງານ"
            DataGridView1.Columns(1).HeaderText = "ຊື່ພະນັກງານ"
            DataGridView1.Columns(2).HeaderText = "ນາມສະກຸນ"
            DataGridView1.Columns(3).HeaderText = "ເບີໂທລະສັບ"
            DataGridView1.Columns(4).HeaderText = "ບ້ານ"
            DataGridView1.Columns(5).HeaderText = "ເມືອງ"
            DataGridView1.Columns(6).HeaderText = "ແຂວງ"
            DataGridView1.Columns(7).HeaderText = "ລະຫັດຜ່ານ"
            DataGridView1.Columns(8).HeaderText = "ລະດັບການນຳໃຊ້"

        Catch ex As Exception
            MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
        End Try
    End Sub


    'Function Close Program
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Close()
    End Sub

    'Function Button New
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim Conn As SqlConnection
        Dim Cmd As New SqlCommand
        Try
            Conn = getConnect()
            Conn.Open()
            Cmd = Conn.CreateCommand
            Cmd.CommandText = "select right(emp_id,len(emp_id)-3) from employees order by cast(right(emp_id,len(emp_id)-3) as int) asc"
            Dim da As New SqlDataAdapter(Cmd.CommandText, Conn)
            Dim dt As New DataTable("restuarant")
            da.Fill(dt)
            Conn.Close()
            Dim i As Integer = dt.Rows.Count - 1
            TextBox1.Text = "Emp" & Val(dt.Rows(i)(0)) + 1
        Catch ex As Exception
            TextBox1.Text = "Emp1"
            'MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
        End Try

        tcondition = "New"
        If Button1.Text = "New" Then
            Button1.Text = "Cancel"
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            TextBox4.Enabled = True
            TextBox5.Enabled = True
            TextBox6.Enabled = True
            TextBox7.Enabled = True
            TextBox8.Enabled = True
            ComboBox1.Enabled = True
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox7.Clear()
            TextBox8.Clear()

        Else
            Button1.Text = "New"
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            TextBox4.Enabled = False
            TextBox5.Enabled = False
            TextBox6.Enabled = False
            TextBox7.Enabled = False
            TextBox8.Enabled = False
            ComboBox1.Enabled = False
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox7.Clear()
            TextBox8.Clear()
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
                cmd.CommandText = "INSERT INTO employees(
                                                [emp_ID],
                                                [emp_name],
                                                [emp_surname],
                                                [emp_tells],
                                                [emp_villages],
                                                [emp_districts],
                                                [emp_provinces],
                                                [emp_pwd],
                                                [emp_level]
                                                ) VALUES(
                                                N'" & TextBox1.Text & "',
                                                N'" & TextBox2.Text & "',
                                                N'" & TextBox3.Text & "',
                                                N'" & TextBox4.Text & "',
                                                N'" & TextBox5.Text & "',
                                                N'" & TextBox6.Text & "',
                                                N'" & TextBox7.Text & "',
                                                N'" & TextBox8.Text & "',
                                                N'" & ComboBox1.Text & "')"

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
                cmd.CommandText = "UPDATE employees SET 
                                                        emp_name = N'" & TextBox2.Text & "', 
                                                        emp_surname = N'" & TextBox3.Text & "', 
                                                        emp_tells = N'" & TextBox4.Text & "', 
                                                        emp_villages = N'" & TextBox5.Text & "', 
                                                        emp_districts = N'" & TextBox6.Text & "', 
                                                        emp_provinces = N'" & TextBox7.Text & "',
                                                        emp_pwd = N'" & TextBox8.Text & "',
                                                        emp_level = N'" & ComboBox1.Text & "'
                                                        where emp_ID= N'" & TextBox1.Text & "'"
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

    'Function clear Data form textbox
    Sub cllear()
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox6.Enabled = False
        TextBox7.Enabled = False
        TextBox8.Enabled = False
        ComboBox1.Enabled = False
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        Button1.Text = "New"
        Button2.Text = "Update"
    End Sub

    'Function Click Show Data in textBox
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        Try
            Dim cindex As Integer
            cindex = DataGridView1.CurrentRow.Index
            TextBox1.Text = DataGridView1.Item(0, cindex).Value
            TextBox2.Text = DataGridView1.Item(1, cindex).Value
            TextBox3.Text = DataGridView1.Item(2, cindex).Value
            TextBox4.Text = DataGridView1.Item(3, cindex).Value
            TextBox5.Text = DataGridView1.Item(4, cindex).Value
            TextBox6.Text = DataGridView1.Item(5, cindex).Value
            TextBox7.Text = DataGridView1.Item(6, cindex).Value
            TextBox8.Text = DataGridView1.Item(7, cindex).Value
            ComboBox1.Text = DataGridView1.Item(8, cindex).Value
        Catch ex As Exception

        End Try

    End Sub

    'Function Update
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        tcondition = "Update"
        If TextBox1.Text.Length > 0 And Button2.Text = "Update" Then
            Button2.Text = "Cancel"
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            TextBox4.Enabled = True
            TextBox5.Enabled = True
            TextBox6.Enabled = True
            TextBox7.Enabled = True
            TextBox8.Enabled = True
            ComboBox1.Enabled = True

        Else
            Button2.Text = "Update"
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            TextBox4.Enabled = False
            TextBox5.Enabled = False
            TextBox6.Enabled = False
            TextBox7.Enabled = False
            TextBox8.Enabled = False
            ComboBox1.Enabled = False
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox7.Clear()
            TextBox8.Clear()

        End If

    End Sub

    'Function Delete Data
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim Conn As SqlConnection
        Dim Cmd As New SqlCommand

        Try
            Conn = getConnect()
            Conn.Open()
            Cmd = Conn.CreateCommand
            Cmd.CommandText = "delete from employees where emp_ID = '" & TextBox1.Text & "'"
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

End Class
