Public Class SubscriptionByOperator
    Inherits System.Web.UI.Page
    Dim TotalAISCharge, TotalDTACCharge, TotalAISTransaction, TotalDTACTransaction As Integer
    Public ParamExport As String

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

            rptReport.DataSource = objRPT.getReportCharging_By_Oper(Session("Authen_ID"), DateStart, DateEnd, dlService.SelectedItem.Value, dlTopic.SelectedValue)
            rptReport.DataBind()
            ParamExport = "?DateStart=" + DateStart + "&DateEnd=" + DateEnd + "&ServiceID=" + dlService.SelectedItem.Value + "&TopicNo=" + dlTopic.SelectedValue
        End If
    End Sub

    Private Sub rptReport_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptReport.ItemDataBound
       
        Dim lblAISSuccessRate As Label = e.Item.FindControl("lblAISSuccessRate")
        Dim lblDTACSuccessRate As Label = e.Item.FindControl("lblDTACSuccessRate")
        If (e.Item.DataItem("AIS_MEMBER_TRANSACTION_COUNT") = "0") Then
            lblAISSuccessRate.Text = "0.00"
        Else
            lblAISSuccessRate.Text = (CInt(e.Item.DataItem("AIS_CHARGE_COUNT")) / CInt(e.Item.DataItem("AIS_MEMBER_TRANSACTION_COUNT")) * 100).ToString("0.00")
        End If
        If (e.Item.DataItem("DTAC_MEMBER_TRANSACTION_COUNT") = "0") Then
            lblDTACSuccessRate.Text = "0.00"
        Else
            lblDTACSuccessRate.Text = (CInt(e.Item.DataItem("DTAC_CHARGE_COUNT")) / CInt(e.Item.DataItem("DTAC_MEMBER_TRANSACTION_COUNT")) * 100).ToString("0.00")
        End If
        TotalAISCharge = TotalAISCharge + CInt(e.Item.DataItem("AIS_CHARGE_COUNT"))
        TotalDTACCharge = TotalDTACCharge + CInt(e.Item.DataItem("DTAC_CHARGE_COUNT"))
        TotalAISTransaction = TotalAISTransaction + CInt(e.Item.DataItem("AIS_MEMBER_TRANSACTION_COUNT"))
        TotalDTACTransaction = TotalDTACTransaction + CInt(e.Item.DataItem("DTAC_MEMBER_TRANSACTION_COUNT"))
        

        lblTotalAISCharge.Text = TotalAISCharge.ToString
        lblTotalDTACCharge.Text = TotalDTACCharge.ToString
        lblTotalAISTransaction.Text = TotalAISTransaction.ToString
        lblTotalDTACTransaction.Text = TotalDTACTransaction.ToString
    

        If (TotalAISTransaction <> 0) Then
            
            lblTotalAISSuccessRate.Text = (TotalAISCharge / TotalAISTransaction * 100).ToString("0.00")
        End If
        If (TotalDTACTransaction <> 0) Then

            lblTotalDTACSuccessRate.Text = (TotalDTACCharge / TotalDTACTransaction * 100).ToString("0.00")
        End If

        'lblTotalAISCharge.text= TotalAISCharge, TotalDTACCharge, TotalAISMember, TotalDTACMember, TotalAISTransaction, TotalDTACTransaction, TotalAISSuccessRate, TotalDTACSuccessRate
    End Sub


    Private Sub btnSubmit_ServerClick(sender As Object, e As System.EventArgs) Handles btnSubmit.ServerClick

        getReport()
    End Sub

    Private Sub dlService_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dlService.SelectedIndexChanged
        PreparSubservice()
    End Sub


    Public Sub WriteToCSV()

        Dim objRPT As New BusinessLogic.clsSUBSCRIPTION_REPORT
        Dim dt As DataTable
        Dim DateStart, DateEnd As String
        If (dtRange.Value <> "") Then
            DateStart = CDate(dtRange.Value.Replace(" ", "").Split("-")(0))
            DateEnd = CDate(dtRange.Value.Replace(" ", "").Split("-")(1))

            dt = objRPT.getReportCharging_By_Oper(Session("Authen_ID"), DateStart, DateEnd, dlService.SelectedItem.Value, dlTopic.SelectedValue).Tables(0)
        Else
            Exit Sub
        End If


        Dim attachment As String = "attachment; filename=PersonList.csv"
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.ClearHeaders()
        HttpContext.Current.Response.ClearContent()
        HttpContext.Current.Response.AddHeader("content-disposition", attachment)
        HttpContext.Current.Response.ContentType = "text/csv"
        HttpContext.Current.Response.AddHeader("Pragma", "public")
        WriteColumnName()
        For Each dr As DataRow In dt.Rows
            WriteUserInfo(dr)
        Next
        HttpContext.Current.Response.End()
    End Sub

    Private Shared Sub WriteUserInfo(dr As DataRow)
        Dim stringBuilder As New StringBuilder()
        AddComma(String.Format("{0:dd-MM-yyyy}", dr("DATE")), stringBuilder)
        AddComma(dr("SERVICE_NAME"), stringBuilder)
        AddComma(dr("SHORTCODE"), stringBuilder)
        AddComma(dr("TOPIC_NO"), stringBuilder)
        AddComma(dr("PRICE"), stringBuilder)
        AddComma(dr("AIS_CHARGE_COUNT"), stringBuilder)
        AddComma(dr("DTAC_CHARGE_COUNT"), stringBuilder)
        AddComma("0", stringBuilder)
        AddComma(dr("AIS_MEMBER_CURRENT_COUNT"), stringBuilder)
        AddComma(dr("DTAC_MEMBER_CURRENT_COUNT"), stringBuilder)
        AddComma("0", stringBuilder)
        AddComma(dr("AIS_MEMBER_TRANSACTION_COUNT"), stringBuilder)
        AddComma(dr("DTAC_MEMBER_TRANSACTION_COUNT"), stringBuilder)
        AddComma("0", stringBuilder)
        If (dr("AIS_MEMBER_TRANSACTION_COUNT") = "0") Then
            AddComma("0", stringBuilder)
        Else
            AddComma((CInt(dr("AIS_CHARGE_COUNT")) / CInt(dr("AIS_MEMBER_TRANSACTION_COUNT")) * 100).ToString("0.00"), stringBuilder)
        End If
        If (dr("DTAC_MEMBER_TRANSACTION_COUNT") = "0") Then
            AddComma("0", stringBuilder)
        Else
            AddComma((CInt(dr("DTAC_CHARGE_COUNT")) / CInt(dr("DTAC_MEMBER_TRANSACTION_COUNT")) * 100).ToString("0.00"), stringBuilder)
        End If
      'AddComma(person.Family, stringBuilder)
        'AddComma(person.Age.ToString(), stringBuilder)
        'AddComma(String.Format("{0:C2}", person.Salary), stringBuilder)
        HttpContext.Current.Response.Write(stringBuilder.ToString())
        HttpContext.Current.Response.Write(Environment.NewLine)
    End Sub

    Private Shared Sub AddComma(value As String, stringBuilder As StringBuilder)
        stringBuilder.Append(value.Replace(","c, " "c))
        stringBuilder.Append(", ")
    End Sub

    Private Shared Sub WriteColumnName()
        Dim columnNames As String = "Date ,Service ,Shortcode ,Topic ,Price ,AIS Charging,DTAC Charging,TRUE Charging,AIS Member,DTAC Member,TRUE Member,AIS All Transaction,DTAC All Transaction,TRUE All Transaction,AIS Success Rate,DTAC Success Rate,TRUE Success Rate"
        HttpContext.Current.Response.Write(columnNames)
        HttpContext.Current.Response.Write(Environment.NewLine)
    End Sub

    '=======================================================
    'Service provided by Telerik (www.telerik.com)
    'Conversion powered by NRefactory.
    'Twitter: @telerik
    'Facebook: facebook.com/telerik
    '=======================================================


    'Private Sub btnExport_ServerClick(sender As Object, e As System.EventArgs) Handles btnExport.ServerClick
    '    WriteToCSV()
    'End Sub
End Class