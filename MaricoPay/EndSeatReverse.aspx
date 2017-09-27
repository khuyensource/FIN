<%@ Page Title="" Language="C#" MasterPageFile="~/masters/Site.Master" AutoEventWireup="true" CodeBehind="EndSeatReverse.aspx.cs" Inherits="Pennycms.Pages.SeatReverseAuction.EndSeatReverse" %>

<%@ Import Namespace="WebLibs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Text" runat="Server">
    <script type="text/javascript" language="javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            var value = $('#ctl00_ContentPlaceHolder_Text_hfError').val();      
            if (value == "13") {
                alert('<%# WebLibs.DBCommon.GetLang(Resources.Lang.Limit_Products) %>');
                $('#ctl00_ContentPlaceHolder_Text_hfError').val('')
                return;
            }
            if (value == "14") {
                alert('<%# WebLibs.DBCommon.GetLang(Resources.Lang.Product_Sold) %>');
                $('#ctl00_ContentPlaceHolder_Text_hfError').val('')
                return;
            }
            if (value == "15") {
                alert('Auction is Ended!');
                $('#ctl00_ContentPlaceHolder_Text_hfError').val('')
                return;
            }

            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }
    </script>
    <div id="Div1" class="title_1 ic11" runat="server">
        <%if (Request.QueryString["s"] == "1")
          {%>
        <% if (WebLibs.DBCommon.GetLang(Resources.Lang.OpenAuctionsList_Tooltip_Title) != "")
           { %>
        <samp class="question_tooltip" align="absmiddle" data-tooltip="OpenAuctionsList_Tooltip_Title">&nbsp;&nbsp;</samp>
        <% } %>
        <%# WebLibs.DBCommon.GetLang(Resources.Lang.OpenAuctionsList_Title) %>
        <%}
          else if (Request.QueryString["s"] == "2")
          {%>
        <% if (WebLibs.DBCommon.GetLang(Resources.Lang.EndedAuctionsList_Tooltip_Title) != "")
           { %>
        <samp class="question_tooltip" align="absmiddle" data-tooltip="EndedAuctionsList_Tooltip_Title">&nbsp;&nbsp;</samp>
        <% } %>
        <%# WebLibs.DBCommon.GetLang(Resources.Lang.EndedAuctionsList_Title) %>
        <% }
          else if (Request.QueryString["s"] == "3")
          { %>
        <% if (WebLibs.DBCommon.GetLang(Resources.Lang.SuspendedAuctionsList_Tooltip_Title) != "")
           { %>
        <samp class="question_tooltip" align="absmiddle" data-tooltip="SuspendedAuctionsList_Tooltip_Title">&nbsp;&nbsp;</samp>
        <% } %>
        <%# WebLibs.DBCommon.GetLang(Resources.Lang.SuspendedAuctionsList_Title) %>
        <% } %>
    </div>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="AjaxLoadingPanel1" runat="server"
        Width="100%" ClientEvents-OnRequestStart="OnRequestStart" ClientEvents-OnResponseEnd="OnResponseEnd">

        <asp:HiddenField ID="hfStatus" runat="server" />

        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr valign="top">
                <td>
                    <fieldset>
                        <legend class="text"><strong>Searching Info</strong></legend>
                        <table cellpadding="1" cellspacing="1" border="0" class="text">
                            <tr>
                                <td>Category:
                                </td>
                                <td>
                                    <asp:HiddenField ID="hddParentID" runat="server" Value="0" />
                                    <asp:TextBox ID="tbxParentName" runat="server" Text="All Category"></asp:TextBox>
                                </td>
                                <td>
                                    <img id="imgSelectParent" alt="Chọn chuyên mục cha" style="cursor: hand; cursor: pointer; border: 0px"
                                        onclick="ShowDialog(); return false;" src="<%# UrlRoot %>images/formSELECT.gif" />&nbsp;<asp:ImageButton
                                            ID="ibtnCategoryIdChange" runat="server" ImageUrl="~/images/blank.gif" OnClick="ibtnCategoryIdChange_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>ID #:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAuctionId" runat="server"></asp:TextBox>
                                </td>
                                <td><%--Auction Type:--%>
                                </td>
                                <td colspan="4">
                                    <asp:DropDownList ID="ddlAuctionType" runat="server" Visible="false">
                                        <asp:ListItem Value="0">All</asp:ListItem>
                                        <asp:ListItem Value="1">Buy Now Earned Reward</asp:ListItem>
                                        <asp:ListItem Value="2">Give Away</asp:ListItem>
                                        <asp:ListItem Value="3">Golden Time</asp:ListItem>
                                        <asp:ListItem Value="4">Minimum Price</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Keyword:
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="tbxKeyword" runat="server"></asp:TextBox>
                                </td>
                                <td style="display: none;">Status :
                                                <asp:DropDownList ID="drpStatusSearch" runat="server">
                                                    <asp:ListItem Value="1">Open</asp:ListItem>
                                                    <asp:ListItem Value="2">Closed</asp:ListItem>
                                                    <asp:ListItem Value="3">Suspended</asp:ListItem>
                                                </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td style="height: 10px"></td>
            </tr>
            <tr>
                <td align="right">
                    <table cellpadding="0" cellspacing="0" class="text">
                        <tr>
                            <td style="padding-right: 10px">
                                <strong>
                                    <asp:Literal ID="ltlTotal" runat="server"></asp:Literal></strong>
                            </td>
                            <td valign="middle">
                                <asp:DataList ID="dlPaper" runat="server" RepeatColumns="15" OnItemCreated="dlPaper_ItemCreated"
                                    OnItemDataBound="dlPaper_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtPage" runat="server" Text="1"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px">
                    <asp:HiddenField ID="hfError" runat="server" />
                    <asp:Button ID="btnDelete" Visible="false" runat="server" CssClass="button" Text="Remove"
                        OnClick="btnDelete_Click" />
                    <asp:Button ID="btnUpdateStatusIcon" Visible="false" runat="server" CssClass="button"
                        Text="Update Status/Icon Type" />
                </td>
            </tr>
            <tr valign="top">
                <td style="padding-left: 10px; padding-right: 10px">
                    <asp:Repeater ID="rptProduct" runat="server" OnItemDataBound="RptProductItemDataBound"
                        OnItemCreated="rptData_ItemCreated" OnItemCommand="rptProduct_ItemCommand">
                        <HeaderTemplate>
                            <table width="100%" border="0" cellspacing="1" cellpadding="1" style="background-color: #E8EDF6"
                                class="text">
                                <tr class="header" style="text-align: center;">
                                    <td align="center" style="width: 30px;">
                                        <asp:CheckBox ID="cbxHeaderSelect" runat="server" AutoPostBack="false" />
                                    </td>
                                    <td>
                                        <asp:Literal ID="ltlName" runat="server" Text="Title"></asp:Literal>
                                    </td>
                                    <td>Reservation Filled</td>
                                    <td>Item Details
                                    </td>
                                    <td id="tdSubspandTime" runat="server" style="display: none;">Suspended Time
                                    </td>
                                    <td align="center" style="width: 100px">
                                        <asp:Literal ID="ltlEdit" runat="server" Text="Action"></asp:Literal>
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="item" id="trItem" runat="server">
                                <td align="center" style="width: 30px;">
                                    <asp:CheckBox ID="cbxSelect" runat="server" />
                                    <asp:HiddenField ID="hiddenID" runat="server" />
                                </td>
                                <td style="padding-left: 10px;">
                                    <a href="#" target="_blank" id="hlTitle" runat="server" class="treebook">
                                        <%# Eval("ProductName")%>
                                    </a>
                                    <br />
                                    <asp:Literal ID="hlView" runat="server"></asp:Literal>
                                    <br />
                                    ID: #<%# Eval("Id") %></td>
                                <td><%# Eval("Filled")%>/<%# Eval("TotalSeat") %>: <%# (int)Eval("TotalSeat")==0?"0":string.Format("{0:n2}",(DBCommon.TryParseFloat(Eval("Filled").ToString())/(int)Eval("TotalSeat"))*100)  %>% </td>
                                <td>
                                    <%//# Eval("Price")%>
                                    <asp:Literal ID="lbStartTime" runat="server"></asp:Literal><br />
                                    <asp:Literal ID="lbEndTime" runat="server"></asp:Literal><br />
                                    <% if (WebLibs.DBSettingModule.Instance().checkPermission(WebLibs.Modules.AutoClone))
                                       { %>
                                    <b>Auto Clone: </b>
                                    <input type="checkbox" id="cbkClone" onclick='Clone(this,<%# Eval("Id")%>)'
                                        <%# fCheck(Eval("IsRepeate")) %>
                                        <%# Request.QueryString["s"]=="1"?"":"disabled" %> />
                                    <br />
                                    <%  } %>
                                    <b>Category:</b>&nbsp;<asp:Literal ID="lbCateName" runat="server"></asp:Literal>
                                    <br />
                                    <b>Type:</b>&nbsp;<%# GetAuctionType(Container.DataItem) %><br /><a class="treebook" href="javascript:MoreInfoAuction(<%# Eval("Id") %>)">More</a>
                                    <br />
                                    <a class="treebook" href="javascript:Seater(<%# Eval("Id") %>)">View User</a>
                                </td>
                                <td id="tdSubSpand" runat="server" style="display: none;">
                                    <asp:Literal ID="lbSubspandTime" runat="server"></asp:Literal>
                                </td>
                                <td style="text-align: left; padding-left: 20px; width: 200px;">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                    </table>
                                    <asp:LinkButton CssClass="treebook" ID="hlDelete" runat="server" Visible="false"
                                        Text="Delete"></asp:LinkButton><br />
                                    <asp:LinkButton CssClass="treebook" ID="hlViewWinner" runat="server" Visible="false"
                                        Text="View Winner"></asp:LinkButton><br />
                                    <asp:Literal ID="hlClone" runat="server" Visible="true" Text="Clone"></asp:Literal><br />
                                    <asp:LinkButton CssClass="treebook" ID="hlReactive" runat="server" Visible="false"
                                        Text="ReActive"></asp:LinkButton>
                                    <asp:LinkButton CssClass="treebook" ID="hlSuspended" runat="server" Visible="false"
                                        Text="Suspend"></asp:LinkButton><br />
                                    <asp:Literal ID="hlEdit" runat="server" Visible="false" Text="Edit"></asp:Literal>
                                    <br />
                                    <asp:LinkButton CssClass='treebook' ID="hlClosed" runat="server" CommandName="hlClosed" CommandArgument='<%#Eval("Id") %>' 
                                        Text="Cancel and Refund"></asp:LinkButton>
                                    <br />
                                    <font color="red"><b><%#fCheckRefunded(Eval("Id")) ? "Refunded":"" %></b></font>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td style="height: 10px"></td>
            </tr>
            <tr>
                <td align="right">
                    <table cellpadding="0" cellspacing="0" class="text">
                        <tr>
                            <td style="padding-right: 10px">
                                <strong>
                                    <asp:Literal ID="ltlTotal1" runat="server"></asp:Literal></strong>
                            </td>
                            <td valign="middle">
                                <asp:DataList ID="dlPaper1" runat="server" RepeatColumns="15" OnItemCreated="dlPaper_ItemCreated"
                                    OnItemDataBound="dlPaper_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtPage" runat="server" Text="1"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
        <div id="dvLoading" style="display:none"></div>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel1" runat="server" Transparency="10"
        MinDisplayTime="300">
        <img src="<%# UrlRoot %>images/loading.gif" alt="Loading" style="border: 0px; vertical-align: middle;" />
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="Singleton" runat="server">
        <Windows>
            <telerik:RadWindow ID="DialogWindow" OnClientShow="OnClientShow" OnClientClose="CallBackFunction" Behaviors="Close"
                Top="22" Modal="true" runat="server">
                <%--OpenerElementID="imgSelectParent" OffsetElementID="imgSelectParent"--%>
            </telerik:RadWindow>
            <telerik:RadWindow OnClientShow="OnClientShow" OnClientClose="CallBackCloneFunction" ID="CloneWindow" Behaviors="Close"
                Top="22" Modal="true" runat="server">
            </telerik:RadWindow>
            <telerik:RadWindow ID="WinnerWindow" Behaviors="Close" Top="22" Modal="true"
                runat="server" Width="330px">
            </telerik:RadWindow>
            <telerik:RadWindow ID="MoreInfoAuction" Behaviors="Close" Top="22" Modal="true"
                runat="server" Width="330px">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <script type="text/javascript">
        function OnRequestStart(sender, args) {
            if (args.EventTarget.indexOf("btnDelete") >= 0) {
                return ConfirmDelete('cbxSelect');
            }
        }
        function confirmClose(pid) {
            if (confirm("Do you really want to close this item?")) {
                sendmessage(pid, "closed");
                return true;
            }
            return false;
        }
        function sendmessage(pid, mode, ShowMessage) {
            var a = "";
            $.ajax({
                type: "GET",
                cache: false,
                url: '<%# UrlRoot %>handlers/sendmessage.ashx?pid=' + pid + '&mode=' + mode,
                processData: false,
                dataType: "text",
                timeout: 60000,
                success: function () {
                    if (ShowMessage) {
                        $('#dvLoading').attr("style", "display:none");
                        alert('Send completed.');
                    }
                },
                error: function () {
                    $('#dvLoading').attr("style", "display:none");
                    alert('Sent failed.');                    
                }
            });
            if (ShowMessage) {
                $('#dvLoading').attr("style", " ");
            }
            return false;
        }
        function Seater(id) {
            var sUrl = "<%# UrlRoot %>common/viewseater.aspx?id=" + id;
            var oWnd = window.radopen(sUrl, "CloneWindow");
            oWnd.SetSize(400, 400);
            oWnd.SetUrl(oWnd.GetUrl());
        }
        function SelectAll(objClick, objRelated) {
            var obj = document.getElementById(objClick);
            var chk = obj.checked;
            var len = theForm.elements.length;
            for (var j = 0; j < len; j++) {
                var e = theForm.elements[j];
                if (e.name.indexOf(objRelated) >= 0 && e.disabled == false) {
                    e.checked = chk;
                }
            }
        }
        function CheckSelected(objRelated) {
            var bRet = false;
            var len = theForm.elements.length;
            for (var j = 0; j < len; j++) {
                var e = theForm.elements[j];
                if (e.name.indexOf(objRelated) >= 0 && e.checked) {
                    bRet = true;
                    break;
                }
            }
            if (!bRet) {
                alert("Please select a record to delete!");
            }
            return bRet;
        }
        function ConfirmDelete(objRelated) {
            if (CheckSelected(objRelated)) {
                return confirm("Do you realy want to delete?");
            }
            return false;
        }
        //Rad Window
        function HistoryPreview(Id) {
            var sUrl = "<%# UrlRoot %>view_winner.aspx?Id=" + Id;
            var oWnd = window.radopen(sUrl, "WinnerWindow");
            oWnd.SetSize(750, 500);
            oWnd.Center();
            oWnd.SetUrl(oWnd.GetUrl());
            return false;
        }
        function ShowDialog() {
            //var sUrl = "<%# UrlRoot %>common/select_cate_product.aspx";
            var sUrl = "<%# UrlRoot %>common/select_cate.aspx?AppCode=<%# _AppCode %>";
            var oWnd = radopen(sUrl, "DialogWindow");
            oWnd.SetSize(330, 400);
            oWnd.SetUrl(oWnd.GetUrl());
        }
        function CloneAuction(id) {
            var sUrl = "<%# UrlRoot %>common/clone.aspx?id=" + id;
            var oWnd = window.radopen(sUrl, "CloneWindow");
            oWnd.SetSize(630, 400);
            oWnd.SetUrl(oWnd.GetUrl());
        }
        function MoreInfoAuction(id) {
            var sUrl = "<%# UrlRoot %>common/moreinfo.aspx?id=" + id;
            var oWnd = window.radopen(sUrl, "MoreInfoAuction");
            oWnd.SetSize(750, 500);
            oWnd.Center();
            oWnd.SetUrl(oWnd.GetUrl());
        }
        function OnClientShow(radWindow) {

        }
        function OnResponseEnd(sender, args) {

            ActualResize('tblContent');
        }
        function CallBackCloneFunction(radWindow, args) {
            if (args != null) {
                var status = document.getElementById("<%# hfStatus.ClientID %>").value;
                if (status != "1") {
                    //window.location.href = "<%# UrlRoot %>product/view/1/index.htm";
                }
                else {
                    document.getElementById("<%# ibtnCategoryIdChange.ClientID %>").click();
                }
            }
        }
        function CallBackFunction(radWindow, args) {
            document.getElementById("<%# tbxParentName.ClientID %>").value = args._argument.Name;
            document.getElementById("<%# hddParentID.ClientID %>").value = args._argument.ID;
            document.getElementById("<%# ibtnCategoryIdChange.ClientID %>").click();

        }
        function OnClientClose(radWindow) {
        }
        var theForm = document.forms[0];
        function Clone(obj,id) {
            if (confirm('Are you sure?')) {
                $.ajax({
                    type: "GET",
                    cache: false,
                    url: '<%# UrlRoot %>handlers/ActionHandler.ashx?id=' + id + '&clone=' + obj.checked,
                    processData: false,
                    dataType: "text",
                    timeout: 60000,
                    success: function() {
                        $(function() {
                            $("#dialog").dialog();
                        });
                    }
                });
                return false;
            } 
            else obj.checked = !obj.checked;
            return true;
        }
    </script>

    <div id="dialog" style="display: none">
        Update successfull!
    </div>
</asp:Content>