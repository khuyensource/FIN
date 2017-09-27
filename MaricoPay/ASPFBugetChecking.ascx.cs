using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
using Telerik.Web.UI;
using Excel = Microsoft.Office.Interop.Excel; 
namespace MaricoPay.uc
{
    public partial class BudgetChecking : System.Web.UI.UserControl
    {
        
    
        Cclass cls = new Cclass();
        clsObj Obj = new clsObj();
        clsSql Sql = new clsSql();
        clsSys sys = new clsSys();
        public DataTable dtdetail; 
        protected void Page_Load(object sender, EventArgs e)
        {

          

            if (!IsPostBack)
            {

              

                LoadCountry();
                //LoadBU();
                //LoadBrand_BU(rdcboNganhHang.SelectedValue);
                //Load_Category_ByBrand(rdCboBrand.SelectedValue);
                //Load_AccountCoding("");
                //Load_AccountCoding_Detail(Rdcode.SelectedValue);
                Load_Channel();
              
               // LoadASPF_Detail("");
              //  LoadInfoUser();
            
              
              //  ShowEmptyGrid(5);

                LoadData();
                loadReason();
            }

            loadcontrol();
        }
        void loadcontrol()
        {
            DataTable tbl = cls.GetDataTable("Sp_LoadStatus_aspf", new string[] { "@ID","@levelapp" }, new object[] { Request.QueryString["Userid"].ToString(),2 });

            if (tbl.Rows.Count <= 0)
            {
                btnSave.Visible = true;
                btnSubmit.Visible = true;
                btnLuu.Visible = true;
            }
            else
            {
                btnSave.Visible = false;
                btnSubmit.Visible = false;
                btnLuu.Visible = false;

             
            }
        }

        private void LoadData()
        {
            // ID	 					AttachFile	UserCreate	DateCreate	UserEdit	DateEdit
            DataTable tbl = cls.GetDataTable("sp_Load_ASPF", new string[] { "@ID" }, new object[] { Request.QueryString["Userid"].ToString() });
            if(tbl.Rows.Count>0)
            {
                //txtASPFNo.Text = tbl.Rows[0]["ASPNo"].ToString();
                LoadBU();
                rddateCreation.SelectedDate =  cls.cToDateTime( tbl.Rows[0]["DateCreation"].ToString());
                Rdcbocountry.SelectedValue = tbl.Rows[0]["Country_FK"].ToString();
                txtBudgetowner.Text = tbl.Rows[0]["Buget_Owner_FK"].ToString();
                rdcboNganhHang.SelectedValue = tbl.Rows[0]["BU_FK"].ToString();
                LoadBrand_BU(rdcboNganhHang.SelectedValue);
                rdCboBrand.SelectedValue = tbl.Rows[0]["Brand_FK"].ToString();
                Load_Category_ByBrand(rdCboBrand.SelectedValue);
                rdCboCategory.SelectedValue = tbl.Rows[0]["Category_FK"].ToString();

                //Rdcode.SelectedValue = tbl.Rows[0]["Code"].ToString();
                //txtGLcode.Text = tbl.Rows[0]["GLCode"].ToString();

                RddatetimeTo.SelectedDate =  cls.cToDateTime(tbl.Rows[0]["To"].ToString());
                RddatetimeFrom.SelectedDate =  cls.cToDateTime(tbl.Rows[0]["From"].ToString());
                rdChannel.SelectedValue = tbl.Rows[0]["Channel_Fk"].ToString();
                txtObjective.Text = tbl.Rows[0]["Objective"].ToString();

                rdFYBudgetCAT.Text = tbl.Rows[0]["FYbudgetcategory"].ToString();
                rdAvailable.Text = tbl.Rows[0]["Availablebudget"].ToString();
                radnumtxtASPFvalue.Text = tbl.Rows[0]["ASPFValue"].ToString();
                RadNumericTextBox1.Text = tbl.Rows[0]["ASPFValue"].ToString();

                radnumtxtBudgetBalance.Text = tbl.Rows[0]["BudgetBalance"].ToString();
                RdnumFYbudgetdepartment.Text = tbl.Rows[0]["FYbudgetcategory"].ToString();   //  tbl.Rows[0]["BudgetDepartment"].ToString();
                RdNumSpent.Text = tbl.Rows[0]["Availablebudget"].ToString();  //tbl.Rows[0]["Spent"].ToString();
                  // RdnumFYbudgetdepartment.Text=   //tbl.Rows[0]["BudgetDepartment"].ToString();
                RadNumericTextBox2.Text = tbl.Rows[0]["BudgetBalance"].ToString(); // tbl.Rows[0]["BudgetBalance_Controller"].ToString();
                  // HyperLink1.NavigateUrl = "~//ImagesUpload//" + tbl.Rows[0]["AttachFile"].ToString();
                linkfile.HRef ="~//ImagesUpload//" + tbl.Rows[0]["AttachFile"].ToString();
              
                LoadASPF_Detail(Request.QueryString["Userid"].ToString());
                loadHistory();

            }
            
          
        }
       
