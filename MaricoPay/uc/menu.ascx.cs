using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using Telerik.Web.UI;
using Data;
namespace MaricoPay.uc
{
    public partial class menu : System.Web.UI.UserControl
    {
        //clsObj Obj;
        //clsSql Sql = new clsSql();


        protected void Page_Load(object sender, EventArgs e)
        {
            //LockmenuPDP(Session["UserID"].ToString());
            //LockmenuMbrN1(Session["UserID"].ToString());
            //LockmenuMbrN2(Session["UserID"].ToString());

        }

        //void LockmenuPDP(string User)
        //{


        //    Obj = new clsObj();
        //    Obj.Parameter = new string[] { "@userid" };
        //    Obj.ValueParameter = new object[] { User };
        //    Obj.SpName = "[sp_load_PDP_User]";
        //    Sql.fGetData(Obj);
        //    if (Obj.Dt.Rows.Count > 0)
        //    {
        //        RadMenu2.FindItemByText("Nhân viên tự đánh giá bản thân").Visible = true;
        //    }
        //    else
        //    {
        //        RadMenu2.FindItemByText("Quản lý đánh giá cấp dưới").Visible = false;
        //    }
        //}

        //void LockmenuMbrN1(string User)
        //{


        //    Obj = new clsObj();
        //    Obj.Parameter = new string[] { "@userid" };
        //    Obj.ValueParameter = new object[] { User };
        //    Obj.SpName = "[sp_load_PDP_User_N1]";
        //    Sql.fGetData(Obj);
        //    if (  int.Parse(Obj.Dt.Rows[0]["nhanvien"].ToString())  > 0)
        //    {
        //        RadMenu2.FindItemByText("Verify goals (for direct reports)").Visible = true;
        //    }
        //    else
        //    {
        //        RadMenu2.FindItemByText("Verify goals (for direct reports)").Visible = false;
        //    }
        //}

        //void LockmenuMbrN2(string User)
        //{


        //    Obj = new clsObj();
        //    Obj.Parameter = new string[] { "@userid" };
        //    Obj.ValueParameter = new object[] { User };
        //    Obj.SpName = "[sp_load_PDP_User_N2]";
        //    Sql.fGetData(Obj);
        //    if (int.Parse(Obj.Dt.Rows[0]["nhanvien"].ToString()) > 0)
        //    {
        //        RadMenu2.FindItemByText("Approve goals (for team members)").Visible = true;
        //    }
        //    else
        //    {
        //        RadMenu2.FindItemByText("Approve goals (for team members)").Visible = false;
        //    }
        //}

        //void Lockmenu(string User)
        //{

        //    Obj = new clsObj();
        //    Obj.Parameter = new string[] { "@userid", };
        //    Obj.ValueParameter = new object[] { User };
        //    Obj.SpName = "sp_load_MIP_User";
        //    Sql.fGetData(Obj);
        //    if (Obj.Dt.Rows.Count > 0)
        //    {
        //        RadMenu2.FindItemByText("MIP").Visible = true;
        //    }
        //    else
        //    {
        //        RadMenu2.FindItemByText("MIP").Visible = false;
        //    }
        //}
    }
}