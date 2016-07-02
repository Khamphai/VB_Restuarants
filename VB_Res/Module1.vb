Imports System.Data
Imports System.Data.SqlClient

Module Module1
    Public Conn As SqlConnection
    Public Function getConnect() As SqlConnection
        Conn = New SqlConnection("Data Source=KHAMPHAI\SQLEXPRESS;Initial Catalog=Restuarant;User ID=Khamphai\K'Phai;Integrated Security=True")
        Return Conn
    End Function

End Module
