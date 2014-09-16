Imports System.Web
Imports System.Web.Services
Imports System.Xml

Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Public Class molistener
    Implements System.Web.IHttpHandler

    Private objConn As OleDbConnection
    Private objCmd As OleDbCommand
    Private Trans As OleDbTransaction
    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim msisdn, txid, url, productid, msg, msgtype, shortcode, serviceid As String
        Dim dtstart, dtfinish As DateTime

        Try
            dtstart = Now
            Dim len As Integer = context.Request.InputStream.Length
            Dim dataRequest As String = ""
            Dim logbuffer(len) As Byte
            context.Request.InputStream.Read(logbuffer, 0, len)
            dataRequest = System.Text.Encoding.UTF8.GetString(logbuffer)


            Dim XmlReader As New XmlDocument
            XmlReader.LoadXml(dataRequest)

            Try
                'IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\morequest-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & dataRequest & vbNewLine)
                IO.File.AppendAllText("D:\www\subscription\dtac\logs\mo-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & XmlReader.InnerXml & vbNewLine)
            Catch ex As Exception
                IO.File.AppendAllText("D:\www\subscription\dtac\logs\mo-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ex.Message.ToString & vbNewLine)
            End Try

            If (XmlReader.SelectNodes("/cpa-mobile-request/txid").Count > 0) Then ' Mobile Request



                txid = XmlReader.SelectSingleNode("/cpa-mobile-request/txid").InnerText
                msisdn = XmlReader.SelectSingleNode("/cpa-mobile-request/originator/msisdn").InnerText
                msgtype = XmlReader.SelectSingleNode("/cpa-mobile-request/message/sms/msgtype").InnerText
                msg = XmlReader.SelectSingleNode("/cpa-mobile-request/message/sms/msg").InnerText
                If msgtype = "H" Then
                    msg = MessageDecode(msg)
                End If
                serviceid = XmlReader.SelectSingleNode("/cpa-mobile-request/destination/serviceid").InnerText
                Dim DataResponseSuccess As String = "<cpa-response><txid>" + txid + "</txid><status>200</status><status-description>Success</status-description></cpa-response>"
                context.Response.ContentType = "text/xml"
                context.Response.Write(DataResponseSuccess)
                context.Response.Flush()
                dtfinish = Now
                WriteLog("Mo", dataRequest, dtstart)
                'BEGIN SUB =============================

                If (serviceid = "45040016" And XmlReader.GetElementsByTagName("ussd").Count = 0) Then 'Chain Quiz
                    Try
                        WriteLog("mosms_quiz", dataRequest, dtstart)
                        WriteLog("mosms_quiz", DataResponseSuccess, dtfinish)

                        Dim comm2 As New SqlCommand("MTQuiz", New SqlConnection(My.Settings.DBCon))
                        'Log.info(dr("ServicePMKey"))
                        Dim ResultText As String = ""
                        Dim sqlout As New SqlParameter() With {.Direction = ParameterDirection.Output, .ParameterName = "ResultText"}
                        With comm2
                            .CommandType = CommandType.StoredProcedure
                            .Parameters.Add(New SqlParameter("MSN", msisdn))
                            .Parameters.Add(New SqlParameter("ServicePMKey", 124))

                            .Parameters.Add(New SqlParameter("Choice_Selected", msg))
                            .Parameters.Add(New SqlParameter("ResultText", ""))

                            .Parameters("ResultText").Direction = ParameterDirection.Output
                            .Parameters("ResultText").Size = 300
                            ''66859194778',124,'A',@r output
                            '.Parameters.Add(sqlout)
                        End With
                        comm2.Connection.Open()
                        comm2.ExecuteNonQuery()
                        ResultText = comm2.Parameters("ResultText").Value
                        If (ResultText <> "") Then


                            'Dim textussd As Strin.
                            'textussd = XmlReader.SelectSingleNode("/cpa-mobile-request/ussd/content-id").InnerText
                            'textussd = textussd.Replace("*496*", "").Replace("#", "")
                            Dim texttest As String
                            texttest = "<?xml version=""1.0"" encoding=""utf-8"" ?>"
                            texttest += "<cpa-request>"
                            texttest += "<txid>" + txid + "</txid>"
                            texttest += "<authentication>"
                            texttest += "<user>cheese123</user>"
                            texttest += "<password>S16ACGMSg</password>"
                            texttest += "</authentication>"
                            texttest += "<originator>"
                            texttest += "<sender>4504301</sender>"
                            texttest += "</originator>"
                            texttest += "<destination>"
                            texttest += "<serviceid>45040016</serviceid>"

                            texttest += "<msisdn>" + msisdn + "</msisdn>"

                            'texttest += "<msisdn>66813041744</msisdn>"
                            'texttest += "<msisdn>66859194778</msisdn>"
                            texttest += "</destination>"
                            texttest += "<message>"
                            texttest += "<header>"
                            texttest += "<timestamp>" + Now.ToString("yyyyMMddHHmmss", New System.Globalization.CultureInfo("en-US")) + "</timestamp>"
                            texttest += "</header>"
                            texttest += "<sms>"
                            texttest += "<msg>" + ResultText + "</msg>"
                            texttest += "<msgtype>T</msgtype>"
                            texttest += "<encoding>8</encoding>"
                            texttest += "</sms>"
                            texttest += "</message>"
                            texttest += "</cpa-request>"
                            'IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\mosms_quiz-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & texttest & vbNewLine)
                            WriteLog("mosms_quiz", texttest, Now)
                            '2556-07-15 12:16:46  <?xml version="1.0" encoding="utf-8" ?><cpa-request><txid>461115121652878</txid><authentication><user>cheese123</user><password>S16ACGMSg</password></authentication><originator><sender>66859194778</sender></originator><destination><serviceid>45040262</serviceid><msisdn>66859194778</msisdn></destination><message><header><timestamp>20130715121646</timestamp></header><sms><msg>ทดสอบการส่งข้อความ serviceid = 45040262 ข้อความที่ส่งมาคือ </msg><msgtype>T</msgtype><encoding>8</encoding></sms></message></cpa-request>
                            Dim DataToSend() As Byte = Encoding.UTF8.GetBytes(texttest)
                            Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create("http://202.91.22.240:8319/SAG/services/cpa/sms")

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


                            Else

                                Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                                ResponseData = sReader.ReadToEnd().Trim()
                                'ResponseData = HttpResponse.GetResponseStream.ToString

                            End If

                            'IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\mosms_quiz-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ResponseData & vbNewLine)

                            WriteLog("mosms_quiz", ResponseData, Now)






                            HttpResponse.Close()
                        End If
                    Catch ex As Exception
                        'IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\mosms_quiz-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ex.Message & vbNewLine)
                        'IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\mosms_quiz-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ex.ToString & vbNewLine)
                        WriteLog("mosms_quiz", ex.Message & vbNewLine & ex.ToString, Now, True)

                    End Try
                ElseIf (serviceid = "45040262") Then '4504711 7-11

                    WriteLog("7mosms_dn", dataRequest, dtstart)
                    WriteLog("7mosms_dn", DataResponseSuccess, dtfinish)
                    Dim textussd As String
                    textussd = XmlReader.SelectSingleNode("/cpa-mobile-request/ussd/content-id").InnerText
                    textussd = textussd.Replace("*496*", "").Replace("#", "")
                    Dim texttest As String

                    'WriteLog("7debug-0-ussd", textussd, Now)

                    Dim objCPALL As New CPALL_2014
                    objCPALL.Code = textussd
                    objCPALL.Mobile = msisdn
                    objCPALL.Oper = "DTAC"

                    Dim FULLIP As String = context.Request.ServerVariables("LOCAL_ADDR")

                    objCPALL.IP = FULLIP.Substring(FULLIP.LastIndexOf(".") + 1, FULLIP.Length - FULLIP.LastIndexOf(".") - 1)
                    objCPALL.Decode_Checking()
                    Dim replySMS As String
                    replySMS = objCPALL.ReplyUSSD
                    If (objCPALL.CheckingStatus) Then
                        replySMS = objCPALL.ReplySMS
                    End If


                    texttest = "<?xml version=""1.0"" encoding=""utf-8"" ?>"
                    texttest += "<cpa-request>"
                    texttest += "<txid>" + txid + "</txid>"
                    texttest += "<authentication>"
                    texttest += "<user>cheese123</user>"
                    texttest += "<password>S16ACGMSg</password>"
                    'texttest += "<user>cheesemobile</user>"
                    'texttest += "<password>Huawei@12345</password>"
                    texttest += "</authentication>"
                    texttest += "<originator>"
                    texttest += "<sender>7-Eleven</sender>"
                    texttest += "</originator>"
                    texttest += "<destination>"
                    texttest += "<serviceid>" + serviceid + "</serviceid>"

                    texttest += "<msisdn>" + msisdn + "</msisdn>"

                    'texttest += "<msisdn>66813041744</msisdn>"
                    'texttest += "<msisdn>66859194778</msisdn>"
                    texttest += "</destination>"
                    texttest += "<message>"
                    texttest += "<header>"
                    texttest += "<timestamp>" + Now.ToString("yyyyMMddHHmmss", New System.Globalization.CultureInfo("en-US")) + "</timestamp>"
                    texttest += "</header>"
                    texttest += "<sms>"
                    texttest += "<msg>" + replySMS + "</msg>"
                    'texttest += "<msg>ทดสอบการส่งข้อความ serviceid = " + serviceid + " ข้อความที่ส่งมาคือ " + textussd + "</msg>"
                    texttest += "<msgtype>T</msgtype>"
                    texttest += "<encoding>8</encoding>"
                    texttest += "</sms>"
                    texttest += "</message>"
                    texttest += "</cpa-request>"
                    ' IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\7mosms_dn-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & texttest & vbNewLine)
                    WriteLog("7mosms_dn", texttest, Now)
                    '2556-07-15 12:16:46  <?xml version="1.0" encoding="utf-8" ?><cpa-request><txid>461115121652878</txid><authentication><user>cheese123</user><password>S16ACGMSg</password></authentication><originator><sender>66859194778</sender></originator><destination><serviceid>45040262</serviceid><msisdn>66859194778</msisdn></destination><message><header><timestamp>20130715121646</timestamp></header><sms><msg>ทดสอบการส่งข้อความ serviceid = 45040262 ข้อความที่ส่งมาคือ </msg><msgtype>T</msgtype><encoding>8</encoding></sms></message></cpa-request>
                    Dim DataToSend() As Byte = Encoding.UTF8.GetBytes(texttest)
                    Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create("http://202.91.22.240:8319/SAG/services/cpa/sms")
                    'Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create("http://202.91.21.248:8319/SAG/services/cpa/sms")

                    With HTTPRequest
                        .Timeout = 5000
                        .Method = "POST"
                        .ContentType = "text/xml"

                        '.Proxy = Nothing
                        '.ContentLength = DataToSend.Length
                    End With
                    Try

                        Dim SendHTTPRequest As IO.Stream = HTTPRequest.GetRequestStream()
                        SendHTTPRequest.Write(DataToSend, 0, DataToSend.Length)
                        SendHTTPRequest.Close()
                        Dim HttpResponse As Net.HttpWebResponse = HTTPRequest.GetResponse()
                        Dim ResponseData As String = Nothing
                        If HttpResponse.StatusCode.Equals(Net.HttpStatusCode.OK) Or HttpResponse.StatusCode.Equals(Net.HttpStatusCode.Accepted) Then
                            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                            ResponseData = sReader.ReadToEnd().Trim()


                        Else

                            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                            ResponseData = sReader.ReadToEnd().Trim()
                            'ResponseData = HttpResponse.GetResponseStream.ToString

                        End If
                        WriteLog("7mosms_dn", ResponseData, Now)

                        ' IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\7mosms_dn-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ResponseData & vbNewLine)







                        HttpResponse.Close()

                    Catch ex As Exception
                        ' IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\7mosms_error_dn-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ex.Message & vbNewLine)
                        WriteLog("7mosms_dn", ex.Message & vbNewLine & ex.ToString, Now, True)
                    End Try


                ElseIf (serviceid = "45040280") Then 'catalog

                    WriteLog("7catmosms_dn", dataRequest, dtstart)
                    WriteLog("7catmosms_dn", DataResponseSuccess, dtfinish)
                    Dim textussd As String
                    Try


                        If Not (XmlReader.SelectSingleNode("/cpa-mobile-request/ussd/content-id") Is Nothing) Then
                            textussd = XmlReader.SelectSingleNode("/cpa-mobile-request/ussd/content-id").InnerText
                            textussd = textussd.Replace("*473*", "").Replace("#", "")
                        Else 'send sms
                            textussd = msg
                        End If
                        Dim texttest As String

                        Dim objCPALL As New SEVENCATLOG
                        objCPALL.Code = textussd
                        objCPALL.Mobile = msisdn
                        objCPALL.Oper = "DTAC"

                        Dim FULLIP As String = context.Request.ServerVariables("LOCAL_ADDR")

                        objCPALL.IP = FULLIP.Substring(FULLIP.LastIndexOf(".") + 1, FULLIP.Length - FULLIP.LastIndexOf(".") - 1)
                        objCPALL.Decode_Checking()
                        Dim replySMS As String
                        replySMS = objCPALL.ReplyUSSD
                        If (objCPALL.CheckingStatus) Then
                            replySMS = objCPALL.ReplySMS
                        End If
                        '                    is: “DirectionFlag (fix to 0) + CPID(3) + 
                        'ddhh24mmss(8)+SEQ(3)”,    mean CP send 
                        'request to SDP.
                        'txid = "0504" + Now.ToString("ddHHmmss") + "000"
                        texttest = "<?xml version=""1.0"" encoding=""utf-8"" ?>"
                        texttest += "<cpa-request>"
                        texttest += "<txid>" + txid + "</txid>"
                        texttest += "<authentication>"
                        texttest += "<user>cheese123</user>"
                        texttest += "<password>S16ACGMSg</password>"
                        'texttest += "<user>cheesemobile</user>"
                        'texttest += "<password>Huawei@12345</password>"
                        texttest += "</authentication>"
                        texttest += "<originator>"
                        texttest += "<sender>7-Catalog</sender>"
                        texttest += "</originator>"
                        texttest += "<destination>"
                        texttest += "<serviceid>" + serviceid + "</serviceid>"

                        texttest += "<msisdn>" + msisdn + "</msisdn>"

                        'texttest += "<msisdn>66813041744</msisdn>"
                        'texttest += "<msisdn>66859194778</msisdn>"
                        texttest += "</destination>"
                        texttest += "<message>"
                        texttest += "<header>"
                        texttest += "<timestamp>" + Now.ToString("yyyyMMddHHmmss", New System.Globalization.CultureInfo("en-US")) + "</timestamp>"
                        texttest += "</header>"
                        texttest += "<sms>"
                        texttest += "<msg>" + replySMS + "</msg>"
                        'texttest += "<msg>ทดสอบการส่งข้อความ serviceid = " + serviceid + " ข้อความที่ส่งมาคือ " + textussd + "</msg>"
                        texttest += "<msgtype>T</msgtype>"
                        texttest += "<encoding>8</encoding>"
                        texttest += "</sms>"
                        texttest += "</message>"
                        texttest += "</cpa-request>"
                        ' IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\7mosms_dn-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & texttest & vbNewLine)
                        WriteLog("7catmosms_dn", texttest, Now)
                        '2556-07-15 12:16:46  <?xml version="1.0" encoding="utf-8" ?><cpa-request><txid>461115121652878</txid><authentication><user>cheese123</user><password>S16ACGMSg</password></authentication><originator><sender>66859194778</sender></originator><destination><serviceid>45040262</serviceid><msisdn>66859194778</msisdn></destination><message><header><timestamp>20130715121646</timestamp></header><sms><msg>ทดสอบการส่งข้อความ serviceid = 45040262 ข้อความที่ส่งมาคือ </msg><msgtype>T</msgtype><encoding>8</encoding></sms></message></cpa-request>
                        Dim DataToSend() As Byte = Encoding.UTF8.GetBytes(texttest)
                        Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create("http://202.91.22.240:8319/SAG/services/cpa/sms")
                        'Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create("http://202.91.21.248:8319/SAG/services/cpa/sms")

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


                        Else

                            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                            ResponseData = sReader.ReadToEnd().Trim()
                            'ResponseData = HttpResponse.GetResponseStream.ToString

                        End If
                        WriteLog("7catmosms_dn", ResponseData, Now)

                        ' IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\7mosms_dn-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ResponseData & vbNewLine)







                        HttpResponse.Close()

                    Catch ex As Exception
                        ' IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\7mosms_error_dn-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ex.Message & vbNewLine)
                        WriteLog("7catmosms_dn", ex.Message & vbNewLine & ex.ToString, Now, True)
                    End Try


                ElseIf (serviceid = "45040323") Then 'OISHI

                    WriteLog("7OISHI_dn", dataRequest, dtstart)
                    WriteLog("7OISHI_dn", DataResponseSuccess, dtfinish)
                    Dim textussd As String
                    Try


                        If Not (XmlReader.SelectSingleNode("/cpa-mobile-request/ussd/content-id") Is Nothing) Then
                            textussd = XmlReader.SelectSingleNode("/cpa-mobile-request/ussd/content-id").InnerText
                            textussd = textussd.Replace("*417*", "").Replace("#", "")
                        Else 'send sms
                            textussd = msg
                        End If
                        Dim texttest As String

                        Dim objCPALL As New SEVENOISHI
                        objCPALL.Code = textussd
                        objCPALL.Mobile = msisdn
                        objCPALL.Oper = "DTAC"

                        Dim FULLIP As String = context.Request.ServerVariables("LOCAL_ADDR")

                        objCPALL.IP = FULLIP.Substring(FULLIP.LastIndexOf(".") + 1, FULLIP.Length - FULLIP.LastIndexOf(".") - 1)
                        objCPALL.Decode_Checking()
                        Dim replySMS As String
                        replySMS = objCPALL.ReplyUSSD
                        If (objCPALL.CheckingStatus) Then
                            replySMS = objCPALL.ReplySMS
                        End If
                        '                    is: “DirectionFlag (fix to 0) + CPID(3) + 
                        'ddhh24mmss(8)+SEQ(3)”,    mean CP send 
                        'request to SDP.
                        'txid = "0504" + Now.ToString("ddHHmmss") + "000"
                        texttest = "<?xml version=""1.0"" encoding=""utf-8"" ?>"
                        texttest += "<cpa-request>"
                        texttest += "<txid>" + txid + "</txid>"
                        texttest += "<authentication>"
                        texttest += "<user>cheese123</user>"
                        texttest += "<password>S16ACGMSg</password>"
                        'texttest += "<user>cheesemobile</user>"
                        'texttest += "<password>Huawei@12345</password>"
                        texttest += "</authentication>"
                        texttest += "<originator>"
                        texttest += "<sender>OISHI</sender>"
                        texttest += "</originator>"
                        texttest += "<destination>"
                        texttest += "<serviceid>" + serviceid + "</serviceid>"

                        texttest += "<msisdn>" + msisdn + "</msisdn>"

                        'texttest += "<msisdn>66813041744</msisdn>"
                        'texttest += "<msisdn>66859194778</msisdn>"
                        texttest += "</destination>"
                        texttest += "<message>"
                        texttest += "<header>"
                        texttest += "<timestamp>" + Now.ToString("yyyyMMddHHmmss", New System.Globalization.CultureInfo("en-US")) + "</timestamp>"
                        texttest += "</header>"
                        texttest += "<sms>"
                        texttest += "<msg>" + replySMS + "</msg>"
                        'texttest += "<msg>ทดสอบการส่งข้อความ OISHI รวยฟ้าผ่า</msg>"
                        texttest += "<msgtype>T</msgtype>"
                        texttest += "<encoding>8</encoding>"
                        texttest += "</sms>"
                        texttest += "</message>"
                        texttest += "</cpa-request>"
                        ' IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\7mosms_dn-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & texttest & vbNewLine)
                        WriteLog("7catmosms_dn", texttest, Now)
                        '2556-07-15 12:16:46  <?xml version="1.0" encoding="utf-8" ?><cpa-request><txid>461115121652878</txid><authentication><user>cheese123</user><password>S16ACGMSg</password></authentication><originator><sender>66859194778</sender></originator><destination><serviceid>45040262</serviceid><msisdn>66859194778</msisdn></destination><message><header><timestamp>20130715121646</timestamp></header><sms><msg>ทดสอบการส่งข้อความ serviceid = 45040262 ข้อความที่ส่งมาคือ </msg><msgtype>T</msgtype><encoding>8</encoding></sms></message></cpa-request>
                        Dim DataToSend() As Byte = Encoding.UTF8.GetBytes(texttest)
                        Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create("http://202.91.22.240:8319/SAG/services/cpa/sms")
                        'Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create("http://202.91.21.248:8319/SAG/services/cpa/sms")

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


                        Else

                            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                            ResponseData = sReader.ReadToEnd().Trim()
                            'ResponseData = HttpResponse.GetResponseStream.ToString

                        End If
                        WriteLog("7catmosms_dn", ResponseData, Now)

                        ' IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\7mosms_dn-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ResponseData & vbNewLine)







                        HttpResponse.Close()

                    Catch ex As Exception
                        ' IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\7mosms_error_dn-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ex.Message & vbNewLine)
                        WriteLog("7catmosms_dn", ex.Message & vbNewLine & ex.ToString, Now, True)
                    End Try


                ElseIf (serviceid = "45040005") Then 'SMS2WAY 4504333
                    Dim sender As String = "4504333"
                    WriteLog("mosms_2way", dataRequest, dtstart)
                    WriteLog("mosms_2way", DataResponseSuccess, dtfinish)
                    WriteLog("mosms_2wayDB", dataRequest, dtstart)
                    Dim textreply As String = ""

                    'If (msg.ToUpper.StartsWith("SBN")) Then
                    '    Dim DataToRequest() As Byte = Encoding.UTF8.GetBytes("code=" & msg.ToUpper.Replace("SBN", "").Replace(" ", "") & "&msisdn=" & msisdn & "&telco=D")
                    '    Dim HTTPReq As Net.HttpWebRequest = Net.WebRequest.Create("http://192.168.0.22/sms2way/sabina.ashx")

                    '    With HTTPReq
                    '        .Timeout = 5000
                    '        .Method = "POST"
                    '        .ContentType = "application/x-www-form-urlencoded"
                    '    End With
                    '    Dim ResponseData As String = "ขออภัย ท่านส่งข้อความไม่ถูกต้อง"
                    '    Try

                    '        Dim SendHTTPRequest As IO.Stream = HTTPReq.GetRequestStream()
                    '        SendHTTPRequest.Write(DataToRequest, 0, DataToRequest.Length)
                    '        SendHTTPRequest.Close()
                    '        Dim HttpResponse As Net.HttpWebResponse = HTTPReq.GetResponse()
                    '        If HttpResponse.StatusCode.Equals(Net.HttpStatusCode.OK) Or HttpResponse.StatusCode.Equals(Net.HttpStatusCode.Accepted) Then
                    '            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                    '            ResponseData = sReader.ReadToEnd().Trim()
                    '        Else
                    '            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                    '            ResponseData = sReader.ReadToEnd().Trim()
                    '        End If

                    '        WriteLog("mosms_2way", ResponseData, Now)
                    '        HttpResponse.Close()

                    '    Catch ex As Exception
                    '        WriteLog("mosms_2way", ex.Message & vbNewLine & ex.ToString, Now, True)
                    '    End Try
                    '    textreply = ResponseData
                    'ElseIf (msg.ToUpper.StartsWith("TL")) Then
                    '    Dim strDate As String() = Now.ToString("yyyy-M-d", New System.Globalization.CultureInfo("en-US")).Split("-")
                    '    Dim sYear As Integer = CInt(strDate(0))
                    '    Dim sMonth As Integer = CInt(strDate(1))
                    '    Dim sDay As Integer = CInt(strDate(2))

                    '    If sMonth <= 10 And sYear = 2013 Then
                    '        If sMonth = 10 Then
                    '            If sDay <= 10 Then 'And Hour(FormatDateTime(time(),4))>20
                    '                textreply = "ขอบคุณที่ร่วมแสดงความคิดเห็น ติดตามประกาศผู้โชคดีในวันที่ 8 ต.ค.นี้"
                    '            Else
                    '                textreply = "ขออภัยขณะนี้ได้สิ้นสุดการร่วมแสดงความคิดเห็น"
                    '            End If
                    '        Else
                    '            textreply = "ขอบคุณที่ร่วมแสดงความคิดเห็น ติดตามประกาศผู้โชคดีในวันที่ 8 ต.ค.นี้"
                    '        End If
                    '    Else
                    '        textreply = "ขออภัยขณะนี้ได้สิ้นสุดการร่วมแสดงความคิดเห็น"
                    '    End If
                    '    WriteLog("mosms_2way", textreply, Now)
                    'Else
                    textreply = "ขออภัย ท่านส่งข้อความไม่ถูกต้อง"
                    Dim strCond As String
                    If (XmlReader.GetElementsByTagName("ivr").Count > 0) Then ' Pass IVR
                        shortcode = XmlReader.SelectSingleNode("/cpa-mobile-request/ivr/from").InnerText
                        msg = Right(msg, 2)
                        strCond = String.Format(" ShortCode='{0}' and (ivrRegister = '{1}' or ivrUnRegister='{1}') ", shortcode, msg)
                    ElseIf (XmlReader.GetElementsByTagName("ussd").Count > 0) Then 'USSD
                        shortcode = XmlReader.SelectSingleNode("/cpa-mobile-request/destination/msisdn").InnerText
                        strCond = String.Format(" USSD='{0}' ", XmlReader.SelectSingleNode("/cpa-mobile-request/ussd/content-id").InnerText)
                    Else ' SMS
                        shortcode = XmlReader.SelectSingleNode("/cpa-mobile-request/destination/msisdn").InnerText
                        strCond = String.Format(" Shortcode='{0}' and (CommandRegister = '{1}' or CommandUnRegister='{1}') ", shortcode, msg)
                    End If
                    'localhost:3650/morequest.ashx?txid=20130829172300&serviceid=4504333&msisdn=66xxxxxxxxx&message=SBN123456798&telco=AIS&timestamp=20130829170000
                    Dim timestamp As String = XmlReader.SelectSingleNode("/cpa-mobile-request/message/header/timestamp").InnerText
                    Dim DataToRequest() As Byte = Encoding.UTF8.GetBytes("txid=" & txid & "&serviceid=" & serviceid & "&shortcode=" & shortcode & "&message=" & msg & "&msisdn=" & msisdn & "&telco=DTAC&timestamp=" & timestamp & "&ip=" & context.Request.ServerVariables("LOCAL_ADDR"))
                    Dim HTTPReq As Net.HttpWebRequest = Net.WebRequest.Create("http://192.168.0.22/sms2way/morequest.ashx")

                    With HTTPReq
                        .Timeout = 1000 * 60
                        .Method = "POST"
                        .ContentType = "application/x-www-form-urlencoded"
                    End With
                    Dim ResponseDataREQ As String = ""
                    Try

                        Dim SendHTTPRequest As IO.Stream = HTTPReq.GetRequestStream()
                        SendHTTPRequest.Write(DataToRequest, 0, DataToRequest.Length)
                        SendHTTPRequest.Close()
                        Dim HttpResponse As Net.HttpWebResponse = HTTPReq.GetResponse()
                        If HttpResponse.StatusCode.Equals(Net.HttpStatusCode.OK) Or HttpResponse.StatusCode.Equals(Net.HttpStatusCode.Accepted) Then
                            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                            ResponseDataREQ = sReader.ReadToEnd().Trim()
                        Else
                            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                            ResponseDataREQ = sReader.ReadToEnd().Trim()
                        End If
                        WriteLog("mosms_2way", ResponseDataREQ, Now)
                        HttpResponse.Close()
                        Dim xXmlReader As New XmlDocument
                        xXmlReader.LoadXml(ResponseDataREQ)
                        '<?xml version="1.0" encoding="utf-8" ?><XML><TRANID>20130829172300</TRANID><MSISDN>66xxxxxxxxx</MSISDN><SENDER>4504333</SENDER><MESSAGE>ขออภัยไม่มีบริการค่ะ</MESSAGE></XML>
                        textreply = xXmlReader.SelectSingleNode("/XML/MESSAGE").InnerText
                        sender = xXmlReader.SelectSingleNode("/XML/SENDER").InnerText

                    Catch ex As Exception
                        WriteLog("mosms_2way", ex.Message & vbNewLine & ex.ToString, Now, True)
                    End Try
                    'End If
                    Dim texttest As String
                    texttest = "<?xml version=""1.0"" encoding=""utf-8"" ?>"
                    texttest += "<cpa-request>"
                    texttest += "<txid>" + txid + "</txid>"
                    texttest += "<authentication>"
                    texttest += "<user>cheese123</user>"
                    texttest += "<password>S16ACGMSg</password>"
                    texttest += "</authentication>"
                    texttest += "<originator>"
                    texttest += "<sender>" & sender & "</sender>"
                    texttest += "</originator>"
                    texttest += "<destination>"
                    texttest += "<serviceid>" + serviceid + "</serviceid>"
                    texttest += "<msisdn>" + msisdn + "</msisdn>"
                    texttest += "</destination>"
                    texttest += "<message>"
                    texttest += "<header>"
                    texttest += "<timestamp>" + Now.ToString("yyyyMMddHHmmss", New System.Globalization.CultureInfo("en-US")) + "</timestamp>"
                    texttest += "</header>"
                    texttest += "<sms>"
                    texttest += "<msg>" + textreply + "</msg>"
                    texttest += "<msgtype>T</msgtype>"
                    texttest += "<encoding>8</encoding>"
                    texttest += "</sms>"
                    texttest += "</message>"
                    texttest += "</cpa-request>"

                    WriteLog("mosms_2way", texttest, Now)
                    Dim DataToSend() As Byte = Encoding.UTF8.GetBytes(texttest)
                    Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create("http://202.91.22.240:8319/SAG/services/cpa/sms")

                    With HTTPRequest
                        .Timeout = 5000
                        .Method = "POST"
                        .ContentType = "text/xml"
                    End With
                    Try
                        Dim SendHTTPRequest As IO.Stream = HTTPRequest.GetRequestStream()
                        SendHTTPRequest.Write(DataToSend, 0, DataToSend.Length)
                        SendHTTPRequest.Close()
                        Dim HttpResponse As Net.HttpWebResponse = HTTPRequest.GetResponse()
                        Dim ResponseData As String = Nothing
                        If HttpResponse.StatusCode.Equals(Net.HttpStatusCode.OK) Or HttpResponse.StatusCode.Equals(Net.HttpStatusCode.Accepted) Then
                            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                            ResponseData = sReader.ReadToEnd().Trim()
                        Else
                            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                            ResponseData = sReader.ReadToEnd().Trim()
                        End If
                        WriteLog("mosms_2way", ResponseData, Now)
                        HttpResponse.Close()
                    Catch ex As Exception
                        WriteLog("mosms_2way", ex.Message & vbNewLine & ex.ToString, Now, True)
                    End Try

                ElseIf (serviceid = "45040387") Then
                    Dim sender As String = "4504222"
                    WriteLog("mosms_2way", dataRequest, dtstart)
                    WriteLog("mosms_2way", DataResponseSuccess, dtfinish)
                    WriteLog("mosms_2wayDB", dataRequest, dtstart)
                    Dim textreply As String = ""

                    textreply = "ขออภัย ท่านส่งข้อความไม่ถูกต้อง"
                    Dim strCond As String
                    If (XmlReader.GetElementsByTagName("ivr").Count > 0) Then ' Pass IVR
                        shortcode = XmlReader.SelectSingleNode("/cpa-mobile-request/ivr/from").InnerText
                        msg = Right(msg, 2)
                        strCond = String.Format(" ShortCode='{0}' and (ivrRegister = '{1}' or ivrUnRegister='{1}') ", shortcode, msg)
                    ElseIf (XmlReader.GetElementsByTagName("ussd").Count > 0) Then 'USSD
                        shortcode = XmlReader.SelectSingleNode("/cpa-mobile-request/destination/msisdn").InnerText
                        strCond = String.Format(" USSD='{0}' ", XmlReader.SelectSingleNode("/cpa-mobile-request/ussd/content-id").InnerText)
                    Else ' SMS
                        shortcode = XmlReader.SelectSingleNode("/cpa-mobile-request/destination/msisdn").InnerText
                        strCond = String.Format(" Shortcode='{0}' and (CommandRegister = '{1}' or CommandUnRegister='{1}') ", shortcode, msg)
                    End If

                    Dim timestamp As String = XmlReader.SelectSingleNode("/cpa-mobile-request/message/header/timestamp").InnerText
                    Dim DataToRequest() As Byte = Encoding.UTF8.GetBytes("txid=" & txid & "&serviceid=" & serviceid & "&shortcode=" & shortcode & "&message=" & msg & "&msisdn=" & msisdn & "&telco=DTAC&timestamp=" & timestamp & "&ip=" & context.Request.ServerVariables("LOCAL_ADDR"))
                    Dim HTTPReq As Net.HttpWebRequest = Net.WebRequest.Create("http://192.168.0.22/sms2way/morequest.ashx")

                    With HTTPReq
                        .Timeout = 1000 * 60
                        .Method = "POST"
                        .ContentType = "application/x-www-form-urlencoded"
                    End With
                    Dim ResponseDataREQ As String = ""
                    Try

                        Dim SendHTTPRequest As IO.Stream = HTTPReq.GetRequestStream()
                        SendHTTPRequest.Write(DataToRequest, 0, DataToRequest.Length)
                        SendHTTPRequest.Close()
                        Dim HttpResponse As Net.HttpWebResponse = HTTPReq.GetResponse()
                        If HttpResponse.StatusCode.Equals(Net.HttpStatusCode.OK) Or HttpResponse.StatusCode.Equals(Net.HttpStatusCode.Accepted) Then
                            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                            ResponseDataREQ = sReader.ReadToEnd().Trim()
                        Else
                            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                            ResponseDataREQ = sReader.ReadToEnd().Trim()
                        End If
                        WriteLog("mosms_2way", ResponseDataREQ, Now)
                        HttpResponse.Close()
                        Dim xXmlReader As New XmlDocument
                        xXmlReader.LoadXml(ResponseDataREQ)

                        textreply = xXmlReader.SelectSingleNode("/XML/MESSAGE").InnerText
                        sender = xXmlReader.SelectSingleNode("/XML/SENDER").InnerText

                    Catch ex As Exception
                        WriteLog("mosms_2way", ex.Message & vbNewLine & ex.ToString, Now, True)
                    End Try
                    'End If
                    Dim texttest As String
                    texttest = "<?xml version=""1.0"" encoding=""utf-8"" ?>"
                    texttest += "<cpa-request>"
                    texttest += "<txid>" + txid + "</txid>"
                    texttest += "<authentication>"
                    texttest += "<user>cheese123</user>"
                    texttest += "<password>S16ACGMSg</password>"
                    texttest += "</authentication>"
                    texttest += "<originator>"
                    texttest += "<sender>" & sender & "</sender>"
                    texttest += "</originator>"
                    texttest += "<destination>"
                    texttest += "<serviceid>" + serviceid + "</serviceid>"
                    texttest += "<msisdn>" + msisdn + "</msisdn>"
                    texttest += "</destination>"
                    texttest += "<message>"
                    texttest += "<header>"
                    texttest += "<timestamp>" + Now.ToString("yyyyMMddHHmmss", New System.Globalization.CultureInfo("en-US")) + "</timestamp>"
                    texttest += "</header>"
                    texttest += "<sms>"
                    texttest += "<msg>" + textreply + "</msg>"
                    texttest += "<msgtype>T</msgtype>"
                    texttest += "<encoding>8</encoding>"
                    texttest += "</sms>"
                    texttest += "</message>"
                    texttest += "</cpa-request>"

                    WriteLog("mosms_2way", texttest, Now)
                    Dim DataToSend() As Byte = Encoding.UTF8.GetBytes(texttest)
                    Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create("http://202.91.22.240:8319/SAG/services/cpa/sms")

                    With HTTPRequest
                        .Timeout = 5000
                        .Method = "POST"
                        .ContentType = "text/xml"
                    End With
                    Try
                        Dim SendHTTPRequest As IO.Stream = HTTPRequest.GetRequestStream()
                        SendHTTPRequest.Write(DataToSend, 0, DataToSend.Length)
                        SendHTTPRequest.Close()
                        Dim HttpResponse As Net.HttpWebResponse = HTTPRequest.GetResponse()
                        Dim ResponseData As String = Nothing
                        If HttpResponse.StatusCode.Equals(Net.HttpStatusCode.OK) Or HttpResponse.StatusCode.Equals(Net.HttpStatusCode.Accepted) Then
                            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                            ResponseData = sReader.ReadToEnd().Trim()
                        Else
                            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                            ResponseData = sReader.ReadToEnd().Trim()
                        End If
                        WriteLog("mosms_2way", ResponseData, Now)
                        HttpResponse.Close()
                    Catch ex As Exception
                        WriteLog("mosms_2way", ex.Message & vbNewLine & ex.ToString, Now, True)
                    End Try

                ElseIf (serviceid = "45040004") Then '4504094 SMS2WAY
                    Dim sender As String = "4504094"
                    WriteLog("mosms_2way", dataRequest, dtstart)
                    WriteLog("mosms_2way", DataResponseSuccess, dtfinish)
                    WriteLog("mosms_2wayDB", dataRequest, dtstart)
                    'Dim textussd As String
                    'textussd = XmlReader.SelectSingleNode("/cpa-mobile-request/ussd/content-id").InnerText
                    'textussd = textussd.Replace("*496*", "").Replace("#", "")
                    Dim texttest As String
                    Dim textreply As String = ""

                    'If (msg.ToUpper = "MSTD") Then
                    '    textreply = "แสดงข้อความนี้ที่หน้างานพร้อมพาเพื่อนมาทดลองขับเพื่อรับService Voucher"
                    'Else
                    '    textreply = "ขออภัย ท่านส่งข้อความไม่ถูกต้อง"

                    'End If
                    'If (msg.ToUpper.StartsWith("MSTD")) Then
                    '    Dim DataToRequest() As Byte = Encoding.UTF8.GetBytes("code=" & msg.ToUpper & "&msisdn=" & msisdn & "&telco=D&ip=" & context.Request.ServerVariables("HTTP_HOST"))
                    '    Dim HTTPReq As Net.HttpWebRequest = Net.WebRequest.Create("http://192.168.0.22/sms2way/ford_mstd.ashx")

                    '    With HTTPReq
                    '        .Timeout = 5000
                    '        .Method = "POST"
                    '        .ContentType = "application/x-www-form-urlencoded"
                    '    End With
                    '    Dim ResponseData As String = "ขออภัย เบอร์โทรศัพท์ของคุณไม่ได้อยู่ในฐานข้อมูลลูกค้าฟอร์ด"
                    '    Try

                    '        Dim SendHTTPRequest As IO.Stream = HTTPReq.GetRequestStream()
                    '        SendHTTPRequest.Write(DataToRequest, 0, DataToRequest.Length)
                    '        SendHTTPRequest.Close()
                    '        Dim HttpResponse As Net.HttpWebResponse = HTTPReq.GetResponse()
                    '        If HttpResponse.StatusCode.Equals(Net.HttpStatusCode.OK) Or HttpResponse.StatusCode.Equals(Net.HttpStatusCode.Accepted) Then
                    '            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                    '            ResponseData = sReader.ReadToEnd().Trim()
                    '        Else
                    '            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                    '            ResponseData = sReader.ReadToEnd().Trim()
                    '        End If

                    '        WriteLog("mosms_2way", ResponseData, Now)
                    '        HttpResponse.Close()

                    '    Catch ex As Exception
                    '        WriteLog("mosms_2way", ex.Message & vbNewLine & ex.ToString, Now, True)
                    '    End Try
                    '    textreply = ResponseData
                    'Else
                    textreply = "ขออภัย ท่านส่งข้อความไม่ถูกต้อง"
                    Dim strCond As String
                    If (XmlReader.GetElementsByTagName("ivr").Count > 0) Then ' Pass IVR
                        shortcode = XmlReader.SelectSingleNode("/cpa-mobile-request/ivr/from").InnerText
                        msg = Right(msg, 2)
                        strCond = String.Format(" ShortCode='{0}' and (ivrRegister = '{1}' or ivrUnRegister='{1}') ", shortcode, msg)
                    ElseIf (XmlReader.GetElementsByTagName("ussd").Count > 0) Then 'USSD
                        shortcode = XmlReader.SelectSingleNode("/cpa-mobile-request/destination/msisdn").InnerText
                        strCond = String.Format(" USSD='{0}' ", XmlReader.SelectSingleNode("/cpa-mobile-request/ussd/content-id").InnerText)
                    Else ' SMS
                        shortcode = XmlReader.SelectSingleNode("/cpa-mobile-request/destination/msisdn").InnerText
                        strCond = String.Format(" Shortcode='{0}' and (CommandRegister = '{1}' or CommandUnRegister='{1}') ", shortcode, msg)
                    End If
                    'localhost:3650/morequest.ashx?txid=20130829172300&serviceid=4504333&msisdn=66xxxxxxxxx&message=SBN123456798&telco=AIS&timestamp=20130829170000
                    Dim timestamp As String = XmlReader.SelectSingleNode("/cpa-mobile-request/message/header/timestamp").InnerText
                    Dim DataToRequest() As Byte = Encoding.UTF8.GetBytes("txid=" & txid & "&serviceid=" & serviceid & "&shortcode=" & shortcode & "&message=" & msg & "&msisdn=" & msisdn & "&telco=DTAC&timestamp=" & timestamp & "&ip=" & context.Request.ServerVariables("LOCAL_ADDR"))
                    Dim HTTPReq As Net.HttpWebRequest = Net.WebRequest.Create("http://192.168.0.22/sms2way/morequest.ashx")

                    With HTTPReq
                        .Timeout = 1000 * 60
                        .Method = "POST"
                        .ContentType = "application/x-www-form-urlencoded"
                    End With
                    Dim ResponseDataREQ As String = ""
                    Try

                        Dim SendHTTPRequest As IO.Stream = HTTPReq.GetRequestStream()
                        SendHTTPRequest.Write(DataToRequest, 0, DataToRequest.Length)
                        SendHTTPRequest.Close()
                        Dim HttpResponse As Net.HttpWebResponse = HTTPReq.GetResponse()
                        If HttpResponse.StatusCode.Equals(Net.HttpStatusCode.OK) Or HttpResponse.StatusCode.Equals(Net.HttpStatusCode.Accepted) Then
                            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                            ResponseDataREQ = sReader.ReadToEnd().Trim()
                        Else
                            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                            ResponseDataREQ = sReader.ReadToEnd().Trim()
                        End If
                        WriteLog("mosms_2way", ResponseDataREQ, Now)
                        HttpResponse.Close()
                        Dim xXmlReader As New XmlDocument
                        xXmlReader.LoadXml(ResponseDataREQ)
                        '<?xml version="1.0" encoding="utf-8" ?><XML><TRANID>20130829172300</TRANID><MSISDN>66xxxxxxxxx</MSISDN><SENDER>4504333</SENDER><MESSAGE>ขออภัยไม่มีบริการค่ะ</MESSAGE></XML>
                        textreply = xXmlReader.SelectSingleNode("/XML/MESSAGE").InnerText
                        sender = xXmlReader.SelectSingleNode("/XML/SENDER").InnerText

                    Catch ex As Exception
                        WriteLog("mosms_2way", ex.Message & vbNewLine & ex.ToString, Now, True)
                    End Try
                    'End If

                    texttest = "<?xml version=""1.0"" encoding=""utf-8"" ?>"
                    texttest += "<cpa-request>"
                    texttest += "<txid>" + txid + "</txid>"
                    texttest += "<authentication>"
                    texttest += "<user>cheese123</user>"
                    texttest += "<password>S16ACGMSg</password>"
                    'texttest += "<user>cheesemobile</user>"
                    'texttest += "<password>Huawei@12345</password>"
                    texttest += "</authentication>"
                    texttest += "<originator>"
                    texttest += "<sender>" & sender & "</sender>"
                    texttest += "</originator>"
                    texttest += "<destination>"
                    texttest += "<serviceid>" + serviceid + "</serviceid>"

                    texttest += "<msisdn>" + msisdn + "</msisdn>"

                    'texttest += "<msisdn>66813041744</msisdn>"
                    'texttest += "<msisdn>66859194778</msisdn>"
                    texttest += "</destination>"
                    texttest += "<message>"
                    texttest += "<header>"
                    texttest += "<timestamp>" + Now.ToString("yyyyMMddHHmmss", New System.Globalization.CultureInfo("en-US")) + "</timestamp>"
                    texttest += "</header>"
                    texttest += "<sms>"
                    texttest += "<msg>" + textreply + "</msg>"
                    'texttest += "<msg>ทดสอบการส่งข้อความ serviceid = " + serviceid + " ข้อความที่ส่งมาคือ " + textussd + "</msg>"
                    texttest += "<msgtype>T</msgtype>"
                    texttest += "<encoding>8</encoding>"
                    texttest += "</sms>"
                    texttest += "</message>"
                    texttest += "</cpa-request>"
                    'IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\mosms_2way-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & texttest & vbNewLine)
                    'IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\mosms_2wayDB-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & texttest & vbNewLine)
                    WriteLog("mosms_2way", texttest, Now)
                    'mosms_2wayDB()
                    '2556-07-15 12:16:46  <?xml version="1.0" encoding="utf-8" ?><cpa-request><txid>461115121652878</txid><authentication><user>cheese123</user><password>S16ACGMSg</password></authentication><originator><sender>66859194778</sender></originator><destination><serviceid>45040262</serviceid><msisdn>66859194778</msisdn></destination><message><header><timestamp>20130715121646</timestamp></header><sms><msg>ทดสอบการส่งข้อความ serviceid = 45040262 ข้อความที่ส่งมาคือ </msg><msgtype>T</msgtype><encoding>8</encoding></sms></message></cpa-request>
                    Dim DataToSend() As Byte = Encoding.UTF8.GetBytes(texttest)
                    Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create("http://202.91.22.240:8319/SAG/services/cpa/sms")
                    'Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create("http://202.91.21.248:8319/SAG/services/cpa/sms")

                    With HTTPRequest
                        .Timeout = 5000
                        .Method = "POST"
                        .ContentType = "text/xml"

                        '.Proxy = Nothing
                        '.ContentLength = DataToSend.Length
                    End With
                    Try

                        Dim SendHTTPRequest As IO.Stream = HTTPRequest.GetRequestStream()
                        SendHTTPRequest.Write(DataToSend, 0, DataToSend.Length)
                        SendHTTPRequest.Close()
                        Dim HttpResponse As Net.HttpWebResponse = HTTPRequest.GetResponse()
                        Dim ResponseData As String = Nothing
                        If HttpResponse.StatusCode.Equals(Net.HttpStatusCode.OK) Or HttpResponse.StatusCode.Equals(Net.HttpStatusCode.Accepted) Then
                            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                            ResponseData = sReader.ReadToEnd().Trim()


                        Else

                            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                            ResponseData = sReader.ReadToEnd().Trim()
                            'ResponseData = HttpResponse.GetResponseStream.ToString

                        End If

                        ' IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\mosms_2way-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ResponseData & vbNewLine)
                        'IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\mosms_2wayDB-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ResponseData & vbNewLine)
                        WriteLog("mosms_2way", ResponseData, Now)


                        ' mosms_2wayDB()
                        HttpResponse.Close()

                    Catch ex As Exception
                        'IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\mosms_2way_error-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ex.Message & vbNewLine)
                        WriteLog("mosms_2way", ex.Message & vbNewLine & ex.ToString, Now, True)

                    End Try
                Else 'subscription

                    WriteLog("subscription", dataRequest, dtstart)
                    WriteLog("subscription", DataResponseSuccess, dtfinish)



                    Dim strCond As String
                    If (XmlReader.GetElementsByTagName("ivr").Count > 0) Then ' Pass IVR
                        shortcode = XmlReader.SelectSingleNode("/cpa-mobile-request/ivr/from").InnerText
                        msg = Right(msg, 2)
                        strCond = String.Format(" ShortCode='{0}' and (ivrRegister = '{1}' or ivrUnRegister='{1}') ", shortcode, msg)
                    ElseIf (XmlReader.GetElementsByTagName("ussd").Count > 0) Then 'USSD
                        shortcode = XmlReader.SelectSingleNode("/cpa-mobile-request/destination/msisdn").InnerText
                        strCond = String.Format(" USSD='{0}' ", XmlReader.SelectSingleNode("/cpa-mobile-request/ussd/content-id").InnerText)
                    Else ' SMS
                        shortcode = XmlReader.SelectSingleNode("/cpa-mobile-request/destination/msisdn").InnerText
                        strCond = String.Format(" Shortcode='{0}' and (CommandRegister = '{1}' or CommandUnRegister='{1}') ", shortcode, msg)
                    End If


                    Dim con As New SqlConnection(My.Settings.DBCon)
                    Dim comm As New SqlCommand
                    comm.Connection = con
                    comm.CommandText = String.Format("select * from services where {0}", strCond)

                    con.Open()
                    Dim dr As SqlDataReader = comm.ExecuteReader()


                    If (dr.HasRows) Then ' Subscription
                        dr.Read()

                        Dim txtxml As String
                        Dim command As String = ""
                        If (msg.ToUpper = dr("CommandRegister").ToString.ToUpper Or msg.ToUpper = dr("IvrRegister").ToString.ToUpper Or XmlReader.GetElementsByTagName("ussd").Count > 0) Then
                            command = "register"
                            url = My.Settings.Reg

                        ElseIf (msg.ToUpper = dr("CommandUnRegister").ToString.ToUpper Or msg.ToUpper = dr("IvrUnRegister").ToString.ToUpper) Then
                            command = "unregister"
                            url = My.Settings.UnReg
                        End If
                        ''no = Right(msg, 1)
                        productid = dr("PRODUCTID").ToString

                        txtxml = "<?xml version=""1.0"" encoding=""UTF-8"" ?>"
                        txtxml += String.Format("<cpa-{0}>", command)
                        txtxml += String.Format("<txid>{0}</txid>", txid)
                        txtxml += "<org-type>107</org-type>"
                        txtxml += "<authentication>"
                        txtxml += String.Format("<user>{0}</user>", My.Settings.MTUser)
                        txtxml += String.Format("<password>{0}</password>", My.Settings.MTPass)
                        txtxml += "</authentication>"
                        txtxml += "<originator>"
                        txtxml += "<sender></sender>"
                        txtxml += "</originator>"
                        txtxml += "<destination>"
                        txtxml += String.Format("<productid>{0}</productid>", productid)
                        txtxml += String.Format("<msisdn>{0}</msisdn>", msisdn)
                        txtxml += "</destination>"
                        txtxml += String.Format("</cpa-{0}>", command)
                        Dim DataToSend() As Byte = Encoding.Default.GetBytes(txtxml)

                        WriteLog("subscription", txtxml, Now)

                        'Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create("http://202.91.21.248:8319/SAG/services/cpa/register")
                        'Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create("http://202.91.21.248:8319/SAG/services/cpa/unregister")
                        Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create(url)

                        With HTTPRequest
                            .Timeout = 5000
                            .Method = "POST"
                            .ContentType = "text/xml"
                            '.Proxy = Nothing
                            '.ContentLength = DataToSend.Length
                        End With


                        Try

                            Dim SendHTTPRequest As IO.Stream = HTTPRequest.GetRequestStream()
                            SendHTTPRequest.Write(DataToSend, 0, DataToSend.Length)
                            SendHTTPRequest.Close()
                            Dim HttpResponse As Net.HttpWebResponse = HTTPRequest.GetResponse()
                            Dim ResponseData As String = Nothing
                            If HttpResponse.StatusCode.Equals(Net.HttpStatusCode.OK) Or HttpResponse.StatusCode.Equals(Net.HttpStatusCode.Accepted) Then
                                Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                                ResponseData = sReader.ReadToEnd().Trim()
                                Dim comm1 As New SqlCommand("AddUpdateActive_User", New SqlConnection(My.Settings.DBCon))
                                'Log.info(dr("ServicePMKey"))
                                With comm1
                                    .CommandType = CommandType.StoredProcedure

                                    .Parameters.Add(New SqlParameter("ServicePMKey", dr("ServicePMKey").ToString))
                                    .Parameters.Add(New SqlParameter("MSN", msisdn))
                                    .Parameters.Add(New SqlParameter("Command", command))


                                End With
                                comm1.Connection.Open()
                                comm1.ExecuteNonQuery()
                                If (Not IsDBNull(dr("FreeFirstLink")) And command = "register") Then
                                    sendwaplinkbycheese(dr("FreeFirstLink").ToString, msisdn, dr("Sender").ToString)
                                End If

                            Else

                                Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
                                ResponseData = sReader.ReadToEnd().Trim()
                                'ResponseData = HttpResponse.GetResponseStream.ToString

                            End If

                            WriteLog("subscription", ResponseData, Now)



                        Catch ex As Exception
                            WriteLog("subscription", ex.Message & vbNewLine & ex.ToString, Now, True)
                        End Try
                        'END SUB =====================
                    End If
                End If

            ElseIf (XmlReader.SelectNodes("/cpa-subscription-status-reply").Count > 0) Then ' Case SDP Control Command is just notification CP
                '2556-11-06 15:44:03 <?xml version="1.0" encoding="utf-8" ?><cpa-subscription-status-reply><txid>200206084334570</txid><destination><productid>45040326001</productid></destination><subscription-status><msisdn>66859194778</msisdn><status>E</status><status-code>200</status-code><status-description>Success</status-description><start-date>20131106</start-date><expired-date>20131106</expired-date><unregister><org-type>4</org-type><accesscode>*450482191</accesscode></unregister></subscription-status></cpa-subscription-status-reply>
                '2556-11-06 16:16:32 <?xml version="1.0" encoding="utf-8" ?><cpa-subscription-status-reply><txid>200106091603836</txid><destination><productid>45040326001</productid></destination><subscription-status><msisdn>66859194778</msisdn><status>A</status><status-code>200</status-code><status-description>Success</status-description><start-date>20131106</start-date><expired-date>20131206</expired-date><register><org-type>4</org-type><accesscode>*450482101</accesscode></register></subscription-status></cpa-subscription-status-reply>
                txid = XmlReader.SelectSingleNode("/cpa-subscription-status-reply/txid").InnerText
                msisdn = XmlReader.SelectSingleNode("/cpa-subscription-status-reply/subscription-status/msisdn").InnerText
                'msgtype = XmlReader.SelectSingleNode("/cpa-mobile-request/message/sms/msgtype").InnerText
                'msg = XmlReader.SelectSingleNode("/cpa-mobile-request/message/sms/msg").InnerText
                ' If msgtype = "H" Then
                'msg = MessageDecode(msg)
                'End If
                productid = XmlReader.SelectSingleNode("/cpa-subscription-status-reply/destination/productid").InnerText
                Dim DataResponseSuccess As String = "<cpa-response><txid>" + txid + "</txid><status>200</status><status-description>Success</status-description></cpa-response>"
                context.Response.ContentType = "text/xml"
                context.Response.Write(DataResponseSuccess)
                context.Response.Flush()
                dtfinish = Now
                WriteLog("Mo", dataRequest, dtstart)





                WriteLog("subscription", dataRequest, dtstart)
                WriteLog("subscription", DataResponseSuccess, dtfinish)



                Dim strCond As String
                strCond = " ProductID='" + productid + "'"
                Dim strRegisterType As String = "unknow"
                If (XmlReader.GetElementsByTagName("register").Count > 0) Then
                    strRegisterType = "register"
                ElseIf (XmlReader.GetElementsByTagName("unregister").Count > 0) Then
                    strRegisterType = "unregister"
                End If
                'If (XmlReader.GetElementsByTagName("ivr").Count > 0) Then ' Pass IVR
                '    shortcode = XmlReader.SelectSingleNode("/cpa-mobile-request/ivr/from").InnerText
                '    msg = Right(msg, 2)
                '    strCond = String.Format(" ShortCode='{0}' and (ivrRegister = '{1}' or ivrUnRegister='{1}') ", shortcode, msg)
                'ElseIf (XmlReader.GetElementsByTagName("ussd").Count > 0) Then 'USSD
                '    shortcode = XmlReader.SelectSingleNode("/cpa-mobile-request/destination/msisdn").InnerText
                '    strCond = String.Format(" USSD='{0}' ", XmlReader.SelectSingleNode("/cpa-mobile-request/ussd/content-id").InnerText)
                'Else ' SMS
                '    shortcode = XmlReader.SelectSingleNode("/cpa-mobile-request/destination/msisdn").InnerText
                '    strCond = String.Format(" Shortcode='{0}' and (CommandRegister = '{1}' or CommandUnRegister='{1}') ", shortcode, msg)
                'End If


                Dim con As New SqlConnection(My.Settings.DBCon)
                Dim comm As New SqlCommand
                comm.Connection = con
                comm.CommandText = String.Format("select * from services where {0}", strCond)

                con.Open()
                Dim dr As SqlDataReader = comm.ExecuteReader()


                If (dr.HasRows) Then ' Subscription
                    dr.Read()



                    Try


                        Dim comm1 As New SqlCommand("AddUpdateActive_User", New SqlConnection(My.Settings.DBCon))
                        'Log.info(dr("ServicePMKey"))
                        With comm1
                            .CommandType = CommandType.StoredProcedure

                            .Parameters.Add(New SqlParameter("ServicePMKey", dr("ServicePMKey").ToString))
                            .Parameters.Add(New SqlParameter("MSN", msisdn))
                            .Parameters.Add(New SqlParameter("Command", strRegisterType))


                        End With
                        comm1.Connection.Open()
                        comm1.ExecuteNonQuery()
                        If (Not IsDBNull(dr("FreeFirstLink")) And Command() = "register") Then
                            sendwaplinkbycheese(dr("FreeFirstLink").ToString, msisdn, dr("Sender").ToString)
                        End If







                    Catch ex As Exception
                        WriteLog("subscription", ex.Message & vbNewLine & ex.ToString, Now, True)
                    End Try
                    'END SUB =====================
                End If
            Else 'Other


            End If


        Catch ex As Exception
            WriteLog("mo", ex.Message & vbNewLine & ex.ToString, Now, True)
        End Try



    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    Public Function QueryDataReader(ByVal strSQL As String) As OleDbDataReader

        Dim dtReader As OleDbDataReader
        objConn = New OleDbConnection
        With objConn
            .ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\www\web\subscriptiondtac\Database2.accdb;"
            .Open()
        End With
        objCmd = New OleDbCommand(strSQL, objConn)
        dtReader = objCmd.ExecuteReader()
        Return dtReader '*** Return DataReader ***'
    End Function
    Public Function MessageDecode(ByVal Text As String) As String
        Dim str_output As String = ""
        Dim StringChar As Char() = Text.ToCharArray
        For n As Integer = 0 To StringChar.Length - 4 Step 4
            Dim s As String = StringChar(n) & StringChar(n + 1)
            If s = "0E" Then
                str_output += "%" & CStr(StringChar(n + 2)).Replace("0", "A").Replace("1", "B").Replace("2", "C").Replace("3", "D").Replace("4", "E").Replace("5", "F") & StringChar(n + 3)
            Else
                str_output += "%" & StringChar(n + 2) & StringChar(n + 3)
            End If
        Next
        Return System.Web.HttpUtility.UrlDecode(str_output, System.Text.Encoding.GetEncoding("TIS-620"))
    End Function

End Class

