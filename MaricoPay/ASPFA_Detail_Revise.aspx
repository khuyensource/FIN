<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ASPFA_Detail_Revise.aspx.cs" Inherits="MaricoPay.ASPFA_Detail_Revise" %>

<%@ Register Src="~/uc/ASPFApproval_Revise.ascx" TagPrefix="uc1" TagName="ASPFApproval_Revise" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:ASPFApproval_Revise runat="server" ID="ASPFApproval_Revise" />
        </div>
    </form>
</body>
</html>
