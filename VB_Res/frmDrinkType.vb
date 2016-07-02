Imports System.Data
Imports System.Data.SqlClient

Public Class frmDrinkType
    Dim tcondition As String

    Private Sub frmDrinkType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            Cmd.CommandText = "Select * From DrinkType"
            Dim da As New SqlDataAdapter(Cmd.CommandText, Conn)
            Dim dt As New DataTable("bababa")
            da.Fill(dt)
            DataGridView1.DataSource = dt
            Conn.Close()
            DataGridView1.Columns(0).HeaderText = "ລະຫັດປະເພດເຄື່ອງດື່ມ"
            DataGridView1.Columns(1).HeaderText = "ຊື່ປະເພດເຄື່ອງດື່ມ"

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
            Cmd.CommandText = "select right(drinktype_id,len(drinktype_id)-2) from drinktype order by cast(right(drinktype_id,len(drinktype_id)-3) as int) asc"
            Dim da As New SqlDataAdapter(Cmd.CommandText, Conn)
            Dim dt As New DataTable("restuarant")
            da.Fill(dt)
            Conn.Close()
            Dim i As Integer = dt.Rows.Count - 1
            TextBox1.Text = "DT" & Val(dt.Rows(i)(0)) + 1
        Catch ex As Exception
            TextBox1.Text = "DT1"
            'MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
        End Try

        tcondition = "New"
        If Button1.Text = "New" Then
            Button1.Text = "Cancel"
            TextBox2.Enabled = True
            TextBox2.Clear()
        Else
            Button1.Text = "New"
            TextBox2.Enabled = False
            TextBox1.Clear()
            TextBox2.Clear()
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
                cmd.CommandText = "INSERT INTO drinktype([drinktype_ID],[drinktype_name]) VALUES(N'" & TextBox1.Text & "',N'" & TextBox2.Text & "')"

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
                cmd.CommandText = "UPDATE drinktype SET drinktype_name = N'" & TextBox2.Text & "' where drinktype_ID= N'" & TextBox1.Text & "'"
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
        TextBox1.Clear()
        TextBox2.Clear()
        Button1.Text = "New"
        Button2.Text = "Update"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        tcondition = "Update"
        If TextBox1.Text.Length > 0 And Button2.Text = "Update" Then
            Button2.Text = "Cancel"
            TextBox2.Enabled = True
        Else
            Button2.Text = "Update"
            TextBox2.Enabled = False
            TextBox1.Clear()
            TextBox2.Clear()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            Dim cindex As Integer
            cindex = DataGridView1.CurrentRow.Index
            TextBox1.Text = DataGridView1.Item(0, cindex).Value
            TextBox2.Text = DataGridView1.Item(1, cindex).Value
        Catch ex As Exception
            'MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Error !!")
        End Try

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim Conn As SqlConnection
        Dim Cmd As New SqlCommand

        Try
            Conn = getConnect()
            Conn.Open()
            Cmd = Conn.CreateCommand
            Cmd.CommandText = "delete from drinktype where drinktype_ID = '" & TextBox1.Text & "'"
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
End Class