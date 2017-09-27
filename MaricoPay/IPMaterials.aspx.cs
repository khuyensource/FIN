﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
using System.Data;
using Telerik.Web.UI;
namespace MaricoPay
{
    public partial class IPMaterials : clsPhanQuyenCaoCap
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
            Obj.SpName = "spLoad_IPMaterial";
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
            Obj.SpName = "spDelete_IPMaterial";
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
            string Name = (userControl.FindControl("tbName") as TextBox).Text.Trim();
            string Note = (userControl.FindControl("tbNote") as TextBox).Text.Trim();
            string SAPCode = (userControl.FindControl("tbSAPCode") as TextBox).Text.Trim();
            CheckBox Active = userControl.FindControl("cbActive") as CheckBox;
            //#region Kiểm tra trùng mã
            //Obj = new clsObj();
            //Obj.Parameter = new string[] { "@ID" };
            //Obj.ValueParameter = new object[] { ID };
            //Obj.SpName = "spLoad_IPMaterial_ByID";
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
            Obj.Parameter = new string[] { "@Name", "@Note", "@SAPCode", "@Active" };
            Obj.ValueParameter = new object[] { Name, Note, SAPCode, Active.Checked };
            Obj.SpName = "spInsert_IPMaterial";
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
                    e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/ISIPMaterials.ascx";
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
                    e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/UPIPMaterials.ascx";
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
            string Name = (userControl.FindControl("tbName") as TextBox).Text.Trim();
            string Note = (userControl.FindControl("tbNote") as TextBox).Text.Trim();
            string SAPCode = (userControl.FindControl("tbSAPCode") as TextBox).Text.Trim();
            CheckBox Active = userControl.FindControl("cbActive") as CheckBox;
            #region Update
            Obj = new clsObj();
            Obj.Parameter = new string[] { "@ID", "@Name", "@Note", "@SAPCode", "@Active" };
            Obj.ValueParameter = new object[] { ID, Name, Note, SAPCode, Active.Checked };
            Obj.SpName = "spEdit_IPMaterial";
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