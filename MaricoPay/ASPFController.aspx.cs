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
using Data;
using Telerik.Web.UI;

namespace MaricoPay
{
  //  clsPhanQuyenCaoCap

    public partial class ASPFController : System.Web.UI.Page  
    {

        Cclass cls = new Cclass();
        clsObj Obj = new clsObj();
        clsSql Sql = new clsSql();

        protected void Page_Load(object sender, EventArgs e)
        {




            if (!IsPostBack)
            {
                LoadGird();

            }
            if (Session["CreateASPF"] != null)
            {
                LoadGird();
            }
            Session["CreateASPF"] = null;



            if (Session["email"] != null)
            {

                if (!IsPostBack)
                {
                    LoadGird();

                }
                if (Session["CreateASPF"] != null)
                {
                    LoadGird();
                }
                Session["CreateASPF"] = null;
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        

        private void LoadGird()
        {
            DataTable tbl = cls.GetDataTable("Sp_load_aspf_ByController_User", new string[] { "@user" }, new object[] { Session["email"].ToString() });

           // DataTable tbl = cls.GetDataTable("Sp_load_aspf_ByController");
            RadGrid2.DataSource = tbl;
            RadGrid2.DataBind();
        }


       

        protected void RadGrid2_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == Telerik.Web.UI.RadGrid.InitInsertCommandName)
            {
                RadGrid2.MasterTableView.ClearEditItems();
                e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/CreateASPF.ascx";
                e.Item.OwnerTableView.InsertItem();

                Session["CreateASPF"] = "CreateASPF";
                LoadGird();

                Session["CreateASPF"] = "CreateASPF";
               
            }
            if (e.CommandName == Telerik.Web.UI.RadGrid.EditCommandName)
            {
                RadGrid2.MasterTableView.IsItemInserted = false;
                e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/CreateASPF.ascx";
                LoadGird();
            }
            if (e.CommandName == "Cancel")
            {

                LoadGird();
            }
            if (e.CommandName == Telerik.Web.UI.RadGrid.FilterCommandName)
            {

                LoadGird();
            }
           
        }

        protected void RadGrid2_InsertCommand(object sender, GridCommandEventArgs e)
        {
            Session["CreateASPF"] = "CreateASPF";
        }
        protected void RadGrid2_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            
            LoadGird();
           
        }
        protected void RadGrid2_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
           
            LoadGird();
         
        }

        protected void RadGrid2_ItemCreated(object sender, GridItemEventArgs e)
        {

            


          

        }

        protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                HyperLink editLink = (HyperLink)e.Item.FindControl("LinkDetail");

                HiddenField HFID = (HiddenField)e.Item.FindControl("HFID");

                HiddenField HFStatus = (HiddenField)e.Item.FindControl("HFStatus");

                HiddenField hdRevise = (HiddenField)e.Item.FindControl("hdRevise");

                HyperLink LinkRevise = (HyperLink)e.Item.FindControl("LinkRevise");

                if (HFStatus.Value == "0")
                {
                    CheckBox CheckBox1 = (CheckBox)e.Item.FindControl("chkSelect");
                    CheckBox1.Visible = true;
                }
                else
                {
                    CheckBox CheckBox1 = (CheckBox)e.Item.FindControl("chkSelect");
                    CheckBox1.Visible = false;
                }
                if (hdRevise.Value == "0" || hdRevise.Value == "")
                {

                    DataTable tblto8881 = cls.GetDataTable("sp_Check_Approval_Revise", new string[] { "@docno" }, new object[] { HFID.Value });
                    /////////////////// duyet het roi ////////////////
                    if (tblto8881.Rows.Count > 0)
                    {
                        LinkRevise.Visible = true;


                    }
                    else
                    {
                        LinkRevise.Visible = false;
                    }


                }
                else
                {
                    //LinkRevise.Visible = false;

                    if (hdRevise.Value != "")
                    {
                        GridDataItem item = (GridDataItem)e.Item;
                        TableCell cell = (TableCell)item["ID"];
                        cell.BackColor = System.Drawing.Color.Red;
                        //  LinkRevise.Visible = false;
                        LinkRevise.Text = hdRevise.Value;
                    }

                }

                editLink.Attributes["href"] = "#";
                editLink.Attributes["onclick"] = String.Format("return openRadWindowPreview('{0}');", HFID.Value);
            

            }

        }

        protected void btnXoa_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in RadGrid2.MasterTableView.Items)
            {
                CheckBox CheckBox1 = item.FindControl("chkSelect") as CheckBox;
                if (CheckBox1 != null && CheckBox1.Checked ==true)
                {
                    string strKey = item.GetDataKeyValue("ID").ToString();
                    cls.ExcuteSQL("Sp_Delete_aspf", new string[] { "@ID" }, new object[] { strKey });
                   
                }
            }
            LoadGird();
           
        }

        protected void RadGrid2_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            LoadGird();
        }

    }
}