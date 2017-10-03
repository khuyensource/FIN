<%@ Page Title="ASPF" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ASPF.aspx.cs" Inherits="MaricoPay.ASPF" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<%@ Register Src="uc/CreateASPF.ascx" TagName="CreateASPF" TagPrefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //<![CDATA[
        function openWin() {
            var oWnd = radopen("AddASPF.aspx", "RadWindow1");
        }
        function OnClientClose() {
            __doPostBack('LoadGird', '');
        }
        function DoPostBack(obj) {
            __doPostBack(obj.id, 'OtherInformation');
        }
        function openRadWindowPreview(ID) {
            var manager = $find("<%= RadWindowManager1.ClientID %>");
            var oWnd = manager.open("ASPFDetail.aspx?Userid=" + ID, "RadWindowManager1");
            oWnd.center();
            oWnd.maximize();
        }

        function openRadprintPreview(ID) {
            var manager = $find("<%= RadWindowManager1.ClientID %>");
            var oWnd = manager.open("PrintASPF.aspx?ID=" + ID, "RadWindowManager1");
            oWnd.center();
            oWnd.maximize();
        }

        function openRadResive(ID) {
            var manager = $find("<%= RadWindowManager1.ClientID %>");
            var oWnd = manager.open("ASPFRevise.aspx?Userid=" + ID, "RadWindowManager1");
             oWnd.center();
             oWnd.maximize();
        }


        function RowDblClick(sender, eventArgs) {
            var dataKeyVal = eventArgs.getDataKeyValue("ID")
            sender.get_masterTableView().fireCommand("Copy", dataKeyVal);
        }


    </script>

    <telerik:RadWindowManager ShowContentDuringLoad="false" AutoSize="true" EnableShadow="true"
        ID="RadWindow1" runat="server" OnClientClose="OnClientClose" VisibleStatusbar="false"
        ReloadOnShow="true">
    </telerik:RadWindowManager>


    <telerik:RadWindowManager ShowContentDuringLoad="false" EnableShadow="true" ID="RadWindowManager1" Width="600px"
        runat="server" OnClientClose="OnClientClose" VisibleStatusbar="false" ReloadOnShow="true">
    </telerik:RadWindowManager>


    <asp:Button ID="btnAdd" runat="server" Text="Create" OnClientClick="openWin(); return false;"
        CausesValidation="false" Font-Bold="True" Font-Size="14px" />
    |
      <asp:Button ID="btnXoa" runat="server" Text="Delete" OnClientClick="return confirm('Do you want to delete this Recod ?');"
          CausesValidation="false" Font-Bold="True" Font-Size="14px" OnClick="btnXoa_Click" />


    </br>
        </br>
    <telerik:RadGrid ID="RadGrid2" Skin="Default" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="Small"
        GridLines="Horizontal" OnItemCommand="RadGrid2_ItemCommand" OnInsertCommand="RadGrid2_InsertCommand" OnItemCreated="RadGrid2_ItemCreated" OnItemDataBound="RadGrid2_ItemDataBound">

        <ClientSettings>
            <Selecting AllowRowSelect="true" />
        </ClientSettings>

        <MasterTableView DataKeyNames="ID" AutoGenerateColumns="false">

            <Columns>


                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>


                <telerik:GridTemplateColumn DataField="Status" Display="false" Groupable="False" HeaderText="Status"
                    UniqueName="Status">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemTemplate>
                        <asp:HiddenField ID="HFStatus" Value='<%# Eval("Status") %>' runat="server" />



                    </ItemTemplate>
                </telerik:GridTemplateColumn>




                <telerik:GridTemplateColumn DataField="ID" Groupable="False" HeaderText="ID"
                    UniqueName="ID">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemTemplate>
                        <asp:HiddenField ID="HFID" Value='<%# Eval("ID") %>' runat="server" />
                        <asp:HyperLink ID="LinkDetail" runat="server" Text='<%# Eval("ID") %>'></asp:HyperLink>
                         <asp:HiddenField ID="hdRevise" Value='<%# Eval("Revise") %>' runat="server" />

                    </ItemTemplate>
                </telerik:GridTemplateColumn>


                <telerik:GridTemplateColumn>
                    <ItemTemplate>

                        <asp:HyperLink ID="LinkPrint" runat="server" Text="Print"></asp:HyperLink>


                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                  <telerik:GridTemplateColumn UniqueName="Revise" HeaderText="Revise" >
                    <ItemTemplate>

                        <asp:HyperLink ID="LinkRevise" runat="server" Text="Revise"></asp:HyperLink>


                    </ItemTemplate>
                </telerik:GridTemplateColumn>


                <telerik:GridBoundColumn DataField="ASPNo" HeaderText="ASP No.">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="aspfvalue"  DataFormatString="{0:#,#}" HeaderText="ASP Value">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Objective" HeaderText="Objective">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="State" HeaderText="ASP State">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="datecreation"  DataFormatString="{0:dd-MMM-yyyy} " HeaderText="Date creation">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn HeaderText="Attached File View" DataField="AttachedFileView" UniqueName="AttachedFileView"
                    EmptyDataText="" AllowFiltering="false">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="60px"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn  >
                    <HeaderTemplate>
                        <asp:Label ID="btton" Text="Copy" runat="server"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton  CommandName="Copy"   CommandArgument="Copy"   ID="ImageButton1" ImageUrl="~/images/add.png" runat="server" OnClick="btnaddsubgoal_Click" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

            </Columns>

        </MasterTableView>
    </telerik:RadGrid>




    <%--  <uc1:CreateASPF ID="CreateASPF1" runat="server" />--%>
</asp:Content>
