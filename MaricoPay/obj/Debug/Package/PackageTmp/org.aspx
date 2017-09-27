<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="org.aspx.cs" Inherits="MaricoPay.org" %>
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
                    Tên/Name
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    DOA CODE
                </td>
                <td>
                    <asp:TextBox ID="txtDOA" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Loại/Type
                </td>
                <td>
                    <asp:TextBox ID="txtType" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Ký hiệu/Notation
                </td>
                <td>
                    <asp:TextBox ID="txtNotation" runat="server" Width="300px"></asp:TextBox>
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
                                <asp:Button ID="btcancel" runat="server" Text="Cancel" 
                                    onclick="btcancel_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
