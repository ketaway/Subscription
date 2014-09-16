Imports System.Web
Imports System.Web.Services
Imports System.IO

Public Class wappullresult
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim fi As New FileInfo(System.Web.HttpContext.Current.Server.MapPath("1378476170-1369136014-o.jpg"))
        With context
            .Response.ContentType = "image/jpeg"
            '.Response.WriteFile(fi.FullName)
            .Response.TransmitFile(fi.FullName)
            .Response.End()
        End With


    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class