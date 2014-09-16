Imports System.Data.SqlClient

Public Class clsAuthen
    Public Function getActiveUser As 
    Public Function getMenu() As SqlDataReader
        Dim objConStr As SqlConnection
        objConStr = New clsDBAccess().CreateSqlConnection()

        Dim objComm As New SqlCommand
        Try


            objComm.Connection = objConStr
            objConStr.Open()
            objComm.CommandText = "select * from T_MENU"
            Return objComm.ExecuteReader()
        Catch ex As Exception
            Return Nothing
        Finally
            ' objConStr.Close()

        End Try

    End Function
End Class
