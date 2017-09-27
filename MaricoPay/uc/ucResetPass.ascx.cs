using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace MaricoPay.uc
{
    public partial class ucResetPass : System.Web.UI.UserControl
    {
        clsSys sys = new clsSys();
        Cclass cls = new Cclass();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Timer1.Enabled = false;

            }
        }
        private string SubEmail()
        {
            //Thiet C just gave you a Woo!
             string sub = "";

            sub = "Fin yeu cau tao lai mat khau/Fin password reset request";

            return sub;
        }
        private string ContenEmail()
        {
            string content = "";
            //Obj = new clsObj();
            //Obj.Parameter = new string[] { "@Email" };
            //Obj.ValueParameter = new object[] { txtemail.Text.Trim() };
            //Obj.SpName = "get_pass_byEmail";
            //Sql.fGetData(Obj);
            DataTable Dt = cls.GetDataTable("get_pass_byEmail", "@Email", txtemail.Text.Trim());
            if (Dt.Rows.Count > 0)
            {
                // string domain = ConfigurationManager.AppSettings["domain"].ToString();
                int vt = txtemail.Text.Trim().LastIndexOf("@");
                //if (Session["LangFrontend"] == null)
                //{
                //    Session["LangFrontend"] = "vi";
                //}
                //switch (Session["LangFrontend"].ToString())
                //{
                //    case "en":
                //        content = "In order to reset your password at WOW!WEB, you need to click the link below\n\r " + domain + "/changepass.aspx?d=" + txtemail.Text.Trim().Substring(vt) + "&ac=" + Dt.Rows[0][0].ToString() + "&m=" + txtemail.Text.Trim().Substring(0, vt) + "\n\rIf you did not request this email, you may safely ignore it.";//d=domain email; acc=mat khau duoc ma hoa; m=email
                //        break;
                //    default: //vi
                //        content = "Vui lòng vào link bên dưới để phục hồi mật khẩu\n\r " + domain + "/changepass.aspx?d=" + txtemail.Text.Trim().Substring(vt) + "&ac=" + Dt.Rows[0][0].ToString() + "&m=" + txtemail.Text.Trim().Substring(0, vt);//d=domain email; acc=mat khau duoc ma hoa; m=email
                //        break;
                //}
                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                content = "Vui lòng vào link bên dưới để phục hồi mật khẩu\n\r " + strUrl + "/changepass.aspx?d=" + txtemail.Text.Trim().Substring(vt) + "&ac=" + Dt.Rows[0][0].ToString() + "&m=" + txtemail.Text.Trim().Substring(0, vt);//d=domain email; acc=mat khau duoc ma hoa; m=email
               
                content = content + "</br>In order to reset your password at WOW!WEB, you need to click the link below\n\r " + strUrl + "/changepass.aspx?d=" + txtemail.Text.Trim().Substring(vt) + "&ac=" + Dt.Rows[0][0].ToString() + "&m=" + txtemail.Text.Trim().Substring(0, vt) + "\n\rIf you did not request this email, you may safely ignore it.";//d=domain email; acc=mat khau duoc ma hoa; m=email
            }

            return content;
        }
        //private string getresource(string vl)
        //{
        //    return (string)HttpContext.GetGlobalResourceObject(Session["LangFrontend"].ToString(), vl);
        //}
        private bool IsExistsUS()
        {
            //Obj = new clsObj();
            //Obj.Parameter = new string[] { "@email" };
            //Obj.ValueParameter = new object[] { txtemail.Text.Trim() };
            //Obj.SpName = "IsExistsUser";
            //Sql.fGetData(Obj);
            DataTable Dt = cls.GetDataTable("IsExistsUserEmail", "@email", txtemail.Text.Trim());
            if (Dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void btsave_Click(object sender, EventArgs e)
        {
            if (sys.CheckEmail(txtemail.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Email không đúng/Email address is incorrect');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
                return;
            }
            if (IsExistsUS() == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Người dùng không tồn tại/User not exists');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
                return;
            }
            //if (sys.CheckEmail(txtemail.Text.Trim()) == true && IsExistsUS()==true)
            //{
            if (sys.SendMailASP(txtemail.Text.Trim(), SubEmail(), ContenEmail()) == true)
            {
                // clsSys sys = new clsSys();
                sys.insertLog(txtemail.Text.Trim(), "resetpass", true);
                //gui mail thanh cong
                lbThongBao.Text = "Vui lòng kiểm tra email để tiến hành thay đổi mật khẩu/Please check your email to change password";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Vui lòng kiểm tra email để tiến hành thay đổi mật khẩu/Please check your email to change password');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
               // Timer1.Enabled = true;
            }
            else
            {
                sys.insertLog(txtemail.Text.Trim(), "resetpass", false);
                //gui mail that bai
                lbThongBao.Text = "Email thay đổi mật khẩu không gửi đến bạn không được/Can't send email to you";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Email thay đổi mật khẩu không gửi đến bạn không được/Can't send email to you');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);

            }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + getresource("emailerror") + "');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            //    return;
            //}
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Timer1.Enabled = false;
            Response.Redirect(".");
        }
    }
}