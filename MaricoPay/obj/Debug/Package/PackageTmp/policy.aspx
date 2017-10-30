<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="policy.aspx.cs" Inherits="MaricoPay.policy" %>
   
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: left; width: 15%;">
                <uc4:uscMsgBox ID="MsgBox1" runat="server" />
                <asp:Label ID="lbTitle" runat="server" Font-Bold="True"></asp:Label>
                <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="False"
                    Font-Names="Arial" Font-Size="Small" ShowHeader="False" OnItemCommand="RadGrid1_ItemCommand"  BorderStyle="None">
                    <GroupingSettings CaseSensitive="False" />
                    <MasterTableView EnableHeaderContextMenu="true" AllowFilteringByColumn="True">
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <Columns>
                            <telerik:GridButtonColumn DataTextField="Text" CommandName="Open" ButtonType="LinkButton"
                                UniqueName="Open">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn HeaderText="Value" DataField="Value" UniqueName="Value"
                                Display="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <EditFormSettings EditFormType="Template">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <ItemStyle BorderStyle="None" />
                </telerik:RadGrid>
            </div>
            <div style="float: left; width: 85%; height: 500px;">
                <iframe title="X" id="myframe" width="100%" height="100%" runat="server"></iframe>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
