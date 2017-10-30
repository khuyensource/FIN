<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IPVendors.aspx.cs" Inherits="MaricoPay.IPVendors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           <telerik:RadNotification ID="RadNotification1" runat="server"  AutoCloseDelay="0" KeepOnMouseOver="true" 
                Position="TopCenter" ShowCloseButton="False" VisibleOnPageLoad="True"
                VisibleTitlebar="False" Width="998px">
                <ContentTemplate>
                    <center>
                        <asp:Label ID="lbLoi" runat="server"></asp:Label>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img alt="Loading..." src="./Images/ajax-loader-bar.gif" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </center>
                </ContentTemplate>
            </telerik:RadNotification>
            <telerik:RadGrid ID="RG" Width="100%" runat="server" AutoGenerateColumns="False"
                EnableLinqExpressions="false" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                GridLines="None" AllowPaging="True" OnItemCommand="RG_ItemCommand" OnCancelCommand="RG_CancelCommand"
                OnDeleteCommand="RG_DeleteCommand" OnEditCommand="RG_EditCommand" OnInsertCommand="RG_InsertCommand"
                OnUpdateCommand="RG_UpdateCommand" ShowFooter="True" OnPageIndexChanged="RG_PageIndexChanged"
                OnPageSizeChanged="RG_PageSizeChanged" AllowFilteringByColumn="True" AllowSorting="True"
                OnGroupsChanging="RG_GroupsChanging" OnSortCommand="RG_SortCommand" ShowGroupPanel="True"
                OnInit="RG_Init">
                <HeaderContextMenu EnableTheming="True">
                    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                </HeaderContextMenu>
                <GroupingSettings CaseSensitive="False" />
                <MasterTableView CommandItemDisplay="TopAndBottom">
                    <CommandItemSettings AddNewRecordText="Thêm mới" RefreshText="" />
                    <RowIndicatorColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                    <Columns>
                       <%-- <telerik:GridTemplateColumn HeaderText="STT" UniqueName="SoTT" DataField="STT" AllowFiltering="False">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <%# Container.DataSetIndex  + 1%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridBoundColumn HeaderText="ID" UniqueName="ID" DataField="ID" FilterControlWidth="100px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  Width="40px" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"  Width="40px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="TenNCC" UniqueName="TenNCC" DataField="TenNCC"
                            FilterControlWidth="100px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="NguoiLienHe" UniqueName="NguoiLienHe" DataField="NguoiLienHe"
                            FilterControlWidth="100px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="DienThoaiDD" UniqueName="DienThoaiDD" DataField="DienThoaiDD"
                            FilterControlWidth="100px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="DienThoaiBan" UniqueName="DienThoaiBan" DataField="DienThoaiBan"
                            FilterControlWidth="100px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Fax" UniqueName="Fax" DataField="Fax" FilterControlWidth="100px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Email" UniqueName="Email" DataField="Email"
                            FilterControlWidth="100px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="DiaChi" UniqueName="DiaChi" DataField="DiaChi"
                            FilterControlWidth="100px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="GhiChu" UniqueName="GhiChu" DataField="GhiChu"
                            FilterControlWidth="100px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="VendorCodeSAP" UniqueName="VendorCodeSAP" DataField="VendorCodeSAP"
                            FilterControlWidth="100px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn DataField="HieuLuc" HeaderText="HieuLuc" UniqueName="HieuLuc"
                            AllowFiltering="False">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbHieuLuc" runat="server" Enabled="false" Checked='<%# fBool(Eval("HieuLuc")) %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                            HeaderText="Chỉnh sửa" EditText="Sửa">
                            <ItemStyle />
                            <HeaderStyle HorizontalAlign="Center" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" UniqueName="Delete"
                            ConfirmText="Bạn có chắc muốn xóa nhóm này?" Text="Xóa" HeaderText="Xóa">
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings EditFormType="WebUserControl">
                        <EditColumn UniqueName="EditCommandColumn1">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2">
                    </Scrolling>
                </ClientSettings>
            </telerik:RadGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
