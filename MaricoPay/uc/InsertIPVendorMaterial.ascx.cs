using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaricoPay.uc
{
    public partial class InsertIPVendorMaterial : System.Web.UI.UserControl
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                cls.LoadRadCbo(RadComboMaterial, "spLoad_IPMaterial", "Name", "", "ID", "Name");
                cls.LoadRadCbo(RadComboVendor, "spLoad_IP_Vendors", "TenNCC", "", "ID", "TenNCC");
           // }
        }
        public int fInt(object value)
        {
            if (value.ToString() == "")
            {
                return 0;
            }
            return int.Parse(value.ToString());
        }
        public bool fBool(object value)
        {
            if (value.ToString() == "")
            {
                return false;
            }
            return bool.Parse(value.ToString());
        }
    }
}