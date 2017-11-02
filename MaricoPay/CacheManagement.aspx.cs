using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaricoPay
{
    public partial class CacheManagement :  clsPhanQuyenCaoCap
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btDepartment_Click(object sender, EventArgs e)
        {
            CacheHelper.Remove("cdepart");
        }

        protected void btTinhThanh_Click(object sender, EventArgs e)
        {
            CacheHelper.Remove("cTinhThanh");
        }

        protected void btQuanHuyen_Click(object sender, EventArgs e)
        {
            DataTable tbltinh;
            if (CacheHelper.Get("cTinhThanh") != null)
            {
                tbltinh = (DataTable)CacheHelper.Get("cTinhThanh");
            }
            else
            {
                tbltinh = cls.GetDataTable("sp_getTinh");

            }
            
            for(int i=0;i<tbltinh.Rows.Count;i++)
            {
                CacheHelper.Remove("cHuyen_"+ cls.cToString(tbltinh.Rows[i]["MaTP"]));
            }
           
        }

        protected void btCharge_Click(object sender, EventArgs e)
        {
            CacheHelper.Remove("ccharge");
        }

        protected void btCategory_Click(object sender, EventArgs e)
        {
            CacheHelper.RemoveLikeKey("ccategory_");
        }

        protected void btIOcontract_Click(object sender, EventArgs e)
        {
            CacheHelper.Remove("cIOContract");
        }

        protected void btEmailLG_FC_Click(object sender, EventArgs e)
        {
            CacheHelper.Remove("cEmailLG");
            CacheHelper.Remove("cEmailFC");
           
            
        }

        protected void btOrgType_Click(object sender, EventArgs e)
        {
            CacheHelper.Remove("cOrgContract");
            CacheHelper.Remove("cTypeContract");
        }

        protected void btsendivpcoo_Click(object sender, EventArgs e)
        {
            CacheHelper.Remove("cSenDirVPCOO");
        }
    }
}