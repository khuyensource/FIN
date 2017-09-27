using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace MaricoPay.uc
{
    public partial class InsertIPRequest : System.Web.UI.UserControl
    {

        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LoadCostcenter();
                try
                {
                    radcomboCostcenter.SelectedValue = cls.cToString(Session["costcenter"]);
                }
                catch {
                    radcomboCostcenter.SelectedIndex = -1;
                }
                LoadMaterial();
                LoadASPNo();
                LoadGL();
                LoadMatrialGroup();
                LoadProfitcenter();
                LoadCountry();
                LoadDivision();
                LoadSalesGroup();
            
            }
        }
        public int fInt(object value)
        {
            if (value.ToString() == "")
            {
                return 0;
            }
            return int.Parse(value.ToString());
        }
        private void LoadASPNo()
        {
            DataTable tbl = cls.GetDataTable("sp_GetASPNo");
            RadComboIO.DataSource = tbl;
            RadComboIO.DataBind();
        }
        private void LoadMaterial()
        {
            DataTable tbl = cls.GetDataTable("spLoad_IPMaterial");
          RadComboMaterial.DataSource = tbl;
         RadComboMaterial.DataBind();
        }
       
        public bool fBool(object value)
        {
            if (value.ToString() == "")
            {
                return false;
            }
            return bool.Parse(value.ToString());
        }
        
        private void LoadCostcenter()
        {
            DataTable tbl = cls.GetDataTable("sp_GetCostCenterALL");
            radcomboCostcenter.DataSource = tbl;
            radcomboCostcenter.DataBind();
        }
        private void LoadGL()
        {
            DataTable tbl = cls.GetDataTable("sp_IPGetGL");
            radcomboGL.DataSource = tbl;
            radcomboGL.DataBind();
        }
        private void LoadMatrialGroup()
        {
            DataTable tbl = cls.GetDataTable("sp_IPGetMatrialGroup");
            radcomboMatrialGroup.DataSource = tbl;
            radcomboMatrialGroup.DataBind();
        }
        private void LoadProfitcenter()
        {
            DataTable tbl = cls.GetDataTable("sp_IPGetProfitcenter");
            radcomboProfitcenter.DataSource = tbl;
            radcomboProfitcenter.DataBind();
        }
        private void LoadCountry()
        {
            DataTable tbl = cls.GetDataTable("sp_IPGetCountry");
            radcomboCountry.DataSource = tbl;
            radcomboCountry.DataBind();
        }
        private void LoadDivision()
        {
            DataTable tbl = cls.GetDataTable("sp_IPGetDivision");
            radcomboDivision.DataSource = tbl;
            radcomboDivision.DataBind();
        }
        private void LoadSalesGroup()
        {
            DataTable tbl = cls.GetDataTable("sp_IPGetSalesGroup");
            radcomboSalesGroup.DataSource = tbl;
            radcomboSalesGroup.DataBind();
        }

    }
}