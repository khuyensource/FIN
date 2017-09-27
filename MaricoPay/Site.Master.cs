using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaricoPay
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["LangFrontend"] = "en";
                if (Session["username"] != null)
                {
                    divLogin.Visible = true;
                    //Session["fullname"]
                    lbuser.Text = "Welcome " + cls.cToString(Session["fullname"]);
                }
                else
                {
                    divLogin.Visible = false;
                }
            }
        }

        //protected void HeadLoginStatus_LoggingOut(object sender, LoginCancelEventArgs e)
        //{
        //    Session.Clear();
        //    Response("~/Login.aspx?type=0");
        //}
    }
}
