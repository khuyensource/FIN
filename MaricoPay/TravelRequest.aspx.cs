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
    public partial class TravelRequest : System.Web.UI.Page
    {
        //them 2 bien nay de fix refresh tu don gui email lai
        private bool _refreshState;
        private bool _isRefresh;
        //them 2 ham nay de fix refresh tu don gui email lai
        protected override void LoadViewState(object savedState)
        {
            object[] AllStates = (object[])savedState;
            base.LoadViewState(AllStates[0]);
            _refreshState = bool.Parse(AllStates[1].ToString());
            _isRefresh = _refreshState == bool.Parse(Session["__ISREFRESH"].ToString());
        }
        //them 2 ham nay de fix refresh tu don gui email lai
        protected override object SaveViewState()
        {
            Session["__ISREFRESH"] = _refreshState;
            object[] AllStates = new object[2];
            AllStates[0] = base.SaveViewState();
            AllStates[1] = !(_refreshState);
            return AllStates;
        }
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
          //  this.comboDepartment1.SelectedIndexChanged += new EventHandler(comboDepartment1_SelectedIndexChanged);
            // raddateFrom.EnableAjaxSkinRendering = true;
            if (!IsPostBack)
            {
                DataTable tbl = new DataTable();
                //xet xem co phai truy cap tu link email
                string activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
                string RejectedCode = !string.IsNullOrEmpty(Request.QueryString["RejectedCode"]) ? Request.QueryString["RejectedCode"] : Guid.Empty.ToString();

                if (activationCode != Guid.Empty.ToString())
                {
                    lbTitle.Text = " Duyệt Phiếu Đề Nghị Công Tác<br /> Approve Business Travel Request";
                    string code = !string.IsNullOrEmpty(Request.QueryString["code"]) ? Request.QueryString["code"] : "";
                    LoadCategory("ALL");
                    LoadMarKet();
                    LoadNguoiDi();
                    tbl = cls.GetDataTable("sp_getByGuiCode", new string[] { "@code", "@appcode" }, new object[] { code, activationCode });
                    if (tbl.Rows.Count > 0)
                    {
                        string docno = cls.cToString0(tbl.Rows[0]["Code_PK"]);
                        
                        string AppEmail = cls.get_UsernameFromEmail(cls.cToString(tbl.Rows[0]["AppEmail"]));
                        LoadClaimApp(AppEmail);
                        //bool kq = cls.bCapNhat(new string[] { "@docno", "@Appcode", "@status", "@DateApp", "@Note" }, new object[] { docno, activationCode, 1, DateTime.Now, "Approved via Email" }, "sp_updateApp");
                        string kq = cls.GetString0("sp_updateApp",new string[] { "@docno", "@Appcode", "@status", "@DateApp", "@Note" }, new object[] { docno, activationCode, 1, DateTime.Now, "Approved via Email" });
                        dropApp.Enabled = false;
                        dropApp.SelectedValue = docno;
                        btApp.Visible = false;
                        btPrint.Visible = false;
                        btPrintAdvance.Visible = false;
                        lbPD.Visible = false;
                        txtDPNo.Visible = false;
                        dropSaved.Visible = false;
                        btReject.Visible = false;
                        txtAppNote.ReadOnly = false;
                        txtAppNote.Visible = false;
                        lbReason.Visible = false;
                        btSave.Visible = false;
                        btSubmit.Visible = false;
                        lbStatus.Visible = false;
                        lbStatusTitle.Visible = false;
                        GetOldClaim(docno);
                        getClaimDetail(docno);
                        FillTable();
                    
                       
                        //check xem co chuyen sang lv tiep ko
                     if (kq == "1")
                     {
                         tbl = cls.GetDataTable("sp_getNextLevel", new string[] { "@Docno", "@username" }, new object[] { docno, activationCode });
                         if (tbl.Rows.Count > 0)
                         {
                             //co chuyen len tren
                             //lay thong tin nguoi tren
                             clsSys sys = new clsSys();
                             string EmailCreate = cls.cToString(tbl.Rows[0]["EmailCreate"]);
                             string emailnextlevel = cls.cToString(tbl.Rows[0]["Approval"]);
                             SenEmailSubmit(docno, emailnextlevel, "");
                             sys.SendMailASP(EmailCreate, "Travel request has been sent to next level", "Travel request " + docno + " has been approved by N+1 and submit to Next level (" + emailnextlevel + ")");
                             MsgBox1.AddMessage("Approved! Notification of the approval (" + docno + ") will be submitted to the next level and copy to the requester", uc.ucMsgBox.enmMessageType.Success);
                         }
                         else
                         {

                             string emailhanhchanh = cls.GetString("sp_getEmailHanhChanh", new string[] { "@Code" }, new object[] { docno });
                             string to = txtMyEmail.Text;
                             if (emailhanhchanh != "")
                             {
                                 to = to + ";" + emailhanhchanh;
                             }
                             //de la nguoi duyet cuoi cung
                             //clsSys sys = new clsSys();
                             SenEmailApproved(docno, to, txtAppEmail.Text, cls.cToString(Session["username"]));
                            // sys.SendMailASP(txtMyEmail.Text, cc, "Travel request has been approved", "Travel request  " + docno + " has been approved");
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
                    // return;

                }
                else
                {
                    if (RejectedCode != Guid.Empty.ToString())
                    {
                        lbTitle.Text = " Duyệt Phiếu Đề Nghị Công Tác<br /> Approve Business Travel Request";
                        string code = !string.IsNullOrEmpty(Request.QueryString["code"]) ? Request.QueryString["code"] : "";
                        LoadCategory("ALL");
                        LoadMarKet();
                        LoadNguoiDi();
                        tbl = cls.GetDataTable("sp_getByGuiCode", new string[] { "@code", "@appcode" }, new object[] { code, RejectedCode });
                        if (tbl.Rows.Count > 0)
                        {
                            string docno = cls.cToString0(tbl.Rows[0]["Code_PK"]);
                            string AppEmail = cls.get_UsernameFromEmail(cls.cToString(tbl.Rows[0]["AppEmail"]));
                            string EmailCreate = cls.cToString(tbl.Rows[0]["EmailCreate"]);
                            LoadClaimApp(AppEmail);
                          //  bool kq = cls.bCapNhat(new string[] { "@docno", "@Appcode", "@status", "@DateApp", "@Note" }, new object[] { docno, RejectedCode, 3, DateTime.Now, "Rejected via Email" }, "sp_updateApp");
                            string kq = cls.GetString0("sp_updateApp",new string[] { "@docno", "@Appcode", "@status", "@DateApp", "@Note" }, new object[] { docno, RejectedCode, 3, DateTime.Now, "Rejected via Email" });
                            if (kq == "1")
                            {
                                clsSys sys = new clsSys();
                                sys.SendMailASP(EmailCreate, "Travel request has been rejected", "Travel request " + docno + " has been rejected");
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
                            btApp.Visible = false;
                            dropSaved.Visible = false;
                            btPrint.Visible = false;
                            btPrintAdvance.Visible = false;
                            lbPD.Visible = false;
                            txtDPNo.Visible = false;
                            btReject.Visible = true;
                            txtAppNote.ReadOnly = false;
                            txtAppNote.Visible = true;
                            lbReason.Visible = true;
                            btSave.Visible = false;
                            btSubmit.Visible = false;
                            lbStatus.Visible = false;
                            lbStatusTitle.Visible = false;
                            GetOldClaim(docno);
                            getClaimDetail(docno);
                            FillTable();

                        }
                       // return;
                    }
                    else
                    {
                        // string sss=  Request.Form["us"];
                        if (Session["username"] != null /*&& Request.Form["us"] != null*/)
                        // if (Request.QueryString["us"] != null)//click vao avatar
                        {

                            hdStatus.Value = "0";
                            //
                            if (Request.QueryString["type"] != null)//click vao avatar
                            {
                                int type = int.Parse(Request.QueryString["type"].ToString());
                                switch (type)
                                {
                                    case 0: //tao moi
                                        RadGrid1.EnableViewState = true;
                                        lbTitle.Text = " Phiếu Đề Nghị Công Tác<br /> Business Travel Request";
                                        dropApp.Visible = false;
                                        dropSaved.Visible = true;
                                         lbStatus.Visible = true;
                        lbStatusTitle.Visible = true;
                                         lbReason.Visible = false;
                                         txtAppNote.Visible = false;
                                         btAdd.Visible = true;
                                         LoadNguoiDi();
                                        LoadClaim();


                                        GetNewClaim();
                                        getClaimDetail("0");
                                        FillTable();
                                        // getClaimDetail("");
                                        Session["AutoNumber"] = 0;
                                        LoadCategory("ALL");
                                        LoadMarKet();
                                        //string n1 = txtAppEmail.Text;
                                        //string senior = cls.getSeniorManager(cls.GetString(Session["Username"]));
                                        //string director = cls.getDirector(cls.GetString(Session["Username"]));
                                        //string vp = cls.getVP(cls.GetString(Session["Username"]));
                                        //string coo = cls.getCOO(cls.GetString(Session["Username"]));

                                        break;
                                    case 2://approved
                                        RadGrid1.EnableViewState = false;
                                        lbTitle.Text = " Duyệt Phiếu Đề Nghị Công Tác<br /> Approve Business Travel Request";
                                        dropApp.Visible = true;
                                        btAdd.Visible = false;
                                        dropSaved.Visible = false;
                                         lbStatus.Visible = false;
                                         lbStatusTitle.Visible = false;
                                          lbReason.Visible = true;
                                          txtAppNote.Visible = true;
                                          LoadNguoiDi();
                                        LoadClaimApp(cls.cToString(Session["username"]));
                                        LoadCategory("ALL");
                                        LoadMarKet();
                                        hdStatus.Value = "2";
                                        break;
                                    case 4://Print
                                        RadGrid1.EnableViewState = false;
                                        LoadNguoiDi();
                                        hdStatus.Value = "4";
                                        lbTitle.Text = "In Phiếu Đề Nghị Công Tác<br /> Print Out Business Travel Request";
                                        dropApp.Visible = false;
                                        dropSaved.Visible = true;
                                         lbStatus.Visible = false;
                                         lbStatusTitle.Visible = false;
                                         lbReason.Visible = false;
                                         txtAppNote.Visible = false;
                                         btAdd.Visible = false;
                                        // radioClaim.Visible = false;
                                        LoadCategory("ALL");
                                        LoadMarKet();
                                        LoadClaimPrint();
                                        GetOldClaim(dropSaved.SelectedValue);
                                        getClaimDetail(dropSaved.SelectedValue);
                                        FillTable();

                                        Session["AutoNumber"] = 0;
                                        

                                        break;
                                    case 20: //tao moi
                                        LoadNguoiDi();
                                        lbTitle.Text = " Phiếu Đề Nghị Tạm Ứng<br /> Advance Request";
                                        dropApp.Visible = false;
                                        dropSaved.Visible = true;
                                        lbStatus.Visible = true;
                                        lbStatusTitle.Visible = true;
                                        lbReason.Visible = false;
                                        txtAppNote.Visible = false;
                                        lbTo.Visible = false;
                                        raddateFrom.Visible = false;
                                        raddateTo.Visible = false;
                                        lbFrom.Visible = false;
                                        lbDestination.Visible = false;
                                        txtNoiDen.Visible = false;
                                        lbItenerary.Visible = false;
                                        txtLoTrinh.Visible = false;
                                        lbTransportation.Visible = false;
                                        chkOto.Visible = false;
                                        chkTauHoa.Visible = false;
                                        chkMayBay.Visible = false;
                                        lbRequest.Visible = false;
                                        chkVeTauMayBay.Visible = false;
                                        chkDatPhong.Visible = false;
                                        chkOther.Visible = false;
                                        txtOther.Visible = false;
                                        btAdd.Visible = true;

                                        LoadClaim();


                                        GetNewClaim();
                                        getClaimDetail("0");
                                        FillTable();
                                        // getClaimDetail("");
                                        Session["AutoNumber"] = 0;
                                        LoadCategory("ALL");
                                        LoadMarKet();
                                        break;
                                    case 21://approved
                                        LoadNguoiDi();
                                        lbTitle.Text = " Duyệt Phiếu Đề Nghị Tạm Ứng<br /> Approve Advance Request";
                                        dropApp.Visible = true;
                                        dropSaved.Visible = false;
                                        lbStatus.Visible = false;
                                        lbStatusTitle.Visible = false;
                                        lbReason.Visible = true;
                                        txtAppNote.Visible = true;
                                        LoadClaimApp(cls.cToString(Session["username"]));
                                        LoadCategory("ALL");
                                        LoadMarKet();
                                        hdStatus.Value = "2";
                                        break;
                                    case 22://Print
                                        LoadNguoiDi();
                                        hdStatus.Value = "4";
                                        lbTitle.Text = "In Phiếu Đề Nghị Tạm Ứng<br /> Print Out Advance Request";
                                        dropApp.Visible = false;
                                        dropSaved.Visible = true;
                                        lbStatus.Visible = false;
                                        lbStatusTitle.Visible = false;
                                        lbReason.Visible = false;
                                        txtAppNote.Visible = false;
                                        // radioClaim.Visible = false;
                                        LoadCategory("ALL");
                                        LoadMarKet();
                                        LoadClaimPrint();
                                        GetOldClaim(dropSaved.SelectedValue);
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
            }
        }
        private void LoadCategory(string costcenter)
        {
            DataTable tbl = cls.GetDataTable("sp_LoadChargesTravel", "@costcenter", costcenter);
            ddlCategory.DataSource = tbl;
            ddlCategory.DataBind();
        }
        private void LoadNguoiDi()
        {
         DataTable tbl=cls.GetDataTable("sp_getAllUserFunction","@User",Session["username"]);
         dropNguoiDi.DataSource = tbl;
         dropNguoiDi.DataBind();
            
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
            if (cls.cToString(Session["costcenter"]) == "")
            {
                MsgBox1.AddMessage("Please update your costcenter in default page", uc.ucMsgBox.enmMessageType.Error);
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                string us = Session["username"].ToString();
                DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
                var kq = dbs.sp_GetUserInfo(us).ToList();

                txtName.Text = kq[0].Fullname;
                txtPosition.Text = kq[0].Position;

                txtAppName.Text = kq[0].FullNameRec;
                txtAppEmail.Text = kq[0].EmailRec;
                txtMyEmail.Text = kq[0].Email;
                dropMarket.SelectedValue = kq[0].Market;
                raddateNow.SelectedDate = DateTime.Now;
                lbStatus.Text = kq[0].StatusText;
                hdStatus.Value = kq[0].Status.ToString();
                txtAppNote.Visible = false;
                txtTelContact.Text = kq[0].TelPhone;
                txtDPNo.Text = "";
                //   RadGrid1.MasterTableView.DataSource = null;
                //  RadGrid1.MasterTableView.DataBind();
                ////    getClaimDetail("0");
                comboDepartment1.Values = kq[0].Costcenter;
                chkOto.Checked = false;
                chkTauHoa.Checked = false;
                chkMayBay.Checked = false;
                chkDatPhong.Checked = false;
                chkVeTauMayBay.Checked = false;
                chkOther.Checked = false;
                txtOther.Text = "";
                //   RadGrid1.DataSource = null;
                //  RadGrid1.DataBind();
                dbs.Dispose();
                Session["NewDocNo"] = GenalCode();
            }
        }
        private void GetOldClaim(string code)//0 cho phep them; 1: ko cho phep them
        {
            if (code != "")
            {
                // string us = Session["username"].ToString();
                DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();

                var kq = dbs.sp_GetUserInfo_TravelExists(code).ToList();
                comboDepartment1.FLoad();
                //comboDepartment1.Text=kq[0].
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
                //radnumAdvncedAmount.Value = (double)kq[0].DaTamUng;
                //  radnumDays.Value =(double) kq[0].NoDays;
                lbStatus.Text = kq[0].StatusText;
                hdStatus.Value = kq[0].Status.ToString();
                txtAppNote.Visible = true;
                txtAppNote.Text = kq[0].NoteApprover;
                txtTelContact.Text = kq[0].TelContact;
                txtNoiDen.Text = kq[0].Destination;
                txtLoTrinh.Text = kq[0].Itenerary;
                chkOto.Checked = cls.cToBool(kq[0].ByCar);
                chkTauHoa.Checked = cls.cToBool(kq[0].ByTrain);
                chkMayBay.Checked = cls.cToBool(kq[0].ByPlane);
                // chkXeSanBay.Checked = cls.cToBool(kq[0].CarAriPort);
                // chkXeCongTac.Checked = cls.cToBool(kq[0].CarTravel);
                chkDatPhong.Checked = cls.cToBool(kq[0].BookHotel);
                chkVeTauMayBay.Checked = cls.cToBool(kq[0].BookTicket);
                chkOther.Checked = cls.cToBool(kq[0].Other);
                txtOther.Text = kq[0].DetailOther;
                txtDPNo.Text = kq[0].DPNo;
                dbs.Dispose();
                Session["NewDocNo"] = code;
            }
        }
        private void setButton(int status)
        {
            switch (status)
            {
                case 0: //new
                    btSave.Visible = true;
                    btSubmit.Visible = true;
                    btDelete.Visible = true;
                    btPrint.Visible = false;
                    btPrintAdvance.Visible = false;
                    lbPD.Visible = false;
                    txtDPNo.Visible = false;
                    txtAppNote.Visible = false;
                    lbReason.Visible = false;
                    btApp.Visible = false;
                    btReject.Visible = false;
                     lbStatusTitle.Visible = true;
                    lbStatus.Visible = true;
                    btAdd.Visible = true;
                    break;
                case 1://approved
                    btSave.Visible = false;
                    btSubmit.Visible = false;
                    btDelete.Visible = false;
                    btPrint.Visible = true;
                    btPrintAdvance.Visible = true;
                     lbPD.Visible = true;
                    txtDPNo.Visible = true;
                    txtAppNote.Visible = false;
                    lbReason.Visible = false;
                    btAdd.Visible = false;
                    btApp.Visible = false;
                    btReject.Visible = false;
                     lbStatusTitle.Visible = true;
                    lbStatus.Visible = true;
                    break;
                case 2://submitted
                    btSave.Visible = false;
                    btSubmit.Visible = false;
                    btDelete.Visible = false;
                    btPrint.Visible = false;
                    btPrintAdvance.Visible = false;
                     lbPD.Visible = false;
                    txtDPNo.Visible = false;
                    txtAppNote.Visible = false;
                    lbReason.Visible = false;
                    btApp.Visible = false;
                    btReject.Visible = false;
                     lbStatusTitle.Visible = false;
                    lbStatus.Visible = false;
                    btAdd.Visible = false;
                    break;
                case 3://rejected
                   btSave.Visible = true;
                    btSubmit.Visible = true;
                    btDelete.Visible = true;
                    btPrint.Visible = false;
                    btPrintAdvance.Visible = false;
                     lbPD.Visible = false;
                    txtDPNo.Visible = false;
                    txtAppNote.Visible = true;
                    lbReason.Visible = true;
                    txtAppNote.ReadOnly = true;
                    btApp.Visible = false;
                    btReject.Visible = false;
                    lbStatusTitle.Visible = false;
                    lbStatus.Visible = false;
                    btAdd.Visible = true;
                    break;
                case 4://Print
                    btSave.Visible = false;
                    btSubmit.Visible = false;
                    btDelete.Visible = false;
                    btPrint.Visible = true;
                    btPrintAdvance.Visible = true;
                    lbPD.Visible = true;
                    txtDPNo.Visible = true;
                    txtAppNote.Visible = true;
                    txtAppNote.ReadOnly = true;
                    lbReason.Visible = true;
                    btApp.Visible = false;
                    btReject.Visible = false;
                     lbStatusTitle.Visible = false;
                    lbStatus.Visible = false;
                    btAdd.Visible = false;
                    break;
                default:
                    btSave.Visible = true;
                    btSubmit.Visible = false;
                    btDelete.Visible = false;
                    btPrint.Visible = false;
                    btPrintAdvance.Visible = false;
                     lbPD.Visible = false;
                    txtDPNo.Visible = false;
                    txtAppNote.Visible = false;
                    btApp.Visible = false;
                    btReject.Visible = false;
                    lbReason.Visible = false;
                    btAdd.Visible = true;
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
           // DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            //var kq = dbs.sp_getClaimSaved(Session["username"].ToString(), "");//sp_getClaimSaved la store co 2 tham so dau vao
            DataTable kq = cls.GetDataTable("sp_getTravelSaved", new string[]{"@username","@Type"},new object[]{Session["username"],"OFFICE"});
            dropSaved.DataSource = kq;
            dropSaved.DataValueField = "Values";
            dropSaved.DataTextField = "Text";
            dropSaved.DataBind();
           // dbs.Dispose();
        }
        private void LoadClaimPrint()
        {

            DataTable kq = cls.GetDataTable("sp_getTravelPrint", new string[] { "@username","@Type" }, new object[] { Session["username"],"OFFICE" });// dbs.sp_getClaimSaved(Session["username"].ToString(), "");//sp_getClaimSaved la store co 2 tham so dau vao
            dropSaved.DataSource = kq;
            dropSaved.DataValueField = "Values";
            dropSaved.DataTextField = "Text";
            dropSaved.DataBind();

        }
        private void LoadClaimApp(string username)
        {
            // DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            //var kq = dbs.sp_getClaimApproved(Session["username"].ToString());//sp_getClaimSaved la store co 2 tham so dau vao
            DataTable kq = new DataTable();

            kq = cls.GetDataTable("sp_getApprovedALL",new string[]{ "@username","@Type"}, new object[]{ username,"TR"});

            dropApp.DataSource = kq;
            dropApp.DataValueField = "Values";
            dropApp.DataTextField = "Text";
            dropApp.DataBind();
            // dbs.Dispose();
        }
        private string GenalCode()
        {
            //dd-MM-yy-Automumber
            //     DBTableDataContext dbs = new DBTableDataContext();
         //   DBStoreDataContext dbs = new DBStoreDataContext();
         //   clsSys sys = new clsSys();sp_GenaCodeTravel
            string code = "TR-"+Session["username"].ToString() + "-" + raddateNow.SelectedDate.Value.ToString("dd-MM-yy") + "-";
         //   var kq = dbs.sp_GenaCode(code).ToList();
           
          //  dbs.Dispose();
            //DataTable kq=cls.GetDataTable("sp_GenaCodeTravel","@code",code);
            //int rows = 0;
            //if (kq.Rows.Count > 0)
            //{
            //    rows = cls.cToInt(kq.Rows[0][0]);
            //}
            //rows = rows + 1;
            //code = code + rows.ToString();

            //return code;


            string kq = cls.GetString0("sp_GenaCodeTravel", new string[] { "@code" }, new object[] { code });
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
            //RadGrid1.DataSource = null;
            //RadGrid1.DataBind();
            List<sp_getTravelDetailResult> tbl = (List<sp_getTravelDetailResult>)Session["TravelDetail"];
          
            RadGrid1.DataSource = tbl;
           // RadGrid1.MasterTableView.DataSource = tbl;
            RadGrid1.DataBind();
        }
        private bool SaveParent(string code)
        {
            try
            {
                using (var dbs = new DBTableDataContext())
                {
                    //ClaimExpense LA TABLE
                    var model = new RequestTravel { Code_PK = code, DateRec = raddateNow.SelectedDate.Value, Username = Session["username"].ToString(), Approver = txtAppName.Text, AppEmail = txtAppEmail.Text, Status = 0, FDate = raddateFrom.SelectedDate.Value, TDate = raddateTo.SelectedDate.Value, NoDays = TinhNgay(), Purpose = txtPurpose.Text, Market = dropMarket.SelectedValue, Department = comboDepartment1.Values, Position = txtPosition.Text, FullName = txtName.Text, NoteApprover = "", Email = txtMyEmail.Text, Costcenter = comboDepartment1.getCoscenter, Destination = txtNoiDen.Text, Itenerary = txtLoTrinh.Text, TelContact = txtTelContact.Text, ByCar = chkOto.Checked, ByTrain = chkTauHoa.Checked, ByPlane = chkMayBay.Checked, CarAriPort = false, CarTravel = false, BookTicket = chkVeTauMayBay.Checked, BookHotel = chkDatPhong.Checked,Other=chkOther.Checked,DetailOther=txtOther.Text.Trim(),Type="OFFICE" };
                    dbs.RequestTravels.InsertOnSubmit(model);
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

                    var model = db.RequestTravels.SingleOrDefault(o => o.Code_PK == code);
                    model.DateRec = raddateNow.SelectedDate.Value;
                    model.Username = Session["username"].ToString();
                    model.Approver = txtAppName.Text;
                    model.AppEmail = txtAppEmail.Text;
                    model.Status = 0;
                    model.FDate = raddateFrom.SelectedDate.Value;
                    model.TDate = raddateTo.SelectedDate.Value;
                    model.NoDays = TinhNgay();
                    model.Purpose = txtPurpose.Text;
                    model.Market = dropMarket.SelectedValue;
                    model.Department = comboDepartment1.Values;
                    model.Position = txtPosition.Text;
                    model.FullName = txtName.Text;
                    model.NoteApprover = "";
                    model.Email = txtMyEmail.Text;
                    model.Costcenter=comboDepartment1.getCoscenter;
                    model.Itenerary=txtLoTrinh.Text;
                    model.Destination = txtNoiDen.Text;
                    model.TelContact=txtTelContact.Text;
                    model.ByCar=chkOto.Checked;
                    model.ByTrain=chkTauHoa.Checked;
                    model.ByPlane=chkMayBay.Checked;
                    model.CarAriPort=false;
                    model.CarTravel=false;
                    model.BookTicket=chkVeTauMayBay.Checked;
                    model.BookHotel = chkDatPhong.Checked;
                    model.Other = chkOther.Checked;
                    model.DetailOther = txtOther.Text.Trim();

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
                db.RequestTravelDetails.DeleteAllOnSubmit(db.RequestTravelDetails.Where(o => o.Code_FK == code));
                db.SubmitChanges();
            }
            //insert
            SaveDetail(code);
        }
     
        private void SaveDetail(string code)
        {
            DBTableDataContext dbs = new DBTableDataContext();
            try
            {
                List<sp_getTravelDetailResult> tbl = (List<sp_getTravelDetailResult>)Session["TravelDetail"];
                foreach (sp_getTravelDetailResult item in tbl)
                {
                    if (item.Cong > 0)
                    {
                        var model = new RequestTravelDetail { Code_FK = code,Charges_FK = item.Charges_FK,SoTien=item.SoTien,SoLuong=item.SoLuong,DonViTinh=item.DonViTinh,Cong=item.Cong,IsTamUng=item.IsTamUng };
                        dbs.RequestTravelDetails.InsertOnSubmit(model);
                    }
                }
                // dbs.ClaimExpensesDetails.InsertAllOnSubmit(tbl);
                dbs.SubmitChanges();
                // LoadSum(code);
                lbMess.Text = "Saved successfully!";

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
                var model1 = dbs.RequestTravelDetails.SingleOrDefault(p => p.Code_FK == code);
                dbs.RequestTravelDetails.DeleteOnSubmit(model1);
                dbs.SubmitChanges();
                lbMess.Text = "Save Error";
            }
            dbs.Dispose();
        }
       
        protected void btAdd_Click(object sender, EventArgs e)
        {
            if (radNumSoTien.Value <= 0 || radNumSoluong.Value <= 0)
            {
                MsgBox1.AddMessage("Please fill in 'Amount'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            clsSys sys = new clsSys();
            List<sp_getTravelDetailResult> tbl = (List<sp_getTravelDetailResult>)Session["TravelDetail"];
            sp_getTravelDetailResult item = new sp_getTravelDetailResult();
            //them Code_FK
           // Session["AutoNumber"] = cls.cToInt(Session["AutoNumber"])+1;
            item.Code_FK = Session["NewDocNo"].ToString();
                    
            item.Charges_FK = ddlCategory.SelectedValue;
            item.Description = ddlCategory.SelectedItem.Text;

            item.SoTien =cls.cToDecimal(radNumSoTien.Value);
            item.SoLuong =cls.cToInt(radNumSoluong.Value);
            item.DonViTinh = "";
            item.Cong = item.SoTien * item.SoLuong;//cls.cToDecimal(radThanhTien.Value);
            item.IsTamUng = chkTamung.Checked;
        
            tbl.Add(item);
            Session["TravelDetail"] = tbl;
          
         
            FillTable();
           
            
        }
        private void getClaimDetail(string code)
        {
            DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            List<sp_getTravelDetailResult> kq = dbs.sp_getTravelDetail(code).OrderBy(o => o.ID).ToList();//sp_getClaimDetail LA STORE
            if (code == "0" || code == "")
            {
                kq.RemoveAt(0);
            }
            Session["TravelDetail"] = kq;
            Session["AutoNumber"] = kq.Count;
            dbs.Dispose();

        }
        protected void dropSaved_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Literal1.Text = "";
            lbMess.Text = "";
            if (dropSaved.SelectedValue == "0")
            {
                RadGrid2.Visible = false;
                GetNewClaim();
                getClaimDetail("0");
                FillTable();
                RadGrid2.DataSource = null;
                RadGrid2.DataBind();
            }
            else
            {
              
                GetOldClaim(dropSaved.SelectedValue);
                getClaimDetail(dropSaved.SelectedValue);
                        FillTable();
                        RadGrid2.Visible = true;
                        DataTable tblstatus = cls.GetDataTable("sp_getStatusDocno", new string[] { "@docno" }, new object[] { dropSaved.SelectedValue });
                        RadGrid2.DataSource = tblstatus;
                        RadGrid2.DataBind();
             
            }
            setButton(int.Parse(hdStatus.Value));
           
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
                btApp.Visible = true;
                btReject.Visible = true;
                txtAppNote.ReadOnly = false;
                txtAppNote.Visible = true;
                lbReason.Visible = true;
                btSave.Visible = false;
                btSubmit.Visible = false;
                GetOldClaim(dropApp.SelectedValue);
            
                FillTable();
              
            }
        }
       
        protected void btSave_Click(object sender, EventArgs e)
        {
            if (raddateFrom.IsEmpty)
            {
                MsgBox1.AddMessage("Please fill in 'From date:'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (raddateTo.IsEmpty)
            {
                MsgBox1.AddMessage("Please fill in 'To date:'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (raddateTo.SelectedDate.Value < raddateFrom.SelectedDate.Value)
            {
                MsgBox1.AddMessage("'To Date' value must be greater or equal 'From Date'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            // // double nodays = TinhNgaySales();
            //  int nodays = TinhNgay();
            if (txtPurpose.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Please fill in 'Purpose:'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }

            if (txtNoiDen.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Please fill in 'Destination:'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (txtLoTrinh.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Please fill in 'Itinerary:'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (chkOther.Checked && txtOther.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Please fill in 'Others'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (RadGrid1.Items.Count <= 0)
            {
                MsgBox1.AddMessage("Please add at least one category and click add button before saving", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (dropSaved.SelectedValue == "0")//tao moi
            {
                string code = Session["NewDocNo"].ToString();
                if (SaveParent(code) == true)
                {
                    SaveDetail(code);
               
                    btSubmit.Visible = true;
                }
                LoadClaim();
                dropSaved.SelectedValue = code;
                lbStatus.Text = "Saved";
            }
            else//update
            {
                if (UpdateParent(dropSaved.SelectedValue) == true)
                {
                    UpdateDetail(dropSaved.SelectedValue);
               
                    btSubmit.Visible = true;
                }
            }
           
        }
       
        //private void ChangeStatus(string code, int status, string note)
        //{
        //    using (var db = new DBTableDataContext())
        //    {
        //        var model = db.RequestTravels.SingleOrDefault(p => p.Code_PK == code);
        //        model.Status = status;
        //        model.NoteApprover = note;
        //        db.SubmitChanges();
        //    }
        //}
        /// <summary>
        /// levelapp=1: senior; 2: director; 3: VP; 4: COO
        /// </summary>
        /// <param name="code"></param>
        /// <param name="to"></param>
        /// <param name="cc"></param>
        /// <param name="status"></param>
        /// <param name="levelapp"></param>
        private void SenEmailSubmit(string code,string to, string appby)
        {
            clsSys sys = new clsSys();
           // Guid activationCode = Guid.NewGuid();
            string activationCode = cls.GetString("sp_getGuiCode", new string[] { "@docno", "@approval" }, new object[] { code, to });
           // string to = txtAppEmail.Text;
           // string cc = txtMyEmail.Text;
            string nguoidenghi = txtName.Text;
            string phongban = comboDepartment1.Text;
            string mucdich = txtPurpose.Text;
            string noiden = txtNoiDen.Text;
            string lotrinh = txtLoTrinh.Text;
            string thoigian = "Từ/From " + raddateFrom.SelectedDate.Value.ToString("dd-MMM-yy") + " Đến/To " + raddateTo.SelectedDate.Value.ToString("dd-MMM-yy");
            string phuongtien = chkOto.Checked ? " Oto / Car " : "";
            phuongtien=phuongtien+cls.cToString( chkTauHoa.Checked ? " Tàu hỏa / Train " : "");
            phuongtien = phuongtien + cls.cToString(chkMayBay.Checked ? " Máy bay / Flight " : "");
            string thuxep = chkVeTauMayBay.Checked ? " Mua vé máy bay / Returned air ticket; " : "";
            thuxep = thuxep + cls.cToString(chkDatPhong.Checked ? " Đặt khách sạn / Hotel booking; " : "");
            thuxep = thuxep + cls.cToString(chkOther.Checked ? txtOther.Text : "");
            string html = "";
            html = "<table><tr><td>Người đề nghị/Requester: <b>" + nguoidenghi + "</b> Phòng ban/Dept: " + phongban + "</td></tr>";
            html = html + "<tr><td>Nơi đến/Destination: <b>" + noiden + "</b></td></tr>";
            html = html + "<tr><td>Lộ trình/Itinerary: <b>" + lotrinh + "</b></td></tr>";
            html = html + "<tr><td>Mục đích công tác/Purpose of business trip: <b>" + mucdich + "</b></td></tr>";
            html = html + "<tr><td>Thời gian/Length of days: <b>" + thoigian + "</b></td></tr>";
          
            
            html = html + "<tr><td>Phương tiện/Transportation mean: <b>" + phuongtien + "</b></td></tr>";
            html = html + "<tr><td>Đề nghị hành chánh thu xếp/Request admin to arrange: <b>" + thuxep + "</b></td></tr>";
            html=html+"</table>";
            html = html + "<table  cellpadding=\"2\" cellspacing=\"0\" style=\"width: 100%; border: 1px solid black; border-collapse: collapse; font-size: 12px;\">";
            html = html + "<tr style=\"border: 1px solid black; border-collapse: collapse\"><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Nội dung chi phí</br>Content of Expenses </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Đơn giá</br>Unit price </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Số lượng</br>Quantity </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Tạm ứng</br>Advance</td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Thành tiền</br>Amount VNĐ</td></tr>";
            double tongtien = 0;
            double tamung = 0;
            foreach (GridDataItem item in RadGrid1.Items)
            {
                tongtien=tongtien+cls.cToDouble(item["Cong"].Text);
                
                Label lb = (Label)item.FindControl("Label1");
                if (lb.Text.ToLower() == "yes")
                {
                    tamung = tamung + cls.cToDouble(item["Cong"].Text);
                }
                html = html + "<tr><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["Description"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["SoTien"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["SoLuong"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + lb.Text + "</td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["Cong"].Text + "</td></tr>";
            }
            html = html + "<tr><td colspan=4 align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\">Tổng chi phí/Total Amount:</td><td  style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\"><b>" + cls.FormatNumber(tongtien) + "</b></td></tr>";
            html = html + "<tr><td colspan=4 align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\">Đề nghị tạm ứng/Advance request:</td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\"><b>" + cls.FormatNumber(tamung) + "</b></td></tr>";
            html = html + "</table>";
            string who="";
            if (appby != "")
            {
                who = "(has been approved by " + appby + ")";
            }
            String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
            //http://
            bool kq = sys.SendMailASP(to, "Approve Travel Request", "Dear  Sir/Madam,</br></br>Please approve Travel Request number " + code + who + ". <a href = '" + strUrl + "/TravelRequest.aspx?ActivationCode=" + activationCode + "&code=" + code + "'>Click here to approve.</a> Or <a href = '" + strUrl + "/TravelRequest.aspx?RejectedCode=" + activationCode + "&code=" + code + "'>Click here to Reject.</a></br>" + html + "</br>Best Regards,");
            if (kq == true)
            {
                btSave.Visible = false;
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
        private void SenEmailApproved(string code, string to, string cc, string appby)
        {
            clsSys sys = new clsSys();
            // Guid activationCode = Guid.NewGuid();
           // string activationCode = cls.GetString("sp_getGuiCode", new string[] { "@docno", "@approval" }, new object[] { code, to });
            // string to = txtAppEmail.Text;
            // string cc = txtMyEmail.Text;
            string nguoidenghi = txtName.Text;
            string phongban = comboDepartment1.Text;
            string mucdich = txtPurpose.Text;
            string noiden = txtNoiDen.Text;
            string lotrinh = txtLoTrinh.Text;
            string thoigian = "Từ/From " + raddateFrom.SelectedDate.Value.ToString("dd-MMM-yy") + " Đến/To " + raddateTo.SelectedDate.Value.ToString("dd-MMM-yy");
            string phuongtien = chkOto.Checked ? " Oto / Car " : "";
            phuongtien = phuongtien + cls.cToString(chkTauHoa.Checked ? " Tàu hỏa / Train " : "");
            phuongtien = phuongtien + cls.cToString(chkMayBay.Checked ? " Máy bay / Flight " : "");
            string thuxep = chkVeTauMayBay.Checked ? " Mua vé máy bay / Returned air ticket; " : "";
            thuxep = thuxep + cls.cToString(chkDatPhong.Checked ? " Đặt khách sạn / Hotel booking; " : "");
            thuxep = thuxep + cls.cToString(chkOther.Checked ? txtOther.Text : "");
            string html = "";
            html = "<table><tr><td>Người đề nghị/Requester: <b>" + nguoidenghi + "</b> Phòng ban/Dept: " + phongban + "</td></tr>";
            html = html + "<tr><td>Nơi đến/Destination: <b>" + noiden + "</b></td></tr>";
            html = html + "<tr><td>Lộ trình/Itinerary: <b>" + lotrinh + "</b></td></tr>";
            html = html + "<tr><td>Mục đích công tác/Purpose of business trip: <b>" + mucdich + "</b></td></tr>";
            html = html + "<tr><td>Thời gian/Length of days: <b>" + thoigian + "</b></td></tr>";
           
            
            html = html + "<tr><td>Phương tiện/Transportation mean: <b>" + phuongtien + "</b></td></tr>";
            html = html + "<tr><td>Đề nghị hành chánh thu xếp/Request admin to arrange: <b>" + thuxep + "</b></td></tr>";
            html = html + "</table>";
            html = html + "<table  cellpadding=\"2\" cellspacing=\"0\" style=\"width: 100%; border: 1px solid black; border-collapse: collapse; font-size: 12px;\">";
            html = html + "<tr style=\"border: 1px solid black; border-collapse: collapse\"><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Nội dung chi phí</br>Content of Expenses </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Đơn giá</br>Unit price </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Số lượng</br>Quantity </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Tạm ứng</br>Advance</td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Thành tiền</br>Amount VNĐ</td></tr>";
            double tongtien = 0;
            double tamung = 0;
            foreach (GridDataItem item in RadGrid1.Items)
            {
                tongtien = tongtien + cls.cToDouble(item["Cong"].Text);

                Label lb = (Label)item.FindControl("Label1");
                if (lb.Text.ToLower() == "yes")
                {
                    tamung = tamung + cls.cToDouble(item["Cong"].Text);
                }
                html = html + "<tr><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["Description"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["SoTien"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["SoLuong"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + lb.Text + "</td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["Cong"].Text + "</td></tr>";
            }
            html = html + "<tr><td colspan=4 align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\">Tổng chi phí/Total Amount:</td><td  style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\"><b>" + cls.FormatNumber(tongtien) + "</b></td></tr>";
            html = html + "<tr><td colspan=4 align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\">Đề nghị tạm ứng/Advance request:</td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\"><b>" + cls.FormatNumber(tamung) + "</b></td></tr>";
            html = html + "</table>";
            string who = "";
            if (appby != "")
            {
                who = "(has been approved by " + appby + ")";
            }
            bool kq = sys.SendMailASP(to,cc, "Travel Request has been approved", "Travel Request number " + code + who + ". </br>" + html);
            if (kq == true)
            {

                MsgBox1.AddMessage("Approved! Notification of the approval (" + code + ") will be sent to the requestor", uc.ucMsgBox.enmMessageType.Success);
            }
            else
            {
                
                MsgBox1.AddMessage("Document "+code+" has been approved but don't send email to the requestor, Please send email to requestor by your microsoft outlook", uc.ucMsgBox.enmMessageType.Info);
            }
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
                     //them danh sach nguoi Approved theo DOA
                     DataTable tbl = cls.GetDataTable("sp_getTotalRequets", new string[] { "@Code" }, new object[] { docno });
                     decimal sotien = cls.cToDecimal(tbl.Rows[0]["Amount"]);
                     string username = cls.cToString(tbl.Rows[0]["Username"]);
                     string n1 = txtAppEmail.Text;
                     string senior = cls.getSeniorManager(username);
                     string director = cls.getDirector(username);
                     string vp = cls.getVP(username);
                     string coo = cls.getCOO(username);
                     //if (n1.ToLower() != coo.ToLower() && cls.cToString(vp)=="")
                     //{
                     //    if (chkMayBay.Checked == true)
                     //    {
                     //        //Khong co VP ma se len thang COO duyet

                     //    }
                     //}
                     if (senior == "")
                     {
                         senior = txtAppEmail.Text;
                     }
                     //if (director == "")
                     //{
                     //    director = txtAppEmail.Text;
                     //}
                     Guid activationCode = Guid.NewGuid();
                     string type = "TR";
                     bool kqsmit = false;
                     //insert N+1 approve
                     kqsmit = cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, n1, 1, 0, activationCode, type }, "sp_insertApprove");
                     if (sotien <= 10000000)
                     {
                         activationCode = Guid.NewGuid();
                         cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, senior, 2, 0, activationCode, type }, "sp_insertApprove");
                         if (chkMayBay.Checked == true)
                         {
                             activationCode = Guid.NewGuid();
                             cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, vp, 3, 0, activationCode, type }, "sp_insertApprove");
                         }
                         goto sendemail;
                     }
                     if (sotien > 10000000 && sotien <= 50000000)
                     {
                         //N+1
                         //  activationCode = Guid.NewGuid();
                         cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, senior, 2, 0, activationCode, type }, "sp_insertApprove");

                         activationCode = Guid.NewGuid();
                         // string director = cls.getDirector(username);
                         cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, director, 3, 0, activationCode, type }, "sp_insertApprove");
                         if (chkMayBay.Checked == true)
                         {
                             activationCode = Guid.NewGuid();
                             cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, vp, 4, 0, activationCode, type }, "sp_insertApprove");
                         }
                         goto sendemail;
                     }
                     if (sotien > 50000000 && sotien <= 500000000)
                     {
                         //N+1
                         cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, senior, 2, 0, activationCode, type }, "sp_insertApprove");

                         activationCode = Guid.NewGuid();
                         //  string director = cls.getDirector(username);
                         cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, director, 3, 0, activationCode, type }, "sp_insertApprove");
                         activationCode = Guid.NewGuid();
                         //  string vp = cls.getVP(username);
                         cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, vp, 4, 0, activationCode, type }, "sp_insertApprove");
                         goto sendemail;
                     }
                     if (sotien > 500000000)
                     {
                         //N+1
                         //N+1
                         cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, senior, 2, 0, activationCode, type }, "sp_insertApprove");
                         activationCode = Guid.NewGuid();
                         //  string director = cls.getDirector(username);
                         cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, sotien, director, 3, 0, activationCode, type }, "sp_insertApprove");
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
                 }
            }
            catch { }

        }
        private void LoadMana()
        {
           DataTable tbl= cls.GetDataTable("sp_LoadManager");
           dropAppNext.DataSource = tbl;
           dropAppNext.DataBind();
        }
        protected void btApp_Click(object sender, EventArgs e)
        {
             clsSys sys = new clsSys();
            string docno = dropApp.SelectedValue;
            bool kq;
           kq= cls.bCapNhat(new string[] { "@docno", "@username", "@status", "@DateApp", "@Note" }, new object[] { docno, Session["username"], 1, DateTime.Now, txtAppNote.Text }, "sp_updateAppbyName");
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

                   string EmailCreate = cls.cToString(tbl.Rows[0]["EmailCreate"]);
                   string emailnextlevel = cls.cToString(tbl.Rows[0]["Approval"]);
                   SenEmailSubmit(docno, emailnextlevel, cls.cToString(Session["username"]));
                   //sent email cc ve cho nguoi tao biet da chuyen len next level
                   sys.SendMailASP(EmailCreate, "Travel request has been sent to next level", "Travel request " + docno + " has been approved by N+1 and submit to Next level (" + emailnextlevel + ")");
                   MsgBox1.AddMessage("Approved! Notification of the approval (" + docno + ") will be submitted to the next level and copy to the requestor", uc.ucMsgBox.enmMessageType.Success);
               }
               else
               {

                   string emailhanhchanh = cls.GetString("sp_getEmailHanhChanh", new string[] { "@Code" }, new object[] { docno });
                   string to = txtMyEmail.Text;
                   if (emailhanhchanh != "")
                   {
                       to = to + ";" + emailhanhchanh;
                   }
                   //de la nguoi duyet cuoi cung
                 //  kq = sys.SendMailASP(txtMyEmail.Text, cc, "Travel request has been approved", "Travel request  " + docno + " has been approved by " + cls.cToString(Session["username"]));

                   SenEmailApproved(docno, to, txtAppEmail.Text, cls.cToString(Session["username"]));
                  // MsgBox1.AddMessage("Approved! Notification of the approval (" + docno + ") will be sent to the requestor", uc.ucMsgBox.enmMessageType.Success);
               }

           }      
                
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
                bool kq = cls.bCapNhat(new string[] { "@docno", "@username", "@status", "@DateApp", "@Note" }, new object[] { docno, Session["username"], 3, DateTime.Now, txtAppNote.Text }, "sp_updateAppbyName");
                if (kq == true)
                {
                    LoadClaimApp(cls.cToString(Session["username"]));
                    clsSys sys = new clsSys();
                    //sys.SenEmailReject(docno, txtAppNote.Text, txtMyEmail.Text, txtAppEmail.Text);
                    sys.SendMailASP(txtMyEmail.Text, /*txtAppEmail.Text, */"Travel request has been rejected", "Travel request " + docno + " has been rejected with reason " + txtAppNote.Text);
                    txtAppNote.Focus();
                    btAdd.Visible = false;
                    btReject.Visible = false;
                    MsgBox1.AddMessage("Rejected! Notification of the rejection (" + docno + ") will be returned to the requestor", uc.ucMsgBox.enmMessageType.Success);
                }
            }
        }
       
        //protected void radioClaim_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadClaim();
            
        //}

        protected void btPrint_Click(object sender, EventArgs e)
        {
            //PrintHelper.PrintWebControl(pnlPrintClaimExpenses);
            //Printed();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            Session["docno"] = dropSaved.SelectedValue;
            sb.Append("window.open('PrintTravelRequest.aspx','PrintMe', 'height=900px,width=1024px,scrollbars=1');");
           ScriptManager.RegisterClientScriptBlock(this.RadAjaxManager1, this.RadAjaxManager1.GetType(), "NewClientScript", sb.ToString(), true);
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

        protected void RadGrid1_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

            int index = e.Item.ItemIndex;

            List<sp_getTravelDetailResult> tbl = (List<sp_getTravelDetailResult>)Session["TravelDetail"];

            tbl.RemoveAt(index);

            Session["TravelDetail"] = tbl;
            FillTable();
           
        }

        protected void btPrintAdvance_Click(object sender, EventArgs e)
        {
            if (txtDPNo.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Please fill in 'DownPayment No.' from SAP", uc.ucMsgBox.enmMessageType.Error);
                txtDPNo.Focus();
            }
            else
            {
                using (var db = new DBTableDataContext())
                {
                    var model = db.RequestTravels.SingleOrDefault(p => p.Code_PK == dropSaved.SelectedValue);
                    model.DPNo = txtDPNo.Text.Trim();
                    db.SubmitChanges();
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                Session["docno"] = dropSaved.SelectedValue;
                sb.Append("window.open('PrintAdvanceRequest.aspx','PrintMe', 'height=900px,width=1024px,scrollbars=1');");
                ScriptManager.RegisterClientScriptBlock(this.RadAjaxManager1, this.RadAjaxManager1.GetType(), "NewClientScript", sb.ToString(), true);
            }
        }
        protected void GetMySelection(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //Response.Write("My selection is: " + e.SelectedItem.Text);
            // MsgBox1.AddMessage("tttttttttttttttttttttt", uc.ucMsgBox.enmMessageType.Error);
            LoadCategory(comboDepartment1.Values);
        }
        protected void btDelete_Click(object sender, EventArgs e)
        {
            if (cls.bXoa(new string[] { "@docno" }, new object[] { dropSaved.SelectedValue }, "sp_deleteTravel") == true)
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