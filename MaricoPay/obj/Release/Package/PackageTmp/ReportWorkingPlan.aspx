<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ReportWorkingPlan.aspx.cs" Inherits="MaricoPay.ReportWorkingPlan" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
<table>
<tr>
<td>Số chứng từ</td>
<td>
    <asp:TextBox ID="txtsochungtu" runat="server"></asp:TextBox>
</td>
<td>
    <asp:Button ID="btViewDocNo" runat="server" Text="View" 
        onclick="btViewDocNo_Click" />
</td>
</tr>
</table>
</div>
    <div>
        <table>
            <tr>
                <td>
                    Từ ngày
                </td>
                <td>
                    <telerik:RadDatePicker ID="raddateTuNgay" runat="server" Enabled="True" Culture="en-US"
                        Width="150px" Font-Names="Arial" Font-Size="Small">
                        <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                            EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;">
                        </Calendar>
                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd-MMM-yy" DateFormat="M/d/yyyy"
                            LabelWidth="40%">
                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                            <FocusedStyle Resize="None"></FocusedStyle>
                            <DisabledStyle Resize="None"></DisabledStyle>
                            <InvalidStyle Resize="None"></InvalidStyle>
                            <HoveredStyle Resize="None"></HoveredStyle>
                            <EnabledStyle Resize="None"></EnabledStyle>
                        </DateInput>
                      <%--  <DatePopupButton CssClass="rcCalPopup" ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                    </telerik:RadDatePicker>
                </td>
                <td>
                    Đến ngày
                </td>
                <td>
                    <telerik:RadDatePicker ID="raddateDenNgay" runat="server" Enabled="True" Culture="en-US"
                        Width="150px" Font-Names="Arial" Font-Size="Small">
                        <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                            EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;">
                        </Calendar>
                        <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd-MMM-yy" DateFormat="M/d/yyyy"
                            LabelWidth="40%">
                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                            <FocusedStyle Resize="None"></FocusedStyle>
                            <DisabledStyle Resize="None"></DisabledStyle>
                            <InvalidStyle Resize="None"></InvalidStyle>
                            <HoveredStyle Resize="None"></HoveredStyle>
                            <EnabledStyle Resize="None"></EnabledStyle>
                        </DateInput>
                    <%--    <DatePopupButton CssClass="rcCalPopup" ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                    </telerik:RadDatePicker>
                </td>
                <td>
                    <asp:Button ID="btView" runat="server" Text="View" onclick="btView_Click" />
                </td>
                <td>
                    <asp:Button ID="btExcel" runat="server" Text="Excel" onclick="btExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="Both"
                        ShowFooter="true" AllowSorting="true" AllowFilteringByColumn="true" OnItemCommand="RadGrid1_ItemCommand">
                        <GroupingSettings CaseSensitive="False" />
                        <MasterTableView EnableHeaderContextMenu="true">
                            <CommandItemSettings ExportToPdfText="Export to PDF" />
                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                            </RowIndicatorColumn>
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="STT<br/>No" AllowFiltering="False">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                                    <ItemTemplate>
                                        <%# Container.DataSetIndex  + 1%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Print" DataField="LinkDocNoPrint" UniqueName="LinkDocNoPrint"
                                    EmptyDataText="" AllowFiltering="false">
                                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="View" DataField="LinkDocNoView" UniqueName="LinkDocNoView"
                                    EmptyDataText="" AllowFiltering="false">
                                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="SốChứngTừ<br/>Docno" DataField="DocNo" UniqueName="DocNo"
                                    EmptyDataText="" Display="false">
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn HeaderText="Tháng<br/>Month" DataField="Thang" UniqueName="Thang"
                                    EmptyDataText="" Display="true" FilterControlWidth="50px">
                                     <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="60px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn HeaderText="Năm<br/>Year" DataField="Nam" UniqueName="Nam"
                                    EmptyDataText="" Display="true" FilterControlWidth="50px">
                                       <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="60px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Họ tên<br/>Fullname" DataField="FullName" UniqueName="FullName"
                                    EmptyDataText="" FilterControlWidth="110px" Display="true">
                                    <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="120px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Chức vụ<br/>Position" DataField="Position" UniqueName="Position"
                                    EmptyDataText="" FilterControlWidth="110px">
                                    <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="120px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Lý do<br/>Purpose" DataField="Purpose" UniqueName="Purpose"
                                    EmptyDataText="" FilterControlWidth="190px">
                                    <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="200px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Ngày tạo<br/>Date" DataField="DateRec" UniqueName="DateRec"
                                    EmptyDataText="" FilterControlWidth="70px" Display="true" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="90px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Email" DataField="Email" UniqueName="Email"
                                    EmptyDataText="" FilterControlWidth="90px" Display="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="TuNgay" DataField="FDate" UniqueName="FDate"
                                    EmptyDataText="" FilterControlWidth="90px" Display="false" DataFormatString="{0:dd/MM/yyyy}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="DenNgay" DataField="TDate" UniqueName="TDate"
                                    EmptyDataText="" FilterControlWidth="90px" Display="false" DataFormatString="{0:dd/MM/yyyy}">
                                </telerik:GridBoundColumn>
                                  <telerik:GridBoundColumn HeaderText="Khu vực<br/>Area" DataField="Area" UniqueName="Area"
                                    EmptyDataText="" FilterControlWidth="90px" Display="true">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="100px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn HeaderText="Phòng ban<br/>Department" DataField="Department" UniqueName="Department"
                                    EmptyDataText="" FilterControlWidth="100px" Display="true">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="100px"></ItemStyle>
                                </telerik:GridBoundColumn>
                            </Columns>
                            <EditFormSettings EditFormType="WebUserControl">
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <%--    AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true"--%>
                        <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                            <Selecting AllowRowSelect="True" />
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="3">
                            </Scrolling>
                            <Resizing AllowColumnResize="true" EnableRealTimeResize="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
