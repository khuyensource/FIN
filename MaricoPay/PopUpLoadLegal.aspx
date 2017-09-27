<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUpLoadLegal.aspx.cs"
    Inherits="MaricoPay.PopUpLoadLegal" %>

<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
       
        function clear() {
            document.getElementById('fileUpDoc').outerHTML = document.getElementById('fileUpDoc').outerHTML;
           
        }
        function clear1() {
            document.getElementById('fileUpPdf').outerHTML = document.getElementById('fileUpPdf').outerHTML;
          
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc4:uscMsgBox ID="MsgBox1" runat="server" />
        <table cellpadding="2" cellspacing="0" style="font-size: 12px; font-family: Arial;">
            <tr>
                <td>
                    ID
                </td>
                <td>
                    <asp:TextBox ID="txtID" runat="server" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Contract number
                </td>
                <td>
                    <asp:TextBox ID="txtcontractnumber" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Stamp Date
                </td>
                <td>
                    <telerik:RadDatePicker ID="radDateContract" runat="server" Font-Names="Arial" Font-Size="Small"
                        Width="150px">
                        <Calendar ID="Calendar1" runat="server" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"
                            UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                        </Calendar>
                        <DateInput ID="DateInput1" runat="server" DateFormat="dd-MMM-yy" DisplayDateFormat="dd-MMM-yy"
                            LabelWidth="40%">
                            <EmptyMessageStyle Resize="None" />
                        </DateInput>
                    <%--    <DatePopupButton HoverImageUrl="" ImageUrl="" Enabled="true" />--%>
                    </telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td>
                    File upload (Doc)
                </td>
                <td>
                    <asp:FileUpload ID="fileUpDoc" runat="server"/>
                    <asp:Button ID="btremoveDoc" runat="server" Text="X" 
                       onclick="btremoveDoc_Click"  />
                </td>
            </tr>
            <tr>
                <td>
                    File upload scan (Pdf)
                </td>
                <td>
                    <asp:FileUpload ID="fileUpPdf" runat="server" />
                    <asp:Button ID="btremovePdf" runat="server" Text="X" OnClientClick="clear1(); return false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btSave" runat="server" Text="Save" OnClick="btSave_Click" />
                </td>
                <td>
                    <asp:Button ID="btCancel" runat="server" Text="Cancel" OnClick="btCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
