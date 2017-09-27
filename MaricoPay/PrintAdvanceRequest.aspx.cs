using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MaricoPay.DB;
using System.Data;
using Telerik.Web.UI;
namespace MaricoPay
{
    public partial class PrintAdvanceRequest : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
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
                    clsSys sys = new clsSys();
                    string docno = Session["docno"].ToString();
                    // string us = Session["username"].ToString();
                    DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
                    var kq = dbs.sp_GetTravelRequest_Print(docno).ToList();
                    if (kq.Count > 0)
                    {
                        lbType.Text ="PHIẾU YÊU CẦU TẠM ỨNG CÔNG TÁC<br/>ADVANCE BUSINESS TRAVELING REQUISITION";// kq[0].TypeText;

                        lbDate.Text = kq[0].DateRec.Value.ToString("dd-MMM-yy");
                        lbdocno.Text = docno;
                       
                        lbName.Text = kq[0].Fullname;
                      //  lbPosition.Text = kq[0].Position;
                        lbContentPurpose.Text = kq[0].Purpose;
                        lbDPNo.Text=kq[0].DPNo;
                       
                        lbTamUng.Text =cls.FormatNumber(kq[0].Advance);
                      
                        lbAppFuction.Text = kq[0].Approver;
                        lbRequest.Text = kq[0].Fullname;
                        ltSign.Text = "<a href='#'><img id='imgApproved' src='/ImagesSignature/" + kq[0].Signture + "' alt='APPROVED' style='width:80px;height:60px;'/></a>";

                        DataTable tbl= cls.GetDataTable("sp_getAdvanceDetail", new string[] { "@code" }, new object[] { docno });
                    //    RadGrid1.DataSource = tbl;
                        //RadGrid1.DataBind();
                        GridView1.DataSource = tbl;
                        GridView1.DataBind();
                        // List<sp_getClaimDetailResult> kq1 = dbs.sp_getClaimDetail(docno,true).OrderBy(o => o.ID).ToList();//sp_getClaimDetail LA STORE
                       // List<sp_getClaimDetailResult> kq1 = dbs.sp_getClaimDetail(docno, true).ToList();//sp_getClaimDetail LA STORE
                      //  Session["ClaimDetailPrint"] = kq1;
                   
                       // LoadSum(docno);
                        dbs.Dispose();
                        // Session["NewDocNo"] = code;
                        // pnlPrintClaim
                        PrintHelper.PrintWebControl(pnlPrintClaim);
                    }
                    else
                    {
                        //chua duoc approved
                    }
                }
            }
        }
       
    }
}