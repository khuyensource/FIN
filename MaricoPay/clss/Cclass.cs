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
using Telerik.Web.UI;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using System.Net.Mail;



public class Cclass : System.Web.UI.Page
{
    clsObj Obj;
    clsSql Sql = new clsSql();
	public Cclass()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string CheckSum(string tablename)
    {
        string kq = GetString("[CheckSum]", new string[] { "@TableName" }, new object[] { tablename });
        return kq;
    }
    public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
    {
        DataTable dtReturn = new DataTable();

        // column names 
        PropertyInfo[] oProps = null;

        if (varlist == null) return dtReturn;

        foreach (T rec in varlist)
        {
            // Use reflection to get property names, to create table, Only first time, others 

            if (oProps == null)
            {
                oProps = ((Type)rec.GetType()).GetProperties();
                foreach (PropertyInfo pi in oProps)
                {
                    Type colType = pi.PropertyType;

                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                    == typeof(Nullable<>)))
                    {
                        colType = colType.GetGenericArguments()[0];
                    }

                    dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                }
            }

            DataRow dr = dtReturn.NewRow();

            foreach (PropertyInfo pi in oProps)
            {
                dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                (rec, null);
            }

            dtReturn.Rows.Add(dr);
        }
        return dtReturn;
    }
    public void Radcombo_ItemsRequested(RadComboBox rad,string tenstore,string bien,object giatri,string value,string text)
    {
        Obj = new clsObj();
        Obj.Parameter = new string[]{bien};
        Obj.ValueParameter = new object[] {giatri };
        Obj.SpName = tenstore;
        Sql.fGetData(Obj);
        rad.DataSource = Obj.Dt;
        rad.DataValueField = value;
        rad.DataTextField = text;
        rad.DataBind();
    }
    public void Radcombo_ItemsRequested(RadComboBox rad, string tenstore, string[] bien, object[] giatri, string value, string text)
    {
        Obj = new clsObj();
        Obj.Parameter = bien;
        Obj.ValueParameter = giatri;
        Obj.SpName = tenstore;
        Sql.fGetData(Obj);
        rad.DataSource = Obj.Dt;
        rad.DataValueField = value;
        rad.DataTextField = text;
        rad.DataBind();
    }
    
    /// <summary>
    /// Get datatable có 1 tham số
    /// </summary>
    /// <param name="tenstore"></param>
    /// <param name="bien"></param>
    /// <param name="giatri"></param>
    /// <returns></returns>
    public DataTable GetDataTable(string tenstore, string bien, object giatri)
    {
            //ClassLibrary lib = new ClassLibrary(connectionString);
            Obj = new clsObj();
           // DataTable dt = new DataTable();
            Obj.Parameter = new string[] { bien };
            Obj.ValueParameter = new object[] { giatri };
            Obj.SpName = tenstore;
            Sql.fGetData(Obj);
            return Obj.Dt;
    }
    public DataTable GetDataTable(string tenstore, string[] bien, object[] giatri)
    {
        //ClassLibrary lib = new ClassLibrary(connectionString);
        Obj = new clsObj();
       // DataTable dt = new DataTable();
        Obj.Parameter = bien;
        Obj.ValueParameter =  giatri;
        Obj.SpName = tenstore;
         Sql.fGetData(Obj);
        return Obj.Dt;
    }
    public string GetString(string tenstore, string[] bien, object[] giatri)
    {
        string kq = "";
        //ClassLibrary lib = new ClassLibrary(connectionString);
        Obj = new clsObj();
        // DataTable dt = new DataTable();
        Obj.Parameter = bien;
        Obj.ValueParameter = giatri;
        Obj.SpName = tenstore;
        Sql.fGetData(Obj);
        if (Obj.Dt.Rows.Count > 0)
        {
            if (cToString(Obj.Dt.Rows[0][0]) != "")
            {
                kq = cToString(Obj.Dt.Rows[0][0]);
            }
        }
        return kq;
    }
    public string GetString0(string tenstore, string[] bien, object[] giatri)
    {
        string kq = "0";
        //ClassLibrary lib = new ClassLibrary(connectionString);
        Obj = new clsObj();
        // DataTable dt = new DataTable();
        Obj.Parameter = bien;
        Obj.ValueParameter = giatri;
        Obj.SpName = tenstore;
        Sql.fGetData(Obj);
        if (Obj.Dt.Rows.Count > 0)
        {
            if (cToString(Obj.Dt.Rows[0][0]) != "")
            {
                kq = cToString(Obj.Dt.Rows[0][0]);
            }
        }
        return kq;
    }
    public string GetString(string tenstore)
    {
        string kq = "";
        //ClassLibrary lib = new ClassLibrary(connectionString);
        Obj = new clsObj();
        // DataTable dt = new DataTable();
        Obj.Parameter = new string[] { };
        Obj.ValueParameter = new object[] { };
        Obj.SpName = tenstore;
        Sql.fGetData(Obj);
        if (Obj.Dt.Rows.Count > 0)
        {
            if (cToString(Obj.Dt.Rows[0][0]) != "")
            {
                kq = cToString(Obj.Dt.Rows[0][0]);
            }
        }
        return kq;
    }
    public DataSet GetDataSet(string tenstore, string[] bien, object[] giatri)
    {
        //ClassLibrary lib = new ClassLibrary(connectionString);
        Obj = new clsObj();
      //  DataSet dt = new DataSet();
        Obj.Parameter = bien;
        Obj.ValueParameter = giatri;
        Obj.SpName = tenstore;
        Sql.fGetDataSet(Obj);
        return Obj.Ds;
    }
    public DataSet GetDataSet(string tenstore, string[] bien, object[] giatri, string connect)
    {
        //ClassLibrary lib = new ClassLibrary(connectionString);
        Obj = new clsObj();
        Obj.CnnString = ConfigurationManager.ConnectionStrings[connect].ToString();
     //   DataSet dt = new DataSet();
        Obj.Parameter = bien;
        Obj.ValueParameter = giatri;
        Obj.SpName = tenstore;
        Sql.fGetDataSet(Obj);
        return Obj.Ds;
    }
   
    public DataTable GetDataTable(string tenstore, string[] bien, object[] giatri,string connect)
    {
        //ClassLibrary lib = new ClassLibrary(connectionString);
        Obj = new clsObj();
        Obj.CnnString = ConfigurationManager.ConnectionStrings[connect].ToString();
      //  DataTable dt = new DataTable();
        Obj.Parameter = bien;
        Obj.ValueParameter = giatri;
        Obj.SpName = tenstore;
        Sql.fGetData(Obj);
        return Obj.Dt;
    }
    /// <summary>
    /// Get datatable không tham số
    /// </summary>
    /// <param name="tenstore"></param>
    /// <returns></returns>
    public DataTable GetDataTable(string tenstore)
    {
            //ClassLibrary lib = new ClassLibrary(connectionString);
            Obj = new clsObj();
          //  DataTable dt = new DataTable();

            Obj.Parameter = new string[] { };
            Obj.ValueParameter = new object[] { };
            Obj.SpName = tenstore;
            Sql.fGetData(Obj);
           return  Obj.Dt;
           
    }
    public DataTable GetDataTable(string tenstore,string connect)
    {
        //ClassLibrary lib = new ClassLibrary(connectionString);
        Obj = new clsObj();
        Obj.CnnString = ConfigurationManager.ConnectionStrings[connect].ToString();
       // DataTable dt = new DataTable();

        Obj.Parameter = new string[] { };
        Obj.ValueParameter = new object[] { };
        Obj.SpName = tenstore;
        Sql.fGetData(Obj);
        return Obj.Dt;

    }
    public DataTable GetDataTableQuery(string sqlquery)
    {
        //ClassLibrary lib = new ClassLibrary(connectionString);
        Obj = new clsObj();
      //  Obj.CnnString = ConfigurationManager.ConnectionStrings[connect].ToString();
        // DataTable dt = new DataTable();

        Obj.Parameter = new string[] { };
        Obj.ValueParameter = new object[] { };
        Obj.SpName = sqlquery;
        Sql.fGetData(Obj);
        return Obj.Dt;

    }
    
    /// <summary>
    /// Load dữ liệu lên Radgrid với dữ liệu từ 1 datatable
    /// </summary>
    /// <param name="rad"></param>
    /// <param name="tbl"></param>
    public void LoadRadGrid(RadGrid rad, DataTable tbl)
    {
        try
        {
            rad.DataSource = tbl;
            rad.DataBind();

        }
        catch
        {
            //XuatThongBaoLoi(ex);
        }
    }
    /// <summary>
    /// Load dữ liệu lên Radgrid với dữ liệu từ 1 store không tham số
    /// </summary>
    /// <param name="rad"></param>
    /// <param name="tenstore"></param>
    public void LoadRadGrid(RadGrid rad, string tenstore)
    {
        try
        {
            Obj = new clsObj();
           
            Obj.Parameter = new string[] { };
            Obj.ValueParameter = new object[] { };
            Obj.SpName = tenstore;
            Sql.fGetData(Obj);
           
            rad.DataSource = Obj.Dt;
            rad.DataBind();
        }
        catch
        {
            //XuatThongBaoLoi(ex);
        }
    }
    /// <summary>
    /// Load dữ liệu lên Radgrid với dữ liệu từ 1 store có tham số
    /// </summary>
    /// <param name="rad"></param>
    /// <param name="tenstore"></param>
    /// <param name="bien"></param>
    /// <param name="giatri"></param>
    public void LoadRadGrid(RadGrid rad, string tenstore, string[] bien, object[] giatri)
    {
        try
        {
            Obj = new clsObj();
           
            Obj.Parameter = bien;
            Obj.ValueParameter = giatri;
            Obj.SpName = tenstore;
            Sql.fGetData(Obj);
          
            rad.DataSource = Obj.Dt;
            rad.DataBind();
        }
        catch
        {
            //XuatThongBaoLoi(ex);
        }
    }
    public void LoadRadGrid(RadGrid rad, string tenstore, string[] bien, object[] giatri,string connect)
    {
        try
        {
            Obj = new clsObj();
            Obj.CnnString = ConfigurationManager.ConnectionStrings[connect].ToString();
            
            Obj.Parameter = bien;
            Obj.ValueParameter = giatri;
            Obj.SpName = tenstore;
            Sql.fGetData(Obj);
            rad.DataSource = Obj.Dt;
            rad.DataBind();
        }
        catch
        {
            //XuatThongBaoLoi(ex);
        }
    }
    public void LoadRadCbo(RadComboBox rad, string tenstore, string bien, object giatri, string cotcantim, string filter)
    {
        try
        {
            //ClassLibrary lib = new ClassLibrary(connectionString);
            Obj = new clsObj();
          //  DataTable dt = new DataTable();

            Obj.Parameter = new string[] { bien };
            Obj.ValueParameter = new object[] { giatri };
            Obj.SpName = tenstore;
            Sql.fGetData(Obj);
           
            if (filter != String.Empty)
            {
                string chuoi = cotcantim + " LIKE '%" + filter + "%'";
                DataView dv = Obj.Dt.DefaultView;

                dv.RowFilter = chuoi;
                rad.DataSource = dv;
                rad.DataBind();
            }
            else
            {

                rad.DataSource = Obj.Dt;
                rad.DataBind();
            }
        }
        catch
        {
            //XuatThongBaoLoi(ex);
        }
    }
   /// <summary>
   /// Load radcombo
   /// </summary>
   /// <param name="rad"></param>
   /// <param name="tenstore"></param>
   /// <param name="bien"></param>
   /// <param name="giatri"></param>
   /// <param name="cotcantim"></param>
   /// <param name="filter"></param>
   /// <param name="connect"></param>
    public void LoadRadCbo(RadComboBox rad, string tenstore, string[] bien, object[] giatri, string cotcantim, string filter,string connect)
    {
        try
        {
            //ClassLibrary lib = new ClassLibrary(connectionString);
            Obj = new clsObj();
            Obj.CnnString = ConfigurationManager.ConnectionStrings[connect].ToString();
           // DataTable dt = new DataTable();

            Obj.Parameter = bien;
            Obj.ValueParameter = giatri;
            Obj.SpName = tenstore;
            Sql.fGetData(Obj);
           
            if (filter != String.Empty)
            {
                string chuoi = cotcantim + " LIKE '%" + filter + "%'";
                DataView dv = Obj.Dt.DefaultView;

                dv.RowFilter = chuoi;
                rad.DataSource = dv;
                rad.DataBind();
            }
            else
            {

                rad.DataSource = Obj.Dt;
                rad.DataBind();
            }
        }
        catch
        {
            //XuatThongBaoLoi(ex);
        }
    }
    /// <summary>
    /// Load Radcombo với datatable
    /// </summary>
    /// <param name="rad"></param>
    /// <param name="tbl"></param>
    /// <param name="cotcantim"></param>
    /// <param name="filter"></param>
    public void LoadRadCbo(RadComboBox rad, DataTable tbl/* string tenstore*/, string cotcantim, string filter)
    {
        try
        {
            if (filter != String.Empty)
            {
                string chuoi = cotcantim + " LIKE '%" + filter + "%'";
                DataView dv = tbl.DefaultView;
                dv.RowFilter = chuoi;
                rad.DataSource = dv;
                rad.DataBind();
            }
            else
            {
                rad.DataSource = tbl;
                rad.DataBind();
            }
        }
        catch
        {
            //XuatThongBaoLoi(ex);
        }
    }
    /// <summary>
    /// Load dữ liệu lên Radcombo với store không tham số
    /// </summary>
    /// <param name="rad"></param>
    /// <param name="tenstore"></param>
    /// <param name="cotcantim"></param>
    /// <param name="filter"></param>
    public void LoadRadCbo(RadComboBox rad, string tenstore, string cotcantim, string filter,string value,string text)
    {
        try
        {
            //ClassLibrary lib = new ClassLibrary(connectionString);
            Obj = new clsObj();
           // DataTable dt = new DataTable();
            Obj.Parameter = new string[] { };
            Obj.ValueParameter = new object[] { };
            Obj.SpName = tenstore;
            Sql.fGetData(Obj);
           // dt = Obj.Dt;
            if (filter != String.Empty)
            {
                string chuoi = cotcantim + " LIKE '%" + filter + "%'";
                DataView dv = Obj.Dt.DefaultView;
                dv.RowFilter = chuoi;
                rad.DataSource = dv;
                rad.DataValueField = value;
                rad.DataTextField=text;
                rad.DataBind();
            }
            else
            {
                rad.DataSource = Obj.Dt;
                rad.DataValueField = value;
                rad.DataTextField = text;
                rad.DataBind();
            }
        }
        catch
        {
            //XuatThongBaoLoi(ex);
        }
    }
    /// <summary>
    /// Load dữ liệu lên Radcombo với store không tham số voi chuoi ket noi
    /// </summary>
    /// <param name="rad"></param>
    /// <param name="tenstore"></param>
    /// <param name="cotcantim"></param>
    /// <param name="filter"></param>
    /// <param name="value"></param>
    /// <param name="text"></param>
    /// <param name="connect"></param>
    public void LoadRadCbo(RadComboBox rad, string tenstore, string cotcantim, string filter, string value, string text,string connect)
    {
        try
        {
            //ClassLibrary lib = new ClassLibrary(connectionString);
            Obj = new clsObj();
            Obj.CnnString = ConfigurationManager.ConnectionStrings[connect].ToString();
          //  DataTable dt = new DataTable();
            Obj.Parameter = new string[] { };
            Obj.ValueParameter = new object[] { };
            Obj.SpName = tenstore;
            Sql.fGetData(Obj);
            
            if (filter != String.Empty)
            {
                string chuoi = cotcantim + " LIKE '%" + filter + "%'";
                DataView dv = Obj.Dt.DefaultView;
                dv.RowFilter = chuoi;
                rad.DataSource = dv;
                rad.DataValueField = value;
                rad.DataTextField = text;
                rad.DataBind();
            }
            else
            {
                rad.DataSource = Obj.Dt;
                rad.DataValueField = value;
                rad.DataTextField = text;
                rad.DataBind();
            }
        }
        catch
        {
            //XuatThongBaoLoi(ex);
        }
    }
    /// <summary>
    /// Load dropdownlist có tham số
    /// </summary>
    /// <param name="drop"></param>
    /// <param name="tenstore"></param>
    /// <param name="bien"></param>
    /// <param name="giatri"></param>
    /// <param name="value"></param>
    /// <param name="display"></param>
    public void Dropdownlist_Load(DropDownList drop, string tenstore, string[] bien, object[] giatri,string value,string display)
    {
        Obj = new clsObj();
        Obj.Parameter =  bien ;
        Obj.ValueParameter =  giatri;
        Obj.SpName = tenstore;
        Sql.fGetData(Obj);
        drop.DataSource = Obj.Dt;
        drop.DataValueField = value;
        drop.DataTextField = display;
        drop.DataBind();
    }
    public void Dropdownlist_Load(DropDownList drop, string tenstore, string[] bien, object[] giatri, string value, string display, string connect)
    {
        Obj = new clsObj();
        Obj.CnnString = ConfigurationManager.ConnectionStrings[connect].ToString();
        Obj.Parameter = bien;
        Obj.ValueParameter = giatri;
        Obj.SpName = tenstore;
        Sql.fGetData(Obj);
        drop.DataSource = Obj.Dt;
        drop.DataValueField = value;
        drop.DataTextField = display;
        drop.DataBind();
    }
   
    /// <summary>
    /// LOad Dropdownlist không tham số
    /// </summary>
    /// <param name="drop"></param>
    /// <param name="tenstore"></param>
    /// <param name="value"></param>
    /// <param name="display"></param>
    /// 

    public void Dropdownlist_Load(DropDownList drop, string tenstore, string value, string display)
    {
        Obj = new clsObj();
        Obj.Parameter = new string[]{};
        Obj.ValueParameter = new object[] { };
        Obj.SpName = tenstore;
        Sql.fGetData(Obj);
        drop.DataSource = Obj.Dt;
        drop.DataValueField = value;
        drop.DataTextField = display;
        drop.DataBind();
    }
    public void Dropdownlist_Load(DropDownList drop, string tenstore, string value, string display, string connect)
    {
        Obj = new clsObj();
        Obj.CnnString = ConfigurationManager.ConnectionStrings[connect].ToString();
        Obj.Parameter = new string[] { };
        Obj.ValueParameter = new object[] { };
        Obj.SpName = tenstore;
        Sql.fGetData(Obj);
        drop.DataSource = Obj.Dt;
        drop.DataValueField = value;
        drop.DataTextField = display;
        drop.DataBind();
    }
   
    /// <summary>
    /// Kiểm tra dữ liệu có tồn tại?
    /// </summary>
    /// <param name="giatri"></param>
    /// <param name="sp_Trigger"></param>
    /// <returns></returns>
    public bool KTCB(string giatri, string sp_Trigger)
    {
        Obj = new clsObj();
        Obj.Parameter = new string[] { "@ma" };
        Obj.ValueParameter = new object[] { giatri };
        Obj.SpName = sp_Trigger;
       // DataTable tableName = new DataTable();
        Sql.fGetData(Obj);
       // tableName = Obj.Dt;
        if (int.Parse(Obj.Dt.Rows[0][0].ToString()) > 0)
            return true;
        else
            return false;
    }
    /// <summary>
    /// Kiem tra vipno co duoc su dung chua; false: da duoc su dung; true: chua duoc su dung
    /// </summary>
    /// <param name="vipno"></param>
    /// <returns></returns>
    public bool CheckVipNoIsUsed(string customerkey,string vipno)
    {
       DataTable tblvip= GetDataTable("sp_CheckVipNo",new string[]{"@Customerkey", "@VipNo"},new object[]{customerkey, vipno});
       if (tblvip.Rows.Count > 0)
           return false;
       else
           return true;
    }
    /// <summary>
    /// Neu count>0 thi true nguoc lai false
    /// </summary>
    /// <param name="bien"></param>
    /// <param name="gtri"></param>
    /// <param name="sp_Trigger"></param>
    /// <returns></returns>
    public bool CheckExists(string[] bien, object[] gtri, string sp_Trigger)
    {
      
       // DataTable tableName = new DataTable();
      

        Obj = new clsObj();
        Obj.Parameter = bien;
        Obj.ValueParameter = gtri;
        Obj.SpName = sp_Trigger;
      
        Sql.fGetData(Obj);
      //  tableName = Obj.Dt;
        if (Obj.Dt.Rows.Count > 0)
        {

            if (cToInt(Obj.Dt.Rows[0][0]) > 0)
                return true;
            else
                return false;
        }
        else
            return false;
    }
    /// <summary>
    /// tra ve gia tri dong 0 cot 0
    /// </summary>
    /// <param name="bien"></param>
    /// <param name="gtri"></param>
    /// <param name="sp_Trigger"></param>
    /// <returns></returns>
    public int CountItem(string[] bien, object[] gtri, string sp_Trigger)
    {
        Obj = new clsObj();
        Obj.Parameter = bien;
        Obj.ValueParameter = gtri;
        Obj.SpName = sp_Trigger;
       // DataTable tableName = new DataTable();
        Sql.fGetData(Obj);
       // tableName = Obj.Dt;
        return int.Parse(Obj.Dt.Rows[0][0].ToString());

    }
    public bool InsenttblTam(string TenBang, string CotPrimary, string GiaTriPrimary)
    {
        Obj = new clsObj();
        Obj.Parameter = new string[] { "@TenBang", "@CotPrimary", "@GiaTriPrimary" };
        Obj.ValueParameter = new object[] { TenBang, CotPrimary, GiaTriPrimary };
        Obj.SpName = "sp_InserttblTam";
       // DataTable tableName = new DataTable();
        Sql.fNonGetData(Obj);
       // tableName = Obj.Dt;
        if (Obj.KetQua>=1)
            return true;
        else
            return false;
    }
    public bool InsenttblLog(string TenBang, string CotPrimary, string GiaTriPrimary, string user, int trangthai)
    {
        Obj = new clsObj();
        Obj.Parameter = new string[] { "@TenBang", "@CotPrimary", "@GiaTriPrimary", "@US", "@TrangThai" };
        Obj.ValueParameter =  new object[] { TenBang, CotPrimary, GiaTriPrimary, user, trangthai };
        Obj.SpName = "sp_Insert_tblLog";
        // DataTable tableName = new DataTable();
        Sql.fNonGetData(Obj);
        // tableName = Obj.Dt;
        if (Obj.KetQua >= 1)
            return true;
        else
            return false;
    }
    //ExecuteQuery_Store
    public string Them(string[] tenbien, object[] giatribien, string sp_Insert)
    {

        Obj = new clsObj();
        Obj.Parameter = tenbien;
        Obj.ValueParameter = giatribien;
        Obj.SpName = sp_Insert;
        // DataTable tableName = new DataTable();
        Sql.fNonGetData(Obj);
        // tableName = Obj.Dt;
        if (Obj.KetQua < 1)
            return "<font color='red'>Chưa thêm được</font>";
        else
            return "";

    }
    public bool bThem(string[] tenbien, object[] giatribien, string sp_Insert)
    {

        Obj = new clsObj();
        Obj.Parameter = tenbien;
        Obj.ValueParameter = giatribien;
        Obj.SpName = sp_Insert;
        // DataTable tableName = new DataTable();
        Sql.fNonGetData(Obj);
        // tableName = Obj.Dt;
        if (Obj.KetQua < 1)
            return false;
        else
            return true;

    }
    
    public bool bThem(string[] tenbien, object[] giatribien, string sp_Insert,string connect)
    {

        Obj = new clsObj();
        Obj.CnnString = ConfigurationManager.ConnectionStrings[connect].ToString();
        Obj.Parameter = tenbien;
        Obj.ValueParameter = giatribien;
        Obj.SpName = sp_Insert;
        // DataTable tableName = new DataTable();
        Sql.fNonGetData(Obj);
        // tableName = Obj.Dt;
        if (Obj.KetQua < 1)
            return false;
        else
            return true;

    }
    public string CapNhat(string[] tenbien, object[] giatribien, string sp_Update)
    {

        Obj = new clsObj();
        Obj.Parameter = tenbien;
        Obj.ValueParameter = giatribien;
        Obj.SpName = sp_Update;
        
        Sql.fNonGetData(Obj);

        if (Obj.KetQua < 1)
        {
            return "<font color='red'> Cập nhật thất bại, vui lòng thử lại</font>";
        }
        else
        {
            return "<font color='blue'> Cập nhật thành công  </font>";
        }

    }
    public bool bCapNhat(string[] tenbien, object[] giatribien, string sp_Update)
    {
        bool kq = false;
        Obj = new clsObj();
        Obj.Parameter = tenbien;
        Obj.ValueParameter = giatribien;
        Obj.SpName = sp_Update;

        Sql.fNonGetData(Obj);

        if (Obj.KetQua < 1)
        {
            kq= false;
        }
        else
        {
            kq= true;
        }
        return kq;
    }
    public string CapNhat(string[] tenbien, object[] giatribien, string sp_Update,string connect)
    {

        Obj = new clsObj();
        Obj.CnnString = ConfigurationManager.ConnectionStrings[connect].ToString();
        Obj.Parameter = tenbien;
        Obj.ValueParameter = giatribien;
        Obj.SpName = sp_Update;

        Sql.fNonGetData(Obj);

        if (Obj.KetQua < 1)
        {
            return "<font color='red'> Cập nhật thất bại, vui lòng thử lại</font>";
        }
        else
        {
            return "<font color='blue'> Cập nhật thành công  </font>";
        }

    }
   
    public bool bCapNhat(string[] tenbien, object[] giatribien, string sp_Update,string connect)
    {

        Obj = new clsObj();
        Obj.CnnString = ConfigurationManager.ConnectionStrings[connect].ToString();
        Obj.Parameter = tenbien;
        Obj.ValueParameter = giatribien;
        Obj.SpName = sp_Update;

        Sql.fNonGetData(Obj);

        if (Obj.KetQua < 1)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    public string Xoa(string[] tenbien, object[] giatribien, string sp_delete)
    {

        Obj = new clsObj();
        Obj.Parameter = tenbien;
        Obj.ValueParameter = giatribien;
        Obj.SpName = sp_delete;
        Sql.fNonGetData(Obj);
        if (Obj.KetQua < 1)
        {
            return "<font color='red'> Xóa thất bại, vui lòng thử lại </font>";
        }
        else
        {
            return "<font color='blue'> Xóa thành công</font>";
        }
         

    }
    public bool bXoa(string[] tenbien, object[] giatribien, string sp_delete)
    {

        Obj = new clsObj();
        Obj.Parameter = tenbien;
        Obj.ValueParameter = giatribien;
        Obj.SpName = sp_delete;
        Sql.fNonGetData(Obj);
        if (Obj.KetQua < 1)
        {
            return false;
        }
        else
        {
            return true;
        }


    }
    /// <summary>
    /// Thực thi SQL va tra ve ket qua phu hop
    /// </summary>
    /// <param name="tenstore"></param>
    /// <param name="bien"></param>
    /// <param name="giatri"></param>
    /// <returns></returns>
    public object ExcuteSQL(string tenstore, string[] bien, object[] giatri)
    {
        object kq = "";
        //ClassLibrary lib = new ClassLibrary(connectionString);
        Obj = new clsObj();
        // DataTable dt = new DataTable();
        Obj.Parameter = bien;
        Obj.ValueParameter = giatri;
        Obj.SpName = tenstore;
        Sql.fGetData(Obj);
        switch (Obj.MaLoi)
        {
            case 1:
                kq = "Lỗi kết nối";
                break;
            case 2:
                kq = "Lỗi cú pháp";
                break;
            default:
                if (Obj.Dt.Rows.Count > 0)
                {
                    kq = Obj.Dt;
                }
                else
                {
                    kq = "Thực hiện thành công";
                }
                break;
        }


        return kq;
    }
    public bool Search_DataTable(DataTable dt, string searchcolumn, string text)
    {
        int i = 0;
        try
        {
            while (dt.Rows[i][searchcolumn].ToString().ToLower() != text.ToLower() && i < dt.Rows.Count)
                i++;
            //return i;
            if (i < dt.Rows.Count)
            {

                return true;
            }
            else
                return false;
        }
        catch { return false; }
    }
    public int Search_DataTablei(DataTable dt, string searchcolumn, string text)
    {
        int i = 0;
        try
        {
            while (dt.Rows[i][searchcolumn].ToString().ToLower() != text.ToLower() && i < dt.Rows.Count)
                i++;
            if (i < dt.Rows.Count)
            {

                return i;
            }
            else
            {
                return -1;
            }
            
        }
        catch { return -1; }
    }
    public bool Search_DataTable(DataTable dt, string searchcolumn1, string searchcolumn2, string text1, string text2)
    {
        int i = 0;
        try
        {
            while ((dt.Rows[i][searchcolumn1].ToString().ToLower() != text1.ToLower()) || (dt.Rows[i][searchcolumn2].ToString().ToLower() != text2.ToLower()) && i < dt.Rows.Count)
                i++;
            //return i;
            if (i < dt.Rows.Count)
            {

                return true;
            }
            else
                return false;
        }
        catch { return false; }
    }
    public string cToString(object str)
    {
        string kq = "";
        try
        {
            kq = str.ToString().Replace("&nbsp;", "");
        }
        catch
        {
            kq = "";
        }
        return kq;
    }
    /// <summary>
    /// Chuyen sang chuoi neu sai tra ve chuoi kqsai
    /// </summary>
    /// <param name="str"></param>
    /// <param name="kqsai"></param>
    /// <returns></returns>
    public string cToString(object str,string kqsai)
    {
        string kq = "";
        try
        {
            kq = str.ToString();
            if (kq.Trim() == "")
            {
                kq = kqsai;
            }
        }
        catch
        {
            kq = kqsai;
        }
        return kq;
    }
    public string cToString0(object str)
    {
        string kq = "0";
        try
        {
            kq = str.ToString().Replace(",","");
            
        }
        catch
        {
            kq = "0";
        }
        return kq;
    }
    public double cToNum(object no)
    {
        double kq = 0;
        try
        {
            kq = Convert.ToDouble(no);
        }
        catch
        {
            kq = 0;
        }
        return kq;
    }
    public float cToFloat(object no)
    {
        float kq = 0;
        try
        {
            kq = float.Parse(cToString0(no).Replace(",", ""));
        }
        catch
        {
            kq = 0;
        }
        return kq;
    }
    //public double cToNum_Null(object no)
    //{
    //    double kq = 0;
    //    try
    //    {
    //        kq = Convert.ToDouble(no);
    //    }
    //    catch
    //    {
    //       return null;
    //    }
    //    return kq;
    //}
    public int cToInt(object no)
    {
        int kq = 0;
        try
        {
            kq = Convert.ToInt32(cToString0(no).Replace(",", ""));
        }
        catch
        {
            kq = 0;
        }
        return kq;
    }
    public bool cToBool(object no)
    {
        
        bool kq =false;
        if (no == null)
        {
            no = 0;
        }
        else
        {
            //try
            //{
                if (no.ToString().ToLower() == "true" || no.ToString().ToLower() == "1")
                {
                    no = 1;
                }
            //}
            //catch
            //{
            //    no = 0;
            //}
        }
        try
        {
            kq = Convert.ToBoolean(no);
        }
        catch
        {
            kq = false;
        }
        return kq;
    }
    public double cToDouble(object no)
    {
        double kq = 0;
        try
        {
            kq = Convert.ToDouble(cToString(no).Replace(",",""));
        }
        catch
        {
            kq = 0;
        }
        return kq;
    }
    public decimal cToDecimal(object no)
    {
        decimal kq = 0;
        try
        {
            kq = Convert.ToDecimal(cToString(no).Replace(",", ""));
        }
        catch
        {
            kq = 0;
        }
        return kq;
    }
    public DateTime? cToDateTime(object obj)
    {
        DateTime? kq = null;
        try
        {
            kq = Convert.ToDateTime(obj);
        }
        catch
        {
            kq = null;
        }
        return kq;
    }
    public string FormatNumber(object value)
    {
        try
        {
            Decimal svalue = Convert.ToDecimal(value);
            if (svalue == 0)
            {
                return "0";
            }
            else
            {
                return svalue.ToString("###,###,###.##");

            }
        }
        catch
        {
            return "0";
        }
    }
    public DateTime sDdMmYyy2Date(string ddMMYyyy)
    {
        DateTime kq;
        string[]ngay=ddMMYyyy.Split('/');

        kq = new DateTime(cToInt(ngay.GetValue(2)), cToInt(ngay.GetValue(1)), cToInt(ngay.GetValue(0)));
       
        return kq;
    }
  
    public string Date2sDdMmYyy(DateTime date,string kytuphancach)
    {
        //string kq;
        int iday = date.Day;
        int ithang = date.Month;
        int inam = date.Year;
        string sngay = cToString(iday).Length == 1 ? "0" + cToString(iday) : cToString(iday);
        string sthang = cToString(ithang).Length == 1 ? "0" + cToString(ithang) : cToString(ithang);
       
        return sngay+kytuphancach+sthang+kytuphancach+cToString(inam);
    }
    public string Date2sYyyyMmDd(DateTime date, string kytuphancach)
    {
        //string kq;
        int iday = date.Day;
        int ithang = date.Month;
        int inam = date.Year;
        string sngay = cToString(iday).Length == 1 ? "0" + cToString(iday) : cToString(iday);
        string sthang = cToString(ithang).Length == 1 ? "0" + cToString(ithang) : cToString(ithang);

        return cToString(inam) + kytuphancach + sthang + kytuphancach + sngay;
    }
    public string Date2YyyyMm(DateTime date)
    {
        //string kq;
     //   int iday = date.Day;
        int ithang = date.Month;
        int inam = date.Year;
      //  string sngay = cToString(iday).Length == 1 ? "0" + cToString(iday) : cToString(iday);
        string sthang = cToString(ithang).Length == 1 ? "0" + cToString(ithang) : cToString(ithang);

        return cToString(inam) + sthang;
    }
    public string DDSurMmSurYYYY2YyyyMm(string date)
    {
        return date.Substring(6, 4) + date.Substring(3, 2);
    }
    /// <summary>
    /// True: được phép xem báo cáo tháng đó
    /// False: Không được phép xem báo cáo tháng đó
    /// </summary>
    /// <param name="DateDDSurMmSurYYYY"></param>
    /// <returns></returns>
    public bool ViewReport(string DateDDSurMmSurYYYY)
    {
       // string YyyyMmNow = Date2YyyyMm(DateTime.Now);
        int sothang = cToInt(Session["Report"]);
        if (sothang == 0)
            return true;
       int YyyyMmKhaDung=cToInt(Date2YyyyMm(DateTime.Now.AddMonths(-sothang)));
       int ThangDangXem = cToInt(DDSurMmSurYYYY2YyyyMm(DateDDSurMmSurYYYY));
       if (ThangDangXem >= YyyyMmKhaDung)
           return true;
       else
           return false;
    }
    public bool ViewReport(DateTime Date)
    {
        // string YyyyMmNow = Date2YyyyMm(DateTime.Now);
        int sothang = cToInt(Session["Report"]);
        if (sothang == 0)
            return true;
        int YyyyMmKhaDung = cToInt(Date2YyyyMm(DateTime.Now.AddMonths(-sothang)));
        int ThangDangXem = cToInt(Date2YyyyMm(Date));
        if (ThangDangXem >= YyyyMmKhaDung)
            return true;
        else
            return false;
    }
    public bool ViewReportMMYYYY(string MmYyyy)
    {
        // string YyyyMmNow = Date2YyyyMm(DateTime.Now);
        int sothang = cToInt(Session["Report"]);
        if (sothang == 0)
            return true;
        int YyyyMmKhaDung = cToInt(Date2YyyyMm(DateTime.Now.AddMonths(-sothang)));
        int ThangDangXem = cToInt(MmYyyy.Substring(2) + MmYyyy.Substring(0,2));
        if (ThangDangXem >= YyyyMmKhaDung)
            return true;
        else
            return false;
    }
    public bool ViewReportYYYYMM(string YyyyMm)
    {
        // string YyyyMmNow = Date2YyyyMm(DateTime.Now);
        int sothang = cToInt(Session["Report"]);
        if (sothang == 0)
            return true;
        int YyyyMmKhaDung = cToInt(Date2YyyyMm(DateTime.Now.AddMonths(-sothang)));
        int ThangDangXem = cToInt(YyyyMm);
        if (ThangDangXem >= YyyyMmKhaDung)
            return true;
        else
            return false;
    }
    public string DdSurMmSurYyyy2sYyyyMmDd(string DdSurMmSurYyyy)
    {
        //string kq;
        string day = DdSurMmSurYyyy.Substring(0,2);
        string thang = DdSurMmSurYyyy.Substring(3, 2);
        string nam = DdSurMmSurYyyy.Substring(6, 4);


        return nam + thang + day;
    }
    public string NextYyyyMm(string YyyyMm)
    {
        string kq = YyyyMm;
        int y = cToInt(YyyyMm.Substring(0, 4));
        int m = cToInt(YyyyMm.Substring(4, 2));
        if (m < 12)
        {
            m++;
            if (m < 10)
            {
                kq = cToString0(y) + "0" + cToString0(m);
            }
            else
            {
                kq = cToString0(y) + cToString0(m);
            }
        }
        else
        {
            if (m == 12)
            {
                //m = 1;

                kq = cToString0(y + 1) + "01";

            }
        }
        return kq;
    }
    public string PrevYyyyMm(string YyyyMm)
    {
        string kq = YyyyMm;
        int y = cToInt(YyyyMm.Substring(0, 4));
        int m = cToInt(YyyyMm.Substring(4, 2));
        if (m != 1)
        {
            m--;
            if (m < 10)
            {
                kq = cToString0(y) + "0" + cToString0(m);
            }
            else
            {
                kq = cToString0(y) + cToString0(m);
            }
        }
        else
        {

            //m = 1;

            kq = cToString0(y - 1) + "12";


        }
        return kq;
    }
    public string MmSurYyyy(string yyyymm)
    {

        return yyyymm.Substring(4, 2) + "/" + yyyymm.Substring(0, 4);

    }
    public bool IsShowRoom(object sitecode)
    {
       DataTable tbl= GetDataTable("IsShowroom", "@sitecode", sitecode);
       if (tbl.Rows.Count > 0)
           return true;
       else
           return false;
    }
   
    /// <summary>
    /// insert log; statuskey: login, logout, resetpass, changepass
    /// active: 1 thanh cong, 0 that bai
    /// </summary>
    /// <param name="email"></param>
    /// <param name="statuskey"></param>
    /// <param name="active"></param>
    public void insertLog(string email, string statuskey, bool active)
    {
        clsObj Obj;
        clsSql Sql = new clsSql();
        Obj = new clsObj();
        Obj.Parameter = new string[] { "@user", "@ipadd", "@LanIP", "@computername", "@userloginwindow", "@statuskey", "@active" };

        string userloginWindow;
        // Get UserHostAddress property.

        HttpRequest currentRequest = HttpContext.Current.Request;
        string ipAddress = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (ipAddress == null || ipAddress.ToLower() == "unknown")
            ipAddress = currentRequest.ServerVariables["REMOTE_ADDR"];

        userloginWindow = System.Environment.UserName;
        string LanIP = GetLanIPAddress();
        // ip=HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        string com = System.Environment.MachineName;//.Net.Dns.GetHostName();
        Obj.ValueParameter = new object[] { email, ipAddress, LanIP, com, userloginWindow, statuskey, active };//dag nhap thanh cong
        Obj.SpName = "sp_insertLog";
        Sql.fNonGetData(Obj);
    }
    //Get Lan Connected IP address method
    public string GetLanIPAddress()
    {
        //Get the Host Name
        string stringHostName = System.Net.Dns.GetHostName();
        //Get The Ip Host Entry
        System.Net.IPHostEntry ipHostEntries = System.Net.Dns.GetHostEntry(stringHostName);
        //Get The Ip Address From The Ip Host Entry Address List
        System.Net.IPAddress[] arrIpAddress = ipHostEntries.AddressList;
        return arrIpAddress[arrIpAddress.Length - 1].ToString();
    }
    /// <summary>
    /// true: ma vach hop le; false: ma vach ko hop le
    /// </summary>
    /// <param name="item"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public string checkMaVach(string itemkey, string mavach,string sitecode,string Batch)
    {
        string kq = "Mã code không đúng khai báo ở kho DC";
        DataTable tblcodec = GetDataTable("sp_CheckCode", new string[] { "@itemkey", "@code", "@sitecode", "@Batch" }, new object[] { itemkey, mavach, sitecode, Batch });
        if (tblcodec.Rows.Count > 0)
        {
            kq = cToString(tblcodec.Rows[0][0]);
        
        }
        return kq;
    }
    public string checkMaVachDoiTra(string itemkey, string mavach, string sitecode, string Batch)
    {
        string kq = "Mã code không đúng khai báo ở kho DC";
        DataTable tblcodec = GetDataTable("sp_CheckCodeNhapDoi", new string[] { "@itemkey", "@code", "@sitecode", "@Batch" }, new object[] { itemkey, mavach, sitecode, Batch });
        if (tblcodec.Rows.Count > 0)
        {
            kq = cToString(tblcodec.Rows[0][0]);

        }
        return kq;
    }
    public double GetDS3ThangKhachHang(string makhachhang,DateTime ngayhoadon)
    {
        DataTable dt = GetDataTable("sp_getDoanhSo3Thang", new string[] { "@customerkey", "@ngayhoadon" }, new object[] { makhachhang, ngayhoadon });
        if (dt.Rows.Count > 0)
            return cToDouble(dt.Rows[0][0]);
        else
            return 0;

    }
    /// <summary>
    /// Ham lam tron len
    /// </summary>
    /// <param name="value"></param>
    /// <param name="digist"></param>
    /// <returns></returns>
    public double RoundUp(double value, int digist)
    {
        double tmp = Math.Round(value, digist);
        if (value > tmp)
        {
            double n = Math.Pow(10, -digist);
            tmp = Math.Round(tmp + n, digist);
        }
        return tmp;
    }
    public string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }
    public double Return1(object so)
    {
        if (cToDouble(so) == 0)
            return 1;
        else
            return cToDouble(so);
    }
    public string FixBatch(object batch)
    {
        string kq = "";
        kq = cToString(batch).Trim().ToLower();
        kq = kq.Replace(" ", "");
        kq = kq.Replace(",", "");
        kq = kq.Replace(":", "");
        kq = kq.Replace(";", "");
        kq = kq.Replace("&", "");
        kq = kq.Replace("amp", "");
        kq = kq.Replace("nbsp", "");
        kq = kq.Replace("h", "");
        kq = kq.Replace("v", "");
        kq = kq.Replace("d", "");
        kq = kq.Replace("l", "");
        kq = kq.Replace("a", "");
        int lg = kq.Length;
        if (lg > 6)
        {
            int vt = lg - 6;
            kq = kq.Substring(vt);
        }
        else
        {
            if (lg == 5)
            {
                kq = "0"+kq;
            }
        }
        return kq;
    }
    //public string CreateEan13(string countrycode,string manufacturercode,string productcode)
    //{
    //    Ean13Barcode2005.Ean13 ean13 = null;

    //    ean13 = new Ean13Barcode2005.Ean13(countrycode, manufacturercode, productcode);
    //    System.Text.StringBuilder sbTemp = new System.Text.StringBuilder();

    //    sbTemp.AppendFormat("{0}{1}{2}{3}",
    //        ean13.CountryCode,
    //        ean13.ManufacturerCode,
    //        ean13.ProductCode,
    //        ean13.ChecksumDigit);
    //    string sTemp = sbTemp.ToString();
    //    //if (Search_DataTable(dt, "CardCode", sTemp) == true)
    //    //{
    //    //    string tanglen = cToString0(cToDouble(productcode)+1);
    //    //    string kq;
    //    //    switch (tanglen.Length)
    //    //    {
    //    //        case 1:
    //    //            kq = "0000" + tanglen;
    //    //            break;
    //    //        case 2:
    //    //            kq = "000" + tanglen;
    //    //            break;
    //    //        case 3:
    //    //            kq = "00" + tanglen;
    //    //            break;
    //    //        case 4:
    //    //            kq = "0" + tanglen;
    //    //            break;
    //    //        default:
    //    //            kq = tanglen;
    //    //            break;
    //    //    }
            
    //    //}
    //    return sTemp;
       
		
    //}
    public string getExtention(string filename)
    {
        string kq="";
        int vt1 = filename.LastIndexOf(".");
       // int vtcanlay = vt1;
        int len = filename.Length;
        if (vt1 >= 0)
        {
            kq = filename.Substring(vt1, len - vt1);
        }
        return kq.ToLower();
    }
    public string getFileName(string bath)
    {
        string kq = "";
        kq=System.IO.Path.GetFileName(bath);
        //int vt1 = bath.LastIndexOf("\\");
        //// int vtcanlay = vt1;
        //int len = bath.Length;
        //if (vt1 >= 0)
        //{
        //    kq = bath.Substring(vt1, len - vt1);
        //}
        return kq.ToLower();
    }
    internal static decimal RoundFactor(int places)
    {
        decimal factor = 1m;

        if (places < 0)
        {
            places = -places;
            for (int i = 0; i < places; i++)
                factor /= 10m;
        }

        else
        {
            for (int i = 0; i < places; i++)
                factor *= 10m;
        }

        return factor;
    }
    public decimal RoundUp(decimal number, int places)
    {
        decimal factor = RoundFactor(places);
        number *= factor;
        number = Math.Ceiling(number);
        number /= factor;
        return number;
    }

    public decimal RoundDown(decimal number, int places)
    {
        decimal factor = RoundFactor(places);
        number *= factor;
        number = Math.Floor(number);
        number /= factor;
        return number;
    }
    public int MyRound(double number)
    {
        int kq;
        int phannguyen = (int)number;
        double thapphan = number - phannguyen;
        if (thapphan >= 0.7)
            kq = (int)Math.Round(number, 0);
        else
            kq = phannguyen;
        return kq;
    }
    public bool SendMailASP(string sto, string cc, string sub, string content)
    {
        bool kq = false;//
        try
        {
            string from = ConfigurationManager.AppSettings["fromemail"].ToString();
            string usemail = ConfigurationManager.AppSettings["useremail"].ToString();
            string smtpserver = ConfigurationManager.AppSettings["smtp"].ToString();
            string smtpport = ConfigurationManager.AppSettings["smtpport"].ToString();
            string ssl = ConfigurationManager.AppSettings["ssl"].ToString();
            string pass = ConfigurationManager.AppSettings["pass"].ToString();
            //SmtpClient smtpclient = new SmtpClient();
            //System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
            // MailAddress mailAddress = new MailAddress(radcombowho.SelectedValue, radcombowho.Text, System.Text.Encoding.UTF8);
            //     MailAddress to = new MailAddress(sto, sto);//, System.Text.Encoding.Unicode
            MailAddress afrom = new MailAddress(from, "Fin System");//, System.Text.Encoding.Unicode
            // MailMessage message = new MailMessage(afrom, to);
            MailMessage message = new MailMessage();
            // System.Net.Mail.MailAddress
            message.IsBodyHtml = true;
            //   message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = sub;
            message.Body = content;
            message.From = afrom;
            string ch = ";";
            int vt = cc.IndexOf(",");
            if (vt >= 0)
                ch = ",";

            //add cc nhieu nguoi
            foreach (var curr_address in cc.Split(new[] { ch }, StringSplitOptions.RemoveEmptyEntries))
            {
                MailAddress copy = new MailAddress(curr_address);
                // TO_addressList.Add(mytoAddress);
                message.CC.Add(copy);
            }
            //add to nhieu nguoi
            ch = ";";
            vt = sto.IndexOf(",");
            if (vt >= 0)
                ch = ",";
            foreach (var address in sto.Split(new[] { ch }, StringSplitOptions.RemoveEmptyEntries))
            {
                MailAddress to = new MailAddress(address);
                message.To.Add(to);
            }
            MailAddress bcc = new MailAddress(from);
            message.Bcc.Add(bcc);
            //MailAddress copy = new MailAddress(cc);
            //message.CC.Add(copy);
            message.SubjectEncoding = System.Text.Encoding.Default;

            //SmtpClient client = new SmtpClient(smtpserver, Convert.ToInt16(smtpport));
            //// client.UseDefaultCredentials = true;
            //client.Credentials = new System.Net.NetworkCredential(usemail, pass);

            //// client.UseDefaultCredentials = true;
            //// client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            //client.EnableSsl = bool.Parse(ssl);
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(usemail, pass);
            client.Port = cToInt(smtpport); // You can use Port 25 if 587 is blocked (mine is!)
            client.Host = smtpserver;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = bool.Parse(ssl);
            //  client.Timeout = 2000000;
            client.Send(message);
            kq = true;
        }
        catch
        {
            kq = false;
        }
        return kq;
    }
    public bool SendMailASP(string sto, string sub, string content)
    {

        bool kq = false;//
        try
        {
            string from = ConfigurationManager.AppSettings["fromemail"].ToString();
            string usemail = ConfigurationManager.AppSettings["useremail"].ToString();
            string smtpserver = ConfigurationManager.AppSettings["smtp"].ToString();
            string smtpport = ConfigurationManager.AppSettings["smtpport"].ToString();
            string ssl = ConfigurationManager.AppSettings["ssl"].ToString();
            string pass = ConfigurationManager.AppSettings["pass"].ToString();
            //SmtpClient smtpclient = new SmtpClient();
            //System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
            // MailAddress mailAddress = new MailAddress(radcombowho.SelectedValue, radcombowho.Text, System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress(sto, sto);//, System.Text.Encoding.Unicode
            MailAddress afrom = new MailAddress(from, "FIN System");//, System.Text.Encoding.Unicode
            MailMessage message = new MailMessage(afrom, to);
            // message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Subject = sub;
            message.Body = content;

            SmtpClient client = new SmtpClient(smtpserver, Convert.ToInt16(smtpport));
            // client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential(usemail, pass);
            client.EnableSsl = bool.Parse(ssl);
            client.Timeout = 0;

            // client.Timeout = 0;

            client.Send(message);
            kq = true;
        }
        catch
        {
            kq = false;
        }
        return kq;
    }
    /// <summary>
    /// UPload file IP PR request
    /// </summary>
    /// <param name="up"></param>
    /// <param name="docno"></param>
    /// <returns></returns>
    public string radupload(RadAsyncUpload up, object docno)
    {
        string kq = "";
        // if(   RadUpload1.UploadedFiles.Count>0)
        if (up.UploadedFiles.Count > 0)
        {
            try
            {

                //int vt1 = up.FileName.LastIndexOf(".");
                //  int vtcanlay = vt1;
                //  int len = up..FileName.Length;
                string extention = up.UploadedFiles[0].GetExtension();
                string filename = "";
                filename = cToString(docno).Replace('/', '-');
                filename = filename + '-' + cToString(Session["username"]) + extention;
                //HinhBia.SaveAs(Server.MapPath("../" + ConfigurationManager.AppSettings["hinhbia_sanpham"].ToString()).ToString() + ANHBIA);
                string sFolderPath = Server.MapPath("Upload/IP/" + filename);
                //  string sFullPath = sFolderPath + filename;
                if (System.IO.File.Exists(sFolderPath) == true)
                    System.IO.File.Delete(sFolderPath);
                //resize
                //  HttpPostedFile pf = FileUpload1.PostedFile;
                // up.SaveAs(sFolderPath);
                // kq = filename;
                try
                {
                    // up.SaveAs(sFolderPath);

                    up.UploadedFiles[0].SaveAs(sFolderPath);
                    kq = filename;
                }
                catch
                {
                    kq = "";

                }

            }
            catch
            {
                kq = "";
            }
        }
        else
        {
            kq = "";
        }
        return kq;
    }
    /// <summary>
    /// Get Senior manager of username
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public string getSeniorManager(string username)
    {
       return GetString("sp_getSeniorManager", new string[] { "@username" }, new object[] { username });
    }
    /// <summary>
    /// Get Director of username
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public string getDirector(string username)
    {
        return GetString("sp_getDirector", new string[] { "@username" }, new object[] { username });
    }
    /// <summary>
    /// Get VP of username
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public string getVP(string username)
    {
        return GetString("sp_getVP", new string[] { "@username" }, new object[] { username });
    }
    /// <summary>
    /// Get COO of username
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public string getCOO(string username)
    {
        return GetString("sp_getCOO", new string[] { "@username" }, new object[] { username });
    }
    /// <summary>
    /// Get user name from email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public string get_UsernameFromEmail(string email)
    {
        string user = "";
       int vt= email.LastIndexOf("@");
       if (vt > 0)
       {
           user = email.Substring(0, vt);
       }
       else
       {
           user = email;
       }
       return user;
    }
   //public struct ketqua
   // {
   //     public bool bketqua;
   //     public string noidung;

   // };
    #region Lunar
    //public class ChuyenDoiNgay
    //{
    private double timeZone = 7.0;
    //Lấy Ngày Julius
    private int getJulius(int intNgay, int intThang, int intNam)
    {
        int a, y, m, jd;
        a = (int)((14 - intThang) / 12);
        y = intNam + 4800 - a;
        m = intThang + 12 * a - 3;
        jd = intNgay + (int)((153 * m + 2) / 5) + 365 * y + (int)(y / 4) - (int)(y / 100) + (int)(y / 400) - 32045;
        if (jd < 2299161)
        {
            jd = intNgay + (int)((153 * m + 2) / 5) + 365 * y + (int)(y / 4) - 32083;
        }
        return jd;
    }

    // Từ ngày Julius chuyển ra ngày thường
    private string jdToDate(int jd)
    {
        int a, b, c, d, e, m;
        int day, month, year;
        if (jd > 2299160)
        { // After 5/10/1582, Gregorian calendar
            a = jd + 32044;
            b = (int)((4 * a + 3) / 146097);
            c = a - (int)((b * 146097) / 4);
        }
        else
        {
            b = 0;
            c = jd + 32082;
        }
        d = (int)((4 * c + 3) / 1461);
        e = c - (int)((1461 * d) / 4);
        m = (int)((5 * e + 2) / 153);
        day = e - (int)((153 * m + 2) / 5) + 1;
        month = m + 3 - 12 * (int)(m / 10);
        year = b * 100 + d - 4800 + (int)(m / 10);
        return day.ToString() + "/" + month.ToString() + "/" + year.ToString();
    }
    /// <summary>
    ///  Tính ngày Sóc
    /// </summary>

    private int getNewMoonDay(int k)
    {
        double T, T2, T3, dr, Jd1, M, Mpr, F, C1, deltat, JdNew;
        T = k / 1236.85;
        T2 = T * T;
        T3 = T2 * T;
        dr = Math.PI / 180;
        Jd1 = 2415020.75933 + 29.53058868 * k + 0.0001178 * T2 - 0.000000155 * T3;
        Jd1 = Jd1 + 0.00033 * Math.Sin((166.56 + 132.87 * T - 0.009173 * T2) * dr); // Mean new moon
        M = 359.2242 + 29.10535608 * k - 0.0000333 * T2 - 0.00000347 * T3; // Sun's mean anomaly
        Mpr = 306.0253 + 385.81691806 * k + 0.0107306 * T2 + 0.00001236 * T3; // Moon's mean anomaly
        F = 21.2964 + 390.67050646 * k - 0.0016528 * T2 - 0.00000239 * T3; // Moon's argument of latitude
        C1 = (0.1734 - 0.000393 * T) * Math.Sin(M * dr) + 0.0021 * Math.Sin(2 * dr * M);
        C1 = C1 - 0.4068 * Math.Sin(Mpr * dr) + 0.0161 * Math.Sin(dr * 2 * Mpr);
        C1 = C1 - 0.0004 * Math.Sin(dr * 3 * Mpr);
        C1 = C1 + 0.0104 * Math.Sin(dr * 2 * F) - 0.0051 * Math.Sin(dr * (M + Mpr));
        C1 = C1 - 0.0074 * Math.Sin(dr * (M - Mpr)) + 0.0004 * Math.Sin(dr * (2 * F + M));
        C1 = C1 - 0.0004 * Math.Sin(dr * (2 * F - M)) - 0.0006 * Math.Sin(dr * (2 * F + Mpr));
        C1 = C1 + 0.0010 * Math.Sin(dr * (2 * F - Mpr)) + 0.0005 * Math.Sin(dr * (2 * Mpr + M));
        if (T < -11)
        {
            deltat = 0.001 + 0.000839 * T + 0.0002261 * T2 - 0.00000845 * T3 - 0.000000081 * T * T3;
        }
        else
        {
            deltat = -0.000278 + 0.000265 * T + 0.000262 * T2;
        }
        JdNew = Jd1 + C1 - deltat;
        return (int)(JdNew + 0.5 + timeZone / 24);
    }
    ///<summary>
    ///Tính toạ độ mặt trời
    ///</summary>
    private int getSunLongitude(int jdn)
    {
        double T, T2, dr, M, L0, DL, L;
        T = (jdn - 2451545.5 - timeZone / 24) / 36525; // Time in Julian centuries from 2000-01-01 12:00:00 GMT
        T2 = T * T;
        dr = Math.PI / 180; // degree to radian
        M = 357.52910 + 35999.05030 * T - 0.0001559 * T2 - 0.00000048 * T * T2; // mean anomaly, degree
        L0 = 280.46645 + 36000.76983 * T + 0.0003032 * T2; // mean longitude, degree
        DL = (1.914600 - 0.004817 * T - 0.000014 * T2) * Math.Sin(dr * M);
        DL = DL + (0.019993 - 0.000101 * T) * Math.Sin(dr * 2 * M) + 0.000290 * Math.Sin(dr * 3 * M);
        L = L0 + DL; // true longitude, degree
        L = L * dr;
        L = L - Math.PI * 2 * (int)(L / (Math.PI * 2)); // Normalize to (0, 2*PI)
        return (int)(L / Math.PI * 6);
    }

    //Tìm ngày bắt đầu tháng 11 âm lịch
    private int getLunarMonthll(int intNam)
    {
        double k, off, nm, sunLong;
        off = getJulius(31, 12, intNam) - 2415021;
        k = (int)(off / 29.530588853);
        nm = getNewMoonDay((int)k);
        sunLong = getSunLongitude((int)nm); // sun longitude at local midnight
        if (sunLong >= 9)
        {
            nm = getNewMoonDay((int)k - 1);
        }
        return (int)nm;
    }

    //Xác định tháng nhuận
    private int getLeapMonthOffset(double a11)
    {
        double last, arc;
        int k, i;
        k = (int)((a11 - 2415021.076998695) / 29.530588853 + 0.5);
        last = 0;
        i = 1; // We start with the month following lunar month 11
        arc = getSunLongitude((int)getNewMoonDay((int)(k + i)));
        do
        {
            last = arc;
            i++;
            arc = getSunLongitude((int)getNewMoonDay((int)(k + i)));
        } while (arc != last && i < 14);
        return i - 1;
    }

    ///<summary>
    ///Đổi ngày dương ra ngày âm string "dd/MM/yyyy"
    ///</summary>

    public string convertSolar2Lunar(int intNgay, int intThang, int intNam)
    {
        try
        {
            double dayNumber, monthStart, a11, b11, lunarDay, lunarMonth, lunarYear;
            //double lunarLeap;
            int k, diff;
            dayNumber = getJulius(intNgay, intThang, intNam);
            k = (int)((dayNumber - 2415021.076998695) / 29.530588853);
            monthStart = getNewMoonDay(k + 1);
            if (monthStart > dayNumber)
            {
                monthStart = getNewMoonDay(k);
            }
            a11 = getLunarMonthll(intNam);
            b11 = a11;
            if (a11 >= monthStart)
            {
                lunarYear = intNam;
                a11 = getLunarMonthll(intNam - 1);
            }
            else
            {
                lunarYear = intNam + 1;
                b11 = getLunarMonthll(intNam + 1);
            }
            lunarDay = dayNumber - monthStart + 1;
            diff = (int)((monthStart - a11) / 29);
            //lunarLeap = 0;
            lunarMonth = diff + 11;
            if (b11 - a11 > 365)
            {
                int leapMonthDiff = getLeapMonthOffset(a11);
                if (diff >= leapMonthDiff)
                {
                    lunarMonth = diff + 10;
                    if (diff == leapMonthDiff)
                    {
                        //lunarLeap = 1;
                    }
                }
            }
            if (lunarMonth > 12)
            {
                lunarMonth = lunarMonth - 12;
            }
            if (lunarMonth >= 11 && diff < 4)
            {
                lunarYear -= 1;
            }
            string strNgay = lunarDay.ToString();
            string strThang = lunarMonth.ToString();
            string strNam = lunarYear.ToString();
            if (strNgay.Length < 2)
                strNgay = "0" + strNgay;
            if (strThang.Length < 2)
                strThang = "0" + strThang;
            return strNgay + "/" + strThang + "/" + strNam;
        }
        catch
        {
            return intNgay.ToString() + "/" + intThang.ToString() + "/" + intNam.ToString();
        }
    }
    /// <summary>
    /// Lấy số ngày tối đa trong tháng của một năm
    /// </summary>
    /// <param name="nam"></param>
    /// <param name="thang"></param>
    /// <returns></returns>
    public int songaytrongthang(int nam, int thang)
    {
        //DateTime dt=new DateTime(nam,thang,01);
        int ngay = DateTime.DaysInMonth(nam, thang);
        //MessageBox.Show("ngay cua thang " + thang + " trog nam " + nam + ":" + ngay.ToString());
        return ngay;

    }
    /// <summary>
    /// Lấy số ngày trong năm
    /// </summary>
    /// <param name="nam"></param>
    /// <returns></returns>
    private int songaytrongnam(int nam)
    {
        DateTime dt = new DateTime(nam, 01, 01);
        int ngay = dt.DayOfYear;
        return ngay;

    }
    /// <summary>
    /// Tính khoảng cách giữa hai ngày,co trừ ngày chủ nhật,ngay nghi le
    /// </summary>
    /// <param name="NgaySau"></param>
    /// <param name="NgayTruoc"></param>
    /// <param name="ngaynghiDL"></param>
    /// <param name="ngaynghiAL"></param>
    /// <returns></returns>
    public int TinhNgay(DateTime NgaySau, DateTime NgayTruoc, ArrayList ngaynghiDL, ArrayList ngaynghiAL)
    {
        TimeSpan ts = NgaySau.Date - NgayTruoc.Date;
        int songay = ts.Days;
        //calendar cl = new calendar();
        //string mung10t3 = cl.convertAMtoDuong(10, 3, NgayTruoc.Year);//tim ngay duong lich cua ngay mung 10 thang 3 am lich
        //mung10t3 = mung10t3.Substring(0, 5);//lay ngay thang
        ArrayList ngayALtoDL = new ArrayList();
        foreach (string d in ngaynghiAL)
        {
            int ngay = Convert.ToInt32(d.Substring(0, d.IndexOf("/")));
            int thang = Convert.ToInt32(d.Substring(d.IndexOf("/") + 1, d.Length - 1 - d.IndexOf("/")));
            string ngayal = convertAMtoDuong(ngay, thang, NgayTruoc.Year);
            ngayALtoDL.Add(ngayal);
        }
        //string[] ngaynghi = new string[] { "01/01","30/04", "01/05", "02/09" };
        for (int ij = 1; ij <= songay; ij++)
        {
            DateTime thu = NgayTruoc.AddDays(ij);
            //'ngay nghi trong tuan
            if (thu.DayOfWeek == DayOfWeek.Sunday) //Then //'ngay thu 7 hoac chu nhat
                songay -= 1;
            foreach (string d in ngaynghiDL)
            {
                if (thu.ToString("dd/MM") == d)
                    songay -= 1;
            }
            foreach (string d in ngayALtoDL)
            {
                if (thu.ToString("dd/MM") == d)
                    songay -= 1;
            }
        }
        return songay;
    }
    /// <summary>
    /// Tính khoảng cách giữa hai ngày,co trừ ngày chủ nhật,ngay nghi le
    /// </summary>
    /// <param name="NgaySau"></param>
    /// <param name="NgayTruoc"></param>
    /// <param name="ngaynghiDL"></param>
    /// <param name="ngaynghiAL"></param>
    /// <returns></returns>
    public int TinhNgay(DateTime NgaySau, DateTime NgayTruoc, string[] ngaynghiDL, string[] ngaynghiAL)
    {
        TimeSpan ts = NgaySau - NgayTruoc;
        int songay = ts.Days;
        //calendar cl = new calendar();
        //string mung10t3 = cl.convertAMtoDuong(10, 3, NgayTruoc.Year);//tim ngay duong lich cua ngay mung 10 thang 3 am lich
        //mung10t3 = mung10t3.Substring(0, 5);//lay ngay thang
        ArrayList ngayALtoDL = new ArrayList();
        foreach (string d in ngaynghiAL)
        {
            int ngay = Convert.ToInt32(d.Substring(0, d.IndexOf("/")));
            int thang = Convert.ToInt32(d.Substring(d.IndexOf("/") + 1, d.Length - 1 - d.IndexOf("/")));
            string ngayal = convertAMtoDuong(ngay, thang, NgayTruoc.Year);
            ngayALtoDL.Add(ngayal);
        }
        //string[] ngaynghi = new string[] { "01/01","30/04", "01/05", "02/09" };
        for (int ij = 1; ij <= songay; ij++)
        {
            DateTime thu = NgayTruoc.AddDays(ij);
            //'ngay nghi trong tuan
            if (thu.DayOfWeek == DayOfWeek.Sunday) //Then //'ngay thu 7 hoac chu nhat
                songay -= 1;
            foreach (string d in ngaynghiDL)
            {
                if (thu.ToString("dd/MM") == d)
                    songay -= 1;
            }
            foreach (string d in ngayALtoDL)
            {
                if (thu.ToString("dd/MM") == d)
                    songay -= 1;
            }
        }
        return songay;
    }
    /// <summary>
    /// Tru 2 ngay
    /// </summary>
    /// <param name="NgaySau"></param>
    /// <param name="NgayTruoc"></param>
    /// <returns></returns>
    public int TruNgay(DateTime NgaySau, DateTime NgayTruoc)
    {
        TimeSpan ts = NgaySau.Date - NgayTruoc.Date;
        int songay = ts.Days;
        return songay;
    }
    public DateTime CongNgay(DateTime NgayTruoc, int SoNgay, string[] ngaynghiDL, string[] ngaynghiAL, bool NgaySX)
    {
        ArrayList ngayALtoDL = new ArrayList();
        foreach (string d in ngaynghiAL)
        {
            int ngay = Convert.ToInt32(d.Substring(0, d.IndexOf("/")));
            int thang = Convert.ToInt32(d.Substring(d.IndexOf("/") + 1, d.Length - 1 - d.IndexOf("/")));

            string ngayal = convertAMtoDuong(ngay, thang, NgayTruoc.Year);
            ngayALtoDL.Add(ngayal);

        }
        if (NgaySX == true)
        {
            if (SoNgay >= 20)
            {
                SoNgay = SoNgay + 2;
            }
            else
            {
                SoNgay = SoNgay + 1;
            }
        }
        //else
        //{

        //}
        DateTime Ngay = new DateTime();
        for (int ij = 1; ij <= SoNgay; ij++)
        {
            Ngay = NgayTruoc.AddDays(ij);
            foreach (string d in ngaynghiDL)
            {
                if (Ngay.ToString("dd/MM") == d)
                    SoNgay += 1;
            }
            foreach (string d in ngayALtoDL)
            {
                if (Ngay.ToString("dd/MM") == d)
                    SoNgay += 1;
            }
        }
        return Ngay;
    }

    public DateTime CongNgay1(DateTime NgayTruoc, int SoNgay, string[] ngaynghiDL, string[] ngaynghiAL, bool TruChuNhat)
    {
        ArrayList ngayALtoDL = new ArrayList();
        foreach (string d in ngaynghiAL)
        {
            int ngay = Convert.ToInt32(d.Substring(0, d.IndexOf("/")));
            int thang = Convert.ToInt32(d.Substring(d.IndexOf("/") + 1, d.Length - 1 - d.IndexOf("/")));
            string ngayal = convertAMtoDuong(ngay, thang, NgayTruoc.Year);
            ngayALtoDL.Add(ngayal);
        }
        DateTime Ngay = new DateTime();
        if (SoNgay > 0)
        {
            for (int ij = 1; ij <= SoNgay; ij++)
            {
                Ngay = NgayTruoc.AddDays(ij);
                if (TruChuNhat == true)
                {
                    if (Ngay.DayOfWeek == DayOfWeek.Sunday) //Then //'ngay thu 7 hoac chu nhat
                        SoNgay += 1;
                }
                foreach (string d in ngaynghiDL)
                {
                    if (Ngay.ToString("dd/MM") == d)
                        SoNgay += 1;
                }
                foreach (string d in ngayALtoDL)
                {
                    if (Ngay.ToString("dd/MM") == d)
                        SoNgay += 1;
                }
            }
        }
        else
        {
            SoNgay = -SoNgay;
            for (int ij = 1; ij <= SoNgay; ij++)
            {
                int tam = -ij;
                Ngay = NgayTruoc.AddDays(tam);
                if (TruChuNhat == true)
                {
                    if (Ngay.DayOfWeek == DayOfWeek.Sunday) //Then //'ngay thu 7 hoac chu nhat
                        SoNgay += 1;
                }
                foreach (string d in ngaynghiDL)
                {
                    if (Ngay.ToString("dd/MM") == d)
                        SoNgay += 1;
                }
                foreach (string d in ngayALtoDL)
                {
                    if (Ngay.ToString("dd/MM") == d)
                        SoNgay += 1;
                }
            }
        }
        return Ngay;
    }

    /// <summary>
    /// convert ngay am sang ngay duong string "dd/MM/yyyy"
    /// </summary>
    /// <param name="intNgay"></param>
    /// <param name="intThang"></param>
    /// <param name="intNam"></param>
    /// <returns></returns>

    public string convertAMtoDuong(int intNgay, int intThang, int intNam)//dd/MM/yyyy
    {
        // string ngay = convertSolar2Lunar(ngay,thang,nam);
        string ngay = intNgay.ToString();
        string thang = intThang.ToString();
        string nam = intNam.ToString();
        if (intNgay < 10)
            ngay = "0" + intNgay.ToString();
        if (intThang < 10)
            thang = "0" + intThang.ToString();
        nam = intNam.ToString();
        DateTime dt = new DateTime(intNam, 1, 1);
        while (convertSolar2Lunar(dt.Day, dt.Month, dt.Year) != ngay + "/" + thang + "/" + nam && dt.Year <= intNam)
        {
            dt = dt.AddDays(1);
        }
        if (dt.Day < 10)
            ngay = "0" + dt.Day.ToString();
        else
            ngay = dt.Day.ToString();
        if (dt.Month < 10)
            thang = "0" + dt.Month.ToString();
        else
            thang = dt.Month.ToString();
        nam = dt.Year.ToString();
        return ngay + "/" + thang + "/" + nam;
    }
    #endregion
    /// <summary>
    /// 0: Ngay1=Ngay2
    /// 1: Ngay1> Ngay2
    /// 2: Ngay1 nho hon Ngay2
    /// </summary>
    /// <param name="Ngay1"></param>
    /// <param name="Ngay2"></param>
    /// <returns></returns>
    public int KiemTraNgay(DateTime Ngay1, DateTime Ngay2)
    {
        try
        {
            if (Ngay1.Date > Ngay2.Date)
                return 1;
            if (Ngay1.Date < Ngay2.Date)
                return 2;
            return 0;
        }
        catch { return -1; }
    }
    /// <summary>
    /// 0: Ngay1=Ngay2
    /// 1: Ngay1> Ngay2
    /// 2: Ngay1 nho hon Ngay2
    /// </summary>
    /// <param name="Ngay1"></param>
    /// <param name="Ngay2"></param>
    /// <returns></returns>
    public int KiemTraNgay(object Ngay1, object Ngay2)
    {
        try
        {
            DateTime n1 = (DateTime)Ngay1;
            DateTime n2 = (DateTime)Ngay2;
            if (n1.Date > n2.Date)
                return 1;
            if (n1.Date < n2.Date)
                return 2;
            return 0;
        }
        catch { return -1; }
    }
    /// <summary>
    /// Sun: 0 - Mon: 1 - Tue: 2 - Web: 3 - Thu: 4 - Fri: 5 - Sat: 6
    /// </summary>
    /// <param name="ngay"></param>
    /// <returns></returns>
    public string getThu(DateTime ngay)
    {
        // Sun: 0 - Mon: 1 - Tue: 2 - Web: 3 - Thu: 4 - Fri: 5 - Sat: 6
        string kq = "";
        int thu = (int)ngay.DayOfWeek;
        switch (thu)
        {
            case 0:
                kq = "CN";
                break;
            case 1:
                kq = "Thứ 2";
                break;
            case 2:
                kq = "Thứ 3";
                break;
            case 3:
                kq = "Thứ 4";
                break;
            case 4:
                kq = "Thứ 5";
                break;
            case 5:
                kq = "Thứ 6";
                break;
            case 6:
                kq = "Thứ 7";
                break;
        }
        return kq;
    }
    ///// <summary>
    ///// Sun: 0 - Mon: 1 - Tue: 2 - Web: 3 - Thu: 4 - Fri: 5 - Sat: 6
    ///// </summary>
    ///// <param name="ngay"></param>
    ///// <returns></returns>
    //public int getThu(DateTime ngay)
    //{
    //    // Sun: 0 - Mon: 1 - Tue: 2 - Web: 3 - Thu: 4 - Fri: 5 - Sat: 6
    //    return (int)ngay.DayOfWeek;
    //}
    public bool SenEmailSubmitWorkingPlan(string code, string to, string appby, string activationCode, string nguoidenghi, string phongban, string mucdich, string TuNgay, string DenNgay, RadGrid RadGrid1,string link)
    {
        bool kq = false;
        clsSys sys = new clsSys();
        // Guid activationCode = Guid.NewGuid();
     //   string activationCode = cls.GetString("sp_getGuiCode", new string[] { "@docno", "@approval" }, new object[] { code, to });
        // string to = txtAppEmail.Text;
        // string cc = txtMyEmail.Text;
       // string nguoidenghi = txtName.Text;
       // string phongban = comboDepartment1.Text;
       // string mucdich = txtPurpose.Text;
        string noiden = "";// txtNoiDen.Text;
        string lotrinh = "";// txtLoTrinh.Text;

        string thoigian = "Từ/From " + TuNgay + " Đến/To " + DenNgay;
        //string phuongtien = chkOto.Checked ? " Oto / Car " : "";
        //phuongtien=phuongtien+cls.cToString( chkTauHoa.Checked ? " Tàu hỏa / Train " : "");
        //phuongtien = phuongtien + cls.cToString(chkMayBay.Checked ? " Máy bay / Flight " : "");
        //string thuxep = chkVeTauMayBay.Checked ? " Mua vé máy bay / Returned air ticket; " : "";
        //thuxep = thuxep + cls.cToString(chkDatPhong.Checked ? " Đặt khách sạn / Hotel booking; " : "");
        //thuxep = thuxep + cls.cToString(chkOther.Checked ? txtOther.Text : "");
        string html = "";
        html = "<table><tr><td>Người đề nghị/Requester: <b>" + nguoidenghi + "</b> Phòng ban/Dept: " + phongban + "</td></tr>";

        html = html + "<tr><td>Mục đích công tác/Purpose of business trip: <b>" + mucdich + "</b></td></tr>";
        html = html + "<tr><td>Thời gian/Length of days: <b>" + thoigian + "</b></td></tr>";


        //   html = html + "<tr><td>Phương tiện/Transportation mean: <b>" + phuongtien + "</b></td></tr>";
        //  html = html + "<tr><td>Đề nghị hành chánh thu xếp/Request admin to arrange: <b>" + thuxep + "</b></td></tr>";
        html = html + "</table>";
        html = html + "<table  cellpadding=\"2\" cellspacing=\"0\" style=\"width: 100%; border: 1px solid black; border-collapse: collapse; font-size: 12px;\">";
        html = html + "<tr style=\"border: 1px solid black; border-collapse: collapse\"><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">STT</br>No</td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Ngày</br>Date </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Thứ</td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Sáng</br>Morning </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">TỉnhS</br>ProvinceM </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">HuyenS</br>DistrictM </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Chiều</br>Afternoon</td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">TỉnhC</br>ProvinceA </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">HuyenC</br>DistrictA </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Chú thích</br>Note</td></tr>";
        //double tongtien = 0;
        //double tamung = 0;
        //RadGrid RadGrid1=new RadGrid();
       // RadGrid1.DataSource = tbldetail;
       // RadGrid1.DataBind();
        foreach (GridDataItem item in RadGrid1.Items)
        {

            html = html + "<tr><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + cToString0(item.ItemIndex + 1) + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["FDate"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["Thu"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["PurposeMorning"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["TenTinhSang"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["TenHuyenSang"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["PurposeAfter"].Text + "</td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["TenTinhChieu"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["TenHuyenChieu"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["Note"].Text + "</td></tr>";
        }

        html = html + "</table>";
        string who = "";
        if (appby != "")
        {
            who = "(has been approved by " + appby + ")";
        }
        string content = "Dear  Sir/Madam,</br></br>Please approve Travel Request number " + code + who;
        content = content + ". <a href = '" + link;
        content = content + "/TravelRequestSales.aspx?ActivationCode=" + activationCode + "&code=" + code;
        content = content + "'>Click here to approve.</a> Or <a href = '" + link;
        content = content + "/TravelRequestSales.aspx?RejectedCode=" + activationCode + "&code=" + code;
        content = content + "'>Click here to Reject.</a></br>" + html + "</br>Best Regards,";
        kq = sys.SendMailASP(to, "Approve Travel Request " + code, content);
         return kq;
    }
    public string FillTableemailClaimSubmit(List<MaricoPay.DB.sp_getClaimDetailSalesResult> tbl)
    {
        string kq = "";

     //   List<sp_getClaimDetailResult> tbl = (List<sp_getClaimDetailResult>)claimdetail;

        double tongtien = 0;
        int stt = 0;
        foreach (MaricoPay.DB.sp_getClaimDetailSalesResult item in tbl)
        {

            if (item.TotalVN != 0 && item.TrangThai==1)
            {
                if (item.Date == new DateTime(2000, 1, 1))
                {
                    //dong subtotal

                    kq = kq + "<tr><td colspan=7 style='color: #000000; font-weight: bold; text-align:left; border:1px solid black; border-collapse:collapse;'>Subtotal</td>";

                    kq = kq
                      + "<td colspan=4 style='color: #000000; font-weight: bold; border:1px solid black; border-collapse:collapse; text-align:left;'>" + String.Format("{0:0,0}", item.TotalVN) + "</td>"
                      + "</tr>";
                }
                else
                {
                    stt++;
                    tongtien = tongtien + cToDouble(item.TotalVN);
                    kq = kq + "<tr><td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + cToString0(stt) + "</td><td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Date.ToString("dd-MMM-yy") + "</td>"
                                      + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>"
                                       + "<a  href='#' onclick=\"javascript:window.open('/popVAT.aspx?cp=" + item.CompanyName + "&pv=" + item.Province + "&vatcode=" + item.VATCode + "&taxnumber=" + item.TaxNumber + "&vatamount=" + item.VATAmount + "','VAT','width=500,height=150')\">" + item.No + "</a></td>";


                    kq = kq
                       + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.DetailExpenses + "</td>"
                        + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Participant + "</td>"
                         + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.ChargeTypeDs + "</td>";

                    kq = kq + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.WorkingPlanDetail + "</td>"
                 + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.Amount) + "</td>"
                + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.VATAmount) + "</td>";

                    kq = kq
                      + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.TotalVN) + "</td>"
                      + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.GL + "</td>"
                      + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.IO + "</td>"
                      + "</tr>";
                }


            }
        }
        //tinh tong tien
        kq = kq + "<tr><td colspan=9 style='color: #000000; font-weight: bold; text-align:right;'>Tổng tiền VND/Total Amount:</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + FormatNumber(tongtien) + "</td><td colspan=2></td></tr>";
        kq = kq + "<tr><td colspan=9 style='color: #000000; font-weight: bold; text-align:right;'>Đã tạm ứng VND/Advanced Amount:</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + FormatNumber(0) + "</td><td colspan=2></td></tr>";
        kq = kq + "<tr><td colspan=9 style='color: #000000; font-weight: bold; text-align:right;'>Chênh lệch VND/Pay back(+)/Reimbursemet(-):</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + FormatNumber(tongtien -0) + "</td><td colspan=2></td></tr>";
        return kq;
    }
    public bool SenEmailSubmitClaimWorkingPlan(string code, string to, string appby, string activationCode, string nguoidenghi, string phongban, string mucdich, string tungay, string denngay, string link)
    {
        clsSys sys = new clsSys();

        // Guid activationCode = Guid.NewGuid();
        //DBTableDataContext db = new DBTableDataContext();
        // db.ClaimExpenses.InsertAllOnSubmit
        //string activationCode = cls.GetString("sp_getGuiCode", new string[] { "@docno", "@approval" }, new object[] { code, to });
        //using (var db = new DBTableDataContext())
        //{
        //    var model = db.ClaimExpenses.SingleOrDefault(p => p.Code_PK == code);
        //    model.ApprovedCode1 = activationCode;
        //    db.SubmitChanges();
        //}

        // string to = txtAppEmail.Text;
        // string cc = txtMyEmail.Text;


       // string nguoidenghi = txtName.Text;
       // string phongban = comboDepartment1.Text;
       // string mucdich = txtPurpose.Text;
        string datamung = "0";
        string thoigian = "Từ/From " + tungay + " Đến/To " + denngay;

        string html = "";
        html = "<table><tr><td>Người đề nghị/Requester: <b>" + nguoidenghi + "</b> Phòng ban/Dept: " + phongban + "</td></tr>";
        html = html + "<tr><td>Nội dung thanh toán/Purpose: <b>" + mucdich + "</b></td></tr>";
        html = html + "<tr><td>Thời gian/Length of days: <b>" + thoigian + "</b></td></tr>";
        html = html + "<tr><td>Đã tạm ứng/Advanced: <b>" + datamung + " VNĐ</b></td></tr>";
        html = html + "</table>";
        html = html + "<table cellpadding=\"2\" cellspacing=\"0\" style=\"width: 100%; border: 1px solid black; border-collapse: collapse; font-size: 12px;\">";
        html = html + "<thead>";
        html = html + "<tr style=\"border: 1px solid black; border-collapse: collapse\">";
        html = html + "<th align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"  rowspan=\"2\">STT<br />No</th>";
        html = html + "<th align=\"center\" colspan=\"2\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Hóa đơn/Invoice</th>";
        html = html + "<th rowspan=\"2\" align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Chi tiết chi phí<br />Detail of Expenses</th>";
        html = html + "<th align=\"center\" rowspan=\"2\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Người tham gia<br /> Participant </th>";
        html = html + "<th align=\"center\" rowspan=\"2\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Loai CP<br /> Charge type </th>";
        html = html + "<th align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\" rowspan=\"2\">Kế hoạch công tác<br /> Working plan </th>";
        html = html + " <th align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\" rowspan=\"2\"> Thành tiền (Ko VAT)<br /> Amount (Non VAT)</th>";
        html = html + "<th align=\"center\" rowspan=\"2\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\">Tiền thuế<br /> Tax Amount</th>";
        html = html + "<th align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\" rowspan=\"2\"> Thành tiền VND<br /> Amount Total</th>";
        html = html + "<th align=\"center\" rowspan=\"2\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\"> GL</th>";
        html = html + "<th align=\"center\" rowspan=\"2\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\"> IO</th>";
        html = html + "</tr>";
        html = html + "<tr style=\"border: 1px solid black; border-collapse: collapse\">";
        html = html + "<th align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> Ngày/Date</th>";
        html = html + "<th align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Số/No</th>";
        html = html + "</tr>";
        html = html + " </thead>";
        html = html + " <tbody>";
        MaricoPay.DB.DBStoreDataContext dbs = new MaricoPay.DB.DBStoreDataContext();
        List<MaricoPay.DB.sp_getClaimDetailSalesResult> kq1 = dbs.sp_getClaimDetailSales(code, false,cToString(Session["Username"])).OrderBy(m => m.Date).ToList();//sp_getClaimDetail LA STORE
      //  Session["ClaimDetailPrintSales"] = kq1;
        dbs.Dispose();
        string kq = FillTableemailClaimSubmit(kq1);
        html = html + kq;

        html = html + "</tbody></table>";

        string who = "";
        if (appby != "")
        {
            who = "(has been approved by " + appby + ")";
        }
        string content= "Dear Sir/Madam,<br/><br/>Please approve Claim number " + code + who;
       content=content + ". <a href = '" + link;
        content=content+ "/ClaimExpensesSales.aspx?ActivationCode=" + activationCode + "&code=" + code;
        content=content+ "'>Click here to approve.</a> or </br><a href = '" + link;
        content=content+ "/ClaimExpensesSales.aspx?RejectedCode=" + activationCode.ToString() + "&code=" + code;
        content = content + "'>Click here to Reject.</a></br></br>" + html + "</br>Best Regards,";
        bool kq2 = sys.SendMailASP(to, "Approve Expense Claim",content);

        return kq2;

    }
}
#region read number
public class VNCurrency
{
    public static string ToString(decimal number)
    {
        string s = number.ToString("#");
        string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
        int i, j, donvi, chuc, tram;
        string str = " ";
        bool booAm = false;
        decimal decS = 0;
        //Tung addnew
        try
        {
            decS = Convert.ToDecimal(s.ToString());
        }
        catch
        {
        }
        if (decS < 0)
        {
            decS = -decS;
            s = decS.ToString();
            booAm = true;
        }
        i = s.Length;
        if (i == 0)
            str = so[0] + str;
        else
        {
            j = 0;
            while (i > 0)
            {
                donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                i--;
                if (i > 0)
                    chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                else
                    chuc = -1;
                i--;
                if (i > 0)
                    tram = Convert.ToInt32(s.Substring(i - 1, 1));
                else
                    tram = -1;
                i--;
                if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                    str = hang[j] + str;
                j++;
                if (j > 3) j = 1;
                if ((donvi == 1) && (chuc > 1))
                    str = "mốt " + str;
                else
                {
                    if ((donvi == 5) && (chuc > 0))
                        str = "lăm " + str;
                    else if (donvi > 0)
                        str = so[donvi] + " " + str;
                }
                if (chuc < 0)
                    break;
                else
                {
                    if ((chuc == 0) && (donvi > 0)) str = "lẻ " + str;
                    if (chuc == 1) str = "mười " + str;
                    if (chuc > 1) str = so[chuc] + " mươi " + str;
                }
                if (tram < 0) break;
                else
                {
                    if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                }
                str = " " + str;
            }
        }
        if (booAm) str = "Âm " + str;
        return str + "đồng chẵn";
    }

