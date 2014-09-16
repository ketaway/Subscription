Public Class SEVENCATLOG

    Private _CheckingStatus As Boolean
    Public ReadOnly Property CheckingStatus() As Boolean
        Get
            Return _CheckingStatus
        End Get

    End Property

    Private _ReplySMS As String
    Public ReadOnly Property ReplySMS() As String
        Get
            Return _ReplySMS
        End Get

    End Property

    Private _ReplyUSSD As String
    Public ReadOnly Property ReplyUSSD() As String
        Get
            Return _ReplyUSSD
        End Get

    End Property
    Private _Code As String
    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(ByVal value As String)
            _Code = value
        End Set
    End Property

    Private _Mobile As String
    Public Property Mobile() As String
        Get
            Return _Mobile
        End Get
        Set(ByVal value As String)
            _Mobile = value
        End Set
    End Property

    Private _Oper As String
    Public Property Oper() As String
        Get
            Return _Oper
        End Get
        Set(ByVal value As String)
            _Oper = value
        End Set
    End Property
    Private _IP As String
    Public Property IP() As String
        Get
            Return _IP
        End Get
        Set(ByVal value As String)
            _IP = value
        End Set
    End Property
    Public Function Decode_Checking() As Boolean
        Code = Code.Trim
        Dim oCode As String = Code
        Dim SystextCode As String = ""
        'Dim _ResultText As Boolean = Nothing
        Dim C As Integer = 0
        Dim textlen = Code.Length

        'Check Length
        If (textlen <> 8) Then
            '_ResultText = False
            SystextCode = ""
            _CheckingStatus = False
            'CallStoreProcudureInsertToCPRegister()
        Else
            '    Dim ST1, ST2, ST3, ST4 As Integer
            '    Dim P As Integer
            '    Dim C1, C2, C3, CTemp As Integer
            '    Dim N1, N2, N3, N4, N5, N6, N7 As Integer
            '    Dim SD As Integer
            '    Dim D, DCal As Integer
            '    Dim XSum As Integera
            '    If (textlen = 16) Then ' ถ้า มี 16 หลัก ให้เสริมให้เป็น 17
            '        oCode = oCode.Substring(0, 1) + "0" + oCode.Substring(1, textlen - 1)
            '    End If

            '    N6 = oCode.Substring(0, 1)
            '    N7 = oCode.Substring(1, 1)
            '    D = oCode.Substring(2, 1)
            '    C2 = oCode.Substring(3, 1)
            '    N1 = oCode.Substring(4, 1)
            '    P = oCode.Substring(5, 1)
            '    C3 = oCode.Substring(6, 1)
            '    ST1 = oCode.Substring(7, 1)
            '    N2 = oCode.Substring(8, 1)
            '    C1 = oCode.Substring(9, 1)
            '    ST2 = oCode.Substring(10, 1)
            '    N3 = oCode.Substring(11, 1)
            '    ST3 = oCode.Substring(12, 1)
            '    N4 = oCode.Substring(13, 1)
            '    ST4 = oCode.Substring(14, 1)
            '    N5 = oCode.Substring(15, 1)
            '    SD = oCode.Substring(16, 1)

            '    XSum = N6 + N7 * 3 + C2 + N1 * 3 + P + C3 * 3 + ST1 + N2 * 3 + C1 + ST2 * 3 + N3 + ST3 * 3 + N4 + ST4 * 3 + N5 + SD * 3
            '    DCal = 9 - CInt(XSum.ToString.Substring(XSum.ToString.Length - 1, 1))
            '    CTemp = CInt(C1.ToString + C2.ToString + C3.ToString)
            '    C = 1000 - CTemp
            '    If (DCal <> D) Then
            '        _CheckingStatus = False
            '    ElseIf C < 1 Or C > 999 Then
            '        _CheckingStatus = False
            '    Else
            '        Dim ST, N As String
            '        ST = ST1.ToString + ST2.ToString + ST3.ToString + ST4.ToString
            '        N = N1.ToString + N2.ToString + N3.ToString + N4.ToString + N5.ToString + N6.ToString + IIf(textlen = 17, N7.ToString, "")
            _CheckingStatus = True
            '    End If
        End If
        CallStoreProcudureInsertToCPRegister()
        'If CheckingStatus Then
        '    'Return "หมายเลขร่วมสนุกของคุณคือ " & Code & " คุณมีสิทธิ์ลุ้นชิงโชค จำนวน " + C.ToString + " สิทธิ์ ขอบคุณค่ะ"
        Return CheckingStatus
        'Else
        ''Return "ขออภัยค่ะ รหัสของท่านไม่ถูกต้อง"
        'Return GetSysText("SMS.AFTERCHKDECODE.FALSE")
        'End If

    End Function

    ''' <summary>
    ''' If Decode_Checking is "TRUE" = Call SP: [192.168.0.21].CPALL.dbo.CPALL_DECODE
    ''' If Decode_Checking is "FALSE" = Call SP: [192.168.0.21].CPALL.dbo.CPALL_DECODE_ERROR
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CallStoreProcudureInsertToCPRegister()
        Try
            Dim Results As Integer = 0
            Dim oSqlConnection As New Data.SqlClient.SqlConnection("Data Source=192.168.0.21;Initial Catalog=CATALOG;User ID=sa;Password=cheeseteam55++")
            Dim oSelectCommand As New Data.SqlClient.SqlCommand()
            If oSqlConnection.State <> Data.ConnectionState.Open Then
                oSqlConnection.Open()
            End If

            'If CheckingStatus = True Then
            oSelectCommand.CommandText = "[DBO].[CATALOG_DECODE]"
            'Else
            '    oSelectCommand.CommandText = "[DBO].[CPALL_DECODE_ERROR]"
            'End If
            oSelectCommand.CommandType = Data.CommandType.StoredProcedure
            oSelectCommand.Parameters.Add("@CODE", Data.SqlDbType.VarChar, 20).Value = _Code
            oSelectCommand.Parameters.Add("@MSN", Data.SqlDbType.VarChar, 16).Value = _Mobile
            oSelectCommand.Parameters.Add("@OPER", Data.SqlDbType.VarChar, 10).Value = _Oper
            oSelectCommand.Parameters.Add("@SERVER_IP", Data.SqlDbType.VarChar, 3).Value = _IP

            oSelectCommand.Parameters.Add("@Check", Data.SqlDbType.Bit, 1).Value = _CheckingStatus
            oSelectCommand.Parameters("@Check").Direction = Data.ParameterDirection.InputOutput

            oSelectCommand.Parameters.Add("@ReplySMS", Data.SqlDbType.VarChar, 200).Direction = Data.ParameterDirection.Output
            oSelectCommand.Parameters.Add("@ReplyUSSD", Data.SqlDbType.VarChar, 200).Direction = Data.ParameterDirection.Output

            oSelectCommand.Connection = oSqlConnection
            oSelectCommand.ExecuteNonQuery()
            oSqlConnection.Close()

            _CheckingStatus = oSelectCommand.Parameters("@Check").Value
            _ReplySMS = oSelectCommand.Parameters("@ReplySMS").Value
            _ReplyUSSD = oSelectCommand.Parameters("@ReplyUSSD").Value

        Catch ex As Exception
            IO.File.AppendAllText(LogPath + "\moUSSDCallSP-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ex.Message & vbNewLine)
        End Try
    End Sub

    'Public Shared Function GetSysText(ByVal Code As String) As String
    '    Dim rData As String
    '    Try
    '        Dim oConnectDB As New Data.SqlClient.SqlConnection("Data Source=192.168.0.21;Initial Catalog=CPALL;User ID=sa;Password=cheeseteam55++")
    '        If oConnectDB.State <> Data.ConnectionState.Open Then
    '            oConnectDB.Open()
    '        End If

    '        Dim oReader As System.Data.SqlClient.SqlDataReader
    '        Dim qrs As String = "SELECT [SYSTEXTVALUE] FROM [CPALL].[dbo].[SYSTEXT] WHERE SYSTEXTCODE = '" & Code & "'"
    '        Dim objCommand = New System.Data.SqlClient.SqlCommand(qrs, oConnectDB)
    '        oReader = objCommand.ExecuteReader()
    '        oReader.Read()
    '        rData = oReader.Item(0)
    '        oReader.Close()
    '        oConnectDB.Close()
    '    Catch ex As Exception
    '        rData = Nothing
    '    End Try
    '    Return rData
    'End Function

End Class