         void loadHistory ()
         {
             DataTable tbl = cls.GetDataTable("Sp_load_Approve_History", new string[] { "@asp_id" }, new object[] { Request.QueryString["Userid"].ToString() });
              RadGridHistory.DataSource = tbl;
              RadGridHistory.DataBind();
         }
         void loadReason()
         {
             DataTable tbl = cls.GetDataTable("sp_Load_ASPF_Reason" );
             RdcboReason.DataSource = tbl;
             RdcboReason.DataBind();
         }


        private void calculateBudgetBlance()
        {
            radnumtxtBudgetBalance.Text = ( rdAvailable.Value - radnumtxtASPFvalue.Value).ToString();
        }

        private void calculateBudgetBlanceController()
        {
            RadNumericTextBox2.Text = ( RdNumSpent.Value - RadNumericTextBox1.Value).ToString();
        }

        private double calculateAmount( double Qty, double Price)
        {
            return Qty * Price;
        }

        private void LoadCountry()
        {
           
            DataTable tbl = cls.GetDataTable("sp_LoadCountry");
            Rdcbocountry.DataSource = tbl;
            Rdcbocountry.DataBind();
        }

         private void LoadBU()
        {

            DataTable tbl = cls.GetDataTable("sp_Load_BU");
            rdcboNganhHang.DataSource = tbl;
            rdcboNganhHang.DataBind();
        }

           private void LoadBrand_BU(string bu)
        {

            DataTable tbl = cls.GetDataTable("sp_Load_Brand_ByBU", new string[] { "@BU" }, new object[] { bu });
            rdCboBrand.DataSource = tbl;
            rdCboBrand.DataBind();
        }

         private void LoadASPF_Detail(string aspf_fk)
        {

            DataTable tbl = cls.GetDataTable("sp_Load_ASPF_Detail", new string[] { "@aspf_fk" }, new object[] { aspf_fk });
            RGKienThuc.DataSource = tbl;
            RGKienThuc.DataBind();
        }

            static DataTable addNewRow(DataTable dtCode)
            {
                DataRow drNewRow = dtCode.NewRow();
                dtCode.Rows.Add(drNewRow);
                dtCode.AcceptChanges();
                return dtCode;
            }
            public void ShowEmptyGrid(int rows)
            {
                DataTable dtdetail = new DataTable();

                dtdetail.Locale = System.Globalization.CultureInfo.InvariantCulture;
                //ID,tu,den,Chucdanh,Phongban,Noilamviec,capbac 

                dtdetail.Columns.Add("ID");
                dtdetail.Columns.Add("Description");
                dtdetail.Columns.Add("Qty");
                dtdetail.Columns.Add("Price");
                dtdetail.Columns.Add("Amount");


                for (int i = 0; i < rows; i++)
                {
                    dtdetail = addNewRow(dtdetail);
                }

                ViewState["Data_Kienthuc"] = dtdetail;
                RGKienThuc.DataSource = dtdetail;
                RGKienThuc.DataBind();


            }
       
        
        private void LoadInfoUser()
        {
                //Session["email"] 

            DataTable tbl = cls.GetDataTable("sp_GetUserInfo", new string[] { "@UserName" }, new object[] {Session["email"]  });
            if (tbl.Rows.Count > 0)
            {
                txtBudgetowner.Text = tbl.Rows[0]["BudgetOwner"].ToString();
            }

        }

