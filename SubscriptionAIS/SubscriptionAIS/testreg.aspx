<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="testreg.aspx.vb" Inherits="AISSubscription.testreg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Shortcode
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>4503900</asp:ListItem>
            <asp:ListItem>4503901</asp:ListItem>
            <asp:ListItem>4503200</asp:ListItem>
            <asp:ListItem>4503201</asp:ListItem>
            <asp:ListItem>4503202</asp:ListItem>
            <asp:ListItem>4503203</asp:ListItem>
            <asp:ListItem>4503204</asp:ListItem>
        </asp:DropDownList>
&nbsp;subtopic
        <asp:DropDownList ID="DropDownList2" runat="server">
            <asp:ListItem>01</asp:ListItem>
            <asp:ListItem>02</asp:ListItem>
            <asp:ListItem>03</asp:ListItem>
            <asp:ListItem>04</asp:ListItem>
            <asp:ListItem>05</asp:ListItem>
            <asp:ListItem>06</asp:ListItem>
            <asp:ListItem>07</asp:ListItem>
            <asp:ListItem>08</asp:ListItem>
            <asp:ListItem>09</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
            <asp:ListItem>13</asp:ListItem>
            <asp:ListItem>14</asp:ListItem>
            <asp:ListItem>15</asp:ListItem>
            <asp:ListItem>16</asp:ListItem>
            <asp:ListItem>17</asp:ListItem>
            <asp:ListItem>18</asp:ListItem>
            <asp:ListItem>19</asp:ListItem>
            <asp:ListItem>20</asp:ListItem>
        </asp:DropDownList>
    
        <br />
        <br />
        <br />
        <asp:Button ID="btnSub" runat="server" Text="Sub" />
&nbsp;
     
    </div>
    <br />

    <div runat="server" id="dv"></div>
    </form>
</body>
</html>
