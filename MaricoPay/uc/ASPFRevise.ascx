﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ASPFRevise.ascx.cs" Inherits="MaricoPay.uc.ASPFRevise" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
 <script type="text/javascript">

     function GetRadWindow()
     {
         var oWindow = null;
         if (window.radWindow) oWindow = window.radWindow;
         else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
         return oWindow;
     }

     function CloseDialog()
     {
         GetRadWindow().close();
         return false;
     }

     function CloseWindow11() {
         window.open('', '_self', '');
         window.close();
     }


     function OnClientClose() {
         __doPostBack('fLoad', '');
     }
    </script>

<telerik:RadScriptManager ID="RadScriptManager1" Runat="server">
                    </telerik:RadScriptManager>
  <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
      </telerik:RadAjaxManager>
   
      <uc4:uscMsgBox ID="MsgBox1" runat="server" />
     


<table>
    <tr>
        <td>Budget owner
        </td>
        <td>
           <telerik:RadComboBox ID="rdNguoiduyet" runat="server" DataTextField="Fullname" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="rdNguoiduyet_SelectedIndexChanged"></telerik:RadComboBox>
            <asp:HiddenField ID="hfBudgetowner" runat="server" />



        </td>
        <td>Quốc gia/Country 
        </td>
        <td>

            <telerik:RadComboBox ID="Rdcbocountry" runat="server" DataTextField="Country" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="Rdcbocountry_SelectedIndexChanged"></telerik:RadComboBox>
            <asp:Label ID="Label2" Text="*" Font-Size="X-Large" ForeColor="Red" runat="server"> </asp:Label>
        </td>

    </tr>

    <tr>
        <td>Ngày/Date of creation  
        </td>
        <td>

            <telerik:RadDatePicker ID="rddateCreation" runat="server" Culture="en-US" ViewStateMode="Disabled">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"></Calendar>

                <DateInput DisplayDateFormat="dd-MMM-yyyy" DateFormat="dd-MMM-yyyy" LabelWidth="40%" Width="160px">
                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>

                    <ReadOnlyStyle Resize="None"></ReadOnlyStyle>

                    <FocusedStyle Resize="None"></FocusedStyle>

                    <DisabledStyle Resize="None"></DisabledStyle>

                    <InvalidStyle Resize="None"></InvalidStyle>

                    <HoveredStyle Resize="None"></HoveredStyle>

                    <EnabledStyle Resize="None"></EnabledStyle>
                </DateInput>

                <DatePopupButton ImageUrl="" HoverImageUrl="" Visible="False"></DatePopupButton>
            </telerik:RadDatePicker>
            <asp:Label ID="Label1" Text="*" Font-Size="X-Large" ForeColor="Red"
                runat="server"> </asp:Label>

        </td>
        <td>Ngành hàng/Business unit 
        </td>
        <td>

            <telerik:RadComboBox ID="rdcboNganhHang" runat="server" AutoPostBack="true" DataTextField="BU" DataValueField="ID" OnSelectedIndexChanged="rdcboNganhHang_SelectedIndexChanged"></telerik:RadComboBox>
            <asp:Label ID="Label4" Text="*" Font-Size="X-Large" ForeColor="Red" runat="server"> </asp:Label>
        </td>

    </tr>

   

    <tr>
        <td>Ngày thực hiện từ ngày/From
        </td>
        <td>


            <telerik:RadDatePicker ID="RddatetimeFrom" runat="server" Culture="en-US" AutoPostBack="True" OnSelectedDateChanged="RddatetimeFrom_SelectedDateChanged">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"></Calendar>

                <DateInput DisplayDateFormat="dd-MMM-yyyy" DateFormat="dd-MMM-yyyy" LabelWidth="40%" AutoPostBack="True">
                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>

                    <ReadOnlyStyle Resize="None"></ReadOnlyStyle>

                    <FocusedStyle Resize="None"></FocusedStyle>

                    <DisabledStyle Resize="None"></DisabledStyle>

                    <InvalidStyle Resize="None"></InvalidStyle>

                    <HoveredStyle Resize="None"></HoveredStyle>

                    <EnabledStyle Resize="None"></EnabledStyle>
                </DateInput>

                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
            </telerik:RadDatePicker>
            <asp:Label ID="Label12" Text="*" Font-Size="X-Large" ForeColor="Red" runat="server"> </asp:Label>
        </td>
        <td>Đến ngày/To 
        </td>
        <td>

            <telerik:RadDatePicker ID="RddatetimeTo" runat="server" Culture="en-US" AutoPostBack="True" OnSelectedDateChanged="RddatetimeTo_SelectedDateChanged">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"></Calendar>

                <DateInput DisplayDateFormat="dd-MMM-yyyy" DateFormat="dd-MMM-yyyy" LabelWidth="40%" AutoPostBack="True">
                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>

                    <ReadOnlyStyle Resize="None"></ReadOnlyStyle>

                    <FocusedStyle Resize="None"></FocusedStyle>

                    <DisabledStyle Resize="None"></DisabledStyle>

                    <InvalidStyle Resize="None"></InvalidStyle>

                    <HoveredStyle Resize="None"></HoveredStyle>

                    <EnabledStyle Resize="None"></EnabledStyle>
                </DateInput>

                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
            </telerik:RadDatePicker>
            <asp:Label ID="Label8" Text="*" Font-Size="X-Large" ForeColor="Red" runat="server"> </asp:Label>
        </td>

    </tr>


    <tr>
        <td>Áp dụng cho kênh/ Channel
        </td>
        <td>


            <telerik:RadComboBox ID="rdChannel" runat="server" DataTextField="Channel" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="rdChannel_SelectedIndexChanged"></telerik:RadComboBox>
            <asp:Label ID="Label9" Text="*" Font-Size="X-Large" ForeColor="Red" runat="server"> </asp:Label>

        </td>

        <td>Mục tiêu/Objective 
        </td>
        <td>
            <asp:TextBox runat="server" ID="txtObjective" TextMode="MultiLine" Width="300px"> </asp:TextBox>
            <asp:Label ID="Label7" Text="*" Font-Size="X-Large" ForeColor="Red" runat="server"> </asp:Label>
        </td>

    </tr>

    <tr>
        <td>ASPF Value
        </td>
        <td>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <telerik:RadNumericTextBox ID="radnumtxtASPFvalue" Value="0" ReadOnly="true" AutoPostBack="true" NumberFormat-DecimalSeparator="," Type="Number" runat="server"   >
                        <NumberFormat DecimalDigits="0" />
                    </telerik:RadNumericTextBox>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%--                    <telerik:RadNumericTextBox ID="rdFYBudgetCAT" Value="0"  NumberFormat-DecimalSeparator=","  Type="Number" AutoPostBack="true" runat ="server"   OnTextChanged="rdFYBudgetCAT_TextChanged"   >
                           <NumberFormat DecimalDigits="0" />

                    </telerik:RadNumericTextBox>

                  <asp:Label ID="Label10"  Text="*" Font-Size="X-Large" ForeColor="Red" runat="server"> </asp:Label>--%>
             

        </td>
        <td>Loại ngân sách/Type of budget</td>
        <td>

            <telerik:RadComboBox ID="rdTypeofBudget" runat="server" DataTextField="Type" DataValueField="ID" OnSelectedIndexChanged="rdTypeofBudget_SelectedIndexChanged"></telerik:RadComboBox>

        </td>

    </tr>


    <%--   <tr>
                <td>
                 ASPF Value 
                    (VND)</td>
                <td>

                 
            
                    
                    
              </td>
               <td>
                 Available budget

                    (VND)</td>
                <td>
                  <telerik:RadNumericTextBox ID="rdAvailable" Value="0" runat="server" AutoPostBack="true"   NumberFormat-DecimalSeparator=","  Type="Number"   OnTextChanged="rdAvailable_TextChanged">
                         <NumberFormat DecimalDigits="0" />
                  </telerik:RadNumericTextBox>
                     <asp:Label ID="Label11"  Text="*" Font-Size="X-Large" ForeColor="Red" runat="server"> </asp:Label>
              </td>
               

          </tr>--%>

    <tr>
        <td>File đính kèm / Attach file
        </td>
        <td>
            <asp:FileUpload ID="FileUpload1" runat="server" />


        </td>
        <td>
            &nbsp;</td>
        <%-- <td>
                 Budget balance 
                    (VND)</td>
                <td>
                     <telerik:RadNumericTextBox ID="radnumtxtBudgetBalance" Value="0"  ReadOnly="true"  AutoPostBack="true" 
                           NumberFormat-DecimalSeparator=","  Type="Number" runat ="server" OnTextChanged="radnumtxtBudgetBalance_TextChanged1" >
                            <NumberFormat DecimalDigits="0" />
                     </telerik:RadNumericTextBox> 
                    

                 
              </td>--%>
    </tr>


