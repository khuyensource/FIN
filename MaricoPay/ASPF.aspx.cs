using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Linq;
using MaricoPay.DB;
using System.IO;
using System.Collections.Specialized;
using Data;
using Telerik.Web.UI;

namespace MaricoPay
{
    public partial class ASPF : System.Web.UI.Page
    {

        Cclass cls = new Cclass();
        clsObj Obj = new clsObj();
        clsSql Sql = new clsSql();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["email"] != null )
            {
                if (!IsPostBack)
                {
                    LoadGird();
                }

                if (Session["CreateASPF"] != null)
                {
                    LoadGird();
                }


            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
           
             

            Session["CreateASPF"] = null;
            
        }
        

        private void LoadGird()
        {
            //Session["email"] 
            DataTable tbl = cls.GetDataTable("Sp_load_aspf_ByUser", new string[] { "@usercreate" }, new object[] { Session["email"].ToString() });
            RadGrid2.DataSource = tbl;
            RadGrid2.DataBind();
        }
        protected void btnaddsubgoal_Click(object sender, EventArgs e)
        {
            //  RGSubgoal.Visible = false;


           
         

        }

        protected void RadGrid2_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string strKey = "";
            if (e.CommandName == Telerik.Web.UI.RadGrid.InitInsertCommandName)
            {
                RadGrid2.MasterTableView.ClearEditItems();
                e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/CreateASPF.ascx";
                e.Item.OwnerTableView.InsertItem();

                Session["CreateASPF"] = "CreateASPF";
                LoadGird();

                Session["CreateASPF"] = "CreateASPF";
               
            }
            if (e.CommandName == Telerik.Web.UI.RadGrid.EditCommandName)
            {
                RadGrid2.MasterTableView.IsItemInserted = false;
                e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/CreateASPF.ascx";
                LoadGird();
            }
            if (e.CommandName == "Cancel")
            {

                LoadGird();
            }
            if (e.CommandName == "Copy")
            {

                GridDataItem item = e.Item as GridDataItem;
                item.Selected = true;
                 strKey = item.GetDataKeyValue("ID").ToString();


                try
                {
                    #region Insert header
                    Obj = new clsObj();
                    Obj.Parameter = new string[] { "@id" };
                    Obj.ValueParameter = new object[] { strKey };
                    Obj.ParameterOutput = new string[] { "@IDout" };
                    Obj.ValueOutput = new string[] { "0" };
                    Obj.SpName = "sp_Copy_ASPF";
                    Sql.fNonGetData_Out(Obj);
                    Session["ID_ASPF"] = int.Parse(Obj.ValueOutput[0].ToString());


                    #endregion
                }
                finally
                {

                    #region Insert Detail

                    Obj = new clsObj();
                    Obj.Parameter = new string[] { "@id", "@id_old" };
                    Obj.ValueParameter = new object[] { Session["ID_ASPF"], strKey };

                    Obj.SpName = "sp_Copy_ASPF_Detail";
                    Sql.fNonGetData(Obj);
                    //  Session["ID_ASPF_Detail"] = int.Parse(Obj.ValueOutput[0].ToString());
                    #endregion
                }
                LoadGird();

            }

        }

        protected void RadGrid2_InsertCommand(object sender, GridCommandEventArgs e)
        {
            Session["CreateASPF"] = "CreateASPF";
        }

        protected void RadGrid2_ItemCreated(object sender, GridItemEventArgs e)
        {

            


          

        }

        protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                HyperLink editLink = (HyperLink)e.Item.FindControl("LinkDetail");
                HyperLink LinkPrint = (HyperLink)e.Item.FindControl("LinkPrint");

                HiddenField HFID = (HiddenField)e.Item.FindControl("HFID");
                 HiddenField hdRevise = (HiddenField)e.Item.FindControl("hdRevise");

                
                HiddenField HFStatus = (HiddenField)e.Item.FindControl("HFStatus");


                      HyperLink LinkRevise = (HyperLink)e.Item.FindControl("LinkRevise");
                   

                if (HFStatus.Value == "0")
                {
                    CheckBox CheckBox1 = (CheckBox)e.Item.FindControl("chkSelect");
                    CheckBox1.Visible = true;
                }
                else
                {
                    CheckBox CheckBox1 = (CheckBox)e.Item.FindControl("chkSelect");
                    CheckBox1.Visible = false;
                }


                if (hdRevise.Value == "0" || hdRevise.Value == "")
                {

                    DataTable tblto8881 = cls.GetDataTable("sp_Check_Approval_Revise", new string[] { "@docno" }, new object[] { HFID.Value });
                    /////////////////// duyet het roi ////////////////
                    if (tblto8881.Rows.Count > 0)
                    {
                        LinkRevise.Visible = true;


                    }
                    else
                    {
                        LinkRevise.Visible = false;
                    }
                  

                }
                else
                {
                    LinkRevise.Visible = false;

                    if (hdRevise.Value != "")
                    {
                        GridDataItem item = (GridDataItem)e.Item;
                        TableCell cell = (TableCell)item["ID"];
                        cell.BackColor = System.Drawing.Color.Red;
                        LinkRevise.Visible = false;
                        LinkRevise.Attributes.Add("onclick", "this.disabled=true");
                        LinkRevise.Attributes.Add("onclick", "this.style.display='none';");
                        LinkRevise.Attributes["OnClick"] = "return false;";

                       // LinkRevise.Text = hdRevise.Value;
                    }

                }

              

                editLink.Attributes["href"] = "#";
                editLink.Attributes["onclick"] = String.Format("return openRadWindowPreview('{0}');", HFID.Value);

                LinkPrint.Attributes["href"] = "#";
                LinkPrint.Attributes["onclick"] = String.Format("return openRadprintPreview('{0}');", HFID.Value);
            
                     LinkRevise.Attributes["href"] = "#";
                LinkRevise.Attributes["onclick"] = String.Format("return openRadResive('{0}');", HFID.Value);

                

                

            }

        }

        protected void btnXoa_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in RadGrid2.MasterTableView.Items)
            {
                CheckBox CheckBox1 = item.FindControl("chkSelect") as CheckBox;
                if (CheckBox1 != null && CheckBox1.Checked ==true)
                {
                    string strKey = item.GetDataKeyValue("ID").ToString();
                    cls.ExcuteSQL("Sp_Delete_aspf", new string[] { "@ID" }, new object[] { strKey });
                   
                }
            }
            LoadGird();
           
        }

    }
}