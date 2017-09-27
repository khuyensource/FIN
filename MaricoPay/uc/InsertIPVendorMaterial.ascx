<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InsertIPVendorMaterial.ascx.cs"
    Inherits="MaricoPay.uc.InsertIPVendorMaterial" %>
<div style="text-align: left; margin-left: 20px">
    <table border="0">
        
        <tr>
            <td>
                Material:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <telerik:RadComboBox ID="RadComboMaterial" Width="260px" runat="server">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Vendor:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <telerik:RadComboBox ID="RadComboVendor" Width="260px" runat="server">
                </telerik:RadComboBox>
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
