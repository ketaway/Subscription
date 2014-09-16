Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Try
        '    sendwaplinkbycheese("http://www.google.com", "66859194778", "test wap")
        'Catch ex As Exception

        'End Try  
        Dim FULLIP As String = Context.Request.ServerVariables("LOCAL_ADDR")
        Response.Write("IP:" + FULLIP.Substring(FULLIP.LastIndexOf(".") + 1, FULLIP.Length - FULLIP.LastIndexOf(".") - 1))
        Response.Write(Context.Request.Cookies("Plain-User-Identity-Forward-msisdn"))
        ' WriteLog("dddddd", "test")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim txid As String
        txid = "04503" + Now.ToString("ddHHmmss") + "001"    'ddhh24mmss
        Dim texttest As String
        texttest = "<?xml version=""1.0"" encoding=""utf-8"" ?>"
        texttest += "<cpa-request>"
        texttest += "<txid>" + txid + "</txid>"
        texttest += "<authentication>"
        texttest += "<user>avimobile503</user>"
        texttest += "<password>m125QRSjn</password>"
        texttest += "</authentication>"
        texttest += "<originator>"
        texttest += "<sender>" + txtSender.Text.Trim + "</sender>"
        texttest += "</originator>"
        texttest += "<destination>"
        texttest += "<serviceid>" + txtServiceid.Text + "</serviceid>"

        '  texttest += "<msisdn>" + txtMSN.Text.Trim + "</msisdn>"

        'texttest += "<msisdn>66813041744</msisdn>"
        'texttest += "<msisdn>66859194778</msisdn>"
        texttest += "</destination>"
        texttest += "<message>"
        texttest += "<header>"
        texttest += "<timestamp>" + Now.ToString("yyyyMMddHHmmss", New System.Globalization.CultureInfo("en-US")) + "</timestamp>"
        texttest += "</header>"
        texttest += "<sms>"
        texttest += "<msg>" + txtText.Text.Trim + "</msg>"
        texttest += "<msgtype>T</msgtype>"
        texttest += "<encoding>8</encoding>"
        texttest += "</sms>"
        texttest += "</message>"
        texttest += "</cpa-request>"
        'IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\mosms_quiz-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & texttest & vbNewLine)
        '2556-07-15 12:16:46  <?xml version="1.0" encoding="utf-8" ?><cpa-request><txid>461115121652878</txid><authentication><user>cheese123</user><password>S16ACGMSg</password></authentication><originator><sender>66859194778</sender></originator><destination><serviceid>45040262</serviceid><msisdn>66859194778</msisdn></destination><message><header><timestamp>20130715121646</timestamp></header><sms><msg>ทดสอบการส่งข้อความ serviceid = 45040262 ข้อความที่ส่งมาคือ </msg><msgtype>T</msgtype><encoding>8</encoding></sms></message></cpa-request>
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
            txtResult.Text = ResponseData

            '<?xml version="1.0" encoding="utf-8" ?><cpa-response><txid>0450324210353001</txid><status>200</status><status-description>Success</status-description></cpa-response>
        Else

            Dim sReader As IO.StreamReader = New IO.StreamReader(HttpResponse.GetResponseStream())
            ResponseData = sReader.ReadToEnd().Trim()
            'ResponseData = HttpResponse.GetResponseStream.ToString
        End If

        'IO.File.AppendAllText("D:\www\web\subscriptiondtac\logs\mosms_quiz-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ResponseData & vbNewLine)







        HttpResponse.Close()
    End Sub
End Class