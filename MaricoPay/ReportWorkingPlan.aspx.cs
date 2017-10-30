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
    public partial class ReportWorkingPlan : System.Web.UI.Page
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
                    LoadClaimApp(Session["username"], raddateTuNgay.SelectedDate.Value, raddateDenNgay.SelectedDate.Value,0,"");
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        private void LoadClaimApp(object username,DateTime tungay,DateTime denngay,int loai,string docno)
        {

            //DataTable kq = new DataTable();

            //kq = cls.GetDataTable("sp_ReportWorkingPlan", new string[] { "@username", "@tungay", "@denngay","@Loai","@docno" }, new object[] { username, tungay, denngay,loai,docno });
            //Session["RWK"] = kq;
            //RadGrid1.DataSource = kq;
            //RadGrid1.DataBind();

            DataTable tbl;
            if (CacheHelper.Get("creportwk_" + cls.cToString(username) + cls.Date2sDdMmYyy(tungay,"")+ cls.Date2sDdMmYyy(denngay,"")+cls.cToString0(loai)+docno) != null)
            {
                tbl = (DataTable)CacheHelper.Get("creportwk_" + cls.cToString(username) + cls.Date2sDdMmYyy(tungay, "") + cls.Date2sDdMmYyy(denngay, "") + cls.cToString0(loai) + docno);
            }
            else
            {
                tbl = cls.GetDataTable("sp_ReportWorkingPlan", new string[] { "@username", "@tungay", "@denngay", "@Loai", "@docno" }, new object[] { username, tungay, denngay, loai, docno });

                CacheHelper.Set("creportwk_" + cls.cToString(username) + cls.Date2sDdMmYyy(tungay, "") + cls.Date2sDdMmYyy(denngay, "") + cls.cToString0(loai) + docno, tbl, 30);
            }
            Session["RWK"] = tbl;
            RadGrid1.DataSource = tbl;
            RadGrid1.DataBind();
        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case Telerik.Web.UI.RadGrid.FilterCommandName:
                    if (Session["RWK"] != null)
                    {
                        DataTable kq = (DataTable)Session["RWK"];
                        RadGrid1.DataSource = kq;
                        RadGrid1.DataBind();
                    }
                    else
                    {
                        LoadClaimApp(Session["username"], raddateTuNgay.SelectedDate.Value, raddateDenNgay.SelectedDate.Value, cls.cToInt(Session["LoaiRPSales"]), txtsochungtu.Text.Trim());
                    }
                    break;
                case Telerik.Web.UI.RadGrid.SortCommandName:
                    if (Session["RWK"] != null)
                    {
                        DataTable kq = (DataTable)Session["RWK"];
                        RadGrid1.DataSource = kq;
                        RadGrid1.DataBind();
                    }
                    else
                    {
                        LoadClaimApp(Session["username"], raddateTuNgay.SelectedDate.Value, raddateDenNgay.SelectedDate.Value, cls.cToInt(Session["LoaiRPSales"]), txtsochungtu.Text.Trim());
                    }
                    break;
            }
        }

        protected void btView_Click(object sender, EventArgs e)
        {
            Session["LoaiRPSales"] = 0;
            LoadClaimApp(Session["username"], raddateTuNgay.SelectedDate.Value, raddateDenNgay.SelectedDate.Value,1,"");
        }

        protected void btExcel_Click(object sender, EventArgs e)
        {
            try
            {
                RadGrid1.MasterTableView.Caption = "<span><br/><b>WORKING PLAN FROM " + cls.Date2sDdMmYyy(raddateTuNgay.SelectedDate.Value, "/") + " TO " + cls.Date2sDdMmYyy(raddateDenNgay.SelectedDate.Value, "/") + "</b></span>";
                RadGrid1.ExportSettings.ExportOnlyData = true;
                RadGrid1.ExportSettings.UseItemStyles = true;
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.FileName = "WorkingPlanSales";
                RadGrid1.MasterTableView.GridLines = GridLines.None;
                RadGrid1.MasterTableView.ExportToExcel();
            }
            catch
            {
               
            }
        }

        protected void btViewDocNo_Click(object sender, EventArgs e)
        {
            Session["LoaiRPSales"] = 1;
            LoadClaimApp(Session["username"], raddateTuNgay.SelectedDate.Value, raddateDenNgay.SelectedDate.Value,1,txtsochungtu.Text.Trim());
        }

      
       
    }
}