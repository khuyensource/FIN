using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Configuration;
namespace MaricoPay
{
    public partial class ADUser : clsPhanQuyenCaoCap
    {
        Cclass cls=new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["ADMaricosea"] = null;
            }

        }

        private void Getdata(string username, string pwd)
        {
            
            string ldapdomain = ConfigurationManager.AppSettings["LDAPDomain"].ToString();// <add key="LDAPDomain" value="LDAP://172.17.0.250" />
          //  bool trave = false;
            string domainuser = "MILCORP" + "\\" + username;
            DirectoryEntry entry = new DirectoryEntry(ldapdomain, domainuser, pwd);


            try
            {
                //Bind to the native AdsObject to force authentication.                 
                Object obj = entry.NativeObject;



                DirectorySearcher search = new DirectorySearcher(entry);
                username = cls.get_UsernameFromEmail(username);
                //  search.Filter = "(company=" + username + ")";
                search.Filter = "(|(company=Marico SEA)(company=INTERNATIONAL BUSINESS))";
                entry.Dispose();

                DataTable tbluslist = new DataTable();
                DataColumn dtc;
                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Name";
                tbluslist.Columns.Add(dtc);

                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "DisplayName";
                tbluslist.Columns.Add(dtc);

                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Username";
                tbluslist.Columns.Add(dtc);
               
                //dtc = new DataColumn();
                //dtc.DataType = Type.GetType("System.String");
                //dtc.ColumnName = "Fullname";
                //tbluslist.Columns.Add(dtc);

                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Email";
                tbluslist.Columns.Add(dtc);

                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Title";
                tbluslist.Columns.Add(dtc);

                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Department";
                tbluslist.Columns.Add(dtc);

                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "company";
                tbluslist.Columns.Add(dtc);

                //dtc = new DataColumn();
                //dtc.DataType = Type.GetType("System.String");
                //dtc.ColumnName = "Description";
                //tbluslist.Columns.Add(dtc);

                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Manager";
                tbluslist.Columns.Add(dtc);

                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Office";
                tbluslist.Columns.Add(dtc);

                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Address";
                tbluslist.Columns.Add(dtc);

                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Country";
                tbluslist.Columns.Add(dtc);

                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "City";
                tbluslist.Columns.Add(dtc);

                
                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Province";
                tbluslist.Columns.Add(dtc);

                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Mobile";
                tbluslist.Columns.Add(dtc);

                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Telephone";
                tbluslist.Columns.Add(dtc);


                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Status";
                tbluslist.Columns.Add(dtc);

                       
                DataRow dr;
                             
                foreach (SearchResult result in search.FindAll())
                {
                    dr = tbluslist.NewRow();
                    dr["Name"] = GetProperty(result, "name"); 
                    dr["Username"] = GetProperty(result, "samaccountname");
                   // dr["Fullname"] = GetProperty(result, "cn");
                    dr["Email"] = GetProperty(result, "mail");
                    dr["DisplayName"] = GetProperty(result, "displayName");
                    dr["Department"] = GetProperty(result, "department");
                    dr["Title"] = GetProperty(result, "title");
                   // dr["Description"] = GetProperty(result, "description");
                    dr["Office"] = GetProperty(result, "physicaldeliveryofficename");
                    dr["Mobile"] = GetProperty(result, "mobile");
                    dr["Telephone"] = GetProperty(result, "telephonenumber");
                    dr["Country"] = GetProperty(result, "co");
                    dr["City"] = GetProperty(result, "l");
                    dr["Province"] = GetProperty(result, "st");
                    dr["Address"] = GetProperty(result, "streetaddress");
                    dr["company"] = GetProperty(result, "company");
                    string n1abs=GetProperty(result, "manager");
                    if (n1abs.Trim() != "")
                    {
                        
                        int vtn1 = n1abs.IndexOf(",");
                        string n1name = n1abs.Substring(0, vtn1);
                        n1name = n1name.Replace("CN=", "");
                        dr["Manager"] = n1name;

                        ////Neu muon lay username của N1 thi mo doan code duoi
                        //search.Filter = "(cn=" + n1name + ")";
                        //SearchResult result1 = search.FindOne();
                        //if (result1 != null)
                        //{
                        //    dr["Manager"] = cls.get_UsernameFromEmail(GetProperty(result1, "mail"));
                          
                        //}
                        //else
                        //{
                        //    dr["Manager"] = "";
                        //}
                       
                    }
                    else
                    {
                        dr["Manager"] = "";
                    }
                    string trangthai="Active";
                    if(GetProperty(result, "useraccountcontrol")!="512")
                    {
                        trangthai="Stop";
                    }
                    dr["Status"] = trangthai;
                    tbluslist.Rows.Add(dr);

                   
                }
                Session["ADMaricosea"] = tbluslist;
                RadGrid1.DataSource = tbluslist;
                RadGrid1.DataBind();
            }
            catch { }
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
        protected void btGetData_Click(object sender, EventArgs e)
        {
            Getdata(cls.cToString(Session["Username"]), txtPass.Text);
        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case Telerik.Web.UI.RadGrid.FilterCommandName:
                    if (Session["ADMaricosea"] != null)
                    {
                        DataTable kq = (DataTable)Session["ADMaricosea"];
                        RadGrid1.DataSource = kq;
                        RadGrid1.DataBind();
                    }
                    
                    break;
                case Telerik.Web.UI.RadGrid.SortCommandName:
                    if (Session["ADMaricosea"] != null)
                    {
                        DataTable kq = (DataTable)Session["ADMaricosea"];
                        RadGrid1.DataSource = kq;
                        RadGrid1.DataBind();
                    }
                    
                    break;
                case Telerik.Web.UI.RadGrid.GroupsCustomExpandCollapseCommandName:
                    if (Session["ADMaricosea"] != null)
                    {
                        DataTable kq = (DataTable)Session["ADMaricosea"];
                        RadGrid1.DataSource = kq;
                        RadGrid1.DataBind();
                    }

                    break;
                case Telerik.Web.UI.RadGrid.GroupsExpandAllCommandName:
                    if (Session["ADMaricosea"] != null)
                    {
                        DataTable kq = (DataTable)Session["ADMaricosea"];
                        RadGrid1.DataSource = kq;
                        RadGrid1.DataBind();
                    }

                    break;
                case Telerik.Web.UI.RadGrid.ExpandCollapseAllCommandName:
                    if (Session["ADMaricosea"] != null)
                    {
                        DataTable kq = (DataTable)Session["ADMaricosea"];
                        RadGrid1.DataSource = kq;
                        RadGrid1.DataBind();
                    }

                    break;
                case Telerik.Web.UI.RadGrid.ExpandCollapseCommandName:
                    if (Session["ADMaricosea"] != null)
                    {
                        DataTable kq = (DataTable)Session["ADMaricosea"];
                        RadGrid1.DataSource = kq;
                        RadGrid1.DataBind();
                    }

                    break;
            }
        }
        protected void btExcel_Click(object sender, EventArgs e)
        {
            try
            {
                RadGrid1.MasterTableView.Caption = "<span><br/><b>AD Export " + cls.Date2sDdMmYyy(DateTime.Now, "/") +  "</b></span>";
                RadGrid1.ExportSettings.ExportOnlyData = true;
                RadGrid1.ExportSettings.UseItemStyles = true;
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.FileName = "ADExport";
                RadGrid1.MasterTableView.GridLines = GridLines.None;
                RadGrid1.MasterTableView.ExportToExcel();
            }
            catch
            {

            }
        }

        protected void RadGrid1_GroupsChanging(object sender, Telerik.Web.UI.GridGroupsChangingEventArgs e)
        {
            if (Session["ADMaricosea"] != null)
            {
                DataTable kq = (DataTable)Session["ADMaricosea"];
                RadGrid1.DataSource = kq;
                RadGrid1.DataBind();
            }
        }

        protected void btUpdateStatus_Click(object sender, EventArgs e)
        {
           if( Session["ADMaricosea"]!= null)
            {
                 DataTable kq = (DataTable)Session["ADMaricosea"];
                DataRow[] filteredRows = kq.Select("Status = 'Stop'");
                foreach (DataRow dr in filteredRows)
                {
                    cls.bCapNhat(new string[] { "@username", "@active" }, new object[] { dr["Username"], false }, "sp_updatestatusUsers");

                }
                    
            }
        }
    }
}