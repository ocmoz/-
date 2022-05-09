<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>高速公路机电管理系统</title>

    <script type="text/javascript" language="javascript" src="<%=Page.ResolveClientUrl("~/js/checkform.js")%>"
        charset="utf-8"></script>

    <style type="text/css">
        .style1
        {
            height: 189px;
        }
        .style2
        {
            height: 189px;
            width: 591px;
        }
        .style3
        {
            width: 591px;
        }
        .style4
        {
            width: 591px;
            height: 47px;
        }
        .style5
        {
            height: 47px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" onsubmit="javascript:return checkForm(this)">
    <div style="vertical-align: middle;">
        <div style="background-image: url(images/bg.jpg); width: 1004px; height: 520px; margin: auto auto auto auto;">
            <table>
                <tr>
                    <td class="style2">
                    </td>
                    <td class="style1">
                    </td>
                    <td class="style1">
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                    </td>
                    <td>
                        <div style="text-align: left">用户名:
                            <asp:TextBox ID="TextBox1" runat="server" title="请输入用户名~!" Style="border: solid 1px #0077B2;
                                height: 23px"></asp:TextBox></div>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                    </td>
                    <td class="style5">
                        <div style="text-align: left">密&nbsp;&nbsp;码:
                            <asp:TextBox ID="TextBox2" runat="server" title="请输入密码~!" TextMode="Password" Style="border: solid 1px #0077B2;
                                height: 23px"></asp:TextBox></div>
                        <div style="display: none;">
                            <asp:TextBox ID="TextBox3" runat="server" Columns="4" title="请输入验证码~4:!" Style="border: solid 1px #0077B2;
                                height: 23px"></asp:TextBox>
                            <img src="" id="ImageCheck" align="absmiddle" style="cursor: pointer" onclick="javascript:ChangeCodeImg();"
                                title="点击更换验证码图片!" /></div>
                    </td>
                    <td class="style5">
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                    </td>
                    <td>
                        <div>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/loginbutton.gif"
                                OnClick="ImageButton1_Click" /></div>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>

<script language="JavaScript" type="text/javascript">

    rnd.today = new Date();

    rnd.seed = rnd.today.getTime();

    function rnd() {
        rnd.seed = (rnd.seed * 9301 + 49297) % 233280;
        return rnd.seed / (233280.0);

    };

    function rand(number) {
        return Math.ceil(rnd() * number);

    }; 

</script>

<script language="javascript" type="text/javascript">
    ChangeCodeImg();
    function ChangeCodeImg() {
        a = document.getElementById("ImageCheck");
        a.src = "inc/CodeImg.aspx?" + rand(10000000);
    }
</script>

