<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="changepass.aspx.cs" Inherits="MaricoPay.changepass" %>
<%@ Register Src="uc/ucChangepass.ascx" TagName="Changepass" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <uc1:Changepass ID="Changepass1" runat="server" />
</asp:Content>
