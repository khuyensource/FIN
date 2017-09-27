<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LegalUpload.aspx.cs" Inherits="MaricoPay.LegalUpload" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <script language="javascript" type="text/javascript">
     function DoPostBack(obj) {
         __doPostBack(obj.id, 'PopUpLoadLegal');
     }
   
    </script>
<%--    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" ChildrenAsTriggers="true"
        runat="server">
        <ContentTemplate>
        <div style=" font-family: Arial; font-size: 25px; font-weight:bold; text-align: center;">
        Upload Contract
        </div>
            <div>
                <asp:RadioButtonList ID="radioType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="radioType_SelectedIndexChanged"
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">All finalise</asp:ListItem>
                    <asp:ListItem Value="1">Not upload</asp:ListItem>
                    <asp:ListItem Value="2">Has been upload</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div>
                <telerik:RadGrid ID="RadGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    ShowFooter="True" AllowFilteringByColumn="True" OnItemCommand="RadGrid1_ItemCommand"
                    Font-Names="Arial" Font-Size="Small">
                    <GroupingSettings CaseSensitive="False" />
                    <MasterTableView EnableHeaderContextMenu="true" AllowFilteringByColumn="True">
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <%-- <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                </RowIndicatorColumn>--%>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="No." UniqueName="SoTT" DataField="STT" AllowFiltering="False">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                                <ItemTemplate>
                                    <%# Container.DataSetIndex  + 1%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="statusUser" HeaderText="Action" AllowFiltering="false"
                                UniqueName="ActionColumn">
                                <ItemTemplate>
                                 
                                    <asp:Button ID="btUploadGrid" CommandName="Upload" Width="60px" runat="server" Text="Upload"
                                        ToolTip="Upload final contract" Visible='<%# isShow(Eval ("Loai")) %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn Display="true" HeaderText="Contract No." DataField="ContractNo"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" FilterControlWidth="100%"
                                UniqueName="ContractNo" EmptyDataText="" SortExpression="ContractNo" ShowFilterIcon="false">
                                <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="80px"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="ViewFile" DataField="AttachedFileView" UniqueName="AttachedFileView"
                                EmptyDataText="" Display="true" AllowFiltering="false">
                                <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="50px"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="ContractDate" UniqueName="ContractDate" HeaderText="ContractDate"
                                FilterControlWidth="60px" SortExpression="ContractDate" PickerType="DatePicker"
                                EnableTimeIndependentFiltering="true" Display="true" DataFormatString="{0:dd-MMM-yyyy}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn HeaderText="Dept" DataField="DepartmentName" UniqueName="DepartmentName"
                                EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                Display="false" FilterControlWidth="100%" SortExpression="DepartmentName" ShowFilterIcon="false">
                                <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="40px"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="ContractContent" DataField="ContractContent"
                                UniqueName="ContractContent" EmptyDataText="" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" Display="true" FilterControlWidth="100%" SortExpression="ContractContent"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="120px"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Vendor" DataField="VendorName" UniqueName="VendorName"
                                EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                Display="false" FilterControlWidth="100%" SortExpression="VendorName" ShowFilterIcon="false">
                                <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="80px"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ContractValue" DataField="ContractValue" HeaderText="ContractValue"
                                DataFormatString="{0:###,###.##}" Aggregate="Sum" FooterText="Total: " DataType="System.Decimal"
                                EmptyDataText="0" FilterControlWidth="40px">
                                <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                <FooterStyle Width="70px" HorizontalAlign="Center" />
                                <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Unit" DataField="UnitPrice" UniqueName="UnitPrice"
                                EmptyDataText="" AllowFiltering="false" Display="true" SortExpression="UnitPrice"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="30px"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="ContractNoLegal" DataField="ContractNoLegal"
                                UniqueName="ContractNoLegal" Display="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="FileDocLegal" DataField="FileDocLegal" UniqueName="FileDocLegal"
                                Display="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Display="true" HeaderText="FileScanLegal" DataField="FileScanLegal"
                                UniqueName="FileScanLegal" EmptyDataText="">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="LegalUploadDate" UniqueName="LegalUploadDate"
                                HeaderText="LegalUploadDate" FilterControlWidth="60px" SortExpression="LegalUploadDate"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="true" Display="true"
                                DataFormatString="{0:dd-MMM-yyyy}">
                            </telerik:GridDateTimeColumn>
                             <telerik:GridDateTimeColumn DataField="StampDate" UniqueName="StampDate"
                                HeaderText="StampDate" FilterControlWidth="60px" SortExpression="StampDate"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="true" Display="true"
                                DataFormatString="{0:dd-MMM-yyyy}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn Display="true" HeaderText="LegalUploadUser" DataField="LegalUploadUser"
                                UniqueName="LegalUploadUser" EmptyDataText="">
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn HeaderText="FileDocLegalName" DataField="FileDocLegalName" UniqueName="FileDocLegalName"
                                Display="false">
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn HeaderText="FileScanLegalName" DataField="FileScanLegalName" UniqueName="FileScanLegalName"
                                Display="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <EditFormSettings EditFormType="Template">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true"
                        EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="True" />
                        <%--<Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="8">
                    </Scrolling>
                    <Resizing AllowColumnResize="true" EnableRealTimeResize="true" />--%>
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
