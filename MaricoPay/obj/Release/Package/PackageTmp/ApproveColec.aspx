<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ApproveColec.aspx.cs" Inherits="MaricoPay.ApproveColec" %>
    <%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
    <%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <uc4:uscMsgBox ID="MsgBox1" runat="server" />
    <asp:Button ID="btSelectALL" runat="server" Text="Select All" OnClick="btSelectALL_Click" />
    <asp:Button ID="btUnselect" runat="server" Text="UnSelect All" OnClick="btUnselect_Click" />
     <asp:Button ID="btRelease" runat="server" Text="Release" onclick="btRelease_Click" />
    <div>
        <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="Both"
            ShowFooter="true">
            <GroupingSettings CaseSensitive="False" />
            <MasterTableView EnableHeaderContextMenu="true">
                <CommandItemSettings ExportToPdfText="Export to PDF" />
                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                </RowIndicatorColumn>
                <Columns>
                    <telerik:GridTemplateColumn HeaderText="STT<br/>No"
                        AllowFiltering="False">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                        <ItemTemplate>
                            <%# Container.DataSetIndex  + 1%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Approve" AllowFiltering="false" UniqueName="ApproveColum">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkApprove" runat="server" />
                        </ItemTemplate>
                       <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn HeaderText="SốChứngTừ<br/>Docno" DataField="LinkDocNo" UniqueName="LinkDocNo"
                        EmptyDataText="" FilterControlWidth="100px" Aggregate="Count" FooterText="Count: ">
                        <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="120px"></ItemStyle>
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn HeaderText="SốChứngTừ<br/>Docno" DataField="DocNo" UniqueName="DocNo"
                        EmptyDataText="" Display="false">
                    
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Họ tên<br/>Fullname" DataField="FullName" UniqueName="FullName"
                        EmptyDataText="" FilterControlWidth="100px" Display="true">
                        <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="120px"></ItemStyle>
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn HeaderText="Working plan" DataField="LinkDocNoWK" UniqueName="LinkDocNoWK"
                        EmptyDataText="" FilterControlWidth="100px" Aggregate="Count" FooterText="Count: ">
                        <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="120px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Chức vụ<br/>Position" DataField="Position" UniqueName="Position"
                        EmptyDataText="" FilterControlWidth="70px">
                        <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="120px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Lý do<br/>Purpose" DataField="Purpose" UniqueName="Purpose"
                        EmptyDataText="" FilterControlWidth="90px">
                        <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="200px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="DocTot" DataField="DocTot" HeaderText="Tổng tiền<br/>Total"
                         EmptyDataText="0" FilterControlWidth="40px" DataFormatString="{0:###,###,###}">
                        <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                       
                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Ngày tạo<br/>Date" DataField="DateRec" UniqueName="DateRec"
                        EmptyDataText="" FilterControlWidth="70px" Display="true" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="90px"></ItemStyle>
                    </telerik:GridBoundColumn>
                      <telerik:GridBoundColumn HeaderText="Email" DataField="Email" UniqueName="Email"
                        EmptyDataText="" FilterControlWidth="90px" Display="false">
                      
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="PhongBan" DataField="PhongBan" UniqueName="PhongBan"
                        EmptyDataText="" FilterControlWidth="90px" Display="false">
                      
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="TuNgay" DataField="TuNgay" UniqueName="TuNgay"
                        EmptyDataText="" FilterControlWidth="90px" Display="false" DataFormatString="{0:dd/MM/yyyy}">
                      
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="DenNgay" DataField="DenNgay" UniqueName="DenNgay"
                        EmptyDataText="" FilterControlWidth="90px" Display="false" DataFormatString="{0:dd/MM/yyyy}">
                      
                    </telerik:GridBoundColumn>
                      <telerik:GridBoundColumn HeaderText="DaTamUng" DataField="DaTamUng" UniqueName="DaTamUng"
                        EmptyDataText="" FilterControlWidth="90px" Display="false" DataFormatString="{0:###,###,###}">
                      
                    </telerik:GridBoundColumn>
                    
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
    </div>
</asp:Content>
