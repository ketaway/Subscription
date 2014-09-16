Imports System.Web
Imports System.Web.Services

Public Class wappullnotify
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Write("success")

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class