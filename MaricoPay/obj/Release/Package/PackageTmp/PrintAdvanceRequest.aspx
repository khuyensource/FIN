<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintAdvanceRequest.aspx.cs"
    Inherits="MaricoPay.PrintAdvanceRequest" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
        
        
        
        input
        {
            font-family: verdana, arial, helvetica, sans-serif;
            font-size: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlPrintClaim" runat="server">
        <div style="width: 100%;">
            <table cellpadding="2" cellspacing="0" width="100%" style="font-size: 12px">
                <tr>
                    <td style="width: 90%; text-align: center;">
                        <asp:Label ID="lbType" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label>
                        <asp:HiddenField ID="hdTamUng" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                    Document number:  <asp:Label ID="lbdocno" runat="server"></asp:Label>
                        Ngày/Date:
                        <asp:Label ID="lbDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        Số/DP No:
                        <asp:Label ID="lbDPNo" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td style="color: #000000; font-weight: bold; width: 25%; font-size: 12px;">
                        Thanh toán cho/Pay to:
                    </td>
                    <td style="color: #000000; width: 65%; font-size: 12px;">
                        <asp:Label ID="lbName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="color: #000000; font-weight: bold; font-size: 12px;">
                        Số tiền/Amount:
                    </td>
                    <td style="color: #000000; font-size: 12px;">
                        <asp:Label ID="lbTamUng" runat="server"></asp:Label>
                        &nbsp;VNĐ
                    </td>
                </tr>
                <tr>
                    <td style="color: #000000; font-weight: bold; width: 25%; font-size: 12px;">
                        Lý do/Reason:
                    </td>
                    <td style="color: #000000; width: 65%; font-size: 12px;">
                        <asp:Label ID="lbContentPurpose" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="color: #000000; width: 100%; font-size: 12px;">
                        <asp:GridView ID="GridView1" runat="server" Width="800px">
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center" style="width: 30%;">
                        &nbsp;
                    </td>
                    <td align="center" style="width: 30%;">
                        &nbsp;
                    </td>
                    <td align="center" style="width: 30%;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 30%">
                        &nbsp;
                    </td>
                    <td align="center" style="width: 30%">
                        &nbsp;
                    </td>
                    <td align="center" style="width: 30%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 30%">
                        Người duyệt
                        <br />
                        Approved by Finance
                    </td>
                    <td align="center" style="width: 30%">
                        Người dyệt
                        <br />
                        Approved by Function
                    </td>
                    <td align="center" style="width: 30%">
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
                        &nbsp;
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
    </asp:Panel>
    </form>
</body>
</html>