</table>

-----------------------------------------------
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Font-Bold="True"   Font-Size="14px" />
--
     <asp:Button ID="btnSubmit" runat="server" Text="Save & Submit" OnClick="btnSubmit_Click"    Font-Bold="True" Font-Size="14px" />
--
 <asp:Button ID="Button1" runat="server" Text="Close" OnClick="Button1_Click" Font-Bold="True" Font-Size="14px"
     OnClientClick="return CloseDialog();" CommandName="Close" />



-----------------------------------------------
         

<asp:UpdatePanel ID="UpdatePanel1222" runat="server">
    <ContentTemplate>


    <table width="900px">

        <tr>

            <td>
               
                <telerik:RadGrid runat="server" ID="RGKienThuc" AllowAutomaticDeletes="True" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="Small" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                    OnDeleteCommand="RGKienThuc_DeleteCommand" OnItemCommand="RGKienThuc_ItemCommand" OnNeedDataSource="RGKienThuc_NeedDataSource" OnItemDataBound="RGKienThuc_ItemDataBound" Width="1024px" OnItemCreated="RGKienThuc_ItemCreated">
                    <ClientSettings>
                        <Scrolling AllowScroll="false" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID">

                        <CommandItemSettings AddNewRecordText="Add New Detail" />

                        <Columns>
                            <telerik:GridTemplateColumn DataField="ID" Display="false" HeaderText="ID" UniqueName="ID">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="0px" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:HiddenField ID="HfID" runat="server" Value='<%# Eval("ID") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            
                                <telerik:GridTemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label ID="RGKienThuclbtSubgoal21"
                                            runat="server">Vùng/Region</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="RDregion" DataTextField="Region" Width="80px" AutoCompleteSeparator="true" runat="server" DataValueField="ID"></telerik:RadComboBox>
                                          <asp:HiddenField ID="HfRDregion" runat="server" Value='<%# Eval("Region") %>' />
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle Width="10px" ForeColor="Red" />
                                    <HeaderStyle Width="10px" />
                                </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="RGKienThuclbtSubgoal21"
                                        runat="server">Mã chi phí/Code</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:RadComboBox ID="RadComboBox1" DataTextField="item"  AutoPostBack="true" AutoCompleteSeparator="true" runat="server" DataValueField="ID" OnSelectedIndexChanged="Rdcode_SelectedIndexChanged"></telerik:RadComboBox>
                                    <asp:HiddenField ID="Hfcode" runat="server" Value='<%# Eval("AccountCoding") %>' />


                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="10px" ForeColor="Red" />
                                <HeaderStyle Width="10px" />
                            </telerik:GridTemplateColumn>


                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="RGKienThuclbtSubgoal21"
                                        runat="server">Nhãn hàng/Brand</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:RadComboBox ID="rdCboBrand" Width="90px" AutoPostBack="true" runat="server" DataTextField="Brand" DataValueField="ID" OnSelectedIndexChanged="rdCboBrand_SelectedIndexChanged"></telerik:RadComboBox>
                                    <asp:HiddenField ID="HfBrand" runat="server" Value='<%# Eval("Nhanhang_fk") %>' />
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="10px" ForeColor="Red" />
                                <HeaderStyle Width="10px" />
                            </telerik:GridTemplateColumn>


                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="RGKienThuclbtSubgoal21"
                                        runat="server">Loại sản phẩm/Category</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:RadComboBox ID="rdCboCategory" runat="server"  AutoPostBack="true"  OnSelectedIndexChanged="rdCboCategory_SelectedIndexChanged" DataTextField="Category" DataValueField="ID"></telerik:RadComboBox>
                                    <asp:HiddenField ID="HfCategory" runat="server" Value='<%# Eval("Category_FK") %>' />
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="10px" ForeColor="Red" />
                                <HeaderStyle Width="10px" />
                            </telerik:GridTemplateColumn>




                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="RGKienThuclbtSubgoal21"
                                        runat="server">Diễn giải/Description</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDescription" Text='<%# Eval("Description") %>' runat="server"
                                        TextMode="MultiLine"></asp:TextBox>
                                    <asp:Label ID="Label12" Text="*" Font-Size="10px" ForeColor="Red" runat="server"> </asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="10px" />
                                <HeaderStyle Width="10px" />

                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="Q1" Display="false">
                                <HeaderTemplate>
                                    <asp:Label ID="RGKienThuclbtSubgoal4"
                                        runat="server">Budget allocation Q1</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>




                                    <telerik:RadNumericTextBox ID="rdnumthanhtienQ1" OnTextChanged="rdnumthanhtienQ1_TextChanged" Text='<%# Eval("AmountQ1") %>'
                                        AutoPostBack="true" Value="0" runat="server" Width="90px"  >
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>

                                </ItemTemplate>
                                <ItemStyle Width="10px" />
                                <HeaderStyle Width="10px" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="Q2" Display="false">
                                <HeaderTemplate>
                                    <asp:Label ID="RGKienThuclbtSubgoal4Q2"
                                        runat="server">Budget allocation Q2</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="rdnumthanhtienQ2" AutoPostBack="true" Value="0" Text='<%# Eval("AmountQ2") %>' runat="server"
                                        Width="90px"   OnTextChanged="rdnumthanhtienQ2_TextChanged">
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>

                                </ItemTemplate>
                                <ItemStyle Width="10px" />
                                <HeaderStyle Width="10px" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="Q3" Display="false">
                                <HeaderTemplate>
                                    <asp:Label ID="RGKienThuclbtSubgoal4Q3"
                                        runat="server">Budget allocation Q3</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="rdnumthanhtienQ3" AutoPostBack="true" Value="0" Text='<%# Eval("AmountQ3") %>' runat="server"
                                        Width="90px"   OnTextChanged="rdnumthanhtienQ3_TextChanged">
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>

                                </ItemTemplate>
                                <ItemStyle Width="10px" />
                                <HeaderStyle Width="10px" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="Q4" Display="false">
                                <HeaderTemplate>
                                    <asp:Label ID="RGKienThuclbtSubgoal4Q4"
                                        runat="server">Budget allocation Q4</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="rdnumthanhtienQ4" AutoPostBack="true" Value="0" Text='<%# Eval("AmountQ4") %>' runat="server"
                                        Width="90px"   OnTextChanged="rdnumthanhtienQ4_TextChanged">
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>

                                </ItemTemplate>
                                <ItemStyle Width="10px" />
                                <HeaderStyle Width="10px" />
                            </telerik:GridTemplateColumn>


                            <telerik:GridTemplateColumn UniqueName="A1" Display="false">
                                <HeaderTemplate>
                                    <asp:Label ID="RGKienThuclbtSubgoal4Q5"
                                        runat="server">Budget available Q1</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="A1" ReadOnly="true" Value="0" Text='<%# Eval("Amount") %>' runat="server"
                                        Width="90px"  >
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>

                                </ItemTemplate>
                                <ItemStyle Width="10px" />
                                <HeaderStyle Width="10px" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="A2" Display="false">
                                <HeaderTemplate>
                                    <asp:Label ID="RGKienThuclbtSubgoal4Q5"
                                        runat="server">Budget available Q2</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="A2" ReadOnly="true" Value="0" Text='<%# Eval("Amount") %>' runat="server"
                                        Width="90px"  >
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>

                                </ItemTemplate>
                                <ItemStyle Width="10px" />
                                <HeaderStyle Width="10px" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="A3" Display="false">
                                <HeaderTemplate>
                                    <asp:Label ID="RGKienThuclbtSubgoal4Q5"
                                        runat="server">Budget available Q3</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="A3" ReadOnly="true" Value="0" Text='<%# Eval("Amount") %>' runat="server"
                                        Width="90px"  >
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>

                                </ItemTemplate>
                                <ItemStyle Width="10px" />
                                <HeaderStyle Width="10px" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="A4" Display="false">
                                <HeaderTemplate>
                                    <asp:Label ID="RGKienThuclbtSubgoal4Q5"
                                        runat="server">Budget available Q4</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="A4" ReadOnly="true" Value="0" Text='<%# Eval("Amount") %>' runat="server"
                                        Width="90px"  >
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>

                                </ItemTemplate>
                                <ItemStyle Width="10px" />
                                <HeaderStyle Width="10px" />
                            </telerik:GridTemplateColumn>

                                 <telerik:GridTemplateColumn UniqueName="M1" Display="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="RGKienThuclbtSubgoal4Q5"
                                            runat="server">Jan/Tháng 1</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="M1" AutoPostBack="true" Value="0" Text='<%# Eval("M1") %>' runat="server"  OnTextChanged="M1_TextChanged"
                                            Width="90px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                    <HeaderStyle Width="10px" />
                                </telerik:GridTemplateColumn>


                                <telerik:GridTemplateColumn UniqueName="M2" Display="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="RGKienThuclbtSubgoal4Q5"
                                            runat="server">Feb/Tháng 2</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="M2" AutoPostBack="true" Value="0" Text='<%# Eval("M2") %>' runat="server" OnTextChanged="M2_TextChanged"
                                            Width="90px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                    <HeaderStyle Width="10px" />
                                </telerik:GridTemplateColumn>


                                <telerik:GridTemplateColumn UniqueName="M3" Display="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="RGKienThuclbtSubgoal4Q5"
                                            runat="server">Mar/Tháng 3</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="M3" AutoPostBack="true" Value="0" Text='<%# Eval("M3") %>' runat="server" OnTextChanged="M3_TextChanged"
                                            Width="90px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                    <HeaderStyle Width="10px" />
                                </telerik:GridTemplateColumn>


                                <telerik:GridTemplateColumn UniqueName="M4" Display="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="RGKienThuclbtSubgoal4Q5"
                                            runat="server">Apr/Tháng 4</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="M4" AutoPostBack="true" Value="0" Text='<%# Eval("M4") %>' runat="server" OnTextChanged="M4_TextChanged"
                                            Width="90px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                    <HeaderStyle Width="10px" />
                                </telerik:GridTemplateColumn>


                                <telerik:GridTemplateColumn UniqueName="M5" Display="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="RGKienThuclbtSubgoal4Q5"
                                            runat="server">May/Tháng 5</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="M5" AutoPostBack="true" Value="0" Text='<%# Eval("M5") %>' runat="server" OnTextChanged="M5_TextChanged"
                                            Width="90px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                    <HeaderStyle Width="10px" />
                                </telerik:GridTemplateColumn>


                                <telerik:GridTemplateColumn UniqueName="M6" Display="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="RGKienThuclbtSubgoal4Q5"
                                            runat="server">Jun/Tháng 6</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="M6" AutoPostBack="true" Value="0" Text='<%# Eval("M6") %>' runat="server" OnTextChanged="M6_TextChanged"
                                            Width="90px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                    <HeaderStyle Width="10px" />
                                </telerik:GridTemplateColumn>


                                <telerik:GridTemplateColumn UniqueName="M7" Display="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="RGKienThuclbtSubgoal4Q5"
                                            runat="server">Jul/Tháng 7</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="M7" AutoPostBack="true" Value="0" Text='<%# Eval("M7") %>' runat="server" OnTextChanged="M7_TextChanged"
                                            Width="90px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                    <HeaderStyle Width="10px" />
                                </telerik:GridTemplateColumn>


                                <telerik:GridTemplateColumn UniqueName="M8" Display="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="RGKienThuclbtSubgoal4Q5"
                                            runat="server">Aug/Tháng 8</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="M8" AutoPostBack="true" Value="0" Text='<%# Eval("M8") %>' runat="server" OnTextChanged="M8_TextChanged"
                                            Width="90px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                    <HeaderStyle Width="10px" />
                                </telerik:GridTemplateColumn>


                                <telerik:GridTemplateColumn UniqueName="M9" Display="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="RGKienThuclbtSubgoal4Q5"
                                            runat="server">Sep/Tháng 9</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="M9" AutoPostBack="true" Value="0" Text='<%# Eval("M9") %>' runat="server" OnTextChanged="M9_TextChanged"
                                            Width="90px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                    <HeaderStyle Width="10px" />
                                </telerik:GridTemplateColumn>


                                <telerik:GridTemplateColumn UniqueName="M10" Display="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="RGKienThuclbtSubgoal4Q5"
                                            runat="server">Oct/Tháng 10</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="M10"  AutoPostBack="true" Value="0" Text='<%# Eval("M10") %>' runat="server" OnTextChanged="M10_TextChanged"
                                            Width="90px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                    <HeaderStyle Width="10px" />
                                </telerik:GridTemplateColumn>


                                <telerik:GridTemplateColumn UniqueName="M11" Display="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="RGKienThuclbtSubgoal4Q5"
                                            runat="server">Nov/Tháng 11</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="M11" AutoPostBack="true" Value="0" Text='<%# Eval("M11") %>' runat="server" OnTextChanged="M11_TextChanged"
                                            Width="90px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                    <HeaderStyle Width="10px" />
                                </telerik:GridTemplateColumn>


                                <telerik:GridTemplateColumn UniqueName="M12" Display="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="RGKienThuclbtSubgoal4Q5"
                                            runat="server">Dec/Tháng 12</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="M12" AutoPostBack="true" Value="0" Text='<%# Eval("M12") %>' runat="server" OnTextChanged="M12_TextChanged"
                                            Width="90px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                    <HeaderStyle Width="10px" />
                                </telerik:GridTemplateColumn>


                            <telerik:GridTemplateColumn UniqueName="column">
                                <HeaderTemplate>
                                    <asp:Label ID="RGKienThuclbtxoa" runat="server"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton OnClientClick="return confirm('Do you want to delete this Recod ?');"
                                        CommandName="Delete" ID="btXoa" runat="server" ImageUrl="~/Images/delete.png" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="btton" Text="Add" runat="server"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/add.png" runat="server" OnClick="btnaddsubgoal_Click" />
                                </ItemTemplate>
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

        </ContentTemplate>
    </asp:UpdatePanel>


        