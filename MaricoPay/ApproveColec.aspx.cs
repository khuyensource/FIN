using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using MaricoPay.DB;
namespace MaricoPay
{
    public partial class ApproveColec : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
      //  private string usernamestatic = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["ckusername"] != null && cls.cToString(Request.Cookies["ckusername"].Value) != "")
            {
                if (cls.cToString(Session["username"]) == "")
                {
                    //chua login thi clear cookies và trả về trang login
                    // Response.Cookies["ckusername"].Expires = DateTime.Now.AddDays(-1);
                    var cookie = HttpContext.Current.Request.Cookies["ckusername"];
                    if (cookie != null)
                    {
                        var cookieName = cookie.Name;
                        var expiredCookie = new HttpCookie(cookieName) { Expires = DateTime.Now.AddDays(-1) };
                        HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                    }
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    if (Request.Cookies["ckusername"].Value != cls.cToString(Session["username"]))
                    {
                        // Đã login bằng user khác nên sẽ reload lại trang với session mới
                        Response.Redirect(Request.RawUrl, false);
                    }
                    else { goto ditiep; }
                }
            }
            else
            {
                if (cls.cToString(Session["username"]) == "")
                {
                    //chua login
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    //đã login và trước đó chưa có cookies (chưa có user nào đăng nhập trước đó)
                    HttpCookie ckusername = new HttpCookie("ckusername");
                    if (cls.cToString(ckusername.Value) != cls.cToString(Session["username"]))
                    {
                        ckusername.Value = cls.cToString(Session["username"]);
                        ckusername.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(ckusername);
                    }
                    Response.Redirect(Request.RawUrl, false);
                }
            }
        ditiep:
            if (!IsPostBack)
            {
                if (Session["username"] != null /*&& Request.Form["us"] != null*/)
                // if (Request.QueryString["us"] != null)//click vao avatar
                {
                    //usernamestatic = cls.cToString(Session["username"]);
                    LoadClaimApp(Session["username"]);
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        private void LoadClaimApp(object username)
        {

            DataTable kq = new DataTable();

            kq = cls.GetDataTable("sp_getApprovedALLColec", new string[] { "@username", "@Type" }, new object[] { username, "ECSALES" });

            RadGrid1.DataSource = kq;
            RadGrid1.DataBind();

        }

        protected void btSelectALL_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in RadGrid1.Items)
            {
                CheckBox chkapp = (CheckBox)item["ApproveColum"].FindControl("chkApprove");
                chkapp.Checked = true;
            }
        }

