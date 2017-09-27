using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
using Telerik.Web.UI;

namespace MaricoPay
{
    public partial class AutotranferList : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
        clsObj Obj = new clsObj();
        clsSql Sql = new clsSql();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["email"] != null)
            {

                if (!IsPostBack)
                {
                    LoadGird();

                }
                if (Session["TransferBudget"] != null)
                {
                    LoadGird();
                }
             //   LoadGird();
            }
            else
            {

                Response.Redirect("~/Login.aspx");
            }
            Session["TransferBudget"] = null;
        }
        private void LoadGird()
        {
            DataTable tbl = cls.GetDataTable("[sp_Load_Autotranfer_detail]", new string[] { "@Userid" }, new object[] { Session["email"] });
            RadGrid2.DataSource = tbl;
            RadGrid2.DataBind();
        }

        protected void btnXoa_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in RadGrid2.MasterTableView.Items)
            {
                CheckBox CheckBox1 = item.FindControl("chkSelect") as CheckBox;
                if (CheckBox1 != null && CheckBox1.Checked == true)
                {
                    string strKey = item.GetDataKeyValue("ID").ToString();
                    cls.ExcuteSQL("sp_Delete_Autotranfer_detail", new string[] { "@ID" }, new object[] { strKey });

                }
            }
            LoadGird();

        }

    }
}