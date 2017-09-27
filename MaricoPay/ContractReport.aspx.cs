using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace MaricoPay
{
    public partial class ContractReport : clsPhanQuyenCaoCap
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                raddateTo.SelectedDate = DateTime.Now.Date;
                raddateFrom.SelectedDate = DateTime.Now.AddDays(-30);
                LoadOrg("DO");
            }
        }

        protected void btBaoCao_Click(object sender, EventArgs e)
        {
           LoadC(1);
        }
        private void LoadOrg(string type)
        {
            DataTable tbl = new DataTable();
            tbl = cls.GetDataTable("sp_getOrgReport", "@type", type);
            dropOrg.DataSource = tbl;
            dropOrg.DataBind();
        }
       private void LoadC(int loaixem)
       {
            Cclass cls = new Cclass();
            DataTable tbl = cls.GetDataTable("sp_getContractReport", new string[] { "@TuNgay", "@DenNgay", "@Department", "@DateType", "@Docno", "@LoaiXem" }, new object[] { raddateFrom.SelectedDate.Value, raddateTo.SelectedDate.Value, dropOrg.SelectedValue, RadioButtonList1.SelectedValue, txtdocno.Text.Trim(), loaixem });
            Session["contractreport"] = tbl;
            radContractReport.DataSource = tbl;
            radContractReport.DataBind();
       }
       
        protected void radContractReport_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case Telerik.Web.UI.RadGrid.FilterCommandName:
                    DataTable tbl = (DataTable)Session["contractreport"];
                    radContractReport.DataSource = tbl;
                    radContractReport.DataBind();
                    break;
            }
        }
        private void configexcel(string filename)
        {
            //GridFooterItem footer = RadGrid1.MasterTableView.GetItems(GridItemType.Footer)[0] as GridFooterItem;
            //foreach (GridColumn column in RadGrid1.MasterTableView.Columns)
            //{
            //    footer[column.UniqueName].Text = "<b>" + footer[column.UniqueName].Text + "</b>";
            //}
            // RadGrid1.MasterTableView.Caption = "<span  style=' font-weight:bold; text-align:left;'>BÁO CÁO SKU CỦA " + dr_TTPP.SelectedItem.Text + " Từ " + drpFrMonth.SelectedValue + " đên" + drpToMonth.SelectedValue + ", ngày lấy BC " + DateTime.Now.ToString() + "</span>";
            radContractReport.ExportSettings.ExportOnlyData = true;
            radContractReport.ExportSettings.IgnorePaging = true;
            radContractReport.ExportSettings.FileName = filename;
            radContractReport.ExportSettings.OpenInNewWindow = true;
            radContractReport.MasterTableView.GridLines = GridLines.Both;
        }
        protected void btExport_Click(object sender, EventArgs e)
        {
            configexcel("ContractExport");
            radContractReport.MasterTableView.ExportToExcel();
        }

        protected void btViewDoc_Click(object sender, EventArgs e)
        {
            LoadC(2);
        }
    }
}