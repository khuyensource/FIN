using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MaricoPay.DB;
namespace MaricoPay
{
    public partial class PrintClaimSales : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ltype = !string.IsNullOrEmpty(Request.QueryString["type"]) ? Request.QueryString["type"] : String.Empty;
                string docno = "";
                if (ltype == "1" || ltype == "2" || ltype == "3") //1 la in; 2 la xem
                {
                    docno = !string.IsNullOrEmpty(Request.QueryString["docno"]) ? Request.QueryString["docno"] : String.Empty;
                }
                else
                {
                    if (Session["docno"] == null)
                    {
                        docno = String.Empty;

                    }
                    else
                    {
                        docno = Session["docno"].ToString();
                    }
                }
                if (docno != String.Empty)
                {
                    clsSys sys = new clsSys();
                  //  string docno = Session["docno"].ToString();
                    // string us = Session["username"].ToString();
                    DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
                    var kq = dbs.sp_GetClaimExpensesSales_Print(docno, ltype).ToList();
                    if (kq.Count > 0)
                    {
                        lbType.Text ="Đề Nghị Thanh Toán Chi Phí<br/>Expenses Claim";// kq[0].TypeText;
                        lbNo.Text = "(" + docno + ")";
                        lbDate.Text = kq[0].DateRec.Value.ToString("dd-MMM-yy");
                        lbMarket.Text = kq[0].Market;
                        lbDepartment.Text = kq[0].Department;
                        lbName.Text = kq[0].Fullname;
                        lbPosition.Text = kq[0].Position;
                        lbThoigian.Text = cls.cToString(kq[0].FDate.Value.ToString("dd-MMM-yy")) + " to " + cls.cToString(kq[0].TDate.Value.ToString("dd-MMM-yy"));
                       // lbFromDate.Text = kq[0].FDate.Value.ToString("dd-MMM-yy");
                        //lbToDate.Text = kq[0].TDate.Value.ToString("dd-MMM-yy");
                        //lbNoDays.Text = sys.cToString(kq[0].NoDays);
                         lbContentPurpose.Text = cls.cToString(kq[0].Purpose);
                         lbCostcenter.Text = kq[0].Costcenter;
                         hdTamUng.Value = cls.cToString0(kq[0].DaTamUng);
                        lbAppFuction.Text = kq[0].FullNameVP;
                        lbRequest.Text = kq[0].Fullname;
                        if (ltype != "3")
                        {
                            ltStatus.Text = "";
                            ltSign.Text = "<a href='#'><img id='imgApproved' src='/ImagesSignature/" + kq[0].SigntureVP + "' alt='APPROVED' style='width:80px;height:60px;'/></a>";
                            ltSignNhay.Text = "<a href='#'><img id='imgApproved' src='/ImagesSignature/" + kq[0].SigntureFinal + "' alt='APPROVED' style='width:25px;height:15px;'/></a>";
                        }
                        else
                        {
                            //Userapprove
                            ltStatus.Text = "<table  cellpadding='2' cellspacing='0' width='100%' style='font-size: 12px'><tr>";
                            for (int i = 0; i < kq.Count; i++)
                            {
                                ltStatus.Text = ltStatus.Text+"<td align='center'>";
                                ltStatus.Text = ltStatus.Text + kq[i].Userapprove+"<br/>";
                                ltStatus.Text = ltStatus.Text+"<a href='#'><img id='imgApproved' src='/ImagesSignature/" + kq[i].Signture + "' alt='' style='width:80px;height:60px;'/></a>";
                                ltStatus.Text = ltStatus.Text + "</td>";
                            }
                            ltStatus.Text =ltStatus.Text + "</tr></table>";
                            ltSign.Text = "";
                            ltSignNhay.Text = "";
                        }
                       // List<sp_getClaimDetailResult> kq1 = dbs.sp_getClaimDetail(docno,true).OrderBy(o => o.ID).ToList();//sp_getClaimDetail LA STORE
                        List<sp_getClaimDetailSalesResult> kq1 = dbs.sp_getClaimDetailSales(docno, false, cls.cToString(Session["Username"])).OrderBy(m => m.Date).ToList();//sp_getClaimDetail LA STORE
                        Session["ClaimDetailPrintSalesPrint"] = kq1;
                        FillTable(docno);
                       // LoadSum(docno);
                        dbs.Dispose();
                        // Session["NewDocNo"] = code;
                        // pnlPrintClaim
                        if (ltype != "2")
                        {
                            PrintHelper.PrintWebControl(pnlPrintClaim);
                        }
                       // PrintHelper.PrintWebControl(pnlPrintClaim);
                    }
                    else
                    {
                        //chua duoc approved
                    }
                }
            }
        }
        //private void LoadSum(string code)
        //{
        //    DBStoreDataContext dbs = new DBStoreDataContext();
        //    var model = dbs.sp_LoadTotalClaim4Doc(code).ToList();
        //    if (model.Count > 3)
        //    {
        //        gridSum.DataSource = model;
        //        gridSum.DataBind();
        //        gridSum.Rows[gridSum.Rows.Count - 1].Font.Bold = true;
        //        gridSum.Rows[gridSum.Rows.Count - 2].Font.Bold = true;
        //        gridSum.Rows[gridSum.Rows.Count - 3].Font.Bold = true;
        //        gridSum.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        //        gridSum.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        //        gridSum.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        //    }
        //    dbs.Dispose();
        //}
        private void FillTable(string code)
        {
            if (Session["ClaimDetailPrintSalesPrint"] == null)
            {
                DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
                List<sp_getClaimDetailSalesResult> kq1 = dbs.sp_getClaimDetailSales(code, false, cls.cToString(Session["Username"])).OrderBy(m => m.Date).ToList();//sp_getClaimDetail LA STORE
                Session["ClaimDetailPrintSalesPrint"] = kq1;
            }
            List<sp_getClaimDetailSalesResult> tbl = (List<sp_getClaimDetailSalesResult>)Session["ClaimDetailPrintSalesPrint"];
             Literal1.Text = "";
             double tongtien = 0;
             int stt = 0;
             foreach (sp_getClaimDetailSalesResult item in tbl)
            {

                if (item.TotalVN != 0 && item.TrangThai==1)
                {
                    if (item.Date == new DateTime(2000, 1, 1))
                    {
                        //dong subtotal

                        Literal1.Text = Literal1.Text + "<tr><td colspan=7 style='color: #000000; font-weight: bold; text-align:left; border:1px solid black; border-collapse:collapse;'>Subtotal</td>";

                        Literal1.Text = Literal1.Text
                          + "<td colspan=4 style='color: #000000; font-weight: bold; border:1px solid black; border-collapse:collapse; text-align:left;'>" + String.Format("{0:0,0}", item.TotalVN) + "</td>"
                          + "</tr>";
                    }
                    else
                    {
                        stt++;
                        tongtien = tongtien + cls.cToDouble(item.TotalVN);
                        Literal1.Text = Literal1.Text + "<tr><td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + cls.cToString0(stt) + "</td><td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Date.ToString("dd-MMM-yy") + "</td>"
                                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>"
                                           + "<a  href='#' onclick=\"javascript:window.open('/popVAT.aspx?cp=" + item.CompanyName + "&pv=" + item.Province + "&vatcode=" + item.VATCode + "&vatamount=" + item.VATAmount + "','VAT','width=500,height=150')\">" + item.No + "</a></td>";


                        Literal1.Text = Literal1.Text
                           + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.DetailExpenses + "</td>"
                            + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Participant + "</td>"
                              + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.ChargeTypeDs + "</td>";

                        Literal1.Text = Literal1.Text + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.WorkingPlanDetail + "</td>"
                     + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.Amount) + "</td>"
                    + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.VATAmount) + "</td>";

                        Literal1.Text = Literal1.Text
                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.TotalVN) + "</td>"
                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.GL + "</td>"
                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.IO + "</td>"
                          + "</tr>";
                    }


                }
            }
            //tinh tong tien
            Literal1.Text = Literal1.Text + "<tr><td colspan=9 style='color: #000000; font-weight: bold; text-align:right;'>Tổng tiền VND/Total Amount:</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + cls.FormatNumber(tongtien) + "</td><td colspan=2></td></tr>";
            Literal1.Text = Literal1.Text + "<tr><td colspan=9 style='color: #000000; font-weight: bold; text-align:right;'>Đã tạm ứng VND/Advanced Amount:</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + cls.FormatNumber(hdTamUng.Value) + "</td><td colspan=2></td></tr>";
            Literal1.Text = Literal1.Text + "<tr><td colspan=9 style='color: #000000; font-weight: bold; text-align:right;'>Chênh lệch VND/Balance(+)/Reimbursemet(-):</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + cls.FormatNumber(tongtien - cls.cToDouble(hdTamUng.Value)) + "</td><td colspan=2></td></tr>";
            //Session["ClaimDetailPrintSalesPrint"] == null;
        }
        //private void FillTableOld(string loai)
        //{
        //    Literal1.Text = "";
        //    List<sp_getClaimDetailResult> tbl = (List<sp_getClaimDetailResult>)Session["ClaimDetailPrintSales"];
        //    foreach (sp_getClaimDetailResult item in tbl)
        //    {
        //        Literal1.Text = Literal1.Text + "<tr><td style='border:1px solid black; border-collapse:collapse;'>"
        //         + item.Date.ToString("dd-MMM-yy")
        //      + "</td><td style='border:1px solid black; border-collapse:collapse;'>"
        //          + item.No
        //       + "</td><td style='border:1px solid black; border-collapse:collapse;'>"
        //          + item.Notation
        //       + "</td> <td style='border:1px solid black; border-collapse:collapse;'>"
        //         + item.Description
        //       + "</td>";
        //        if (loai.ToLower() != "domestic")//trong nuoc
        //        {
        //            Literal1.Text = Literal1.Text + "<td style='border:1px solid black; border-collapse:collapse; text-align:right;'>"
        //            + item.Currency + " - " + String.Format("{0:0,0}",item.Rate)
        //          + "</td><td style='border:1px solid black; border-collapse:collapse; text-align:right;'>"
        //         + String.Format("{0:0,0}",item.Amount)
        //       + "</td>";
        //        }
        //        Literal1.Text = Literal1.Text
        //       + "<td style='border:1px solid black; border-collapse:collapse; text-align:right;'>"
        //        + String.Format("{0:0,0}",item.TotalVN)
        //       + "</td><td style='border:1px solid black; border-collapse:collapse;'>" + item.CompanyName + "</td>"
        //       +"<td style='border:1px solid black; border-collapse:collapse;'>" + item.Province + "</td>"
        //       +"<td style='border:1px solid black; border-collapse:collapse;'>" + item.VATCode + "</td>"
        //       +"<td style='border:1px solid black; border-collapse:collapse;'>" + String.Format("{0:0,0}",item.VATAmount) + "</td></tr>";
        //    }

        //}
    }
}