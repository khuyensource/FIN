using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
namespace MaricoPay
{
    public partial class DocDefault : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
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
                        if (hdType.Value.ToLower() == "di")
                        {
                            ucVendor1.FLoad();
                            ucVendor1.Values = cls.cToString(Session["vendorname"]);
                        }
                        else
                        {
                            ucVendor2.FLoad();
                            ucVendor2.Values = cls.cToString(Session["vendorname"]);
                        }
                    }

                }
                else
                {
                    if (eventTarget == "orgPostBack")
                    {
                        if (Session["orgname"] != null)
                        {
                          //  ucVendor1.FLoad();
                            if (hdType.Value.ToLower() == "di")
                            {
                                LoadOrg(hdType.Value);
                                dropOrg.SelectedValue = cls.cToString(Session["orgname"]);
                            }
                            else
                            {
                                LoadOrgGuiDen();
                                dropGuiDen.SelectedValue = cls.cToString(Session["orgname"]);
                            }
                        }

                    }
                }

            }

            else
            {
                if (Session["username"] != null /*&& Request.Form["us"] != null*/)
                // if (Request.QueryString["us"] != null)//click vao avatar
                {
                    string type = !string.IsNullOrEmpty(Request.QueryString["type"]) ? Request.QueryString["type"] : "";
                    hdType.Value = type;
                    ucVendor1.Visible = false;
                    LoadOrg(type);
                    LoadDocType();
                     string DOACode = cls.GetString("sp_getDOACodeFormCode", new string[] { "@Org_PK" }, new object[] { dropOrg.SelectedValue });
                    LoadPepoleNexStep(DOACode,radioType.SelectedValue);
                    LoadCV();
                    openpp.HRef = "org.aspx?type=" + hdType.Value;
                    switch (type.ToUpper())
                    {
                        case "DI":
                            newDo();
                            //dropNguoiXL.Enabled = true;
                            lbTitle.Text = "Công Văn Đến<br/>Incoming Document";
                            lbOrg.Text = "Gửi từ<br/>From";
                            lbNgayNhan.Text = "Ngày nhận<br/>Recieved date";
                            lbGuiDen.Visible = false;
                            dropGuiDen.Visible = false;
                            RadGrid1.MasterTableView.GetColumn("OrgSentTo").Visible = false;
                            RadGrid1.MasterTableView.GetColumn("SentLegal").Visible = false;
                           
                            break;
                        case "DO":
                            newDo();
                           // dropNguoiXL.Enabled = true;
                            RadGrid1.MasterTableView.GetColumn("OrgSentTo").Visible = true;
                            RadGrid1.MasterTableView.GetColumn("SentLegal").Visible = false;
                            RadGrid1.MasterTableView.GetColumn("ReceiveDate").HeaderText = "Ngày gửi<br/>Submitted date";
                            lbTitle.Text = "Công Văn Đi<br/>Outgoing Document";
                            lbOrg.Text = "Gửi đi bởi<br/>From department";
                            lbNgayNhan.Text = "Ngày gửi<br/>Submitted date";
                            lbGuiDen.Visible = true;
                            dropGuiDen.Visible = true;
                            lbKyHieu.Visible = false;
                            txtKyHieu.Visible = false;
                            lbGuiDen.Text = "Gửi đến<br/>To";
                            LoadOrgGuiDen();
                            break;
                        case "APPDI":
                            RadGrid2.Width = 800;
                            lbTitle.Text = "Xem xét Công Văn Đến<br/>Review Incoming Document";
                            dvParent.Visible = false;
                            btExpand1.Text = "+";
                            btExpand1.ToolTip = "Expand";
                            dropNguoiXL.Enabled = false;
                            btCreate.Visible = false;
                            txtDocno.Text = "";
                            lbDocno.Visible = false;
                            txtDocno.Visible = false;
                            btSave.Visible = false;
                            btSubmit.Visible = false;
                            btExpand1.Visible = false;
                            btCancel.Visible = false;
                            lbOrg.Text = "Gửi từ<br/>From";
                          //  lbGuiDen.Visible = true;
                         //   lbGuiDen.Text = "Gửi đến";
                            lbNgayNhan.Text = "Ngày nhận<br/>Recieved date";
                            hdFlagg.Value = "";
                            lbStatus.Text = "";
                          //  LoadOrgGuiDen();
                            //OrgSentTo
                            //SentLegal
                              RadGrid1.MasterTableView.GetColumn("OrgSentTo").Visible = false;
                           // RadGrid1.MasterTableView.GetColumn("SentLegal").Visible = false;

                           // RadGrid1.MasterTableView.GetColumn("OrgSentTo").Visible = true;
                            RadGrid1.MasterTableView.GetColumn("SentLegal").Visible = true;
                            RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
                            RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                            RadGrid1.MasterTableView.GetColumn("ActionColumn").Visible = false;
                           
                            Session["EmailDOADoc"]= cls.GetDataTable("sp_getEmailDoc");
                            break;
                        case "APPDO":
                            RadGrid2.Width = 800;
                            lbTitle.Text = "Xem xet Công Văn Đi<br/>Review Outgoing Document";
                            dvParent.Visible = false;
                            btExpand1.Text = "+";
                            btExpand1.ToolTip = "Expand";
                            dropNguoiXL.Enabled = false;
                            btCreate.Visible = false;
                            txtDocno.Text = "";
                            lbDocno.Visible = false;
                            txtDocno.Visible = false;
                            btSave.Visible = false;
                            btSubmit.Visible = false;
                            btExpand1.Visible = false;
                            btCancel.Visible = false;
                             lbKyHieu.Visible = false;
                            txtKyHieu.Visible = false;
                            lbOrg.Text = "Gửi từ<br/>From";
                              lbGuiDen.Visible = true;
                               lbGuiDen.Text = "Gửi đến<br/>To";
                            lbNgayNhan.Text = "Ngày gửi<br/>Submitted date";
                            hdFlagg.Value = "";
                            lbStatus.Text = "";
                            LoadOrgGuiDen();
                            //OrgSentTo
                            //SentLegal
                              RadGrid1.MasterTableView.GetColumn("OrgSentTo").Visible = true;
                          //  RadGrid1.MasterTableView.GetColumn("SentLegal").Visible = false;
                           // RadGrid1.MasterTableView.GetColumn("OrgSentTo").Visible = true;
                            RadGrid1.MasterTableView.GetColumn("SentLegal").Visible = true;
                            RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
                            RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                            RadGrid1.MasterTableView.GetColumn("ActionColumn").Visible = false;
                         
                            Session["EmailDOADoc"] = cls.GetDataTable("sp_getEmailDoc");
                            break;
                    }
                   
                   
                    //btCancel.Visible = false;
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
            

        }
        //public string fgetText(object loaicv)
        //{
        //    string kq = "";
        //    switch (loaicv.ToString())
        //    {
        //        case "-1":
        //            kq = "Submit";
        //            break;
        //        case "3":
        //            kq = "Submit";
        //            break;
        //        default:
        //            kq = "";
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
        public bool isShowClosed(object status,object statususer)
        {
            bool kq = false;
            if (status.ToString() == "1" && statususer.ToString()!="10")
            {
                kq=true;
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
        //public string getHref()
        //{
        //   // string kq = "vendor.aspx";
        //    openpp.HRef = "vendor.aspx";
        //    if (radioType.SelectedValue.ToLower() == "gov")
        //    {
        //        openpp.HRef = "org.aspx";
        //       // kq = "org.aspx";
        //    }
        //    //return kq;
            
        //}
        //sp_getDocType
        private void LoadOrg(string type)
             // private void LoadOrg(string type,string Org)
        {
            DataTable tbl = new DataTable();
            tbl = cls.GetDataTable("sp_getOrg", new string[] { "@type" }, new object[]{type});
            //tbl = cls.GetDataTable("sp_getOrg", new string[] { "@type", "@Org" }, new object[] { type, Org });
            dropOrg.DataSource = tbl;
            dropOrg.DataBind();
        }
        private void LoadOrgGuiDen()
        {
            DataTable tbl = new DataTable();
            
                tbl = cls.GetDataTable("sp_getOrg", new string[]{"@type","@Org"}, new object[]{"DI",radioType.SelectedValue});
           
            dropGuiDen.DataSource = tbl;
            dropGuiDen.DataBind();
        }
        private void LoadDocType()
        {
            DataTable tbl = cls.GetDataTable("sp_getDocType", new string[] { "@type", "@Org" }, new object[] { hdType.Value,radioType.SelectedValue});
            dropType.DataSource = tbl;
            dropType.DataBind();
        }
        private void LoadPepoleNexStep(string DOA,string GovBiz)
        {
            DataTable tbl = cls.GetDataTable("sp_getDocDOA", new string[] { "@DOACode", "@GovBiz" }, new object[] { DOA, GovBiz });
            dropNguoiXL.DataSource = tbl;
            dropNguoiXL.DataBind();
        }
        //private void LoadOrgDO()
        //{
        //    DataTable tbl = new DataTable();
        //    tbl = cls.GetDataTable("sp_getOrg", "@type", "DI");
        //    dropNguoiXL.DataSource = tbl;
        //    dropNguoiXL.DataBind();
        //}
        protected void dropOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
                string DOACode = cls.GetString("sp_getDOACodeFormCode", new string[] { "@Org_PK" }, new object[] { dropOrg.SelectedValue });
                LoadPepoleNexStep(DOACode,radioType.SelectedValue);
        }
        private void Save(bool submit)
        {

            if (radDateCV.IsEmpty)
            {
                MsgBox1.AddMessage("Vui lòng nhập ngày công văn<br/>Please fill in 'Document date:'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (txtContent.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Vui lòng nhập nội dung tóm tắt<br/>Please fill in 'Brief content:'", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            string orgstring = "";
            string orgstringname = "";
            string orgstringden = "";
            string orgstringdenname = "";
            if (hdType.Value.ToLower() == "di")
            {
                if (radioType.SelectedValue.ToLower() == "gov")
                {
                    orgstring = dropOrg.SelectedValue;
                    orgstringname = dropOrg.SelectedItem.Text;
                }
                else
                {
                    orgstring = ucVendor1.Values;
                    orgstringname = ucVendor1.Text;
                }
            }
            else
            {
                orgstring = dropOrg.SelectedValue;
                orgstringname = dropOrg.SelectedItem.Text;
               
            }
            if (hdType.Value.ToLower() == "do")
            {
                if (radioType.SelectedValue.ToLower() == "gov")
                {
                    orgstringden = dropGuiDen.SelectedValue;
                    orgstringdenname = dropGuiDen.SelectedItem.Text;
                }
                else
                {
                    orgstringden = ucVendor2.Values;
                    orgstringdenname = ucVendor2.Text;
                }
            }
            else
            {
                orgstringden = "0";
            }
            if (cls.cToString(orgstring) == "")
            {
                MsgBox1.AddMessage("Vui lòng chọn gửi từ<br/>Please select From", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            string nguoixuly = dropNguoiXL.SelectedValue;
            if (dropNguoiXL.SelectedValue.ToLower() == "other")
            {
                nguoixuly = txtEmailOther.Text.Trim();
                clsSys sys=new clsSys();
                if (sys.CheckEmail(nguoixuly)==false)
                {
                    MsgBox1.AddMessage("Vui lòng nhập chính xác email người xử lý<br/>Please fill in Email asign to", uc.ucMsgBox.enmMessageType.Error);
                    return;
                }
            }

            string content = "";
            if (hdFlagg.Value == "edit")
            {
                string filename = "";
                if (FileUpload1.HasFile)
                {
                    filename = upload(txtDocno.Text);
                }
               
                bool binsert = cls.bThem(new string[] { "@Type", "@DocType_FK", "@DocNo", "@DocnoReceived", "@DocDate", "@SentFrom", "@SentTo", "@AttachedFile", "@DocStorage", "@Org_FK", "@Approval", "@AppDate", "@Content", "@ReceiveDate", "@UserCreate", "@AppNote", "@Level", "@EmailApp","@GovBiz" }
                    , new object[] { hdType.Value, dropType.SelectedValue, txtDocno.Text, txtKyHieu.Text.Trim(), radDateCV.SelectedDate, dropOrg.SelectedItem.Text, orgstringden, filename, txtNoiLuu.Text.Trim(),/* dropOrg.SelectedValue*/orgstring, txtNguoiKy.Text.Trim(), radDateCV.SelectedDate, txtContent.Text.Trim(), radDateNhan.SelectedDate, Session["username"], txtYKien.Text.Trim(), dropLevel.SelectedValue, nguoixuly, radioType.SelectedValue }, "sp_insertDoc");
                if (binsert == true)
                {

                    if (submit == true)
                    {
                        content = "Dear Sir/Madam,<br/>Please kindly find the attached & following note of the in-coming documentation<br/>- No." + txtKyHieu.Text.Trim() + "<br/>- Received on: " + radDateCV.SelectedDate.Value.ToString("dd-MMM-yy") + "<br/>- From: " + orgstringname + ".<br/>- Brief content: " + txtContent.Text.Trim() + "<br/>-----------------------<br/>" +
"Kính gởi Ông/Bà.<br/>Xin vui lòng xem xét các hồ sơ công văn đính kèm <br/>- Số " + txtKyHieu.Text.Trim() + "<br/>- Được nhận vào ngày " + radDateCV.SelectedDate.Value.ToString("dd-MM-yy") + "<br/>- Từ " + orgstringname + "<br/>- Nội dung chính: " + txtContent.Text.Trim() + ". <br/>";
                        SendEmailSubmit(txtDocno.Text, nguoixuly, filename == "" ? hdFilename.Value : filename, cls.cToString(Session["email"]), txtYKien.Text, hdType.Value, content);
                    }
                    else
                    {
                        LoadCV();
                        MsgBox1.AddMessage("Lưu thành công<br/>Saved successfully!", uc.ucMsgBox.enmMessageType.Success);
                    }
                }
                else
                {
                    MsgBox1.AddMessage("Lưu thất bại<br/>Failed to save", uc.ucMsgBox.enmMessageType.Error);
                }

            }
            else
            {
                if (!FileUpload1.HasFile)
                {
                    MsgBox1.AddMessage("Phải có tập tin đính kèm<br/>You must attach a file!", uc.ucMsgBox.enmMessageType.Error);
                    return;
                }
                string docno = GenalCode();
                string filename = upload(docno);
                if (filename != "")
                {
                    bool binsert = cls.bThem(new string[] { "@Type", "@DocType_FK", "@DocNo", "@DocnoReceived", "@DocDate", "@SentFrom", "@SentTo", "@AttachedFile", "@DocStorage", "@Org_FK", "@Approval", "@AppDate", "@Content", "@ReceiveDate", "@UserCreate", "@AppNote", "@Level", "@EmailApp","@GovBiz" }
                        , new object[] { hdType.Value, dropType.SelectedValue, docno, txtKyHieu.Text.Trim(), radDateCV.SelectedDate, orgstringname, orgstringden, filename, txtNoiLuu.Text.Trim(), /* dropOrg.SelectedValue*/orgstring, txtNguoiKy.Text.Trim(), radDateCV.SelectedDate, txtContent.Text.Trim(), radDateNhan.SelectedDate, Session["username"], txtYKien.Text.Trim(), dropLevel.SelectedValue, nguoixuly, radioType.SelectedValue }, "sp_insertDoc");
                    if (binsert == true)
                    {
                        if (submit == true)
                        {
                            content = "Dear Sir/Madam,<br/>Please kindly find the attached & following note of the in-coming documentation <br/>- No." + txtKyHieu.Text.Trim() + " <br/>- Received on " + radDateCV.SelectedDate.Value.ToString("dd-MNM-yy") + " <br/>-From " + orgstringname + " <br/>- Brief content: " + txtContent.Text.Trim() + ".<br/>" + "-----------------------<br/>" +
"Kính gởi Ông/Bà.<br/>Xin vui lòng xem xét các hồ sơ công văn đính kèm <br/>- Số " + txtKyHieu.Text.Trim() + " <br/>- Được nhận vào ngày " + radDateCV.SelectedDate.Value.ToString("dd-MM-yy") + " <br/>- Từ " + orgstringname + " <br/>- Nội dung chính: " + txtContent.Text.Trim() + ". <br/>";
                            SendEmailSubmit(docno, nguoixuly, filename, cls.cToString(Session["email"]), txtYKien.Text.Trim(), hdType.Value, content);
                        }
                        else
                        {
                            LoadCV();
                            hdFlagg.Value = "edit";
                            lbStatus.Text = "Edit";
                            txtDocno.Text = docno;
                            hdFilename.Value = filename;
                            MsgBox1.AddMessage("Lưu thành công<br/>Saved successfully!", uc.ucMsgBox.enmMessageType.Success);
                        }
                    }
                    else
                    {
                        MsgBox1.AddMessage("Lưu thất bại<br/>Failed to save", uc.ucMsgBox.enmMessageType.Error);
                    }
                }
                else
                {
                    MsgBox1.AddMessage("Lưu tập tin đính kèm thất bại<br/>Failed to save the attached file, Please try again", uc.ucMsgBox.enmMessageType.Error);
                    return;
                }
            }
        }
        protected void btSave_Click(object sender, EventArgs e)
        {

            Save(false);

        }
        private string upload(string docno)
        {
            string kq = "";
            if (FileUpload1.HasFile)
            {
                try
                {
                    int vt1 = FileUpload1.FileName.LastIndexOf(".");
                    int vtcanlay = vt1;
                    int len = FileUpload1.FileName.Length;
                    string extention = FileUpload1.FileName.Substring(vtcanlay, len - vtcanlay);
                    string filename = "";
                    filename = docno.Replace('/','-');
                    filename = filename + extention;
                    //HinhBia.SaveAs(Server.MapPath("../" + ConfigurationManager.AppSettings["hinhbia_sanpham"].ToString()).ToString() + ANHBIA);
                    string sFolderPath = Server.MapPath("Upload/"+hdType.Value+"/" + filename);
                    if (System.IO.File.Exists(sFolderPath) == true)
                        System.IO.File.Delete(sFolderPath);
                    //resize
                    //  HttpPostedFile pf = FileUpload1.PostedFile;
                    try
                    {
                        FileUpload1.SaveAs(sFolderPath);
                        kq = filename;
                    }
                    catch {
                        kq = "";
                    
                    }

                }
                catch 
                {
                    kq= "";
                }
            }
            else
            {
                kq= "";
            }
            return kq;
        }
        private string GenalCode()
        {
            string code = radDateNhan.SelectedDate.Value.Year.ToString() + "/"+hdType.Value + "/" ;
            DataTable tbl=cls.GetDataTable("sp_GenaCodeDoc",new string[]{"@code"},new object[]{code});
            int rows = 0;
            if (tbl.Rows.Count > 0)
            {
                rows = cls.cToInt(tbl.Rows[0][0]);
            }
            rows = rows + 1;
            code = code + rows.ToString();
            Session["NewDocNo"]=code;
            return code;
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
        private void LoadCV()
        {
            RadGrid1.EditIndexes.Clear();
            RadGrid1.DataSource = null;
            DataTable tbl = cls.GetDataTable("sp_LoadCV", new string[] { "@username", "@type" }, new object[] { Session["username"], hdType.Value });
            RadGrid1.DataSource = tbl;
            ViewState["CurrentTable"] = tbl;
            RadGrid1.DataBind();
           
        }
        protected void RadGrid1_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = (GridEditableItem)e.Item;

            string status = editedItem["status"].Text;
            if (status == "2")
            {
                MsgBox1.AddMessage("Chứng đang chờ xem xét không được phép xóa<br/>You can't delete this document because it has been submitted", uc.ucMsgBox.enmMessageType.Error);
                ReLoadCV();
                return;
            }
            if (status == "1")
            {
                MsgBox1.AddMessage("Chứng từ đã được xem xét không được phép xóa<br/>You can't delete this document because it has been reviewed", uc.ucMsgBox.enmMessageType.Error);
                ReLoadCV();
                return;
            }

          //  txtDocno.Text = editedItem["Docno"].Text;
            if (cls.bXoa(new string[] { "@Docno" }, new object[] { editedItem["Docno"].Text }, "sp_DeleteDocument") == true)
            {
                LoadCV();
                MsgBox1.AddMessage("Chứng từ đã được xóa thành công<br/>Document has been deleted", uc.ucMsgBox.enmMessageType.Success);
            }
           // ReLoadCV();

        }



        protected void RadGrid1_EditCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = (GridEditableItem)e.Item;
          
            string status = editedItem["status"].Text;
            if (status == "2")
            {
                MsgBox1.AddMessage("Chứng từ đang chờ xem xét không được phép sửa đổi<br/>You can't edit this document because it has been submitted", uc.ucMsgBox.enmMessageType.Error);
                ReLoadCV();
                return;
            }
            if (status == "1")
            {
                MsgBox1.AddMessage("Chứng từ đã được xem xét không được phép sửa đổi<br/>You can't edit this document because it has been reviewed", uc.ucMsgBox.enmMessageType.Error);
                ReLoadCV();
                return;
            }
            
            txtDocno.Text = editedItem["Docno"].Text;
            radioType.SelectedValue = editedItem["GovBiz"].Text;
            radioType_SelectedIndexChanged(sender, e);
            //LoadOrg(hdType.Value);
            btCancel.Visible = true;
            btCreate.Visible = false;
            btSave.Visible = true;
            btSubmit.Visible = true;
            hdFlagg.Value = "edit";
            lbStatus.Text = "Edit";
            hdType.Value = editedItem["DIDO"].Text;
            if (hdType.Value.ToLower() == "di")
            {
                if (radioType.SelectedValue.ToLower() == "gov")
                {
                    dropOrg.SelectedValue = editedItem["MaOrg"].Text;
                }
                else
                {
                    ucVendor1.Values = editedItem["MaOrg"].Text;
                }
            }
            else
            {
                dropOrg.SelectedValue = editedItem["MaOrg"].Text;
                if (radioType.SelectedValue.ToLower() == "gov")
                {
                    dropGuiDen.SelectedValue = editedItem["SentTo"].Text;
                }
                else
                {
                    ucVendor2.Values = editedItem["SentTo"].Text;
                }
            }
            dropType.SelectedValue = editedItem["MaLoaiCV"].Text;
            txtKyHieu.Text = editedItem["Docnoreceived"].Text;
           // radDateCV.SelectedDate = RadGrid1.Items[index]["DocDate"].Text;
            DateTime? pkr = cls.cToDateTime(editedItem["DocDate"].Text);
            radDateCV.SelectedDate = pkr;
            pkr = cls.cToDateTime(editedItem["ReceiveDate"].Text);
            radDateNhan.SelectedDate = pkr;
            string DOACode = cls.GetString("sp_getDOACodeFormCode", new string[] { "@Org_PK" }, new object[] { dropOrg.SelectedValue });
            LoadPepoleNexStep(DOACode,radioType.SelectedValue);
            try
            {
                dropNguoiXL.SelectedValue = editedItem["EmailApp"].Text;
                txtEmailOther.Visible = false;
            }
            catch {
                dropNguoiXL.SelectedValue = "Other";
                txtEmailOther.Visible = true;
                txtEmailOther.Text = editedItem["EmailApp"].Text;
            }
            dropLevel.SelectedValue = editedItem["Level"].Text;
           
            //if (hdType.Value == "DO")
            //{
            //    dropGuiDen.SelectedValue = editedItem["SentTo"].Text;
            //}
           
            txtNguoiKy.Text = editedItem["Approval"].Text;
            txtContent.Text = editedItem["Content"].Text;
            txtNoiLuu.Text = editedItem["DocStorage"].Text;
            txtYKien.Text = editedItem["AppNote"].Text;
            hdFilename.Value = editedItem["AttachedFile"].Text;
            ReLoadCV();
        }

        //protected void RadGrid1_Init(object sender, EventArgs e)
        //{
        //    if (ViewState["CurrentTable"] != null)
        //    {
        //        RadGrid1.DataSource = (DataTable)ViewState["CurrentTable"];
        //        RadGrid1.DataBind();
        //    }
        //    else
        //    {
        //        LoadCV();
        //    } 
        //}

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string status = "";
            GridEditableItem editedItem;
            DateTime? pkr;
            string content = "";
            switch (e.CommandName)
            {
                //case Telerik.Web.UI.RadGrid.InitInsertCommandName:
                //    RG.MasterTableView.ClearEditItems();
                //    e.Item.OwnerTableView.EditFormSettings.UserControlName = "../../../ControlUser/insert/Insertinloc.ascx";
                //    e.Item.OwnerTableView.InsertItem();
                //    if (ViewState["CurrentTable"] != null)
                //    {
                //        RG.DataSource = (DataTable)ViewState["CurrentTable"];
                //        RG.DataBind();
                //    }
                //    else
                //    {
                //        fLoad();
                //    } break;
                    
                case "Submit":
                    
                    editedItem = (GridEditableItem)e.Item;
             status = editedItem["status"].Text;
                    if (status == "0" || status == "3")//chi nhưng thang chưa submit hoac bi rejected thi moi duoc trinh ky
                    {
                        string docno = editedItem["Docno"].Text;
                        string emailapp = editedItem["EmailApp"].Text;
                        TextBox ykien = (TextBox)editedItem["AppNote"].FindControl("txtYKienGrid");
                       pkr = cls.cToDateTime(editedItem["DocDate"].Text);

                       content = "Dear Sir/Madam,<br/>Please kindly find the attached & following note of the in-coming documentation <br/>- No." + editedItem["Docnoreceived"].Text + " <br/>- Received on " + pkr.Value.ToString("dd-MMM-yy") + " <br/>- From " + editedItem["Org"].Text + " <br/>- Brief content: " + editedItem["Content"].Text + ".<br/>" + "-----------------------<br/>" +
"Kính gởi Ông/Bà.<br/>Xin vui lòng xem xét các hồ sơ công văn đính kèm <br/> Số " + editedItem["Docnoreceived"].Text + "<br/>- Được nhận vào ngày " + pkr.Value.ToString("dd-MM-yy") + "<br/> Từ " + editedItem["Org"].Text + "<br/> - Nội dung chính: " + editedItem["Content"].Text + ". <br/>";
                        SendEmailSubmit(docno, emailapp, editedItem["AttachedFile"].Text, cls.cToString(Session["email"]), ykien.Text.Trim(), editedItem["DIDO"].Text, content);
                       
                    }
                    else
                    {
                        MsgBox1.AddMessage("Chứng từ đã được xem xét hoặc đã trình ký không được phép trình ký tiếp<br/>You can't submit this document because it has been submitted or reviewed", uc.ucMsgBox.enmMessageType.Error);
                        return;
                    }
                    
                    break;
                case "SentLegal":
                    //GridEditableItem editedItem1 = (GridEditableItem)e.Item;
                    //string status1 = editedItem1["status"].Text;
                   editedItem = (GridEditableItem)e.Item;
             status = editedItem["status"].Text;
                    if (status == "1")//chi nhung cong van da duoc approved thi moi chuyen legal
                    {
                        string docno = editedItem["Docno"].Text;
                        TextBox ykien = (TextBox)editedItem["AppNote"].FindControl("txtYKienGrid");
                        DropDownList ddl = (DropDownList)editedItem["SentLegal"].FindControl("dropEmail");
                        string emaillegal = ddl.SelectedValue;// cls.GetString("sp_getEmailLegal");
                       pkr = cls.cToDateTime(editedItem["DocDate"].Text);
                       content = "Dear " + ddl.SelectedItem.Text+ ",<br/>" +
"Please kindly help to review and comment from you view on the in-coming documentation<br/>- No." + editedItem["Docnoreceived"].Text + "<br/> Dated " + pkr.Value.ToString("dd-MMM-yy") + "<br/>- Brief content: " + editedItem["Content"].Text + 
"<br/>---------------------------<br/>"+
"Thân gởi Legal,<br/>"+
"Xin vui lòng xem xét và cho nhận xét về quan điểm của bạn đối với công văn hồ sơ đến<br/>- Số " + editedItem["Docnoreceived"].Text + "<br/>- Ngày " + pkr.Value.ToString("dd-MM-yy") + "<br/>- Nội dung chính: " + editedItem["Content"].Text + ".";
                       //update da sent legal
                       cls.bCapNhat(new string[] { "@Docno", "@username","@status" }, new object[] { docno,Session["username"], 4 }, "sp_updateStastusApp");
                        SendEmailSubmit(docno, emaillegal, editedItem["AttachedFile"].Text, cls.cToString(Session["email"]), ykien.Text.Trim(), editedItem["DIDO"].Text,content);
                        
                    }
                    else
                    {
                        MsgBox1.AddMessage("Chứng từ chưa được trình ký hoặc chưa được xem xét thì không được chuyển cho pháp lý<br/>You can't send this document to other one because it is not submitted or reviewed ", uc.ucMsgBox.enmMessageType.Error);
                        return;
                    }

                    break;
                case "Approve":
                      editedItem = (GridEditableItem)e.Item;
             status = editedItem["status"].Text;
                    if (status == "2")//chi nhung cong van nao dang trinh ky thi moi approve
                    {
                        string docno = editedItem["Docno"].Text;
                        TextBox ykien = (TextBox)editedItem["AppNote"].FindControl("txtYKienGrid");
                        if (approve(docno, ykien.Text) == true)
                        {
                            pkr = cls.cToDateTime(editedItem["DocDate"].Text);
                            cls.bCapNhat(new string[] { "@docno", "@note" }, new object[] { docno, ykien.Text }, "sp_UpdateDocument");
                            content = "Dear Sir/Madam,<br/>" +
"Please kindly be advised that we receive the in-coming documentation>br/>- No." + editedItem["Docnoreceived"].Text + ">br/>- Dated " + pkr.Value.ToString("dd-MMM-yy") + "<br/>- Brief content: " + editedItem["Content"].Text + "<br/>" + "----------------------------<br/>" +
"Thân gởi ông/bà,<br/>" +
"Chúng tôi xác nhận là đã nhận được công văn hồ sơ đến<br/>- Số " + editedItem["Docnoreceived"].Text + "<br/>- Ngày " + pkr.Value.ToString("dd-MM-yy") + "<br/>- Nội dung chính: " + editedItem["Content"].Text;
                            SendEmailApprove(docno, editedItem["Email"].Text, editedItem["AttachedFile"].Text, cls.cToString(Session["email"]), ykien.Text.Trim(), editedItem["DIDO"].Text, content);
                        }
                    }
                    else
                    {
                        MsgBox1.AddMessage("Chứng từ chưa được trình xem xét<br/>You can't approve this document because it is not submitted", uc.ucMsgBox.enmMessageType.Error);
                        return;
                    }

                    break;
                case "Reject":
                      editedItem = (GridEditableItem)e.Item;
             status = editedItem["status"].Text;
                    if (status == "2")//chi nhung cong van nao dang trinh ky thi moi reject
                    {
                        string docno = editedItem["Docno"].Text;
                        TextBox ykien = (TextBox)editedItem["AppNote"].FindControl("txtYKienGrid");
                        if (ykien.Text.Trim() == "")
                        {
                            MsgBox1.AddMessage("Phải nhập lý do trước khi từ chối<br/>You must fill in reason", uc.ucMsgBox.enmMessageType.Error);
                            return;
                        }
                        if (Reject(docno, ykien.Text) == true)
                        {
                            pkr = cls.cToDateTime(editedItem["DocDate"].Text);
                            content = "Dear Requester,<br/>" +
"Please kindly forward the in-coming documentation<br/>- No. " + editedItem["Docnoreceived"].Text + "<br/> Dated " + pkr.Value.ToString("dd-MMM-yy") + " to other department for processing because it is not within our territory in charge.<br/>" +
"-----------------------<br/>" +
"Thân gởi người yêu cầu.<br/>" +
"Xin vui lòng chuyển các hồ sơ công văn đính kèm>br/>- Số " + editedItem["Docnoreceived"].Text + "<br/>- Ngày " + pkr.Value.ToString("dd-MM-yy") + " cho bộ phận liên quan khác để xử lý vì nội dung không nằm trong phạm vi phụ trách của chúng tôi.";
                          
                            cls.bCapNhat(new string[] { "@docno", "@note" }, new object[] { docno, ykien.Text }, "sp_UpdateDocument");
                            SendEmailReject(docno, editedItem["Email"].Text, editedItem["AttachedFile"].Text, cls.cToString(Session["email"]), ykien.Text.Trim(), editedItem["DIDO"].Text, content);
                        }

                    }
                    else
                    {
                        MsgBox1.AddMessage("Chứng từ chưa được trình xem xét<br/>You can't approve this document because it is not submitted", uc.ucMsgBox.enmMessageType.Error);
                        return;
                    }

                    break;
                case "Closed":
                      editedItem = (GridEditableItem)e.Item;
             status = editedItem["status"].Text;
                    if (txtNoiLuu.Text.Trim() == "")
                    {
                        MsgBox1.AddMessage("Phải nhập nơi lưu công văn trước khi đóng<br/>You must fill in storage location", uc.ucMsgBox.enmMessageType.Error);
                        txtNoiLuu.Focus();
                        return;
                    }
                    if (status == "1")//chi nhung cong van nao dang trinh ky thi moi reject
                    {
                        string docno = editedItem["Docno"].Text;
                       
//                        if (Reject(docno, ykien.Text) == true)
//                        {
//                            pkr = cls.cToDateTime(editedItem["DocDate"].Text);
//                            content = "Dear Requester,<br/>" +
//"Please kindly forward the in-coming documentation, No. " + editedItem["Docnoreceived"].Text + ", dated " + pkr.Value.ToString("dd-MM-yy") + " to other department for processing because it is not within our territory in charge.<br/>" +
//"-----------------------<br/>" +
//"Thân gởi người yêu cầu.<br/>" +
//"Xin vui lòng chuyển các hồ sơ công văn đính kèm, số " + editedItem["Docnoreceived"].Text + ", ngày " + pkr.Value.ToString("dd-MM-yy") + " cho bộ phận liên quan khác để xử lý vì nội dung không nằm trong phạm vi phụ trách của chúng tôi.";

                        if (cls.bCapNhat(new string[] { "@Docno", "@DocStorage", "@ClosedBy" }, new object[] { docno, txtNoiLuu.Text.Trim(), Session["username"] }, "sp_UpdateClosedDoc") == true)
                        {
                            LoadCV();
                            MsgBox1.AddMessage("Quá trình xử lý công văn đã hoàn thành<br/>Closed successfully!", uc.ucMsgBox.enmMessageType.Success);
                            
                        }
                            
                        //    SendEmailReject(docno, editedItem["Email"].Text, editedItem["AttachedFile"].Text, cls.cToString(Session["email"]), ykien.Text.Trim(), editedItem["DIDO"].Text, content);
                        //}

                    }
                    else
                    {
                        MsgBox1.AddMessage("Chứng từ chưa được xem xét nên không được đóng<br/>Failed to close", uc.ucMsgBox.enmMessageType.Error);
                        return;
                    }

                    break;
                case Telerik.Web.UI.RadGrid.FilterCommandName:
                    ReLoadCV();
                    break;
                case Telerik.Web.UI.RadGrid.RebindGridCommandName:
                    ReLoadCV();
                    break;
            }
        }
        private bool approve(string docno, string ykien)
        {
            bool kq = cls.bCapNhat(new string[] { "@docno", "@username", "@status", "@DateApp", "@Note" }, new object[] { docno, Session["username"], 1, DateTime.Now, ykien }, "sp_updateAppbyName");
            return kq;
        }
        private bool Reject(string docno, string ykien)
        {
            bool kq = cls.bCapNhat(new string[] { "@docno", "@username", "@status", "@DateApp", "@Note" }, new object[] { docno, Session["username"], 3, DateTime.Now, ykien }, "sp_updateAppbyName");
            return kq;
        }
        private bool submit(string docno, string emailapp,string ykien,string type)
        {
            Guid activationCode = Guid.NewGuid();
          //  string type = hdType.Value;
            bool kq = cls.bThem(new string[] { "@Docno", "@Amount", "@Approval", "@LevelApp", "@Status", "@ApprovedCode", "@Type", "@Note" }, new object[] { docno, 0, emailapp, 1, 0, activationCode, type, ykien }, "sp_insertApproveDoc");
           return kq;
        }
        private void SendEmailSubmit(string docno, string emailapp,string filename,string cc,string ykien,string type,string content)
        {
            bool kq = submit(docno, emailapp, ykien, type);
            if (kq == true)
            {
                clsSys sys = new clsSys();
             
                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                string urldownloadfile = @"" + strUrl + "/Upload/" + type + "/" + filename;
                bool kq1 = sys.SendMailASP(emailapp, cc, "Document submited", content + "<br/>Lưu ý/Note:" + ykien + "<br/> <br/>Please <a href=" + urldownloadfile + "> click here</a> to download attached  file");
                
                if (kq1 == true)
                {
                    MsgBox1.AddMessage("Trình xem xét thành công<br/>Submitted successfully!", uc.ucMsgBox.enmMessageType.Success);
                }
                else
                {
                    cls.bXoa(new string[] { "@Docno", "@username" }, new object[] { docno, emailapp }, "sp_DeleteApproveByEmail");
                   cls.bCapNhat(new string[] { "@Docno", "@username","@status" }, new object[] { docno,Session["username"] ,1 }, "sp_updateStastusApp");
                    MsgBox1.AddMessage("Đã xảy ra lỗi trong quá khi gửi email, vui lòng gửi lại<br/>Email sent to fail, please try again", uc.ucMsgBox.enmMessageType.Error);
                    return;
                }
            }
            else
            {
                MsgBox1.AddMessage("Đã xảy ra lỗi trong quá khi trình xem xét<br/>Failed to submit", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            LoadCV();
        }
        private void SendEmailReject(string docno, string emailcreate, string filename, string cc, string ykien, string type,string content)
        {
                clsSys sys = new clsSys();
              //  bool kq1 = sys.SendMailASPAtt(emailcreate, cc, "Document rejected", content +"<br/>Lưu ý/note: "+ ykien, Server.MapPath(@"~/Upload/" + type + "/" + filename));
                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                string urldownloadfile = @"" + strUrl + "/Upload/" + type + "/" + filename;
                bool kq1 = sys.SendMailASP(emailcreate, cc, "Document rejected", content + "<br/>Lưu ý/Note:" + ykien + "<br/> <br/>Please <a href=" + urldownloadfile + "> click here</a> to download attached  file");
                if (kq1 == true)
                {
                    MsgBox1.AddMessage("Từ chối thành công<br/>Rejected successfully!", uc.ucMsgBox.enmMessageType.Success);
                }
                else
                {
                    bool kq = cls.bCapNhat(new string[] { "@docno", "@username", "@status", "@DateApp", "@Note" }, new object[] { docno, Session["username"], 0, DateTime.Now, ykien }, "sp_updateAppbyName");
                    MsgBox1.AddMessage("Đã xảy ra lỗi trong quá khi gửi email, vui lòng reject lại<br/>Failed to send email, please try again", uc.ucMsgBox.enmMessageType.Error);
                }
           
            LoadCV();
        }
        private void SendEmailApprove(string docno, string emailcreate, string filename, string cc, string ykien, string type,string content)
        {
           
            clsSys sys = new clsSys();
            //bool kq1 = sys.SendMailASPAtt(emailcreate, cc, "Document reviewed", content + "<br/>Lưu ý/Note: ngaynhan"+ ykien, Server.MapPath(@"~/Upload/" + type + "/" + filename));
            String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
            string urldownloadfile = @"" + strUrl + "/Upload/" + type + "/" + filename;
            bool kq1 = sys.SendMailASP(emailcreate, cc, "Document reviewed", content + "<br/>Lưu ý/Note:" + ykien + "<br/> <br/>Please <a href=" + urldownloadfile + "> click here</a> to download attached  file");
            if (kq1 == true)
            {
                MsgBox1.AddMessage("Xem xét thành công<br/>Reviewed successfully!", uc.ucMsgBox.enmMessageType.Success);
            }
            else
            {
                bool kq = cls.bCapNhat(new string[] { "@docno", "@username", "@status", "@DateApp", "@Note" }, new object[] { docno, Session["username"], 0, DateTime.Now, ykien }, "sp_updateAppbyName");
                MsgBox1.AddMessage("Đã xảy ra lỗi trong quá khi gửi email, vui lòng duyệt lại<br/>Fail to sent email, please try again", uc.ucMsgBox.enmMessageType.Error);
                
            }
           
            LoadCV();
        }
        private void newDo()
        {
            hdFlagg.Value = "new";
            lbStatus.Text = "Create new";
            txtKyHieu.Text = "";
            txtNguoiKy.Text = "";
            txtNoiLuu.Text = "";
            txtContent.Text = "";
            txtYKien.Text = "";
            hdFilename.Value = "";
            radDateCV.SelectedDate = DateTime.Now;
            radDateNhan.SelectedDate = DateTime.Now;
            txtDocno.Text = "";// GenalCode();
            btCancel.Visible = false;
            btSave.Visible = true;
            btSubmit.Visible = true;
            RadGrid2.Visible = false;
        }
        protected void btCreate_Click(object sender, EventArgs e)
        {

            newDo();
            
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
           // System.Threading.Thread.Sleep(30000);
            newDo();
           // ReLoadCV();
            RadGrid1.EditIndexes.Clear();

           // ReLoadCV();
            btCreate.Visible = true;
        }

        protected void btSubmit_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(30000);
            Save(true);
        }
        protected void btExpand1_Click(object sender, EventArgs e)
        {
            dvParent.Visible = !dvParent.Visible;
            if (dvParent.Visible)
            {
                btExpand1.Text = "-";
                btExpand1.ToolTip = "Collapse";
                RadGrid2.Width = 300;
                
            }
            else
            {
                btExpand1.Text = "+";
                btExpand1.ToolTip = "Expand";
                RadGrid2.Width = 800;
            }
        }

        protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dataItem = RadGrid1.SelectedItems[0] as GridDataItem;
            if (dataItem != null)
            {
                string name = dataItem["DocStorage"].Text;
                txtNoiLuu.Text = name;
                btSave.Visible = false;
                btSubmit.Visible = false;
                lbStatus.Text = "";
                LoadNote(dataItem["Docno"].Text);
            }   
            //GridEditableItem editedItem = (GridEditableItem)e.Item;
            //string noiluu = editedItem["DocStorage"].Text;
            //txtNoiLuu.Text = noiluu;
        }
        private void LoadNote(string docno)
        {
            RadGrid2.Visible = true;
            RadGrid2.DataSource = null;
            DataTable tbl = cls.GetDataTable("sp_loadAppInfo", "@Docno", docno);
            RadGrid2.DataSource = tbl;
            RadGrid2.DataBind();
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem item = e.Item as GridHeaderItem;
                if (hdType.Value.ToUpper().Contains("DI") == true)
                {
                    item["ReceiveDate"].Text = "Ngày nhận<br/>Recieved date";
                }
                else
                {
                    item["ReceiveDate"].Text = "Ngày gửi<br/>Submitted date";
                }
            } 
            if ((e.Item is GridDataItem) && hdType.Value.ToUpper().Contains("APP") ==true)
            {
                GridDataItem editform = (GridDataItem)e.Item;
                DropDownList ddl = (DropDownList)editform["SentLegal"].FindControl("dropEmail");
                if (isShowSubmitLegal(editform["statusUser"].Text) == true)
                {
                    ddl.DataTextField = "Description";
                    ddl.DataValueField = "Email";
                    if (Session["EmailDOADoc"] != null)
                    {
                        ddl.DataSource = (DataTable)Session["EmailDOADoc"];
                    }
                    else
                    {
                        Session["EmailDOADoc"] = cls.GetDataTable("sp_getEmailDoc");
                        ddl.DataSource = (DataTable)Session["EmailDOADoc"];
                    }
                    ddl.DataBind();
                    ddl.Visible = true;

                }
                else
                {
                    ddl.Visible = false;
                }
            } 
        }

        protected void radioType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadOrg(hdType.Value, radioType.SelectedValue);
            if (hdType.Value.ToLower() == "di")
            {
                if (radioType.SelectedValue.ToLower() == "gov")
                {
                    dropOrg.Visible = true;
                    ucVendor1.Visible = false;
                    string DOACode = cls.GetString("sp_getDOACodeFormCode", new string[] { "@Org_PK" }, new object[] { dropOrg.SelectedValue });
                    LoadPepoleNexStep(DOACode, radioType.SelectedValue);
                    openpp.HRef = "org.aspx?type=" + hdType.Value;

                }
                else
                {
                    dropOrg.Visible = false;
                    ucVendor1.Visible = true;
                    openpp.HRef = "vendor.aspx?type=" + hdType.Value;
                    LoadPepoleNexStep("", radioType.SelectedValue);
                }
            }
            else//cong van di
            {
                if (radioType.SelectedValue.ToLower() == "gov")
                {
                    dropGuiDen.Visible = true;
                    ucVendor2.Visible = false;
                  //  string DOACode = cls.GetString("sp_getDOACodeFormCode", new string[] { "@Org_PK" }, new object[] { dropOrg.SelectedValue });
                  //  LoadPepoleNexStep(DOACode, radioType.SelectedValue);
                    openpp1.HRef = "org.aspx?type=" + hdType.Value;

                }
                else
                {
                    dropGuiDen.Visible = false;
                    ucVendor2.Visible = true;
                   openpp1.HRef = "vendor.aspx?type=" + hdType.Value;
                   // LoadPepoleNexStep("", radioType.SelectedValue);
                }
            }
            LoadDocType();
            
        }

        protected void dropNguoiXL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropNguoiXL.SelectedValue.ToLower() == "other")
            {
                txtEmailOther.Visible = true;
            }
            else
            {
                txtEmailOther.Visible = false;
            }
        }
    }
}