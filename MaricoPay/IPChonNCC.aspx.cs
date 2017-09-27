using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Data;
using System.Data;
using System.IO;

namespace MaricoPay
{
    public partial class IPChonNCC : clsPhanQuyenCaoCap
    {
        clsObj Obj;
        clsSql Sql = new clsSql();
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hfLoai.Value= !string.IsNullOrEmpty(Request.QueryString["loai"]) ? Request.QueryString["loai"] :"0";
                LoadTrangThai();
                LoadIP();
                LoadPR(cls.cToInt(radcomboTrangThai.SelectedValue));
                LoadIPXuLy();
                radcomboPR.SelectedIndex = 0;
                PRChangeIndex();
               // fLoad();
            }
        }
        public int fInt(object value)
        {
            if (value.ToString() == "")
            {
                return 0;
            }
            return int.Parse(value.ToString());
        }
        private void LoadIP()
        {
            DataTable tbl = cls.GetDataTable("sp_getEmailIP");
            RadComboIPUser.DataSource = tbl;
            RadComboIPUser.DataBind();
         
        }
        private void LoadTrangThai()
        {
            DataTable tbl = cls.GetDataTable("sp_IPGetTrangThai");
            radcomboTrangThai.DataSource = tbl;
            radcomboTrangThai.DataBind();
        }
        private void LoadIPXuLy()
        {
            DataTable tbl = cls.GetDataTable("sp_getEmailIPXuLy");
            RadComboIPUserXuLy.DataSource = tbl;
            RadComboIPUserXuLy.DataBind();

        }
        public bool fBool(object value)
        {
            if (value.ToString() == "")
            {
                return false;
            }
            return bool.Parse(value.ToString());
        }
        void fLoadPRDetail(object IPPR)
        {
     
            DataTable tbl = cls.GetDataTable("sp_LoadIPPRDetailByIDPR", new string[] { "@IDPR" }, new object[] { IPPR });
            ViewState["CurrentTable"] = tbl;
            RG.DataSource = tbl;
            RG.DataBind();
        }
        private void LoadPR(int TrangThaiPR)
        {
            //Chi load PR co trang thai=0 (Nguoi de nghi da gui den IP, nhung IP chua trinh ky) va =5 (sep IP da rejected)
            //trangthai=1 la da trinh ky cung load nhug ko duoc sua
            DataTable tbl = cls.GetDataTable("sp_LoadIPPRActiveSubmited", new string[] { "@Username", "@Loai", "@TrangThaiPR" }, new object[] { Session["Username"], hfLoai.Value, TrangThaiPR });
            radcomboPR.DataSource = tbl;
            radcomboPR.DataBind();
        }
      
        protected void RG_CancelCommand(object source, GridCommandEventArgs e)
        {
            if (ViewState["CurrentTable"] != null)
            {
                RG.DataSource = (DataTable)ViewState["CurrentTable"];
                RG.DataBind();
            }
            else
            {
                fLoadPRDetail(radcomboPR.SelectedValue);
            }
        }
        protected void RG_EditCommand(object source, GridCommandEventArgs e)
        {
            if (ViewState["CurrentTable"] != null)
            {
                RG.DataSource = (DataTable)ViewState["CurrentTable"];
                RG.DataBind();
            }
            else
            {
                fLoadPRDetail(radcomboPR.SelectedValue);
            }
        }
        protected void RG_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            if (ViewState["CurrentTable"] != null)
            {
                RG.DataSource = (DataTable)ViewState["CurrentTable"];
                RG.DataBind();
            }
            else
            {
                fLoadPRDetail(radcomboPR.SelectedValue);
            }
        }
        protected void RG_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            if (ViewState["CurrentTable"] != null)
            {
                RG.DataSource = (DataTable)ViewState["CurrentTable"];
                RG.DataBind();
            }
            else
            {
                fLoadPRDetail(radcomboPR.SelectedValue);
            }
        }
        protected void RG_GroupsChanging(object source, GridGroupsChangingEventArgs e)
        {
            if (ViewState["CurrentTable"] != null)
            {
                RG.DataSource = (DataTable)ViewState["CurrentTable"];
                RG.DataBind();
            }
            else
            {
                fLoadPRDetail(radcomboPR.SelectedValue);
            }
        }
        protected void RG_SortCommand(object source, GridSortCommandEventArgs e)
        {
            if (ViewState["CurrentTable"] != null)
            {
                RG.DataSource = (DataTable)ViewState["CurrentTable"];
                RG.DataBind();
            }
            else
            {
                fLoadPRDetail(radcomboPR.SelectedValue);
            }
        }
        protected void RG_DeleteCommand(object source, GridCommandEventArgs e)
        {
            string ID = RG.Items[e.Item.ItemIndex]["IDPRDetail"].Text;
            Obj = new clsObj();
            Obj.Parameter = new string[] { "@IDPRDetail" };
            Obj.ValueParameter = new object[] { ID };
            Obj.SpName = "spDelete_IPPRDetail";
            Sql.fNonGetData(Obj);
            if (Obj.KetQua < 1)
            {
                lbLoi.Text = "<font color='red'>Xóa thất bại. Vui lòng thử lại sau/Delete fail, please try again</font>";
            }
            else
            {
                string filename = RG.Items[e.Item.ItemIndex]["HinhMau"].Text;
                if (filename != "")
                {
                    string sFolderPath = Server.MapPath("Upload/IP/" + filename);
                    if (System.IO.File.Exists(sFolderPath) == true)
                        System.IO.File.Delete(sFolderPath);
                }
                lbLoi.Text = "<font color='blue'>Xóa thành công/Deleted successfully</font>";
            }
            fLoadPRDetail(radcomboPR.SelectedValue);
        }
        private string InsertPR()
        {
            //IPPR_InsertUpdate
           string IDPR = cls.GetString0("IPPR_InsertUpdate", new string[] { "@IDPR", "@NguoiLap",  "@N1Duyet", "@N2Duyet", "@IPUserXacNhan", "@GhiChu", "@HieuLuc" },
                new object[] { radcomboPR.SelectedValue, Session["Username"], "", "", RadComboIPUser.SelectedValue,txtNoiDung.Text.Trim(),1 });
           return IDPR;
        }
        protected void RG_ItemCommand(object source, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "MoRong":
                    RG.MasterTableView.IsItemInserted = false;
                      e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/UpdateIPChonNCC.ascx";
                       // e.Item.OwnerTableView.InsertItem();
                        if (ViewState["CurrentTable"] != null)
                        {
                            RG.DataSource = (DataTable)ViewState["CurrentTable"];
                            RG.DataBind();
                        }
                        else
                        {
                            fLoadPRDetail(radcomboPR.SelectedValue);
                        }
                    break;
                case Telerik.Web.UI.RadGrid.EditCommandName:
                    RG.MasterTableView.IsItemInserted = false;
                    e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/UpdateIPChonNCC.ascx";
                    if (ViewState["CurrentTable"] != null)
                    {
                        RG.DataSource = (DataTable)ViewState["CurrentTable"];
                        RG.DataBind();
                    }
                    else
                    {
                        fLoadPRDetail(radcomboPR.SelectedValue);
                    } break;
                case Telerik.Web.UI.RadGrid.FilterCommandName:
                    if (ViewState["CurrentTable"] != null)
                    {
                        RG.DataSource = (DataTable)ViewState["CurrentTable"];
                        RG.DataBind();
                    }
                    else
                    {
                        fLoadPRDetail(radcomboPR.SelectedValue);
                    } break;
                case Telerik.Web.UI.RadGrid.RebindGridCommandName:
                    fLoadPRDetail(radcomboPR.SelectedValue);
                    break;
            }
        }
        protected void RG_InsertCommand(object source, GridCommandEventArgs e)
        {
            if (txtNoiDung.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Chưa nhập nội dung PR<br/>Please fill in PR content", uc.ucMsgBox.enmMessageType.Error);
            }
            else
            {
                string IDPR = "";
                if (cls.cToDecimal(radcomboPR.SelectedValue) == 0)
                {
                    IDPR = InsertPR();
                    LoadPR(cls.cToInt(radcomboTrangThai.SelectedValue));
                   int idx= radcomboPR.FindItemIndexByValue(IDPR, true);
                   radcomboPR.ClearSelection();
                   radcomboPR.SelectedIndex = idx;
                   radcomboPR.Text = radcomboPR.SelectedValue;
                    PRChangeIndex();
                }
                else
                {
                    IDPR = radcomboPR.SelectedValue;
                }
                lbLoi.Text = "";
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                //  float ID = float.Parse((userControl.FindControl("rnID") as RadNumericTextBox).Value.ToString());
                   RadComboBox cboMaterial = userControl.FindControl("RadComboMaterial") as RadComboBox;
                   cboMaterial.SelectedIndex = cboMaterial.FindItemByText(cboMaterial.Text).Index;
                //  float Material = float.Parse((userControl.FindControl("RadComboMaterial") as RadComboBox).SelectedValue);
                   RadComboBox cboIO = userControl.FindControl("RadComboIO") as RadComboBox;
                   cboIO.SelectedIndex = cboIO.FindItemByText(cboIO.Text).Index;
             
                string kichthuoc = (userControl.FindControl("txtKichThuoc") as TextBox).Text.Trim();
                string chatlieu = (userControl.FindControl("txtChatLieu") as TextBox).Text.Trim();
                string doday = (userControl.FindControl("txtDoDay") as TextBox).Text.Trim();
                CheckBox coden = userControl.FindControl("chkCoDen") as CheckBox;
                double soluong = cls.cToDouble((userControl.FindControl("radnumSoLuong") as RadNumericTextBox).Value);
                //   FileUpload fileupload=userControl.FindControl("FileUpload1") as FileUpload;
                RadAsyncUpload radfileupload = userControl.FindControl("RadAsyncUpload1") as RadAsyncUpload;
                DateTime ngaygiao = (userControl.FindControl("radDateNgayGiao") as RadDatePicker).SelectedDate.Value;
                string noinhan = (userControl.FindControl("txtNoiNhan") as TextBox).Text.Trim();

                string Note = (userControl.FindControl("tbNote") as TextBox).Text.Trim();

                #region Insert
                //if(Material<=0

                string[] bien = new string[] { "@IDPRDetail", "@IDPR", "@IO", "@IDMaterial", "@TenHang", "@KichThuoc", "@ChatLieu", "@DoDay", "@CoDen", "@SoLuong", "@NgayGiao", "@NoiNhan", "@ChiTietKhac" };
                object[] giatri = new object[] { 0, IDPR, cboIO.SelectedValue, cboMaterial.SelectedValue, cboMaterial.SelectedItem.Text, kichthuoc, chatlieu, doday, coden.Checked, soluong, ngaygiao, noinhan, Note };
                // Obj.SpName = "IPPRDetail_InsertUpdate";
                // Sql.fNonGetData(Obj);
                decimal IDPRDetail = cls.cToDecimal(cls.GetString0("IPPRDetail_InsertUpdate", bien, giatri));
                if (IDPRDetail <= 0)
                {
                    lbLoi.Text = "<font color='red'>Thêm thất bại. Vui lòng thử lại sau<br/>Insert fail, please try again</font>";
                }
                else
                {
                   
                    if (radfileupload.UploadedFiles.Count > 0)
                    {
                        
                       string filename = radupload(radfileupload, IDPRDetail);
                        cls.bCapNhat(new string[] { "@IDPRDetail", "@filename" }, new object[] { IDPRDetail, filename }, "sp_IPPRDetailupdateImage");
                    }

                    lbLoi.Text = "<font color='blue'>Thêm thành công/Insert successfully</font>";
                }
                #endregion
                fLoadPRDetail(radcomboPR.SelectedValue);
            }
        }


        protected void RG_UpdateCommand(object source, GridCommandEventArgs e)
        {
            
            if ((hfTrangThai.Value == "1" || hfTrangThai.Value == "4") && hfLoai.Value=="1")
            {
                fLoadPRDetail(radcomboPR.SelectedValue);
                MsgBox1.AddMessage("PR đã được trình ký hoặc đã được duyệt nên không được phép thay đổi<br/>PR alreadly submit, you can not revise", uc.ucMsgBox.enmMessageType.Error);
            }
            else
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                HiddenField hfIDPR = userControl.FindControl("hfIDPR") as HiddenField;
                HiddenField hfIDPRDetail = userControl.FindControl("hfIDPRDetail") as HiddenField;
                HiddenField hfIDPRVendor = userControl.FindControl("hfIDPRVendor") as HiddenField;
                //xoa tat ca NCC da chon truoc neu co
                if (hfIDPRVendor.Value != "")
                {
                    cls.bCapNhat(new string[] { "@IDPR", "@IDPRDetail" }, new object[] { hfIDPR.Value, hfIDPRDetail.Value }, "sp_IPUnselectedVendor");

                    //tim thang NCC nao duoc chon thi update vao DB

                    if (hfIDPRVendor.Value != "0")
                    {

                        if (cls.bCapNhat(new string[] { "@IDPR", "@IDPRDetail", "@IDPRVendor" }, new object[] { hfIDPR.Value, hfIDPRDetail.Value, hfIDPRVendor.Value }, "sp_IPselectedVendor") == true)
                        {
                            lbLoi.Text = "<font color='blue'>Chọn NCC thành công/Vendor selection successfully</font>";
                        }
                        else
                        {
                            lbLoi.Text = "<font color='red'>Không thể lưu NCC được chọn/Can not save vendor selected</font>";
                        }
                    }
                    else
                    {
                        lbLoi.Text = "<font color='blue'>Không có NCC nào được chọn/Please select at least one vendor</font>";
                    }
                }
                else
                {
                    lbLoi.Text = "<font color='blue'>Không có sự thay đổi nào/No change</font>";
                }

                fLoadPRDetail(radcomboPR.SelectedValue);
            }
            
        }
        private string radupload(RadAsyncUpload up, object docno)
        {
            string kq = "";
            // if(   RadUpload1.UploadedFiles.Count>0)
            if (up.UploadedFiles.Count > 0)
            {
                try
                {

                    //int vt1 = up.FileName.LastIndexOf(".");
                    //  int vtcanlay = vt1;
                    //  int len = up..FileName.Length;
                    string extention = up.UploadedFiles[0].GetExtension();
                    string filename = "";
                    filename = cls.cToString(docno).Replace('/', '-');
                    filename = filename + '-' + cls.cToString(Session["username"]) + extention;
                    //HinhBia.SaveAs(Server.MapPath("../" + ConfigurationManager.AppSettings["hinhbia_sanpham"].ToString()).ToString() + ANHBIA);
                    string sFolderPath = Server.MapPath("Upload/IP/" + filename);
                    //  string sFullPath = sFolderPath + filename;
                    if (System.IO.File.Exists(sFolderPath) == true)
                        System.IO.File.Delete(sFolderPath);
                    //resize
                    //  HttpPostedFile pf = FileUpload1.PostedFile;
                    // up.SaveAs(sFolderPath);
                    // kq = filename;
                    try
                    {
                        // up.SaveAs(sFolderPath);

                        up.UploadedFiles[0].SaveAs(sFolderPath);
                        kq = filename;
                    }
                    catch
                    {
                        kq = "";

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
        private string upload(FileUpload up, object docno)
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
                    string filename = "";
                    filename = cls.cToString(docno).Replace('/', '-');
                    filename = filename + '-' + cls.cToString(Session["username"]) + extention;
                    //HinhBia.SaveAs(Server.MapPath("../" + ConfigurationManager.AppSettings["hinhbia_sanpham"].ToString()).ToString() + ANHBIA);
                    string sFolderPath = Server.MapPath("Upload/IP/" + filename);
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
        protected void RG_Init(object sender, EventArgs e)
        {
            GridFilterMenu menu = RG.FilterMenu;
            int i = 0;
            while (i < menu.Items.Count)
            {
                if (menu.Items[i].Text == "Contains" || menu.Items[i].Text == "NoFilter")
                {
                    if (menu.Items[i].Text == "Contains")
                    {
                        menu.Items[i].Text = "Có chứa";
                    }
                    if (menu.Items[i].Text == "NoFilter")
                    {
                        menu.Items[i].Text = "Không tìm";
                    }
                    i++;
                }
                else
                {
                    menu.Items.RemoveAt(i);
                }
            }
        }
        private void PRChangeIndex()
        {
            try
            {
                
                Label lbEmailNguoiTao = (Label)radcomboPR.SelectedItem.FindControl("lbNguoiTao");
                Label lbGhiChu = (Label)radcomboPR.SelectedItem.FindControl("lbGhiChu");
                Label slbTrangThai = (Label)radcomboPR.SelectedItem.FindControl("lbTrangThai");
                HiddenField shfTrangThai = (HiddenField)radcomboPR.SelectedItem.FindControl("hfTrangThai");
                HiddenField hfIPNote = (HiddenField)radcomboPR.SelectedItem.FindControl("hfIPNote");
                HiddenField hfNgaySubmit = (HiddenField)radcomboPR.SelectedItem.FindControl("hfNgaySubmit");
                HiddenField hfIPUserXacNhan = (HiddenField)radcomboPR.SelectedItem.FindControl("hfIPUserXacNhan");
                HiddenField hfIPUserXuLy = (HiddenField)radcomboPR.SelectedItem.FindControl("hfIPUserXuLy");
                txtNoiDung.Text = lbGhiChu.Text;
                txtIPNote.Text = hfIPNote.Value;
                RadComboIPUser.SelectedValue = hfIPUserXacNhan.Value;
                RadComboIPUserXuLy.SelectedValue = hfIPUserXuLy.Value;
                if (cls.cToString(Session["Email"]).ToLower() == hfIPUserXacNhan.Value.ToLower())//neu la nguoi xac nhan thi cho phep phan cong nguoi khac
                {
                    RadComboIPUserXuLy.Enabled = true;
                    btGiaoViec.Visible = true;
                }
                else
                {
                    RadComboIPUserXuLy.Enabled = false;
                    btGiaoViec.Visible = false;
                }
                lbTrangThaiShow.Text = "Status: " + slbTrangThai.Text;
                lbNgaySubmitShow.Text = "Submit on " + hfNgaySubmit.Value;
                hfTrangThai.Value = shfTrangThai.Value;
                hfEmailNguoiTao.Value = lbEmailNguoiTao.Text;
                if (hfLoai.Value == "2") //giao dien cho Manager IP duyet NCC
                {
                    btManagerApprove.Visible = true;
                    btManagerReject.Visible = true;
                    btConfirm.Visible = false;
                    btRejectPR.Visible = false;
                    btGiaoViec.Visible = false;
                    RadComboIPUser.Enabled = false;
                    RadComboIPUserXuLy.Enabled = false;
                }
                else//giao dien cho IP chon NCC
                {
                    btManagerApprove.Visible = false;
                    btManagerReject.Visible = false;
                    txtNoiDung.Enabled = false;
                    switch (hfTrangThai.Value)
                    {

                        case "0"://Da submit dang cho IP xac nhan
                            btRejectPR.Visible = true;
                            btConfirm.Visible = false;
                            txtIPNote.Enabled = true;
                            //RG.MasterTableView.GetColumn("EditCommandColumn").Visible = false;
                            //RG.MasterTableView.GetColumn("Delete").Visible = false;
                            break;
                        case "1"://IP da gui len IP's Manager duyet
                            btRejectPR.Visible = false;
                            btConfirm.Visible = false;
                            txtIPNote.Enabled = false;
                            RadComboIPUserXuLy.Enabled = false;
                            btGiaoViec.Visible = false;
                            //RG.MasterTableView.GetColumn("EditCommandColumn").Visible = false;
                            //RG.MasterTableView.GetColumn("Delete").Visible = false;
                            break;
                        //case "2"://IP da gui len IP's Manager duyet
                        //    btRejectPR.Visible = false;
                        //    btConfirm.Visible = false;
                        //    txtIPNote.Enabled = false;
                        //    //RG.MasterTableView.GetColumn("EditCommandColumn").Visible = false;
                        //    //RG.MasterTableView.GetColumn("Delete").Visible = false;
                        //    break;
                        case "3"://IP reject PR
                            btRejectPR.Visible = false;
                            btConfirm.Visible = false;
                            txtIPNote.Enabled = false;
                            RadComboIPUserXuLy.Enabled = false;
                            btGiaoViec.Visible = false;
                            //RG.MasterTableView.GetColumn("EditCommandColumn").Visible = false;
                            //RG.MasterTableView.GetColumn("Delete").Visible = false;
                            break;
                        case "4"://Sep IP da approved(Anh Lap da approved)
                            btRejectPR.Visible = false;
                            btConfirm.Visible = false;
                            txtIPNote.Enabled = false;
                            RadComboIPUserXuLy.Enabled = false;
                            btGiaoViec.Visible = false;

                            break;
                        case "5"://Sep IP da Rejeted(Anh Lap da Rejeted)
                            btRejectPR.Visible = true;
                            btConfirm.Visible = true;
                            txtIPNote.Enabled = true;
                            RadComboIPUserXuLy.Enabled = false;
                            btGiaoViec.Visible = false;
                            break;
                        case "6"://IP da gui y/c bao gia den NCC
                            btRejectPR.Visible = false;
                            btConfirm.Visible = false;
                            txtIPNote.Enabled = false;
                            RadComboIPUserXuLy.Enabled = false;
                            btGiaoViec.Visible = false;
                            break;
                        case "7"://NCC da gui bao gia cho IP
                            btRejectPR.Visible = false;
                            btConfirm.Visible = true;
                            txtIPNote.Enabled = true;
                            RadComboIPUserXuLy.Enabled = false;
                            btGiaoViec.Visible = false;
                            break;
                        default://IP da gui len IP's Manager duyet
                            btRejectPR.Visible = false;
                            btConfirm.Visible = false;
                            txtIPNote.Enabled = false;
                            RadComboIPUserXuLy.Enabled = false;
                            btGiaoViec.Visible = false;
                            break;
                    }

                }
                fLoadPRDetail(radcomboPR.SelectedValue);
            }
            catch { }
            
        }
        protected void radcomboPR_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PRChangeIndex();
        }

        protected void btConfirm_Click(object sender, EventArgs e)
        {
            if (radcomboPR.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Không có PR nào để submit<br/>Please select at least one PR", uc.ucMsgBox.enmMessageType.Error);
            }
            else
            {
                //phan nay con phai gui email den anh Lam truoc moi cap nhat data
                if (RG.Items.Count > 0)
                {
                    DataTable IPtbl = cls.GetDataTable("sp_IPGetEmailIPManager");
                    if (IPtbl.Rows.Count > 0)
                    {
                        string emailapprove = cls.cToString(IPtbl.Rows[0]["Email"]);
                        string prid = radcomboPR.SelectedValue;
                        string tittle = "Vendor’s Recommendation Notification - " + prid + ":" + txtNoiDung.Text;
                        string noidungemail = "Dear Head of IP<br/>";
                        noidungemail = noidungemail + " PR: " + prid + " – " + txtNoiDung.Text + "<br/>";
                        noidungemail = noidungemail + "Vui lòng đăng nhập vào hệ thống <a href=\"http://fin.maricosea.com\">FIN</a> để xem xét đề nghị chọn nhà cung cấp<br/>";
                        noidungemail = noidungemail + "Ghi chú: " + txtIPNote.Text + "<br/>";
                        //  noidungemail = noidungemail + " PR: " + prid + " – " + txtNoiDung.Text + "<br/>";
                        noidungemail = noidungemail + "<br/>Please login <a href=\"http://fin.maricosea.com\">FIN</a> system for your approval<br/>";
                        noidungemail = noidungemail + "Note: " + txtIPNote.Text + "<br/>";
                        noidungemail = noidungemail + "Trân trọng /Best regards, ";
                        if (cls.SendMailASP(/*hfEmailNguoiTao.Value*/emailapprove, cls.cToString(Session["Email"]) + ";" + RadComboIPUser.SelectedValue, tittle, noidungemail) == true)
                        {
                            if (cls.bCapNhat(new string[] { "@IDPR", "@TrangThai", "@IPUserXacNhan", "@GhiChu", "@IPnote", "@EmailN1" }, new object[] { prid, 1, RadComboIPUser.SelectedValue, txtNoiDung.Text.Trim(), txtIPNote.Text, emailapprove }, "IP_submitPR2IPApp") == true)
                            {
                                LoadPR(cls.cToInt(radcomboTrangThai.SelectedValue));
                                radcomboPR.SelectedValue = prid;
                                PRChangeIndex();
                                MsgBox1.AddMessage("Đã trình duyệt thành công<br/>Submit successfully", uc.ucMsgBox.enmMessageType.Success);
                            }
                        }
                        else
                        {
                            MsgBox1.AddMessage("Không thể trình duyệt vì không thể gửi được email<br/>Can not send email, submit fail", uc.ucMsgBox.enmMessageType.Error);
                        }
                    }
                    else
                    {
                        MsgBox1.AddMessage("Không tìm thấy email của người duyệt<br/>Email not found", uc.ucMsgBox.enmMessageType.Error);
                    }
                }
                else
                {
                    MsgBox1.AddMessage("Cần thêm mặt hàng yêu cầu mua trước khi submit<br/>Please at least one item", uc.ucMsgBox.enmMessageType.Error);
                }
            }
        }

        protected void btRejectPR_Click(object sender, EventArgs e)
        {
            if (radcomboPR.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Không có PR nào để reject<br/>Please select PR", uc.ucMsgBox.enmMessageType.Error);
            }
            else
            {
                if (RG.Items.Count > 0)
                {
                    if (txtIPNote.Text.Trim() == "")
                    {
                        MsgBox1.AddMessage("Vui lòng nhập Ghi chú IP<br/>Please fill in IP's note", uc.ucMsgBox.enmMessageType.Error);
                    }
                    else
                    {
                        string prid = radcomboPR.SelectedValue;
                        string tittle = "PR Reject - " + prid + ":" + txtNoiDung.Text;
                        string noidungemail = "Dear Requester<br/>";
                        noidungemail = noidungemail + "PR " + prid + " – " + txtNoiDung.Text + " của bạn đã bị từ chối<br/>";
                        noidungemail = noidungemail + "Lý do: " + txtIPNote.Text + "<br/>";
                        noidungemail = noidungemail + "Vui lòng đăng nhập vào hệ thống <a href=\"http://fin.maricosea.com\">FIN</a> để thực hiện điều chỉnh<br/>";

                        noidungemail = noidungemail + "<br/>PR " + prid + " – " + txtNoiDung.Text + " has been rejected<br/>";
                        noidungemail = noidungemail + "Reason: " + txtIPNote.Text + "<br/>";
                        noidungemail = noidungemail + "Please login <a href=\"http://fin.maricosea.com\">FIN</a> system to amend<br/>";
                        noidungemail = noidungemail + "Trân trọng /Best regards, ";
                        if (cls.SendMailASP(hfEmailNguoiTao.Value, cls.cToString(Session["Email"]), tittle, noidungemail) == true)
                        {
                            if (cls.bCapNhat(new string[] { "@IDPR", "@TrangThai", "@IPUserXacNhan", "@GhiChu", "@IPnote", "@EmailN1" }, new object[] { prid, 3, RadComboIPUser.SelectedValue, txtNoiDung.Text.Trim(), txtIPNote.Text, "" }, "IP_submitPR2IPApp") == true)
                            {
                                LoadPR(cls.cToInt(radcomboTrangThai.SelectedValue));
                                radcomboPR.SelectedValue = prid;
                                PRChangeIndex();
                                MsgBox1.AddMessage("Đã reject thành công<br/>Rejected successfully", uc.ucMsgBox.enmMessageType.Success);
                            }
                        }
                        else
                        {
                            MsgBox1.AddMessage("Không thể reject vì không thể gửi được email<br/>Can not send email, Reject fail", uc.ucMsgBox.enmMessageType.Error);

                        }
                    }
                }
                else
                {
                    MsgBox1.AddMessage("Cần thêm mặt hàng yêu cầu mua trước khi submit<br/>Please add at least one item", uc.ucMsgBox.enmMessageType.Error);
                }
            }
        }

        protected void btManagerApprove_Click(object sender, EventArgs e)
        {
            if (radcomboPR.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Không có PR nào để approve<br/>PR not exists", uc.ucMsgBox.enmMessageType.Error);
            }
            else
            {
                //phan nay con phai gui email den anh Lam truoc moi cap nhat data
                if (RG.Items.Count > 0)
                {

                    string prid = radcomboPR.SelectedValue;
                    string tittle = "Vendor recommandation Accept - " + prid + ":" + txtNoiDung.Text;
                    string noidungemail = "Dear IP team,<br/>";
                    noidungemail = noidungemail + "PR: " + prid + " – " + txtNoiDung.Text + ", Đề nghị nhà cung cấp đã được duyệt<br/>";
                    noidungemail = noidungemail + "Ghi chú: " + txtIPNote.Text + "<br/>";
                    noidungemail = noidungemail + "Vui lòng đăng nhập vào hệ thống <a href=\"http://fin.maricosea.com\">FIN</a> để thực hiện bước tiếp theo<br/>";

                    noidungemail = noidungemail + "<br/>PR " + prid + " – " + txtNoiDung.Text + ", Vendor recommandation has been accepted<br/>";
                    noidungemail = noidungemail + "Note: " + txtIPNote.Text + "<br/>";
                    noidungemail = noidungemail + "Please login <a href=\"http://fin.maricosea.com\">FIN</a> system to next step<br/>";
                    noidungemail = noidungemail + "Trân trọng /Best regards, ";
                    if (cls.SendMailASP(RadComboIPUserXuLy.SelectedValue, cls.cToString(Session["Email"]) + ";" + RadComboIPUser.SelectedValue, tittle, noidungemail) == true)
                    {
                        if (cls.bCapNhat(new string[] { "@IDPR", "@TrangThai", "@IPUserXacNhan", "@GhiChu", "@IPnote", "@EmailN1" }, new object[] { prid, 4, RadComboIPUser.SelectedValue, txtNoiDung.Text.Trim(), txtIPNote.Text, Session["Email"] }, "IP_N1AppPR") == true)
                        {
                            LoadPR(cls.cToInt(radcomboTrangThai.SelectedValue));
                            radcomboPR.SelectedValue = prid;
                            PRChangeIndex();
                            MsgBox1.AddMessage("Đã approved thành công<br/>Approved successfully", uc.ucMsgBox.enmMessageType.Success);
                        }
                    }
                    else
                    {
                        MsgBox1.AddMessage("Không thể approved vì không thể gửi được email<br/>Can not send email, approval fail", uc.ucMsgBox.enmMessageType.Error);

                    }

                }
                else
                {
                    MsgBox1.AddMessage("Cần thêm mặt hàng yêu cầu mua trước khi approve<br/>Please add at least one item", uc.ucMsgBox.enmMessageType.Error);
                }
            }
        }

        protected void btManagerReject_Click(object sender, EventArgs e)
        {
            if (radcomboPR.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Không có PR nào để reject<br/>PR not exists", uc.ucMsgBox.enmMessageType.Error);
            }
            else
            {
                if (RG.Items.Count > 0)
                {
                    if (txtIPNote.Text.Trim() == "")
                    {
                        MsgBox1.AddMessage("Vui lòng nhập ghi chú IP<br/>Please fill in IP's note", uc.ucMsgBox.enmMessageType.Error);
                    }
                    else
                    {
                        string prid = radcomboPR.SelectedValue;
                        string tittle = "Vendor recommandation Reject - " + prid + ":" + txtNoiDung.Text;
                        string noidungemail = "Dear IP team,<br/>";
                        noidungemail = noidungemail + "PR: " + prid + " – " + txtNoiDung.Text + ", Đề nghị nhà cung cấp đã bị từ chối<br/>";
                        noidungemail = noidungemail + "Lý do: " + txtIPNote.Text + "<br/>";
                        noidungemail = noidungemail + "Vui lòng đăng nhập vào hệ thống <a href=\"http://fin.maricosea.com\">FIN</a> để thực hiện điều chỉnh<br/>";

                        noidungemail = noidungemail + "<br/>PR " + prid + " – " + txtNoiDung.Text + ", Vendor recommandation has been rejected<br/>";
                        noidungemail = noidungemail + "Reason: " + txtIPNote.Text + "<br/>";
                        noidungemail = noidungemail + "Please login <a href=\"http://fin.maricosea.com\">FIN</a> system to amend<br/>";
                        noidungemail = noidungemail + "Trân trọng /Best regards, ";
                        if (cls.SendMailASP(RadComboIPUserXuLy.SelectedValue, cls.cToString(Session["Email"]) + ";" + RadComboIPUser.SelectedValue, tittle, noidungemail) == true)
                        {
                            if (cls.bCapNhat(new string[] { "@IDPR", "@TrangThai", "@IPUserXacNhan", "@GhiChu", "@IPnote", "@EmailN1" }, new object[] { prid, 5, RadComboIPUser.SelectedValue, txtNoiDung.Text.Trim(), txtIPNote.Text, "" }, "IP_N1AppPR") == true)
                            {
                                radcomboPR.Items.Clear();
                                LoadPR(cls.cToInt(radcomboTrangThai.SelectedValue));
                                // radcomboPR.SelectedValue = prid;
                                //  PRChangeIndex();
                                MsgBox1.AddMessage("Đã từ chối thành công<br/>Rejectd successfully", uc.ucMsgBox.enmMessageType.Success);
                            }
                        }
                        else
                        {
                            MsgBox1.AddMessage("Không thể từ chối vì không thể gửi được email<br/>Can not send email, Rejected fail", uc.ucMsgBox.enmMessageType.Error);

                        }
                    }
                }
                else
                {
                    MsgBox1.AddMessage("Cần thêm mặt hàng yêu cầu mua trước khi trình duyệt<br/>Please add at least one item", uc.ucMsgBox.enmMessageType.Error);
                }
            }
        }

        protected void btGiaoViec_Click(object sender, EventArgs e)
        {
            if (radcomboPR.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Không có PR nào để giao việc<br/>Please select PR for assignment", uc.ucMsgBox.enmMessageType.Error);
            }
            else
            {
                if (RadComboIPUser.SelectedValue == RadComboIPUserXuLy.SelectedValue)
                {
                    MsgBox1.AddMessage("Không thể chuyển xử lý cho chính bạn<br/>Can not assign to your self", uc.ucMsgBox.enmMessageType.Error);
                }
                else
                {
                    string tittle = "PR Application Process - " + radcomboPR.SelectedValue + ":" + txtNoiDung.Text;
                    string noidungemail = "Dear IP team,<br/>";
                    noidungemail = noidungemail + " Vui lòng xử lý PR: " + radcomboPR.SelectedValue + " – " + txtNoiDung.Text + "<br/>";
                    noidungemail = noidungemail + "Lưu ý: " + txtIPNote.Text + "<br/>";
                    noidungemail = noidungemail + "Vui lòng đăng nhập vào hệ thống <a href=\"http://fin.maricosea.com\">FIN</a> để xử lý<br/>";

                    noidungemail = noidungemail + "<br/>Kindly process the PR " + radcomboPR.SelectedValue + " – " + txtNoiDung.Text + "<br/>";
                    noidungemail = noidungemail + "Note: " + txtIPNote.Text + "<br/>";
                    noidungemail = noidungemail + "Please login <a href=\"http://fin.maricosea.com\">FIN</a> system to process<br/>";
                    noidungemail = noidungemail + "Trân trọng /Best regards, ";
                    if (cls.SendMailASP(RadComboIPUserXuLy.SelectedValue, RadComboIPUser.SelectedValue, tittle, noidungemail) == true)
                    {
                        if (cls.bCapNhat(new string[] { "@IDPR", "@IPUserXuLy" }, new object[] { radcomboPR.SelectedValue, RadComboIPUserXuLy.SelectedValue }, "sp_IPUpdateUserXuLy") == true)
                        {
                            MsgBox1.AddMessage("Đã gửi PR đến người xử lý<br/>Assign successfully", uc.ucMsgBox.enmMessageType.Success);
                        }
                        else
                        {
                            MsgBox1.AddMessage("Không thể giao việc đến người xử lý<br/>Can not assignment", uc.ucMsgBox.enmMessageType.Error);
                        }
                    }
                    else
                    {
                        MsgBox1.AddMessage("Không gửi được email đến người xử lý, vui lòng thử lại<br/>Can not send email, assign fail, please try again ", uc.ucMsgBox.enmMessageType.Error);
                    }
                }
            }
        }
        protected void RG_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {

                //   GridDataItem dataItem = (GridDataItem)e.Item;

                GridDataItem item = (GridDataItem)e.Item;
                switch (cls.cToInt(item["tomau"].Text))//hfTrangThai.Value
                {
                    case 1:
                        item.ForeColor = System.Drawing.Color.Red;
                        break;
                    case 2:
                        item.ForeColor = System.Drawing.Color.Blue;
                        break;
                }
               

            }
        }

        protected void radcomboTrangThai_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            radcomboPR.Items.Clear();
            LoadPR(cls.cToInt(radcomboTrangThai.SelectedValue));
            if (radcomboPR.Items.Count > 0)
            {
                radcomboPR.SelectedIndex = 0;
                //   int idx = radcomboPR.FindItemIndexByValue(radcomboPR.SelectedValue, true);
                //  radcomboPR.ClearSelection();
                //  radcomboPR.SelectedIndex = idx;
                radcomboPR.Text = radcomboPR.SelectedValue;
                PRChangeIndex();
                PRChangeIndex();
            }
            else
            {
                radcomboPR.SelectedIndex = -1;
                radcomboPR.Text = "";
                
                RG.DataSource = null;
                RG.DataBind();
            }
            
        }
    }
}