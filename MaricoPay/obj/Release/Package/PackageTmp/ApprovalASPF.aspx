<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovalASPF.aspx.cs" Inherits="MaricoPay.ApprovalASPF" %>

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

        function openRadWindowPreview(ID, ActivationCode) {
            var manager = $find("<%= RadWindowManager1.ClientID %>");
             var oWnd = manager.open("ASPFApproval_Detail.aspx?Userid=" + ID + "&ActivationCode=" + ActivationCode, "RadWindowManager1");
             oWnd.center();
             oWnd.maximize();
         }
         function openRadWindowrevise(ID, revise) {
             var manager = $find("<%= RadWindowManager1.ClientID %>");
             var oWnd = manager.open("ASPFA_Detail_Revise.aspx?Userid=" + ID + "&Revise=" + revise, "RadWindowManager1");
             oWnd.center();
             oWnd.maximize();
         }

    </script>



    <telerik:RadWindowManager ShowContentDuringLoad="false" AutoSize="true" EnableShadow="true"
        ID="RadWindow1" runat="server" OnClientClose="OnClientClose" VisibleStatusbar="false"
        ReloadOnShow="true">
    </telerik:RadWindowManager>


    <telerik:RadWindowManager ShowContentDuringLoad="false" EnableShadow="true" ID="RadWindowManager1" Width="600px"
        runat="server" OnClientClose="OnClientClose" VisibleStatusbar="false" ReloadOnShow="true">
    </telerik:RadWindowManager>


    </br>
        </br>
    <telerik:RadGrid ID="RadGrid2" Skin="Default" runat="server" AutoGenerateColumns="False" AllowSorting="true"
        AllowFilteringByColumn="True" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
        GridLines="Horizontal" OnItemDataBound="RadGrid2_ItemDataBound" OnItemCommand="RadGrid2_ItemCommand">
        <MasterTableView DataKeyNames="ID" AutoGenerateColumns="false" AllowFilteringByColumn="True">

            <Columns>


                <telerik:GridTemplateColumn Display="false">
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


                <telerik:GridTemplateColumn UniqueName="Revise" HeaderText="Revise" AllowSorting="true">
                    <ItemTemplate>

                        <asp:HyperLink ID="LinkRevise" runat="server" Text=""></asp:HyperLink>
                        <asp:HiddenField ID="hdRevise" Value='<%# Eval("Revise") %>' runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn DataField="ID" HeaderText="ID" AllowSorting="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                    UniqueName="ID">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemTemplate   >
                        <asp:HiddenField ID="HFID" Value='<%# Eval("ID") %>' runat="server" />
                        <asp:HiddenField ID="HDActivecode" Value='<%# Eval("ApprovedCode") %>' runat="server" />
                        <asp:HyperLink ID="LinkDetail" runat="server" Text='<%# Eval("ID") %>'></asp:HyperLink>


                    </ItemTemplate>
                </telerik:GridTemplateColumn>




                <telerik:GridBoundColumn DataField="ASPNo" HeaderText="ASP No." AllowSorting="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>


                <telerik:GridBoundColumn DataField="aspfvalue" HeaderText="ASP Value" AllowSorting="true">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Objective" HeaderText="Objective" AllowSorting="true">
                </telerik:GridBoundColumn>
                <%--   <telerik:GridBoundColumn DataField="State" HeaderText="ASP State">
                </telerik:GridBoundColumn>--%>

                <telerik:GridBoundColumn DataField="Usercreate" HeaderText="User created" AllowSorting="true">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="datecreation" HeaderText="Date creation" AllowSorting="true">
                </telerik:GridBoundColumn>

            </Columns>

        </MasterTableView>
    </telerik:RadGrid>



</asp:Content>
