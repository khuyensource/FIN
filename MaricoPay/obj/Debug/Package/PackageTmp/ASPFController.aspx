<%@ Page Title="ASPF" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ASPFController.aspx.cs" Inherits="MaricoPay.ASPFController" %>

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

        function openRadWindowPreview(ID) {
            var manager = $find("<%= RadWindowManager1.ClientID %>");
            var oWnd = manager.open("BudgetChecking.aspx?Userid=" + ID, "RadWindowManager1");
             oWnd.center();
             oWnd.maximize();
        }

          </script>

    
      <telerik:RadWindowManager ShowContentDuringLoad="false" AutoSize="true" EnableShadow="true"  
        ID="RadWindow1" runat="server" OnClientClose="OnClientClose" VisibleStatusbar="false"
        ReloadOnShow="true" >
    </telerik:RadWindowManager>


     <telerik:RadWindowManager ShowContentDuringLoad="false" EnableShadow="true" ID="RadWindowManager1" 
        runat="server" AutoSize="true" OnClientClose="OnClientClose" VisibleStatusbar="false" ReloadOnShow="true"
        >
    </telerik:RadWindowManager>


      </br>
        </br>
    <telerik:RadGrid ID="RadGrid2" Skin="Default" runat="server" AutoGenerateColumns="False"  PageSize="10" AllowPaging="true"   Font-Names="Arial" Font-Size="Small"    AllowSorting="True"  OnPageIndexChanged="RadGrid2_PageIndexChanged"
                OnPageSizeChanged="RadGrid2_PageSizeChanged"    AllowFilteringByColumn="True" AutoPostBackOnFilter="true"
        GridLines="Horizontal" OnItemCommand="RadGrid2_ItemCommand" OnInsertCommand="RadGrid2_InsertCommand" OnItemCreated="RadGrid2_ItemCreated" OnItemDataBound="RadGrid2_ItemDataBound" OnSortCommand="RadGrid2_SortCommand">
        <MasterTableView DataKeyNames="ID"  AutoGenerateColumns="false"   AllowMultiColumnSorting="true"  AllowFilteringByColumn="True" ShowFooter="True" >

           
        
            <Columns>


                <telerik:GridTemplateColumn>
                       <ItemTemplate>
                           <asp:CheckBox ID="chkSelect" runat="server"  />
                       </ItemTemplate>
                   </telerik:GridTemplateColumn>


                 <telerik:GridTemplateColumn DataField="Status" Display="false"  Groupable="False" HeaderText="Status"
                    UniqueName="Status">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemTemplate>
                        <asp:HiddenField ID="HFStatus" Value='<%# Eval("Status") %>'  runat="server" />
                      
                      
                       
                    </ItemTemplate>
                </telerik:GridTemplateColumn>


                  

                <telerik:GridTemplateColumn DataField="ID"  Groupable="False" HeaderText="ID" CurrentFilterFunction="Contains"
                    UniqueName="ID">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemTemplate>
                        <asp:HiddenField ID="HFID" Value='<%# Eval("ID") %>'  runat="server" />
                        <asp:HyperLink  ID="LinkDetail" runat="server" Text='<%# Eval("ID") %>' ></asp:HyperLink>
                      
                       
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="Revise" HeaderText="Revise" >
                    <ItemTemplate>

                        <asp:HyperLink ID="LinkRevise" runat="server" Text=""></asp:HyperLink>
                        <asp:HiddenField ID="hdRevise" Value='<%# Eval("Revise") %>' runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                
               
                <telerik:GridBoundColumn DataField="ASPNo" DataType="System.String"  HeaderText="ASP No." CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" >
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="aspfvalue"   DataFormatString="{0:#,#}"  HeaderText="ASP Value">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Objective" HeaderText="Objective">
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="Usercreate" HeaderText="User create">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="datecreation"   DataFormatString="{0:dd-MMM-yyyy} "  HeaderText="Date creation">
                </telerik:GridBoundColumn>
             <telerik:GridBoundColumn HeaderText="Attached File View" DataField="AttachedFileView" UniqueName="AttachedFileView"
                        EmptyDataText="" AllowFiltering="false">
                        <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="60px"></ItemStyle>
                    </telerik:GridBoundColumn>
            </Columns>
             
        </MasterTableView>
    </telerik:RadGrid>
  



    <%--  <uc1:CreateASPF ID="CreateASPF1" runat="server" />--%>
</asp:Content>
