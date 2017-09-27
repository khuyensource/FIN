<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateIPChonNCC.ascx.cs"
    Inherits="MaricoPay.uc.UpdateIPChonNCC" %>
<div>
    <asp:HiddenField ID="hfIDPR" runat="server" />
    <asp:HiddenField ID="hfIDPRDetail" runat="server" />
    <asp:HiddenField ID="hfIDPRVendor" runat="server" />
    <telerik:RadGrid ID="RGNCC" Width="1000px" Height="180px" runat="server" AutoGenerateColumns="False"
        EnableLinqExpressions="false" GridLines="None" AllowPaging="False" ShowFooter="False"
        ShowHeader="true" AllowFilteringByColumn="False" AllowSorting="True" ShowGroupPanel="False" OnSortCommand="RGNCC_SortCommand">
        <HeaderContextMenu EnableTheming="True">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </HeaderContextMenu>
        <GroupingSettings CaseSensitive="False" />
        <MasterTableView HeaderStyle-BackColor="Green">
            <HeaderStyle BackColor="Green" BorderColor="Green"  />
            
            <Columns>
                <telerik:GridTemplateColumn HeaderText="STT" UniqueName="SoTT" DataField="STT" AllowFiltering="False">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" />
                    <ItemTemplate>
                        <%# Container.DataSetIndex  + 1%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="IDPRVendor" UniqueName="IDPRVendor" DataField="IDPRVendor"
                    Display="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="IDPR" UniqueName="IDPR" DataField="IDPR" Display="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="IDPRDetail" UniqueName="IDPRDetail" DataField="IDPRDetail"
                    Display="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="IDVendor" UniqueName="IDVendor" DataField="IDVendor"
                    Display="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="EmailVendor" UniqueName="EmailVendor" DataField="EmailVendor"
                    Display="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="NguoiGuiBaoGia" UniqueName="NguoiGuiBaoGia"
                    DataField="NguoiGuiBaoGia" Display="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="EmailIPApprove" UniqueName="EmailIPApprove"
                    DataField="EmailIPApprove" Display="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="LanGuiYCBaoGia" UniqueName="LanGuiYCBaoGia"
                    DataField="LanGuiYCBaoGia" Display="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="NgayGuiYCBaoGia" UniqueName="NgayGuiYCBaoGia"
                    DataField="NgayGuiYCBaoGia" Display="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="LanNhanBaoGia" UniqueName="LanNhanBaoGia" DataField="LanNhanBaoGia"
                    Display="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="NgayNhanBaoGia" UniqueName="NgayNhanBaoGia"
                    DataField="NgayNhanBaoGia" Display="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="VendorDuocChon" UniqueName="VendorDuocChon"
                    DataField="VendorDuocChon" Display="false">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="VendorDuocChon" HeaderText="Chọn" UniqueName="VendorDuocChon"
                    AllowFiltering="False">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkChon" AutoPostBack="true"  oncheckedchanged="chkChon_CheckedChanged" runat="server" Enabled="true" Checked='<%# fBool(Eval("VendorDuocChon")) %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="NCC" UniqueName="TenNCC" DataField="TenNCC"
                    FilterControlWidth="100px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  Width="270px"/>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"  Width="270px"/>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Đơn giá" UniqueName="DonGia" DataField="DonGia"
                    FilterControlWidth="100px" CurrentFilterFunction="Contains" DataFormatString="{0:###,###,###}"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Thành tiền" UniqueName="ThanhTien" DataField="ThanhTien"
                    FilterControlWidth="100px" Display="true" DataFormatString="{0:###,###,###}"
                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Làm Mẫu" UniqueName="NgayLamMau" DataField="NgayLamMau"
                    FilterControlWidth="50px" Display="true" DataFormatString="{0:dd/MMM/yy}" CurrentFilterFunction="Contains"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="SX" UniqueName="NgaySX" DataField="NgaySX" FilterControlWidth="60px"
                    CurrentFilterFunction="Contains" ShowFilterIcon="false" DataFormatString="{0:dd/MMM/yy}"
                    AutoPostBackOnFilter="true">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Giao" UniqueName="NgayGiao" DataField="NgayGiao"
                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                    DataFormatString="{0:dd/MMM/yy}">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="HạnTT" UniqueName="HanThanhToan" DataField="HanThanhToan"
                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Mẫu" UniqueName="HinhMau" DataField="HinhMau"
                    CurrentFilterFunction="Contains" Display="false" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Mẫu" UniqueName="HinhMauView" DataField="HinhMauView"
                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Ghi chú" UniqueName="GhiChuVendor" DataField="GhiChuVendor"
                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                </telerik:GridBoundColumn>
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
    <asp:ImageButton ID="btnEdit" runat="server" CommandName="Update" ImageUrl="~/images/luuEn.png"
        ValidationGroup="GEdit"/>
    &nbsp;<asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/Images/dongEn.png"
        CommandName="Cancel" />
    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# fGet(Eval("IDPRDetail"),Eval("IDPR"))%>' />
</div>
