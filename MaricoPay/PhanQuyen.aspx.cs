using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Configuration;
using System.Xml.Linq;
using Data;
using Telerik.Web.UI;
namespace MaricoPay
{
    public partial class PhanQuyen : clsPhanQuyenCaoCap
    {
        Cclass cls = new Cclass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fLoadDMSITE();
                fLoadDSSITE();
                fLoad_RGDMQuyen();
                fLoadUser();
            }
        }
        void fLoadDMSITE()
        {

            DataTable tbl = cls.GetDataTable("SPLay_DMSITE");
            //  ViewState["fLoadDMSITE"] = Obj.Dt;
            RGDMSITE.DataSource = tbl;
            RGDMSITE.DataBind();
        }
        void fLoadDSSITE()
        {

            DataTable tbl = cls.GetDataTable("SP_DSSITE");
            RadIDSITE.DataSource = tbl;
            RadIDSITE.DataBind();
        }
        void fLoadUser()
        {

            DataTable tbl = cls.GetDataTable("SP_NHANVIEN");
            radcomboUser.DataSource = tbl;

            radcomboUser.DataBind();
        }
        public bool fBool(object Value)
        {
            try
            {
                return bool.Parse(Value.ToString());
            }
            catch { return false; }
        }
        void fLoadDMSITE_DA_PHANQUYEN()
        {

            DataTable tbl = cls.GetDataTable("SP_DS_PhanQuyen", "@manv", radcomboUser.SelectedValue);
            RGPHANQUYEN.DataSource = tbl;
            RGPHANQUYEN.DataBind();
        }
        void fLoadDMQuyen_Trong_Luoi()
        {
            for (int i = 0; i < RGPHANQUYEN.Items.Count; i++)
            {
                RadioButtonList rbQuyen = (RadioButtonList)RGPHANQUYEN.Items[i].FindControl("rbQuyen");
                fLoad_DM_Quyen_Ctrl_Luoi(rbQuyen);
            }
        }
        void fLoad_DM_Quyen_Ctrl_Luoi(RadioButtonList Quyen)
        {

            DataTable tbl = cls.GetDataTable("SP_Lay_DMQuyen");
            Quyen.DataSource = tbl;
            Quyen.DataBind();
        }
        void fLoad_Chon_Quyen()
        {

            DataTable tbl = cls.GetDataTable("SP_DS_PhanQuyen", new string[] { "@manv" }, new object[] { radcomboUser.SelectedValue });
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                for (int j = 0; j < RGPHANQUYEN.Items.Count; j++)
                {
                    RadioButtonList rbQuyen = (RadioButtonList)RGPHANQUYEN.Items[j].FindControl("rbQuyen");
                    HiddenField hdIDSITE = (HiddenField)RGPHANQUYEN.Items[j].FindControl("hdIDSITE");
                    if (hdIDSITE.Value == tbl.Rows[i]["IDSITE"].ToString())
                    {
                        rbQuyen.SelectedValue = tbl.Rows[i]["IDQuyen"].ToString();
                        break;
                    }
                }
            }
        }
        void fLoad_RGDMQuyen()
        {

            DataTable tbl = cls.GetDataTable("SP_Lay_DMQuyen");
            RGDMQuyen.DataSource = tbl;
            RGDMQuyen.DataBind();
        }
        protected void RGDMSITE_CancelCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

            fLoadDMSITE();

        }
        protected void RGDMSITE_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            HiddenField IDSITE = (HiddenField)RGDMSITE.Items[e.Item.ItemIndex].FindControl("hfIDSITE");

            cls.Xoa(new string[] { "@IDSITE" }, new object[] { IDSITE.Value }, "SP_DEL_DMSITE");

            fLoadDMSITE();
        }
        protected void RGDMSITE_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(Telerik.Web.UI.GridEditFormItem.EditFormUserControlID);
            TextBox IDSITE = userControl.FindControl("tbIDSITE") as TextBox;
            TextBox TenSite = userControl.FindControl("tbTenSite") as TextBox;
            CheckBox HieuLuc = userControl.FindControl("cbHieuLuc") as CheckBox;

            #region Kiểm tra trùng

            DataTable tbl = cls.GetDataTable("SPLaySITE_ID", new string[] { "@IDSITE" }, new object[] { IDSITE.Text.ToLower() });
            if (tbl.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Đã có danh mục " + IDSITE.Text.ToLower() + " này.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
                fLoadDMSITE();
                return;
            }
            #endregion

            #region Thêm id mới

            bool kq = cls.bThem(new string[] { "@IDSITE", "@TENSITE", "@HIEULUC" }, new object[] { IDSITE.Text.ToLower(), TenSite.Text, HieuLuc.Checked }, "SPThemSITE1");
            if (kq == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Đã thêm thành công.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Cập nhật thất bại. Vui lòng thử lại sau');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
            fLoadDMSITE();
            #endregion
        }
        protected void RGDMSITE_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == Telerik.Web.UI.RadGrid.InitInsertCommandName)
            {
                e.Canceled = true;
                e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/InsertDMSITE.ascx";
                e.Item.OwnerTableView.InsertItem();
                Telerik.Web.UI.GridEditableItem insertedItem = e.Item.OwnerTableView.GetInsertItem();
                RGDMSITE.MasterTableView.CurrentPageIndex = 0;

                fLoadDMSITE();

            }
            if (e.CommandName == Telerik.Web.UI.RadGrid.EditCommandName)
            {
                e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/UpdateDMSITE.ascx";

                fLoadDMSITE();

            }
            if (e.CommandName == Telerik.Web.UI.RadGrid.RebindGridCommandName)
            {
                fLoadDMSITE();
            }
        }
        protected void RGDMSITE_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {

            fLoadDMSITE();

        }
        protected void RGDMSITE_PageSizeChanged(object source, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {

            fLoadDMSITE();

        }
        protected void RGDMSITE_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(Telerik.Web.UI.GridEditFormItem.EditFormUserControlID);
            TextBox IDSITE = userControl.FindControl("tbIDSITE") as TextBox;
            HiddenField IDSITECu = userControl.FindControl("hdIDSITE") as HiddenField;
            TextBox TenSite = userControl.FindControl("tbTenSite") as TextBox;
            CheckBox HieuLuc = userControl.FindControl("cbHieuLuc") as CheckBox;

            #region Kiểm tra trùng
            if (IDSITE.Text.ToLower() != IDSITECu.Value)
            {

                DataTable tbl = cls.GetDataTable("SPLaySITE_ID", new string[] { "@IDSITE" }, new object[] { IDSITE.Text.ToLower() });
                if (tbl.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Đã có danh mục " + IDSITE.Text.ToLower() + " này.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
                    fLoadDMSITE();
                    return;
                }
            }
            #endregion

            #region Cập nhật id mới

            bool kq = cls.bThem(new string[] { "@IDSITE", "@IDSITECu", "@TENSITE", "@HIEULUC" }, new object[] { IDSITE.Text.ToLower(), IDSITECu.Value, TenSite.Text, HieuLuc.Checked }, "SPCapNhatSITE");
            if (kq == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Đã cập nhật thành công.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Không thể cập nhật vì có tham chiếu.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
            fLoadDMSITE();
            #endregion
        }
        protected void RadTabStrip1_TabClick(object sender, Telerik.Web.UI.RadTabStripEventArgs e)
        {
            fLoadDMSITE();
            fLoadDSSITE();
            fLoadDMSITE_DA_PHANQUYEN();
            fLoadDMQuyen_Trong_Luoi();
            fLoad_Chon_Quyen();
            fLoad_RGDMQuyen();
        }
        //protected void tbMANV_TextChanged(object sender, EventArgs e)
        //{
        //    tbTenNV.Text = "";
        //    Obj = new clsObj();
        //    Obj.Parameter = new string[] { "@manv" };
        //    Obj.ValueParameter = new object[] { tbMANV.Text.Trim() };
        //   // Obj.CnnString = wqlvattu;
        //    Obj.SpName = "SP_NHANVIEN_ID";
        //    Sql.fGetData(Obj);
        //    if (Obj.Dt.Rows.Count > 0)
        //    {
        //        tbTenNV.Text = Obj.Dt.Rows[0]["ten"].ToString();
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Không có mã nhân viên này.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
        //    }
        //    fLoadDMSITE_DA_PHANQUYEN();
        //    fLoadDMQuyen_Trong_Luoi();
        //    fLoad_Chon_Quyen();
        //}
        protected void btLuu_Click(object sender, ImageClickEventArgs e)
        {
            //#region Kiểm tra tồn tại nhân viên
            //if (tbTenNV.Text=="")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Không có mã nhân viên này.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            //    fLoadDMSITE_DA_PHANQUYEN();
            //    return;
            //}
            //#endregion

            #region Kiểm tra đã chọn phân quyền
            if (rdDMQuyen.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Vui lòng chọn quyền.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
                return;
            }
            #endregion

            #region Kiểm tra trùng

            DataTable tbl = cls.GetDataTable("SP_Lay_PhanQuyen_ID", new string[] { "@manv", "@idsite" }, new object[] { radcomboUser.SelectedValue, RadIDSITE.SelectedValue });
            if (tbl.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Đã có phân quyền cho chức năng này.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
                return;
            }
            #endregion

            #region Lưu phân quyền

            bool kq = cls.bThem(new string[] { "@manv", "@idsite", "@IDQuyen" }, new object[] { radcomboUser.SelectedValue, RadIDSITE.SelectedValue, rdDMQuyen.SelectedValue }, "SP_Them_PhanQuyen");
            if (kq == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Phân quyền thành công.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Phân quyền thất bại. Vui lòng thử lại sau.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
            #endregion

            fLoadDMSITE_DA_PHANQUYEN();
            fLoadDMQuyen_Trong_Luoi();
            fLoad_Chon_Quyen();
        }
        protected void rbQuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList rbQuyen = (RadioButtonList)sender;
            int Dem = 0;
            for (int i = 0; i < RGPHANQUYEN.Items.Count; i++)
            {
                RadioButtonList rbQuyenLuoi = (RadioButtonList)RGPHANQUYEN.Items[i].FindControl("rbQuyen");
                if (rbQuyen == rbQuyenLuoi)
                {
                    break;
                }
                Dem += 1;
            }
            HiddenField hdIDSITE = (HiddenField)RGPHANQUYEN.Items[Dem].FindControl("hdIDSITE");


            bool kq = cls.bCapNhat(new string[] { "@manv", "@idsite", "@IDQuyen" }, new object[] { radcomboUser.SelectedValue, hdIDSITE.Value, rbQuyen.SelectedValue }, "SPCapNhat_PhanQuyen");
            if (kq == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Cập nhật thất bại. Vui lòng thử lại sau');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Cập nhật thành công.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
        }
        protected void RGPHANQUYEN_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            HiddenField hdIDSITE = (HiddenField)RGPHANQUYEN.Items[e.Item.ItemIndex].FindControl("hdIDSITE");

            bool kq = cls.bCapNhat(new string[] { "@manv", "@idsite" }, new object[] { radcomboUser.SelectedValue, hdIDSITE.Value }, "SP_DEL_PHANQUYEN");
            if (kq == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Xoá thành công.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Xoá thất bại. Vui lòng thử lại sau.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
            fLoadDMSITE_DA_PHANQUYEN();
            fLoadDMQuyen_Trong_Luoi();
            fLoad_Chon_Quyen();

        }
        protected void RGDMQuyen_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == Telerik.Web.UI.RadGrid.InitInsertCommandName)
            {
                e.Canceled = true;
                e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/InsertDMQuyen.ascx";
                e.Item.OwnerTableView.InsertItem();
                Telerik.Web.UI.GridEditableItem insertedItem = e.Item.OwnerTableView.GetInsertItem();
                RGDMSITE.MasterTableView.CurrentPageIndex = 0;
                fLoad_RGDMQuyen();
            }
            if (e.CommandName == Telerik.Web.UI.RadGrid.EditCommandName)
            {
                e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/UpdateDMQuyen.ascx";
                fLoad_RGDMQuyen();
            }
        }
        protected void RGDMQuyen_CancelCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            fLoad_RGDMQuyen();
        }
        protected void RGDMQuyen_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(Telerik.Web.UI.GridEditFormItem.EditFormUserControlID);
            TextBox IDQuyen = userControl.FindControl("tbIDQuyen") as TextBox;
            TextBox TenQuyen = userControl.FindControl("tbTenQuyen") as TextBox;

            #region Kiểm tra trùng

            DataTable tbl = cls.GetDataTable("SPLayQUYEN_ID", new string[] { "@IDQuyen" }, new object[] { IDQuyen.Text.ToLower() });
            if (tbl.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Đã có danh mục " + IDQuyen.Text.ToLower() + " này.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
                fLoadDMSITE();
                return;
            }
            #endregion

            #region Thêm id mới

            bool kq = cls.bThem(new string[] { "@IDQuyen", "@Quyen" }, new object[] { IDQuyen.Text.ToUpper(), TenQuyen.Text }, "SPINSERT_QUYEN");
            if (kq == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Đã thêm thành công.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Cập nhật thất bại. Vui lòng thử lại sau');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
            fLoad_RGDMQuyen();
            #endregion
        }
        protected void RGDMQuyen_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(Telerik.Web.UI.GridEditFormItem.EditFormUserControlID);
            TextBox IDQuyen = userControl.FindControl("tbIDQuyen") as TextBox;
            TextBox TenQuyen = userControl.FindControl("tbTenQuyen") as TextBox;
            HiddenField IDSITECu = userControl.FindControl("hdIDQuyen") as HiddenField;

            #region Kiểm tra trùng
            if (IDQuyen.Text.ToLower() != IDSITECu.Value)
            {

                DataTable tbl = cls.GetDataTable("SPLayQUYEN_ID", new string[] { "@IDQuyen" }, new object[] { IDQuyen.Text.ToLower() });
                if (tbl.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Đã có danh mục " + IDQuyen.Text.ToLower() + " này.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
                    fLoad_RGDMQuyen();
                    return;
                }
            }
            #endregion

            #region Cập nhật id mới

            bool kq = cls.bThem(new string[] { "@IDQuyen", "@IDQuyenCu", "@Quyen" }, new object[] { IDQuyen.Text.ToUpper(), IDSITECu.Value, TenQuyen.Text }, "SPUPDATE_QUYEN");
            if (kq == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Đã cập nhật thành công.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Không thể cập nhật vì có tham chiếu.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
            fLoad_RGDMQuyen();
            #endregion
        }
        protected void RGDMQuyen_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            HiddenField IDQuyen = (HiddenField)RGDMQuyen.Items[e.Item.ItemIndex].FindControl("hfIDQuyen");

            bool kq = cls.bCapNhat(new string[] { "@IDQuyen" }, new object[] { IDQuyen.Value }, "SP_DEL_QUYEN");
            if (kq == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Đã xoá thành công.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Xoá thất bại do dữ liệu có tham chiếu.');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
            fLoad_RGDMQuyen();
        }
        protected void radcomboUser_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            fLoadDMSITE_DA_PHANQUYEN();
            fLoadDMQuyen_Trong_Luoi();
            fLoad_Chon_Quyen();
        }
    }
}