        protected void rdcboNganhHang_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadBrand_BU(rdcboNganhHang.SelectedValue);
            Load_Category_ByBrand(rdCboBrand.SelectedValue);
        }

        protected void RGKienThuc_DeleteCommand(object source, GridCommandEventArgs e)
        {

        }
        protected void RGKienThuc_ItemCommand(object source, GridCommandEventArgs e)
        {
        }
         private void Load_Category_ByBrand(string brand)
        {

            DataTable tbl = cls.GetDataTable("sp_Load_Category_ByBrand", new string[] { "@brand_fk" }, new object[] { brand });
            rdCboCategory.DataSource = tbl;
            rdCboCategory.DataBind();

        }


         private void Load_AccountCoding(string id) 
         {

             //DataTable tbl = cls.GetDataTable("sp_Load_AccountCoding",new string[] { "@id" }, new object[] { id});
             //Rdcode.DataSource = tbl;
             //Rdcode.DataBind();

         }

         private void Load_Channel()
         {

             DataTable tbl = cls.GetDataTable("sp_Load_Channel");
             rdChannel.DataSource = tbl;
             rdChannel.DataBind();

         }


         //private void Load_AccountCoding_Detail(string id)
         //{

         //    DataTable tbl = cls.GetDataTable("sp_Load_AccountCoding", new string[] { "@id" }, new object[] { id });
         //    if(tbl.Rows.Count>0)
         //    {
         //        txtGLcode.Text = tbl.Rows[0]["GLnumber"].ToString();
         //    }

         //}




         protected void rdCboBrand_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
         {
            // Load_Category_ByBrand(rdCboBrand.SelectedValue);
         }

         protected void Rdcode_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
         {
            // Load_AccountCoding_Detail(Rdcode.SelectedValue);
         }

         protected void btnAddrow_Click(object sender, ImageClickEventArgs e)
         {
             ShowEmptyGrid(1);


         }

         protected void RGKienThuc_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
         {
             if (IsPostBack)
             {
                 ViewState["Data_Kienthuc"] = dtdetail;
                 dtdetail = (DataTable)ViewState["Data_Kienthuc"];
                 RGKienThuc.DataSource = dtdetail;
             } 
         }

         protected void rdFYBudgetCAT_TextChanged(object sender, EventArgs e)
         {
             calculateBudgetBlance();
         }

         protected void rdAvailable_TextChanged(object sender, EventArgs e)
         {
             calculateBudgetBlance();
         }

         protected void radnumtxtASPFvalue_TextChanged(object sender, EventArgs e)
         {
             calculateBudgetBlance();
             calculateBudgetBlanceController();
         }


           protected void rdnuumsoluong_TextChanged (object sender, EventArgs e)
         {

             Calculation();

          }

           private void Calculation()
           {
               double totalASPF = 0; 
               for (int i = 0; i < RGKienThuc.Items.Count; i++)
               {


                   RadNumericTextBox rdnuumsoluong = (RGKienThuc.Items[i].FindControl("rdnuumsoluong") as RadNumericTextBox);
                   RadNumericTextBox rdnumdongia = (RGKienThuc.Items[i].FindControl("rdnumdongia") as RadNumericTextBox);
                   RadNumericTextBox rdnumthanhtien = (RGKienThuc.Items[i].FindControl("rdnumthanhtien") as RadNumericTextBox);
                   rdnumthanhtien.Text = calculateAmount(rdnuumsoluong.Value == null ? 0 : (double)rdnuumsoluong.Value, rdnumdongia.Value==null ? 0 : (double)rdnumdongia.Value).ToString();
                   totalASPF += (double)rdnumthanhtien.Value;
               }
               radnumtxtASPFvalue.Text = totalASPF.ToString();
               RadNumericTextBox1.Text = totalASPF.ToString();
               calculateBudgetBlanceController();
               calculateBudgetBlance();
           }


