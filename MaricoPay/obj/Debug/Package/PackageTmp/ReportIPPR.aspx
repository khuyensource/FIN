<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ReportIPPR.aspx.cs" Inherits="MaricoPay.ReportIPPR" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                      <%--  <DatePopupButton CssClass="rcCalPopup" ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
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
                    <telerik:RadGrid ID="radIPPRReport" runat="server" 
                    AllowFilteringByColumn="True" ResolvedRenderMode="Classic" 
                    onitemcommand="radradIPPRReport_ItemCommand" 
                   >
                      <GroupingSettings CaseSensitive="False" />
                    <ClientSettings AllowColumnHide="True">
                        <Selecting AllowRowSelect="True" />
                        <Resizing AllowColumnResize="True" />
                    </ClientSettings>
                    <FilterMenu ViewStateMode="Enabled">
                    </FilterMenu>
                </telerik:RadGrid>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
