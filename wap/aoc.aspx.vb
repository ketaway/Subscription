Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.IO

Public Class aoc
    Inherits System.Web.UI.Page
    Public ServiceName, Price, ivrReg, ComReg, ShortCode, ivrUnReg, ComUnReg, Comregplus, a As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim OService_ID As String
        OService_ID = Request.QueryString("sid")
        Dim comm2 As New SqlCommand("getSubServiceByServiceID", New SqlConnection(My.Settings.DBCon))
        'Log.info(dr("ServicePMKey"))

        With comm2
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("OPERATOR_SERVICE_ID", OService_ID))

        End With
        Try
            Dim FreeLink As String
            Dim chkoper As String = CheckOper()
            a = chkoper
            comm2.Connection.Open()
            Dim dr As SqlDataReader = comm2.ExecuteReader()
            If (dr.Read) Then
                ServiceName = dr("SERVICE_NAME")
                Price = dr("PRICE")
                ivrReg = dr("IVR_REG")
                ComReg = dr("COMMAND_REG")
                ShortCode = dr("SHORTCODE")
                ivrUnReg = dr("IVR_UNREG")
                ComUnReg = dr("COMMAND_UNREG")
                Comregplus = ComReg.Replace(" ", "+")
                ' If (dr("FREE_CONTENT_ID") Is DBNull.Value) Then
                FreeLink = "http://m.mobindy.com/wap/regresult.aspx"
                'Else
                ' FreeLink = dr("FREE_CONTENT_MSG")
                ' End If
                If (chkoper = "AIS") Then '=================AIS================
                    servicedetail.Visible = False

                    Dim baseURL, url As String
                    baseURL = "http://ss1.mobilelife.co.th/wis/wap?"

                    Dim spsID As String = makeRandomNumber(15)
                    url = "cmd=s_exp&ch=WAP&SN=" & ivrReg & "&spsID=" & spsID & "&spName=503&cURL=" + EncryptRSA(FreeLink.Replace("/", "%2F").Replace("=", "%3D").Replace("+", "%2B").Replace(":", "%3A")) + "&cct=09"

                    url = baseURL + (url)

                    Response.Redirect(url)
                    'Response.Write("<script>window.location=" + url + ";</script>")
                ElseIf (chkoper = "DTAC" Or chkoper = "no") Then '=================DTAC and NO================
                    servicedetail.Visible = True

                End If
            Else
                lblMessage.Text = "ไม่พบบริการ"
                servicedetail.Visible = False
            End If

        Catch ex As Exception
            lblMessage.Text = ex.Message
            Throw ex
        Finally
            comm2.Connection.Close()

        End Try
    End Sub
    Private Function CheckOper()
        Dim oper As String
        oper = "no"


        If (Not Context.Request.Cookies("User-Identity-Forward-ppp-username") Is Nothing) Then
            oper = "AIS"

        ElseIf (Not Context.Request.Cookies("Plain-User-Identity-Forward-msisdn") Is Nothing) Then
            oper = "DTAC"
        End If




        Return oper
    End Function

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