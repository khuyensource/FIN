<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="NewEmployee.aspx.cs" Inherits="MaricoPay.NewEmployee" %>

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
    <script language="javascript" type="text/javascript">
        function DoPostBack(obj) {
            __doPostBack(obj.id, 'OtherInformation');
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
            MEMBER CREATION </legend>
        <table width="60%">
         <tr>
                <td style="width: 20%;">
                    Email:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtEmail" runat="server" Width="290px" Font-Names="Arial" AutoPostBack="true"
                        Font-Size="Medium" ontextchanged="txtEmail_TextChanged"></asp:TextBox>
                    <asp:CheckBox ID="chkActive" runat="server" Text="Active" />
                    <asp:Button ID="btNew" runat="server" Text="New" onclick="btNew_Click" />
                </td>
            </tr>
             <tr>
                <td style="width: 20%;">
                    Full name:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtFullName" runat="server" Width="290px" Font-Names="Arial"
                        Font-Size="Medium"></asp:TextBox>
                    <asp:CheckBox ID="chkManager" runat="server" Text="Is manager" />
                </td>
            </tr>
             <tr>
                <td style="width: 20%;">
                    Company:
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropCompany" AutoPostBack="true" DataTextField="Name" 
                        DataValueField="Company_PK" Width="290px" runat="server" Font-Names="Arial"
                        Font-Size="Medium" 
                        onselectedindexchanged="dropCompany_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Department:
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropDepartment"  DataTextField="Description" AutoPostBack="true"
                        DataValueField="CostCenter" Width="290px" runat="server" Font-Names="Arial"
                        Font-Size="Medium" 
                        onselectedindexchanged="dropDepartment_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 20%;">
                    Position:
                </td>
                <td align="left">
                     <asp:DropDownList ID="dropPosition"  DataTextField="Description" DataValueField="Position_PK" Width="290px" runat="server" Font-Names="Arial"
                        Font-Size="Medium">
                    </asp:DropDownList>
                    <a href="NewPosition.aspx" onclick="window.open(this.href, 'mywin','left=300,top=300,width=500,height=150,toolbar=1,resizable=0'); return false;">
                                        New..</a>
                </td>
            </tr>
             <tr>
                <td>
                    Level:
                </td>
                <td align="left">
                     <asp:DropDownList ID="dropLevel"  DataTextField="Name" DataValueField="ID" Width="290px" runat="server" Font-Names="Arial"
                        Font-Size="Medium">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Manager's (N+1):
                </td>
                <td align="left">
                     <asp:DropDownList ID="dropN1"  DataTextField="Fullname" DataValueField="Username" Width="290px" runat="server" Font-Names="Arial"
                        Font-Size="Medium">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                   Need approve by RSM (N+3):
                </td>
                <td align="left">
                    <asp:CheckBox ID="chkN3" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Vendor code (SAP): 
                    <asp:TextBox ID="txtVendorSAP" runat="server" Width="60px" Font-Names="Arial"
                        Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
           <tr>
                <td>
                    Senior Manager:
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropSenior"  DataTextField="fullname" DataValueField="email" Width="290px" runat="server" Font-Names="Arial"
                        Font-Size="Medium">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td>
                    Director:
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropDirector"  DataTextField="fullname" DataValueField="email" Width="290px" runat="server" Font-Names="Arial"
                        Font-Size="Medium">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td>
                    VP:
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropVP" DataTextField="fullname" DataValueField="email" Width="290px" runat="server" Font-Names="Arial"
                        Font-Size="Medium">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td>
                    COO:
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropCOO"  DataTextField="fullname" DataValueField="email" Width="290px" runat="server" Font-Names="Arial"
                        Font-Size="Medium">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Area(For Sales Only):
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropArea"  DataTextField="Area" DataValueField="ID" Width="290px" runat="server" Font-Names="Arial"
                        Font-Size="Medium">
                    </asp:DropDownList>
                </td>
            </tr>
               <tr>
                <td>
                   Auto update from AD
                </td>
                <td align="left">
                    <asp:CheckBox ID="chkAuto" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btSave" runat="server" Text="Save" OnClick="btSave_Click" />
                </td>
            </tr>
        </table>
    </fieldset>

</asp:Content>
