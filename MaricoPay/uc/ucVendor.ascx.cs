using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MaricoPay.DB;
using System.Data;
namespace MaricoPay.uc
{
    public partial class ucVendor : System.Web.UI.UserControl
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
        private int _width = 100;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FLoad(); 
            }
           

        }
        public class SelectedIndexChangedEventArgs : EventArgs
        {
            private ListItem m_selectedItem;

            public ListItem SelectedItem
            {
                get { return m_selectedItem; }
            }

            public SelectedIndexChangedEventArgs(ListItem selectedItem)
            {
                m_selectedItem = selectedItem;
            }
        }
        public void FLoad()
        {
            //using (var db = new DBTableDataContext())
            //{
            //   // var model = db.Charges.OrderBy(t => t.No).SingleOrDefault(t1 => t1.Active == true);
            //    var model = db.Vendors.Where(o => o.Active == true).ToList();//.Select(o => o.Active == true).ToList();
            //    radcomboCharges.DataSource = model;
            //    radcomboCharges.DataBind();
            //    //  RG.DataSource = model;
            //    //RG.DataBind();
            //}
            Cclass cls = new Cclass();
            DataTable tbl = cls.GetDataTable("sp_getvedors", "@Purorg", PurOrg);
            radcomboCharges.DataSource = tbl;
            radcomboCharges.DataBind();
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
       private string purorg="ALL";
       /// <summary>
       /// Store procedure can lay
       /// </summary>
       public string PurOrg
       {
           get
           {
               return this.purorg;
           }
           set
           {
               this.purorg = value;
           }
       }
        /// <summary>
        /// Get vendor code
        /// </summary>
       public string getVendorCode
       {
           get
           {
               Label costcenter = (Label)radcomboCharges.SelectedItem.FindControl("lbVendorCode");
               return costcenter.Text;
           }

       }
       /// <summary>
       /// Get vendor name
       /// </summary>
       public string getVendorName
       {
           get
           {
               Label lbDescription = (Label)radcomboCharges.SelectedItem.FindControl("lbVendorName");
               return lbDescription.Text;
           }

       }
         //[Category("Behavior"), DefaultValue(150), Description("Field can be edited"), NotifyParentProperty(true)]
    public int Width
       {
           get { return _width; }
           set { _width = value; }
       }

    protected void radcomboCharges_TextChanged(object sender, EventArgs e)
    {

    }
    //public delegate void MySelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e);
  // public event EventHandler SelectedIndexChanged;
   // public event MySelectedIndexChanged SelectedIndexChanged;
   
    }
}