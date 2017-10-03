<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_IP_NCCInput.ascx.cs" Inherits="MaricoPay.uc.UC_IP_NCCInput" %>

<style type="text/css">
    .auto-style1
    {
        color: #FF0000;
    }
</style>
<div>

    

    <br />
    <table>
        <tr>
            <td>Đơn giá 1 sản phẩm/Estimate unit cost <span class="auto-style1">(*) </span></td>
            <td></td>
            <td>
                <telerik:RadNumericTextBox ID="Rdnumdongia" runat="server" AutoPostBack="True" OnTextChanged="Rdnumdongia_TextChanged">
                    <NegativeStyle Resize="None"></NegativeStyle>

                    <NumberFormat ZeroPattern="n" DecimalDigits="2"></NumberFormat>

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
            <td>Tổng cộng/Total cost</td>
            <td></td>
            <td>
                <telerik:RadNumericTextBox ID="RadNumericTextBox1" runat="server" ReadOnly="true">
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
            <td>Thời gian làm/in mẫu/Color proof/Mock up timing<span class="auto-style1"> (*) </span></td>
            <td></td>
            <td>
                <telerik:RadDatePicker ID="rddatethoigianlammau" runat="server">
                </telerik:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td>Thời gian sản xuất/Production timing <span class="auto-style1">(*) </span></td>
            <td></td>
            <td>
                <telerik:RadDatePicker ID="rddatethoigiansanxuat" runat="server">
                </telerik:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td>Ngày giao hàng/Delivery date <span class="auto-style1">(*) </span></td>
            <td></td>
            <td>
                <telerik:RadDatePicker ID="rdngaygiaohang" runat="server">
                </telerik:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td>Thời hạn thanh toán/Payment term <span class="auto-style1">(*) </span></td>
            <td></td>
            <td>
                <telerik:RadNumericTextBox ID="RadNumericTextBox4" runat="server" EmptyMessage="Nhập số ngày/ Input number of day">
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
            <td>Ghi chú/Note</td>
            <td></td>
            <td>
                <asp:TextBox ID="txtGhichu" TextMode="MultiLine" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>File đính kèm/Attach file

            </td>
            <td></td>
            <td>
                <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" MaxFileInputsCount="1">
                </telerik:RadAsyncUpload>
            </td>
        </tr>

        <tr>
            <td>Áp dụng cho các mặt hàng tương tự

            </td>

            <td></td>
            <td>
                <asp:CheckBox ID="CheckBox1" runat="server" />
            </td>
        </tr>

    </table>
    <br />

    <asp:ImageButton ID="btnEdit" runat="server" CommandName="Update" ImageUrl="~/images/luu.png"
        ValidationGroup="GEdit" OnClick="btnEdit_Click" />
    &nbsp;<asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/Images/dong.png"
        CommandName="Cancel" />
    <%--  --%>
    <asp:HiddenField ID="HiddenField1" runat="server"
        Value='<%# fGet(Eval("IDPRDetail"),Eval("IDPR"),Eval("IDMaterial"),Eval("soluong"),Eval("linkcode"),Eval("DaNhanBaoGia"))%>' />
    <asp:HiddenField ID="HFIDPRDetail" runat="server" />
    <asp:HiddenField ID="HFIDPR" runat="server" />
    <asp:HiddenField ID="HfIDMaterial" runat="server" />
    <asp:HiddenField ID="hfSoluong" runat="server" />
      <asp:HiddenField ID="HfLinkcode" runat="server" />  
       <asp:HiddenField ID="HfDaNhanBaoGia" runat="server" />  


</div>
