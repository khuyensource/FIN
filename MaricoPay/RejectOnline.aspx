<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RejectOnline.aspx.cs" Inherits="MaricoPay.RejectOnline" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1
        {
            width: 208px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <script language="javascript">
                function CloseWindow11() {
                    window.open('', '_self', '');
                    window.close();
                }
            </script>

            <script type="text/javascript">



                var sec = 0;
                function closewindow() {
                    sec++;
                    if (sec == 10) {
                        window.parent.close();
                    }
                    window.setTimeout("closewindow();", 1000);

                }


                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow) oWindow = window.radWindow;
                    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                    return oWindow;
                }

                function CloseDialog() {
                    GetRadWindow().close();
                    return false;
                }

                function OnClientClose() {
                    __doPostBack('fLoad', '');
                }

                function openRadWindowPreview(ID) {
                    var manager = $find("<%= RadWindowManager1.ClientID %>");
                 var oWnd = manager.open("ASPFApproval_Detail.aspx?Userid=" + ID, "RadWindowManager1");
                 oWnd.center();
                 oWnd.maximize();
             }


            </script>

            <telerik:RadWindowManager ShowContentDuringLoad="false" AutoSize="true" EnableShadow="true"
                ID="RadWindow1" runat="server" OnClientClose="OnClientClose" VisibleStatusbar="false"
                ReloadOnShow="true">
            </telerik:RadWindowManager>


            <telerik:RadWindowManager ShowContentDuringLoad="false" EnableShadow="true" ID="RadWindowManager1" Width="600px"
                runat="server" OnClientClose="OnClientClose" VisibleStatusbar="false" ReloadOnShow="true">
            </telerik:RadWindowManager>



            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            </telerik:RadScriptManager>

            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            </telerik:RadAjaxManager>


            <uc4:uscMsgBox ID="MsgBox1" runat="server" />


            <table>
                <tr>
                    <td>Reason
                    </td>
                    <td class="auto-style1">
                        <telerik:RadComboBox ID="RdcboReason" DataTextField="Name" DataValueField="ID" runat="server"></telerik:RadComboBox>
                        <asp:Label ID="Label123" Text="*" Font-Size="X-Large" ForeColor="Red"
                            runat="server"> </asp:Label>
                    </td>

                    <td>Note
                    </td>
                    <td>
                        <asp:TextBox ID="txtNote" TextMode="MultiLine" runat="server" Height="20px" Width="172px"></asp:TextBox>
                    </td>

                </tr>

            </table>
            |
     <asp:Button ID="btnSubmit" runat="server" Text="Send" OnClick="btnSubmit_Click" Font-Bold="True" Font-Size="14px" />
            |
 <asp:Button ID="Button1" runat="server" Text="Close" OnClick="Button1_Click" Font-Bold="True" Font-Size="14px"
     OnClientClick="return CloseDialog();" CommandName="Close" />
            &nbsp;

              
    <table width="600px">

        <tr>

            <td>
                <%--  <asp:ImageButton ID="btnAddrow" runat="server" ImageUrl="~/images/add.png"  ToolTip="Add row" AlternateText="Add row" ImageAlign="Left" OnClick="btnAddrow_Click" />--%>
                 
            </td>

        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
        </div>
    </form>
</body>
</html>