    public static string ToString(double number)
    {
        string s = number.ToString("#");
        string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
        int i, j, donvi, chuc, tram;
        string str = " ";
        bool booAm = false;
        double decS = 0;
        //Tung addnew
        try
        {
            decS = Convert.ToDouble(s.ToString());
        }
        catch
        {
        }
        if (decS < 0)
        {
            decS = -decS;
            s = decS.ToString();
            booAm = true;
        }
        i = s.Length;
        if (i == 0)
            str = so[0] + str;
        else
        {
            j = 0;
            while (i > 0)
            {
                donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                i--;
                if (i > 0)
                    chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                else
                    chuc = -1;
                i--;
                if (i > 0)
                    tram = Convert.ToInt32(s.Substring(i - 1, 1));
                else
                    tram = -1;
                i--;
                if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                    str = hang[j] + str;
                j++;
                if (j > 3) j = 1;
                if ((donvi == 1) && (chuc > 1))
                    str = "mốt " + str;
                else
                {
                    if ((donvi == 5) && (chuc > 0))
                        str = "lăm " + str;
                    else if (donvi > 0)
                        str = so[donvi] + " " + str;
                }
                if (chuc < 0)
                    break;
                else
                {
                    if ((chuc == 0) && (donvi > 0)) str = "lẻ " + str;
                    if (chuc == 1) str = "mười " + str;
                    if (chuc > 1) str = so[chuc] + " mươi " + str;
                }
                if (tram < 0) break;
                else
                {
                    if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                }
                str = " " + str;
            }
        }
        if (booAm) str = "Âm " + str;
        return str + "đồng chẵn";
    }
}
#endregion

public struct ketquaSign
{
    public bool bketqua;
    public string noidung;

    public ketquaSign(bool p1, string p2)
    {
        bketqua = p1;
        noidung = p2;
    }
}

