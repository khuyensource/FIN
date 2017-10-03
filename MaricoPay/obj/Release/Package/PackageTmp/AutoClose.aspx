<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoClose.aspx.cs" Inherits="MaricoPay.AutoClose" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register src="uc/ucMsgBox.ascx" tagname="ucMsgBox" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="JavaScript" type="text/javascript">

        var sec = 0;
        function closewindow() {
            //sec++;
            //if (sec == 1) {

            window.parent.close();
            //  }
            window.setTimeout("closewindow();", 1000);

        }




    </script>

    <script language="javascript">
        function CloseWindow11() {
            window.open('', '_self', '');
            window.close();
        }
    </script>


    <script type="text/javascript">
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>


    <script language="javascript" type="text/javascript">
        function StartProgressBar() {
            var myExtender = $find('ProgressBarModalPopupExtender');
            myExtender.show();
            return true;
        }
    </script>

    <%-- --%>

    <style type="text/css">
        .loader
        {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('images/ajax-loader-bar.gif') 50% 50% no-repeat rgb(249,249,249);
        }

        .ModalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            -moz-opacity: 0.5;
            -khtml-opacity: 0.5;
            opacity: 0.5;
        }
    </style>

</head>
<body>
    &nbsp;
    <form id="form1" runat="server">
         <div class="loader">
           


          
           


        </div>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
           <uc1:ucMsgBox ID="MsgBox1" runat="server" />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Button ID="btnSubmit" OnClientClick="StartProgressBar()"
                        runat="server" Text="Submit Time" Width="170px" />

                    

                    <asp:Panel ID="Panel1" runat="server" Style="display: none; background-color: #C0C0C0;">
                        <img src="~/images\ajax-loader-bar.gif.gif" alt="" />
                    </asp:Panel>

                    <asp:HiddenField ID="hiddenField" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>




    </form>
</body>
</html>
