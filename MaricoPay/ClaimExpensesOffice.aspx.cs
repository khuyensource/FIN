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
    public partial class ClaimExpensesOffice : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //xet xem co phai truy cap tu link email
                DataTable tbl = new DataTable();
                string activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
                string RejectedCode = !string.IsNullOrEmpty(Request.QueryString["rejectedcode"]) ? Request.QueryString["rejectedcode"] : Guid.Empty.ToString();
                if (activationCode.ToLower() != Guid.Empty.ToString().ToLower())
                {
                    btDelete.Visible = false;
                    dvSum.Visible = false;
                        btExpand.Text = "+";
                        btExpand.ToolTip = "Expand";
                   
                    // clsSys sys = new clsSys();
                    //  DBTableDataContext db = new DBTableDataContext();
                    //  string AppLevel = !string.IsNullOrEmpty(Request.QueryString["AppLevel"]) ? Request.QueryString["AppLevel"] : "";
                    lbTitle.Text = "Duyệt Đề Nghị Thanh Toán<br /> Approve Expenses Claim";
                    string code = !string.IsNullOrEmpty(Request.QueryString["code"]) ? Request.QueryString["code"] : "";
                    LoadCategory(comboDepartment1.Values);
                    LoadMarKet();
                    FLoadAdvance(true,true);
                  //  DataTable tbl = cls.GetDataTable("sp_getClaimExpensesByGuiCode", new string[] { "@code", "@appcode" }, new object[] { code, RejectedCode });
                    tbl = cls.GetDataTable("sp_getByGuiCode", new string[] { "@code", "@appcode" }, new object[] { code, activationCode });
                    if (tbl.Rows.Count > 0)
                    {
                        string docno = cls.cToString0(tbl.Rows[0]["Code_PK"]);
                        
                        string AppEmail = cls.get_UsernameFromEmail(cls.cToString(tbl.Rows[0]["AppEmail"])).ToLower();
                        LoadClaimApp(AppEmail);
                       // bool kq = cls.bCapNhat(new string[] { "@docno", "@Appcode", "@status", "@DateApp", "@Note" }, new object[] { docno, activationCode, 1, DateTime.Now, "Approved via Email" }, "sp_updateApp");
                        string kq = cls.GetString0("sp_updateApp",new string[] { "@docno", "@Appcode", "@status", "@DateApp", "@Note" }, new object[] { docno, activationCode, 1, DateTime.Now, "Approved via Email" });
                        dropApp.Enabled = false;
                        dropApp.SelectedValue = docno;
                        btApp.Visible = true;
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
                        GetOldClaim(docno);
                        getClaimDetail(docno);
                        FillTable();
                      

                        //check xem co chuyen sang lv tiep ko
                        if (kq == "1")
                        {
                            tbl = cls.GetDataTable("sp_getNextLevelClaim", new string[] { "@Docno", "@username" }, new object[] { docno, activationCode });
                            if (tbl.Rows.Count > 0)
                            {
                                //co chuyen len tren
                                //lay thong tin nguoi tren
                                clsSys sys = new clsSys();
                                string EmailCreate = cls.cToString(tbl.Rows[0]["EmailCreate"]);
                                string emailnextlevel = cls.cToString(tbl.Rows[0]["Approval"]);
                                SenEmailSubmit(docno, emailnextlevel, "");
                                sys.SendMailASP(EmailCreate, "Claim Expenses has been sent to next level", "Claim Expenses " + docno + " has been approved by N+1 and submit to Next level (" + emailnextlevel + ")");
                                MsgBox1.AddMessage("Approved. Notification of the approval (" + docno + ") will be submitted to the next level(" + emailnextlevel + ") and copy to the requester", uc.ucMsgBox.enmMessageType.Success);
                            }
                            else
                            {

                                string emailhanhchanh = cls.GetString("sp_getEmailHanhChanh", new string[] { "@Code" }, new object[] { docno });
                                string cc = txtAppEmail.Text;
                                if (emailhanhchanh != "")
                                {
                                    cc = cc + ";" + emailhanhchanh;
                                }
                                //de la nguoi duyet cuoi cung
                                clsSys sys = new clsSys();
                                sys.SendMailASP(txtMyEmail.Text, cc, "Expenses claim request has been Approved", "Expenses claim request  " + docno + " has been approved");
                                MsgBox1.AddMessage("Approved! Notification of the approval (" + docno + ") will be sent to the requester", uc.ucMsgBox.enmMessageType.Success);
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
                else{
                    if (RejectedCode.ToLower() != Guid.Empty.ToString().ToLower())
                    {
                        btDelete.Visible = false;
                        dvSum.Visible = false;
                        btExpand.Text = "+";
                        btExpand.ToolTip = "Expand";
                        lbTitle.Text = "Duyệt Đề Nghị Thanh Toán<br /> Approve Expenses Claim";
                        // clsSys sys = new clsSys();
                        //  DBTableDataContext db = new DBTableDataContext();
                        //  string AppLevel = !string.IsNullOrEmpty(Request.QueryString["AppLevel"]) ? Request.QueryString["AppLevel"] : "";
                        string code = !string.IsNullOrEmpty(Request.QueryString["code"]) ? Request.QueryString["code"] : "";


                        LoadCategory(comboDepartment1.Values);
                        LoadMarKet();
                        FLoadAdvance(true,true);
                        tbl = cls.GetDataTable("sp_getByGuiCode", new string[] { "@code", "@appcode" }, new object[] { code, RejectedCode });
                       // DataTable tbl = cls.GetDataTable("sp_getClaimExpensesByGuiCode", new string[] { "@code", "@appcode" }, new object[] { code, RejectedCode });
                        if (tbl.Rows.Count > 0)
                        {
                            string docno = cls.cToString0(tbl.Rows[0]["Code_PK"]);
                            string AppEmail = cls.get_UsernameFromEmail(cls.cToString(tbl.Rows[0]["AppEmail"])).ToLower();
                             string EmailCreate = cls.cToString(tbl.Rows[0]["EmailCreate"]).ToLower();
                           
                            LoadClaimApp(AppEmail);
                          //  bool kq = cls.bCapNhat(new string[] { "@docno", "@Appcode", "@status", "@DateApp", "@Note" }, new object[] { docno, RejectedCode, 3, DateTime.Now, "Rejected via Email" }, "sp_updateApp");
                            string kq = cls.GetString0("sp_updateApp",new string[] { "@docno", "@Appcode", "@status", "@DateApp", "@Note" }, new object[] { docno, RejectedCode, 3, DateTime.Now, "Rejected via Email" });
                            if (kq == "1")
                            {
                                clsSys sys = new clsSys();
                                sys.SendMailASP(EmailCreate, "Claim Expenses has been rejected", "Claim Expenses " + docno + " has been rejected");
                                MsgBox1.AddMessage("Doc no " + docno + " has been Rejected", uc.ucMsgBox.enmMessageType.Success);
                            }
                            else
                            {
                                switch (kq)
                                {
                                    case "0":
                                        MsgBox1.AddMessage("Rejected fail, please contract IT! (" + docno + ")", uc.ucMsgBox.enmMessageType.Error);
                                        break;
                                    case "3":
                                        MsgBox1.AddMessage("This document has already been approved (" + docno + ")", uc.ucMsgBox.enmMessageType.Error);
                                        break;
                                    case "4":
                                        MsgBox1.AddMessage("This document has already been rejected (" + docno + ")", uc.ucMsgBox.enmMessageType.Error);
                                        break;
                                }
                            }
                            dropApp.Enabled = false;
                            dropApp.SelectedValue = docno;
                            btApp.Visible = true;
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
                            GetOldClaim(docno);
                            getClaimDetail(docno);
                            FillTable();

                        }
                        //  db.Dispose();

                    }
                    else
                    {
                        // string sss=  Request.Form["us"];
                        if (Session["username"] != null /*&& Request.Form["us"] != null*/)
                        // if (Request.QueryString["us"] != null)//click vao avatar
                        {
                            Session["indexdit"] = null;

                            hdStatus.Value = "0";
                            hdPrint.Value = "0";
                            //
                            if (Request.QueryString["type"] != null)//click vao avatar
                            {
                                int type = int.Parse(Request.QueryString["type"].ToString());
                                switch (type)
                                {
                                    case 0: //tao moi
                                        RadGrid1.EnableViewState = true;
                                        btAdd.Visible = true;
                                        lbTitle.Text = "Đề Nghị Thanh Toán<br /> Expenses Claim";
                                        dropApp.Visible = false;
                                        dropSaved.Visible = true;
                                        lbStatus.Visible = true;
                                        lbStatusTitle.Visible = true;
                                        //  radioClaim.Visible = true;
                                        btDelete.Visible = false;
                                        LoadMarKet();
                                        LoadClaim();
                                        GetNewClaim();
                                        //if (comboDepartment1.Values != "")
                                        //{
                                            LoadCategory(comboDepartment1.Values);
                                        //}
                                        //else
                                        //{
                                        //    LoadCategory("ALL");
                                        //}
                                        ddlCategory_SelectedIndexChanged(sender, e);
                                        getClaimDetail("");
                                        FLoadAdvance(false,false);
                                        Session["AutoNumber"] = 0;

                                        break;
                                    case 2://approved
                                        RadGrid1.EnableViewState = false;
                                        dvSum.Visible = false;
                                        btExpand.Text = "Expand";
                                        lbTitle.Text = "Duyệt Đề Nghị Thanh Toán<br /> Approve Expenses Claim";
                                        dropApp.Visible = true;
                                        dropSaved.Visible = false;
                                        lbStatus.Visible = false;
                                        lbStatusTitle.Visible = false;
                                        btAdd.Visible = false;
                                        // radioClaim.Visible = false;
                                        btDelete.Visible = false;
                                        LoadCategory(comboDepartment1.Values);
                                        LoadMarKet();
                                        FLoadAdvance(true,true);
                                        hdStatus.Value = "2";
                                        LoadClaimApp(cls.cToString(Session["username"]));

                                        break;
                                    case 4://Print
                                        RadGrid1.EnableViewState = false;
                                        btAdd.Visible = false;
                                        hdPrint.Value = "1";
                                        dvSum.Visible = false;
                                        btExpand.Text = "Expand";
                                        lbTitle.Text = "In Đề Nghị Thanh Toán<br /> Print Out Expenses Claim";
                                        dropApp.Visible = false;
                                        dropSaved.Visible = true;
                                        lbStatus.Visible = false;
                                        lbStatusTitle.Visible = false;
                                        lbAdvance.Visible = true;
                                        dropAdvance.Visible = true;
                                        // radioClaim.Visible = false;
                                        btDelete.Visible = false;
                                        LoadCategory(comboDepartment1.Values);
                                        LoadMarKet();
                                        FLoadAdvance(true,true);
                                        LoadClaimPrint();
                                        GetOldClaim(dropSaved.SelectedValue);
                                        getClaimDetail(dropSaved.SelectedValue);
                                        FillTable();
                                        break;
                                }
                                setButton(int.Parse(hdStatus.Value));
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
        }
        private void LoadCategory(string costcenter)
        {
            DataTable tbl = cls.GetDataTable("sp_LoadChargesClaim", "@costcenter", costcenter);
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
            hdStatus.Value = kq[0].Status.ToString();
            txtAppNote.Visible = false;
            //  txtPurpose.Text = "";
            txtNguoiThuHuong.Text = "";
            radnumAdvncedAmount.Value = 0;
            dbs.Dispose();
          Session["indexdit"] = null;
            Session["NewDocNo"] = GenalCode();

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
                dropAdvance.SelectedValue = kq[0].AdvanceDocNo;
                //  radnumDays.Value =(double) kq[0].NoDays;
                lbStatus.Text = kq[0].StatusText;
                hdStatus.Value = kq[0].Status.ToString();
                txtAppNote.Visible = true;
                txtAppNote.Text = kq[0].NoteApprover;
                txtNguoiThuHuong.Text = kq[0].NguoiThuHuong;
                dbs.Dispose();
                Session["NewDocNo"] = code;
                Session["indexdit"] = null;
            }
        }
        private void setButton(int status)
        {
            switch (status)
            {
                case 0: //new
                    btAdd.Text = "Add";
                    btSave.Visible = true;
                    btDelete.Visible = true;
                    btSubmit.Visible = true;
                    btPrint.Visible = false;
                    txtAppNote.Visible = false;
                    lbReason.Visible = false;
                    btApp.Visible = false;
                    btReject.Visible = false;
                     lbAdvance.Visible = true;
                     dropAdvance.Visible = true;
                     dropAdvance.Enabled = true;
                     lbStatusTitle.Visible = false;
                    lbStatus.Visible = false;
                    btAdd.Visible = true;
                  //  FLoadAdvance(false, false);
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
                    dropAdvance.Visible = true;
                    dropAdvance.Enabled = false;
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
                    dropAdvance.Enabled = false;
                    dropAdvance.Visible = true;
                     lbStatusTitle.Visible = false;
                    lbStatus.Visible = false;
                    btDelete.Visible = false;
                    break;
                case 3://rejected
                    btSave.Visible = true;
                    btAdd.Visible = true;
                    btSubmit.Visible = false;
                    btPrint.Visible = false;
                    txtAppNote.Visible = true;
                    txtAppNote.ReadOnly = true;
                    lbReason.Visible = true;
                    btApp.Visible = false;
                    btReject.Visible = false;
                     lbAdvance.Visible = true;
                    dropAdvance.Enabled = false;
                    dropAdvance.Visible = true;
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
                    
                
                    txtAppNote.Visible = true;
                    txtAppNote.ReadOnly = true;
                    lbReason.Visible = true;
                    btApp.Visible = false;
                    btReject.Visible = false;
                    lbStatusTitle.Visible = false;
                    lbStatus.Visible = false;
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
                    dropAdvance.Visible=true;
                    dropAdvance.Enabled = true;
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
            var kq = dbs.sp_getClaimSaved(Session["username"].ToString(), "OFFICE");//sp_getClaimSaved la store co 2 tham so dau vao
            dropSaved.DataSource = kq;
            dropSaved.DataValueField = "Values";
            dropSaved.DataTextField = "Text";
            dropSaved.DataBind();
            dbs.Dispose();
        }
        private void LoadClaimPrint()
        {

            DataTable kq = cls.GetDataTable("sp_getClaimPrint", new string[] { "@username" }, new object[] { Session["username"]});// dbs.sp_getClaimSaved(Session["username"].ToString(), "");//sp_getClaimSaved la store co 2 tham so dau vao
            dropSaved.DataSource = kq;
            dropSaved.DataValueField = "Values";
            dropSaved.DataTextField = "Text";
            dropSaved.DataBind();
            
        }
        private void LoadClaimApp(string username)
        {
            //DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            //var kq = dbs.sp_getClaimApproved(username);//sp_getClaimSaved la store co 2 tham so dau vao
            //dropApp.DataSource = kq;
            //dropApp.DataValueField = "Values";
            //dropApp.DataTextField = "Text";
            //dropApp.DataBind();
            //dbs.Dispose();

            DataTable kq = new DataTable();

            kq = cls.GetDataTable("sp_getApprovedALL", new string[] { "@username", "@Type" }, new object[] { username, "EC" });

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
            List<sp_getClaimDetailResult> tbl = (List<sp_getClaimDetailResult>)Session["ClaimDetail"];
            RadGrid1.DataSource = tbl;
            RadGrid1.DataBind();
            
        }
        private bool SaveParent(string code)
        {
            try
            {
                using (var dbs = new DBTableDataContext())
                {
                    //ClaimExpense LA TABLE
                    var model = new ClaimExpense { Code_PK = code, Type = "OFFICE", DateRec = raddateNow.SelectedDate.Value, UserName = Session["username"].ToString(), Approver = txtAppName.Text, AppEmail = txtAppEmail.Text.ToLower(), Status = 0, FDate = raddateFrom.SelectedDate.Value, TDate = raddateTo.SelectedDate.Value, NoDays = TinhNgay(), Purpose = txtPurpose.Text, Market = dropMarket.SelectedValue, Department = comboDepartment1.Values, Position = txtPosition.Text, FullName = txtName.Text, DaTamUng = (decimal)radnumAdvncedAmount.Value, Tra_ThuChenhLech = 0, DocTot = 0, NoteApprover = "", Email = txtMyEmail.Text.ToLower(), AdvanceDocNo = dropAdvance.SelectedValue,Costcenter=comboDepartment1.getCoscenter,NguoiThuHuong=txtNguoiThuHuong.Text };
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
                    //  model.FDate = raddateFrom.SelectedDate.Value;
                    //  model.TDate = raddateTo.SelectedDate.Value;
                    //  model.NoDays = (int)radnumDays.Value;
                    // model.Purpose = txtPurpose.Text;
                    model.Market = dropMarket.SelectedValue;
                    model.Department = comboDepartment1.Values;
                    model.Position = txtPosition.Text;
                    model.FullName = txtName.Text;
                    model.DaTamUng = (decimal)radnumAdvncedAmount.Value;
                    model.AdvanceDocNo = dropAdvance.SelectedValue;
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
                List<sp_getClaimDetailResult> tbl = (List<sp_getClaimDetailResult>)Session["ClaimDetail"];
                foreach (sp_getClaimDetailResult item in tbl)
                {
                    if (item.TotalVN > 0)
                    {
                        var model = new ClaimExpensesDetail { Code_FK = code, Date = item.Date, No = item.No, Notation = item.Notation, Charges_FK = item.Charges_FK, Currency = item.Currency, Rate = item.Rate, Amount = item.Amount, TotalVN = item.TotalVN, PictureURL = item.PictureURL, CompanyName = item.CompanyName, Province = item.Province, VATCode = item.VATCode, VATAmount = item.VATAmount, FDate = item.FDate, TDate = item.TDate, NoDays = item.NoDays, Purpose = item.Purpose, CompanyCode = item.CompanyCode, GL = item.GL, IO = item.IO, DetailExpenses = item.DetailExpenses, Participant = item.Participant, FileAttach=item.FileAttach,Costcenter=item.Costcenter,CurrencyDescription=item.CurrencyDescription };
                        dbs.ClaimExpensesDetails.InsertOnSubmit(model);
                    }
                }
                // dbs.ClaimExpensesDetails.InsertAllOnSubmit(tbl);
                dbs.SubmitChanges();
                // LoadSum(code);
                lbMess.Text = "Saved successfully";

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
            }
            dbs.Dispose();
        }
        private void SaveOneDetail(string code)
        {
            DBTableDataContext dbs = new DBTableDataContext();
            try
            {
                Cclass cls = new Cclass();

                if (radnumAmount.Value > 0)
                {
                    var model = new ClaimExpensesDetail { Code_FK = code, Date = raddateInvoice.SelectedDate.Value, No = txtNoInvoice.Text, Notation = "", Charges_FK = ddlCategory.SelectedValue, Currency = comboCurrence1.CurrText, Rate = cls.cToDouble(comboCurrence1.RateText), Amount = cls.cToDecimal(radnumAmount.Value), TotalVN = cls.cToDecimal(radnumAmount.Value * cls.cToDouble(comboCurrence1.RateText)), PictureURL = "", CompanyName = "", Province = "", VATCode = "", VATAmount = 0, FDate = raddateFrom.SelectedDate.Value, TDate = raddateTo.SelectedDate.Value, NoDays = TinhNgay(), Purpose = txtPurpose.Text, CompanyCode = "", GL = txtGL.Text, IO = txtIO.Text, DetailExpenses = txtDetailExpenses.Text, Participant = txtParticipant.Text, Costcenter = comboDepartment1.Values, CurrencyDescription = comboCurrence1.CurrTextFull };
                    dbs.ClaimExpensesDetails.InsertOnSubmit(model);
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
                MsgBox1.AddMessage("Please fill in Amount(FC):", uc.ucMsgBox.enmMessageType.Error);
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
            clsSys sys = new clsSys();
            List<sp_getClaimDetailResult> tbl = (List<sp_getClaimDetailResult>)Session["ClaimDetail"];
            sp_getClaimDetailResult item = new sp_getClaimDetailResult();
            int index = -1;
            if (Session["indexdit"] != null && cls.cToInt(Session["indexdit"]) >= 0)
            {
                index=cls.cToInt(Session["indexdit"]);
                tbl.RemoveAt(index);
                btAdd.Text = "Add";
                Session["indexdit"] = null;
            }
            //else
            //{
            //    item = tbl[cls.cToInt(Session["indexdit"])];
            //}
            
            
            //them Code_FK
            item.Code_FK = Session["NewDocNo"].ToString();
            item.Date = raddateInvoice.SelectedDate.Value;
            item.No = txtNoInvoice.Text;
            item.Notation = "";
            item.Charges_FK = ddlCategory.SelectedValue;
            item.Description = ddlCategory.SelectedItem.Text;
            item.CompanyName = sys.cToString(Session["Company"]);
            item.Province = sys.cToString(Session["Province"]);
            item.VATCode = sys.cToString(Session["TaxCode"]);
            item.VATAmount = sys.cToDuoble(Session["VATAmount"]);
            item.FDate = raddateFrom.SelectedDate.Value;
            item.TDate = raddateTo.SelectedDate.Value;
            item.Purpose = txtPurpose.Text.Trim();
            item.TaxNumber = sys.cToString(Session["TaxNumber"]);
            item.Vendor = "vendor";
            if (ddlCategory.SelectedValue.ToLower() == "ho")
            {
                item.NoDays = nodays - 1;

            }
            else
            {
                item.NoDays = nodays;
            }
            //if (radioClaim.SelectedValue.ToLower() == "domestic")
            //{
            //    item.Currency = "VND";
            //    item.Rate = 1;
            //}
            //else
            //{
            item.Currency = comboCurrence1.CurrText;
            item.CurrencyDescription = comboCurrence1.CurrTextFull;
            item.Rate = double.Parse(comboCurrence1.RateText);
            //  }
            item.Amount = (decimal)radnumAmount.Value;
            decimal tongtienvnd = cls.cToDecimal(comboCurrence1.RateText) * cls.cToDecimal(radnumAmount.Value);
            item.TotalVN = tongtienvnd;// (decimal)radnuTotalVND.Value;
            Session["AutoNumber"] = (int)Session["AutoNumber"] + 1;
            // imgUpload1.uploadimg(Session["NewDocNo"].ToString() + "-"+Session["AutoNumber"].ToString());
            //  item.PictureURL = imgUpload1.FileName;
            item.PictureURL = "";
            item.DetailExpenses = txtDetailExpenses.Text;
            item.Participant = txtParticipant.Text;
            item.GL = txtGL.Text;
            item.IO = txtIO.Text;
            item.Costcenter = comboDepartment1.Values;
            if (FileUpload1.HasFile)
            {
                try
                {
                    int vt1 = FileUpload1.FileName.LastIndexOf(".");
                    int vtcanlay = vt1;
                    int len = FileUpload1.FileName.Length;
                    string extention = FileUpload1.FileName.Substring(vtcanlay, len - vtcanlay);
                    string filename = "";
                    filename = Session["NewDocNo"].ToString() + "-" + Session["AutoNumber"].ToString();
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
           
            Session["ClaimDetail"] = tbl;
            Session["Company"] = "";
            Session["Province"] = "";
            Session["TaxCode"] = "";
            Session["TaxNumber"] = "";
            Session["VATAmount"] = 0;
            //if (radioClaim.SelectedValue.ToLower() == "domestic")
            FillTable();
            raddateInvoice.Focus();
            //SaveOneDetail(Session["NewDocNo"].ToString());
            // }
        }
        private void getClaimDetail(string code)
        {
            DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            List<sp_getClaimDetailResult> kq = dbs.sp_getClaimDetail(code,false).OrderBy(o => o.ID).ToList();//sp_getClaimDetail LA STORE
            if (code == "0")
            {
                kq.RemoveAt(0);
            }
            Session["ClaimDetail"] = kq;
            Session["AutoNumber"] = kq.Count;
            dbs.Dispose();

        }
        protected void dropSaved_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Literal1.Text = "";
            lbMess.Text = "";
            getClaimDetail(dropSaved.SelectedValue);
            if (dropSaved.SelectedValue == "0")
            {
                GetNewClaim();
                lbStatus.Visible = true;
                lbStatusTitle.Visible = true;
                FLoadAdvance(false,false);
                getClaimDetail("0");
                FillTable();
                btAdd.Text = "Add";
                btDelete.Visible = false;
                RadGrid2.Visible = false;
           
                RadGrid2.DataSource = null;
                RadGrid2.DataBind();
            }
            else
            {
               // FLoadAdvance(true);
                
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

            }
            setButton(int.Parse(hdStatus.Value));
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
                dropAdvance.Enabled = false;
                GetOldClaim(dropApp.SelectedValue);
               
                FillTable();
                //gridSum.Visible = true;
                //  LoadSum(dropApp.SelectedValue);
            }
        }
     
        protected void btSave_Click(object sender, EventArgs e)
        {
            //if (txtPurpose.Text.Trim() == "")
            //{
            //    MsgBox1.AddMessage("Please fill Purpose",uc.ucMsgBox.enmMessageType.Error);
            //    txtPurpose.Focus();
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
                string code = Session["NewDocNo"].ToString();
                if (SaveParent(code) == true)
                {
                    SaveDetail(code);
                    updateDocTot(code);
                    //  LoadSum(code);
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
                    UpdateDetail(dropSaved.SelectedValue);
                    updateDocTot(dropSaved.SelectedValue);
                    //  LoadSum(dropSaved.SelectedValue);
                    btSubmit.Visible = true;
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
        private void SenEmailSubmit(string code, string to, string appby)
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


            string nguoidenghi = txtName.Text;
            string phongban = comboDepartment1.Text;
            string mucdich = txtPurpose.Text;
            string datamung = cls.FormatNumber(radnumAdvncedAmount.Value);
            string thoigian = "Từ/From " + raddateFrom.SelectedDate.Value.ToString("dd-MMM-yy") + " Đến/To " + raddateTo.SelectedDate.Value.ToString("dd-MMM-yy");

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
            html = html + "<th align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\" rowspan=\"2\">Tiền tệ<br /> Currency </th>";
            html = html + " <th align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\" rowspan=\"2\"> Nguyên tệ<br /> Amount</th>";
            html = html + "<th align=\"center\" rowspan=\"2\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\">Tỉ giá<br /> Rate (VND)</th>";
            html = html + "<th align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\" rowspan=\"2\"> Thành tiền VND<br /> Amount</th>";
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
            List<sp_getClaimDetailResult> kq1 = dbs.sp_getClaimDetail(code, true).ToList();//sp_getClaimDetail LA STORE
            Session["ClaimDetailPrint"] = kq1;
            dbs.Dispose();
           string kq= FillTableemail();
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
           
            if (kq2 == true)
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
                cls.bXoa(new string[] { "@Docno", "@username" }, new object[] { code, to }, "sp_DeleteApproveByEmail");
                lbStatus.Text = "Saved";
                lbMess.Text = "Failed to submit";
                MsgBox1.AddMessage("Failed to send, Please try again!", uc.ucMsgBox.enmMessageType.Error);
            }

        }
        private string FillTableemail()
        {
            string kq = "";

            List<sp_getClaimDetailResult> tbl = (List<sp_getClaimDetailResult>)Session["ClaimDetailPrint"];
            
            double tongtien = 0;
            int stt = 0;
            foreach (sp_getClaimDetailResult item in tbl)
            {

                if (item.TotalVN != 0)
                {
                    if (item.Date == new DateTime(2000, 1, 1))
                    {
                        //dong subtotal

                        kq = kq + "<tr><td colspan=8 style='color: #000000; font-weight: bold; text-align:left; border:1px solid black; border-collapse:collapse;'>Subtotal</td>";

                        kq = kq
                          + "<td colspan=3 style='color: #000000; font-weight: bold; border:1px solid black; border-collapse:collapse; text-align:left;'>" + String.Format("{0:0,0}", item.TotalVN) + "</td>"
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
                            + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Participant + "</td>";

                        kq = kq + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.Currency + "</td>"
                     + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.Amount) + "</td>"
                    + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.Rate) + "</td>";

                        kq = kq
                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.TotalVN) + "</td>"
                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.GL + "</td>"
                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.IO + "</td>"
                          + "</tr>";
                    }


                }
            }
            //tinh tong tien
            kq = kq + "<tr><td colspan=8 style='color: #000000; font-weight: bold; text-align:right;'>Tổng tiền VND/Total Amount:</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + cls.FormatNumber(tongtien) + "</td><td colspan=2></td></tr>";
            kq = kq + "<tr><td colspan=8 style='color: #000000; font-weight: bold; text-align:right;'>Đã tạm ứng VND/Advanced Amount:</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + cls.FormatNumber(radnumAdvncedAmount.Value) + "</td><td colspan=2></td></tr>";
            kq = kq + "<tr><td colspan=8 style='color: #000000; font-weight: bold; text-align:right;'>Chênh lệch VND/Pay back(+)/Reimbursemet(-):</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + cls.FormatNumber(tongtien - cls.cToDouble(radnumAdvncedAmount.Value)) + "</td><td colspan=2></td></tr>";
            return kq;
        }
        protected void btSubmit_Click(object sender, EventArgs e)
        {
         
            try
            {
                string docno = Session["NewDocNo"].ToString();
                  
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
                    string n1 = txtAppEmail.Text;
                    string senior = cls.getSeniorManager(username);
                    string director = cls.getDirector(username);
                    string vp = cls.getVP(username);
                    string coo = cls.getCOO(username);
                    string type = "EC";
                    bool kqsmit = false;
                    //if (senior == "")
                    //{
                    //    senior = txtAppEmail.Text;
                    //}
                    Guid activationCode = Guid.NewGuid();
                    kqsmit = cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, n1, 1, 0, activationCode, type }, "sp_insertApprove");
                    if (sotien <= 10000000)
                    {
                        if (senior == "")
                        {
                            senior = director;
                        }
                        if (senior == "")
                        {
                            senior = vp;
                        }
                        if (senior == "")
                        {
                            senior = coo;
                        }
                        activationCode = Guid.NewGuid();
                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, senior, 2, 0, activationCode, type }, "sp_insertApprove");
                        goto sendemail;
                    }
                    if (sotien > 10000000 && sotien <= 50000000)
                    {
                        if (senior == "")
                        {
                            senior = director;
                        }
                        if (senior == "")
                        {
                            senior = vp;
                        }
                        if (senior == "")
                        {
                            senior = coo;
                        }
                        //N+1
                          activationCode = Guid.NewGuid();
                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, senior, 2, 0, activationCode, type }, "sp_insertApprove");

                       
                        if (director == "")
                        {
                            director = vp;
                        }
                        if (director == "")
                        {
                            director = coo;
                        }
                        activationCode = Guid.NewGuid();
                        // string director = cls.getDirector(username);
                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, director, 3, 0, activationCode, type }, "sp_insertApprove");
                        goto sendemail;
                    }
                    if ((sotien > 50000000 && sotien <= 500000000))
                    {
                        //N+1
                        if (senior == "")
                        {
                            senior = director;
                        }
                        if (senior == "")
                        {
                            senior = vp;
                        }
                        if (senior == "")
                        {
                            senior = coo;
                        }
                        activationCode = Guid.NewGuid();
                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, senior, 2, 0, activationCode, type }, "sp_insertApprove");
                        
                        if (director == "")
                        {
                            director = vp;
                        }
                        if (director == "")
                        {
                            director = coo;
                        }
                        activationCode = Guid.NewGuid();
                        //  string director = cls.getDirector(username);
                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, director, 3, 0, activationCode, type }, "sp_insertApprove");
                        
                        if (vp == "")
                        {
                            vp = coo;
                        }
                        activationCode = Guid.NewGuid();
                        //  string vp = cls.getVP(username);
                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, vp, 4, 0, activationCode, type }, "sp_insertApprove");
                        goto sendemail;
                    }
                    if (sotien > 500000000)
                    {
                        //N+1
                        //N+1
                        if (senior == "")
                        {
                            senior = director;
                        }
                        if (senior == "")
                        {
                            senior = vp;
                        }
                        if (senior == "")
                        {
                            senior = coo;
                        }
                        activationCode = Guid.NewGuid();
                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, senior, 2, 0, activationCode, type }, "sp_insertApprove");
                       
                        
                        if (director == "")
                        {
                            director = vp;
                        }
                        if (director == "")
                        {
                            director = coo;
                        }
                        activationCode = Guid.NewGuid();
                                                //  string director = cls.getDirector(username);
                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, director, 3, 0, activationCode, type }, "sp_insertApprove");
                        if (vp == "")
                        {
                            vp = coo;
                        }
                        activationCode = Guid.NewGuid();
                        //  string vp = cls.getVP(username);
                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, vp, 4, 0, activationCode, type }, "sp_insertApprove");
                        activationCode = Guid.NewGuid();
                        // string coo = cls.getCOO(username);
                        cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, coo, 5, 0, activationCode, type }, "sp_insertApprove");
                        goto sendemail;
                    }
                sendemail:
                    if (kqsmit == true)
                    {
                        SenEmailSubmit(docno, n1 /*txtAppEmail.Text*/, /*txtMyEmail.Text*/"");
                    }

                    //if (dropSaved.SelectedValue == "0")//tao moi
                    //{
                    //    //ChangeStatus(Session["NewDocNo"].ToString(), 2, "");
                    //    using (var db = new DBTableDataContext())
                    //    {
                    //        var model = db.ClaimExpenses.SingleOrDefault(p => p.Code_PK == cls.cToString(Session["NewDocNo"]));
                    //        model.Status = 2;
                    //        model.NoteApprover = "";
                    //        model.DateSubmit = DateTime.Now;
                    //        db.SubmitChanges();
                    //    }
                    //    //send email nguoi approve anh cc me
                    //    SenEmailSubmit(Session["NewDocNo"].ToString());
                    //    btSave.Visible = false;
                    //    btSubmit.Visible = false;
                    //}
                    //else
                    //{
                    //    //ChangeStatus(dropSaved.SelectedValue, 2, "");
                    //    using (var db = new DBTableDataContext())
                    //    {
                    //        var model = db.ClaimExpenses.SingleOrDefault(p => p.Code_PK == dropSaved.SelectedValue);
                    //        model.Status = 2;
                    //        model.NoteApprover = "";
                    //        model.DateSubmit = DateTime.Now;
                    //        db.SubmitChanges();
                    //    }
                    //    //send email nguoi approve anh cc me
                    //    SenEmailSubmit(dropSaved.SelectedValue);
                    //    btSave.Visible = false;
                    //    btSubmit.Visible = false;
                    //}
                    //   Submitted 	
                    //lbStatus.Text = "Submitted";
                    //lbMess.Text = "Submit success";
                    //MsgBox1.AddMessage("Submit success", uc.ucMsgBox.enmMessageType.Success);0
                }
            }
            catch { }

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
                    SenEmailSubmit(docno, emailnextlevel, cls.cToString(Session["username"]));
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
            }
            else
            {
                string docno = dropApp.SelectedValue;
                // ChangeStatus(docno, 3, txtAppNote.Text);
                //send email
                bool kq = cls.bCapNhat(new string[] { "@docno", "@username", "@status", "@DateApp", "@Note" }, new object[] { docno, Session["username"], 3, DateTime.Now, txtAppNote.Text }, "sp_updateAppbyName");
                if (kq == true)
                {
                    LoadClaimApp(cls.cToString(Session["username"]));
                    clsSys sys = new clsSys();
                    //sys.SenEmailReject(docno, txtAppNote.Text, txtMyEmail.Text, txtAppEmail.Text);
                    sys.SendMailASP(txtMyEmail.Text, /*txtAppEmail.Text, */"Expenses claim request has been Rejected", "Expenses claim request " + docno + " has been rejected with reason " + txtAppNote.Text);
                    txtAppNote.Focus();
                    btAdd.Visible = false;
                    btReject.Visible = false;
                    MsgBox1.AddMessage("Rejected. Notification of the reject (" + docno + ") will be emailed to the expenses claim initiator", uc.ucMsgBox.enmMessageType.Success);
                }


                //using (var db = new DBTableDataContext())
                //{
                //    var model = db.ClaimExpenses.SingleOrDefault(p => p.Code_PK == docno);
                //    model.ApprovedCode1 = null;
                //    model.Status = 3;
                //    model.DateApp1 = DateTime.Now;
                //    model.NoteApprover = txtAppNote.Text;
                //    db.SubmitChanges();
                //}
               
                //LoadClaimApp(cls.cToString(Session["username"]));
                //clsSys sys = new clsSys();
                //sys.SenEmailReject(docno, txtAppNote.Text, txtMyEmail.Text, txtAppEmail.Text);
                //btAdd.Visible = false;
                //btReject.Visible = false;
                //MsgBox1.AddMessage("Rejected success", uc.ucMsgBox.enmMessageType.Success);
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
                DataTable tbl = cls.GetDataTable("sp_GetAdvanceNo", new string[] { "@username", "@LoadAll", "@LoadAllUser" }, new object[] { Session["username"], LoadAll, LoadAllUser });
                dropAdvance.DataSource = tbl;
                dropAdvance.DataBind();
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
            sb.Append("window.open('PrintClaimOffice.aspx','PrintMe', 'height=900px,width=1024px,scrollbars=1');");
            //ScriptManager.RegisterClientScriptBlock(this.RadAjaxManager1, this.RadAjaxManager1.GetType(), "NewClientScript", sb.ToString(), true);
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "NewClientScript", sb.ToString(), true);
        }
       
        protected void radnumAmount_TextChanged(object sender, EventArgs e)
        {
            clsSys sys = new clsSys();
            radnuTotalVND.Value = radnumAmount.Value * sys.cToDuoble(comboCurrence1.RateText);
            radnuTotalVND.Focus();
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
            List<sp_getClaimDetailResult> tbl = (List<sp_getClaimDetailResult>)Session["ClaimDetail"];
            string filename = tbl[index].FileAttach;
            if (filename != "")
            {
                string sFolderPath = Server.MapPath("Upload/EC/" + filename);
                if (System.IO.File.Exists(sFolderPath) == true)
                    System.IO.File.Delete(sFolderPath);
            }
            tbl.RemoveAt(index);
            Session["ClaimDetail"] = tbl;
            FillTable();
        }

        protected void dropAdvance_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            List<sp_GetUserInfo_TravelExistsResult> kq = dbs.sp_GetUserInfo_TravelExists(dropAdvance.SelectedValue).ToList();//sp_getClaimDetail LA STORE
            if (kq.Count>0)
            {
                comboDepartment1.Values = kq[0].Department;
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
                radnumAdvncedAmount.Value = cls.cToDouble(kq[0].AdvanceAmount);
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
           
            DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            List<sp_getTravelDetailResult> kq = dbs.sp_getTravelDetail(dropAdvance.SelectedValue).OrderBy(o => o.ID).ToList();//sp_getClaimDetail LA STORE
            if (dropAdvance.SelectedValue == "0" || dropAdvance.SelectedValue == "")
            {
                kq.RemoveAt(0);
            }
            if (kq.Count > 0)
            {
                Session["TravelDetail"] = kq;
                dbs.Dispose();
                Session["docno"] = dropAdvance.SelectedValue;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("window.open('PrintTravelRequest.aspx','PrintMe', 'height=900px,width=1024px,scrollbars=1');");
                //ScriptManager.RegisterClientScriptBlock(this.RadAjaxManager1, this.RadAjaxManager1.GetType(), "NewClientScript", sb.ToString(), true);
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "NewClientScript", sb.ToString(), true);
            }
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
           // comboCurrence1.CurrText = editedItem["Currency"].Text;// tbl[index].Currency;
            comboCurrence1.CurrTextFull = editedItem["CurrencyDescription"].Text;// tbl[index].Currency;
           // comboCurrence1.CurrValues = editedItem["Currency"].Text;
            comboCurrence1.RateText = editedItem["Rate"].Text;//cls.cToString(tbl[index].Rate);
            radnumAmount.Value = cls.cToDouble(editedItem["Amount"].Text);// cls.cToDouble(tbl[index].Amount);
            radnuTotalVND.Value = cls.cToDouble(editedItem["TotalVN"].Text);//cls.cToDouble(tbl[index].TotalVN);
           
            txtGL.Text = editedItem["GL"].Text;//tbl[index].GL;
            txtIO.Text = editedItem["IO"].Text;
            txtDetailExpenses.Text = editedItem["DetailExpenses"].Text;
            txtParticipant.Text = editedItem["Participant"].Text;
            btAdd.Text = "Update";
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
            if (cls.bXoa(new string[] { "@docno" }, new object[] { dropSaved.SelectedValue }, "sp_deleteExpenses") == true)
            {
                LoadClaim();
                MsgBox1.AddMessage("Deleted successfully", uc.ucMsgBox.enmMessageType.Success);

            }
            else
            {
                MsgBox1.AddMessage("Deleted fail", uc.ucMsgBox.enmMessageType.Error);
            }
            
        }
    }
}