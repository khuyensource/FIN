<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintClaimOfficeBK.aspx.cs" Inherits="MaricoPay.PrintClaimOfficeBK" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css" media="all">
        @page
        {
            size: 11in 8.5in;
            margin-top: 0;
            margin-bottom: 1cm;
            margin-left: 0;
            margin-right: 0;
        }
     
        table
        {
            border: .02em solid #666;
            border-collapse: collapse;
            width: 100%;
        }
        td, th
        {
          
            font-size: 12px;
            line-height: 12px;
            vertical-align: middle;
            padding: 5px;
            font-family: "Arial";
        }
        th
        {
            text-align: left;
            font-size: 12px;
            font-weight: bold;
        }
        h2
        {
            margin-bottom: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlPrintClaim" runat="server">
        <div style="width: 100%;">
            <table cellpadding="2" cellspacing="0" width="100%" style="font-size: 12px">
                <tr>
                    <td colspan="1" style="width: 50%;">
                        <asp:Label ID="lbType" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label>
                    </td>
                    <td style="width: 50%;">
                        <table style="font-size: 12px">
                            <tr>
                                <td>
                                    Date:
                                </td>
                                <td>
                                    <asp:Label ID="lbDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%; border: 1px solid black; border-collapse: collapse">
                        <table cellpadding="0" cellspacing="0" style="font-size: 12px">
                            <tr>
                                <td style="color: #000000; font-weight: bold;">
                                    Market:
                                </td>
                                <td style="color: #000000;">
                                    <asp:Label ID="lbMarket" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="color: #000000; font-weight: bold;">
                                    Department:
                                </td>
                                <td style="color: #000000;">
                                    <asp:Label ID="lbDepartment" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; border: 1px solid black; border-collapse: collapse">
                        <table cellpadding="0" cellspacing="0" style="font-size: 12px">
                            <tr>
                                <td style="color: #000000; font-weight: bold;">
                                    Name:
                                </td>
                                <td style="color: #000000;">
                                    <asp:Label ID="lbName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="color: #000000; font-weight: bold;">
                                    Position:
                                </td>
                                <td style="color: #000000;">
                                    <asp:Label ID="lbPosition" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <%--  <td style="width: 25%; border: 1px solid black; border-collapse: collapse">
                        <table cellpadding="0" cellspacing="0" style="font-size: 12px">
                            <tr>
                                <td style="color: #000000; font-weight: bold;">
                                    From:
                                </td>
                                <td style="color: #000000;">
                                    <asp:Label ID="lbFromDate" runat="server"></asp:Label>
                                </td>
                                <td style="color: #000000; font-weight: bold;">
                                    To:
                                </td>
                                <td style="color: #000000;">
                                    <asp:Label ID="lbToDate" runat="server"></asp:Label>
                                </td>
                                <td style="color: #000000;">
                                    No of days:
                                </td>
                                <td style="color: #000000;">
                                    <asp:Label ID="lbNoDays" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="color: #000000; font-weight: bold;">
                                    Purpose:
                                </td>
                                <td colspan="5">
                                    <asp:Label ID="lbPurpose" runat="server" Width="322px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>--%>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        <table>
                            <tr>
                                <td align="center" style="color: #000000; font-weight: bold;">
                                    SUMMARY
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gridSum" runat="server" ForeColor="Black" AutoGenerateColumns="False"
                                        Font-Names="Tahoma" Font-Size="12px" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="Description" HeaderText="Summary" ItemStyle-Width="220px"
                                                HeaderStyle-Width="220px" />
                                            <asp:BoundField DataField="Total_VND" HeaderText="Total-VND">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NoDays" HeaderText="NoDays">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AVG_VND" HeaderText="AVG_VND">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="color: #000000; font-weight: bold;">
                        DETAIL
                    </td>
                </tr>
                <tr style="vertical-align: top;">
                    <td colspan="2" style="width: 95%;">
                        <table cellpadding="2" cellspacing="0" style="width: 100%; border: 1px solid black;
                            border-collapse: collapse; font-size: 12px;" id="myTable">
                            <thead>
                                <tr style="border: 1px solid black; border-collapse: collapse">
                                   
                                    <th colspan="3" align="center" style="border: 1px solid black; border-collapse: collapse;
                                        font-weight: bold;">
                                        Invoice/Document
                                    </th>
                                    <th rowspan="2" align="center" style="border: 1px solid black; border-collapse: collapse;
                                        font-weight: bold;">
                                        Description
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-bottom: 0px; border-collapse: collapse;
                                        font-weight: bold;" id="tdCurr" runat="server">
                                        Currency
                                    </th>
                                    <th align="center" id="tdAmountCurr" style="border: 1px solid black; border-bottom: 0px;
                                        border-collapse: collapse; font-weight: bold;" runat="server">
                                        Amount
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-bottom: 0px; border-collapse: collapse;
                                        font-weight: bold;">
                                        Amount
                                    </th>
                                    <th colspan="4" align="center" style="border: 1px solid black; border-collapse: collapse;
                                        font-weight: bold;">
                                        Invoice information (VAT)
                                    </th>
                                </tr>
                                <tr style="border: 1px solid black; border-collapse: collapse">
                                    <th align="center" style="border: 1px solid black; border-collapse: collapse; font-weight: bold;">
                                        Date
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-collapse: collapse; font-weight: bold;">
                                        No
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-collapse: collapse; font-weight: bold;">
                                        Notation
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-top: 0px; border-collapse: collapse;
                                        font-weight: bold;" id="tdRate" runat="server">
                                        Rate
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-top: 0px; border-collapse: collapse;
                                        font-weight: bold;" id="tdCurr1" runat="server">
                                        Currency
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-top: 0px; border-collapse: collapse;
                                        font-weight: bold;">
                                        VND
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-collapse: collapse; font-weight: bold;">
                                        Company
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-collapse: collapse; font-weight: bold;">
                                        Address
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-collapse: collapse; font-weight: bold;">
                                        Code
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-collapse: collapse; font-weight: bold;">
                                        Amount
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td align="center" style="width: 30%">
                                    <table style="font-size: 12px">
                                        <tr>
                                            <td align="center">
                                                Người duyệt </br> Approved by Finance
                                            </td>
                                        </tr>
                                        <tr style="height: 70px;">
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lbAppFinance" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="center" style="width: 30%">
                                    <table style="font-size: 12px">
                                        <tr>
                                            <td align="center">
                                                Người dyệt </br> Approved by Function
                                            </td>
                                        </tr>
                                        <tr style="height: 70px;">
                                            <td align="center">
                                                <asp:Literal ID="ltSign" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lbAppFuction" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="center" style="width: 30%">
                                    <table style="font-size: 12px">
                                        <tr>
                                            <td align="center">
                                                Người đề nghị
                                                <br>
                                                Requested by
                                            </td>
                                        </tr>
                                        <tr style="height: 70px;">
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lbRequest" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
