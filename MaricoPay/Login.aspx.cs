using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using MaricoPay.DB;
using System.Collections.Specialized;
//using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Data;
//using System.DirectoryServices.Protocols;
//using System.Security;
//using System.Web.Configuration;
using System.Configuration;
//using System.Web.Services;
namespace MaricoPay
{
    public partial class Login1 : System.Web.UI.Page
    {
        clsControl Ctrl = new clsControl();
        Cclass cls = new Cclass();
      
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Form.DefaultButton = LoginButton.UniqueID;
            if (!IsPostBack)
            {
                string code = !string.IsNullOrEmpty(Request.QueryString["type"]) ? Request.QueryString["type"] : "";
                if (code == "0")
                {
                   
                   Session.Clear();
                  Response.Cookies.Clear();
                    //Session.Abandon();
                  //  string jScript;
                   // jScript = "<script>wnds[wnds.length] = window.open();for(i = 0; i < wnds.length; ++i) wnds[i].close(); </script>";
                   // ClientScript.RegisterClientScriptBlock(this.GetType(), "keyClientBlock", jScript);
                   //  Page.ClientScript.RegisterOnSubmitStatement(typeof(Page), "closePage", "window.onload = HandleOnclose();");
                }
                else
                {
                    if (Session["Username"] != null)
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                }
               // this.Form.DefaultButton = this.LoginButton.UniqueID;
            }
        }
       
