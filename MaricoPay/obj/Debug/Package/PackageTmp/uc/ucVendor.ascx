<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucVendor.ascx.cs"
    Inherits="MaricoPay.uc.ucVendor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>--%>
<telerik:RadComboBox ID="radcomboCharges" runat="server" Width="100%"
    EnableLoadOnDemand="True" HighlightTemplatedItems="True" DataTextField="VendorName"
    DropDownWidth="450px" DataValueField="VendorCode"
    Filter="Contains" Font-Size="Small" BorderColor="Transparent" 
    BackColor="White" ResolvedRenderMode="Classic" AllowCustomText="True" 
    AutoPostBack="True" 
    onselectedindexchanged="radcomboCharges_SelectedIndexChanged" 
    Font-Names="Arial" ontextchanged="radcomboCharges_TextChanged">
    <ItemTemplate>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr valign="middle">
                  <td style="width:80%; text-align:left;">
                    <div style="font-size: 11;">
                        
                        <asp:Label ID="lbVendorName" runat="server" Text=  <%# Eval("VendorName")%>></asp:Label>
                    </div>
                </td>
                <td style="width:20%; text-align:right;">
                    <div style="font-size: 11;">
                      
                         <asp:Label ID="lbVendorCode" runat="server" Text= <%# Eval("VendorCode")%>></asp:Label>
                    </div>
                </td>
              
            </tr>
        </table>
    </ItemTemplate>
    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
</telerik:RadComboBox>
