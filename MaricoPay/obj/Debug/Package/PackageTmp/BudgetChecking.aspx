<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BudgetChecking.aspx.cs" Inherits="MaricoPay.BudgetChecking" %>

<%@ Register src="uc/ASPFBugetChecking.ascx" tagname="ASPFBugetChecking" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc1:ASPFBugetChecking ID="ASPFBugetChecking1" runat="server" />
    
    </div>
    </form>
</body>
</html>
