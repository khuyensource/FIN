using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaricoPay.uc
{
    public partial class UPIPMaterials : System.Web.UI.UserControl
    {
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
        public string fGet(object ID, object Name, object Note, object SAPCode, object Active)
        {
            if (ID.ToString() != "")
            {
                rnID.Value = float.Parse(ID.ToString());
            } tbName.Text = Name.ToString();
            tbNote.Text = Note.ToString();
            tbSAPCode.Text = SAPCode.ToString();
            if (Active.ToString() != "")
            {
                cbActive.Checked = bool.Parse(Active.ToString());
            } return "";
        }
    }
}