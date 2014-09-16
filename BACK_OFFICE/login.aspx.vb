Public Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If (Request("logout") <> Nothing) Then

        '    FormsAuthentication.SignOut()
        '    Response.Redirect("./")
        'End If
        If (Not Page.IsPostBack) Then
            If (User.Identity.IsAuthenticated) Then
                FormsAuthentication.RedirectFromLoginPage(User.Identity.Name, True)
            End If

            'IF redirect from some page by any reason
            If (Request.QueryString("reason") = "UExpire") Then
                divErrorMessage.Visible = True
                lblMessage.Text = "This Username is Expired"
            End If

        End If
        
    End Sub
    Protected Sub onbtnLogin_lick() Handles btnLogin.ServerClick
        'divErrorMessage.Visible = True
        'lblMessage.Text = Now.ToString
        Try


            '' If (FormsAuthentication.Authenticate("", "")) Then
            Dim objActhen As New BusinessLogic.clsAUTHENTICATION(txtUsername.Text, txtPassword.Text)
            If (objActhen.Status) Then
                Session("Authen_ID") = objActhen.Id
                Session("Name") = objActhen.NAME
                Session("SURNAME") = objActhen.SURNAME
                Session("COMPANY") = objActhen.COMPANY
                FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, chkRemember.Checked)
            Else
                divErrorMessage.Visible = True
                lblMessage.Text = objActhen.MESSAGE
            End If


        Catch ex As Exception
            divErrorMessage.Visible = True
            lblMessage.Text = ex.Message
        End Try

    End Sub
End Class