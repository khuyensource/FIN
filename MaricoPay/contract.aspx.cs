using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MaricoPay.DB;
using System.Data;
using Telerik.Web.UI;
using System.Runtime.InteropServices;
using System.IO;
namespace MaricoPay
{
    public partial class contract : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
        bool formload = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.GetPostBackEventReference(this, string.Empty);
            if (this.IsPostBack)
            {
                string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];
                if (eventTarget == "ChildWindowPostBack")
                {
                    if (Session["vendorname"] != null)
                    {
                      //  Session["PurOrg"] = PurOrg;
                        //vendor1.PurOrg =cls.cToString(Session["PurOrg"]);
                       // vendor1.FLoad();
                        FLoadVendor(cls.cToString(Session["PurOrg"]));
                      //  FLoadVendor(
                       // vendor1.Values = cls.cToString(Session["vendorname"]);
                        radcmbVendor.SelectedValue = cls.cToString(Session["vendorname"]);
                    }
                    
                }
                if (cls.cToString(Session["username"]) == "")
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

            else
           // if (!IsPostBack)
            {
                if (cls.cToString(Session["username"]) != "" /*&& Request.Form["us"] != null*/)
                // if (Request.QueryString["us"] != null)//click vao avatar
                {
                    formload = true;
                    string type = !string.IsNullOrEmpty(Request.QueryString["type"]) ? Request.QueryString["type"] : "";
                   // hdType.Value = type;
                    FLoadContractType();
                    LoadLegal();
                    LoadFinance();
                    LoadOrg("DO");
                    LoadASPNo();
                   
                    if (type.ToUpper() == "0")
                    {
                        hdType.Value = type;
                        //this.UpdatePanel1.Triggers.RemoveAt(2);
                        lbTitle.Text = "Contract Create";
                        hdFlagg.Value = "new";
                        lbStatus.Text = "Create new";
                        txtAppnote.ReadOnly = false;
                        tblChange.Visible = false;
                        RadGrid1.MasterTableView.GetColumn("SentLegal").Visible = false;
                        RadGrid1.MasterTableView.GetColumn("ActionColumn").Visible = true;
                        RadGrid1.MasterTableView.GetColumn("AppnoteLegal_Finance").Visible = false;
                        newDo();
                    }
                    else
                    {
                        if (type.ToUpper() == "2")
                        {
                            //PostBackTrigger posttrg = new PostBackTrigger();
                            //posttrg.ControlID = "RadGrid1";// e.Item.FindControl("btRejectGrid").u.UniqueID;
                            //this.UpdatePanel1.Triggers.Add(posttrg);
                            //  txtContractNo.Visible = true;
                            hdType.Value = type;
                            lbTitle.Text = "Contract Review";
                            txtAppnote.ReadOnly = true;
                            dvParent.Visible = false;
                            btSubmit.Visible = false;
                            btSave.Visible = false;
                            btCreate.Visible = false;
                            btCancel.Visible = false;
                            tblChange.Visible = true;
                            btExpand1.Text = "+";
                            btExpand1.ToolTip = "Expand";
                            RadGrid1.MasterTableView.GetColumn("SentLegal").Visible = true;
                            RadGrid1.MasterTableView.GetColumn("AppnoteLegal_Finance").Visible = true;
                            RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
                            RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                            RadGrid1.MasterTableView.GetColumn("ActionColumn").Visible = false;
                            RadGrid2.Width = 800;
                        }
                        else //type=3 Archive
                        
                        {
                            hdType.Value = type;
                            lbTitle.Text = "Contract Archive";
                            hdFlagg.Value = "new";
                            lbStatus.Text = "Create new";
                            txtAppnote.ReadOnly = false;
                            tblChange.Visible = false;
                            RadGrid1.MasterTableView.GetColumn("SentLegal").Visible = false;
                            RadGrid1.MasterTableView.GetColumn("ActionColumn").Visible = false;
                            RadGrid1.MasterTableView.GetColumn("AppnoteLegal_Finance").Visible = false;
                            RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
                            RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                            dropLegal.Enabled = false;
                            dropFinance.Enabled = false;
                          
                            newDo();
                        }
                    }
                  //  hdType.Value = type;
                    LoadCV();
                    formload = false;
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
               
            }
        }
        //public bool isShowSubmit(object loaicv)
        //{
        //    bool kq = false;
        //    switch (loaicv.ToString())
        //    {
        //        case "-1":
        //            kq = true;
        //            break;
        //        case "3":
        //            kq = true;
        //            break;
        //        default:
        //            kq = false;
        //            break;
        //    }
        //    return kq;
        //}
        public bool isShowSubmit(object loaicv)
        {
            bool kq = false;
            switch (loaicv.ToString())
            {
                case "-1":
                    kq = true;
                    break;
                case "0":
                    kq = false;
                    break;
                case "1":
                    kq = false;
                    break;
                case "2":
                    kq = false;
                    break;
                case "3":
                    kq = true;
                    break;
                case "4":
                    kq = false;
                    break;

                default:
                    kq = false;
                    break;
            }
            return kq;
        }
        public bool isShowApprove_Reject(object loaicv)
        {
            bool kq = false;
            switch (loaicv.ToString())
            {
                case "0":
                    kq = true;
                    break;

            }
            return kq;
        }
        public bool isShowSubmitLegal(object loaicv)
        {
            bool kq = false;
            switch (loaicv.ToString())
            {
                case "1":
                    kq = true;
                    break;

            }
            return kq;
        }
        public bool isShowClosed(object status, object statususer)
        {
            bool kq = false;
            if (status.ToString() == "1" && statususer.ToString() != "10")
            {
                kq = true;
            }
            return kq;
        }
        public bool isShowfinal(object status,object isclose)
        {
            //khi nao de tu nguoi yeu cau final thi mo cai nay ra
            //bool kq = false;
            //if (status.ToString() == "1" && cls.cToBool(isclose) == false)
            //{
            //    //RadGrid1.MasterTableView.GetColumn("AppnoteLegal_Finance").Visible = true;
            //    kq = true;
            //}
            //return kq;
            return false;
        }
        public bool isShowPrintAdvice(object isclose)
        {
            bool kq = false;
            if (cls.cToBool(isclose) == true)
            {
                //RadGrid1.MasterTableView.GetColumn("AppnoteLegal_Finance").Visible = true;
                kq = true;
            }
            return kq;
        }
        public bool isApproved(object loaicv)
        {
            bool kq = false;
            switch (loaicv.ToString())
            {
                case "1":
                    kq = true;
                    break;

            }
            return kq;
        }
        public bool isRejected(object loaicv)
        {
            bool kq = false;
            switch (loaicv.ToString())
            {
                case "3":
                    kq = true;
                    break;

            }
            return kq;
        }
        private void LoadASPNo()
        {
           DataTable tbl= cls.GetDataTable("sp_GetASPNo");
           radcomboASP.DataSource = tbl;
           radcomboASP.DataBind();
        }
        public void FLoadVendor(string PurOrg)
        {
          
           // Cclass cls = new Cclass();
            //get purcharg org from
            DataTable tbl = cls.GetDataTable("sp_getvedors", "@Purorg", PurOrg);
            radcmbVendor.DataSource = tbl;
            radcmbVendor.DataBind();
        }
        public void FLoadContractType()
        {
            using (var db = new DBTableDataContext())
            {
                // var model = db.Charges.OrderBy(t => t.No).SingleOrDefault(t1 => t1.Active == true);
                var model = db.ContractTypes.Where(o => o.Active == true).ToList();//.Select(o => o.Active == true).ToList();
                dropContractType.DataSource = model;
                dropContractType.DataBind();
                //  RG.DataSource = model;
                //RG.DataBind();
            }
        }
        private void LoadLegal()
        {
            DataTable tbl = cls.GetDataTable("sp_getEmailAllLegalContract");
           dropLegal.DataSource = tbl;
           dropLegal.DataBind();
           ddlLegalChange.DataSource = tbl;
           ddlLegalChange.DataBind();
        }
        private void LoadFinance()
        {
            DataTable tbl = cls.GetDataTable("sp_getEmailAllFinanceContract");
            dropFinance.DataSource = tbl;
            dropFinance.DataBind();
            ddlFinanceChange.DataSource = tbl;
            ddlFinanceChange.DataBind();
        }
        private void LoadOrg(string type)
        {
            DataTable tbl = new DataTable();
            tbl = cls.GetDataTable("sp_getOrg", "@type", type);
            dropOrg.DataSource = tbl;
            dropOrg.DataBind();
        }
        protected void btCreate_Click(object sender, EventArgs e)
        {
            newDo();
            
        }
        private void newDo()
        {
            hdFlagg.Value = "new";
            lbStatus.Text = "Create new";
            txtContractNo.Text = "";
            chkHopDongMau.Checked = false;
            radDateContract.SelectedDate = DateTime.Now;
          //  raddateExpiry.SelectedDate = DateTime.Now;
          //  radDateApp.SelectedDate = DateTime.Now;
            txtAppby.Text = "";
            txtContent.Text = "";
            radnumContractValue.Value = 0;
            radnumRenewal.Value = 0;
            radnumAmendment.Value = 0;
            chkDighitalsised.Checked = false;
            txtStorega.Text = "";
            txtAppnote.Text = "";
            hdFilename.Value = "";
            radcomboASP.SelectedValue = ""; 
          RadGrid2.Visible=false;
            btCancel.Visible = false;
            if (hdType.Value=="3")
            {
                btSaveArchive.Visible = true;
                btSave.Visible = false;
            }
            else
            {
                btSaveArchive.Visible = false;
                btSave.Visible = true;
            }
           // btSubmit.Visible = true;
            DataTable tblcost = cls.GetDataTable("sp_getOrgByCostcenter", "@costcenter", Session["costcenter"]);
            if (tblcost.Rows.Count > 0)
            {
                dropOrg.SelectedValue = cls.cToString0(tblcost.Rows[0]["Org_PK"]);
                
          // string PurOrg = cls.cToString(tblcost.Rows[0]["PurOrg"]);
           Session["PurOrg"] = dropOrg.SelectedValue;
               // vendor1.PurOrg = PurOrg;
               FLoadVendor(dropOrg.SelectedValue);
            }
            else
            {
                Session["PurOrg"] = "ALL";
            }
        }
        private string GenalCode(string departcode,string typecontract)
        {
            string code = radDateContract.SelectedDate.Value.Year.ToString() + "/" + departcode + "/" + typecontract+"/";
            DataTable tbl = cls.GetDataTable("sp_GenaCodeContract", new string[] { "@code" }, new object[] { code });
            int rows = 0;
            if (tbl.Rows.Count > 0)
            {
                rows = cls.cToInt(tbl.Rows[0][0]);
            }
            rows = rows + 1;
            code = code + rows.ToString();
            Session["NewDocNo"] = code;
            return code;
        }
        protected void btSave_Click(object sender, EventArgs e)
        {
            if (radcmbVendor.SelectedValue=="")
            {
                MsgBox1.AddMessage("Vendor has been not create!", uc.ucMsgBox.enmMessageType.Error);
            }
            else
            {
                if(txtContent.Text.Trim()=="")
                {
                    MsgBox1.AddMessage("Please fill in Brief content", uc.ucMsgBox.enmMessageType.Error);
                }
                else
                {
                Save(false);
                }
            }
        }
        protected void btSubmit_Click(object sender, EventArgs e)
        {
            if (radcmbVendor.SelectedValue == "")
            {
                MsgBox1.AddMessage("Vendor has been not create!", uc.ucMsgBox.enmMessageType.Error);
            }
            else
            {
                if (txtContent.Text.Trim() == "")
                {
                    MsgBox1.AddMessage("Please fill in Brief content", uc.ucMsgBox.enmMessageType.Error);
                }
                else
                {
                    Save(true);
                }
            }
        }
        private void Save(bool submit)
        {
           
            if (hdFlagg.Value == "edit")
            {
                string filename = "";
                if (FileUpload1.HasFile)
                {
                    filename = upload(FileUpload1,txtContractNo.Text);
                    if (filename == "")
                    {
                        MsgBox1.AddMessage("Can't save attach file (please use doc, docx, pdf, zip file only)", uc.ucMsgBox.enmMessageType.Error);
                        return;
                    }
                }

                bool binsert = cls.bThem(new string[] { "@ContractNo","@ContractDate","@Department","@ContractType"
           ,"@ContractContent","@Vendor","@ExpiryDate","@ContractValue","@Renewal","@RenewDate"
           ,"@Amendment","@AmendmentDate","@Dighitalsised","@HardcopyStored","@Approveby"
           ,"@ApproveDate","@CreateDate","@UserCreate","@attachedfile","@Appnote","@UnitPrice","@LegalReview","@FinanceReview","@ContractNoLegal","@CurrencyDescription","@ASPNo","@IsHDMau"  }
                         , new object[] { txtContractNo.Text, radDateContract.SelectedDate,dropOrg.SelectedValue,dropContractType.SelectedValue
                            , txtContent.Text.Trim(),radcmbVendor.SelectedValue,raddateExpiry.SelectedDate,radnumContractValue.Value,radnumRenewal.Value,null
                        ,radnumAmendment.Value,null,chkDighitalsised.Checked,txtStorega.Text,txtAppby.Text
                        ,radDateApp.SelectedDate,DateTime.Now,Session["username"],filename,txtAppnote.Text.Trim(),Curr1.CurrValues,dropLegal.SelectedValue,dropFinance.SelectedValue,txtConctNoFinal.Text,Curr1.CurrTextFull,radcomboASP.SelectedValue,chkHopDongMau.Checked}, "sp_InsertContract");
                if (binsert == true)
                {

                    if (submit == true)
                    {
                        string content = "Dear Sir/Madam,<br/>Please kindly find the attached & following note of the Contract<br/>- No." + txtContractNo.Text + "<br/>- Date " + radDateContract.SelectedDate.Value.ToString("dd-MMM-yy") + "<br/>- Brief content: " + txtContent.Text.Trim() + "<br/>-----------------------<br/>" +
     "Kính gởi Ông/Bà.<br/>Xin vui lòng xem xét hợp đồng đính kèm<br/> Số " + txtContractNo.Text + "<br/>- Ngày " + radDateContract.SelectedDate.Value.ToString("dd-MM-yy") + " <br/>- Nội dung chính: " + txtContent.Text.Trim();

                        SendEmailSubmit(txtContractNo.Text, dropLegal.SelectedValue + ";" + dropFinance.SelectedValue, filename == "" ? hdFilename.Value : filename, cls.cToString(Session["email"]), txtAppnote.Text.Trim(), content);
                    }
                    else
                    {
                        LoadCV();

                        MsgBox1.AddMessage("Saved successfully!", uc.ucMsgBox.enmMessageType.Success);
                    
                    }
                }
                else
                {
                    MsgBox1.AddMessage("Failed to save", uc.ucMsgBox.enmMessageType.Error);
                }

            }
            else
            {
                string kyhieu = cls.GetString("sp_getKyHieuOrg", new string[] { "@Org_FK" }, new object[] { dropOrg.SelectedValue });
                string docno= GenalCode(kyhieu, dropContractType.SelectedValue);
                string filename = "";
                if (!FileUpload1.HasFile)
                {
                    filename = "";
                    //MsgBox1.AddMessage("Must add attach file", uc.ucMsgBox.enmMessageType.Error);
                    //return;
                }
                else
                {
                    filename = upload(FileUpload1, docno);
                }
                if (filename != "")
                {
                    bool binsert = cls.bThem(new string[] { "@ContractNo","@ContractDate","@Department","@ContractType"
           ,"@ContractContent","@Vendor","@ExpiryDate","@ContractValue","@Renewal","@RenewDate"
           ,"@Amendment","@AmendmentDate","@Dighitalsised","@HardcopyStored","@Approveby"
           ,"@ApproveDate","@CreateDate","@UserCreate","@attachedfile","@Appnote","@UnitPrice","@LegalReview","@FinanceReview","@ContractNoLegal","@CurrencyDescription","@ASPNo","@IsHDMau" }
                        , new object[] { docno, radDateContract.SelectedDate,dropOrg.SelectedValue,dropContractType.SelectedValue
                            , txtContent.Text.Trim(),radcmbVendor.SelectedValue,raddateExpiry.SelectedDate,radnumContractValue.Value,radnumRenewal.Value,null
                        ,radnumAmendment.Value,null,chkDighitalsised.Checked,txtStorega.Text,txtAppby.Text
                        ,radDateApp.SelectedDate,DateTime.Now,Session["username"],filename,txtAppnote.Text.Trim(),Curr1.CurrValues,dropLegal.SelectedValue,dropFinance.SelectedValue,txtConctNoFinal.Text,Curr1.CurrTextFull,radcomboASP.SelectedValue,chkHopDongMau.Checked}, "sp_InsertContract");
                    if (binsert == true)
                    {
                        if (submit == true)
                        {
                             string content = "Dear Sir/Madam,<br/>Please kindly find the attached & following note of the Contract<br/>- No." + docno + "<br/>- Date " + radDateContract.SelectedDate.Value.ToString("dd-MMM-yy") + "<br/>- Brief content: " + txtContent.Text.Trim() + "<br/>-----------------------<br/>" +
     "Kính gởi Ông/Bà.<br/>Xin vui lòng xem xét hợp đồng đính kèm<br/> Số " + docno + "<br/>- Ngày " + radDateContract.SelectedDate.Value.ToString("dd-MM-yy") + " <br/>- Nội dung chính: " + txtContent.Text.Trim();

                            SendEmailSubmit(docno, dropLegal.SelectedValue + ";" + dropFinance.SelectedValue, filename, cls.cToString(Session["email"]), txtAppnote.Text.Trim(), content);
                        }
                        else
                        {
                            LoadCV();
                            txtContractNo.Text = docno;
                            hdFlagg.Value = "edit";
                            lbStatus.Text = "Edit";
                            hdFilename.Value = filename;
                            MsgBox1.AddMessage("Saved successfully!", uc.ucMsgBox.enmMessageType.Success);
                        }
                    }
                    else
                    {
                        MsgBox1.AddMessage("Failed to save", uc.ucMsgBox.enmMessageType.Error);
                    }
                }
                else
                {
                    MsgBox1.AddMessage("Can't save attach file (please use doc, docx, pdf, zip file only)", uc.ucMsgBox.enmMessageType.Error);
                    return;
                }
            }
        }
        private void SaveArchive()
        {

            if (hdFlagg.Value == "edit")
            {
                string filename = "";
                if (FileUpload1.HasFile)
                {
                    filename = upload(FileUpload1, txtContractNo.Text);
                }

                bool binsert = cls.bThem(new string[] { "@ContractNo","@ContractDate","@Department","@ContractType"
           ,"@ContractContent","@Vendor","@ExpiryDate","@ContractValue","@Renewal","@RenewDate"
           ,"@Amendment","@AmendmentDate","@Dighitalsised","@HardcopyStored","@Approveby"
           ,"@ApproveDate","@CreateDate","@UserCreate","@attachedfile","@Appnote","@UnitPrice","@LegalReview","@FinanceReview","@ContractNoLegal","@CurrencyDescription"  }
                         , new object[] { txtContractNo.Text, radDateContract.SelectedDate,dropOrg.SelectedValue,dropContractType.SelectedValue
                            , txtContent.Text.Trim(),radcmbVendor.SelectedValue,raddateExpiry.SelectedDate,radnumContractValue.Value,radnumRenewal.Value,null
                        ,radnumAmendment.Value,null,chkDighitalsised.Checked,txtStorega.Text,txtAppby.Text
                        ,radDateApp.SelectedDate,DateTime.Now,Session["username"],filename,txtAppnote.Text.Trim(),Curr1.CurrValues,dropLegal.SelectedValue,dropFinance.SelectedValue,txtConctNoFinal.Text,Curr1.CurrTextFull}, "sp_InsertContractArchive");
                if (binsert == true)
                {
                        LoadCV();
                        MsgBox1.AddMessage("Saved successfully!", uc.ucMsgBox.enmMessageType.Success);
                }
                else
                {
                    MsgBox1.AddMessage("Failed to save", uc.ucMsgBox.enmMessageType.Error);
                }

            }
            else
            {
                string kyhieu = cls.GetString("sp_getKyHieuOrg", new string[] { "@Org_FK" }, new object[] { dropOrg.SelectedValue });
                string docno = GenalCode(kyhieu, dropContractType.SelectedValue);
                string filename = "";
                if (!FileUpload1.HasFile)
                {
                    filename = "";
                    //MsgBox1.AddMessage("Must add attach file", uc.ucMsgBox.enmMessageType.Error);
                    //return;
                }
                else
                {
                    filename = upload(FileUpload1, docno);
                }
                if (filename != "")
                {
                    bool binsert = cls.bThem(new string[] { "@ContractNo","@ContractDate","@Department","@ContractType"
           ,"@ContractContent","@Vendor","@ExpiryDate","@ContractValue","@Renewal","@RenewDate"
           ,"@Amendment","@AmendmentDate","@Dighitalsised","@HardcopyStored","@Approveby"
           ,"@ApproveDate","@CreateDate","@UserCreate","@attachedfile","@Appnote","@UnitPrice","@LegalReview","@FinanceReview","@ContractNoLegal","@CurrencyDescription" }
                        , new object[] { docno, radDateContract.SelectedDate,dropOrg.SelectedValue,dropContractType.SelectedValue
                            , txtContent.Text.Trim(),radcmbVendor.SelectedValue,raddateExpiry.SelectedDate,radnumContractValue.Value,radnumRenewal.Value,null
                        ,radnumAmendment.Value,null,chkDighitalsised.Checked,txtStorega.Text,txtAppby.Text
                        ,radDateApp.SelectedDate,DateTime.Now,Session["username"],filename,txtAppnote.Text.Trim(),Curr1.CurrValues,dropLegal.SelectedValue,dropFinance.SelectedValue,txtConctNoFinal.Text,Curr1.CurrTextFull}, "sp_InsertContractArchive");
                    if (binsert == true)
                    {
                       
                            LoadCV();
                            txtContractNo.Text = docno;
                            hdFlagg.Value = "edit";
                            lbStatus.Text = "Edit";
                            hdFilename.Value = filename;
                            MsgBox1.AddMessage("Saved successfully!", uc.ucMsgBox.enmMessageType.Success);
                        
                    }
                    else
                    {
                        MsgBox1.AddMessage("Failed to save", uc.ucMsgBox.enmMessageType.Error);
                    }
                }
                else
                {
                    MsgBox1.AddMessage("Can't save attach file", uc.ucMsgBox.enmMessageType.Error);
                    return;
                }
            }
        }
        private string upload(FileUpload up, string docno)
        {
            string kq = "";
         // if(   RadUpload1.UploadedFiles.Count>0)
            if (up.HasFile)
            {
                try
                {

                    int vt1 = up.FileName.LastIndexOf(".");
                    int vtcanlay = vt1;
                    int len = up.FileName.Length;
                    string extention = up.FileName.Substring(vtcanlay, len - vtcanlay);
                    if (extention.ToLower().Contains("doc") == false && extention.ToLower().Contains("pdf") == false && extention.ToLower().Contains("zip") == false)
                    {
                        kq = "";
                    }
                    else
                    {
                        string filename = "";
                        filename = docno.Replace('/', '-');
                        filename = filename + '-' + cls.cToString(Session["username"]) + extention;
                        //HinhBia.SaveAs(Server.MapPath("../" + ConfigurationManager.AppSettings["hinhbia_sanpham"].ToString()).ToString() + ANHBIA);
                        string sFolderPath = Server.MapPath("Upload/CO/" + filename);
                        if (System.IO.File.Exists(sFolderPath) == true)
                            System.IO.File.Delete(sFolderPath);
                        //resize
                        //  HttpPostedFile pf = FileUpload1.PostedFile;
                        // up.SaveAs(sFolderPath);
                        // kq = filename;
                        try
                        {
                            up.SaveAs(sFolderPath);
                            kq = filename;
                        }
                        catch
                        {
                            kq = "";

                        }
                    }
                }
                catch
                {
                    kq = "";
                }
            }
            else
            {
                kq = "";
            }
            return kq;
        }
        struct ketqua
        {
            public bool bketqua;
            public string noidung;
            
        };
        private ketqua SendEmailSubmit(string docno, string emailapp, string filename, string cc, string ykien, string content)
        {
         //   bool kq = submit(docno, emailapp, "", "CO");
            ketqua kketqua;
            kketqua.bketqua = true;
            kketqua.noidung = "";
           // bool bkq = true;
            DataTable kq = submit(docno, emailapp, "", "CO");
            int rowupdate = 0;
            foreach (DataRow dr in kq.Rows)
            {
                if (cls.cToBool(dr["ketqua"]) == true)
                {
                    clsSys sys = new clsSys();
                    String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                    String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                    //   bool kq1 = sys.SendMailASPAtt(emailapp, cc,"Contract review", content + "<br/>Lưu ý/Note:" + ykien, Server.MapPath("~/Upload/CO/" + filename));
                    string urldownloadfile = @"" + strUrl + "/Upload/CO/" + filename;
                    bool kq1 = sys.SendMailASP(/*emailapp*/cls.cToString(dr["Email"]), cc, "Contract review", content + "<br/><b>Lưu ý/Note:</b>" + ykien + "<br/> <br/>Please <a href=" + urldownloadfile + "> click here</a> to download attached  file");
                    if (kq1 == true)
                    {
                        LoadCV();
                        rowupdate++;
                        kketqua.bketqua = true;
                        kketqua.noidung = "Sent to " + cls.cToString(dr["Email"]) + " for review successfully!";
                       
                       // MsgBox1.AddMessage("Sent to " + cls.cToString(dr["Email"]) + " for review successfully!", uc.ucMsgBox.enmMessageType.Success);
                    }
                    else
                    {
                        //cls.bXoa(new string[] { "@Docno", "@username" }, new object[] { docno, emailapp }, "sp_DeleteApproveByEmail");
                        LoadCV();
                       
                        kketqua.bketqua = true;
                        kketqua.noidung = "Failed to send email " + cls.cToString(dr["Email"]) + ", please try send email manual to creator";
                     //   MsgBox1.AddMessage("Failed to send email " + cls.cToString(dr["Email"]) + ", please try send email manual to creator", uc.ucMsgBox.enmMessageType.Error);
                      //  return;
                    }
                }
                //else
                //{
                //    rowupdate++;
                //}
            }
            if (rowupdate == 0)
            {
               
                kketqua.bketqua = false;
                kketqua.noidung = "Failed to submit, Please close FIN system and submit again";
                LoadCV();
               // MsgBox1.AddMessage("Failed to submit", uc.ucMsgBox.enmMessageType.Error);
            }
            return kketqua;
          
            
        }
        private DataTable submit(string docno, string emailapp, string ykien, string type)
        {
            string ch = ";";
            int vt = emailapp.IndexOf(",");
            if (vt >= 0)
                ch = ",";
           // bool kq1 = true;
            DataTable tbluslist = new DataTable();
            DataColumn dtc;
           
            dtc = new DataColumn();
            dtc.DataType = Type.GetType("System.String");
            dtc.ColumnName = "Email";
            tbluslist.Columns.Add(dtc);
           
            dtc = new DataColumn();
            dtc.DataType = Type.GetType("System.Boolean");
            dtc.ColumnName = "ketqua";
            tbluslist.Columns.Add(dtc);

            foreach (string address in emailapp.Split(new[] { ch }, StringSplitOptions.RemoveEmptyEntries))
            {
                Guid activationCode = Guid.NewGuid();
                bool kq = cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type", "@Note", "@Filename" }, new object[] { docno, 0, address, 1, 0, activationCode, type, ykien, "" }, "sp_insertApproveCon");
                //if (kq == false)
                //{
                //    kq1 = false;
                //}
                //else
                //{
                    DataRow dr;
                    dr = tbluslist.NewRow();
                    dr["Email"] = address;
                    dr["ketqua"] = kq;
                    tbluslist.Rows.Add(dr);

               // }
               
            }
            return tbluslist;
            //return kq1;
        }
        private bool approve(string docno, string ykien,string filenameupload/*,string filenameEmail,string toapp*/)
        {
            bool kq = cls.bCapNhat(new string[] { "@docno", "@username", "@Note", "@filename" }, new object[] { docno, Session["username"], ykien, filenameupload }, "sp_updateAppbyNameApproved");
            //if (kq == true) //kiem xem neu la finance approve thi gui den legal
            //{
            //    SendEmailSubmit(docno, toapp/*dropLegal.SelectedValue + ";" + dropFinance.SelectedValue*/, filenameEmail, cls.cToString(Session["email"]), ykien/*txtAppnote.Text.Trim()*/, "The contract has been review by finance, Next step, we want to review by Legal department");
            //}
            return kq;
        }
        private bool Reject(string docno, string ykien, string filename)
        {
            bool kq = cls.bCapNhat(new string[] { "@docno", "@username", "@Note", "@filename" }, new object[] { docno, Session["username"], ykien, filename }, "sp_updateAppbyNameRejected");
            return kq;
        }
        private void SendEmailApprove(string docno, string emailcreate, string filename, string cc, string ykien, string content)
        {

            clsSys sys = new clsSys();
            String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
            //bool kq1 = sys.SendMailASPAtt(emailcreate, cc, "Contract accept", content + "<br/>Lưu ý/Note: ngaynhan" + ykien, Server.MapPath("~/Upload/CO/" + filename));
            string urldownloadfile = @"" + strUrl + "/Upload/CO/" + filename;
            bool kq1 = sys.SendMailASP(emailcreate, cc, "Contract accept", content + "<br/>Lưu ý/Note:" + ykien + "<br/> <br/>Please <a href=" + urldownloadfile + "> click here</a> to download attached  file");
            if (kq1 == true)
            {
                MsgBox1.AddMessage("Accept successfully", uc.ucMsgBox.enmMessageType.Success);
            }
            else
            {
                //bool kq = cls.bCapNhat(new string[] { "@docno", "@username", "@status", "@DateApp", "@Note" }, new object[] { docno, Session["username"], 0, DateTime.Now, ykien }, "sp_updateAppbyName");
                MsgBox1.AddMessage("Failed to send email, please try send email manual to creator", uc.ucMsgBox.enmMessageType.Error);

            }

            LoadCV();
        }
        private void SendEmailReject(string docno, string emailcreate, string filename, string cc, string ykien, string content)
        {
            clsSys sys = new clsSys();
            String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
            //bool kq1 = sys.SendMailASPAtt(emailcreate, cc, "Contract rejected", content + "<br/>Lưu ý/note: " + ykien, Server.MapPath("~/Upload/CO/" + filename));
            string urldownloadfile = @"" + strUrl + "/Upload/CO/" + filename;
            bool kq1 = sys.SendMailASP(emailcreate, cc, "Contract rejected", content + "<br/>Lưu ý/Note:" + ykien + "<br/> <br/>Please <a href=" + urldownloadfile + "> click here</a> to download attached  file");
            if (kq1 == true)
            {
                MsgBox1.AddMessage("Rejected successfully", uc.ucMsgBox.enmMessageType.Success);
            }
            else
            {
                //bool kq = cls.bCapNhat(new string[] { "@docno", "@username", "@status", "@DateApp", "@Note" }, new object[] { docno, Session["username"], 0, DateTime.Now, ykien }, "sp_updateAppbyName");
                MsgBox1.AddMessage("Failed to send email, please try send email manual to creator", uc.ucMsgBox.enmMessageType.Error);
            }

            LoadCV();
        }
        private void LoadCV()
        {
            RadGrid1.EditIndexes.Clear();
            RadGrid1.DataSource = null;
            DataTable tbl = cls.GetDataTable("sp_LoadContract", new string[] { "@username", "@type" }, new object[] { Session["username"], hdType.Value });
            RadGrid1.DataSource = tbl;
            ViewState["CurrentTable"] = tbl;
            RadGrid1.DataBind();
           // PostBackTrigger posttrg = new PostBackTrigger();

           // posttrg.ControlID = this.RadGrid1.FindControl("FileUpload2").UniqueID;
           //// FileUpload up = RadGrid1.Items[index].FindControl("FileUpload2") as FileUpload;

           // this.UpdatePanel1.Triggers.Add(posttrg);

        }
        private void ReLoadCV()
        {
            RadGrid1.EditIndexes.Clear();
            RadGrid1.DataSource = null;
            if (ViewState["CurrentTable"] != null)
            {
                RadGrid1.DataSource = (DataTable)ViewState["CurrentTable"];
                RadGrid1.DataBind();
            }
            else
            {
                LoadCV();
            }
        }
        protected void RadGrid1_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = (GridEditableItem)e.Item;

