<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintContractAdviceNote.aspx.cs"
    Inherits="MaricoPay.PrintContractAdviceNote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlPrintClaim" runat="server">
        <div style="width: 100%;">
            <div style="text-align: center; width: 100%; font-weight: bold; font-size: 20px;
                font-family: Arial;">
                CONTRACT ADVICE NOTE
                <br />
                <br />
            </div>
            <div style="width: 100%; font-size: 13px; font-family: Arial;">
                <table cellpadding="2" cellspacing="0" width="100%" style="font-size: 13px; font-family: Arial;">
                    <tr>
                        <td style="width: 20%;">
                            Date <b>(Ngày)</b>
                        </td>
                        <td style="width: 2%;">
                            :
                        </td>
                        <td>
                            <asp:Label ID="lbDate" runat="server" Text=""></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Contract number (provided by legal administrator) :
                            <asp:Label ID="lbContractNo" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            To <b>(Trình đến)</b>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lbTrinhDen" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <hr />
                <br />
                <br />
            </div>
            <div style="width: 100%;">
                <table cellpadding="2" cellspacing="2" width="100%" style="font-size: 13px; font-family: Arial;">
                    <tr>
                        <td colspan="3" style="font-weight: bold; font-size: 13px; font-family: Arial; width: 100%;">
                            CONTRACT FOR APPROVAL:
                          <br />
                            (Hợp đồng trình duyệt)
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 33%;">
                            Prepared by<br />
                            <b><i>(Hợp đồng được chuẩn bị bởi)</i></b>
                        </td>
                        <td style="width: 1%;">
                            :
                        </td>
                        <td>
                            <asp:Label ID="lbPrepared" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Parties to contract<br />
                            <b><i>(Các bên ký hợp đồng)</i></b>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lbParties" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Brief details<br />
                            <b><i>(Nội dung hợp đồng)</i></b>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lbBrief" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Estimated value<br />
                            <b><i>(Giá trị hợp đồng)</i></b>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lbGiaTri" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Commencement date<br />
                            <b><i>(Ngày bắt đầu hợp đồng)</i></b>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lbNgayBatDau" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Expiry date<br />
                            <b><i>(Ngày kết thúc hợp đồng)</i></b>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lbNgayketThuc" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
            <br />
            <div style="width: 100%; text-align: left; font-size: 13px; font-family: Arial;">
                Reviewed by <b><i>(Được kiểm tra bởi):</i></b>
                <br />
                <br />
                <table cellpadding="2" cellspacing="2" width="100%" style="font-size: 13px; font-family: Arial;">
                    <tr>
                        <td style="width: 25%;">
                            *Department/Function<br />
                            <b><i>(Bộ phận/Phòng ban)</i></b>
                        </td>
                        <td style="width: 2%;">
                        </td>
                        <td style="width: 20%; font-size: 10px">
                             -----------------------------------------<br />
                            <b><i>
                                <asp:Label ID="lbNameFunctionHead" runat="server" Text=""></asp:Label></i></b>
                        </td>
                        <td style="width: 2%;">
                        </td>
                        <td style="width: 20%; font-size: 10px">
                            ------------------------------------------<br />
                            <b><i>Signature & date</i></b>
                        </td>
                        <td style="width: 2%;">
                        </td>
                        <td style="width: 20%; font-size: 10px">
                            ----------------------------------------<br />
                            <b><i>Comments, if any</i></b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                        </td>
                    </tr>
                     <tr>
                        <td colspan="7">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                        </td>
                    </tr>
                     <tr>
                        <td colspan="7">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                        </td>
                    </tr>
                     <tr>
                        <td colspan="7">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            *Finance Department<br />
                            <b><i>(Bộ phận tài chính)</i></b>
                        </td>
                        <td>
                        </td>
                        <td style="font-size: 10px">
                            -----------------------------------------<br />
                            <b><i>
                                <asp:Label ID="lbFinanceHead" runat="server" Text=""></asp:Label></i></b>
                        </td>
                        <td>
                        </td>
                        <td style="font-size: 10px">
                            -----------------------------------------<br />
                            <b><i>Signature & date</i></b>
                        </td>
                        <td>
                        </td>
                        <td style="font-size: 10px">
                            ----------------------------------------<br />
                            <b><i>Comments, if any</i></b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                        </td>
                    </tr>
                     <tr>
                        <td colspan="7">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                        </td>
                    </tr>
                     <tr>
                        <td colspan="7">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                        </td>
                    </tr>
                     <tr>
                        <td colspan="7">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            *Legal Department<br />
                            <b><i>(Bộ phận pháp lý)</i></b>
                        </td>
                        <td>
                        </td>
                        <td style="font-size: 10px">
                           -----------------------------------------<br />
                            <b><i>
                                <asp:Label ID="lbLegalHead" runat="server" Text=""></asp:Label></i></b>
                        </td>
                        <td>
                        </td>
                        <td style="font-size: 10px">
                            -----------------------------------------<br />
                            <b><i>Signature & date</i></b>
                        </td>
                        <td>
                        </td>
                        <td style="font-size: 10px">
                            ----------------------------------------<br />
                            <b><i>Comments, if any</i></b>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <div style="width: 100%; text-align: left; font-size: 12px">
                <b><i>This note is a part of the contract management for filing purpose </i></b>
            </div>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
