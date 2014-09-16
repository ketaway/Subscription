<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site3.Master"
    CodeBehind="SubscriptionByOperator.aspx.vb" Inherits="BACK_OFFICE.SubscriptionByOperator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- page specific plugin styles -->
    <link rel="stylesheet" href="../assets/css/jquery-ui.custom.min.css" />
    <link rel="stylesheet" href="../assets/css/datepicker.css" />
    <link rel="stylesheet" href="../assets/css/daterangepicker.css" />
    <link rel="stylesheet" href="../assets/css/chosen.css" />
    <script src="../assets/js/ace-extra.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="clearfix form-actions">
            <div class="col-xs-3">
                <div class="input-group">
                    <span class="input-group-addon">Date Range</span> <span class="input-group-addon"><i
                        class="fa fa-calendar bigger-110"></i></span>
                    <input class="form-control" type="text" name="date-range-picker" id="dtRange" clientidmode="Static"
                        runat="server" />
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="col-xs-2">
                        <div class="input-group">
                            <span class="input-group-addon">Service</span>
                            <asp:DropDownList ID="dlService" runat="server" AutoPostBack="true" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-2">
                        <div class="input-group">
                            <span class="input-group-addon">Topic</span>
                            <asp:DropDownList ID="dlTopic" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <%-- <div class="col-xs-2">
                        <div class="input-group">
                            <span class="input-group-addon">Telco</span>
                            <asp:DropDownList ID="dlTelco" runat="server" class="form-control">
                            <asp:ListItem Text="All Item(s)" Value="0"></asp:ListItem>
                            <asp:ListItem Text="AIS" Value="A"></asp:ListItem>
                            <asp:ListItem Text="DTAC" Value="D"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>--%>
                    <div class="col-xs-1">
                        <button class="btn btn-info" type="button" runat="server" id="btnSubmit">
                            <i class="ace-icon fa fa-check bigger-110"></i>Submit
                        </button>
                    </div>
                    <div class="col-xs-1">
                        <button class="btn btn-info" type="button" id="btnExport" onclick="window.open('../export/SubscriptionByOperator_CSVExport.aspx<% =ParamExport %>','MsgWindow', 'width=200, height=200');" >
                            <i class="ace-icon fa fa-download bigger-125"></i>Export
                        </button>
                    </div>

                     
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
     

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    

            <div class="row">
                <div class="col-xs-12">
                    <table id="sample-table-1" class="table table-striped table-bordered">
                        <thead>
                            <tr>
                                <th rowspan="2">
                                    <i class="ace-icon fa fa-clock-o bigger-110 hidden-480"></i>Date
                                </th>
                                <th rowspan="2">
                                    Service
                                </th>
                                <th rowspan="2">
                                    Shortcode
                                </th>
                                <th rowspan="2">
                                    Topic
                                </th>
                                <th rowspan="2">
                                    Price
                                </th>
                                <th colspan="3"  >
                                    <center>Charging</center>
                                </th>
                                  <th colspan="3"  >
                                    <center>Member</center>
                                </th>
                                  <th colspan="3"  >
                                    <center>All Transaction</center>
                                </th><th colspan="3"  >
                                    <center>Success Rate</center>
                                </th>
                            </tr>
                              <tr>
                                 
                              
                                 <th>
                                    AIS
                                </th>
                                <th>
                                    DTAC
                                </th>
                                <th>
                                    TRUE
                                </th>
                                  <th>
                                    AIS
                                </th>
                                <th>
                                    DTAC
                                </th>
                                <th>
                                    TRUE
                                </th>
                                  <th>
                                    AIS
                                </th> 
                                <th>
                                    DTAC
                                </th> 
                                <th>
                                    TRUE
                                </th>  <th>
                                    AIS
                                </th> 
                                <th>
                                    DTAC
                                </th> 
                                <th>
                                    TRUE
                                </th> 
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="rptReport">
                                <ItemTemplate>
                                    <tr>
                                        <td id="tdDate" runat="server">
                                            <%-- <asp:Label ID="lblDate" runat="server"></asp:Label>--%>
                                            <%# DataBinder.Eval(Container.DataItem, "Date", "{0:dd-MM-yyyy}")%>
                                        </td>
                                        <td id="tdShortcode" runat="server">
                                            <%-- <asp:Label ID="lblRptShortCode" runat="server"></asp:Label>--%>
                                            <%# DataBinder.Eval(Container.DataItem, "SERVICE_NAME")%>
                                        </td>
                                        <td id="td1" runat="server">
                                            <%-- <asp:Label ID="lblRptShortCode" runat="server"></asp:Label>--%>
                                            <%# DataBinder.Eval(Container.DataItem, "SHORTCODE")%>
                                        </td>
                                        <td id="tdShortcodeTopcic" runat="server">
                                            <%# DataBinder.Eval(Container.DataItem, "TOPIC_NO")%>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "PRICE")%>
                                        </td>
                                        <td align="right">
                                            <%# DataBinder.Eval(Container.DataItem, "AIS_CHARGE_COUNT")%>
                                        </td>
                                        <td align="right">
                                            <%# DataBinder.Eval(Container.DataItem, "DTAC_CHARGE_COUNT")%>
                                        </td>
                                        <td align="right">
                                            0
                                        </td>
                                        <td align="right">
                                            <%# DataBinder.Eval(Container.DataItem, "AIS_MEMBER_CURRENT_COUNT")%>
                                        </td>
                                        <td align="right">
                                            <%# DataBinder.Eval(Container.DataItem, "DTAC_MEMBER_CURRENT_COUNT")%>
                                        </td>
                                        <td align="right">
                                            0
                                        </td>
                                        <td align="right">
                                            <%# DataBinder.Eval(Container.DataItem, "AIS_MEMBER_TRANSACTION_COUNT")%>
                                        </td>
                                        <td align="right">
                                            <%# DataBinder.Eval(Container.DataItem, "DTAC_MEMBER_TRANSACTION_COUNT") %>
                                        </td>
                                        <td align="right">
                                            0
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblAISSuccessRate" runat="server"></asp:Label>%
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblDTACSuccessRate" runat="server"></asp:Label>%
                                        </td>
                                        <td align="right">
                                            0.00%
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="5">
                                    Total
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblTotalAISCharge" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblTotalDTACCharge" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    0
                                </td>
                                <td align="right" colspan=3>
                                   
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblTotalAISTransaction" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblTotalDTACTransaction" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    0
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblTotalAISSuccessRate" runat="server"></asp:Label>%
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblTotalDTACSuccessRate" runat="server"></asp:Label>%
                                </td>
                                <td align="right">
                                    0%
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="updProgress"
    AssociatedUpdatePanelID="UpdatePanel1"
    runat="server">
        <ProgressTemplate>            
        <img alt="progress" src="../images/processing.gif"/>
           Processing...            
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerscript" runat="server">
    <!-- page specific plugin scripts -->
    <script src="../assets/js/date-time/bootstrap-datepicker.min.js"></script>
    <script src="../assets/js/date-time/moment.min.js"></script>
    <!-- ace scripts -->
    <script src="../assets/js/date-time/daterangepicker.min.js"></script>
    <script type="text/javascript">

      //datepicker plugin
				//link
				$('.date-picker').datepicker({
					autoclose: true,
					todayHighlight: true
				})
				//show datepicker when clicking on the icon
				.next().on(ace.click_event, function(){
					$(this).prev().focus();
				});
			
				//or change it into a date range picker
				$('.input-daterange').datepicker({autoclose:true});
			
			
				//to translate the daterange picker, please copy the "examples/daterange-fr.js" contents here before initialization
				$('input[id=dtRange]').daterangepicker({
					'applyClass' : 'btn-sm btn-success',
					'cancelClass' : 'btn-sm btn-default',
                    format: 'YYYY/MM/DD',
					locale: {
						applyLabel: 'Apply',
						cancelLabel: 'Cancel',
					}


				})
				.prev().on(ace.click_event, function(){
					$(this).next().focus();
				});
			
			
			 
				
               
			
    </script>
    <!-- ace scripts -->
</asp:Content>
