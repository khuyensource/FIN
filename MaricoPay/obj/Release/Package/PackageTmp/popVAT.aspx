<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popVAT.aspx.cs" Inherits="MaricoPay.popVAT" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VAT Detail</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <table>
            <tr>
                <td>
                    Company
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtCompany" runat="server" Width="384px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    City/Province
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtProvince" runat="server" Width="379px"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <td>
                    Tax numbeer
                </td>
                <td>
                    <asp:TextBox ID="txtTaxNumber" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    Tax Code
                </td>
                <td>
                    <asp:DropDownList ID="dropTaxCode" runat="server">
                        <asp:ListItem Value="I0">0%</asp:ListItem>
                        <asp:ListItem Value="I1">5%</asp:ListItem>
                        <asp:ListItem Value="I2">10%</asp:ListItem>
                        <asp:ListItem Value="I3">25%</asp:ListItem>
                        <asp:ListItem Value="I4">30%</asp:ListItem>
                        <asp:ListItem Value="I5">50%</asp:ListItem>
                    </asp:DropDownList>
                </td>
                 <td>
                    VAT Amount
                </td>
                <td>
                    <telerik:RadNumericTextBox ID="radnumVATAmount" runat="server">
                    </telerik:RadNumericTextBox>
                </td>
            </tr>
           <tr>
           <td colspan="6" align="center"> <asp:Button ID="btSave" runat="server" Text="Save" 
                   onclick="btSave_Click" /></td>
           </tr>
        </table>
    </div>
   
    </form>
</body>
</html>
