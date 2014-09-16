<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="SubscriptionDTAC._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        ServiceID&nbsp;&nbsp;
        <asp:TextBox ID="txtServiceid" runat="server"></asp:TextBox>
        <br />
        <br />
        MSN&nbsp;&nbsp;
        <asp:TextBox ID="txtMSN" runat="server"></asp:TextBox>
        <br />
        <br />
        Text&nbsp;&nbsp;
        <asp:TextBox ID="txtText" runat="server"></asp:TextBox>
        <br />
        <br />
        Sender&nbsp;&nbsp;
        <asp:TextBox ID="txtSender" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Button" />
        <br />
        <asp:TextBox ID="txtResult" runat="server" Height="209px" TextMode="MultiLine" 
            Width="859px"></asp:TextBox>
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
