Imports System.IO
Imports NLog

Module common
    Public LogPath As String = My.Settings.LogPath
    Public Nlogger As Logger = LogManager.GetLogger("Log")

    Public Function WriteLog(FileName As String, Text As String, logtime As DateTime, Optional IsError As Boolean = False) As Boolean

        Dim sWrite As StreamWriter = Nothing
        Dim sWriteError As StreamWriter = Nothing
        Try
            Text = IIf(IsError, " [ERROR] ", " ") & Text
            sWrite = New System.IO.StreamWriter(LogPath + FileName & "_" & logtime.ToString("yyyy-MM-dd") & ".log", True)
            sWrite.WriteLine(logtime.ToString("yyyy-MM-dd HH:mm:ss") & Text)


            'Using fs = New FileStream(LogPath + FileName & "_" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)
            '    fs.SetLength(0)
            '    Dim textbyte() As Byte = Encoding.Default.GetBytes(Text + "DDDDDDDSSSS")
            '    fs.Write(textbyte, 0, textbyte.Count())
            '    fs.Flush()
            '    fs.Close()
            'End Using


            'IO.File.AppendAllText(Path + FileName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & Text & vbNewLine)
            Return True
        Catch ex As Exception
            Try
                sWriteError = New System.IO.StreamWriter(LogPath + FileName & "_Error_" & logtime.ToString("yyyy-MM-dd") & ".log", True)
                sWriteError.WriteLine(logtime.ToString("yyyy-MM-dd HH:mm:ss") & " [LOG TEXT] " & Text)
                sWriteError.WriteLine(logtime.ToString("yyyy-MM-dd HH:mm:ss") & " [ERROR] " & ex.Message)
            Catch ex2 As Exception
            Finally
                sWriteError.Close()
                sWriteError.Dispose()
            End Try

            ' IO.File.AppendAllText(Path + "moerror-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ex.Message & vbNewLine)
            Return False
        Finally
            sWrite.Close()
            sWrite.Dispose()
            'sWriteError.Close()
            'sWriteError.Dispose()
        End Try
    End Function
    Public Sub sendwaplinkbycheese(getURLwap As String, getMsn As String, getwapname As String)

        Dim strURL, strParameters As String


        strURL = "http://202.142.221.242/sms/sendwappush.php"

        'strParameters = "username=PopIdols&passwd=wijit_popsms&name=suchot&url="&server.URLEncode("wap.fone4fun.com/download.php?sid=3&code=5844&partner=5003&cid=40818&back=http://wap.fone4fun.com&scode=")&"&to=0816429778"
        strParameters = "username=freelink1&passwd=freelink1&name=" & getwapname & "&url=" & HttpUtility.UrlEncode(getURLwap) & "&to=" & getMsn


        'strinXML = strParameters
        Dim DataToSend() As Byte = Encoding.UTF8.GetBytes(strParameters)

        Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create(strURL)

        With HTTPRequest
            .Timeout = 5000
            .Method = "POST"
            .ContentType = "application/x-www-form-urlencoded"

            '.Proxy = Nothing
            '.ContentLength = DataToSend.Length
        End With
        Dim SendHTTPRequest As IO.Stream = HTTPRequest.GetRequestStream()
        SendHTTPRequest.Write(DataToSend, 0, DataToSend.Length)
        SendHTTPRequest.Close()
        Dim HttpResponse As Net.HttpWebResponse = HTTPRequest.GetResponse()
        Dim ResponseData As String = Nothing
        If HttpResponse.StatusCode.Equals(Net.HttpStatusCode.OK) Or HttpResponse.StatusCode.Equals(Net.HttpStatusCode.Accepted) Then
            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
            ResponseData = sReader.ReadToEnd().Trim()
        End If

    End Sub

End Module
