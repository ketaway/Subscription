<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="wap.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <a href="http://m.mobindy.com/wap/chkoperator.ashx?curl=http://m.mobindy.com/wap/webform1.aspx">click test check operator</a>
    <asp:Label ID=lbl runat=server></asp:Label>
                    <input class="form-control" type="text" name="date-range-picker" id="dtRange" runat="server"   />
                    <asp:Button ID="d" runat="server" />
    </div>
    </form>
</body>
</html>
