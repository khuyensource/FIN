<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhanQuyen.aspx.cs" Inherits="MaricoPay.PhanQuyen" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>phan quyen</title>
</head>
<body>
    <form id="Form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" SelectedIndex="1" MultiPageID="MultiPage"
                AutoPostBack="True" OnTabClick="RadTabStrip1_TabClick" 
                ResolvedRenderMode="Classic">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Phân quyền">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Danh mục site" Selected="True">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Danh mục quyền">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <center>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img style="margin-top: 10px;" alt="Loading..." src="images/ajax-loader-bar.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </center>
            <telerik:RadMultiPage ID="MultiPage" runat="server" Width="100%" SelectedIndex="1"
                Height="100%">
                <telerik:RadPageView ID="RadPageView1" runat="server">
                    <center>
                        <fieldset style="width: 90%">
                            <legend>Danh mục SITE</legend>
                            <table border="0" cellpadding="2" cellspacing="2" style="width: 96%; text-align: left">
                                <tr>
                                    <td align="left" width="120px">
                                        Danh mục site:
                                    </td>
                                    <td align="left">
                                        <telerik:RadComboBox ID="RadIDSITE" runat="server" Height="200px" Width="250px" HighlightTemplatedItems="true"
                                            DataTextField="IDSITE" DropDownWidth="300px" DataValueField="IDSITE">
                                            <HeaderTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" width="250px">
                                                    <tr>
                                                        <td style="width: 60px" align="left">
                                                            IDSITE
                                                        </td>
                                                        <td style="width: 230px" align="right">
                                                            TÊN SITE
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" width="250px">
                                                    <tr>
                                                        <td style="width: 60px" align="left">
                                                            <%# Eval("IDSITE")%>
                                                        </td>
                                                        <td style="width: 230px" align="right">
                                                            <%# Eval("TENSITE")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 12px">
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                        <fieldset style="width: 90%">
                            <legend>Danh mục Nhân Viên</legend>
                            <table border="0" cellpadding="2" cellspacing="2" style="width: 96%; text-align: left">
                                <tr>
                                    <td align="left" width="120px">
                                        Mã nhân viên:
                                    </td>
                                    <td align="left">
                                        <telerik:RadComboBox ID="radcomboUser" runat="server" DataTextField="ten" DataValueField="manv"
                                            OnSelectedIndexChanged="radcomboUser_SelectedIndexChanged" AutoPostBack="true">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="120px">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 12px">
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                        <fieldset style="width: 90%">
                            <legend>Quyền</legend>
                            <table border="0" cellpadding="2" cellspacing="2" style="width: 96%; text-align: left">
                                <tr>
                                    <td align="left" width="120px">
                                        Danh mục quyền:
                                    </td>
                                    <td align="left">
                                        <asp:RadioButtonList ID="rdDMQuyen" runat="server" DataSourceID="Quyen" DataTextField="Quyen"
                                            DataValueField="IDQuyen" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        </asp:RadioButtonList>
                                        <asp:SqlDataSource ID="Quyen" runat="server" ConnectionString="<%$ ConnectionStrings:FINConnectionString %>"
                                            SelectCommand="SELECT [IDQuyen], [Quyen] FROM [QUYEN]"></asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 12px">
                                        <asp:ImageButton ID="btLuu" runat="server" ImageUrl="images/luu.png" OnClick="btLuu_Click"
                                            ValidationGroup="Gpq" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                        <fieldset style="width: 90%">
                            <legend>Phân quyền</legend>
                            <telerik:RadGrid ID="RGPHANQUYEN" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True"
                                AllowAutomaticUpdates="True" AutoGenerateColumns="False" BorderStyle="None" GridLines="None"
                                OnCancelCommand="RGDMSITE_CancelCommand" OnDeleteCommand="RGPHANQUYEN_DeleteCommand"
                                PageSize="20" Width="100%">
                                <PagerStyle Mode="NextPrevAndNumeric" />
                                <MasterTableView AutoGenerateColumns="False" HorizontalAlign="NotSet" Width="100%">
                                    <Columns>
                                        <telerik:GridTemplateColumn DataField="IDSITE" HeaderText="IDSITE" UniqueName="IDSITE">
                                            <ItemTemplate>
                                                <%# Eval("IDSITE")%><asp:HiddenField ID="hdIDSITE" runat="server" Value='<%# Eval("IDSITE")%>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="Quyen" HeaderText="Quyền" UniqueName="Quyen">
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="rbQuyen" runat="server" DataTextField="Quyen" DataValueField="IDQuyen"
                                                    RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rbQuyen_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                            ConfirmText="Bạn có muốn xóa thông tin này không?" ConfirmTitle="Delete" HeaderText="Xóa"
                                            Text="Xóa" UniqueName="column">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </fieldset>
                    </center>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView2" runat="server">
                    <telerik:RadGrid ID="RGDMSITE" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True"
                        AllowAutomaticUpdates="True" AllowPaging="True" AutoGenerateColumns="False" BorderStyle="None"
                        GridLines="None" OnCancelCommand="RGDMSITE_CancelCommand" OnDeleteCommand="RGDMSITE_DeleteCommand"
                        OnInsertCommand="RGDMSITE_InsertCommand" OnItemCommand="RGDMSITE_ItemCommand"
                        OnPageIndexChanged="RGDMSITE_PageIndexChanged" OnPageSizeChanged="RGDMSITE_PageSizeChanged"
                        OnUpdateCommand="RGDMSITE_UpdateCommand" PageSize="20" Width="100%">
                        <HeaderContextMenu EnableTheming="True">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <PagerStyle Mode="NextPrevAndNumeric" />
                        <MasterTableView AutoGenerateColumns="False" CommandItemDisplay="Top" HorizontalAlign="NotSet"
                            Width="100%">
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <CommandItemSettings AddNewRecordText="Thêm mới" RefreshText="" />
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridTemplateColumn DataField="IDSITE" HeaderText="ID site" UniqueName="IDSITE">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <%# Eval("IDSITE")%><asp:HiddenField ID="hfIDSITE" runat="server" Value='<%# Eval("IDSITE")%>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="TENSITE" HeaderText="Tên site" UniqueName="TENSITE">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <%# Eval("TENSITE")%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="HIEULUC" HeaderText="Hiệu lực" UniqueName="HIEULUC">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox Enabled="false" ID="cbHIEULUC" runat="server" Checked='<%# fBool(Eval("HIEULUC")) %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Sửa" HeaderText="Chỉnh sửa"
                                    UniqueName="EditCommandColumn">
                                    <ItemStyle />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                    ConfirmText="Bạn có muốn xóa thông tin này không?" ConfirmTitle="Delete" HeaderText="Xóa"
                                    Text="Xóa" UniqueName="column">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings EditFormType="WebUserControl">
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView3" runat="server">
                    <telerik:RadGrid ID="RGDMQuyen" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True"
                        AllowAutomaticUpdates="True" AllowPaging="True" PageSize="20" Width="100%" OnItemCommand="RGDMQuyen_ItemCommand"
                        OnCancelCommand="RGDMQuyen_CancelCommand" OnDeleteCommand="RGDMQuyen_DeleteCommand"
                        OnInsertCommand="RGDMQuyen_InsertCommand" OnUpdateCommand="RGDMQuyen_UpdateCommand">
                        <HeaderContextMenu EnableTheming="True">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <PagerStyle Mode="NextPrevAndNumeric" />
                        <MasterTableView AutoGenerateColumns="False" CommandItemDisplay="Top" HorizontalAlign="NotSet"
                            Width="100%">
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <CommandItemSettings AddNewRecordText="Thêm mới" RefreshText="" />
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridTemplateColumn DataField="IDQuyen" HeaderText="ID quyền" UniqueName="IDQuyen">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <%# Eval("IDQuyen")%><asp:HiddenField ID="hfIDQuyen" runat="server" Value='<%# Eval("IDQuyen")%>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Quyen" HeaderText="Tên quyền" UniqueName="Quyen">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <%# Eval("Quyen")%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Sửa" HeaderText="Chỉnh sửa"
                                    UniqueName="EditCommandColumn">
                                    <ItemStyle />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                    ConfirmText="Bạn có muốn xóa thông tin này không?" ConfirmTitle="Delete" HeaderText="Xóa"
                                    Text="Xóa" UniqueName="column">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings EditFormType="WebUserControl">
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--</asp:Content>--%>
    </form>
</body>
</html>
