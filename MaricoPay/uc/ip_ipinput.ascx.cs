using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using Data;
namespace MaricoPay.uc
{
    public partial class ip_ipinput : System.Web.UI.UserControl
    {

        Cclass cls = new Cclass();
        clsObj Obj = new clsObj();
        clsSql Sql = new clsSql();
        clsSys sys = new clsSys();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["email"] != null)
            {

             
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        
        }
        public int fInt(object value)
        {
            if (value.ToString() == "")
            {
                return 0;
            }
            return int.Parse(value.ToString());
        }
        public bool fBool(object value)
        {
            if (value.ToString() == "" || value.ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string fGet(object IDPRDetail, object IDPR, object IDMaterial)
        {
            HFIDPR.Value = IDPR.ToString();
            HFIDPRDetail.Value = IDPRDetail.ToString();
            HfIDMaterial.Value = IDMaterial.ToString();
            decimal dIDPRDetail = cls.cToDecimal(IDPRDetail);
            if (dIDPRDetail > 0)
            {
                DataTable tbl = cls.GetDataTable("[sp_IP_LoadVenderBy_IDPRDetail]", new string[] { "@IDPRDetail" }, new object[] { dIDPRDetail });
                RGNCC.DataSource = tbl;
                RGNCC.DataBind();
            }

            return "";
        }

        protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                if (chkApdung.Checked)  // ap dung cho nhieu ma san pham tuong tu   
                {
                    foreach (GridDataItem item in RGNCC.MasterTableView.Items)
                    {
                        CheckBox CheckBox1 = item.FindControl("CheckBox1") as CheckBox;
                        CheckBox CheckBox2 = item.FindControl("CheckBox2") as CheckBox;
                        string strKey = item.GetDataKeyValue("ID").ToString();
                         if (CheckBox1 != null && CheckBox1.Checked && CheckBox2 != null && CheckBox2.Checked)
                         {
                            
                            #region resend vao bang IPPRVendor  IP_Resend_IPPRVendor_All
                            Obj = new clsObj();
                            Obj.Parameter = new string[] { "@IDPR", "@EmailVendor", "@idvendor", "@EmailIPRequest", "@IDMaterial" }; //Session["Email"]
                            Obj.ValueParameter = new object[] { HFIDPR.Value, item["Email"].Text == "&nbsp;" ? "" : item["Email"].Text , strKey, Session["Email"], HfIDMaterial.Value };

                            //  Obj.ValueOutput = new string[] { "0" };
                            Obj.SpName = "IP_Resend_IPPRVendor_All";
                            Sql.fNonGetData(Obj);
                            //    Session["ID_ASPF"] = int.Parse(Obj.ValueOutput[0].ToString());
                            #endregion
                         }
                      
                        if (CheckBox1 != null && CheckBox1.Checked)
                        {

                            // HiddenField1.Value;  fGet(Eval("IDPRDetail"),Eval("IDPR")

                            #region Insert vao bang IPPRVendor  IP_Insert_IPPRVendor_All
                            Obj = new clsObj();
                            Obj.Parameter = new string[] { "@IDPR", "@EmailVendor", "@idvendor", "@EmailIPRequest", "@IDMaterial", "@songaygoibaogia" }; //Session["Email"]
                            Obj.ValueParameter = new object[] { HFIDPR.Value, item["Email"].Text=="&nbsp;" ? "" :  item["Email"].Text  , strKey, Session["Email"], HfIDMaterial.Value,radnumNgaybaogia.Value
                        };

                            //  Obj.ValueOutput = new string[] { "0" };
                            Obj.SpName = "IP_Insert_IPPRVendor_All";
                            Sql.fNonGetData(Obj);
                            //    Session["ID_ASPF"] = int.Parse(Obj.ValueOutput[0].ToString());
                            #endregion
                        }
                        else if (CheckBox1 != null && !CheckBox1.Checked)
                        {
                           
                            #region delete vao bang IPPRVendor  IP_delete_IPPRVendor_Temp
                            Obj = new clsObj();
                            Obj.Parameter = new string[] { "@IDPR", "@EmailVendor", "@idvendor", "@EmailIPRequest", "@IDMaterial" }; //Session["Email"]
                            Obj.ValueParameter = new object[] { HFIDPR.Value, item["Email"].Text == "&nbsp;" ? "" : item["Email"].Text , strKey, Session["Email"], HfIDMaterial.Value };

                            //  Obj.ValueOutput = new string[] { "0" };
                            Obj.SpName = "IP_delete_IPPRVendor_All";
                            Sql.fNonGetData(Obj);
                            //    Session["ID_ASPF"] = int.Parse(Obj.ValueOutput[0].ToString());
                            #endregion
                        }
                    }
                    return;
                }
                else
                {
                    foreach (GridDataItem item in RGNCC.MasterTableView.Items)
                    {
                        string strKey = item.GetDataKeyValue("ID").ToString();
                        CheckBox CheckBox1 = item.FindControl("CheckBox1") as CheckBox;
                        CheckBox CheckBox2 = item.FindControl("CheckBox2") as CheckBox;
                        if (CheckBox1 != null && CheckBox1.Checked && CheckBox2 != null && CheckBox2.Checked)
                        {

                            #region resend vao bang IPPRVendor  IP_delete_IPPRVendor_Temp
                            Obj = new clsObj();
                            Obj.Parameter = new string[] { "@IDPR", "@IDPRDetail", "@idvendor", }; //Session["Email"]
                            Obj.ValueParameter = new object[] { HFIDPR.Value, HFIDPRDetail.Value, strKey };

                            //  Obj.ValueOutput = new string[] { "0" };
                            Obj.SpName = "[IP_Resend_IPPRVendor_Temp]";
                            Sql.fNonGetData(Obj);
                            //    Session["ID_ASPF"] = int.Parse(Obj.ValueOutput[0].ToString());
                            #endregion
                        }

                  
                        if (CheckBox1 != null && CheckBox1.Checked)
                        {

                            // HiddenField1.Value;  fGet(Eval("IDPRDetail"),Eval("IDPR")

                            #region Insert vao bang IPPRVendor  IP_Insert_IPPRVendor_Temp
                            Obj = new clsObj();
                            Obj.Parameter = new string[] { "@IDPR", "@IDPRDetail", "@EmailVendor", "@idvendor", "@EmailIPRequest","@songaygoibaogia" }; //Session["Email"]
                            Obj.ValueParameter = new object[] { HFIDPR.Value, HFIDPRDetail.Value, item["Email"].Text == "&nbsp;" ? "" : item["Email"].Text, strKey, Session["Email"],radnumNgaybaogia.Value };

                            //  Obj.ValueOutput = new string[] { "0" };
                            Obj.SpName = "IP_Insert_IPPRVendor_Temp";
                            Sql.fNonGetData(Obj);
                            //    Session["ID_ASPF"] = int.Parse(Obj.ValueOutput[0].ToString());
                            #endregion
                        }
                        else if (CheckBox1 != null && !CheckBox1.Checked)
                        {

                            #region delete vao bang IPPRVendor  IP_delete_IPPRVendor_Temp
                            Obj = new clsObj();
                            Obj.Parameter = new string[] { "@IDPR", "@IDPRDetail", "@idvendor", }; //Session["Email"]
                            Obj.ValueParameter = new object[] { HFIDPR.Value, HFIDPRDetail.Value, strKey };

                            //  Obj.ValueOutput = new string[] { "0" };
                            Obj.SpName = "IP_delete_IPPRVendor_Temp";
                            Sql.fNonGetData(Obj);
                            //    Session["ID_ASPF"] = int.Parse(Obj.ValueOutput[0].ToString());
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message+ "');", true);

            }
            finally
            {

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Đã lưu thành công/Saved successfully');", true);

               // lbthongbao.Text = "Đã lưu thành công/Saved successfully";
            }       
        }



    }
}

