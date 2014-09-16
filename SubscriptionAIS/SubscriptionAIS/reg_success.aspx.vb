
Partial Class reg_success
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Write(Request.QueryString("status") + " " + Request.QueryString("reason"))
    End Sub
End Class
