'   using System; 
Imports System.IO
Imports System.Security
Imports System.Security.Cryptography
Imports System.Xml

Public Class testreg
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Request.Cookies("User-Identity-Forward-ppp-username") Is Nothing) Then
            Response.Write(Request.Cookies("User-Identity-Forward-ppp-username").Value)

        End If
    End Sub


    Protected Sub btnSub_Click(sender As Object, e As EventArgs) Handles btnSub.Click
        Dim baseURL, url As String
        baseURL = "http://ss1.mobilelife.co.th/wis/wap?"
        If (DropDownList1.SelectedValue.StartsWith("350")) Then
            baseURL = "http://ss2.mobilelife.co.th/wis/wap?"
        End If
        Dim spsID As String = makeRandomNumber(15)
        url = "cmd=s_exp&ch=WAP&SN=" & DropDownList1.SelectedValue + DropDownList2.SelectedValue & "&spsID=" & spsID & "&spName=503&cURL=" + EncryptRSA("http://www.avi-mobiletainment.com:11000/reg_success.aspx".Replace("/", "%2F").Replace("=", "%3D").Replace("+", "%2B").Replace(":", "%3A")) + "&cct=09"

        url = baseURL + (url)
         
         
        dv.InnerHtml = "<a href = '" + url + "'> คลิ๊ก เพื่อสมัคร " + DropDownList1.SelectedValue + DropDownList2.SelectedValue + "</a>"
    End Sub


    Public Function EncryptRSA(dataEncrypt As String) As String

        Dim dataByte As Byte() = Encoding.UTF8.GetBytes(dataEncrypt)

        Dim rsa As RSACryptoServiceProvider = New RSACryptoServiceProvider
        'rsa.FromXmlString(My.Computer.FileSystem.ReadAllText(System.Configuration.ConfigurationManager.AppSettings("rsapathxml")))
        rsa.FromXmlString(File.ReadAllText(Server.MapPath("pub/wap_id_rsa_pub.xml")))

        Dim rDataEncrypt As String = Convert.ToBase64String(rsa.Encrypt(dataByte, False)).Replace("/", "%2F").Replace("?", "%3F").Replace("=", "%3D").Replace(".", "%2E").Replace("+", "%2B")

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
    Public Shared Function getURLEncodedString(ByVal stt As String) As String
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
        Dim i, j As Integer
        Dim reserved() As Char = {ChrW(63), ChrW(61), ChrW(38)}

        While i < stt.Length
            j = stt.IndexOfAny(reserved, i)
            If j = -1 Then
                sb.Append(HttpUtility.UrlEncode(stt.Substring(i, stt.Length - i)))
                Exit While
            End If

            sb.Append(HttpUtility.UrlEncode(stt.Substring(i, j - i)))
            sb.Append(stt.Substring(j, 1))
            i = j + 1
        End While

        Return sb.ToString() ' (sb.ToString().Replace(ChrW(38), "&amp;"))
    End Function
 

    Public Function PublicEncryption(data As Byte()) As String
        Dim rsa As New RSACryptoServiceProvider()

        rsa.FromXmlString(File.ReadAllText(Server.MapPath("pub/wap_id_rsa_pub.xml")))

        Dim encryptedData As Byte()

        encryptedData = rsa.Encrypt(data, True)
        Return Convert.ToBase64String(encryptedData).Replace("/", "%2F").Replace("?", "%3F").Replace("=", "%3D").Replace(".", "%2E").Replace("+", "%2B")
    End Function
End Class