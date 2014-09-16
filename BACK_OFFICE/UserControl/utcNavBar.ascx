<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="utcNavBar.ascx.vb" Inherits="BACK_OFFICE.utcNavBar" %>
 <ul class="breadcrumb">
                    <li><i class="ace-icon fa fa-home home-icon"></i><a href="<%= Page.ResolveClientUrl("~/main.aspx") %>">Home</a> </li>
                    <li runat="server" id="liParentMenu"><a href="#"><asp:Label ID="lblParentMenuName" runat="server" ></asp:Label></a> </li>
                    <li runat="server" id="liMenu"><asp:Label ID="lblMenuName" runat="server"></asp:Label></li>
                </ul>