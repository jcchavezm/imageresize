<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="facebooksync.aspx.cs" Inherits="WebApplication2.facebooksync" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 163px;
        }

        .auto-style2 {
            width: 88px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <h2>Register User</h2>
<%--            <iframe src="https://www.facebook.com/plugins/registration?client_id=1402968979937332&redirect_uri=http://localhost:39180/facebooksync.aspx&fields=[{'name':'name'},{'name':'email'},{'name':'gender'},{'name':'birthday'},{'name':'password'},{'name':'captcha'}]"
                scrolling="auto"
                frameborder="no"
                style="border: none"
                allowtransparency="true"
                width="100%"
                height="450"></iframe>--%>

        </div>
        <table style="width: 100%;">
            <tr>
                <td class="auto-style1">Username
        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    Password
        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    E-mail
        <asp:TextBox ID="txtEmail" runat="server" OnTextChanged="TextBox4_TextChanged"></asp:TextBox>
                    <br />
                    <br />
                    Birthday
        <asp:TextBox ID="txtBirthday" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    Nickname
        <asp:TextBox ID="txtNickname" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    Country ID
        <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    Gender
        <asp:TextBox ID="txtGender" runat="server"></asp:TextBox>
                </td>
                </td>
                <td>
                    <asp:TextBox ID="TextBox8" runat="server" Height="216px" ReadOnly="True" TextMode="MultiLine" Width="647px" Style="margin-bottom: 0px"></asp:TextBox>
                    <br />
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <br />
                    <asp:Button ID="Button2" runat="server" Text="Send" OnClick="Button2_Click" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">
                    <asp:Button ID="Button1" runat="server" Text="Register" OnClick="Button1_Click" Width="114px" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">
                    <asp:Button ID="Button3" runat="server" Text="Login" OnClick="Button3_Click" Width="114px" />
                    <asp:Button ID="Button4" runat="server" Text="Logout" OnClick="Button4_Click" Width="114px" />
                    <br />
                    <asp:Button ID="Button5" runat="server" Text="Get Friends" Width="114px" OnClick="Button5_Click" />
                    <asp:Button ID="Button6" runat="server" Text="Recover Password" OnClick="Button6_Click" />
                </td>
                <td>
                    <asp:Button ID="Button8" runat="server" Text="Get Newsletter" OnClick="Button8_Click" />
                    <br />
                    <asp:Button ID="Button7" runat="server" Text="Add Newsletter" OnClick="Button7_Click" />
                </td>
            </tr>
        </table>

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        &nbsp;<br />
        &nbsp;<br />
        <br />
        <br />

    </form>
</body>
</html>
