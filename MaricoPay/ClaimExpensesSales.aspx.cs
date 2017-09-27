using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Linq;
using MaricoPay.DB;
using System.IO;
using System.Collections.Specialized;
using Telerik.Web.UI;
namespace MaricoPay
{
    public partial class ClaimExpensesSales : System.Web.UI.Page
    {
        //them 2 bien nay de fix refresh tu don gui email lai
        //private bool _refreshState;
        //private bool _isRefresh;
        //them 2 ham nay de fix refresh tu don gui email lai
        //protected override void LoadViewState(object savedState)
        //{
        //    object[] AllStates = (object[])savedState;
        //    base.LoadViewState(AllStates[0]);
        //    _refreshState = bool.Parse(AllStates[1].ToString());
        //    _isRefresh = _refreshState == bool.Parse(Session["__ISREFRESH"].ToString());
        //}
        ////them 2 ham nay de fix refresh tu don gui email lai
        //protected override object SaveViewState()
        //{
        //    Session["__ISREFRESH"] = _refreshState;
        //    object[] AllStates = new object[2];
        //    AllStates[0] = base.SaveViewState();
        //    AllStates[1] = !(_refreshState);
        //    return AllStates;
        //}
        Cclass cls = new Cclass();
     //  private string usernamestatic="";
        public bool setTrangThai(object trangthaidb,object value)
        {
            bool kq = false;
            if (trangthaidb == value)
                kq = true;

            return kq;
        }
        public bool isShowReject(object trangthaidb)
        {
            bool kq = false;
            if (cls.cToInt(trangthaidb) == 1)
                kq = true;

            return kq;
        }
        public bool IsEditDetail(object usernamedb,object noidung)
        {
            bool kq = false;
            if (usernamedb == Session["Username"] || usernamedb == "" || noidung=="")
                kq = true;

            return kq;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
            string RejectedCode = !string.IsNullOrEmpty(Request.QueryString["RejectedCode"]) ? Request.QueryString["RejectedCode"] : Guid.Empty.ToString();
            if (activationCode != Guid.Empty.ToString() || RejectedCode != Guid.Empty.ToString())
            {
                goto ditiep;
            }
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
                        Response.Redirect(Request.RawUrl,false);
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
                        Response.Redirect(Request.RawUrl,false);
                }
            }
            ditiep:
            if (!IsPostBack)
            {
                raddateInvoice.MaxDate = DateTime.Now;
                LoadCategory1("ALL");
                LoadLoaiChiPhi("ALL");
                //xet xem co phai truy cap tu link email
                DataTable tbl = new DataTable();
                 activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
                 RejectedCode = !string.IsNullOrEmpty(Request.QueryString["RejectedCode"]) ? Request.QueryString["RejectedCode"] : Guid.Empty.ToString();
                if (activationCode.ToLower() != Guid.Empty.ToString().ToLower())
                {
                    btDelete.Visible = false;
                    dvSum.Visible = false;
                    btExpand.Text = "+";
                    btExpand.ToolTip = "Expand";

                    
                    lbTitle.Text = "Duyệt Đề Nghị Thanh Toán (Sales)<br /> Approve Expenses Claim (Sales)";
                    string code = !string.IsNullOrEmpty(Request.QueryString["code"]) ? Request.QueryString["code"] : "";
                  //  LoadCategory(comboDepartment1.Values);

                  
                  //  LoadMarKet();
                  //  FLoadAdvance(true, true);
                 
                    tbl = cls.GetDataTable("sp_getByGuiCode", new string[] { "@code", "@appcode" }, new object[] { code, activationCode });
                    if (tbl.Rows.Count > 0)
                    {
                        string docno = cls.cToString0(tbl.Rows[0]["Code_PK"]);
                        string EmailCreate = cls.cToString0(tbl.Rows[0]["EmailCreate"]);
                        string EmailAppEmail = cls.cToString0(tbl.Rows[0]["AppEmail"]);
                        string AppEmail = cls.get_UsernameFromEmail(cls.cToString(tbl.Rows[0]["AppEmail"])).ToLower();
                      // LoadClaimApp(AppEmail);
                       
                        string kq = cls.GetString0("sp_updateApp", new string[] { "@docno", "@Appcode", "@status", "@DateApp", "@Note" }, new object[] { docno, activationCode, 1, DateTime.Now, "Approved via Email" });
                        //dropApp.Enabled = false;
                      //  dropApp.SelectedValue = docno;
                        //btApp.Visible = true;
                        //dropSaved.Visible = false;
                        //btReject.Visible = true;
                        //txtAppNote.ReadOnly = false;
                        //txtAppNote.Visible = true;
                        //lbReason.Visible = true;
                        //btSave.Visible = false;
                        //btDelete.Visible = false;
                        //btSubmit.Visible = false;
                        //lbStatus.Visible = false;
                        //lbStatusTitle.Visible = false;
                        //GetOldClaim(docno);
                        //getClaimDetail(docno);
                        //FillTable();


                        //check xem co chuyen sang lv tiep ko
                        if (kq == "1")
                        {
                            tbl = cls.GetDataTable("sp_getNextLevelClaim", new string[] { "@Docno", "@username" }, new object[] { docno, activationCode });
                            if (tbl.Rows.Count > 0)
                            {
                                //co chuyen len tren
                                //lay thong tin nguoi tren
                                clsSys sys = new clsSys();
                               // string EmailCreate = cls.cToString(tbl.Rows[0]["EmailCreate"]);
                                string emailnextlevel = cls.cToString(tbl.Rows[0]["Approval"]);
                                string fullname = cls.cToString(tbl.Rows[0]["FullName"]);
                                string depart = cls.cToString(tbl.Rows[0]["Deparment"]);
                                string Purpose = cls.cToString(tbl.Rows[0]["Purpose"]);
                                string tugnay = cls.cToDateTime(tbl.Rows[0]["FDate"]).Value.ToString("dd-MMM-yyyy");
                                string denngay = cls.cToDateTime(tbl.Rows[0]["TDate"]).Value.ToString("dd-MMM-yyyy");
                                string activationCode1 = cls.GetString("sp_getGuiCode", new string[] { "@docno", "@approval" }, new object[] { docno, emailnextlevel });
                                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                                if (cls.SenEmailSubmitClaimWorkingPlan(docno, emailnextlevel, "", activationCode1, fullname, depart, Purpose, tugnay, denngay, strUrl) == true)
                                {
                                    btSave.Visible = false;
                                    btDelete.Visible = false;
                                    btSubmit.Visible = false;
                                    lbStatus.Text = "Submitted";
                                    lbMess.Text = "Submitted successfully!";
                                    MsgBox1.AddMessage("Lưu thành công<br/>Submitted successfully!", uc.ucMsgBox.enmMessageType.Success);
                                }
                                else
                                {
                                    cls.bXoa(new string[] { "@Docno", "@username" }, new object[] { docno, emailnextlevel }, "sp_DeleteApproveByEmail");
                                    lbStatus.Text = "Saved";
                                    lbMess.Text = "Failed to submit";
                                    MsgBox1.AddMessage("Không thể gửi email, vui lòng thử lại<br/>Failed to send, Please try again!", uc.ucMsgBox.enmMessageType.Error);
                                }
                                sys.SendMailASP(EmailCreate, "Đề nghị thanh toán (" + docno + ") của bạn đã được gửi đến cấp tiếp theo (" + emailnextlevel + ") để phê duyệt<br/>Claim Expenses has been sent to next level", "Claim Expenses " + docno + " has been approved by N+1 and submit to Next level (" + emailnextlevel + ")");
                                MsgBox1.AddMessage("Đề nghị thanh toán (" + docno + ") của bạn đã được gửi đến cấp tiếp theo (" + emailnextlevel + ") để phê duyệt<br/>Approved. Notification of the approval (" + docno + ") will be submitted to the next level(" + emailnextlevel + ") and copy to the requester", uc.ucMsgBox.enmMessageType.Success);
                            }
                            else
                            {

                                string emailhanhchanh = cls.GetString("sp_getEmailHanhChanh", new string[] { "@Code" }, new object[] { docno });
                                string cc = EmailAppEmail;
                                if (emailhanhchanh != "")
                                {
                                    cc = cc + ";" + emailhanhchanh;
                                }
                                //de la nguoi duyet cuoi cung
                                clsSys sys = new clsSys();
                                sys.SendMailASP(EmailCreate, cc, "Đề nghị thanh toán " + docno + " của bạn đã được duyệt<br/>Expenses claim request has been Approved", "Expenses claim request  " + docno + " has been approved");
                                MsgBox1.AddMessage("Đã phê duyệt xong, hệ thống sẽ gửi email thông báo đến người đề nghị <br/>Approved! Notification of the approval (" + docno + ") will be sent to the requester", uc.ucMsgBox.enmMessageType.Success);
                            }
                        }
                        else
                        {
                            switch (kq)
                            {
                                case "0":
                                    MsgBox1.AddMessage("Approved fail, please contract IT! (" + docno + ")", uc.ucMsgBox.enmMessageType.Error);
                                    break;
                                case "3":
                                    MsgBox1.AddMessage("This document has already been approved (" + docno + ")", uc.ucMsgBox.enmMessageType.Error);
                                    break;
                                case "4":
                                    MsgBox1.AddMessage("This document has already been rejected (" + docno + ")", uc.ucMsgBox.enmMessageType.Error);
                                    break;
                            }
                        }
                    }

                    //  db.Dispose();

                }
                else
                {
                    if (RejectedCode.ToLower() != Guid.Empty.ToString().ToLower())
                    {
                        btDelete.Visible = false;
                        dvSum.Visible = false;
                        btExpand.Text = "+";
                        btExpand.ToolTip = "Expand";
                        lbTitle.Text = "Duyệt Đề Nghị Thanh Toán (Sales)<br /> Approve Expenses Claim (Sales)";
                        
                        string code = !string.IsNullOrEmpty(Request.QueryString["code"]) ? Request.QueryString["code"] : "";


                      //  LoadCategory(comboDepartment1.Values);
                       // LoadMarKet();
                       // FLoadAdvance(true, true);
                        tbl = cls.GetDataTable("sp_getByGuiCode", new string[] { "@code", "@appcode" }, new object[] { code, RejectedCode });
                         if (tbl.Rows.Count > 0)
                        {
                            string docno = cls.cToString0(tbl.Rows[0]["Code_PK"]);
                            string AppEmail = cls.get_UsernameFromEmail(cls.cToString(tbl.Rows[0]["AppEmail"])).ToLower();
                            string EmailCreate = cls.cToString(tbl.Rows[0]["EmailCreate"]).ToLower();
                            txtMyEmail.Text = EmailCreate;
                            LoadClaimApp(AppEmail);

                            dropApp.Enabled = false;
                            dropApp.SelectedValue = docno;
                            btApp.Visible = false;
                            dropSaved.Visible = false;
                            btReject.Visible = true;
                            txtAppNote.ReadOnly = false;
                            txtAppNote.Visible = true;
                            lbReason.Visible = true;
                            btSave.Visible = false;
                            btDelete.Visible = false;
                            btSubmit.Visible = false;
                            lbStatus.Visible = false;
                            lbStatusTitle.Visible = false;
                            //GetOldClaim(docno);
                            //getClaimDetail(docno);
                            //FillTable();
                            if (Session["username"] != null)
                            {
                                Session["usernametemp"] = Session["username"];
                            }
                            Session["username"] = RejectedCode;
                        }
                     
                    }
                    else
                    {
                        // string sss=  Request.Form["us"];
                        if (Session["username"] != null /*&& Request.Form["us"] != null*/)
                        // if (Request.QueryString["us"] != null)//click vao avatar
                        {
                            //usernamestatic=cls.cToString(Session["username"]);
                            Session["indexdit"] = null;

                            hdStatus.Value = "0";
                            hdPrint.Value = "0";
                            //
                            if (Request.QueryString["type"] != null)//click vao avatar
                            {
                                int type = cls.cToInt(Request.QueryString["type"]);
                                switch (type)
                                {
                                    case 0: //tao moi
                                        RadGrid1.EnableViewState = true;
                                        btAdd.Visible = true;
                                        lbTitle.Text = "Đề Nghị Thanh Toán (Sales)<br /> Expenses Claim (Sales)";
                                        dropApp.Visible = false;
                                        dropSaved.Visible = true;
                                        lbStatus.Visible = true;
                                        lbStatusTitle.Visible = true;
                                        RadGrid1.MasterTableView.GetColumn("IO").Display = false;
                                        RadGrid1.MasterTableView.GetColumn("GL").Display = false;
                                        RadGrid1.MasterTableView.GetColumn("ActionColumn").Display = false;
                                        RadGrid1.MasterTableView.GetColumn("EditColumn").Display = true;
                                        RadGrid1.MasterTableView.GetColumn("DeleteColumn").Display = true;
                                        //  radioClaim.Visible = true;
                                        btDelete.Visible = false;
                                        LoadMarKet();
                                        LoadClaim();
                                        GetNewClaim();
                                        //if (comboDepartment1.Values != "")
                                        //{
                                        LoadCategory(hfDepartment.Value);
                                        //}
                                        //else
                                        //{
                                        //    LoadCategory("ALL");
                                        //}
                                        ddlCategory_SelectedIndexChanged(sender, e);
                                        getClaimDetail("");
                                        FLoadAdvance(false, false);
                                        Session["AutoNumber"] = 0;
                                        hdStatus.Value = "0";

                                        setButton(int.Parse(hdStatus.Value));
                                        btDelete.Visible = false;
                                        RadGrid2.Visible = false;

                                        RadGrid2.DataSource = null;
                                        RadGrid2.DataBind();
                                        btSubmit.Visible = false;
                                        btDelete.Visible = false;

                                        break;
                                    case 2://approved
                                        RadGrid1.EnableViewState = true;
                                        dvSum.Visible = false;
                                        btExpand.Text = "Expand";
                                        lbTitle.Text = "Duyệt Đề Nghị Thanh Toán (Sales)<br /> Approve Expenses Claim (Sales)";
                                        dropApp.Visible = true;
                                        dropSaved.Visible = false;
                                        lbStatus.Visible = false;
                                        lbStatusTitle.Visible = false;
                                        btAdd.Visible = false;
                                        RadGrid1.MasterTableView.GetColumn("IO").Display = true;
                                        RadGrid1.MasterTableView.GetColumn("GL").Display = true;
                                        RadGrid1.MasterTableView.GetColumn("ActionColumn").Display = true;
                                        RadGrid1.MasterTableView.GetColumn("EditColumn").Display = false;
                                        RadGrid1.MasterTableView.GetColumn("DeleteColumn").Display = false;
                                        // radioClaim.Visible = false;
                                        btDelete.Visible = false;
                                        LoadCategory(comboDepartment1.Values);
                                        LoadMarKet();
                                        FLoadAdvance(true, true);
                                        hdStatus.Value = "2";
                                        LoadClaimApp(cls.cToString(Session["username"]));
                                        setButton(int.Parse(hdStatus.Value));

                                        string docno = !string.IsNullOrEmpty(Request.QueryString["docno"]) ? Request.QueryString["docno"] : "";
                                        if (docno != "")//goi tu form duyet collect
                                        {
                                            dropApp.SelectedValue = docno;
                                            dropApp.Enabled = false;
                                            dropApp_SelectedIndexChanged(sender, e);
                                        }

                                        break;
                                    case 4://Print
                                        RadGrid1.EnableViewState = true;
                                        btAdd.Visible = false;
                                        hdPrint.Value = "1";
                                        dvSum.Visible = false;
                                        btExpand.Text = "Expand";
                                        lbTitle.Text = "In Đề Nghị Thanh Toán (Sales)<br /> Print Out Expenses Claim (Sales)";
                                        dropApp.Visible = false;
                                        dropSaved.Visible = true;
                                        lbStatus.Visible = false;
                                        lbStatusTitle.Visible = false;
                                        lbAdvance.Visible = true;
                                        dropTravelRequest.Visible = true;
                                        // radioClaim.Visible = false;
                                        btDelete.Visible = false;
                                       
                                        LoadCategory(comboDepartment1.Values);
                                        LoadMarKet();
                                        FLoadAdvance(true, true);
                                        LoadClaimPrint();
                                          string docno1 = !string.IsNullOrEmpty(Request.QueryString["docno"]) ? Request.QueryString["docno"] : "";
                                        if (docno1 != "")//goi tu form duyet collect
                                        {
                                            dropSaved.SelectedValue = docno1;
                                            dropSaved.Enabled = false;
                                           // dropApp_SelectedIndexChanged(sender, e);
                                        }
                                        GetOldClaim(dropSaved.SelectedValue);
                                        getClaimDetail(dropSaved.SelectedValue);
                                        FillTable();
                                        hdStatus.Value = "4";
                                        setButton(int.Parse(hdStatus.Value));
                                      //  setButton(4);
                                      RadGrid1.MasterTableView.GetColumn("IO").Display = true;
                                        RadGrid1.MasterTableView.GetColumn("GL").Display = true;
                                        RadGrid1.MasterTableView.GetColumn("ActionColumn").Display = false;
                                        RadGrid1.MasterTableView.GetColumn("EditColumn").Display = false;
                                        RadGrid1.MasterTableView.GetColumn("DeleteColumn").Display = false;
                                        //btVRequetsTravel.Visible = true;
                                        //btReview.Visible = false;
                                        break;
                                }

                            }
                            else
                            {
                                Response.Redirect("~/Login.aspx");
                            }

                        }
                        else
                        {
                            Response.Redirect("~/Login.aspx");
                        }
                    }
                }
                //else
                //{
                //    // something here

                //    if (!string.IsNullOrEmpty(__EVENTTARGET.Value)) // it should be changed value here...
                //    {
                //        string s = __EVENTTARGET.Value;
                //    }
                //}
            }
            //else
            //{
            //    string docno = !string.IsNullOrEmpty(Request.QueryString["docno"]) ? Request.QueryString["docno"] : "";
               
