using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace MaricoPay.uc
{
    public partial class ucViewStatus : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Store!="")
            {
                Cclass cls = new Cclass();
                DataTable tbl = cls.GetDataTable(Store, "@username", Session["username"]);
                RadGrid1.DataSource = tbl;
                RadGrid1.DataBind();
            }
        }
        private string store;
        /// <summary>
        /// Store procedure can lay
        /// </summary>
        public string Store
        {
            get
            {
                return this.store;
            }
            set
            {
                this.store = value;
            }
        }
    }
}