<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExportASPF.aspx.cs" Inherits="MaricoPay.ExportASPF" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
        <asp:Button ID="Excel" runat="server" Text="Export Excel" OnClick="Excel_Click" />
        <div>


            <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="true"  
                 ExportSettings-IgnorePaging="true" GridLines="Both" OnPreRender="RadGrid1_PreRender"  AutoGenerateColumns="False">
                <ExportSettings ExportOnlyData="True" IgnorePaging="True">
                    <Excel Format="ExcelML" />
                </ExportSettings>
                <MasterTableView>
                    <Columns>


                        <telerik:GridBoundColumn DataField="IOnumber" HeaderText="IOnumber" UniqueName="IOnumber"   FilterListOptions="AllowAllFilters"  AutoPostBackOnFilter="true"   AllowFiltering="true" >
                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="ID_ASP" HeaderText="ID_ASP" UniqueName="ID_ASP"   FilterListOptions="AllowAllFilters"   AutoPostBackOnFilter="true"    AllowFiltering="true" >
                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>


                        <telerik:GridBoundColumn DataField="Quater" HeaderText="Quater" UniqueName="Quater"  FilterListOptions="AllowAllFilters"    AutoPostBackOnFilter="true"    AllowFiltering="true" >
                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="FY" HeaderText="FY" UniqueName="FY"  FilterListOptions="AllowAllFilters"   AutoPostBackOnFilter="true"   AllowFiltering="true" >
                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Country" HeaderText="Country" UniqueName="Country"  FilterListOptions="AllowAllFilters"   AutoPostBackOnFilter="true"    AllowFiltering="true" >
                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>



                        <telerik:GridBoundColumn DataField="BudgetOwner" HeaderText="BudgetOwner" UniqueName="BudgetOwner"  FilterListOptions="AllowAllFilters"    AutoPostBackOnFilter="true"    AllowFiltering="true" >
                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="ActivityGroup" HeaderText="ActivityGroup" UniqueName="ActivityGroup"  FilterListOptions="AllowAllFilters"   AutoPostBackOnFilter="true"     AllowFiltering="true" >
                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>

                           <telerik:GridBoundColumn DataField="Item" HeaderText="Item" UniqueName="Item"  FilterListOptions="AllowAllFilters"   AutoPostBackOnFilter="true"     AllowFiltering="true" >
                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>


                        <telerik:GridBoundColumn DataField="Brand" HeaderText="Brand" UniqueName="Brand"  AllowFiltering="true"   FilterListOptions="AllowAllFilters"   AutoPostBackOnFilter="true"   >
                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="COVERPLAN" HeaderText="COVERPLAN" UniqueName="COVERPLAN" DataFormatString="{0:#,#}" >
                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>


                        <telerik:GridBoundColumn DataField="COMMITMENT" HeaderText="COMMITMENT" UniqueName="COMMITMENT" DataFormatString="{0:#,#}" >
                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="available" HeaderText="available" UniqueName="available" DataFormatString="{0:#,#}">
                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="DateCreate" HeaderText="DateCreate" UniqueName="DateCreate" DataFormatString="{0:M/d/yyyy}">
                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="Fullname" HeaderText="Fullname" UniqueName="Fullname" >
                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                        </telerik:GridBoundColumn>

                    

                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>

        </div>
    </form>
</body>
</html>
