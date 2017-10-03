<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TravelRequestSales.aspx.cs" Inherits="MaricoPay.TravelRequestSales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/uc/ucComboCharges.ascx" TagName="combocharges" TagPrefix="uc1" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<%@ Register Src="~/uc/ucComboDepartment.ascx" TagName="department" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <%-- <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/Ajax.js")%>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/auto.js")%>'></script>--%>
    <style type="text/css">
        .style1
        {
            color: #CC0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="javascript">
        function UserDeleteConfirmation() {
            return confirm("Bạn có chắc muốn xóa PR này?<br/>Are you sure you want to delete this PR?");
        }

        function UserSubmitConfirmation() {
            return confirm("Bạn có chắc muốn gửi working plan đến cấp trên phê duyệt, sau khi gửi bạn không thể chỉnh sửa <br/>Are you sure to send to N1, you will not revise this WK");
        }
    </script>
    <%--<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />--%>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btExpand1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbTitle" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="btExpand1" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="dvParent" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radNumSoTien">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radNumSoluong" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="radThanhTien" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radNumSoluong">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radNumSoTien" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="radThanhTien" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btAdd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelCssClass="" />
                    <%--<telerik:AjaxUpdatedControl ControlID="btSave" UpdatePanelCssClass="" />--%>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="uscSubmit">
                <UpdatedControls>
                    <%-- <telerik:AjaxUpdatedControl ControlID="btSubmit" UpdatePanelCssClass="" />--%>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid2" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="lbMess" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="UscApproved">
                <UpdatedControls>
                    <%-- <telerik:AjaxUpdatedControl ControlID="btSubmit" UpdatePanelCssClass="" />--%>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid2" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="lbMess" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btReject" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="btAdd" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="raddateNow0" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="txtMorning" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="droTinhS" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="droHuyenS" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="txtAfternoon" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="droTinhC" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="droHuyenC" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="txtNote" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="droTinhS">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="droHuyenS" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="droTinhC" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="droHuyenC" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="droTinhC">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="droHuyenC" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="width: 99%; float: left; overflow: auto; height: 840px;">
        <div>
            <uc4:uscMsgBox ID="MsgBox1" runat="server" />
            <uc4:uscMsgBox ID="uscSubmit" runat="server" OnMsgBoxAnswered="ms_answerSubmit" />
            <uc4:uscMsgBox ID="UscApproved" runat="server" OnMsgBoxAnswered="ms_answerApproved" />
            <table width="100%">
                <tr>
                    <td align="center" style="font-size: large; color: Black; font-weight: bold;">
                        <asp:Label ID="lbTitle" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table cellpadding="5px">
                <tr>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        1.Số chứng từ<br />
                        Request ID
                    </td>
                    <td style="color: #000000;">
                        <asp:DropDownList ID="dropSaved" runat="server" Width="200px" AutoPostBack="True"
                            OnSelectedIndexChanged="dropSaved_SelectedIndexChanged" Font-Names="Arial" Font-Size="Small">
                        </asp:DropDownList>
                        <asp:DropDownList ID="dropApp" runat="server" Width="190px" AutoPostBack="True" OnSelectedIndexChanged="dropApp_SelectedIndexChanged"
                            Font-Names="Arial" Font-Size="Small">
                        </asp:DropDownList>
                        <asp:Button ID="btDelete" Width="55px" runat="server" Text="Delete" OnClick="btDelete_Click" />
                    </td>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        <asp:Label ID="lbStatusTitle" runat="server" Text=" Trạng thái<br />Status"></asp:Label>
                    </td>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        <asp:Label ID="lbStatus" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0000CC"></asp:Label>
                        <asp:HiddenField ID="hdStatus" runat="server" />
                    </td>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btApp" runat="server" Text="Approve" OnClick="btApp_Click" Font-Names="Arial"
                                        Font-Size="Small" />
                                </td>
                                <td>
                                    <asp:Button ID="btReject" runat="server" Text="Reject" OnClick="btReject_Click" Font-Names="Arial"
                                        Font-Size="Small" />
                                </td>
                                <td style="color: #000000;">
                                    <asp:Label ID="lbReason" runat="server" Text="Reason"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAppNote" runat="server" ReadOnly="false" Width="250px" Font-Size="Small"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        <asp:Button ID="btPrint" runat="server" Text="In/Print" OnClick="btPrint_Click" Font-Names="Arial"
                            Font-Size="Small" />
                        <asp:TextBox ID="txtMyEmail" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        <table>
                            <tr>
                                <td>
                                    <%--<asp:Label ID="lbPD" runat="server" Text="Nhập số chứng từ tạm ứng từ SAP để in phiếu tạm ứng<br />Input SAP downpayment request number to print advance"
                                        ToolTip="If you not use SAP, Please contract your admin to create downpayment in SAP system"></asp:Label>--%>
                                </td>
                                <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                                    <%-- <asp:TextBox ID="txtDPNo" runat="server" Font-Names="Arial" Font-Size="Small"></asp:TextBox>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        <%--   <asp:Button ID="btPrintAdvance" runat="server" Text="Print cash advance" OnClick="btPrintAdvance_Click"
                            Font-Names="Arial" Font-Size="Small" />--%>
                    </td>
                    <td style="color: #000000;">
                        <telerik:RadGrid ID="RadGrid2" runat="server">
                        </telerik:RadGrid>
                    </td>
                    <td style="color: #000000;">
                    </td>
                </tr>
            </table>
        </div>
        <hr />
        <div>
            <asp:Button ID="btExpand1" runat="server" Text="-" OnClick="btExpand1_Click" Font-Names="Arial"
                Font-Size="Small" ToolTip="Collapse" />
        </div>
        <div id="dvParent" runat="server">
            <table cellpadding="5px">
                <tr>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        Thị trường<br />
                        Market
                    </td>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        <asp:DropDownList ID="dropMarket" runat="server" Width="190px" Font-Names="Arial"
                            Font-Size="Small">
                        </asp:DropDownList>
                    </td>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        Họ và tên<br />
                        Full Name
                    </td>
                    <td style="color: #000000;">
                        <asp:TextBox ID="txtName" runat="server" Width="150px" Font-Names="Arial" Font-Size="Small"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        Ngày<br />
                        Date
                    </td>
                    <td style="color: #000000;">
                        <asp:DropDownList ID="dropNguoiDi" runat="server" DataTextField="Fullname" DataValueField="Email"
                            Visible="false">
                        </asp:DropDownList>
                        <telerik:RadDatePicker ID="raddateNow" runat="server" Enabled="False" Culture="en-US"
                            Width="190px" Font-Size="Small">
                            <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;">
                            </Calendar>
                            <DateInput runat="server" DisplayDateFormat="dd-MMM-yy" DateFormat="M/d/yyyy" LabelWidth="40%">
                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                <FocusedStyle Resize="None"></FocusedStyle>
                                <DisabledStyle Resize="None"></DisabledStyle>
                                <InvalidStyle Resize="None"></InvalidStyle>
                                <HoveredStyle Resize="None"></HoveredStyle>
                                <EnabledStyle Resize="None"></EnabledStyle>
                            </DateInput>
                          <%--  <DatePopupButton CssClass="rcCalPopup rcDisabled" ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                        </telerik:RadDatePicker>
                    </td>
                    <td style="color: #000000;">
                        <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
                    </td>
                    <td style="color: #000000;">
                        <asp:TextBox ID="txtTelContact" Visible="false" runat="server" Width="150px" Font-Names="Arial"
                            Font-Size="Small"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        Phòng ban<br />
                        Department
                    </td>
                    <td style="color: #000000;">
                        <uc5:department ID="comboDepartment1" runat="server" OnSelectedIndexChanged="GetMySelection" />
                        <asp:HiddenField ID="hdCompanycode" runat="server" />
                    </td>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        Chức vụ<br />
                        Position
                    </td>
                    <td style="color: #000000;">
                        <asp:TextBox ID="txtPosition" runat="server" Width="150px" Font-Names="Arial" Font-Size="Small"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        Ký duyệt<br />
                        Approved by
                    </td>
                    <td style="color: #000000;">
                        <asp:TextBox ID="txtAppName" runat="server" Width="190px" Font-Names="Arial" ReadOnly="true"
                            Font-Size="Small"></asp:TextBox>
                    </td>
                    <td style="color: #000000;">
                        <asp:Label ID="lbEmailApp" runat="server" Text="Email Approved" Visible="false" Width="150px"></asp:Label>
                    </td>
                    <td style="color: #000000;">
                        <asp:DropDownList ID="dropAppNext" runat="server" DataTextField="Fullname" DataValueField="Email"
                            Visible="false" Width="150px" Font-Size="Small">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtAppEmail" runat="server" Visible="False" Width="150px" Font-Size="Small"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        2.Tháng/Month <span class="style1">(*)</span>
                    </td>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        <table>
                            <tr>
                                <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                                    <asp:DropDownList ID="dropThang" runat="server" Font-Names="Arial" 
                                        Font-Size="Small" onselectedindexchanged="dropThang_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1">01</asp:ListItem>
                                        <asp:ListItem Value="2">02</asp:ListItem>
                                        <asp:ListItem Value="3">03</asp:ListItem>
                                        <asp:ListItem Value="4">04</asp:ListItem>
                                        <asp:ListItem Value="5">05</asp:ListItem>
                                        <asp:ListItem Value="6">06</asp:ListItem>
                                        <asp:ListItem Value="7">07</asp:ListItem>
                                        <asp:ListItem Value="8">08</asp:ListItem>
                                        <asp:ListItem Value="9">09</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="11">11</asp:ListItem>
                                        <asp:ListItem Value="12">12</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                                   3.Năm/Year <span class="style1">(*)</span>
                                </td>
                                <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                                    <asp:DropDownList ID="dropNam" runat="server" DataTextField="Years" 
                                        DataValueField="Years" Font-Names="Arial" Font-Size="Small" 
                                        onselectedindexchanged="dropNam_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        4.Từ ngày<br />
                        From <span class="style1">(*)</span>
                    </td>
                    <td style="color: #000000;">
                        <telerik:RadDatePicker ID="raddateFrom" runat="server" Width="150px" Font-Names="Arial"
                            Font-Size="Small">
                            <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;">
                            </Calendar>
                            <DateInput DisplayDateFormat="dd-MMM-yy" DateFormat="dd-MMM-yy" LabelWidth="40%">
                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                            </DateInput>
                            <%--<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                        </telerik:RadDatePicker>
                    </td>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        5.Đến ngày<br />
                        To <span class="style1">(*)</span>
                    </td>
                    <td style="color: #000000;">
                        <telerik:RadDatePicker ID="raddateTo" runat="server" Width="150px" Font-Names="Arial"
                            Font-Size="Small">
                            <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;">
                            </Calendar>
                            <DateInput runat="server" DisplayDateFormat="dd-MMM-yy" DateFormat="dd-MMM-yy" LabelWidth="40%">
                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                            </DateInput>
                          <%--  <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                        </telerik:RadDatePicker>
                    </td>
                    <td colspan="3" style="color: #000000;">
                    </td>
                </tr>
                <tr>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        6.Mục đích<br />
                        Purpose <span class="style1">(*)</span>
                    </td>
                    <td colspan="7" style="color: #000000;">
                        <asp:TextBox ID="txtPurpose" MaxLength="500" runat="server" TextMode="MultiLine" Width="500px" Font-Names="Arial"
                            Font-Size="Small"></asp:TextBox>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <%--  <table>
                <tr>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        <asp:Label ID="lbTransportation" runat="server" Text="Phương tiện đi lại<br />Transportation mean"></asp:Label>
                    </td>
                    <td style="color: #000000; width: 100px;">
                        <asp:CheckBox ID="chkOto" runat="server" Text="Ôtô<br />Car" Font-Names="Arial" Font-Size="Small" />
                    </td>
                    <td style="color: #000000; width: 100px;">
                        <asp:CheckBox ID="chkTauHoa" runat="server" Text="Tàu hỏa<br />Train" Font-Names="Arial"
                            Font-Size="Small" />
                    </td>
                    <td style="color: #000000;">
                        <asp:CheckBox ID="chkMayBay" runat="server" Text="Máy bay<br />Flight" Font-Names="Arial"
                            Font-Size="Small" />
                    </td>
                </tr>
                <tr>
                    <td style="color: #000000; width: 190px; font-size: small; font-family: @Arial Unicode MS;">
                        <asp:Label ID="lbRequest" runat="server" Text="Đề nghị hành chánh thu xếp<br />Request admin to arrange"></asp:Label>
                    </td>
                    <td style="color: #000000; width: 150px;">
                        <asp:CheckBox ID="chkVeTauMayBay" runat="server" Text="Mua vé Máy bay<br />Returned air ticket"
                            Font-Names="Arial" Font-Size="Small" />
                    </td>
                    <td style="color: #000000; width: 150px;">
                        <asp:CheckBox ID="chkDatPhong" runat="server" Text="Đặt khách sạn<br />Hotel booking"
                            Font-Names="Arial" Font-Size="Small" />
                    </td>
                    <td style="color: #000000;">
                        <table>
                            <tr>
                                <td style="width: 100px;">
                                    <asp:CheckBox ID="chkOther" runat="server" Text="Khác<br />Others" Font-Names="Arial"
                                        Font-Size="Small" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOther" runat="server" Width="150px" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>--%>
        </div>
        <hr />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btSave" runat="server" Text="12.Lưu/Save" OnClick="btSave_Click" Font-Names="Arial"
                        Font-Size="Small" />
                </td>
                <td>
                    <asp:Button ID="btSubmit" runat="server" Text="13.Trình ký/Submit" OnClientClick="if ( ! UserSubmitConfirmation()) return false;"
                        OnClick="btSubmit_Click" Font-Names="Arial" Font-Size="Small" />
                </td>
                <td>
                </td>
                <td style="color: #000000;">
                </td>
                <td>
                    <asp:Label ID="lbMess" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <div>
            <table>
                <tr>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        7.Ngày<br />
                        Date
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="raddateNow0" runat="server" Enabled="true" Culture="en-US"
                            Width="100px" Font-Size="Small">
                            <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;">
                            </Calendar>
                            <DateInput runat="server" DisplayDateFormat="dd-MMM-yy" DateFormat="M/d/yyyy" LabelWidth="40%">
                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                <FocusedStyle Resize="None"></FocusedStyle>
                                <DisabledStyle Resize="None"></DisabledStyle>
                                <InvalidStyle Resize="None"></InvalidStyle>
                                <HoveredStyle Resize="None"></HoveredStyle>
                                <EnabledStyle Resize="None"></EnabledStyle>
                            </DateInput>
                           <%-- <DatePopupButton CssClass="" ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                        </telerik:RadDatePicker>
                    </td>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        8.Sáng<br />
                        Morning
                    </td>
                    <td>
                        <asp:TextBox ID="txtMorning" runat="server" MaxLength="500" TextMode="MultiLine" Width="150px" Font-Names="Arial"
                            Font-Size="Small"></asp:TextBox>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="droTinhS" DataTextField="TenTP" DataValueField="MaTP" AutoPostBack="true"
                                        Font-Names="Arial" Font-Size="Small" runat="server" OnSelectedIndexChanged="droTinhS_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="droHuyenS" DataTextField="QuanHuyen" DataValueField="MaQH"
                                        AutoPostBack="true" runat="server" Font-Names="Arial" Font-Size="Small" OnSelectedIndexChanged="droHuyenS_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        9.Chiều<br />
                        Afternoon
                    </td>
                    <td>
                        <asp:TextBox ID="txtAfternoon" MaxLength="500" runat="server" TextMode="MultiLine" Width="150px"
                            Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="droTinhC" DataTextField="TenTP" DataValueField="MaTP" AutoPostBack="true"
                                        Font-Names="Arial" Font-Size="Small" runat="server" OnSelectedIndexChanged="droTinhC_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="droHuyenC" DataTextField="QuanHuyen" DataValueField="MaQH"
                                        runat="server" Font-Names="Arial" Font-Size="Small">
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="X-Small" OnClick="LinkButton1_Click">Reload</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                        10.Chú thích<br />
                        Note
                    </td>
                    <td>
                        <asp:TextBox ID="txtNote" runat="server" MaxLength="1000" TextMode="MultiLine" Width="150px" Font-Names="Arial"
                            Font-Size="Small"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btAdd" runat="server" OnClick="btAdd_Click" Text="11.Add" Font-Names="Arial"
                            Font-Size="Small" Font-Bold="True" Height="37px" ToolTip="Thêm" />
                    </td>
                </tr>
            </table>
        </div>
        <hr />
        <div>
            <asp:HiddenField ID="hdNoDays" runat="server" />
            <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="Both"
                ShowFooter="true" OnDeleteCommand="RadGrid1_DeleteCommand" AllowSorting="true"
                OnSortCommand="RadGrid1_SortCommand" OnEditCommand="RadGrid1_EditCommand">
                <GroupingSettings CaseSensitive="False" />
                <MasterTableView EnableHeaderContextMenu="true">
                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                    </RowIndicatorColumn>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="STT<br/>No" UniqueName="SoTT" DataField="SoTT"
                            AllowFiltering="False">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
                            <ItemTemplate>
                                <%# Container.DataSetIndex  + 1%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn CommandName="Edit" Text="Edit" ButtonType="ImageButton"
                            HeaderText="Sửa<br/>Edit" UniqueName="EditColumn">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                        </telerik:GridButtonColumn>
                        <telerik:GridButtonColumn CommandName="Delete" Text="Delete" ButtonType="ImageButton"
                            HeaderText="Xóa<br/>Delete" UniqueName="DeleteColumn">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
                        </telerik:GridButtonColumn>
                        <telerik:GridBoundColumn HeaderText="Ngày<br/>Date" DataField="FDate" UniqueName="FDate"
                            EmptyDataText="" FilterControlWidth="70px" DataFormatString="{0:dd-MMM-yy}">
                            <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="90px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Thứ" EmptyDataText="" DataField="Thu" UniqueName="Thu"
                            FilterControlWidth="90px">
                            <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="90px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Sáng<br/>Morning" DataField="PurposeMorning"
                            UniqueName="PurposeMorning" EmptyDataText="" FilterControlWidth="200px">
                            <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="200px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="TỉnhS<br/>provinceM" DataField="TenTinhSang"
                            UniqueName="TenTinhSang" EmptyDataText="" FilterControlWidth="80px">
                            <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="90px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="MaTinhSang" DataField="TinhSang" Display="false"
                            UniqueName="TinhSang" EmptyDataText="" FilterControlWidth="80px">
                            <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="90px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="HuyênS<br/>DistrictM" DataField="TenHuyenSang"
                            UniqueName="TenHuyenSang" EmptyDataText="" FilterControlWidth="80px">
                            <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="90px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="MaHuyenSang" DataField="HuyenSang" Display="false"
                            UniqueName="HuyenSang" EmptyDataText="" FilterControlWidth="80px">
                            <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="90px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Chiều<br/>Afternoon" DataField="PurposeAfter"
                            UniqueName="PurposeAfter" EmptyDataText="" FilterControlWidth="200px">
                            <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="200px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="TỉnhC<br/>provinceA" DataField="TenTinhChieu"
                            UniqueName="TenTinhChieu" EmptyDataText="" FilterControlWidth="80px">
                            <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="90px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="MaTinhChieu" DataField="TinhChieu" Display="false"
                            UniqueName="TinhChieu" EmptyDataText="" FilterControlWidth="80px">
                            <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="90px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="HuyênC<br/>DistrictA" DataField="TenHuyenChieu"
                            UniqueName="TenHuyenChieu" EmptyDataText="" FilterControlWidth="80px">
                            <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="90px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="MaHuyenChieu" DataField="HuyenChieu" Display="false"
                            UniqueName="HuyenChieu" EmptyDataText="" FilterControlWidth="80px">
                            <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="90px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Ghi chú<br/>Note" DataField="Note" UniqueName="Note"
                            EmptyDataText="" FilterControlWidth="130px">
                            <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="150px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="ID" DataField="ID" HeaderText="ID" EmptyDataText=""
                            FilterControlWidth="40px" Display="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Code_FK" DataField="Code_FK" HeaderText="Code_FK"
                            EmptyDataText="" FilterControlWidth="40px" Display="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="IsClaimWorkingPlan" DataField="IsClaimWorkingPlan"
                            HeaderText="IsClaimWorkingPlan" EmptyDataText="" FilterControlWidth="40px" Display="false"
                            DataType="System.Boolean">
                        </telerik:GridBoundColumn>
                    </Columns>
                    <EditFormSettings EditFormType="WebUserControl">
                        <EditColumn UniqueName="EditCommandColumn1">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2">
                    </Scrolling>
                    <Resizing AllowColumnResize="true" EnableRealTimeResize="true" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
    </div>
    <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
