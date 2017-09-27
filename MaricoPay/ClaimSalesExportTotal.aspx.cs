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
    public partial class ClaimSalesExportTotal : clsPhanQuyenCaoCap
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
        private void LoadClaimEx(object thang, object nam)
        {

            DataTable kq = new DataTable();

            kq = cls.GetDataTable("sp_ExportTotalClaimSales", new string[] { "@Thang", "@Nam" }, new object[] { dropThang.SelectedValue, dropNam.SelectedValue });
            Session["ClaimSalesExtotal"] = kq;
            RadGrid1.DataSource = kq;
            RadGrid1.DataBind();

        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case Telerik.Web.UI.RadGrid.FilterCommandName:
                    if (Session["ClaimSalesExtotal"] != null)
                    {
                        DataTable kq = (DataTable)Session["ClaimSalesExtotal"];
                        RadGrid1.DataSource = kq;
                        RadGrid1.DataBind();
                    }
                    else
                    {
                        LoadClaimEx( dropThang.SelectedValue, dropNam.SelectedValue);
                    }
                    break;
                case Telerik.Web.UI.RadGrid.SortCommandName:
                    if (Session["ClaimSalesExtotal"] != null)
                    {
                        DataTable kq = (DataTable)Session["ClaimSalesExtotal"];
                        RadGrid1.DataSource = kq;
                        RadGrid1.DataBind();
                    }
                    else
                    {
                        LoadClaimEx( dropThang.SelectedValue, dropNam.SelectedValue);
                    }
                    break;
            }
        }

        protected void btView_Click(object sender, EventArgs e)
        {
            LoadClaimEx(dropThang.SelectedValue, dropNam.SelectedValue);
        }

        protected void btExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string ft = "<br/><span> &emsp; &emsp; &emsp; &emsp; <B>NGƯỜI LẬP</B>  &emsp;  &emsp; &emsp;  &emsp;  <B>KẾ TOÁN TRƯỞNG</B> &emsp;  &emsp;  &emsp;  &emsp;  &emsp; <B>PHÓ TỔNG GIÁM ĐỐC</b></span>";
                ft = ft + "<BR/><span>&emsp; &emsp; &emsp; <I>(PREPARE BY)</I> &emsp; &emsp;  &emsp;  <I>(CHIEF ACCOUNTANT)</I>  &emsp;  &emsp;  &emsp; &emsp;  &emsp; &emsp; <I>(VP SALES)</I></span>";
                RadGrid1.MasterTableView.Controls.Add(new LiteralControl(ft));
                string cap = "<br/><span><b>CÔNG TY CP MARICO SOUTH EAST ASIA</b></span>";
                cap = cap + "<br/><span style=\"float:left; text-align:left;\"><i>MARICO SOUTH EAST ASIA CORPORATION</i></span>";
                cap = cap + "<br/><span style=\"float:left;\"><b>ĐỊA CHỈ: SỐ 3, ĐƯỜNG SỐ 05, KCN SÓNG THẦN 1, TX DĨ AN, BÌNH DƯƠNG</b></span>";
                cap = cap + "<br/><span style=\"float:left;\"><i>ADDRESS: NO 3, STREET NO 05, SONG THAN 1 INDUSTRIAL ZONE, DI AN DISTRIC, BINH DUONG PROVINCE</i></span>";
                cap = cap + "<br/><span style=\"float:left;\"><b>MST: 3700579324</b></span>";
                cap = cap + "<br/><span style=\"float:left;\"><i>TAX NUMBER: 3700579324</i></span>";
                cap = cap + "<br/><br/><br/>";
                cap = cap + "<span><b>DANH SÁCH THANH TOÁN CÔNG TÁC PHÍ THÁNG " + dropThang.SelectedValue + "/" + dropNam.SelectedValue + "</b></span>";
                cap = cap + "<br/><span><i>(PAYMENT LIST FOR TRAVEL EXPENSES IN " + dropThang.SelectedValue + "/" + dropNam.SelectedValue + ")</i></span>";
                cap = cap + "<br/><span><b>NGÀY " + DateTime.Now.Day.ToString() + " THÁNG " + DateTime.Now.Month.ToString() + " NĂM " + DateTime.Now.Year.ToString() + "</b></span>";
                cap = cap + "<br/><span><i>DATE " + DateTime.Now.Day.ToString() + " MONTH " + DateTime.Now.Month.ToString() + " YEAR " + DateTime.Now.Year.ToString() + "</i></span><br/><br/>";
                RadGrid1.MasterTableView.Caption = cap;
                RadGrid1.ExportSettings.ExportOnlyData = true;
                RadGrid1.ExportSettings.UseItemStyles = true;
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.FileName = "ClaimSalesTotal" + dropThang.SelectedValue+dropNam.SelectedValue;
                RadGrid1.MasterTableView.GridLines = GridLines.Both;
             //   RadGrid1.MasterTableView.Caption.
                RadGrid1.MasterTableView.ExportToExcel();
            }
            catch
            {
               
            }
        }

        protected void RadGrid1_ExcelExportCellFormatting(object sender, ExcelExportCellFormattingEventArgs e)
        {
            if ((e.FormattedColumn.UniqueName) == "TotalVN")
            {
                e.Cell.Style["mso-number-format"] = @"###,###,###";

            }
            
        }

      
       
    }
}