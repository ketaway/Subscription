
Imports System
Imports System.Web

Imports System.Data.SqlClient
Imports NLog

Public Class drlistener : Implements IHttpHandler
    Public DRLog As Logger = LogManager.GetLogger("DR")

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim XmlReader As New Xml.XmlDocument

        Try
            
            Dim len As Integer = context.Request.InputStream.Length
            Dim dataRequest As String = ""
            Dim logbuffer(len) As Byte
            context.Request.InputStream.Read(logbuffer, 0, len)
            dataRequest = System.Text.Encoding.UTF8.GetString(logbuffer)

            context.Response.ContentType = "text/xml"
            ''LOG
            Nlogger.Trace(dataRequest)

            XmlReader.LoadXml(dataRequest)
            DRLog.Trace(XmlReader.InnerXml)

            
            Dim DataResponseSuccess As String = "<cpa-response><txid>" + XmlReader.SelectSingleNode("/cpa-dr-reply/txid").InnerText + "</txid><status>200</status><status-description>Success</status-description></cpa-response>"

            ''LOG
            Nlogger.Trace(DataResponseSuccess)

            context.Response.Write(DataResponseSuccess)
            context.Response.Flush()



          


            Dim comm1 As New SqlCommand("Add_DR_XML", New SqlConnection(My.Settings.DBCon))
            Try
                With comm1
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("xmlStringInput", XmlReader.InnerXml))
                End With
                comm1.Connection.Open()
                comm1.ExecuteNonQuery()
            Catch ex As Exception
                Nlogger.Error(ex.Message)
            Finally
                comm1.Connection.Close()

            End Try

        Catch ex As Exception
            Nlogger.Error(ex.Message)
        Finally

        End Try

    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class