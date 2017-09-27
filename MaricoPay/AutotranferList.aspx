<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AutotranferList.aspx.cs" Inherits="MaricoPay.AutotranferList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //<![CDATA[
        function openWin() {
            var oWnd = radopen("TransferBudget.aspx", "RadWindow1");
        }
        function OnClientClose() {
            __doPostBack('LoadGird', '');
        }

        function openRadWindowPreview(ID, ActivationCode) {
            var manager = $find("<%= RadWindowManager1.ClientID %>");
              var oWnd = manager.open("ASPFApproval_Detail.aspx?Userid=" + ID + "&ActivationCode=" + ActivationCode, "RadWindowManager1");
              oWnd.center();
              oWnd.maximize();
          }

    </script>

    <telerik:RadWindowManager ShowContentDuringLoad="false" EnableShadow="true" Width="900px" Height="400px"
        ID="RadWindow1" runat="server" OnClientClose="OnClientClose" VisibleStatusbar="false"
        ReloadOnShow="true">
    </telerik:RadWindowManager>


    <telerik:RadWindowManager ShowContentDuringLoad="false" EnableShadow="true" ID="RadWindowManager1" Width="900px"
        runat="server" OnClientClose="OnClientClose" VisibleStatusbar="false" ReloadOnShow="true">
    </telerik:RadWindowManager>


    </br>
        </br>
        <asp:Button ID="btnAdd" runat="server" Text="Create" OnClientClick="openWin(); return false;"
        CausesValidation="false" Font-Bold="True" Font-Size="14px" />
    |
      <asp:Button ID="btnXoa" runat="server" Text="Delete" OnClientClick="return confirm('Do you want to delete this Recod ?');"
          CausesValidation="false" Font-Bold="True" Font-Size="14px" OnClick="btnXoa_Click"  />
   <%-- OnClick="btnXoa_Click"--%>

    <telerik:RadGrid ID="RadGrid2" Skin="Default" runat="server" AutoGenerateColumns="False"
        GridLines="Horizontal" >
        <MasterTableView DataKeyNames="ID" AutoGenerateColumns="false">

            <Columns>


                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>


                <%--                 <telerik:GridTemplateColumn DataField="Status" Display="false"  Groupable="False" HeaderText="Status"
                    UniqueName="Status">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemTemplate>
                        <asp:HiddenField ID="HFStatus" Value='<%# Eval("Status") %>'  runat="server" />
                      
                      
                       
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>




                <telerik:GridTemplateColumn DataField="ID" Groupable="False" HeaderText="ID"
                    UniqueName="ID">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemTemplate>
                        <asp:HiddenField ID="HFID" Value='<%# Eval("ID") %>' runat="server" />
                   
                        <asp:HyperLink ID="LinkDetail" runat="server" Text='<%# Eval("ID") %>'></asp:HyperLink>


                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn DataField="Country_from" HeaderText="Country_from">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Function_from" HeaderText="Function_from">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Brand_from" HeaderText="Brand_from">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Country_from" HeaderText="Country_from">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Function_from" HeaderText="Function_from">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Brand_from" HeaderText="Brand_from">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Category_From" HeaderText="Category_From">
                </telerik:GridBoundColumn>


                <telerik:GridBoundColumn DataField="Function_from" HeaderText="Function_from">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="ActivityGroup_from" HeaderText="ActivityGroup_from">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Country_To" HeaderText="Country_To">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Function_To" HeaderText="Function_To">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Brand_To" HeaderText="Brand_To">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Category_To" HeaderText="Category_To">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Country_To" HeaderText="Country_To">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Function_To" HeaderText="Function_To">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ActivityGroup_To" HeaderText="ActivityGroup_To">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Budget_transfer" HeaderText="Budget_transfer">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Approved" HeaderText="Approved">
                </telerik:GridBoundColumn>



            </Columns>

        </MasterTableView>
    </telerik:RadGrid>



</asp:Content>
