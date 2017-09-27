<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ADUser.aspx.cs" Inherits="MaricoPay.ADUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
<table>
<tr>
<td>Password AD</td>
<td>
<asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>
</td>
<td>
 <asp:Button ID="btGetData" runat="server" Text="GetData" 
        onclick="btGetData_Click" />
</td>
<td>
 <asp:Button ID="btExcel" runat="server" Text="Excel" onclick="btExcel_Click" />
</td>
<td>
<asp:Button ID="btUpdateStatus" runat="server" Text="Set Inactive auto" 
        onclick="btUpdateStatus_Click"/>
</td>
</tr>
</table>
    
   
        
</div>
<div>
    <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="true" AllowCustomPaging="false"
        AllowSorting="true" ShowGroupPanel="True" 
        onitemcommand="RadGrid1_ItemCommand" Width="100%" 
        ongroupschanging="RadGrid1_GroupsChanging">
          <GroupingSettings CaseSensitive="False" />
           <MasterTableView TableLayout="fixed" Width="100%" Height="99%">  
           <CommandItemStyle HorizontalAlign="Right" /> 
                                            </MasterTableView>
                    <ClientSettings AllowColumnHide="True" AllowDragToGroup="True">
                        <Selecting AllowRowSelect="True" />
                        <Resizing AllowColumnResize="True" />
                        <Scrolling AllowScroll="true" SaveScrollPosition="true"  UseStaticHeaders="True" ScrollHeight="700px"/>
                    </ClientSettings>
    </telerik:RadGrid>

</div>
</asp:Content>