            //    if (Request.QueryString["type"] != null)
            //    {
            //        int type = cls.cToInt(Request.QueryString["type"]);
                    
            //        if (docno != "" && type == 2)//goi tu form duyet collect
            //        {
            //            getClaimDetail(dropApp.SelectedValue);
            //            FillTable();
            //            // dropApp_SelectedIndexChanged(sender, e);
            //        }
            //    }
            //}
        }
        private void LoadCategory1(string costcenter)
        {
            DataTable tbl = cls.GetDataTable("sp_LoadChargesTravelSales", "@costcenter", costcenter);
            ddlCategory.DataSource = tbl;
            ddlCategory.DataBind();
        }
        private void LoadCategory(string costcenter)
        {
            DataTable tbl = cls.GetDataTable("sp_LoadChargesClaimSales", "@costcenter", costcenter);
            ddlCategory.DataSource = tbl;
            ddlCategory.DataBind();
        }
        private void LoadMarKet()
        {
            DBTableDataContext ds = new DBTableDataContext();
            var kq = ds.Markets.ToList();//Markets LA TABLE
            dropMarket.DataValueField = "Market_PK";
            dropMarket.DataTextField = "Description";
            dropMarket.DataSource = kq;
            dropMarket.DataBind();
            ds.Dispose();

        }
        private void GetNewClaim()
        {
            string us = Session["username"].ToString();
            DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            var kq = dbs.sp_GetUserInfo(us).ToList();
            // txtMarket.Text = kq[0].Market;
            //   txtDepartment.Text = kq[0].Department;
            // comboDepartment1.Values = kq[0].Department;
            comboDepartment1.Values = kq[0].Costcenter;
            hfDepartment.Value = kq[0].Costcenter;
            txtName.Text = kq[0].Fullname;
            txtPosition.Text = kq[0].Position;
            txtAppName.Text = kq[0].FullNameRec;
            txtAppEmail.Text = kq[0].EmailRec;
            txtMyEmail.Text = kq[0].Email;
            dropMarket.SelectedValue = kq[0].Market;
            raddateNow.SelectedDate = DateTime.Now;
            raddateFrom.SelectedDate = DateTime.Now;
            raddateTo.SelectedDate = DateTime.Now;
            lbStatus.Text = kq[0].StatusText;
            hdVendorSAP.Value = kq[0].vendorSAP;
         //   hdStatus.Value = kq[0].Status.ToString();
            txtAppNote.Visible = false;
            //  txtPurpose.Text = "";
            txtNguoiThuHuong.Text = "";
            radnumAdvncedAmount.Value = 0;
            dbs.Dispose();
          Session["indexdit"] = null;
            Session["NewDocNoClaimSales"] = GenalCode();

        }
        private void GetOldClaim(string code)//0 cho phep them; 1: ko cho phep them
        {
            if (code != "")
            {
                     // string us = Session["username"].ToString();
                     DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
                     var kq = dbs.sp_GetUserInfo_ClaimExists(code).ToList();
                     
                         comboDepartment1.FLoad();
                         comboDepartment1.Values = kq[0].Department;
                         txtName.Text = kq[0].Fullname;
                         txtPosition.Text = kq[0].Position;
                         txtAppName.Text = kq[0].FullNameRec;
                         txtAppEmail.Text = kq[0].EmailRec;
                         txtMyEmail.Text = kq[0].Email;
                         dropMarket.SelectedValue = kq[0].Market;
                         raddateNow.SelectedDate = kq[0].DateRec;
                         //  radioClaim.SelectedValue = kq[0].Type;
                         txtPurpose.Text = kq[0].Purpose;
                         raddateFrom.SelectedDate = kq[0].FDate;
                         raddateTo.SelectedDate = kq[0].TDate;
                         radnumAdvncedAmount.Value = (double)kq[0].DaTamUng;
                         try
                         {
                             dropTravelRequest.SelectedValue = kq[0].AdvanceDocNo;
                         }
                         catch { }
                         //  radnumDays.Value =(double) kq[0].NoDays;
                         lbStatus.Text = kq[0].StatusText;
                         hdStatus.Value = kq[0].Status.ToString();
                         txtAppNote.Visible = true;
                         txtAppNote.Text = kq[0].NoteApprover;
                         txtNguoiThuHuong.Text = kq[0].NguoiThuHuong;
                         dbs.Dispose();
                         Session["NewDocNoClaimSales"] = code;
                         Session["indexdit"] = null;
                         //string stkq = cls.GetString0("StatusFinalSales", new string[] { "@travaldocno" }, new object[] { kq[0].AdvanceDocNo });
                         //if (cls.cToBool(stkq) == true)
                         //{
                         //    btSubmit.Visible = true;
                         //    btSave.Visible = true;
                         //}
                         //else
                         //{
                         //    btSave.Visible = true;
                         //    btSubmit.Visible = false;

                         //    MsgBox1.AddMessage("Working plan chưa đươc duyệt", uc.ucMsgBox.enmMessageType.Error);
                           
                         //   // hdStatus.Value = "-1";
                         //}
            }
        }
        private void setButton(int status)
        {
            switch (status)
            {
                case 0: //new
                    btAdd.Text = "24.Add";
                    btSave.Visible = true;
                    btDelete.Visible = true;
                    btSubmit.Visible = true;
                    btPrint.Visible = false;
                    txtAppNote.Visible = false;
                    lbReason.Visible = false;
                    btApp.Visible = false;
                    btReject.Visible = false;
                     lbAdvance.Visible = true;
                     dropTravelRequest.Visible = true;
                     dropTravelRequest.Enabled = true;
                     lbStatusTitle.Visible = false;
                     RadGrid2.Visible = false;
                    lbStatus.Visible = false;
                    btAdd.Visible = true;
                    //FLoadAdvance(false, false);
                    break;
                case 1://approved
                    btSave.Visible = false;
                    btAdd.Visible = false;
                    btSubmit.Visible = false;
                    btPrint.Visible = true;
                    txtAppNote.Visible = false;
                    lbReason.Visible = false;
                    txtAppNote.ReadOnly = true;
                    btApp.Visible = false;
                    btReject.Visible = false;
                    lbAdvance.Visible = true;
                    dropTravelRequest.Visible = true;
                    dropTravelRequest.Enabled = false;
                     lbStatusTitle.Visible = true;
                    lbStatus.Visible = true;
                    btDelete.Visible = false;
                    break;
                case 2://submitted
                    btSave.Visible = false;
                    btAdd.Visible = false;
                    btSubmit.Visible = false;
                    btPrint.Visible = false;
                    txtAppNote.Visible = false;
                    lbReason.Visible = false;
                    btApp.Visible = false;
                    btReject.Visible = false;
                     lbAdvance.Visible = true;
                     dropTravelRequest.Enabled = false;
                     dropTravelRequest.Visible = true;
                     lbStatusTitle.Visible = false;
                    lbStatus.Visible = false;
                    btDelete.Visible = false;
                    break;
                case 3://rejected
                    btSave.Visible = true;
                    btAdd.Visible = true;
                    btSubmit.Visible = false;
                    btPrint.Visible = false;
                    txtAppNote.Visible = false;
                    txtAppNote.ReadOnly = true;
                    lbReason.Visible = false;
                    btApp.Visible = false;
                    btReject.Visible = false;
                     lbAdvance.Visible = true;
                     dropTravelRequest.Enabled = false;
                     dropTravelRequest.Visible = true;
                     lbStatusTitle.Visible = false;
                    lbStatus.Visible = false;
                   // FLoadAdvance(false, false);
                    btDelete.Visible = true;
                    break;
                case 4://Print
                    btSave.Visible = false;
                    btAdd.Visible = false;
                    btSubmit.Visible = false;
                    btPrint.Visible = true;
                    txtAppNote.Visible = false;
                    txtAppNote.ReadOnly = true;
                    lbReason.Visible = false;
                    btApp.Visible = false;
                    btReject.Visible = false;
                    lbStatusTitle.Visible = false;
                    lbStatus.Visible = false;
                   btDelete.Visible = false;
                   dropTravelRequest.Enabled = false;
                    // RadGrid1.MasterTableView.GetColumn("IO").Display = true;
                    //RadGrid1.MasterTableView.GetColumn("GL").Display = true;
                    //RadGrid1.MasterTableView.GetColumn("ActionColumn").Display = false;
                    //RadGrid1.MasterTableView.GetColumn("EditColumn").Display = false;
                    //RadGrid1.MasterTableView.GetColumn("DeleteColumn").Display = false;
                    break;
                case -1://working plan chua duoc approved
                    btSave.Visible = false;
                    btAdd.Visible = false;
                    btSubmit.Visible = false;
                    btPrint.Visible = false;
                    txtAppNote.Visible = false;
                    txtAppNote.ReadOnly = false;
                    lbReason.Visible = false;
                    btApp.Visible = false;
                    btReject.Visible = false;
                    lbAdvance.Visible = true;
                    dropTravelRequest.Enabled = true;
                    dropTravelRequest.Visible = true;
                    lbStatusTitle.Visible = false;
                    lbStatus.Visible = false;
                    RadGrid2.Visible = false;
                    // FLoadAdvance(false, false);
                    btDelete.Visible = false;
                    break;
                default:
                    btSave.Visible = true;
                    btAdd.Visible = true;
                    btSubmit.Visible = false;
                    btPrint.Visible = false;
                    txtAppNote.Visible = false;
                    btApp.Visible = false;
                    btReject.Visible = false;
                    lbReason.Visible = false;
                     lbAdvance.Visible = true;
                     dropTravelRequest.Visible = true;
                     dropTravelRequest.Enabled = true;
                    btDelete.Visible = false;
                    break;

            }
        }

        //protected void raddateTo_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        //{
        //    TinhNgay();
        //}

        //protected void raddateFrom_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        //{
        //    TinhNgay();
        //}
        //protected void raddateTo_Sales_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        //{
        //    TinhNgaySales();
        //}

        //protected void raddateFrom_Sales_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        //{
        //    TinhNgaySales();
        //}
        private int TinhNgay()
        {
            int kq = 0;
            clsSys sys = new clsSys();
            if (raddateTo.SelectedDate != null && raddateFrom.SelectedDate != null)
            {
                if (ddlCategory.SelectedValue.ToLower() == "ho")
                {
                    kq = sys.cToInt((raddateTo.SelectedDate.Value - raddateFrom.SelectedDate.Value).TotalDays);
                }
                else
                {
                    kq = sys.cToInt((raddateTo.SelectedDate.Value - raddateFrom.SelectedDate.Value).TotalDays + 1);
                }

            }
            return kq;
        }
        private void TinhNgay(DateTime From,DateTime To)
        {
           // int kq = 0;
            clsSys sys = new clsSys();
            if (To != null && From != null)
            {
                //if (dropLoaiCP.SelectedValue.ToLower() == "ho")
                //{
                //    kq = sys.cToInt((To - From).TotalDays);
                //}
                //else
                //{
                //    kq = sys.cToInt((To - From).TotalDays + 1);
                //}
                radnumSoNgay.Value = sys.cToDuoble((To - From).TotalDays + 1);
                radnumSoDem.Value = sys.cToDuoble((To - From).TotalDays);
            }
            else
            {
                radnumSoNgay.Value =0;
                radnumSoDem.Value = 0;
            }
           // return kq;
        }
        //private double TinhNgaySales()
        //{
        //    double kq = 0;
        //    clsSys sys = new clsSys();
        //    if (raddateTo_Sales.SelectedDate != null && raddateFrom_Sales.SelectedDate != null)
        //    {

        //        kq = (raddateTo_Sales.SelectedDate.Value - raddateFrom_Sales.SelectedDate.Value).TotalDays + 1;
        //    }
        //    return kq;
        //}
        private void LoadClaim()
        {
            DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            var kq = dbs.sp_getClaimSaved(Session["username"].ToString(), "SALES");//sp_getClaimSaved la store co 2 tham so dau vao
            dropSaved.DataSource = kq;
            dropSaved.DataValueField = "Values";
            dropSaved.DataTextField = "Text";
            dropSaved.DataBind();
            dbs.Dispose();
        }
        private void LoadClaimPrint()
        {
            

          //  DataTable kq = cls.GetDataTable("sp_getClaimPrint", new string[] { "@username" }, new object[] { Session["username"]});// dbs.sp_getClaimSaved(Session["username"].ToString(), "");//sp_getClaimSaved la store co 2 tham so dau vao
            DataTable kq = cls.GetDataTable("sp_getAllClaimPrintSales", new string[] { "@username" }, new object[] { Session["username"] });// dbs.sp_getClaimSaved(Session["username"].ToString(), "");//sp_getClaimSaved la store co 2 tham so dau vao
            dropSaved.DataSource = kq;
            dropSaved.DataValueField = "Values";
            dropSaved.DataTextField = "Text";
            dropSaved.DataBind();
            
        }
        private void LoadClaimApp(string username)
        {
          
            DataTable kq = new DataTable();

            kq = cls.GetDataTable("sp_getApprovedALL", new string[] { "@username", "@Type" }, new object[] { username, "ECSALES" });

            dropApp.DataSource = kq;
            dropApp.DataValueField = "Values";
            dropApp.DataTextField = "Text";
            dropApp.DataBind();
            
        }
        private string GenalCode()
        {
            //dd-MM-yy-Automumber
            //     DBTableDataContext dbs = new DBTableDataContext();
           // DBStoreDataContext dbs = new DBStoreDataContext();
           // clsSys sys = new clsSys();
            string code = "EC"+"-"+Session["username"].ToString() + "-" + raddateNow.SelectedDate.Value.ToString("dd-MM-yy") + "-";
            //var kq = dbs.sp_GenaCode(code).ToList();
           string kq= cls.GetString0("sp_GenaCode", new string[] { "@code" }, new object[] { code });
            //dbs.Dispose();
            int rows = 0;
            //if (kq.Count > 0)
            //{
                rows = cls.cToInt(kq);
          //  }
            rows = rows + 1;
            code = code + rows.ToString();

            return code;
        }
        private void FillTable()
        {
            RadGrid1.MasterTableView.DataSource = null;
            List<sp_getClaimDetailSalesResult> tbl = (List<sp_getClaimDetailSalesResult>)Session["ClaimDetailSale"];
            RadGrid1.DataSource = tbl;
           
            int type = 0;
            if(Request.QueryString["type"]!=null)
            {
                type = cls.cToInt(Request.QueryString["type"]);
            }
           
            if (type == 2) //form duyet claim
            {
                RadGrid1.MasterTableView.GetColumn("IO").Display = true;
                RadGrid1.MasterTableView.GetColumn("GL").Display = true;
                RadGrid1.MasterTableView.GetColumn("ActionColumn").Display = true;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Display = false;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Display = false;
                RadGrid1.MasterTableView.GetColumn("TrangThaiS").Display = false;
            }
            else
            {
                RadGrid1.MasterTableView.GetColumn("IO").Display = false;
                RadGrid1.MasterTableView.GetColumn("GL").Display = false;
                RadGrid1.MasterTableView.GetColumn("ActionColumn").Display = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Display = true;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Display = true;
                RadGrid1.MasterTableView.GetColumn("TrangThaiS").Display = true;
            }
            RadGrid1.DataBind();
        }
        private bool SaveParent(string code)
        {
            try
            {
                using (var dbs = new DBTableDataContext())
                {
                    //ClaimExpense LA TABLE
                    var model = new ClaimExpense { Code_PK = code, Type = "SALES", DateRec = raddateNow.SelectedDate.Value, UserName = Session["username"].ToString(), Approver = txtAppName.Text, AppEmail = txtAppEmail.Text.ToLower(), Status = 0, FDate = raddateFrom.SelectedDate.Value, TDate = raddateTo.SelectedDate.Value, NoDays = TinhNgay(), Purpose = txtPurpose.Text, Market = dropMarket.SelectedValue, Department = comboDepartment1.Values, Position = txtPosition.Text, FullName = txtName.Text, DaTamUng = (decimal)radnumAdvncedAmount.Value, Tra_ThuChenhLech = 0, DocTot = 0, NoteApprover = "", Email = txtMyEmail.Text.ToLower(), AdvanceDocNo = dropTravelRequest.SelectedValue, Costcenter = comboDepartment1.getCoscenter, NguoiThuHuong = txtNguoiThuHuong.Text };
                    dbs.ClaimExpenses.InsertOnSubmit(model);
                    dbs.SubmitChanges();

                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool UpdateParent(string code)
        {
            try
            {
                using (var db = new DBTableDataContext())
                {
                    //ClaimExpense LA TABLE

                    var model = db.ClaimExpenses.SingleOrDefault(o => o.Code_PK == code);
                    model.DateRec = raddateNow.SelectedDate.Value;
                    model.UserName = Session["username"].ToString();
                    model.Approver = txtAppName.Text;
                    model.AppEmail = txtAppEmail.Text.ToLower();
                    model.Status = 0;
                    model.FDate = raddateFrom.SelectedDate.Value;
                    model.TDate = raddateTo.SelectedDate.Value;
                    //  model.NoDays = (int)radnumDays.Value;
                    // model.Purpose = txtPurpose.Text;
                    model.Market = dropMarket.SelectedValue;
                    model.Department = comboDepartment1.Values;
                    model.Position = txtPosition.Text;
                    model.FullName = txtName.Text;
                    model.DaTamUng = (decimal)radnumAdvncedAmount.Value;
                    model.AdvanceDocNo = dropTravelRequest.SelectedValue;
                    //   model.Tra_ThuChenhLech = 0;
                    //  model.DocTot = 0;
                    //  model.NoteApprover = "";
                    model.Email = txtMyEmail.Text.ToLower();
                    model.Costcenter = comboDepartment1.getCoscenter;
                    model.NguoiThuHuong = txtNguoiThuHuong.Text;
                    db.SubmitChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void UpdateDetail(string code)
        {
            //delete
            using (var db = new DBTableDataContext())
            {
                db.ClaimExpensesDetails.DeleteAllOnSubmit(db.ClaimExpensesDetails.Where(o => o.Code_FK == code));
                db.SubmitChanges();
            }
            //insert
            SaveDetail(code);
        }
        private void updateDocTot(string code)
        {
            DBStoreDataContext dbs = new DBStoreDataContext();
            dbs.sp_UpdateDocTotal(code);
            dbs.SubmitChanges();
            dbs.Dispose();
        }
        private void SaveDetail(string code)
        {
            DBTableDataContext dbs = new DBTableDataContext();
            try
            {
                List<sp_getClaimDetailSalesResult> tbl = (List<sp_getClaimDetailSalesResult>)Session["ClaimDetailSale"];
                foreach (sp_getClaimDetailSalesResult item in tbl)
                {
                    if (item.TotalVN > 0)
                    {
                        var model = new ClaimExpensesDetail { Code_FK = code, Date = item.Date, No = item.No, TaxNumber = item.TaxNumber, Notation = item.Notation, Charges_FK = item.Charges_FK, Currency = item.Currency, Rate = item.Rate, Amount = item.Amount, TotalVN = item.TotalVN, PictureURL = item.PictureURL, CompanyName = item.CompanyName, Province = item.Province, VATCode = item.VATCode, VATAmount = item.VATAmount, FDate = item.FDate, TDate = item.TDate, NoDays = item.NoDays, Purpose = item.Purpose, CompanyCode = item.CompanyCode, GL = item.GL, IO = item.IO, DetailExpenses = item.DetailExpenses, Participant = item.Participant, FileAttach = item.FileAttach, Costcenter = item.Costcenter, CurrencyDescription = item.CurrencyDescription, NoNight = item.NoNight, IDWorkingPlan = item.IDWorkingPlan, VATPercent = item.VATPercent, WorkingPlanDetail = item.WorkingPlanDetail, ChargeType = item.ChargeType, ChargeTypeDs = item.ChargeTypeDs, Vendor = item.Vendor, TrangThai = 1/*item.TrangThai*/, LyDo = ""/*item.LyDo*/, UserAppReject =""/*item.UserAppReject*/};
                        dbs.ClaimExpensesDetails.InsertOnSubmit(model);
                        cls.bThem(new string[] { "@VendorName", "@Address", "@TaxCode" }, new object[] {item.CompanyName,item.Province,item.TaxNumber }, "sp_insertVendorSales");
                    }
                }
                // dbs.ClaimExpensesDetails.InsertAllOnSubmit(tbl);
                dbs.SubmitChanges();
                // LoadSum(code);
                //save reject item
                lbMess.Text = "Saved successfully";
                btSubmit.Visible = true;

            }
            catch
            {

                #region Delete file
                //string fileToDelete = Server.MapPath("~/" + Lib.clsConfig.Ads + FileName).ToString();
                //if (File.Exists(fileToDelete) && FileName != "NoPhoto.jpg")
                //{
                //    File.Delete(fileToDelete);
                //}
                #endregion
                var model1 = dbs.ClaimExpenses.SingleOrDefault(p => p.Code_PK == code);
                dbs.ClaimExpenses.DeleteOnSubmit(model1);
                dbs.SubmitChanges();
                lbMess.Text = "Save Error";
                btSubmit.Visible = false;
            }
            dbs.Dispose();
        }
        private void UpdateDetail()
        {
          //  DBTableDataContext dbs = new DBTableDataContext();
            try
            {
                string[]bien=new string[]{"@ID","@Code_FK","@Date","@No","@Notation","@Charges_FK","@Currency","@Rate"
           ,"@Amount","@TotalVN","@PictureURL","@CompanyName","@Province"
           ,"@TaxNumber","@VATCode","@VATAmount","@FDate","@TDate"
           ,"@NoDays","@Purpose","@GL","@CompanyCode","@DetailExpenses"
           ,"@Participant","@IO","@FileAttach","@Costcenter","@CurrencyDescription"
           ,"@Vendor","@IDWorkingPlan","@NoNight","@WorkingPlanDetail"
           ,"@VATPercent" ,"@ChargeType" ,"@ChargeTypeDs" ,"@TrangThai","@LyDo"
           ,"@UserAppReject"};
                List<sp_getClaimDetailSalesResult> tbl = (List<sp_getClaimDetailSalesResult>)Session["ClaimDetailSale"];
                foreach (sp_getClaimDetailSalesResult item in tbl)
                {
                    object[]gtri=new object[]{item.ID,item.Code_FK,item.Date,item.No,item.Notation,item.Charges_FK,item.Currency,item.Rate
                    ,item.Amount,item.TotalVN,item.PictureURL,item.CompanyName,item.Province
                    ,item.TaxNumber,item.VATCode,item.VATAmount,item.FDate,item.TDate
                    ,item.NoDays,item.Purpose,item.GL,item.CompanyCode,item.DetailExpenses
                    ,item.Participant,item.IO,item.FileAttach,item.Costcenter,item.CurrencyDescription
                    ,item.Vendor,item.IDWorkingPlan,item.NoNight,item.WorkingPlanDetail
                    ,item.VATPercent,item.ChargeType,item.ChargeTypeDs,item.TrangThai,item.LyDo
                    ,item.UserAppReject};

                    cls.bThem(bien,gtri,"InsertClaimExpensesDetail");
                    //if (item.ID <= 0)
                    //{
                    //    var model = new ClaimExpensesDetail { Code_FK =item.Code_FK, Date = item.Date, No = item.No, TaxNumber = item.TaxNumber, Notation = item.Notation, Charges_FK = item.Charges_FK, Currency = item.Currency, Rate = item.Rate, Amount = item.Amount, TotalVN = item.TotalVN, PictureURL = item.PictureURL, CompanyName = item.CompanyName, Province = item.Province, VATCode = item.VATCode, VATAmount = item.VATAmount, FDate = item.FDate, TDate = item.TDate, NoDays = item.NoDays, Purpose = item.Purpose, CompanyCode = item.CompanyCode, GL = item.GL, IO = item.IO, DetailExpenses = item.DetailExpenses, Participant = item.Participant, FileAttach = item.FileAttach, Costcenter = item.Costcenter, CurrencyDescription = item.CurrencyDescription, NoNight = item.NoNight, IDWorkingPlan = item.IDWorkingPlan, VATPercent = item.VATPercent, WorkingPlanDetail = item.WorkingPlanDetail, ChargeType = item.ChargeType, ChargeTypeDs = item.ChargeTypeDs, Vendor = item.Vendor, TrangThai = 1/*item.TrangThai*/, LyDo = ""/*item.LyDo*/, UserAppReject = ""/*item.UserAppReject*/};
                    //    dbs.ClaimExpensesDetails.InsertOnSubmit(model);
                    //    dbs.SubmitChanges();
                    //    cls.bThem(new string[] { "@VendorName", "@Address", "@TaxCode" }, new object[] { item.CompanyName, item.Province, item.TaxNumber }, "sp_insertVendorSales");
                    //}
                    //else
                    //{
                    //     ClaimExpensesDetail model1 = dbs.ClaimExpensesDetails.SingleOrDefault(p => p.ID == item.ID);
                    //     model1.Code_FK = item.Code_FK;
                    //     model1.Date = item.Date;
                    //     model1.No = item.No; 
                    //     model1.TaxNumber = item.TaxNumber; 
                    //     model1.Notation = item.Notation;
                    //     model1.Charges_FK = item.Charges_FK;
                    //     model1.Currency = item.Currency;
                    //     model1.Rate = item.Rate;
                    //     model1.Amount = item.Amount;
                    //     model1.TotalVN = item.TotalVN;
                    //     model1.PictureURL = item.PictureURL;
                    //     model1.CompanyName = item.CompanyName;
                    //     model1.Province = item.Province;
                    //     model1.VATCode = item.VATCode;
                    //     model1.VATAmount = item.VATAmount;
                    //     model1.FDate = item.FDate;
                    //     model1.TDate = item.TDate;
                    //     model1.NoDays = item.NoDays;
                    //     model1.Purpose = item.Purpose;
                    //     model1.CompanyCode = item.CompanyCode;
                    //     model1.GL = item.GL;
                    //     model1.IO = item.IO;
                    //     model1.DetailExpenses = item.DetailExpenses;
                    //     model1.Participant = item.Participant;
                    //     model1.FileAttach = item.FileAttach;
                    //     model1.Costcenter = item.Costcenter;
                    //     model1.CurrencyDescription = item.CurrencyDescription;
                    //     model1.NoNight = item.NoNight;
                    //     model1.IDWorkingPlan = item.IDWorkingPlan;
                    //     model1.VATPercent = item.VATPercent;
                    //     model1.WorkingPlanDetail = item.WorkingPlanDetail;
                    //     model1.ChargeType = item.ChargeType;
                    //     model1.ChargeTypeDs = item.ChargeTypeDs;
                    //     model1.Vendor = item.Vendor;
                    //     model1.TrangThai = item.TrangThai;
                    //     model1.LyDo = item.LyDo;
                    //     model1.UserAppReject = item.UserAppReject;
                    //     dbs.ClaimExpensesDetails
                       
                    //    cls.bThem(new string[] { "@VendorName", "@Address", "@TaxCode" }, new object[] { item.CompanyName, item.Province, item.TaxNumber }, "sp_insertVendorSales");
                    //}
                }
                // dbs.ClaimExpensesDetails.InsertAllOnSubmit(tbl);
               
                // LoadSum(code);
                //save reject item
                lbMess.Text = "Saved successfully";
                btSubmit.Visible = true;

            }
            catch
            {

                //#region Delete file
                ////string fileToDelete = Server.MapPath("~/" + Lib.clsConfig.Ads + FileName).ToString();
                ////if (File.Exists(fileToDelete) && FileName != "NoPhoto.jpg")
                ////{
                ////    File.Delete(fileToDelete);
                ////}
                //#endregion
                //var model1 = dbs.ClaimExpenses.SingleOrDefault(p => p.Code_PK == code);
                //dbs.ClaimExpenses.DeleteOnSubmit(model1);
                //dbs.SubmitChanges();
                //lbMess.Text = "Save Error";
                //btSubmit.Visible = false;
            }
           // dbs.Dispose();
        }
        private void SaveOneDetail(string code, int trangthai, string lydo, string user)
        {
            DBTableDataContext dbs = new DBTableDataContext();
            try
            {
                Cclass cls = new Cclass();

                if (radnumAmount.Value > 0)
                {
                    var model = new ClaimExpensesDetail { Code_FK = code, TaxNumber = txtVAT.Text.Trim(), Date = raddateInvoice.SelectedDate.Value, No = txtNoInvoice.Text, Notation =txtNotationInvoice.Text, Charges_FK = ddlCategory.SelectedValue, Currency = "VND", Rate = 1, Amount = cls.cToDecimal(radnumAmount.Value), TotalVN = cls.cToDecimal(radnumAmount.Value) + cls.cToDecimal(radnuTaxAmount.Value), PictureURL = "", CompanyName = txtCompanyName.Text, Province = txtDiaChi.Text, VATCode = dropTaxcode.SelectedItem.Text, VATAmount = radnuTaxAmount.Value, FDate = raddateFrom.SelectedDate.Value, TDate = raddateTo.SelectedDate.Value, NoDays = TinhNgay(), Purpose = txtPurpose.Text, CompanyCode = "", GL = txtGL.Text, IO = txtIO.Text, DetailExpenses = txtDetailExpenses.Text, Participant = txtParticipant.Text, Costcenter = comboDepartment1.Values, CurrencyDescription = "VND - VIETNAM DONG", NoNight = radnumSoDem.Value, IDWorkingPlan = cls.cToDecimal(dropWorkingPlan.SelectedValue), VATPercent = cls.cToDouble(dropTaxcode.SelectedValue), WorkingPlanDetail = dropWorkingPlan.SelectedItem.Text, ChargeType = dropLoaiCP.SelectedValue, ChargeTypeDs = dropLoaiCP.SelectedItem.Text, Vendor = hdVendorSAP.Value, TrangThai = trangthai, LyDo = lydo, UserAppReject = user };
                    dbs.ClaimExpensesDetails.InsertOnSubmit(model);
                    cls.bThem(new string[] { "@VendorName", "@Address", "@TaxCode" }, new object[] { txtCompanyName.Text.Trim(), txtDiaChi.Text.Trim(), txtVAT.Text.Trim() }, "sp_insertVendorSales");
                }
                lbMess.Text = "Saved successfully";
            }
            catch { }
            dbs.Dispose();
        }
        protected void btAdd_Click(object sender, EventArgs e)
        {
            if (raddateFrom.IsEmpty)
            {
                MsgBox1.AddMessage("Please fill in 'From Date:'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (raddateTo.IsEmpty)
            {
                MsgBox1.AddMessage("Please fill in 'To Date:'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (raddateTo.SelectedDate.Value < raddateFrom.SelectedDate.Value)
            {
                MsgBox1.AddMessage("'To Date' value must be greater or equal 'From Date'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (radnumAmount.Value <= 0)
            {
                MsgBox1.AddMessage("Please fill in 'Amount(FC):'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (raddateInvoice.IsEmpty)
            {
                MsgBox1.AddMessage("Please fill in 'Invoice Date'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            // double nodays = TinhNgaySales();
            int nodays = TinhNgay();
            if (txtPurpose.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Please fill in 'Purpose'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (dropWorkingPlan.SelectedItem == null)
            {
                MsgBox1.AddMessage("Please select in 'Working plan'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
           // dropWorkingPlan.SelectedItem
            clsSys sys = new clsSys();
            List<sp_getClaimDetailSalesResult> tbl = (List<sp_getClaimDetailSalesResult>)Session["ClaimDetailSale"];
            sp_getClaimDetailSalesResult item = new sp_getClaimDetailSalesResult();
            item.ID = 0;
            int index = -1;
            if (Session["indexdit"] != null && cls.cToInt(Session["indexdit"]) >= 0)//dag o trang thai update
            {
                index=cls.cToInt(Session["indexdit"]);
                tbl.RemoveAt(index);
                btAdd.Text = "24.Add";
                Session["indexdit"] = null;
                item.ID = cls.cToDecimal(hdID.Value);
               // hdID.Value = "0";
            }
            //else
            //{
            //    item = tbl[cls.cToInt(Session["indexdit"])];
            //}
            
            
            //them Code_FK
           
            item.Code_FK = Session["NewDocNoClaimSales"].ToString();
            item.Date = raddateInvoice.SelectedDate.Value;
            item.No = txtNoInvoice.Text;
            item.Notation = txtNotationInvoice.Text;
            item.Charges_FK = ddlCategory.SelectedValue;
          //  item.Description = ddlCategory.SelectedItem.Text;
            item.CompanyName = txtCompanyName.Text;
            item.Province = txtDiaChi.Text;
            item.VATCode = dropTaxcode.SelectedItem.Text;
            item.VATPercent = cls.cToDouble(dropTaxcode.SelectedValue);
            item.VATAmount = radnuTaxAmount.Value; /*Math.Round(radnumAmount.Value / (1 + 100 / cls.cToDouble(dropTaxcode.SelectedValue)),0);*/ //TongTien/(1+100/phantram)radnuTaxAmount.Value = Math.Round(cls.cToDouble(radnumAmount.Value) / (1 + 100 / cls.cToDouble(dropTaxcode.SelectedValue)),0); 
            item.Amount = cls.cToDecimal(Math.Round(cls.cToDouble(radnumAmount.Value - radnuTaxAmount.Value),0));
                item.TotalVN = cls.cToDecimal(radnumAmount.Value);// item.Amount + cls.cToDecimal(item.VATAmount);
                item.IDWorkingPlan = cls.cToDecimal(dropWorkingPlan.SelectedValue);
                item.WorkingPlanDetail = dropWorkingPlan.SelectedItem.Text;
                item.FDate = raddateF1.SelectedDate.Value;
                item.TDate = raddateTo1.SelectedDate.Value;
            item.Purpose = txtPurpose.Text.Trim();
            item.TaxNumber = txtVAT.Text.Trim();
            item.Vendor = hdVendorSAP.Value;
            item.NoDays=cls.cToFloat(radnumSoNgay.Value);
            item.NoNight=cls.cToFloat(radnumSoDem.Value);
            //if (ddlCategory.SelectedValue.ToLower() == "ho")
            //{
            //    item.NoDays = nodays - 1;

            //}
            //else
            //{
            //    item.NoDays = nodays;
            //}
            //if (radioClaim.SelectedValue.ToLower() == "domestic")
            //{
            //    item.Currency = "VND";
            //    item.Rate = 1;
            //}
            //else
            //{
            item.Currency = "VND";
            item.CurrencyDescription = "VND - VIETNAM DONG";
            item.Rate = 1;
            //  }
          //  item.Amount = (decimal)radnumAmount.Value;
            //decimal tongtienvnd = 1 * cls.cToDecimal(radnumAmount.Value);
           // item.TotalVN = tongtienvnd;// (decimal)radnuTotalVND.Value;
            Session["AutoNumber"] = (int)Session["AutoNumber"] + 1;
            // imgUpload1.uploadimg(Session["NewDocNo"].ToString() + "-"+Session["AutoNumber"].ToString());
            //  item.PictureURL = imgUpload1.FileName;
            item.PictureURL = "";
            item.DetailExpenses = txtDetailExpenses.Text;
            item.Participant = txtParticipant.Text;
            item.GL = txtGL.Text;
            item.IO = txtIO.Text;
            item.Costcenter = comboDepartment1.Values;
            item.ChargeType = dropLoaiCP.SelectedValue;
            item.ChargeTypeDs = dropLoaiCP.SelectedItem.Text;
            item.TrangThai = 1;
            item.LyDo = "";
            item.UserAppReject = "";
            if (FileUpload1.HasFile)
            {
                try
                {
                    int vt1 = FileUpload1.FileName.LastIndexOf(".");
                    int vtcanlay = vt1;
                    int len = FileUpload1.FileName.Length;
                    string extention = FileUpload1.FileName.Substring(vtcanlay, len - vtcanlay);
                    string filename = "";
                    filename = Session["NewDocNoClaimSales"].ToString() + "-" + Session["AutoNumber"].ToString();
                    filename = filename + extention;
                    //HinhBia.SaveAs(Server.MapPath("../" + ConfigurationManager.AppSettings["hinhbia_sanpham"].ToString()).ToString() + ANHBIA);
                    string sFolderPath = Server.MapPath("Upload/EC/" + filename);
                    if (System.IO.File.Exists(sFolderPath) == true)
                        System.IO.File.Delete(sFolderPath);
                    //resize
                    //  HttpPostedFile pf = FileUpload1.PostedFile;
                    FileUpload1.SaveAs(sFolderPath);
                    item.FileAttach = filename;
                    
                }
                catch (Exception ex)
                {
                    //hdPathSave.Value = "";
                    item.FileAttach = "";
                    Response.Write(ex.Message);
                }
            }
            else
            {
                item.FileAttach = "";
            }
            if (index >= 0)
            {
                //dang sua
                tbl.Insert(index,item);
            }
            else
            {
                //dag them
                tbl.Add(item);
            }
        //    tbl.Add(item);
            var newList = tbl.OrderBy(m => m.Date).ToList();
              Session["ClaimDetailSale"] = newList;
              Session["ChuaLuuClaimSales"] = true;
           // Session["Company"] = "";
          //  Session["Province"] = "";
          //  Session["TaxCode"] = "";
          //  Session["TaxNumber"] = "";
          //  Session["VATAmount"] = 0;
            //if (radioClaim.SelectedValue.ToLower() == "domestic")
            FillTable();
          //  getWorkingPlan(dropTravelRequest.SelectedValue, raddateF1.SelectedDate.Value, raddateTo1.SelectedDate.Value, false);
            if (chkShowAll.Checked == false)
            {
                dropWorkingPlan.Items.RemoveAt(dropWorkingPlan.SelectedIndex);
            }
           
            //if (dropSaved.SelectedValue == "0")//tao moi
            //{
            //    string code = Session["NewDocNoClaimSales"].ToString();
            //    if (SaveParent(code) == true)
            //    {
            //        LoadClaim();
            //        dropSaved.SelectedValue = code;
            //        SaveOneDetail(dropSaved.SelectedValue, 1, "", "");
            //    }
            //}
            //else { SaveOneDetail(dropSaved.SelectedValue, 1, "", ""); }
            raddateInvoice.Focus();
        }
        private void getClaimDetail(string code)
        {
            DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            List<sp_getClaimDetailSalesResult> kq = dbs.sp_getClaimDetailSales(code,false,cls.cToString(Session["Username"])).OrderBy(o => o.ID).ToList();//sp_getClaimDetail LA STORE
            if (code == "0")
            {
                kq.RemoveAt(0);
            }
            Session["ClaimDetailSale"] = kq;
            Session["AutoNumber"] = kq.Count;
            dbs.Dispose();

        }
        protected void dropSaved_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Literal1.Text = "";
            lbMess.Text = "";
           // getClaimDetail(dropSaved.SelectedValue);
            if (dropSaved.SelectedValue == "0")
            {
               
                GetNewClaim();
                lbStatus.Visible = true;
                lbStatusTitle.Visible = true;
                FLoadAdvance(false,false);
                getClaimDetail("0");
                FillTable();
                btAdd.Text = "24.Add";
             
                hdStatus.Value = "0";
                setButton(int.Parse(hdStatus.Value));
                btDelete.Visible = false;
                RadGrid2.Visible = false;

                RadGrid2.DataSource = null;
                RadGrid2.DataBind();
                btSubmit.Visible = false;
                btDelete.Visible = false;
            }
            else
            {
               // FLoadAdvance(true);
                Session["ChuaLuuClaimSales"] = false;
                if (hdPrint.Value == "1")
                {
                    FLoadAdvance(true,true);
                    GetOldClaim(dropSaved.SelectedValue);
                }
                else
                {
                    FLoadAdvance(true,false);
                    GetOldClaim(dropSaved.SelectedValue);
                }
                
                getClaimDetail(dropSaved.SelectedValue);
                // radioClaim.Enabled = false;
                FillTable();
                RadGrid2.Visible = true;
                DataTable tblstatus = cls.GetDataTable("sp_getStatusDocno", new string[] { "@docno" }, new object[] { dropSaved.SelectedValue });
                RadGrid2.DataSource = tblstatus;
                RadGrid2.DataBind();
                setButton(int.Parse(hdStatus.Value));
            }
            
            // btApp.Visible = false;
        }
        protected void dropApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Literal1.Text = "";
            getClaimDetail(dropApp.SelectedValue);
            if (dropApp.SelectedValue == "0")
            {
                // GetNewClaim();
                btApp.Visible = false;
                btReject.Visible = false;
                // gridSum.Visible = false;
            }
            else
            {
                FLoadAdvance(true,true);
                btApp.Visible = true;
                btReject.Visible = true;
                txtAppNote.ReadOnly = false;
                txtAppNote.Visible = true;
                lbReason.Visible = true;
                btSave.Visible = false;
                btDelete.Visible = false;
                btSubmit.Visible = false;
                dropTravelRequest.Enabled = false;
                GetOldClaim(dropApp.SelectedValue);
               
                FillTable();
                //gridSum.Visible = true;
                //  LoadSum(dropApp.SelectedValue);
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            //if (usernamestatic.ToLower() != cls.cToString(Session["username"]).ToLower() && usernamestatic!="")
            //{
            //    MsgBox1.AddMessage("Bạn đã login bằng user khác nên không thể lưu chứng từ này " + usernamestatic.ToLower() + "=" + cls.cToString(Session["username"]).ToLower(), uc.ucMsgBox.enmMessageType.Error);
            //}
            //else
            //{
                if (RadGrid1.Items.Count <= 0)
                {
                    MsgBox1.AddMessage("Please fill in travel detail and click add button before saving", uc.ucMsgBox.enmMessageType.Error);
                    return;
                }
                if (dropSaved.SelectedValue == "0")//tao moi
                {
                    string code = Session["NewDocNoClaimSales"].ToString();
                    if (SaveParent(code) == true)
                    {
                        SaveDetail(code);
                        updateDocTot(code);
                        Session["ChuaLuuClaimSales"] = false;
                        getClaimDetail(dropSaved.SelectedValue);
                        FillTable();
                        btSubmit.Visible = true;
                    }
                    LoadClaim();
                    dropSaved.SelectedValue = code;
                    lbStatus.Visible = false;
                    lbStatusTitle.Visible = false;
                }
                else//update
                {
                    if (UpdateParent(dropSaved.SelectedValue) == true)
                    {
                        // UpdateDetail(dropSaved.SelectedValue);
                        UpdateDetail();
                        updateDocTot(dropSaved.SelectedValue);
                        Session["ChuaLuuClaimSales"] = false;
                        getClaimDetail(dropSaved.SelectedValue);
                        FillTable();
                    }
                }
           // }
        }
       
        //private void ChangeStatus(string code, int status, string note)
        //{
        //    using (var db = new DBTableDataContext())
        //    {
        //        var model = db.ClaimExpenses.SingleOrDefault(p => p.Code_PK == code);
        //        model.Status = status;
        //        model.NoteApprover = note;
        //        db.SubmitChanges();
        //    }
        //}
        
        private string FillTableemail()
        {
            string kq = "";

            List<sp_getClaimDetailSalesResult> tbl = (List<sp_getClaimDetailSalesResult>)Session["ClaimDetailPrintSales"];
            
            double tongtien = 0;
            int stt = 0;
            foreach (sp_getClaimDetailSalesResult item in tbl)
            {

                if (item.TotalVN != 0 && item.TrangThai==1)
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
            kq = kq + "<tr><td colspan=9 style='color: #000000; font-weight: bold; text-align:right;'>Đã tạm ứng VND/Advanced Amount:</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + cls.FormatNumber(radnumAdvncedAmount.Value) + "</td><td colspan=2></td></tr>";
            kq = kq + "<tr><td colspan=9 style='color: #000000; font-weight: bold; text-align:right;'>Chênh lệch VND/Pay back(+)/Reimbursemet(-):</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + cls.FormatNumber(tongtien - cls.cToDouble(radnumAdvncedAmount.Value)) + "</td><td colspan=2></td></tr>";
            return kq;
        }


        protected void btSubmit_Click(object sender, EventArgs e)
        {

            //if (usernamestatic.ToLower() != cls.cToString(Session["username"]).ToLower() && usernamestatic != "")
            //{
            //    MsgBox1.AddMessage("Bạn đã login bằng user khác nên không thể submit chứng từ này " + usernamestatic.ToLower() + "=" + cls.cToString(Session["username"]).ToLower(), uc.ucMsgBox.enmMessageType.Error);
            //}
            //else
            //{
                if (cls.cToBool(Session["ChuaLuuClaimSales"]) == true)
                {
                    MsgBox1.AddMessage("Before submit, Please save changes", uc.ucMsgBox.enmMessageType.Info);
                }
                else
                {
                    string stkq = cls.GetString0("StatusFinalSales", new string[] { "@travaldocno" }, new object[] { dropTravelRequest.SelectedValue });
                    if (cls.cToBool(stkq) == true)
                    {
                        try
                        {
                            string docno = Session["NewDocNoClaimSales"].ToString();
                            if (dropSaved.SelectedValue != "0")//tao moi
                            {
                                docno = dropSaved.SelectedValue;
                            }
                            DataTable chksubmit = cls.GetDataTable("sp_checkSumited", "@docno", docno);
                            if (cls.cToString(chksubmit.Rows[0]["KetQua"]).ToUpper() != "OK")
                            {
                                MsgBox1.AddMessage("Document already submited", uc.ucMsgBox.enmMessageType.Info);
                            }
                            else
                            {
                                //xoa  DOA truoc khi trinh ky lai
                                cls.bXoa(new string[] { "@Docno" }, new object[] { docno }, "sp_DeleteApprove");

                                DataTable tbl = cls.GetDataTable("sp_getTotalClaim", new string[] { "@Code" }, new object[] { docno });
                                decimal sotien = cls.cToDecimal(tbl.Rows[0]["Amount"]);
                                string username = cls.cToString(tbl.Rows[0]["Username"]);
                                string emailN1 = txtAppEmail.Text;
                                DataTable tblN1N2 = cls.GetDataTable("sp_getN1_N2", "@username", username);
                                string senior = cls.getSeniorManager(username);//BM
                                string director = cls.getDirector(username);
                                string vp = cls.getVP(username);
                                //  string coo = cls.getCOO(username);
                                string emailsalesadmin = cls.GetString("sp_getEmailSalesAdmin");
                                string emailsalesadminMT = cls.GetString("sp_getEmailSalesAdminMT");
                                string emailacc = cls.GetString("sp_getEmailAccountant");
                                string emailacc2 = cls.GetString("sp_getEmailAccountant2");
                                string emailMTManager = cls.GetString("sp_getEmailMTManager");
                                string emailFoodManager = cls.GetString("sp_getEmailFoodManager");
                                //sp_getEmailMTManager
                                string type = "ECSALES";
                                bool kqsmit = false;
                                //if (senior == "")
                                //{
                                //    senior = txtAppEmail.Text;
                                //}
                                Guid activationCode = Guid.NewGuid();
                                // cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, n1, 1, 0, activationCode, type }, "sp_insertApprove");
                                if (tblN1N2.Rows.Count > 0 && senior.ToLower() != emailMTManager.ToLower()) //truong hop MT thi chi can qua N1 roi den Admin....
                                {
                                    string userN1 = cls.cToString(tblN1N2.Rows[0]["UserN1"]);
                                    string userN2 = cls.cToString(tblN1N2.Rows[0]["UserN2"]);
                                    emailN1 = cls.cToString(tblN1N2.Rows[0]["EmailN1"]);
                                    string emailN2 = cls.cToString(tblN1N2.Rows[0]["EmailN2"]);
                                    string userN3 = cls.cToString(tblN1N2.Rows[0]["UserN3"]);
                                    string emailN3 = cls.cToString(tblN1N2.Rows[0]["EmailN3"]);
                                    if (emailN2.ToLower() == vp.ToLower())//Neu N2 la VP thi dung lai o N1
                                    {
                                        userN2 = userN1;
                                        emailN2 = emailN1;
                                    }
                                    if (userN1.ToLower() != userN2.ToLower())
                                    {
                                        kqsmit = cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, emailN1, 1, 0, activationCode, type }, "sp_insertApprove");
                                        if (emailN2.ToLower() != vp.ToLower())//neu N2 la VP thi chi can N1 approve
                                        {
                                            activationCode = Guid.NewGuid();
                                            cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, emailN2, 2, 0, activationCode, type }, "sp_insertApprove");
                                        }
                                        if (userN3 != "" && emailN3.ToLower() != vp.ToLower() && emailN3.ToLower() != emailMTManager.ToLower() && emailN3.ToLower() != emailFoodManager.ToLower())
                                        {
                                            activationCode = Guid.NewGuid();
                                            cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, emailN3, 3, 0, activationCode, type }, "sp_insertApprove");
                                        }

                                    }
                                    else
                                    {
                                        kqsmit = cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, emailN1, 1, 0, activationCode, type }, "sp_insertApprove");
                                    }
                                }
                                else
                                {
                                    kqsmit = cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, emailN1, 1, 0, activationCode, type }, "sp_insertApprove");
                                }
                                if (senior.ToLower() == emailMTManager.ToLower())//MT ALL
                                {
                                    activationCode = Guid.NewGuid();
                                    cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, emailsalesadminMT, 4, 0, activationCode, type }, "sp_insertApprove");
                                    activationCode = Guid.NewGuid();
                                    cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, emailMTManager, 5, 0, activationCode, type }, "sp_insertApprove");
                                    activationCode = Guid.NewGuid();
                                    cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, vp, 6, 0, activationCode, type }, "sp_insertApprove");
                                    activationCode = Guid.NewGuid();
                                    cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, emailacc, 7, 0, activationCode, type }, "sp_insertApprove");
                                    activationCode = Guid.NewGuid();
                                    cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, emailacc2, 8, 0, activationCode, type }, "sp_insertApprove");

                                }
                                else
                                {
                                    if (senior.ToLower() == emailFoodManager.ToLower())//GT FOOD
                                    {
                                        activationCode = Guid.NewGuid();
                                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, emailFoodManager, 4, 0, activationCode, type }, "sp_insertApprove");
                                        activationCode = Guid.NewGuid();
                                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, emailsalesadmin, 5, 0, activationCode, type }, "sp_insertApprove");
                                        activationCode = Guid.NewGuid();
                                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, vp, 6, 0, activationCode, type }, "sp_insertApprove");
                                        activationCode = Guid.NewGuid();
                                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, emailacc, 7, 0, activationCode, type }, "sp_insertApprove");
                                        activationCode = Guid.NewGuid();
                                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, emailacc2, 8, 0, activationCode, type }, "sp_insertApprove");
                                    }
                                    else //GT HPC
                                    {
                                        activationCode = Guid.NewGuid();
                                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, senior, 4, 0, activationCode, type }, "sp_insertApprove");
                                        activationCode = Guid.NewGuid();
                                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, emailsalesadmin, 5, 0, activationCode, type }, "sp_insertApprove");
                                        activationCode = Guid.NewGuid();
                                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, vp, 6, 0, activationCode, type }, "sp_insertApprove");
                                        activationCode = Guid.NewGuid();
                                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, emailacc, 7, 0, activationCode, type }, "sp_insertApprove");
                                        activationCode = Guid.NewGuid();
                                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, emailacc2, 8, 0, activationCode, type }, "sp_insertApprove");
                                    }

                                }

                                //  sendemail:
                                if (kqsmit == true)
                                {
                                    //  SenEmailSubmit(docno, emailN1, "");

                                    string activationCode1 = cls.GetString("sp_getGuiCode", new string[] { "@docno", "@approval" }, new object[] { docno, emailN1 });
                                    String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                                    String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                                    if (cls.SenEmailSubmitClaimWorkingPlan(docno, emailN1, "", activationCode1, txtName.Text, comboDepartment1.Text, txtPurpose.Text, raddateFrom.SelectedDate.Value.ToString("dd-MMM-yyyy"), raddateTo.SelectedDate.Value.ToString("dd-MMM-yyyy"), strUrl) == true)
                                    {
                                        btSave.Visible = false;
                                        btDelete.Visible = false;
                                        btSubmit.Visible = false;
                                        lbStatus.Text = "Submitted";
                                        lbMess.Text = "Submitted successfully!";
                                        MsgBox1.AddMessage("Submitted successfully!", uc.ucMsgBox.enmMessageType.Success);
                                    }
                                    else
                                    {
                                        cls.bXoa(new string[] { "@Docno", "@username" }, new object[] { docno, emailN1 }, "sp_DeleteApproveByEmail");
                                        lbStatus.Text = "Saved";
                                        lbMess.Text = "Failed to submit";
                                        MsgBox1.AddMessage("Failed to send, Please try again!", uc.ucMsgBox.enmMessageType.Error);
                                    }
                                    btSubmit.Visible = false;
                                    btDelete.Visible = false;
                                }

                            }
                        }
                        catch { }
                    }
                    else
                    {
                        MsgBox1.AddMessage("Working plan chưa đươc duyệt", uc.ucMsgBox.enmMessageType.Error);
                    }

              //  }
            }
        }

        protected void btApp_Click(object sender, EventArgs e)
        {
            clsSys sys = new clsSys();
            string docno = dropApp.SelectedValue;
            bool kq;
            kq = cls.bCapNhat(new string[] { "@docno", "@username", "@status", "@DateApp", "@Note" }, new object[] { docno, Session["username"], 1, DateTime.Now, txtAppNote.Text }, "sp_updateAppbyName");
            //kiem tra xem co can chuyen chung tu len cap cao hon
            if (kq == true)
            {
                LoadClaimApp(cls.cToString(Session["username"]));
                btAdd.Visible = false;
                btReject.Visible = false;
                DataTable tbl = cls.GetDataTable("sp_getNextLevel", new string[] { "@Docno", "@username" }, new object[] { docno, Session["username"] });
                if (tbl.Rows.Count > 0)
                {
                    //co chuyen len tren
                    //lay thong tin nguoi tren
                    //clsSys sys = new clsSys();
                    string EmailCreate = cls.cToString(tbl.Rows[0]["EmailCreate"]);
                    string emailnextlevel = cls.cToString(tbl.Rows[0]["Approval"]);
                 //   SenEmailSubmit(docno, emailnextlevel, cls.cToString(Session["username"]));
                    string activationCode1 = cls.GetString("sp_getGuiCode", new string[] { "@docno", "@approval" }, new object[] { docno, emailnextlevel });
                    String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                    String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                    if (cls.SenEmailSubmitClaimWorkingPlan(docno, emailnextlevel, cls.cToString(Session["username"]), activationCode1, txtName.Text, comboDepartment1.Text, txtPurpose.Text, raddateFrom.SelectedDate.Value.ToString("dd-MMM-yyyy"), raddateTo.SelectedDate.Value.ToString("dd-MMM-yyyy"), strUrl) == true)
                    {
                        btSave.Visible = false;
                        btDelete.Visible = false;
                        btSubmit.Visible = false;
                        lbStatus.Text = "Submitted";
                        lbMess.Text = "Submitted successfully!";
                        MsgBox1.AddMessage("Submitted successfully!", uc.ucMsgBox.enmMessageType.Success);
                    }
                    else
                    {
                        cls.bXoa(new string[] { "@Docno", "@username" }, new object[] { docno, emailnextlevel }, "sp_DeleteApproveByEmail");
                        lbStatus.Text = "Saved";
                        lbMess.Text = "Failed to submit";
                        MsgBox1.AddMessage("Failed to send, Please try again!", uc.ucMsgBox.enmMessageType.Error);
                    }
                    sys.SendMailASP(EmailCreate, "Claim Expenses has been sent to next level", "Claim Expenses " + docno + " has been approved by N+1 and submit to Next level (" + emailnextlevel + ")");
                    MsgBox1.AddMessage("Approved. Notification of the approval (" + docno + ") will be submitted to the next level and copy to the requestor", uc.ucMsgBox.enmMessageType.Success);
                }
                else
                {

                    // string emailhanhchanh = cls.GetString("sp_getEmailHanhChanh", new string[] { "@Code" }, new object[] { docno });
                    string cc = txtAppEmail.Text;
                    //if (emailhanhchanh != "")
                    //{
                    //    cc = cc + ";" + emailhanhchanh;
                    //}
                    //de la nguoi duyet cuoi cung
                    kq = sys.SendMailASP(txtMyEmail.Text, cc, "Expenses claim request has been Approved", "Expenses claim request  " + docno + " has been approved by " + cls.cToString(Session["username"]));
                    MsgBox1.AddMessage("Approved! Notification of the approval (" + docno + ") will be sent to the requestor", uc.ucMsgBox.enmMessageType.Success);
                }

            }

            ////ChangeStatus(docno, 1, txtAppNote.Text);
            //using (var db = new DBTableDataContext())
            //{
            //    var model = db.ClaimExpenses.SingleOrDefault(p => p.Code_PK == docno);
            //    model.ApprovedCode1 = null;
            //    model.Status = 1;
            //    model.DateApp1 = DateTime.Now;
            //    model.NoteApprover = txtAppNote.Text;
            //    db.SubmitChanges();
            //}
            //LoadClaimApp(cls.cToString(Session["username"]));
            ////send email
            //clsSys sys = new clsSys();
            //sys.SenEmailApproved(docno, txtMyEmail.Text, txtAppEmail.Text);
            //btAdd.Visible = false;
            //btReject.Visible = false;
            //MsgBox1.AddMessage("Approved success", uc.ucMsgBox.enmMessageType.Success);
        }
        protected void btReject_Click(object sender, EventArgs e)
        {
            
            if (txtAppNote.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Please fill in reject 'Reason'", uc.ucMsgBox.enmMessageType.Error);

                txtAppNote.Focus();
                //string docno = !string.IsNullOrEmpty(Request.QueryString["docno"]) ? Request.QueryString["docno"] : "";
                //if (docno != "")//goi tu form duyet collect
                //{
                //    dropApp.SelectedValue = docno;
                //    dropApp.Enabled = false;
                if (cls.cToString(Session["username"]).Length < 30)//mo form reject tu email nen sau khi reject xong thi clear session
                {
                    dropApp_SelectedIndexChanged(sender, e);
                }
                //}
            }
            else
            {
                string docno = dropApp.SelectedValue;
                // ChangeStatus(docno, 3, txtAppNote.Text);
                //send email
                bool kq = cls.bCapNhat(new string[] { "@docno", "@username", "@Note" }, new object[] { docno, Session["username"], txtAppNote.Text }, "sp_updateAppbyNameRejected");
                if (kq == true)
                {
                    if (cls.cToString(Session["username"]).Length > 30)//mo form reject tu email nen sau khi reject xong thi clear session
                    {
                        Session["username"] = Session["usernametemp"];
                        //  Session["usernametemp"] 
                    }
                    LoadClaimApp(cls.cToString(Session["username"]));
                    clsSys sys = new clsSys();
                    //sys.SenEmailReject(docno, txtAppNote.Text, txtMyEmail.Text, txtAppEmail.Text);
                    sys.SendMailASP(txtMyEmail.Text, /*txtAppEmail.Text, */"Expenses claim request has been Rejected", "Expenses claim request " + docno + " has been rejected with reason " + txtAppNote.Text);
                    txtAppNote.Focus();
                    btAdd.Visible = false;
                    btReject.Visible = false;
                    
                    MsgBox1.AddMessage("Rejected. Notification of the reject (" + docno + ") will be emailed to the expenses claim initiator", uc.ucMsgBox.enmMessageType.Success);
                }


               
            }
        }
        /// <summary>
        /// LoadAll=true: load tat ca tam ung cua user login; false: chi load nhung tam ung chua thanh toan cua user login
        /// LoadAllUser=true: ko quan tam den LoadAll ma no sẽ load tat ca tam ung cua tat ca user len, False thi xet theo LoadAll
        /// </summary>
        /// <param name="LoadAll"></param>
        /// <param name="LoadAllUser"></param>
        private void FLoadAdvance(bool LoadAll,bool LoadAllUser)
        {
            try
            {
                Cclass cls = new Cclass();
                DataTable tbl = cls.GetDataTable("sp_GetWorkingNo", new string[] { "@username", "@LoadAll", "@LoadAllUser" }, new object[] { Session["username"], LoadAll, LoadAllUser });
                dropTravelRequest.DataSource = tbl;
                dropTravelRequest.DataBind();
            }
            catch { }
        }
        protected void radioClaim_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadClaim();
        
        }

        protected void btPrint_Click(object sender, EventArgs e)
        {
            //PrintHelper.PrintWebControl(pnlPrintClaimExpenses);
            //Printed();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            Session["docno"] = dropSaved.SelectedValue;
            sb.Append("window.open('PrintClaimSales.aspx','PrintMe', 'height=900px,width=1024px,scrollbars=1');");
            //ScriptManager.RegisterClientScriptBlock(this.RadAjaxManager1, this.RadAjaxManager1.GetType(), "NewClientScript", sb.ToString(), true);
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "NewClientScript", sb.ToString(), true);
        }
        protected void btReview_Click(object sender, EventArgs e)
        {
            //PrintHelper.PrintWebControl(pnlPrintClaimExpenses);
            //Printed();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //Session["docno"] = dropSaved.SelectedValue;
            sb.Append("window.open('PrintClaimSales.aspx?type=3&docno=" + dropSaved.SelectedValue + "','PrintMe', 'height=900px,width=1024px,scrollbars=1');");
            //ScriptManager.RegisterClientScriptBlock(this.RadAjaxManager1, this.RadAjaxManager1.GetType(), "NewClientScript", sb.ToString(), true);
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "NewClientScript", sb.ToString(), true);
        }
        protected void radnumAmount_TextChanged(object sender, EventArgs e)
        {
           // clsSys sys = new clsSys();
            if (cls.cToDouble(dropTaxcode.SelectedValue) == 0)
            {
                 radnuTaxAmount.Value = 0;
            }
            else
            {
                radnuTaxAmount.Value = Math.Round(cls.cToDouble(radnumAmount.Value) / (1 + 100 / cls.cToDouble(dropTaxcode.SelectedValue)),0); 
            }
           
           // radnuTotalVND.Focus();
        }

        protected void btExpand_Click(object sender, EventArgs e)
        {
            dvSum.Visible = !dvSum.Visible;
            if (dvSum.Visible)
            {
                btExpand.Text = "-";

                btExpand.ToolTip = "Collapse";
            }
            else
            {
                btExpand.Text = "+";
                btExpand.ToolTip = "Expand";
            }
        }
        protected void btExpand1_Click(object sender, EventArgs e)
        {
            dvParent.Visible = !dvParent.Visible;
            if (dvParent.Visible)
            {
                btExpand1.Text = "-";
                btExpand1.ToolTip = "Collapse";
            }
            else
            {
                btExpand1.Text = "+";
                btExpand1.ToolTip = "Expand";
            }
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboDepartment1.Values != "")
            {
                DataTable tbl = cls.GetDataTable("sp_getGLIO", new string[] { "@Company_FK", "@CostCenter", "@charges_FK" }, new object[] { 1/*comboDepartment1.getCompanycode*/, comboDepartment1.Values, ddlCategory.SelectedValue });
                if (tbl.Rows.Count > 0)
                {
                    txtGL.Text = tbl.Rows[0]["GL"].ToString();
                    txtIO.Text = tbl.Rows[0]["IO"].ToString();
                }
            }
            else
            {
                MsgBox1.AddMessage("Please fill in Department", uc.ucMsgBox.enmMessageType.Error);
            }
        }
       
        protected void RadGrid1_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            int index = e.Item.ItemIndex;
            List<sp_getClaimDetailSalesResult> tbl = (List<sp_getClaimDetailSalesResult>)Session["ClaimDetailSale"];
            string filename = tbl[index].FileAttach;
            if (filename != "")
            {
                string sFolderPath = Server.MapPath("Upload/EC/" + filename);
                if (System.IO.File.Exists(sFolderPath) == true)
                    System.IO.File.Delete(sFolderPath);
            }
            if (tbl[index].ID > 0)//xoa dong da duoc luu vao data base truoc do
            {
                using (var db = new DBTableDataContext())
                {
                    db.ClaimExpensesDetails.DeleteAllOnSubmit(db.ClaimExpensesDetails.Where(o => o.ID == tbl[index].ID));
                    db.SubmitChanges();
                }
            }
            tbl.RemoveAt(index);
            Session["ClaimDetailSale"] = tbl;
            FillTable();
        }

        protected void dropTravelRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            List<sp_GetUserInfo_TravelExistsResult> kq = dbs.sp_GetUserInfo_TravelExists(dropTravelRequest.SelectedValue).ToList();//sp_getClaimDetail LA STORE
            if (kq.Count>0)
            {
                comboDepartment1.Values = kq[0].Department;
                hfDepartment.Value = kq[0].Department;
                txtName.Text = kq[0].Fullname;
                txtPosition.Text = kq[0].Position;
                txtAppName.Text = kq[0].FullNameRec;
                txtAppEmail.Text = kq[0].EmailRec;
                txtMyEmail.Text = kq[0].Email;
                dropMarket.SelectedValue = kq[0].Market;
              //  raddateNow.SelectedDate = kq[0].DateRec;
                //  radioClaim.SelectedValue = kq[0].Type;

                txtPurpose.Text = kq[0].Purpose;
                raddateFrom.SelectedDate = kq[0].FDate;
                raddateTo.SelectedDate = kq[0].TDate;
            //    radnumAdvncedAmount.Value = cls.cToDouble(kq[0].AdvanceAmount);
                //  radnumDays.Value =(double) kq[0].NoDays;
               // lbStatus.Text = kq[0].StatusText;
              //  hdStatus.Value = kq[0].Status.ToString();
             //   txtAppNote.Visible = true;
             //   txtAppNote.Text = kq[0].NoteApprover;
             //   txtTelContact.Text = kq[0].TelContact;
               // txtNoiDen.Text = kq[0].Destination;
              //  txtLoTrinh.Text = kq[0].Itenerary;
              //  chkOto.Checked = cls.cToBool(kq[0].ByCar);
              //  chkTauHoa.Checked = cls.cToBool(kq[0].ByTrain);
             //   chkMayBay.Checked = cls.cToBool(kq[0].ByPlane);
                // chkXeSanBay.Checked = cls.cToBool(kq[0].CarAriPort);
                // chkXeCongTac.Checked = cls.cToBool(kq[0].CarTravel);
             //   chkDatPhong.Checked = cls.cToBool(kq[0].BookHotel);
            //    chkVeTauMayBay.Checked = cls.cToBool(kq[0].BookTicket);
            //    chkOther.Checked = cls.cToBool(kq[0].Other);
            //    txtOther.Text = kq[0].DetailOther;
               // txtDPNo.Text = kq[0].DPNo;
            }
        }

        protected void btVRequetsTravel_Click(object sender, EventArgs e)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
           // Session["docno"] = dropTravelRequest.SelectedValue;
            sb.Append("window.open('PrintTravelRequestSales.aspx?type=2&docno=" + dropTravelRequest.SelectedValue + "','mywindowtitle', 'height=600px,width=850px,scrollbars=1');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "NewClientScript", sb.ToString(), true);

           
        }
        protected void GetMySelection(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //Response.Write("My selection is: " + e.SelectedItem.Text);
           // MsgBox1.AddMessage("tttttttttttttttttttttt", uc.ucMsgBox.enmMessageType.Error);
            LoadCategory(comboDepartment1.Values);
            ddlCategory_SelectedIndexChanged(sender, e);
        }

        protected void RadGrid1_EditCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
          //  int index = e.Item.ItemIndex;
            GridEditableItem editedItem = (GridEditableItem)e.Item;
            int index = editedItem.ItemIndex;
          //  List<sp_getClaimDetailResult> tbl = (List<sp_getClaimDetailResult>)Session["ClaimDetail"];
          //  string filename = tbl[index].FileAttach;
            //try
            //{
            if (cls.cToString(editedItem["Costcenter"].Text) != "")
            {
                comboDepartment1.Values = editedItem["Costcenter"].Text;
                // LoadCategory(comboDepartment1.Values);

            }
            else
            {
                comboDepartment1.Values=cls.GetString("getCostcentFromCate", new string[] { "@Charges_PK" }, new object[] { editedItem["Charges_FK"].Text });
            }
            //    }
            //catch { }
            LoadCategory(comboDepartment1.Values);
                ddlCategory.SelectedValue = editedItem["Charges_FK"].Text;//tbl[index].Charges_FK;
            raddateInvoice.SelectedDate = cls.cToDateTime(editedItem["Date"].Text);// tbl[index].Date;
            txtNoInvoice.Text = editedItem["No"].Text;// tbl[index].No;
            txtNotationInvoice.Text = editedItem["Notation"].Text;
            txtVAT.Text = editedItem["TaxNumber"].Text;
            txtCompanyName.Text=editedItem["CompanyName"].Text;
            txtDiaChi.Text=editedItem["Province"].Text;
           // comboCurrence1.CurrText = editedItem["Currency"].Text;// tbl[index].Currency;
         //   comboCurrence1.CurrTextFull = editedItem["CurrencyDescription"].Text;// tbl[index].Currency;
           // comboCurrence1.CurrValues = editedItem["Currency"].Text;
          //  comboCurrence1.RateText = editedItem["Rate"].Text;//cls.cToString(tbl[index].Rate);
            radnumAmount.Value = cls.cToDouble(editedItem["TotalVN"].Text);// cls.cToDouble(tbl[index].Amount);
           // radnumAmount.Value = cls.cToDouble(editedItem["TotalVN"].Text);//cls.cToDouble(tbl[index].TotalVN);
            dropTaxcode.SelectedValue = editedItem["VATPercent"].Text;
            radnuTaxAmount.Value=cls.cToDouble(editedItem["VATAmount"].Text);
            raddateF1.SelectedDate = cls.cToDateTime(editedItem["FDate"].Text); 
            raddateTo1.SelectedDate = cls.cToDateTime(editedItem["TDate"].Text);
            getWorkingPlan(dropTravelRequest.SelectedValue, raddateF1.SelectedDate.Value, raddateTo1.SelectedDate.Value,true);
            dropWorkingPlan.SelectedValue=editedItem["IDWorkingPlan"].Text;
            radnumSoNgay.Value = cls.cToFloat(editedItem["NoDays"].Text);
            radnumSoDem.Value = cls.cToFloat(editedItem["NoNight"].Text);
            txtGL.Text = editedItem["GL"].Text;//tbl[index].GL;
            txtIO.Text = editedItem["IO"].Text;
            txtDetailExpenses.Text = editedItem["DetailExpenses"].Text;
            txtParticipant.Text = editedItem["Participant"].Text;
            dropLoaiCP.SelectedValue = editedItem["ChargeType"].Text;//
            hdID.Value = editedItem["ID"].Text;
            btAdd.Text = "24.Update";
            //if (filename != "")
            //{
            //    string sFolderPath = Server.MapPath("Upload/EC/" + filename);
            //    if (System.IO.File.Exists(sFolderPath) == true)
            //        System.IO.File.Delete(sFolderPath);
            //}
           // tbl.RemoveAt(index);
            Session["indexdit"] = index;
           // Session["ClaimDetail"] = tbl;
            RadGrid1.EditIndexes.Clear();
           FillTable();
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            //if (usernamestatic.ToLower() != cls.cToString(Session["username"]).ToLower() && usernamestatic != "")
            //{
            //    MsgBox1.AddMessage("Bạn đã login bằng user khác nên không thể xóa chứng từ này " + usernamestatic.ToLower() + "=" + cls.cToString(Session["username"]).ToLower(), uc.ucMsgBox.enmMessageType.Error);
            //}
            //else
            //{
                if (cls.bXoa(new string[] { "@docno" }, new object[] { dropSaved.SelectedValue }, "sp_deleteExpenses") == true)
                {
                    LoadClaim();
                    MsgBox1.AddMessage("Deleted successfully", uc.ucMsgBox.enmMessageType.Success);

                }
                else
                {
                    MsgBox1.AddMessage("Deleted fail", uc.ucMsgBox.enmMessageType.Error);
                }
            //}
        }
        private void LoadLoaiChiPhi(string costcenter)
        {
            DataTable tbl = cls.GetDataTable("sp_LoadChargesTravelSales", "@costcenter", costcenter);
            dropLoaiCP.DataSource = tbl;
            dropLoaiCP.DataBind();
        }
        protected void txtVAT_TextChanged(object sender, EventArgs e)
        {
          DataTable tblvendor=  cls.GetDataTable("sp_getVendorByTax", "@MST", txtVAT.Text.Trim());
          if (tblvendor.Rows.Count > 0)
          {
              txtCompanyName.Text = cls.cToString(tblvendor.Rows[0]["VendorName"]);
              txtDiaChi.Text = cls.cToString(tblvendor.Rows[0]["Address"]);
          }
        }

        protected void dropTaxcode_SelectedIndexChanged(object sender, EventArgs e)
        {
           // radnuTaxAmount.Value = radnumAmount.Value * cls.cToDouble(dropTaxcode.SelectedValue) / 100;
          //  clsSys sys = new clsSys();
            if (cls.cToDouble(dropTaxcode.SelectedValue) == 0)
            {
                radnuTaxAmount.Value = 0;
            }
            else
            {
                radnuTaxAmount.Value = Math.Round(cls.cToDouble(radnumAmount.Value) / (1 + 100 / cls.cToDouble(dropTaxcode.SelectedValue)), 0); //radnumAmount.Value * sys.cToDuoble(dropTaxcode.SelectedValue) / 100;
            }
        }
        private void getWorkingPlan(string CodeFK,DateTime From,DateTime To,bool getAll)
        {
            if (From != null && To != null && CodeFK!="")
            {
                DataTable tblwk = cls.GetDataTable("sp_getWorkingPlan", new string[] { "@Code_FK", "@FromDate", "@ToDate", "@ALL" }, new object[] { CodeFK, From, To, getAll });
                dropWorkingPlan.DataSource = tblwk;
                dropWorkingPlan.DataBind();
            }
        }
        protected void raddateF1_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            raddateTo1.SelectedDate = raddateF1.SelectedDate;
            TinhNgay(raddateF1.SelectedDate.Value, raddateTo1.SelectedDate.Value);
            getWorkingPlan(dropTravelRequest.SelectedValue, raddateF1.SelectedDate.Value, raddateTo1.SelectedDate.Value, chkShowAll.Checked);
        }

        protected void raddateTo1_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            TinhNgay(raddateF1.SelectedDate.Value, raddateTo1.SelectedDate.Value);
            getWorkingPlan(dropTravelRequest.SelectedValue, raddateF1.SelectedDate.Value, raddateTo1.SelectedDate.Value,chkShowAll.Checked);
        }

        protected void radnumSoNgay_TextChanged(object sender, EventArgs e)
        {
            if (radnumSoDem.Value > 0)
            {
                radnumSoDem.Value = radnumSoNgay.Value - 1;
            }
        }

        protected void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            TinhNgay(raddateF1.SelectedDate.Value, raddateTo1.SelectedDate.Value);
            getWorkingPlan(dropTravelRequest.SelectedValue, raddateF1.SelectedDate.Value, raddateTo1.SelectedDate.Value, chkShowAll.Checked);
        }
        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            // RadGrid1.Rebind();
            GridEditableItem editedItem;
            decimal ID = 0;
            TextBox txtlydo;
            switch (e.CommandName)
            {
                case "Rejected":
                    editedItem = (GridEditableItem)e.Item;
                    ID = cls.cToDecimal(editedItem["ID"].Text);
                    //if (cls.cToString(editedItem["Attachedfile"].Text)=="")//chi nhưng thang chưa submit hoac bi rejected thi moi duoc trinh ky
                    //{
                    txtlydo = editedItem["ActionColumn"].FindControl("txtRejectitem") as TextBox;
                    if (txtlydo.Text.Trim() == "")
                    {
                        MsgBox1.AddMessage("Phải nhập lý do reject", uc.ucMsgBox.enmMessageType.Error);
                        txtlydo.Focus();
                    }
                    else
                    {
                        if (cls.GetString("sp_updateTrangThaiItemDetail", new string[] { "@ID", "@TrangThai", "@LyDo", "@User", "@email" }, new object[] { ID, 2, txtlydo.Text.Trim(), Session["Username"], Session["Email"] }).ToUpper() == "OK")
                        {
                            Button btReject = editedItem["ActionColumn"].FindControl("btRejectdetail") as Button;
                            Button btAppDetail = editedItem["ActionColumn"].FindControl("btAppDetail") as Button;
                            Button btRejectPermanently = editedItem["ActionColumn"].FindControl("btRejectPermanently") as Button;
                            btReject.Visible = false;
                            btAppDetail.Visible = true;
                            btRejectPermanently.Visible = true;
                            MsgBox1.AddMessage("Đã rejected khoản mục chi tiết thành công", uc.ucMsgBox.enmMessageType.Success);
                        }
                        else
                        {
                            MsgBox1.AddMessage("Đã reject không thành công, Không thể cập nhật khi chứng từ đã được Approved hoặc Rejected", uc.ucMsgBox.enmMessageType.Error);
                        }
                   
                    }
                  

                    break;
                case "RejectPermanently":
                    editedItem = (GridEditableItem)e.Item;
                    ID = cls.cToDecimal(editedItem["ID"].Text);
                    //if (cls.cToString(editedItem["Attachedfile"].Text)=="")//chi nhưng thang chưa submit hoac bi rejected thi moi duoc trinh ky
                    //{
                    txtlydo = editedItem["ActionColumn"].FindControl("txtRejectitem") as TextBox;
                    if (txtlydo.Text.Trim() == "")
                    {
                        MsgBox1.AddMessage("Phải nhập lý do reject", uc.ucMsgBox.enmMessageType.Error);
                        txtlydo.Focus();
                    }
                    else
                    {
                        if (cls.GetString("sp_updateTrangThaiItemDetail", new string[] { "@ID", "@TrangThai", "@LyDo", "@User", "@email" }, new object[] { ID, 3, txtlydo.Text.Trim(), Session["Username"], Session["Email"] }).ToUpper() == "OK")
                        {
                            Button btReject = editedItem["ActionColumn"].FindControl("btRejectdetail") as Button;
                            Button btAppDetail = editedItem["ActionColumn"].FindControl("btAppDetail") as Button;
                            Button btRejectPermanently = editedItem["ActionColumn"].FindControl("btRejectPermanently") as Button;
                            btReject.Visible = true;
                            btAppDetail.Visible = true;
                            btRejectPermanently.Visible = false;
                            MsgBox1.AddMessage("Đã rejected vĩnh viễn khoản mục chi tiết thành công", uc.ucMsgBox.enmMessageType.Success);
                        }
                        else
                        {
                            MsgBox1.AddMessage("Đã reject vĩnh viễn không thành công, Không thể cập nhật khi chứng từ đã được Approved hoặc Rejected", uc.ucMsgBox.enmMessageType.Error);
                        }

                    }


                    break;
                case "Approve":
                    editedItem = (GridEditableItem)e.Item;
                    ID = cls.cToDecimal(editedItem["ID"].Text);
                    //if (cls.cToString(editedItem["Attachedfile"].Text)=="")//chi nhưng thang chưa submit hoac bi rejected thi moi duoc trinh ky
                    //{

                    if (cls.GetString("sp_updateTrangThaiItemDetail",new string[] { "@ID", "@TrangThai", "@LyDo", "@User", "@email" }, new object[] { ID, 1, "", Session["Username"], Session["Email"] }).ToUpper() == "OK")
                        {
                            Button btReject = editedItem["ActionColumn"].FindControl("btRejectdetail") as Button;
                            Button btAppDetail = editedItem["ActionColumn"].FindControl("btAppDetail") as Button;
                            Button btRejectPermanently = editedItem["ActionColumn"].FindControl("btRejectPermanently") as Button;
                            txtlydo = editedItem["ActionColumn"].FindControl("txtRejectitem") as TextBox;
                            btReject.Visible = true;
                            btAppDetail.Visible = false;
                            btRejectPermanently.Visible = true;
                            txtlydo.Visible = true;
                            MsgBox1.AddMessage("Đã approved khoản mục chi tiết thành công", uc.ucMsgBox.enmMessageType.Success);
                        }
                        else
                        {
                            MsgBox1.AddMessage("Approved không thành công, Không thể cập nhật khi chứng từ đã được Approved hoặc Rejected", uc.ucMsgBox.enmMessageType.Error);
                        }

                   


                    break;
               
                //case Telerik.Web.UI.RadGrid.FilterCommandName:
                //    ReLoadCV();
                //    break;
                //case Telerik.Web.UI.RadGrid.RebindGridCommandName:
                //    ReLoadCV();
                    //break;
            }
        }

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<sp_getClaimDetailSalesResult> tbl = (List<sp_getClaimDetailSalesResult>)Session["ClaimDetailSale"];
            RadGrid1.DataSource = tbl;
        }
    }
}