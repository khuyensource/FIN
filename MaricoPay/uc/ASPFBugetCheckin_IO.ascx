<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ASPFBugetCheckin_IO.ascx.cs" Inherits="MaricoPay.uc.ASPFBugetCheckin_IO" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>

<script type="text/javascript">

    function GetRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    }

    function CloseDialog() {
        GetRadWindow().close();
        return false;
    }

    function OnClientClose() {
        __doPostBack('fLoad', '');
    }
</script>

<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>

<uc4:uscMsgBox ID="MsgBox1" runat="server" />

<table>

    <tr>
        <td>Ngày/Date of creation
        </td>
        <td>

            <telerik:RadDatePicker ID="rddateCreation" runat="server"></telerik:RadDatePicker>

        </td>
        <td>Quốc gia/Country 
        </td>
        <td>

            <telerik:RadComboBox ID="Rdcbocountry" runat="server" DataTextField="Country" DataValueField="ID"></telerik:RadComboBox>

        </td>

    </tr>

    <tr>
        <td>Budget owner
        </td>
        <td>

            <asp:TextBox ID="txtBudgetowner" ReadOnly="true" runat="server"> </asp:TextBox>


        </td>
        <td>Ngành hàng/Business unit 
        </td>
        <td>

            <telerik:RadComboBox ID="rdcboNganhHang" runat="server" DataTextField="BU" DataValueField="ID"></telerik:RadComboBox>

        </td>

    </tr>

    <tr>
        <td>Nhãn hàng/Brand
        </td>
        <td>

            <telerik:RadComboBox ID="rdCboBrand" runat="server" DataTextField="Brand" DataValueField="ID" OnSelectedIndexChanged="rdCboBrand_SelectedIndexChanged"></telerik:RadComboBox>


        </td>
        <td>Loại sản phẩm/Category 
        </td>
        <td>

            <telerik:RadComboBox ID="rdCboCategory" runat="server" DataTextField="Category" DataValueField="ID" AutoPostBack="True"></telerik:RadComboBox>

        </td>

    </tr>


    <%-- <tr>
                <td>
                  Mã chi phí/Code
              </td>
                <td>

                  <telerik:RadComboBox ID="Rdcode"  DataTextField="item" AutoCompleteSeparator="true" runat ="server"  DataValueField="ID" ></telerik:RadComboBox>
                   
             

              </td>
                <td>
                  Số tài khoản/GL code 
              </td>
                <td>
                   
                    <asp:TextBox runat="server" ID="txtGLcode"  ReadOnly="true"> </asp:TextBox>
              </td>

          </tr>--%>


    <tr>
        <td>Ngày thực hiện từ ngày/From
        </td>
        <td>


            <telerik:RadDatePicker ID="RddatetimeFrom" runat="server"></telerik:RadDatePicker>

        </td>
        <td>Đến ngày/To 
        </td>
        <td>

            <telerik:RadDatePicker ID="RddatetimeTo" runat="server"></telerik:RadDatePicker>

        </td>

    </tr>


    <tr>
        <td>Áp dụng cho kênh/ Channel
        </td>
        <td>


            <telerik:RadComboBox ID="rdChannel" runat="server" DataTextField="Channel" DataValueField="ID"></telerik:RadComboBox>


        </td>
        <td>Mục tiêu/Objective 
        </td>
        <td>
            <asp:TextBox runat="server" ID="txtObjective" TextMode="MultiLine" Width="300px"> </asp:TextBox>
        </td>

    </tr>

    <tr>
        <td>FY budget of category
        </td>
        <td>
            <telerik:RadNumericTextBox ID="rdFYBudgetCAT" Value="0" NumberFormat-DecimalSeparator="," Type="Number" runat="server" OnTextChanged="rdFYBudgetCAT_TextChanged"></telerik:RadNumericTextBox>



        </td>
        <td>Available budget

        </td>
        <td>
            <telerik:RadNumericTextBox ID="rdAvailable" Value="0" runat="server" AutoPostBack="true" NumberFormat-DecimalSeparator="," Type="Number" OnTextChanged="rdAvailable_TextChanged"></telerik:RadNumericTextBox>

        </td>

    </tr>


    <tr>
        <td>ASPF Value 
        </td>
        <td>



            <telerik:RadNumericTextBox ID="radnumtxtASPFvalue" Value="0" ReadOnly="true" AutoPostBack="true" NumberFormat-DecimalSeparator="," Type="Number" runat="server" OnTextChanged="radnumtxtASPFvalue_TextChanged"></telerik:RadNumericTextBox>

        </td>
        <td>Budget balance 
        </td>
        <td>
            <telerik:RadNumericTextBox ID="radnumtxtBudgetBalance" Value="0" ReadOnly="true" AutoPostBack="true" NumberFormat-DecimalSeparator="," Type="Number" runat="server"></telerik:RadNumericTextBox>



        </td>

    </tr>
    <tr>
        <td colspan="4">

            <a id="linkfile" runat="server" style="background-color: #FFFFFF; color: #FF0000">Attached file </a>

        </td>
    </tr>

