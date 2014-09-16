Imports System.IO
Imports System.Security
Imports System.Security.Cryptography
Imports System.Xml
Public Class testwappull
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim baseURL, url As String
        '"http://ww1.mobileLIFE.co.th/wis/wap?ch=WAP&cmd=s_exp&cct=10&SN=4504901&spName=504
        baseURL = "http://ww1.mobilelife.co.th/wis/wap?"
        Dim spsID As String = makeRandomNumber(15)
        url = "cmd=s_exp&ch=WAP&SN=4503001&spsID=" & EncryptRSA(spsID, True) & "&spName=503&cURL=" + EncryptRSA("http://www.avi-mobiletainment.com:11000/wappullresult.ashx", True) + "&cct=10"

        url = baseURL + (url)
        lnk.HRef = url

        ' dv.InnerHtml = "<a href = '" + url + "'> คลิ๊ก เพื่อสมัคร " + DropDownList1.SelectedValue + DropDownList2.SelectedValue + "</a>"
    End Sub
    Public Function EncryptRSA(dataEncrypt As String, special As Boolean) As String

        Dim dataByte As Byte() = Encoding.UTF8.GetBytes(dataEncrypt)

        Dim rsa As RSACryptoServiceProvider = New RSACryptoServiceProvider
        'rsa.FromXmlString(My.Computer.FileSystem.ReadAllText(System.Configuration.ConfigurationManager.AppSettings("rsapathxml")))
        rsa.FromXmlString(File.ReadAllText(Server.MapPath("pub/wap_id_rsa_pub.xml")))

        Dim rDataEncrypt As String = Convert.ToBase64String(rsa.Encrypt(dataByte, False))
        If (special) Then
            rDataEncrypt = (rDataEncrypt).Replace("/", "%2F").Replace("?", "%3F").Replace("=", "%3D").Replace(".", "%2E").Replace("+", "%2B")
        End If

        rsa = Nothing
        Return rDataEncrypt
    End Function
    Public Shared Function makeRandomNumber(ByVal maxLen As String) As String

        Dim strNewPass2 As String = ""
        Dim whatsNext, upper, lower, intCounter As Integer

        Randomize()

        For intCounter = 1 To maxLen
            whatsNext = Int((1 - 0 + 1) * Rnd() + 0)
            If whatsNext = 0 Then
                'character
                upper = 57
                lower = 48
            Else
                upper = 57
                lower = 48
            End If
            strNewPass2 += Chr(Int((upper - lower + 1) * Rnd() + lower))
        Next
        Return strNewPass2

    End Function
End Class