Imports BusinessLogic
Public Class utcLeftMenu
    Inherits System.Web.UI.UserControl
    Dim dv As DataView
    Private WithEvents rptSubMenuL1 As Repeater
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ds As DataSet = New clsMENU().getLeftMenu(Convert.ToInt16(Session("Authen_ID")))
        dv = ds.Tables(0).AsDataView()
        dv.RowFilter = "Parent_ID is null"
        rptLeftMenu.DataSource = dv
        rptLeftMenu.DataBind()
    End Sub

    Private Sub rptLeftMenu_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptLeftMenu.ItemDataBound
        Dim liLV0NoChild, liLV0HasChild As HtmlControl
        liLV0NoChild = e.Item.FindControl("liLV0NoChild")
        liLV0HasChild = e.Item.FindControl("liLV0HasChild")
        Dim pid, mid, prid As String
        pid = Request("pid") 'Current Page ID
        mid = Request("mid") 'Current Menu ID
        prid = Request("prid") 'Current Parent Menu ID
        If (e.Item.DataItem("Parent_ID") Is DBNull.Value And e.Item.DataItem("Page_URL") Is DBNull.Value) Then 'Main Menu No link
            Dim Menu_ID As Int16 = e.Item.DataItem("Menu_ID")
            dv.RowFilter = "Parent_ID = " + Menu_ID.ToString
            If (dv.Count > 0) Then
                rptSubMenuL1 = e.Item.FindControl("rptSubMenuL1")

                rptSubMenuL1.DataSource = dv
                rptSubMenuL1.DataBind()
            End If
            liLV0NoChild.Visible = False
            liLV0HasChild.Visible = True
            '    e.Item.Visible = False
            If (prid = e.Item.DataItem("MENU_ID").ToString) Then
                liLV0HasChild.Attributes("class") = "active open"
            End If
        Else 'Main Menu there is link
            liLV0NoChild.Visible = True
            liLV0HasChild.Visible = False
            If (pid = e.Item.DataItem("Page_ID")) Then
                liLV0NoChild.Attributes("class") = "active"
            End If
        End If
        'Dim a As String
        'a = ""
    End Sub
    Private Sub rptSubMenuL1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptSubMenuL1.ItemDataBound
        Dim lilink As HtmlControl
        Dim pid, mid, prid As String
        pid = Request("pid") 'Current Page ID
        mid = Request("mid") 'Current Menu ID
        prid = Request("prid") 'Current Parent Menu ID
        lilink = e.Item.FindControl("lilink")
        If (pid = e.Item.DataItem("Page_ID")) Then
            lilink.Attributes("class") = "active"
        End If
    End Sub
End Class