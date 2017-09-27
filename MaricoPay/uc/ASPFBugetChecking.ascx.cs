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
           
                Load_Channel();
         
                Load_typeofBudget();
 
                LoadData();
                loadReason();
                loadbudget();
            }
            calculateQuater(int.Parse(RddatetimeFrom.SelectedDate.Value.Month.ToString()), int.Parse(RddatetimeTo.SelectedDate.Value.Month.ToString()));
          //  LoadInfoUser();
            loadcontrol();
        }

        protected void M1_TextChanged(object sender, EventArgs e)
        {
            SumAmountbyQuater();
        }
        protected void M2_TextChanged(object sender, EventArgs e)
        {
            SumAmountbyQuater();
        }
        protected void M3_TextChanged(object sender, EventArgs e)
        {
            SumAmountbyQuater();
        }
        protected void M4_TextChanged(object sender, EventArgs e)
        {
            SumAmountbyQuater();
        }
        protected void M5_TextChanged(object sender, EventArgs e)
        {
            SumAmountbyQuater();
        }
        protected void M6_TextChanged(object sender, EventArgs e)
        {
            SumAmountbyQuater();
        }
        protected void M7_TextChanged(object sender, EventArgs e)
        {
            SumAmountbyQuater();
        }
        protected void M8_TextChanged(object sender, EventArgs e)
        {
            SumAmountbyQuater();
        }
        protected void M9_TextChanged(object sender, EventArgs e)
        {
            SumAmountbyQuater();
        }
        protected void M10_TextChanged(object sender, EventArgs e)
        {
            SumAmountbyQuater();
        }

        protected void M11_TextChanged(object sender, EventArgs e)
        {
            SumAmountbyQuater();
        }
        protected void M12_TextChanged(object sender, EventArgs e)
        {
            SumAmountbyQuater();
        }

        void SumAmountbyQuater()
        {
            double q1; double q2; double q3; double q4;
            q1 = 0; q2 = 0; q3 = 0; q4 = 0;
            for (int i = 0; i < RGKienThuc.Items.Count; i++)
            {


                RadNumericTextBox M1 = (RGKienThuc.Items[i].FindControl("M1") as RadNumericTextBox);
                RadNumericTextBox M2 = (RGKienThuc.Items[i].FindControl("M2") as RadNumericTextBox);
                RadNumericTextBox M3 = (RGKienThuc.Items[i].FindControl("M3") as RadNumericTextBox);
                RadNumericTextBox M4 = (RGKienThuc.Items[i].FindControl("M4") as RadNumericTextBox);
                RadNumericTextBox M5 = (RGKienThuc.Items[i].FindControl("M5") as RadNumericTextBox);
                RadNumericTextBox M6 = (RGKienThuc.Items[i].FindControl("M6") as RadNumericTextBox);
                RadNumericTextBox M7 = (RGKienThuc.Items[i].FindControl("M7") as RadNumericTextBox);
                RadNumericTextBox M8 = (RGKienThuc.Items[i].FindControl("M8") as RadNumericTextBox);
                RadNumericTextBox M9 = (RGKienThuc.Items[i].FindControl("M9") as RadNumericTextBox);
                RadNumericTextBox M10 = (RGKienThuc.Items[i].FindControl("M10") as RadNumericTextBox);
                RadNumericTextBox M11 = (RGKienThuc.Items[i].FindControl("M11") as RadNumericTextBox);
                RadNumericTextBox M12 = (RGKienThuc.Items[i].FindControl("M12") as RadNumericTextBox);

                RadNumericTextBox rdnumthanhtienQ1 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ1") as RadNumericTextBox);
                RadNumericTextBox rdnumthanhtienQ2 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ2") as RadNumericTextBox);
                RadNumericTextBox rdnumthanhtienQ3 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ3") as RadNumericTextBox);
                RadNumericTextBox rdnumthanhtienQ4 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ4") as RadNumericTextBox);

                q1 = q1 + (double)(M4.Value == null ? 0 : M4.Value) + (double)(M5.Value == null ? 0 : M5.Value) + (double)(M6.Value == null ? 0 : M6.Value);
                q2 = q2 + (double)(M7.Value == null ? 0 : M7.Value) + (double)(M8.Value == null ? 0 : M8.Value) + (double)(M9.Value == null ? 0 : M9.Value);
                q3 = q3 + (double)(M10.Value == null ? 0 : M10.Value) + (double)(M11.Value == null ? 0 : M11.Value) + (double)(M12.Value == null ? 0 : M12.Value);
                q4 = q4 + (double)(M1.Value == null ? 0 : M1.Value) + (double)(M2.Value == null ? 0 : M2.Value) + (double)(M3.Value == null ? 0 : M3.Value);

                rdnumthanhtienQ1.Value = q1;
                rdnumthanhtienQ2.Value = q2;
                rdnumthanhtienQ3.Value = q3;
                rdnumthanhtienQ4.Value = q4;

                //  RadNumericTextBox rdnumthanhtienQ5 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ5") as RadNumericTextBox);
                //  RadNumericTextBox rdnumdongia = (RGKienThuc.Items[i].FindControl("rdnumdongia") as RadNumericTextBox);
            }
            //   totalASPF += (rdnumthanhtienQ1.Value == null ? 0 : (double)rdnumthanhtienQ1.Value) + (rdnumthanhtienQ2.Value == null ? 0 : (double)rdnumthanhtienQ2.Value) + (rdnumthanhtienQ3.Value == null ? 0 : (double)rdnumthanhtienQ3.Value) + (rdnumthanhtienQ4.Value == null ? 0 : (double)rdnumthanhtienQ4.Value);

            radnumtxtASPFvalue.Text = (q1 + q2 + q3 + q4).ToString(); ;

            //   Calculation();
        }

        private void calculateQuater(int start, int end)
        {
            if (start <= end)
            {

                for (int i = start; i <= end; i++)
                {
                    if (i == 1)
                        RGKienThuc.MasterTableView.GetColumn("M1").Display = true;
                    else if (i == 2) RGKienThuc.MasterTableView.GetColumn("M2").Display = true;
                    else if (i == 3) RGKienThuc.MasterTableView.GetColumn("M3").Display = true;
                    else if (i == 4) RGKienThuc.MasterTableView.GetColumn("M4").Display = true;
                    else if (i == 5) RGKienThuc.MasterTableView.GetColumn("M5").Display = true;
                    else if (i == 6) RGKienThuc.MasterTableView.GetColumn("M6").Display = true;
                    else if (i == 7) RGKienThuc.MasterTableView.GetColumn("M7").Display = true;
                    else if (i == 8) RGKienThuc.MasterTableView.GetColumn("M8").Display = true;
                    else if (i == 9) RGKienThuc.MasterTableView.GetColumn("M9").Display = true;
                    else if (i == 10) RGKienThuc.MasterTableView.GetColumn("M10").Display = true;
                    else if (i == 11) RGKienThuc.MasterTableView.GetColumn("M11").Display = true;
                    else if (i == 12) RGKienThuc.MasterTableView.GetColumn("M12").Display = true;

                }
            }
            else
            {
                for (int i = start; i <= 12; i++)
                {
                    if (i == 1)
                        RGKienThuc.MasterTableView.GetColumn("M1").Display = true;
                    else if (i == 2) RGKienThuc.MasterTableView.GetColumn("M2").Display = true;
                    else if (i == 3) RGKienThuc.MasterTableView.GetColumn("M3").Display = true;
                    else if (i == 4) RGKienThuc.MasterTableView.GetColumn("M4").Display = true;
                    else if (i == 5) RGKienThuc.MasterTableView.GetColumn("M5").Display = true;
                    else if (i == 6) RGKienThuc.MasterTableView.GetColumn("M6").Display = true;
                    else if (i == 7) RGKienThuc.MasterTableView.GetColumn("M7").Display = true;
                    else if (i == 8) RGKienThuc.MasterTableView.GetColumn("M8").Display = true;
                    else if (i == 9) RGKienThuc.MasterTableView.GetColumn("M9").Display = true;
                    else if (i == 10) RGKienThuc.MasterTableView.GetColumn("M10").Display = true;
                    else if (i == 11) RGKienThuc.MasterTableView.GetColumn("M11").Display = true;
                    else if (i == 12) RGKienThuc.MasterTableView.GetColumn("M12").Display = true;

                }

                for (int i = 1; i <= end; i++)
                {
                    if (i == 1)
                        RGKienThuc.MasterTableView.GetColumn("M1").Display = true;
                    else if (i == 2) RGKienThuc.MasterTableView.GetColumn("M2").Display = true;
                    else if (i == 3) RGKienThuc.MasterTableView.GetColumn("M3").Display = true;
                    else if (i == 4) RGKienThuc.MasterTableView.GetColumn("M4").Display = true;
                    else if (i == 5) RGKienThuc.MasterTableView.GetColumn("M5").Display = true;
                    else if (i == 6) RGKienThuc.MasterTableView.GetColumn("M6").Display = true;
                    else if (i == 7) RGKienThuc.MasterTableView.GetColumn("M7").Display = true;
                    else if (i == 8) RGKienThuc.MasterTableView.GetColumn("M8").Display = true;
                    else if (i == 9) RGKienThuc.MasterTableView.GetColumn("M9").Display = true;
                    else if (i == 10) RGKienThuc.MasterTableView.GetColumn("M10").Display = true;
                    else if (i == 11) RGKienThuc.MasterTableView.GetColumn("M11").Display = true;
                    else if (i == 12) RGKienThuc.MasterTableView.GetColumn("M12").Display = true;
                }

            }
            int quarterNumber = 0;
            if (start <= end)
            {
                for (int i = start; i <= end; i++)
                {



                    if (i >= 4 && i <= 6)
                        quarterNumber = 1;
                    else if (i >= 7 && i <= 9)
                        quarterNumber = 2;
                    else if (i >= 10 && i <= 12)
                        quarterNumber = 3;
                    else
                        if (i >= 1 && i <= 3)
                            quarterNumber = 4;



                    switch (quarterNumber)
                    {
                        case 1:
                            //  RGKienThuc.MasterTableView.GetColumn("Q1").Display = true;
                            RGKienThuc.MasterTableView.GetColumn("A1").Display = true;


                            break;
                        case 2:
                            //    RGKienThuc.MasterTableView.GetColumn("Q2").Display = true;
                            RGKienThuc.MasterTableView.GetColumn("A2").Display = true;

                            break;
                        case 3:
                            //    RGKienThuc.MasterTableView.GetColumn("Q3").Display = true;
                            RGKienThuc.MasterTableView.GetColumn("A3").Display = true;


                            break;

                        case 4:
                            //  RGKienThuc.MasterTableView.GetColumn("Q4").Display = true;
                          RGKienThuc.MasterTableView.GetColumn("A4").Display = true;

                            break;




                    }
                }
            }
            else
            {

                for (int i = start; i <= 12; i++)
                {
                    if (i >= 4 && i <= 6)
                        quarterNumber = 1;
                    else if (i >= 7 && i <= 9)
                        quarterNumber = 2;
                    else if (i >= 10 && i <= 12)
                        quarterNumber = 3;
                    else
                        if (i >= 1 && i <= 3)
                            quarterNumber = 4;
                    switch (quarterNumber)
                    {
                        case 1:
                            // RGKienThuc.MasterTableView.GetColumn("Q1").Display = true;
                          RGKienThuc.MasterTableView.GetColumn("A1").Display = true;
                            break;
                        case 2:
                            // RGKienThuc.MasterTableView.GetColumn("Q2").Display = true;
                          RGKienThuc.MasterTableView.GetColumn("A2").Display = true;

                            break;
                        case 3:
                            //  RGKienThuc.MasterTableView.GetColumn("Q3").Display = true;
                           RGKienThuc.MasterTableView.GetColumn("A3").Display = true;

                            break;

                        case 4:
                            //  RGKienThuc.MasterTableView.GetColumn("Q4").Display = true;
                          RGKienThuc.MasterTableView.GetColumn("A4").Display = true;

                            break;

                    }
                }

                if (end >= 4 && end <= 6)
                    quarterNumber = 1;
                else if (end >= 7 && end <= 9)
                    quarterNumber = 2;
                else if (end >= 10 && end <= 12)
                    quarterNumber = 3;
                else
                    if (end >= 1 && end <= 3)
                        quarterNumber = 4;
                switch (quarterNumber)
                {
                    case 1:
                        //  RGKienThuc.MasterTableView.GetColumn("Q1").Display = true;
                        RGKienThuc.MasterTableView.GetColumn("A1").Display = true;
                        break;
                    case 2:
                        // RGKienThuc.MasterTableView.GetColumn("Q2").Display = true;
                        RGKienThuc.MasterTableView.GetColumn("A2").Display = true;

                        break;
                    case 3:
                        //  RGKienThuc.MasterTableView.GetColumn("Q3").Display = true;
                        RGKienThuc.MasterTableView.GetColumn("A3").Display = true;

                        break;

                    case 4:
                        //  RGKienThuc.MasterTableView.GetColumn("Q4").Display = true;
                        RGKienThuc.MasterTableView.GetColumn("A4").Display = true;

                        break;

                }


            }

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
        private void LoadBU(string Country_fk)
        {

            DataTable tbl = cls.GetDataTable("sp_Load_BU", new string[] { "@Country_fk" }, new object[] { Country_fk });
            rdcboNganhHang.DataSource = tbl;
            rdcboNganhHang.DataBind();
        }
        private void Loaddetail_Quater(RadComboBox rdCboBrand, RadComboBox rdCboCategory, RadNumericTextBox A1, RadNumericTextBox A2, RadNumericTextBox A3, RadNumericTextBox A4, RadComboBox RadComboBox1, double BookA1, double BookA2, double BookA3, double BookA4)
        {
            int monthStart, MonthEnd, yearstart, yeadend, quarterNumber1, quarterNumber2;
            monthStart = int.Parse(RddatetimeFrom.SelectedDate.Value.Month.ToString());
            MonthEnd = int.Parse(RddatetimeTo.SelectedDate.Value.Month.ToString());
            yearstart = int.Parse(RddatetimeFrom.SelectedDate.Value.Year.ToString());
            yeadend = int.Parse(RddatetimeTo.SelectedDate.Value.Year.ToString());
            if (monthStart >= 4 && monthStart <= 6)
                quarterNumber1 = 1;
            else if (monthStart >= 7 && monthStart <= 9)
                quarterNumber1 = 2;
            else if (monthStart >= 10 && monthStart <= 12)
                quarterNumber1 = 3;
            else
                quarterNumber1 = 4;
            ///----------------------/////////

            if (MonthEnd >= 4 && MonthEnd <= 6)
                quarterNumber2 = 1;
            else if (MonthEnd >= 7 && MonthEnd <= 9)
                quarterNumber2 = 2;
            else if (MonthEnd >= 10 && MonthEnd <= 12)
                quarterNumber2 = 3;
            else
                quarterNumber2 = 4;
            for (int i = quarterNumber1; i <= quarterNumber2; i++)
            {
              
                if (i == 1)
                    A1.Value = calculationAvailable_quater(int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value), int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue), 1, RddatetimeTo.SelectedDate.Value) ;
                if (i == 2)
                    A2.Value = calculationAvailable_quater(int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value), int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue), 2, RddatetimeTo.SelectedDate.Value) ;
                if (i == 3)
                    A3.Value = calculationAvailable_quater(int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value), int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue), 3, RddatetimeTo.SelectedDate.Value) ;
                if (i == 4)
                    A4.Value = calculationAvailable_quater(int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value), int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue), 4, RddatetimeTo.SelectedDate.Value) ;
            }

         
        }


        private void LoadData()
        {
            // ID	 						BudgetDepartment	Spent	BudgetBalance_Controller	ASPFValue_Controller	AttachFile	UserCreate	DateCreate	UserEdit	DateEdit
            DataTable tbl = cls.GetDataTable("sp_Load_ASPF", new string[] { "@ID" }, new object[] { Request.QueryString["Userid"].ToString() });
            if (tbl.Rows.Count > 0)
            {
                LoadBU(Rdcbocountry.SelectedValue);


                //txtASPFNo.Text = tbl.Rows[0]["ASPNo"].ToString();
                rddateCreation.SelectedDate = cls.cToDateTime(tbl.Rows[0]["DateCreation"].ToString());
                Rdcbocountry.SelectedValue = tbl.Rows[0]["Country_FK"].ToString();
                txtBudgetowner.Text = tbl.Rows[0]["BudgetOwner"].ToString();
                rdcboNganhHang.SelectedValue = tbl.Rows[0]["BU_FK"].ToString();
                rdTypeofBudget.SelectedValue = tbl.Rows[0]["Upcharge"].ToString();
                hfBudgetowner.Value = tbl.Rows[0]["Buget_Owner_FK"].ToString();
                RddatetimeTo.SelectedDate = cls.cToDateTime(tbl.Rows[0]["To"].ToString());
                RddatetimeFrom.SelectedDate = cls.cToDateTime(tbl.Rows[0]["From"].ToString());
                rdChannel.SelectedValue = tbl.Rows[0]["Channel_Fk"].ToString();
                txtObjective.Text = tbl.Rows[0]["Objective"].ToString();
                //  rdFYBudgetCAT.Text = tbl.Rows[0]["FYbudgetcategory"].ToString();
                //   rdAvailable.Text = tbl.Rows[0]["Availablebudget"].ToString();
                radnumtxtASPFvalue.Text = tbl.Rows[0]["ASPFValue"].ToString();
                //   radnumtxtBudgetBalance.Text = tbl.Rows[0]["BudgetBalance"].ToString();
                linkfile.HRef = "~//ImagesUpload//" + tbl.Rows[0]["AttachFile"].ToString();
                LoadASPF_Detail(Request.QueryString["Userid"].ToString());
              //  Loadregion(rdChannel.SelectedValue);
                //rdRegion.SelectedValue = tbl.Rows[0]["region"].ToString();

                loadHistory();

            }


        }
        private void Load_typeofBudget()
        {

            DataTable tbl = cls.GetDataTable("sp_Load_TypeOfBudget");
            rdTypeofBudget.DataSource = tbl;
            rdTypeofBudget.DataBind();

        }

        //private void Loadregion(string channel)
        //{
        //    DataTable tbl = cls.GetDataTable("sp_Load_region", new string[] { "@channel_fk" }, new object[] { channel });

        //    rdRegion.DataSource = tbl;
        //    rdRegion.DataBind();
        //}
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
            //radnumtxtBudgetBalance.Text = ( rdAvailable.Value - radnumtxtASPFvalue.Value).ToString();
        }

        private void calculateBudgetBlanceController()
        {
          //  RadNumericTextBox2.Text = ( RdNumSpent.Value - RadNumericTextBox1.Value).ToString();
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

       

        //   private void LoadBrand_BU(string bu)
        //{

        //    DataTable tbl = cls.GetDataTable("sp_Load_Brand_ByBU", new string[] { "@BU" }, new object[] { bu });
        //    rdCboBrand.DataSource = tbl;
        //    rdCboBrand.DataBind();
        //}

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
          //  LoadBrand_BU(rdcboNganhHang.SelectedValue);
            //Load_Category_ByBrand(rdCboBrand.SelectedValue);
        }

        protected void RGKienThuc_DeleteCommand(object source, GridCommandEventArgs e)
        {

        }
        protected void RGKienThuc_ItemCommand(object source, GridCommandEventArgs e)
        {
        }
        // private void Load_Category_ByBrand(string brand)
        //{

        //    DataTable tbl = cls.GetDataTable("sp_Load_Category_ByBrand", new string[] { "@brand_fk" }, new object[] { brand });
        //    rdCboCategory.DataSource = tbl;
        //    rdCboCategory.DataBind();

        //}


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
           //    RadNumericTextBox1.Text = totalASPF.ToString();
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


                   //  RadNumericTextBox rdnuumsoluong = (RGKienThuc.Items[i].FindControl("rdnuumsoluong") as RadNumericTextBox);
                   //  RadNumericTextBox rdnumdongia = (RGKienThuc.Items[i].FindControl("rdnumdongia") as RadNumericTextBox);
                   // RadComboBox RadComboBox1 = (RGKienThuc.Items[i].FindControl("RadComboBox1") as RadComboBox);
                   //   HiddenField Hfcode = (RGKienThuc.Items[i].FindControl("Hfcode") as HiddenField); 
                   //   rdnuumsoluong.TextChanged += new EventHandler(rdnuumsoluong_TextChanged);
                   //  rdnumdongia.TextChanged += new EventHandler(rdnumdongia_TextChanged);

                   RadComboBox RadComboBox1 = (RGKienThuc.Items[i].FindControl("RadComboBox1") as RadComboBox);
                   HiddenField Hfcode = (RGKienThuc.Items[i].FindControl("Hfcode") as HiddenField);  // Session["username"] upddate ngay 22/11
                   if (RadComboBox1 != null)
                   {
                       DataTable tbl = cls.GetDataTable("sp_Load_AccountCoding_form", new string[] { "@id", "@userid" }, new object[] { "", "" });
                       RadComboBox1.DataSource = tbl;
                       RadComboBox1.DataBind();

                       RadComboBox1.SelectedValue = Hfcode.Value;

                   }



                   RadNumericTextBox rdnumthanhtienQ4 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ4") as RadNumericTextBox);
                   RadNumericTextBox rdnumthanhtienQ3 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ3") as RadNumericTextBox);
                   RadNumericTextBox rdnumthanhtienQ2 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ2") as RadNumericTextBox);
                   RadNumericTextBox rdnumthanhtienQ1 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ1") as RadNumericTextBox);

                   //  RadNumericTextBox rdnumdongia = (RGKienThuc.Items[i].FindControl("rdnumdongia") as RadNumericTextBox);

                   RadComboBox rdCboBrand = (RGKienThuc.Items[i].FindControl("rdCboBrand") as RadComboBox);

                   HiddenField HfBrand = (RGKienThuc.Items[i].FindControl("HfBrand") as HiddenField);
                   if (rdCboBrand != null)
                   {
                       DataTable tbl1 = cls.GetDataTable("sp_Load_Brand_ByBU", new string[] { "@BU" }, new object[] { rdcboNganhHang.SelectedValue });

                       //   DataTable tbl = cls.GetDataTable("sp_Load_AccountCoding_Bis", new string[] { "@id", "@userid" }, new object[] { "", Session["username"] });
                       rdCboBrand.DataSource = tbl1;
                       rdCboBrand.DataBind();
                       rdCboBrand.SelectedValue = HfBrand.Value;

                   }

                   RadComboBox rdCboCategory = (RGKienThuc.Items[i].FindControl("rdCboCategory") as RadComboBox);

                   HiddenField HfCategory = (RGKienThuc.Items[i].FindControl("HfCategory") as HiddenField);
                   //  
                   if (rdCboCategory != null)
                   {

                       DataTable tbl122 = cls.GetDataTable("sp_Load_Category_ByBrand", new string[] { "@brand_fk" }, new object[] { rdCboBrand.SelectedValue });
                       rdCboCategory.DataSource = tbl122;
                       //    rdCboCategory.ClearSelection();
                       rdCboCategory.DataBind();
                       rdCboCategory.SelectedValue = HfCategory.Value;

                   }

                   RadComboBox RDregion = (RGKienThuc.Items[i].FindControl("RDregion") as RadComboBox);
                   HiddenField HfRDregion = (RGKienThuc.Items[i].FindControl("HfRDregion") as HiddenField);

                   if (RDregion != null)
                   {
                       DataTable tbl = cls.GetDataTable("sp_Load_region", new string[] { "@channel_fk" }, new object[] { rdChannel.SelectedValue });

                       RDregion.DataSource = tbl;
                       RDregion.DataBind();
                       RDregion.SelectedValue = HfRDregion.Value;
                   }

                   /////--------------------- hang tang -----------.////////////////

                   RadComboBox rdCboBrandTang = (RGKienThuc.Items[i].FindControl("rdCboBrandTang") as RadComboBox);

                   HiddenField HfBrandTang = (RGKienThuc.Items[i].FindControl("HfBrandTang") as HiddenField);
                   if (rdCboBrandTang != null)
                   {
                       DataTable tblCboBrandTang = cls.GetDataTable("sp_Load_Brand_ByBU_HangTang", new string[] { "@BU" }, new object[] { rdcboNganhHang.SelectedValue });

                       //   DataTable tbl = cls.GetDataTable("sp_Load_AccountCoding_Bis", new string[] { "@id", "@userid" }, new object[] { "", Session["username"] });
                       rdCboBrandTang.DataSource = tblCboBrandTang;
                       rdCboBrandTang.DataBind();
                       rdCboBrandTang.SelectedValue = HfBrandTang.Value;

                   }

                   RadComboBox rdCboCategoryTang = (RGKienThuc.Items[i].FindControl("rdCboCategoryTang") as RadComboBox);

                   HiddenField HfCategoryTang = (RGKienThuc.Items[i].FindControl("HfCategoryTang") as HiddenField);
                   //  
                   if (rdCboCategoryTang != null)
                   {

                       DataTable tbCboCategoryTang = cls.GetDataTable("sp_Load_Category_ByBrand", new string[] { "@brand_fk" }, new object[] { rdCboBrandTang.SelectedValue });
                       rdCboCategoryTang.DataSource = tbCboCategoryTang;
                       //    rdCboCategory.ClearSelection();
                       rdCboCategoryTang.DataBind();
                       rdCboCategoryTang.SelectedValue = HfCategoryTang.Value;

                   }

                   //HiddenField hfbatbuocchon = (RGKienThuc.Items[i].FindControl("hfbatbuocchon") as HiddenField);

                   //DataTable tblhfbatbuocchon = cls.GetDataTable("sp_loadAccountCoding_chon", new string[] { "@id" }, new object[] { RadComboBox1.SelectedValue });
                   //if (tblhfbatbuocchon.Rows.Count > 0)
                   //{
                   //    hfbatbuocchon.Value = tblhfbatbuocchon.Rows[0]["batbuocchon"].ToString();
                   //}


                   ///////////////// --------------------////////////////////////////////////




                   RadNumericTextBox M1 = (RGKienThuc.Items[i].FindControl("M1") as RadNumericTextBox);
                   RadNumericTextBox M2 = (RGKienThuc.Items[i].FindControl("M2") as RadNumericTextBox);
                   RadNumericTextBox M3 = (RGKienThuc.Items[i].FindControl("M3") as RadNumericTextBox);
                   RadNumericTextBox M4 = (RGKienThuc.Items[i].FindControl("M4") as RadNumericTextBox);
                   RadNumericTextBox M5 = (RGKienThuc.Items[i].FindControl("M5") as RadNumericTextBox);
                   RadNumericTextBox M6 = (RGKienThuc.Items[i].FindControl("M6") as RadNumericTextBox);
                   RadNumericTextBox M7 = (RGKienThuc.Items[i].FindControl("M7") as RadNumericTextBox);
                   RadNumericTextBox M8 = (RGKienThuc.Items[i].FindControl("M8") as RadNumericTextBox);
                   RadNumericTextBox M9 = (RGKienThuc.Items[i].FindControl("M9") as RadNumericTextBox);
                   RadNumericTextBox M10 = (RGKienThuc.Items[i].FindControl("M10") as RadNumericTextBox);
                   RadNumericTextBox M11 = (RGKienThuc.Items[i].FindControl("M11") as RadNumericTextBox);
                   RadNumericTextBox M12 = (RGKienThuc.Items[i].FindControl("M12") as RadNumericTextBox);

                   M1.TextChanged += new EventHandler(M1_TextChanged);

                   M2.TextChanged += new EventHandler(M2_TextChanged);

                   M3.TextChanged += new EventHandler(M3_TextChanged);
                   M4.TextChanged += new EventHandler(M4_TextChanged);
                   M5.TextChanged += new EventHandler(M5_TextChanged);

                   M6.TextChanged += new EventHandler(M6_TextChanged);
                   M7.TextChanged += new EventHandler(M7_TextChanged);
                   M8.TextChanged += new EventHandler(M8_TextChanged);
                   M9.TextChanged += new EventHandler(M9_TextChanged);
                   M10.TextChanged += new EventHandler(M10_TextChanged);
                   M11.TextChanged += new EventHandler(M11_TextChanged);
                   M12.TextChanged += new EventHandler(M12_TextChanged);


               }

           }


           protected void rdCboBrandTang_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
           {
               RadComboBox rdCboBrandTang = (sender as RadComboBox);

               int index = (rdCboBrandTang.NamingContainer as GridItem).ItemIndex;

               RadComboBox rdCboCategoryTang = RGKienThuc.Items[index].FindControl("rdCboCategoryTang") as RadComboBox;



               DataTable tbl122 = cls.GetDataTable("sp_Load_Category_ByBrand", new string[] { "@brand_fk" }, new object[] { rdCboBrandTang.SelectedValue });
               rdCboCategoryTang.DataSource = tbl122;
               rdCboCategoryTang.DataBind();
           }

           protected void rdCboCategoryTang_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
           {
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
                  tblto.Rows[0]["Category"].ToString(), tblto.Rows[0]["ASPNo"].ToString(), tblto.Rows[0]["ASPFvalue"].ToString(), tblto.Rows[0]["Objective"].ToString(), tblto.Rows[0]["BudgetDepartment"].ToString(), tblto.Rows[0]["Spent"].ToString(), tblto.Rows[0]["BudgetBalance_Controller"].ToString(), tblto.Rows[0]["ApprovedCode"].ToString(), Request.QueryString["Userid"].ToString(), RdcboReason.Text+ txtNote.Text);

                     sys.SendMailASPAtt(tblto.Rows[0]["emailcc"].ToString(), tblto.Rows[0]["emailto"].ToString(), "Phản hồi từ " + tblto.Rows[0]["nguoiduyet"].ToString() + " về yêu cầu duyệt ASPF (ID:" + Request.QueryString["Userid"].ToString() + ") /Respond from " + tblto.Rows[0]["nguoiduyet"].ToString() + " for ASPF approval request ", content, tblto.Rows[0]["attachfile"].ToString() == "" ? "" : "~/ImagesUpload/" + tblto.Rows[0]["attachfile"].ToString());

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

             //if (RadNumericTextBox2.Value <= 0)
             //{
             //    MsgBox1.AddMessage("Ngân sách còn lại phải lớn hơn 0 / Budget balance must be greater than 0", uc.ucMsgBox.enmMessageType.Error);

             //    return;
             //}
            #region Upadte header
             try
             {
                 #region Upadte header
                 Obj = new clsObj();
                 Obj.Parameter = new string[] { "@ASPNo","@BudgetDepartment","@Spent","@BudgetBalance_Controller"
                                                 ,"@ASPFValue_Controller","@id"};
                 Obj.ValueParameter = new object[] {  "", 0,0,0,0,
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
                     string content = sys.noidungApproval(tblto.Rows[0]["nguoiduyet"].ToString(), tblto.Rows[0]["nguoitao"].ToString(), tblto.Rows[0]["Country"].ToString(), tblto.Rows[0]["BudgetOwner"].ToString(), tblto.Rows[0]["BU"].ToString(),
                  tblto.Rows[0]["Category"].ToString(), tblto.Rows[0]["ASPNo"].ToString(), tblto.Rows[0]["ASPFvalue"].ToString(), tblto.Rows[0]["Objective"].ToString(), tblto.Rows[0]["BudgetDepartment"].ToString(), tblto.Rows[0]["Spent"].ToString(), tblto.Rows[0]["BudgetBalance_Controller"].ToString(), tblto.Rows[0]["ApprovedCode"].ToString(), Request.QueryString["Userid"].ToString());

                     sys.SendMailASPAtt(tblto.Rows[0]["emailto"].ToString(), tblto.Rows[0]["emailcc"].ToString(), "Phản hồi từ " + tblto.Rows[0]["nguoiduyet"].ToString() + " về yêu cầu duyệt ASPF (ID:" + Request.QueryString["Userid"].ToString() + ")/Respond from " + tblto.Rows[0]["nguoiduyet"].ToString() + " for ASPF approval request ", content, tblto.Rows[0]["attachfile"].ToString() == "" ? "" : "~/ImagesUpload/" + tblto.Rows[0]["attachfile"].ToString());

                 }




                 DataTable tblto5555 = cls.GetDataTable("sp_Load_content_Bylevel", new string[] { "@docno", "@levelapp" }, new object[] { Request.QueryString["Userid"].ToString(), 3 });
                 if (tblto5555.Rows.Count > 0)
                 {
                     string yeucau = sys.noidungsendApproval(tblto5555.Rows[0]["nguoiduyet"].ToString(), tblto5555.Rows[0]["nguoitao"].ToString(), tblto5555.Rows[0]["Country"].ToString(), tblto5555.Rows[0]["BudgetOwner"].ToString(), tblto5555.Rows[0]["Brand"].ToString(),
                  tblto5555.Rows[0]["Category"].ToString(), tblto5555.Rows[0]["ASPNo"].ToString(), tblto5555.Rows[0]["ASPFvalue"].ToString(), tblto5555.Rows[0]["Objective"].ToString(), tblto5555.Rows[0]["BudgetDepartment"].ToString(), tblto5555.Rows[0]["Spent"].ToString(), tblto5555.Rows[0]["BudgetBalance_Controller"].ToString(), tblto5555.Rows[0]["ApprovedCode"].ToString(), Request.QueryString["Userid"].ToString(), tblto5555.Rows[0]["attachfile"].ToString());

                     sys.SendMailASPAtt(tblto5555.Rows[0]["emailto"].ToString(), "Yêu cầu duyệt ASPF(ID :"+Request.QueryString["Userid"].ToString()+") từ " + tblto.Rows[0]["nguoitao"].ToString() + "/ASPF approval request from " + tblto.Rows[0]["nguoitao"].ToString(), yeucau, tblto5555.Rows[0]["attachfile"].ToString() == "" ? "" : "~/ImagesUpload/" + tblto5555.Rows[0]["attachfile"].ToString());

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
         void loadbudget()
         {
             for (int i = 0; i < RGKienThuc.Items.Count; i++)
             {


                 RadComboBox rdCboCategory = RGKienThuc.Items[i].FindControl("rdCboCategory") as RadComboBox;

                 RadNumericTextBox A1 = (RGKienThuc.Items[i].FindControl("A1") as RadNumericTextBox);
                 RadNumericTextBox A2 = (RGKienThuc.Items[i].FindControl("A2") as RadNumericTextBox);
                 RadNumericTextBox A3 = (RGKienThuc.Items[i].FindControl("A3") as RadNumericTextBox);
                 RadNumericTextBox A4 = (RGKienThuc.Items[i].FindControl("A4") as RadNumericTextBox);

                 RadComboBox RadComboBox1 = RGKienThuc.Items[i].FindControl("RadComboBox1") as RadComboBox;
                 RadComboBox rdCboBrand = RGKienThuc.Items[i].FindControl("rdCboBrand") as RadComboBox;

                 RadNumericTextBox rdnumthanhtienQ1 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ1") as RadNumericTextBox);
                 RadNumericTextBox rdnumthanhtienQ2 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ2") as RadNumericTextBox);
                 RadNumericTextBox rdnumthanhtienQ3 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ3") as RadNumericTextBox);
                 RadNumericTextBox rdnumthanhtienQ4 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ4") as RadNumericTextBox);

                 Loaddetail_Quater(rdCboBrand, rdCboCategory, A1, A2, A3, A4, RadComboBox1, rdnumthanhtienQ1.Value == null ? 0 : (double)rdnumthanhtienQ1.Value, rdnumthanhtienQ2.Value == null ? 0 : (double)rdnumthanhtienQ2.Value, rdnumthanhtienQ3.Value == null ? 0 : (double)rdnumthanhtienQ3.Value, rdnumthanhtienQ4.Value == null ? 0 : (double)rdnumthanhtienQ4.Value);

             }
         }

         private double calculationAvailable(int monthStart, int MonthEnd, int Country_FK, int BudgetOwner, int ActivityGroup, int Brand_fk, int Cat_Fk, double q1, double q2, double q3, double q4, DateTime todate)
         {

             int quarterNumber1 = 0, quarterNumber2 = 0;
             double trave = 0;
             double totalvalue1 = 0, totalvalue2 = 0;
             if (monthStart >= 4 && monthStart <= 6)
                 quarterNumber1 = 1;
             else if (monthStart >= 7 && monthStart <= 9)
                 quarterNumber1 = 2;
             else if (monthStart >= 10 && monthStart <= 12)
                 quarterNumber1 = 3;
             else
                 quarterNumber1 = 4;
             ///----------------------/////////

             if (MonthEnd >= 4 && MonthEnd <= 6)
                 quarterNumber2 = 1;
             else if (MonthEnd >= 7 && MonthEnd <= 9)
                 quarterNumber2 = 2;
             else if (MonthEnd >= 10 && MonthEnd <= 12)
                 quarterNumber2 = 3;
             else
                 quarterNumber2 = 4;

             if (quarterNumber1 != quarterNumber2)
             {
                 if (quarterNumber1 == 1)
                     totalvalue1 = q1;
                 else if (quarterNumber1 == 2)
                     totalvalue1 = q2;
                 else if (quarterNumber1 == 3)
                     totalvalue1 = q3;
                 else if (quarterNumber1 == 4)
                     totalvalue1 = q4;


                 if (quarterNumber2 == 1)
                     totalvalue2 = q1;
                 else if (quarterNumber2 == 2)
                     totalvalue2 = q2;
                 else if (quarterNumber2 == 3)
                     totalvalue2 = q3;
                 else if (quarterNumber2 == 4)
                     totalvalue2 = q4;




                 if (Calculationbudget(Country_FK, BudgetOwner, ActivityGroup, Brand_fk, Cat_Fk, quarterNumber1, rdTypeofBudget.SelectedItem.Text, rdcboNganhHang.SelectedValue, todate) <
                     CalculationCommitment(Country_FK, BudgetOwner, ActivityGroup, Brand_fk, Cat_Fk, quarterNumber1, rdTypeofBudget.SelectedItem.Text, rdcboNganhHang.SelectedValue, todate) + totalvalue1 ||

                     Calculationbudget(Country_FK, BudgetOwner, ActivityGroup, Brand_fk, Cat_Fk, quarterNumber2, rdTypeofBudget.SelectedItem.Text, rdcboNganhHang.SelectedValue, todate) <
                     CalculationCommitment(Country_FK, BudgetOwner, ActivityGroup, Brand_fk, Cat_Fk, quarterNumber2, rdTypeofBudget.SelectedItem.Text, rdcboNganhHang.SelectedValue, todate) + totalvalue2

                     )
                 {
                     trave = 0;
                 }
                 else
                 {
                     trave = 1;
                 }


             }
             else
             {
                 if (Calculationbudget(Country_FK, BudgetOwner, ActivityGroup, Brand_fk, Cat_Fk, quarterNumber1, rdTypeofBudget.SelectedItem.Text, rdcboNganhHang.SelectedValue, todate) <
                     CalculationCommitment(Country_FK, BudgetOwner, ActivityGroup, Brand_fk, Cat_Fk, quarterNumber1, rdTypeofBudget.SelectedItem.Text, rdcboNganhHang.SelectedValue, todate) + q1 + q2 + q3 + q4)
                 {
                     trave = 0;
                 }
                 else
                 {
                     trave = 1;
                 }

             }


             return trave;



         }

         private double calculationAvailable_quater(int Country_FK, int BudgetOwner, int ActivityGroup, int Brand_fk, int Cat_Fk, int quater, DateTime ngay)
         {
             double trave;
             trave = Calculationbudget(Country_FK, BudgetOwner, ActivityGroup, Brand_fk, Cat_Fk, quater, rdTypeofBudget.SelectedItem.Text, rdcboNganhHang.SelectedValue, ngay) -
                       CalculationCommitment(Country_FK, BudgetOwner, ActivityGroup, Brand_fk, Cat_Fk, quater, rdTypeofBudget.SelectedItem.Text, rdcboNganhHang.SelectedValue, ngay);

             return trave;

         }

         private double CalculationCommitment(int Country_FK, int BudgetOwner, int ActivityGroup, int Brand_fk, int Cat_Fk, int Quater, string Upcharge, string BU, DateTime todate)
         {

             double trave = 0;

             DataTable tbl = cls.GetDataTable("sp_Load_Commitment_ASPF", new string[] { "@Country_fk", "@BudgetOwner", "@ActivityGroup", "@Brand_fk", "@Cat_Fk", "@Quater", "@Upcharge", "@BU", "@todate" },
                 new object[] { Country_FK, BudgetOwner, ActivityGroup, Brand_fk, Cat_Fk, Quater, Upcharge, BU, todate });
             if (tbl.Rows.Count <= 0)
             {
                 trave = 0;
             }
             else
             {
                 trave = double.Parse(tbl.Rows[0]["COMMITMENT"].ToString());
             }
             return trave;
         }

         private double Calculationbudget(int Country_FK, int BudgetOwner, int ActivityGroup, int Brand_fk, int Cat_Fk, int Quater, string Upcharge, string BU, DateTime todate)
         {


             double trave = 0;


             DataTable tbl = cls.GetDataTable("sp_Load_budget_ASPF", new string[] { "@Country_fk", "@BudgetOwner", "@ActivityGroup", "@Brand_fk", "@Cat_Fk", "@Quater", "@Upcharge", "@BU", "@todate" },
                 new object[] { Country_FK, BudgetOwner, ActivityGroup, Brand_fk, Cat_Fk, Quater, Upcharge, BU, todate });
             if (tbl.Rows.Count <= 0)
             {
                 trave = 0;
             }
             else
             {
                 trave = double.Parse(tbl.Rows[0]["COVERPLAN"].ToString());
             }

             return trave;
         }      

         
         

         protected void Button2_Click(object sender, EventArgs e)
         {
             Session["CreateASPF"] = "Update successfull";

             //if (RadNumericTextBox2.Value <= 0)
             //{
             //    MsgBox1.AddMessage("Ngân sách còn lại phải lớn hơn 0 / Budget balance must be greater than 0", uc.ucMsgBox.enmMessageType.Error);

             //    return;
             //}
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
                 Obj.ValueParameter = new object[] {  "", 0,0,0,0,
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

         protected void Rdcbocountry_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
         {
             LoadBU(Rdcbocountry.SelectedValue);


             for (int i = 0; i < RGKienThuc.Items.Count; i++)
             {


                 RadComboBox rdCboBrand = (RGKienThuc.Items[i].FindControl("rdCboBrand") as RadComboBox);
                 DataTable tbl = cls.GetDataTable("sp_Load_Brand_ByBU", new string[] { "@BU" }, new object[] { rdcboNganhHang.SelectedValue });

                 ////   DataTable tbl = cls.GetDataTable("sp_Load_AccountCoding_Bis", new string[] { "@id", "@userid" }, new object[] { "", Session["username"] });
                 rdCboBrand.ClearSelection();
                 rdCboBrand.DataSource = tbl;
                 rdCboBrand.DataBind();

                 RadComboBox rdCboCategory = (RGKienThuc.Items[i].FindControl("rdCboCategory") as RadComboBox);

                 DataTable tbl122 = cls.GetDataTable("sp_Load_Category_ByBrand", new string[] { "@brand_fk" }, new object[] { rdCboBrand.SelectedValue });
                 rdCboCategory.ClearSelection();
                 rdCboCategory.DataSource = tbl122;
                 rdCboCategory.DataBind();

             }

         }


       

    }

}
