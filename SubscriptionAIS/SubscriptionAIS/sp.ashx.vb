Imports System.Web
Imports System.Web.Services
Imports System
Imports com.tnswireless.Logger
Imports com.hp.AISMM7
Imports com.hp.AISMM7.DataType
Imports com.hp.AISMM7.MT
Imports System.Data.SqlClient

Public Class sp
    Inherits com.hp.AISMM7.MM7ReceiverBase


    Protected Overrides Sub onDeliveryReportReqReceived(ByVal context As System.Web.HttpContext, _
              ByVal obj As com.hp.AISMM7.DataType.deliveryReportReqType, _
              ByVal transactionId As com.hp.AISMM7.DataType.TransactionID)

        Dim rsp As DataType.DeliveryReportRsp = New DataType.DeliveryReportRsp("1000", "SUCCESS")
        returnDeliveryReportRsp(context, rsp, transactionId)
        Dim VPkgID As addressTypeNumber = CType(obj.Sender.Item, addressTypeNumber)
        Dim Receipient As addressTypeNumber = CType(obj.Recipient.Item, addressTypeNumber)
        Dim MessageID As String = obj.MessageID
        Dim ServiceID As String = VPkgID.Value
        Dim CustID As String = Receipient.Value
        Dim MMSRelayServerID As String = obj.MMSRelayServerID
        Dim SSSActionReport As String = obj.findMessageExtraData("SSSActionReport").value
        Dim MSISDN As String = obj.getMSISDN()
        Dim MMStatus As String = obj.MMStatus
        Dim StatusText As String = obj.StatusText
        Dim LinkedID As String = obj.findMessageExtraData("LinkedID").value
        Dim Bearer As String = obj.findMessageExtraData("Bearer").value
        Dim GMessageId As String = obj.findMessageExtraData("GMessageId").value
        Dim ClassOfService As String = obj.findMessageExtraData("ClassOfService").value
        Dim BillInfo As String = obj.findMessageExtraData("BillInfo").value
        Dim Channel As String = obj.findMessageExtraData("Channel").value
        Dim OPT As String = vbNewLine & _
        "===============================================" & vbNewLine & _
        "====== onDeliveryReportReqReceived (DN1)  ======" & vbNewLine & _
        "===============================================" & vbNewLine & _
        "VPkgID = " & ServiceID & vbNewLine & _
        "CustID = " & CustID & vbNewLine & _
        "===============================================" & vbNewLine & _
        "MSISDN = " & MSISDN & vbNewLine & _
        "StatusText = " & StatusText & vbNewLine & _
        "MMStatus = " & MMStatus & vbNewLine & _
        "MMSRelayServerID = " & MMSRelayServerID & vbNewLine & _
        "MessageID = " & MessageID & vbNewLine & _
        "============= MessageExtraDatas ===============" & vbNewLine & _
        "SSSActionReport = " & SSSActionReport & vbNewLine & _
        "LinkedID = " & LinkedID & vbNewLine & _
        "Bearer = " & Bearer & vbNewLine & _
        "GMessageId = " & GMessageId & vbNewLine & _
        "ClassOfService = " & ClassOfService & vbNewLine & _
        "BillInfo = " & BillInfo & vbNewLine & _
        "Channel = " & Channel & vbNewLine & _
        "===============================================" & vbNewLine & _
        "##########################################################################################"
        Log.info(OPT)
        '#######################################################################################
        Dim shortcode As String
        shortcode = ServiceID.Substring(0, 7)

        If SSSActionReport = "REG_SUCCESS" Then
            Dim URL As String = "http://ss1.mobilelife.co.th:9500"
            If (ServiceID.StartsWith("350")) Then
                URL = "http://ss2.mobilelife.co.th:9500"

            End If
            
            If (shortcode = "3503203" Or shortcode = "3503202" Or shortcode = "3503900" Or shortcode = "3503201" Or shortcode = "3503200" Or shortcode = "3503901") Then
                Try
                    Log.info("XXXXXXXXXX " + ServiceID, True)
                    Log.info("XXXXXXXXXX " + LinkedID, True)
                    Log.info("XXXXXXXXXX " + ServiceID, True)
                    Log.info("addressTypeNumber " + CType(obj.Sender.Item, DataType.addressTypeNumber).Value, True)

                    Dim mt As SendMT = New SendMT(shortcode + "00000000", "qlk6WGIETotCxTl", URL)
                    Dim req As MT.GeneralReq = New GeneralReq
                    req.cct = "14"
                    req.addMessageExtraData("CPAction", "BROADCAST")
                    req.linkId = LinkedID
                    req.toNumber = New String() {CustID}
                    req.fromNumber = ServiceID
                    req.senderName = shortcode
                    Threading.Thread.Sleep(5000)
                    Dim srsp As DataType.SubmitRsp = mt.sendSMSText(req, "ขอคุณที่ใช้บริการ")
                    Log.warn("== Result of sending SMS text ==")
                    Log.warn("  StatusCode='" + srsp.Status.StatusCode + "'")
                    Log.warn("  StatusText='" + srsp.Status.StatusText + "'")


                    Dim DBConn As SqlConnection = createConnection()
                    DBConn.Open()
                    Dim strsql As String = "EXEC MemberRegister '" + MSISDN + "','" + ServiceID + "',1 "

                    Dim SQLCMD_SV As New SqlCommand(strsql, DBConn)
                    SQLCMD_SV.CommandTimeout = 1000 * 60 * 5
                    SQLCMD_SV.ExecuteNonQuery()
                    DBConn.Close()



                Catch ex As Exception
                    Log.warn("  Error='" + ex.Message + "'")

                End Try
            End If



        ElseIf SSSActionReport = "UNREG_IMMEDIATE" Then
           

            Dim DBConn As SqlConnection = createConnection()
            DBConn.Open()
            Dim strsql As String = "EXEC MemberRegister '" + MSISDN + "','" + ServiceID + "',1 "
            Dim SQLCMD_SV As New SqlCommand(strsql, DBConn)
            SQLCMD_SV.CommandTimeout = 1000 * 60 * 5
            SQLCMD_SV.ExecuteNonQuery()
            DBConn.Close()

           
        End If


    End Sub

           
    Public Function createConnection() As SqlConnection
        Dim ConnString As String = ""
        Dim DBHost As String = "127.0.0.1"
        Dim DBName As String = "subAISCurrent"
        Dim DBUsername As String = "sa"
        Dim DBPassword As String = "cheeseteam55++"
        Return New SqlConnection("Server=" & DBHost & ";Database=" & DBName & ";User ID=" & DBUsername & ";Password=" & DBPassword & ";Trusted_Connection=False")
    End Function
    Protected Overrides Sub onDeliveryReqReceived(ByVal context As System.Web.HttpContext, _
               ByVal obj As com.hp.AISMM7.DataType.DeliverReq, _
               ByVal attachment As System.Collections.ArrayList, _
               ByVal transactionId As com.hp.AISMM7.DataType.TransactionID)

        Log.info("I receive MO")
        Dim i As Integer

        For i = 0 To obj.MessageExtraData.element.Length - 1
            Dim e As com.hp.AISMM7.DataType.ElementType = _
                     obj.MessageExtraData.element(i)
            Log.info(e.key + "='" + e.value + "'")
        Next


        Dim pd As com.tnswireless.Transport.PostData = CType(attachment.Item(0), com.tnswireless.Transport.PostData)
        Log.warn("ContentType='" + pd.contentType + "'")
        Log.warn("Name='" + pd.contentId + "'")
        If pd.contentBinary Is Nothing Then
            Log.warn("Data='Null'")
        End If

        Dim fileName As String = "c:\temp\" + Now.ToString("yyyyMMddHHmmss") + ".gz"
        Dim fs As IO.FileStream = New IO.FileStream(fileName, IO.FileMode.Create, IO.FileAccess.Write)
        Dim bs As IO.BinaryWriter = New IO.BinaryWriter(fs)
        bs.Write(pd.contentBinary)
        bs.Close()
        fs.Close()

        Log.warn("file '" + fileName + "' is written successfully.")

        Dim rsp As com.hp.AISMM7.DataType.DeliveryReportRsp = New com.hp.AISMM7.DataType.DeliveryReportRsp("1000", "SUCCESS")
        sp.returnDeliveryReportRsp(context, rsp, transactionId)


    End Sub

    Protected Overrides Sub onDeliveryReportFileReceived(ByVal context As System.Web.HttpContext, _
              ByVal obj As com.hp.AISMM7.DataType.DeliveryReportFile, _
              ByVal attachment As System.Collections.ArrayList, _
              ByVal transactionId As com.hp.AISMM7.DataType.TransactionID)

        Log.info("I receive REPORTFILE")
        Try
            Dim service As String = obj.Service
            Log.info("MM7version='" + obj.MM7Version.ToString + "'")
            Log.info("Service='" + service + "'")
            Dim i As Integer

            For i = 0 To obj.MessageExtraData.element.Length - 1
                Dim e As com.hp.AISMM7.DataType.ElementFileType = _
                         obj.MessageExtraData.element(i)
                Log.info(e.key + "='" + e.value + "'")
            Next


            Dim pd As com.tnswireless.Transport.PostData = CType(attachment.Item(0), com.tnswireless.Transport.PostData)
            Log.warn("ContentType='" + pd.contentType + "'")
            Log.warn("Name='" + pd.contentId + "'")
            If pd.contentBinary Is Nothing Then
                Log.warn("Data='Null'")
                Log.warn("Text {" + pd.contentText + "}")
            Else
                Log.warn("Got Bin")
            End If

            Dim fileName As String = "c:\temp\" + service + "-" + Now.ToString("yyyyMMddHHmmss") + ".gz"
            Dim fs As IO.FileStream = New IO.FileStream(fileName, IO.FileMode.Create, IO.FileAccess.Write)
            Dim bs As IO.BinaryWriter = New IO.BinaryWriter(fs)
            Log.warn("Content Length='" + pd.contentBinary.Length.ToString() + "'")
            bs.Write(pd.contentBinary)
            bs.Close()
            fs.Close()

            Log.warn("file '" + fileName + "' is written successfully.")
        Catch ex As Exception
            Log.warn(ex.Message)
            Log.warn(ex.StackTrace)
        End Try


        Dim rsp As com.hp.AISMM7.DataType.DeliveryReportRsp = New com.hp.AISMM7.DataType.DeliveryReportRsp("1000", "SUCCESS")
        sp.returnDeliveryReportRsp(context, rsp, transactionId)

    End Sub

End Class