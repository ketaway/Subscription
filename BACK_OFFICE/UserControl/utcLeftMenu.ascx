<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="utcLeftMenu.ascx.vb"
    Inherits="BACK_OFFICE.utcLeftMenu" %>

               
<ul class="nav nav-list">

    <asp:Repeater ID="rptLeftMenu" runat="server">
        <ItemTemplate>
            <%--<li class=""><a href="<%# Page.ResolveClientUrl("~/"+iif(DataBinder.Eval(Container.DataItem, "PAGE_URL") is dbnull.value,"#",DataBinder.Eval(Container.DataItem, "PAGE_URL") ))%>"><i class="menu-icon fa <%# DataBinder.Eval(Container.DataItem, "MENU_ICON")%>"></i><span
        class="menu-text"><%# DataBinder.Eval(Container.DataItem, "MENU_NAME")%></span></a><b class="arrow"></b></li>
            --%>
            <%--<%#If(DataBinder.Eval(Container.DataItem, "PAGE_URL") Is DBNull.Value)%>
               <%#Else %>
            --%>
            <li runat="server"  id="liLV0NoChild" class="" >
                <a href="<%# Page.ResolveClientUrl("~/"+DataBinder.Eval(Container.DataItem, "PAGE_URL")) & "?pid=" & DataBinder.Eval(Container.DataItem, "PAGE_ID") & "&mid=" & DataBinder.Eval(Container.DataItem, "MENU_ID") & "&prid=" & DataBinder.Eval(Container.DataItem, "PARENT_ID") %>">
                    <i class="menu-icon fa <%# DataBinder.Eval(Container.DataItem, "MENU_ICON")%>"></i>
                    <span class="menu-text">
                        <%# DataBinder.Eval(Container.DataItem, "MENU_NAME")%>
                    </span></a><b class="arrow"></b></li>
            <li runat="server" id="liLV0HasChild" class="">
                <a href="#" class="dropdown-toggle"><i class="menu-icon fa <%# DataBinder.Eval(Container.DataItem, "MENU_ICON")%>">
                </i><span class="menu-text">
                    <%# DataBinder.Eval(Container.DataItem, "MENU_NAME")%>
                </span><b class="arrow fa fa-angle-down"></b></a><b class="arrow"></b>
                <ul class="submenu">
                    <asp:Repeater ID="rptSubMenuL1" runat="server">
                        <ItemTemplate>
                            <li runat="server" id="lilink" class=""><a href="<%# Page.ResolveClientUrl("~/"+DataBinder.Eval(Container.DataItem, "PAGE_URL")) & "?pid=" & DataBinder.Eval(Container.DataItem, "PAGE_ID") & "&mid=" & DataBinder.Eval(Container.DataItem, "MENU_ID") & "&prid=" & DataBinder.Eval(Container.DataItem, "PARENT_ID") %>">
                                <i class="menu-icon fa fa-caret-right"></i>
                                <%# DataBinder.Eval(Container.DataItem, "MENU_NAME")%>
                            </a><b class="arrow"></b></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </li>
            <%--
                 <li class=""><a href="<%#If(DataBinder.Eval(Container.DataItem, "PAGE_URL") Is DBNull.Value, "#", DataBinder.Eval(Container.DataItem, "PAGE_URL"))%>" class="dropdown-toggle"><i class="menu-icon fa <%# DataBinder.Eval(Container.DataItem, "MENU_ICON")%>"></i>
        <span class="menu-text"><%# DataBinder.Eval(Container.DataItem, "MENU_NAME")%> </span><%#If(DataBinder.Eval(Container.DataItem, "PAGE_URL") Is DBNull.Value, "<b class=""arrow fa fa-angle-dow</b>""", "")%>"></a>
        <b class="arrow"></b> 
        <ul class="submenu">
            <asp:Repeater ID="rptSubMenuL1" runat="server">
            <ItemTemplate>
              <li class=""><a href="<%# Page.ResolveClientUrl("~/"+DataBinder.Eval(Container.DataItem, "PAGE_URL"))%>"><i class="menu-icon fa fa-caret-right"></i><%# DataBinder.Eval(Container.DataItem, "MENU_NAME")%> </a><b class="arrow"></b></li>
            </ItemTemplate>
            </asp:Repeater>--%>
            <%-- <li class=""><a href="tables.html"><i class="menu-icon fa fa-caret-right"></i>Simple
                &amp; Dynamic </a><b class="arrow"></b></li>
            <li class=""><a href="jqgrid.html"><i class="menu-icon fa fa-caret-right"></i>jqGrid
                plugin </a><b class="arrow"></b></li>--%>
            <%--
                        <li class=""><a href="<%# Page.ResolveClientUrl("~/"+ DataBinder.Eval(Container.DataItem, "PAGE_URL") )%>"><i class="menu-icon fa <%# DataBinder.Eval(Container.DataItem, "MENU_ICON")%>"></i><span
        class="menu-text"><%# DataBinder.Eval(Container.DataItem, "MENU_NAME")%></span></a><b class="arrow"></b></li>
            --%>
        </ItemTemplate>
    </asp:Repeater>
   <%-- 
   <li class=""><a href="index.html"><i class="menu-icon fa fa-tachometer"></i><span
   class="menu-text">Dashboard </span></a><b class="arrow"></b></li>


    <li class=""><a href="#" class="dropdown-toggle"><i class="menu-icon fa fa-desktop">
    </i><span class="menu-text">UI &amp; Elements </span><b class="arrow fa fa-angle-down">
    </b></a><b class="arrow"></b>



        <ul class="submenu">
            <li class=""><a href="#" class="dropdown-toggle"><i class="menu-icon fa fa-caret-right">
            </i>Layouts <b class="arrow fa fa-angle-down"></b></a><b class="arrow"></b>
                <ul class="submenu">
                    <li class=""><a href="top-menu.html"><i class="menu-icon fa fa-caret-right"></i>Top
                        Menu </a><b class="arrow"></b></li>
                    <li class=""><a href="mobile-menu-1.html"><i class="menu-icon fa fa-caret-right"></i>
                        Default Mobile Menu </a><b class="arrow"></b></li>
                    <li class=""><a href="mobile-menu-2.html"><i class="menu-icon fa fa-caret-right"></i>
                        Mobile Menu 2 </a><b class="arrow"></b></li>
                    <li class=""><a href="mobile-menu-3.html"><i class="menu-icon fa fa-caret-right"></i>
                        Mobile Menu 3 </a><b class="arrow"></b></li>
                </ul>
            </li>
            <li class=""><a href="typography.html"><i class="menu-icon fa fa-caret-right"></i>Typography
            </a><b class="arrow"></b></li>
            <li class=""><a href="elements.html"><i class="menu-icon fa fa-caret-right"></i>Elements
            </a><b class="arrow"></b></li>
            <li class=""><a href="buttons.html"><i class="menu-icon fa fa-caret-right"></i>Buttons
                &amp; Icons </a><b class="arrow"></b></li>
            <li class=""><a href="treeview.html"><i class="menu-icon fa fa-caret-right"></i>Treeview
            </a><b class="arrow"></b></li>
            <li class=""><a href="jquery-ui.html"><i class="menu-icon fa fa-caret-right"></i>jQuery
                UI </a><b class="arrow"></b></li>
            <li class=""><a href="nestable-list.html"><i class="menu-icon fa fa-caret-right"></i>
                Nestable Lists </a><b class="arrow"></b></li>
            <li class=""><a href="#" class="dropdown-toggle"><i class="menu-icon fa fa-caret-right">
            </i>Three Level Menu <b class="arrow fa fa-angle-down"></b></a><b class="arrow"></b>
                <ul class="submenu">
                    <li class=""><a href="#" ><i class="menu-icon fa fa-leaf green"></i>Item #1 </a><b
                        class="arrow"></b></li>
                    <li class=""><a href="#" class="dropdown-toggle"><i class="menu-icon fa fa-pencil orange">
                    </i>4th level <b class="arrow fa fa-angle-down"></b></a><b class="arrow"></b>
                        <ul class="submenu">
                            <li class=""><a href="#"><i class="menu-icon fa fa-plus purple"></i>Add Product </a>
                                <b class="arrow"></b></li>
                            <li class=""><a href="#"><i class="menu-icon fa fa-eye pink"></i>View Products </a><b
                                class="arrow"></b></li>
                        </ul>
                    </li>
                </ul>
            </li>
        </ul>
    </li>
    <li class=""><a href="#" class="dropdown-toggle"><i class="menu-icon fa fa-list"></i>
        <span class="menu-text">Tables </span><b class="arrow fa fa-angle-down"></b></a>
        <b class="arrow"></b>
        <ul class="submenu">
            <li class=""><a href="tables.html"><i class="menu-icon fa fa-caret-right"></i>Simple
                &amp; Dynamic </a><b class="arrow"></b></li>
            <li class=""><a href="jqgrid.html"><i class="menu-icon fa fa-caret-right"></i>jqGrid
                plugin </a><b class="arrow"></b></li>
        </ul>
    </li>
    <li class=""><a href="#" class="dropdown-toggle"><i class="menu-icon fa fa-pencil-square-o">
    </i><span class="menu-text">Forms </span><b class="arrow fa fa-angle-down"></b></a><b
        class="arrow"></b>
        <ul class="submenu">
            <li class=""><a href="form-elements.html"><i class="menu-icon fa fa-caret-right"></i>
                Form Elements </a><b class="arrow"></b></li>
            <li class=""><a href="form-wizard.html"><i class="menu-icon fa fa-caret-right"></i>Wizard
                &amp; Validation </a><b class="arrow"></b></li>
            <li class=""><a href="wysiwyg.html"><i class="menu-icon fa fa-caret-right"></i>Wysiwyg
                &amp; Markdown </a><b class="arrow"></b></li>
            <li class=""><a href="dropzone.html"><i class="menu-icon fa fa-caret-right"></i>Dropzone
                File Upload </a><b class="arrow"></b></li>
        </ul>
    </li>
    <li class=""><a href="widgets.html"><i class="menu-icon fa fa-list-alt"></i><span
        class="menu-text">Widgets </span></a><b class="arrow"></b></li>
    <li class=""><a href="calendar.html"><i class="menu-icon fa fa-calendar"></i><span
        class="menu-text">Calendar <span class="badge badge-transparent tooltip-error" title="2 Important Events">
            <i class="ace-icon fa fa-exclamation-triangle red bigger-130"></i></span></span>
    </a><b class="arrow"></b></li>
    <li class=""><a href="gallery.html"><i class="menu-icon fa fa-picture-o"></i><span
        class="menu-text">Gallery </span></a><b class="arrow"></b></li>
    <li class=""><a href="#" class="dropdown-toggle"><i class="menu-icon fa fa-tag"></i>
        <span class="menu-text">More Pages </span><b class="arrow fa fa-angle-down"></b>
    </a><b class="arrow"></b>
        <ul class="submenu">
            <li class=""><a href="profile.html"><i class="menu-icon fa fa-caret-right"></i>User
                Profile </a><b class="arrow"></b></li>
            <li class=""><a href="inbox.html"><i class="menu-icon fa fa-caret-right"></i>Inbox </a>
                <b class="arrow"></b></li>
            <li class=""><a href="pricing.html"><i class="menu-icon fa fa-caret-right"></i>Pricing
                Tables </a><b class="arrow"></b></li>
            <li class=""><a href="invoice.html"><i class="menu-icon fa fa-caret-right"></i>Invoice
            </a><b class="arrow"></b></li>
            <li class=""><a href="timeline.html"><i class="menu-icon fa fa-caret-right"></i>Timeline
            </a><b class="arrow"></b></li>
            <li class=""><a href="email.html"><i class="menu-icon fa fa-caret-right"></i>Email Templates
            </a><b class="arrow"></b></li>
            <li class=""><a href="login.html"><i class="menu-icon fa fa-caret-right"></i>Login &amp;
                Register </a><b class="arrow"></b></li>
        </ul>
    </li>
    <li class="active open"><a href="#" class="dropdown-toggle"><i class="menu-icon fa fa-file-o">
    </i><span class="menu-text">Other Pages <span class="badge badge-primary">5</span> </span>
        <b class="arrow fa fa-angle-down"></b></a><b class="arrow"></b>
        <ul class="submenu">
            <li class=""><a href="faq.html"><i class="menu-icon fa fa-caret-right"></i>FAQ </a><b
                class="arrow"></b></li>
            <li class=""><a href="error-404.html"><i class="menu-icon fa fa-caret-right"></i>Error
                404 </a><b class="arrow"></b></li>
            <li class=""><a href="error-500.html"><i class="menu-icon fa fa-caret-right"></i>Error
                500 </a><b class="arrow"></b></li>
            <li class=""><a href="grid.html"><i class="menu-icon fa fa-caret-right"></i>Grid </a>
                <b class="arrow"></b></li>
            <li class="active"><a href="blank.html"><i class="menu-icon fa fa-caret-right"></i>Blank
                Page </a><b class="arrow"></b></li>
        </ul>
    </li>--%>
</ul>
 