using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MaricoPay.DB;

namespace MaricoPay.uc
{
    public partial class ucComboCharges : System.Web.UI.UserControl
    {
        public event SelectedIndexChangedEventHandler SelectedIndexChanged;
        public delegate void SelectedIndexChangedEventHandler(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs args);
        protected void radcomboCharges_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (this.SelectedIndexChanged != null)
            {
                this.SelectedIndexChanged(this, e);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FLoad(); 
            }
           

        }
       private void FLoad()
        {
            using (var db = new DBTableDataContext())
            {
               // var model = db.Charges.OrderBy(t => t.No).SingleOrDefault(t1 => t1.Active == true);
                var model = db.Charges.ToList().OrderBy(o=>o.No);
                radcomboCharges.DataSource = model;
                radcomboCharges.DataBind();
                //  RG.DataSource = model;
                //RG.DataBind();
            }
        }
       public string Text
       {
           
           get
           {
               return radcomboCharges.SelectedItem.Text;
           }
           set
           {
               radcomboCharges.SelectedItem.Text = value;
           }
       }
       public string Values
       {
           get
           {
               return radcomboCharges.SelectedValue;
           }
           set
           {
               radcomboCharges.SelectedValue = value;
           }
       }

      
    }
}