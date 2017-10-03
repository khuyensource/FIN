<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TravelRequest.aspx.cs" Inherits="MaricoPay.TravelRequest"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/uc/ucComboCharges.ascx" TagName="combocharges" TagPrefix="uc1" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<%@ Register Src="~/uc/ucComboDepartment.ascx" TagName="department" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <%-- <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/Ajax.js")%>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/auto.js")%>'></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
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
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btReject" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="width: 99%; float: left; overflow: auto; height: 840px;">
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
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        Số chứng từ<br />
                        Request ID
                    </td>
                    <td style="color: #000000;">
                        <asp:DropDownList ID="dropSaved" runat="server" Width="130px" AutoPostBack="True"
                            OnSelectedIndexChanged="dropSaved_SelectedIndexChanged" Font-Names="Arial" 
                            Font-Size="Small">
                        </asp:DropDownList>
                        <asp:DropDownList ID="dropApp" runat="server" Width="190px" AutoPostBack="True" 
                            OnSelectedIndexChanged="dropApp_SelectedIndexChanged" Font-Names="Arial" 
                            Font-Size="Small">
                        </asp:DropDownList>
                         <asp:Button ID="btDelete" Width="55px" runat="server" Text="Delete" 
                                    onclick="btDelete_Click" />
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                      <asp:Label ID="lbStatusTitle" runat="server" Text=" Trạng thái<br />Status"></asp:Label>
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        <asp:Label ID="lbStatus" runat="server" Font-Bold="True" Font-Size="Small"
                            ForeColor="#0000CC"></asp:Label>
                        <asp:HiddenField ID="hdStatus" runat="server" />
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btApp" runat="server" Text="Approve" OnClick="btApp_Click" 
                                        Font-Names="Arial" Font-Size="Small" />
                                </td>
                                <td>
                                    <asp:Button ID="btReject" runat="server" Text="Reject" OnClick="btReject_Click" 
                                        Font-Names="Arial" Font-Size="Small" />
                                </td>
                                <td style="color: #000000;">
                                    <asp:Label ID="lbReason" runat="server" Text="Reason"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAppNote" runat="server" ReadOnly="false" Width="250px" 
                                        Font-Size="Small"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        <asp:Button ID="btPrint" runat="server" Text="Print travel request" 
                            OnClick="btPrint_Click" Font-Names="Arial" Font-Size="Small" />
                        <asp:TextBox ID="txtMyEmail" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lbPD" runat="server" Text="Nhập số chứng từ tạm ứng từ SAP để in phiếu tạm ứng<br />Input SAP downpayment request number to print advance" ToolTip="If you not use SAP, Please contract your admin to create downpayment in SAP system"></asp:Label>
                                </td>
                                <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                                    <asp:TextBox ID="txtDPNo" runat="server" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        <asp:Button ID="btPrintAdvance" runat="server" Text="Print cash advance" 
                            OnClick="btPrintAdvance_Click" Font-Names="Arial" Font-Size="Small" />
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
            <asp:Button ID="btExpand1" runat="server" Text="-" 
                OnClick="btExpand1_Click" Font-Names="Arial" Font-Size="Small" 
                ToolTip="Collapse" />
        </div>
        <div id="dvParent" runat="server">
            <table cellpadding="5px">
                <tr>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        Thị trường<br />
                        Market
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        <asp:DropDownList ID="dropMarket" runat="server" Width="190px" 
                            Font-Names="Arial" Font-Size="Small">
                        </asp:DropDownList>
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        Họ và tên<br />
                        Full Name
                    </td>
                    <td style="color: #000000;">
                        <asp:TextBox ID="txtName" runat="server" Width="150px" Font-Names="Arial" 
                            Font-Size="Small" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        Ngày<br />
                        Date
                    </td>
                    <td style="color: #000000;">
                        <asp:DropDownList ID="dropNguoiDi" runat="server" DataTextField="Fullname" DataValueField="Email" Visible="false">
                        </asp:DropDownList>
                        <telerik:RadDatePicker ID="raddateNow" runat="server" Enabled="False" Culture="en-US"
                            Width="190px" Font-Size="Small">
                            <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                FastNavigationNextText="&amp;lt;&amp;lt;">
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
                    <td style="color: #000000;">
                        <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
                    </td>
                    <td style="color: #000000;">
                        <asp:TextBox ID="txtTelContact" Visible="false" runat="server" Width="150px" 
                            Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        Phòng ban<br />
                        Department
                    </td>
                    <td style="color: #000000;">
                        <uc5:department ID="comboDepartment1" runat="server" OnSelectedIndexChanged="GetMySelection" />
                        <asp:HiddenField ID="hdCompanycode" runat="server" />
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        Chức vụ<br />
                        Position
                    </td>
                    <td style="color: #000000;">
                        <asp:TextBox ID="txtPosition" runat="server" Width="150px" Font-Names="Arial" 
                            Font-Size="Small" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
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
                        <asp:TextBox ID="txtAppEmail" runat="server" Visible="False" Width="150px" 
                            Font-Size="Small"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        <asp:Label ID="lbFrom" runat="server" Text=" Từ ngày<br />From"></asp:Label><asp:Label ID="Label6" runat="server" Text="(*)" ForeColor="Red"></asp:Label>
                    </td>
                    <td style="color: #000000;">
                        <telerik:RadDatePicker ID="raddateFrom" runat="server" Width="150px" 
                            Font-Names="Arial" Font-Size="Small">
                            <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                FastNavigationNextText="&amp;lt;&amp;lt;">
                            </Calendar>
                            <DateInput DisplayDateFormat="dd-MMM-yy" DateFormat="dd-MMM-yy" LabelWidth="40%">
                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                            </DateInput>
                         <%--   <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                        </telerik:RadDatePicker>
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                       
                        <asp:Label ID="lbTo" runat="server" Text=" Đến ngày<br />To"></asp:Label><asp:Label ID="Label5" runat="server" Text="(*)" ForeColor="Red"></asp:Label>
                    </td>
                    <td style="color: #000000;">
                        <telerik:RadDatePicker ID="raddateTo" runat="server" Width="150px" 
                            Font-Names="Arial" Font-Size="Small">
                            <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                FastNavigationNextText="&amp;lt;&amp;lt;">
                            </Calendar>
                            <DateInput runat="server" DisplayDateFormat="dd-MMM-yy" DateFormat="dd-MMM-yy" LabelWidth="40%">
                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                            </DateInput>
                           <%-- <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                        </telerik:RadDatePicker>
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        Mục đích<br />
                        Purpose
                        <asp:Label ID="Label4" runat="server" Text="(*)" ForeColor="Red"></asp:Label>
                    </td>
                    <td colspan="3" style="color: #000000;">
                        <asp:TextBox ID="txtPurpose" runat="server" Width="190px" Font-Names="Arial" 
                            Font-Size="Small"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        
                        <asp:Label ID="lbDestination" runat="server" Text="Nơi đến<br />Destination"></asp:Label><asp:Label ID="Label3" runat="server" Text="(*)" ForeColor="Red"></asp:Label>
                    </td>
                    <td colspan="3" style="color: #000000;">
                        <asp:TextBox ID="txtNoiDen" runat="server" Width="420px" Font-Names="Arial" 
                            Font-Size="Small"></asp:TextBox>
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        
                         <asp:Label ID="lbItenerary" runat="server" Text="Lộ trình<br />Itinerary"></asp:Label><asp:Label ID="Label2" runat="server" Text="(*)" ForeColor="Red"></asp:Label>
                    </td>
                    <td colspan="3" style="color: #000000;">
                        <asp:TextBox ID="txtLoTrinh" runat="server" Width="190px" Font-Names="Arial" 
                            Font-Size="Small"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        
                        <asp:Label ID="lbTransportation" runat="server" Text="Phương tiện đi lại<br />Transportation mean"></asp:Label>
                    </td>
                    <td style="color: #000000; width: 100px;">
                        <asp:CheckBox ID="chkOto" runat="server" Text="Ôtô<br />Car" Font-Names="Arial" 
                            Font-Size="Small" />
                    </td>
                    <td style="color: #000000; width: 100px;">
                        <asp:CheckBox ID="chkTauHoa" runat="server" Text="Tàu hỏa<br />Train" 
                            Font-Names="Arial" Font-Size="Small" />
                    </td>
                    <td style="color: #000000;">
                        <asp:CheckBox ID="chkMayBay" runat="server" Text="Máy bay<br />Flight" 
                            Font-Names="Arial" Font-Size="Small" />
                    </td>
                </tr>
                <tr>
                    <td style="color: #000000; width: 190px; font-size:small; font-family:@Arial Unicode MS;">
                        
                        <asp:Label ID="lbRequest" runat="server" Text="Đề nghị hành chánh thu xếp<br />Request admin to arrange"></asp:Label>
                    </td>
                    <td style="color: #000000; width: 150px;">
                        <asp:CheckBox ID="chkVeTauMayBay" runat="server" 
                            Text="Mua vé Máy bay<br />Returned air ticket" Font-Names="Arial" 
                            Font-Size="Small" />
                    </td>
                    <td style="color: #000000; width: 150px;">
                        <asp:CheckBox ID="chkDatPhong" runat="server" 
                            Text="Đặt khách sạn<br />Hotel booking" Font-Names="Arial" Font-Size="Small" />
                    </td>
                    <td style="color: #000000;">
                        <table>
                            <tr>
                                <td style="width: 100px;">
                                    <asp:CheckBox ID="chkOther" runat="server" Text="Khác<br />Others" 
                                        Font-Names="Arial" Font-Size="Small" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOther" runat="server" Width="150px" Font-Names="Arial" 
                                        Font-Size="Small"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <hr />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btSave" runat="server" Text="Save" OnClick="btSave_Click" 
                        Font-Names="Arial" Font-Size="Small" />
                </td>
                <td>
                    <asp:Button ID="btSubmit" runat="server" Text="Submit" OnClick="btSubmit_Click" 
                        Font-Names="Arial" Font-Size="Small" />
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
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        Khoản mục<br />
                        Category
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCategory" runat="server" DataTextField="Description" DataValueField="Charges_PK"
                            Width="150px" Font-Names="Arial" Font-Size="Small">
                        </asp:DropDownList>
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        Đơn giá<br />
                        Unit price
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="radNumSoTien" runat="server" AutoPostBack="False"
                            Value="0" Width="100px" Font-Names="Arial" Font-Size="Small">
                        </telerik:RadNumericTextBox>
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                        Số lượng<br />
                        Qlty
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="radNumSoluong" runat="server" AutoPostBack="False"
                            Value="0" Width="60px" 
                            MinValue="1" Font-Names="Arial" Font-Size="Small">
                        </telerik:RadNumericTextBox>
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                       <%-- Thành tiền<br />
                        Amount--%>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="radThanhTien" runat="server" Value="0" 
                            Width="100px" Font-Names="Arial" Font-Size="Small" Visible="false">
                        </telerik:RadNumericTextBox>
                    </td>
                    <td colspan="3" style="color: #000000;">
                        <asp:CheckBox ID="chkTamung" runat="server" Text="Tạm ứng<br/>Advance request" 
                            
                            ToolTip="Vui lòng đánh dấu vào &quot;Tạm ứng&quot; nếu bạn muốn ứng tiền mặt/Please check &quot;Advance request&quot; checkbox if you want to propose cash advance this amount" 
                            Font-Names="Arial" Font-Size="Small" />
                    </td>
                    <td>
                        <asp:Button ID="btAdd" runat="server" OnClick="btAdd_Click" Text="Add" 
                            Font-Names="Arial" Font-Size="Small" />
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                   <asp:Label ID="Label8" runat="server" Text="(*)" ForeColor="Red"></asp:Label> 
                    </td>
                    <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                    <asp:Label ID="Label7" runat="server" Text="Phải có ít nhất 1 loại chi phí<br/>Must add at least one category amount"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <hr />
        <div>
            <asp:HiddenField ID="hdNoDays" runat="server" />
            <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False"
                GridLines="Both" ShowFooter="true" OnDeleteCommand="RadGrid1_DeleteCommand">
                <GroupingSettings CaseSensitive="False" />
                <MasterTableView EnableHeaderContextMenu="true">
                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                    </RowIndicatorColumn>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="STT<br/>No" UniqueName="SoTT" DataField="STT"
                            AllowFiltering="False">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
                            <ItemTemplate>
                                <%# Container.DataSetIndex  + 1%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn CommandName="Delete" Text="Delete" ButtonType="ImageButton"
                            HeaderText="Xóa<br/>Delete" UniqueName="DeleteColumn">
                        </telerik:GridButtonColumn>
                        <telerik:GridBoundColumn HeaderText="Loại chi phí<br/>Detail Expenses" DataField="Description"
                            UniqueName="Description" EmptyDataText="" FilterControlWidth="240px">
                            <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="200px"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="SoTien" DataField="SoTien" HeaderText="Số tiền<br/>Amount"
                            DataFormatString="{0:###,###.##}" Aggregate="Sum" FooterText="Total: " DataType="System.Double"
                            EmptyDataText="0" FilterControlWidth="150px">
                            <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                            <FooterStyle Width="200px" HorizontalAlign="Center" />
                            <ItemStyle Width="200px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="SoLuong" DataField="SoLuong" HeaderText="Số lượng<br/>Qlty"
                            DataFormatString="{0:###,###.##}" Aggregate="Sum" FooterText="Total: " DataType="System.Double"
                            EmptyDataText="0" FilterControlWidth="40px">
                            <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                            <FooterStyle Width="70px" HorizontalAlign="Center" />
                            <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Cong" DataField="Cong" HeaderText="Thành tiền VNĐ<br/>Total VNĐ"
                            DataFormatString="{0:###,###}" Aggregate="Sum" FooterText="Total: " DataType="System.Double"
                            EmptyDataText="0" FilterControlWidth="150px">
                            <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                            <FooterStyle Width="200px" HorizontalAlign="Center" />
                            <ItemStyle Width="200px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Tạm ứng<br/>Advance" UniqueName="IsTamUng">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Convert.ToBoolean(Eval("IsTamUng")) == true ? "Yes" : "No" %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn UniqueName="ID" DataField="ID" HeaderText="ID" EmptyDataText=""
                            FilterControlWidth="40px" Display="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Code_FK" DataField="Code_FK" HeaderText="Code_FK"
                            EmptyDataText="" FilterControlWidth="40px" Display="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Charges_FK" DataField="Charges_FK" HeaderText="Charges_FK"
                            EmptyDataText="" FilterControlWidth="40px" Display="false">
                        </telerik:GridBoundColumn>
                        
                    </Columns>
                    <EditFormSettings EditFormType="WebUserControl">
                        <EditColumn UniqueName="EditCommandColumn1">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="3">
                    </Scrolling>
                    <Resizing AllowColumnResize="true" EnableRealTimeResize="true" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
    </div>
    <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
