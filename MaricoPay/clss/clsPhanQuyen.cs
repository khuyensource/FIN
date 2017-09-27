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
//using Data;
using System.Web.Configuration;
using MaricoPay.DB;
namespace MaricoPay
{
    public class clsPhanQuyen : System.Web.UI.Page
    {
        string loaidf = ConfigurationManager.AppSettings["loadwowdefault"].ToString();
        protected override void OnPreInit(EventArgs e)
        {
            string site = Request.Url.Segments[Request.Url.Segments.Length - 1].ToString().Split('.')[0];

            if (Session.Contents["username"] == null)
            {
                Session["WebID"] = "~/" + site + ".aspx";
                Response.Redirect("~/Login.aspx");
            }
        }
    }
    public class clsPhanQuyenCaoCap : System.Web.UI.Page// : clsPhanQuyen
    {
        //clsObj Obj;
        //clsSql Sql = new clsSql();
        //string loaidf = ConfigurationManager.AppSettings["loadwowdefault"].ToString();
        protected override void OnPreInit(EventArgs e)
        {
            string site = Request.Url.Segments[Request.Url.Segments.Length - 1].ToString().Split('.')[0];
            Cclass cls = new Cclass();
            if (cls.cToString(Session["username"]) == "")
            {
                Session["WebID"] = "~/" + site + ".aspx";
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                //Obj = new clsObj();
                //Obj.Parameter = new string[] { "@manv", "@idsite" };
                //Obj.ValueParameter = new object[] { Session["UserID"].ToString(), site };
                //Obj.SpName = "SP_Lay_PhanQuyen_ID";
                //Sql.fGetData(Obj);
                // DBStoreDataContext dbs =new DBStoreDataContext();
                // DBStoreDataContext dbs = new DBStoreDataContext();
                //  var kq= dbs.SP_Lay_PhanQuyen_ID(Session["UserID"].ToString(), site);
              //  Cclass cls = new Cclass();
                DataTable tbl = cls.GetDataTable("SP_Lay_PhanQuyen_ID", new string[] { "@manv", "@idsite" }, new object[] { Session["username"].ToString(), site });
                if (tbl.Rows.Count == 0)
                {
                    Session["Name"] = Session["username"];
                    string Loi = "Bạn chưa được phân quyền vào trang này";
                    Response.Redirect("~/Loi.aspx?Strloi=" + Loi + "");
                }
                else
                {
                    // if (Obj.Dt.Rows[0]["IDQuyen"].ToString() == "KH")
                    if (cls.cToString(tbl.Rows[0]["IDQuyen"]) == "KH")
                    {
                        string Loi = "Bạn chưa được phân quyền vào trang này";
                        Response.Redirect("~/Loi.aspx?Strloi=" + Loi + "");
                    }
                }
            }
        }
    }
}