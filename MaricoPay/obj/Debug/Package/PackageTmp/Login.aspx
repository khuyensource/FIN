<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableSessionState="true"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MaricoPay.Login1"
    ValidateRequest="false" %>

<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <%-- <script type="text/javascript" language="javascript">
        function CloseWindow() {
            window.close();
        }
    </script>
    <script type="text/javascript">
        function HandleOnclose() {
            alert("Close Session");
            PageMethods.AbandonSession();
        }
        window.onbeforeunload = HandleOnclose;
    </script>--%>
    <h2>
        Log In
    </h2>
    <p>
        Please enter your username and password.
        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Register</asp:HyperLink>
        if you don't have an account.
    </p>
    <uc4:uscMsgBox ID="MsgBox1" runat="server" />
    <%--  <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false">
        <LayoutTemplate>--%>
    <span class="failureNotification">
        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
    </span>
    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
        ValidationGroup="LoginUserValidationGroup" />
    <div class="accountInfo">
        <fieldset class="login">
            <legend>Account Information</legend>
            <p>
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtUserName">Username:</asp:Label>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName"
                    CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                    ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="txtPassword">Password:</asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                    CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                    ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <a href="resetpass.aspx" id="forgot" class="forgot" runat="server" visible="false">Reset
                    password</a>
                <asp:CheckBox ID="RememberMe" runat="server" Visible="false" />
                <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline"
                    Visible="false">Keep me logged in</asp:Label>
            </p>
        </fieldset>
        <p class="submitButton">
            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Sign in" ValidationGroup="LoginUserValidationGroup"
                OnClick="LoginButton_Click" />
        </p>
    </div>
    <%-- </LayoutTemplate>
    </asp:Login>--%>
</asp:Content>
