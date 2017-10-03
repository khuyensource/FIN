<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddASPF.aspx.cs" Inherits="MaricoPay.AddASPF" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register src="uc/CreateASPF.ascx" tagname="CreateASPF" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%-- / <telerik:RadScriptManager ID="RadScriptManager166" runat="server"></telerik:RadScriptManager>--%>

        <uc1:CreateASPF ID="CreateASPF1" runat="server" />
    
    </div>
    </form>
</body>
</html>
