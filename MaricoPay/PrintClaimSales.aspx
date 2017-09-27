<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintClaimSales.aspx.cs"
    Inherits="MaricoPay.PrintClaimSales" %>

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
            text-align: center;
            font-size: 12px;
            font-weight: bold;
        }
        h2
        {
            margin-bottom: 0;
        }
        .style1
        {
            width: 54px;
        }
        .style2
        {
            width: 30%;
            height: 58px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlPrintClaim" runat="server">
        <div style="width: 100%;">
            <table cellpadding="2" cellspacing="0" width="100%" style="font-size: 12px">
                <tr>
                    <td colspan="3" style="width: 100%; text-align: center;">
                        <asp:Label ID="lbType" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label>
                        <asp:HiddenField ID="hdTamUng" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: right;">
                        Ngày/Date:
                        <asp:Label ID="lbDate" runat="server"></asp:Label>
                        <asp:Label ID="lbNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <table cellpadding="0" cellspacing="5px" style="font-size: 12px">
                            <tr>
                                <td style="color: #000000; font-weight: bold;">
                                    Họ và tên/Full name:
                                </td>
                                <td style="color: #000000;">
                                    <asp:Label ID="lbName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="color: #000000; font-weight: bold;">
                                    Thị trường/Market:
                                </td>
                                <td style="color: #000000;">
                                    <asp:Label ID="lbMarket" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="style2">
                        <table cellpadding="0" cellspacing="5px" style="font-size: 12px">
                            <tr>
                                <td style="color: #000000; font-weight: bold;">
                                    Phòng ban/Department:
                                </td>
                                <td style="color: #000000;">
                                    <asp:Label ID="lbDepartment" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="color: #000000; font-weight: bold;">
                                    Chức vụ/Position:
                                </td>
                                <td style="color: #000000;">
                                    <asp:Label ID="lbPosition" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="style2">
                        <table cellpadding="0" cellspacing="5px" style="font-size: 12px">
                            <tr>
                                <td style="color: #000000; font-weight: bold;">
                                    TTCP/Cost center:
                                </td>
                                <td style="color: #000000;">
                                    <asp:Label ID="lbCostcenter" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="color: #000000; font-weight: bold;">
                                    Thơi gian/period:
                                </td>
                                <td style="color: #000000;">
                                    <asp:Label ID="lbThoigian" runat="server"></asp:Label>
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
                    <td style="color: #000000; font-weight: bold;">
                        Nội dung thanh toán/Purpose:
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lbContentPurpose" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="vertical-align: top;">
                    <td colspan="3" style="width: 95%;">
                        <table cellpadding="2" cellspacing="0" style="width: 100%; border: 1px solid black;
                            border-collapse: collapse; font-size: 12px;" id="myTable">
                            <thead>
                                <tr style="border: 1px solid black; border-collapse: collapse">
                                    <th align="center" style="border: 1px solid black; border-collapse: collapse; font-weight: bold;"
                                        rowspan="2">
                                        STT<br />
                                        No
                                    </th>
                                    <th align="center" colspan="2" style="border: 1px solid black; border-collapse: collapse;
                                        font-weight: bold;">
                                        Hóa đơn/Invoice
                                    </th>
                                    <th rowspan="2" align="center" style="border: 1px solid black; border-collapse: collapse;
                                        font-weight: bold;">
                                        Chi tiết chi phí<br />
                                        Detail of Expenses
                                    </th>
                                    <th align="center" rowspan="2" style="border: 1px solid black; border-collapse: collapse;
                                        font-weight: bold;">
                                        Người tham gia<br />
                                        Participant
                                    </th>
                                    <th align="center" rowspan="2" style="border: 1px solid black; border-collapse: collapse;
                                        font-weight: bold;">
                                        Loai CP<br />
                                        Charge type
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-bottom: 0px; border-collapse: collapse;
                                        font-weight: bold;" id="tdCurr" runat="server" rowspan="2">
                                        Kế hoạch CT<br />
                                        Working plan
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-bottom: 0px; border-collapse: collapse;
                                        font-weight: bold;" rowspan="2">
                                        Thanh Tien(KoVAT)<br />
                                        Amount(NonVAT)
                                    </th>
                                    <th align="center" rowspan="2" style="border: 1px solid black; border-bottom: 0px;
                                        border-collapse: collapse; font-weight: bold;">
                                        Tien Thue<br />
                                        Tax Amount
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-bottom: 0px; border-collapse: collapse;
                                        font-weight: bold;" rowspan="2">
                                        Thành tiền VND<br />
                                        Amount
                                    </th>
                                    <th align="center" rowspan="2" style="border: 1px solid black; border-bottom: 0px;
                                        border-collapse: collapse; font-weight: bold;">
                                        GL
                                    </th>
                                    <th align="center" rowspan="2" style="border: 1px solid black; border-bottom: 0px;
                                        border-collapse: collapse; font-weight: bold;">
                                        IO
                                    </th>
                                </tr>
                                <tr style="border: 1px solid black; border-collapse: collapse">
                                    <th align="center" class="style1" style="border: 1px solid black; border-collapse: collapse;
                                        font-weight: bold;">
                                        Ngày/Date
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-collapse: collapse; font-weight: bold;">
                                        Số/No
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
                    <td align="center">
                        <table>
                            <tr>
                                <td align="center">
                                    Người duyệt
                                    <br />
                                    Approved by Finance
                                </td>
                                <td>
                                    <asp:Literal ID="ltSignNhay" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="center">
                        Người dyệt
                        <br />
                        Approved by Function
                    </td>
                    <td align="center">
                        Người đề nghị
                        <br />
                        Requested by
                    </td>
                </tr>
                <tr>
                    <td align="center">
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltSign" runat="server"></asp:Literal>
                    </td>
                    <td align="center">
                    </td>
                </tr>
                <tr>
                    <td align="center">
                    </td>
                    <td align="center">
                        <asp:Label ID="lbAppFuction" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="lbRequest" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div>
        <asp:Literal ID="ltStatus" runat="server"></asp:Literal>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
