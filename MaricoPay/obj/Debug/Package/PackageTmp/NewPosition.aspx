<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewPosition.aspx.cs" Inherits="MaricoPay.NewPosition" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Position</title>
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
                <td>
                    <asp:DropDownList ID="dropCompany" AutoPostBack="true" DataTextField="Name" 
                        DataValueField="Company_PK" Width="190px" runat="server" Font-Names="Arial"
                        Font-Size="Medium" 
                        onselectedindexchanged="dropCompany_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Costcenter
                </td>
                <td>
                      <asp:DropDownList ID="dropDepartment"  DataTextField="Description" AutoPostBack="true"
                        DataValueField="CostCenter" Width="190px" runat="server" Font-Names="Arial"
                        Font-Size="Medium" 
                       >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
            <td>
                    Position name
                </td>
                <td>
                    <asp:TextBox ID="txtPosition" runat="server" Width="379px"></asp:TextBox>
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
