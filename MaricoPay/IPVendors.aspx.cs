using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
using Telerik.Web.UI;
using System.Data;
namespace MaricoPay
{
    public partial class IPVendors:clsPhanQuyenCaoCap
    {
        clsObj Obj;
        clsSql Sql = new clsSql();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fLoad();
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
        public bool fBool(object value)
        {
            if (value.ToString() == "")
            {
                return false;
            }
            return bool.Parse(value.ToString());
        }
        void fLoad()
        {
            Obj = new clsObj();
            Obj.Parameter = new string[] { };
            Obj.ValueParameter = new object[] { };
            Obj.SpName = "spLoad_IP_Vendors";
            Sql.fGetData(Obj);
            ViewState["CurrentTable"] = Obj.Dt;
            RG.DataSource = Obj.Dt;
            RG.DataBind();
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
                fLoad();
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
                fLoad();
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
                fLoad();
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
                fLoad();
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
                fLoad();
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
                fLoad();
            }
        }
        protected void RG_DeleteCommand(object source, GridCommandEventArgs e)
        {
            string ID = RG.Items[e.Item.ItemIndex]["ID"].Text;
            Obj = new clsObj();
            Obj.Parameter = new string[] { "@ID" };
            Obj.ValueParameter = new object[] { ID };
            Obj.SpName = "spDelete_IP_Vendors";
            Sql.fNonGetData(Obj);
            if (Obj.KetQua < 1)
            {
                lbLoi.Text = "<font color='red'>Xóa thất bại. Vui lòng thử lại sau.</font>";
            }
            else
            {
                lbLoi.Text = "<font color='blue'>Xóa thành công.</font>";
            }
            fLoad();
        }
        protected void RG_InsertCommand(object source, GridCommandEventArgs e)
        {
            lbLoi.Text = "";
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
         //   float ID = float.Parse((userControl.FindControl("rnID") as RadNumericTextBox).Value.ToString());
            string TenNCC = (userControl.FindControl("tbTenNCC") as TextBox).Text.Trim();
            string NguoiLienHe = (userControl.FindControl("tbNguoiLienHe") as TextBox).Text.Trim();
            string DienThoaiDD = (userControl.FindControl("tbDienThoaiDD") as TextBox).Text.Trim();
            string DienThoaiBan = (userControl.FindControl("tbDienThoaiBan") as TextBox).Text.Trim();
            string Fax = (userControl.FindControl("tbFax") as TextBox).Text.Trim();
            string Email = (userControl.FindControl("tbEmail") as TextBox).Text.Trim();
            string DiaChi = (userControl.FindControl("tbDiaChi") as TextBox).Text.Trim();
            string GhiChu = (userControl.FindControl("tbGhiChu") as TextBox).Text.Trim();
            string VendorCodeSAP = (userControl.FindControl("tbVendorCodeSAP") as TextBox).Text.Trim();
            CheckBox HieuLuc = userControl.FindControl("cbHieuLuc") as CheckBox;
            //#region Kiểm tra trùng mã
            //Obj = new clsObj();
            //Obj.Parameter = new string[] { "@ID" };
            //Obj.ValueParameter = new object[] { ID };
            //Obj.SpName = "spLoad_IP_Vendors_ByID";
            //Sql.fGetData(Obj);
            //if (Obj.Dt.Rows.Count > 0)
            //{
            //    lbLoi.Text = "<font color='red'>Đã có mã này. Vui lòng thử lại sau.</font>";
            //    fLoad();
            //    return;
            //}
            //#endregion
            #region Insert
            Obj = new clsObj();
            Obj.Parameter = new string[] {"@TenNCC", "@NguoiLienHe", "@DienThoaiDD", "@DienThoaiBan", "@Fax", "@Email", "@DiaChi", "@GhiChu", "@VendorCodeSAP", "@HieuLuc" };
            Obj.ValueParameter = new object[] {TenNCC, NguoiLienHe, DienThoaiDD, DienThoaiBan, Fax, Email, DiaChi, GhiChu, VendorCodeSAP, HieuLuc.Checked };
            Obj.SpName = "spInsert_IP_Vendors";
            Sql.fNonGetData(Obj);
            if (Obj.KetQua < 1)
            {
                lbLoi.Text = "<font color='red'>Thêm thất bại. Vui lòng thử lại sau.</font>";
            }
            else
            {
                lbLoi.Text = "<font color='blue'>Thêm thành công.</font>";
            }
            #endregion
            fLoad();
        }
        protected void RG_ItemCommand(object source, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case Telerik.Web.UI.RadGrid.InitInsertCommandName:
                    RG.MasterTableView.ClearEditItems();
                    e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/ISIPVendors.ascx";
                    e.Item.OwnerTableView.InsertItem();
                    if (ViewState["CurrentTable"] != null)
                    {
                        RG.DataSource = (DataTable)ViewState["CurrentTable"];
                        RG.DataBind();
                    }
                    else
                    {
                        fLoad();
                    } break;
                case Telerik.Web.UI.RadGrid.EditCommandName:
                    RG.MasterTableView.IsItemInserted = false;
                    e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/UPIPVendors.ascx";
                    if (ViewState["CurrentTable"] != null)
                    {
                        RG.DataSource = (DataTable)ViewState["CurrentTable"];
                        RG.DataBind();
                    }
                    else
                    {
                        fLoad();
                    } break;
                case Telerik.Web.UI.RadGrid.FilterCommandName:
                    if (ViewState["CurrentTable"] != null)
                    {
                        RG.DataSource = (DataTable)ViewState["CurrentTable"];
                        RG.DataBind();
                    }
                    else
                    {
                        fLoad();
                    } break;
                case Telerik.Web.UI.RadGrid.RebindGridCommandName:
                    fLoad();
                    break;
            }
        }
        protected void RG_UpdateCommand(object source, GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            float ID = float.Parse((userControl.FindControl("rnID") as RadNumericTextBox).Value.ToString());
            string TenNCC = (userControl.FindControl("tbTenNCC") as TextBox).Text.Trim();
            string NguoiLienHe = (userControl.FindControl("tbNguoiLienHe") as TextBox).Text.Trim();
            string DienThoaiDD = (userControl.FindControl("tbDienThoaiDD") as TextBox).Text.Trim();
            string DienThoaiBan = (userControl.FindControl("tbDienThoaiBan") as TextBox).Text.Trim();
            string Fax = (userControl.FindControl("tbFax") as TextBox).Text.Trim();
            string Email = (userControl.FindControl("tbEmail") as TextBox).Text.Trim();
            string DiaChi = (userControl.FindControl("tbDiaChi") as TextBox).Text.Trim();
            string GhiChu = (userControl.FindControl("tbGhiChu") as TextBox).Text.Trim();
            string VendorCodeSAP = (userControl.FindControl("tbVendorCodeSAP") as TextBox).Text.Trim();
            CheckBox HieuLuc = userControl.FindControl("cbHieuLuc") as CheckBox;
            #region Update
            Obj = new clsObj();
            Obj.Parameter = new string[] { "@ID", "@TenNCC", "@NguoiLienHe", "@DienThoaiDD", "@DienThoaiBan", "@Fax", "@Email", "@DiaChi", "@GhiChu", "@VendorCodeSAP", "@HieuLuc" };
            Obj.ValueParameter = new object[] { ID, TenNCC, NguoiLienHe, DienThoaiDD, DienThoaiBan, Fax, Email, DiaChi, GhiChu, VendorCodeSAP, HieuLuc.Checked };
            Obj.SpName = "spEdit_IP_Vendors";
            Sql.fNonGetData(Obj);
            if (Obj.KetQua < 1)
            {
                lbLoi.Text = "<font color='red'>Cập nhật thất bại. Vui lòng thử lại sau.</font>";
            }
            else
            {
                lbLoi.Text = "<font color='blue'>Cập nhật thành công.</font>";
            }
            #endregion
            fLoad();
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
    }
}