using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaricoPay
{
    public partial class NewPosition : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               try
                {
                                  
                    LoadCompany();
                   dropCompany.SelectedValue=  cls.cToString(Session["company"]);
                    LoadDepartment();
                   dropDepartment.SelectedValue=  cls.cToString(Session["department"]);
                   
                 //   // txtCompany.Text =Request.QueryString["cp"];
                 //   //txtProvince.Text =  Request.QueryString["pv"];
                 //   //    //  txtVATCode.Text =Request.QueryString["vatcode"];
                 //   //      dropTaxCode.SelectedValue = Request.QueryString["vatcode"];
                 //   //      txtTaxNumber.Text = Request.QueryString["taxnumber"];
                 //   //      radnumVATAmount.Value = sys.cToDuoble(Request.QueryString["vatamount"]);
                 //  txtCompany.Text= Request.QueryString["vatcode"];
                 //  txtCostcenter.Text = Request.QueryString["vatcode"];
                 ////  txtPosition
                        
                    //txtCompany.Text = sys.cToString(Session["Company"]);
                    //txtProvince.Text = sys.cToString(Session["Province"]);
                    //txtVATCode.Text = sys.cToString(Session["VAT"]);
                    //radnumVATAmount.Value = sys.cToDuoble(Session["VATAmount"]);
                }
                catch { }
            }
        }
        private void LoadCompany()
        {
            System.Data.DataTable tbl = cls.GetDataTable("sp_loadCompany");
            dropCompany.DataSource = tbl;
            dropCompany.DataBind();
        }
        private void LoadDepartment()
        {
           System.Data.DataTable tbl = cls.GetDataTable("sp_LoadDepartMentMPay", "@company_FK", dropCompany.SelectedValue);
            dropDepartment.DataSource = tbl;
            dropDepartment.DataBind();
        }
        protected void dropCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDepartment();
        }
        protected void btSave_Click(object sender, EventArgs e)
        {
            if (txtPosition.Text.Trim() != "")
            {
            
                cls.bThem(new string[] { "@description", "@Costcenter" }, new object[] {txtPosition.Text.Trim(),dropDepartment.SelectedValue }, "sp_insertPosition");
                Session["positionnew"] = txtPosition.Text;
                Response.Write("<script language=javascript> window.opener.__doPostBack('ChildWindowPostBackPosition', '');window.close();</script>");
            }
           
        }
    }
}