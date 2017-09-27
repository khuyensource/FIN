using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MaricoPay.DB;
namespace MaricoPay
{
    public partial class PrintTravelRequest : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //gridSum.DefaultCellStyle.Font = new Font("Tahoma", 12);
                if (Session["docno"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    clsSys sys = new clsSys();
                    string docno = Session["docno"].ToString();
                    // string us = Session["username"].ToString();
                    DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
                    var kq = dbs.sp_GetTravelRequest_Print(docno).ToList();

                    if (kq.Count > 0)
                    {
                        lbType.Text ="PHIẾU YÊU CẦU CÔNG TÁC<br/>BUSINESS TRAVELING REQUISITION";// kq[0].TypeText;

                        lbDate.Text = kq[0].DateRec.Value.ToString("dd-MMM-yy");
                     
                        lbDepartment.Text = kq[0].Department;
                        lbName.Text = kq[0].Fullname;
                      //  lbPosition.Text = kq[0].Position;
                        lbContentPurpose.Text = kq[0].Purpose;
                        lbDocNo.Text = docno;
                        lbNodays.Text = cls.cToString0(kq[0].NoDays);
                        lbTungay.Text = cls.cToString(kq[0].FDate.Value.ToString("dd-MMM-yy"));
                        lbDenngay.Text = cls.cToString(kq[0].TDate.Value.ToString("dd-MMM-yy"));
                        lbNoiDen.Text = kq[0].Destination;
                        lbLoTrinh.Text = kq[0].Itenerary;
                        lbTamUng.Text =cls.FormatNumber(kq[0].Advance);
                        chkOto.Checked=cls.cToBool(kq[0].ByCar);
                        chkTauHoa.Checked=cls.cToBool(kq[0].ByTrain);
                        chkMayBay.Checked=cls.cToBool(kq[0].ByPlane);
                        chkOther.Checked = cls.cToBool(kq[0].Other);
                        lbOther.Text = kq[0].DetailOther;
                        chkDatPhong.Checked=cls.cToBool(kq[0].BookHotel);
                        chkVeTauMayBay.Checked = cls.cToBool(kq[0].BookTicket);
                        // hdTamUng.Value = cls.cToString0(kq[0].DaTamUng);
                        lbAppFuction.Text = kq[0].Approver;
                        lbRequest.Text = kq[0].Fullname;
                        ltSign.Text = "<a href='#'><img id='imgApproved' src='/ImagesSignature/" + kq[0].Signture + "' alt='APPROVED' style='width:80px;height:60px;'/></a>";

                     
                       // List<sp_getClaimDetailResult> kq1 = dbs.sp_getClaimDetail(docno,true).OrderBy(o => o.ID).ToList();//sp_getClaimDetail LA STORE
                       // List<sp_getClaimDetailResult> kq1 = dbs.sp_getClaimDetail(docno, true).ToList();//sp_getClaimDetail LA STORE
                      //  Session["ClaimDetailPrint"] = kq1;
                        FillTable(docno);
                       // LoadSum(docno);
                        dbs.Dispose();
                        // Session["NewDocNo"] = code;
                        // pnlPrintClaim
                        PrintHelper.PrintWebControl(pnlPrintClaim);
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
        private string Bool2Yes(bool b)
        {
            return b ? "Yes" : "No";
        }
        private void FillTable(string code)
        {
            if (Session["TravelDetail"] == null)
            {
                DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
                List<sp_getTravelDetailResult> kq = dbs.sp_getTravelDetail(code).OrderBy(o => o.ID).ToList();//sp_getClaimDetail LA STORE
                if (code == "0" || code == "")
                {
                    kq.RemoveAt(0);
                }
                Session["TravelDetail"] = kq;
            }
            List<sp_getTravelDetailResult> tbl = (List<sp_getTravelDetailResult>)Session["TravelDetail"];
             Literal1.Text = "";
             double tongtien = 0;
             double tongtientamung = 0;
             double sotien = 0;
             double sotienung = 0;
             double soluong = 0;
            double soluongung = 0;
             int stt = 0;
             foreach (sp_getTravelDetailResult item in tbl)
            {

                if (item.Cong != 0)
                {
                    
                        stt++;
                        tongtien = tongtien + cls.cToDouble(item.Cong);
                      sotien=sotien+cls.cToDouble(item.SoTien);
                                soluong=soluong+cls.cToDouble(item.SoLuong);
                        if (item.IsTamUng == true)
                        {
                            tongtientamung = tongtientamung + cls.cToDouble(item.Cong);
                            sotienung=sotienung+cls.cToDouble(item.SoTien);
                                soluongung=soluongung+cls.cToDouble(item.SoLuong);
                        }
                        Literal1.Text = Literal1.Text + "<tr><td style=\"color: #000000; border:1px solid black; border-collapse:collapse;\">" + cls.cToString0(stt) + "</td>";
                                         


                        Literal1.Text = Literal1.Text
                           + "<td style=\"color: #000000; border:1px solid black; border-collapse:collapse;\">" + item.Description + "</td>"
                            + "<td style=\"color: #000000; border:1px solid black; border-collapse:collapse;\">" +String.Format("{0:0,0}", item.SoTien) + "</td>";

                        Literal1.Text = Literal1.Text + "<td style=\"color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;\">" + String.Format("{0:0,0}", item.SoLuong) + "</td>";

                        Literal1.Text = Literal1.Text + "<td style=\"color: #000000; border:1px solid black; border-collapse:collapse; text-align:center;\">" + Bool2Yes(cls.cToBool(item.IsTamUng)) + "</td>";

                        Literal1.Text = Literal1.Text
                          + "<td style=\"color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;\">" + String.Format("{0:0,0}", item.Cong) + "</td>"
                          + "</tr>";
                 

                }
            }
            //tinh tong tien
             Literal1.Text = Literal1.Text + "<tr><td colspan=2 style=\"color: #000000; font-weight: bold; text-align:right;\">Tổng tiền/Total:</td><td style=\"color: #000000; font-weight: bold; text-align:right;\">" + cls.FormatNumber(sotien) + " </td><td style=\"color: #000000; font-weight: bold; text-align:right;\">" + cls.FormatNumber(soluong) + " </td><td></td><td style=\"color: #000000; font-weight: bold; text-align:right;\">" + cls.FormatNumber(tongtien) + "</td></tr>";
             Literal1.Text = Literal1.Text + "<tr><td colspan=2 style=\"color: #000000; font-weight: bold; text-align:right;\">Đề nghị tạm ứng/Advance request:</td><td style=\"color: #000000; font-weight: bold; text-align:right;\">" + cls.FormatNumber(sotienung) + " </td><td style=\"color: #000000; font-weight: bold; text-align:right;\">" + cls.FormatNumber(soluongung) + " </td><td></td><td style=\"color: #000000; font-weight: bold; text-align:right;\">" + cls.FormatNumber(tongtientamung) + "</td></tr>";
        }
       
    }
}