<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCurrence.ascx.cs"
    Inherits="MaricoPay.uc.ucCurrence" %>
<table>
    <tr>
        <td>
            <asp:DropDownList ID="dropCurr" runat="server" AutoPostBack="True" 
                onselectedindexchanged="dropCurr_SelectedIndexChanged" Font-Names="Arial" 
                Font-Size="small">
            </asp:DropDownList>
        </td>
        <td style="color: #000000; width:40px; font-size:small; font-family:@Arial Unicode MS;">Tỉ giá<br />Rate</td>
        <td>
            <asp:TextBox ID="txtTiGia" runat="server" Width="70px" Font-Names="Arial" 
                Font-Size="small"></asp:TextBox>
        </td>
    </tr>
</table>
