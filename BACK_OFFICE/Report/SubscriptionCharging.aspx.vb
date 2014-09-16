Public Class SubscriptionCharging
    Inherits System.Web.UI.Page
    Private PrevDate, PrevShortcode, PrevShortcode_Topic, CurDate, CurShortcode, CurShortcode_Topic As String
    Private CountDupDate, CountDupShortcode, CountDupShortcode_Topic, rptCount, TotalRegCount, TotalChargeCount, TotalUnRegCount As Integer
    Private tdDate As HtmlTableCell
    Private tdShortcode As HtmlTableCell
    Private tdShortcodeTopic As HtmlTableCell

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            PreparePage()
            PreparSubservice()
            dtRange.Value = Now.ToString("yyyy/MM/dd") + " - " + Now.ToString("yyyy/MM/dd")
            getReport()

            'dtEnd.Value = Now.ToString("dd/MM/yyyy")
        End If
    End Sub
    Private Sub PreparePage()
        Dim objRPT As New BusinessLogic.clsSUBSCRIPTION_REPORT
        dlService.DataSource = objRPT.getServiceOwner(Session("Authen_ID"))
        dlService.DataTextField = "SHORTCODE_SERVICENAME"
        dlService.DataValueField = "SERVICE_ID"
        dlService.DataBind()
        dlService.Items.Insert(0, New ListItem("All Item(s)", 0))

 
    End Sub
    Private Sub PreparSubservice()
        Dim objRPT As New BusinessLogic.clsSUBSCRIPTION_REPORT
 
        Dim DS As DataSet = objRPT.getSubServiceOwner(CInt(Session("Authen_ID")), dlService.SelectedValue)

      
        If (DS.Tables.Count > 0) Then
            dlTopic.DataSource = DS
            dlTopic.DataTextField = "TOPIC_NO"
            dlTopic.DataValueField = "TOPIC_NO"
            dlTopic.DataBind()
        Else
            dlTopic.Items.Clear()
        End If
        dlTopic.Items.Insert(0, New ListItem("All Item(s)", 0))
    End Sub
    Private Sub getReport()
        Dim objRPT As New BusinessLogic.clsSUBSCRIPTION_REPORT
        Dim DateStart, DateEnd As String
        If (dtRange.Value <> "") Then
            DateStart = CDate(dtRange.Value.Replace(" ", "").Split("-")(0))
            DateEnd = CDate(dtRange.Value.Replace(" ", "").Split("-")(1))

            rptReport.DataSource = objRPT.getReport(Session("Authen_ID"), DateStart, DateEnd, dlService.SelectedItem.Value, dlTopic.SelectedValue, dlTelco.SelectedValue)
            rptReport.DataBind()
        End If
      
    End Sub

    Private Sub rptReport_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptReport.ItemDataBound
        rptCount += 1
        CurDate = e.Item.DataItem("DATE")
        CurShortcode = e.Item.DataItem("Shortcode")
        CurShortcode_Topic = e.Item.DataItem("Shortcode") + e.Item.DataItem("TOPIC_NO")
        Dim lblRptDate As Label = e.Item.FindControl("lblRptDate")
        Dim lblRptShortCode As Label = e.Item.FindControl("lblRptShortCode")
        Dim lblRptTopicNo As Label = e.Item.FindControl("lblRptTopicNo")
        Dim lblRptOperator As Label = e.Item.FindControl("lblRptOperator")
        Dim imgRptLogo As Image = e.Item.FindControl("imgRptLogo")

        TotalRegCount = TotalRegCount + CInt(e.Item.DataItem("REG_COUNT"))
        TotalChargeCount = TotalChargeCount + CInt(e.Item.DataItem("CHARGE_COUNT"))
        TotalUnRegCount = TotalUnRegCount + CInt(e.Item.DataItem("UNREG_COUNT"))

        lblTotalRegCount.Text = TotalRegCount.ToString
        lblTotalChargeCount.Text = TotalChargeCount.ToString
        lblTotalUnRegCount.Text = TotalUnRegCount.ToString
        '======================= DATE =====================
        If (CurDate = PrevDate) Then
            lblRptDate.Text = ""
            CType(e.Item.FindControl("tdDate"), HtmlTableCell).Attributes.Add("class", "hidden")
            'tdDate.Attributes.Add("class", "hidden")
            CountDupDate += 1
        Else
            CountDupDate = 1
            tdDate = e.Item.FindControl("tdDate")
            lblRptDate.Text = CDate(CurDate).ToString("dd-MM-yyyy")

        End If
        If (rptCount > 1) Then
            tdDate.RowSpan = CountDupDate
        End If
        PrevDate = e.Item.DataItem("DATE")
        '======================= DATE =====================

        '======================= Shortcode =====================
        If (CurShortcode = PrevShortcode) Then
            lblRptShortCode.Text = ""
            CType(e.Item.FindControl("tdShortcode"), HtmlTableCell).Attributes.Add("class", "hidden")
            'tdDate.Attributes.Add("class", "hidden")
            CountDupShortcode += 1
        Else
            CountDupShortcode = 1
            tdShortcode = e.Item.FindControl("tdShortcode")
            lblRptShortCode.Text = CurShortcode + ":" + e.Item.DataItem("SERVICE_NAME")
        End If
        PrevShortcode = e.Item.DataItem("Shortcode")
        If (rptCount > 1) Then
            tdShortcode.RowSpan = CountDupShortcode
        End If

        '======================= Shortcode =====================
        'tdShortcodeTopcic
        '======================= Topcic  =====================
        If (CurShortcode_Topic = PrevShortcode_Topic) Then
            lblRptTopicNo.Text = ""
            CType(e.Item.FindControl("tdShortcodeTopcic"), HtmlTableCell).Attributes.Add("class", "hidden")

            'tdDate.Attributes.Add("class", "hidden")
            CountDupShortcode_Topic += 1
        Else
            CountDupShortcode_Topic = 1
            tdShortcodeTopic = e.Item.FindControl("tdShortcodeTopcic")
            lblRptTopicNo.Text = e.Item.DataItem("TOPIC_NO")
        End If
        PrevShortcode_Topic = e.Item.DataItem("Shortcode") + e.Item.DataItem("TOPIC_NO")
        If (rptCount > 1) Then
            tdShortcodeTopic.RowSpan = CountDupShortcode_Topic
        End If
        '======================= Topcic =====================

        '======================= Operator =====================
        If (e.Item.DataItem("OPERATOR") = "A") Then
            lblRptOperator.Text = "AIS"
            imgRptLogo.ImageUrl = "~/images/aislogo.png"
        ElseIf (e.Item.DataItem("OPERATOR") = "D") Then
            lblRptOperator.Text = "DTAC"
            imgRptLogo.ImageUrl = "~/images/dtaclogo.png"

        ElseIf (e.Item.DataItem("OPERATOR") = "T") Then
            lblRptOperator.Text = "TRUEH"
            imgRptLogo.ImageUrl = "~/images/truemovelogo.png"

        End If


        'lblRptOperator
        'Dim liLV0NoChild, liLV0HasChild As HtmlControl
        'liLV0NoChild = e.Item.FindControl("liLV0NoChild")
        'liLV0HasChild = e.Item.FindControl("liLV0HasChild")
        'Dim pid, mid, prid As String
        'pid = Request("pid") 'Current Page ID
        'mid = Request("mid") 'Current Menu ID
        'prid = Request("prid") 'Current Parent Menu ID
        'If (e.Item.DataItem("Parent_ID") Is DBNull.Value And e.Item.DataItem("Page_URL") Is DBNull.Value) Then 'Main Menu No link
        '    Dim Menu_ID As Int16 = e.Item.DataItem("Menu_ID")
        '    dv.RowFilter = "Parent_ID = " + Menu_ID.ToString
        '    If (dv.Count > 0) Then
    End Sub

    
    Private Sub btnSubmit_ServerClick(sender As Object, e As System.EventArgs) Handles btnSubmit.ServerClick

        getReport()
    End Sub

    Private Sub dlService_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dlService.SelectedIndexChanged
        PreparSubservice()
    End Sub
End Class