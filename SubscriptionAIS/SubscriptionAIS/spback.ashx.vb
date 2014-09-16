Imports System.Web
Imports System.Web.Services
Imports com.hp.AISMM7.DataType

Public Class spback
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

      


        context.Response.Write(context.Request.QueryString("status") + " " + context.Request.QueryString("reason"))

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class