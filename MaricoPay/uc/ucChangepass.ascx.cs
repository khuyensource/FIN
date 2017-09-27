using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace MaricoPay.uc
{
    public partial class ucChangepass : System.Web.UI.UserControl
    {
        clsControl Ctrl = new clsControl();
        Cclass cls = new Cclass();
        static string prevPage = String.Empty;
        //string loaidf = ConfigurationManager.AppSettings["loadwowdefault"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Timer1.Enabled = false;
                try
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                catch { prevPage = Request.Url.ToString(); }
                if (Session["UserID"] == null)
                {
                    //kiem tra xem co phai goi tu chuc nang reset pas
                    if (Request.QueryString["d"] != null && Request.QueryString["ac"] != null && Request.QueryString["m"] != null)//goi reset pass
                    {
                        string email = Request.QueryString["m"].ToString() + Request.QueryString["d"].ToString();
                        string usemail = Request.QueryString["m"].ToString();
                        string pass = Request.QueryString["ac"].ToString();
                        ltcurentpass.Visible = false;
                        txtmkcu.Visible = false;
                        //check user password xem dung khong, neu dung cho phep thuc hien reset

                        DataTable Dt = cls.GetDataTable("spLoad_USER_ByIDPASS", new string[] { "@email", "@PASS" }, new object[] { email, pass });
                        if (Dt.Rows.Count > 0)
                        {
                            hfflag.Value = "1";//khong kiem tra mat khau cu
                            hfemail.Value = email;
                            hfuser.Value = usemail;
                        }
                        else
                        {
                            hfflag.Value = "0";
                            hfemail.Value = "";
                            hfuser.Value = "";
                            //ko ton tai user
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Thông tin đăng nhập không đúng/Login is incorrect');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
                            Response.Redirect("Login.aspx");

                        }
                    }
                    else
                    {
                        ltcurentpass.Visible = true;
                        txtmkcu.Visible = true;
                        hfflag.Value = "0";
                        Response.Redirect(prevPage);
                        //Response.Redirect("~/wowdayweek.aspx?loai=0");
                    }
                }
                else
                {
                    hfemail.Value = Session["UserID"].ToString();
                    hfuser.Value = Session["UserID"].ToString();
                    ltcurentpass.Visible = true;
                    txtmkcu.Visible = true;
                    hfflag.Value = "0";
                }
            }
        }
        //private string getresource(string vl)
        //{
        //    return (string)HttpContext.GetGlobalResourceObject(Session["LangFrontend"].ToString(), vl);
        //}
        private bool Fkiemtrapasscu()
        {
            string passcu = Ctrl.fMaHoaPassWord(txtmkcu.Text + Session["UserID"].ToString());
            bool kq = false;
            //Obj = new clsObj();
            //Obj.Parameter = new string[] { "@email", "@PASS" };
            //Obj.ValueParameter = new object[] { Session["UserID"], passcu };
            //Obj.SpName = "spLoad_USER_ByIDPASS";
            //Sql.fGetData(Obj);
            DataTable Dt = cls.GetDataTable("spLoad_USER_ByIDPASS", new string[] { "@email", "@PASS" }, new object[] { Session["UserID"], passcu });
            if (Dt.Rows.Count == 0)
            {
                kq = false;
                // ltLoi.Text = "<font color='red'>" + getresource("passerror") + "</font>";
            }
            else
            {
                kq = true;
            }
            return kq;
        }

        private void Fdoipass()
        {
            if (tbPass.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Mật khẩu không được để trống/Password not must empty');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }
            else
            {
                if (tbPass.Text != tbPasslan2.Text)
                {
                    ltLoi.Text = "<font color='red'>Nhập lại mật khẩu không đúng/Confirm password is incorrect</font>";
                }
                else
                {

                    bool kq = cls.bCapNhat(new string[] { "@email", "@PASS" }, new object[] { hfemail.Value, Ctrl.fMaHoaPassWord(tbPasslan2.Text + hfuser.Value) }, "spEdit_Pass");
                    if (kq == true)
                    {
                        clsSys sys = new clsSys();
                        sys.insertLog(hfemail.Value, "changepass", true);
                        // Response.Redirect(".");
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Đổi mật khẩu thành công/Password has changed successfuly');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
                        ltLoi.Text = "<font color='blue'><a href='login.aspx'>Mật khẩu đã đổi thành công,bạn cần đăng nhập lại với mật khẩu mới</a></font>";
                     //   Timer1.Enabled = true;
                      //  Response.Redirect("Login.aspx");
                    }
                }
            }
        }
        protected void btDangNhap_Click(object sender, EventArgs e)
        {
            if (hfflag.Value == "1")//goi chuc nang reset mat khau, da kiem tra hop le roi jo ko kiem nua
            {
                Fdoipass();
            }
            else
            {
                if (Fkiemtrapasscu() == true)
                {
                    Fdoipass();
                }
                else
                {
                    ltLoi.Text = "<font color='red'>Mật khẩu không đúng/Password is incorrect</font>";
                }
            }

        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Timer1.Enabled = false;
            Response.Redirect(".");
        }
    }
}