Imports System.Data.SqlClient
Imports System.Data.OleDb
Public Class DBClass
    Private _ErrorMsg As String
    Public Property ErrorMsn() As String
        Get
            Return _ErrorMsg
        End Get
        Set(ByVal value As String)
            _ErrorMsg = value
        End Set
    End Property
    Public Sub AddDR(MESSAGE_ID As String, OPERATOR_SERVICE_ID As String, STATUS As String, STATUSTEXT As String, CARRIER As String, CHANNEL As String _
                     , MSN As String, CCT As String, CHARGE As String, DRDAY As String, DRMONTH As String, DRYEAR As String, DRTIME As String, DRFILE As String)
        Dim conn As New DBConnSQL
        Dim cmDEL As New SqlCommand
        With cmDEL
            '.CommandType = CommandType.Text
            .Connection = conn.Conn
            .CommandType = CommandType.StoredProcedure

            .Parameters.Add("MESSAGE_ID", SqlDbType.VarChar).Value = MESSAGE_ID
            .Parameters.Add("OPERATOR_SERVICE_ID", SqlDbType.VarChar).Value = OPERATOR_SERVICE_ID
            .Parameters.Add("STATUS", SqlDbType.VarChar).Value = STATUS
            .Parameters.Add("STATUSTEXT", SqlDbType.VarChar).Value = STATUSTEXT
            .Parameters.Add("CARRIER", SqlDbType.VarChar).Value = CARRIER
            .Parameters.Add("CHANNEL", SqlDbType.VarChar).Value = CHANNEL
            .Parameters.Add("MSN", SqlDbType.VarChar).Value = MSN
            .Parameters.Add("CCT", SqlDbType.VarChar).Value = CCT
            .Parameters.Add("CHARGE", SqlDbType.NVarChar).Value = CHARGE
            .Parameters.Add("DRDAY", SqlDbType.VarChar).Value = DRDAY
            .Parameters.Add("DRMONTH", SqlDbType.VarChar).Value = DRMONTH
            .Parameters.Add("DRYEAR", SqlDbType.VarChar).Value = DRYEAR
            .Parameters.Add("DRTIME", SqlDbType.VarChar).Value = DRTIME
            .Parameters.Add("DRFILE", SqlDbType.VarChar).Value = DRFILE
            ',@Flag as nvarchar(1)

            .CommandText = "AddDR"
            '.CommandText = "DELETE FROM [SubmitRequest] WHERE submitrequestid in (" & SubmitRequestIDList & ")"
            Try
                conn.DBConnect()
                .ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.DBClose()
                cmDEL = Nothing
            End Try
            '.Clone()
        End With

    End Sub
    Public Sub RunReport()
        Dim conn As New DBConnSQL
        Dim cmDEL As New SqlCommand
        With cmDEL
            '.CommandType = CommandType.Text
            .Connection = conn.Conn
            .CommandType = CommandType.StoredProcedure

            ',@Flag as nvarchar(1)

            .CommandText = "RUN_REPORT"
            '.CommandText = "DELETE FROM [SubmitRequest] WHERE submitrequestid in (" & SubmitRequestIDList & ")"
            Try
                conn.DBConnect()
                .ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.DBClose()
                cmDEL = Nothing
            End Try
            '.Clone()
        End With

    End Sub
    'CheckTypeLWaitingtoProcess
 
    'Public Function RecallSchedule() As DataTable
    '    Dim conn As New DBConnAccess
    '    Dim SqlCommand As String = "Select * from SubmitRequest ORDER BY Schedule,SQLSubmitRequestID"
    '    Dim da As New OleDbDataAdapter(SqlCommand, conn.Conn)
    '    Dim dt As New DataTable
    '    da.Fill(dt)
    '    da.Dispose()
    '    conn.DBClose()
    '    conn = Nothing
    '    RecallSchedule = dt
    '    dt.Dispose()
    'End Function
    'Public Function DeleteSchedule(ByVal ScheduleText As String) As Boolean
    '    Dim conn As New DBConnAccess
    '    Dim SqlCommand As String = "delete from SubmitRequest where ScheduleText=""" + ScheduleText + """"
    '    Try
    '        Dim dc As New OleDbCommand(SqlCommand, conn.Conn)
    '        conn.DBConnect()
    '        dc.ExecuteNonQuery()
    '        dc.Dispose()
    '        conn.DBClose()
    '        conn = Nothing
    '        Return False
    '    Catch ex As Exception
    '        _ErrorMsg = ex.Message
    '        Return False
    '    End Try
    'End Function
    'Public Function SetScheduleInProcess(ByVal ScheduleText As String) As Boolean
    '    Dim conn As New DBConnAccess
    '    Dim SqlCommand As String = "Update SubmitRequest where ScheduleText=""" + ScheduleText + """"
    '    Try
    '        Dim dc As New OleDbCommand(SqlCommand, conn.Conn)
    '        conn.DBConnect()
    '        dc.ExecuteNonQuery()
    '        dc.Dispose()
    '        conn.DBClose()
    '        conn = Nothing
    '        Return False
    '    Catch ex As Exception
    '        _ErrorMsg = ex.Message
    '        Return False
    '    End Try
    'End Function
End Class
