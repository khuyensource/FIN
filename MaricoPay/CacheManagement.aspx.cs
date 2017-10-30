using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaricoPay
{
    public partial class CacheManagement : System.Web.UI.Page
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
    }
}