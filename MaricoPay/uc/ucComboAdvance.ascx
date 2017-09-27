<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucComboAdvance.ascx.cs"
    Inherits="MaricoPay.uc.ucComboAdvance" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>--%>
<telerik:RadComboBox ID="radcomboCharges" runat="server" Width="200px"
    EnableLoadOnDemand="True" HighlightTemplatedItems="True" DataTextField="Code_PK"
    DropDownWidth="200px" DataValueField="TienTamUng" 
    Filter="Contains" Font-Size="Small" BorderColor="Transparent" 
    BackColor="White" ResolvedRenderMode="Classic" AllowCustomText="True">
   <ItemTemplate>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr valign="middle">
                  <td style="width:80%; text-align:left;">
                    <div style="font-size: 11;">
                        <%# Eval("Code_PK")%>
                    </div>
                </td>
                <td style="width:20%; text-align:right;">
                    <div style="font-size: 11;">
                        <%# Eval("TienTamUng")%>
                    </div>
                </td>
               
            </tr>
        </table>
    </ItemTemplate>
    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
</telerik:RadComboBox>