        protected void btUnselect_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in RadGrid1.Items)
            {
                CheckBox chkapp = (CheckBox)item["ApproveColum"].FindControl("chkApprove");
                chkapp.Checked = false;
            }
        }

        protected void btRelease_Click(object sender, EventArgs e)
        {
            //if (usernamestatic.ToLower() != cls.cToString(Session["username"]).ToLower() && usernamestatic != "")
            //{
            //    MsgBox1.AddMessage("Bạn đã login bằng user khác nên không thể release chứng từ này " + usernamestatic.ToLower() + "=" + cls.cToString(Session["username"]).ToLower(), uc.ucMsgBox.enmMessageType.Error);
            //}
            //else
            //{
            foreach (GridDataItem item in RadGrid1.Items)
            {
                CheckBox chkapp = (CheckBox)item["ApproveColum"].FindControl("chkApprove");
                if (chkapp.Checked)
                {
                    Approve(item["DocNo"].Text, item["Email"].Text, item["FullName"].Text, item["PhongBan"].Text, item["Purpose"].Text, item["DaTamUng"].Text, item["TuNgay"].Text, item["DenNgay"].Text);

                }
            }
            LoadClaimApp(Session["username"]);
          //  }
        }

        protected void Approve(string docno, string emailcreate, string nguoidenghi,string phongban,string mucdich,string datamung,string tungay,string denngay)
        {
            clsSys sys = new clsSys();
            // string docno = dropApp.SelectedValue;
            bool kq;
            kq = cls.bCapNhat(new string[] { "@docno", "@username", "@status", "@DateApp", "@Note" }, new object[] { docno, Session["username"], 1, DateTime.Now, "Approve collect" }, "sp_updateAppbyName");
            //kiem tra xem co can chuyen chung tu len cap cao hon
            if (kq == true)
            {
                //  LoadClaimApp(cls.cToString(Session["username"]));
                // btAdd.Visible = false;
                // btReject.Visible = false;
                DataTable tbl = cls.GetDataTable("sp_getNextLevelClaim", new string[] { "@Docno", "@username" }, new object[] { docno, Session["username"] });
                if (tbl.Rows.Count > 0)
                {
                    //co chuyen len tren
                    //lay thong tin nguoi tren
                    //clsSys sys = new clsSys();
                    string EmailCreate = cls.cToString(tbl.Rows[0]["EmailCreate"]);
                    string emailnextlevel = cls.cToString(tbl.Rows[0]["Approval"]);
                    SenEmailSubmit(docno, emailnextlevel, cls.cToString(Session["username"]),nguoidenghi,phongban,mucdich,datamung,tungay,denngay);
                    sys.SendMailASP(EmailCreate, "Claim Expenses has been sent to next level", "Claim Expenses " + docno + " has been approved by N+1 and submit to Next level (" + emailnextlevel + ")");
                  //  MsgBox1.AddMessage("Approved. Notification of the approval (" + docno + ") will be submitted to the next level and copy to the requestor", uc.ucMsgBox.enmMessageType.Success);
                }
                else
                {

                    // string emailhanhchanh = cls.GetString("sp_getEmailHanhChanh", new string[] { "@Code" }, new object[] { docno });
                    // string cc = emailcreate;
                    //if (emailhanhchanh != "")
                    //{
                    //    cc = cc + ";" + emailhanhchanh;
                    //}
                    //de la nguoi duyet cuoi cung
                    kq = sys.SendMailASP(emailcreate, "Expenses claim request has been Approved", "Expenses claim request  " + docno + " has been approved by " + cls.cToString(Session["username"]));
                    //  MsgBox1.AddMessage("Approved! Notification of the approval (" + docno + ") will be sent to the requestor", uc.ucMsgBox.enmMessageType.Success);
                }

            }
        }
        private void SenEmailSubmit(string code, string to, string appby, string nguoidenghi,string phongban,string mucdich,string datamung,string tungay,string denngay)
        {
            clsSys sys = new clsSys();

            // Guid activationCode = Guid.NewGuid();
            //DBTableDataContext db = new DBTableDataContext();
            // db.ClaimExpenses.InsertAllOnSubmit
            string activationCode = cls.GetString("sp_getGuiCode", new string[] { "@docno", "@approval" }, new object[] { code, to });
            //using (var db = new DBTableDataContext())
            //{
            //    var model = db.ClaimExpenses.SingleOrDefault(p => p.Code_PK == code);
            //    model.ApprovedCode1 = activationCode;
            //    db.SubmitChanges();
            //}

            // string to = txtAppEmail.Text;
            // string cc = txtMyEmail.Text;


          //  string nguoidenghi = txtName.Text;
           // string phongban = comboDepartment1.Text;
           // string mucdich = txtPurpose.Text;
           // string datamung = cls.FormatNumber(radnumAdvncedAmount.Value);
            string thoigian = "Từ/From " + tungay + " Đến/To " + denngay;

            string html = "";
            html = "<table><tr><td>Người đề nghị/Requester: <b>" + nguoidenghi + "</b> Phòng ban/Dept: " + phongban + "</td></tr>";
            html = html + "<tr><td>Nội dung thanh toán/Purpose: <b>" + mucdich + "</b></td></tr>";
            html = html + "<tr><td>Thời gian/Length of days: <b>" + thoigian + "</b></td></tr>";
            html = html + "<tr><td>Đã tạm ứng/Advanced: <b>" + datamung + " VNĐ</b></td></tr>";
            html = html + "</table>";
            html = html + "<table cellpadding=\"2\" cellspacing=\"0\" style=\"width: 100%; border: 1px solid black; border-collapse: collapse; font-size: 12px;\">";
            html = html + "<thead>";
            html = html + "<tr style=\"border: 1px solid black; border-collapse: collapse\">";
            html = html + "<th align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"  rowspan=\"2\">STT<br />No</th>";
            html = html + "<th align=\"center\" colspan=\"2\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Hóa đơn/Invoice</th>";
            html = html + "<th rowspan=\"2\" align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Chi tiết chi phí<br />Detail of Expenses</th>";
            html = html + "<th align=\"center\" rowspan=\"2\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Người tham gia<br /> Participant </th>";
            html = html + "<th align=\"center\" rowspan=\"2\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Loai CP<br /> Charge type </th>";
            html = html + "<th align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\" rowspan=\"2\">Kế hoạch công tác<br /> Working plan </th>";
            html = html + " <th align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\" rowspan=\"2\"> Thành tiền (Ko VAT)<br /> Amount (Non VAT)</th>";
            html = html + "<th align=\"center\" rowspan=\"2\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\">Tiền thuế<br /> Tax Amount</th>";
            html = html + "<th align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\" rowspan=\"2\"> Thành tiền VND<br /> Amount Total</th>";
            html = html + "<th align=\"center\" rowspan=\"2\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\"> GL</th>";
            html = html + "<th align=\"center\" rowspan=\"2\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\"> IO</th>";
            html = html + "</tr>";
            html = html + "<tr style=\"border: 1px solid black; border-collapse: collapse\">";
            html = html + "<th align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> Ngày/Date</th>";
            html = html + "<th align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Số/No</th>";
            html = html + "</tr>";
            html = html + " </thead>";
            html = html + " <tbody>";
            DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            List<sp_getClaimDetailResult> kq1 = dbs.sp_getClaimDetail(code, false).OrderBy(m=>m.Date).ToList();//sp_getClaimDetail LA STORE
            Session["ClaimDetailPrintSales"] = kq1;
            dbs.Dispose();
            string kq = FillTableemail(cls.cToDouble(datamung));
            html = html + kq;

            html = html + "</tbody></table>";

            string who = "";
            if (appby != "")
            {
                who = "(has been approved by " + appby + ")";
            }
            String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
            bool kq2 = sys.SendMailASP(to, "Approve Expense Claim", "Dear Sir/Madam,<br/><br/>Please approve Claim number " + code + who + ". <a href = '" + strUrl + "/ClaimExpensesOffice.aspx?ActivationCode=" + activationCode + "&code=" + code + "'>Click here to approve.</a> or </br><a href = '" + strUrl + "/ClaimExpensesOffice.aspx?RejectedCode=" + activationCode.ToString() + "&code=" + code + "'>Click here to Reject.</a></br></br>" + html + "</br>Best Regards,");

            //if (kq2 == true)
            //{
            //    //btSave.Visible = false;
            //  //  btDelete.Visible = false;
            //   // btSubmit.Visible = false;
            //  //  lbStatus.Text = "Submitted";
            //  //  lbMess.Text = "Submitted successfully!";
            //    MsgBox1.AddMessage("Submitted successfully!", uc.ucMsgBox.enmMessageType.Success);
            //}
            //else
            //{
            //    cls.bXoa(new string[] { "@Docno", "@username" }, new object[] { code, to }, "sp_DeleteApproveByEmail");
            //    lbStatus.Text = "Saved";
            //    lbMess.Text = "Failed to submit";
            //    MsgBox1.AddMessage("Failed to send, Please try again!", uc.ucMsgBox.enmMessageType.Error);
            //}

        }
        private string FillTableemail(double datamung)
        {
            string kq = "";

            List<sp_getClaimDetailResult> tbl = (List<sp_getClaimDetailResult>)Session["ClaimDetailPrintSales"];

            double tongtien = 0;
            int stt = 0;
            foreach (sp_getClaimDetailResult item in tbl)
            {

                if (item.TotalVN != 0)
                {
                    if (item.Date == new DateTime(2000, 1, 1))
                    {
                        //dong subtotal

                        kq = kq + "<tr><td colspan=7 style='color: #000000; font-weight: bold; text-align:left; border:1px solid black; border-collapse:collapse;'>Subtotal</td>";

                        kq = kq
                          + "<td colspan=4 style='color: #000000; font-weight: bold; border:1px solid black; border-collapse:collapse; text-align:left;'>" + String.Format("{0:0,0}", item.TotalVN) + "</td>"
                          + "</tr>";
                    }
                    else
                    {
                        stt++;
                        tongtien = tongtien + cls.cToDouble(item.TotalVN);
                        kq = kq + "<tr><td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + cls.cToString0(stt) + "</td><td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Date.ToString("dd-MMM-yy") + "</td>"
                                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>"
                                           + "<a  href='#' onclick=\"javascript:window.open('/popVAT.aspx?cp=" + item.CompanyName + "&pv=" + item.Province + "&vatcode=" + item.VATCode + "&taxnumber=" + item.TaxNumber + "&vatamount=" + item.VATAmount + "','VAT','width=500,height=150')\">" + item.No + "</a></td>";


                        kq = kq
                           + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.DetailExpenses + "</td>"
                            + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Participant + "</td>"
                             + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.ChargeTypeDs + "</td>";

                        kq = kq + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.WorkingPlanDetail + "</td>"
                     + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.Amount) + "</td>"
                    + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.VATAmount) + "</td>";

                        kq = kq
                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.TotalVN) + "</td>"
                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.GL + "</td>"
                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.IO + "</td>"
                          + "</tr>";
                    }


                }
            }
            //tinh tong tien
            kq = kq + "<tr><td colspan=9 style='color: #000000; font-weight: bold; text-align:right;'>Tổng tiền VND/Total Amount:</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + cls.FormatNumber(tongtien) + "</td><td colspan=2></td></tr>";
            kq = kq + "<tr><td colspan=9 style='color: #000000; font-weight: bold; text-align:right;'>Đã tạm ứng VND/Advanced Amount:</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + cls.FormatNumber(datamung) + "</td><td colspan=2></td></tr>";
            kq = kq + "<tr><td colspan=9 style='color: #000000; font-weight: bold; text-align:right;'>Chênh lệch VND/Pay back(+)/Reimbursemet(-):</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + cls.FormatNumber(tongtien - datamung) + "</td><td colspan=2></td></tr>";
            return kq;
        }
    }
}