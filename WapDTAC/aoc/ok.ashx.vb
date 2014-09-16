Imports System.Web
Imports System.Web.Services
Imports System.Xml

Public Class ok
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        'Dim len As Integer = context.Request.InputStream.Length
        'Dim dataRequest As String = ""
        'Dim logbuffer(len) As Byte
        'context.Request.InputStream.Read(logbuffer, 0, len)
        'dataRequest = System.Text.Encoding.UTF8.GetString(logbuffer)

        'Try

        'Dim XmlReader As New XmlDocument
        'XmlReader.LoadXml(dataRequest)

        '    'IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\morequest-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & dataRequest & vbNewLine)
        '    IO.File.AppendAllText("D:\www\subscription\dtac\logs\aoc-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & XmlReader.InnerXml & vbNewLine)
        'Catch ex As Exception
        '    IO.File.AppendAllText("D:\www\subscription\dtac\logs\aoc-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ex.Message.ToString & vbNewLine)
        'End Try
        Dim msisdn, tk, ssid As String
        msisdn = context.Request.Cookies("Plain-User-Identity-Forward-msisdn").Value
        tk = context.Request("tk")
        ssid = context.Request("ssid")

        Dim txid As String
        txid = "04503" + Now.ToString("ddHHmmss") + "001"    'ddhh24mmss
        Dim texttest As String
        texttest = "<?xml version=""1.0"" encoding=""utf-8"" ?>"
        texttest += "<cpa-request-aoc>"
        texttest += "<authentication>"
        texttest += "<user>avimobile503</user>"
        texttest += "<password>m125QRSjn</password>"
        texttest += "</authentication>"
        texttest += "<delivery-confirm>"
        texttest += "<session>" + ssid + "</session>"
        texttest += "<ticket>" + tk + "</ticket>"
        texttest += "<cp-ref-id>0002</cp-ref-id>"
        texttest += "<command>WPRD</command>"
        texttest += "<msisdn>" + msisdn + "</msisdn>"
        texttest += "<timestamp>" + Now.ToString("yyyyMMddHHmmss", New System.Globalization.CultureInfo("en-US")) + "</timestamp>"
        texttest += "</delivery-confirm>"
        texttest += "</cpa-request-aoc>"
        IO.File.AppendAllText("D:\www\subscription\dtac\logs\aoc-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & texttest & vbNewLine)
        'IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\mosms_quiz-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & texttest & vbNewLine)
        '2556-07-15 12:16:46  <?xml version="1.0" encoding="utf-8" ?><cpa-request><txid>461115121652878</txid><authentication><user>cheese123</user><password>S16ACGMSg</password></authentication><originator><sender>66859194778</sender></originator><destination><serviceid>45040262</serviceid><msisdn>66859194778</msisdn></destination><message><header><timestamp>20130715121646</timestamp></header><sms><msg>ทดสอบการส่งข้อความ serviceid = 45040262 ข้อความที่ส่งมาคือ </msg><msgtype>T</msgtype><encoding>8</encoding></sms></message></cpa-request>
        Dim DataToSend() As Byte = Encoding.UTF8.GetBytes(texttest)
        Dim HTTPRequest As Net.HttpWebRequest = Net.WebRequest.Create("http://sdpapi.dtac.co.th:8319/SAG/services/cpa/aoc/deliveryConfirm")

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
            ' txtResult.Text = ResponseData


        Else

            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
            ResponseData = sReader.ReadToEnd().Trim()
            'ResponseData = HttpResponse.GetResponseStream.ToString
        End If
        IO.File.AppendAllText("D:\www\subscription\dtac\logs\aoc-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ResponseData & vbNewLine)








        HttpResponse.Close()
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class