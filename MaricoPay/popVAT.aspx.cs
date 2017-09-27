using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaricoPay
{
    public partial class popVAT : System.Web.UI.Page
    {
        clsSys sys = new clsSys();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               try
                {
                     txtCompany.Text =Request.QueryString["cp"];
                    txtProvince.Text =  Request.QueryString["pv"];
                        //  txtVATCode.Text =Request.QueryString["vatcode"];
                          dropTaxCode.SelectedValue = Request.QueryString["vatcode"];
                          txtTaxNumber.Text = Request.QueryString["taxnumber"];
                          radnumVATAmount.Value = sys.cToDuoble(Request.QueryString["vatamount"]);

                        
               
                
                    //txtCompany.Text = sys.cToString(Session["Company"]);
                    //txtProvince.Text = sys.cToString(Session["Province"]);
                    //txtVATCode.Text = sys.cToString(Session["VAT"]);
                    //radnumVATAmount.Value = sys.cToDuoble(Session["VATAmount"]);
                }
                catch { }
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            Session["Company"] = txtCompany.Text.Trim();
            Session["Province"]= txtProvince.Text;
            Session["TaxCode"] = dropTaxCode.SelectedValue;
                Session["TaxNumber"] =txtTaxNumber.Text.Trim();
                Session["VATAmount"] = radnumVATAmount.Value;
                ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }
    }
}