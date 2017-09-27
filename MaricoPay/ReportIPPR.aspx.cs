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
    public partial class ReportIPPR: clsPhanQuyenCaoCap
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
                    LoadIPPRReport(Session["username"], raddateTuNgay.SelectedDate.Value, raddateDenNgay.SelectedDate.Value);
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        private void LoadIPPRReport(object username,DateTime tungay,DateTime denngay)
        {

            DataTable kq = new DataTable();

            kq = cls.GetDataTable("sp_ReportIPPR", new string[] { "@tungay", "@denngay","@username" }, new object[] {tungay, denngay,username });
            Session["RIPPR"] = kq;
            radIPPRReport.DataSource = kq;
            radIPPRReport.DataBind();

        }

        protected void radradIPPRReport_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case Telerik.Web.UI.RadGrid.FilterCommandName:
                    if (Session["RIPPR"] != null)
                    {
                        DataTable kq = (DataTable)Session["RIPPR"];
                        radIPPRReport.DataSource = kq;
                        radIPPRReport.DataBind();
                    }
                    else
                    {
                        LoadIPPRReport(Session["username"], raddateTuNgay.SelectedDate.Value, raddateDenNgay.SelectedDate.Value);
                    }
                    break;
                case Telerik.Web.UI.RadGrid.SortCommandName:
                    if (Session["RIPPR"] != null)
                    {
                        DataTable kq = (DataTable)Session["RIPPR"];
                        radIPPRReport.DataSource = kq;
                        radIPPRReport.DataBind();
                    }
                    else
                    {
                        LoadIPPRReport(Session["username"], raddateTuNgay.SelectedDate.Value, raddateDenNgay.SelectedDate.Value);
                    }
                    break;
            }
        }

        protected void btView_Click(object sender, EventArgs e)
        {
            LoadIPPRReport(Session["username"], raddateTuNgay.SelectedDate.Value, raddateDenNgay.SelectedDate.Value);
        }

        protected void btExcel_Click(object sender, EventArgs e)
        {
            try
            {
                radIPPRReport.MasterTableView.Caption = "<span><br/><b>PR Report " + cls.Date2sDdMmYyy(raddateTuNgay.SelectedDate.Value, "/") + " TO " + cls.Date2sDdMmYyy(raddateDenNgay.SelectedDate.Value, "/") + "</b></span>";
                radIPPRReport.ExportSettings.ExportOnlyData = true;
                radIPPRReport.ExportSettings.UseItemStyles = true;
                radIPPRReport.ExportSettings.IgnorePaging = true;
                radIPPRReport.ExportSettings.FileName = "PRReport";
                radIPPRReport.MasterTableView.GridLines = GridLines.None;
                radIPPRReport.MasterTableView.ExportToExcel();
            }
            catch
            {
               
            }
        }

      
       
    }
}