using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices.AccountManagement;

namespace MaricoPay
{
    public partial class _Default : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                System.Web.HttpBrowserCapabilities browser = Request.Browser;

                if (browser.Browser.ToLower().Contains("explorer") == true)
                {
                    MsgBox1.AddMessage("The FIN system is not compatible with Internet Explorer, Please using Firefox or Chorme", uc.ucMsgBox.enmMessageType.Error);
                }
                else
                {
                    var cookie = HttpContext.Current.Request.Cookies["ckusername"];
                    if (cookie != null)
                    {
                        var cookieName = cookie.Name;
                        var expiredCookie = new HttpCookie(cookieName) { Expires = DateTime.Now.AddDays(-1) };
                        HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                    }
                    LoadVPList();
                    LoadDirectorList();
                    LoadCOOList();
                    ucViewStatus1.Store = "sp_getTravelStatus";
                    ucViewStatus2.Store = "sp_getExpensesStatus";
                    ucViewStatus3.Store = "sp_getContractStatus";
                    //try
                    //{
                    //    string loggedOnUser = string.Empty;

                    //    //loggedOnUser = Request.ServerVariables.Get("AUTH_USER");
                    //    loggedOnUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name; //HttpContext.Current.Request.LogonUserIdentity.Name;
                    //    // System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    //    //    System.Web.Security.MembershipUser usr = System.Web.Security.Membership.GetUser();
                    //    lbloggedOnUser.Text = loggedOnUser;
                    //    //  System.Web.HttpContext.Current.User.Identity.IsAuthenticated
                    //    int vt = loggedOnUser.LastIndexOf("\\");
                    //    string usernameid = loggedOnUser.Substring(vt + 1);
                    //    lbusernameid.Text = usernameid;
                    //    // usernameid = "khuyentt";
                    //    //if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                    //    //{

                    //    //    string userName = "Hello, " + System.Web.HttpContext.Current.User.Identity.Name;

                    //    //    //TextBox1.Text = userName;

                    //    //}
                    //    string userName = string.Empty;
                    //    string sdomain = "marico.com";
                    //    PrincipalContext pc = new PrincipalContext(ContextType.Domain, sdomain);
                    //    lbPC.Text = pc.Name;
                    //    UserPrincipal user = new UserPrincipal(pc);

                    //    user = UserPrincipal.FindByIdentity(pc, usernameid);
                    //    lbuser.Text = user.GivenName + " " + user.Surname;
                    //    if (user != null)
                    //    {
                    //        userName = user.GivenName + " " + user.Surname;

                    //        clsSys sys = new clsSys();
                    //        string email = user.EmailAddress;
                    //        Session["username"] = usernameid;
                    //        Session["fullname"] = user.GivenName + " " + user.Surname;
                    //        Session["email"] = email;
                    //        sys.insertLog(email, "login", true);
                    //        System.Data.DataTable tbl = cls.GetDataTable("sp_getInfoDefault", "@username", Session["username"]);
                    //        if (tbl.Rows.Count > 0)
                    //        {
                    //            lbName.Text = cls.cToString(tbl.Rows[0]["fullname"]) + "'s";
                    //            txtPosition.Text = cls.cToString(tbl.Rows[0]["Position"]);
                    //            txtN1.Text = cls.cToString(tbl.Rows[0]["N1"]);
                    //            txtN2.Text = cls.cToString(tbl.Rows[0]["N2"]);
                    //            comboDepartment1.Values = cls.cToString(tbl.Rows[0]["Costcenter"]);
                    //            dropVP.SelectedValue = cls.cToString(tbl.Rows[0]["VP_HOF"]);
                    //            dropDirector.SelectedValue = cls.cToString(tbl.Rows[0]["Director"]);
                    //            dropCOO.SelectedValue = cls.cToString(tbl.Rows[0]["COO"]);
                    //        }
                    //    }
                    //    pc.Dispose();
                    //    user.Dispose();
                    //}
                    //catch
                    //{

                        if (Session["username"] != null /*&& Request.Form["us"] != null*/)
                        // if (Request.QueryString["us"] != null)//click vao avatar
                        {
                          //  userName = user.GivenName + " " + user.Surname;

                         //   clsSys sys = new clsSys();
                         //   string email = user.EmailAddress;
                           // Session["username"] = usernameid;
                         //   Session["fullname"] = user.GivenName + " " + user.Surname;
                         //   Session["email"] = email;
                         //   sys.insertLog(email, "login", true);
                            System.Data.DataTable tbl = cls.GetDataTable("sp_getInfoDefault", "@username", Session["username"]);
                            if (tbl.Rows.Count > 0)
                            {
                                lbName.Text = cls.cToString(tbl.Rows[0]["fullname"]) + "'s";
                                txtPosition.Text = cls.cToString(tbl.Rows[0]["Position"]);
                                txtN1.Text = cls.cToString(tbl.Rows[0]["N1"]);
                                txtN2.Text = cls.cToString(tbl.Rows[0]["N2"]);
                                try
                                {
                                    comboDepartment1.Values = cls.cToString(tbl.Rows[0]["Costcenter"]);
                                }
                                catch { }
                                dropVP.SelectedValue = cls.cToString(tbl.Rows[0]["VP_HOF"]);
                                dropDirector.SelectedValue = cls.cToString(tbl.Rows[0]["Director"]);
                                dropCOO.SelectedValue = cls.cToString(tbl.Rows[0]["COO"]);
                                txtSAPCode.Text = cls.cToString(tbl.Rows[0]["vendorSAP"]);
                            }
                        }
                        else
                        {
                            Response.Redirect("~/Login.aspx");
                        }
                    //}
                }
            }
        }
        private void LoadVPList()
        {
          System.Data.DataTable tbl=  cls.GetDataTable("sp_getVPList");
          dropVP.DataSource = tbl;
          dropVP.DataBind();
        }
        private void LoadDirectorList()
        {
            System.Data.DataTable tbl = cls.GetDataTable("sp_getDirectorList");
            dropDirector.DataSource = tbl;
            dropDirector.DataBind();
        }
        private void LoadCOOList()
        {
            System.Data.DataTable tbl = cls.GetDataTable("sp_getCOOList");
            dropCOO.DataSource = tbl;
            dropCOO.DataBind();
        }
        protected void btSave_Click(object sender, EventArgs e)
        {
            if (comboDepartment1.Values == "")
            {
                MsgBox1.AddMessage("Please select costcenter",uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            //if (dropVP.SelectedValue == "")
            //{
            //    MsgBox1.AddMessage("Please select VP",uc.ucMsgBox.enmMessageType.Error);
            //    return;
            //}
            //if (dropDirector.SelectedValue == "")
            //{
            //    MsgBox1.AddMessage("Please select Director",uc.ucMsgBox.enmMessageType.Error;
            //    return;
            //}
            if (dropCOO.SelectedValue == "")
            {
                MsgBox1.AddMessage("Please select COO", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (cls.bCapNhat(new string[] { "@username", "@costcenter","@VP","@COO","@DR" }, new object[] { Session["username"], comboDepartment1.Values,dropVP.SelectedValue,dropCOO.SelectedValue,dropDirector.SelectedValue }, "sp_updateCostcenter4User") == true)
            {
                Session["costcenter"] = comboDepartment1.Values;
                MsgBox1.AddMessage("Update Sucessfully!", uc.ucMsgBox.enmMessageType.Success);
            }
            else
            {
                MsgBox1.AddMessage("Update Fail!", uc.ucMsgBox.enmMessageType.Error);
            }
        }
    }
}
