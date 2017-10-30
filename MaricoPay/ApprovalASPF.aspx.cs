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
    public partial class ApprovalASPF : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
        clsObj Obj = new clsObj();
        clsSql Sql = new clsSql();

        protected void Page_Load(object sender, EventArgs e)
        {
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
                LoadGird();
            }
            else
            {

                Response.Redirect("~/Login.aspx");
            }
            Session["CreateASPF"] = null;
           
        }
        private void LoadGird()
        {
            DataTable tbl = cls.GetDataTable("[Sp_load_aspf_Approval]", new string[] { "@user" }, new object[] {Session["email"]  });
            RadGrid2.DataSource = tbl;
            RadGrid2.DataBind();
        }

        protected void RadGrid2_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {

            LoadGird();

        }

        protected void RadGrid2_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {

            LoadGird();

        }
        protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                HyperLink editLink = (HyperLink)e.Item.FindControl("LinkDetail");

                HiddenField HFID = (HiddenField)e.Item.FindControl("HFID");
                HiddenField HDActivecode = (HiddenField)e.Item.FindControl("HDActivecode");
                HiddenField hdRevise = (HiddenField)e.Item.FindControl("hdRevise");
               // HiddenField HFStatus = (HiddenField)e.Item.FindControl("HFStatus");

               
              
                editLink.Attributes["href"] = "#";
                editLink.Attributes["onclick"] = String.Format("return openRadWindowPreview('{0}','{1}');", HFID.Value, HDActivecode.Value);


                HyperLink LinkRevise = (HyperLink)e.Item.FindControl("LinkRevise");
              


                LinkRevise.Attributes["href"] = "#";
                LinkRevise.Attributes["onclick"] = String.Format("return openRadWindowrevise('{0}','{1}');", HFID.Value, hdRevise.Value);


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

            }

        }

        protected void RadGrid2_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == Telerik.Web.UI.RadGrid.FilterCommandName)
            {

                LoadGird();
            }
            if (e.CommandName == "Cancel")
            {

                LoadGird();
            }
            if (e.CommandName == "Page")
            {

                LoadGird();
            }
        }

        protected void RadGrid2_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            LoadGird();
        }

    }
}