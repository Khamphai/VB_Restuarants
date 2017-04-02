Imports System.Data
Imports System.Data.SqlClient

Public Class frmInvoice
    Dim sum As Integer = 0

    Private Sub frmInvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        frmMain.WindowState = FormWindowState.Normal
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim conn As New SqlConnection
        Dim cmd As New SqlCommand

        Dim baht As Integer
        Dim USD As Integer

        '----------------Get Currency BAHT

        Try
            conn = getConnect()
            conn.Open()
            cmd = conn.CreateCommand
            cmd.CommandText = "select * from currencys where currency_name = N'" & "Baht" & "' order by dates asc"
            Dim da As New SqlDataAdapter(cmd.CommandText, conn)
            Dim dt As New DataTable("restuarant")
            da.Fill(dt)
            Dim i As Integer = dt.Rows.Count - 1
            baht = dt.Rows(i)(2)
            Label10.Text = "ອັດຕາແລກປ່ຽນ Baht = " & baht
            conn.Close()

        Catch ex As Exception
            'MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
            'MsgBox("???????????????????")
        End Try

        '--------------Get Currency USD

        Try
            conn = getConnect()
            conn.Open()
            cmd = conn.CreateCommand
            cmd.CommandText = "select * from currencys where currency_name = N'" & "US Dolar" & "' order by dates asc"
            Dim da As New SqlDataAdapter(cmd.CommandText, conn)
            Dim dt As New DataTable("restuarant")
            da.Fill(dt)
            Dim i As Integer = dt.Rows.Count - 1
            USD = dt.Rows(i)(2)
            conn.Close()
            Label12.Text = "ອັດຕາແລກປ່ຽນ $ = " & USD.ToString("#,###")
        Catch ex As Exception
            'MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
            'MsgBox("???????????????????")
        End Try

        sum = 0
        Try
            conn = getConnect()
            conn.Open()
            cmd = conn.CreateCommand
            'cmd.CommandText = "select description, sale_qtt, des_price, (sale_qtt * des_price) from sale_detail where sale_id ='" & "Sale" & "' and table_no='" & TextBox1.Text & "'"
            cmd.CommandText = "select sales.Sale_ID, sales.Table_ID, Sales.Paid_Status, Sale_Detail.Description, Sale_Detail.Des_Price,Sale_Detail.Sale_Qtt, Sale_Detail.Amount from Sales join Sale_Detail on Sales.Table_ID = Sale_Detail.Table_No where Sales.Table_ID = '" & TextBox2.Text & "' and Sales.Paid_Status = 'NO'"
            'select sales.Sale_ID, sales.Table_ID, Sales.Paid_Status, Sale_Detail.Description, Sale_Detail.Des_Price,Sale_Detail.Sale_Qtt, Sale_Detail.Amounts from Sales join Sale_Detail on Sales.Table_ID = Sale_Detail.Table_ID where Sales.Table_ID = '3' and Sales.Paid_Status = 'NO'
            Dim da As New SqlDataAdapter(cmd.CommandText, conn)
            Dim dt As New DataTable("restuarant")
            da.Fill(dt)
            Dim i As Integer = dt.Rows.Count - 1

            DataGridView1.DataSource = dt
            'DataGridView1.Columns(0).HeaderText = "??????????? ??? ??????????"
            'DataGridView1.Columns(1).HeaderText = "??????"
            'DataGridView1.Columns(2).HeaderText = "????"
            'DataGridView1.Columns(3).HeaderText = "???"

            If DataGridView1.Item(5, 0).Value = 0 Then
                Label8.Text = "0"
                Label10.Text = "0"
                Label12.Text = "0"
            End If


            sum = 0
            For i = 0 To DataGridView1.RowCount - 2 Step 1
                sum = sum + Val(DataGridView1.Item(6, i).Value)
            Next

            Label8.Text = sum.ToString("#,###") & " LAK"
            Label10.Text = (sum / baht).ToString("#,###") & " BAHT"
            Label12.Text = (sum / USD).ToString("#,###") & " USD"

            conn.Close()

        Catch ex As Exception
            MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Connection Error !!")
            Label8.Text = "0"
            Label10.Text = "0"
            Label12.Text = "0"
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim dtReport As New DataTable
            With dtReport
                .Columns.Add("DataColumn1")
                .Columns.Add("DataColumn2")
                .Columns.Add("DataColumn3")
                .Columns.Add("DataColumn4")
                .Columns.Add("DataColumn5")
                .Columns.Add("DataColumn6")
            End With
            For Each row As DataGridViewRow In DataGridView1.Rows
                dtReport.Rows.Add(TextBox2.Text, row.Cells(3).Value, row.Cells(5).Value, row.Cells(4).Value, row.Cells(6).Value, Label8.Text)
            Next
            frmRptInvoice.ReportViewer1.LocalReport.DataSources.Item(0).Value = dtReport
            frmRptInvoice.WindowState = FormWindowState.Maximized
            frmRptInvoice.ShowDialog()
            frmRptInvoice.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class