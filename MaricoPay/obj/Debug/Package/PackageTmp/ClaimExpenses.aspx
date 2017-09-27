<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ClaimExpenses.aspx.cs" Inherits="MaricoPay.ClaimExpenses" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/uc/ucComboCharges.ascx" TagName="combocharges" TagPrefix="uc1" %>
<%@ Register Src="~/uc/ucCurrence.ascx" TagName="comboCurrence" TagPrefix="uc2" %>
<%@ Register Src="~/uc/ucUploadimage.ascx" TagName="imgUpload" TagPrefix="uc3" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/Ajax.js")%>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/auto.js")%>'></script>
    <script type="text/javascript">
        function deleteRow(r) {
            var i = r.parentNode.parentNode.rowIndex;
            document.getElementById("myTable").deleteRow(i);

            document.getElementById('<%= __EVENTTARGET.ClientID %>').value = i;
            __doPostBack('__Page', 'MyCustomArgument');
        }
        function __doPostBack(eventTarget, eventArgument) {
            //            var objDiv = document.getElementById("dvScroll");
            //            objDiv.scrollTop = objDiv.scrollHeight;

            if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
                theForm.__EVENTTARGET.value = eventTarget;

                theForm.submit();

            }

        }
        $("#<%= btApp.ClientID %>").click(function () {
            $.blockUI({
                message: '<h1>Please waiting Approved</h1>',
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#f00',
                    opacity: .5,
                    color: '#fff'
                }
            });
        });
        $("#<%= btReject.ClientID %>").click(function () {
            $.blockUI({
                message: '<h1>Please wait Rejectting</h1>',
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#f00',
                    opacity: .5,
                    color: '#fff'
                }
            });
        });
        $("#<%= btSave.ClientID %>").click(function () {
            $.blockUI({
                message: '<h1>Please wait save</h1>',
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
                message: '<h1>Please wait Submit</h1>',
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

        function SetDivPosition() {
            var intY = document.getElementById("<%=dvScroll.ClientID%>");
            intY.scrollTop = 10000; //  intY.scrollHeight;
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:HiddenField ID="__EVENTTARGET" runat="server" OnValueChanged="hdRowDelete_ValueChanged" />
    <asp:HiddenField ID="div_position" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btAdd" />
        </Triggers>
        <ContentTemplate>
            <div style="width: 99%; float: left; overflow: auto; height: 600px;" id="dvScroll"
                runat="server">
                <div>
                    <uc4:uscMsgBox ID="MsgBox1" runat="server" />
                    <table>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="radioClaim" runat="server" RepeatDirection="Horizontal"
                                    Font-Bold="True" ForeColor="Black" AutoPostBack="true" OnSelectedIndexChanged="radioClaim_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="Domestic">Domestic travelling</asp:ListItem>
                                    <asp:ListItem Value="Oversea">Oversea travelling</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table>
                        <tr>
                            <td style="color: #000000;">
                                Claim
                            </td>
                            <td>
                                <asp:DropDownList ID="dropSaved" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropSaved_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="dropApp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropApp_SelectedIndexChanged"
                                    Width="61px">
                                </asp:DropDownList>
                            </td>
                            <td style="color: #000000;">
                                Status
                            </td>
                            <td>
                                <asp:Label ID="lbStatus" runat="server" Text="lbStatus" Font-Bold="True" Font-Size="Large"
                                    ForeColor="#0000CC"></asp:Label>
                                <asp:HiddenField ID="hdStatus" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtMyEmail" runat="server" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <hr />
                <div>
                    <table>
                        <tr>
                            <td style="color: #000000;">
                                Market
                            </td>
                            <td style="color: #000000;">
                                <asp:DropDownList ID="dropMarket" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td style="color: #000000;">
                                Name
                            </td>
                            <td style="color: #000000;">
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            </td>
                            <td style="color: #000000;">
                                Date
                            </td>
                            <td style="color: #000000;">
                                <telerik:RadDatePicker ID="raddateNow" runat="server" Enabled="False" Culture="en-US"
                                    ResolvedRenderMode="Classic" Width="100px">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd-MMM-yy" DateFormat="M/d/yyyy" LabelWidth="40%">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                        <FocusedStyle Resize="None"></FocusedStyle>
                                        <DisabledStyle Resize="None"></DisabledStyle>
                                        <InvalidStyle Resize="None"></InvalidStyle>
                                        <HoveredStyle Resize="None"></HoveredStyle>
                                        <EnabledStyle Resize="None"></EnabledStyle>
                                    </DateInput>
                                    <%--<DatePopupButton CssClass="rcCalPopup rcDisabled" ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                                </telerik:RadDatePicker>
                            </td>
                            <td style="color: #000000;">
                                Approver
                            </td>
                            <td style="color: #000000;">
                                <asp:TextBox ID="txtAppName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: #000000;">
                                Department
                            </td>
                            <td style="color: #000000;">
                                <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>
                            </td>
                            <td style="color: #000000;">
                                Position
                            </td>
                            <td style="color: #000000;">
                                <asp:TextBox ID="txtPosition" runat="server"></asp:TextBox>
                            </td>
                            <td style="color: #000000;">
                                Advanced amount
                            </td>
                            <td style="color: #000000;">
                                <telerik:RadNumericTextBox ID="radnumAdvncedAmount" runat="server" Width="100px"
                                    ReadOnly="False" Value="0">
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
                            <td style="color: #000000;">
                                Email Approved &nbsp;
                            </td>
                            <td style="color: #000000;">
                                <asp:TextBox ID="txtAppEmail" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <hr />
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btExpand" runat="server" Text="Collapse" OnClick="btExpand_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btSave" runat="server" Text="Save" OnClick="btSave_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btSubmit" runat="server" Text="Submit" OnClick="btSubmit_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btPrint" runat="server" Text="Print" OnClick="btPrint_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btApp" runat="server" Text="Approved" OnClick="btApp_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btReject" runat="server" Text="Rejected" OnClick="btReject_Click" />
                        </td>
                        <td style="color: #000000;">
                            <asp:Label ID="lbReason" runat="server" Text="Reason"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAppNote" runat="server" ReadOnly="false" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lbMess" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="dvSum" runat="server">
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="gridSum" runat="server" ForeColor="Black" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="Description" HeaderText="Summary" ItemStyle-Width="200px"
                                            HeaderStyle-Width="200px" />
                                        <asp:BoundField DataField="Total_VND" HeaderText="Total-VND">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NoDays" HeaderText="NoDays">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AVG_VND" HeaderText="AVG_VND">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <hr />
                <div>
                    <asp:HiddenField ID="hdNoDays" runat="server" />
                    <table cellpadding="2" cellspacing="0" style="border: 1px solid black; border-collapse: collapse;"
                        id="myTable">
                        <tr>
                            <td rowspan="2" align="center" style="color: #000000; border: 1px solid black; border-collapse: collapse;
                                font-weight: bold;">
                                From
                            </td>
                            <td rowspan="2" align="center" style="color: #000000; border: 1px solid black; border-collapse: collapse;
                                font-weight: bold;">
                                To
                            </td>
                            <td rowspan="2" align="center" style="color: #000000; border: 1px solid black; border-collapse: collapse;
                                font-weight: bold;">
                                Purpose
                            </td>
                            <td colspan="3" align="center" style="color: #000000; border: 1px solid black; border-collapse: collapse;
                                font-weight: bold;">
                                Invoice/Document
                            </td>
                            <td rowspan="2" align="center" style="color: #000000; border: 1px solid black; border-collapse: collapse;
                                font-weight: bold;">
                                Description
                            </td>
                            <td align="center" style="color: #000000; font-weight: bold; border: 1px solid black;
                                border-bottom: 0px;" id="tdCurr" runat="server">
                                Currency
                            </td>
                            <td align="center" style="color: #000000; font-weight: bold; border: 1px solid black;
                                border-bottom: 0px;" id="tdAmountCurr" runat="server">
                                Amount
                            </td>
                            <td align="center" style="color: #000000; font-weight: bold; border: 1px solid black;
                                border-bottom: 0px;">
                                Amount
                            </td>
                            <td rowspan="2" align="center" style="color: #000000; border: 1px solid black; border-collapse: collapse;
                                font-weight: bold;">
                                Image
                            </td>
                            <td rowspan="2" align="center" style="color: #000000; border: 1px solid black; border-collapse: collapse;
                                font-weight: bold;">
                                Add
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="color: #000000; border: 1px solid black; border-collapse: collapse;
                                font-weight: bold;">
                                Date
                            </td>
                            <td align="center" style="color: #000000; border: 1px solid black; border-collapse: collapse;
                                font-weight: bold;">
                                No
                            </td>
                            <td align="center" style="color: #000000; font-weight: bold; border: 1px solid black;
                                border-collapse: collapse;">
                                Notation
                            </td>
                            <td align="center" style="color: #000000; border: 1px solid black; border-top: 0px;
                                border-collapse: collapse; font-weight: bold;" id="tdRate" runat="server">
                                Rate
                            </td>
                            <td align="center" style="color: #000000; font-weight: bold; border: 1px solid black;
                                border-top: 0px; border-collapse: collapse;" id="tdCurr1" runat="server">
                                Currency
                            </td>
                            <td align="center" style="color: #000000; font-weight: bold; border: 1px solid black;
                                border-top: 0px; border-collapse: collapse;">
                                VND
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#33cc33">
                                <telerik:RadDatePicker ID="raddateFrom_Sales" runat="server" Width="100px">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd-MMM-yy" DateFormat="dd-MMM-yy" LabelWidth="40%">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                    </DateInput>
                                  <%--  <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                                </telerik:RadDatePicker>
                            </td>
                            <td bgcolor="#33cc33">
                                <telerik:RadDatePicker ID="raddateTo_Sales" runat="server" Width="100px">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd-MMM-yy" DateFormat="dd-MMM-yy" LabelWidth="40%">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                    </DateInput>
                                  <%--  <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                                </telerik:RadDatePicker>
                            </td>
                            <td bgcolor="#33cc33">
                                <asp:TextBox ID="txtPurpose_sales" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td bgcolor="Yellow">
                                <telerik:RadDatePicker ID="raddateInvoice" runat="server" Width="100px">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd-MMM-yy" DateFormat="dd-MMM-yy" LabelWidth="40%">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                    </DateInput>
                                   <%-- <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                                </telerik:RadDatePicker>
                            </td>
                            <td bgcolor="Yellow">
                                <asp:TextBox ID="txtNoInvoice" runat="server" Width="80px"></asp:TextBox>
                                <a href="popVAT.aspx" onclick="window.open(this.href, 'mywin','left=300,top=300,width=500,height=150,toolbar=1,resizable=0'); return false;">
                                    More..</a>
                            </td>
                            <td bgcolor="Yellow">
                                <asp:TextBox ID="txtNotation" runat="server" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 130px;" bgcolor="Yellow">
                                <uc1:combocharges ID="combocharges1" runat="server" />
                            </td>
                            <td bgcolor="Yellow" id="tdcomboCurr" runat="server">
                                <uc2:comboCurrence ID="comboCurrence1" runat="server" />
                            </td>
                            <td bgcolor="Yellow" id="tdradnumAmount" runat="server">
                                <telerik:RadNumericTextBox ID="radnumAmount" runat="server" Width="60px" Value="0"
                                    AutoPostBack="True" OnTextChanged="radnumAmount_TextChanged">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td bgcolor="Yellow">
                                <telerik:RadNumericTextBox ID="radnuTotalVND" runat="server" Width="60px">
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
                            <td bgcolor="Yellow" style="width: 70px;">
                                <uc3:imgUpload ID="imgUpload1" runat="server" />
                            </td>
                            <td bgcolor="Yellow">
                                <asp:Button ID="btAdd" runat="server" Text="Add" OnClick="btAdd_Click" />
                            </td>
                        </tr>
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
