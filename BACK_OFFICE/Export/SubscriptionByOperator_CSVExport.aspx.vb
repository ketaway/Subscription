Imports BusinessLogic

Public Class SubscriptionByOperator_CSVExport
    Inherits System.Web.UI.Page

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


        WriteToCSV()

    End Sub
    Public Sub WriteToCSV()

        Dim objRPT As New BusinessLogic.clsSUBSCRIPTION_REPORT
        Dim dt As DataTable
        Dim DateStart, DateEnd, ServiceID, TopicNo As String

        DateStart = Request.QueryString("DateStart")
        DateEnd = Request.QueryString("DateEnd")
        ServiceID = Request.QueryString("ServiceID")
        TopicNo = Request.QueryString("TopicNo")

        dt = objRPT.getReportCharging_By_Oper(Session("Authen_ID"), DateStart, DateEnd, ServiceID, TopicNo).Tables(0)

         

        Dim attachment As String = "attachment; filename=SubscriptionByOperator_CSVExport.csv"
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

End Class