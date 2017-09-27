using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
namespace MaricoPay.uc
{
    public partial class UpdateIPChonNCC : System.Web.UI.UserControl
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
        public string fGet(object IDPRDetail, object IDPR)
        {

            decimal dIDPRDetail =cls.cToDecimal(IDPRDetail);
            if (dIDPRDetail > 0)
            {
                hfIDPR.Value = cls.cToString(IDPR);
                hfIDPRDetail.Value = cls.cToString(IDPRDetail);
                DataTable tbl = cls.GetDataTable("sp_IPLoadBaoGiaCuaNCC", new string[] { "@IDPRDetail" }, new object[] { dIDPRDetail });
                ViewState["CurrentTable"] = tbl;
                RGNCC.DataSource = tbl;
                RGNCC.DataBind();
            }
            else
            {
                hfIDPR.Value = "0";
                hfIDPRDetail.Value = "0";
            }
            
            return "";
        }
        protected void RGNCC_SortCommand(object source, GridSortCommandEventArgs e)
        {
            if (ViewState["CurrentTable"] != null)
            {
                RGNCC.DataSource = (DataTable)ViewState["CurrentTable"];
                RGNCC.DataBind();
            }
            else
            {
                fGet(hfIDPRDetail.Value, hfIDPR.Value);
            }
        }
        //public struct ChonNCC
        //{
        //   public decimal IDPRDetail;
        //   public decimal IDVendor;
        //   public decimal ThanhTien;
        //}
        //public ChonNCC[] IPChonNCC
        //{
        //    get;
        //    set;
        //}
        protected void chkChon_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkChon = (CheckBox)sender;

            
            int index = (chkChon.NamingContainer as GridItem).ItemIndex;
            
            foreach (GridDataItem item in RGNCC.Items)
            {
                 CheckBox chk = item.FindControl("chkChon") as CheckBox;
                if (item.ItemIndex != index)//ko xu ly dong vua check change
                {
                   
                    chk.Checked = false;
                }
                else
                {
                    if(chk.Checked)
                    {
                        hfIDPRVendor.Value = item["IDPRVendor"].Text;

                    }
                    else
                    {
                        hfIDPRVendor.Value="0";
                    }
                }
            }

           

        }

        //protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        //{
        //    ////xoa tat ca NCC da chon truoc neu co
        //    //cls.bCapNhat(new string[]{"@IDPR","@IDPRDetail"},new object[]{hfIDPR.Value,hfIDPRDetail.Value},"sp_IPUnselectedVendor");
        //    ////tim thang NCC nao duoc chon thi update vao DB
        //    //foreach (GridDataItem item in RGNCC.Items)
        //    //{
        //    //    CheckBox chk = item.FindControl("chkChon") as CheckBox;

        //    //    if (chk.Checked)
        //    //    {
                   
        //    //       cls.bCapNhat(new string[] { "@IDPR", "@IDPRDetail", "@IDPRVendor" }, new object[] { hfIDPR.Value, hfIDPRDetail.Value, item["IDPRVendor"].Text }, "sp_IPselectedVendor");
        //    //    }
               
        //    //}
        //}
    }
}