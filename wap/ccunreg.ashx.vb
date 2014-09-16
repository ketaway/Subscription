Imports System.Web
Imports System.Web.Services
Imports NLog
Imports System.Data.SqlClient

Public Class ccunreg
    Implements System.Web.IHttpHandler
    Public Nlogger As Logger = LogManager.GetLogger("Log")
    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim msn As String
        msn = context.Request.QueryString("number")
        If (msn Is Nothing) Then
            context.Response.Write("ERROR")
            Exit Sub
        End If
        If (msn.StartsWith("0")) Then
            msn = "66" + msn.Substring(1, msn.Length - 1)
        End If
        Nlogger.Trace(msn)
        Dim RESULT, OPER As String

        Dim OService_ID As String
        OService_ID = context.Request.QueryString("sid")
        Dim comm2 As New SqlCommand("checkExistingSubscriber", New SqlConnection(My.Settings.DBCon))

        With comm2
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("MSN", SqlDbType.VarChar)).Value = msn
            .Parameters.Add(New SqlParameter("OPERATOR", SqlDbType.VarChar, 1)).Direction = ParameterDirection.Output
            .Parameters.Add(New SqlParameter("Result", SqlDbType.VarChar, 10)).Direction = ParameterDirection.Output

            Try
                .Connection.Open()
                .ExecuteNonQuery()
                RESULT = .Parameters("Result").Value
                OPER = .Parameters("OPERATOR").Value
                Nlogger.Trace(OPER)
                Nlogger.Trace(RESULT)
                context.Response.Write(RESULT)
            Catch ex As Exception
                context.Response.Write("ERROR")
                Nlogger.Error(ex.Message)
            Finally
                .Connection.Close()
                .Dispose()
            End Try


        End With
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class