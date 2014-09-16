Imports System.Text
Imports NLog

Module Module1
    Private txID, StatusCode As String
    Sub Main()
        Dim objDB As New DBClass
        Dim dt As New DataTable
       
        Dim dtBRSchedule As New DataTable

 
      



        dt = objDB.getDueContentSchedule("D")

        If (dt.Rows.Count > 0) Then
            Dim TypeLFTPFile As String = ""
            Console.WriteLine(dt.Rows.Count.ToString)
            For Each dr As DataRow In dt.Rows
                'SELECT CS.CONTENT_SCHEDULE_ID,CSS.CONTENT_SCHEDULE_SUBSERVICE_ID,S.SUBSERVICE_ID,CS.SMSCONTENT,CS.SCHEDULE FROM T_CONTENT_SCHEDULE CS
                dtBRSchedule = objDB.getActive_Member_for_Schedule_MT(dr("CONTENT_SCHEDULE_SUBSERVICE_ID").ToString, dr("SUBSERVICE_ID").ToString, "D")
                '            	,SV.SERVICE_NAME,SD.SENDER_NAME,S.PRICE
                MT(dr("SENDER_NAME"), dr("OPERATOR_SERVICE_ID"), dr("SMSCONTENT"))
                'PutFileContent(dtBRSchedule, TypeLFTPFile, dr("SERVICE_NAME").ToString, dr("SENDER_NAME").ToString, dr("SMSCONTENT").ToString, dr("PRICE").ToString, dr("OPERATOR_SERVICE_ID").ToString)
                objDB.set_Status_for_Schedule_MT(CInt(dr("CONTENT_SCHEDULE_SUBSERVICE_ID").ToString), txID, StatusCode)
                Threading.Thread.Sleep(5000)
            Next
        End If

    End Sub
    Private Sub MT(strSender As String, strOperator_ServiceID As String, strSMSText As String)
        Dim Nlogger As Logger = LogManager.GetLogger("Log")

        txID = "04503" + Now.ToString("ddHHmmss") + "001"    'ddhh24mmss
        Dim texttest As String
        texttest = "<?xml version=""1.0"" encoding=""utf-8"" ?>"
        texttest += "<cpa-request>"
        texttest += "<txid>" + txid + "</txid>"
        texttest += "<authentication>"
        texttest += "<user>avimobile503</user>"
        texttest += "<password>m125QRSjn</password>"
        texttest += "</authentication>"
        texttest += "<originator>"
        texttest += "<sender>" + strSender + "</sender>"
        texttest += "</originator>"
        texttest += "<destination>"
        texttest += "<serviceid>" + strOperator_ServiceID + "</serviceid>"

        '  texttest += "<msisdn>" + txtMSN.Text.Trim + "</msisdn>"

        'texttest += "<msisdn>66813041744</msisdn>"
        'texttest += "<msisdn>66859194778</msisdn>"
        texttest += "</destination>"
        texttest += "<message>"
        texttest += "<header>"
        texttest += "<timestamp>" + Now.ToString("yyyyMMddHHmmss", New System.Globalization.CultureInfo("en-US")) + "</timestamp>"
        texttest += "</header>"
        texttest += "<sms>"
        texttest += "<msg>" + strSMSText + "</msg>"
        texttest += "<msgtype>T</msgtype>"
        texttest += "<encoding>8</encoding>"
        texttest += "</sms>"
        texttest += "</message>"
        texttest += "</cpa-request>"
        'IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\mosms_quiz-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & texttest & vbNewLine)
        '2556-07-15 12:16:46  <?xml version="1.0" encoding="utf-8" ?><cpa-request><txid>461115121652878</txid><authentication><user>cheese123</user><password>S16ACGMSg</password></authentication><originator><sender>66859194778</sender></originator><destination><serviceid>45040262</serviceid><msisdn>66859194778</msisdn></destination><message><header><timestamp>20130715121646</timestamp></header><sms><msg>ทดสอบการส่งข้อความ serviceid = 45040262 ข้อความที่ส่งมาคือ </msg><msgtype>T</msgtype><encoding>8</encoding></sms></message></cpa-request>

        Try

      
        Nlogger.Trace(texttest)
        Dim DataToSend() As Byte = Encoding.UTF8.GetBytes(texttest)
        Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create("http://sdpapi.dtac.co.th:8319/SAG/services/cpa/sms")

        With HTTPRequest
            .Timeout = 5000
            .Method = "POST"
            .ContentType = "text/xml"

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

            '<?xml version="1.0" encoding="utf-8" ?><cpa-response><txid>0450324210353001</txid><status>200</status><status-description>Success</status-description></cpa-response>
        Else

            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
            ResponseData = sReader.ReadToEnd().Trim()
            'ResponseData = HttpResponse.GetResponseStream.ToString
        End If
            Nlogger.Trace(ResponseData)
            StatusCode = ResponseData.Substring(ResponseData.IndexOf("<status>") + 8)
            StatusCode = StatusCode.Substring(0, StatusCode.IndexOf("</status>"))
        'IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\mosms_quiz-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ResponseData & vbNewLine)
            HttpResponse.Close()
        Catch ex As Exception
            Nlogger.Error(texttest)

        End Try


    End Sub
End Module
