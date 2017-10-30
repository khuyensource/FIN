<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Department.aspx.cs" Inherits="MaricoPay.Department1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        
        <tr>
            
            <td>Department</td>
            <td>
                <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox></td>
            
            
        </tr>
        <tr>
            <td>Description</td>
            <td>
                <asp:TextBox ID="txtDecription" runat="server"></asp:TextBox></td>

        </tr>
        <tr>
            <td>Active</td>
            <td>
                <asp:CheckBox ID="chkActive" runat="server" Text="" /></td>

        </tr>
        <tr>
            
            <td colspan="2">
                <asp:Button ID="btAdd" runat="server" Text="Add" onclick="btAdd_Click" /></td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
