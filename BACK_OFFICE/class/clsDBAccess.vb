Imports System.Data.SqlClient

Public Class clsDBAccess

    Public Function CreateSqlConnection() As SqlConnection
        Dim connectionString As String

        connectionString = My.Settings.ConStr

        Return New SqlConnection(connectionString)
    End Function

   
End Class
