Imports BusinessLogic

Public Class utcNavBar
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            SetNavBar()
        End If
    End Sub
    Private Sub SetNavBar()
       
        If (Request.QueryString("mid") Is Nothing Or Request.QueryString("prid") Is Nothing) Then
            liMenu.Visible = False
            liParentMenu.Visible = False
        Else
            liMenu.Visible = True
            liParentMenu.Visible = True
            Dim mid, prid As Int16
            mid = Request.QueryString("mid")
            prid = Request.QueryString("prid")
            Dim objMenu As New clsMENU
            Dim MenuName, ParentMenuName As String
            MenuName = ""
            ParentMenuName = ""


            objMenu.getNavBar(mid, prid, MenuName, ParentMenuName)
            lblParentMenuName.Text = ParentMenuName
            lblMenuName.Text = MenuName
        End If
      
    End Sub
End Class