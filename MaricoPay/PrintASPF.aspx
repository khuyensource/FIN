<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintASPF.aspx.cs" Inherits="MaricoPay.PrintASPF" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <telerik:RadScriptManager ID="RadScriptManager2" runat="server">
    </telerik:RadScriptManager>
    <title></title>
    <style type="text/css">
        .auto-style5
        {
        }

        .auto-style9
        {
            height: 23px;
            font-weight: 700;
        }

        .auto-style12
        {
            height: 23px;
        }

        .auto-style13
        {
            width: 113px;
            height: 77px;
        }

        .auto-style14
        {
            text-align: center;
            font-weight: 700;
            font-size: medium;
        }

        .auto-style15
        {
            width: 168px;
            text-align: center;
        }

        .auto-style68
        {
            width: 103px;
            font-weight: bold;
        }

        .auto-style69
        {
            width: 103px;
        }

        .auto-style70
        {
            width: 103px;
            height: 23px;
        }

        .auto-style90
        {
            width: 154px;
        }

        .auto-style91
        {
            height: 23px;
            text-align: right;
            width: 154px;
        }

        .auto-style95
        {
            width: 168px;
            font-weight: 700;
        }

        .auto-style96
        {
            height: 23px;
            font-weight: 700;
            width: 168px;
        }

        .auto-style97
        {
            width: 244px;
        }

        .auto-style101
        {
            width: 269px;
        }
        .auto-style102
        {
            width: 244px;
            height: 23px;
        }
        .auto-style103
        {
            width: 269px;
            height: 23px;
        }
    </style>