        protected void LoginButton_Click(object sender, EventArgs e)
        {
          
            //Microsoft.IdentityModel.Clients.ActiveDirectory.UserInfo us=new UserInfo();
             // Microsoft.IdentityModel.Clients.ActiveDirectory
          // us.
           // string sss = Ctrl.GetResource("loginerror");
            int vt = txtUserName.Text.Trim().IndexOf("@");
            string email;
            string username;
            if (vt > 0)
            {
                username = txtUserName.Text.Trim().Substring(0, vt).ToLower();
                email=txtUserName.Text.Trim().ToLower();
            }
            else
            {
                username =  txtUserName.Text.Trim().ToLower();
                email=username+"@marico.com";
            }
           // semail = semail + "@marico.com";
            //validateEmail chkemail = new validateEmail();
            //bool kqteeee = chkemail.ValidateCredentials(semail, txtPassword.Text, "smtp.office365.com", 587, true);
            string pssss =txtPassword.Text;
            string kqau = cls.GetString("sp_getConfig", new string[] { "@ID"}, new object[] { 4});
          //  kqau = "1";
            if (kqau == "1")
            {
                // LDAPAuthenticated(semail, txtPassword.Text);
                //   validateEmail chkemail = new validateEmail();
                //  bool kqteeee = chkemail.ValidateCredentials(semail, txtPassword.Text, "smtp.office365.com", 587, true);
                if (IsAuthenticated(username, txtPassword.Text) == true /*|| txtUserName.Text.ToLower() == "khuyen.tran" || txtUserName.Text.ToLower() == "diemptk" || txtUserName.Text.ToLower() == "dienpl"|| txtUserName.Text.ToLower() == "phuongdtd"*/ )
                {
                    if (Session["Username"] != null && username.ToLower() != cls.cToString(Session["Username"]).ToLower())
                    {

                        MsgBox1.AddMessage("Bạn đã đăng nhập với username: " + cls.cToString(Session["Username"]) + "<br/>Nếu bạn muốn đăng nhập với username khác vui lòng Log Out user đã đăng nhập trước đó", uc.ucMsgBox.enmMessageType.Error);

                    }
                    else
                    {
                        if (Session["WebID"] == null)
                        {
                            // Session["WebID"] = "~/Default.aspx";
                            Session["WebID"] = "~/Default.aspx";
                        }


                        // DBTableDataContext ds = new DBTableDataContext();

                        //try
                        //{
                        // User us=new DB.User();
                        //  Cclass cls = new Cclass();
                        email = cls.cToString(Session["email"]);
                        System.Data.DataTable tbl = cls.GetDataTable("sp_GetUserID", "@UserName", email);
                        //  var   us = ds.Users.First(o => o.Username == semail);
                        #region Auto add user từ AD
                        if (tbl.Rows.Count <= 0) //xac thu AD thanh cong nhung chua co user tren FIN, he thong se tu dong them tren FIN
                        {
                            //Session["username"] = null;

                            //MsgBox1.AddMessage("Your user ID has not been created in MBR system, Kindly contact HR to create user ID", uc.ucMsgBox.enmMessageType.Error);
                            int company = 1;
                            // string email = "";
                            string emailn1 = "";
                            string usern1 = "";
                            string fullname = "";
                            string chucdanh = "";
                            string dienthoai = "";
                            string phongban = "";
                            string Country = "";
                            string displayname = "";
                            int area = 48;//ko co area
                            if (cls.cToString(Session["area"]) != "")
                            {
                                DataTable Are = cls.GetDataTable("sp_GetAreaByName", new string[] { "@areaname" }, new object[] { Session["area"] });
                                area = cls.cToInt(Are.Rows[0]["ID"]);
                            }
                            
                            string senior = "";
                            string dir = "";
                            string vp = "";
                            string coo = "";
                            bool isN3 = false;
                            bool isManager = false;
                            
                            fullname = cls.cToString(Session["fullname"]);
                            chucdanh = cls.cToString(Session["chucdanh"]);
                            dienthoai = cls.cToString(Session["dienthoai"]);
                            phongban = cls.cToString(Session["phongban"]);
                            //  Country=GetProperty(result, "co");
                            Country = "VN";
                            displayname = cls.cToString(Session["displayname"]);
                            // area=GetProperty(result, "l");
                         //   area = 48;//Ko co Area
                            int level = 7;//ko co level
                            if (chucdanh.ToLower().Contains("director") == true || chucdanh.ToLower().Contains("manager") == true || chucdanh.ToLower().Contains("chief") == true || chucdanh.ToLower().Contains("vice president") == true || chucdanh.ToLower().Contains("asm") == true || chucdanh.ToLower().Contains("rsm") == true)
                            {
                                level = 2;
                                isManager = true;
                            }
                            else
                            {
                                if (chucdanh.ToLower().Contains("super") == true || chucdanh.ToLower().Contains("profe") == true)
                                {
                                    level = 5;

                                }
                                else
                                {
                                    if (chucdanh.ToLower().Contains("executive") == true)
                                    {
                                        level = 1;

                                    }
                                    else
                                    {
                                        if (chucdanh.ToLower().Contains("staff") == true)
                                        {
                                            level = 4;

                                        }
                                        else
                                        {
                                            if (chucdanh.ToLower().Contains("worker") == true)
                                            {
                                                level = 6;

                                            }
                                            else
                                            {
                                                level = 7;//ko co level
                                            }
                                        }
                                    }
                                }
                            }

                            // string n1abs = GetProperty(result, "manager");
                            //  int vtn1 = n1abs.IndexOf(",");
                            //  string n1name = n1abs.Substring(0, vtn1);
                            // n1name = n1name.Replace("CN=", "");
                            // search.Filter = "(cn=" + n1name + ")";

                            //  SearchResult result1 = search.FindOne();
                            if (Session["emailn1"] != null)
                            {
                                //Session["emailn1"] = GetProperty(result1, "mail");
                                // Session["usern1"] = cls.get_UsernameFromEmail(emailn1);
                                emailn1 = cls.cToString(Session["emailn1"]);
                                usern1 = cls.cToString(Session["usern1"]);
                                DataTable costcenterfc = cls.GetDataTable("sp_getCostCenterFunctionFromUser", new string[] { "@username" }, new object[] { usern1 });
                                string costcenter = cls.cToString(costcenterfc.Rows[0]["Costcenter"]);
                                string functionfk = cls.cToString0(costcenterfc.Rows[0]["Function_FK"]);
                                System.Data.DataTable tbltemp = cls.GetDataTable("sp_CreateTableSenDirVPCOO");
                                int index = cls.Search_DataTablei(tbltemp, "Username", emailn1);
                                if (index >= 0)//N1 co trong ds manager thi chac chan ko co bao cao RSM
                                {
                                    isN3 = false;
                                    int ps = cls.cToInt(tbltemp.Rows[index]["Position"]);
                                    System.Data.DataTable tblss = cls.GetDataTable("sp_getSeniDirVPCOOUser", "@username", emailn1);
                                    if (tblss.Rows.Count > 0)
                                    {
                                        senior = cls.cToString(tblss.Rows[0]["Senior"]);
                                        dir = cls.cToString(tblss.Rows[0]["Direct"]);
                                        vp = cls.cToString(tblss.Rows[0]["VP"]);
                                        coo = cls.cToString(tblss.Rows[0]["COO"]);
                                    }
                                    switch (ps)
                                    {
                                        case 1: //senior
                                            senior = emailn1;
                                            break;
                                        case 2://Director
                                            dir = emailn1;
                                            break;
                                        case 3://VP
                                            vp = emailn1;
                                            break;
                                        case 4://Coo
                                            coo = emailn1;
                                            break;
                                    }
                                }
                                else
                                {
                                    string sIsN3 = cls.GetString("sp_getIsN3FromRecomender", new string[] { "@Recoment" }, new object[] { emailn1 });
                                    isN3 = cls.cToBool(sIsN3);
                                    System.Data.DataTable tblss = cls.GetDataTable("sp_getSeniDirVPCOOUser", "@username", emailn1);
                                    if (tblss.Rows.Count > 0)
                                    {
                                        senior = cls.cToString(tblss.Rows[0]["Senior"]);
                                        dir = cls.cToString(tblss.Rows[0]["Direct"]);
                                        vp = cls.cToString(tblss.Rows[0]["VP"]);
                                        coo = cls.cToString(tblss.Rows[0]["COO"]);
                                    }
                                }
                                //xet xem co bao cao RSM isN3=true
                                //lay 1 thang linh dang co trong he thong cua N1 xem no co N3 ko, neu no co thi thang nay co, no ko thang nay ko

                                string position = cls.GetString0("sp_getPosition", new string[] { "@costcenter", "@position" }, new object[] { costcenter, chucdanh });
                                string depFK = cls.GetString0("sp_GetDepartmentFK", new string[] { "@positionpk", "@costcenter" }, new object[] { position, costcenter });
                                if (depFK == null || depFK == "0")
                                {
                                    depFK = costcenter;
                                }
                                if (cls.bThem(new string[] {"@Username","@Fullname","@Department_FK","@Market_FK","@Position_FK","@Email","@Recommender"
                                ,"@Pass","@Active","@Function_FK","@IsManager","@EmployeeCode","@Area_FK","@Level_FK","@SignatureURL","@Company_FK","@TelPhone","@SeniorManager"
                                ,"@Director","@VP_HOF","@COO","@Budget_Owner","@Costcenter","@IsN3","@vendorSAP","@DisplayName","@Autoupdate" }
                                , new object[] {username,fullname,depFK,"VN",position,email,usern1
                                ,"123",true,functionfk,isManager,"",area,level,"",company,dienthoai,senior
                                ,dir,vp,coo,null,costcenter,isN3,"",displayname,true}, "sp_InsertUsers") == true)
                                {
                                    // MsgBox1.AddMessage("Saved successfully", uc.ucMsgBox.enmMessageType.Success);
                                    Session["username"] = username.ToLower();
                                    Session["fullname"] = fullname;
                                    Session["email"] = email;
                                    Session["costcenter"] = costcenter;
                                    //HttpCookie loginCookie1 = new HttpCookie("loginCookie");
                                    //Response.Cookies["loginCookie1"].Value = username.ToLower(); // <--- strange!!!!
                                    //Response.Cookies.Add(loginCookie1); 
                                    clsSys sys = new clsSys();
                                    sys.insertLog(email, "login", true);

                                    Response.Redirect("~/Default.aspx");
                                }
                                else
                                {
                                    MsgBox1.AddMessage("Can not auto insert user", uc.ucMsgBox.enmMessageType.Error);
                                }

                            }
                            else
                            {
                                //ko co N1
                                // TextBox1.Text = "N1 khong ton tai";
                                MsgBox1.AddMessage("Your N1 has not been created in system, Kindly contact HR to create user ID", uc.ucMsgBox.enmMessageType.Error);
                            }
                        }
                        #endregion
                        else
                        {
                            ////Cap nhat lai thong tin neu co thay doi chuc danh, nguoi bao cao
                            clsSys sys = new clsSys();
                            email = cls.cToString(tbl.Rows[0]["Email"]).ToLower();
                            Session["username"] = cls.cToString(tbl.Rows[0]["Username"]).ToLower();
                          string  chucdanh = cls.cToString(Session["chucdanh"]);
                         
                           string phongban = cls.cToString(Session["phongban"]);
                          string  emailn1 = cls.cToString(Session["emailn1"]);
                            string usern1 = cls.cToString(Session["usern1"]);
                            //AutoUpdate
                            if (cls.cToBool(tbl.Rows[0]["AutoUpdate"]) == true && (cls.cToString(tbl.Rows[0]["Recommender"]).ToLower() != usern1.ToLower() || cls.cToString(tbl.Rows[0]["PositionName"]).ToLower() != chucdanh.ToLower() || cls.cToString(tbl.Rows[0]["AreaName"]).ToLower() != cls.cToString(Session["area"]).ToLower()))//đã thay đổi người bao cao hoac thay doi chuc danh
                            {
                                string senior = "";
                                string dir = "";
                                string vp = "";
                                string coo = "";
                                bool isN3 = false;
                                bool isManager = false;
                                string fullname = cls.cToString(Session["fullname"]);
                                string displayname = cls.cToString(Session["displayname"]);
                                string dienthoai = cls.cToString(Session["dienthoai"]);
                                int area = 48;//ko co area
                                if (cls.cToString(Session["area"]) != "")
                                {
                                    DataTable Are = cls.GetDataTable("sp_GetAreaByName", new string[] { "@areaname" }, new object[] { Session["area"] });
                                    area = cls.cToInt(Are.Rows[0]["ID"]);
                                }
                                int level = 7;//ko co level
                                if (chucdanh.ToLower().Contains("director") == true || chucdanh.ToLower().Contains("manager") == true || chucdanh.ToLower().Contains("chief") == true || chucdanh.ToLower().Contains("vice president") == true || chucdanh.ToLower().Contains("asm") == true || chucdanh.ToLower().Contains("rsm") == true)
                                {
                                    level = 2;
                                    isManager = true;
                                }
                                else
                                {
                                    if (chucdanh.ToLower().Contains("super") == true || chucdanh.ToLower().Contains("profe") == true )
                                    {
                                        level = 5;
                                       
                                    }
                                    else
                                    {
                                        if (chucdanh.ToLower().Contains("executive") == true)
                                        {
                                            level = 1;

                                        }
                                        else
                                        {
                                            if (chucdanh.ToLower().Contains("staff") == true)
                                            {
                                                level = 4;

                                            }
                                            else
                                            {
                                                if (chucdanh.ToLower().Contains("worker") == true)
                                                {
                                                    level = 6;

                                                }
                                                else
                                                {
                                                    level = 7;//ko co level
                                                }
                                            }
                                        }
                                    }
                                }
                                int company = 1;
                                //update full name
                                //cls.bCapNhat(new string[] { "@username", "@Fullname" }, new object[] { Session["username"], Session["fullname"] }, "sp_updateFullName");
                                DataTable costcenterfc = cls.GetDataTable("sp_getCostCenterFunctionFromUser", new string[] { "@username" }, new object[] { usern1 });
                                if (costcenterfc.Rows.Count <= 0)
                                {
                                    MsgBox1.AddMessage("N1 does not exists", uc.ucMsgBox.enmMessageType.Error);
                                    return;
                                }
                                string costcenter = cls.cToString(costcenterfc.Rows[0]["Costcenter"]);
                                string functionfk = cls.cToString0(costcenterfc.Rows[0]["Function_FK"]);
                                System.Data.DataTable tbltemp = cls.GetDataTable("sp_CreateTableSenDirVPCOO");
                                int index = cls.Search_DataTablei(tbltemp, "Username", emailn1);
                                if (index >= 0)//N1 co trong ds manager thi chac chan ko co bao cao RSM
                                {
                                    isN3 = false;
                                    int ps = cls.cToInt(tbltemp.Rows[index]["Position"]);
                                    System.Data.DataTable tblss = cls.GetDataTable("sp_getSeniDirVPCOOUser", "@username", emailn1);
                                    if (tblss.Rows.Count > 0)
                                    {
                                        senior = cls.cToString(tblss.Rows[0]["Senior"]);
                                        dir = cls.cToString(tblss.Rows[0]["Direct"]);
                                        vp = cls.cToString(tblss.Rows[0]["VP"]);
                                        coo = cls.cToString(tblss.Rows[0]["COO"]);
                                    }
                                    switch (ps)
                                    {
                                        case 1: //senior
                                            senior = emailn1;
                                            break;
                                        case 2://Director
                                            dir = emailn1;
                                            break;
                                        case 3://VP
                                            vp = emailn1;
                                            break;
                                        case 4://Coo
                                            coo = emailn1;
                                            break;
                                    }
                                }
                                else
                                {
                                    string sIsN3 = cls.GetString("sp_getIsN3FromRecomender", new string[] { "@Recoment" }, new object[] { emailn1 });
                                    isN3 = cls.cToBool(sIsN3);
                                    System.Data.DataTable tblss = cls.GetDataTable("sp_getSeniDirVPCOOUser", "@username", emailn1);
                                    if (tblss.Rows.Count > 0)
                                    {
                                        senior = cls.cToString(tblss.Rows[0]["Senior"]);
                                        dir = cls.cToString(tblss.Rows[0]["Direct"]);
                                        vp = cls.cToString(tblss.Rows[0]["VP"]);
                                        coo = cls.cToString(tblss.Rows[0]["COO"]);
                                    }
                                }
                                string position = cls.GetString0("sp_getPosition", new string[] { "@costcenter", "@position" }, new object[] { costcenter, chucdanh });
                                string depFK = cls.GetString0("sp_GetDepartmentFK", new string[] { "@positionpk", "@costcenter" }, new object[] { position, costcenter });
                                if (depFK == null || depFK == "0")
                                {
                                    depFK = costcenter;
                                }
                                if (cls.bThem(new string[] {"@Username","@Fullname","@Department_FK","@Market_FK","@Position_FK","@Email","@Recommender"
                                ,"@Pass","@Active","@Function_FK","@IsManager","@EmployeeCode","@Area_FK","@Level_FK","@SignatureURL","@Company_FK","@TelPhone","@SeniorManager"
                                ,"@Director","@VP_HOF","@COO","@Budget_Owner","@Costcenter","@IsN3","@vendorSAP","@DisplayName","@Autoupdate" }, new object[] {username,fullname,depFK,"VN",position,email,usern1
                                ,"123",true,functionfk,isManager,"",area,level,"",company,dienthoai,senior
                                ,dir,vp,coo,null,costcenter,isN3,"",displayname,true}, "sp_InsertUsers") == true)
                                {
                                    // MsgBox1.AddMessage("Saved successfully", uc.ucMsgBox.enmMessageType.Success);
                                    Session["username"] = username.ToLower();
                                    Session["fullname"] = fullname;
                                    Session["email"] = email;
                                    Session["costcenter"] = costcenter;
                                    //HttpCookie loginCookie1 = new HttpCookie("loginCookie");
                                    //Response.Cookies["loginCookie1"].Value = username.ToLower(); // <--- strange!!!!
                                    //Response.Cookies.Add(loginCookie1); 

                                }
                            }
                            else
                            {
                                if ((cls.cToString(tbl.Rows[0]["Fullname"]).Contains("/") == true || cls.cToString(tbl.Rows[0]["Fullname"]).Contains("-") == true) && (cls.cToString(Session["fullname"]).Contains("/") == false || cls.cToString(Session["fullname"]).Contains("-") == false))//full name chua duoc chinh sua
                                {
                                    //update full name
                                    cls.bCapNhat(new string[] { "@username", "@Fullname" }, new object[] { Session["username"], Session["fullname"] }, "sp_updateFullName");
                                }
                                else
                                {
                                    Session["fullname"] = tbl.Rows[0]["Fullname"];
                                }

                                Session["email"] = email;
                                Session["costcenter"] = tbl.Rows[0]["Costcenter"];
                            }
                          
                            sys.insertLog(email, "login", true);
                           
                            Response.Redirect("~/Default.aspx");
                          
                        }

                    }
                }
                else
                {
                    Session["username"] = null;
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + Ctrl.GetResource("loginerror") + "');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
                    MsgBox1.AddMessage("The user name or password incorrect", uc.ucMsgBox.enmMessageType.Error);
                }

            }

            else
            {

                // System.Data.DataTable tbl = cls.GetDataTable("sp_GetUserID", "@UserName", semail);
                DataTable Dt = cls.GetDataTable("spLoad_USER_ByIDPASS", new string[] { "@email", "@PASS" }, new object[] { email, Ctrl.fMaHoaPassWord(txtPassword.Text.Trim() + email) });
                if (Dt.Rows.Count > 0)
                {
                    if (Session["Username"] != null && username.ToLower() != cls.cToString(Session["Username"]).ToLower())
                    {

                        MsgBox1.AddMessage("Bạn đã đăng nhập với username: " + cls.cToString(Session["Username"]) + "<br/>Nếu bạn muốn đăng nhập với username khác vui lòng Log Out user đã đăng nhập trước đó", uc.ucMsgBox.enmMessageType.Error);

                    }
                    else
                    {
                        if (Session["WebID"] == null)
                        {
                            // Session["WebID"] = "~/Default.aspx";

                            Session["WebID"] = "~/Default.aspx";
                        }
                        Session["UserID"] = email;
                        //Session["fullname"] = Dt.Rows[0]["Fullname"];

                        clsSys sys = new clsSys();

                        email = cls.cToString(Dt.Rows[0]["Email"]).ToLower();
                        Session["username"] = cls.cToString(Dt.Rows[0]["Username"]).ToLower();
                        Session["fullname"] = Dt.Rows[0]["Fullname"];
                        Session["email"] = email;
                        Session["costcenter"] = Dt.Rows[0]["Costcenter"];
                        sys.insertLog(email, "login", true);
                        //NameValueCollection data = new NameValueCollection();
                        //data.Add("us", semail);

                        Response.Redirect("~/Default.aspx");

                    }
                }
                else
                {
                    clsSys sys = new clsSys();
                    sys.insertLog(email, "login", false);
                    Session["UserID"] = null;
                    Session["fullname"] = null;
                    Session["username"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Login fail');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);

                    //ltLoi.Text = "<font color='red'>Đăng nhập thất bại.</font>";
                }
            }
        }
        public string GetProperty(SearchResult searchResult, string PropertyName)
        {
            if (searchResult.Properties.Contains(PropertyName))
            {
                return searchResult.Properties[PropertyName][0].ToString().Trim();
            }
            else
            {
                return string.Empty;
            }
        }
        // Hàm này nhằm kiểm tra việc đăng nhập có đúng không
        public bool IsAuthenticated(String username, String pwd)
        {
            string ldapdomain = ConfigurationManager.AppSettings["LDAPDomain"].ToString();// <add key="LDAPDomain" value="LDAP://172.17.0.250" />
            bool trave = false;
            string domainuser = "MILCORP"+"\\" + username;
          //string  email = "";
          //string fullname = "";
          //string chucdanh = "";
          //string dienthoai = "";
          //string phongban = "";
          //string sinhnhat = "";
          //string displayname = "";
            DirectoryEntry entry = new DirectoryEntry(ldapdomain, domainuser, pwd/*, AuthenticationTypes.Anonymous*/);
           // entry.RefreshCache();

            try
            {
                //Bind to the native AdsObject to force authentication.                 
                Object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                entry.Dispose();
                SearchResult result = search.FindOne();
                Session["email"] = GetProperty(result, "mail");
                Session["fullname"] = GetProperty(result, "cn");
                Session["chucdanh"] = GetProperty(result, "title");
                Session["phongban"] = GetProperty(result, "department");
                Session["area"] = GetProperty(result, "physicalDeliveryOfficeName");
                Session["dienthoai"] = GetProperty(result, "mobile");
                Session["displayName"] = GetProperty(result, "displayName");
              
                //userAccountControl
                if (result!=null)
                {
                    trave = true;
                    try
                    {
                        string n1abs = GetProperty(result, "manager");
                        if (n1abs.Trim() != "")
                        {
                            int vtn1 = n1abs.IndexOf(",");
                            string n1name = n1abs.Substring(0, vtn1);
                            n1name = n1name.Replace("CN=", "");
                            search.Filter = "(cn=" + n1name + ")";
                            SearchResult result1 = search.FindOne();
                            if (result1 != null)
                            {
                                Session["emailn1"] = GetProperty(result1, "mail");
                                Session["usern1"] = cls.get_UsernameFromEmail(cls.cToString(Session["emailn1"]));
                            }
                            else
                            {
                                Session["emailn1"] = null;
                            }
                        }
                        else
                        {
                            Session["emailn1"] = null;
                        }
                    }
                    catch { }
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
        //public void LDAPAuthenticated(String username, String pwd)
        //{
        //   // var password = new[] { 'P', 'a', 's', 's', 'w', '@', 'r', 'd' };
        //    var secureString = new SecureString();
        //    foreach (var character in pwd)
        //        secureString.AppendChar(character);

        //    var baseOfSearch = "dc=milcorp,dc=com";
        //    var ldapHost = "milcorp.com";
        //    var ldapPort = 389; //SSL
        //    var connectAsDN = "cn=" + username;
        //   // var connectAsDN = "cn=" + username + ",dc=marico,dc=com";
        //    var pageSize = 1000;

        //    var openLDAPHelper = new LDAPHelper(
        //        baseOfSearch,
        //        ldapHost,
        //        ldapPort,
        //        AuthType.Basic,
        //        connectAsDN,
        //        secureString,
        //        pageSize);

        //    var searchFilter = "nextUID=*";
        //    var attributesToLoad = new[] { "nextUID" };
        //    var pagedSearchResults = openLDAPHelper.PagedSearch(
        //        searchFilter,
        //        attributesToLoad);

        //    foreach (var searchResultEntryCollection in pagedSearchResults)
        //        foreach (SearchResultEntry searchResultEntry in searchResultEntryCollection)
        //            Console.WriteLine(searchResultEntry.Attributes["nextUID"][0]);

        //    Console.Read();
        //}
    }
}