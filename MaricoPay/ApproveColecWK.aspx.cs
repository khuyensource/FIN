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
    public partial class ApproveColecWK : System.Web.UI.Page
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

            kq = cls.GetDataTable("sp_getApprovedALLColecWK", new string[] { "@username", "@Type" }, new object[] { username, "TRSALES" });

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
            string kqs = "";
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
                 string kq= Approve(item["DocNo"].Text, item["Email"].Text, item["FullName"].Text, item["PhongBan"].Text, item["Purpose"].Text, item["TuNgay"].Text, item["DenNgay"].Text);
                 kqs = kqs +"/n/r"+ kq;
                }
            }
            CacheHelper.RemoveLikeKey("creportwk_");
            LoadClaimApp(Session["username"]);
            MsgBox1.AddMessage(kqs, uc.ucMsgBox.enmMessageType.Info);
          //  }
        }
        private void FillTable(string code)
        {
            //RadGrid1.DataSource = null;
            //RadGrid1.DataBind();
            DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            List<sp_getTravelDetailSalesResult> kq = dbs.sp_getTravelDetailSales(code).OrderBy(o => o.FDate).ToList();//sp_getClaimDetail LA STORE
            RadGrid2.DataSource = kq;
            RadGrid2.DataBind();
        }
        protected string Approve(string docno, string emailcreate, string nguoidenghi,string phongban,string mucdich,string tungay,string denngay)
        {
            clsSys sys = new clsSys();
            // string docno = dropApp.SelectedValue;
            bool kq;
            string kqstring = "";
        //    string kqfalse = "";
            kq = cls.bCapNhat(new string[] { "@docno", "@username", "@status", "@DateApp", "@Note" }, new object[] { docno, Session["username"], 1, DateTime.Now, "Approve collect" }, "sp_updateAppbyName");
            //kiem tra xem co can chuyen chung tu len cap cao hon
            if (kq == true)
            {
                //  LoadClaimApp(cls.cToString(Session["username"]));
                // btAdd.Visible = false;
                // btReject.Visible = false;
                DataTable tbl = cls.GetDataTable("sp_getNextLevel", new string[] { "@Docno", "@username" }, new object[] { docno, Session["username"] });
                if (tbl.Rows.Count > 0)
                {
                    //co chuyen len tren
                    //lay thong tin nguoi tren
                    //clsSys sys = new clsSys();
                    string EmailCreate = cls.cToString(tbl.Rows[0]["EmailCreate"]);
                    string emailnextlevel = cls.cToString(tbl.Rows[0]["Approval"]);
                    string activationCode = cls.GetString("sp_getGuiCode", new string[] { "@docno", "@approval" }, new object[] { docno, emailnextlevel });
                    if (cls.SenEmailSubmitWorkingPlan(docno, emailnextlevel, cls.cToString(Session["username"]), activationCode, nguoidenghi, phongban, mucdich, tungay, denngay, RadGrid2, Request.Url.Authority) == true)
                    {
                        kqstring = "Đã duyệt: " + EmailCreate + "(" + docno + ");";
                        //lbStatus.Text = "Submitted";
                        //lbMess.Text = "Submitted successfully!";
                        //MsgBox1.AddMessage("Submitted successfully!", uc.ucMsgBox.enmMessageType.Success);
                        //sent email cc ve cho nguoi tao biet da chuyen len next level
                        sys.SendMailASP(EmailCreate, cls.cToString(Session["email"]), "Travel request has been sent to next level", "Travel request " + docno + " has been approved by N+1 and submit to Next level (" + emailnextlevel + ")");
                        // MsgBox1.AddMessage("Approved! Notification of the approval (" + docno + ") will be submitted to the next level and copy to the requestor", uc.ucMsgBox.enmMessageType.Success);
                    }
                    else
                    {
                        cls.bXoa(new string[] { "@Docno", "@username" }, new object[] { docno, emailnextlevel }, "sp_DeleteApproveByEmail");
                        kqstring = "Lỗi chưa duyệt được: " + EmailCreate + "(" + docno + ");";
                        //lbStatus.Text = "Saved";
                        //lbMess.Text = "Failed to submit";
                        //MsgBox1.AddMessage("Failed to send, Please try again!", uc.ucMsgBox.enmMessageType.Error);
                    }


                }
                else
                {
                   // CacheHelper.RemoveLikeKey("creportwk_");
                    kqstring = "Đã duyệt: " + emailcreate + "(" + docno + ");";
                    kq = sys.SendMailASP(emailcreate, "Working plan has been Approved", "Working plan " + docno + " has been approved by " + cls.cToString(Session["username"]));
                }

            }
            else
            {
                kqstring = "Lỗi chưa duyệt được: " + emailcreate + "(" + docno + ");";
            }
            return kqstring;
        }
     
    }
}