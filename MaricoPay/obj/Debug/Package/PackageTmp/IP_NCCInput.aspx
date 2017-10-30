<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IP_NCCInput.aspx.cs" Inherits="MaricoPay.IP_NCCInput" %>


<%@ Register Src="uc/ucMsgBox.ascx" TagName="ucMsgBox" TagPrefix="uc1" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NCC báo giá</title>


    <link rel="stylesheet" href="~/Styles/Layout.css" type="text/css" />
    <script type="text/javascript" language="javascript">
        function UserDeleteConfirmation() {
            return confirm("Bạn có chắc muốn xóa PR này?</br>Are you sure you want to Reject this PR?");
        }

        function UserSubmitConfirmation() {
            return confirm("Bạn có chắc muốn gửi PR này qua bộ phận IP, sau khi gửi bạn không thể chỉnh sửa PR này</br>Are you sure to send to IP, you will not revise this PR");
        }
    </script>

    <style type="text/css">
        .thumbnail img
        {
            border: 1px solid white;
            margin: 0 5px 5px 0;
        }

        .thumbnail:hover
        {
            background-color: transparent;
        }

            .thumbnail:hover img
            {
                border: 1px solid blue;
            }

        .thumbnail span
        {
            position: absolute;
            background-color: lightyellow;
            padding: 5px;
            left: -500px;
            border: 1px dashed gray;
            visibility: hidden;
            color: black;
            text-decoration: none;
        }

            .thumbnail span img
            {
                border-width: 0;
                padding: 2px;
                width: 500px;
                height: 500px;
            }

        .thumbnail:hover span
        {
            visibility: visible;
            top: 50px;
            left: 230px;
            z-index: 50;
            display: block;
            position: absolute;
            z-index: 99;
        }
    </style>

    <style type="text/css">
        .thumbnail img
        {
            border: 1px solid white;
            margin: 0 5px 5px 0;
        }

        .thumbnail:hover
        {
            background-color: transparent;
        }

            .thumbnail:hover img
            {
                border: 1px solid blue;
            }

        .thumbnail span
        {
            position: absolute;
            background-color: lightyellow;
            padding: 5px;
            left: -500px;
            border: 1px dashed gray;
            visibility: hidden;
            color: black;
            text-decoration: none;
        }

            .thumbnail span img
            {
                border-width: 0;
                padding: 2px;
                width: 500px;
                height: 500px;
            }

        .thumbnail:hover span
        {
            visibility: visible;
            top: 50px;
            left: 230px;
            z-index: 50;
            display: block;
            position: absolute;
            z-index: 99;
        }
        .auto-style1
        {
            font-size: large;
            font-weight: bold;
            color: blue;
        }
    </style>


</head>
<body>

    <form id="form1" runat="server">
        <div class="header">
            <div class="title">
                <h1 style="background: #4b6c9e">
                    <a href="Default.aspx">
                        <img alt="" src="LogoMARICOSEA.png" width="70px" height="75px" /></a>
                </h1>

            </div>
        </div>

        <div>


            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>


            <uc1:ucMsgBox ID="ucMsgBox" runat="server" />

            <telerik:RadNotification ID="RadNotification1" runat="server" AutoCloseDelay="0" KeepOnMouseOver="true"
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



                    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                        <AjaxSettings>
                            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadGrid1"></telerik:AjaxUpdatedControl>
                                </UpdatedControls>
                            </telerik:AjaxSetting>
                        </AjaxSettings>
                    </telerik:RadAjaxManager>




                    <table style="width: 100%">
                        <tr>
                            <td>
                                <center>

                              
                                    <b>YÊU CẦU BÁO GIÁ - RFQ</b>
                               </center>
                            </td>
                        </tr>
                        <tr>



                            <td>


                                <asp:Button ID="Button1" runat="server" Text="Submit quotation - Gửi Báo giá" OnClientClick="if ( ! UserSubmitConfirmation()) return false;" Font-Size="Small" OnClick="Button1_Click" />

                            </td>
                        </tr>

                        <tr>
                            <td>Phần yêu cầu: Quý Nhà cung cấp vui lòng báo giá theo chi tiết kỹ thuật sau đây, bằng cách điền các thông tin bên dưới và nhấn vào nút 'Submit quotation - Gửi Báo giá'</td>

                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" ></asp:Label>
                                &nbsp; </td>

                        </tr>
                        <tr>
                            <td>
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
                                            <telerik:GridBoundColumn HeaderText="Đã nhận báo giá</br>Quotation received" UniqueName="DaNhanBaoGia" DataField="DaNhanBaoGia" Display="false">
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </telerik:GridBoundColumn>


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
                                            <telerik:GridBoundColumn HeaderText="CT Khác</br>Other specificatio" UniqueName="ChiTietKhac" DataField="ChiTietKhac"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Linkcode" UniqueName="Linkcode" DataField="Linkcode" Display="false"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn HeaderText="tomau" UniqueName="tomau" DataField="tomau" Display="false"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </telerik:GridBoundColumn>
                                            <%--  <telerik:GridBoundColumn HeaderText="IDVendor" UniqueName="IDVendor" DataField="IDVendor" >
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>--%>
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
                            </td>
                        </tr>

                    </table>








                </ContentTemplate>
                <Triggers>
                </Triggers>

            </asp:UpdatePanel>

        </div>
    </form>
</body>
</html>
