Imports BusinessLogic
Imports System.Data.SqlClient

Public Class Site3
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session("Authen_ID") Is Nothing) Then
            Dim objAuthen As New clsAUTHENTICATION(HttpContext.Current.User.Identity.Name)
            If (objAuthen.Status) Then
                Session("Authen_ID") = objAuthen.Id
                Session("Name") = objAuthen.NAME
                Session("SURNAME") = objAuthen.SURNAME
                Session("COMPANY") = objAuthen.COMPANY
            Else
                FormsAuthentication.SignOut()
                FormsAuthentication.RedirectToLoginPage("reason=UExpire")
            End If
        End If
        lblName.Text = Session("Name") + " " + Session("SURNAME")



    End Sub

    Private Sub btnLogOut_ServerClick(sender As Object, e As System.EventArgs) Handles btnLogOut.ServerClick
        FormsAuthentication.SignOut()
        FormsAuthentication.RedirectToLoginPage()
    End Sub
End Class