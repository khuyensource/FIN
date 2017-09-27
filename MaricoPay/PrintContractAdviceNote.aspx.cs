using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MaricoPay.DB;
namespace MaricoPay
{
    public partial class PrintContractAdviceNote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //gridSum.DefaultCellStyle.Font = new Font("Tahoma", 12);
                if (Session["docno"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    Cclass cls=new Cclass();
                     string docno = Session["docno"].ToString();
                  DataTable tbl=  cls.GetDataTable("sp_PrintContractAdviceNote",new string[]{"@ContractNo"},new object[]{docno});
                    if(tbl.Rows.Count>0)
                    {
                        lbContractNo.Text=tbl.Rows[0]["ContractNoLegal"].ToString();
                    lbDate.Text=string.Format("{0:dd-MMM-yyyy hh:mm}",tbl.Rows[0]["NgayIn"]);
                        lbTrinhDen.Text=tbl.Rows[0]["NguoiDuocUyQuyen"].ToString();
                        lbPrepared.Text=tbl.Rows[0]["PreparedBy"].ToString();
                        lbParties.Text=tbl.Rows[0]["Vendor"].ToString(); 
                        lbBrief.Text=tbl.Rows[0]["ContractContent"].ToString();
                        lbGiaTri.Text = cls.FormatNumber(tbl.Rows[0]["ContractValue"]) + " "+tbl.Rows[0]["UnitPrice"].ToString();
                        lbNgayBatDau.Text=string.Format("{0:dd-MMM-yyyy}",tbl.Rows[0]["ContractDate"]);
                        lbNgayketThuc.Text=string.Format("{0:dd-MMM-yyyy}",tbl.Rows[0]["ExpiryDate"]);
                        lbNameFunctionHead.Text = tbl.Rows[0]["DepartmentHead"].ToString();
                        lbFinanceHead.Text = tbl.Rows[0]["Finance"].ToString();
                        lbLegalHead.Text = tbl.Rows[0]["Legal"].ToString();
                        lbTrinhDen.Text = tbl.Rows[0]["Approveby"].ToString();
                        PrintHelper.PrintWebControl(pnlPrintClaim);
                    }
                    
                }
            }
        }
      
      
    }
}