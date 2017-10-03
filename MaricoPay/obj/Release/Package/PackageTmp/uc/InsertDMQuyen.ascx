<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InsertDMQuyen.ascx.cs" Inherits="Controls_InsertDMQuyen" %>
<style type="text/css">
    .Rong
    {
        width: 100px;
    }
</style>
<br />
<div style="margin-left: 20px">
    <table border="0" cellpadding="2" cellspacing="2">
        <tr>
            <td class="Rong" align="left">
                IDQuyen:
            </td>
            <td style="width: 20px">
            </td>
            <td>
                <asp:TextBox ID="tbIDQuyen" runat="server" Width="185px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbIDQuyen"
                    Display="Dynamic" ErrorMessage="Nhập IDQuyen." SetFocusOnError="True" 
                    ValidationGroup="Ginsert"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="Rong" align="left">
                Tên quyền:
            </td>
            <td style="width: 20px">
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="tbTenQuyen" runat="server" Width="185px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbTenQuyen"
                    Display="Dynamic" ErrorMessage="Nhập tên Quyen." SetFocusOnError="True" 
                    ValidationGroup="Ginsert"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:ImageButton ID="btLuu" runat="server" ImageUrl="../Images/luu.png" ValidationGroup="Ginsert" AlternateText="Lưu"
                    CommandName="PerformInsert" />
                &nbsp;<asp:ImageButton ID="btnClose" runat="server" ImageUrl="../Images/dong.png" AlternateText="Đóng"
                    CommandName="Cancel" />
            </td>
        </tr>
    </table>
</div>
<br />