</table>



<table width="600px">

    <tr>

        <td>
            <%--  <asp:ImageButton ID="btnAddrow" runat="server" ImageUrl="~/images/add.png"  ToolTip="Add row" AlternateText="Add row" ImageAlign="Left" OnClick="btnAddrow_Click" />--%>

            <telerik:RadGrid runat="server" ID="RGKienThuc" AllowAutomaticDeletes="True" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="Small"
                GridLines="None" Skin="Default" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                OnDeleteCommand="RGKienThuc_DeleteCommand" OnItemCommand="RGKienThuc_ItemCommand" OnNeedDataSource="RGKienThuc_NeedDataSource" OnItemDataBound="RGKienThuc_ItemDataBound">
                <ClientSettings>
                    <Scrolling AllowScroll="false" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID">

                    <CommandItemSettings AddNewRecordText="Add New Detail" />

                    <Columns>
                        <telerik:GridTemplateColumn DataField="ID" Display="false" HeaderText="ID" UniqueName="ID">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:HiddenField ID="HfID" runat="server" Value='<%# Eval("ID") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn DataField="IOnumber" HeaderText="IOnumber" UniqueName="IOnumber">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:Label ID="lbtIoNumber" runat="server" Text='<%# Eval("IOnumber") %>'> </asp:Label>

                            </ItemTemplate>
                        </telerik:GridTemplateColumn>


                        <telerik:GridTemplateColumn>
                            <HeaderTemplate>
                                <asp:Label ID="RGKienThuclbtSubgoal21"
                                    runat="server">Mã chi phí/Code</asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <telerik:RadComboBox ID="RadComboBox1" AutoPostBack="true" DataTextField="item" AutoCompleteSeparator="true" runat="server" DataValueField="ID" OnSelectedIndexChanged="Rdcode_SelectedIndexChanged"></telerik:RadComboBox>
                                <asp:HiddenField ID="Hfcode" runat="server" Value='<%# Eval("Accountcoding") %>' />

                                <asp:Label ID="Label1442" Text="*" Font-Size="X-Large" ForeColor="Red" runat="server"> </asp:Label>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                        </telerik:GridTemplateColumn>


                        <telerik:GridTemplateColumn>
                            <HeaderTemplate>
                                <asp:Label ID="RGKienThuclbtSubgoal21"
                                    runat="server">Diễn giải/Description</asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtDescription" Text='<%# Eval("Description") %>' runat="server"
                                    TextMode="MultiLine"></asp:TextBox>
                                <asp:Label ID="Label12" Text="*" Font-Size="X-Large" ForeColor="Red" runat="server"> </asp:Label>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                        </telerik:GridTemplateColumn>

                           <telerik:GridTemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label ID="RGKienThuclbtSubgoal21"
                                            runat="server">Nhãn hàng tặng/Brand free</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="rdCboBrandTang" Width="90px" AutoPostBack="true" runat="server" DataTextField="Brand" DataValueField="ID" OnSelectedIndexChanged="rdCboBrandTang_SelectedIndexChanged"></telerik:RadComboBox>
                                   
                                            <asp:HiddenField ID="HfBrandTang" runat="server" Value='<%# Eval("Nhanhang_fk_Tang") %>' />

                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle Width="10px" ForeColor="Red" />
                                    <HeaderStyle Width="10px" />
                                </telerik:GridTemplateColumn>


                                <telerik:GridTemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label ID="RGKienThuclbtSubgoal21tang"
                                            runat="server">Loại sản phẩm tặng/Category free</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="rdCboCategoryTang" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdCboCategoryTang_SelectedIndexChanged" DataTextField="Category" DataValueField="ID"></telerik:RadComboBox>
                                              <asp:HiddenField ID="HfCategoryTang" runat="server" Value='<%# Eval("Category_FK_Tang") %>' />
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle Width="10px" ForeColor="Red" />
                                    <HeaderStyle Width="10px" />
                                </telerik:GridTemplateColumn>



                        <telerik:GridTemplateColumn>
                            <HeaderTemplate>
                                <asp:Label ID="RGKienThuclbtSubgoal2" runat="server">Số lượng/Set</asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="rdnuumsoluong" Text='<%# Eval("Qty") %>' runat="server" Value="0" AutoPostBack="true"
                                    OnTextChanged="rdnuumsoluong_TextChanged">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>

                                <asp:Label ID="Label122" Text="*" Font-Size="X-Large" ForeColor="Red" runat="server"> </asp:Label>


                            </ItemTemplate>

                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn>
                            <HeaderTemplate>
                                <asp:Label ID="RGKienThuclbtSubgoal3" runat="server">Đơn giá/Unit Price</asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="rdnumdongia" Value="0" Text='<%# Eval("Price") %>' runat="server" AutoPostBack="true"
                                    OnTextChanged="rdnumdongia_TextChanged" MinValue="1">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                <asp:Label ID="Label121" Text="*" Font-Size="X-Large" ForeColor="Red" runat="server"> </asp:Label>
                            </ItemTemplate>

                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn>
                            <HeaderTemplate>
                                <asp:Label ID="RGKienThuclbtSubgoal4"
                                    runat="server">Thành tiền/Amount</asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="rdnumthanhtien" ReadOnly="true" Value="0" Text='<%# Eval("Amount") %>' runat="server">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>

                            </ItemTemplate>

                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="column">
                            <HeaderTemplate>
                                <asp:Label ID="RGKienThuclbtxoa" runat="server"></asp:Label>
                            </HeaderTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <EditFormSettings UserControlName="" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EditCommandColumn1">
                        </EditColumn>
                    </EditFormSettings>
                    <HeaderStyle BackColor="#ADD3F7"></HeaderStyle>
                </MasterTableView>
                <ActiveItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </telerik:RadGrid>
        </td>

    </tr>
    <tr>
        <td>Document's Transition History

           
                <telerik:RadGrid ID="RadGridHistory" Skin="Default" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="Small"
                    GridLines="Horizontal">
                    <MasterTableView>


                        <Columns>



                            <telerik:GridBoundColumn DataField="State" HeaderText="ASP State">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Note" HeaderText="Note">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Datesubmit" HeaderText="Submit date">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="dateAPP" HeaderText="Approval date">
                            </telerik:GridBoundColumn>

                            <%--   <telerik:GridBoundColumn DataField="N+2Status" HeaderText="Approve by Manager (N+2)">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="N+2Appoved Date" HeaderText="Approved date">
                </telerik:GridBoundColumn>--%>
                        </Columns>
                        <EditFormSettings UserControlName="" EditFormType="WebUserControl">
                        </EditFormSettings>
                    </MasterTableView>
                </telerik:RadGrid>
        </td>
    </tr>
</table>

