<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewEmployee1.aspx.cs" Inherits="MaricoPay.NewEmployee1" %>
<%@ Register src="uc/ucNewEmployee.ascx" tagname="newemployee" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:newemployee ID="newemployee1" runat="server" />
</asp:Content>
