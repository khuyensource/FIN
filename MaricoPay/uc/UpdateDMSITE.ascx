<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UpdateDMSITE.ascx.cs" Inherits="Controls_UpdateDMSITE" %>
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
                IDSITE:
            </td>
            <td style="width: 20px">
            </td>
            <td>
                <asp:TextBox ID="tbIDSITE" runat="server" Width="185px" Text='<%# Bind( "IDSITE") %>'></asp:TextBox>
                <asp:HiddenField ID="hdIDSITE" runat="server" Value='<%# Bind( "IDSITE") %>' />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbIDSITE"
                    Display="Dynamic" ErrorMessage="Nhập IDSITE." SetFocusOnError="True" 
                    ValidationGroup="Gedit"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="Rong" align="left">
                Tên SITE:
            </td>
            <td style="width: 20px">
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="tbTenSite" runat="server" Width="185px" Text='<%# Bind( "TENSITE") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbTenSite"
                    Display="Dynamic" ErrorMessage="Nhập tên SITE." SetFocusOnError="True" 
                    ValidationGroup="Gedit"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="Rong" align="left">
                Hiệu lực:
            </td>
            <td style="width: 20px">
                &nbsp;
            </td>
            <td>
                <asp:CheckBox ID="cbHieuLuc" runat="server" Checked='<%# fBool(DataBinder.Eval(Container, "DataItem.HieuLuc").ToString()) %>' />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:ImageButton ID="btLuu" runat="server" ImageUrl="../images/luu.png" ValidationGroup="Gedit" AlternateText="Lưu"
                    CommandName="Update" />
                &nbsp;<asp:ImageButton ID="btnClose" runat="server" ImageUrl="../images/dong.png" AlternateText="Đóng"
                    CommandName="Cancel" />
            </td>
        </tr>
    </table>
</div>
<br />