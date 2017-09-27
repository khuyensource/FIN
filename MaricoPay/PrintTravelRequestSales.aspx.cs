using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MaricoPay.DB;
namespace MaricoPay
{
    public partial class PrintTravelRequestSales : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                    string ltype = !string.IsNullOrEmpty(Request.QueryString["type"]) ? Request.QueryString["type"] : String.Empty;
                    string docno = "";
                    if (ltype == "1" || ltype == "2") //1 la in; 2 la xem
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

                        // string us = Session["username"].ToString();
                        DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
                        var kq = dbs.sp_GetTravelRequest_Print(docno).ToList();

                        if (kq.Count > 0)
                        {
                            lbType.Text = "PHIẾU YÊU CẦU CÔNG TÁC<br/>BUSINESS TRAVELING REQUISITION";// kq[0].TypeText;

                            lbDate.Text = kq[0].DateRec.Value.ToString("dd-MMM-yy");

                            lbDepartment.Text = kq[0].Department;
                            lbName.Text = kq[0].Fullname + "-" + kq[0].Position;
                            //  lbPosition.Text = kq[0].Position;
                            lbContentPurpose.Text = kq[0].Purpose;
                            lbDocNo.Text = docno;
                            // lbNodays.Text = cls.cToString0(kq[0].NoDays);
                            lbTungay.Text = cls.cToString(kq[0].FDate.Value.ToString("dd-MMM-yy"));
                            lbDenngay.Text = cls.cToString(kq[0].TDate.Value.ToString("dd-MMM-yy"));
                            // lbNoiDen.Text = kq[0].Destination;
                            // lbLoTrinh.Text = kq[0].Itenerary;
                            // lbTamUng.Text =cls.FormatNumber(kq[0].Advance);
                            // chkOto.Checked=cls.cToBool(kq[0].ByCar);
                            // chkTauHoa.Checked=cls.cToBool(kq[0].ByTrain);
                            // chkMayBay.Checked=cls.cToBool(kq[0].ByPlane);
                            // chkOther.Checked = cls.cToBool(kq[0].Other);
                            // lbOther.Text = kq[0].DetailOther;
                            //  chkDatPhong.Checked=cls.cToBool(kq[0].BookHotel);
                            // chkVeTauMayBay.Checked = cls.cToBool(kq[0].BookTicket);
                            // hdTamUng.Value = cls.cToString0(kq[0].DaTamUng);
                            lbAppFuction.Text = kq[0].ApproverN1;
                            lbAppN2.Text = kq[0].Approver;
                            lbRequest.Text = kq[0].Fullname;
                            ltSign.Text = "<a href='#'><img id='imgApproved' src='/ImagesSignature/" + kq[0].SigntureN1 + "' alt='APPROVED' style='width:80px;height:60px;'/></a>";
                            ltSignN2.Text = "<a href='#'><img id='imgApproved' src='/ImagesSignature/" + kq[0].Signture + "' alt='APPROVED' style='width:80px;height:60px;'/></a>";

                            // List<sp_getClaimDetailResult> kq1 = dbs.sp_getClaimDetail(docno,true).OrderBy(o => o.ID).ToList();//sp_getClaimDetail LA STORE
                            // List<sp_getClaimDetailResult> kq1 = dbs.sp_getClaimDetail(docno, true).ToList();//sp_getClaimDetail LA STORE
                            //  Session["ClaimDetailPrint"] = kq1;
                            FillTable(docno);
                            // LoadSum(docno);
                            dbs.Dispose();
                            // Session["NewDocNo"] = code;
                            // pnlPrintClaim
                            if (ltype != "2")
                            {
                                PrintHelper.PrintWebControl(pnlPrintClaim);
                            }
                        }
                        else
                        {
                            //chua duoc approved
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Login.aspx");
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
        private string Bool2Yes(bool b)
        {
            return b ? "Yes" : "No";
        }
        private void FillTable(string code)
        {
            //if (Session["TravelDetailSales"] == null)
            //{
                DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
                List<sp_getTravelDetailSalesResult> kq = dbs.sp_getTravelDetailSales(code).OrderBy(o => o.FDate).ToList();//sp_getClaimDetail LA STORE
                if (code == "0" || code == "")
                {
                    kq.RemoveAt(0);
                }
                Session["TravelDetailSales"] = kq;
          //  }
            List<sp_getTravelDetailSalesResult> tbl = (List<sp_getTravelDetailSalesResult>)Session["TravelDetailSales"];
            Literal1.Text = "";

            int stt = 0;
            foreach (sp_getTravelDetailSalesResult item in tbl)
            {


                stt++;


                //item.FDate;
                //item.Thu;
                //item.PurposeMorning;
                //item.TenTinhSang;
                //item.TenHuyenSang;
                //item.PurposeAfter;
                //item.TenTinhChieu;
                //item.TenHuyenChieu;
                int thang = item.FDate.Month;
                int nam = item.FDate.Year;
                int ngay = item.FDate.Day;
                DateTime dngay = item.FDate;
                DateTime ngay1 = new DateTime(nam, thang, 1);
                int thungay1 = (int)ngay1.Date.DayOfWeek;
                int Maxngaytuan1 = 7 - thungay1 + 1;
                if (Maxngaytuan1 == 8)
                {
                    Maxngaytuan1 = 1;
                }
                if (ngay <= Maxngaytuan1)
                {
                    int thu = (int)dngay.Date.DayOfWeek;//cls.getThu(ngay1);//0 CN
                    switch (thu)
                    {
                        case 0://CN
                            lb1Ngay0.Text = dngay.ToString("dd/MM/yyyy");
                            lb1ThuCN.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                                + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 1://T2
                            lb1Ngay1.Text = dngay.ToString("dd/MM/yyyy");
                            lb1Thu2.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 2://T3
                            lb1Ngay2.Text = dngay.ToString("dd/MM/yyyy");
                            lb1Thu3.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 3://T4
                            lb1Ngay3.Text = dngay.ToString("dd/MM/yyyy");
                            lb1Thu4.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 4://T5
                            lb1Ngay4.Text = dngay.ToString("dd/MM/yyyy");
                            lb1Thu5.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 5://T6
                            lb1Ngay5.Text = dngay.ToString("dd/MM/yyyy");
                            lb1Thu6.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 6://T7
                            lb1Ngay6.Text = dngay.ToString("dd/MM/yyyy");
                            lb1Thu7.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;

                    }
                }
                int Beginngaytuan2 = Maxngaytuan1 + 1;
                int Maxngaytuan2 = Beginngaytuan2 + 6;
                if (ngay >= Beginngaytuan2 && ngay <= Maxngaytuan2)//tuan 2
                {
                    int thu = (int)dngay.Date.DayOfWeek;//cls.getThu(ngay1);//0 CN
                    switch (thu)
                    {
                        case 0://CN
                            lb2Ngay0.Text = dngay.ToString("dd/MM/yyyy");
                            lb2ThuCN.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 1://T2
                            lb2Ngay1.Text = dngay.ToString("dd/MM/yyyy");
                            lb2Thu2.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 2://T3
                            lb2Ngay2.Text = dngay.ToString("dd/MM/yyyy");
                            lb2Thu3.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 3://T4
                            lb2Ngay3.Text = dngay.ToString("dd/MM/yyyy");
                            lb2Thu4.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 4://T5
                            lb2Ngay4.Text = dngay.ToString("dd/MM/yyyy");
                            lb2Thu5.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 5://T6
                            lb2Ngay5.Text = dngay.ToString("dd/MM/yyyy");
                            lb2Thu6.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 6://T7
                            lb2Ngay6.Text = dngay.ToString("dd/MM/yyyy");
                            lb2Thu7.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;

                    }
                }
                int Beginngaytuan3 = Maxngaytuan2 + 1;
                int Maxngaytuan3 = Beginngaytuan3 + 6;
                if (ngay >= Beginngaytuan3 && ngay <= Maxngaytuan3)//tuan 3
                {
                    int thu = (int)dngay.Date.DayOfWeek;//cls.getThu(ngay1);//0 CN
                    switch (thu)
                    {
                        case 0://CN
                            lb3Ngay0.Text = dngay.ToString("dd/MM/yyyy");
                            lb3ThuCN.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 1://T2
                            lb3Ngay1.Text = dngay.ToString("dd/MM/yyyy");
                            lb3Thu2.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 2://T3
                            lb3Ngay2.Text = dngay.ToString("dd/MM/yyyy");
                            lb3Thu3.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 3://T4
                            lb3Ngay3.Text = dngay.ToString("dd/MM/yyyy");
                            lb3Thu4.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 4://T5
                            lb3Ngay4.Text = dngay.ToString("dd/MM/yyyy");
                            lb3Thu5.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 5://T6
                            lb3Ngay5.Text = dngay.ToString("dd/MM/yyyy");
                            lb3Thu6.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 6://T7
                            lb3Ngay6.Text = dngay.ToString("dd/MM/yyyy");
                            lb3Thu7.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;

                    }
                }
                int Beginngaytuan4 = Maxngaytuan3 + 1;
                int Maxngaytuan4 = Beginngaytuan4 + 6;
                if (ngay >= Beginngaytuan4 && ngay <= Maxngaytuan4)//tuan 4
                {
                    int thu = (int)dngay.Date.DayOfWeek;//cls.getThu(ngay1);//0 CN
                    switch (thu)
                    {
                        case 0://CN
                            lb4Ngay0.Text = dngay.ToString("dd/MM/yyyy");
                            lb4ThuCN.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 1://T2
                            lb4Ngay1.Text = dngay.ToString("dd/MM/yyyy");
                            lb4Thu2.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 2://T3
                            lb4Ngay2.Text = dngay.ToString("dd/MM/yyyy");
                            lb4Thu3.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 3://T4
                            lb4Ngay3.Text = dngay.ToString("dd/MM/yyyy");
                            lb4Thu4.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 4://T5
                            lb4Ngay4.Text = dngay.ToString("dd/MM/yyyy");
                            lb4Thu5.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 5://T6
                            lb4Ngay5.Text = dngay.ToString("dd/MM/yyyy");
                            lb4Thu6.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 6://T7
                            lb4Ngay6.Text = dngay.ToString("dd/MM/yyyy");
                            lb4Thu7.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;

                    }
                }
                int Beginngaytuan5 = Maxngaytuan4 + 1;
                int Maxngaytuan5 = Beginngaytuan5 + 6;
                if (ngay >= Beginngaytuan5 && ngay <= Maxngaytuan5)//tuan5
                {
                    int thu = (int)dngay.Date.DayOfWeek;//cls.getThu(ngay1);//0 CN
                    switch (thu)
                    {
                        case 0://CN
                            lb5Ngay0.Text = dngay.ToString("dd/MM/yyyy");
                            lb5ThuCN.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 1://T2
                            lb5Ngay1.Text = dngay.ToString("dd/MM/yyyy");
                            lb5Thu2.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 2://T3
                            lb5Ngay2.Text = dngay.ToString("dd/MM/yyyy");
                            lb5Thu3.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 3://T4
                            lb5Ngay3.Text = dngay.ToString("dd/MM/yyyy");
                            lb5Thu4.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 4://T5
                            lb5Ngay4.Text = dngay.ToString("dd/MM/yyyy");
                            lb5Thu5.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 5://T6
                            lb5Ngay5.Text = dngay.ToString("dd/MM/yyyy");
                            lb5Thu6.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 6://T7
                            lb5Ngay6.Text = dngay.ToString("dd/MM/yyyy");
                            lb5Thu7.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;

                    }
                }
                if (ngay > Maxngaytuan5)//tuan6 quay nguoc ve tuan 1
                {
                    int thu = (int)dngay.Date.DayOfWeek;//cls.getThu(ngay1);//0 CN
                    switch (thu)
                    {
                        case 0://CN
                            lb1Ngay0.Text = dngay.ToString("dd/MM/yyyy");
                            lb1ThuCN.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                                + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 1://T2
                            lb1Ngay1.Text = dngay.ToString("dd/MM/yyyy");
                            lb1Thu2.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 2://T3
                            lb1Ngay2.Text = dngay.ToString("dd/MM/yyyy");
                            lb1Thu3.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 3://T4
                            lb1Ngay3.Text = dngay.ToString("dd/MM/yyyy");
                            lb1Thu4.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 4://T5
                            lb1Ngay4.Text = dngay.ToString("dd/MM/yyyy");
                            lb1Thu5.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 5://T6
                            lb1Ngay5.Text = dngay.ToString("dd/MM/yyyy");
                            lb1Thu6.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;
                        case 6://T7
                            lb1Ngay6.Text = dngay.ToString("dd/MM/yyyy");
                            lb1Thu7.Text = "<b>Sáng:</b> " + item.PurposeMorning + "(" + item.TenTinhSang + "-" + item.TenHuyenSang + ")<br/><br/>"
                               + "<b>Chiều:</b> " + item.PurposeAfter + "(" + item.TenTinhChieu + "-" + item.TenHuyenChieu + ")";
                            break;

                    }
                }

            }
        }
    }


}