<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucChangepass.ascx.cs" Inherits="MaricoPay.uc.ucChangepass" %>
<center>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="bg_regis_L" valign="middle" align="center">
                </td>
                <td class="bg_regis_C">
                    <table cellpadding="2" cellspacing="2" border="0">
                        <tr>
                            <td align="left">
                                <asp:Literal ID="ltcurentpass" Text="Old password" runat="server"></asp:Literal>
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtmkcu" runat="server" Width="180px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Literal ID="ltnewpass" Text="New password" runat="server"></asp:Literal> 
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="tbPass" runat="server" Width="180px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Literal ID="ltretypepass" Text="Confirm new password" runat="server"></asp:Literal> 
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="tbPasslan2" runat="server" Width="180px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Literal ID="ltLoi" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hfflag" runat="server" />
                                <asp:HiddenField ID="hfemail" runat="server" />
                                 <asp:HiddenField ID="hfuser" runat="server" />
                                <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="1000" 
                                    ontick="Timer1_Tick">
                                </asp:Timer>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="btDangNhap" runat="server" OnClick="btDangNhap_Click" Text="OK" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="bg_regis_R">
                </td>
            </tr>
        </table>
    </center>