           protected void rdnumdongia_TextChanged(object sender, EventArgs e)
           {

               Calculation();

           }

         protected void RGKienThuc_ItemDataBound(object sender, GridItemEventArgs e)
         {

             for (int i = 0; i < RGKienThuc.Items.Count; i++)
               {


                   RadNumericTextBox rdnuumsoluong = (RGKienThuc.Items[i].FindControl("rdnuumsoluong") as RadNumericTextBox);
                   RadNumericTextBox rdnumdongia = (RGKienThuc.Items[i].FindControl("rdnumdongia") as RadNumericTextBox);
                

                     rdnuumsoluong.TextChanged += new EventHandler(rdnuumsoluong_TextChanged);

                     rdnumdongia.TextChanged += new EventHandler(rdnumdongia_TextChanged);

                     RadComboBox RadComboBox1 = (RGKienThuc.Items[i].FindControl("RadComboBox1") as RadComboBox);
                     HiddenField Hfcode = (RGKienThuc.Items[i].FindControl("Hfcode") as HiddenField);
                     if (RadComboBox1 != null)
                     {
                         DataTable tbl = cls.GetDataTable("sp_Load_AccountCoding_Bis", new string[] { "@id", "@userid" }, new object[] { "","" });
                         RadComboBox1.DataSource = tbl;
                         RadComboBox1.DataBind();

                         RadComboBox1.SelectedValue = Hfcode.Value;

                     }


                 }

         }

         protected void btnSave_Click(object sender, EventArgs e)
         {

             Session["CreateASPF"] = "Update successfull";

             if (RdcboReason.SelectedValue == "0")
             {
                 MsgBox1.AddMessage("Lý do không được rỗng / Reason not be empty ", uc.ucMsgBox.enmMessageType.Error);

                 return;
             }



             try
             {
                 Obj = new clsObj();
                 Obj.Parameter = new string[] { "@id", "@reason","@note" };
                 Obj.ValueParameter = new object[] { Request.QueryString["Userid"].ToString(), RdcboReason.SelectedValue,txtNote.Text };

                 Obj.SpName = "sp_Reject_ASPF_Controller";
                 Sql.fNonGetData(Obj);

                 btnSave.Visible = false;
                 btnSubmit.Visible = false;
                 btnLuu.Visible = false;

                 DataTable tblto = cls.GetDataTable("sp_Load_content_Bylevel", new string[] { "@docno", "@levelapp" }, new object[] { Request.QueryString["Userid"].ToString(), 2 });
                 if (tblto.Rows.Count > 0)
                 {
                     string content = sys.noidungReject(tblto.Rows[0]["nguoiduyet"].ToString(), tblto.Rows[0]["nguoitao"].ToString(), tblto.Rows[0]["Country"].ToString(), tblto.Rows[0]["BudgetOwner"].ToString(), tblto.Rows[0]["Brand"].ToString(),
                  tblto.Rows[0]["Category"].ToString(), tblto.Rows[0]["ASPNo"].ToString(), tblto.Rows[0]["ASPFvalue"].ToString(), tblto.Rows[0]["Objective"].ToString(), tblto.Rows[0]["BudgetDepartment"].ToString(), tblto.Rows[0]["Spent"].ToString(), tblto.Rows[0]["BudgetBalance_Controller"].ToString(), tblto.Rows[0]["ApprovedCode"].ToString(), Request.QueryString["Userid"].ToString(), RdcboReason.Text);

                     sys.SendMailASPAtt(tblto.Rows[0]["emailcc"].ToString(), tblto.Rows[0]["emailto"].ToString(), "Phản hồi từ " + tblto.Rows[0]["nguoiduyet"].ToString() + " về yêu cầu duyệt ASPF /Respond from " + tblto.Rows[0]["nguoiduyet"].ToString() + " for ASPF approval request ", content, tblto.Rows[0]["attachfile"].ToString() == "" ? "" : "~/ImagesUpload/" + tblto.Rows[0]["attachfile"].ToString());

                 }


                 Page.RegisterStartupScript("closeWindow", "<script type='text/javascript'>GetRadWindow().close()</script>");


             }
             catch
             {
                 MsgBox1.AddMessage("Update failed. Please try again later", uc.ucMsgBox.enmMessageType.Error);
             }



           
          

          

         }

