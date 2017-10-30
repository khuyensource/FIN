<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="IPPurRequest.aspx.cs" Inherits="MaricoPay.IPPurRequest" %>

<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="javascript">
        function UserDeleteConfirmation() {
            return confirm("Bạn có chắc muốn xóa PR này?<br/>Are you sure you want to delete this PR?");
        }

        function UserSubmitConfirmation() {
            return confirm("Bạn có chắc muốn gửi PR này qua bộ phận IP, sau khi gửi bạn không thể chỉnh sửa PR này<br/>Are you sure to send to IP, you will not revise this PR");
        }
    </script>
  <style type="text/css">
      
        .thumbnail img {
          border: 1px solid white;
          margin: 0 5px 5px 0;
        }
        .thumbnail:hover {
          background-color: transparent;
  
        }
        .thumbnail:hover img {
          border: 1px solid blue;
        }
        .thumbnail span {
          position: absolute;
          background-color: lightyellow;
          padding: 5px;
          left: -500px;
          border: 1px dashed gray;
          visibility: hidden;
          color: black;
          text-decoration: none;

        }
        .thumbnail span img {
          border-width: 0;
          padding: 2px;
            width:500px;
          height:500px;
  
        }
        .thumbnail:hover span {
          visibility: visible;
          top: 50px;
          left: 230px;
          z-index: 50;
          display:block; 
          position:absolute;
  
          z-index:99;
        }
        
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="radcomboPR" />
        </Triggers>
        <ContentTemplate>
            <uc4:uscMsgBox ID="MsgBox1" runat="server" />
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
            <div>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="color: #000000; font-size:small; font-family:@Arial Unicode MS;">
                           
                            <asp:Label ID="Label1" runat="server" Text="Số PR</br>PR number" Font-Names="Arial" Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="radcomboPR" runat="server" Width="100px" EnableLoadOnDemand="True"
                                HighlightTemplatedItems="True" DataTextField="IDPR" DropDownWidth="650px" DataValueField="IDPR"
                                Filter="Contains" Font-Size="Small" BorderColor="Transparent" BackColor="White"
                                ResolvedRenderMode="Classic" AllowCustomText="True" AutoPostBack="True" Font-Names="Arial"
                                OnSelectedIndexChanged="radcomboPR_SelectedIndexChanged">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr valign="middle">
                                            <td style="width: 70px; text-align: left;">
                                                <div style="font-size: 11;">
                                                    <asp:Label ID="lbNgayLap" runat="server" Text='<%# Eval("NgayLap")%>'></asp:Label>
                                                </div>
                                            </td>
                                            <td style="width: 250px; text-align: left;">
                                                <div style="font-size: 11;">
                                                    <asp:Label ID="lbTrangThai" runat="server" Text='<%# Eval("TrangThai")%>'></asp:Label>
                                                    <asp:HiddenField ID="hfNgaySubmit" Value='<%# Eval("NgaySubmitIP")%>' runat="server" />
                                                    <asp:HiddenField ID="hfTrangThai" Value='<%# Eval("TrangThaiHiden")%>' runat="server" />
                                                </div>
                                            </td>
                                            <td style="width: 200px; text-align: left;">
                                                <div style="font-size: 11;">
                                                    <asp:Label ID="lbGhiChu" runat="server" Text='<%# Eval("GhiChu")%>'></asp:Label>
                                                    <asp:HiddenField ID="hfIPNote" Value='<%# Eval("IPNote")%>' runat="server" />
                                                </div>
                                            </td>
                                            <td style="width: 150px; text-align: left;">
                                                <div style="font-size: 11;">
                                                    <asp:Label ID="lbIPUsser" runat="server" Text='<%# Eval("IPUserXacNhan")%>'></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <asp:Label ID="lbNoiDung" runat="server" Text="Nội dung</br>Content" Font-Names="Arial" Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNoiDung" Font-Names="Arial" Font-Size="Small" runat="server"
                                Width="250px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="laIPUser" runat="server" Text="Người phụ trách</br>IP Users" Font-Names="Arial" Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="RadComboIPUser" DataTextField="Description" DataValueField="Email"
                                Width="150px" runat="server" Font-Names="Arial" Font-Size="Small">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbTrangThaiShow" runat="server" Text="" Font-Bold="true" Font-Names="Arial" Font-Size="Small"></asp:Label>
                            <asp:HiddenField ID="hfTrangThai" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lbNgaySubmitShow" runat="server" Text="" Font-Bold="true" Font-Names="Arial" Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbIPNoteTitle" runat="server" Text="Ghi chú</br>IP's note:" Font-Bold="true" Font-Names="Arial" Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbIPnoteShow" runat="server" Text="" Font-Names="Arial" Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btSubmit" runat="server" Font-Names="Arial" Font-Size="Small" Text="Gửi qua IP/Send To IP" OnClientClick="if ( ! UserSubmitConfirmation()) return false;"
                                OnClick="btSubmit_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btDeletePR" Font-Names="Arial" Font-Size="Small" Visible="false" OnClientClick="if ( ! UserDeleteConfirmation()) return false;"
                                runat="server" Text="Xóa yêu cầu/Delete PR" OnClick="btDeletePR_Click" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <telerik:RadGrid ID="RG" Width="100%" runat="server" AutoGenerateColumns="False"
                    EnableLinqExpressions="false" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                    GridLines="None" AllowPaging="True" OnItemCommand="RG_ItemCommand" OnCancelCommand="RG_CancelCommand"
                    OnDeleteCommand="RG_DeleteCommand" OnEditCommand="RG_EditCommand" OnInsertCommand="RG_InsertCommand"
                    OnUpdateCommand="RG_UpdateCommand" ShowFooter="True" OnPageIndexChanged="RG_PageIndexChanged"
                    OnPageSizeChanged="RG_PageSizeChanged" AllowFilteringByColumn="False" AllowSorting="True"
                    OnGroupsChanging="RG_GroupsChanging" OnSortCommand="RG_SortCommand" ShowGroupPanel="True"
                    OnInit="RG_Init">
                    <HeaderContextMenu EnableTheming="True">
                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                    </HeaderContextMenu>
                    <GroupingSettings CaseSensitive="False" />
                    <MasterTableView CommandItemDisplay="TopAndBottom">
                        <CommandItemSettings AddNewRecordText="Thêm mới/New item" RefreshText="" />
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <%--<telerik:GridTemplateColumn HeaderText="STT" UniqueName="SoTT" DataField="STT" AllowFiltering="False">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <%# Container.DataSetIndex  + 1%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>--%>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                HeaderText="Sửa</br>Edit" EditText="Sửa">
                                <ItemStyle Width="30px" />
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" UniqueName="Delete"
                                ConfirmText="Bạn có chắc muốn xóa nhóm này?</br>Are you sure to delete this item?" Text="Xóa/Delete" HeaderText="Xóa</br>Delete">
                                <ItemStyle Width="50px" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn HeaderText="ID" UniqueName="IDPRDetail" DataField="IDPRDetail"
                                FilterControlWidth="100px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" Display="false">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="IDPR" UniqueName="IDPR" DataField="IDPR" FilterControlWidth="100px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                Display="false">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="IO" UniqueName="IO" DataField="IO" FilterControlWidth="60px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  Width="60px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"  Width="60px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="IDMaterial" UniqueName="IDMaterial" DataField="IDMaterial"
                                FilterControlWidth="100px" Display="false" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Tên Hàng</br>Kind of purchase" UniqueName="TenHang" DataField="TenHang"
                                FilterControlWidth="90px" Display="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  Width="120px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"  Width="120px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Kích Thước</br>Size" UniqueName="KichThuoc" DataField="KichThuoc"
                                FilterControlWidth="150px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Chất Liệu</br>Material" UniqueName="ChatLieu" DataField="ChatLieu"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Độ Dày</br>Thickness" UniqueName="DoDay" DataField="DoDay"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn  HeaderText="Có Đèn</br>Lighting" DataField="CoDen" UniqueName="CoDen"
                                AllowFiltering="False">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCoDen" runat="server" Enabled="false" Checked='<%# fBool(Eval("CoDen")) %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"/>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="Mẫu</br>Design" UniqueName="HinhMau" DataField="HinhMau"
                                CurrentFilterFunction="Contains" Display="false" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Mẫu</br>Design" UniqueName="HinhMauView" DataField="HinhMauView"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="SL</br>Quantity" UniqueName="SoLuong" DataField="SoLuong"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:###,###,###}">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Ngày Giao</br>Delivery Date" UniqueName="NgayGiao" DataField="NgayGiao"
                                CurrentFilterFunction="Contains" DataFormatString="{0:dd/MMM/yy}" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="90px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="90px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Nơi Nhận</br>Received Location" UniqueName="NoiNhan" DataField="NoiNhan"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="130px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="130px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="CT Khác</br>Other specification" UniqueName="ChiTietKhac" DataField="ChiTietKhac"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"/>
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn HeaderText="Costcenter" UniqueName="Costcenter" DataField="Costcenter"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"/>
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn HeaderText="GL" UniqueName="GL" DataField="GL"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"/>
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn HeaderText="MaterialGroup" UniqueName="MaterialGroup" DataField="MaterialGroup"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="90px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="90px"/>
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn HeaderText="Profitcenter" UniqueName="Profitcenter" DataField="Profitcenter"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"/>
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn HeaderText="Country" UniqueName="Country" DataField="Country"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"/>
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn HeaderText="Division" UniqueName="Division" DataField="Division"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"/>
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn HeaderText="SalesGroup" UniqueName="SalesGroup" DataField="SalesGroup"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"/>
                            </telerik:GridBoundColumn>
                        </Columns>
                        <EditFormSettings EditFormType="WebUserControl">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True"/>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2">
                        </Scrolling>
                        <Resizing AllowColumnResize="True" />
                        
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
