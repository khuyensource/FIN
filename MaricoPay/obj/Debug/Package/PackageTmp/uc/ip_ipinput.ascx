<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ip_ipinput.ascx.cs"
    Inherits="MaricoPay.uc.ip_ipinput" %>

<%@ Register Src="ucMsgBox.ascx" TagName="ucMsgBox" TagPrefix="uc1" %>

<div>





    <telerik:RadGrid ID="RGNCC" Width="100%" runat="server" AutoGenerateColumns="False"
        EnableLinqExpressions="false" HeaderStyle-BackColor="Chartreuse"
        GridLines="Both" AllowPaging="False" ShowHeader="true"
        AllowFilteringByColumn="False" AllowSorting="False"
        ShowGroupPanel="False">
        <HeaderStyle BackColor="Chartreuse" />
        <HeaderContextMenu EnableTheming="True">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </HeaderContextMenu>
        <GroupingSettings CaseSensitive="False" />
        <MasterTableView DataKeyNames="ID">

            <Columns>
                <telerik:GridTemplateColumn HeaderText="ID" UniqueName="ID" DataField="ID" AllowFiltering="False" Display="false">
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Gởi lại">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox2" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn HeaderText="Chọn NCC">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# fBool(Eval("chon")) %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn HeaderText="TenNCC" UniqueName="TenNCC" DataField="TenNCC">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="NguoiLienHe" UniqueName="NguoiLienHe" DataField="NguoiLienHe">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="DienThoaiDD" UniqueName="DienThoaiDD" DataField="DienThoaiDD">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="DienThoaiBan" UniqueName="DienThoaiBan" DataField="DienThoaiBan">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Fax" UniqueName="Fax" DataField="Fax">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Email" UniqueName="Email" DataField="Email">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="DiaChi" UniqueName="DiaChi"
                    DataField="DiaChi">
                </telerik:GridBoundColumn>

            </Columns>
            <EditFormSettings EditFormType="WebUserControl">
                <EditColumn UniqueName="EditCommandColumn1">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True" />
            <%-- <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>--%>
        </ClientSettings>
    </telerik:RadGrid>
    <br />
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server">Số ngày NCC phải gởi báo giá /The number of days the supplier must send a quotation </asp:Label>
            </td>
            <td>
                
                <telerik:RadNumericTextBox ID="radnumNgaybaogia" Runat="server" Value="2">
<NegativeStyle Resize="None"></NegativeStyle>

<NumberFormat ZeroPattern="n" DecimalDigits="0"></NumberFormat>

<EmptyMessageStyle Resize="None"></EmptyMessageStyle>

<ReadOnlyStyle Resize="None"></ReadOnlyStyle>

<FocusedStyle Resize="None"></FocusedStyle>

<DisabledStyle Resize="None"></DisabledStyle>

<InvalidStyle Resize="None"></InvalidStyle>

<HoveredStyle Resize="None"></HoveredStyle>

<EnabledStyle Resize="None"></EnabledStyle>
                </telerik:RadNumericTextBox>
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label22" runat="server">Áp dụng cho các mặt hàng tương tự / Applies to similar items </asp:Label>

            </td>
            <td>
                <asp:CheckBox ID="chkApdung" runat="server" />
            </td>
        </tr>
    </table>


    <br />

    <asp:ImageButton ID="btnEdit" runat="server" CommandName="Update" ImageUrl="~/images/luuEn.png"
        ValidationGroup="GEdit" OnClick="btnEdit_Click" />
    &nbsp;<asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/Images/dongEn.png"
        CommandName="Cancel" />
    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# fGet(Eval("IDPRDetail"),Eval("IDPR"),Eval("IDMaterial"))%>' />
    <asp:HiddenField ID="HFIDPRDetail" runat="server" />
    <asp:HiddenField ID="HFIDPR" runat="server" />
    <asp:HiddenField ID="HfIDMaterial" runat="server" />
</div>
