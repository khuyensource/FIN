using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace MaricoPay.uc
{
    public partial class UpdateIPRequest : System.Web.UI.UserControl
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
           
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
        public string fGet(object IDPRDetail, object IDPR, object IO, object IDMaterial, object TenHang, object KichThuoc, object ChatLieu, object DoDay, object CoDen, object HinhMau, object SoLuong, object NgayGiao, object NoiNhan, object ChiTietKhac, object Costcenter, object GL, object MaterialGroup, object Profitcenter, object Country, object Division, object SalesGroup)
        {
            if (IDPRDetail.ToString() != "")
            {
                rnID.Value = float.Parse(IDPRDetail.ToString());
                LoadCostcenter();
                LoadGL();
                LoadMatrialGroup();
                LoadProfitcenter();
                LoadCountry();
                LoadDivision();
                LoadSalesGroup();
                cls.LoadRadCbo(RadComboMaterial, "spLoad_IPMaterial", "Name", "", "ID", "Name");
                cls.LoadRadCbo(RadComboIO, "sp_GetASPNo", "Objective", "", "ASPNo", "Objective");

            }
            if (IO.ToString() != "")
            {
                try
                {
                    int idx = RadComboIO.FindItemIndexByValue(IO.ToString());
                    if (idx < 0)
                    {
                        RadComboIO.Text = IO.ToString();
                    }
                    else
                    {
                        RadComboIO.SelectedIndex = idx;
                        //  RadComboIO.SelectedValue = IO.ToString();
                        // sIO = cboIO.SelectedValue;
                    }
                }
                catch
                {
                    RadComboIO.Text = IO.ToString();

                }

            }
            if (IDMaterial.ToString() != "")
            {
                RadComboMaterial.SelectedValue = IDMaterial.ToString();

            }
            hfIDPR.Value = cls.cToString0(IDPR);
            txtKichThuoc.Text = cls.cToString(KichThuoc);
            txtChatLieu.Text = cls.cToString(ChatLieu);
            txtDoDay.Text = cls.cToString(DoDay);
            chkCoDen.Checked = cls.cToBool(CoDen);
            radnumSoLuong.Value = cls.cToDouble(SoLuong);
            //hinh mau
            radDateNgayGiao.SelectedDate = cls.cToDateTime(NgayGiao);
            // txtNoiNhan.Text = cls.cToString(NoiNhan);
            dropNoiNhan.Text = cls.cToString(NoiNhan);
            tbNote.Text = cls.cToString(ChiTietKhac);

            // txtCostCenter.Text = cls.cToString(Costcenter);
            // hfcostcenter.Value = cls.cToString(Costcenter);
            // comboCostcenter.Values = hfcostcenter.Value;
            try
            {
                radcomboCostcenter.SelectedValue = cls.cToString(Costcenter);
            }
            catch
            {
                radcomboCostcenter.SelectedIndex = -1;
            }
            try
            {
                radcomboGL.SelectedValue = cls.cToString(GL);
            }
            catch
            {
                radcomboGL.SelectedIndex = 0;
            }
            try
            {
                radcomboMatrialGroup.SelectedValue = cls.cToString(MaterialGroup);
            }
            catch
            {
                radcomboMatrialGroup.SelectedIndex = 0;
            }

            try
            {
                radcomboProfitcenter.SelectedValue = cls.cToString(Profitcenter);
            }
            catch
            {
                radcomboProfitcenter.SelectedIndex = 0;
            }

            try
            {
                radcomboCountry.SelectedValue = cls.cToString(Country);
            }
            catch
            {
                radcomboCountry.SelectedIndex = 0;
            }

            try
            {
                radcomboDivision.SelectedValue = cls.cToString(Division);
            }
            catch
            {
                radcomboDivision.SelectedIndex = 0;
            }

            try
            {
                radcomboSalesGroup.SelectedValue = cls.cToString(SalesGroup);
            }
            catch
            {
                radcomboSalesGroup.SelectedIndex = 0;
            }


            return "";
        }

        protected void btnCopy_Click(object sender, ImageClickEventArgs e)
        {
            string IDPR = hfIDPR.Value;
          //  lbLoi.Text = "";

            //UserControl userControl = (UserControl)e.Item.OwnerTableView.FindControl(GridEditFormItem.EditFormUserControlID);
          //  RadComboBox cboMaterial = userControl.FindControl("RadComboMaterial") as RadComboBox;
           // RadComboMaterial.SelectedIndex = RadComboMaterial.FindItemByText(RadComboMaterial.Text).Index;
        //    //  float Material = float.Parse((userControl.FindControl("RadComboMaterial") as RadComboBox).SelectedValue);
            string sIO = "";
       // //    RadComboBox cboIO = userControl.FindControl("RadComboIO") as RadComboBox;
            try
            {
                int idx1 = RadComboIO.FindItemByText(RadComboIO.Text).Index;
                if (idx1 < 0)
                {
                    sIO = RadComboIO.Text;
                }
                else
                {
                    RadComboIO.SelectedIndex = idx1;
                    sIO = RadComboIO.SelectedValue;
                }
            }
            catch
            {
                sIO = RadComboIO.Text;
            }

            string kichthuoc = txtKichThuoc.Text.Trim();
            string chatlieu = txtChatLieu.Text.Trim();
            string doday = txtDoDay.Text.Trim();
          //  CheckBox coden = userControl.FindControl("chkCoDen") as CheckBox;
            double soluong = cls.cToDouble(radnumSoLuong.Value);
            //   FileUpload fileupload=userControl.FindControl("FileUpload1") as FileUpload;
         //   RadAsyncUpload radfileupload = userControl.FindControl("RadAsyncUpload1") as RadAsyncUpload;
            DateTime ngaygiao = radDateNgayGiao.SelectedDate.Value;
            // string noinhan = (userControl.FindControl("txtNoiNhan") as TextBox).Text.Trim();
            string noinhan = dropNoiNhan.Text.Trim();

            string Note = tbNote.Text.Trim();

           // string Costcenter = txtCostCenter.Text.Trim();
            string Costcenter = radcomboCostcenter.SelectedValue;
            string GL = radcomboGL.SelectedValue;
            string MaterialGroup = radcomboMatrialGroup.SelectedValue;
            string Profitcenter = radcomboProfitcenter.SelectedValue;
            string Country = radcomboCountry.SelectedValue;
            string Division = radcomboDivision.SelectedValue;
            string SalesGroup = radcomboSalesGroup.SelectedValue;

            #region Insert
            //if(Material<=0

            string[] bien = new string[] { "@IDPRDetail", "@IDPR", "@IO", "@IDMaterial", "@TenHang", "@KichThuoc", "@ChatLieu", "@DoDay", "@CoDen", "@SoLuong", "@NgayGiao", "@NoiNhan", "@ChiTietKhac", "@Costcenter", "@GL", "@MaterialGroup", "@Profitcenter", "@Country", "@Division", "@SalesGroup" };
            object[] giatri = new object[] { 0, IDPR, sIO, RadComboMaterial.SelectedValue, RadComboMaterial.SelectedItem.Text, kichthuoc, chatlieu, doday, chkCoDen.Checked, soluong, ngaygiao, noinhan, Note, Costcenter, GL, MaterialGroup, Profitcenter, Country, Division, SalesGroup };
            // Obj.SpName = "IPPRDetail_InsertUpdate";
            // Sql.fNonGetData(Obj);
            decimal IDPRDetail = cls.cToDecimal(cls.GetString0("IPPRDetail_InsertUpdate", bien, giatri));
            if (IDPRDetail > 0)
            {
              
                if (RadAsyncUpload1.UploadedFiles.Count > 0)
                {

                    string filename = cls.radupload(RadAsyncUpload1, IDPRDetail);
                    cls.bCapNhat(new string[] { "@IDPRDetail", "@filename" }, new object[] { IDPRDetail, filename }, "sp_IPPRDetailupdateImage");
                }

              //  lbLoi.Text = "<font color='blue'>Thêm thành công.</font>";
            }
            #endregion
          //  fLoadPRDetail(radcomboPR.SelectedValue);
        }
     
    }
}