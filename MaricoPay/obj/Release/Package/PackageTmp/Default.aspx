<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="MaricoPay._Default" %>

<%@ Register Src="~/uc/ucComboDepartment.ascx" TagName="department" TagPrefix="uc5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<%@ Register Src="~/uc/ucViewStatus.ascx" TagName="ucViewStatus" TagPrefix="uc6" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        html, body
        {
            height: 100%;
            margin: 0;
            padding: 0;
        }
        .style2
        {
            width: 247px;
        }
    </style>
    <script type="text/javascript">

        function GetUserName() {
            var objUserInfo = new ActiveXObject("WScript.network");
            document.write(objUserInfo.ComputerName + "<br>");
            document.write(objUserInfo.UserDomain + "<br>");
            document.write(objUserInfo.UserName + "<br>");
            var uname = objUserInfo.UserName;
            alert(uname);
        }
        function getUser() {
            return Components.classes["@mozilla.org/process/environment;1"].getService(Components.interfaces.nsIEnvironment).get('USERNAME');
        }
        function getLogin() {
            var szLogin = '<%Response.Write(Request.ServerVariables["LOGON_USER"]); %>'

            var length = szLogin.length;
            alert(szLogin);
            return szLogin;
        }
</script>
    <h2>
        Welcome to FIN SYSTEM!
    </h2>
   <%-- <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />--%>
    <uc4:uscMsgBox ID="MsgBox1" runat="server" />
    <fieldset>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="getLogin();" Visible="false" />
        <legend style="font-size: large; font-weight: bold; font-style: oblique">
            <asp:Label ID="lbName" runat="server" Text="Label"></asp:Label>
            &nbsp; &nbsp;Information: </legend>
        <table width="60%">
            <tr>
                <td style="width: 20%;">
                    Position:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtPosition" runat="server" Width="190px" ReadOnly="true" Font-Names="Arial"
                        Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Manager's (N+1):
                </td>
                <td align="left">
                    <asp:TextBox ID="txtN1" runat="server" Width="190px" ReadOnly="true" Font-Names="Arial"
                        Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Manager's (N+2):
                </td>
                <td align="left">
                    <asp:TextBox ID="txtN2" runat="server" Width="190px" ReadOnly="true" Font-Names="Arial"
                        Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Department:
                </td>
                <td align="left">
                    <uc5:department ID="comboDepartment1" runat="server" />
                </td>
            </tr>
           
             <tr>
                <td>
                    Director:
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropDirector"  DataTextField="fullname" DataValueField="email" Width="190px" runat="server" Font-Names="Arial"
                        Font-Size="Medium">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td>
                    VP:
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropVP" DataTextField="fullname" DataValueField="email" Width="190px" runat="server" Font-Names="Arial"
                        Font-Size="Medium">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td>
                    COO:
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropCOO"  DataTextField="fullname" DataValueField="email" Width="190px" runat="server" Font-Names="Arial"
                        Font-Size="Medium">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
            <td>SAP Code</td>
            <td>
                <asp:TextBox ID="txtSAPCode" runat="server" Width="190px" ReadOnly="true" Font-Names="Arial"
                        Font-Size="Medium"></asp:TextBox>
            </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btSave" runat="server" Text="Save" Visible="false" OnClick="btSave_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    </p>
    <div id="divinfo" runat="server" visible="false">
        <table>
            <tr>
                <td>
                    loggedOnUser
                </td>
                <td>
                    <asp:Label ID="lbloggedOnUser" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    PC
                </td>
                <td>
                    <asp:Label ID="lbPC" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    user
                </td>
                <td>
                    <asp:Label ID="lbuser" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    username ID
                </td>
                <td>
                    <asp:Label ID="lbusernameid" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <fieldset>
    <legend style="font-size: large; font-weight: bold; font-style: oblique">
           Travel request last 30 day status </legend>
      <uc6:ucViewStatus ID="ucViewStatus1" runat="server" />

       </fieldset>
    </div>
     <div>
        <fieldset>
    <legend style="font-size: large; font-weight: bold; font-style: oblique">
           Expenses claim last 30 day status </legend>
      <uc6:ucViewStatus ID="ucViewStatus2" runat="server" />

       </fieldset>
    </div>
        <div>
        <fieldset>
    <legend style="font-size: large; font-weight: bold; font-style: oblique">
           Contract review last 30 day status </legend>
      <uc6:ucViewStatus ID="ucViewStatus3" runat="server" />

       </fieldset>
    </div>
</asp:Content>
