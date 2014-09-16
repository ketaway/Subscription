Public Class testdl
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Write(Request.Cookies("Plain-User-Identity-Forward-msisdn").Value)
    End Sub

End Class