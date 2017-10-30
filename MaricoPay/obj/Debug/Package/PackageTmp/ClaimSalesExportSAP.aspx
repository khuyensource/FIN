<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ClaimSalesExportSAP.aspx.cs" Inherits="MaricoPay.ClaimSalesExportSAP" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table>
            <tr>
                <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                    1.Loai/Type <span class="style1">(*)</span>
                </td>
                <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                    <asp:DropDownList ID="dropLoai" runat="server" Font-Names="Arial" Font-Size="Small">
                        <asp:ListItem Value="1">Employee</asp:ListItem>
                        <asp:ListItem Value="2">Ontime vendor employee</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                    2.Posting date <span class="style1">(*)</span>
                </td>
                <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                    <asp:TextBox ID="txtPostdate" runat="server" MaxLength="10" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                </td>
                <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                    3.Tháng/Month <span class="style1">(*)</span>
                </td>
                <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                    <asp:DropDownList ID="dropThang" runat="server" Font-Names="Arial" Font-Size="Small">
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
                    4.Năm/Year <span class="style1">(*)</span>
                </td>
                <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                    <asp:DropDownList ID="dropNam" runat="server" DataTextField="Years" DataValueField="Years"
                        Font-Names="Arial" Font-Size="Small">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btView" runat="server" Text="View" OnClick="btView_Click" />
                </td>
                <td>
                    <asp:Button ID="btExcel" runat="server" Text="Excel" OnClick="btExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="True" GridLines="Both"
                        ShowFooter="true" AllowSorting="true" AllowFilteringByColumn="False" OnItemCommand="RadGrid1_ItemCommand">
                        <GroupingSettings CaseSensitive="False" />
                        <MasterTableView EnableHeaderContextMenu="true">
                            <CommandItemSettings ExportToPdfText="Export to PDF" />
                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                            </RowIndicatorColumn>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
