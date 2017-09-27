using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace MaricoPay
{
    public partial class ExportASPF : System.Web.UI.Page
    {
        Cclass cls = new Cclass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["email"] != null)
            {
                DataTable tblto11111 = cls.GetDataTable("sp_Export_ASPF");
                RadGrid1.DataSource = tblto11111;
                RadGrid1.DataBind();
            //    MergeRows(RadGrid1);
            }
            else
            {

                Response.Redirect("~/Login.aspx");
            }
        }


        public static void MergeRows(RadGrid RadGrid1)
        {
            for (int i = RadGrid1.Items.Count - 1; i > 0; i--)
            {
                if (RadGrid1.Items[i][RadGrid1.Columns[0]].Text == RadGrid1.Items[i - 1][RadGrid1.Columns[0]].Text)
                {
                    RadGrid1.Items[i - 1][RadGrid1.Columns[0]].RowSpan = RadGrid1.Items[i][RadGrid1.Columns[0]].RowSpan < 2 ? 2 : RadGrid1.Items[i][RadGrid1.Columns[0]].RowSpan + 1;
                    RadGrid1.Items[i][RadGrid1.Columns[0]].Visible = false;
                    // RadGrid1.Items[i][RadGrid1.Columns[2]].Text = "&nbsp;";  
                }

                if (RadGrid1.Items[i][RadGrid1.Columns[1]].Text == RadGrid1.Items[i - 1][RadGrid1.Columns[1]].Text)
                {
                    RadGrid1.Items[i - 1][RadGrid1.Columns[1]].RowSpan = RadGrid1.Items[i][RadGrid1.Columns[1]].RowSpan < 2 ? 2 : RadGrid1.Items[i][RadGrid1.Columns[1]].RowSpan + 1;
                    RadGrid1.Items[i][RadGrid1.Columns[1]].Visible = false;
                    // RadGrid1.Items[i][RadGrid1.Columns[2]].Text = "&nbsp;";  
                }
              

                if (RadGrid1.Items[i][RadGrid1.Columns[4]].Text == RadGrid1.Items[i - 1][RadGrid1.Columns[4]].Text)
                {
                    RadGrid1.Items[i - 1][RadGrid1.Columns[4]].RowSpan = RadGrid1.Items[i][RadGrid1.Columns[4]].RowSpan < 2 ? 2 : RadGrid1.Items[i][RadGrid1.Columns[4]].RowSpan + 1;
                    RadGrid1.Items[i][RadGrid1.Columns[4]].Visible = false;
                    // RadGrid1.Items[i][RadGrid1.Columns[2]].Text = "&nbsp;";  
                }
                if (RadGrid1.Items[i][RadGrid1.Columns[5]].Text == RadGrid1.Items[i - 1][RadGrid1.Columns[5]].Text)
                {
                    RadGrid1.Items[i - 1][RadGrid1.Columns[5]].RowSpan = RadGrid1.Items[i][RadGrid1.Columns[5]].RowSpan < 2 ? 2 : RadGrid1.Items[i][RadGrid1.Columns[5]].RowSpan + 1;
                    RadGrid1.Items[i][RadGrid1.Columns[5]].Visible = false;
                    // RadGrid1.Items[i][RadGrid1.Columns[2]].Text = "&nbsp;";  
                }

                if (RadGrid1.Items[i][RadGrid1.Columns[6]].Text == RadGrid1.Items[i - 1][RadGrid1.Columns[6]].Text)
                {
                    RadGrid1.Items[i - 1][RadGrid1.Columns[6]].RowSpan = RadGrid1.Items[i][RadGrid1.Columns[6]].RowSpan < 2 ? 2 : RadGrid1.Items[i][RadGrid1.Columns[6]].RowSpan + 1;
                    RadGrid1.Items[i][RadGrid1.Columns[6]].Visible = false;
                    // RadGrid1.Items[i][RadGrid1.Columns[2]].Text = "&nbsp;";  
                }

                if (RadGrid1.Items[i][RadGrid1.Columns[7]].Text == RadGrid1.Items[i - 1][RadGrid1.Columns[7]].Text)
                {
                    RadGrid1.Items[i - 1][RadGrid1.Columns[7]].RowSpan = RadGrid1.Items[i][RadGrid1.Columns[7]].RowSpan < 2 ? 2 : RadGrid1.Items[i][RadGrid1.Columns[7]].RowSpan + 1;
                    RadGrid1.Items[i][RadGrid1.Columns[7]].Visible = false;
                    // RadGrid1.Items[i][RadGrid1.Columns[2]].Text = "&nbsp;";  
                }

                if (RadGrid1.Items[i][RadGrid1.Columns[8]].Text == RadGrid1.Items[i - 1][RadGrid1.Columns[8]].Text)
                {
                    RadGrid1.Items[i - 1][RadGrid1.Columns[8]].RowSpan = RadGrid1.Items[i][RadGrid1.Columns[8]].RowSpan < 2 ? 2 : RadGrid1.Items[i][RadGrid1.Columns[8]].RowSpan + 1;
                    RadGrid1.Items[i][RadGrid1.Columns[8]].Visible = false;
                    // RadGrid1.Items[i][RadGrid1.Columns[2]].Text = "&nbsp;";  
                }

               


            }
            //To mau lai cho Radgird 
            foreach (GridDataItem dataItem in RadGrid1.Items)
            {
                foreach (GridColumn col in RadGrid1.MasterTableView.RenderColumns)
                {
                    //if (dataItem[col.UniqueName].Text == string.Empty) 
                   // dataItem[col.UniqueName].Style.Add("border-top", "solid 3px #red");
                  //  dataItem[col.UniqueName].Style.Add("border-bottom", "solid 3px #red");
                }
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
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.FileName = filename;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.GridLines = GridLines.Both;
        }

        protected void Excel_Click(object sender, EventArgs e)
        {
            configexcel("ASP_report");
            RadGrid1.MasterTableView.ExportToExcel();
        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
          //  MergeRows(RadGrid1);
        }

    }
}