         protected void btnSubmit_Click(object sender, EventArgs e)
         {

             Session["CreateASPF"] = "Update successfull";





             //if (txtASPFNo.Text =="")
             //{
             //    MsgBox1.AddMessage("Số ASPF không được rỗng / Number ASPF not be empty ", uc.ucMsgBox.enmMessageType.Error);

             //    return;
             //}

             if (RadNumericTextBox2.Value <= 0)
             {
                 MsgBox1.AddMessage("Ngân sách còn lại phải lớn hơn 0 / Budget balance must be greater than 0", uc.ucMsgBox.enmMessageType.Error);

                 return;
             }
            #region Upadte header
             try
             {
                 #region Upadte header
                 Obj = new clsObj();
                 Obj.Parameter = new string[] { "@ASPNo","@BudgetDepartment","@Spent","@BudgetBalance_Controller"
                                                 ,"@ASPFValue_Controller","@id"};
                 Obj.ValueParameter = new object[] {  "", RdnumFYbudgetdepartment.Text,RdNumSpent.Text,RadNumericTextBox2.Text,RadNumericTextBox1.Text,
                          Request.QueryString["Userid"].ToString()  };

                 Obj.SpName = "sp_Update_ASPF_Controller";
                 Sql.fNonGetData(Obj);

              

                 #endregion


                 Obj = new clsObj();
                 Obj.Parameter = new string[] { "@id" };
                 Obj.ValueParameter = new object[] { Request.QueryString["Userid"].ToString() };

                 Obj.SpName = "sp_Approval_Controller";
                 Sql.fNonGetData(Obj);


                 DataTable tblto = cls.GetDataTable("sp_Load_content_Bylevel", new string[] { "@docno", "@levelapp" }, new object[] { Request.QueryString["Userid"].ToString(), 2 });
                 if (tblto.Rows.Count > 0)
                 {
                     string content = sys.noidungApproval(tblto.Rows[0]["nguoiduyet"].ToString(), tblto.Rows[0]["nguoitao"].ToString(), tblto.Rows[0]["Country"].ToString(), tblto.Rows[0]["BudgetOwner"].ToString(), tblto.Rows[0]["Brand"].ToString(),
                  tblto.Rows[0]["Category"].ToString(), tblto.Rows[0]["ASPNo"].ToString(), tblto.Rows[0]["ASPFvalue"].ToString(), tblto.Rows[0]["Objective"].ToString(), tblto.Rows[0]["BudgetDepartment"].ToString(), tblto.Rows[0]["Spent"].ToString(), tblto.Rows[0]["BudgetBalance_Controller"].ToString(), tblto.Rows[0]["ApprovedCode"].ToString(), Request.QueryString["Userid"].ToString());

                     sys.SendMailASPAtt(tblto.Rows[0]["emailto"].ToString(), tblto.Rows[0]["emailcc"].ToString(), "Phản hồi từ " + tblto.Rows[0]["nguoiduyet"].ToString() + " về yêu cầu duyệt ASPF /Respond from " + tblto.Rows[0]["nguoiduyet"].ToString() + " for ASPF approval request ", content, tblto.Rows[0]["attachfile"].ToString() == "" ? "" : "~/ImagesUpload/" + tblto.Rows[0]["attachfile"].ToString());

                 }




                 DataTable tblto5555 = cls.GetDataTable("sp_Load_content_Bylevel", new string[] { "@docno", "@levelapp" }, new object[] { Request.QueryString["Userid"].ToString(), 3 });
                 if (tblto5555.Rows.Count > 0)
                 {
                     string yeucau = sys.noidungsendApproval(tblto5555.Rows[0]["nguoiduyet"].ToString(), tblto5555.Rows[0]["nguoitao"].ToString(), tblto5555.Rows[0]["Country"].ToString(), tblto5555.Rows[0]["BudgetOwner"].ToString(), tblto5555.Rows[0]["Brand"].ToString(),
                  tblto5555.Rows[0]["Category"].ToString(), tblto5555.Rows[0]["ASPNo"].ToString(), tblto5555.Rows[0]["ASPFvalue"].ToString(), tblto5555.Rows[0]["Objective"].ToString(), tblto5555.Rows[0]["BudgetDepartment"].ToString(), tblto5555.Rows[0]["Spent"].ToString(), tblto5555.Rows[0]["BudgetBalance_Controller"].ToString(), tblto5555.Rows[0]["ApprovedCode"].ToString(), Request.QueryString["Userid"].ToString(), tblto5555.Rows[0]["attachfile"].ToString());

                     sys.SendMailASPAtt(tblto5555.Rows[0]["emailto"].ToString(), "Yêu cầu duyệt ASPF từ " + tblto.Rows[0]["nguoitao"].ToString() + "/ASPF approval request from " + tblto.Rows[0]["nguoitao"].ToString(), yeucau, tblto5555.Rows[0]["attachfile"].ToString() == "" ? "" : "~/ImagesUpload/" + tblto5555.Rows[0]["attachfile"].ToString());

                 }


                 Page.RegisterStartupScript("closeWindow", "<script type='text/javascript'>GetRadWindow().close()</script>");

                 MsgBox1.AddMessage("Update successfull", uc.ucMsgBox.enmMessageType.Error);
             }
             catch
             {
                 MsgBox1.AddMessage("Update failed. Please try again later", uc.ucMsgBox.enmMessageType.Error);
             }

            #endregion

         }

