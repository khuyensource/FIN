<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ContractReport.aspx.cs" Inherits="MaricoPay.ContractReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
        <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
        <ContentTemplate>
        <div>
        <table>
        <tr>
        <td>Số hợp đồng</td>
        <td>
            <asp:TextBox ID="txtdocno" runat="server"></asp:TextBox>
        </td>
        <td>
         <asp:Button ID="btViewDoc" runat="server" Text="View" onclick="btViewDoc_Click" />
        </td>
        </tr>
        </table>
        </div>
            <div>
                <table>
                    <tr>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">Contract date</asp:ListItem>
                            <asp:ListItem Value="2">Uploaded date</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                        <td>
                            From date
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="raddateFrom" runat="server" Width="190px" Font-Names="Arial"
                                Font-Size="Small">
                                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                    EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;">
                                </Calendar>
                                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd-MMM-yy" DateFormat="dd-MMM-yy"
                                    LabelWidth="40%">
                                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                </DateInput>
                              
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            To date
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="raddateTo" runat="server" Width="190px" Font-Names="Arial"
                                Font-Size="Small">
                                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                    EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;">
                                </Calendar>
                                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd-MMM-yy" DateFormat="dd-MMM-yy"
                                    LabelWidth="40%">
                                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                </DateInput>
                          <%--      <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                                Department
                            </td>
                            <td>
                                <asp:DropDownList ID="dropOrg" runat="server" Width="190px" DataValueField="Org_PK"
                                    DataTextField="Description" Font-Names="Arial" Font-Size="Small" 
                                    AutoPostBack="False">
                                </asp:DropDownList>
                            </td>
                        <td>
                            <asp:Button ID="btBaoCao" runat="server" Text="View" OnClick="btBaoCao_Click" />
                        </td>
                        <td>
                        <asp:Button ID="btExport" runat="server" Text="Export" onclick="btExport_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <telerik:RadGrid ID="radContractReport" runat="server" 
                    AllowFilteringByColumn="True" ResolvedRenderMode="Classic" 
                    onitemcommand="radContractReport_ItemCommand" 
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
</asp:Content>
