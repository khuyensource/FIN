<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="MaricoPay.uc.menu" %>
<%--<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>--%>
<%--<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadMenu2">
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>--%>
<style type="text/css">
    body
    {
        font-family: Arial;
        font-size: 11pt;
    }
    .main_menu
    {
        width: 100px;
        color: White;
        text-align: center;
        height: 30px;
        line-height: 30px;
        margin-right: 5px;
        margin-top: -10px;
        font-size: 11pt;
    }
    .main_menu1
    {
        width: 100px;
        color: White;
        text-align: center;
        height: 30px;
        line-height: 30px;
        margin-right: 5px;
        margin-top: -10px;
        font-size: 11pt;
        background-color: Yellow;
    }
    .level_menu
    {
        width: 130px;
        background-color: #00CC33;
        color: White;
        text-align: center;
        height: 30px;
        line-height: 30px;
        margin-top: 5px;
    }
    .selected
    {
        background-color: #FFEBCD;
        color: White;
    }
    .hover
    {
        background-color: yellow;
    }
</style>
<div>
    <table cellpadding="0px" cellspacing="0px">
        <tr>
            <td>
                <asp:Menu ID="navMenu" runat="server" StaticMenuItemStyle-VerticalPadding="6px" StaticMenuItemStyle-HorizontalPadding="10px"
                    RenderingMode="List" Orientation="Horizontal">
                    <%--  <StaticMenuStyle BackColor="#EEEEEE" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="0px"
            HorizontalPadding="0px" VerticalPadding="0px" />--%>
                    <%--  mau nen va chu của menu cha--%>
                    <StaticMenuItemStyle BackColor="#33FF99" ForeColor="Black" ItemSpacing="4px" />
                    <%--  mau nen va chua của menu cha khi re chuot--%>
                    <StaticHoverStyle BackColor="#000000" ForeColor="White" />
                    <%--<DynamicMenuStyle BackColor="#007FFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />--%>
                    <%--<StaticSelectedStyle BackColor="#007FFF" />--%>
                    <%--  mau nen, chu của menu con--%>
                    <DynamicMenuItemStyle BackColor="#6699CC" Width="160px" BorderColor="#CCCCCC" BorderStyle="Solid"
                        BorderWidth="1px" ForeColor="Black" HorizontalPadding="4px" VerticalPadding="4px" />
                    <%--  mau nen của menu con khi re chuot--%>
                    <DynamicHoverStyle BackColor="#000000" ForeColor="White" />
                    <Items>
                        <asp:MenuItem Text="Travel Request" Value="Travel">
                         <asp:MenuItem NavigateUrl="~/TravelRequest.aspx?type=0" Text="Create" Value="Create">
                                </asp:MenuItem>
                                 <asp:MenuItem NavigateUrl="~/TravelRequestSales.aspx?type=0" Text="Create For Sales" Value="CreateSales">
                                </asp:MenuItem>
                                <asp:MenuItem Text="Approve" Value="Approve" NavigateUrl="~/TravelRequest.aspx?type=2">
                                </asp:MenuItem>
                                <asp:MenuItem Text="Print out" Value="Print" NavigateUrl="~/TravelRequest.aspx?type=4">
                                </asp:MenuItem>

                            <%--<asp:MenuItem Text="Travel request" Value="Create">
                                <asp:MenuItem NavigateUrl="~/TravelRequest.aspx?type=0" Text="Create" Value="Create">
                                </asp:MenuItem>
                                <asp:MenuItem Text="Approve" Value="Approve" NavigateUrl="~/TravelRequest.aspx?type=2">
                                </asp:MenuItem>
                                <asp:MenuItem Text="Print out" Value="Print" NavigateUrl="~/TravelRequest.aspx?type=4">
                                </asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="Vendor/staff" Value="Create">
                                <asp:MenuItem NavigateUrl="~/TravelRequest.aspx?type=20" Text="Create" Value="Create">
                                </asp:MenuItem>
                                <asp:MenuItem Text="Approve" Value="Approve" NavigateUrl="~/TravelRequest.aspx?type=21">
                                </asp:MenuItem>
                                <asp:MenuItem Text="Print out" Value="Print" NavigateUrl="~/TravelRequest.aspx?type=22">
                                </asp:MenuItem>
                            </asp:MenuItem>--%>
                        </asp:MenuItem>
                        <asp:MenuItem Text="|" Value="Travel"></asp:MenuItem>
                        <asp:MenuItem Text="Expense claim" Value="Expenses">
                            <asp:MenuItem NavigateUrl="~/ClaimExpensesOffice.aspx?type=0" Text="Create" Value="Create">
                            </asp:MenuItem>
                            <asp:MenuItem Text="Approve" Value="Approve" NavigateUrl="~/ClaimExpensesOffice.aspx?type=2">
                            </asp:MenuItem>
                            <asp:MenuItem Text="Print out" Value="Print" NavigateUrl="~/ClaimExpensesOffice.aspx?type=4">
                            </asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="|" Value="Travel"></asp:MenuItem>
                        <asp:MenuItem Text="Document Management" Value="Document">
                            <asp:MenuItem NavigateUrl="~/DocDefault.aspx?type=DI" Text="Incoming" Value="IN">
                            </asp:MenuItem>
                            <asp:MenuItem Text="Outgoing" Value="Out" NavigateUrl="~/DocDefault.aspx?type=DO">
                            </asp:MenuItem>
                            <asp:MenuItem Text="Accept incoming" Value="Approve" NavigateUrl="~/DocDefault.aspx?type=APPDI">
                            </asp:MenuItem>
                            <asp:MenuItem Text="Accept outgoing" Value="Approve" NavigateUrl="~/DocDefault.aspx?type=APPDO">
                            </asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="|" Value="Travel"></asp:MenuItem>
                        <asp:MenuItem Text="Contract Management" Value="Contract">
                            <asp:MenuItem NavigateUrl="~/contract.aspx?type=0" Text="Create" Value="Create">
                            </asp:MenuItem>
                            <asp:MenuItem Text="Review" Value="Approve" NavigateUrl="~/contract.aspx?type=2">
                            </asp:MenuItem>
                             <asp:MenuItem Text="LegalUpload" Value="LegalUpload" NavigateUrl="~/LegalUpload.aspx">
                            </asp:MenuItem>
                             <asp:MenuItem Text="Report" Value="ConReport" NavigateUrl="~/ContractReport.aspx">
                            </asp:MenuItem>
                              <asp:MenuItem Text="Archive" Value="Archive" NavigateUrl="~/contract.aspx?type=3">
                            </asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="|" Value="Travel"></asp:MenuItem>
                        <asp:MenuItem Text="ASP Control" Value="ASP">
                            <asp:MenuItem NavigateUrl="~/ASPF.aspx" Text="Create" Value="Create"></asp:MenuItem>
                            <asp:MenuItem Text="Budget checking" Value="Checking" NavigateUrl="~/ASPFController.aspx">
                            </asp:MenuItem>
                            <asp:MenuItem Text="Approve" Value="Approve" NavigateUrl="~/ApprovalASPF.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Export ASPF " Value="exportaspf" NavigateUrl="~/ExportASPF.aspx"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="|" Value="Travel"></asp:MenuItem>
                        <asp:MenuItem Text="Policy & Procedure" Value="Policy">
                          <asp:MenuItem Text="DOA" Value="doa1" NavigateUrl="~/policy.aspx?type=DOA">
                                <%-- <asp:MenuItem Text="Policy" Value="Policy1" NavigateUrl="~/#"></asp:MenuItem>
                                <asp:MenuItem Text="Procedure" Value="Procedure1" NavigateUrl="~/#"></asp:MenuItem>--%>
                            </asp:MenuItem>
                         <asp:MenuItem Text="Travel request" Value="travel1" NavigateUrl="~/policy.aspx?type=Travel">
                                <%-- <asp:MenuItem Text="Policy" Value="Policy1" NavigateUrl="~/#"></asp:MenuItem>
                                <asp:MenuItem Text="Procedure" Value="Procedure1" NavigateUrl="~/#"></asp:MenuItem>--%>
                            </asp:MenuItem>
                            <asp:MenuItem Text="Expense Claim" Value="expenses1" NavigateUrl="~/policy.aspx?type=Expense">
                                <%--<asp:MenuItem Text="Policy" Value="Policy1" NavigateUrl="~/#"></asp:MenuItem>
                                <asp:MenuItem Text="Procedure" Value="Procedure1" NavigateUrl="~/#"></asp:MenuItem>--%>
                            </asp:MenuItem>
                            <asp:MenuItem Text="Document management" Value="Document1" NavigateUrl="~/policy.aspx?type=Document">
                             
                            </asp:MenuItem>
                            <asp:MenuItem Text="Contract management" Value="Contract1" NavigateUrl="~/policy.aspx?type=Contract">
                                <%--   <asp:MenuItem Text="Policy" Value="Policy1" NavigateUrl="~/#"></asp:MenuItem>
                                <asp:MenuItem Text="Procedure" Value="Procedure1" NavigateUrl="~/#"></asp:MenuItem>--%>
                            </asp:MenuItem>
                            <asp:MenuItem Text="ASP control" Value="asp1" NavigateUrl="~/policy.aspx?type=ASP">
                                <%--   <asp:MenuItem Text="Policy" Value="Policy1" NavigateUrl="~/#"></asp:MenuItem>
                                <asp:MenuItem Text="Procedure" Value="Procedure1" NavigateUrl="~/#"> </asp:MenuItem>--%>
                            </asp:MenuItem>
                           
                            <asp:MenuItem Text="SAP Payment" Value="advance1" NavigateUrl="~/policy.aspx?type=SAP">
                                <%-- <asp:MenuItem Text="Policy" Value="Policy1" NavigateUrl="~/#"></asp:MenuItem>
                                <asp:MenuItem Text="Procedure" Value="Procedure1" NavigateUrl="~/#"></asp:MenuItem>--%>
                            </asp:MenuItem>
                             <asp:MenuItem Text="Settings" Value="setting1" NavigateUrl="~/policy.aspx?type=Settings">
                                <%-- <asp:MenuItem Text="Policy" Value="Policy1" NavigateUrl="~/#"></asp:MenuItem>
                                <asp:MenuItem Text="Procedure" Value="Procedure1" NavigateUrl="~/#"></asp:MenuItem>--%>
                            </asp:MenuItem>
                        </asp:MenuItem>
                    </Items>
                </asp:Menu>
            </td>
        </tr>
    </table>
</div>