            string status = editedItem["status"].Text;
            if (status == "2")
            {
                MsgBox1.AddMessage("You can't delete this contract because it has waiting to review", uc.ucMsgBox.enmMessageType.Error);
                ReLoadCV();
                return;
            }
            if (status == "1")
            {
                MsgBox1.AddMessage("You can't delete this Contract because it has been reviewed", uc.ucMsgBox.enmMessageType.Error);
                ReLoadCV();
                return;
            }
            if (status == "10")
            {
                MsgBox1.AddMessage("You can't delete this Contract because it has been closed", uc.ucMsgBox.enmMessageType.Error);
                ReLoadCV();
                return;
            }

            if (cls.bXoa(new string[] { "@Docno" }, new object[] { editedItem["ContractNo"].Text }, "sp_DeleteContract") == true)
            {
                LoadCV();
                MsgBox1.AddMessage("Deleted contract successfully!", uc.ucMsgBox.enmMessageType.Success);
            }

        }
        protected void RadGrid1_EditCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = (GridEditableItem)e.Item;

            string status = editedItem["status"].Text;
            if (status == "2")
            {
                MsgBox1.AddMessage("You can't edit this Contract because it has been waiting to review", uc.ucMsgBox.enmMessageType.Error);
                ReLoadCV();
                return;
            }
            if (status == "1")
            {
                MsgBox1.AddMessage("You can't edit this Contract because it has been accepted", uc.ucMsgBox.enmMessageType.Error);
                ReLoadCV();
                return;
            }
            if (status == "10")
            {
                MsgBox1.AddMessage("You can't edit this Contract because it has been closed", uc.ucMsgBox.enmMessageType.Error);
                ReLoadCV();
                return;
            }
           txtContractNo.Text = editedItem["ContractNo"].Text;
           
            btCancel.Visible = true;
            btCreate.Visible = false;
            btSave.Visible = true;
           // btSubmit.Visible = true;
            hdFlagg.Value = "edit";
            lbStatus.Text = "Edit";
            
            dropOrg.SelectedValue = editedItem["Department"].Text;
            try
            {
                dropLegal.SelectedValue = editedItem["LegalReview"].Text;
                ddlLegalChange.SelectedValue = editedItem["LegalReview"].Text;
                dropFinance.SelectedValue = editedItem["FinanceReview"].Text;
                ddlFinanceChange.SelectedValue = editedItem["FinanceReview"].Text;
            }
            catch { }
            // radDateCV.SelectedDate = RadGrid1.Items[index]["DocDate"].Text;
            DateTime? pkr = cls.cToDateTime(editedItem["ContractDate"].Text);
            radDateContract.SelectedDate = pkr;
            dropContractType.SelectedValue = editedItem["ContractType"].Text;

            FLoadVendor(dropOrg.SelectedValue);
          //  vendor1.Values = editedItem["Vendor"].Text;
            radcmbVendor.SelectedValue = editedItem["Vendor"].Text;
            pkr = cls.cToDateTime(editedItem["ExpiryDate"].Text);
            raddateExpiry.SelectedDate = pkr;
            pkr = cls.cToDateTime(editedItem["ApproveDate"].Text);
            radDateApp.SelectedDate = pkr;

            radnumContractValue.Value = cls.cToDouble(editedItem["ContractValue"].Text);
            Curr1.CurrValues=editedItem["UnitPrice"].Text;
          //  Curr1.CurrTextFull = editedItem["CurrencyDescription"].Text;// tbl[index].Currency;
            txtAppby.Text = editedItem["Approveby"].Text;
            hdFilename.Value = editedItem["Attachedfile"].Text;
            txtContent.Text = editedItem["ContractContent"].Text;

            txtStorega.Text = editedItem["HardcopyStored"].Text;
            chkDighitalsised.Checked = cls.cToBool(editedItem["Dighitalsised"].Text);
            radnumAmendment.Value = cls.cToDouble(editedItem["Amendment"].Text);
            radnumRenewal.Value = cls.cToDouble(editedItem["Renewal"].Text);
            //AppnoteLegal_Finance
            txtAppnote.Text = editedItem["Appnote"].Text; //((TextBox)editedItem["Appnote"].FindControl("txtYKienGrid")).Text;
             LoadNote(txtContractNo.Text);
             radcomboASP.SelectedValue = editedItem["ASPNoHide"].Text;
             chkHopDongMau.Checked = cls.cToBool(editedItem["IsHDMau"].Text);
            ReLoadCV();
        }
        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
           // RadGrid1.Rebind();
            GridEditableItem editedItem;
            string status = "";
            DateTime? pkr;
            string content = "";
            switch (e.CommandName)
            {
                case "Submit":
                    editedItem = (GridEditableItem)e.Item;
             status = editedItem["status"].Text;
                    //if (cls.cToString(editedItem["Attachedfile"].Text)=="")//chi nhưng thang chưa submit hoac bi rejected thi moi duoc trinh ky
                    //{
                        string docno = editedItem["ContractNo"].Text;
                        string emailLegal = editedItem["LegalReview"].Text;
                        string emailFinance = editedItem["FinanceReview"].Text;
                        string ykien = editedItem["Appnote"].Text;
                        pkr = cls.cToDateTime(editedItem["ContractDate"].Text);
                        content = "Dear Sir/Madam,<br/>Please kindly find the attached & following note of the Contract<br/>- No." + docno + "<br/>- Date " + pkr.Value.ToString("dd-MMM-yy") + "<br/>- Brief content: " + editedItem["ContractContent"].Text + "<br/>-----------------------<br/>" +
 "Kính gởi Ông/Bà.<br/>Xin vui lòng xem xét hợp đồng đính kèm<br/> Số " + docno + "<br/>- Ngày " + pkr.Value.ToString("dd-MM-yy") + " <br/>- Nội dung chính: " + editedItem["ContractContent"].Text;
                        ketqua kketqua=SendEmailSubmit(docno, emailLegal + ";" + emailFinance, editedItem["Attachedfile"].Text, cls.cToString(Session["email"]), ykien, content);
                        if (kketqua.bketqua == true)
                        {
                            newDo();
                            MsgBox1.AddMessage(kketqua.noidung, uc.ucMsgBox.enmMessageType.Success);
                        }
                        else
                        {
                            MsgBox1.AddMessage(kketqua.noidung, uc.ucMsgBox.enmMessageType.Error);
                        }
                    //}
                    //else
                    //{
                    //    MsgBox1.AddMessage("Contract file is not exists, Please check attached file", uc.ucMsgBox.enmMessageType.Error);
                    //    return;
                    //}

                    break;
         //       
                ////PrintAdviceNote
                case "PrintAdviceNote":
                    editedItem = (GridEditableItem)e.Item;
                   
                  //  string docno1 = editedItem["ContractNo"].Text;
                           System.Text.StringBuilder sb = new System.Text.StringBuilder();
                           Session["docno"] = editedItem["ContractNo"].Text;
            sb.Append("window.open('PrintContractAdviceNote.aspx','PrintMe', 'height=900px,width=1024px,scrollbars=1');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "NewClientScript", sb.ToString(), true);    
                    break;
 //               case "SentLegal":
 //                    editedItem = (GridEditableItem)e.Item;
 //            status = editedItem["status"].Text;
 //                   //GridEditableItem editedItem1 = (GridEditableItem)e.Item;
 //                   //string status1 = editedItem1["status"].Text;

 //                   //if (status == "1")//chi nhung cong van da duoc approved thi moi chuyen legal
 //                   //{
 //                       string docno2 = editedItem["ContractNo"].Text;
 //                       string ykien4 = editedItem["Appnote"].Text;
 //                       string emaillegal = cls.GetString("sp_getEmailLegal");
 //                       pkr = cls.cToDateTime(editedItem["ContractDate"].Text);
 //                       content = "Dear Legal,<br/>" +
 //"Please kindly help to review and comment from legal view on the contract<br/>- No." + docno2 + "<br/>- Date " + pkr.Value.ToString("dd-MMM-yy") + "<br/>- Brief content: " + editedItem["ContractContent"].Text +
 //"<br/>---------------------------<br/>" +
 //"Thân gởi Legal,<br/>" +
 //"Xin vui lòng xem xét và cho nhận xét về quan điểm pháp lý đối với hợp đồng<br/> Số " + docno2 + "<br/>- Ngày " + pkr.Value.ToString("dd-MM-yy") + "<br/>- Nội dung chính: " + editedItem["ContractContent"].Text + ".";
 //                       //update da sent legal
 //                       cls.bCapNhat(new string[] { "@Docno", "@username", "@status" }, new object[] { docno2, Session["username"], 4 }, "sp_updateStastusApp");
 //                       SendEmailSubmit(docno2, emaillegal, editedItem["Attachedfile"].Text, cls.cToString(Session["email"]), ykien4,  content);

 //                   //}
 //                   //else
 //                   //{
 //                   //    MsgBox1.AddMessage("You can't send this Contract to legal because it has not approve", uc.ucMsgBox.enmMessageType.Error);
 //                   //    return;
 //                   //}

 //                   break;
                case "Approve"://finalise
                     editedItem = (GridEditableItem)e.Item;
                    status = editedItem["status"].Text;
                    //if (status == "2")//chi nhung cong van nao dang trinh ky thi moi approve
                    //{
                        string docno4 = editedItem["ContractNo"].Text;
                        TextBox ykien1 = (TextBox)editedItem["AppnoteLegal_Finance"].FindControl("txtYKienGrid");
                        FileUpload up = (FileUpload)editedItem["AppnoteLegal_Finance"].FindControl("FileUpload2");
                        string emailFC = editedItem["FinanceReview"].Text;
                        string emailLG = editedItem["LegalReview"].Text;
                        string filenameup1 = editedItem["Attachedfile"].Text;
                        string fileatt = "";
                        bool isHDMau = cls.cToBool(editedItem["IsHDMau"].Text);
                        if (up.HasFile)//co upload file
                        {
                            filenameup1 = upload(up, docno4);
                            fileatt = filenameup1;// Server.MapPath("Upload/CO/" + filenameup1);
                        }
                        else
                        {
                            fileatt = editedItem["Attachedfile"].Text;
                        }

                        //if (approve(docno4, ykien1.Text, filenameup1/*, fileatt, emailFC + ";" + emailLG*/) == true)//vua approve va vua gui den legal
                        //{
                            pkr = cls.cToDateTime(editedItem["ContractDate"].Text);
                           // cls.bCapNhat(new string[] { "@docno", "@note" }, new object[] { docno4, ykien1.Text }, "sp_UpdateContractNote");
                            CContract ct = new CContract();
                            
                            #region Legal
                            if (emailLG.ToLower() == cls.cToString(Session["email"]).ToLower())
                            {
                                //TextBox finalnumber = (TextBox)editedItem["ContractNoLegal"].FindControl("txtContractNoFinal");
                                //cls.bCapNhat(new string[] { "@Docno", "@DocStorage", "@ClosedBy", "@filename", "@ContractNoLegal" }, new object[] { docno4, "", Session["username"], fileatt, finalnumber.Text.Trim() }, "sp_UpdateClosedContract");

                                //cho nay add chu ky legal
                                string extention = cls.getExtention(filenameup1);
                                #region Legal Doc
                                if (extention.ToLower().IndexOf("doc") >= 0)
                                {
                                    string orgfilepath = Server.MapPath("Upload/CO/" + filenameup1);

                                    string chuky = Server.MapPath("ImagesSignature/" + cls.cToString(Session["username"]) + ".png");
                                    if (System.IO.File.Exists(chuky) == false)
                                    {
                                        chuky = Server.MapPath("ImagesSignature/approved.png");
                                    }
                                    string filenameout = "L" + filenameup1;
                                    string outfilepath = Server.MapPath("Upload/CO/" + filenameout);
                                    string pdffilename = "LF" + docno4.Replace('/', '-') + ".pdf";

                                    string outpdffilepath = Server.MapPath("Upload/CO/" + pdffilename);
                                    String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                                    String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                                    string urldownloadfile = @"" + strUrl + "/Upload/CO/" + filenameup1;

                                    // string kq = ct.AddSinagureFooterWord(orgfilepath, chuky, outfilepath,45,0);
                                    ketquaSign kqs = ct.AddSinagureFooterWord1(orgfilepath, chuky, outfilepath, 45, 0);
                                    //if (kq.ToLower() == "ok")
                                    if (kqs.bketqua == true || kqs.noidung.IndexOf("File has been protected") >= 0)
                                    {
                                        //string kqcv = ct.Doc2PdfSave(outfilepath, outpdffilepath);
                                        string kqcv = "";
                                        if (kqs.bketqua == false)//file protected thi van chuyen sang pdf ko co chu ky
                                        {
                                            kqcv = ct.word2PDF(orgfilepath, outpdffilepath);
                                            cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailLG, "Can't add Legal Sinagure " + kqs.noidung }, "sp_updateState");
                                        }
                                        else
                                        {
                                            kqcv = ct.word2PDF(outfilepath, outpdffilepath);
                                        }
                                        //   string kqcv = ct.word2PDF(outfilepath,outpdffilepath);
                                        if (kqcv.ToLower() == "ok")
                                        {
                                            //try
                                            //{
                                            //    System.IO.File.Delete(outfilepath);
                                            //}
                                            //catch { }
                                            fileatt = pdffilename;
                                            if (approve(docno4, ykien1.Text, fileatt) == true)
                                            {
                                                cls.bCapNhat(new string[] { "@docno", "@newnamefile", "@username" }, new object[] { docno4, fileatt, Session["username"] }, "sp_updatefilenamecontract");
                                                //sent email to requestor
                                                content = "Dear Requestor,<br/>" +
                                        "The contract has been completed review<br/>The contract<br/>- No." + docno4 + "<br/>- Date " + pkr.Value.ToString("dd-MMM-yy") + " has been final reviewed by legal, You can print it<br/>- Brief content: " + editedItem["ContractContent"].Text + "<br/>----------------------------" +
                                        "Thân gởi,<br/>" +
                                        "Hợp đồng đã được xem xét<br/>Hợp đồng<br>- Số " + docno4 + "<br/> Ngày " + pkr.Value.ToString("dd-mm-yy") + " đã được bộ phận pháp lý xem xét xong, Bạn có thể in nó ra<br/>- Nội dung chính: " + editedItem["ContractContent"].Text;
                                                SendEmailApprove(docno4, editedItem["Email"].Text, fileatt, cls.cToString(Session["email"]), ykien1.Text.Trim(), content);
                                                TextBox finalnumber = (TextBox)editedItem["ContractNoLegal"].FindControl("txtContractNoFinal");
                                                cls.bCapNhat(new string[] { "@Docno", "@DocStorage", "@ClosedBy", "@filename", "@ContractNoLegal" }, new object[] { docno4, "", Session["username"], fileatt, finalnumber.Text.Trim() }, "sp_UpdateClosedContract");
                                            }
                                        }
                                        else//ko chuyen sang pdf duoc
                                        {
                                            //revert approve
                                            cls.bCapNhat(new string[] { "@docno", "@username", "@Note" }, new object[] { docno4, Session["username"], ykien1.Text.Trim() }, "sp_updateAppbyNameApprovedRevert");
                                            cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailLG, kqcv }, "sp_updateState");
                                            MsgBox1.AddMessage(kqcv, uc.ucMsgBox.enmMessageType.Error);
                                            //        fileatt = filenameout;
                                            //        cls.bCapNhat(new string[] { "@docno", "@newnamefile", "@username" }, new object[] { docno4, fileatt, Session["username"] }, "sp_updatefilenamecontract");
                                            //        //sent email to requestor
                                            //        content = "Dear Requestor,<br/>" +
                                            //"The contract has been completed review<br/>The contract<br/>- No." + docno4 + "<br/>- Date " + pkr.Value.ToString("dd-MMM-yy") + " has been final reviewed by legal, You can print it<br/>- Brief content: " + editedItem["ContractContent"].Text + "<br/>----------------------------" +
                                            //"Thân gởi,<br/>" +
                                            //"Hợp đồng đã được xem xét<br/>Hợp đồng<br>- Số " + docno4 + "<br/> Ngày " + pkr.Value.ToString("dd-mm-yy") + " đã được bộ phận pháp lý xem xét xong, Bạn có thể in nó ra<br/>- Nội dung chính: " + editedItem["ContractContent"].Text;
                                            //        SendEmailApprove(docno4, editedItem["Email"].Text, fileatt, cls.cToString(Session["email"]), ykien1.Text.Trim(), content);
                                            //        //  MsgBox1.AddMessage("Không thể chuyển sang pdf/n/r" + kqcv, uc.ucMsgBox.enmMessageType.Error);
                                        }

                                    }
                                    else
                                    {

                                        cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailLG, "Can't add Legal Sinagure " + kqs.noidung }, "sp_updateState");

                                        MsgBox1.AddMessage("Không thể thêm chữ ký vào file/n/r" + kqs.noidung, uc.ucMsgBox.enmMessageType.Error);
                                    }
                                }
                                #endregion
                                else
                                {
                                    #region Legal Pdf
                                    if (cls.getExtention(filenameup1).ToLower().IndexOf("pdfxxx") >= 0)
                                    {
                                        string orgfilepath = Server.MapPath("Upload/CO/" + filenameup1);

                                        string chuky = Server.MapPath("ImagesSignature/" + cls.cToString(Session["username"]) + ".png");
                                        if (System.IO.File.Exists(chuky) == false)
                                        {
                                            chuky = Server.MapPath("ImagesSignature/approved.png");
                                        }
                                        string filenameout = "L" + filenameup1;
                                        string outfilepath = Server.MapPath("Upload/CO/" + filenameout);

                                        String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                                        String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                                       // string outpdffilepath = Server.MapPath("Upload/CO/" + pdffilename);
                                        string urldownloadfile = @"" + strUrl + "/Upload/CO/" + filenameup1;

                                        // string kq = ct.AddSinagureFooterWord(orgfilepath, chuky, outfilepath,45,0);
                                        ketquaSign kqs = ct.AddSinagureFooterPdf(orgfilepath, chuky, outfilepath, 45, 0);
                                        if (kqs.bketqua == true)
                                        {
                                            fileatt = filenameout;
                                            if (approve(docno4, ykien1.Text, fileatt) == true)
                                            {
                                                cls.bCapNhat(new string[] { "@docno", "@newnamefile", "@username" }, new object[] { docno4, fileatt, Session["username"] }, "sp_updatefilenamecontract");
                                                //sent email to requestor
                                                content = "Dear Requestor,<br/>" +
                                        "The contract has been completed review<br/>The contract<br/>- No." + docno4 + "<br/>- Date " + pkr.Value.ToString("dd-MMM-yy") + " has been final reviewed by legal, You can print it<br/>- Brief content: " + editedItem["ContractContent"].Text + "<br/>----------------------------" +
                                        "Thân gởi,<br/>" +
                                        "Hợp đồng đã được xem xét<br/>Hợp đồng<br>- Số " + docno4 + "<br/> Ngày " + pkr.Value.ToString("dd-mm-yy") + " đã được bộ phận pháp lý xem xét xong, Bạn có thể in nó ra<br/>- Nội dung chính: " + editedItem["ContractContent"].Text;
                                                SendEmailApprove(docno4, editedItem["Email"].Text, fileatt, cls.cToString(Session["email"]), ykien1.Text.Trim(), content);
                                                TextBox finalnumber = (TextBox)editedItem["ContractNoLegal"].FindControl("txtContractNoFinal");
                                                cls.bCapNhat(new string[] { "@Docno", "@DocStorage", "@ClosedBy", "@filename", "@ContractNoLegal" }, new object[] { docno4, "", Session["username"], fileatt, finalnumber.Text.Trim() }, "sp_UpdateClosedContract");
                                            }
                                        }
                                        else
                                        {
                                            fileatt = filenameup1;
                                            cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailLG, "pdf Can't add Legal Sinagure " + kqs.noidung }, "sp_updateState");
                                            MsgBox1.AddMessage("Không thể thêm chữ ký vào file/n/r" + kqs.noidung, uc.ucMsgBox.enmMessageType.Error);
                                        }
                                    }
                                    #endregion
                                    else
                                    {
                                        #region Legal zip
                                        if (cls.getExtention(filenameup1).ToLower().IndexOf("zip") >= 0 /*|| cls.getExtention(filenameup1).ToLower().IndexOf("7z") >= 0 || cls.getExtention(filenameup1).ToLower().IndexOf("rar") >= 0*/)
                                        {
                                            //giai nen ra mot thu muc voi ten giong ten file nen
                                            string orgfilepath = Server.MapPath("Upload/CO/" + filenameup1);
                                            //ten file nen sau khi da xu ly va nen lai
                                            string zipfilename = "L" + filenameup1;

                                            string outfilezip = Server.MapPath("Upload/CO/" + zipfilename);

                                            // string foldername = filenameup1.Substring(0, 15);
                                            string foldername = "L" + filenameup1.Replace(".zip", "").Replace("-", "").Replace(".", "");
                                            var folderbathfull = Server.MapPath("Upload/CO/" + foldername);
                                            if (Directory.Exists(folderbathfull))
                                            {
                                                try
                                                {
                                                    Directory.Delete(folderbathfull, true);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        Directory.Delete(folderbathfull);
                                                    }
                                                    catch { }
                                                }
                                            }
                                            Directory.CreateDirectory(folderbathfull);
                                            ct.Extract(orgfilepath, folderbathfull);//extract va move tat ca file ve thu muc foldername
                                            string[] fileEntries = Directory.GetFiles(folderbathfull);
                                            //xu ly cac file doc da duoc giai nen
                                            foreach (string fileName in fileEntries)
                                            {
                                                #region Legal Zip doc
                                                if (cls.getExtention(fileName).ToLower().IndexOf("doc") >= 0)
                                                {
                                                    string orgfilepath1 = Server.MapPath("Upload/CO/" + foldername + "/" + cls.getFileName(fileName));
                                                    string chuky = Server.MapPath("ImagesSignature/" + cls.cToString(Session["username"]) + ".png");
                                                    if (System.IO.File.Exists(chuky) == false)
                                                    {
                                                        chuky = Server.MapPath("ImagesSignature/approved.png");
                                                    }
                                                    string filenameout = "L" + cls.getFileName(fileName);
                                                    string outfilepath = Server.MapPath("Upload/CO/" + foldername + "/" + filenameout);
                                                    String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                                                    String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                                                    string urldownloadfile = @"" + strUrl + "/Upload/CO/" + foldername + "/" + cls.getFileName(fileName);


                                                    // string kq = ct.AddSinagureFooterWord(orgfilepath1, chuky, outfilepath, 45, 0);
                                                    ketquaSign kqs = ct.AddSinagureFooterWord1(orgfilepath1, chuky, outfilepath, 45, 0);
                                                    // if (kq.ToLower() == "ok")
                                                    if (kqs.bketqua == true || kqs.noidung.IndexOf("File has been protected") >= 0)
                                                    {
                                                        string pdffilename = "LF" + cls.getFileName(fileName).Replace(cls.getExtention(fileName), ".pdf");

                                                        string outpdffilepath = Server.MapPath("Upload/CO/" + foldername + "/" + pdffilename);

                                                        string kqcv = "";
                                                        if (kqs.bketqua == false)//file protected thi van chuyen sang pdf ko co chu ky
                                                        {
                                                            outfilepath = "";
                                                            kqcv = ct.word2PDF(orgfilepath1, outpdffilepath);
                                                            cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailLG, "Can't add Legal Sinagure " + kqs.noidung }, "sp_updateState");
                                                        }
                                                        else
                                                        {
                                                            kqcv = ct.word2PDF(outfilepath, outpdffilepath);
                                                        }

                                                        //   string kqcv = ct.word2PDF(outfilepath, outpdffilepath);
                                                        if (kqcv.ToLower() == "ok")
                                                        {
                                                            if (kqs.bketqua == true)
                                                            {
                                                                try
                                                                {
                                                                    var myFile = File.Create(outfilepath);
                                                                    myFile.Close();
                                                                    myFile.Dispose();
                                                                    System.IO.File.Delete(outfilepath);
                                                                }
                                                                catch (Exception e1)
                                                                {
                                                                    cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailLG, ";LG " + e1.Message + " " + outfilepath }, "sp_updateState");
                                                                }
                                                            }
                                                        }
                                                        try
                                                        {
                                                            var myFile = File.Create(orgfilepath1);
                                                            myFile.Close();
                                                            myFile.Dispose();
                                                            System.IO.File.Delete(orgfilepath1);
                                                        }
                                                        catch (Exception e1)
                                                        {
                                                            cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailLG, ";LG " + e1.Message + " " + orgfilepath1 }, "sp_updateState");
                                                        }
                                                    }
                                                    else
                                                    {

                                                        cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailLG, ";Can't add Legal Sinagure to " + fileName }, "sp_updateState");


                                                    }

                                                }
                                                #endregion
                                                else
                                                {
                                                    #region Legal zip pdf
                                                    if (cls.getExtention(fileName).ToLower().IndexOf("pdfxxx") >= 0)
                                                    {
                                                        string orgfilepath1 = Server.MapPath("Upload/CO/" + foldername + "/" + cls.getFileName(fileName));
                                                        string chuky = Server.MapPath("ImagesSignature/" + cls.cToString(Session["username"]) + ".png");
                                                        if (System.IO.File.Exists(chuky) == false)
                                                        {
                                                            chuky = Server.MapPath("ImagesSignature/approved.png");
                                                        }
                                                        string filenameout = "L" + cls.getFileName(fileName);
                                                        string outfilepath = Server.MapPath("Upload/CO/" + foldername + "/" + filenameout);
                                                        String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                                                        String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                                                        string urldownloadfile = @"" + strUrl + "/Upload/CO/" + foldername + "/" + cls.getFileName(fileName);


                                                        // string kq = ct.AddSinagureFooterWord(orgfilepath1, chuky, outfilepath, 45, 0);
                                                        ketquaSign kqs = ct.AddSinagureFooterPdf(orgfilepath1, chuky, outfilepath, 45, 0);
                                                        if (kqs.bketqua == true)
                                                        {
                                                            try
                                                            {
                                                                var myFile = File.Create(orgfilepath1);
                                                                myFile.Close();
                                                                myFile.Dispose();
                                                                System.IO.File.Delete(orgfilepath1);
                                                            }
                                                            catch (Exception e1)
                                                            {
                                                                cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailLG, ";LG " + e1.Message + " " + orgfilepath1 }, "sp_updateState");
                                                            }
                                                        }
                                                        else {
                                                            cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailLG, ";LG " + "Can't add singature into pdf zip" + " " + orgfilepath1 }, "sp_updateState");
                                                        }
                                                    }
                                                    #endregion
                                                }
                                            }

                                            //nen lai thu muc thanh file nen

                                            if (ct.Archive(folderbathfull, outfilezip) == true)
                                            {
                                                if (approve(docno4, ykien1.Text, zipfilename) == true)
                                                {
                                                    cls.bCapNhat(new string[] { "@docno", "@newnamefile", "@username" }, new object[] { docno4, zipfilename, Session["username"] }, "sp_updatefilenamecontract");
                                                    //sent email to requestor
                                                    content = "Dear Requestor,<br/>" +
                                            "The contract has been completed review<br/>The contract<br/>- No." + docno4 + "<br/>- Date " + pkr.Value.ToString("dd-MMM-yy") + " has been final reviewed by legal, You can print it<br/>- Brief content: " + editedItem["ContractContent"].Text + "<br/>----------------------------" +
                                            "Thân gởi,<br/>" +
                                            "Hợp đồng đã được xem xét<br/>Hợp đồng<br>- Số " + docno4 + "<br/> Ngày " + pkr.Value.ToString("dd-mm-yy") + " đã được bộ phận pháp lý xem xét xong, Bạn có thể in nó ra<br/>- Nội dung chính: " + editedItem["ContractContent"].Text;
                                                    SendEmailApprove(docno4, editedItem["Email"].Text, zipfilename, cls.cToString(Session["email"]), ykien1.Text.Trim(), content);
                                                    TextBox finalnumber = (TextBox)editedItem["ContractNoLegal"].FindControl("txtContractNoFinal");
                                                    cls.bCapNhat(new string[] { "@Docno", "@DocStorage", "@ClosedBy", "@filename", "@ContractNoLegal" }, new object[] { docno4, "", Session["username"], zipfilename, finalnumber.Text.Trim() }, "sp_UpdateClosedContract");
                                                }
                                                else
                                                {
                                                    cls.bCapNhat(new string[] { "@docno", "@username", "@Note" }, new object[] { docno4, Session["username"], ykien1.Text.Trim() }, "sp_updateAppbyNameApprovedRevert");
                                                    cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailLG, "Khong the update status=1" }, "sp_updateState");
                                                    MsgBox1.AddMessage("Không thể approve<br></br>Can't approve", uc.ucMsgBox.enmMessageType.Error);
                                                }
                                            }

                                            else
                                            {
                                                cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailLG, " Ko The Nen file" }, "sp_updateState");
                                                MsgBox1.AddMessage("Không thể nén file", uc.ucMsgBox.enmMessageType.Error);
                                            }
                                        }
                                        #endregion
                                        #region Legal file khac
                                        else //ko phai file doc, cung ko phai file zip
                                        {
                                            if (approve(docno4, ykien1.Text, fileatt) == true)
                                            {
                                                //sent email to requestor
                                                content = "Dear Requestor,<br/>" +
                                           "The contract has been completed review<br/>The contract<br/>- No." + docno4 + "<br/>- Date " + pkr.Value.ToString("dd-MMM-yy") + " has been final reviewed by legal, You can print it<br/>- Brief content: " + editedItem["ContractContent"].Text + "<br/>----------------------------" +
                                           "Thân gởi,<br/>" +
                                           "Hợp đồng đã được xem xét<br/>Hợp đồng<br>- Số " + docno4 + "<br/> Ngày " + pkr.Value.ToString("dd-mm-yy") + " đã được bộ phận pháp lý xem xét xong, Bạn có thể in nó ra<br/>- Nội dung chính: " + editedItem["ContractContent"].Text;
                                                SendEmailApprove(docno4, editedItem["Email"].Text, fileatt, cls.cToString(Session["email"]), ykien1.Text.Trim(), content);
                                                TextBox finalnumber = (TextBox)editedItem["ContractNoLegal"].FindControl("txtContractNoFinal");
                                                cls.bCapNhat(new string[] { "@Docno", "@DocStorage", "@ClosedBy", "@filename", "@ContractNoLegal" }, new object[] { docno4, "", Session["username"], fileatt, finalnumber.Text.Trim() }, "sp_UpdateClosedContract");
                                            }
                                        }
                                        #endregion
                                    }
                                }

                            }
                            #endregion
                            #region Finance
                            else
                            {
                                //cho nay add chu ky finance
                                #region Finance Doc
                                if (cls.getExtention(filenameup1).ToLower().IndexOf("doc") >= 0)
                                {
                                    string orgfilepath = Server.MapPath("Upload/CO/" + filenameup1);
                                    string chuky = Server.MapPath("ImagesSignature/" + cls.cToString(Session["username"]) + ".png");
                                    if (System.IO.File.Exists(chuky) == false)
                                    {
                                        chuky = Server.MapPath("ImagesSignature/approved.png");
                                    }
                                    string filenameout = "F" + filenameup1;
                                    string outfilepath = Server.MapPath("Upload/CO/" + filenameout);

                                    //  string kq = ct.AddSinagureFooterWord(orgfilepath, chuky, outfilepath, 10, 0);
                                    ketquaSign kqs = ct.AddSinagureFooterWord1(orgfilepath, chuky, outfilepath, 10, 0);
                                    // if (kq.ToLower() == "ok")
                                    if (kqs.bketqua == true || kqs.noidung.IndexOf("File has been protected") >= 0)
                                    {
                                        //da tao ra duoc file add chu ky finance
                                        //update lai ten file
                                        if (kqs.bketqua == false)//file protected ko add duoc chu ky
                                        {
                                            fileatt = filenameup1;
                                            //  cls.bCapNhat(new string[] { "@docno", "@newnamefile", "@username" }, new object[] { docno4, filenameup1, Session["username"] }, "sp_updatefilenamecontract");
                                        }
                                        else
                                        {
                                            fileatt = filenameout;
                                            //  cls.bCapNhat(new string[] { "@docno", "@newnamefile", "@username" }, new object[] { docno4, filenameout, Session["username"] }, "sp_updatefilenamecontract");
                                        }
                                        //set trang thang trong ban approve =1
                                        if (approve(docno4, ykien1.Text, fileatt) == true)
                                        {
                                            if (isHDMau == false)//neu ko phai la hop dau mau thi moi gui den legal
                                            {
                                                //sent email den legal
                                                ketqua kketqua1 = SendEmailSubmit(docno4, emailLG, fileatt, emailFC, ykien1.Text.Trim(), "The contract has been review by finance, Next step, we want to review by Legal department");
                                                //sent email to requestor
                                                if (kketqua1.bketqua == true)
                                                {
                                                    cls.bCapNhat(new string[] { "@docno", "@newnamefile", "@username" }, new object[] { docno4, fileatt, Session["username"] }, "sp_updatefilenamecontract");
                                                    content = "Dear Requestor,<br/>" +
                                                  "The contract<br/>- No." + docno4 + "<br/>- Date " + pkr.Value.ToString("dd-MMM-yy") + " has been reviewed by finance and sent to legal to review<br/>- Brief content: " + editedItem["ContractContent"].Text + "<br/>----------------------------" +
                                                  "Thân gởi,<br/>" +
                                                  "Hợp đồng<br>- Số " + docno4 + "<br/> Ngày " + pkr.Value.ToString("dd-mm-yy") + " đã được xem xét bởi phòng tài chính và đã được chuyển đến bộ phận pháp lý để xem xét tiếp theo qui trình<br/>- Nội dung chính: " + editedItem["ContractContent"].Text;
                                                    SendEmailApprove(docno4, editedItem["Email"].Text, fileatt, cls.cToString(Session["email"]), ykien1.Text.Trim(), content);
                                                }
                                                else
                                                {
                                                    //revert approve
                                                    cls.bCapNhat(new string[] { "@docno", "@username", "@Note" }, new object[] { docno4, Session["username"], ykien1.Text.Trim() }, "sp_updateAppbyNameApprovedRevert");
                                                    cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailFC, kketqua1.noidung }, "sp_updateState");
                                                    MsgBox1.AddMessage(kketqua1.noidung, uc.ucMsgBox.enmMessageType.Error);
                                                }
                                            }
                                            else //la hop dong mau thi final tai buoc nay va gui email ve cho end user
                                            {
                                                //string kqcv = ct.Doc2PdfSave(outfilepath, outpdffilepath);
                                                string kqcv = "";
                                                orgfilepath = Server.MapPath("Upload/CO/" + fileatt);
                                                string pdffilename = "F" + cls.getFileName(fileatt).Replace(cls.getExtention(fileatt), ".pdf");
                                                string outpdffilepath = Server.MapPath("Upload/CO/" + pdffilename);
                                                if (kqs.bketqua == false)//file protected thi van chuyen sang pdf ko co chu ky
                                                {
                                                    kqcv = ct.word2PDF(orgfilepath, outpdffilepath);
                                                    cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailFC, "Can't add Finance Sinagure " + kqs.noidung }, "sp_updateState");
                                                }
                                                else
                                                {
                                                    kqcv = ct.word2PDF(outfilepath, outpdffilepath);
                                                }
                                                //   string kqcv = ct.word2PDF(outfilepath,outpdffilepath);
                                                if (kqcv.ToLower() == "ok")
                                                {
                                                    fileatt = pdffilename;
                                                        cls.bCapNhat(new string[] { "@docno", "@newnamefile", "@username" }, new object[] { docno4, fileatt, Session["username"] }, "sp_updatefilenamecontract");
                                                        //sent email to requestor
                                                        content = "Dear Requestor,<br/>" +
                                                "The contract has been completed review<br/>The contract<br/>- No." + docno4 + "<br/>- Date " + pkr.Value.ToString("dd-MMM-yy") + " has been final reviewed by Finance, You can print it<br/>- Brief content: " + editedItem["ContractContent"].Text + "<br/>----------------------------" +
                                                "Thân gởi,<br/>" +
                                                "Hợp đồng đã được xem xét<br/>Hợp đồng<br>- Số " + docno4 + "<br/> Ngày " + pkr.Value.ToString("dd-mm-yy") + " đã được bộ phận tài chính và kiểm soát xem xét xong, Bạn có thể in nó ra<br/>- Nội dung chính: " + editedItem["ContractContent"].Text;
                                                        SendEmailApprove(docno4, editedItem["Email"].Text, fileatt, cls.cToString(Session["email"]), ykien1.Text.Trim(), content);
                                                        TextBox finalnumber = (TextBox)editedItem["ContractNoLegal"].FindControl("txtContractNoFinal");
                                                        cls.bCapNhat(new string[] { "@Docno", "@DocStorage", "@ClosedBy", "@filename", "@ContractNoLegal" }, new object[] { docno4, "", Session["username"], fileatt, finalnumber.Text.Trim() }, "sp_UpdateClosedContract");
                                                }
                                                else//ko chuyen sang pdf duoc
                                                {
                                                    //revert approve
                                                    cls.bCapNhat(new string[] { "@docno", "@username", "@Note" }, new object[] { docno4, Session["username"], ykien1.Text.Trim() }, "sp_updateAppbyNameApprovedRevert");
                                                    cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailFC, kqcv }, "sp_updateState");
                                                    MsgBox1.AddMessage(kqcv, uc.ucMsgBox.enmMessageType.Error);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailFC, "Can't add Finance Sinagure" }, "sp_updateState");
                                        MsgBox1.AddMessage("Không thể thêm chữ ký vào file/n/r" + kqs.noidung, uc.ucMsgBox.enmMessageType.Error);
                                    }
                                }
                                #endregion

                                else //neu la file nen
                                    #region Finace Zip
                                    if (cls.getExtention(filenameup1).ToLower().IndexOf("zip") >= 0 /*|| cls.getExtention(filenameup1).ToLower().IndexOf("7z") >= 0 || cls.getExtention(filenameup1).ToLower().IndexOf("rar") >= 0*/)
                                    {
                                        //giai nen ra mot thu muc voi ten giong ten file nen
                                        string orgfilepath = Server.MapPath("Upload/CO/" + filenameup1);
                                        //ten file nen sau khi da xu ly va nen lai
                                        string zipfilename = "F" + filenameup1;

                                        string outfilezip = Server.MapPath("Upload/CO/" + zipfilename);
                                        string foldername = filenameup1.Replace(".zip", "").Replace("-", "").Replace(".", "");
                                        var folderbathfull = Server.MapPath("Upload/CO/" + foldername);
                                        if (Directory.Exists(folderbathfull))
                                        {
                                            //string[] fileEntries = Directory.GetFiles(folderbathfull);

                                            try
                                            {
                                                Directory.Delete(folderbathfull, true);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    Directory.Delete(folderbathfull);
                                                }
                                                catch { }
                                            }
                                        }
                                        Directory.CreateDirectory(folderbathfull);
                                        ct.Extract(orgfilepath, folderbathfull);//extract va move tat ca file ve thu muc foldername
                                        string[] fileEntries = Directory.GetFiles(folderbathfull);
                                        //xu ly cac file doc da duoc giai nen
                                        foreach (string fileName in fileEntries)
                                        {
                                            if (cls.getExtention(fileName).ToLower().IndexOf("doc") >= 0)
                                            {
                                                string orgfilepath1 = Server.MapPath("Upload/CO/" + foldername + "/" + cls.getFileName(fileName));
                                               

                                                string chuky = Server.MapPath("ImagesSignature/" + cls.cToString(Session["username"]) + ".png");
                                                if (System.IO.File.Exists(chuky) == false)
                                                {
                                                    chuky = Server.MapPath("ImagesSignature/approved.png");
                                                }
                                                string filenameout = "F" + cls.getFileName(fileName);
                                                //  string filenameout =cls.getFileName(fileName);
                                                string outfilepath = Server.MapPath("Upload/CO/" + foldername + "/" + filenameout);
                                                // string kq = ct.AddSinagureFooter(orgfilepath1, chuky, outfilepath);
                                                //  string kq = ct.AddSinagureFooterWord(orgfilepath1, chuky, outfilepath, 10, 0);
                                                ketquaSign kqs = ct.AddSinagureFooterWord1(orgfilepath1, chuky, outfilepath, 10, 0);
                                                //  if (kq.ToLower() == "ok")
                                                if (kqs.bketqua == true || kqs.noidung.IndexOf("File has been protected") >= 0)
                                                {
                                                    if (kqs.bketqua == true)
                                                    {
                                                        try
                                                        {
                                                            var myFile = File.Create(orgfilepath1);
                                                            myFile.Close();
                                                            myFile.Dispose();
                                                            System.IO.File.Delete(orgfilepath1);
                                                            orgfilepath1 = outfilepath;
                                                            cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailFC, ";FC " + kqs.noidung }, "sp_updateState");
                                                        }
                                                        catch (Exception e1)
                                                        {
                                                            cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailFC, ";FC " + e1.Message + " " + orgfilepath1 }, "sp_updateState");
                                                        }
                                                    }

                                                    #region Chuyen cac file trong zip thanh pdf
                                                    if (isHDMau == true)
                                                    {
                                                        string pdffilename = "F" + cls.getFileName(fileName).Replace(cls.getExtention(fileName), ".pdf");

                                                        string outpdffilepath = Server.MapPath("Upload/CO/" + foldername + "/" + pdffilename);

                                                        string kqcv = "";
                                                        if (kqs.bketqua == false)//file protected thi van chuyen sang pdf ko co chu ky
                                                        {
                                                            outfilepath = "";
                                                            kqcv = ct.word2PDF(orgfilepath1, outpdffilepath);
                                                            cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailFC, "Can't add Finance Sinagure " + kqs.noidung }, "sp_updateState");
                                                        }
                                                        else
                                                        {
                                                            kqcv = ct.word2PDF(orgfilepath1, outpdffilepath);
                                                        }

                                                        //   string kqcv = ct.word2PDF(outfilepath, outpdffilepath);
                                                        if (kqcv.ToLower() == "ok")
                                                        {
                                                            if (kqs.bketqua == true)
                                                            {
                                                                try
                                                                {
                                                                    var myFile = File.Create(outfilepath);
                                                                    myFile.Close();
                                                                    myFile.Dispose();
                                                                    System.IO.File.Delete(outfilepath);
                                                                }
                                                                catch (Exception e1)
                                                                {
                                                                    cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailFC, ";FC " + e1.Message + " " + outfilepath }, "sp_updateState");
                                                                }
                                                            }
                                                        }
                                                        try
                                                        {
                                                            var myFile = File.Create(orgfilepath1);
                                                            myFile.Close();
                                                            myFile.Dispose();
                                                            System.IO.File.Delete(orgfilepath1);
                                                        }
                                                        catch (Exception e1)
                                                        {
                                                            cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailFC, ";FC " + e1.Message + " " + orgfilepath1 }, "sp_updateState");
                                                        }
                                                    }
                                                    #endregion

                                                }
                                                else
                                                {
                                                    cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailFC, ";Can't add Finance Sinagure to " + fileName + "-" + kqs.noidung }, "sp_updateState");
                                                }

                                            }
                                            else
                                            {
                                                #region Zip pdf
                                                if (cls.getExtention(fileName).ToLower().IndexOf("pdfxxx") >= 0)
                                                {
                                                    string orgfilepath1 = Server.MapPath("Upload/CO/" + foldername + "/" + cls.getFileName(fileName));
                                                    string chuky = Server.MapPath("ImagesSignature/" + cls.cToString(Session["username"]) + ".png");
                                                    if (System.IO.File.Exists(chuky) == false)
                                                    {
                                                        chuky = Server.MapPath("ImagesSignature/approved.png");
                                                    }
                                                    string filenameout = "F" + cls.getFileName(fileName);
                                                    //  string filenameout =cls.getFileName(fileName);
                                                    string outfilepath = Server.MapPath("Upload/CO/" + foldername + "/" + filenameout);
                                                    // string kq = ct.AddSinagureFooter(orgfilepath1, chuky, outfilepath);
                                                    //  string kq = ct.AddSinagureFooterWord(orgfilepath1, chuky, outfilepath, 10, 0);
                                                    ketquaSign kqs = ct.AddSinagureFooterPdf(orgfilepath1, chuky, outfilepath, 10, 0);
                                                    //  if (kq.ToLower() == "ok")
                                                    if (kqs.bketqua == true)
                                                    {
                                                        try
                                                        {
                                                            var myFile = File.Create(orgfilepath1);
                                                            myFile.Close();
                                                            myFile.Dispose();
                                                            System.IO.File.Delete(orgfilepath1);
                                                            orgfilepath1 = outfilepath;
                                                            cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailFC, ";FC " + kqs.noidung }, "sp_updateState");
                                                        }
                                                        catch (Exception e1)
                                                        {
                                                            cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailFC, ";FC " + e1.Message + " " + orgfilepath1 }, "sp_updateState");
                                                        }
                                                    }
                                                }
                                                #endregion
                                            }
                                        }

                                        //nen lai thu muc thanh file nen

                                        if (ct.Archive(folderbathfull, outfilezip) == true)
                                        {
                                            if (isHDMau == false)
                                            {
                                                //sent email toi legal
                                                if (approve(docno4, ykien1.Text, zipfilename) == true)
                                                {
                                                    ketqua kketqua3 = SendEmailSubmit(docno4, emailLG, zipfilename, emailFC, ykien1.Text.Trim(), "The contract has been review by finance, Next step, we want to review by Legal department");
                                                    //sent email to requestor
                                                    if (kketqua3.bketqua == true)
                                                    {
                                                        cls.bCapNhat(new string[] { "@docno", "@newnamefile", "@username" }, new object[] { docno4, zipfilename, Session["username"] }, "sp_updatefilenamecontract");
                                                        content = "Dear Requestor,<br/>" +
                                                     "The contract<br/>- No." + docno4 + "<br/>- Date " + pkr.Value.ToString("dd-MMM-yy") + " has been reviewed by finance and sent to legal to review<br/>- Brief content: " + editedItem["ContractContent"].Text + "<br/>----------------------------" +
                                                     "Thân gởi,<br/>" +
                                                     "Hợp đồng<br>- Số " + docno4 + "<br/> Ngày " + pkr.Value.ToString("dd-mm-yy") + " đã được xem xét bởi phòng tài chính và đã được chuyển đến bộ phận pháp lý để xem xét tiếp theo qui trình<br/>- Nội dung chính: " + editedItem["ContractContent"].Text;
                                                        SendEmailApprove(docno4, editedItem["Email"].Text, zipfilename, cls.cToString(Session["email"]), ykien1.Text.Trim(), content);
                                                    }
                                                    else
                                                    {
                                                        cls.bCapNhat(new string[] { "@docno", "@username", "@Note" }, new object[] { docno4, Session["username"], ykien1.Text.Trim() }, "sp_updateAppbyNameApprovedRevert");
                                                        cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailFC, kketqua3.noidung }, "sp_updateState");
                                                        MsgBox1.AddMessage(kketqua3.noidung, uc.ucMsgBox.enmMessageType.Error);
                                                    }
                                                }
                                            }
                                            else//la hop dong mau thi ko can gui de legal
                                            {
                                                cls.bCapNhat(new string[] { "@docno", "@newnamefile", "@username" }, new object[] { docno4, zipfilename, Session["username"] }, "sp_updatefilenamecontract");
                                                //sent email to requestor
                                                content = "Dear Requestor,<br/>" +
                                        "The contract has been completed review<br/>The contract<br/>- No." + docno4 + "<br/>- Date " + pkr.Value.ToString("dd-MMM-yy") + " has been final reviewed by Finance, You can print it<br/>- Brief content: " + editedItem["ContractContent"].Text + "<br/>----------------------------" +
                                        "Thân gởi,<br/>" +
                                        "Hợp đồng đã được xem xét<br/>Hợp đồng<br>- Số " + docno4 + "<br/> Ngày " + pkr.Value.ToString("dd-mm-yy") + " đã được bộ phận pháp lý xem xét xong, Bạn có thể in nó ra<br/>- Nội dung chính: " + editedItem["ContractContent"].Text;
                                                SendEmailApprove(docno4, editedItem["Email"].Text, zipfilename, cls.cToString(Session["email"]), ykien1.Text.Trim(), content);
                                                TextBox finalnumber = (TextBox)editedItem["ContractNoLegal"].FindControl("txtContractNoFinal");
                                                cls.bCapNhat(new string[] { "@Docno", "@DocStorage", "@ClosedBy", "@filename", "@ContractNoLegal" }, new object[] { docno4, "", Session["username"], zipfilename, finalnumber.Text.Trim() }, "sp_UpdateClosedContract");
                                            }
                                        }

                                        else
                                        {

                                            cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailFC, " Ko The Nen file '" + folderbathfull + "' Thanh '" + outfilezip + "'" }, "sp_updateState");
                                            MsgBox1.AddMessage("Không thể nén file", uc.ucMsgBox.enmMessageType.Error);

                                        }
                                    }
                                    #endregion
                                    else//file upload ko phai la file doc, cung ko phai la file zíp
                                    {
                                        #region Finance pdf
                                        if (cls.getExtention(filenameup1).ToLower().IndexOf("pdfxxx") >= 0)//neu la file pdf
                                        {
                                            string orgfilepath = Server.MapPath("Upload/CO/" + filenameup1);
                                            string chuky = Server.MapPath("ImagesSignature/" + cls.cToString(Session["username"]) + ".png");
                                            if (System.IO.File.Exists(chuky) == false)
                                            {
                                                chuky = Server.MapPath("ImagesSignature/approved.png");
                                            }
                                            string filenameout = "F" + filenameup1;
                                            string outfilepath = Server.MapPath("Upload/CO/" + filenameout);
                                            ketquaSign kqs = ct.AddSinagureFooterPdf(orgfilepath, chuky, outfilepath,10,0);
                                            if (kqs.bketqua == true)
                                            {
                                                fileatt = filenameout;
                                            }
                                            else {
                                                fileatt = filenameup1;
                                            }
                                            if (approve(docno4, ykien1.Text, fileatt) == true)
                                            {
                                                if (isHDMau == false)//neu ko phai la hop dau mau thi moi gui den legal
                                                {
                                                    //sent email den legal
                                                    ketqua kketqua1 = SendEmailSubmit(docno4, emailLG, fileatt, emailFC, ykien1.Text.Trim(), "The contract has been review by finance, Next step, we want to review by Legal department");
                                                    //sent email to requestor
                                                    if (kketqua1.bketqua == true)
                                                    {
                                                        cls.bCapNhat(new string[] { "@docno", "@newnamefile", "@username" }, new object[] { docno4, fileatt, Session["username"] }, "sp_updatefilenamecontract");
                                                        content = "Dear Requestor,<br/>" +
                                                      "The contract<br/>- No." + docno4 + "<br/>- Date " + pkr.Value.ToString("dd-MMM-yy") + " has been reviewed by finance and sent to legal to review<br/>- Brief content: " + editedItem["ContractContent"].Text + "<br/>----------------------------" +
                                                      "Thân gởi,<br/>" +
                                                      "Hợp đồng<br>- Số " + docno4 + "<br/> Ngày " + pkr.Value.ToString("dd-mm-yy") + " đã được xem xét bởi phòng tài chính và đã được chuyển đến bộ phận pháp lý để xem xét tiếp theo qui trình<br/>- Nội dung chính: " + editedItem["ContractContent"].Text;
                                                        SendEmailApprove(docno4, editedItem["Email"].Text, fileatt, cls.cToString(Session["email"]), ykien1.Text.Trim(), content);
                                                    }
                                                    else
                                                    {
                                                        //revert approve
                                                        cls.bCapNhat(new string[] { "@docno", "@username", "@Note" }, new object[] { docno4, Session["username"], ykien1.Text.Trim() }, "sp_updateAppbyNameApprovedRevert");
                                                        cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailFC, kketqua1.noidung }, "sp_updateState");
                                                        MsgBox1.AddMessage(kketqua1.noidung, uc.ucMsgBox.enmMessageType.Error);
                                                    }
                                                }
                                                else //la hop dong mau thi final tai buoc nay va gui email ve cho end user
                                                {
                                                  
                                                       // fileatt = pdffilename;
                                                        cls.bCapNhat(new string[] { "@docno", "@newnamefile", "@username" }, new object[] { docno4, fileatt, Session["username"] }, "sp_updatefilenamecontract");
                                                        //sent email to requestor
                                                        content = "Dear Requestor,<br/>" +
                                                "The contract has been completed review<br/>The contract<br/>- No." + docno4 + "<br/>- Date " + pkr.Value.ToString("dd-MMM-yy") + " has been final reviewed by Finance, You can print it<br/>- Brief content: " + editedItem["ContractContent"].Text + "<br/>----------------------------" +
                                                "Thân gởi,<br/>" +
                                                "Hợp đồng đã được xem xét<br/>Hợp đồng<br>- Số " + docno4 + "<br/> Ngày " + pkr.Value.ToString("dd-mm-yy") + " đã được bộ phận pháp lý xem xét xong, Bạn có thể in nó ra<br/>- Nội dung chính: " + editedItem["ContractContent"].Text;
                                                        SendEmailApprove(docno4, editedItem["Email"].Text, fileatt, cls.cToString(Session["email"]), ykien1.Text.Trim(), content);
                                                        TextBox finalnumber = (TextBox)editedItem["ContractNoLegal"].FindControl("txtContractNoFinal");
                                                        cls.bCapNhat(new string[] { "@Docno", "@DocStorage", "@ClosedBy", "@filename", "@ContractNoLegal" }, new object[] { docno4, "", Session["username"], fileatt, finalnumber.Text.Trim() }, "sp_UpdateClosedContract");
                                                   
                                                }
                                            }
                                        }
                                        #endregion
                                        #region Finance file khac
                                        else//file upload ko phai la file doc, cung ko phai la file zíp, ko phai pdf
                                        {
                                            //sent email toi legal
                                            if (approve(docno4, ykien1.Text, fileatt) == true)
                                            {
                                                ketqua kketqua2 = SendEmailSubmit(docno4, emailLG, fileatt, emailFC, ykien1.Text.Trim(), "The contract has been review by finance, Next step, we want to review by Legal department");
                                                if (kketqua2.bketqua == true)
                                                {
                                                    cls.bCapNhat(new string[] { "@docno", "@newnamefile", "@username" }, new object[] { docno4, fileatt, Session["username"] }, "sp_updatefilenamecontract");
                                                    //sent email to requestor
                                                    content = "Dear Requestor,<br/>" +
                                                 "The contract<br/>- No." + docno4 + "<br/>- Date " + pkr.Value.ToString("dd-MMM-yy") + " has been reviewed by finance and sent to legal to review<br/>- Brief content: " + editedItem["ContractContent"].Text + "<br/>----------------------------" +
                                                 "Thân gởi,<br/>" +
                                                 "Hợp đồng<br>- Số " + docno4 + "<br/> Ngày " + pkr.Value.ToString("dd-mm-yy") + " đã được xem xét bởi phòng tài chính và đã được chuyển đến bộ phận pháp lý để xem xét tiếp theo qui trình<br/>- Nội dung chính: " + editedItem["ContractContent"].Text;
                                                    SendEmailApprove(docno4, editedItem["Email"].Text, fileatt, cls.cToString(Session["email"]), ykien1.Text.Trim(), content);
                                                }
                                                else
                                                {
                                                    cls.bCapNhat(new string[] { "@docno", "@username", "@Note" }, new object[] { docno4, Session["username"], ykien1.Text.Trim() }, "sp_updateAppbyNameApprovedRevert");
                                                    cls.bCapNhat(new string[] { "@docno", "@approval", "@state" }, new object[] { docno4, emailFC, kketqua2.noidung }, "sp_updateState");
                                                    MsgBox1.AddMessage(kketqua2.noidung, uc.ucMsgBox.enmMessageType.Error);
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                            }
                            #endregion


                      //  }
                    //}
                    //else
                    //{
                    //    MsgBox1.AddMessage("You can't review this Contract because it has not submit", uc.ucMsgBox.enmMessageType.Error);
                    //    return;
                    //}

                    break;
                case "Reject"://review
                     editedItem = (GridEditableItem)e.Item;
                    status = editedItem["status"].Text;
                    //if (status == "2")//chi nhung cong van nao dang trinh ky thi moi reject
                    //{
                        //PostBackTrigger posttrg = new PostBackTrigger();
                        //posttrg.ControlID = "RadGrid1";// e.Item.FindControl("btRejectGrid").u.UniqueID;
                        //this.UpdatePanel1.Triggers.Add(posttrg);

                        string docno5 = editedItem["ContractNo"].Text;
                        TextBox ykien2 = (TextBox)editedItem["AppnoteLegal_Finance"].FindControl("txtYKienGrid");
                        if (ykien2.Text.Trim() == "")
                        {
                            MsgBox1.AddMessage("You must fill in reason to reject", uc.ucMsgBox.enmMessageType.Error);
                            return;
                        }
                        FileUpload up3 = (FileUpload)editedItem["AppnoteLegal_Finance"].FindControl("FileUpload2");
                        string filenameup2 = "";
                        string fileatt2 = "";
                        if (up3.HasFile)//co upload file
                        {
                            filenameup2 = upload(up3, docno5);
                            fileatt2 = filenameup2;// Server.MapPath("Upload/CO/" + filenameup1);
                        }
                        else
                        {
                            fileatt2 = editedItem["Attachedfile"].Text;
                        }
                        if (Reject(docno5, ykien2.Text, filenameup2) == true)
                        {
                            pkr = cls.cToDateTime(editedItem["ContractDate"].Text);
                            content = "Dear Requester,<br/>" +
                            "Please kindly forward the contract<br/>- No. " + docno5 + "<br/> Date " + pkr.Value.ToString("dd-MMM-yy") + "<br/>- Brief content: " + editedItem["ContractContent"].Text + " <br/>- Rejected with reason:<br/>" +
                            "-----------------------<br/>" +
                            "Thân gởi người yêu cầu.<br/>" +
                            "Xin vui lòng chuyển các hợp đồng đính kèm<br/>- Số " + docno5 + "<br/>- Ngày " + pkr.Value.ToString("dd-MM-yy") + "<br/>- Nội dung chính: " + editedItem["ContractContent"].Text + " <br/>- Phản hổi với lý do:";

                         //   cls.bCapNhat(new string[] { "@docno", "@note" }, new object[] { docno, ykien.Text }, "sp_UpdateContractNote");
                            SendEmailReject(docno5, editedItem["Email"].Text, fileatt2, cls.cToString(Session["email"]), ykien2.Text.Trim(), content);
                        }

                    //}
                    //else
                    //{
                    //    MsgBox1.AddMessage("You can't reject this Contract because it has not submit", uc.ucMsgBox.enmMessageType.Error);
                    //    return;
                    //}

                    break;
             //   case "Closed"://ko su dung
             //        editedItem = (GridEditableItem)e.Item;
             //status = editedItem["status"].Text;
             //       if (txtStorega.Text.Trim() == "")
             //       {
             //           MsgBox1.AddMessage("You must fill in Storage location", uc.ucMsgBox.enmMessageType.Error);
             //           txtStorega.Focus();
             //           return;
             //       }
             //       //if (status == "1")//chi nhung cong van nao dang trinh ky thi moi reject
             //       //{
             //           string docno3 = editedItem["ContractNo"].Text;

             //           //                        if (Reject(docno, ykien.Text) == true)
             //           //                        {
             //           //                            pkr = cls.cToDateTime(editedItem["DocDate"].Text);
             //           //                            content = "Dear Requester,<br/>" +
             //           //"Please kindly forward the in-coming documentation, No. " + editedItem["Docnoreceived"].Text + ", dated " + pkr.Value.ToString("dd-MM-yy") + " to other department for processing because it is not within our territory in charge.<br/>" +
             //           //"-----------------------<br/>" +
             //           //"Thân gởi người yêu cầu.<br/>" +
             //           //"Xin vui lòng chuyển các hồ sơ công văn đính kèm, số " + editedItem["Docnoreceived"].Text + ", ngày " + pkr.Value.ToString("dd-MM-yy") + " cho bộ phận liên quan khác để xử lý vì nội dung không nằm trong phạm vi phụ trách của chúng tôi.";

             //           if (cls.bCapNhat(new string[] { "@Docno", "@DocStorage", "@ClosedBy" }, new object[] { docno3, txtStorega.Text.Trim(), Session["username"] }, "sp_UpdateClosedContract") == true)
             //           {
             //               LoadCV();
             //               MsgBox1.AddMessage("Contract has been complete", uc.ucMsgBox.enmMessageType.Success);

             //           }

             //           //    SendEmailReject(docno, editedItem["Email"].Text, editedItem["AttachedFile"].Text, cls.cToString(Session["email"]), ykien.Text.Trim(), editedItem["DIDO"].Text, content);
             //           //}

             //       //}
             //       //else
             //       //{
             //       //    MsgBox1.AddMessage("You can't close this Contract because it has not review", uc.ucMsgBox.enmMessageType.Error);
             //       //    return;
             //       //}

             //       break;
                case Telerik.Web.UI.RadGrid.FilterCommandName:
                    ReLoadCV();
                    break;
                case Telerik.Web.UI.RadGrid.RebindGridCommandName:
                    ReLoadCV();
                    break;
            }
        }
        protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dataItem = RadGrid1.SelectedItems[0] as GridDataItem;
            if (dataItem != null)
            {
               // this.UpdatePanel1.Triggers.RemoveAt(2);
                
              //  btCreate.Visible = true;Email
                txtContractNo.Text = dataItem["ContractNo"].Text;
                hdEmailCreate.Value = dataItem["Email"].Text;
                radcomboASP.SelectedValue = dataItem["ASPNo"].Text;
                lbStatus.Text = "";
                txtStorega.Text = dataItem["HardcopyStored"].Text;
                chkDighitalsised.Checked=cls.cToBool(dataItem["Dighitalsised"].Text);
                radnumAmendment.Value=cls.cToDouble(dataItem["Amendment"].Text);
                radnumRenewal.Value = cls.cToDouble(dataItem["Renewal"].Text);
                try
                {
                    dropLegal.SelectedValue = dataItem["LegalReview"].Text;
                    dropFinance.SelectedValue = dataItem["FinanceReview"].Text;
                    ddlFinanceChange.SelectedValue = dataItem["FinanceReview"].Text;
                    ddlLegalChange.SelectedValue = dataItem["LegalReview"].Text;
                }
                catch { }
                //.SelectedValue = editedItem["FinanceReview"].Text;
                //AppnoteLegal_Finance
              //  TextBox ykien = (TextBox)dataItem["Appnote"].FindControl("txtYKienGrid");
                txtAppnote.Text = dataItem["Appnote"].Text;
               
               
                LoadNote(dataItem["ContractNo"].Text);
              //  btSave.Visible = false;
              //  btSubmit.Visible = false;
            }
            btSave.Visible = false;
            btSubmit.Visible = false;
        }
        private void LoadNote(string docno)
        {
            RadGrid2.Visible=true;
            RadGrid2.DataSource=null;
              DataTable tbl = cls.GetDataTable("sp_loadAppInfo", "@Docno", docno);
                RadGrid2.DataSource = tbl;
                RadGrid2.DataBind();
        }
        protected void btCancel_Click(object sender, EventArgs e)
        {
            newDo();
            // ReLoadCV();
            RadGrid1.EditIndexes.Clear();

            ReLoadCV();
            btCreate.Visible = true;
        }
        protected void btExpand1_Click(object sender, EventArgs e)
        {
            dvParent.Visible = !dvParent.Visible;
            if (dvParent.Visible)
            {
                btExpand1.Text = "-";
                btExpand1.ToolTip = "Collapse";
                RadGrid2.Width = 800;
            }
            else
            {
                btExpand1.Text = "+";
                btExpand1.ToolTip = "Expand";
                RadGrid2.Width = 800;
            }
        }
        protected void btFinal_Click(object sender, EventArgs e)
        {
            if (txtConctNoFinal.Text.Trim() == "")
            {
                MsgBox1.AddMessage("You must fill in Contract final", uc.ucMsgBox.enmMessageType.Error);
                txtConctNoFinal.Focus();
            }
            else
            {
                Save(false);
                if (cls.bCapNhat(new string[] { "@Docno", "@DocStorage", "@ClosedBy",  "@ContractNoLegal" }, new object[] { txtContractNo.Text, txtStorega.Text.Trim(), Session["username"], txtConctNoFinal.Text.Trim() }, "sp_UpdateClosedContract") == true)
                {
                    LoadCV();
                    MsgBox1.AddMessage("Final Contract has been saved", uc.ucMsgBox.enmMessageType.Success);

                }
            }
        }
        protected void btSentLegal_Click(object sender, EventArgs e)
        {
            if (dropLegal.SelectedValue != cls.cToString(Session["email"]))
            {
                MsgBox1.AddMessage("You can't sent to other pepole to review", uc.ucMsgBox.enmMessageType.Error);
            }
            else
            {
                bool kq = cls.bCapNhat(new string[] { "@docno", "@olduser", "@newuser" }, new object[] { txtContractNo.Text, Session["email"], ddlLegalChange.SelectedValue }, "sp_UpdatePersonLegalReview");
                if (kq == true)
                {
                    clsSys sys = new clsSys();
                    bool kq1 = sys.SendMailASP(ddlLegalChange.SelectedValue, Session["email"] + ";" + hdEmailCreate.Value, "Contract review", "You recieved contract review number " + txtContractNo.Text + " from " + Session["email"] + "<br/> Please login https://fin.maricosea.com system to review");
                    if (kq1 == true)
                    {
                        LoadCV();
                        MsgBox1.AddMessage("Contract has been sent to " + ddlLegalChange.SelectedValue + " to review", uc.ucMsgBox.enmMessageType.Success);
                    }
                    else
                    {
                        MsgBox1.AddMessage("Can't send contract to " + ddlLegalChange.SelectedValue + " to review, Please check email address", uc.ucMsgBox.enmMessageType.Error);
                    }
                }
                else
                {
                    MsgBox1.AddMessage("Can't update pepole review contract, Please selected the contract or check contract status", uc.ucMsgBox.enmMessageType.Error);
                }
            }
        }
        protected void btSentFinance_Click(object sender, EventArgs e)
        {
            if (dropFinance.SelectedValue != cls.cToString(Session["email"]))
            {
                MsgBox1.AddMessage("You can't sent to other pepole to review", uc.ucMsgBox.enmMessageType.Error);
            }
            else
            {
                bool kq = cls.bCapNhat(new string[] { "@docno", "@olduser", "@newuser" }, new object[] { txtContractNo.Text, Session["email"], ddlFinanceChange.SelectedValue }, "sp_UpdatePersonFinanceReview");
                if (kq == true)
                {
                    clsSys sys = new clsSys();
                    bool kq1 = sys.SendMailASP(ddlFinanceChange.SelectedValue, Session["email"] + ";" + hdEmailCreate.Value, "Contract review", "You recieved contract review number " + txtContractNo.Text + " from " + Session["email"] + "<br/> Please login https://fin.maricosea.com system to review");
                    if (kq1 == true)
                    {
                        LoadCV();
                        MsgBox1.AddMessage("Contract has been sent to " + ddlFinanceChange.SelectedValue + " to review", uc.ucMsgBox.enmMessageType.Success);
                    }
                    else
                    {
                        MsgBox1.AddMessage("Can't send contract to " + ddlFinanceChange.SelectedValue + " to review, Please check email address", uc.ucMsgBox.enmMessageType.Error);
                    }
                }
                else
                {
                    MsgBox1.AddMessage("Can't update pepole review contract, Please selected the contract or check contract status", uc.ucMsgBox.enmMessageType.Error);
                }
            }
        }
        //private void LoadVendor()
        //{
        //    if (formload == false)
        //    {
        //        DataTable tblcost = cls.GetDataTable("sp_getOrgByCostcenter", "@costcenter", dropOrg.SelectedValue);
        //        if (tblcost.Rows.Count > 0)
        //        {
        //            string PurOrg = cls.cToString0(tblcost.Rows[0]["PurOrg"]);
        //            Session["PurOrg"] = PurOrg;
        //            vendor1.PurOrg = PurOrg;
        //        }
        //        else
        //        {
        //            Session["PurOrg"] = "ALL";
        //        }
        //        vendor1.FLoad();
        //    }
        //}
        protected void dropOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            FLoadVendor(dropOrg.SelectedValue);
            Session["PurOrg"] = dropOrg.SelectedValue;
            //if (formload == false)
            //{
            //    DataTable tblcost = cls.GetDataTable("sp_getOrgByCostcenter", "@costcenter", dropOrg.SelectedValue);
            //    if (tblcost.Rows.Count > 0)
            //    {
            //        string PurOrg = cls.cToString0(tblcost.Rows[0]["PurOrg"]);
            //        Session["PurOrg"] = PurOrg;
            //        vendor1.PurOrg = PurOrg;
            //    }
            //    else
            //    {
            //        Session["PurOrg"] = "ALL";
            //    }
            //    vendor1.FLoad();
            //}
        }
        protected void btRefresh_Click(object sender, EventArgs e)
        {
            LoadCV();
        }
        protected void btSaveArchive_Click(object sender, EventArgs e)
        {
            if (radcmbVendor.SelectedValue == "")
            {
                MsgBox1.AddMessage("Vendor has been not create!", uc.ucMsgBox.enmMessageType.Error);
            }
            else
            {
                if (txtContent.Text.Trim() == "")
                {
                    MsgBox1.AddMessage("Please fill in Brief content", uc.ucMsgBox.enmMessageType.Error);
                }
                else
                {
                    SaveArchive();
                }
            }
        }
    }
}