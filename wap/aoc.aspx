<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="aoc.aspx.vb" Inherits="wap.aoc" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="th">
<head runat="server">
    <meta charset="utf-8">
    <title>aoc</title>
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;">
    <link rel="Stylesheet" href="../scripts/jquery.mobile-1.4.1.min.css" />
    <link rel="Stylesheet" href="../scripts/jquery.mobile.theme-1.4.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.4.1/jquery.mobile-1.4.1.min.js"></script>
    <script type="text/javascript">
		<!--
        function click() {
            return 'tel:*<% =ivrReg %>';
        }

        function redirect(sec, enable) {
            if (enable) {
                setTimeout(function () {
                    //window.location.href=click();
                    $(location).attr('href', click());
                }, sec * 1000);
            }
        }

        function getUrlVar(key) {
            var result = new RegExp(key + "=([^&]*)", "i").exec(window.location.search);
            return result && unescape(result[1]) || "";
        }
        (function ($) {
            $.getUrlVar = function (key) {
                var result = new RegExp(key + "=([^&]*)", "i").exec(window.location.search);
                return result && unescape(result[1]) || "";
            };
        })(jQuery);

        $(document).ready(function () {
            redirect(7, true);
            //var url = "sms:4170710?body=R+A4";    
            //url = "tel:*4444444";
            //$(location).attr('href', url);
            //window.location.replace("tel:*4444444");
        });
		//-->
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <div id="servicedetail" runat="server"> 
            <img src="../images/logo-s.png" width="100" /><br />
            คุณต้องการสมัครบริการ รับคลิปวิดีโอรายวัน <% =ServiceName%> (<% =Price%> บาทต่อข้อความ ไม่รวมภาษี)
            โทร <strong>*<% =ivrReg%></strong> หรือพิมพ์ <strong><% =ComReg%></strong> ส่งมาที่ <strong><% =ShortCode%></strong><br />
            หรือ
            <br />
            <a href="tel:*<% =ivrReg %>" class="ui-btn ui-btn-inline"><font size="5"><strong>
                สมัครบริการ คลิกที่นี่</strong></font></a><br />
            ผู้สมัครสมาชิกจะได้รับ 1 ข้อความ/วัน ยกเลิกบริการโทร *<% =ivrUnReg%> หรือพิมพ์ <% = ComUnReg%> ส่งมาที่
            <% =ShortCode%> สอบถามโทร 02 684 0922 เวลา 9:00 – 18:00 (จ.-ศ.) ให้บริการโดย Mobindy
        </div>
    </center>
    </form>
</body>
</html>
