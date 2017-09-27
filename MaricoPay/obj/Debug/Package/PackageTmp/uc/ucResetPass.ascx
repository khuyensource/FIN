<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucResetPass.ascx.cs" Inherits="MaricoPay.uc.ucResetPass" %>
 <center>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="bg_regis_L" valign="middle" align="center">
                </td>
                <td class="bg_regis_C">
                   
                    <table>
                    <tr>
                    <td colspan="2">
                    <asp:Literal ID="titleresetpass" Text="Reset your password login Fin system" runat="server"></asp:Literal>
                    </td>
                 
                    </tr>
                        <tr align="left">
                            <td>
                                <asp:Literal ID="ltresetpass" Text="Email" runat="server"></asp:Literal>
                            </td>
                            <td>
                                 <asp:TextBox ID="txtemail" runat="server" placeholder="Email" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr align="center">
                            <td colspan="2">
                                <asp:Button ID="btsave" runat="server" Text="Reset Password" OnClick="btsave_Click" />
                                <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="1000" 
                                    ontick="Timer1_Tick">
                                </asp:Timer>
                            </td>
                        </tr> <tr align="center">
                            <td colspan="2">
                                <asp:Label ID="lbThongBao" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="bg_regis_R">
                </td>
            </tr>
        </table>
    </center>