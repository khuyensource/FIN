<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Loi.aspx.cs" Inherits="MaricoPay.Loi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table style="border-right: #ffcc00 1px solid; padding-right: 0px; border-top: #ffcc00 1px solid;
        padding-left: 0px; padding-bottom: 0px; margin: 0px auto; border-left: #ffcc00 1px solid;
        width: 422px; padding-top: 0px; border-bottom: #ffcc00 1px solid; height: 300px;">
        <tr>
            <td colspan="5" style="width: 494px; height: 28px; text-align: center">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="Blue"
                    Text="THÔNG BÁO" Width="197px"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" colspan="5" style="width: 494px; height: 21px">
                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="User:" Width="37px"></asp:Label><asp:Label ID="lbuser" runat="server" ForeColor="Red"></asp:Label>&nbsp;
                <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="don't authorization"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="5" style="vertical-align: top; width: 594px; height: 300px;
                text-align: left">
                <asp:Label ID="lbquyen" runat="server" ForeColor="Red" Width="488px"></asp:Label></td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
