<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site3.Master"
    CodeBehind="SubscriptionMTSchedule.aspx.vb" Inherits="BACK_OFFICE.SubscriptionMTSchedule" %>

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
                    <div class="col-xs-1">
                        <button class="btn btn-info" type="button" runat="server" id="btnSubmit">
                            <i class="ace-icon fa fa-check bigger-110"></i>Submit
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
                                    Service Name
                                </th>
                                <th rowspan="2">
                                    Shortcode
                                </th>
                                <th rowspan="2">
                                    Topic
                                </th>
                                <th rowspan="2">
                                    <center>AIS<img src="../images/aislogo.png" /></center>
                                </th>
                                <th rowspan="2">
                                   <center>DTAC<img src="../images/dtaclogo.png" /></center>
                                </th>
                                <th rowspan="2">
                                    <i class="ace-icon fa fa-clock-o bigger-110 hidden-480"></i>Schedule
                                </th>
                                <th rowspan="2">
                                    Content
                                </th>
                                <th colspan=3><center>AIS<img src="../images/aislogo.png" /></center></th><th colspan="3"><center>DTAC<img src="../images/dtaclogo.png" /></center></th>
                            </tr>
                            <tr>
                            
                            <th>
                                    Sent
                                </th>
                                <th>
                                    TXID
                                </th>
                                <th>
                                    Member Count
                                </th>
                                <th>
                                    Sent
                                </th>
                                <th>
                                    TXID
                                </th>
                                <th>
                                    Member Count
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="rptReport">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "SERVICE_NAME")%>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "SHORTCODE")%>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "TOPIC_NO")%>
                                        </td>
                                        <td>
                                            <center>
                                                <i class="ace-icon fa fa-check" runat="server" id="chkAIS"></i>
                                            </center>
                                        </td>
                                        <td>
                                            <center>
                                                <i class="ace-icon fa fa-check" runat="server" id="chkDTAC"></i>
                                            </center>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Schedule")%>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "SMSCONTENT")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSentStatus_A" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "SENT_FILE_NAME_A")%>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "MEMBER_BC_COUNT_A")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSentStatus_D" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "SENT_FILE_NAME_D")%>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "MEMBER_BC_COUNT_D")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
        <ProgressTemplate>
            <img alt="progress" src="../images/processing.gif" />
            Processing...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerscript" runat="server">
    <!-- page specific plugin scripts -->
    
    <script src="../assets/js/date-time/moment.min.js"></script>
    <script src="../assets/js/date-time/daterangepicker.min.js"></script>
    <!-- ace scripts -->
    <script src="../assets/js/date-time/bootstrap-datepicker.min.js"></script>
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
