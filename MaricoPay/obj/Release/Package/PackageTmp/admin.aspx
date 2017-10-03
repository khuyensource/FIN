<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="admin.aspx.cs" Inherits="MaricoPay.admin" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function UserDeleteConfirmationTraval() {

            //  var strUser = document.getElementById("dropTravel").value;
            var mess = "Are you sure you want to delete the travel request?";
            return confirm(mess);
        }
        function UserDeleteConfirmationExpenses() {
            //var e = document.getElementById("dropExpenses");
            //  var strUser = e.options[e.selectedIndex].value;
            return confirm("Are you sure you want to delete the expenses claim?");
        }
        function UserRejectConfirmation() {
            //var e = document.getElementById("dropExpenses");
            //  var strUser = e.options[e.selectedIndex].value;
            return confirm("Are you sure you want to reject the document has been approved?");
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <uc4:uscMsgBox ID="MsgBox1" runat="server" />
            <table cellspacing="1" class="style1">
                <tr>
                    <td>
                        Travel document &nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="dropTravel" AutoPostBack="true" runat="server" DataTextField="Code_PK"
                            DataValueField="Code_PK" OnSelectedIndexChanged="dropTravel_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btViewTravel" runat="server" Text="View" OnClick="btViewTravel_Click" />
                        &nbsp;
                    </td>
                    <td>
                        
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btDeleteTravel" runat="server" Text="Delete" OnClick="btDeleteTravel_Click"
                            OnClientClick="if ( ! UserDeleteConfirmationTraval()) return false;" />
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                         <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" OnItemCommand="RadGrid1_ItemCommand">
                            <GroupingSettings CaseSensitive="False" />
                            <MasterTableView EnableHeaderContextMenu="true">
                                <CommandItemSettings ExportToPdfText="Export to PDF" />
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                </RowIndicatorColumn>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="STT<br/>No" UniqueName="SoTT" DataField="STT"
                                        AllowFiltering="False">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                                        <ItemTemplate>
                                            <%# Container.DataSetIndex  + 1%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="Docno" DataField="Docno" UniqueName="Docno"
                                        EmptyDataText="" FilterControlWidth="180px" Aggregate="Count" FooterText="Count: ">
                                        <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="200px"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="StatusUser" DataField="StatusUser" UniqueName="StatusUser"
                                        EmptyDataText="" FilterControlWidth="100px" Display="false" Aggregate="Count"
                                        FooterText="Count: ">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Approval" DataField="approval" UniqueName="approval"
                                        EmptyDataText="" FilterControlWidth="150px">
                                        <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="150px"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Status" DataField="Status" UniqueName="Status"
                                        EmptyDataText="" FilterControlWidth="130px">
                                        <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="150px"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Note" DataField="Note" HeaderText="Note" EmptyDataText=""
                                        FilterControlWidth="120px">
                                        <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                                        <FooterStyle Width="150px" HorizontalAlign="Center" />
                                        <ItemStyle Width="150px" HorizontalAlign="Center"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Date" DataField="Date" UniqueName="Date" EmptyDataText=""
                                        FilterControlWidth="70px" DataFormatString="{0:dd-MMM-yy}">
                                        <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="90px"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Date Submit" DataField="DateSubmit" UniqueName="DateSubmit"
                                        EmptyDataText="" FilterControlWidth="70px" DataFormatString="{0:dd-MMM-yy}">
                                        <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="90px"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ApprovedCode" DataField="ApprovedCode" HeaderText="ApprovedCode"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="FDate" DataField="FDate" UniqueName="FDate"
                                        EmptyDataText="" FilterControlWidth="70px" Display="false" DataFormatString="{0:dd-MMM-yy}">
                                        <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="90px"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="TDate" DataField="TDate" UniqueName="TDate"
                                        EmptyDataText="" FilterControlWidth="70px" Display="false" DataFormatString="{0:dd-MMM-yy}">
                                        <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="90px"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Username" DataField="Username" HeaderText="Username"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Department" DataField="Department" HeaderText="Department"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Purpose" DataField="Purpose" HeaderText="Purpose"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Destination" DataField="Destination" HeaderText="Destination"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>

                                      <telerik:GridBoundColumn UniqueName="Itenerary" DataField="Itenerary" HeaderText="Itenerary"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn UniqueName="Oto" DataField="Oto" HeaderText="Oto"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn UniqueName="Tauhoa" DataField="Tauhoa" HeaderText="Tauhoa"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn UniqueName="Maybay" DataField="Maybay" HeaderText="Maybay"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn UniqueName="DatVe" DataField="DatVe" HeaderText="DatVe"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn UniqueName="DatPhong" DataField="DatPhong" HeaderText="DatPhong"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>

                                     <telerik:GridBoundColumn UniqueName="Khac" DataField="Khac" HeaderText="Khac"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn UniqueName="Type" DataField="Type" HeaderText="Type"
                                        EmptyDataText="" FilterControlWidth="120px" Display="true">
                                    </telerik:GridBoundColumn>


                                    <telerik:GridTemplateColumn DataField="statusUser" HeaderText="ReSend Email" AllowFiltering="false"
                                        UniqueName="ActionColumn">
                                        <ItemTemplate>
                                            <asp:Button ID="btSubmitGrid" CommandName="Email" Width="90px" runat="server" Text="Send Email"
                                                ToolTip="Send to review" Visible='<%# isShowSendEmail(Eval ("statusUser")) %>' />
                                                <asp:Button ID="btReject" CommandName="Reject" Width="50px" runat="server" Text="Reject" 
                                                ToolTip="Reject" OnClientClick="if ( ! UserRejectConfirmation()) return false;" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <EditFormSettings EditFormType="WebUserControl">
                                    <EditColumn UniqueName="EditCommandColumn1">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                            <%--    AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true"--%>
                            <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                <Selecting AllowRowSelect="True" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="3">
                                </Scrolling>
                                <Resizing AllowColumnResize="true" EnableRealTimeResize="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        Expenses document &nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="dropExpenses" AutoPostBack="true" runat="server" DataTextField="Code_PK"
                            DataValueField="Code_PK" OnSelectedIndexChanged="dropExpenses_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btviewExpenses" runat="server" Text="View" OnClick="btviewExpenses_Click" />
                        &nbsp;
                    </td>
                    <td>
                       
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btDeleteExpenses" runat="server" Text="Delete" OnClick="btDeleteExpenses_Click"
                            OnClientClick="if ( ! UserDeleteConfirmationExpenses()) return false;" />
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <telerik:RadGrid ID="RadGrid2" runat="server" AutoGenerateColumns="False" OnItemCommand="RadGrid2_ItemCommand">
                            <GroupingSettings CaseSensitive="False" />
                            <MasterTableView EnableHeaderContextMenu="true">
                                <CommandItemSettings ExportToPdfText="Export to PDF" />
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                </RowIndicatorColumn>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="STT<br/>No" UniqueName="SoTT" DataField="STT"
                                        AllowFiltering="False">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                                        <ItemTemplate>
                                            <%# Container.DataSetIndex  + 1%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="Docno" DataField="Docno" UniqueName="Docno"
                                        EmptyDataText="" FilterControlWidth="180px" Aggregate="Count" FooterText="Count: ">
                                        <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="200px"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="StatusUser" DataField="StatusUser" UniqueName="StatusUser"
                                        EmptyDataText="" FilterControlWidth="100px" Display="false" Aggregate="Count"
                                        FooterText="Count: ">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Approval" DataField="approval" UniqueName="approval"
                                        EmptyDataText="" FilterControlWidth="150px">
                                        <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="150px"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Status" DataField="Status" UniqueName="Status"
                                        EmptyDataText="" FilterControlWidth="130px">
                                        <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="150px"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Note" DataField="Note" HeaderText="Note" EmptyDataText=""
                                        FilterControlWidth="120px">
                                        <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                                        <FooterStyle Width="150px" HorizontalAlign="Center" />
                                        <ItemStyle Width="150px" HorizontalAlign="Center"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Date" DataField="Date" UniqueName="Date" EmptyDataText=""
                                        FilterControlWidth="70px" DataFormatString="{0:dd-MMM-yy}">
                                        <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="90px"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Date Submit" DataField="DateSubmit" UniqueName="DateSubmit"
                                        EmptyDataText="" FilterControlWidth="70px" DataFormatString="{0:dd-MMM-yy}">
                                        <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="90px"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ApprovedCode" DataField="ApprovedCode" HeaderText="ApprovedCode"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="FDate" DataField="FDate" UniqueName="FDate"
                                        EmptyDataText="" FilterControlWidth="70px" Display="false" DataFormatString="{0:dd-MMM-yy}">
                                        <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="90px"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="TDate" DataField="TDate" UniqueName="TDate"
                                        EmptyDataText="" FilterControlWidth="70px" Display="false" DataFormatString="{0:dd-MMM-yy}">
                                        <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="90px"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Username" DataField="Username" HeaderText="Username"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Department" DataField="Department" HeaderText="Department"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Purpose" DataField="Purpose" HeaderText="Purpose"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="DaTamUng" DataField="DaTamUng" HeaderText="DaTamUng"
                                        EmptyDataText="" FilterControlWidth="120px" Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Type" DataField="Type" HeaderText="Type"
                                        EmptyDataText="" FilterControlWidth="120px" Display="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn DataField="statusUser" HeaderText="ReSend Email" AllowFiltering="false"
                                        UniqueName="ActionColumn">
                                        <ItemTemplate>
                                            <asp:Button ID="btSubmitGrid" CommandName="Email" Width="90px" runat="server" Text="Send Email"
                                                ToolTip="Send to review" Visible='<%# isShowSendEmail(Eval ("statusUser")) %>' />
                                                <asp:Button ID="btReject" CommandName="Reject" Width="50px" runat="server" Text="Reject"
                                                ToolTip="Reject" OnClientClick="if ( ! UserRejectConfirmation()) return false;"/>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <EditFormSettings EditFormType="WebUserControl">
                                    <EditColumn UniqueName="EditCommandColumn1">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                            <%--    AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true"--%>
                            <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                <Selecting AllowRowSelect="True" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="3">
                                </Scrolling>
                                <Resizing AllowColumnResize="true" EnableRealTimeResize="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
