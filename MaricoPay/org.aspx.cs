using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaricoPay
{
    public partial class org : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = !string.IsNullOrEmpty(Request.QueryString["type"]) ? Request.QueryString["type"] : "";
                txtType.Text = type;
            }
        }

        protected void btsave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Please fill in Name",uc.ucMsgBox.enmMessageType.Error);
                return;
            }

            Session["orgname"] = cls.GetString("insert_Org", new string[] { "@name", "@DOA", "@Type", "@KyHieu"}
                , new object[] {txtName.Text.Trim(),txtDOA.Text.Trim(),txtType.Text.Trim(),txtNotation.Text.Trim()});

          
            Response.Write("<script language=javascript> window.opener.__doPostBack('orgPostBack', '');window.close();</script>");

        }

        protected void btcancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>window.close();</script>");
        }
    }
}