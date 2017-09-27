using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using MaricoPay.DB;
namespace MaricoPay
{
    public partial class ReportClaimSales : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] != null /*&& Request.Form["us"] != null*/)
                // if (Request.QueryString["us"] != null)//click vao avatar
                {
                  
                    raddateDenNgay.SelectedDate = DateTime.Now;
                    raddateTuNgay.SelectedDate = DateTime.Now.AddDays(-60);
                    LoadClaimApp(Session["username"], raddateTuNgay.SelectedDate.Value, raddateDenNgay.SelectedDate.Value);
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        private void LoadClaimApp(object username, DateTime tungay, DateTime denngay)
        {

            DataTable kq = new DataTable();

            kq = cls.GetDataTable("sp_ReportClaimSales", new string[] { "@username", "@tungay", "@denngay" }, new object[] { username, tungay, denngay });
            Session["RCS"] = kq;
            RadGrid1.DataSource = kq;
            RadGrid1.DataBind();

        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case Telerik.Web.UI.RadGrid.FilterCommandName:
                    if (Session["RCS"] != null)
                    {
                        DataTable kq = (DataTable)Session["RCS"];
                        RadGrid1.DataSource = kq;
                        RadGrid1.DataBind();
                    }
                    else
                    {
                        LoadClaimApp(Session["username"], raddateTuNgay.SelectedDate.Value, raddateDenNgay.SelectedDate.Value);
                    }
                    break;
                case Telerik.Web.UI.RadGrid.SortCommandName:
                    if (Session["RCS"] != null)
                    {
                        DataTable kq = (DataTable)Session["RCS"];
                        RadGrid1.DataSource = kq;
                        RadGrid1.DataBind();
                    }
                    else
                    {
                        LoadClaimApp(Session["username"], raddateTuNgay.SelectedDate.Value, raddateDenNgay.SelectedDate.Value);
                    }
                    break;
            }
        }

        protected void btView_Click(object sender, EventArgs e)
        {
            LoadClaimApp(Session["username"], raddateTuNgay.SelectedDate.Value, raddateDenNgay.SelectedDate.Value);
        }

        protected void btExcel_Click(object sender, EventArgs e)
        {
            try
            {
                RadGrid1.MasterTableView.Caption = "<span><br/><b>CLAIM FROM " + cls.Date2sDdMmYyy(raddateTuNgay.SelectedDate.Value, "/") + " TO " + cls.Date2sDdMmYyy(raddateDenNgay.SelectedDate.Value, "/") + "</b></span>";
                RadGrid1.ExportSettings.ExportOnlyData = true;
                RadGrid1.ExportSettings.UseItemStyles = true;
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.FileName = "ClaimSales";
                RadGrid1.MasterTableView.GridLines = GridLines.None;
                RadGrid1.MasterTableView.ExportToExcel();
            }
            catch
            {

            }
        }
       
    }
}