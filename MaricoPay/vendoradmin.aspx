<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vendoradmin.aspx.cs" Inherits="MaricoPay.vendoradmin" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
     <uc4:uscMsgBox ID="MsgBox1" runat="server" />
        <table>
         <tr>
                <td>
                    Code
                </td>
                <td>
                    <asp:TextBox ID="txtCode" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Name
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" Width="300px"></asp:TextBox>
                    <asp:Button ID="btFind" runat="server" Text="Find" onclick="btFind_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    Address
                </td>
                <td>
                    <asp:TextBox ID="txtAdd" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Telephone
                </td>
                <td>
                    <asp:TextBox ID="txtTel" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Taxcode
                </td>
                <td>
                    <asp:TextBox ID="txtTax" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Bank Acc No.
                </td>
                <td>
                    <asp:TextBox ID="txtBankNo" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Bank Acc Name
                </td>
                <td>
                    <asp:TextBox ID="txtBanAcc" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Bank name
                </td>
                <td>
                    <asp:TextBox ID="txtBankName" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Bank city
                </td>
                <td>
                    <asp:TextBox ID="txtBankcity" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    Pur Org
                </td>
                <td>
                    <asp:TextBox ID="txtPurOrg" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btsave" runat="server" Text="Save" onclick="btsave_Click" />
                            </td>
                            <td>
                               
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <telerik:RadGrid ID="RadGrid1" runat="server">
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </div>
    </form>
</body>
</html>