</head>
<body>




    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnlPrintASPF" runat="server" Width="871px">

                <table>

                    <tr>
                        <td class="auto-style15">
                            <img alt="logoM" class="auto-style13" src="LogoMARICOSEA.png" width="10%" /></td>
                        <td class="auto-style14" colspan="3">YÊU CẦU PHÊ DUYỆT NGÂN SÁCH QUẢNG CÁO - KHUYẾN MÃI<br />
                            ASPF ( Advertising and Promotion Request)</td>
                    </tr>
                    <tr>
                        <td class="auto-style95">&nbsp;</td>
                        <td class="auto-style90">&nbsp;</td>
                        <td class="auto-style68">ASPF số :</td>
                        <td>
                            <asp:Label ID="lbtaspso0" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style95">&nbsp;</td>
                        <td class="auto-style90">&nbsp;</td>
                        <td class="auto-style68">Ngày :</td>
                        <td>
                            <asp:Label ID="ltNgaytao0" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style95">Country :</td>
                        <td class="auto-style90">
                            <asp:Label ID="ltCountry1" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style68">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style95">Budget ower :</td>
                        <td class="auto-style90">
                            <asp:Label ID="ltbudgetowner100000" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="auto-style95">Ngành hàng/ Bussiness Unit :</td>
                        <td class="auto-style90">
                            <asp:Label ID="ltBU1" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style68">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                    </tr>
                    <%--  <tr>
                <td class="auto-style8">Mã chi phí / Code :</td>
                <td>
                    <asp:Label ID="ltmachiphi" runat="server"  ></asp:Label>
                </td>
                <td class="auto-style18">Số tài khoản / GL code :</td>
                <td class="auto-style5">
                    <asp:Label ID="ltCode" runat="server"  ></asp:Label>
                </td>
            </tr>--%>
                    <tr>
                        <td class="auto-style95">Ngày thực hiện từ ngày / From :</td>
                        <td class="auto-style90">
                            <asp:Label ID="ltfrom" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style68">Đến ngày / To :</td>
                        <td>
                            <asp:Label ID="ltTo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style95">Áp dụng cho kênh / Channel :</td>
                        <td class="auto-style90">
                            <asp:Label ID="ltChannel" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style69">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style95">Mục tiêu / Objective :</td>
                        <td class="auto-style91" style="text-align: right">
                            <asp:Label ID="ltMuctieu" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style69">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style95">ASPF value (VND): </td>
                        <td class="auto-style91" style="text-align: right">
                            <asp:Label ID="ltaspfvalue" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style69">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style9" colspan="4">

                            <asp:GridView ID="GridView1" runat="server" BorderColor="#000066" BorderWidth="2px" AutoGenerateColumns="False">
                                <Columns>
                                      <asp:BoundField DataField="IOnumber" HeaderText="IOnumber" />
                                      <asp:BoundField DataField="Region" HeaderText="Vùng/Region" />
                                    <asp:BoundField DataField="Item" HeaderText="Mã chi phí/Code" />
                                      <asp:BoundField DataField="Brand" HeaderText="Nhãn hàng/Brand" />
                                    <asp:BoundField DataField="Category" HeaderText="Chủng Loại/Category" />
                                    <asp:BoundField DataField="Description" HeaderText="Diễn giải/Description" />
                                   
                                    <asp:BoundField DataField="Amount" DataFormatString="{0:0,0}" HeaderText="Thành tiền/ Amount (VND)">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>

                                       <asp:BoundField DataField="Budget" DataFormatString="{0:0,0}" HeaderText="Ngân sách còn lại/ Budget available(VND)">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>

                                </Columns>
                            </asp:GridView>
                            <%--                    <telerik:RadGrid runat="server" ID="RGKienThuc" AllowAutomaticDeletes="True" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="Small" 
              GridLines="None" Skin="Default"    AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
      ><ClientSettings><Scrolling AllowScroll="false" /></ClientSettings><MasterTableView DataKeyNames="ID" ><CommandItemSettings AddNewRecordText="Add New Detail" /><Columns><telerik:GridTemplateColumn DataField="ID" Display="false"  HeaderText="ID" UniqueName="ID"><HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" /><ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" /><ItemTemplate><asp:HiddenField ID="HfID" runat="server" Value='<%# Eval("ID") %>' /></ItemTemplate></telerik:GridTemplateColumn><telerik:GridTemplateColumn><HeaderTemplate><asp:Label ID="RGKienThuclbtSubgoal21"  
                                    runat="server" >Diễn giải/Description</asp:Label></HeaderTemplate><ItemTemplate><asp:TextBox ID="txtDescription"   Text='<%# Eval("Description") %>' runat="server"
                                    TextMode="MultiLine"></asp:TextBox></ItemTemplate><FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" /></telerik:GridTemplateColumn><telerik:GridTemplateColumn><HeaderTemplate><asp:Label ID="RGKienThuclbtSubgoal2" runat="server">Số lượng/Set</asp:Label></HeaderTemplate><ItemTemplate>

                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Qty") %>' ></asp:Label>
                                      

                                       </ItemTemplate></telerik:GridTemplateColumn><telerik:GridTemplateColumn><HeaderTemplate><asp:Label ID="RGKienThuclbtSubgoal3"  runat="server">Đơn giá/Unit Price</asp:Label></HeaderTemplate><ItemTemplate>
                                         
                                                                <asp:Label ID="ltPrice" runat="server" Text='<%# Eval("Price") %>' ></asp:Label>

                                                                                                                                                                                                                                     </ItemTemplate></telerik:GridTemplateColumn><telerik:GridTemplateColumn><HeaderTemplate><asp:Label ID="RGKienThuclbtSubgoal4" 
                                    runat="server">Thành tiền/Amount</asp:Label></HeaderTemplate><ItemTemplate>
                                          <asp:Label ID="ltAmount" runat="server" Text='<%# Eval("Amount") %>' ></asp:Label>

                                      

                                                                                                   </ItemTemplate>

                                                                                                                                                       </telerik:GridTemplateColumn><telerik:GridTemplateColumn  UniqueName="column"><HeaderTemplate><asp:Label ID="RGKienThuclbtxoa"  runat="server"></asp:Label></HeaderTemplate><ItemTemplate><asp:ImageButton OnClientClick="return confirm('Do you want to delete this Recod ?');"
                                    CommandName="Delete" ID="btXoa" runat="server" ImageUrl="~/Images/delete.png" /></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" /><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /></telerik:GridTemplateColumn></Columns><EditFormSettings UserControlName="" EditFormType="WebUserControl"><EditColumn UniqueName="EditCommandColumn1"></EditColumn></EditFormSettings><HeaderStyle BackColor="#ADD3F7" ></HeaderStyle></MasterTableView><ActiveItemStyle HorizontalAlign="Left" VerticalAlign="Middle" /></telerik:RadGrid>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style9" colspan="4">&nbsp;</td>
                    </tr>

                </table>
                <table style="width: 872px">
                    <tr>

                        <td>Requested by 
                        </td>
                        <td>Budget confirmed </td>

                        <td class="auto-style101">Review and confirmed</td>
                    </tr>


                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td class="auto-style101">
                            &nbsp;</td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                        <td style="text-align: left">
                            <asp:Literal ID="ltBudgetcontrol" runat="server"></asp:Literal>
                        </td>
                        <td class="auto-style101">
                            <asp:Literal ID="ltReview" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="ltNamecreation0" runat="server" CssClass="auto-style24"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbtApp8" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style101">
                            <asp:Label ID="lbtBudget0" runat="server" Style="text-align: right"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="ltNamecreation" runat="server" CssClass="auto-style24"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbtApp1" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style101">
                            <asp:Label ID="lbtBudget" runat="server" Style="text-align: right"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="3"></td>
                    </tr>

                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>

                    <tr>

                        <td>Approved name</td>
                        <td>Approved name</td>
                        <td class="auto-style101">Approved name</td>
                    </tr>
                    <tr>
                        <td class="auto-style97">
                            &nbsp;</td>
                        <td class="auto-style94">
                            &nbsp;</td>
                        <td class="auto-style101">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style102">
                            <asp:Literal ID="ltBudgetcontrol4" runat="server"></asp:Literal>
                        </td>
                        <td class="auto-style12">
                            <asp:Literal ID="ltBudgetcontrol5" runat="server"></asp:Literal>
                        </td>
                        <td class="auto-style103">
                            <asp:Literal ID="ltBudgetcontrol6" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style97">
                            <asp:Label ID="lbtapp9" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style94">
                            <asp:Label ID="lbtapp10" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style101">
                            <asp:Label ID="lbtapp11" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style97">
                            <asp:Label ID="lbtapp2" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style94">
                            <asp:Label ID="lbtapp3" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style101">
                            <asp:Label ID="lbtapp4" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                    </tr>


                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>


                    <tr>
                        <td >Approved name</td>
                        <td >Approved name</td>
                        <td >Approved name</td>
                    </tr>
                     <tr>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltBudgetcontrol7" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="ltBudgetcontrol8" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="ltBudgetcontrol9" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbtapp12" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbtapp13" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbtapp14" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbtapp5" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbtapp6" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbtapp7" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
