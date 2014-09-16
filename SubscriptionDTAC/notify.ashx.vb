
Imports System
Imports System.Web
Imports System.Data.SqlClient

Public Class notificationlistener : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
            Dim len As Integer = context.Request.InputStream.Length
            Dim dataRequest As String = ""
            Dim logbuffer(len) As Byte
            context.Request.InputStream.Read(logbuffer, 0, len)
            dataRequest = System.Text.Encoding.UTF8.GetString(logbuffer)

            ''LOG
            Nlogger.Trace(dataRequest)
            Dim XmlReader As New Xml.XmlDocument
            XmlReader.LoadXml(dataRequest)
             
            context.Response.ContentType = "text/xml"
            Dim DataResponseSuccess As String = "<cpa-response><txid>" + XmlReader.SelectSingleNode("/cpa-subscription-status-reply/txid").InnerText + "</txid><status>200</status><status-description>Success</status-description></cpa-response>"
            ''LOG
            Nlogger.Trace(DataResponseSuccess)
            'Nlogger.Error(
            context.Response.Write(DataResponseSuccess)

            Dim sqlConn As SqlConnection
            Dim oSelectCommand As New SqlCommand
            Dim sqlConnectString As String = My.Settings.DBCon
            sqlConn = New SqlConnection(sqlConnectString)
            sqlConn.Open()

            Dim otxid As String = XmlReader.SelectSingleNode("/cpa-subscription-status-reply/txid").InnerText
            Dim oProductID As String = XmlReader.SelectSingleNode("/cpa-subscription-status-reply/destination/productid").InnerText
            Dim oMsisdn As String = XmlReader.SelectSingleNode("/cpa-subscription-status-reply/subscription-status/msisdn").InnerText
            Dim oStatus As String = XmlReader.SelectSingleNode("/cpa-subscription-status-reply/subscription-status/status").InnerText
            Dim oStatusCode As String = XmlReader.SelectSingleNode("/cpa-subscription-status-reply/subscription-status/status-code").InnerText
            Dim oStatusDesc As String = XmlReader.SelectSingleNode("/cpa-subscription-status-reply/subscription-status/status-description").InnerText
            Dim oStart_Sate As String = XmlReader.SelectSingleNode("/cpa-subscription-status-reply/subscription-status/start-date").InnerText
            Dim oExpired_Date As String = XmlReader.SelectSingleNode("/cpa-subscription-status-reply/subscription-status/expired-date").InnerText

            oSelectCommand = New SqlCommand
            oSelectCommand.CommandText = "[Register]"
            ' oSelectCommand.Parameters.Add("@txid", SqlDbType.VarChar, 1000).Value = otxid
            'oSelectCommand.Parameters.Add("@productid", SqlDbType.VarChar, 1000).Value = oProductID
            oSelectCommand.Parameters.Add("@msn", SqlDbType.VarChar, 20).Value = oMsisdn
            oSelectCommand.Parameters.Add("@status", SqlDbType.VarChar, 1).Value = oStatus
            oSelectCommand.Parameters.Add("@serviceid", SqlDbType.VarChar, 20).Value = oProductID.Substring(0, 8) ' CONVERT FOR SERVICE ID
            ' oSelectCommand.Parameters.Add("@statuscode", SqlDbType.VarChar, 1000).Value = oStatusCode
            ' oSelectCommand.Parameters.Add("@statusdesc", SqlDbType.VarChar, 1000).Value = oStatusDesc
            'oSelectCommand.Parameters.Add("@start_date", SqlDbType.VarChar, 1000).Value = oStart_Sate
            ' oSelectCommand.Parameters.Add("@expired_date", SqlDbType.VarChar, 1000).Value = oExpired_Date

            'If XmlReader.SelectNodes("/cpa-subscription-status-reply/subscription-status").Item(0).ChildNodes.Item(6).Name = "register" Then
            '    Dim oOrg_typeRegis As String = XmlReader.SelectSingleNode("/cpa-subscription-status-reply/subscription-status/register/org-type").InnerText
            '    Dim oAccesscode As String = XmlReader.SelectSingleNode("/cpa-subscription-status-reply/subscription-status/register/accesscode").InnerText
            '    oSelectCommand.Parameters.Add("@org_type", SqlDbType.VarChar, 1000).Value = oOrg_typeRegis
            '    oSelectCommand.Parameters.Add("@accesscode", SqlDbType.VarChar, 1000).Value = oAccesscode
            '    oSelectCommand.Parameters.Add("@FLAG", SqlDbType.VarChar, 1000).Value = "register"
            'ElseIf XmlReader.SelectNodes("/cpa-subscription-status-reply/subscription-status").Item(0).ChildNodes.Item(6).Name = "unregister" Then
            '    Dim oOrg_typeUnRegis As String = XmlReader.SelectSingleNode("/cpa-subscription-status-reply/subscription-status/unregister/org-type").InnerText
            '    oSelectCommand.Parameters.Add("@org_type", SqlDbType.VarChar, 1000).Value = oOrg_typeUnRegis
            '    oSelectCommand.Parameters.Add("@accesscode", SqlDbType.VarChar, 1000).Value = DBNull.Value
            '    oSelectCommand.Parameters.Add("@FLAG", SqlDbType.VarChar, 1000).Value = "unregister"
            'Else
            '    oSelectCommand.Parameters.Add("@org_type", SqlDbType.VarChar, 1000).Value = DBNull.Value
            '    oSelectCommand.Parameters.Add("@accesscode", SqlDbType.VarChar, 1000).Value = DBNull.Value
            '    oSelectCommand.Parameters.Add("@FLAG", SqlDbType.VarChar, 1000).Value = DBNull.Value
            'End If
            oSelectCommand.Connection = sqlConn
            oSelectCommand.CommandType = CommandType.StoredProcedure
            oSelectCommand.ExecuteNonQuery()
            'Dim Results = CInt(oSelectCommand.Parameters("@ReturnValue").Value)

            'If Results <> 0 Then
            '    Throw New Exception("Error in StoredProcedure")
            'End If
            sqlConn.Close()
        Catch ex As Exception
            Nlogger.Error(ex.Message)
            Nlogger.Error(ex.InnerException.ToString)
            'IO.File.AppendAllText("D:\www\subscription\dtac\logs\NTError-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  " & ex.Message.ToString & vbNewLine & ex.ToString & vbNewLine)
        End Try
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class