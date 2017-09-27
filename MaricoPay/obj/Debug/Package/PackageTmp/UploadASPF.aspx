<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadASPF.aspx.cs" Inherits="MaricoPay.UploadASPF" %>
    <%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title></title>
</head>
<body>
  
    <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
            <uc4:uscMsgBox ID="uscMsgBox1" runat="server" />


    <div>
        <telerik:RadComboBox ID="RadComboBox1" runat="server" DataTextField="display"  DataValueField="value"
            ></telerik:RadComboBox>  ||
        <asp:FileUpload ID="FileUpload1" runat="server" /> |||
        <asp:Button ID="Button1" runat="server" Text="Load" OnClick="btupload_Click" />

           <asp:Button ID="Button2" runat="server" Text="Import" OnClick="Button2_Click" />

    </div>
        <telerik:RadGrid ID="RadGrid1" runat="server">
        </telerik:RadGrid>
    </form>
</body>
</html>
