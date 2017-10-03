<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DocDefault.aspx.cs" Inherits="MaricoPay.DocDefault" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<%@ Register Src="~/uc/ucVendor.ascx" TagName="ucVendor" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">
        function DoPostBack(obj) {
            __doPostBack(obj.id, 'OtherInformation');
        }
   
    </script>
    <%--<script type="text/javascript">
        function pageLoad() {
            $("#<%= btSave.ClientID %>").click(function () {
                $.blockUI({
                    message: '<h1>Please waiting Save</h1>',
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#f00',
                        opacity: .5,
                        color: '#fff'
                    }
                });
            });
            $("#<%= btSubmit.ClientID %>").click(function () {
                $.blockUI({
                    message: '<h1>Please wait Save and Submit</h1>',
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#f00',
                        opacity: .5,
                        color: '#fff'
                    }
                });
            });
            $("#<%= RadGrid1.ClientID %>").change(function () {
                $.blockUI({
                    message: '<h1>Please wait</h1>',
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
       
       
    </script>--%>
  <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btSave" />
            <asp:PostBackTrigger ControlID="btSubmit" />
            <asp:PostBackTrigger ControlID="radioType" />
        </Triggers>
        <ContentTemplate>
            <telerik:RadNotification ID="RadNotification1" runat="server" AutoCloseDelay="0"
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
            </telerik:RadNotification>
            <div>
                <uc4:uscMsgBox ID="MsgBox1" runat="server" />
                <table width="100%">
                    <tr>
                        <td align="center" style="font-size: large; color: Black; font-weight: bold;">
                            <asp:Label ID="lbTitle" runat="server" Text="" Font-Size="Larger" Font-Bold="True"></asp:Label>
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
                                <asp:Label ID="lbDocno" runat="server" Text="Số chứng từ"></asp:Label>
                                <br />
                                System Doc. ID
                            </td>
                            <td>
                                <asp:TextBox ID="txtDocno" runat="server" Enabled="False" Width="190px" Font-Names="Arial"
                                    Font-Size="Small"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lbStatus" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0000CC"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="radioType" runat="server" OnSelectedIndexChanged="radioType_SelectedIndexChanged"
                                    RepeatDirection="Horizontal" AutoPostBack="True">
                                    <asp:ListItem Selected="True" Value="Gov">Government</asp:ListItem>
                                    <asp:ListItem Value="Biz">Business</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:HiddenField ID="hdFlagg" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="hdType" runat="server" />
                                <asp:HiddenField ID="hdFilename" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a id="openpp" runat="server" href="org.aspx" onclick="window.open(this.href, 'mywin','left=300,top=300,width=500,height=300,toolbar=1,resizable=0'); return false;">
                                    <asp:Label ID="lbOrg" runat="server" Text=""></asp:Label>
                                </a>
                            </td>
                            <td>
                                <asp:DropDownList ID="dropOrg" runat="server" Width="190px" DataValueField="Org_PK"
                                    DataTextField="Description" AutoPostBack="true" OnSelectedIndexChanged="dropOrg_SelectedIndexChanged"
                                    Font-Names="Arial" Font-Size="Small">
                                </asp:DropDownList>
                                <uc5:ucVendor ID="ucVendor1" runat="server" Visible="False" />
                            </td>
                            <td>
                                Loại công văn<br />
                                Type
                            </td>
                            <td>
                                <asp:DropDownList ID="dropType" runat="server" Width="190px" DataValueField="DocType_PK"
                                    DataTextField="Description" Font-Names="Arial" Font-Size="Small">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lbKyHieu" runat="server" Text="Số công văn<br />Document number"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtKyHieu" runat="server" Width="190px" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Ngày ký CV<br />
                                Released date
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="radDateCV" runat="server" Enabled="True" Culture="en-US"
                                    Width="190px" Font-Names="Arial" Font-Size="Small">
                                    <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                        EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;">
                                    </Calendar>
                                    <DateInput runat="server" DisplayDateFormat="dd-MMM-yy" DateFormat="M/d/yyyy" LabelWidth="40%">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                        <FocusedStyle Resize="None"></FocusedStyle>
                                        <DisabledStyle Resize="None"></DisabledStyle>
                                        <InvalidStyle Resize="None"></InvalidStyle>
                                        <HoveredStyle Resize="None"></HoveredStyle>
                                        <EnabledStyle Resize="None"></EnabledStyle>
                                    </DateInput>
                                   <%-- <DatePopupButton CssClass="rcCalPopup" ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                Người ký-chức vụ<br />
                                Signed by - Title
                            </td>
                            <td>
                                <asp:TextBox ID="txtNguoiKy" runat="server" Width="190px" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lbNgayNhan" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="radDateNhan" runat="server" Enabled="True" Culture="en-US"
                                    Width="190px" Font-Names="Arial" Font-Size="Small">
                                    <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                        EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;">
                                    </Calendar>
                                    <DateInput runat="server" DisplayDateFormat="dd-MMM-yy" DateFormat="M/d/yyyy" LabelWidth="40%">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                        <FocusedStyle Resize="None"></FocusedStyle>
                                        <DisabledStyle Resize="None"></DisabledStyle>
                                        <InvalidStyle Resize="None"></InvalidStyle>
                                        <HoveredStyle Resize="None"></HoveredStyle>
                                        <EnabledStyle Resize="None"></EnabledStyle>
                                    </DateInput>
                                <%--    <DatePopupButton CssClass="rcCalPopup" ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a id="openpp1" runat="server" href="org.aspx" onclick="window.open(this.href, 'mywin','left=300,top=300,width=500,height=300,toolbar=1,resizable=0'); return false;">
                                    <asp:Label ID="lbGuiDen" runat="server" Text=""></asp:Label>
                                </a>
                            </td>
                            <td>
                                <asp:DropDownList ID="dropGuiDen" runat="server" DataValueField="Org_PK" DataTextField="Description"
                                    Width="190px" Font-Names="Arial" Font-Size="Small">
                                </asp:DropDownList>
                                <uc5:ucVendor ID="ucVendor2" runat="server" Visible="False" />
                            </td>
                            <td>
                                Người xử lý<br />
                                Assign to
                            </td>
                            <td>
                                <asp:DropDownList ID="dropNguoiXL" runat="server" Width="190px" DataValueField="Email"
                                    DataTextField="Description" Font-Names="Arial" Font-Size="Small" AutoPostBack="True"
                                    OnSelectedIndexChanged="dropNguoiXL_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:TextBox ID="txtEmailOther" runat="server" Visible="False" Width="190px" Font-Names="Arial"
                                    Font-Size="Small"></asp:TextBox>
                            </td>
                            <td>
                                Độ ưu tiên<br />
                                Priority
                            </td>
                            <td>
                                <asp:DropDownList ID="dropLevel" runat="server" Width="190px" Visible="true" Font-Names="Arial"
                                    Font-Size="Small">
                                    <asp:ListItem Value="1">Bình thường/Normal</asp:ListItem>
                                    <asp:ListItem Value="2">Quan trọng/Important</asp:ListItem>
                                    <asp:ListItem Value="3">Cấp bách/Urgent</asp:ListItem>
                                    <asp:ListItem Value="4">Tuyệt mật/Top Secret</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Nội dung<br />
                                Brief content
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtContent" Width="100%" runat="server" MaxLength="500" Font-Names="Arial"
                                    Font-Size="Small"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Đính kèm file<br />
                                Attached file
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="190px" />
                            </td>
                            <td>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtYKien" Width="100%" runat="server" MaxLength="500" Visible="false"
                                    Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="float: left; top: 200px; width: 300px;">
                    <telerik:RadGrid ID="RadGrid2" runat="server" Font-Names="Arial" Font-Size="Small">
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
            <div style="width: 75%;">
                <table cellpadding="5px" style="font-family: @Arial Unicode MS; font-size: small;"
                    width="100%">
                    <tr>
                        <td>
                            Nơi lưu trữ<br />
                            Storage location
                        </td>
                        <td>
                            <asp:TextBox ID="txtNoiLuu" runat="server" Width="600px" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table cellpadding="5px" style="font-family: @Arial Unicode MS; font-size: small;">
                    <tr>
                        <td>
                            <asp:Button ID="btSave" runat="server" Text="Save" OnClick="btSave_Click" Font-Names="Arial"
                                Font-Size="Small" />
                        </td>
                        <td>
                            <asp:Button ID="btCancel" runat="server" Text="Cancel" OnClick="btCancel_Click" Font-Names="Arial"
                                Font-Size="Small" />
                        </td>
                        <td>
                            <asp:Button ID="btSubmit" runat="server" Text="Save & Submit" OnClick="btSubmit_Click"
                                Font-Names="Arial" Font-Size="Small" />
                        </td>
                        <td>
                            <asp:Button ID="btCreate" runat="server" Text="Create" OnClick="btCreate_Click" Font-Names="Arial"
                                Font-Size="Small" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <telerik:RadGrid ID="RadGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            ShowFooter="True" OnDeleteCommand="RadGrid1_DeleteCommand" AllowFilteringByColumn="True"
            OnItemCommand="RadGrid1_ItemCommand" OnEditCommand="RadGrid1_EditCommand" OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged"
            Font-Names="Arial" Font-Size="Small" OnItemDataBound="RadGrid1_ItemDataBound">
            <GroupingSettings CaseSensitive="False" />
            <MasterTableView EnableHeaderContextMenu="true" AllowFilteringByColumn="True">
                <CommandItemSettings ExportToPdfText="Export to PDF" />
                <%-- <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                </RowIndicatorColumn>--%>
                <Columns>
                    <telerik:GridTemplateColumn HeaderText="STT<br/>No." UniqueName="SoTT" DataField="STT"
                        AllowFiltering="False">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                        <ItemTemplate>
                            <%# Container.DataSetIndex  + 1%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn CommandName="Edit" Text="Sửa<br/>Edit" ButtonType="ImageButton"
                        HeaderText="Sửa" UniqueName="EditColumn">
                    </telerik:GridButtonColumn>
                    <telerik:GridButtonColumn CommandName="Delete" Text="Xóa<br/>Delete" ButtonType="ImageButton"
                        HeaderText="Xóa" UniqueName="DeleteColumn" ConfirmDialogType="RadWindow" ConfirmText="Are you sure you want to delete this record?"
                        ConfirmTitle="Delete">
                    </telerik:GridButtonColumn>
                    <%--<telerik:GridButtonColumn CommandName="Submit" Text="submit" ButtonType="ImageButton"
                        HeaderText="Action" UniqueName="Submit" ConfirmDialogType="RadWindow" ConfirmText="Are you sure you want to Submit this record?"
                        ConfirmTitle="Submit">
                    </telerik:GridButtonColumn>--%>
                    <telerik:GridTemplateColumn DataField="statusUser" HeaderText="Action" AllowFiltering="false"
                        UniqueName="ActionColumn">
                        <ItemTemplate>
                            <asp:Button ID="btSubmitGrid" CommandName="Submit" runat="server" Text="Submit" Visible='<%# isShowSubmit(Eval ("statusUser")) %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Action" AllowFiltering="false" UniqueName="SentLegal">
                        <ItemTemplate>
                            <asp:Button ID="btRejectGrid" Width="60px" CommandName="Reject" runat="server" Text="Reject"
                                Visible='<%# isShowApprove_Reject(Eval ("statusUser")) %>' />
                            <asp:Button ID="btApprove" CommandName="Approve" Width="60px" runat="server" Text="Accept"
                                Visible='<%# isShowApprove_Reject(Eval ("statusUser")) %>' />
                            <asp:Button ID="btSentLegal" CommandName="SentLegal" runat="server" Width="60px"
                                Text="To" Visible='<%# isShowSubmitLegal(Eval ("statusUser")) %>' />
                            <asp:DropDownList ID="dropEmail" runat="server" Width="150px">
                            </asp:DropDownList>
                            <asp:Button ID="btClosed" Width="60px" CommandName="Closed" runat="server" Text="Close"
                                Visible='<%# isShowClosed(Eval ("status"),Eval ("statusUser")) %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn HeaderText="Loai" DataField="DIDO" UniqueName="DIDO" EmptyDataText=""
                        Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="MaLoaiCV" DataField="MaLoaiCV" UniqueName="MaLoaiCV"
                        EmptyDataText="" Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Display="false" HeaderText="LoaiCV" DataField="LoaiCV" CurrentFilterFunction="Contains"
                        AutoPostBackOnFilter="true" FilterControlWidth="100%" UniqueName="LoaiCV" EmptyDataText=""
                        SortExpression="LoaiCV" ShowFilterIcon="false">
                        <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="100px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Số chứng từ" DataField="Docno" UniqueName="Docno"
                        EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        Display="false" FilterControlWidth="100%" SortExpression="Docno" ShowFilterIcon="false">
                        <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="80px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Số CV<br/>Doc.Number" DataField="Docnoreceived"
                        UniqueName="Docnoreceived" EmptyDataText="" CurrentFilterFunction="Contains"
                        AutoPostBackOnFilter="true" FilterControlWidth="100%" ShowFilterIcon="false">
                        <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="80px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Trạng thái<br/>Status" DataField="statustext"
                        UniqueName="statustext" EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        FilterControlWidth="100%" ShowFilterIcon="false">
                        <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="50px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="File" DataField="AttachedFileView" UniqueName="AttachedFileView"
                        EmptyDataText="" AllowFiltering="false">
                        <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="60px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Trạng thái Final" DataField="status" UniqueName="status"
                        EmptyDataText="" Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Trạng thái User" DataField="statusUser" UniqueName="statusUser"
                        EmptyDataText="" Display="false">
                    </telerik:GridBoundColumn>
                    <%-- <telerik:GridBoundColumn UniqueName="DocDate" DataField="DocDate" HeaderText="Ngày"
                            DataFormatString="{0:dd-MMM-yy}" EmptyDataText="0" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="true" FilterControlWidth="70%" SortExpression="DocDate" PickerType="DatePicker" EnableTimeIndependentFiltering="true">
                            <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                            <FooterStyle Width="70px" HorizontalAlign="Center" />
                            <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>--%>
                    <telerik:GridDateTimeColumn DataField="DocDate" UniqueName="DocDate" HeaderText="Ngày<br/>Date"
                        FilterControlWidth="60%" SortExpression="DocDate" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                        Display="true" DataFormatString="{0:dd-MMM-yyyy}">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn HeaderText="Nội dung tóm tắt<br/>Brief Content" DataField="Content"
                        UniqueName="Content" EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        ShowFilterIcon="false" FilterControlWidth="100%">
                        <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="200px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="File" DataField="AttachedFile" UniqueName="AttachedFile"
                        EmptyDataText="" AllowFiltering="false" Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Gửi từ<br/>From" DataField="Org" UniqueName="Org"
                        EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        FilterControlWidth="100%" SortExpression="Org" ShowFilterIcon="false">
                        <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="200px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Display="false" HeaderText="SentFrom" DataField="SentFrom"
                        UniqueName="SentFrom" EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        FilterControlWidth="100%" ShowFilterIcon="false">
                        <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="200px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Gửi đến<br/>To" DataField="OrgSentTo" UniqueName="OrgSentTo"
                        EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        FilterControlWidth="100%" SortExpression="Org" ShowFilterIcon="false">
                        <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="200px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Display="false" HeaderText="SentTo" DataField="SentTo" UniqueName="SentTo"
                        EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        FilterControlWidth="100%" ShowFilterIcon="false">
                        <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="200px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Display="false" HeaderText="DocStorage" DataField="DocStorage"
                        UniqueName="DocStorage" EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        FilterControlWidth="100%" ShowFilterIcon="false">
                        <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="200px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="MaOrg" DataField="MaOrg" UniqueName="MaOrg"
                        EmptyDataText="" Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Display="false" HeaderText="Người ký<br/>Signed off by"
                        DataField="Approval" UniqueName="Approval" EmptyDataText="" CurrentFilterFunction="Contains"
                        AutoPostBackOnFilter="true" FilterControlWidth="100%" SortExpression="Approval"
                        ShowFilterIcon="false">
                        <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="200px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="AppDate" UniqueName="AppDate" HeaderText="Ngày ký<br/>Signed date"
                        FilterControlWidth="60%" SortExpression="AppDate" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                        Display="false" DataFormatString="{0:dd-MMM-yyyy}">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn Display="false" DataField="RecDate" UniqueName="RecDate"
                        HeaderText="Ngay nhập" FilterControlWidth="60%" SortExpression="RecDate" PickerType="DatePicker"
                        EnableTimeIndependentFiltering="true" DataFormatString="{0:dd-MMM-yyyy}">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn Display="false" HeaderText="Email" DataField="Email" UniqueName="Email"
                        EmptyDataText="" ShowFilterIcon="false">
                    </telerik:GridBoundColumn>
                    <%--   <telerik:GridBoundColumn UniqueName="RecDate" DataField="RecDate" HeaderText="Ngày nhập liệu"
                            DataFormatString="{0:dd-MMM-yy}" EmptyDataText="0" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="true" FilterControlWidth="70%" SortExpression="RecDate" PickerType="DatePicker" EnableTimeIndependentFiltering="true">
                            <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                            <FooterStyle Width="70px" HorizontalAlign="Center" />
                            <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="ReceiveDate" DataField="ReceiveDate" HeaderText="Ngày nhận CV"
                            DataFormatString="{0:dd-MMM-yy}" EmptyDataText="0" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="true" FilterControlWidth="70%" SortExpression="ReceiveDate" PickerType="DatePicker" EnableTimeIndependentFiltering="true">
                            <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                            <FooterStyle Width="70px" HorizontalAlign="Center" />
                            <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>--%>
                    <telerik:GridDateTimeColumn DataField="ReceiveDate" UniqueName="ReceiveDate" HeaderText="Ngày nhận/gửi"
                        FilterControlWidth="60%" SortExpression="ReceiveDate" PickerType="DatePicker"
                        EnableTimeIndependentFiltering="true" DataFormatString="{0:dd-MMM-yyyy}">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn Display="false" HeaderText="NguoiNhap" DataField="UserCreate"
                        UniqueName="UserCreate" EmptyDataText="">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Display="false" HeaderText="IsSentLegal" DataField="IsSentLegal"
                        UniqueName="IsSentLegal" EmptyDataText="">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Display="false" HeaderText="IsClosed" DataField="IsClosed"
                        UniqueName="IsClosed" EmptyDataText="">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn DataField="AppNote" HeaderText="Ý kiến<br/>Notes" AllowFiltering="false"
                        UniqueName="AppNote">
                        <ItemTemplate>
                            <asp:TextBox ID="txtYKienGrid" runat="server" Text='<%# Eval ("AppNote") %>' MaxLength="500"
                                TextMode="MultiLine"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="200px"></ItemStyle>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn HeaderText="Mức độ<br/>Priority" DataField="Level" UniqueName="Level"
                        EmptyDataText="" AllowFiltering="false" Display="true">
                        <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="40px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <%-- <telerik:GridBoundColumn HeaderText="Ý kiến" DataField="AppNote" UniqueName="AppNote"
                        EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        FilterControlWidth="70%" SortExpression="AppNote" ShowFilterIcon="false">
                        <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="200px"></ItemStyle>
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn Display="false" HeaderText="Email Người xử lý" DataField="EmailApp"
                        UniqueName="EmailApp" EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        FilterControlWidth="100%" ShowFilterIcon="false">
                        <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="200px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Display="true" HeaderText="Người xử lý<br/>Assign to" DataField="NguoiXuLy"
                        UniqueName="NguoiXuLy" EmptyDataText="" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        FilterControlWidth="100%" ShowFilterIcon="false">
                        <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="120px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Display="false" HeaderText="GovBiz" DataField="GovBiz" UniqueName="GovBiz"
                        EmptyDataText="">
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
        </telerik:RadGrid>
    </div>
</asp:Content>
