Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Write("<h1>SessionID-->" + Session.SessionID)
        Response.Write("<h1>Your are user Number-->" + Application("hits").ToString())
    End Sub
End Class