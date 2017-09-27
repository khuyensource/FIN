using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Data;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using MaricoPay.DB;
//using Telerik.Web.UI;

public partial class uc_ucloginV2 : clsLang // System.Web.UI.UserControl
{
    //clsObj Obj;
    //clsSql Sql = new clsSql();
    clsControl Ctrl = new clsControl();
    string loaidf = ConfigurationManager.AppSettings["loadwowdefault"].ToString();
    static string email, fullname, chucdanh, dienthoai, phongban, sinhnhat;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getsessionLang();
           
            //btlogin.Text = getresource("ltlogin");
            //ltforgetpass.Text=getresource("ltforgetpass");
            //ltregis.Text = getresource("ltregis");
           
        }
    }
    private string ApplicationPath
    {
        get
        {
            string url = HttpContext.Current.Request.ApplicationPath;
            if (url.EndsWith("/"))
                return url;
            else
                return url + "/";
        }
    }
    private string getresource(string vl)
    {
        //getsessionLang();
        return (string)HttpContext.GetGlobalResourceObject(Session["LangFrontend"].ToString(), vl);
    }
    //../../admincp/Top/images/'<%# getsessionLang() %>'/login.png
    public string getsessionLang()
    {
        string kq = "vi";
        if (Session["LangFrontend"] != null)
            kq = Session["LangFrontend"].ToString();

        else
            Session["LangFrontend"] = "vi";
        kq = "vi";
        return kq;

    }
    //public string getsessionLang()
    //{
    //    if (Session["LangFrontend"] != null)
    //        //  return Session["LangFrontend"].ToString();
    //        return "this.src='admincp/Top/images/" + Session["LangFrontend"].ToString() + "/loginOver.png'";
    //    else
    //        //return "vi";
    //        return "this.src='admincp/Top/images/vi/loginOver.png'";
    //}
    private bool IsExistsUS()
    {
        DBTableDataContext db = new DBTableDataContext();
        
            //User us = new User();
            bool euser = db.Users.Any(p => p.Username == email);
        db.Dispose();
            if (euser)
            {
                return true;
            }
            else
            {
                return false;
            }
       
        ////bool contactExists = db.Any(contact => contact.ContactName.Equals(ContactName));

        ////if (contactExists)
        ////{
        ////    return -1;
        ////}

        ////Obj = new clsObj();
        ////Obj.Parameter = new string[] { "@email" };
        ////Obj.ValueParameter = new object[] { email };
        ////Obj.SpName = "IsExistsUser";
        ////Sql.fGetData(Obj);
        //DBStoreDataContext dbs=new DBStoreDataContext();
        //var bien =dbs.IsExistsUser(email);
        //if (bien.Count() > 0)
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}
    }
    protected void btlogin_Click(object sender, EventArgs e)
    {

        int vt =txtemail.Text.Trim().IndexOf("@");
        string semail;
        if (vt > 0)
        {
            semail = txtemail.Text.Trim().Substring(0, vt);
        }
        else
        {
            semail = txtemail.Text.Trim();
        }
       
        string pssss = txtpass.Text;
        if (IsAuthenticated(semail, txtpass.Text) == true)
        {
            if (Session["WebID"] == null)
            {
                // Session["WebID"] = "~/Default.aspx";
                Session["WebID"] = "~/wowdayweek.aspx?loai=" + loaidf;
            }

            //    Obj = new clsObj();
            //    Obj.Parameter = new string[] { "@acc" };
            //    Obj.ValueParameter = new object[] { semail };
            //Obj.SpName = "getemail";
            //Sql.fGetData(Obj);
            DBTableDataContext ds = new DBTableDataContext();
            //var model = db.dl_Ads.ToList();
            //RG.DataSource = model;
            //RG.DataBind();
            User us = ds.Users.First(o => o.Username == semail);

            if (us.Username.Count() > 0)
            {

                //}
                //if (Obj.Dt.Rows.Count > 0)
                //{
                clsSys sys = new clsSys();
                email = sys.cToString(us.Email);
                Session["UserID"] = us.Username;
                Session["fullname"] = us.Fullname;
                sys.insertLog(email, "login", true);
                //// Obj = new clsObj();
                // // DataTable dt = new DataTable();
                // Obj.Parameter = new string[] { "@email" };
                // Obj.ValueParameter = new object[] { email };
                // Obj.SpName = "get_usersv2";
                // Sql.fGetData(Obj);
                // Session["allusers"] = Obj.Dt;
                // // dt = Obj.Dt;
                // Response.Redirect("~/wowdayweek.aspx?loai=" + loaidf);
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + Ctrl.GetResource("regerror") + "');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);


            }
        }
        else
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + Ctrl.GetResource("regerror") + "');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
        }
    }
    //private short _tabIndex;
    //public short TabIndex
    //{
    //    get
    //    {
    //        return _tabIndex;
    //    }
    //    set
    //    {
    //        _tabIndex = value;

    //        txtemail.TabIndex = (short)(value++);

    //        txtpass.TabIndex = (short)(value++);


    //        btlogin.TabIndex = (short)(value);
    //    }
    //}
    //// Hàm này nhằm mục đích đọc các thông tin như ( email,họ tên,chức danh,SĐT…) của 1 user trong domain 

    public string GetProperty(SearchResult searchResult, string PropertyName)
    {
        if (searchResult.Properties.Contains(PropertyName))
        {
            return searchResult.Properties[PropertyName][0].ToString();
        }
        else
        {
            return string.Empty;
        }
    }
    // Hàm này nhằm kiểm tra việc đăng nhập có đúng không
    public bool IsAuthenticated(String username, String pwd)
    {
        bool trave = false;
        email = "";
        fullname = "";
        chucdanh = "";
        dienthoai="";
        phongban = "";
        sinhnhat = "";
        DirectoryEntry entry = new DirectoryEntry("LDAP://icpvietnam.com", username, pwd);
        

        try
        {
            //Bind to the native AdsObject to force authentication.                 
            Object obj = entry.NativeObject;

            DirectorySearcher search = new DirectorySearcher(entry);
            search.Filter = "(SAMAccountName=" + username + ")";

            SearchResult result = search.FindOne();
            email = GetProperty(result, "mail");
            fullname = GetProperty(result, "cn");
            chucdanh = GetProperty(result, "title");
            phongban = GetProperty(result, "title");
            sinhnhat = GetProperty(result, "title");
            dienthoai=GetProperty(result, "telephoneNumber");
            //Response.Write("Họ: " + GetProperty(sResultSet, "givenName") + "<br>");
            //// Middle Initials
            //Response.Write("Đệm: " + GetProperty(sResultSet, "initials") + "<br>");
            //// Last Name
            //Response.Write("Tên: " + GetProperty(sResultSet, "sn") + "<br>");
            if (null != result)
            {
                trave = true;
            }
        }
        //Update the new path to the user in the directory.
        catch //(Exception ex)
        {
            trave = false;
           // lbl_erorr.Text = "The username or password you entered is incorrect";
        }
        return trave;
    }

}
