using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MaricoPay
{
    public partial class NewEmployee : clsPhanQuyenCaoCap
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.GetPostBackEventReference(this, string.Empty);
            //Response.Redirect("~/Login.aspx");
            if (this.IsPostBack)
            {
                string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];
                if (eventTarget == "ChildWindowPostBackPosition")
                {
                    if (Session["positionnew"] != null && Session["positionnew"]!="")
                    {
                       
                        LoadPosition(dropDepartment.SelectedValue);
                        
                        
                    }

                }
                if (cls.cToString(Session["username"]) == "")
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

            else
          //  if (!IsPostBack)
            {
                System.Web.HttpBrowserCapabilities browser = Request.Browser;

                if (browser.Browser.ToLower().Contains("explorer") == true)
                {
                    MsgBox1.AddMessage("The FIN system is not compatible with Internet Explorer, Please using Firefox or Chorme", uc.ucMsgBox.enmMessageType.Error);
                }
                else
                {
                  
                   

                        if (Session["username"] != null /*&& Request.Form["us"] != null*/)
                        // if (Request.QueryString["us"] != null)//click vao avatar
                        {
                            LoadCompany();
                            Session["company"] = dropCompany.SelectedValue;
                            LoadDerpartment();
                            Session["department"] = dropDepartment.SelectedValue;
                            LoadPosition(dropDepartment.SelectedValue);
                            LoadN1();
                            LoadSeniorList();
                            LoadVPList();
                            LoadDirectorList();
                            LoadCOOList();
                            try
                            {
                                LoadLevel();
                            }
                            catch { }
                            try
                            {
                                Area();
                            }
                            catch { }
                            
                            chkActive.Checked = true;
                            chkActive.Visible = false;
                        }
                        else
                        {
                            Response.Redirect("~/Login.aspx");
                        }
                    //}
                }
            }
        }
        private void LoadCompany()
        {
            System.Data.DataTable tbl = cls.GetDataTable("sp_loadCompany");
            dropCompany.DataSource = tbl;
            dropCompany.DataBind();
        }
        private void Area()
        {
            System.Data.DataTable tbl = cls.GetDataTable("sp_loadArea");
            dropArea.DataSource = tbl;
            dropArea.DataBind();
        }
        private void LoadVPList()
        {
          System.Data.DataTable tbl=  cls.GetDataTable("sp_getVPList");
          dropVP.DataSource = tbl;
          dropVP.DataBind();
        }
        private void LoadLevel()
        {
            System.Data.DataTable tbl = cls.GetDataTable("sp_getLevels");

            dropLevel.DataSource = tbl;
            dropLevel.DataBind();
        }
        private void LoadDirectorList()
        {
            System.Data.DataTable tbl = cls.GetDataTable("sp_getDirectorList");
            dropDirector.DataSource = tbl;
            dropDirector.DataBind();
        }
        private void LoadSeniorList()
        {
            System.Data.DataTable tbl = cls.GetDataTable("sp_getSeniorList");
            dropSenior.DataSource = tbl;
            dropSenior.DataBind();
        }
        private void LoadCOOList()
        {
            System.Data.DataTable tbl = cls.GetDataTable("sp_getCOOList");
            dropCOO.DataSource = tbl;
            dropCOO.DataBind();
        }
        private void LoadN1()
        {
            System.Data.DataTable tbl = cls.GetDataTable("sp_LoadN1New");
            dropN1.DataSource = tbl;
            dropN1.DataBind();
        }
        protected void btSave_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim()=="")
            {
                MsgBox1.AddMessage("Please fill in Email",uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (txtEmail.Text.Trim().ToLower().IndexOf("@marico.com") <0)
            {
                MsgBox1.AddMessage("Please fill in Email marico.com", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (txtEmail.Text.Trim().ToLower().IndexOf(" ") >= 0)
            {
                MsgBox1.AddMessage("The email address don't contain space character", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (txtFullName.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Please fill in Full name", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (dropCompany.SelectedValue == "")
            {
                MsgBox1.AddMessage("Please select Company", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (dropDepartment.SelectedValue == "")
            {
                MsgBox1.AddMessage("Please select Department", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (dropPosition.SelectedValue == "")
            {
                MsgBox1.AddMessage("Please select Position", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (dropN1.SelectedValue == "")
            {
                MsgBox1.AddMessage("Please select N+1", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (dropCOO.SelectedValue == "")
            {
                MsgBox1.AddMessage("Please select COO", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
           //DataTable tblchk=cls.GetDataTable("IsExistsUser",new string[] { "@username" }, new object[] { txtEmail.Text.Trim() });
           // if(tblchk.Rows.Count<=0)
           // {
                string depFK = cls.GetString0("sp_GetDepartmentFK", new string[] { "@positionpk", "@costcenter" }, new object[] { dropPosition.SelectedValue,dropDepartment.SelectedValue});
            if (depFK== null || depFK=="0")
            {
                depFK=dropDepartment.SelectedValue;
            }
            //Session["costcenter"] = comboDepartment1.Values;
                //MsgBox1.AddMessage("Update Sucessfully!", uc.ucMsgBox.enmMessageType.Success);
                if (cls.bThem(new string[] {"@Username","@Fullname","@Department_FK","@Market_FK","@Position_FK","@Email","@Recommender"
    ,"@Pass","@Active","@Function_FK","@IsManager","@EmployeeCode","@Area_FK","@Level_FK","@SignatureURL","@Company_FK","@TelPhone","@SeniorManager"
    ,"@Director","@VP_HOF","@COO","@Budget_Owner","@Costcenter","@IsN3","@vendorSAP","@UserCreate","@Autoupdate" }, new object[] {"",txtFullName.Text.Trim(),depFK,"VN",dropPosition.SelectedValue,txtEmail.Text.Trim(),dropN1.SelectedValue
    ,"123",chkActive.Checked,null,chkManager.Checked,"",dropArea.SelectedValue,dropLevel.SelectedValue,"",dropCompany.SelectedValue,"",dropSenior.SelectedValue
    ,dropDirector.SelectedValue,dropVP.SelectedValue,dropCOO.SelectedValue,null,dropDepartment.SelectedValue,chkN3.Checked,txtVendorSAP.Text.Trim(),Session["Username"],chkAuto.Checked}, "sp_InsertUsers") == true)
                {
                    MsgBox1.AddMessage("Saved successfully", uc.ucMsgBox.enmMessageType.Success);
                }
                else
                {
                    MsgBox1.AddMessage("Save fail", uc.ucMsgBox.enmMessageType.Error);
                }

            //}
            //else
            //{
            //    MsgBox1.AddMessage("The user already exists in FIN system", uc.ucMsgBox.enmMessageType.Error);
            //}
        }
        private void LoadDerpartment()
        {
            DataTable tbl = cls.GetDataTable("sp_LoadDepartMentMPayAut", new string[] { "@company_FK", "@Username", "@IDSite" }, new object[] { dropCompany.SelectedValue, Session["Username"], "NewEmployee" });
            dropDepartment.DataSource = tbl;
            dropDepartment.DataBind();
        }
        protected void dropCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDerpartment();
            Session["company"] = dropCompany.SelectedValue;
        }
        private void LoadPosition(string costcenter)
        {
            DataTable tbl = cls.GetDataTable("sp_LoadPosition", "@CostCenter", costcenter);
            dropPosition.DataSource = tbl;
            dropPosition.DataBind();
        }
        protected void dropDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            LoadPosition(dropDepartment.SelectedValue);
            Session["department"] = dropDepartment.SelectedValue;
        }

        protected void btNew_Click(object sender, EventArgs e)
        {
            txtEmail.Text = "";
            txtFullName.Text = "";
            chkManager.Checked = false;
            txtEmail.Enabled = true;
            chkActive.Checked = true;
            chkActive.Visible = false;
            dropArea.SelectedIndex = 0;
            
        }
        private void Loaddata(string email)
        {
            DataTable tbl = cls.GetDataTable("sp_getInfoCreateAut", new string[] { "@Email", "@username" }, new object[] { email.Trim(), Session["Username"] });
            //txtEmail.Text
            if (tbl.Rows.Count > 0)
            {
                txtFullName.Text = cls.cToString(tbl.Rows[0]["fullname"]);
                try
                {
                    dropCompany.SelectedValue = cls.cToString(tbl.Rows[0]["Company_FK"]);
                }
                catch
                {
                    dropCompany.SelectedValue = "1";
                }
                Session["company"] = dropCompany.SelectedValue;
                LoadDerpartment();
                chkManager.Checked = cls.cToBool(tbl.Rows[0]["IsManager"]);
                chkActive.Checked = cls.cToBool(tbl.Rows[0]["Active"]);
                chkAuto.Checked = cls.cToBool(tbl.Rows[0]["AutoUpdate"]);
                chkActive.Visible = true;
                chkN3.Checked = cls.cToBool(tbl.Rows[0]["IsN3"]);
                txtVendorSAP.Text = cls.cToString(tbl.Rows[0]["vendorSAP"]);//vendorSAP
                try
                {
                    dropDepartment.SelectedValue = cls.cToString(tbl.Rows[0]["Costcenter"]);
                    Session["department"] = dropDepartment.SelectedValue;
                }
                catch { }
                try
                {
                    LoadPosition(dropDepartment.SelectedValue);
                }
                catch { }
                try
                {
                    dropPosition.SelectedValue = cls.cToString(tbl.Rows[0]["Position_FK"]);
                }
                catch { }
                try
                {
                    dropLevel.SelectedValue = cls.cToString(tbl.Rows[0]["Level_FK"]);
                }
                catch { }
                try
                {
                    dropN1.SelectedValue = cls.cToString(tbl.Rows[0]["N1"]);
                }
                catch { }
                try
                {
                    dropSenior.SelectedValue = cls.cToString(tbl.Rows[0]["SeniorManager"]);
                }
                catch { }
                try
                {
                    dropDirector.SelectedValue = cls.cToString(tbl.Rows[0]["Director"]);
                }
                catch { }
                try
                {
                    dropVP.SelectedValue = cls.cToString(tbl.Rows[0]["VP_HOF"]);
                }
                catch { }
                try
                {
                    dropCOO.SelectedValue = cls.cToString(tbl.Rows[0]["COO"]);
                }
                catch { }
                try
                {
                    dropArea.SelectedValue = cls.cToString(tbl.Rows[0]["Area_FK"]);
                }
                catch { }
            }
            else
            {
                MsgBox1.AddMessage("Bạn không được phép xem thông tin nhân viên này hoặc nhân viên này</br> Not authorize for this", uc.ucMsgBox.enmMessageType.Error);
            }

        }
        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
             DataTable tblchk=cls.GetDataTable("IsExistsUser",new string[] { "@username" }, new object[] { txtEmail.Text.Trim() });
             if (tblchk.Rows.Count > 0)
             {
                 txtEmail.Enabled = false;
                 Loaddata(txtEmail.Text);
             }
        }
    }
}
