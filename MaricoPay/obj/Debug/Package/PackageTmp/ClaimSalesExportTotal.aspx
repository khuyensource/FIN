<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ClaimSalesExportTotal.aspx.cs" Inherits="MaricoPay.ClaimSalesExportTotal" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table>
            <tr>
                <td style="color: #000000; font-size: small; font-family: @Arial Unicode MS;">
                    1.Tháng/Month <span class="style1">(*)</span>
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
                    2.Năm/Year <span class="style1">(*)</span>
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
                    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="Both"
                        ShowFooter="true" AllowSorting="true" AllowFilteringByColumn="False" 
                        OnItemCommand="RadGrid1_ItemCommand" 
                        onexcelexportcellformatting="RadGrid1_ExcelExportCellFormatting">
                        <GroupingSettings CaseSensitive="False" />
                        <MasterTableView EnableHeaderContextMenu="true">
                            <CommandItemSettings ExportToPdfText="Export to PDF" />
                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                            </RowIndicatorColumn>
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="STT<br/>NUMBER" UniqueName="SoTT" DataField="STT" AllowFiltering="False">
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                                    <ItemTemplate>
                                        <%# Container.DataSetIndex  + 1%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="MÃ NHÂN VIÊN<br/>VENDOR CODE" DataField="Vendor" UniqueName="Vendor"
                                    EmptyDataText="" FilterControlWidth="70px" Aggregate="Count" FooterText="Count: ">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="70px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="HỌ VÀ TÊN<br/>FULL NAME" DataField="FullName" UniqueName="FullName"
                                    EmptyDataText="" FilterControlWidth="130px">
                                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="150px"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Position" DataField="Position" HeaderText="CHỨC VỤ<br/>POSITON TITLE"
                                    EmptyDataText="" FilterControlWidth="120px">
                                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle Width="150px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Area" DataField="Area" HeaderText="KHU VỰC<br/>AREA"
                                    EmptyDataText="" FilterControlWidth="40px" FooterText="TỔNG CỘNG">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                               <telerik:GridBoundColumn UniqueName="TotalVN" DataField="TotalVN" HeaderText="SỐ TIỀN (VND)<br/>AMOUNT "
                                    DataFormatString="{0:###,###,###.##}" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterText="<b>TỔNG CỘNG:</b> " DataType="System.Decimal"
                                    EmptyDataText="0" FilterControlWidth="40px">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="GhiChu" DataField="GhiChu" HeaderText="CHI CHÚ<br/>NOTE"
                                    EmptyDataText="" FilterControlWidth="40px">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Docno" DataField="Docno" HeaderText="SỐ CT<br/>DOC No"
                                    EmptyDataText="" FilterControlWidth="90px">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
