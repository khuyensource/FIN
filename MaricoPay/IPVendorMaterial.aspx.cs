using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Data;
using System.Data;
namespace MaricoPay
{
    public partial class IPVendorMaterial : clsPhanQuyenCaoCap
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
            Obj.SpName = "spLoad_IPVendorMaterial";
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
            Obj.SpName = "spDelete_IPVendorMaterial";
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
          //  float ID = float.Parse((userControl.FindControl("rnID") as RadNumericTextBox).Value.ToString());
            RadComboBox radcomboMaterial = userControl.FindControl("RadComboMaterial") as RadComboBox;
            radcomboMaterial.SelectedIndex=radcomboMaterial.FindItemByText(radcomboMaterial.Text).Index;
            float Material = float.Parse(radcomboMaterial.SelectedValue);

            RadComboBox radcomboVendor = userControl.FindControl("RadComboVendor") as RadComboBox;
            radcomboVendor.SelectedIndex = radcomboVendor.FindItemByText(radcomboVendor.Text).Index;
            float Vendor = float.Parse(radcomboVendor.SelectedValue);

          
            string Note = (userControl.FindControl("tbNote") as TextBox).Text.Trim();
            CheckBox Active = userControl.FindControl("cbActive") as CheckBox;
            //#region Kiểm tra trùng mã
            //Obj = new clsObj();
            //Obj.Parameter = new string[] { "@ID" };
            //Obj.ValueParameter = new object[] { ID };
            //Obj.SpName = "spLoad_IPVendorMaterial_ByID";
            //Sql.fGetData(Obj);
            //if (Obj.Dt.Rows.Count > 0)
            //{
            //    lbLoi.Text = "<font color='red'>Đã có mã này. Vui lòng thử lại sau.</font>";
            //    fLoad();
            //    return;
            //}
            //#endregion
            #region Insert
            //if(Material<=0
            Obj = new clsObj();
            Obj.Parameter = new string[] { "@Material", "@Vendor", "@Note", "@Active" };
            Obj.ValueParameter = new object[] {Material, Vendor, Note, Active.Checked };
            Obj.SpName = "spInsert_IPVendorMaterial";
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
                    e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/InsertIPVendorMaterial.ascx";
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
                    e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/UpdateIPVendorMaterial.ascx";
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
            RadComboBox radcomboMaterial = userControl.FindControl("RadComboMaterial") as RadComboBox;
            radcomboMaterial.SelectedIndex = radcomboMaterial.FindItemByText(radcomboMaterial.Text).Index;
            float Material = float.Parse(radcomboMaterial.SelectedValue);

            RadComboBox radcomboVendor = userControl.FindControl("RadComboVendor") as RadComboBox;
            radcomboVendor.SelectedIndex = radcomboVendor.FindItemByText(radcomboVendor.Text).Index;
            float Vendor = float.Parse(radcomboVendor.SelectedValue);
            string Note = (userControl.FindControl("tbNote") as TextBox).Text.Trim();
            CheckBox Active = userControl.FindControl("cbActive") as CheckBox;
            #region Update
            Obj = new clsObj();
            Obj.Parameter = new string[] { "@ID", "@Material", "@Vendor", "@Note", "@Active" };
            Obj.ValueParameter = new object[] { ID, Material, Vendor, Note, Active.Checked };
            Obj.SpName = "spEdit_IPVendorMaterial";
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