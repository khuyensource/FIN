<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="true"
    CodeBehind="ClaimExpensesSales.aspx.cs" Inherits="MaricoPay.ClaimExpensesSales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/uc/ucComboCharges.ascx" TagName="combocharges" TagPrefix="uc1" %>
<%@ Register Src="~/uc/ucCurrence.ascx" TagName="comboCurrence" TagPrefix="uc2" %>
<%@ Register Src="~/uc/ucUploadimage.ascx" TagName="imgUpload" TagPrefix="uc3" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<%@ Register Src="~/uc/ucComboDepartment.ascx" TagName="department" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  <%--  <script type="text/javascript" src='<%=ResolveUrl("Scripts/Ajax.js")%>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("Scripts/auto.js")%>'></script>
    <link rel="stylesheet" href='<%=ResolveUrl("Scripts/auto.css")%>' type="text/css" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="javascript">
        function UserDeleteConfirmation() {
            return confirm("Bạn có chắc muốn xóa PR này?<br/>Are you sure you want to delete this PR?");
        }

        function UserSubmitConfirmation() {
            return confirm("Bạn có chắc muốn gửi đề nghị thanh toán đến cấp trên phê duyệt, sau khi gửi bạn không thể chỉnh sửa <br/>Are you sure to send to N1, you will not revise this claim");
        }
    </script>
    <%-- <script type="text/javascript">
        
       function pageLoad() {
            $("#<%= btApp.ClientID %>").click(function () {
                $.blockUI({
                    message: '<h1>Please waiting Approved</h1>',
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#f00',
                        opacity: .5,
                        color: '#fff'
                    }
                });
            });
            $("#<%= btReject.ClientID %>").click(function () {
                $.blockUI({
                    message: '<h1>Please wait Rejectting</h1>',
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#f00',
                        opacity: .5,
                        color: '#fff'
                    }
                });
            });
            $("#<%= btSave.ClientID %>").click(function () {
                $.blockUI({
                    message: '<h1>Please wait save</h1>',
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#f00',
                        opacity: .5,
                        color: '#fff'
                    }
                });
            });
            $("#<%= btSubmit.ClientID %>").click(function () {
                $.blockUI({
                    message: '<h1>Please wait Submit</h1>',
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#f00',
                        opacity: .5,
                        color: '#fff'
                    }
                });
            });
            $("#<%= dropSaved.ClientID %>").change(function () {
                $.blockUI({
                    message: '<h1>Please wait Submit</h1>',
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#f00',
                        opacity: .5,
                        color: '#fff'
                    }
                });
            });
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                $.unblockUI();
            }
        }
       
       
    </script>--%>
    <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:HiddenField ID="hdPrint" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="btSave" />
            <asp:PostBackTrigger ControlID="btSubmit" />
            <asp:PostBackTrigger ControlID="btReject" />
            <asp:PostBackTrigger ControlID="btApp" />
            <asp:PostBackTrigger ControlID="btAdd" />
            <asp:PostBackTrigger ControlID="RadGrid1" />
            <asp:PostBackTrigger ControlID="btDelete" />
            <asp:PostBackTrigger ControlID="dropWorkingPlan" />
            <asp:PostBackTrigger ControlID="btReview" />
            <asp:PostBackTrigger ControlID="btVRequetsTravel" />
             
            <%--  <asp:PostBackTrigger ControlID="txtAppNote" />--%>
        </Triggers>
        <ContentTemplate>
            <%--<telerik:RadNotification ID="RadNotification1" runat="server" AutoCloseDelay="0"
                Height="35px" Position="BottomRight" ShowCloseButton="False" VisibleOnPageLoad="True"
                VisibleTitlebar="False" Width="35px" BackColor="Transparent">
                <ContentTemplate>
                    <center>
                        <asp:Label ID="lbLoi" runat="server"></asp:Label>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <asp:Image ID="Image1" ImageUrl="~/images/process.gif" runat="server" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </center>
                </ContentTemplate>
            </telerik:RadNotification>--%>
            <div style="width: 99%; float: left; overflow: auto; height: 1029px; margin-right: 0px;"
                id="dvScroll" runat="server">
                <div>
                    <uc4:uscMsgBox ID="MsgBox1" runat="server" />
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
                                Receipt ID
                            </td>
                            <td>
                                <asp:DropDownList ID="dropSaved" Width="190px" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="dropSaved_SelectedIndexChanged" Font-Names="Arial" Font-Size="Small">
                                </asp:DropDownList>
                                <asp:DropDownList ID="dropApp" Width="190px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropApp_SelectedIndexChanged"
                                    Font-Names="Arial" Font-Size="Small">
                                </asp:DropDownList>
                                <asp:Button ID="btReview" runat="server" Text="Xem/Review" OnClick="btReview_Click"
                                    Font-Names="Arial" Font-Size="Small" />
                                <asp:Button ID="btDelete" Width="55px" runat="server" Text="Xóa/Delete" OnClick="btDelete_Click"  Font-Names="Arial" Font-Size="Small"/>
                            </td>
                            <td style="color: #000000;">
                                <asp:Label ID="lbAdvance" runat="server" Text="2.Phiếu đề nghị<br />Working plan"
                                    Font-Names="Arial" Font-Size="Small"></asp:Label>
                            </td>
                            <td style="color: #000000;">
                                <asp:DropDownList ID="dropTravelRequest" runat="server" DataTextField="Code_PK" DataValueField="Code_PK"
                                    AutoPostBack="true" OnSelectedIndexChanged="dropTravelRequest_SelectedIndexChanged"
                                    Width="200px" Font-Names="Arial" Font-Size="Small">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btVRequetsTravel" runat="server" Text="Xem/View" OnClick="btVRequetsTravel_Click"
                                    Font-Names="Arial" Font-Size="Small" />
                            </td>
                            <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                                <asp:Label ID="lbStatusTitle" runat="server" Text=" Trạng thái<br />Status"></asp:Label>
                            </td>
                            <td style="color: #000000;">
                                <asp:Label ID="lbStatus" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0000CC"></asp:Label>
                                <asp:HiddenField ID="hdStatus" runat="server" />
                                <asp:TextBox ID="txtMyEmail" runat="server" Visible="false" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                                <telerik:RadGrid ID="RadGrid2" runat="server">
                                </telerik:RadGrid>
                            </td>
                            <td style="color: #000000;">
                                <table cellpadding="5px">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btPrint" runat="server" Text="In/Print" OnClick="btPrint_Click" Font-Names="Arial"
                                                Font-Size="Small" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btApp" runat="server" Text="Approve" OnClick="btApp_Click" Font-Names="Arial"
                                                Font-Size="Small" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btReject" runat="server" Text="Reject" OnClick="btReject_Click" Font-Names="Arial"
                                                Font-Size="Small" />
                                        </td>
                                        <td style="color: #000000;">
                                            <asp:Label ID="lbReason" runat="server" Text="Lý do/Reason" Font-Names="Arial" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAppNote" runat="server" ReadOnly="false" Width="250px" Font-Names="Arial"
                                                Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
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
                            <td style="color: #000000;">
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
                                <asp:HiddenField ID="hdVendorSAP" runat="server" />
                            </td>
                            <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                                Ngày<br />
                                Date
                            </td>
                            <td style="color: #000000;">
                                <telerik:RadDatePicker ID="raddateNow" runat="server" Enabled="False" Culture="en-US"
                                    Width="150px" Font-Names="Arial" Font-Size="Small">
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
                                   <%-- <DatePopupButton CssClass="rcCalPopup rcDisabled" ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                                </telerik:RadDatePicker>
                            </td>
                            <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                                Người phê duyệt<br />
                                Approved by
                            </td>
                            <td style="color: #000000;">
                                <asp:TextBox ID="txtAppName" runat="server" Width="150px" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
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
                                <asp:HiddenField ID="hfDepartment" runat="server" />
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
                                Đã tạm ứng<br />
                                Advanced amount VNĐ
                            </td>
                            <td style="color: #000000;">
                                <telerik:RadNumericTextBox ID="radnumAdvncedAmount" runat="server" Width="150px"
                                    ReadOnly="False" Value="0" Font-Names="Arial" Font-Size="Small">
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
                            <td style="color: #000000;">
                                <asp:Label ID="lbEmailApp" runat="server" Text="Email Approved" Visible="False" Width="150px"
                                    Font-Names="Arial" Font-Size="Small"></asp:Label>
                                <asp:Label ID="lbNguoiThuHuong" runat="server" Text="Người thụ hưởng" Visible="false"></asp:Label>
                            </td>
                            <td style="color: #000000;">
                                <asp:TextBox ID="txtAppEmail" runat="server" Visible="False" Width="150px" Font-Names="Arial"
                                    Font-Size="Small"></asp:TextBox>
                                <asp:TextBox ID="txtNguoiThuHuong" runat="server" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                                3.Từ<br />
                                From
                            </td>
                            <td style="color: #000000;">
                                <telerik:RadDatePicker ID="raddateFrom" runat="server" Width="190px" Font-Names="Arial"
                                    Font-Size="Small">
                                    <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                        EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;">
                                    </Calendar>
                                    <DateInput runat="server" DisplayDateFormat="dd-MMM-yy" DateFormat="dd-MMM-yy" LabelWidth="40%">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                    </DateInput>
                                   <%-- <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                                </telerik:RadDatePicker>
                            </td>
                            <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                                4.Đến<br />
                                To
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
                                   <%-- <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                                </telerik:RadDatePicker>
                            </td>
                            <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                                5.Lý do<br />
                                Purpose
                            </td>
                            <td colspan="3" style="color: #000000;">
                                <asp:TextBox ID="txtPurpose" runat="server" Width="400px" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <hr />
                <table cellpadding="5px">
                    <tr>
                        <td>
                            <asp:Button ID="btExpand" runat="server" Text="-" OnClick="btExpand_Click" Font-Names="Arial"
                                Font-Size="Small" ToolTip="Collapse" />
                        </td>
                        <td>
                            <asp:Button ID="btSave" runat="server" Text="25.Lưu/Save" OnClick="btSave_Click"
                                Font-Names="Arial" Font-Size="Small" />
                        </td>
                        <td>
                            <asp:Button ID="btSubmit" runat="server" Text="26.Trình ký/Submit" OnClick="btSubmit_Click"
                                OnClientClick="if ( ! UserSubmitConfirmation()) return false;" Font-Names="Arial"
                                Font-Size="Small" />
                        </td>
                        <td>
                            <asp:Label ID="lbMess" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="dvSum" runat="server">
                    <table>
                        <tr>
                            <td>
                                6.Từ ngày<br />
                                From
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="raddateF1" runat="server" Width="150px" Font-Names="Arial"
                                    AutoPostBack="true" Font-Size="Small" OnSelectedDateChanged="raddateF1_SelectedDateChanged">
                                    <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                        EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;">
                                    </Calendar>
                                    <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd-MMM-yy" DateFormat="dd-MMM-yy"
                                        LabelWidth="40%">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                    </DateInput>
                                  <%--  <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                Đến ngày<br />
                                To
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="raddateTo1" runat="server" Width="150px" Font-Names="Arial"
                                    AutoPostBack="true" Font-Size="Small" OnSelectedDateChanged="raddateTo1_SelectedDateChanged">
                                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                        EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;">
                                    </Calendar>
                                    <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd-MMM-yy" DateFormat="dd-MMM-yy"
                                        LabelWidth="40%">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                    </DateInput>
                                  <%--  <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                7.Kế hoặc Ctac<br />
                                Working plan
                            </td>
                            <td>
                                <asp:DropDownList ID="dropWorkingPlan" runat="server" DataTextField="Purpose" DataValueField="ID">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkShowAll" Checked="true" Text="Hiển thị tất cả" ToolTip="Hiển thị tất cả working plan đã thanh toán"
                                    runat="server" OnCheckedChanged="chkShowAll_CheckedChanged" AutoPostBack="true" />
                            </td>
                            <td>
                                8.Số ngày<br />
                                Days
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="radnumSoNgay" runat="server" AutoPostBack="true" Value="0"
                                    Width="50px" Font-Names="Arial" Font-Size="Small" OnTextChanged="radnumSoNgay_TextChanged">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                9.Số đêm<br />
                                Night
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="radnumSoDem" runat="server" AutoPostBack="False" Value="0"
                                    Width="50px" Font-Names="Arial" Font-Size="Small">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </table>
                    <fieldset title="Hóa đơn">
                        <legend style="font-size: small; font-weight: bold; font-style: oblique">Hóa đơn/Invoice
                        </legend>
                        <table cellpadding="5px" style="font-size: small; font-family: @Arial Unicode MS;">
                            <tr align="center">
                                <td>
                                    10.Loại chi phí<br />
                                    Expenses to
                                </td>
                                <td>
                                    11.Ngày HĐơn<br />
                                    Date
                                </td>
                                <td>
                                    12.KýHiệu|SốHĐơn
                                </td>
                                <td>
                                    13.Mẫu HĐơn
                                </td>
                                <td>
                                    14.MST<br />
                                    VAT Number
                                </td>
                                <td>
                                    15.Số tiền (Có VAT)<br />
                                    Amount (Include VAT)
                                </td>
                                <td>
                                    16.Tiền thuế<br />
                                    Tax amount
                                </td>
                                <td>
                                    17.Công ty<br />
                                    Company name
                                </td>
                                <td>
                                    18.Địa chỉ<br />
                                    Address
                                </td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <asp:DropDownList ID="dropLoaiCP" runat="server" DataTextField="DescriptionVN" DataValueField="Charges_PK"
                                        Width="100px" Font-Names="Arial" Font-Size="Small">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="raddateInvoice" runat="server" Width="100px" Font-Names="Arial"
                                        Font-Size="Small">
                                        <Calendar runat="server" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"
                                            UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                        </Calendar>
                                        <DateInput runat="server" DateFormat="dd-MMM-yy" DisplayDateFormat="dd-MMM-yy" LabelWidth="40%">
                                            <EmptyMessageStyle Resize="None" />
                                        </DateInput>
                                       <%-- <DatePopupButton HoverImageUrl="" ImageUrl="" />--%>
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNoInvoice" runat="server" Width="110px" MaxLength="50" Font-Names="Arial"
                                        Font-Size="Small"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNotationInvoice" runat="server" Width="100px" MaxLength="50"
                                        Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVAT" AutoPostBack="true" Width="100px" runat="server" MaxLength="50"
                                        OnTextChanged="txtVAT_TextChanged" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="radnumAmount" runat="server" AutoPostBack="true" OnTextChanged="radnumAmount_TextChanged"
                                                    Value="0" Width="90px" Font-Names="Arial" Font-Size="Small">
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dropTaxcode" runat="server" Font-Names="Arial" Height="23px" Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="dropTaxcode_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">0%</asp:ListItem>
                                                    <asp:ListItem Value="5">5%</asp:ListItem>
                                                    <asp:ListItem Value="10">10%</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="radnuTaxAmount" runat="server" Width="90px" Font-Names="Arial"
                                        Font-Size="Small" Visible="true" ReadOnly="true">
                                        <NegativeStyle Resize="None" />
                                        <NumberFormat DecimalDigits="0" ZeroPattern="n" />
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td style="margin-left: 40px">
                                    <asp:TextBox ID="txtCompanyName" runat="server" MaxLength="350"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDiaChi" runat="server" MaxLength="150"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    19.Khoản mục<br />
                                    Category
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCategory" runat="server" DataTextField="DescriptionVN" DataValueField="Charges_PK"
                                        AutoPostBack="True" Width="150px" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                        Font-Names="Arial" Font-Size="Small">
                                    </asp:DropDownList>
                                </td>
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td>
                                                20.GL
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtGL" runat="server" Width="90px" Font-Names="Arial" Font-Size="Small"
                                                    Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                21.IO
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtIO" runat="server" Width="90px" Font-Names="Arial" Font-Size="Small"
                                                    Enabled="false"></asp:TextBox>
                                                <asp:HiddenField ID="hdID" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>
                                                22.Chi tiết chi phí<br />
                                                Detail Expenses
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDetailExpenses" MaxLength="500" runat="server" Width="250px"
                                                    Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    23. Người tham gia<br />
                                    Participant
                                </td>
                                <td colspan="7">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtParticipant" runat="server" Width="650px" Font-Names="Arial"
                                                    MaxLength="500" Font-Size="Small"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Arial" Font-Size="Small" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btAdd" runat="server" OnClick="btAdd_Click" Text="24.Add" Font-Names="Arial"
                                                    Font-Size="Small" Font-Bold="True" Height="37px" ToolTip="Thêm" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
                <hr />
                <div>
                    <asp:HiddenField ID="hdNoDays" runat="server" />
                    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="Both"
                        ShowFooter="true" OnDeleteCommand="RadGrid1_DeleteCommand" OnEditCommand="RadGrid1_EditCommand"
                        OnItemCommand="RadGrid1_ItemCommand" OnNeedDataSource="RadGrid1_NeedDataSource">
                        <GroupingSettings CaseSensitive="False" />
                        <MasterTableView EnableHeaderContextMenu="true">
                            <CommandItemSettings ExportToPdfText="Export to PDF" />
                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                            </RowIndicatorColumn>
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="STT<br/>No" UniqueName="SoTT" DataField="STT"
                                    AllowFiltering="False">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                                    <ItemTemplate>
                                        <%# Container.DataSetIndex  + 1%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn CommandName="Edit" Text="Edit" ButtonType="ImageButton"
                                    HeaderText="Edit" UniqueName="EditColumn">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                                </telerik:GridButtonColumn>
                                <telerik:GridButtonColumn CommandName="Delete" Text="Delete" ButtonType="ImageButton"
                                    HeaderText="Delete" UniqueName="DeleteColumn">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                                </telerik:GridButtonColumn>
                                <telerik:GridTemplateColumn DataField="TrangThai" HeaderText="Trạng thái" UniqueName="ActionColumn">
                                    <ItemTemplate>
                                        <asp:Button ID="btRejectdetail" runat="server" Text="Reject" CommandName="Rejected"
                                            Visible='<%# isShowReject(Eval ("TrangThai")) %>' />
                                        <asp:Button ID="btAppDetail" runat="server" Text="Approve" CommandName="Approve"
                                            Visible='<%# !isShowReject(Eval ("TrangThai")) %>' />
                                        <asp:Button ID="btRejectPermanently" runat="server" CommandName="RejectPermanently"
                                            Text="Permanently Reject" ToolTip="Permanently rejected" Visible='<%# isShowReject(Eval ("TrangThai")) %>' />
                                        <asp:TextBox ID="txtRejectitem" TextMode="MultiLine" Height="50px" runat="server"
                                            Text='<%# Eval("LyDo")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Trạng thái<br/>Item status" DataField="TrangThaiS"
                                    UniqueName="TrangThaiS" EmptyDataText="" FilterControlWidth="70px">
                                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="90px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Ngày HĐn<br/>DateInvoice" DataField="Date" UniqueName="Date"
                                    EmptyDataText="" FilterControlWidth="70px" DataFormatString="{0:dd-MMM-yy}">
                                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="90px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Số HĐn<br/>InvoiceNo" DataField="No" UniqueName="No"
                                    EmptyDataText="" FilterControlWidth="100px">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="100px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Mẫu HĐn<br/>Notation" DataField="Notation" UniqueName="Notation"
                                    EmptyDataText="" FilterControlWidth="70px">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="80px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ChargeType" DataField="ChargeType" UniqueName="ChargeType"
                                    EmptyDataText="" FilterControlWidth="100px" Display="false">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="100px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Loại CP<br/>ChargeType" DataField="ChargeTypeDs"
                                    UniqueName="ChargeTypeDs" EmptyDataText="" FilterControlWidth="70px">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="80px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Chi tiết CP<br/>DetailExpenses" DataField="DetailExpenses"
                                    UniqueName="DetailExpenses" EmptyDataText="" FilterControlWidth="90px">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="100px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Participant" DataField="Participant" HeaderText="Người Tgia<br/>Participant"
                                    EmptyDataText="" FilterControlWidth="70px">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="80px" HorizontalAlign="Center" />
                                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Currency" DataField="Currency" HeaderText="Tiền tệ<br/>Currency"
                                    EmptyDataText="VND" FilterControlWidth="40px" Display="false">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Ngày HĐn<br/>FromDate" DataField="FDate" UniqueName="FDate"
                                    EmptyDataText="" FilterControlWidth="70px" DataFormatString="{0:dd-MMM-yy}" Display="false">
                                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="90px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Ngày HĐn<br/>ToDate" DataField="TDate" UniqueName="TDate"
                                    EmptyDataText="" FilterControlWidth="70px" DataFormatString="{0:dd-MMM-yy}" Display="false">
                                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="90px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NoDays" DataField="NoDays" HeaderText="Số ngày<br/>NoDays"
                                    DataFormatString="{0:###,###}" Aggregate="Sum" FooterText="Total: " DataType="System.Double"
                                    EmptyDataText="0" FilterControlWidth="40px">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NoNight" DataField="NoNight" HeaderText="Số đêm<br/>NoNight"
                                    DataFormatString="{0:###,###}" Aggregate="Sum" FooterText="Total: " DataType="System.Double"
                                    EmptyDataText="0" FilterControlWidth="40px">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="IDWorkingPlan" DataField="IDWorkingPlan" HeaderText="IDWorkingPlan"
                                    EmptyDataText="" FilterControlWidth="40px" Display="false">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="WorkingPlanDetail" DataField="WorkingPlanDetail"
                                    HeaderText="KH Ctac<br/>WorkingPlan" EmptyDataText="" FilterControlWidth="130px">
                                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle Width="150px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CompanyName" DataField="CompanyName" HeaderText="Tên Cty<br/>CompanyName"
                                    EmptyDataText="" FilterControlWidth="80px">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Province" DataField="Province" HeaderText="DiaChi<br/>Address"
                                    EmptyDataText="" FilterControlWidth="40px">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="TaxNumber" DataField="TaxNumber" HeaderText="MST"
                                    EmptyDataText="" FilterControlWidth="40px">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="VATCode" DataField="VATCode" HeaderText="VATCode"
                                    EmptyDataText="" FilterControlWidth="40px" Display="false">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="VATPercent" DataField="VATPercent" HeaderText="VATPercent"
                                    DataFormatString="{0:###,###.##}" Aggregate="Sum" FooterText="Total: " DataType="System.Double"
                                    EmptyDataText="0" FilterControlWidth="40px" Display="false">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Amount" DataField="Amount" HeaderText="Sốtiền_KoVAT<br/>Amount(Non_VAT)"
                                    DataFormatString="{0:###,###.##}" Aggregate="Sum" FooterText="Total: " DataType="System.Double"
                                    EmptyDataText="0" FilterControlWidth="80px">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="VATAmount" DataField="VATAmount" HeaderText="TienThue<br/>VATAmount"
                                    DataFormatString="{0:###,###.##}" Aggregate="Sum" FooterText="Total: " DataType="System.Double"
                                    EmptyDataText="0" FilterControlWidth="50px">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Rate" DataField="Rate" HeaderText="Tỉ giá<br/>Rate"
                                    DataFormatString="{0:###,###}" FooterText="Avg: " DataType="System.Int32" Aggregate="Avg"
                                    Display="false" EmptyDataText="0" FilterControlWidth="40px">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="TotalVN" DataField="TotalVN" HeaderText="Thành tiền<br/>TotalVN"
                                    DataFormatString="{0:###,###}" Aggregate="Sum" FooterText="Total: " DataType="System.Double"
                                    EmptyDataText="0" FilterControlWidth="40px">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="GL" DataField="GL" HeaderText="GL" EmptyDataText=""
                                    FilterControlWidth="40px">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="IO" DataField="IO" HeaderText="IO" EmptyDataText=""
                                    FilterControlWidth="40px">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Costcenter" DataField="Costcenter" HeaderText="Costcenter"
                                    EmptyDataText="" FilterControlWidth="40px">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Attached File">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# Eval("FileAttach", "~/Upload/EC/{0}") %>'
                                            runat="server" Text='<%# Eval("FileAttach") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="Charges_FK" DataField="Charges_FK" HeaderText="Charges_FK"
                                    EmptyDataText="" FilterControlWidth="40px" Display="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CurrencyDescription" DataField="CurrencyDescription"
                                    HeaderText="CurrencyDescription" EmptyDataText="" FilterControlWidth="40px" Display="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ID" DataField="ID" HeaderText="ID" EmptyDataText=""
                                    Display="false">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <EditFormSettings EditFormType="WebUserControl">
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true"
                            EnablePostBackOnRowClick="false">
                            <Selecting AllowRowSelect="True" />
                            <Scrolling SaveScrollPosition="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
