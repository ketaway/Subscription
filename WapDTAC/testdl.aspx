<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="testdl.aspx.vb" Inherits="WapDTAC.testdl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <a href="http://sdpwap.dtac.co.th/portalone/aoc/display.action?ch=wap&cmd=WPRD&cpid=503&svid=45030016&cprefid=0002&lc=en&cid=12345<% = now.tostring("mmss") %>">click to download</a>
    </div>
    
    </form>
</body>
</html>
