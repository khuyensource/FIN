<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ip_ipinput.aspx.cs" Inherits="MaricoPay.ip_ipinput" %>

<%@ Register Src="uc/ucCurr.ascx" TagName="ucCurr" TagPrefix="uc1" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<%@ Reference Control="~/uc/ucCurr.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
    <style type="text/css">
        table
        {
            font-family: arial, sans-serif;
            border-collapse: initial;
            width: auto;
        }

        td, th
        {
            border: 0.5px solid #dddddd;
            text-align: left;
            padding: 0.5px;
            width: inherit;
        }

        tr:nth-child(even)
        {
            background-color: #dddddd;
        }
    </style>

    
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


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
     <script type="text/javascript"  language="javascript">
         function UserDeleteConfirmation() {
             return confirm("Bạn có chắc muốn xóa PR này?</br>Are you sure you want to Reject this PR?");
         }

         function UserSubmitConfirmation() {
             return confirm("Bạn có chắc muốn gửi PR này qua bộ phận IP, sau khi gửi bạn không thể chỉnh sửa PR này</br>Are you sure to send to IP, you will not revise this PR");
         }
    </script>

    
     <telerik:RadNotification ID="RadNotification1" runat="server"  AutoCloseDelay="0" KeepOnMouseOver="true" 
                Position="TopCenter" ShowCloseButton="False" VisibleOnPageLoad="True"
                VisibleTitlebar="False" Width="998px">
                <ContentTemplate>
                    <center>
                        <asp:Label ID="lbLoi" runat="server"></asp:Label>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" >
                            <ProgressTemplate>
                                <img alt="Loading..." src="./Images/ajax-loader-bar.gif" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </center>
                </ContentTemplate>
            </telerik:RadNotification>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <uc4:uscMsgBox ID="MsgBox1" runat="server" />
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="RadGrid1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGrid1"></telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>



            <p>
                <b>YÊU CẦU BÁO GIÁ - RFQ</b>
            </p>
            <table>
                <tr> 
                   
                    <td>
                         <telerik:RadComboBox ID="radcomboTrangThai" AutoPostBack="true" DropDownWidth="250px"
                                DataTextField="Ten" DataValueField="MaTrangThai"
                                Width="150px" runat="server" Font-Size="Small" Font-Names="Arial" 
                                onselectedindexchanged="radcomboTrangThai_SelectedIndexChanged">
                            </telerik:RadComboBox>
                    </td>
                    <td>Số PR/PR No. :</td>

                    <td>
                        <telerik:RadComboBox ID="radcomboPR" runat="server" Width="100px" EnableLoadOnDemand="True"
                            HighlightTemplatedItems="True" DataTextField="IDPR" DropDownWidth="500px" DataValueField="IDPR"
                            Filter="Contains" Font-Size="Small" BorderColor="Transparent" BackColor="White"
                            ResolvedRenderMode="Classic" AllowCustomText="True" AutoPostBack="True" Font-Names="Arial"
                            OnSelectedIndexChanged="radcomboPR_SelectedIndexChanged">
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr valign="middle">
                                        <td style="width: 70px; text-align: left;">
                                            <div style="font-size: inherit;">
                                                <asp:Label ID="lbNgayLap" runat="server" Text='<%# Eval("NgayLap")%>'></asp:Label>
                                            </div>
                                        </td>
                                        <td style="width: 100px; text-align: left;">
                                            <div style="font-size: inherit;">
                                                <asp:Label ID="lbTrangThai" runat="server" Text='<%# Eval("TrangThai")%>'></asp:Label>
                                                <asp:HiddenField ID="hfNgaySubmit" Value='<%# Eval("NgaySubmitIP")%>' runat="server" />
                                                <asp:HiddenField ID="hfTrangThai" Value='<%# Eval("TrangThaiHiden")%>' runat="server" />
                                            </div>
                                        </td>
                                        <td style="width: 150px; text-align: left;">
                                            <div style="font-size: inherit;">
                                                <asp:Label ID="lbGhiChu" runat="server" Text='<%# Eval("GhiChu")%>'></asp:Label>
                                                <asp:HiddenField ID="hfIPNote" Value='<%# Eval("IPNote")%>' runat="server" />
                                            </div>
                                        </td>
                                        <td style="width: 150px; text-align: left;">
                                            <div style="font-size: inherit;">
                                                <asp:Label ID="lbNguoiTao" runat="server" Text='<%# Eval("EmailNguoiTao")%>'></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                        </telerik:RadComboBox>

                    </td>
                    <td>


                        <asp:Button ID="Button1" runat="server"  Font-Size="13px" Text="Gửi cho Nhà Cung Cấp/Send for RFQ" OnClientClick="if ( ! UserSubmitConfirmation()) return false;"   OnClick="Button1_Click" />
                         <asp:Button ID="Button2" runat="server"  Font-Size="13px" Text="Gửi lại cho Nhà Cung Cấp/Resend for RFQ" OnClientClick="if ( ! UserSubmitConfirmation()) return false;"   OnClick="Button2_Click" />

                    </td>
                </tr>




                <tr>
                    <td colspan="3">Specification (chi tiết kỹ thuật cần báo giá)</td>

                </tr>

            </table>



            <telerik:RadGrid ID="RadGrid1" Width="100%" runat="server" AutoGenerateColumns="False"
                EnableLinqExpressions="false" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                GridLines="None" AllowPaging="True" OnItemCommand="RG_ItemCommand" OnCancelCommand="RG_CancelCommand"
                OnDeleteCommand="RG_DeleteCommand" OnEditCommand="RG_EditCommand" Font-Size="Medium"
                OnUpdateCommand="RG_UpdateCommand" ShowFooter="True" OnPageIndexChanged="RG_PageIndexChanged"
                OnPageSizeChanged="RG_PageSizeChanged" AllowSorting="True"
                OnGroupsChanging="RG_GroupsChanging" OnSortCommand="RG_SortCommand" ShowGroupPanel="True" OnItemDataBound="RadGrid1_ItemDataBound">
                <HeaderContextMenu EnableTheming="True">
                    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                </HeaderContextMenu>
                <GroupingSettings CaseSensitive="False" />
                <MasterTableView>
                    <%--  CommandItemDisplay="TopAndBottom"--%>
                    <CommandItemSettings AddNewRecordText="Thêm mới" RefreshText="" ShowAddNewRecordButton="false" />
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

                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20px" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20px" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderText="ID" UniqueName="IDPRDetail" DataField="IDPRDetail" Display="false"
                            FilterControlWidth="100px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="IDPR" UniqueName="IDPR" DataField="IDPR" FilterControlWidth="100px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            Display="false">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="IO" UniqueName="IO" DataField="IO" FilterControlWidth="100px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="IDMaterial" UniqueName="IDMaterial" DataField="IDMaterial"
                            FilterControlWidth="100px" Display="false" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Tên Hàng</br>Kind of purchase" UniqueName="TenHang" DataField="TenHang"
                            FilterControlWidth="100px" Display="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Kích Thước</br>Size" UniqueName="KichThuoc" DataField="KichThuoc"
                            FilterControlWidth="100px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Chất Liệu</br>Material" UniqueName="ChatLieu" DataField="ChatLieu"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Độ Dày</br>Thickness" UniqueName="DoDay" DataField="DoDay"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn DataField="CoDen" HeaderText="Có Đèn</br>Lighting" UniqueName="CoDen"
                            AllowFiltering="False">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCoDen" runat="server" Enabled="false" Checked='<%# fBool(Eval("CoDen")) %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Mẫu</br>Design" UniqueName="HinhMau" DataField="HinhMau"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn HeaderText="SL</br>Quantity" UniqueName="SoLuong" DataField="SoLuong"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Ngày Giao</br>Delivery Date" UniqueName="NgayGiao" DataField="NgayGiao"
                            CurrentFilterFunction="Contains" DataFormatString="{0:dd/MMM/yy}" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Nơi Nhận</br>Received Location" UniqueName="NoiNhan" DataField="NoiNhan"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="CT Khác</br>Other specification" UniqueName="ChiTietKhac" DataField="ChiTietKhac"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="tomau" UniqueName="tomau" DataField="tomau" Display="false"  DataFormatString=""
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                       

                    </Columns>
                    <EditFormSettings EditFormType="WebUserControl">
                        <EditColumn UniqueName="EditCommandColumn1">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" />
                    <%--  <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>--%>
                </ClientSettings>
            </telerik:RadGrid>


           

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="radcomboPR" />
        </Triggers>

    </asp:UpdatePanel>
</asp:Content>
