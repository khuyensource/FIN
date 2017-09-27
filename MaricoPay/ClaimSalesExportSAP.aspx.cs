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
    public partial class ClaimSalesExportSAP : clsPhanQuyenCaoCap
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] != null /*&& Request.Form["us"] != null*/)
                // if (Request.QueryString["us"] != null)//click vao avatar
                {
                    getYear();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        private void getYear()
        {
            DataTable tbly = cls.GetDataTable("sp_getYear");
            dropNam.DataSource = tbly;
            dropNam.DataBind();
        }
        private void LoadClaimEx(object postdate, object loai, object thang, object nam)
        {

            DataTable kq = new DataTable();

            kq = cls.GetDataTable("sp_ExportTextFV60", new string[] { "@PostDate", "@Loai", "@Thang", "@Nam" }, new object[] { postdate,loai, dropThang.SelectedValue, dropNam.SelectedValue });
            Session["ClaimSalesEx"] = kq;
            RadGrid1.DataSource = kq;
            RadGrid1.DataBind();

        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case Telerik.Web.UI.RadGrid.FilterCommandName:
                    if (Session["ClaimSalesEx"] != null)
                    {
                        DataTable kq = (DataTable)Session["ClaimSalesEx"];
                        RadGrid1.DataSource = kq;
                        RadGrid1.DataBind();
                    }
                    else
                    {
                        LoadClaimEx(txtPostdate.Text.Trim(), dropLoai.SelectedValue, dropThang.SelectedValue, dropNam.SelectedValue);
                    }
                    break;
                case Telerik.Web.UI.RadGrid.SortCommandName:
                    if (Session["ClaimSalesEx"] != null)
                    {
                        DataTable kq = (DataTable)Session["ClaimSalesEx"];
                        RadGrid1.DataSource = kq;
                        RadGrid1.DataBind();
                    }
                    else
                    {
                        LoadClaimEx(txtPostdate.Text.Trim(), dropLoai.SelectedValue, dropThang.SelectedValue, dropNam.SelectedValue);
                    }
                    break;
            }
        }

        protected void btView_Click(object sender, EventArgs e)
        {
            LoadClaimEx(txtPostdate.Text.Trim(), dropLoai.SelectedValue, dropThang.SelectedValue, dropNam.SelectedValue);
        }

        protected void btExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //RadGrid1.MasterTableView.Caption = "<span><br/><b>WORKING PLAN FROM " + cls.Date2sDdMmYyy(raddateTuNgay.SelectedDate.Value, "/") + " TO " + cls.Date2sDdMmYyy(raddateDenNgay.SelectedDate.Value, "/") + "</b></span>";
                RadGrid1.ExportSettings.ExportOnlyData = true;
                RadGrid1.ExportSettings.UseItemStyles = true;
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.FileName = "ClaimSalesImportSAP";
                RadGrid1.MasterTableView.GridLines = GridLines.None;
                RadGrid1.MasterTableView.ExportToExcel();
            }
            catch
            {
               
            }
        }

      
       
    }
}