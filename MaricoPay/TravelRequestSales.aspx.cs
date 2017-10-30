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
   
    public partial class TravelRequestSales : System.Web.UI.Page
    {
        //them 2 bien nay de fix refresh tu don gui email lai
        private bool _refreshState;
        private bool _isRefresh;
       // private string usernamestatic = "";
        //them 2 ham nay de fix refresh tu don gui email lai
        protected override void LoadViewState(object savedState)
        {
            object[] AllStates = (object[])savedState;
            base.LoadViewState(AllStates[0]);
            _refreshState = cls.cToBool(cls.cToString0(AllStates[1]));
            _isRefresh = _refreshState == cls.cToBool(cls.cToString0(Session["__ISREFRESH"]));
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
      static  DataTable tblDelist;
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
              getYear();
              LoadTinhS();
              LoadHuyenS(cls.cToInt(droTinhS.SelectedValue));
              LoadTinhC();
              LoadHuyenC(cls.cToInt(droTinhC.SelectedValue));
              DataTable tbl = new DataTable();
              //xet xem co phai truy cap tu link email
               activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
              RejectedCode = !string.IsNullOrEmpty(Request.QueryString["RejectedCode"]) ? Request.QueryString["RejectedCode"] : Guid.Empty.ToString();

              if (activationCode != Guid.Empty.ToString())
              {
                  lbTitle.Text = "Duyệt Phiếu Đề Nghị Công Tác (Sales)<br/> Approve Business Travel Request (Sales)";
                  string code = !string.IsNullOrEmpty(Request.QueryString["code"]) ? Request.QueryString["code"] : "";
                  // LoadCategory("ALL");
                  LoadMarKet();
                  LoadNguoiDi();
                  tbl = cls.GetDataTable("sp_getByGuiCode", new string[] { "@code", "@appcode" }, new object[] { code, activationCode });
                  if (tbl.Rows.Count > 0)
                  {
                      string docno = cls.cToString0(tbl.Rows[0]["Code_PK"]);

                      string AppEmail = cls.get_UsernameFromEmail(cls.cToString(tbl.Rows[0]["AppEmail"]));
                      LoadClaimApp(AppEmail);
                      //  bool kq = cls.bCapNhat(new string[] { "@docno", "@Appcode", "@status", "@DateApp", "@Note" }, new object[] { docno, activationCode, 1, DateTime.Now, "Approved via Email" }, "sp_updateApp");
                      string kq = cls.GetString0("sp_updateApp", new string[] { "@docno", "@Appcode", "@status", "@DateApp", "@Note" }, new object[] { docno, activationCode, 1, DateTime.Now, "Approved via Email" });
                      dropApp.Enabled = false;
                      dropApp.SelectedValue = docno;
                      btApp.Visible = false;
                      btPrint.Visible = false;
                      //btPrintAdvance.Visible = false;
                      //lbPD.Visible = false;
                      //txtDPNo.Visible = false;
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
                              //  SenEmailSubmit(docno, emailnextlevel, "");
                              string activationCode1 = cls.GetString("sp_getGuiCode", new string[] { "@docno", "@approval" }, new object[] { docno, emailnextlevel });
                              String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                              String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                              if (cls.SenEmailSubmitWorkingPlan(docno, emailnextlevel, "", activationCode1, txtName.Text, comboDepartment1.Text, txtPurpose.Text, raddateFrom.SelectedDate.Value.ToString("dd-MMM-yyyy"), raddateTo.SelectedDate.Value.ToString("dd-MMM-yyyy"), RadGrid1, strUrl) == true)
                              {
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
                              sys.SendMailASP(EmailCreate, "Travel request has been sent to next level", "Travel request " + docno + " has been approved by N+1 and submit to Next level (" + emailnextlevel + ")");
                              MsgBox1.AddMessage("Approved! Notification of the approval (" + docno + ") will be submitted to the next level(" + emailnextlevel + ") and copy to the requester", uc.ucMsgBox.enmMessageType.Success);
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
                  else
                  {
                      //da bi thay doi approve code co the do submit lai
                      MsgBox1.AddMessage("This document has been resubmit, Please approve on other email", uc.ucMsgBox.enmMessageType.Error);
                  }
                  // return;

              }
              else
              {
                  if (RejectedCode != Guid.Empty.ToString())
                  {
                      lbTitle.Text = " Duyệt Phiếu Đề Nghị Công Tác (Sales)<br /> Approve Business Travel Request (Sales)";
                      string code = !string.IsNullOrEmpty(Request.QueryString["code"]) ? Request.QueryString["code"] : "";
                      //  LoadCategory("ALL");
                      LoadMarKet();
                      LoadNguoiDi();
                      tbl = cls.GetDataTable("sp_getByGuiCode", new string[] { "@code", "@appcode" }, new object[] { code, RejectedCode });
                      if (tbl.Rows.Count > 0)
                      {
                          string docno = cls.cToString0(tbl.Rows[0]["Code_PK"]);
                          string AppEmail = cls.get_UsernameFromEmail(cls.cToString(tbl.Rows[0]["AppEmail"]));
                          LoadClaimApp(AppEmail);
                          dropApp.Enabled = false;
                          dropApp.SelectedValue = docno;
                          btApp.Visible = false;
                          dropSaved.Visible = false;
                          btPrint.Visible = false;
                          //btPrintAdvance.Visible = false;
                          //lbPD.Visible = false;
                          //txtDPNo.Visible = false;
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
                          Session["username"] = RejectedCode;

                      }
                      else
                      {
                          //da bi thay doi approve code co the do submit lai
                          MsgBox1.AddMessage("This document has been resubmit, Please check an other email", uc.ucMsgBox.enmMessageType.Error);
                      }
                      // return;
                  }
                  else
                  {
                      // string sss=  Request.Form["us"];
                      if (Session["username"] != null /*&& Request.Form["us"] != null*/)
                      // if (Request.QueryString["us"] != null)//click vao avatar
                      {
                          // usernamestatic = cls.cToString(Session["username"]);
                          hdStatus.Value = "0";
                          //
                          if (Request.QueryString["type"] != null)//click vao avatar
                          {
                              int type = int.Parse(Request.QueryString["type"].ToString());
                              switch (type)
                              {
                                  case 0: //tao moi
                                      RadGrid1.EnableViewState = true;
                                      lbTitle.Text = " Phiếu Đề Nghị Công Tác (Sales)<br /> Business Travel Request (Sales)";
                                      //                                        dropApp.Visible = false;
                                      //dropSaved.Visible = true;
                                      // lbStatus.Visible = true;
                                      //lbStatusTitle.Visible = true;
                                      // lbReason.Visible = false;
                                      // txtAppNote.Visible = false;
                                      // btAdd.Visible = true;


                                      LoadNguoiDi();
                                      LoadClaim();


                                      GetNewClaim();
                                      getClaimDetail("0");
                                      FillTable();
                                      // getClaimDetail("");
                                      Session["AutoNumber"] = 0;
                                      //  LoadCategory("ALL");
                                      LoadMarKet();
                                      hdStatus.Value = "0";
                                      setButton(int.Parse(hdStatus.Value));
                                      btSubmit.Visible = false;
                                      btDelete.Visible = false;
                                      break;
                                  case 2://approved
                                      RadGrid1.EnableViewState = false;
                                      lbTitle.Text = " Duyệt Phiếu Đề Nghị Công Tác (Sales)<br /> Approve Business Travel Request (Sales)";
                                      dropApp.Visible = true;
                                      btAdd.Visible = false;
                                      dropSaved.Visible = false;
                                      lbStatus.Visible = false;
                                      lbStatusTitle.Visible = false;
                                      lbReason.Visible = true;
                                      txtAppNote.Visible = true;
                                      LoadNguoiDi();
                                      LoadClaimApp(cls.cToString(Session["username"]));
                                      // LoadCategory("ALL");
                                      LoadMarKet();
                                      hdStatus.Value = "2";

                                      setButton(int.Parse(hdStatus.Value));
                                      dropSaved.Visible = false;
                                      dropApp.Visible = true;
                                      lbReason.Visible = true;
                                      txtAppNote.Visible = true;
                                      btReject.Visible = true;
                                      btApp.Visible = true;
                                      break;
                                  case 4://Print
                                      RadGrid1.EnableViewState = false;
                                      LoadNguoiDi();
                                      hdStatus.Value = "4";
                                      lbTitle.Text = "In Phiếu Đề Nghị Công Tác (Sales)<br /> Print Out Business Travel Request (Sales)";
                                      dropApp.Visible = false;
                                      dropSaved.Visible = true;
                                      lbStatus.Visible = false;
                                      lbStatusTitle.Visible = false;
                                      lbReason.Visible = false;
                                      txtAppNote.Visible = false;
                                      btAdd.Visible = false;
                                      // radioClaim.Visible = false;
                                      ////  LoadCategory("ALL");
                                      LoadMarKet();
                                      LoadClaimPrint();
                                      GetOldClaim(dropSaved.SelectedValue);
                                      getClaimDetail(dropSaved.SelectedValue);
                                      FillTable();

                                      Session["AutoNumber"] = 0;
                                      // setButton(int.Parse(hdStatus.Value));

                                      break;
                                  case 20: //tao moi
                                      LoadNguoiDi();
                                      lbTitle.Text = " Phiếu Đề Nghị Tạm Ứng (Sales)<br /> Advance Request (Sales)";
                                      //   dropApp.Visible = false;
                                      //   dropSaved.Visible = true;
                                      //   lbStatus.Visible = true;
                                      //   lbStatusTitle.Visible = true;
                                      //   lbReason.Visible = false;
                                      //   txtAppNote.Visible = false;
                                      //   lbTo.Visible = false;
                                      //   raddateFrom.Visible = false;
                                      //   raddateTo.Visible = false;
                                      //   lbFrom.Visible = false;
                                      //  // lbDestination.Visible = false;
                                      ////   txtNoiDen.Visible = false;
                                      // //  lbItenerary.Visible = false;
                                      ////   txtLoTrinh.Visible = false;
                                      //   lbTransportation.Visible = false;
                                      //   chkOto.Visible = false;
                                      //   chkTauHoa.Visible = false;
                                      //   chkMayBay.Visible = false;
                                      //   lbRequest.Visible = false;
                                      //   chkVeTauMayBay.Visible = false;
                                      //   chkDatPhong.Visible = false;
                                      //   chkOther.Visible = false;
                                      //   txtOther.Visible = false;
                                      //   btAdd.Visible = true;

                                      LoadClaim();


                                      GetNewClaim();
                                      getClaimDetail("0");
                                      FillTable();
                                      // getClaimDetail("");
                                      Session["AutoNumber"] = 0;
                                      //    LoadCategory("ALL");
                                      LoadMarKet();
                                      setButton(int.Parse(hdStatus.Value));
                                      break;
                                  case 21://approved
                                      LoadNguoiDi();
                                      lbTitle.Text = " Duyệt Phiếu Đề Nghị Tạm Ứng (Sales)<br /> Approve Advance Request (Sales)";
                                      //dropApp.Visible = true;
                                      //dropSaved.Visible = false;
                                      //lbStatus.Visible = false;
                                      //lbStatusTitle.Visible = false;
                                      //lbReason.Visible = true;
                                      //txtAppNote.Visible = true;
                                      LoadClaimApp(cls.cToString(Session["username"]));
                                      // LoadCategory("ALL");
                                      LoadMarKet();
                                      hdStatus.Value = "2";
                                      setButton(int.Parse(hdStatus.Value));
                                      // dropSaved.Visible = false;
                                      break;
                                  case 22://Print
                                      LoadNguoiDi();
                                      hdStatus.Value = "4";
                                      lbTitle.Text = "In Phiếu Đề Nghị Tạm Ứng<br /> Print Out Advance Request";
                                      // dropApp.Visible = false;
                                      // dropSaved.Visible = true;
                                      // lbStatus.Visible = false;
                                      // lbStatusTitle.Visible = false;
                                      // lbReason.Visible = false;
                                      // txtAppNote.Visible = false;
                                      // // radioClaim.Visible = false;
                                      //// LoadCategory("ALL");
                                      LoadMarKet();
                                      LoadClaimPrint();
                                      GetOldClaim(dropSaved.SelectedValue);
                                      FillTable();
                                      setButton(int.Parse(hdStatus.Value));
                                      break;
                              }
                              // setButton(int.Parse(hdStatus.Value));
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
        //private void LoadCategory(string costcenter)
        //{
        //    DataTable tbl = cls.GetDataTable("sp_LoadChargesTravel", "@costcenter", costcenter);
        //    ddlCategory.DataSource = tbl;
        //    ddlCategory.DataBind();
        //}
        private void getYear()
        {
           DataTable tbly= cls.GetDataTable("sp_getYear");
           dropNam.DataSource = tbly;
           dropNam.DataBind();
        }
        private void LoadNguoiDi()
        {
         DataTable tbl=cls.GetDataTable("sp_getAllUserFunction","@User",Session["username"]);
         dropNguoiDi.DataSource = tbl;
         dropNguoiDi.DataBind();
            
        }
        private void LoadTinhS()
        {
            DataTable tbltinh;
            if (CacheHelper.Get("cTinhThanh") != null)
                 {
             tbltinh = (DataTable) CacheHelper.Get("cTinhThanh");
            }
            else {
                 tbltinh = cls.GetDataTable("sp_getTinh");
               
                CacheHelper.SetDays("cTinhThanh", tbltinh, 30);
            }
            droTinhS.DataSource = tbltinh;
            droTinhS.DataBind();
        }
        private void LoadHuyenS(int MaTP)
        {
            DataTable tblhuyen;
          
            if (CacheHelper.Get("cHuyen_" + MaTP) != null)
            {
                tblhuyen = (DataTable)CacheHelper.Get("cHuyen_" + MaTP);
            }
            else
            {
                tblhuyen = cls.GetDataTable("sp_getHuyen", "@MaTP", MaTP);

                CacheHelper.SetDays("cHuyen_"+MaTP, tblhuyen, 30);
            }
            droHuyenS.DataSource = tblhuyen;
            droHuyenS.DataBind();




        }
        private void LoadTinhC()
        {
            //DataTable tbltinh = cls.GetDataTable("sp_getTinh");
            //droTinhC.DataSource = tbltinh;
            //droTinhC.DataBind();

            DataTable tbltinh;
            if (CacheHelper.Get("cTinhThanh") != null)
            {
                tbltinh = (DataTable)CacheHelper.Get("cTinhThanh");
            }
            else
            {
                tbltinh = cls.GetDataTable("sp_getTinh");

                CacheHelper.SetDays("cTinhThanh", tbltinh, 30);
            }
            droTinhC.DataSource = tbltinh;
            droTinhC.DataBind();
        }
        private void LoadHuyenC(int MaTP)
        {
            //DataTable tblhuyen = cls.GetDataTable("sp_getHuyen", "@MaTP", MaTP);
            //droHuyenC.DataSource = tblhuyen;
            //droHuyenC.DataBind();

            DataTable tblhuyen;

            if (CacheHelper.Get("cHuyen_" + MaTP) != null)
            {
                tblhuyen = (DataTable)CacheHelper.Get("cHuyen_" + MaTP);
            }
            else
            {
                tblhuyen = cls.GetDataTable("sp_getHuyen", "@MaTP", MaTP);

                CacheHelper.SetDays("cHuyen_" + MaTP, tblhuyen, 30);
            }
            droHuyenC.DataSource = tblhuyen;
            droHuyenC.DataBind();

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
          
            txtName.Text = kq[0].Fullname;
            txtPosition.Text = kq[0].Position;
            
            txtAppName.Text = kq[0].FullNameRec;
            txtAppEmail.Text = kq[0].EmailRec;
            txtMyEmail.Text = kq[0].Email;
            dropMarket.SelectedValue = kq[0].Market;
            raddateNow.SelectedDate = DateTime.Now;
            lbStatus.Text = kq[0].StatusText;
           
         //   hdStatus.Value = kq[0].Status.ToString();
            txtAppNote.Visible = false;
            txtTelContact.Text = kq[0].TelPhone ;
         //   txtDPNo.Text = "";
            txtNote.Text = "";
         //   RadGrid1.MasterTableView.DataSource = null;
          //  RadGrid1.MasterTableView.DataBind();
        ////    getClaimDetail("0");
            comboDepartment1.Values = kq[0].Costcenter;
          //  chkOto.Checked = false;
          //  chkTauHoa.Checked = false;
          //  chkMayBay.Checked = false;
          //  chkDatPhong.Checked = false;
          //  chkVeTauMayBay.Checked = false;
         //   chkOther.Checked = false;
          //  txtOther.Text = "";
         //   RadGrid1.DataSource = null;
          //  RadGrid1.DataBind();
            dropThang.SelectedValue = cls.cToString(DateTime.Now.Month);
            dropNam.SelectedValue = cls.cToString(DateTime.Now.Year);
            raddateFrom.SelectedDate = new DateTime(cls.cToInt(dropNam.SelectedValue), cls.cToInt(dropThang.SelectedValue), 1);
           raddateTo.SelectedDate=new DateTime( cls.cToInt(dropNam.SelectedValue),cls.cToInt(dropThang.SelectedValue),cls.songaytrongthang(cls.cToInt(dropNam.SelectedValue),cls.cToInt(dropThang.SelectedValue)));
            dbs.Dispose();
             Session["NewDocNoTravelSales"] = GenalCode();
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
                dropThang.SelectedValue = cls.cToString(kq[0].Thang);
                dropNam.SelectedValue = cls.cToString(kq[0].Nam);
               // txtNoiDen.Text = kq[0].Destination;
              //  txtLoTrinh.Text = kq[0].Itenerary;
              //  chkOto.Checked = cls.cToBool(kq[0].ByCar);
              //  chkTauHoa.Checked = cls.cToBool(kq[0].ByTrain);
             //   chkMayBay.Checked = cls.cToBool(kq[0].ByPlane);
                // chkXeSanBay.Checked = cls.cToBool(kq[0].CarAriPort);
                // chkXeCongTac.Checked = cls.cToBool(kq[0].CarTravel);
             //   chkDatPhong.Checked = cls.cToBool(kq[0].BookHotel);
              //  chkVeTauMayBay.Checked = cls.cToBool(kq[0].BookTicket);
             //   chkOther.Checked = cls.cToBool(kq[0].Other);
             //   txtOther.Text = kq[0].DetailOther;
               // txtDPNo.Text = kq[0].DPNo;
                dbs.Dispose();
                 Session["NewDocNoTravelSales"] = code;
            }
        }
        private void setButton(int status)
        {
            switch (status)
            {
                case 0: //new hoac da save chua submit
                    btAdd.Text = "11.Add";
                    btSave.Visible = true;
                    btSubmit.Visible = true;
                    btDelete.Visible = true;
                    btPrint.Visible = false;
                    dropApp.Visible = false;
                    //btPrintAdvance.Visible = false;
                    //lbPD.Visible = false;
                    //txtDPNo.Visible = false;
                    txtAppNote.Visible = false;
                    lbReason.Visible = false;
                    btApp.Visible = false;
                    btReject.Visible = false;
                     lbStatusTitle.Visible = true;
                    lbStatus.Visible = true;
                    btAdd.Visible = true;
                    RadGrid2.Visible = false;
                    break;
                case 1://approved
                    btSave.Visible = true;
                    btSubmit.Visible = false;
                    btDelete.Visible = false;
                    btPrint.Visible = true;
                    //btPrintAdvance.Visible = true;
                    // lbPD.Visible = true;
                    //txtDPNo.Visible = true;
                    txtAppNote.Visible = false;
                    lbReason.Visible = false;
                    btAdd.Visible = true;
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
                    //btPrintAdvance.Visible = false;
                    // lbPD.Visible = false;
                    //txtDPNo.Visible = false;
                    txtAppNote.Visible = false;
                    lbReason.Visible = false;
                
                     lbStatusTitle.Visible = true;
                    lbStatus.Visible = true;
                    btAdd.Visible = false;
                    btReject.Visible = false;
                    btApp.Visible = false;
                    dropSaved.Visible = true;
                    dropApp.Visible = false;
                    break;
                case 3://rejected
                   btSave.Visible = true;
                    btSubmit.Visible = true;
                    btDelete.Visible = true;
                    btPrint.Visible = false;
                    //btPrintAdvance.Visible = false;
                    //lbPD.Visible = false;
                    //txtDPNo.Visible = false;
                    txtAppNote.Visible = false;
                    lbReason.Visible = false;
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
                    //btPrintAdvance.Visible = false;
                    //lbPD.Visible = false;
                    //txtDPNo.Visible = false;
                    txtAppNote.Visible = true;
                    txtAppNote.ReadOnly = true;
                    lbReason.Visible = true;
                    btApp.Visible = false;
                    btReject.Visible = false;
                     lbStatusTitle.Visible = false;
                    lbStatus.Visible = false;
                    btAdd.Visible = false;
                    break;
                case -1: //save lai
                    btSave.Visible = true;
                    btSubmit.Visible = true;
                    btDelete.Visible = true;
                    btPrint.Visible = false;
                    //btPrintAdvance.Visible = false;
                    //lbPD.Visible = false;
                    //txtDPNo.Visible = false;
                    txtAppNote.Visible = false;
                    lbReason.Visible = false;
                    btApp.Visible = false;
                    btReject.Visible = false;
                    lbStatusTitle.Visible = true;
                    lbStatus.Visible = true;
                    btAdd.Visible = true;
                    RadGrid2.Visible = false;
                    lbStatus.Text = "Saved";
                    lbMess.Visible = true;
                    lbMess.Text = "Saved successfully!";
                    break;
                default:
                    btSave.Visible = true;
                    btSubmit.Visible = false;
                    btDelete.Visible = false;
                    btPrint.Visible = false;
                    //btPrintAdvance.Visible = false;
                    // lbPD.Visible = false;
                    //txtDPNo.Visible = false;
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
                //if (ddlCategory.SelectedValue.ToLower() == "ho")
                //{
                //    kq = sys.cToInt((raddateTo.SelectedDate.Value - raddateFrom.SelectedDate.Value).TotalDays);
                //}
                //else
                //{
                    kq = sys.cToInt((raddateTo.SelectedDate.Value - raddateFrom.SelectedDate.Value).TotalDays + 1);
                //}

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
            DataTable kq = cls.GetDataTable("sp_getTravelSaved", new string[]{"@username","@Type"},new object[]{Session["username"],"SALES"});
            dropSaved.DataSource = kq;
            dropSaved.DataValueField = "Values";
            dropSaved.DataTextField = "Text";
            dropSaved.DataBind();
           // dbs.Dispose();
        }
        private void LoadClaimPrint()
        {

            DataTable kq = cls.GetDataTable("sp_getTravelPrint", new string[] { "@username", "@Type" }, new object[] { Session["username"], "SALES" });// dbs.sp_getClaimSaved(Session["username"].ToString(), "");//sp_getClaimSaved la store co 2 tham so dau vao
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

            kq = cls.GetDataTable("sp_getApprovedALL",new string[]{ "@username","@Type"}, new object[]{ username,"TRSALES"});

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
            List<sp_getTravelDetailSalesResult> tbl = (List<sp_getTravelDetailSalesResult>)Session["TravelDetailSales"];
          
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
                    var model = new RequestTravel { Code_PK = code, DateRec = raddateNow.SelectedDate.Value, Username = Session["username"].ToString(), Approver = txtAppName.Text, AppEmail = txtAppEmail.Text, Status = 0, FDate = raddateFrom.SelectedDate.Value, TDate = raddateTo.SelectedDate.Value, NoDays = TinhNgay(), Purpose = txtPurpose.Text, Market = dropMarket.SelectedValue, Department = comboDepartment1.Values, Position = txtPosition.Text, FullName = txtName.Text, NoteApprover = "", Email = txtMyEmail.Text, Costcenter = comboDepartment1.getCoscenter, Destination = "", Itenerary ="", TelContact = txtTelContact.Text, ByCar = false/*chkOto.Checked*/, ByTrain =false /*chkTauHoa.Checked*/, ByPlane =false /*chkMayBay.Checked*/, CarAriPort = false, CarTravel = false, BookTicket = false/*chkVeTauMayBay.Checked*/, BookHotel = false /*chkDatPhong.Checked*/,Other=false/*chkOther.Checked*/,DetailOther=""/*txtOther.Text.Trim()*/,Type="SALES",Thang=cls.cToInt(dropThang.SelectedValue),Nam=cls.cToInt(dropNam.SelectedValue) };
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
                    model.Itenerary="";
                    model.Destination = "";
                    model.TelContact=txtTelContact.Text;
                    model.ByCar = false;// chkOto.Checked;
                    model.ByTrain = false;// chkTauHoa.Checked;
                    model.ByPlane = false;// chkMayBay.Checked;
                    model.CarAriPort=false;
                    model.CarTravel=false;
                    model.BookTicket = false;// chkVeTauMayBay.Checked;
                    model.BookHotel = false;// chkDatPhong.Checked;
                    model.Other = false;// chkOther.Checked;
                    model.DetailOther = "";// txtOther.Text.Trim();
                    model.Thang = cls.cToInt(dropThang.SelectedValue);
                    model.Nam = cls.cToInt(dropNam.SelectedValue);
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
            ////delete
            //using (var db = new DBTableDataContext())
            //{
            //    db.RequestTravelDetailSales.DeleteAllOnSubmit(db.RequestTravelDetailSales.Where(o => o.Code_FK == code));
            //    db.SubmitChanges();

               
            //}
            ////insert
            //SaveDetail(code);

            for (int i = 0; i < tblDelist.Rows.Count; i++)
            {
                cls.bXoa(new string[] { "@Code_FK", "@ID" }, new object[] { tblDelist.Rows[i]["Docno"], tblDelist.Rows[i]["ID"] }, "sp_DeleteRequestTravelDetailSales");
            }
            DBTableDataContext db = new DBTableDataContext();
            List<sp_getTravelDetailSalesResult> tbl = (List<sp_getTravelDetailSalesResult>)Session["TravelDetailSales"];
            foreach (sp_getTravelDetailSalesResult item in tbl)
            {
                cls.bCapNhat(new string[] {"@Code_FK","@FDate","@TDate","@PurposeMorning","@PurposeAfter","@Note","@TinhSang","@HuyenSang","@TinhChieu","@HuyenChieu","@ID","@Thu" }
                    , new object[] {item.Code_FK,item.FDate,item.TDate,item.PurposeMorning,item.PurposeAfter,item.Note,item.TinhSang,item.HuyenSang,item.TinhChieu,item.HuyenChieu,item.ID,item.Thu }
                    , "sp_EditRequestTravelDetailSales");
               
            }
        }
     
        private void SaveDetail(string code)
        {
            DBTableDataContext dbs = new DBTableDataContext();
            try
            {
                List<sp_getTravelDetailSalesResult> tbl = (List<sp_getTravelDetailSalesResult>)Session["TravelDetailSales"];
                foreach (sp_getTravelDetailSalesResult item in tbl)
                {
                    //if (item.Cong > 0)
                    //{
                        var model = new RequestTravelDetailSale { Code_FK = code,FDate=item.FDate,TDate=item.FDate,PurposeMorning=item.PurposeMorning,PurposeAfter=item.PurposeAfter,DateRec=DateTime.Now,Note=item.Note,TinhSang=item.TinhSang,HuyenSang=item.HuyenSang,TinhChieu=item.TinhChieu,HuyenChieu=item.HuyenChieu,Thu=item.Thu };
                        dbs.RequestTravelDetailSales.InsertOnSubmit(model);
                   // }
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
                var model1 = dbs.RequestTravelDetailSales.SingleOrDefault(p => p.Code_FK == code);
                dbs.RequestTravelDetailSales.DeleteOnSubmit(model1);
                dbs.SubmitChanges();
                lbMess.Text = "Save Error";
            }
            dbs.Dispose();
        }
        protected void RadGrid1_EditCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = (GridEditableItem)e.Item;
            int index = editedItem.ItemIndex;
         
            bool iswk = cls.cToBool(editedItem["IsClaimWorkingPlan"].Text);
            if (iswk == true)
            {
                MsgBox1.AddMessage("Working plan này đã được tạo Claim nên không thể sửa", uc.ucMsgBox.enmMessageType.Error);
            }
            else
            {

               
                raddateNow0.SelectedDate = cls.cToDateTime(editedItem["FDate"].Text);
                txtMorning.Text = editedItem["PurposeMorning"].Text;
                droTinhS.SelectedValue = editedItem["TinhSang"].Text;
                LoadHuyenS(cls.cToInt(droTinhS.SelectedValue));
               
                
                droHuyenS.SelectedValue = editedItem["HuyenSang"].Text;
                txtAfternoon.Text = editedItem["PurposeAfter"].Text;
                droTinhC.SelectedValue = editedItem["TinhChieu"].Text;
                LoadHuyenC(cls.cToInt(droTinhC.SelectedValue));
                droHuyenC.SelectedValue = editedItem["HuyenChieu"].Text;
                txtNote.Text = editedItem["Note"].Text;

                btAdd.Text = "11.Update";

                Session["indexdit"] = index;

                RadGrid1.EditIndexes.Clear();
                FillTable();
            }
        }
        protected void btAdd_Click(object sender, EventArgs e)
        {
            //if (radNumSoTien.Value <= 0 || radNumSoluong.Value <= 0)
            //{
            //    MsgBox1.AddMessage("Please fill in 'Amount'", uc.ucMsgBox.enmMessageType.Error);
            //    return;
            //}
           // clsSys sys = new clsSys();
            
            List<sp_getTravelDetailSalesResult> tbl = (List<sp_getTravelDetailSalesResult>)Session["TravelDetailSales"];
            sp_getTravelDetailSalesResult item = new sp_getTravelDetailSalesResult();
            string error = "";
           
            int index = -1;
            if (Session["indexdit"] != null && cls.cToInt(Session["indexdit"]) >= 0)
            {
                index = cls.cToInt(Session["indexdit"]);
                DataRow dr = tblDelist.NewRow();
                dr["Docno"] = tbl[index].Code_FK;
                dr["ID"] = tbl[index].ID;
                tblDelist.Rows.Add(dr);
                tbl.RemoveAt(index);
                btAdd.Text = "11.Add";
                Session["indexdit"] = null;
            }
            if (index < 0)//trang thai them thi can kiem tra xem da co wk ngay do chua, neu co roi ko cho them
            {
                foreach (sp_getTravelDetailSalesResult item1 in tbl)
                {
                    if (cls.Date2sYyyyMmDd(item1.FDate,"") == cls.Date2sYyyyMmDd(raddateNow0.SelectedDate.Value,""))
                    {
                        error = "Đã có working plant cho ngày này rồi";
                        break;
                    }

                }
            }
            if (error == "")
            {

                item.Code_FK =  Session["NewDocNoTravelSales"].ToString();

                item.FDate = raddateNow0.SelectedDate.Value;
                item.TDate = item.FDate;

                item.PurposeMorning = txtMorning.Text.Trim();
                item.PurposeAfter = txtAfternoon.Text.Trim();
                item.TinhSang = cls.cToInt(droTinhS.SelectedValue);
                item.HuyenSang = cls.cToInt(droHuyenS.SelectedValue);
                item.TenTinhSang = droTinhS.SelectedItem.Text;
                item.TenHuyenSang = droHuyenS.SelectedItem.Text;

                item.TinhChieu = cls.cToInt(droTinhC.SelectedValue);
                item.HuyenChieu = cls.cToInt(droHuyenC.SelectedValue);
                item.TenTinhChieu = droTinhC.SelectedItem.Text;
                item.TenHuyenChieu = droHuyenC.SelectedItem.Text;
                item.Thu = cls.getThu(item.TDate);

                item.Note = txtNote.Text;
              //  tbl.Add(item);
                if (index >= 0)
                {
                    //dang sua
                    tbl.Insert(index, item);
                }
                else
                {
                    //dag them
                    tbl.Add(item);
                }
                var newList = tbl.OrderBy(m => m.FDate).ToList();
                Session["TravelDetailSales"] = newList;
                Session["ChuaLuuTravelSales"] = true;

                FillTable();
            }
            else
            {
                MsgBox1.AddMessage(error, uc.ucMsgBox.enmMessageType.Error);
            }
           // btSave.Enabled = true;
            
        }
        private void getClaimDetail(string code)
        {
            DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            List<sp_getTravelDetailSalesResult> kq = dbs.sp_getTravelDetailSales(code).OrderBy(o => o.FDate).ToList();//sp_getClaimDetail LA STORE
            if (code == "0" || code == "")
            {
                kq.RemoveAt(0);
            }
            Session["TravelDetailSales"] = kq;
            Session["AutoNumber"] = kq.Count;
            dbs.Dispose();
            tblDelist = new DataTable();
            System.Data.DataColumn dtc = new System.Data.DataColumn();
            dtc.DataType = Type.GetType("System.String");
            dtc.ColumnName = "Docno";
           tblDelist.Columns.Add(dtc);
           dtc = new System.Data.DataColumn();
           dtc.DataType = Type.GetType("System.String");
           dtc.ColumnName = "ID";
           tblDelist.Columns.Add(dtc);

        }
        protected void dropSaved_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Literal1.Text = "";
            lbMess.Text = "";
            if (dropSaved.SelectedValue == "0")
            {
                //   RadGrid2.Visible = false;
                GetNewClaim();
                getClaimDetail("0");
                FillTable();
                hdStatus.Value = "0";
                btAdd.Text = "11.Add";
                // RadGrid2.DataSource = null;
                // RadGrid2.DataBind();
                setButton(int.Parse(hdStatus.Value));
                btSubmit.Visible = false;
                btDelete.Visible = false;
            }
            else
            {

                GetOldClaim(dropSaved.SelectedValue);
                getClaimDetail(dropSaved.SelectedValue);
                Session["ChuaLuuTravelSales"] = false;
                FillTable();
                RadGrid2.Visible = true;
                DataTable tblstatus = cls.GetDataTable("sp_getStatusDocno", new string[] { "@docno" }, new object[] { dropSaved.SelectedValue });
                RadGrid2.DataSource = tblstatus;
                RadGrid2.DataBind();
                setButton(int.Parse(hdStatus.Value));
            }
           
           
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
            //if (usernamestatic.ToLower() != cls.cToString(Session["username"]).ToLower() && usernamestatic != "")
            //{
            //    MsgBox1.AddMessage("Bạn đã login bằng user khác nên không thể lưu chứng từ này " + usernamestatic.ToLower() + "=" + cls.cToString(Session["username"]).ToLower(), uc.ucMsgBox.enmMessageType.Error);
            //}
            //else
            //{
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


                //if (chkOther.Checked && txtOther.Text.Trim() == "")
                //{
                //    MsgBox1.AddMessage("Please fill in 'Others'", uc.ucMsgBox.enmMessageType.Error);
                //    return;
                //}
                if (RadGrid1.Items.Count <= 0)
                {
                    MsgBox1.AddMessage("Please add at least one category and click add button before saving", uc.ucMsgBox.enmMessageType.Error);
                    return;
                }
                if (dropSaved.SelectedValue == "0")//tao moi
                {
                    string code = Session["NewDocNoTravelSales"].ToString();
                    if (SaveParent(code) == true)
                    {
                        SaveDetail(code);
                        Session["ChuaLuuTravelSales"] = false;
                        btSubmit.Visible = true;

                    }
                    LoadClaim();
                    dropSaved.SelectedValue = code;
                    lbStatus.Text = "Saved";
                }
                else//update
                {
                    if (hdStatus.Value == "1")//da duoc approved roi jo sua lai
                    {
                        //xet tiep xem da submit claim chua
                        string issubmitclaim = cls.GetString0("IsTravelSubmitClaim_Sales", new string[] { "@travaldocno" }, new object[] { dropSaved.SelectedValue });
                        if (issubmitclaim == "0")
                        {
                            UscApproved.AddMessage("Working plan đã được duyệt, khi bạn save sẽ phải duyệt lại, bạn có chắc không?", uc.ucMsgBox.enmMessageType.Attention, true, true, "ms_answerApproved");

                        }
                        else
                        {
                            MsgBox1.AddMessage("Đề  nghị thanh toán của working plan này đã được trình ký (submited), nên không được phép điều chỉnh", uc.ucMsgBox.enmMessageType.Error);
                        }
                    }
                    else
                    {
                        if (hdStatus.Value == "2")//da duoc submit nhug chua approved roi jo sua lai
                        {
                            uscSubmit.AddMessage("Working plan đã được trình ký, khi bạn save sẽ phải trình ký lại, bạn có chắc không?", uc.ucMsgBox.enmMessageType.Attention, true, true, "ms_answerSubmit");

                        }
                        else//chua submit nen sua lai va luu binh thuong
                        {
                            if (UpdateParent(dropSaved.SelectedValue) == true)
                            {
                                // btSave.Enabled = false;
                                UpdateDetail(dropSaved.SelectedValue);
                                Session["ChuaLuuTravelSales"] = false;
                                hdStatus.Value = "-1";
                                getClaimDetail(dropSaved.SelectedValue);
                                FillTable();
                                setButton(int.Parse(hdStatus.Value));

                            }
                        }
                    }

                }
           // }
        }
        protected void ms_answerApproved(object sender, uc.ucMsgBox.MsgBoxEventArgs e)
        {
            if (e.Answer == 0 || e.Answer == uc.ucMsgBox.enmAnswer.OK)//click ok
            {
                if (cls.bXoa(new string[] { "@Docno" }, new object[] { dropSaved.SelectedValue }, "sp_DeleteApprove") == true)
                {
                    if (UpdateParent(dropSaved.SelectedValue) == true)
                    {
                        UpdateDetail(dropSaved.SelectedValue);
                       // Session["ChuaLuuTravelSales"] = false;
                       // hdStatus.Value = "-1";//save thanh cong va can submit lai
                       // setButton(int.Parse(hdStatus.Value));
                       //btSubmit.Visible = true;


                       GetOldClaim(dropSaved.SelectedValue);
                       getClaimDetail(dropSaved.SelectedValue);
                       Session["ChuaLuuTravelSales"] = false;
                       FillTable();
                       RadGrid2.Visible = true;
                       DataTable tblstatus = cls.GetDataTable("sp_getStatusDocno", new string[] { "@docno" }, new object[] { dropSaved.SelectedValue });
                       RadGrid2.DataSource = tblstatus;
                       RadGrid2.DataBind();
                       setButton(int.Parse(hdStatus.Value));
                    }
                    //GetOldClaim(dropSaved.SelectedValue);
                    //getClaimDetail(dropSaved.SelectedValue);
                    //FillTable();
                    //RadGrid2.Visible = true;
                    //DataTable tblstatus = cls.GetDataTable("sp_getStatusDocno", new string[] { "@docno" }, new object[] { dropSaved.SelectedValue });
                    //RadGrid2.DataSource = tblstatus;
                    //RadGrid2.DataBind();
                    //setButton(int.Parse(hdStatus.Value));
                }
            }
            //else
            //{
            //  //  UscApproved.AddMessage("Chọn CANCEL ", uc.ucMsgBox.enmMessageType.Error);
            //    dropSaved.SelectedIndex = 0;
            //}
        }
        protected void ms_answerSubmit(object sender, uc.ucMsgBox.MsgBoxEventArgs e)
        {
            if (e.Answer == 0 || e.Answer==uc.ucMsgBox.enmAnswer.OK)//click ok
            {
               // Session["answer"] = "1";
              //  uscSubmit.AddMessage("Chọn OK ", uc.ucMsgBox.enmMessageType.Success);
                //delete submit
                if (cls.bXoa(new string[] { "@Docno" }, new object[] { dropSaved.SelectedValue }, "sp_DeleteApprove") == true)
                {
                    string code = dropSaved.SelectedValue;
                    if (UpdateParent(dropSaved.SelectedValue) == true)
                    {
                        UpdateDetail(dropSaved.SelectedValue);
                       // Session["ChuaLuuTravelSales"] = false;
                       // hdStatus.Value = "-1";
                       // setButton(int.Parse(hdStatus.Value));
                       //btSubmit.Visible = true;

                       GetOldClaim(dropSaved.SelectedValue);
                       getClaimDetail(dropSaved.SelectedValue);
                       Session["ChuaLuuTravelSales"] = false;
                       FillTable();
                       RadGrid2.Visible = true;
                       DataTable tblstatus = cls.GetDataTable("sp_getStatusDocno", new string[] { "@docno" }, new object[] { dropSaved.SelectedValue });
                       RadGrid2.DataSource = tblstatus;
                       RadGrid2.DataBind();
                       setButton(int.Parse(hdStatus.Value));
                    }

                }
            }
            //else
            //{
            //    // uscSubmit.AddMessage("Chọn CANCEL ", uc.ucMsgBox.enmMessageType.Error);
            //  //  dropSaved.SelectedIndex = 0;
            //    Session["answer"] = "0";
            //}
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
           // string noiden = "";// txtNoiDen.Text;
           // string lotrinh = "";// txtLoTrinh.Text;
            string thoigian = "Từ/From " + raddateFrom.SelectedDate.Value.ToString("dd-MMM-yy") + " Đến/To " + raddateTo.SelectedDate.Value.ToString("dd-MMM-yy");
            //string phuongtien = chkOto.Checked ? " Oto / Car " : "";
            //phuongtien = phuongtien + cls.cToString(chkTauHoa.Checked ? " Tàu hỏa / Train " : "");
            //phuongtien = phuongtien + cls.cToString(chkMayBay.Checked ? " Máy bay / Flight " : "");
            //string thuxep = chkVeTauMayBay.Checked ? " Mua vé máy bay / Returned air ticket; " : "";
            //thuxep = thuxep + cls.cToString(chkDatPhong.Checked ? " Đặt khách sạn / Hotel booking; " : "");
            //thuxep = thuxep + cls.cToString(chkOther.Checked ? txtOther.Text : "");
            string html = "";
            html = "<table><tr><td>Người đề nghị/Requester: <b>" + nguoidenghi + "</b> Phòng ban/Dept: " + phongban + "</td></tr>";
         
            html = html + "<tr><td>Mục đích công tác/Purpose of business trip: <b>" + mucdich + "</b></td></tr>";
            html = html + "<tr><td>Thời gian/Length of days: <b>" + thoigian + "</b></td></tr>";
           
            
            //html = html + "<tr><td>Phương tiện/Transportation mean: <b>" + phuongtien + "</b></td></tr>";
            //html = html + "<tr><td>Đề nghị hành chánh thu xếp/Request admin to arrange: <b>" + thuxep + "</b></td></tr>";
            html = html + "</table>";
            html = html + "<table  cellpadding=\"2\" cellspacing=\"0\" style=\"width: 100%; border: 1px solid black; border-collapse: collapse; font-size: 12px;\">";
            html = html + "<tr style=\"border: 1px solid black; border-collapse: collapse\"><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">STT</br>No</td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Ngày</br>Date </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Sáng</br>Morning </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Chiều</br>Afternoon</td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Chú thích</br>Note</td></tr>";
            //double tongtien = 0;
            //double tamung = 0;
            foreach (GridDataItem item in RadGrid1.Items)
            {

                html = html + "<tr><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + cls.cToString0(item.ItemIndex + 1) + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["FDate"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["PurposeMorning"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["PurposeAfter"].Text + "</td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["Note"].Text + "</td></tr>";
            }
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
            //if (usernamestatic.ToLower() != cls.cToString(Session["username"]).ToLower() && usernamestatic != "")
            //{
            //    MsgBox1.AddMessage("Bạn đã login bằng user khác nên không thể submit chứng từ này " + usernamestatic.ToLower() + "=" + cls.cToString(Session["username"]).ToLower(), uc.ucMsgBox.enmMessageType.Error);
            //}
            //else
            //{
                if (!_isRefresh)
                {
                    if (cls.cToBool(Session["ChuaLuuTravelSales"]) == true)
                    {
                        MsgBox1.AddMessage("Before submit, Please save changes", uc.ucMsgBox.enmMessageType.Info);
                    }
                    else
                    {
                        try
                        {
                            string docno = Session["NewDocNoTravelSales"].ToString();
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
                                DataTable tbl = cls.GetDataTable("sp_getTotalRequetsSales", new string[] { "@Code" }, new object[] { docno });
                                //  decimal sotien = cls.cToDecimal(tbl.Rows[0]["Amount"]);
                                string username = cls.cToString(tbl.Rows[0]["Username"]);
                                DataTable tblN1N2 = cls.GetDataTable("sp_getN1_N2", "@username", username);
                                string senior = cls.getSeniorManager(username);
                                string vp = cls.getVP(username);
                                if (tblN1N2.Rows.Count > 0)
                                {
                                    string userN1 = cls.cToString(tblN1N2.Rows[0]["UserN1"]);
                                    string userN2 = cls.cToString(tblN1N2.Rows[0]["UserN2"]);
                                    string emailN1 = cls.cToString(tblN1N2.Rows[0]["EmailN1"]);
                                    string emailN2 = cls.cToString(tblN1N2.Rows[0]["EmailN2"]);
                                    string userN3 = cls.cToString(tblN1N2.Rows[0]["UserN3"]);
                                    string emailN3 = cls.cToString(tblN1N2.Rows[0]["EmailN3"]);
                                    string emailMTManager = cls.GetString("sp_getEmailMTManager");
                                    //  string emailFoodManager = cls.GetString("sp_getEmailFoodManager");
                                    //  string emailGTBMSounth = cls.GetString("sp_getEmailGTSounth");
                                    //  string emailGTBMNoth = cls.GetString("sp_getEmailGTNoth");

                                    Guid activationCode = Guid.NewGuid();
                                    string type = "TRSALES";
                                    bool kqsmit = false;
                                    if (senior.ToLower() == emailMTManager.ToLower())//MT ALL thi chi can N1 duyet
                                    {
                                        kqsmit = cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, 0, emailN1, 1, 0, activationCode, type }, "sp_insertApprove");
                                    }
                                    else
                                    {
                                        if (userN1.ToLower() != userN2.ToLower())
                                        {
                                            kqsmit = cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, 0, emailN1, 1, 0, activationCode, type }, "sp_insertApprove");
                                            if (emailN2.ToLower() != vp.ToLower())//neu N2 la VP thi chi can N1 approve
                                            {
                                                activationCode = Guid.NewGuid();
                                                cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, 0, emailN2, 2, 0, activationCode, type }, "sp_insertApprove");
                                            }
                                            if (userN3 != "" && emailN3.ToLower() != vp.ToLower())//neu N3 la VP thi ko can N3 approve
                                            {
                                                activationCode = Guid.NewGuid();
                                                cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, 0, emailN3, 3, 0, activationCode, type }, "sp_insertApprove");
                                            }

                                        }
                                        else
                                        {
                                            kqsmit = cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type" }, new object[] { docno, 0, emailN1, 1, 0, activationCode, type }, "sp_insertApprove");
                                        }
                                    }
                                    if (kqsmit == true)
                                    {
                                        //  SenEmailSubmit(docno, emailN1 /*txtAppEmail.Text*/, /*txtMyEmail.Text*/"");
                                        string activationCode2 = cls.GetString("sp_getGuiCode", new string[] { "@docno", "@approval" }, new object[] { docno, emailN1 });
                                        String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                                        String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                                        if (cls.SenEmailSubmitWorkingPlan(docno, emailN1, "", activationCode2, txtName.Text, comboDepartment1.Text, txtPurpose.Text, raddateFrom.SelectedDate.Value.ToString("dd-MMM-yyyy"), raddateTo.SelectedDate.Value.ToString("dd-MMM-yyyy"), RadGrid1, strUrl) == true)
                                        {
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
                                        hdStatus.Value = "2";//trang thai da gui di submit nhug chua duoc approve
                                        btSubmit.Visible = false;
                                        btDelete.Visible = false;
                                    }

                                }
                                else
                                {
                                    MsgBox1.AddMessage("Can't find N1, N2, Please contract Admin", uc.ucMsgBox.enmMessageType.Error);
                                }
                            }

                        }
                        catch { }
                    }
                }
           // }
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
                  // SenEmailSubmit(docno, emailnextlevel, cls.cToString(Session["username"]));
                   string activationCode = cls.GetString("sp_getGuiCode", new string[] { "@docno", "@approval" }, new object[] { docno, emailnextlevel });
                   String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                   String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                   if (cls.SenEmailSubmitWorkingPlan(docno, emailnextlevel, cls.cToString(Session["username"]), activationCode, txtName.Text, comboDepartment1.Text, txtPurpose.Text, raddateFrom.SelectedDate.Value.ToString("dd-MMM-yyyy"), raddateTo.SelectedDate.Value.ToString("dd-MMM-yyyy"), RadGrid1, strUrl) == true)
                   {
                       //sent email cc ve cho nguoi tao biet da chuyen len next level
                       sys.SendMailASP(EmailCreate, cls.cToString(Session["email"]), "Travel request has been sent to next level", "Travel request " + docno + " has been approved by N+1 and submit to Next level (" + emailnextlevel + ")");
                       lbStatus.Text = "Submitted";
                       lbMess.Text = "Submitted successfully!";
                      // MsgBox1.AddMessage("Submitted successfully!", uc.ucMsgBox.enmMessageType.Success);
                       MsgBox1.AddMessage("Approved! Notification of the approval (" + docno + ") will be submitted to the next level and copy to the requestor", uc.ucMsgBox.enmMessageType.Success);
                   }
                   else
                   {
                       cls.bXoa(new string[] { "@Docno", "@username" }, new object[] { docno, emailnextlevel }, "sp_DeleteApproveByEmail");
                       lbStatus.Text = "Saved";
                       lbMess.Text = "Failed to submit";
                       MsgBox1.AddMessage("Failed to send, Please try again!", uc.ucMsgBox.enmMessageType.Error);
                   }
                   
               }
               else
               {
                    CacheHelper.RemoveLikeKey("creportwk_");
                    // string emailhanhchanh = cls.GetString("sp_getEmailHanhChanh", new string[] { "@Code" }, new object[] { docno });
                    string to = txtMyEmail.Text;
                 //  if (emailhanhchanh != "")
                 //  {
                 //      to = to + ";" + emailhanhchanh;
                 //  }
                 //  //de la nguoi duyet cuoi cung
                 ////  kq = sys.SendMailASP(txtMyEmail.Text, cc, "Travel request has been approved", "Travel request  " + docno + " has been approved by " + cls.cToString(Session["username"]));

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
              //  if(Session["username"])
                // ChangeStatus(docno, 3, txtAppNote.Text);
                bool kq = cls.bCapNhat(new string[] { "@docno", "@username", "@Note" }, new object[] { docno, Session["username"], txtAppNote.Text }, "sp_updateAppbyNameRejected");
                if (kq == true)
                {
                    LoadClaimApp(cls.cToString(Session["username"]));
                    clsSys sys = new clsSys();
                    //sys.SenEmailReject(docno, txtAppNote.Text, txtMyEmail.Text, txtAppEmail.Text);
                    sys.SendMailASP(txtMyEmail.Text, /*txtAppEmail.Text, */"Travel request has been rejected", "Travel request " + docno + " has been rejected with reason " + txtAppNote.Text);
                    txtAppNote.Focus();
                    btAdd.Visible = false;
                    btReject.Visible = false;
                    if (cls.cToString(Session["username"]).Length > 30)//mo form reject tu email nen sau khi reject xong thi clear session
                    {
                        Session["username"] = null;
                    }
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
           // System.Threading.Thread.Sleep(3000);
           
            //PrintHelper.PrintWebControl(pnlPrintClaimExpenses);
            //Printed();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            Session["docno"] = dropSaved.SelectedValue;
            sb.Append("window.open('PrintTravelRequestSales.aspx','PrintMe', 'height=900px,width=1024px,scrollbars=1');");
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
     //  protected DataRow dr;

        protected void RadGrid1_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

            int index = e.Item.ItemIndex;

            List<sp_getTravelDetailSalesResult> tbl = (List<sp_getTravelDetailSalesResult>)Session["TravelDetailSales"];

            if (tbl[index].IsClaimWorkingPlan == true)
            {
                MsgBox1.AddMessage("Working plan này đã được tạo Claim nên không thể xóa", uc.ucMsgBox.enmMessageType.Error);
            }
            else
            {
                //if (tbl[index].ID > 0)
                //{
                //    cls.bXoa(new string[] { "@Code_FK", "@ID" }, new object[] { tbl[index].Code_FK, tbl[index].ID }, "sp_DeleteRequestTravelDetailSales");
                //}

                DataRow dr= tblDelist.NewRow();
               dr["Docno"] = tbl[index].Code_FK;
                dr["ID"] = tbl[index].ID;
                tblDelist.Rows.Add(dr);
                tbl.RemoveAt(index);
              
              
            }
            Session["TravelDetailSales"] = tbl;
            FillTable();
        }

        //protected void btPrintAdvance_Click(object sender, EventArgs e)
        //{
        //    if (txtDPNo.Text.Trim() == "")
        //    {
        //        MsgBox1.AddMessage("Please fill in 'DownPayment No.' from SAP", uc.ucMsgBox.enmMessageType.Error);
        //        txtDPNo.Focus();
        //    }
        //    else
        //    {
        //        using (var db = new DBTableDataContext())
        //        {
        //            var model = db.RequestTravels.SingleOrDefault(p => p.Code_PK == dropSaved.SelectedValue);
        //            model.DPNo = txtDPNo.Text.Trim();
        //            db.SubmitChanges();
        //        }

        //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //        Session["docno"] = dropSaved.SelectedValue;
        //        sb.Append("window.open('PrintAdvanceRequest.aspx','PrintMe', 'height=900px,width=1024px,scrollbars=1');");
        //        ScriptManager.RegisterClientScriptBlock(this.RadAjaxManager1, this.RadAjaxManager1.GetType(), "NewClientScript", sb.ToString(), true);
        //    }
        //}
        protected void GetMySelection(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //Response.Write("My selection is: " + e.SelectedItem.Text);
            // MsgBox1.AddMessage("tttttttttttttttttttttt", uc.ucMsgBox.enmMessageType.Error);
           // LoadCategory(comboDepartment1.Values);
        }
        protected void btDelete_Click(object sender, EventArgs e)
        {
            //if (usernamestatic.ToLower() != cls.cToString(Session["username"]).ToLower() && usernamestatic != "")
            //{
            //    MsgBox1.AddMessage("Bạn đã login bằng user khác nên không thể xóa chứng từ này " + usernamestatic.ToLower() + "=" + cls.cToString(Session["username"]).ToLower(), uc.ucMsgBox.enmMessageType.Error);
            //}
            //else
            //{
                if (cls.bXoa(new string[] { "@docno" }, new object[] { dropSaved.SelectedValue }, "sp_deleteTravel") == true)
                {
                    LoadClaim();
                    MsgBox1.AddMessage("Deleted successfully", uc.ucMsgBox.enmMessageType.Success);

                }
                else
                {
                    MsgBox1.AddMessage("Deleted fail", uc.ucMsgBox.enmMessageType.Error);
                }
           // }
        }

        protected void droTinhS_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadHuyenS(cls.cToInt(droTinhS.SelectedValue));
            droTinhC.SelectedValue = droTinhS.SelectedValue;
            LoadHuyenC(cls.cToInt(droTinhC.SelectedValue));
        }

        protected void droTinhC_SelectedIndexChanged(object sender, EventArgs e)
        {
            droTinhC.CausesValidation = true;
            LoadHuyenC(cls.cToInt(droTinhC.SelectedValue));
        }

        protected void droHuyenS_SelectedIndexChanged(object sender, EventArgs e)
        {
            droHuyenC.SelectedValue = droHuyenS.SelectedValue;
        }

        protected void RadGrid1_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            FillTable();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            droTinhC.CausesValidation = true;
            LoadHuyenC(cls.cToInt(droTinhC.SelectedValue));

        }

        protected void dropThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            raddateFrom.SelectedDate = new DateTime(cls.cToInt(dropNam.SelectedValue), cls.cToInt(dropThang.SelectedValue), 1);
            raddateTo.SelectedDate = new DateTime(cls.cToInt(dropNam.SelectedValue), cls.cToInt(dropThang.SelectedValue), cls.songaytrongthang(cls.cToInt(dropNam.SelectedValue), cls.cToInt(dropThang.SelectedValue)));
            //raddateNow0.MinDate = raddateFrom.SelectedDate.Value;
            //raddateNow0.MaxDate = raddateTo.SelectedDate.Value;
        }

        protected void dropNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            raddateFrom.SelectedDate = new DateTime(cls.cToInt(dropNam.SelectedValue), cls.cToInt(dropThang.SelectedValue), 1);
            raddateTo.SelectedDate = new DateTime(cls.cToInt(dropNam.SelectedValue), cls.cToInt(dropThang.SelectedValue), cls.songaytrongthang(cls.cToInt(dropNam.SelectedValue), cls.cToInt(dropThang.SelectedValue)));
            //raddateNow0.MinDate = raddateFrom.SelectedDate.Value;
            //raddateNow0.MaxDate = raddateTo.SelectedDate.Value;
        }
       
    }
}