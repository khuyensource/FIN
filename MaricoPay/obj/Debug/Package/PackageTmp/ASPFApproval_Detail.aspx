<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ASPFApproval_Detail.aspx.cs" Inherits="MaricoPay.ASPFApproval_Detail" %>

<%@ Register Src="~/uc/ASPFApproval.ascx" TagPrefix="uc1" TagName="ASPFApproval" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:ASPFApproval runat="server" ID="ASPFApproval" />
        </div>
    </form>
</body>
</html>
