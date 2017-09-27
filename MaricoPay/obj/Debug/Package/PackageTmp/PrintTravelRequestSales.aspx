<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintTravelRequestSales.aspx.cs"
    Inherits="MaricoPay.PrintTravelRequestSales" %>

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
                        <asp:Label ID="lbDocNo" runat="server" Font-Bold="True" Text=""></asp:Label>
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
                    <td style="color: #000000; font-weight: bold; width: 25%; font-size: 12px;">
                        Mục đích công tác/Purpose of business trip:
                    </td>
                    <td style="color: #000000; width: 65%; font-size: 12px;">
                        <asp:Label ID="lbContentPurpose" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="color: #000000; font-weight: bold; width: 25%; font-size: 12px;">
                        Thời gian/Date:
                    </td>
                    <td style="color: #000000; width: 65%; font-size: 12px;">
                        <table>
                            <tr>
                               
                                <td style="color: #000000; font-size: 12px;">
                                    Từ/From
                                </td>
                                <td style="color: #000000; font-size: 12px; font-weight: bold;">
                                    <asp:Label ID="lbTungay" runat="server"></asp:Label>
                                </td>
                                <td style="color: #000000; font-size: 12px;">
                                    Đến/To
                                </td>
                                <td style="color: #000000; font-size: 12px; font-weight: bold;">
                                    <asp:Label ID="lbDenngay" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
               
            </table>
            <table>
               
                <tr style="vertical-align: top;">
                    <td colspan="3" style="width: 95%;">
                        <table cellpadding="2" cellspacing="0" style="width: 100%; border: 1px solid black;
                            border-collapse: collapse; font-size: 12px;" border="1" frame="border">
                            <thead>
                                <tr style="border: 1px solid black; border-collapse: collapse; font-weight: bold">
                                   <td align="center" bgcolor="#669999">Thứ2</td><td align="center" 
                                        bgcolor="#669999">Thứ3</td><td align="center" bgcolor="#669999">Thứ4</td>
                                    <td align="center" bgcolor="#669999">Thứ5</td><td align="center" 
                                        bgcolor="#669999">Thứ6</td><td align="center" bgcolor="#669999">Thứ7</td>
                                    <td align="center" bgcolor="#669999">CN</td>
                                </tr>
                                <tr style="border: 1px solid black; border-collapse: collapse;  font-weight: bold">
                                   <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb1Ngay1" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb1Ngay2" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb1Ngay3" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb1Ngay4" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb1Ngay5" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb1Ngay6" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb1Ngay0" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr style="border: 1px solid black; border-collapse: collapse">
                                   <td><asp:Label ID="lb1Thu2" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb1Thu3" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb1Thu4" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb1Thu5" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb1Thu6" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb1Thu7" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb1ThuCN" runat="server" Text=""></asp:Label></td>
                                </tr>

                                 <tr style="border: 1px solid black; border-collapse: collapse;  font-weight: bold">
                                   <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb2Ngay1" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb2Ngay2" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb2Ngay3" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb2Ngay4" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb2Ngay5" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb2Ngay6" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb2Ngay0" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr style="border: 1px solid black; border-collapse: collapse">
                                   <td><asp:Label ID="lb2Thu2" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb2Thu3" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb2Thu4" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb2Thu5" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb2Thu6" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb2Thu7" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb2ThuCN" runat="server" Text=""></asp:Label></td>
                                </tr>

                                 <tr style="border: 1px solid black; border-collapse: collapse;  font-weight: bold">
                                   <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb3Ngay1" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb3Ngay2" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb3Ngay3" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb3Ngay4" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb3Ngay5" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb3Ngay6" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb3Ngay0" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr style="border: 1px solid black; border-collapse: collapse">
                                   <td><asp:Label ID="lb3Thu2" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb3Thu3" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb3Thu4" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb3Thu5" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb3Thu6" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb3Thu7" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb3ThuCN" runat="server" Text=""></asp:Label></td>
                                </tr>

                                 <tr style="border: 1px solid black; border-collapse: collapse;  font-weight: bold">
                                   <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb4Ngay1" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb4Ngay2" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb4Ngay3" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb4Ngay4" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb4Ngay5" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb4Ngay6" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb4Ngay0" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr style="border: 1px solid black; border-collapse: collapse">
                                   <td><asp:Label ID="lb4Thu2" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb4Thu3" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb4Thu4" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb4Thu5" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb4Thu6" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb4Thu7" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb4ThuCN" runat="server" Text=""></asp:Label></td>
                                </tr>

                                 <tr style="border: 1px solid black; border-collapse: collapse;  font-weight: bold">
                                   <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb5Ngay1" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb5Ngay2" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb5Ngay3" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb5Ngay4" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb5Ngay5" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb5Ngay6" runat="server" Text=""></asp:Label></td>
                                       <td align="center" bgcolor="#FFCCFF"><asp:Label ID="lb5Ngay0" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr style="border: 1px solid black; border-collapse: collapse">
                                   <td><asp:Label ID="lb5Thu2" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb5Thu3" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb5Thu4" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb5Thu5" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb5Thu6" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb5Thu7" runat="server" Text=""></asp:Label></td>
                                       <td><asp:Label ID="lb5ThuCN" runat="server" Text=""></asp:Label></td>
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
                        Approved by N+2
                    </td>
                    <td align="center">
                        Người dyệt
                        <br />
                        Approved by N+1
                    </td>
                    <td align="center">
                        Người đề nghị
                        <br />
                        Requested by
                    </td>
                </tr>
                <tr>
                    <td align="center">
                    <asp:Literal ID="ltSignN2" runat="server"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltSign" runat="server"></asp:Literal>
                    </td>
                    <td align="center">
                    </td>
                </tr>
                <tr>
                    <td align="center">
                       <asp:Label ID="lbAppN2" runat="server" Text=""></asp:Label>
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
