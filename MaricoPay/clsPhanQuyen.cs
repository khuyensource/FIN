using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Data;
using System.Web.Configuration;

public class clsPhanQuyen : System.Web.UI.Page
{
    string loaidf = ConfigurationManager.AppSettings["loadwowdefault"].ToString();
    protected override void OnPreInit(EventArgs e)
    {
        string site = Request.Url.Segments[Request.Url.Segments.Length - 1].ToString().Split('.')[0];

        if (Session.Contents["UserID"] == null)
        {
            Session["WebID"] = "~/" + site + ".aspx";
            Response.Redirect("~/wowdayweek.aspx?loai=" + loaidf);
        }
    }
}
public class clsPhanQuyenCaoCap : clsPhanQuyen
{
    clsObj Obj;
    clsSql Sql = new clsSql();
    string loaidf = ConfigurationManager.AppSettings["loadwowdefault"].ToString();
    protected override void OnPreInit(EventArgs e)
    {
        string site = Request.Url.Segments[Request.Url.Segments.Length - 1].ToString().Split('.')[0];

        if (Session.Contents["UserID"] == null)
        {
            Session["WebID"] = "~/" + site + ".aspx";
            Response.Redirect("~/wowdayweek.aspx?loai=" + loaidf);
        }
        Obj = new clsObj();
        Obj.Parameter = new string[] { "@manv", "@idsite" };
        Obj.ValueParameter = new object[] { Session["UserID"].ToString(), site };
        Obj.SpName = "SP_Lay_PhanQuyen_ID";
        Sql.fGetData(Obj);
        if (Obj.Dt.Rows.Count == 0)
        {
            string Loi = "Bạn chưa được phân quyền vào trang này";
            Response.Redirect("~/Loi.aspx?Strloi=" + Loi + "");
        }
        else
        {
            if (Obj.Dt.Rows[0]["IDQuyen"].ToString() == "KH")
            {
                string Loi = "Bạn chưa được phân quyền vào trang này";
                Response.Redirect("~/Loi.aspx?Strloi=" + Loi + "");
            }
        }
    }
}
