<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="contract.aspx.cs" Inherits="MaricoPay.contract" %>

<%@ Register Src="~/uc/ucComboDepartment.ascx" TagName="department" TagPrefix="uc5" %>
<%@ Register Src="~/uc/ucCurr.ascx" TagName="Curr" TagPrefix="uc3" %>
<%@ Register Src="~/uc/ucVendor.ascx" TagName="vendor" TagPrefix="uc2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">
        function DoPostBack(obj) {
            __doPostBack(obj.id, 'OtherInformation');
        }
        function pageLoad() {
           

            $("#<%= btSave.ClientID %>").click(function () {
                $.blockUI({
                    message: '<h1>Xin vui lòng chờ xử lý</h1>',
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#f00',
                        opacity: .5,
                        color: '#fff'
                    }
                });
            });

           
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                $.unblockUI();
            }
          
        }
    </script>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" ChildrenAsTriggers="true"
        runat="server">
        <contenttemplate>
            <%-- <telerik:RadNotification ID="RadNotification1" runat="server" AutoCloseDelay="0"
                Height="35px" Position="BottomRight" ShowCloseButton="False" VisibleOnPageLoad="True"
                VisibleTitlebar="False" Width="35px" BackColor="Transparent">
                <ContentTemplate>
                    <center>
                        <asp:Label ID="lbLoi" runat="server"></asp:Label>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <asp:Image ID="Image1" ImageUrl="~/images/process.gif" runat="server" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </center>
                </ContentTemplate>
            </telerik:RadNotification>--%>
            <div>
                <uc4:uscMsgBox ID="MsgBox1" runat="server" />
                <table width="100%">
                    <tr>
                        <td align="center" style="font-size: large; color: Black; font-weight: bold;">
                            <asp:Label ID="lbTitle" runat="server" Text="" Font-Size="Larger" Font-Bold="True"></asp:Label>
                            <asp:HiddenField ID="hdFlagg" runat="server" />
                            <asp:HiddenField ID="hdFilename" runat="server" />
                            <asp:HiddenField ID="hdType" runat="server" />
                            <asp:HiddenField ID="hdEmailCreate" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:Button ID="btExpand1" runat="server" Text="-" OnClick="btExpand1_Click" Font-Names="Arial"
                    Font-Size="Small" ToolTip="Collapse" />
            </div>
            <div style="width: 100%;">
                <div id="dvParent" runat="server" style="float: left; width: 100%">
                    <table cellpadding="5px" style="font-family: @Arial Unicode MS; font-size: small;">
                        <tr>
                            <td>
                                Contract number
                            </td>
                            <td>
                                <table cellpadding="0px" style="font-family: @Arial Unicode MS; font-size: small;">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtConctNoFinal" Enabled="true" MaxLength="50" runat="server" Width="110px"
                                                Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbStatus" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0000CC"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkHopDongMau" Text="Standard contract" Checked="false" runat="server" Visible="true" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                Contract date
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="radDateContract" runat="server" Font-Names="Arial" Font-Size="Small"
                                    Width="150px">
                                    <Calendar runat="server" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput runat="server" DateFormat="dd-MMM-yy" DisplayDateFormat="dd-MMM-yy" LabelWidth="40%">
                                        <EmptyMessageStyle Resize="None" />
                                    </DateInput>
                                <%--    <DatePopupButton HoverImageUrl="" ImageUrl="" Enabled="true" />--%>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                Contract type
                            </td>
                            <td>
                                <asp:DropDownList ID="dropContractType" runat="server" DataTextField="Description"
                                    DataValueField="Type_PK" Width="190px" Font-Names="Arial" Font-Size="Small">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Department
                            </td>
                            <td>
                                <asp:DropDownList ID="dropOrg" runat="server" Width="190px" DataValueField="Org_PK"
                                    DataTextField="Description" Font-Names="Arial" Font-Size="Small" AutoPostBack="True"
                                    OnSelectedIndexChanged="dropOrg_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <a href="vendor.aspx" onclick="window.open(this.href, 'mywin','left=300,top=300,width=500,height=300,toolbar=1,resizable=0'); return false;">
                                    Vendor</a>
                            </td>
                            <td>
                                <%-- <uc2:vendor ID="vendor1" runat="server" />--%>
                                <telerik:RadComboBox ID="radcmbVendor" runat="server" Width="190px" 
                                    DataTextField="VendorName" DropDownWidth="350px"
                                    DataValueField="VendorCode" Filter="Contains" Font-Size="Small" AutoPostBack="False"
                                    Font-Names="Arial"  MarkFirstMatch="true" AllowCustomText="True">
                                    <ItemTemplate>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr valign="middle">
                                                <td style="width: 80%; text-align: left;">
                                                    <div style="font-size: 11;">
                                                        <asp:Label ID="lbVendorName" runat="server" Text='<%# Eval("VendorName")%>'></asp:Label>
                                                    </div>
                                                </td>
                                                <td style="width: 20%; text-align: right;">
                                                    <div style="font-size: 11;">
                                                        <asp:Label ID="lbVendorCode" runat="server" Text='<%# Eval("VendorCode")%>'></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                Review by Legal
                            </td>
                            <td>
                                <asp:DropDownList ID="dropLegal" runat="server" Width="190px" DataValueField="Email"
                                    DataTextField="Description" Font-Names="Arial" Font-Size="Small">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Signature&nbsp; date
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="radDateApp" runat="server" Font-Names="Arial" Font-Size="Small"
                                    Width="150px">
                                    <Calendar ID="Calendar1" runat="server" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput1" runat="server" DateFormat="dd-MMM-yy" DisplayDateFormat="dd-MMM-yy"
                                        LabelWidth="40%">
                                        <EmptyMessageStyle Resize="None" />
                                    </DateInput>
                                 <%--   <DatePopupButton HoverImageUrl="" ImageUrl="" />--%>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                Expire date
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="raddateExpiry" runat="server" Font-Names="Arial" Font-Size="Small"
                                    Width="150px">
                                    <Calendar ID="Calendar2" runat="server" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput2" runat="server" DateFormat="dd-MMM-yy" DisplayDateFormat="dd-MMM-yy"
                                        LabelWidth="40%">
                                        <EmptyMessageStyle Resize="None" />
                                    </DateInput>
                                 <%--   <DatePopupButton HoverImageUrl="" ImageUrl="" />--%>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                Review by Finance
                            </td>
                            <td>
                                <asp:DropDownList ID="dropFinance" runat="server" Width="190px" DataValueField="Email"
                                    DataTextField="Description" Font-Names="Arial" Font-Size="Small">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contract value
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <telerik:RadNumericTextBox ID="radnumContractValue" runat="server" Width="120px"
                                                ReadOnly="False" Value="0" Font-Names="Arial" Font-Size="Small" MinValue="0">
                                                <NegativeStyle Resize="None"></NegativeStyle>
                                                <NumberFormat ZeroPattern="n" DecimalDigits="2"></NumberFormat>
                                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                <FocusedStyle Resize="None"></FocusedStyle>
                                                <DisabledStyle Resize="None"></DisabledStyle>
                                                <InvalidStyle Resize="None"></InvalidStyle>
                                                <HoveredStyle Resize="None"></HoveredStyle>
                                                <EnabledStyle Resize="None"></EnabledStyle>
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <uc3:Curr ID="Curr1" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                Signature by
                            </td>
                            <td>
                                <asp:TextBox ID="txtAppby" runat="server" Width="190px" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                            </td>
                            <td>
                                Upload contract file
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" accept=".doc, .docx, .pdf, .zip" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Brief content
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtContent" runat="server" Width="100%" Font-Names="Arial" Font-Size="Small"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="float: left; top: 200px; width: 800px;">
                    <table cellpadding="0px" style="font-family: @Arial Unicode MS; font-size: small;">
                        <tr>
                            <td>
                                ID Number
                            </td>
                            <td>
                                <asp:TextBox ID="txtContractNo" runat="server" Enabled="false" Font-Names="Arial"
                                    Font-Size="Small" Visible="True" Width="110px"></asp:TextBox>
                            </td>
                            <td>
                                IO
                            </td>
                            <td>
                                <telerik:RadComboBox ID="radcomboASP" runat="server" Width="400px" EnableLoadOnDemand="True"
                                    HighlightTemplatedItems="True" DataTextField="Objective" DropDownWidth="450px"
                                    DataValueField="ASPNo" Filter="Contains" Font-Size="Small" BorderColor="Transparent"
                                    BackColor="White" ResolvedRenderMode="Classic" AllowCustomText="True" AutoPostBack="True"
                                    Font-Names="Arial">
                                    <ItemTemplate>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr valign="middle">
                                                <td style="width: 80%; text-align: left;">
                                                    <div style="font-size: 11;">
                                                        <asp:Label ID="lbVendorName" runat="server" Text='<%# Eval("Objective")%>'></asp:Label>
                                                    </div>
                                                </td>
                                                <td style="width: 20%; text-align: right;">
                                                    <div style="font-size: 11;">
                                                        <asp:Label ID="lbVendorCode" runat="server" Text='<%# Eval("ASPNo")%>'></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="RadGrid2" Width="100%" runat="server" Font-Names="Arial" Font-Size="Small">
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
            <div id="tblChange" runat="server" style="width: 75%;">
                <table cellpadding="5px" style="font-family: @Arial Unicode MS; font-size: small;"
                    width="100%">
                    <tr>
                        <td>
                            Review by Finance
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFinanceChange" runat="server" Width="190px" DataValueField="Email"
                                DataTextField="Description" Font-Names="Arial" Font-Size="Small">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btSentFinance" runat="server" Text="Sent" ToolTip="Thay đổi người review"
                                OnClick="btSentFinance_Click" />
                        </td>
                        <td>
                            Review by Legal
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLegalChange" runat="server" Width="190px" DataValueField="Email"
                                DataTextField="Description" Font-Names="Arial" Font-Size="Small">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btSentLegal" runat="server" Text="Sent" ToolTip="Thay đổi người review"
                                OnClick="btSentLegal_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 75%;">
                <table cellpadding="5px" style="font-family: @Arial Unicode MS; font-size: small;"
                    width="100%">
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkDighitalsised" runat="server" Text="Digitalized" />
                        </td>
                        <td>
                            <table cellpadding="0px" style="font-family: @Arial Unicode MS; font-size: small;">
                                <tr>
                                    <td>
                                        Renewal version
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="radnumRenewal" runat="server" Width="90px" ReadOnly="False"
                                            Value="0" Font-Names="Arial" Font-Size="Small" MinValue="0">
                                            <NegativeStyle Resize="None"></NegativeStyle>
                                            <NumberFormat ZeroPattern="n" DecimalDigits="0"></NumberFormat>
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                            <FocusedStyle Resize="None"></FocusedStyle>
                                            <DisabledStyle Resize="None"></DisabledStyle>
                                            <InvalidStyle Resize="None"></InvalidStyle>
                                            <HoveredStyle Resize="None"></HoveredStyle>
                                            <EnabledStyle Resize="None"></EnabledStyle>
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        Modified version
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="radnumAmendment" runat="server" Width="90px" ReadOnly="False"
                                            Value="0" Font-Names="Arial" Font-Size="Small" MinValue="0">
                                            <NegativeStyle Resize="None"></NegativeStyle>
                                            <NumberFormat ZeroPattern="n" DecimalDigits="0"></NumberFormat>
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                            <FocusedStyle Resize="None"></FocusedStyle>
                                            <DisabledStyle Resize="None"></DisabledStyle>
                                            <InvalidStyle Resize="None"></InvalidStyle>
                                            <HoveredStyle Resize="None"></HoveredStyle>
                                            <EnabledStyle Resize="None"></EnabledStyle>
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            Storage location
                        </td>
                        <td>
                            <asp:TextBox ID="txtStorega" runat="server" Width="100%" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Remark
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtAppnote" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table cellpadding="5px" style="font-family: @Arial Unicode MS; font-size: small;">
                    <tr>
                        <td>
                            <asp:Button ID="btSave" runat="server" ToolTip="Save contract" Text="Save" OnClick="btSave_Click"
                                Font-Names="Arial" Font-Size="Small" />
                            <asp:Button ID="btSaveArchive" runat="server" ToolTip="Save Archive" Text="Save"
                                OnClick="btSaveArchive_Click" Font-Names="Arial" Font-Size="Small" />
                        </td>
                        <td>
                            <asp:Button ID="btCancel" runat="server" Text="Cancel" OnClick="btCancel_Click" Font-Names="Arial"
                                Font-Size="Small" />
                        </td>
                        <td>
                            <asp:Button ID="btSubmit" runat="server" Text="Save & Submit" OnClick="btSubmit_Click"
                                Font-Names="Arial" Font-Size="Small" Visible="False" />
                        </td>
                        <td>
                            <asp:Button ID="btCreate" runat="server" Text="Create" OnClick="btCreate_Click" Font-Names="Arial"
                                Font-Size="Small" />
                        </td>
                        <td>
                            &nbsp;<asp:Button ID="btFinal" runat="server" Font-Names="Arial" Font-Size="Small"
                                OnClick="btFinal_Click" Text="Final" ToolTip="Chi de luu hop dong final ko qua ky duyet tren he thong"
                                Visible="False" />
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                            <asp:Button ID="btRefresh" runat="server" Text="Refresh" OnClick="btRefresh_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </contenttemplate>
        <triggers>
            <asp:PostBackTrigger ControlID="btSave" />
            <asp:PostBackTrigger ControlID="btSaveArchive" />
            <asp:PostBackTrigger ControlID="btSubmit" />
            <asp:PostBackTrigger ControlID="RadGrid1" />
            <asp:PostBackTrigger ControlID="btSentFinance" />
            <asp:PostBackTrigger ControlID="btSentLegal" />
             <asp:PostBackTrigger ControlID="radcmbVendor" />
        </triggers>
    </asp:UpdatePanel>
    <div>
        <telerik:radgrid id="RadGrid1" runat="server" allowsorting="True" autogeneratecolumns="False"
            showfooter="True" ondeletecommand="RadGrid1_DeleteCommand" allowfilteringbycolumn="True"
            onitemcommand="RadGrid1_ItemCommand" oneditcommand="RadGrid1_EditCommand" onselectedindexchanged="RadGrid1_SelectedIndexChanged"
            font-names="Arial" font-size="Small">
            <GroupingSettings CaseSensitive="False" />
            <MasterTableView EnableHeaderContextMenu="true" AllowFilteringByColumn="True">
                <CommandItemSettings ExportToPdfText="Export to PDF" />
                <%-- <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                </RowIndicatorColumn>--%>
                <Columns>
                    <telerik:GridTemplateColumn HeaderText="No." UniqueName="SoTT" DataField="STT" AllowFiltering="False">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                        <ItemTemplate>
                            <%# Container.DataSetIndex  + 1%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn CommandName="Edit" Text="Edit" ButtonType="ImageButton"
                        HeaderText="Edit" UniqueName="EditColumn">
                    </telerik:GridButtonColumn>
                    <telerik:GridButtonColumn CommandName="Delete" Text="Delete" ButtonType="ImageButton"
                        HeaderText="Delete" UniqueName="DeleteColumn" ConfirmDialogType="RadWindow" ConfirmText="Are you sure you want to delete this record?"
                        ConfirmTitle="Delete">
                    </telerik:GridButtonColumn>
                    <telerik:GridTemplateColumn DataField="statusUser" HeaderText="Action" AllowFiltering="false"
                        UniqueName="ActionColumn">
                        <ItemTemplate>
                            <asp:Button ID="btSubmitGrid" CommandName="Submit" Width="60px" runat="server" Text="Submit"
                                ToolTip="Send to review" Visible='<%# isShowSubmit(Eval ("statusUser")) %>' />
                            <asp:Button ID="btPrintAdviceNote" CommandName="PrintAdviceNote" Width="60px" runat="server"
                                Text="Print" ToolTip="Print Contract Advice Note" Visible='<%# isShowPrintAdvice(Eval ("IsClosed")) %>' />
                            <asp:Button ID="btFinal" CommandName="Finalise" Width="60px" runat="server" Text="Finalise"
                                ToolTip="Close contract process" Visible='<%# isShowfinal(Eval ("status"),Eval ("IsClosed")) %>' /><br />
                            <asp:FileUpload ID="fuFinal" Width="65px" runat="server" Style="color: transparent;
                                direction: ltr;" Visible='<%# isShowfinal(Eval ("status"),Eval ("IsClosed")) %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Action" AllowFiltering="false" UniqueName="SentLegal">
                        <ItemTemplate>
                            <asp:Button ID="btRejectGrid" Width="60px" CommandName="Reject" runat="server" Text="Review"
                                ToolTip="Khi review xong nhưng cần chỉnh sửa (reject)" Visible='<%# isShowApprove_Reject(Eval ("statusUser")) %>' />
                            <asp:Button ID="btApprove" CommandName="Approve" Width="60px" runat="server" Text="Finalise"
                                Visible='<%# isShowApprove_Reject(Eval ("statusUser")) %>' />
                            <%-- <asp:Button ID="btSentLegal" CommandName="SentLegal" runat="server" Width="60px"
                                Text="To Legal" Visible='<%# isShowSubmitLegal(Eval ("statusUser")) %>' />--%>
                            <%--  <asp:Button ID="btClosed" Width="60px" CommandName="Closed" runat="server" Text="Close"
                                Visible='<%# isShowClosed(Eval ("status"),Eval ("statusUser")) %>' />--%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn Display="true" HeaderText="Contract No." DataField="ContractNo"
                        CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" FilterControlWidth="100%"
                        UniqueName="ContractNo" EmptyDataText="" SortExpression="ContractNo" ShowFilterIcon="false">
                        <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="80px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Status" DataField="statustext" UniqueName="statustext"
                        EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        FilterControlWidth="100%" ShowFilterIcon="false">
                        <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="40px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="IsHDMau" DataField="IsHDMau" UniqueName="IsHDMau"
                        EmptyDataText="" CurrentFilterFunction="Contains" AllowFiltering="false" AutoPostBackOnFilter="false"
                        FilterControlWidth="100%" ShowFilterIcon="false">
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="30px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="ViewFile" DataField="AttachedFileView" UniqueName="AttachedFileView"
                        EmptyDataText="" Display="true" AllowFiltering="false">
                        <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="50px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Trạng thái Final" DataField="status" UniqueName="status"
                        EmptyDataText="" Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Trạng thái User" DataField="statusUser" UniqueName="statusUser"
                        EmptyDataText="" Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="ContractDate" UniqueName="ContractDate" HeaderText="ContractDate"
                        FilterControlWidth="60px" SortExpression="ContractDate" PickerType="DatePicker"
                        EnableTimeIndependentFiltering="true" Display="true" DataFormatString="{0:dd-MMM-yyyy}">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn HeaderText="Type" DataField="ContractType" UniqueName="ContractType"
                        Display="true" EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        FilterControlWidth="100%" SortExpression="ContractType" ShowFilterIcon="false">
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="30px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Type" DataField="ContractTypeName" UniqueName="ContractTypeName"
                        EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        Display="false" FilterControlWidth="100%" SortExpression="ContractTypeName" ShowFilterIcon="false">
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="30px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Dept" DataField="Department" UniqueName="Department"
                        Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Dept" DataField="DepartmentName" UniqueName="DepartmentName"
                        EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        Display="false" FilterControlWidth="100%" SortExpression="DepartmentName" ShowFilterIcon="false">
                        <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="40px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Dept" DataField="KyHieu" UniqueName="KyHieu"
                        EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        Display="true" FilterControlWidth="100%" SortExpression="KyHieu" ShowFilterIcon="false">
                        <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="50px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="ContractContent" DataField="ContractContent"
                        UniqueName="ContractContent" EmptyDataText="" CurrentFilterFunction="Contains"
                        AutoPostBackOnFilter="true" Display="true" FilterControlWidth="100%" SortExpression="ContractContent"
                        ShowFilterIcon="false">
                        <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="120px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Vendor" DataField="Vendor" UniqueName="Vendor"
                        Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Vendor" DataField="VendorName" UniqueName="VendorName"
                        EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        Display="false" FilterControlWidth="100%" SortExpression="VendorName" ShowFilterIcon="false">
                        <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="80px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="ExpiryDate" UniqueName="ExpiryDate" HeaderText="ExpiryDate"
                        FilterControlWidth="60px" SortExpression="ExpiryDate" PickerType="DatePicker"
                        EnableTimeIndependentFiltering="true" Display="true" DataFormatString="{0:dd-MMM-yyyy}">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn UniqueName="ContractValue" DataField="ContractValue" HeaderText="ContractValue"
                        DataFormatString="{0:###,###.##}" Aggregate="Sum" FooterText="Total: " DataType="System.Decimal"
                        EmptyDataText="0" FilterControlWidth="40px">
                        <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                        <FooterStyle Width="70px" HorizontalAlign="Center" />
                        <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="IO No" DataField="ASPNo" UniqueName="ASPNo"
                        EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        Display="true" FilterControlWidth="100%" SortExpression="ASPNo" ShowFilterIcon="false">
                        <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="80px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="IO No" DataField="ASPNoHide" UniqueName="ASPNoHide"
                        EmptyDataText="" CurrentFilterFunction="Contains" Display="false" ShowFilterIcon="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Unit" DataField="UnitPrice" UniqueName="UnitPrice"
                        EmptyDataText="" AllowFiltering="false" Display="true" SortExpression="UnitPrice"
                        ShowFilterIcon="false">
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="30px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Renewal" DataType="System.Int16" DataField="Renewal"
                        UniqueName="Renewal" Display="false" ShowFilterIcon="false" AllowFiltering="false">
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <FooterStyle Width="30px" HorizontalAlign="Center" />
                        <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="RenewDate" UniqueName="RenewDate" HeaderText="RenewDate"
                        FilterControlWidth="60%" SortExpression="RenewDate" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                        Display="false" DataFormatString="{0:dd-MMM-yyyy}">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn HeaderText="Amendment" DataType="System.Int16" DataField="Amendment"
                        UniqueName="Amendment" Display="false" ShowFilterIcon="false" AllowFiltering="false">
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <FooterStyle Width="30px" HorizontalAlign="Center" />
                        <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="AmendmentDate" UniqueName="AmendmentDate"
                        HeaderText="AmendmentDate" FilterControlWidth="60%" SortExpression="AmendmentDate"
                        PickerType="DatePicker" EnableTimeIndependentFiltering="true" Display="false"
                        DataFormatString="{0:dd-MMM-yyyy}">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn HeaderText="Dighitalsised" DataType="System.Boolean" DataField="Dighitalsised"
                        UniqueName="Dighitalsised" Display="false" ShowFilterIcon="false" AllowFiltering="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="HardcopyStored" DataField="HardcopyStored" UniqueName="HardcopyStored"
                        EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        Display="false" FilterControlWidth="100%" SortExpression="HardcopyStored" ShowFilterIcon="false">
                        <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="80px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Approveby" DataField="Approveby" UniqueName="Approveby"
                        EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        Display="false" FilterControlWidth="100%" SortExpression="Approveby" ShowFilterIcon="false">
                        <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="80px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="ApproveDate" UniqueName="ApproveDate" HeaderText="ApproveDate"
                        FilterControlWidth="60%" SortExpression="ApproveDate" PickerType="DatePicker"
                        EnableTimeIndependentFiltering="true" Display="false" DataFormatString="{0:dd-MMM-yyyy}">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn DataField="CreateDate" UniqueName="CreateDate" HeaderText="CreateDate"
                        FilterControlWidth="60%" SortExpression="CreateDate" PickerType="DatePicker"
                        EnableTimeIndependentFiltering="true" Display="false" DataFormatString="{0:dd-MMM-yyyy}">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn HeaderText="IsClosed" DataField="IsClosed" UniqueName="IsClosed"
                        Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Email" DataField="Email" UniqueName="Email"
                        Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Display="true" HeaderText="Created by" DataField="UserCreate"
                        UniqueName="UserCreate" EmptyDataText="">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Attachedfile" DataField="Attachedfile" UniqueName="Attachedfile"
                        Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Remark" DataField="Appnote" UniqueName="Appnote"
                        EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        Display="true" FilterControlWidth="100%" SortExpression="Appnote" ShowFilterIcon="false">
                        <HeaderStyle Width="130px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="130px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn DataField="MyNote" HeaderText="My Note" AllowFiltering="false"
                        UniqueName="AppnoteLegal_Finance">
                        <ItemTemplate>
                            <asp:TextBox ID="txtYKienGrid" runat="server" Text='<%# Eval ("MyNote") %>' MaxLength="500"
                                TextMode="MultiLine" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                            <asp:FileUpload ID="FileUpload2" runat="server" accept=".doc, .docx, .pdf, .zip" />
                        </ItemTemplate>
                        <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="200px"></ItemStyle>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="ContractNoLegal" HeaderText="ContractNoFinal"
                        AllowFiltering="true" UniqueName="ContractNoLegal">
                        <ItemTemplate>
                            <asp:TextBox ID="txtContractNoFinal" runat="server" Text='<%# Eval ("ContractNoLegal") %>'
                                MaxLength="50" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="200px"></ItemStyle>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn HeaderText="FinanceReview" DataField="FinanceReview" UniqueName="FinanceReview"
                        Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="LegalReview" DataField="LegalReview" UniqueName="LegalReview"
                        Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="vnd" DataField="CurrencyDescription" UniqueName="CurrencyDescription"
                        EmptyDataText="" Display="false">
                    </telerik:GridBoundColumn>
                </Columns>
                <EditFormSettings EditFormType="Template">
                    <EditColumn UniqueName="EditCommandColumn1">
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
            <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true"
                EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="True" />
                <%--<Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="8">
                    </Scrolling>
                    <Resizing AllowColumnResize="true" EnableRealTimeResize="true" />--%>
            </ClientSettings>
        </telerik:radgrid>
    </div>
</asp:Content>
