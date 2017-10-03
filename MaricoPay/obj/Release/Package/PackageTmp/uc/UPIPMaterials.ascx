<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UPIPMaterials.ascx.cs"
    Inherits="MaricoPay.uc.UPIPMaterials" %>
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
                <asp:CheckBox ID="cbActive" runat="server"></asp:CheckBox>
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
    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# fGet(Eval("ID"),Eval("Name"),Eval("Note"),Eval("SAPCode"),Eval("Active"))%>' />
</div>
