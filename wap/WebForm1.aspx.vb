 
Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If (Not Request.Cookies("User-Identity-Forward-ppp-username") Is Nothing) Then
        '    Response.Write(Request.Cookies("User-Identity-Forward-ppp-username").Value)
        '    '   Response.Write(Request.Cookies("<br>AIS").Value)

        'End If
        'If (Not Request.Cookies("Plain-User-Identity-Forward-msisdn") Is Nothing) Then
        '    Response.Write(Request.Cookies("Plain-User-Identity-Forward-msisdn").Value)
        '    ' Response.Write(Request.Cookies("<br>DTAC").Value)

        'End If
        'If (Not Request.QueryString("oper") Is Nothing) Then
        '    Response.Write(Request.QueryString("oper"))
        'End If
        
    End Sub

    Private Sub d_Click(sender As Object, e As System.EventArgs) Handles d.Click
        Dim a As String
    End Sub
End Class