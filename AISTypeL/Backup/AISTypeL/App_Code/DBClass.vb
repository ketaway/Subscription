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
    Public Function CreateQueueLegacy(ByVal ScheduleGroup As String, ByVal BatchCount As Integer) As Boolean
        Dim conn As New DBConnSQL
        conn.DBConnect()
        Dim cmDEL As New SqlCommand
        With cmDEL
            '.CommandType = CommandType.Text
            .Connection = conn.Conn
            .CommandType = CommandType.StoredProcedure
            '@submitrequestid as int
            ',@BatchCount as int
            ',@HaveRemainInprocess as bit output
            .Parameters.Add("ScheduleGroup", SqlDbType.VarChar).Value = ScheduleGroup
            .Parameters.Add("BatchCount", SqlDbType.Int).Value = BatchCount
            .Parameters.Add("HaveRemainInprocess", SqlDbType.Bit)
            .Parameters("HaveRemainInprocess").Direction = ParameterDirection.Output
            .CommandText = "CreateQueueLegacy"
            '.CommandText = "DELETE FROM [SubmitRequest] WHERE submitrequestid in (" & SubmitRequestIDList & ")"
            .ExecuteNonQuery()
            '.Clone()
        End With
        conn.DBClose()
        CreateQueueLegacy = CBool(cmDEL.Parameters("HaveRemainInprocess").Value)
        cmDEL = Nothing
    End Function
    'CheckTypeLWaitingtoProcess
    Public Function CheckTypeLWaitingtoProcess() As DataTable
        Dim conn As New DBConnSQL
        conn.DBConnect()
        Dim cmDEL As New SqlCommand
        Dim sqlDa As New SqlDataAdapter(cmDEL)
        With cmDEL
            '.CommandType = CommandType.Text
            .Connection = conn.Conn
            .CommandType = CommandType.StoredProcedure
            '@submitrequestid as int
            ',@BatchCount as int
            ',@HaveRemainInprocess as bit output
            .CommandText = "CheckTypeLWaitingtoProcess"
            '.CommandText = "DELETE FROM [SubmitRequest] WHERE submitrequestid in (" & SubmitRequestIDList & ")"
            '.Clone()
        End With
        Dim dt As New DataTable
        sqlDa.Fill(dt)
        CheckTypeLWaitingtoProcess = dt
        sqlDa.Dispose()
        conn.DBClose()
        conn = Nothing
        cmDEL = Nothing
    End Function
    Public Function getBroadcastActiveUserList_TypeL() As DataTable
        Dim conn As New DBConnSQL
        conn.DBConnect()
        Dim cmDEL As New SqlCommand
        Dim sqlDa As New SqlDataAdapter(cmDEL)
        With cmDEL
            '.CommandType = CommandType.Text
            .Connection = conn.Conn
            .CommandType = CommandType.StoredProcedure
            '@submitrequestid as int
            ',@BatchCount as int
            ',@HaveRemainInprocess as bit output
            .CommandText = "getBroadcastActiveUserList_TypeL"

            '.CommandText = "DELETE FROM [SubmitRequest] WHERE submitrequestid in (" & SubmitRequestIDList & ")"
            '.Clone()
        End With
        Dim dt As New DataTable
        sqlDa.Fill(dt)
        getBroadcastActiveUserList_TypeL = dt
        sqlDa.Dispose()
        conn.DBClose()
        conn = Nothing
        cmDEL = Nothing
    End Function
    Public Function getBroadcastActiveUserList_TypeL_ByMSN() As DataTable
        Dim conn As New DBConnSQL
        conn.DBConnect()
        Dim cmDEL As New SqlCommand
        Dim sqlDa As New SqlDataAdapter(cmDEL)
        With cmDEL
            '.CommandType = CommandType.Text
            .Connection = conn.Conn
            .CommandType = CommandType.StoredProcedure
            '@submitrequestid as int
            ',@BatchCount as int
            ',@HaveRemainInprocess as bit output
            .CommandText = "getBroadcastActiveUserList_TypeL_ByMSN"

            '.CommandText = "DELETE FROM [SubmitRequest] WHERE submitrequestid in (" & SubmitRequestIDList & ")"
            '.Clone()
        End With
        Dim dt As New DataTable
        sqlDa.Fill(dt)
        getBroadcastActiveUserList_TypeL_ByMSN = dt
        sqlDa.Dispose()
        conn.DBClose()
        conn = Nothing
        cmDEL = Nothing
    End Function

    Public Function SetSuccessTypeLProcess(ByVal SubmitRequest_ID As Integer, ByVal TypeLFTPFile As String) As Boolean
        Dim conn As New DBConnSQL
        conn.DBConnect()
        Dim cmDEL As New SqlCommand
        With cmDEL
            '.CommandType = CommandType.Text
            .Connection = conn.Conn
            .CommandType = CommandType.StoredProcedure
            '@submitrequestid as int
            ',@BatchCount as int
            ',@HaveRemainInprocess as bit output
            .CommandText = "SetSuccessTypeLProcess"
            .Parameters.Add("SubmitRequest_ID", SqlDbType.Int).Value = SubmitRequest_ID
            .Parameters.Add("TypeLFTPFile", SqlDbType.VarChar).Value = TypeLFTPFile


            '.CommandText = "DELETE FROM [SubmitRequest] WHERE submitrequestid in (" & SubmitRequestIDList & ")"
            .ExecuteNonQuery()
            '.Clone()
        End With
        conn.DBClose()
        cmDEL = Nothing
    End Function

    Public Function SetSuccessTypeLProcess_ByMSN(ByVal ServicePMKey_ID As Integer, ByVal TypeLFTPFile As String) As Boolean
        Dim conn As New DBConnSQL
        conn.DBConnect()
        Dim cmDEL As New SqlCommand
        With cmDEL
            '.CommandType = CommandType.Text
            .Connection = conn.Conn
            .CommandType = CommandType.StoredProcedure
            '@submitrequestid as int
            ',@BatchCount as int
            ',@HaveRemainInprocess as bit output
            .CommandText = "SetSuccessTypeLProcess_ByMSN"
            .Parameters.Add("ServicePMKey", SqlDbType.Int).Value = ServicePMKey_ID
            .Parameters.Add("TypeLFTPFile", SqlDbType.VarChar).Value = TypeLFTPFile


            '.CommandText = "DELETE FROM [SubmitRequest] WHERE submitrequestid in (" & SubmitRequestIDList & ")"
            .ExecuteNonQuery()
            '.Clone()
        End With
        conn.DBClose()
        cmDEL = Nothing
    End Function

    Public Function getSubmitRequestInprocessQueue_ByBatchID(ByVal BatchID As Integer) As DataTable
        Dim conn As New DBConnSQL
        conn.DBConnect()
        Dim cmDEL As New SqlCommand
        Dim sqlDa As New SqlDataAdapter(cmDEL)
        With cmDEL
            '.CommandType = CommandType.Text
            .Connection = conn.Conn
            .CommandType = CommandType.StoredProcedure
            '@submitrequestid as int
            ',@BatchCount as int
            ',@HaveRemainInprocess as bit output
            .Parameters.Add("BatchID", SqlDbType.Int).Value = BatchID
            .CommandText = "getSubmitRequestInprocessQueue_ByBatchID"
            '.CommandText = "DELETE FROM [SubmitRequest] WHERE submitrequestid in (" & SubmitRequestIDList & ")"


            '.Clone()
        End With
        Dim dt As New DataTable
        sqlDa.Fill(dt)
        getSubmitRequestInprocessQueue_ByBatchID = dt
        sqlDa.Dispose()
        conn.DBClose()
        conn = Nothing
        cmDEL = Nothing
    End Function
    'SetSuccessSubmit
    Public Function SetSuccessSubmit(ByVal SubmitRequestID As Integer, ByVal messageid As String, ByVal description As String, ByVal details As String, ByVal StatusCode As String, Optional ByVal action_remark As String = "", Optional ByVal dr_remark As String = "") As Boolean
        Dim conn As New DBConnSQL
        conn.DBConnect()
        Dim cmDEL As New SqlCommand
        With cmDEL
            '.CommandType = CommandType.Text
            .Connection = conn.Conn
            .CommandType = CommandType.StoredProcedure
            '@submitrequestid as int
            ',@BatchCount as int
            ',@HaveRemainInprocess as bit output
            .Parameters.Add("SubmitRequestID", SqlDbType.Int).Value = SubmitRequestID
            .Parameters.Add("messageid", SqlDbType.NVarChar).Value = messageid
            .Parameters.Add("description", SqlDbType.NVarChar).Value = description
            .Parameters.Add("details", SqlDbType.NVarChar).Value = details
            .Parameters.Add("StatusCode", SqlDbType.NVarChar).Value = StatusCode
            .Parameters.Add("action_remark", SqlDbType.NVarChar).Value = action_remark
            .Parameters.Add("dr_remark", SqlDbType.NVarChar).Value = dr_remark
            ',@dr_remark as nvarchar(255)
            .CommandText = "SetSuccessSubmit"
            '.CommandText = "DELETE FROM [SubmitRequest] WHERE submitrequestid in (" & SubmitRequestIDList & ")"
            .ExecuteNonQuery()
            '.Clone()
        End With
        conn.DBClose()

        cmDEL = Nothing
    End Function
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
