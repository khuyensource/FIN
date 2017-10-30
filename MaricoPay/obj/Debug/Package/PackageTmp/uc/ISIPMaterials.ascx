<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ISIPMaterials.ascx.cs"
    Inherits="MaricoPay.uc.ISIPMaterials" %>
<div style="text-align: left; margin-left: 20px">
    <table border="0">
        
        <tr>
            <td>
                Name:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="tbName" runat="server" Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Note:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="tbNote" runat="server" Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                SAPCode:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="tbSAPCode" runat="server" Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Active:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:CheckBox ID="cbActive" runat="server" Checked="true"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:ImageButton ID="btnInsert" runat="server" CommandName="PerformInsert" ImageUrl="~/images/luuEn.png"
                    ValidationGroup="GInsert" />
                &nbsp;<asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/Images/dongEn.png"
                    CommandName="Cancel" />
            </td>
        </tr>
    </table>
</div>
