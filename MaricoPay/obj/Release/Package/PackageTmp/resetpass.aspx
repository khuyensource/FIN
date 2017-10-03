<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="resetpass.aspx.cs" Inherits="MaricoPay.resetpass" %>
<%@ Register Src="uc/ucResetPass.ascx" TagName="reset" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <uc1:reset ID="reset1" runat="server" />
</asp:Content>
