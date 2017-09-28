using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
namespace MaricoPay.uc
{
    public partial class ISIPVendors : System.Web.UI.UserControl
    {
     //   clsObj Obj;
        clsSql Sql = new clsSql();
        protected void Page_Load(object sender, EventArgs e)
        {
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