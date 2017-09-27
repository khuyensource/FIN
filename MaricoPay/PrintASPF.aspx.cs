using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaricoPay
{
    public partial class PrintASPF : System.Web.UI.Page
    {
        Cclass cls = new Cclass();

        clsSys sys = new clsSys();
        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!IsPostBack)
            {

                //gridSum.DefaultCellStyle.Font = new Font("Tahoma", 12);
                if (1>2)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    LoadData();
                    PrintHelper.PrintWebControl(pnlPrintASPF);
                }
            }
        }
        private void LoadASPF_Detail(string aspf_fk)
        {

            DataTable tbl = cls.GetDataTable("[sp_Load_ASPF_Detail_RP]", new string[] { "@aspf_fk" }, new object[] { aspf_fk });
               GridView1.DataSource = tbl;
                GridView1.DataBind();
        }
        private void LoadData()
        {
           // fullname,Country,BudgetOwner,BU,Brand,Category,Channel
            DataTable tbl = cls.GetDataTable("sp_Load_ASPF_Report", new string[] { "@ID" }, new object[] { Request.QueryString["ID"].ToString() });
            if (tbl.Rows.Count > 0)
            {
                lbtaspso0.Text = "";
                ltNgaytao0.Text = DateTime.Parse(tbl.Rows[0]["DateCreation"].ToString()).ToString("dd/MM/yyyy"); // DateTime.(tbl.Rows[0]["DateCreation"].ToString(), "dd/MM/yyyy").ToString();
                ltCountry1.Text = tbl.Rows[0]["Country"].ToString();
                ltbudgetowner100000.Text = tbl.Rows[0]["ngansachcua"].ToString();
                ltBU1.Text = tbl.Rows[0]["BU"].ToString();
              //  ltBrand1.Text = tbl.Rows[0]["Brand"].ToString();
              //  ltCat1.Text = tbl.Rows[0]["Category"].ToString();
               // ltbudgetowner1.Text = tbl.Rows[0]["Code"].ToString();
                //ltCode.Text = tbl.Rows[0]["GLCode"].ToString();
                ltTo.Text = DateTime.Parse(tbl.Rows[0]["To"].ToString()).ToString("dd/MM/yyyy");
                ltfrom.Text = DateTime.Parse(tbl.Rows[0]["From"].ToString()).ToString("dd/MM/yyyy") ;//(tbl.Rows[0]["From"].ToString());
                ltChannel.Text = tbl.Rows[0]["Channel"].ToString();
                ltMuctieu.Text = tbl.Rows[0]["Objective"].ToString();
              //  ltBudget.Text = string.Format("{0:0,0}", tbl.Rows[0]["FYbudgetcategory"]);
             //   ltAvailable.Text = string.Format("{0:0,0}", tbl.Rows[0]["Availablebudget"]);
                ltaspfvalue.Text = string.Format("{0:0,0}", tbl.Rows[0]["ASPFValue"]);
             //   ltbudgetbalance.Text = string.Format("{0:0,0}", tbl.Rows[0]["BudgetBalance"]);
                ltNamecreation.Text=   tbl.Rows[0]["fullname"].ToString(); 
                ltChannel.Text = tbl.Rows[0]["Channel"].ToString();    //Channel
                    ltChannel.Text = tbl.Rows[0]["Channel"].ToString();    //Channel
                    //ltmachiphi.Text = tbl.Rows[0]["machiphi"].ToString();   
                //ltcreatedateKy.Text =DateTime.Parse(tbl.Rows[0]["DateCreation"].ToString()).ToString("dd/MM/yyyy");    //Channel
                //			 
            //    ltFYbugetController.Text = string.Format("{0:0,0}", tbl.Rows[0]["BudgetDepartment"]);
           //     ltSpent.Text = string.Format("{0:0,0}", tbl.Rows[0]["Spent"]);
            //    ltASPFvalueController.Text = string.Format("{0:0,0}", tbl.Rows[0]["ASPFValue_Controller"]);
            //    ltbudgetblanceControlller.Text = string.Format("{0:0,0}", tbl.Rows[0]["BudgetBalance_Controller"]);
               // fileUpload = tbl.Rows[0]["AttachFile"].ToString();
                LoadASPF_Detail(Request.QueryString["ID"].ToString());
               // loadHistory();

            }
            DataTable tbl11 = cls.GetDataTable("sp_Load_chuky", new string[] { "@ID" }, new object[] { Request.QueryString["ID"].ToString() });
            if (tbl11.Rows.Count > 0)
            {
                for (int i = 0; i < tbl11.Rows.Count; i++)
                {
                    
                   
                     if (tbl11.Rows[i]["LevelApp"].ToString() == "1")
                    {
                        ltNamecreation0.Text = tbl11.Rows[i]["DateSubmit"].ToString();
                    }

                     if (tbl11.Rows[i]["LevelApp"].ToString() == "3" && tbl11.Rows[i]["Status"].ToString() == "1")
                    {
                        lbtBudget.Text = tbl11.Rows[i]["fullname"].ToString();
                        lbtBudget0.Text = tbl11.Rows[i]["DateApp"].ToString();
                        ltReview.Text = "<a href='#'><img id='imgApproved' src='/ImagesSignature/approved.png' alt='APPROVED' style='width:80px;height:60px;'/></a>";
                    }
                     if (tbl11.Rows[i]["LevelApp"].ToString() == "2" && tbl11.Rows[i]["Status"].ToString() == "1" )
                    {
                        lbtApp1.Text = tbl11.Rows[i]["fullname"].ToString();
                        lbtApp8.Text = tbl11.Rows[i]["DateApp"].ToString();
                        ltBudgetcontrol.Text = "<a href='#'><img id='imgApproved' src='/ImagesSignature/approved.png' alt='APPROVED' style='width:80px;height:60px;'/></a>";
                    }
                     if (tbl11.Rows[i]["LevelApp"].ToString() == "4" && tbl11.Rows[i]["Status"].ToString() == "1" )
                    {
                        lbtapp2.Text = tbl11.Rows[i]["fullname"].ToString();
                        lbtapp9.Text = tbl11.Rows[i]["DateApp"].ToString();
                        ltBudgetcontrol4.Text = "<a href='#'><img id='imgApproved' src='/ImagesSignature/approved.png' alt='APPROVED' style='width:80px;height:60px;'/></a>";
                    }
                     if (tbl11.Rows[i]["LevelApp"].ToString() == "5" && tbl11.Rows[i]["Status"].ToString() == "1" )
                    {
                        lbtapp3.Text = tbl11.Rows[i]["fullname"].ToString();
                        lbtapp10.Text = tbl11.Rows[i]["DateApp"].ToString();
                        ltBudgetcontrol5.Text = "<a href='#'><img id='imgApproved' src='/ImagesSignature/approved.png' alt='APPROVED' style='width:80px;height:60px;'/></a>";
                    }
                     if (tbl11.Rows[i]["LevelApp"].ToString() == "6" && tbl11.Rows[i]["Status"].ToString() == "1" )
                    {
                        lbtapp4.Text = tbl11.Rows[i]["fullname"].ToString();
                        lbtapp11.Text = tbl11.Rows[i]["DateApp"].ToString();
                        ltBudgetcontrol6.Text = "<a href='#'><img id='imgApproved' src='/ImagesSignature/approved.png' alt='APPROVED' style='width:80px;height:60px;'/></a>";
                    }
                     if (tbl11.Rows[i]["LevelApp"].ToString() == "7" && tbl11.Rows[i]["Status"].ToString() == "1" )
                    {
                        lbtapp5.Text = tbl11.Rows[i]["fullname"].ToString();
                        lbtapp12.Text = tbl11.Rows[i]["DateApp"].ToString();
                        ltBudgetcontrol7.Text = "<a href='#'><img id='imgApproved' src='/ImagesSignature/approved.png' alt='APPROVED' style='width:80px;height:60px;'/></a>";

                    }
                     if (tbl11.Rows[i]["LevelApp"].ToString() == "8" && tbl11.Rows[i]["Status"].ToString() == "1")
                    {
                        lbtapp6.Text = tbl11.Rows[i]["fullname"].ToString();
                        lbtapp13.Text = tbl11.Rows[i]["DateApp"].ToString();
                        ltBudgetcontrol8.Text = "<a href='#'><img id='imgApproved' src='/ImagesSignature/approved.png' alt='APPROVED' style='width:80px;height:60px;'/></a>";
                    }
                     if (tbl11.Rows[i]["LevelApp"].ToString() == "9" && tbl11.Rows[i]["Status"].ToString() == "1" )
                    {
                        lbtapp7.Text = tbl11.Rows[i]["fullname"].ToString();
                        lbtapp14.Text = tbl11.Rows[i]["DateApp"].ToString();
                        ltBudgetcontrol9.Text = "<a href='#'><img id='imgApproved' src='/ImagesSignature/approved.png' alt='APPROVED' style='width:80px;height:60px;'/></a>";
                    }

                }
            }

        }

    }
}