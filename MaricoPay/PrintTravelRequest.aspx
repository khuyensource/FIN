<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintTravelRequest.aspx.cs"
    Inherits="MaricoPay.PrintTravelRequest" %>

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
        .style3
        {
            height: 27px;
        }
        .style4
        {
            width: 60%;
            height: 49px;
        }
        .style5
        {
            width: 30%;
            height: 49px;
        }
                
        
        
        input
        {
            font-family: verdana, arial, helvetica, sans-serif;
            font-size: 10px;
        }
        .style6
        {
            font-size: xx-small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlPrintClaim" runat="server">
        <div style="width: 100%;">
            <table cellpadding="2" cellspacing="0" width="100%" style="font-size: 12px">
                <tr>
                    <td colspan="2" style="width: 100%; text-align: center;">
                        <asp:Label ID="lbType" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label>
                        <asp:HiddenField ID="hdTamUng" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right;">
                        Số chứng từ/Document No:
                        <asp:Label ID="lbDocNo" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                        &nbsp;Ngày/Date:
                        <asp:Label ID="lbDate" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        <table cellpadding="0" cellspacing="5px" style="font-size: 12px">
                            <tr>
                                <td style="color: #000000; font-weight: bold;" class="style3">
                                    Người đề nghị/Requester:
                                    <asp:Label ID="lbName" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="style5">
                        <table cellpadding="0" cellspacing="5px" style="font-size: 12px">
                            <tr>
                                <td style="color: #000000; font-weight: bold; font-size: 12px;">
                                    Phòng/Dept:
                                </td>
                                <td style="color: #000000; font-size: 12px;">
                                    <asp:Label ID="lbDepartment" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table>
            <tr>
                    <td style="color: #000000; font-weight: bold; font-size: 12px;">
                        Nơi đến/Destination:
                    </td>
                    <td style="color: #000000; font-size: 12px;">
                        <asp:Label ID="lbNoiDen" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="color: #000000; font-weight: bold; width: 25%; font-size: 12px;">
                        Lộ trình/Itinerary:
                    </td>
                    <td style="color: #000000; width: 65%; font-size: 12px;">
                        <asp:Label ID="lbLoTrinh" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="color: #000000; font-weight: bold; width: 25%; font-size: 12px;">
                        Mục đích công tác/Purpose of business trip:
                    </td>
                    <td style="color: #000000; width: 65%; font-size: 12px;">
                        <asp:Label ID="lbContentPurpose" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="color: #000000; font-weight: bold; width: 25%; font-size: 12px;">
                        Trong thời gian/Length of days:
                    </td>
                    <td style="color: #000000; width: 65%; font-size: 12px;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lbNodays" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td style="color: #000000; font-size: 12px;">
                                    Từ ngày/From
                                </td>
                                <td style="color: #000000; font-size: 12px; font-weight: bold;">
                                    <asp:Label ID="lbTungay" runat="server"></asp:Label>
                                </td>
                                <td style="color: #000000; font-size: 12px;">
                                    Đến ngày/To
                                </td>
                                <td style="color: #000000; font-size: 12px; font-weight: bold;">
                                    <asp:Label ID="lbDenngay" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                
                <tr>
                    <td style="color: #000000; font-weight: bold; width: 25%; font-size: 12px;">
                        Phương tiện/Transportation mean</td>
                    <td style="color: #000000; width: 65%; font-size: 12px;">
                        <table>
                            <tr>
                                <td style="color: #000000; font-size: 12px;">
                                    <asp:CheckBox ID="chkOto" runat="server" Text="Ôtô/Car" />
                                </td>
                                <td style="color: #000000; font-size: 12px;">
                                    <asp:CheckBox ID="chkTauHoa" runat="server" Text="Tàu hỏa/Train" />
                                </td>
                                <td style="color: #000000; font-size: 12px;">
                                    <asp:CheckBox ID="chkMayBay" runat="server" Text="Máy bay/Flight" />
                                </td>
                            </tr>
                        </table>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="color: #000000; font-weight: bold; width: 25%; font-size: 12px;">
                        Đề nghị hành chánh thu xếp/Request admin to arrange
                    </td>
                    <td style="color: #000000; width: 65%; font-size: 12px;" >
                        <table>
                            <tr>
                                <td style="color: #000000; font-size: 12px;">
                                    <asp:CheckBox ID="chkVeTauMayBay" runat="server" 
                                        Text="Mua vé máy bay/Return air ticket" />
                                </td>
                                <td style="color: #000000; font-size: 12px;">
                                    <asp:CheckBox ID="chkDatPhong" runat="server" 
                                        Text="Đặt phòng khách sạn/Hotel booking" />
                                </td>
                            </tr>
                            <tr>
                                <td style="color: #000000; font-size: 12px;" colspan="2">
                                    <asp:CheckBox ID="chkOther" runat="server" Text="Khác/Other" />
                                    :
                                    <asp:Label ID="lbOther" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="color: #000000; font-weight: bold; width: 25%; font-size: 12px;">
                        Đề nghị tạm ứng/Advance request</td>
                    <td style="color: #000000; width: 65%">
                        <asp:Label ID="lbTamUng" runat="server"></asp:Label> VNĐ
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="color: #000000; font-weight: bold; font-size: 12px;">
                        Ước tính chi phí/Charges:
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr style="vertical-align: top;">
                    <td colspan="3" style="width: 95%;">
                        <table cellpadding="2" cellspacing="0" style="width: 100%; border: 1px solid black;
                            border-collapse: collapse; font-size: 12px;" id="myTable">
                            <thead>
                                <tr style="border: 1px solid black; border-collapse: collapse">
                                    <th align="center" style="border: 1px solid black; border-collapse: collapse; font-weight: bold;">
                                        STT<br />
                                        No
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-collapse: collapse; font-weight: bold;">
                                        Khoản mục<br />
                                        Category
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-bottom: 0px; border-collapse: collapse;
                                        font-weight: bold;" id="tdCurr" runat="server">
                                        Đơn giá<br />
                                        Unit price
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;">
                                        Số lượng<br />
                                        Quantity
                                    </th>
                                  
                                     <th align="center" style="border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;">
                                        Tạm ứng<br />
                                        Advance
                                    </th>
                                    <th align="center" style="border: 1px solid black; border-bottom: 0px; border-collapse: collapse;
                                        font-weight: bold;">
                                        Thành tiền<br />
                                        Amount VNĐ</th>
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
                        Người duyệt
                        <br />
                        Approved by VP/COO
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
                <tr>
                <td align="center" colspan="3">
                <strong><em><span class="style6">* Đi công tác quốc tế, đi công tác quốc nội bằng máy bay, cần phải có sự chấp thuận của VP/ &nbsp;International traveling/Domestic traveling by air, requires VP approval at least</span></em></strong>
                 </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