         protected void Button1_Click(object sender, EventArgs e)
         {
             Session["CreateASPF"] = "Update successfull";
             Page.RegisterStartupScript("closeWindow", "<script type='text/javascript'>GetRadWindow().close()</script>");
           
         }

         protected void Button2_Click(object sender, EventArgs e)
         {
             Session["CreateASPF"] = "Update successfull";

             if (RadNumericTextBox2.Value <= 0)
             {
                 MsgBox1.AddMessage("Ngân sách còn lại phải lớn hơn 0 / Budget balance must be greater than 0", uc.ucMsgBox.enmMessageType.Error);

                 return;
             }
             //if (txtASPFNo.Text == "")
             //{
             //    MsgBox1.AddMessage("Số ASPF không được rỗng / Number ASPF not be empty ", uc.ucMsgBox.enmMessageType.Error);

             //    return;
             //}

              try
             {
                 #region Upadte header
                 Obj = new clsObj();
                 Obj.Parameter = new string[] { "@ASPNo","@BudgetDepartment","@Spent","@BudgetBalance_Controller"
                                                 ,"@ASPFValue_Controller","@id"};
                 Obj.ValueParameter = new object[] {  "", RdnumFYbudgetdepartment.Text,RdNumSpent.Text,RadNumericTextBox2.Text,RadNumericTextBox1.Text,
                          Request.QueryString["Userid"].ToString()  };

                 Obj.SpName = "sp_Update_ASPF_Controller";
                 Sql.fNonGetData(Obj);
              #endregion

                 Page.RegisterStartupScript("closeWindow", "<script type='text/javascript'>GetRadWindow().close()</script>");

                 MsgBox1.AddMessage("Update successfull", uc.ucMsgBox.enmMessageType.Error);

               
             }
             catch
             {
                 MsgBox1.AddMessage("Update failed. Please try again later", uc.ucMsgBox.enmMessageType.Error);
             }

         }

         protected void RdnumFYbudgetdepartment_TextChanged(object sender, EventArgs e)
         {
             calculateBudgetBlanceController();
         }

         protected void RdNumSpent_TextChanged(object sender, EventArgs e)
         {
             calculateBudgetBlanceController();
         }

         protected void RadNumericTextBox2_TextChanged(object sender, EventArgs e)
         {
             calculateBudgetBlanceController();
         }

       

    }

}
