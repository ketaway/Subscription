Imports System.Web
Imports System.Web.Services

Public Class chkoperator
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim curl, oper, redirecturl As String
        oper = "no"
        If (context.Request.QueryString("curl") Is Nothing) Then
            context.Response.Write("Fail!!!!! Request curl")
        Else
            curl = context.Request.QueryString("curl")
            If (Not context.Request.Cookies("User-Identity-Forward-ppp-username") Is Nothing) Then
                oper = "AIS"

            ElseIf (Not context.Request.Cookies("Plain-User-Identity-Forward-msisdn") Is Nothing) Then
                oper = "DTAC"
            End If


            If (curl.IndexOf("?") > -1) Then 'case เจอ  ?
                redirecturl = curl + "&oper=" + oper
            Else
                redirecturl = curl + "?oper=" + oper
            End If
            context.Response.Redirect(redirecturl)
        End If
        
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class