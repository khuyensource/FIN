<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InsertIPRequest.ascx.cs"
    Inherits="MaricoPay.uc.InsertIPRequest" %>
<%--<%@ Register Src="ucComboDepartment.ascx" TagName="department" TagPrefix="uc5" %>--%>
<style type="text/css">
    .auto-style1
    {
        color: #FF0000;
    }
</style>
<div style="text-align: left; margin-left: 20px">
    <table border="0" cellpadding="3" cellspacing="2">
        <tr>
            <td>
                Internal order (IO) no <span class="auto-style1">(*) </span>:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <telerik:RadComboBox ID="RadComboIO" DataValueField="ASPNo" DataTextField="Objective"
                    runat="server" Width="450px" Filter="Contains"  MarkFirstMatch="true" AllowCustomText="True" Font-Names="Arial"
                    Font-Size="Small">
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorID" runat="server" ControlToValidate="RadComboIO"
                    Display="Dynamic" ErrorMessage="Input IO" SetFocusOnError="True" ValidationGroup="GEdit"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Cost center <span class="auto-style1">(*) </span>
            </td>
            <td>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="radcomboCostcenter" runat="server" Width="150px" DataTextField="Description" DropDownWidth="300px"
                                DataValueField="CostCenter" Filter="Contains" MarkFirstMatch="true" AllowCustomText="True"  Font-Size="Small" Font-Names="Arial">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr valign="middle">
                                            <td style="width: 150px; text-align: left;">
                                                <div style="font-size: 11;">
                                                    <asp:Label ID="lbDescription" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                </div>
                                            </td>
                                            <td style="width: 70px; text-align: left;">
                                                <div style="font-size: 11;">
                                                    <asp:Label ID="lbCostCenter" runat="server" Text='<%# Eval("CostCenter")%>'></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="radcomboCostcenter"
                                Display="Dynamic" ErrorMessage="Input Costcenter" SetFocusOnError="True" ValidationGroup="GEdit"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            GL
                        </td>
                        <td>
                            <telerik:RadComboBox ID="radcomboGL" runat="server" Width="150px" 
                                HighlightTemplatedItems="True" DataTextField="Description" DropDownWidth="400px"
                                DataValueField="GL" Filter="Contains" MarkFirstMatch="true"  AllowCustomText="True"  Font-Size="Small" Font-Names="Arial">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr valign="middle">
                                            <td style="width: 250px; text-align: left;">
                                                <div style="font-size: 11;">
                                                    <asp:Label ID="lbDescriptionGL" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                </div>
                                            </td>
                                            <td style="width: 70px; text-align: left;">
                                                <div style="font-size: 11;">
                                                    <asp:Label ID="lbGL" runat="server" Text='<%# Eval("GL")%>'></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            Material group
                        </td>
                        <td>
                            <telerik:RadComboBox ID="radcomboMatrialGroup" runat="server" Width="150px" DataTextField="Description" DropDownWidth="300px"
                                DataValueField="MaterialGroup" Filter="Contains" MarkFirstMatch="true"  AllowCustomText="True"  Font-Size="Small" Font-Names="Arial">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr valign="middle">
                                            <td style="width: 150px; text-align: left;">
                                                <div style="font-size: 11;">
                                                    <asp:Label ID="lbDescriptionMaterialGroup" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                </div>
                                            </td>
                                            <td style="width: 70px; text-align: left;">
                                                <div style="font-size: 11;">
                                                    <asp:Label ID="lbMaterialGroup" runat="server" Text='<%# Eval("MaterialGroup")%>'></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                Profit center
            </td>
            <td>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="radcomboProfitcenter" runat="server" Width="150px" DataTextField="Description" DropDownWidth="300px"
                                DataValueField="Profitcenter" Filter="Contains"  MarkFirstMatch="true" AllowCustomText="True"  Font-Size="Small" Font-Names="Arial">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr valign="middle">
                                            <td style="width: 150px; text-align: left;">
                                                <div style="font-size: 11;">
                                                    <asp:Label ID="lbDescriptionProfitcenter" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                </div>
                                            </td>
                                            <td style="width: 70px; text-align: left;">
                                                <div style="font-size: 11;">
                                                    <asp:Label ID="lbProfitcenter" runat="server" Text='<%# Eval("Profitcenter")%>'></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            Country
                        </td>
                        <td>
                            <telerik:RadComboBox ID="radcomboCountry" runat="server" Width="150px" DataTextField="Description" DropDownWidth="300px"
                                DataValueField="Country"  Filter="Contains" AllowCustomText="True"  MarkFirstMatch="true"  Font-Size="Small" Font-Names="Arial">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr valign="middle">
                                            <td style="width: 150px; text-align: left;">
                                                <div style="font-size: 11;">
                                                    <asp:Label ID="lbDescriptionCountry" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                </div>
                                            </td>
                                            <td style="width: 70px; text-align: left;">
                                                <div style="font-size: 11;">
                                                    <asp:Label ID="lbCountry" runat="server" Text='<%# Eval("Country")%>'></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            Division
                        </td>
                        <td>
                            <telerik:RadComboBox ID="radcomboDivision" runat="server" Width="150px" DataTextField="Description" DropDownWidth="300px"
                                DataValueField="Division"  Filter="Contains" AllowCustomText="True"  MarkFirstMatch="true"  Font-Size="Small" Font-Names="Arial">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr valign="middle">
                                            <td style="width: 150px; text-align: left;">
                                                <div style="font-size: 11;">
                                                    <asp:Label ID="lbDescriptionDivision" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                </div>
                                            </td>
                                            <td style="width: 70px; text-align: left;">
                                                <div style="font-size: 11;">
                                                    <asp:Label ID="lbDivision" runat="server" Text='<%# Eval("Division")%>'></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                Sales group
            </td>
            <td>
            </td>
            <td>
                <telerik:RadComboBox ID="radcomboSalesGroup" runat="server" Width="150px" DataTextField="Description" DropDownWidth="300px"
                    DataValueField="SalesGroup" Filter="Contains" AllowCustomText="True"  MarkFirstMatch="true"  Font-Size="Small" Font-Names="Arial">
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr valign="middle">
                                <td style="width: 150px; text-align: left;">
                                    <div style="font-size: 11;">
                                        <asp:Label ID="lbDescriptionSalesGroup" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                    </div>
                                </td>
                                <td style="width: 70px; text-align: left;">
                                    <div style="font-size: 11;">
                                        <asp:Label ID="lbSalesGroup" runat="server" Text='<%# Eval("SalesGroup")%>'></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Mã hàng-Item <span class="auto-style1">(*) </span>:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <telerik:RadComboBox ID="RadComboMaterial" DataTextField="Name" DataValueField="ID"
                    Width="450px" runat="server" Filter="Contains" AllowCustomText="True" Font-Names="Arial" MarkFirstMatch="true" 
                    Font-Size="Small">
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadComboMaterial"
                    Display="Dynamic" ErrorMessage="Input item" SetFocusOnError="True" ValidationGroup="GEdit"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Kích thước-Size:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtKichThuoc" runat="server" Width="450px" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Chất liệu-Material:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtChatLieu" runat="server" Width="450px" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Độ dày-Thickness:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtDoDay" runat="server" Width="450px" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Có đèn-Lighting:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:CheckBox ID="chkCoDen" runat="server"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td>
                Số lượng-Quantity <span class="auto-style1">(*) </span>:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <telerik:RadNumericTextBox ID="radnumSoLuong" runat="server" Width="450px" ReadOnly="False"
                    Value="0" Font-Names="Arial" Font-Size="Small" MinValue="0">
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radnumSoLuong"
                    Display="Dynamic" ErrorMessage="Input Quantity" SetFocusOnError="True" ValidationGroup="GEdit"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Thiết kế đính kèm-Design attached:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" MaxFileInputsCount="1"
                    Width="450px">
                </telerik:RadAsyncUpload>
                <%-- <asp:CustomValidator ID="ctruImage" runat="server" ClientValidationFunction="validateUpload"
                    ValidationGroup="GEdit" Display="Dynamic" CssClass="error">                         
                </asp:CustomValidator>--%>
            </td>
        </tr>
        <tr>
            <td>
                Ngày giao hàng-Require delivery on <span class="auto-style1">(*) </span>:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <telerik:RadDatePicker ID="radDateNgayGiao" runat="server" Font-Names="Arial" Font-Size="Small"
                    Width="450px">
                    <Calendar ID="Calendar1" runat="server" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"
                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                    </Calendar>
                    <DateInput ID="DateInput1" runat="server" DateFormat="dd-MMM-yy" DisplayDateFormat="dd-MMM-yy"
                        LabelWidth="40%">
                        <EmptyMessageStyle Resize="None" />
                    </DateInput>
              <%--      <DatePopupButton HoverImageUrl="" ImageUrl="" />--%>
                </telerik:RadDatePicker>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="radDateNgayGiao"
                    Display="Dynamic" ErrorMessage="Input delivery date" SetFocusOnError="True" ValidationGroup="GEdit"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Nơi nhận-Received Location <span class="auto-style1">(*) </span>:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <telerik:RadComboBox ID="dropNoiNhan" Width="450px" runat="server" Filter="Contains"
                    AllowCustomText="True" Font-Names="Arial" Font-Size="Small" ResolvedRenderMode="Classic">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Kho ICD số 6, 7/20, Đường ĐT 743, Khu phố Bình Đáng, Phường Bình Hòa, Thị xã Thuận An, Tỉnh Bình Dương"
                            Value="1" />
                        <telerik:RadComboBoxItem runat="server" Text="Kho Thuận Phát, số 7, block D2, KCN Lê Minh Xuân, Bình Chánh, TPHCM"
                            Value="2" />
                        <telerik:RadComboBoxItem runat="server" Text="Văn Phòng Marico, tầng 28, tòa nhà Pearl Plaza, 561A Điện Biên Phủ, Quận Bình Thạnh, TPHCM"
                            Value="3" />
                             <telerik:RadComboBoxItem runat="server" Text="Kho ICD số 3, 7/20, Đường ĐT 743, Khu phố Bình Đáng, Phường Bình Hòa, Thị xã Thuận An, Tỉnh Bình Dương"
                            Value="4" />
                    </Items>
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="dropNoiNhan"
                    Display="Dynamic" ErrorMessage="Input nơi nhận" SetFocusOnError="True" ValidationGroup="GEdit"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Chi tiết khác cần lưu ý-Other specification:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="tbNote" runat="server" Width="450px" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:ImageButton ID="btnInsert" runat="server" CommandName="PerformInsert" ImageUrl="~/images/luuEn.png"
                    ValidationGroup="GEdit" />
                &nbsp;<asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/Images/dongEn.png"
                    CommandName="Cancel" />
            </td>
        </tr>
    </table>
</div>
