using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaricoPay.uc
{
    public partial class UpdateIPVendorMaterial : System.Web.UI.UserControl
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    cls.LoadRadCbo(RadComboMaterial, "spLoad_IPMaterial", "Name", "", "Name", "ID");
            //    cls.LoadRadCbo(RadComboVendor, "spLoad_IP_Vendors", "TenNCC", "", "TenNCC", "ID");
            //}
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
        public string fGet(object ID, object Material, object Vendor, object Note, object Active)
        {
            if (ID.ToString() != "")
            {
                rnID.Value = float.Parse(ID.ToString());
                cls.LoadRadCbo(RadComboMaterial, "spLoad_IPMaterial", "Name", "", "ID", "Name");
                cls.LoadRadCbo(RadComboVendor, "spLoad_IP_Vendors", "TenNCC", "", "ID", "TenNCC");
            } if (Material.ToString() != "")
            {
                RadComboMaterial.SelectedValue = Material.ToString();
               
            } if (Vendor.ToString() != "")
            {
                RadComboVendor.SelectedValue = Vendor.ToString();
            } tbNote.Text = Note.ToString();
            if (Active.ToString() != "")
            {
                cbActive.Checked = bool.Parse(Active.ToString());
            } return "";
        }
    }
}