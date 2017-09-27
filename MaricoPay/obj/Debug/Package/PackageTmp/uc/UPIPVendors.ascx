<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UPIPVendors.ascx.cs" Inherits="MaricoPay.uc.UPIPVendors" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div style="text-align: left; margin-left: 20px">
    <table cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td>
                ID:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <telerik:RadNumericTextBox Enabled="false" ID="rnID" runat="server" DataType="System.Int64"
                    MinValue="0" Value="0" Width="260px">
                    <IncrementSettings InterceptArrowKeys="False" InterceptMouseWheel="False" />
                </telerik:RadNumericTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorID" runat="server" ControlToValidate="rnID"
                    Display="Dynamic" ErrorMessage="ID" SetFocusOnError="True" ValidationGroup="GEdit"></asp:RequiredFieldValidator>
            </td>
        </tr>
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
                    Display="Dynamic" ErrorMessage="TenNCC" SetFocusOnError="True" ValidationGroup="GEdit"></asp:RequiredFieldValidator>
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
                ĐT bàn:
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
                <asp:CheckBox ID="cbHieuLuc" runat="server"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:ImageButton ID="btnEdit" runat="server" CommandName="Update" ImageUrl="~/images/luuEn.png"
                    ValidationGroup="GEdit" />
                &nbsp;<asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/Images/dongEn.png"
                    CommandName="Cancel" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# fGet(Eval("ID"),Eval("TenNCC"),Eval("NguoiLienHe"),Eval("DienThoaiDD"),Eval("DienThoaiBan"),Eval("Fax"),Eval("Email"),Eval("DiaChi"),Eval("GhiChu"),Eval("VendorCodeSAP"),Eval("HieuLuc"))%>' />
</div>
