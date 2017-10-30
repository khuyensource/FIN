<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="MaricoPay.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btcontract" runat="server" Text="Contract" 
            onclick="btcontract_Click" />
        <asp:Button ID="Button1" runat="server" 
            Text="2pdf" onclick="Button1_Click" />
        <asp:Button
                ID="Button2" runat="server" Text="getProperticeKhuyen" onclick="Button2_Click" />
                 <asp:Button
                ID="Button3" runat="server" Text="getalluser" onclick="Button3_Click"/>
                   <asp:Button
                ID="btUnlockFile" runat="server" Text="UnlockFile" 
            onclick="btUnlockFile_Click"/>
             <asp:Button
                ID="Button4" runat="server" Text="AddImagePdf" onclick="Button4_Click" 
            />
    </div>
    <div>
        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Height="143px" 
            Width="630px"></asp:TextBox>
    </div>
    <div>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
