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
    public partial class IPPurRequest : clsPhanQuyenCaoCap
    {
        clsObj Obj;
        clsSql Sql = new clsSql();
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadIP();
                LoadPR();
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
        private void LoadPR()
        {
            DataTable tbl = cls.GetDataTable("sp_LoadIPPRActive", new string[] { "@Username" }, new object[] { Session["Username"] });
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
            int sodongtruocthaydoi = 0;
            switch (e.CommandName)
            {
                case Telerik.Web.UI.RadGrid.InitInsertCommandName:
                    if (hfTrangThai.Value == "-2" || hfTrangThai.Value == "-1" || hfTrangThai.Value == "3")
                    {

                        RG.MasterTableView.ClearEditItems();
                        e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/InsertIPRequest.ascx";
                     //   e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/ISIPMaterials.ascx";
                       
                        e.Item.OwnerTableView.InsertItem();
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
                    else
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
                        lbLoi.Text = "<font color='red'>PR đã submit không được điều chỉnh/PR already submited, you can not revise</font>";
                        // MsgBox1.AddMessage("PR đã submit không được điều chỉnh", uc.ucMsgBox.enmMessageType.Error);
                    }
                    break;
                case Telerik.Web.UI.RadGrid.EditCommandName:
                    sodongtruocthaydoi = RG.Items.Count;
                    RG.MasterTableView.IsItemInserted = false;
                    e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/UpdateIPRequest.ascx";
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
                case "Copy":
                    
                    if (RG.Items.Count > sodongtruocthaydoi)
                    {
                        lbLoi.Text = "<font color='blue'>Sao chép thành công/Copy successfully</font>";
                    }
                    else
                    {
                        lbLoi.Text = "<font color='red'>Sao chép thất bại. Vui lòng thử lại sau/Can not copy</font>";
                    }
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
                    LoadPR();
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
                   string sIO = "";
                   int idx1 = 0;
                   RadComboBox cboIO = userControl.FindControl("RadComboIO") as RadComboBox;
                   try
                   {
                       idx1 = cboIO.FindItemByText(cboIO.Text).Index;
                       if (idx1 < 0)
                       {
                           sIO = cboIO.Text;
                       }
                       else
                       {
                           cboIO.SelectedIndex = idx1;
                           sIO = cboIO.SelectedValue;
                       }
                   }
                   catch {
                       sIO = cboIO.Text;
                   }
             
                string kichthuoc = (userControl.FindControl("txtKichThuoc") as TextBox).Text.Trim();
                string chatlieu = (userControl.FindControl("txtChatLieu") as TextBox).Text.Trim();
                string doday = (userControl.FindControl("txtDoDay") as TextBox).Text.Trim();
                CheckBox coden = userControl.FindControl("chkCoDen") as CheckBox;
                double soluong = cls.cToDouble((userControl.FindControl("radnumSoLuong") as RadNumericTextBox).Value);
                //   FileUpload fileupload=userControl.FindControl("FileUpload1") as FileUpload;
                RadAsyncUpload radfileupload = userControl.FindControl("RadAsyncUpload1") as RadAsyncUpload;
                DateTime ngaygiao = (userControl.FindControl("radDateNgayGiao") as RadDatePicker).SelectedDate.Value;
               // string noinhan = (userControl.FindControl("txtNoiNhan") as TextBox).Text.Trim();
                string noinhan = (userControl.FindControl("dropNoiNhan") as RadComboBox).Text.Trim();
                
                string Note = (userControl.FindControl("tbNote") as TextBox).Text.Trim();

                RadComboBox radCostcenter = (userControl.FindControl("radcomboCostcenter") as RadComboBox);
                string Costcenter = "";
                idx1 = 0;
                try
                {
                    idx1 = radCostcenter.FindItemByText(radCostcenter.Text).Index;
                    if (idx1 > 0)
                    {
                        radCostcenter.SelectedIndex = idx1;
                        Costcenter = radCostcenter.SelectedValue;
                    }
                }
                catch
                {
                    
                }
                if (Costcenter == "")
                {
                    MsgBox1.AddMessage("Cost center không đúng, vui lòng chọn lại<br><br/>Costcente incorrect, please choose again", uc.ucMsgBox.enmMessageType.Error);
                    return;
                }
                RadComboBox radcomboGL = (userControl.FindControl("radcomboGL") as RadComboBox);
                idx1 = 0;
                string GL = "";
                idx1 = radcomboGL.FindItemByText(radcomboGL.Text).Index;
                if (idx1 > 0)
                {
                    radcomboGL.SelectedIndex = idx1;
                    GL = radcomboGL.SelectedValue;
                }

                RadComboBox radcomboMatrialGroup = (userControl.FindControl("radcomboMatrialGroup") as RadComboBox);
                idx1 = 0;
                string MaterialGroup = "";
                idx1 = radcomboMatrialGroup.FindItemByText(radcomboMatrialGroup.Text).Index;
                if (idx1 > 0)
                {
                    radcomboMatrialGroup.SelectedIndex = idx1;
                    MaterialGroup = radcomboMatrialGroup.SelectedValue;
                }

                RadComboBox radcomboProfitcenter = (userControl.FindControl("radcomboProfitcenter") as RadComboBox);
                idx1 = 0;
                string Profitcenter = "";
                idx1 = radcomboProfitcenter.FindItemByText(radcomboProfitcenter.Text).Index;
                if (idx1 > 0)
                {
                    radcomboProfitcenter.SelectedIndex = idx1;
                    Profitcenter = radcomboProfitcenter.SelectedValue;
                }

                RadComboBox radcomboCountry = (userControl.FindControl("radcomboCountry") as RadComboBox);
                idx1 = 0;
                string Country = "";
                idx1 = radcomboCountry.FindItemByText(radcomboCountry.Text).Index;
                if (idx1 > 0)
                {
                    radcomboCountry.SelectedIndex = idx1;
                    Country = radcomboCountry.SelectedValue;
                }

                RadComboBox radcomboDivision = (userControl.FindControl("radcomboDivision") as RadComboBox);
                idx1 = 0;
                string Division = "";
                idx1 = radcomboDivision.FindItemByText(radcomboDivision.Text).Index;
                if (idx1 > 0)
                {
                    radcomboDivision.SelectedIndex = idx1;
                    Division = radcomboDivision.SelectedValue;
                }

                RadComboBox radcomboSalesGroup = (userControl.FindControl("radcomboSalesGroup") as RadComboBox);
                idx1 = 0;
                string SalesGroup = "";
                idx1 = radcomboSalesGroup.FindItemByText(radcomboSalesGroup.Text).Index;
                if (idx1 > 0)
                {
                    radcomboSalesGroup.SelectedIndex = idx1;
                    SalesGroup = radcomboSalesGroup.SelectedValue;
                }

                #region Insert
                //if(Material<=0

                string[] bien = new string[] { "@IDPRDetail", "@IDPR", "@IO", "@IDMaterial", "@TenHang", "@KichThuoc", "@ChatLieu", "@DoDay", "@CoDen", "@SoLuong", "@NgayGiao", "@NoiNhan", "@ChiTietKhac", "@Costcenter", "@GL", "@MaterialGroup", "@Profitcenter", "@Country", "@Division", "@SalesGroup" };
                object[] giatri = new object[] { 0, IDPR, sIO, cboMaterial.SelectedValue, cboMaterial.SelectedItem.Text, kichthuoc, chatlieu, doday, coden.Checked, soluong, ngaygiao, noinhan, Note, Costcenter, GL, MaterialGroup, Profitcenter, Country, Division, SalesGroup };
                // Obj.SpName = "IPPRDetail_InsertUpdate";
                // Sql.fNonGetData(Obj);
                decimal IDPRDetail = cls.cToDecimal(cls.GetString0("IPPRDetail_InsertUpdate", bien, giatri));
                if (IDPRDetail <= 0)
                {
                    lbLoi.Text = "<font color='red'>Thêm thất bại. Vui lòng thử lại sau/Insert fail, please try again</font>";
                }
                else
                {
                   
                    if (radfileupload.UploadedFiles.Count > 0)
                    {
                        
                       string filename = cls.radupload(radfileupload, IDPRDetail);
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
            string IDPR = "";
            if (cls.cToDecimal(radcomboPR.SelectedValue) == 0)
            {
                IDPR = InsertPR();
                radcomboPR.SelectedValue = IDPR;
                PRChangeIndex();
            }
            else
            {
                IDPR = radcomboPR.SelectedValue;
            }
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            decimal ID = cls.cToDecimal((userControl.FindControl("rnID") as RadNumericTextBox).Value);
            RadComboBox cboMaterial = userControl.FindControl("RadComboMaterial") as RadComboBox;
            cboMaterial.SelectedIndex = cboMaterial.FindItemByText(cboMaterial.Text).Index;

           // RadComboBox cboIO = userControl.FindControl("RadComboIO") as RadComboBox;
           // cboIO.SelectedIndex = cboIO.FindItemByText(cboIO.Text).Index;
            string sIO = "";
            RadComboBox cboIO = userControl.FindControl("RadComboIO") as RadComboBox;
            try
            {
                int idx = cboIO.FindItemByText(cboIO.Text).Index;
                if (idx < 0)
                {
                    sIO = cboIO.Text;
                }
                else
                {
                    cboIO.SelectedIndex = idx;
                    sIO = cboIO.SelectedValue;
                }
            }
            catch {
                sIO = cboIO.Text;
            }
            string kichthuoc = (userControl.FindControl("txtKichThuoc") as TextBox).Text.Trim();
            string chatlieu = (userControl.FindControl("txtChatLieu") as TextBox).Text.Trim();
            string doday = (userControl.FindControl("txtDoDay") as TextBox).Text.Trim();
            CheckBox coden = userControl.FindControl("chkCoDen") as CheckBox;
            double soluong = cls.cToDouble((userControl.FindControl("radnumSoLuong") as RadNumericTextBox).Value);
            //   FileUpload fileupload=userControl.FindControl("FileUpload1") as FileUpload;
            RadAsyncUpload radfileupload = userControl.FindControl("RadAsyncUpload1") as RadAsyncUpload;
            DateTime ngaygiao = (userControl.FindControl("radDateNgayGiao") as RadDatePicker).SelectedDate.Value;
            // string noinhan = (userControl.FindControl("txtNoiNhan") as TextBox).Text.Trim();
            string noinhan = (userControl.FindControl("dropNoiNhan") as RadComboBox).Text.Trim();

            string Note = (userControl.FindControl("tbNote") as TextBox).Text.Trim();

           // string Costcenter = (userControl.FindControl("txtCostCenter") as TextBox).Text.Trim();
            int idx1 = -1;
            RadComboBox radCostcenter = (userControl.FindControl("radcomboCostcenter") as RadComboBox);
            string Costcenter = "";
            try
            {
                idx1 = radCostcenter.FindItemByText(radCostcenter.Text).Index;
                if (idx1 > 0)
                {
                    radCostcenter.SelectedIndex = idx1;
                    Costcenter = radCostcenter.SelectedValue;
                }
            }
            catch
            {

            }
            if (Costcenter == "")
            {
                MsgBox1.AddMessage("Cost center không đúng, vui lòng chọn lại<br><br/>Costcente incorrect, please choose again", uc.ucMsgBox.enmMessageType.Error);
                return;
            }

            RadComboBox radcomboGL = (userControl.FindControl("radcomboGL") as RadComboBox);
            idx1 = 0;
            string GL = "";
            idx1 = radcomboGL.FindItemByText(radcomboGL.Text).Index;
            if (idx1 > 0)
            {
                radcomboGL.SelectedIndex = idx1;
                GL = radcomboGL.SelectedValue;
            }

            RadComboBox radcomboMatrialGroup = (userControl.FindControl("radcomboMatrialGroup") as RadComboBox);
            idx1 = 0;
            string MaterialGroup = "";
            idx1 = radcomboMatrialGroup.FindItemByText(radcomboMatrialGroup.Text).Index;
            if (idx1 > 0)
            {
                radcomboMatrialGroup.SelectedIndex = idx1;
                MaterialGroup = radcomboMatrialGroup.SelectedValue;
            }

            RadComboBox radcomboProfitcenter = (userControl.FindControl("radcomboProfitcenter") as RadComboBox);
            idx1 = 0;
            string Profitcenter = "";
            idx1 = radcomboProfitcenter.FindItemByText(radcomboProfitcenter.Text).Index;
            if (idx1 > 0)
            {
                radcomboProfitcenter.SelectedIndex = idx1;
                Profitcenter = radcomboProfitcenter.SelectedValue;
            }

            RadComboBox radcomboCountry = (userControl.FindControl("radcomboCountry") as RadComboBox);
            idx1 = 0;
            string Country = "";
            idx1 = radcomboCountry.FindItemByText(radcomboCountry.Text).Index;
            if (idx1 > 0)
            {
                radcomboCountry.SelectedIndex = idx1;
                Country = radcomboCountry.SelectedValue;
            }

            RadComboBox radcomboDivision = (userControl.FindControl("radcomboDivision") as RadComboBox);
            idx1 = 0;
            string Division = "";
            idx1 = radcomboDivision.FindItemByText(radcomboDivision.Text).Index;
            if (idx1 > 0)
            {
                radcomboDivision.SelectedIndex = idx1;
                Division = radcomboDivision.SelectedValue;
            }

            RadComboBox radcomboSalesGroup = (userControl.FindControl("radcomboSalesGroup") as RadComboBox);
            idx1 = 0;
            string SalesGroup = "";
            idx1 = radcomboSalesGroup.FindItemByText(radcomboSalesGroup.Text).Index;
            if (idx1 > 0)
            {
                radcomboSalesGroup.SelectedIndex = idx1;
                SalesGroup = radcomboSalesGroup.SelectedValue;
            }
         
            #region Insert
            //if(Material<=0

            string[] bien = new string[] { "@IDPRDetail", "@IDPR", "@IO", "@IDMaterial", "@TenHang", "@KichThuoc", "@ChatLieu", "@DoDay", "@CoDen", "@SoLuong", "@NgayGiao", "@NoiNhan", "@ChiTietKhac", "@Costcenter", "@GL", "@MaterialGroup", "@Profitcenter", "@Country", "@Division", "@SalesGroup" };
            object[] giatri = new object[] { ID, IDPR, sIO, cboMaterial.SelectedValue, cboMaterial.SelectedItem.Text, kichthuoc, chatlieu, doday, coden.Checked, soluong, ngaygiao, noinhan, Note, Costcenter, GL, MaterialGroup, Profitcenter, Country, Division, SalesGroup };
            // Obj.SpName = "IPPRDetail_InsertUpdate";
            // Sql.fNonGetData(Obj);
            decimal IDPRDetail = cls.cToDecimal(cls.GetString0("IPPRDetail_InsertUpdate", bien, giatri));
            if (IDPRDetail <= 0)
            {
                lbLoi.Text = "<font color='red'>Cập nhật thất bại. Vui lòng thử lại sau/Update fail, please try again</font>";
            }
            else
            {
                // string filename = "";
                // if (!fileupload.HasFile)
                if (radfileupload.UploadedFiles.Count > 0)
                {
                    string filename = cls.radupload(radfileupload, IDPRDetail);
                    cls.bCapNhat(new string[] { "@IDPRDetail", "@filename" }, new object[] { IDPRDetail, filename }, "sp_IPPRDetailupdateImage");
                }

                lbLoi.Text = "<font color='blue'>Cập nhật thành công/Update successfully</font>";
            }
            #endregion
            fLoadPRDetail(radcomboPR.SelectedValue);
        }
        //private string radupload(RadAsyncUpload up, object docno)
        //{
        //    string kq = "";
        //    // if(   RadUpload1.UploadedFiles.Count>0)
        //    if (up.UploadedFiles.Count > 0)
        //    {
        //        try
        //        {

        //            //int vt1 = up.FileName.LastIndexOf(".");
        //            //  int vtcanlay = vt1;
        //            //  int len = up..FileName.Length;
        //            string extention = up.UploadedFiles[0].GetExtension();
        //            string filename = "";
        //            filename = cls.cToString(docno).Replace('/', '-');
        //            filename = filename + '-' + cls.cToString(Session["username"]) + extention;
        //            //HinhBia.SaveAs(Server.MapPath("../" + ConfigurationManager.AppSettings["hinhbia_sanpham"].ToString()).ToString() + ANHBIA);
        //            string sFolderPath = Server.MapPath("Upload/IP/" + filename);
        //            //  string sFullPath = sFolderPath + filename;
        //            if (System.IO.File.Exists(sFolderPath) == true)
        //                System.IO.File.Delete(sFolderPath);
        //            //resize
        //            //  HttpPostedFile pf = FileUpload1.PostedFile;
        //            // up.SaveAs(sFolderPath);
        //            // kq = filename;
        //            try
        //            {
        //                // up.SaveAs(sFolderPath);

        //                up.UploadedFiles[0].SaveAs(sFolderPath);
        //                kq = filename;
        //            }
        //            catch
        //            {
        //                kq = "";

        //            }

        //        }
        //        catch
        //        {
        //            kq = "";
        //        }
        //    }
        //    else
        //    {
        //        kq = "";
        //    }
        //    return kq;
        //}
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
            Label lbIPUsser = new Label();
            Label lbGhiChu = new Label();
            Label slbTrangThai=new Label();
            HiddenField shfTrangThai;
            HiddenField hfIPNote;
            HiddenField hfNgaySubmit;
            if (radcomboPR.SelectedIndex == -1)
            {
                radcomboPR.SelectedIndex = 0;
            }
            fLoadPRDetail(radcomboPR.SelectedValue);
           
                
                 lbIPUsser = (Label)radcomboPR.SelectedItem.FindControl("lbIPUsser");
                 lbGhiChu = (Label)radcomboPR.SelectedItem.FindControl("lbGhiChu");
                 slbTrangThai = (Label)radcomboPR.SelectedItem.FindControl("lbTrangThai");
                 shfTrangThai = (HiddenField)radcomboPR.SelectedItem.FindControl("hfTrangThai");
                 hfIPNote = (HiddenField)radcomboPR.SelectedItem.FindControl("hfIPNote");
                 hfNgaySubmit = (HiddenField)radcomboPR.SelectedItem.FindControl("hfNgaySubmit");


                lbIPnoteShow.Text = hfIPNote.Value;
                try
                {
                    RadComboIPUser.SelectedValue = lbIPUsser.Text;
                }
                catch { }
                lbTrangThaiShow.Text = "Status: " + slbTrangThai.Text;
                lbNgaySubmitShow.Text = "Submit on: " + hfNgaySubmit.Value;
                hfTrangThai.Value = shfTrangThai.Value;
            //}
            //else
            //{
            //    hfTrangThai.Value = "-2";
            //}
            switch (hfTrangThai.Value)
            {
                case "-2"://tao mơi
                    btDeletePR.Visible = false;
                    btSubmit.Visible = false;
                    RG.MasterTableView.GetColumn("EditCommandColumn").Visible = true;
                    RG.MasterTableView.GetColumn("Delete").Visible = true;
                    radcomboPR.Text = "0-Creat new";
                    txtNoiDung.Text = "";
                    txtNoiDung.Attributes.Add("placeholder", lbGhiChu.Text);
                    break;
                case "-1"://Da save chua submit
                    btDeletePR.Visible = true;
                    btSubmit.Visible = true;
                    RG.MasterTableView.GetColumn("EditCommandColumn").Visible = true;
                    RG.MasterTableView.GetColumn("Delete").Visible = true;
                    txtNoiDung.Text = lbGhiChu.Text;
                    break;
                case "0"://Da submit dang cho IP xac nhan
                    btDeletePR.Visible = false;
                    btSubmit.Visible = false;
                    RG.MasterTableView.GetColumn("EditCommandColumn").Visible = false;
                    RG.MasterTableView.GetColumn("Delete").Visible = false;
                    txtNoiDung.Text = lbGhiChu.Text;
                    break;
                case "1"://IP da xac nhan
                    btDeletePR.Visible = false;
                    btSubmit.Visible = false;
                    RG.MasterTableView.GetColumn("EditCommandColumn").Visible = false;
                    RG.MasterTableView.GetColumn("Delete").Visible = false;
                    txtNoiDung.Text = lbGhiChu.Text;
                    break;
                case "3"://IP da rejected
                    btDeletePR.Visible = true;
                    btSubmit.Visible = true;
                    RG.MasterTableView.GetColumn("EditCommandColumn").Visible = true;
                    RG.MasterTableView.GetColumn("Delete").Visible = true;
                    txtNoiDung.Text = lbGhiChu.Text;
                    break;
                default:
                    btDeletePR.Visible = false;
                    btSubmit.Visible = false;
                    RG.MasterTableView.GetColumn("EditCommandColumn").Visible = false;
                    RG.MasterTableView.GetColumn("Delete").Visible = false;
                    txtNoiDung.Text = lbGhiChu.Text;
                    break;
            }
            
        }
        protected void radcomboPR_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PRChangeIndex();
        }

        protected void btSubmit_Click(object sender, EventArgs e)
        {
            if (RG.Items.Count > 0)
            {
                string prid = radcomboPR.SelectedValue;
                string tittle = "PR Application - " + prid + ":" + txtNoiDung.Text;
                string noidungemail = "Dear IP team,<br/>";
                noidungemail = noidungemail + "Xin vui lòng xem xét yêu cầu mua hàng đính kèm. PR : " + prid + " – " + txtNoiDung.Text + "<br/>";
                noidungemail = noidungemail + "Vui lòng đăng nhập vào hệ thống <a href=\"http://fin.maricosea.com\">FIN</a> để thực hiện các bước tiếp theo<br/>";
                noidungemail = noidungemail + "<br/>Please kindly process the attached purchase request. PR : " + prid + " – " + txtNoiDung.Text + "<br/>";
                noidungemail = noidungemail + "Please login <a href=\"http://fin.maricosea.com\">FIN</a> system to process next step<br/>";
                noidungemail = noidungemail + "Trân trọng /Best regards, ";

                if (cls.SendMailASP(RadComboIPUser.SelectedValue, cls.cToString(Session["Email"]), tittle, noidungemail) == true)
                {
                    if (cls.bCapNhat(new string[] { "@IDPR", "@TrangThai", "@IPUserXacNhan", "@GhiChu", "@IPnote", "@IPUserXuLy" }, new object[] { prid, 0, RadComboIPUser.SelectedValue, txtNoiDung.Text.Trim(), lbIPnoteShow.Text, RadComboIPUser.SelectedValue }, "IP_submitPR2IP") == true)
                    {
                        LoadPR();
                        radcomboPR.SelectedValue = prid;
                        PRChangeIndex();
                        MsgBox1.AddMessage("Submit đến IP thành công<br/>Submit to IP successfully", uc.ucMsgBox.enmMessageType.Success);
                    }
                }
                else
                {
                    MsgBox1.AddMessage("Không submit đến IP vì không thể gửi được email<br/>Can not send email to IP", uc.ucMsgBox.enmMessageType.Error);

                }
            }
            else
            {
                MsgBox1.AddMessage("Cần thêm mặt hàng yêu cầu mua trước khi submit<br/>Please add at least one item", uc.ucMsgBox.enmMessageType.Error);
            }
        }

        protected void btDeletePR_Click(object sender, EventArgs e)
        {
            if (cls.bXoa(new string[] { "@IDPR" }, new object[] { radcomboPR.SelectedValue }, "sp_DeletePR") == true)
            {
                LoadPR();
                radcomboPR.SelectedIndex = 0;
                PRChangeIndex();
                MsgBox1.AddMessage("xóa thành công<br/>Delete successfully<br/> PR No: " + radcomboPR.SelectedValue, uc.ucMsgBox.enmMessageType.Success);
            }
        }
    }
}