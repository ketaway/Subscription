Public Class CPALL_2014
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
    Private _chance As Integer
    Public Property chance() As Integer
        Get
            Return _chance
        End Get
        Set(ByVal value As Integer)
            _chance = value
        End Set
    End Property
    Public Function Decode_Checking() As Boolean
        Code = Code.Trim
        Dim oCode As String = Code
        Dim SystextCode As String = ""
        'Dim _ResultText As Boolean = Nothing
        Dim C As Integer = 0
        Dim textlen = Code.Length
        Try

            'Check Length
            If (textlen <> 16) Then
                SystextCode = ""
                _CheckingStatus = False
            Else
                Dim S1, S2, S3, S4, S5 As Integer
                Dim C1, C2, C3 As Integer
                Dim N1, N2, N3, N4, N5, N6 As Integer
                Dim SD As Integer
                Dim D, DCal As Integer
                Dim XSum As Integer

                N6 = oCode.Substring(0, 1)
                C2 = oCode.Substring(1, 1)
                S5 = oCode.Substring(2, 1)
                N1 = oCode.Substring(3, 1)
                D = oCode.Substring(4, 1)
                C3 = oCode.Substring(5, 1)
                S1 = oCode.Substring(6, 1)
                N2 = oCode.Substring(7, 1)
                C1 = oCode.Substring(8, 1)
                S2 = oCode.Substring(9, 1)
                N3 = oCode.Substring(10, 1)
                S3 = oCode.Substring(11, 1)
                N4 = oCode.Substring(12, 1)
                S4 = oCode.Substring(13, 1)
                N5 = oCode.Substring(14, 1)
                SD = oCode.Substring(15, 1)

                'If SD = 5 Or SD = 6 Or SD = 7 Or SD = 8 Then
                '    Dim Nint As Integer = CInt(N1.ToString + N2.ToString + N3.ToString + N4.ToString + N5.ToString + N6.ToString)
                '    Nint = Nint + 999999
                '    N1 = Nint.ToString.Substring(0, 1)
                '    N2 = Nint.ToString.Substring(1, 1)
                '    N3 = Nint.ToString.Substring(2, 1)
                '    N4 = Nint.ToString.Substring(3, 1)
                '    N5 = Nint.ToString.Substring(4, 1)
                '    N6 = Nint.ToString.Substring(5, 1)
                '    N7 = Nint.ToString.Substring(6, 1)
                'End If

                'WriteLog("7debug-1-CODE16", N6 & "|" & C2 & "|" & S5 & "|" & N1 & "|" & D & "|" & C3 & "|" & S1 & "|" & N2 & "|" & C1 & "|" & S2 & "|" & N3 & "|" & S3 & "|" & N4 & "|" & S4 & "|" & N5 & "|" & SD, Now)



                'Dim ST, P As Integer
                'Dim STKey As Integer = CInt(S1.ToString + S2.ToString + S3.ToString + S4.ToString + S5.ToString)
                'ST = Math.Ceiling(CDbl(STKey / 9))

                'If STKey Mod 9 <> 0 Then
                '    P = STKey Mod 9
                'Else
                '    P = 9
                'End If
                'WriteLog("7debug-6-ST", ST.ToString & "|" & ST1.ToString & "|" & ST2.ToString & "|" & ST3.ToString & "|" & ST4.ToString, Now)


                C = 1000 - CInt(C1.ToString + C2.ToString + C3.ToString)
                'WriteLog("7debug-5-C", C.ToString & "|" & C1.ToString & "|" & C2.ToString & "|" & C3.ToString, Now)

                'Dim N As String
                'N = N1.ToString + N2.ToString + N3.ToString + N4.ToString + N5.ToString + N6.ToString + IIf(textlen = 17, N7.ToString, "")
                'WriteLog("7debug-7-N", N.ToString & "|" & N1.ToString & "|" & N2.ToString & "|" & N3.ToString & "|" & N4.ToString & "|" & N5.ToString & "|" & N6.ToString & "|" & IIf(textlen = 17, N7.ToString, ""), Now)

                S2 = S2 * 3
                S3 = S3 * 3
                S4 = S4 * 3
                C3 = C3 * 3
                N1 = N1 * 3
                N2 = N2 * 3
                SD = SD * 3

                XSum = N1 + N2 + N3 + N4 + N5 + N6 + C1 + C2 + C3 + S1 + S2 + S3 + S4 + S5 + SD
                'WriteLog("7debug-3-XSUM", XSum.ToString & "|" & N1 & "+" & N2 & "+" & N3 & "+" & N4 & "+" & N5 & "+" & N6 & "+" & IIf(textlen = 17, N7, 0) & "+" & C1 & "+" & C2 & "+" & C3 & "+" & S1 & "+" & S2 & "+" & S3 & "+" & S4 & "+" & S5 & "+" & SD, Now)

                Dim D2 As Integer = Right(XSum.ToString, 1)
                DCal = 9 - D2
                'WriteLog("7debug-4-DCAL", DCal.ToString, Now)


                If (DCal <> D) Then ' CheckDigit
                    _CheckingStatus = False

                    'WriteLog("7debug-7-STATUS-FALSE", DCal.ToString & "<>" & D.ToString & "|" & _CheckingStatus, Now)
                Else
                    _CheckingStatus = True
                    'WriteLog("7debug-7-STATUS-TRUE", DCal.ToString & "<>" & D.ToString & "|" & _CheckingStatus, Now)
                End If
            End If

        Catch ex As Exception
            _CheckingStatus = False
            'WriteLog("7debug-1-MAPCODEERROR", ex.ToString, Now)
        End Try

        'WriteLog("7debug-8-STATUS", _CheckingStatus.ToString, Now)

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
            Dim oSqlConnection As New Data.SqlClient.SqlConnection("Data Source=192.168.0.21;Initial Catalog=CPALL_2014;User ID=sa;Password=cheeseteam55++")
            Dim oSelectCommand As New Data.SqlClient.SqlCommand()
            If oSqlConnection.State <> Data.ConnectionState.Open Then
                oSqlConnection.Open()
            End If

            'If CheckingStatus = True Then
            oSelectCommand.CommandText = "[DBO].[Decode_Checking]"
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


            'WriteLog("7debug-9-PARAMETER", _Code & "|" & _Mobile & "|" & _Oper & "|" & _IP & "|" & _CheckingStatus, Now)

        Catch ex As Exception
            IO.File.AppendAllText(LogPath + "\moUSSDCallSP-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ex.Message & vbNewLine)
        End Try
    End Sub

End Class
