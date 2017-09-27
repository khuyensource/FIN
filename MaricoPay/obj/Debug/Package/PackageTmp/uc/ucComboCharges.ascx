<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucComboCharges.ascx.cs"
    Inherits="MaricoPay.uc.ucComboCharges" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>--%>
<telerik:RadComboBox ID="radcomboCharges" runat="server" Width="100%"
    EnableLoadOnDemand="True" HighlightTemplatedItems="True" DataTextField="Description"
    DropDownWidth="180px" DataValueField="Charges_PK" 
    Filter="Contains"
    Font-Size="Small" BorderColor="Transparent" 
    BackColor="White" ResolvedRenderMode="Classic" AutoPostBack="True" 
    onselectedindexchanged="radcomboCharges_SelectedIndexChanged">
    <ItemTemplate>
        <table cellpadding="0" cellspacing="0" width="150px">
            <tr valign="middle">
                  <td style="width:80%; text-align:left;">
                    <div style="font-size: 11;">
                        <%# Eval("Description")%>
                    </div>
                </td>
                <td style="width:20%; text-align:right;">
                    <div style="font-size: 11;">
                        <%# Eval("Charges_PK")%>
                    </div>
                </td>
               
            </tr>
        </table>
    </ItemTemplate>
    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
</telerik:RadComboBox>
