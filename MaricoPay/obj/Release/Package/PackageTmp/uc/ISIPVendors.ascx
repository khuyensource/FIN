<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ISIPVendors.ascx.cs" Inherits="MaricoPay.uc.ISIPVendors" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div style="text-align: left; margin-left: 20px">
    <table border="0">
        
        <tr>
            <td>
                Tên NCC:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="tbTenNCC" runat="server" Width="260px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTenNCC" runat="server" ControlToValidate="tbTenNCC"
                    Display="Dynamic" ErrorMessage="TenNCC" SetFocusOnError="True" ValidationGroup="GInsert"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Người liên hệ:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="tbNguoiLienHe" runat="server" Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                ĐTDĐ:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="tbDienThoaiDD" runat="server" Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Điện thoại bàn:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="tbDienThoaiBan" runat="server" Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Fax:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="tbFax" runat="server" Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Email:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="tbEmail" runat="server" Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Địa chỉ:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="tbDiaChi" runat="server" Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Ghi chú:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="tbGhiChu" runat="server" Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                VendorCodeSAP:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="tbVendorCodeSAP" runat="server" Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Hiệu lực:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:CheckBox ID="cbHieuLuc" runat="server" Checked="true"></asp:CheckBox>
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