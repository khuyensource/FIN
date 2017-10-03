<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucComboDepartment.ascx.cs"
    Inherits="MaricoPay.uc.ucComboDepartment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>--%>
<telerik:RadComboBox ID="radcomboCharges" runat="server" Width="200px"
    EnableLoadOnDemand="True" HighlightTemplatedItems="True" DataTextField="Description"
    DropDownWidth="350px" DataValueField="CostCenter" 
    Filter="Contains" Font-Size="Small" BorderColor="Transparent" 
    BackColor="White" ResolvedRenderMode="Classic" AllowCustomText="True" 
    AutoPostBack="True" 
    onselectedindexchanged="radcomboCharges_SelectedIndexChanged" 
    Font-Names="Arial">
    <ItemTemplate>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr valign="middle">
                  <td style="width:70%; text-align:left;">
                    <div style="font-size: 11;">
                        
                        <asp:Label ID="lbDescription" runat="server" Text=  <%# Eval("Description")%>></asp:Label>
                    </div>
                </td>
                <td style="width:20%; text-align:right;">
                    <div style="font-size: 11;">
                      
                         <asp:Label ID="lbCostcenter" runat="server" Text= <%# Eval("CostCenter")%>></asp:Label>
                    </div>
                </td>
               <td style="width:10%; text-align:right;">
                    <div style="font-size: 11;">
                      
                         <asp:Label ID="lbCompany" runat="server" Text= <%# Eval("Company_FK")%>></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
    </ItemTemplate>
    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
</telerik:RadComboBox>
