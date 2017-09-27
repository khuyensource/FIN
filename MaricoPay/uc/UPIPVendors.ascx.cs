using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
namespace MaricoPay.uc
{
    public partial class UPIPVendors : System.Web.UI.UserControl
    {
        
        //clsObj Obj;
        //clsSql Sql = new clsSql();
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
        public string fGet(object ID, object TenNCC, object NguoiLienHe, object DienThoaiDD, object DienThoaiBan, object Fax, object Email, object DiaChi, object GhiChu, object VendorCodeSAP, object HieuLuc)
        {
            if (ID.ToString() != "")
            {
                rnID.Value = float.Parse(ID.ToString());
            } tbTenNCC.Text = TenNCC.ToString();
            tbNguoiLienHe.Text = NguoiLienHe.ToString();
            tbDienThoaiDD.Text = DienThoaiDD.ToString();
            tbDienThoaiBan.Text = DienThoaiBan.ToString();
            tbFax.Text = Fax.ToString();
            tbEmail.Text = Email.ToString();
            tbDiaChi.Text = DiaChi.ToString();
            tbGhiChu.Text = GhiChu.ToString();
            tbVendorCodeSAP.Text = VendorCodeSAP.ToString();
            if (HieuLuc.ToString() != "")
            {

                  cbHieuLuc.Checked = bool.Parse(HieuLuc.ToString());
            } return "";
        }
    }
}