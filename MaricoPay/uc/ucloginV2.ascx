<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucloginV2.ascx.cs" Inherits="uc_ucloginV2" %>
<link rel="stylesheet" href='<%=ResolveUrl("usercontrol.css")%>' type="text/css" />
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Block">
    <ContentTemplate>--%>
        <table cellpadding="5" cellspacing="0">
            <tr>
                <td>
                </td>
                <td valign="middle" align="center" class="lbLogin">
                    <asp:Literal ID="ltlogin" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr valign="middle">
                <td class="lblogin">
                    Email
                </td>
                <td align="left">
                    <asp:TextBox ID="txtemail" runat="server" placeholder="Email" class="input"/>
                </td>
            </tr>
            <tr>
                <td class="lblogin">
                    <asp:Literal ID="pass" runat="server"></asp:Literal>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtpass" TextMode="Password" runat="server" placeholder="Password"
                        class="input"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="center" class="bogocbt" style="border-style: groove;">
                    <%--   <asp:ImageButton ID="btlogin" ImageUrl="../../admincp/Top/images/login.png" runat="server"
                    AlternateText="Login" onmouseover="this.src='admincp/Top/images/loginOver.png'"
                    onmouseout="this.src='admincp/Top/images/login.png'" OnClick="btlogin_Click" />--%>
                    <asp:Button ID="btlogin" CssClass="btlogin" runat="server" Text="Login" BackColor="Transparent"
                        Style="cursor: pointer;" Width="100%" OnClick="btlogin_Click" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="center">
                    <table>
                        <tr>
                            <td>
                                <%--<a href="resetpass.aspx" class="forgot">--%>
                                   <a href="../../resetpass.aspx" id="forgot" class="forgot" runat="server">
                                    <asp:Literal ID="ltforgetpass" runat="server"></asp:Literal></a>
                            </td>
                            <td>
                                &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                            </td>
                            <td align="right">
                                <%-- <a href="registermember.aspx" class="forgot">--%>
                                  <a href="../../registermember.aspx" id="A1" class="forgot" runat="server">
                                    <asp:Literal ID="ltregis" runat="server"></asp:Literal></a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    <%--</ContentTemplate>
</asp:UpdatePanel>--%>
