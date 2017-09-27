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
    public partial class ucComboAdvance : System.Web.UI.UserControl
    {
        private int _width = 100;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FLoad(); 
            }
           

        }
       private void FLoad()
        {
           
            Cclass cls = new Cclass();
           DataTable tbl=  cls.GetDataTable("sp_GetAdvanceNo", new string[] { "@username" }, new object[] { Session["username"] });
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
         //[Category("Behavior"), DefaultValue(150), Description("Field can be edited"), NotifyParentProperty(true)]
    public int Width
       {
           get { return _width; }
           set { _width = value; }
       }
    }
}