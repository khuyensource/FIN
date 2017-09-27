using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;

namespace MaricoPay
{
    public partial class UploadASPF : clsPhanQuyenCaoCap
    {
        Cclass cls = new Cclass();
        clsObj Obj = new clsObj();
        clsSql Sql = new clsSql();
        clsSys sys = new clsSys();
      //  public int flagCheck ;
        int bienketQua = 0;
        public int KetQua
        {
            get { return bienketQua; }
            set { bienketQua = value; }
        }

        
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["email"] != null)
            {

                if (!IsPostBack)
                {
                    DataTable tblto = cls.GetDataTable("sp_ASPF_Load_Table", new string[] { }, new object[] { });
                    RadComboBox1.DataSource = tblto;
                    RadComboBox1.DataBind();
                }

            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
           
           
        }

        protected void btupload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string filename = FileUpload1.FileName;
                string path = string.Concat((Server.MapPath("~/ImagesUpload/" + filename)));
                FileUpload1.PostedFile.SaveAs(path);
                ExportToGrid(path);
              
            }

        }
        void ExportToGrid(String path)
        {
          
            OleDbConnection MyConnection = null;
            DataSet DtSet = null;
            OleDbDataAdapter MyCommand = null;
            bool xlsx = false;
            if (path.Substring(path.Length - 4).ToLower() == "xlsx")
            {
                xlsx = true;
                MyConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Extended Properties=\"Excel 12.0 Xml;HDR=YES; IMEX=1\";");
            }
            else
            {
                //MyConnection = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + path + "';Extended Properties=Excel 8.0;");
                MyConnection = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + path + "';Extended Properties=\"Excel 8.0; HDR=Yes; IMEX=1\";");
            }

            //use below connection string if your excel file .xslx 2007 format
            // 

            try
            {
                MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection);
                DtSet = new System.Data.DataSet();
                MyCommand.Fill(DtSet, "[Sheet1$]");
                DataTable dt = new DataTable();
                DataColumn dtc;


                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Country";
                dt.Columns.Add(dtc);

                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Business";
                dt.Columns.Add(dtc);

                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Function";
                dt.Columns.Add(dtc);

                 dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Channel";
                dt.Columns.Add(dtc);

                 dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "salegroup";
                dt.Columns.Add(dtc);

                 dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Activity";
                dt.Columns.Add(dtc);

                 dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Brand";
                dt.Columns.Add(dtc);

                 dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Cat";
                dt.Columns.Add(dtc);

                 dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "IO";
                dt.Columns.Add(dtc);

                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Amount";
                dt.Columns.Add(dtc);
                
                  dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "From";
                dt.Columns.Add(dtc);

                  dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "To";
                dt.Columns.Add(dtc);

          
                   dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "quarter";
                dt.Columns.Add(dtc);


                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "FY";
                dt.Columns.Add(dtc);
                
                dtc = new DataColumn();
                dtc.DataType = Type.GetType("System.String");
                dtc.ColumnName = "Upcharge";
                dt.Columns.Add(dtc);
                


                //  DataTable dt = DtSet.Tables[0];
                //  dt.Columns[0].DataType = typeof(string);
                MyConnection.Close();
         
                DataRow dr;
                for (int i = 0; i < DtSet.Tables[0].Rows.Count; i++)
                {

                    string Country = cls.cToString(DtSet.Tables[0].Rows[i][0]);
                    string Business = cls.cToString(DtSet.Tables[0].Rows[i][1]);
                    string Function = cls.cToString(DtSet.Tables[0].Rows[i][2]);
                    string Channel = cls.cToString(DtSet.Tables[0].Rows[i][3]);
                    string salegroup = cls.cToString(DtSet.Tables[0].Rows[i][4]);
                    string Activity = cls.cToString(DtSet.Tables[0].Rows[i][5]);
                    string Brand = cls.cToString(DtSet.Tables[0].Rows[i][6]);
                    string CAT = cls.cToString(DtSet.Tables[0].Rows[i][7]);
                    string IO = cls.cToString(DtSet.Tables[0].Rows[i][8]);
                    string Amount = cls.cToString(DtSet.Tables[0].Rows[i][9]);
                    string From = cls.cToString(DtSet.Tables[0].Rows[i][10]);
                    string To = cls.cToString(DtSet.Tables[0].Rows[i][11]);
                    string quarter = cls.cToString(DtSet.Tables[0].Rows[i][12]);
                    string FY = cls.cToString(DtSet.Tables[0].Rows[i][13]);
                    string Upcharge = cls.cToString(DtSet.Tables[0].Rows[i][14]);

                    //////////////// check quoc gia /////////////////
                    DataTable tblto = cls.GetDataTable("sp_checkName_country", new string[] { "@name" }, new object[] { Country });
                    if (tblto.Rows.Count <= 0)
                    {
                        Country = "SAI QUOC GIA";
                    //    Session["KetQua"] = 1;
                      //  return;
                    }
                 
                    //////////////// check sp_checkName_Bussiness /////////////////
                    DataTable tblto1 = cls.GetDataTable("sp_checkName_Bussiness", new string[] { "@name" }, new object[] { Business });
                    if (tblto1.Rows.Count <= 0)
                    {
                        Business = "SAI Business";
                     //   Session["KetQua"] = 1;
                     //   return;
                    }
                  
                    //////////////// check sp_checkName_function /////////////////
                    DataTable tblto2 = cls.GetDataTable("sp_checkName_function", new string[] { "@name" }, new object[] { Function });
                    if (tblto2.Rows.Count <= 0)
                    {
                        Function = "SAI Function";

                    }
                   

                    //////////////// sp_checkName_channel /////////////////
                    DataTable tblto3 = cls.GetDataTable("sp_checkName_channel", new string[] { "@name" }, new object[] { Channel });
                    if (tblto3.Rows.Count <= 0)
                    {
                        Channel = "SAI CHANNEL";
                      
                    }
                 
                    //////////////// sp_checkName_activity  /////////////////
                    DataTable tblto4 = cls.GetDataTable("sp_checkName_activity", new string[] { "@name" }, new object[] { Activity });
                    if (tblto4.Rows.Count <= 0)
                    {
                        Activity = "SAI Activity";
                   
                    }
                 
                    //////////////// sp_checkName_brand /////////////////
                    DataTable tblto5 = cls.GetDataTable("sp_checkName_brand", new string[] { "@name" }, new object[] { Brand });
                    if (tblto5.Rows.Count <= 0)
                    {
                        Brand = "SAI Brand";
                      
                    }
                  
                    //////////////// check quoc gia /////////////////
                    DataTable tblto6 = cls.GetDataTable("sp_checkName_Category", new string[] { "@name" }, new object[] { CAT });
                    if (tblto6.Rows.Count <= 0)
                    {
                        CAT = "SAI Category";
                     
                    }
                   


                 


                        dr = dt.NewRow();
                        dr["Country"] = Country;
                        dr["Business"] = Business;
                        dr["Channel"] = Channel;
                        dr["Function"] = Function;
                        dr["salegroup"] = ( salegroup=="&nbsp;" ?  "" : salegroup );
                        dr["Activity"] = Activity;
                        dr["Brand"] = Brand;
                        dr["CAT"] = CAT;
                        dr["IO"] = (IO == "&nbsp;" ? "" : IO);
                        dr["Amount"] = Amount;
                        dr["From"] = (From == "&nbsp;" ? "" : From);
                        dr["To"] = (To == "&nbsp;" ? "" : To);
                        dr["quarter"] = quarter;
                        dr["FY"] = FY;
                        dr["Upcharge"] = (Upcharge == "&nbsp;" ? "" : Upcharge);

                      //  dr["batch"] = cls.FixBatch(batch);
                        dt.Rows.Add(dr);
                   // }

                    //dt.Rows[i][0] = cls.cToString(dt.Rows[i][0]);
                    //dt.Rows[i][3] = cls.FixBatch(dt.Rows[i][3]);
                }
                if (dt.Rows.Count > 0)
                {

                    RadGrid1.DataSource = dt;
                    RadGrid1.DataBind();

                  

                    for (int i = 0; i < RadGrid1.Items.Count; i++)
                    {
                        if (RadGrid1.Items[i]["Country"].Text == "SAI QUOC GIA" || RadGrid1.Items[i]["Country"].Text == "&nbsp;")
                        {
                            RadGrid1.Items[i]["Country"].ForeColor = System.Drawing.Color.Red;
                          Session["KetQua"] = 1;
                          return;
                        }
                        else
                        {
                            Session["KetQua"] = 0;
                        }

                        if (RadGrid1.Items[i]["Business"].Text == "SAI Business" || RadGrid1.Items[i]["Business"].Text == "&nbsp;")
                        {
                            RadGrid1.Items[i]["Business"].ForeColor = System.Drawing.Color.Red;
                          Session["KetQua"] = 1;
                          return;
                        }
                        else
                        {
                            Session["KetQua"] = 0;
                        }

                        if (RadGrid1.Items[i]["Function"].Text == "SAI Function" || RadGrid1.Items[i]["Function"].Text == "&nbsp;")
                        {
                            RadGrid1.Items[i]["Function"].ForeColor = System.Drawing.Color.Red;
                          Session["KetQua"] = 1;
                          return;
                        }
                        else
                        {
                            Session["KetQua"] = 0;
                        }

                        if (RadGrid1.Items[i]["CHANNEL"].Text == "SAI CHANNEL" || RadGrid1.Items[i]["CHANNEL"].Text == "&nbsp;")
                        {
                            RadGrid1.Items[i]["CHANNEL"].ForeColor = System.Drawing.Color.Red;
                          Session["KetQua"] = 1;
                          return;
                        }
                        if (RadGrid1.Items[i]["Activity"].Text == "SAI Activity" || RadGrid1.Items[i]["Activity"].Text == "&nbsp;")
                        {
                            RadGrid1.Items[i]["Activity"].ForeColor = System.Drawing.Color.Red;
                          Session["KetQua"] = 1;
                          return;
                        }
                        else
                        {
                            Session["KetQua"] = 0;
                        }

                        if (RadGrid1.Items[i]["Brand"].Text == "SAI Brand" || RadGrid1.Items[i]["Brand"].Text == "&nbsp;")
                        {
                            RadGrid1.Items[i]["Brand"].ForeColor = System.Drawing.Color.Red;
                          Session["KetQua"] = 1;
                          return;
                        }

                        if (RadGrid1.Items[i]["CAT"].Text == "SAI Category" || RadGrid1.Items[i]["CAT"].Text == "&nbsp;")
                        {
                            RadGrid1.Items[i]["CAT"].ForeColor = System.Drawing.Color.Red;
                          Session["KetQua"] = 1;
                          return;
                        }
                        else
                        {
                            Session["KetQua"] = 0;
                        }



                    }



                }
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception e)
            {
               
                    uscMsgBox1.AddMessage(e.Message.ToString(), uc.ucMsgBox.enmMessageType.Error);
              
            }



        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int sodong=0; 
            if (   Session["KetQua"].ToString() == "1")
            {
                uscMsgBox1.AddMessage("Có lỗi trong quá trình upload dữ liệu, vui lòng kiểm tra lại file excel upload", uc.ucMsgBox.enmMessageType.Error);

             
                return;
            }
            else
            {
                try
                {

                    for (int i = 0; i < RadGrid1.Items.Count; i++)
                    {

                        Obj = new clsObj();
                        Obj.Parameter = new string[] { "@table", "@Country", "@BUSINESSUNIT", "@Function", "@Channel", "@salegroup", "@Activity", "@Brand", "@Cat", "@IO", "@Amount", "@Beginning", "@Ending", "@Quater", "@FY", "@Upcharge","@User" };
                        Obj.ValueParameter = new object[] { RadComboBox1.SelectedValue, RadGrid1.Items[i]["Country"].Text, RadGrid1.Items[i]["Business"].Text, RadGrid1.Items[i]["Function"].Text, RadGrid1.Items[i]["Channel"].Text, RadGrid1.Items[i]["salegroup"].Text  == "&nbsp;" ? "" :  RadGrid1.Items[i]["salegroup"].Text
                , RadGrid1.Items[i]["Activity"].Text, RadGrid1.Items[i]["Brand"].Text, RadGrid1.Items[i]["Cat"].Text 
            , RadGrid1.Items[i]["IO"].Text  == "&nbsp;" ? "" :  RadGrid1.Items[i]["IO"].Text   , RadGrid1.Items[i]["Amount"].Text , RadGrid1.Items[i]["From"].Text   == "&nbsp;" ? "" : RadGrid1.Items[i]["From"].Text  , RadGrid1.Items[i]["To"].Text  == "&nbsp;" ? "" : RadGrid1.Items[i]["To"].Text 
                , RadGrid1.Items[i]["quarter"].Text , RadGrid1.Items[i]["FY"].Text, RadGrid1.Items[i]["Upcharge"].Text  == "&nbsp;" ? "" : RadGrid1.Items[i]["Upcharge"].Text,Session["email"].ToString()  };
                        Obj.SpName = "sp_import_excel_To_DB";
                        Sql.fNonGetData(Obj);
                        sodong = sodong + 1;
                        Session["KetQua"] = 0;
                    }
                }
                catch (Exception ex)
                {
                    uscMsgBox1.AddMessage(ex.ToString(), uc.ucMsgBox.enmMessageType.Error);
                    return;
                }
                finally
                {
                    //flagCheck = 0;
                    Session["KetQua"] = 0;
                    uscMsgBox1.AddMessage("Đã upload thành công " +sodong.ToString()+ " dòng vào hệ thống", uc.ucMsgBox.enmMessageType.Success);
                    RadGrid1.DataSource = null;
                    RadGrid1.DataBind();
                }
            }


        }

    }
}