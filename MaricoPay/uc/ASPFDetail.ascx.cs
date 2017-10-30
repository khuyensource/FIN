using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
using Telerik.Web.UI;
using Excel = Microsoft.Office.Interop.Excel; 
namespace MaricoPay.uc
{
    public partial class ASPFDetail : System.Web.UI.UserControl
    {
        
    
        Cclass cls = new Cclass();
        clsObj Obj = new clsObj();
        clsSql Sql = new clsSql();
        clsSys sys = new clsSys();
        string fileUpload = "";
        public DataTable dtdetail; 
        protected void Page_Load(object sender, EventArgs e)
        {

           

            if (!IsPostBack)
            {
              
                LoadCountry();
                LoadBU(Rdcbocountry.SelectedValue);
         
                LoadInfoUser();

                Load_typeofBudget();
           
                Load_Channel();
               
                LoadData();
                loadcontrol();

                loadbudget();
              
               
            }
           // ShowEmptyGrid(5);
            calculateQuater(int.Parse(RddatetimeFrom.SelectedDate.Value.Month.ToString()), int.Parse(RddatetimeTo.SelectedDate.Value.Month.ToString()));

        }
        void loadcontrol()
        {
            DataTable tbl = cls.GetDataTable("Sp_LoadStatus_aspf", new string[] { "@ID","@levelapp" }, new object[] { Request.QueryString["Userid"].ToString(),1 });

            if (tbl.Rows.Count > 0)
            {
                btnSave.Visible = false;
                btnSubmit.Visible = false;
                RGKienThuc.MasterTableView.GetColumn("column").Display = false;

            }
            else
            {
                btnSave.Visible = true;
                btnSubmit.Visible = true;
                RGKienThuc.MasterTableView.GetColumn("column").Display = true;
            }
        }
        private void LoadBU(string Country_fk)
        {

            DataTable tbl = cls.GetDataTable("sp_Load_BU", new string[] { "@Country_fk" }, new object[] { Country_fk });
            rdcboNganhHang.DataSource = tbl;
            rdcboNganhHang.DataBind();
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

         

        private void LoadData()
        {
            // ID	 						BudgetDepartment	Spent	BudgetBalance_Controller	ASPFValue_Controller	AttachFile	UserCreate	DateCreate	UserEdit	DateEdit
            DataTable tbl = cls.GetDataTable("sp_Load_ASPF", new string[] { "@ID" }, new object[] { Request.QueryString["Userid"].ToString() });
            if(tbl.Rows.Count>0)
            {
                LoadBU(Rdcbocountry.SelectedValue);
             

                //txtASPFNo.Text = tbl.Rows[0]["ASPNo"].ToString();
                rddateCreation.SelectedDate = cls.cToDateTime( tbl.Rows[0]["DateCreation"].ToString());
                Rdcbocountry.SelectedValue = tbl.Rows[0]["Country_FK"].ToString();
                rdNguoiduyet.SelectedValue = tbl.Rows[0]["BudgetOwnerN1"].ToString();
                hfBudgetowner.Value = tbl.Rows[0]["BudgetOwnerN1"].ToString();
                rdcboNganhHang.SelectedValue = tbl.Rows[0]["BU_FK"].ToString();
             //   CheckBox1.Checked=  tbl.Rows[0]["Upcharge"].ToString() =="1" ?  true : false;
                LoadBrand_BU(rdcboNganhHang.SelectedValue);
           
                RddatetimeTo.SelectedDate =  cls.cToDateTime(tbl.Rows[0]["To"].ToString());
                RddatetimeFrom.SelectedDate =  cls.cToDateTime(tbl.Rows[0]["From"].ToString());
                rdChannel.SelectedValue = tbl.Rows[0]["Channel_Fk"].ToString();
                txtObjective.Text = tbl.Rows[0]["Objective"].ToString();
                rdTypeofBudget.SelectedValue = tbl.Rows[0]["Upcharge"].ToString(); 
              //  rdFYBudgetCAT.Text = tbl.Rows[0]["FYbudgetcategory"].ToString();
             //   rdAvailable.Text = tbl.Rows[0]["Availablebudget"].ToString();
                radnumtxtASPFvalue.Text = tbl.Rows[0]["ASPFValue"].ToString();
             //   radnumtxtBudgetBalance.Text = tbl.Rows[0]["BudgetBalance"].ToString();
                fileUpload =  tbl.Rows[0]["AttachFile"].ToString();
                LoadASPF_Detail(Request.QueryString["Userid"].ToString());
              //  Loadregion(rdChannel.SelectedValue);
              //  rdRegion.SelectedValue = tbl.Rows[0]["region"].ToString();

                loadHistory();



            }
            
          
        }
       
         void loadHistory ()
         {
             DataTable tbl = cls.GetDataTable("Sp_load_Approve_History", new string[] { "@asp_id" }, new object[] { Request.QueryString["Userid"].ToString() });
              RadGridHistory.DataSource = tbl;
              RadGridHistory.DataBind();
         }


         //private void Loadregion(string channel)
         //{
         //    DataTable tbl = cls.GetDataTable("sp_Load_region", new string[] { "@channel_fk" }, new object[] { channel });

         //    rdRegion.DataSource = tbl;
         //    rdRegion.DataBind();
         //}
        private void calculateBudgetBlance()
        {
           // radnumtxtBudgetBalance.Text = (rdAvailable.Value - radnumtxtASPFvalue.Value).ToString();
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


        private void Load_typeofBudget()
        {

            DataTable tbl = cls.GetDataTable("sp_Load_TypeOfBudget");
            rdTypeofBudget.DataSource = tbl;
            rdTypeofBudget.DataBind();

        }

           private void LoadBrand_BU(string bu)
        {

            DataTable tbl = cls.GetDataTable("sp_Load_Brand_ByBU", new string[] { "@BU" }, new object[] { bu });
           // rdCboBrand.DataSource = tbl;
           // rdCboBrand.DataBind();
        }

         private void LoadASPF_Detail(string aspf_fk)
        {

            DataTable tbl = cls.GetDataTable("sp_Load_ASPF_Detail", new string[] { "@aspf_fk" }, new object[] { aspf_fk });
            ViewState["Data"] = tbl;
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
                dtdetail.Columns.Add("Code");
                dtdetail.Columns.Add("ID");

              
                dtdetail.Columns.Add("Nhanhang_fk");
                dtdetail.Columns.Add("Category_FK");
                dtdetail.Columns.Add("AmountQ1");
                 dtdetail.Columns.Add("AmountQ2");
                 dtdetail.Columns.Add("AmountQ3");
                 dtdetail.Columns.Add("AmountQ4");
                 dtdetail.Columns.Add("IOnumber");

                 dtdetail.Columns.Add("Region");
                 dtdetail.Columns.Add("AccountCoding");
              

                  dtdetail.Columns.Add("Description");
                 dtdetail.Columns.Add("Nhanhang_fk_Tang");
                 dtdetail.Columns.Add("Category_FK_Tang");
                 dtdetail.Columns.Add("Amount");

                 dtdetail.Columns.Add("M1");
                 dtdetail.Columns.Add("M2");
                 dtdetail.Columns.Add("M3");
                 dtdetail.Columns.Add("M4");
                 dtdetail.Columns.Add("M5");
                 dtdetail.Columns.Add("M6");
                 dtdetail.Columns.Add("M7");

                 dtdetail.Columns.Add("M8");
                 dtdetail.Columns.Add("M9");
                 dtdetail.Columns.Add("M10");
                 dtdetail.Columns.Add("M11");
                 dtdetail.Columns.Add("M12");
                
              //   dtdetail.Columns.Add("Category_FK");

                             

               if (ViewState["Data"] !=null)
                {
                    dtdetail = (DataTable)ViewState["Data"];
                    
                    for (int i = 0; i < rows; i++)
                    {
                        DataRow drNewRow = dtdetail.NewRow();
                        dtdetail.Rows.Add(drNewRow);
                        dtdetail.AcceptChanges();
                    }

                }
                else
                {
                    ViewState["Data"] = dtdetail;
                }

              //  ViewState["Data"] = dtdetail;
                RGKienThuc.DataSource = ViewState["Data"];
                RGKienThuc.DataBind();
                


            }

            private void LoadInfoUser()
            {
                //   Session["username"]    set  'nguyen.nguyen'  ngay 28/10

                DataTable tbl = cls.GetDataTable("sp_Load_Approval_ASPF_MKT", new string[] { "@UserName" }, new object[] { Session["username"] });
                if (tbl.Rows.Count > 0)
                {
                    rdNguoiduyet.DataSource = tbl;
                    rdNguoiduyet.DataBind();

                    //  txtBudgetowner.Text = rdNguoiduyet.Text; //.te tbl.Rows[0]["BudgetOwner"].ToString();
                    hfBudgetowner.Value = rdNguoiduyet.SelectedValue; //tbl.Rows[0]["ID"].ToString();

                }

            }

        protected void rdcboNganhHang_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            loadbudget();
            for (int i = 0; i < RGKienThuc.Items.Count; i++)
            {


                RadComboBox rdCboBrand = (RGKienThuc.Items[i].FindControl("rdCboBrand") as RadComboBox);
                DataTable tbl = cls.GetDataTable("sp_Load_Brand_ByBU", new string[] { "@BU" }, new object[] { rdcboNganhHang.SelectedValue });

                ////   DataTable tbl = cls.GetDataTable("sp_Load_AccountCoding_Bis", new string[] { "@id", "@userid" }, new object[] { "", Session["username"] });
                rdCboBrand.DataSource = tbl;
                rdCboBrand.DataBind();

                RadComboBox rdCboBrandTang = (RGKienThuc.Items[i].FindControl("rdCboBrandTang") as RadComboBox);
                if (rdCboBrandTang != null)
                {
                    DataTable tbl1 = cls.GetDataTable("sp_Load_Brand_ByBU_HangTang", new string[] { "@BU" }, new object[] { rdcboNganhHang.SelectedValue });

                    //   DataTable tbl = cls.GetDataTable("sp_Load_AccountCoding_Bis", new string[] { "@id", "@userid" }, new object[] { "", Session["username"] });
                    rdCboBrandTang.DataSource = tbl1;
                    rdCboBrandTang.DataBind();


                }

                RadComboBox rdCboCategoryTang = (RGKienThuc.Items[i].FindControl("rdCboCategoryTang") as RadComboBox);


                if (rdCboCategoryTang != null)
                {

                    DataTable tblCategoryTang = cls.GetDataTable("sp_Load_Category_ByBrand", new string[] { "@brand_fk" }, new object[] { rdCboBrandTang.SelectedValue });
                    rdCboCategoryTang.DataSource = tblCategoryTang;

                    ////////// load brand,CAT hang tặng -----
                }

            }

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
                DataTable tbl = cls.GetDataTable("sp_Check_ActivityGroup", new string[] { "@id" }, new object[] { RadComboBox1.SelectedValue });
                double q1tam = 0, q2tam = 0, q3tam = 0, q4tam = 0;

                if (tbl.Rows[0]["MKT"].ToString() == "1")
                {
                    foreach (GridDataItem item2 in RGKienThuc.Items)
                    {

                        RadComboBox RadComboBox1duyet = item2.FindControl("RadComboBox1") as RadComboBox;
                        RadComboBox rdCboBrandduyet = item2.FindControl("rdCboBrand") as RadComboBox;
                        DataTable tblduyet = cls.GetDataTable("sp_Check_ActivityGroup", new string[] { "@id" }, new object[] { RadComboBox1duyet.SelectedValue });

                        if (tbl.Rows[0]["ActivityGroup"].ToString() == tblduyet.Rows[0]["ActivityGroup"].ToString() && rdCboBrandduyet.SelectedValue == rdCboBrand.SelectedValue)
                        {
                            RadNumericTextBox M1chay = item2.FindControl("M1") as RadNumericTextBox;
                            RadNumericTextBox M2chay = item2.FindControl("M2") as RadNumericTextBox;
                            RadNumericTextBox M3chay = item2.FindControl("M3") as RadNumericTextBox;
                            RadNumericTextBox M4chay = item2.FindControl("M4") as RadNumericTextBox;

                            RadNumericTextBox M5chay = item2.FindControl("M5") as RadNumericTextBox;
                            RadNumericTextBox M6chay = item2.FindControl("M6") as RadNumericTextBox;
                            RadNumericTextBox M7chay = item2.FindControl("M7") as RadNumericTextBox;
                            RadNumericTextBox M8chay = item2.FindControl("M8") as RadNumericTextBox;

                            RadNumericTextBox M9chay = item2.FindControl("M9") as RadNumericTextBox;
                            RadNumericTextBox M10chay = item2.FindControl("M10") as RadNumericTextBox;
                            RadNumericTextBox M11chay = item2.FindControl("M11") as RadNumericTextBox;
                            RadNumericTextBox M12chay = item2.FindControl("M12") as RadNumericTextBox;

                            q1tam = q1tam + (double)(M4chay.Value == null ? 0 : M4chay.Value) + (double)(M5chay.Value == null ? 0 : M5chay.Value) + (double)(M6chay.Value == null ? 0 : M6chay.Value);
                            q2tam = q2tam + (double)(M7chay.Value == null ? 0 : M7chay.Value) + (double)(M8chay.Value == null ? 0 : M8chay.Value) + (double)(M9chay.Value == null ? 0 : M9chay.Value);
                            q3tam = q3tam + (double)(M10chay.Value == null ? 0 : M10chay.Value) + (double)(M11chay.Value == null ? 0 : M11chay.Value) + (double)(M12chay.Value == null ? 0 : M12chay.Value);
                            q4tam = q4tam + (double)(M1chay.Value == null ? 0 : M1chay.Value) + (double)(M2chay.Value == null ? 0 : M2chay.Value) + (double)(M3chay.Value == null ? 0 : M3chay.Value);

                        }


                    }
                }
                else
                {
                    foreach (GridDataItem item2 in RGKienThuc.Items)
                    {

                        RadComboBox RadComboBox1duyet = item2.FindControl("RadComboBox1") as RadComboBox;
                        RadComboBox rdCboBrandduyet = item2.FindControl("rdCboBrand") as RadComboBox;
                        DataTable tblduyet = cls.GetDataTable("sp_Check_ActivityGroup", new string[] { "@id" }, new object[] { RadComboBox1duyet.SelectedValue });

                        if (tbl.Rows[0]["ActivityGroup"].ToString() == tblduyet.Rows[0]["ActivityGroup"].ToString())
                        {
                            RadNumericTextBox M1chay = item2.FindControl("M1") as RadNumericTextBox;
                            RadNumericTextBox M2chay = item2.FindControl("M2") as RadNumericTextBox;
                            RadNumericTextBox M3chay = item2.FindControl("M3") as RadNumericTextBox;
                            RadNumericTextBox M4chay = item2.FindControl("M4") as RadNumericTextBox;

                            RadNumericTextBox M5chay = item2.FindControl("M5") as RadNumericTextBox;
                            RadNumericTextBox M6chay = item2.FindControl("M6") as RadNumericTextBox;
                            RadNumericTextBox M7chay = item2.FindControl("M7") as RadNumericTextBox;
                            RadNumericTextBox M8chay = item2.FindControl("M8") as RadNumericTextBox;

                            RadNumericTextBox M9chay = item2.FindControl("M9") as RadNumericTextBox;
                            RadNumericTextBox M10chay = item2.FindControl("M10") as RadNumericTextBox;
                            RadNumericTextBox M11chay = item2.FindControl("M11") as RadNumericTextBox;
                            RadNumericTextBox M12chay = item2.FindControl("M12") as RadNumericTextBox;

                            q1tam = q1tam + (double)(M4chay.Value == null ? 0 : M4chay.Value) + (double)(M5chay.Value == null ? 0 : M5chay.Value) + (double)(M6chay.Value == null ? 0 : M6chay.Value);
                            q2tam = q2tam + (double)(M7chay.Value == null ? 0 : M7chay.Value) + (double)(M8chay.Value == null ? 0 : M8chay.Value) + (double)(M9chay.Value == null ? 0 : M9chay.Value);
                            q3tam = q3tam + (double)(M10chay.Value == null ? 0 : M10chay.Value) + (double)(M11chay.Value == null ? 0 : M11chay.Value) + (double)(M12chay.Value == null ? 0 : M12chay.Value);
                            q4tam = q4tam + (double)(M1chay.Value == null ? 0 : M1chay.Value) + (double)(M2chay.Value == null ? 0 : M2chay.Value) + (double)(M3chay.Value == null ? 0 : M3chay.Value);

                        }


                    }
                }


                Loaddetail_Quater(rdCboBrand, rdCboCategory, A1, A2, A3, A4, RadComboBox1, q1tam, q2tam, q3tam, q4tam);

            }
        }

        protected void RddatetimeFrom_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            int result = DateTime.Compare(RddatetimeFrom.SelectedDate.Value, RddatetimeTo.SelectedDate.Value);
            if (result == 1)
            {
                MsgBox1.AddMessage("Từ ngày phải nhỏ hơn đến ngày / From day to day should be less than ", uc.ucMsgBox.enmMessageType.Error);

                return;
            }
            else
            {

                RGKienThuc.MasterTableView.GetColumn("Q1").Display = false;
                RGKienThuc.MasterTableView.GetColumn("A1").Display = false;

                RGKienThuc.MasterTableView.GetColumn("Q2").Display = false;
                RGKienThuc.MasterTableView.GetColumn("A2").Display = false;

                RGKienThuc.MasterTableView.GetColumn("Q3").Display = false;
                RGKienThuc.MasterTableView.GetColumn("A3").Display = false;

                RGKienThuc.MasterTableView.GetColumn("Q4").Display = false;
                RGKienThuc.MasterTableView.GetColumn("A4").Display = false;

                RGKienThuc.MasterTableView.GetColumn("M1").Display = false;
                RGKienThuc.MasterTableView.GetColumn("M2").Display = false;

                RGKienThuc.MasterTableView.GetColumn("M3").Display = false;
                RGKienThuc.MasterTableView.GetColumn("M4").Display = false;

                RGKienThuc.MasterTableView.GetColumn("M5").Display = false;
                RGKienThuc.MasterTableView.GetColumn("M6").Display = false;

                RGKienThuc.MasterTableView.GetColumn("M7").Display = false;
                RGKienThuc.MasterTableView.GetColumn("M8").Display = false;

                RGKienThuc.MasterTableView.GetColumn("M9").Display = false;
                RGKienThuc.MasterTableView.GetColumn("M10").Display = false;

                RGKienThuc.MasterTableView.GetColumn("M11").Display = false;
                RGKienThuc.MasterTableView.GetColumn("M12").Display = false;



                calculateQuater(int.Parse(RddatetimeFrom.SelectedDate.Value.Month.ToString()), int.Parse(RddatetimeTo.SelectedDate.Value.Month.ToString()));

            }
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
                            // RGKienThuc.MasterTableView.GetColumn("Q1").Display = true;
                            RGKienThuc.MasterTableView.GetColumn("A1").Display = true;


                            break;
                        case 2:
                            //RGKienThuc.MasterTableView.GetColumn("Q2").Display = true;

                            RGKienThuc.MasterTableView.GetColumn("A2").Display = true;


                            break;
                        case 3:
                            //  RGKienThuc.MasterTableView.GetColumn("Q3").Display = true;

                            RGKienThuc.MasterTableView.GetColumn("A3").Display = true;



                            break;

                        case 4:
                            // RGKienThuc.MasterTableView.GetColumn("Q4").Display = true;

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
                            //RGKienThuc.MasterTableView.GetColumn("Q2").Display = true;

                            RGKienThuc.MasterTableView.GetColumn("A2").Display = true;


                            break;
                        case 3:
                            //  RGKienThuc.MasterTableView.GetColumn("Q3").Display = true;

                            RGKienThuc.MasterTableView.GetColumn("A3").Display = true;



                            break;

                        case 4:
                            // RGKienThuc.MasterTableView.GetColumn("Q4").Display = true;

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
                        // RGKienThuc.MasterTableView.GetColumn("Q1").Display = true;
                        RGKienThuc.MasterTableView.GetColumn("A1").Display = true;


                        break;
                    case 2:
                        //RGKienThuc.MasterTableView.GetColumn("Q2").Display = true;

                        RGKienThuc.MasterTableView.GetColumn("A2").Display = true;


                        break;
                    case 3:
                        //  RGKienThuc.MasterTableView.GetColumn("Q3").Display = true;

                        RGKienThuc.MasterTableView.GetColumn("A3").Display = true;



                        break;

                    case 4:
                        // RGKienThuc.MasterTableView.GetColumn("Q4").Display = true;

                        RGKienThuc.MasterTableView.GetColumn("A4").Display = true;

                        break;
                }


            }

        }
   


        protected void RddatetimeTo_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            int result = DateTime.Compare(RddatetimeFrom.SelectedDate.Value, RddatetimeTo.SelectedDate.Value);
            if (result == 1)
            {
                MsgBox1.AddMessage("Đến ngày phải lớn hơn từ ngày / To date must be greater than the date ", uc.ucMsgBox.enmMessageType.Error);

                return;
            }
            else
            {
                RGKienThuc.MasterTableView.GetColumn("Q1").Display = false;
                RGKienThuc.MasterTableView.GetColumn("A1").Display = false;

                RGKienThuc.MasterTableView.GetColumn("Q2").Display = false;
                RGKienThuc.MasterTableView.GetColumn("A2").Display = false;

                RGKienThuc.MasterTableView.GetColumn("Q3").Display = false;
                RGKienThuc.MasterTableView.GetColumn("A3").Display = false;

                RGKienThuc.MasterTableView.GetColumn("Q4").Display = false;
                RGKienThuc.MasterTableView.GetColumn("A4").Display = false;

                RGKienThuc.MasterTableView.GetColumn("M1").Display = false;
                RGKienThuc.MasterTableView.GetColumn("M2").Display = false;

                RGKienThuc.MasterTableView.GetColumn("M3").Display = false;
                RGKienThuc.MasterTableView.GetColumn("M4").Display = false;

                RGKienThuc.MasterTableView.GetColumn("M5").Display = false;
                RGKienThuc.MasterTableView.GetColumn("M6").Display = false;

                RGKienThuc.MasterTableView.GetColumn("M7").Display = false;
                RGKienThuc.MasterTableView.GetColumn("M8").Display = false;

                RGKienThuc.MasterTableView.GetColumn("M9").Display = false;
                RGKienThuc.MasterTableView.GetColumn("M10").Display = false;

                RGKienThuc.MasterTableView.GetColumn("M11").Display = false;
                RGKienThuc.MasterTableView.GetColumn("M12").Display = false;


                calculateQuater(int.Parse(RddatetimeFrom.SelectedDate.Value.Month.ToString()), int.Parse(RddatetimeTo.SelectedDate.Value.Month.ToString()));

            }

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

        protected void RGKienThuc_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            string strId = item.GetDataKeyValue("ID").ToString();


            Obj = new clsObj();
            Obj.Parameter = new string[] { "@ID" };
            Obj.ValueParameter = new object[] { strId };
            Obj.SpName = "Sp_Delete_aspf_Detail";
            Sql.fNonGetData(Obj);
            if (Obj.KetQua < 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Delete failed. Please try again later');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
            }

            LoadASPF_Detail(Request.QueryString["Userid"].ToString());

        }
        protected void RGKienThuc_ItemCommand(object source, GridCommandEventArgs e)
        {
        }
         private void Load_Category_ByBrand(string brand)
        {

            DataTable tbl = cls.GetDataTable("sp_Load_Category_ByBrand", new string[] { "@brand_fk" }, new object[] { brand });
            //rdCboCategory.DataSource = tbl;
           // rdCboCategory.DataBind();

        }


         //private void Load_AccountCoding(string id) 
         //{

         //    DataTable tbl = cls.GetDataTable("sp_Load_AccountCoding_Bis", new string[] { "@id", "@userid" }, new object[] { id, Session["username"] });
         //    Rdcode.DataSource = tbl;
         //    Rdcode.DataBind();

         //}

         private void Load_Channel()
         {

             DataTable tbl = cls.GetDataTable("sp_Load_Channel");
             rdChannel.DataSource = tbl;
             rdChannel.DataBind();

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

         protected void rdCboBrand_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
         {
             RadComboBox rdCboBrand = (sender as RadComboBox);

             int index = (rdCboBrand.NamingContainer as GridItem).ItemIndex;


             RadComboBox rdCboCategory = RGKienThuc.Items[index].FindControl("rdCboCategory") as RadComboBox;

             RadNumericTextBox A1 = (RGKienThuc.Items[index].FindControl("A1") as RadNumericTextBox);
             RadNumericTextBox A2 = (RGKienThuc.Items[index].FindControl("A2") as RadNumericTextBox);
             RadNumericTextBox A3 = (RGKienThuc.Items[index].FindControl("A3") as RadNumericTextBox);
             RadNumericTextBox A4 = (RGKienThuc.Items[index].FindControl("A4") as RadNumericTextBox);

             RadComboBox RadComboBox1 = RGKienThuc.Items[index].FindControl("RadComboBox1") as RadComboBox;

             DataTable tbl122 = cls.GetDataTable("sp_Load_Category_ByBrand", new string[] { "@brand_fk" }, new object[] { rdCboBrand.SelectedValue });
             rdCboCategory.DataSource = tbl122;
             rdCboCategory.ClearSelection();
             rdCboCategory.DataBind();



             loadbudget();

         }

         protected void Rdcode_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
         {
             //  Load_Category_ByBrand(rdCboBrand.SelectedValue);
             // LoadASPF_Detail("");
             RadComboBox Rdcode = (sender as RadComboBox);
             int index = (Rdcode.NamingContainer as GridItem).ItemIndex;
             //  GridEditFormItem editForm = (GridEditFormItem)RGKienThuc.NamingContainer;
             // RadComboBox rdCboBrand = (RadComboBox)editForm["rdCboBrand"].Controls[0];

             RadComboBox rdCboBrand = (RGKienThuc.Items[index].FindControl("rdCboBrand") as RadComboBox);

         
            
          
               
             RadNumericTextBox A1 = (RGKienThuc.Items[index].FindControl("A1") as RadNumericTextBox);
             RadNumericTextBox A2 = (RGKienThuc.Items[index].FindControl("A2") as RadNumericTextBox);
             RadNumericTextBox A3 = (RGKienThuc.Items[index].FindControl("A3") as RadNumericTextBox);
             RadNumericTextBox A4 = (RGKienThuc.Items[index].FindControl("A4") as RadNumericTextBox);


             RadComboBox RadComboBox1 = RGKienThuc.Items[index].FindControl("RadComboBox1") as RadComboBox;
             // RadComboBox rdCboBrand = RGKienThuc.Items[index].FindControl("rdCboBrand") as RadComboBox;
             RadComboBox rdCboCategory = RGKienThuc.Items[index].FindControl("rdCboCategory") as RadComboBox;

             HiddenField hfbatbuocchon = (RGKienThuc.Items[index].FindControl("hfbatbuocchon") as HiddenField);

             DataTable tblhfbatbuocchon = cls.GetDataTable("sp_loadAccountCoding_chon", new string[] { "@id" }, new object[] { RadComboBox1.SelectedValue });
             if (tblhfbatbuocchon.Rows.Count > 0)
             {
                 hfbatbuocchon.Value = tblhfbatbuocchon.Rows[0]["batbuocchon"].ToString();
             }


             loadbudget();
         }

         protected void btnAddrow_Click(object sender, ImageClickEventArgs e)
         {
             ShowEmptyGrid(1);


         }

         protected void RGKienThuc_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
         {
             if (IsPostBack)
             {
                 ViewState["Data"] = dtdetail;
                 dtdetail = (DataTable)ViewState["Data"];
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


                   RadNumericTextBox rdnumthanhtienQ1 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ1") as RadNumericTextBox);
                   RadNumericTextBox rdnumthanhtienQ2 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ2") as RadNumericTextBox);
                   RadNumericTextBox rdnumthanhtienQ3 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ3") as RadNumericTextBox);
                   RadNumericTextBox rdnumthanhtienQ4 = (RGKienThuc.Items[i].FindControl("rdnumthanhtienQ4") as RadNumericTextBox);




                   totalASPF += (rdnumthanhtienQ1.Value == null ? 0 : (double)rdnumthanhtienQ1.Value) + (rdnumthanhtienQ2.Value == null ? 0 : (double)rdnumthanhtienQ2.Value) + (rdnumthanhtienQ3.Value == null ? 0 : (double)rdnumthanhtienQ3.Value) + (rdnumthanhtienQ4.Value == null ? 0 : (double)rdnumthanhtienQ4.Value);
               }
               radnumtxtASPFvalue.Text = totalASPF.ToString();
           }

           protected void rdnumdongia_TextChanged(object sender, EventArgs e)
           {

               Calculation();

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

            
                                
                 HiddenField Hfcode = (RGKienThuc.Items[i].FindControl("Hfcode") as HiddenField); 
                 if (RadComboBox1 != null)
                 {
                     DataTable tbl = cls.GetDataTable("sp_Load_AccountCoding_Bis", new string[] { "@id", "@userid" }, new object[] { "", hfBudgetowner.Value });
                     RadComboBox1.DataSource = tbl;
                     RadComboBox1.DataBind();
                     
                     RadComboBox1.SelectedValue = Hfcode.Value;

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

                 HiddenField hfbatbuocchon = (RGKienThuc.Items[i].FindControl("hfbatbuocchon") as HiddenField);

                 DataTable tblhfbatbuocchon = cls.GetDataTable("sp_loadAccountCoding_chon", new string[] { "@id" }, new object[] { RadComboBox1.SelectedValue });
                 if (tblhfbatbuocchon.Rows.Count > 0)
                 {
                     hfbatbuocchon.Value = tblhfbatbuocchon.Rows[0]["batbuocchon"].ToString();
                 }


                 ///////////////// --------------------////////////////////////////////////


                 rdnumthanhtienQ1.TextChanged += new EventHandler(rdnumthanhtienQ1_TextChanged);
                 rdnumthanhtienQ2.TextChanged += new EventHandler(rdnumthanhtienQ2_TextChanged);
                 rdnumthanhtienQ3.TextChanged += new EventHandler(rdnumthanhtienQ3_TextChanged);
                 rdnumthanhtienQ4.TextChanged += new EventHandler(rdnumthanhtienQ4_TextChanged);

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
         protected void rdnumthanhtienQ4_TextChanged(object sender, EventArgs e)
         {
             Calculation();
         }

         protected void rdnumthanhtienQ3_TextChanged(object sender, EventArgs e)
         {
             Calculation();
         }

         protected void rdnumthanhtienQ2_TextChanged(object sender, EventArgs e)
         {
             Calculation();
         }

         protected void rdnumthanhtienQ1_TextChanged(object sender, EventArgs e)
         {
             Calculation();
         }

         protected void btnaddsubgoal_Click(object sender, EventArgs e)
         {
             //  RGSubgoal.Visible = false;

             if (ViewState["Data"] != null)
             {

                 DataTable dt = (DataTable)ViewState["Data"];
                 //if (dt.Rows.Count < 4)
                 //{
                     dt = addNewRow(dt);
                     RGKienThuc.DataSource = dt;
                     RGKienThuc.Rebind();
               //  }
             //    else
              //   {
                   //  ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Number of Sub Goal not greater than 4');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
                  //   return;
             //    }

             }
         }

         protected void RGKienThuc_ItemCreated(object sender, GridItemEventArgs e)
         {
             if (e.Item is GridEditableItem && e.Item.IsInEditMode)
             {
                 RadComboBox rdCboBrand = (e.Item as GridEditableItem)["rdCboBrand"].Controls[0] as RadComboBox;
                 rdCboBrand.AutoPostBack = true;
                 rdCboBrand.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(rdCboBrand_SelectedIndexChanged);
             }

             if (e.Item is GridEditableItem && e.Item.IsInEditMode)
             {
                 RadComboBox rdCboCategory = (e.Item as GridEditableItem)["rdCboCategory"].Controls[0] as RadComboBox;
                 rdCboCategory.AutoPostBack = true;
                 rdCboCategory.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(rdCboCategory_SelectedIndexChanged);
             }
         }

         protected void rdCboCategory_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
         {
             RadComboBox rdCboCategory = (sender as RadComboBox);
             int index = (rdCboCategory.NamingContainer as GridItem).ItemIndex;
             //   GridEditFormItem editForm = (GridEditFormItem)RGKienThuc.NamingContainer;
             RadComboBox rdCboBrand = RGKienThuc.Items[index].FindControl("rdCboBrand") as RadComboBox;
             RadComboBox RadComboBox1 = RGKienThuc.Items[index].FindControl("RadComboBox1") as RadComboBox;

             RadNumericTextBox A1 = (RGKienThuc.Items[index].FindControl("A1") as RadNumericTextBox);
             RadNumericTextBox A2 = (RGKienThuc.Items[index].FindControl("A2") as RadNumericTextBox);
             RadNumericTextBox A3 = (RGKienThuc.Items[index].FindControl("A3") as RadNumericTextBox);
             RadNumericTextBox A4 = (RGKienThuc.Items[index].FindControl("A4") as RadNumericTextBox);


             RadNumericTextBox rdnumthanhtienQ1 = (RGKienThuc.Items[index].FindControl("rdnumthanhtienQ1") as RadNumericTextBox);
             RadNumericTextBox rdnumthanhtienQ2 = (RGKienThuc.Items[index].FindControl("rdnumthanhtienQ2") as RadNumericTextBox);
             RadNumericTextBox rdnumthanhtienQ3 = (RGKienThuc.Items[index].FindControl("rdnumthanhtienQ3") as RadNumericTextBox);
             RadNumericTextBox rdnumthanhtienQ4 = (RGKienThuc.Items[index].FindControl("rdnumthanhtienQ4") as RadNumericTextBox);

             Loaddetail_Quater(rdCboBrand, rdCboCategory, A1, A2, A3, A4, RadComboBox1, rdnumthanhtienQ1.Value == null ? 0 : (double)rdnumthanhtienQ1.Value, rdnumthanhtienQ2.Value == null ? 0 : (double)rdnumthanhtienQ2.Value, rdnumthanhtienQ3.Value == null ? 0 : (double)rdnumthanhtienQ3.Value, rdnumthanhtienQ4.Value == null ? 0 : (double)rdnumthanhtienQ4.Value);
                     


         }

         protected void btnSave_Click(object sender, EventArgs e)
         {
             Session["CreateASPF"] = "Update successfull";
             int monthStart, MonthEnd, quarterNumber1, quarterNumber2;
             #region check after Update
             string filename = "";
             int result = DateTime.Compare(RddatetimeFrom.SelectedDate.Value, RddatetimeTo.SelectedDate.Value);
             if (result == 1)
             {
                 MsgBox1.AddMessage("Từ ngày phải lớn hơn đến ngày / From day to day is greater than ", uc.ucMsgBox.enmMessageType.Error);

                 return;
             }

             //if (radnumtxtBudgetBalance.Value <0)
             //{
             //    MsgBox1.AddMessage("Ngân sách còn lại phải lớn hơn 0 / Budget balance must be greater than 0", uc.ucMsgBox.enmMessageType.Error);

             //    return;
             //}

             if (radnumtxtASPFvalue.Value <= 0)
             {
                 MsgBox1.AddMessage("ASPF phải lớn hơn 0 / ASPF must be greater than 0", uc.ucMsgBox.enmMessageType.Error);

                 return;
             }
             //if (rdFYBudgetCAT.Value < rdAvailable.Value)
             //{
             //    MsgBox1.AddMessage("Available budget phải nhỏ hơn FY budget  / Available budget must be less than FY budget", uc.ucMsgBox.enmMessageType.Error);

             //    return;
             //}
             //if (txtBudgetowner.Text == "")
             //{
             //    MsgBox1.AddMessage("Budget owner không được rỗng  / Budget owner is not empty", uc.ucMsgBox.enmMessageType.Error);

             //    return;
             //}


             if (txtObjective.Text == "")
             {
                 MsgBox1.AddMessage("Mục tiêu không được rỗng  / Objective is not empty", uc.ucMsgBox.enmMessageType.Error);

                 return;
             }

             if (rdTypeofBudget.SelectedItem.Text == "Upcharge" && radnumtxtASPFvalue.Value > 300000000)
             {
                 MsgBox1.AddMessage("Upcharge budget không được lớn hơn 300.000.000 VNĐ  / Upcharge budget is not greater than 300 million VND ", uc.ucMsgBox.enmMessageType.Error);

                 return;
             }


             if (rdTypeofBudget.SelectedItem.Text == "")
             {
                 MsgBox1.AddMessage("Loại ngân sách không được rỗng  / type of budget is not emty", uc.ucMsgBox.enmMessageType.Error);

                 return;
             }



             if (FileUpload1.HasFile)
             {
                 try
                 {
                     int vt1 = FileUpload1.FileName.LastIndexOf(".");
                     int vtcanlay = vt1;
                     int len = FileUpload1.FileName.Length;
                     string filetemp = FileUpload1.FileName.Substring(0, vt1);
                     string extention = FileUpload1.FileName.Substring(vtcanlay, len - vtcanlay);
                     //     filename = Session["NewDocNo"].ToString() + "-" + Session["AutoNumber"].ToString();
                     filename = FileUpload1.FileName.Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("+", String.Empty).Replace("-", String.Empty).Replace(" ", String.Empty).Replace("*", String.Empty);
                     string sFolderPath = Server.MapPath("ImagesUpload/" + filename);

                     Random rnd = new Random();
                     filename = filetemp.Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("+", String.Empty).Replace("-", String.Empty).Replace(" ", String.Empty).Replace("*", String.Empty) + rnd.Next().ToString() + extention;

                     // }
                     filename = Regex.Replace(filename, @"\s", ""); //+ DateTime.Now.ToString("yyyyMMdd-HHMMss");
                  
                     sFolderPath = Server.MapPath("ImagesUpload/" + filename);
                     FileUpload1.SaveAs(sFolderPath);

                     // item.FileAttach = filename;

                 }
                 catch (Exception ex)
                 {
                     //hdPathSave.Value = "";
                     //    item.FileAttach = "";
                     Response.Write(ex.Message);
                 }
             }
             else
             {
                 filename = "";
             }


             
             #endregion



             #region Upadte header
             Obj = new clsObj();
             Obj.Parameter = new string[] { "@ASPNo","@DateCreation","@Country_FK","@BU_FK","@Brand_FK","@Category_FK","@Code","@GLCode","@From"
                                                   ,"@To","@Channel_Fk","@Objective","@FYbudgetcategory","@Availablebudget","@ASPFValue","@BudgetBalance","@BudgetDepartment","@Spent","@BudgetBalance_Controller"
                                                 ,"@ASPFValue_Controller","@AttachFile","@UserCreate","@UserEdit","@region","@upcharge","@N1","@BudgetOwnerN1"};
             Obj.ValueParameter = new object[] { Request.QueryString["Userid"].ToString(), rddateCreation.SelectedDate, Rdcbocountry.SelectedValue, rdcboNganhHang.SelectedValue, "", "", 0, "", RddatetimeFrom.SelectedDate ,
                    RddatetimeTo.SelectedDate,rdChannel.SelectedValue,txtObjective.Text,"0","0",radnumtxtASPFvalue.Text,"0",0,0,0,0,filename,
                         Session["email"]  ,"","" , rdTypeofBudget.SelectedValue,rdNguoiduyet.Text,rdNguoiduyet.SelectedValue   };

             Obj.SpName = "sp_Update_ASPF";
             Sql.fNonGetData(Obj);
               

              #endregion

             if (Request.QueryString["Userid"].ToString() != "")
             {

                 #region check after Detail
                 foreach (GridDataItem item in RGKienThuc.Items)
                 {
                     TextBox txtDescription = item.FindControl("txtDescription") as TextBox;

                     RadComboBox rdCboBrand = item.FindControl("rdCboBrand") as RadComboBox;
                     RadComboBox rdCboCategory = item.FindControl("rdCboCategory") as RadComboBox;

                     RadNumericTextBox rdnumthanhtienQ1 = item.FindControl("rdnumthanhtienQ1") as RadNumericTextBox;
                     RadNumericTextBox rdnumthanhtienQ2 = item.FindControl("rdnumthanhtienQ2") as RadNumericTextBox;
                     RadNumericTextBox rdnumthanhtienQ3 = item.FindControl("rdnumthanhtienQ3") as RadNumericTextBox;
                     RadNumericTextBox rdnumthanhtienQ4 = item.FindControl("rdnumthanhtienQ4") as RadNumericTextBox;
                     RadNumericTextBox A1 = item.FindControl("A1") as RadNumericTextBox;
                     RadNumericTextBox A2 = item.FindControl("A2") as RadNumericTextBox;
                     RadNumericTextBox A3 = item.FindControl("A3") as RadNumericTextBox;
                     RadNumericTextBox A4 = item.FindControl("A4") as RadNumericTextBox;


                     RadComboBox RadComboBox1 = item.FindControl("RadComboBox1") as RadComboBox;
                     DataTable tbl = cls.GetDataTable("sp_Check_ActivityGroup", new string[] { "@id" }, new object[] { RadComboBox1.SelectedValue });
                     double q1tam = 0, q2tam = 0, q3tam = 0, q4tam = 0;

                     RadComboBox rdCboBrandTang = item.FindControl("rdCboBrandTang") as RadComboBox;
                     RadComboBox rdCboCategoryTang = item.FindControl("rdCboCategoryTang") as RadComboBox;
                     HiddenField hfbatbuocchon = item.FindControl("hfbatbuocchon") as HiddenField;

                     if (hfbatbuocchon.Value.ToString() != "" && rdCboBrandTang.SelectedValue.ToString() == "0")
                     {
                         item.BackColor = System.Drawing.Color.Red;
                         item.Font.Bold = true;
                        
                         MsgBox1.AddMessage("Bạn phải chọn nhãn hàng và chủng loại hàng tặng cho các sự kiện này", uc.ucMsgBox.enmMessageType.Error);
                         return;
                     }

                     if (tbl.Rows[0]["MKT"].ToString() == "1")
                     {
                         foreach (GridDataItem item2 in RGKienThuc.Items)
                         {

                             RadComboBox RadComboBox1duyet = item2.FindControl("RadComboBox1") as RadComboBox;
                             RadComboBox rdCboBrandduyet = item2.FindControl("rdCboBrand") as RadComboBox;
                             DataTable tblduyet = cls.GetDataTable("sp_Check_ActivityGroup", new string[] { "@id" }, new object[] { RadComboBox1duyet.SelectedValue });

                             if (tbl.Rows[0]["ActivityGroup"].ToString() == tblduyet.Rows[0]["ActivityGroup"].ToString() && rdCboBrandduyet.SelectedValue == rdCboBrand.SelectedValue)
                             {
                                 RadNumericTextBox M1chay = item2.FindControl("M1") as RadNumericTextBox;
                                 RadNumericTextBox M2chay = item2.FindControl("M2") as RadNumericTextBox;
                                 RadNumericTextBox M3chay = item2.FindControl("M3") as RadNumericTextBox;
                                 RadNumericTextBox M4chay = item2.FindControl("M4") as RadNumericTextBox;

                                 RadNumericTextBox M5chay = item2.FindControl("M5") as RadNumericTextBox;
                                 RadNumericTextBox M6chay = item2.FindControl("M6") as RadNumericTextBox;
                                 RadNumericTextBox M7chay = item2.FindControl("M7") as RadNumericTextBox;
                                 RadNumericTextBox M8chay = item2.FindControl("M8") as RadNumericTextBox;

                                 RadNumericTextBox M9chay = item2.FindControl("M9") as RadNumericTextBox;
                                 RadNumericTextBox M10chay = item2.FindControl("M10") as RadNumericTextBox;
                                 RadNumericTextBox M11chay = item2.FindControl("M11") as RadNumericTextBox;
                                 RadNumericTextBox M12chay = item2.FindControl("M12") as RadNumericTextBox;

                                 q1tam = q1tam + (double)(M4chay.Value == null ? 0 : M4chay.Value) + (double)(M5chay.Value == null ? 0 : M5chay.Value) + (double)(M6chay.Value == null ? 0 : M6chay.Value);
                                 q2tam = q2tam + (double)(M7chay.Value == null ? 0 : M7chay.Value) + (double)(M8chay.Value == null ? 0 : M8chay.Value) + (double)(M9chay.Value == null ? 0 : M9chay.Value);
                                 q3tam = q3tam + (double)(M10chay.Value == null ? 0 : M10chay.Value) + (double)(M11chay.Value == null ? 0 : M11chay.Value) + (double)(M12chay.Value == null ? 0 : M12chay.Value);
                                 q4tam = q4tam + (double)(M1chay.Value == null ? 0 : M1chay.Value) + (double)(M2chay.Value == null ? 0 : M2chay.Value) + (double)(M3chay.Value == null ? 0 : M3chay.Value);

                             }


                         }
                     }
                     else
                     {
                         foreach (GridDataItem item2 in RGKienThuc.Items)
                         {

                             RadComboBox RadComboBox1duyet = item2.FindControl("RadComboBox1") as RadComboBox;
                             RadComboBox rdCboBrandduyet = item2.FindControl("rdCboBrand") as RadComboBox;
                             DataTable tblduyet = cls.GetDataTable("sp_Check_ActivityGroup", new string[] { "@id" }, new object[] { RadComboBox1duyet.SelectedValue });

                             if (tbl.Rows[0]["ActivityGroup"].ToString() == tblduyet.Rows[0]["ActivityGroup"].ToString())
                             {
                                 RadNumericTextBox M1chay = item2.FindControl("M1") as RadNumericTextBox;
                                 RadNumericTextBox M2chay = item2.FindControl("M2") as RadNumericTextBox;
                                 RadNumericTextBox M3chay = item2.FindControl("M3") as RadNumericTextBox;
                                 RadNumericTextBox M4chay = item2.FindControl("M4") as RadNumericTextBox;

                                 RadNumericTextBox M5chay = item2.FindControl("M5") as RadNumericTextBox;
                                 RadNumericTextBox M6chay = item2.FindControl("M6") as RadNumericTextBox;
                                 RadNumericTextBox M7chay = item2.FindControl("M7") as RadNumericTextBox;
                                 RadNumericTextBox M8chay = item2.FindControl("M8") as RadNumericTextBox;

                                 RadNumericTextBox M9chay = item2.FindControl("M9") as RadNumericTextBox;
                                 RadNumericTextBox M10chay = item2.FindControl("M10") as RadNumericTextBox;
                                 RadNumericTextBox M11chay = item2.FindControl("M11") as RadNumericTextBox;
                                 RadNumericTextBox M12chay = item2.FindControl("M12") as RadNumericTextBox;

                                 q1tam = q1tam + (double)(M4chay.Value == null ? 0 : M4chay.Value) + (double)(M5chay.Value == null ? 0 : M5chay.Value) + (double)(M6chay.Value == null ? 0 : M6chay.Value);
                                 q2tam = q2tam + (double)(M7chay.Value == null ? 0 : M7chay.Value) + (double)(M8chay.Value == null ? 0 : M8chay.Value) + (double)(M9chay.Value == null ? 0 : M9chay.Value);
                                 q3tam = q3tam + (double)(M10chay.Value == null ? 0 : M10chay.Value) + (double)(M11chay.Value == null ? 0 : M11chay.Value) + (double)(M12chay.Value == null ? 0 : M12chay.Value);
                                 q4tam = q4tam + (double)(M1chay.Value == null ? 0 : M1chay.Value) + (double)(M2chay.Value == null ? 0 : M2chay.Value) + (double)(M3chay.Value == null ? 0 : M3chay.Value);

                             }


                         }
                     }

                     if (txtDescription.Text == "" && (A1.Value != null || A2.Value != null || A3.Value != null || A4.Value != null))
                     {
                      

                         MsgBox1.AddMessage("Diễn giải không được rỗng  / Description is not empty", uc.ucMsgBox.enmMessageType.Error);
                         return;
                     }

                     if (txtDescription.Text != "")
                     {


                         double adada = calculationAvailable(int.Parse(RddatetimeFrom.SelectedDate.Value.Month.ToString()), int.Parse(RddatetimeTo.SelectedDate.Value.Month.ToString()), int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value),
                              int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue),
                              q1tam,q2tam,q3tam,q4tam, RddatetimeFrom.SelectedDate.Value);
                         if (adada == 0)
                         {
                             item.BackColor = System.Drawing.Color.Red;
                             item.Font.Bold = true;

                             MsgBox1.AddMessage("Ngân sách không đủ/insufficient budget", uc.ucMsgBox.enmMessageType.Error);
                           
                             Loaddetail_Quater(rdCboBrand, rdCboCategory, A1, A2, A3, A4, RadComboBox1, rdnumthanhtienQ1.Value == null ? 0 : (double)rdnumthanhtienQ1.Value, rdnumthanhtienQ2.Value == null ? 0 : (double)rdnumthanhtienQ2.Value, rdnumthanhtienQ3.Value == null ? 0 : (double)rdnumthanhtienQ3.Value, rdnumthanhtienQ4.Value == null ? 0 : (double)rdnumthanhtienQ4.Value);
                        

                             return;
                         }
                         else
                         {

                             item.BackColor = System.Drawing.Color.Green;


                         }

                     }


                 }
                 #endregion

                 #region Update Detail
                 foreach (GridDataItem item in RGKienThuc.Items)
                 {
                     TextBox txtDescription = item.FindControl("txtDescription") as TextBox;

                     RadComboBox rdCboBrand = item.FindControl("rdCboBrand") as RadComboBox;
                     RadComboBox rdCboCategory = item.FindControl("rdCboCategory") as RadComboBox;
                     RadComboBox RDregion = item.FindControl("RDregion") as RadComboBox;


                     RadNumericTextBox rdnumthanhtienQ1 = item.FindControl("rdnumthanhtienQ1") as RadNumericTextBox;
                     RadNumericTextBox rdnumthanhtienQ2 = item.FindControl("rdnumthanhtienQ2") as RadNumericTextBox;
                     RadNumericTextBox rdnumthanhtienQ3 = item.FindControl("rdnumthanhtienQ3") as RadNumericTextBox;
                     RadNumericTextBox rdnumthanhtienQ4 = item.FindControl("rdnumthanhtienQ4") as RadNumericTextBox;


                     RadNumericTextBox M1 = item.FindControl("M1") as RadNumericTextBox;
                     RadNumericTextBox M2 = item.FindControl("M2") as RadNumericTextBox;
                     RadNumericTextBox M3 = item.FindControl("M3") as RadNumericTextBox;
                     RadNumericTextBox M4 = item.FindControl("M4") as RadNumericTextBox;

                     RadNumericTextBox M5 = item.FindControl("M5") as RadNumericTextBox;
                     RadNumericTextBox M6 = item.FindControl("M6") as RadNumericTextBox;
                     RadNumericTextBox M7 = item.FindControl("M7") as RadNumericTextBox;
                     RadNumericTextBox M8 = item.FindControl("M8") as RadNumericTextBox;

                     RadNumericTextBox M9 = item.FindControl("M9") as RadNumericTextBox;
                     RadNumericTextBox M10 = item.FindControl("M10") as RadNumericTextBox;
                     RadNumericTextBox M11 = item.FindControl("M11") as RadNumericTextBox;
                     RadNumericTextBox M12 = item.FindControl("M12") as RadNumericTextBox;

                     RadNumericTextBox A1 = item.FindControl("A1") as RadNumericTextBox;
                     RadNumericTextBox A2 = item.FindControl("A2") as RadNumericTextBox;
                     RadNumericTextBox A3 = item.FindControl("A3") as RadNumericTextBox;
                     RadNumericTextBox A4 = item.FindControl("A4") as RadNumericTextBox;

                  



                     HiddenField HfID = item.FindControl("HfID") as HiddenField;
                     HiddenField hfaspf_fk = item.FindControl("hfaspf_fk") as HiddenField;
                     RadComboBox RadComboBox1 = item.FindControl("RadComboBox1") as RadComboBox;

                     if (txtDescription.Text != "")
                     {

                         RadComboBox rdCboBrandTang = item.FindControl("rdCboBrandTang") as RadComboBox;
                         RadComboBox rdCboCategoryTang = item.FindControl("rdCboCategoryTang") as RadComboBox;
                         HiddenField hfbatbuocchon = item.FindControl("hfbatbuocchon") as HiddenField;
                         if (hfbatbuocchon.Value.ToString() != "" && rdCboBrandTang.SelectedValue.ToString() == "0")
                         {
                             item.BackColor = System.Drawing.Color.Red;
                             item.Font.Bold = true;
                             
                             MsgBox1.AddMessage("Bạn phải chọn nhãn hàng và chủng loại hàng tặng cho các sự kiện này", uc.ucMsgBox.enmMessageType.Error);
                             return;
                         }


                         Obj = new clsObj();
                         Obj.Parameter = new string[] { "@ID", "@ASPF_FK", "@Description", "@Qty", "@Price", "@Amount", "@AccountCoding", "@Nhanhang_fk", "@Category_FK", "@AmountQ1", "@AmountQ2", "@AmountQ3", "@AmountQ4", "@Budget", "@region",
                           "@M1","@M2","@M3","@M4","@M5","@M6","@M7","@M8","@M9","@M10","@M11","@M12","@Nhanhang_fk_Tang","@Category_FK_Tang"};
                         Obj.ValueParameter = new object[] { HfID.Value,Request.QueryString["Userid"].ToString(), txtDescription.Text, 0, 0,0, RadComboBox1.SelectedValue, rdCboBrand.SelectedValue, rdCboCategory.SelectedValue,
                            rdnumthanhtienQ1.Value == null ? 0 : (double)rdnumthanhtienQ1.Value, rdnumthanhtienQ2.Value == null ? 0 : (double)rdnumthanhtienQ2.Value, rdnumthanhtienQ3.Value == null ? 0 : (double)rdnumthanhtienQ3.Value,rdnumthanhtienQ4.Value == null ? 0 : (double)rdnumthanhtienQ4.Value,
                              (A1.Value == null || A1.Value<0 ? 0 : (double)A1.Value)+ (A2.Value == null || A2.Value<0 ? 0 : (double)A2.Value)+ (A3.Value == null || A3.Value<0 ? 0 : (double)A3.Value)+ (A4.Value == null || A4.Value<0 ? 0 : (double)A4.Value)
                              ,RDregion.SelectedValue ,
                             M1.Value == null ? 0 : (double)M1.Value, M2.Value == null ? 0 : (double)M2.Value, M3.Value == null ? 0 : (double)M3.Value, M4.Value == null ? 0 : (double)M4.Value, M5.Value == null ? 0 : (double)M5.Value,
                          M6.Value == null ? 0 : (double)M6.Value, M7.Value == null ? 0 : (double)M7.Value, M8.Value == null ? 0 : (double)M8.Value,
                          M9.Value == null ? 0 : (double)M9.Value, M10.Value == null ? 0 : (double)M10.Value, M11.Value == null ? 0 : (double)M11.Value, M12.Value == null ? 0 : (double)M12.Value
                         ,  rdCboBrandTang.SelectedValue == "0" ? 0 : (int.Parse(rdCboBrandTang.SelectedValue.ToString())), rdCboCategoryTang.SelectedValue == "" ? 0 : (int.Parse(rdCboCategoryTang.SelectedValue.ToString()))  };
                         Obj.SpName = "sp_Update_ASPF_Detail_Bis";
                         Sql.fNonGetData(Obj);


                         //if (Session["ID_ASPF_Detail"].ToString() != "")
                         //{
                         //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Insert successfull');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
                         //}
                     }


                 }
                 #endregion



                // ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Insert successfull');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);

                 Session["CreateASPF"] = "Update successfull";
                 MsgBox1.AddMessage("Update successfull", uc.ucMsgBox.enmMessageType.Error);
                 Page.RegisterStartupScript("closeWindow", "<script type='text/javascript'>GetRadWindow().close()</script>");
             }
             else
             {
                 MsgBox1.AddMessage("Update failed. Please try again later", uc.ucMsgBox.enmMessageType.Error);

                 // ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Insert failed. Please try again later');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
                    
                // MsgBox1.AddMessage("Please input Reason Reject", uc.ucMsgBox.enmMessageType.Error);
             }


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
                 //if (quarterNumber1 == 1)
                 //    A1.Value = calculationAvailable_quater(int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value), int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue), 1, RddatetimeFrom.SelectedDate.Value.ToShortDateString());
                 //if (quarterNumber1 == 2)
                 //    A2.Value = calculationAvailable_quater(int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value), int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue), 2, RddatetimeFrom.SelectedDate.Value.ToShortDateString());
                 //if (quarterNumber1 == 3)
                 //    A3.Value = calculationAvailable_quater(int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value), int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue), 3, RddatetimeFrom.SelectedDate.Value.ToShortDateString());
                 //if (quarterNumber1 == 4)
                 //    A4.Value = calculationAvailable_quater(int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value), int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue), 4, RddatetimeFrom.SelectedDate.Value.ToShortDateString());

                 if (i == 1)
                     A1.Value = calculationAvailable_quater(int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value), int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue), 1, RddatetimeTo.SelectedDate.Value) - BookA1;
                 if (i == 2)
                     A2.Value = calculationAvailable_quater(int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value), int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue), 2, RddatetimeTo.SelectedDate.Value) - BookA2;
                 if (i == 3)
                     A3.Value = calculationAvailable_quater(int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value), int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue), 3, RddatetimeTo.SelectedDate.Value) - BookA3;
                 if (i == 4)
                     A4.Value = calculationAvailable_quater(int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value), int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue), 4, RddatetimeTo.SelectedDate.Value) - BookA4;
             }

             //}
             //else
             //{
             //    if (quarterNumber1 == 1)
             //        A1.Value = calculationAvailable_quater(int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value), int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue), quarterNumber1);
             //    if (quarterNumber1 == 2)
             //        A2.Value = calculationAvailable_quater(int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value), int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue), quarterNumber1);
             //    if (quarterNumber1 == 3)
             //        A3.Value = calculationAvailable_quater(int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value), int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue), quarterNumber1);
             //    if (quarterNumber1 == 4)
             //        A4.Value = calculationAvailable_quater(int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value), int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue), quarterNumber1);

             //}
         }

         protected void btnSubmit_Click(object sender, EventArgs e)
         {
             int monthStart, MonthEnd, quarterNumber1, quarterNumber2;

             Session["CreateASPF"] = "Update successfull";
             try
             {
                 #region check after Update
                 string filename = "";
                 int result = DateTime.Compare(RddatetimeFrom.SelectedDate.Value, RddatetimeTo.SelectedDate.Value);
                 if (result == 1)
                 {
                     MsgBox1.AddMessage("Từ ngày phải lớn hơn đến ngày / From day to day is greater than ", uc.ucMsgBox.enmMessageType.Error);

                     return;
                 }


                 if (txtObjective.Text == "")
                 {
                     MsgBox1.AddMessage("Mục tiêu không được rỗng  / Objective is not empty", uc.ucMsgBox.enmMessageType.Error);

                     return;
                 }

                 if (rdTypeofBudget.SelectedItem.Text == "Upcharge" && radnumtxtASPFvalue.Value > 300000000)
                 {
                     MsgBox1.AddMessage("Upcharge budget không được lớn hơn 300.000.000 VNĐ  / Upcharge budget is not greater than 300 million VND ", uc.ucMsgBox.enmMessageType.Error);

                     return;
                 }


                 if (rdTypeofBudget.SelectedItem.Text == "")
                 {
                     MsgBox1.AddMessage("Loại ngân sách không được rỗng  / type of budget is not emty", uc.ucMsgBox.enmMessageType.Error);

                     return;
                 }



                 //if (radnumtxtBudgetBalance.Value < 0)
                 //{
                 //    MsgBox1.AddMessage("Ngân sách còn lại phải lớn hơn 0 / Budget balance must be greater than 0", uc.ucMsgBox.enmMessageType.Error);

                 //    return;
                 //}

                 if (radnumtxtASPFvalue.Value <= 0)
                 {
                     MsgBox1.AddMessage("ASPF phải lớn hơn 0 / ASPF must be greater than 0", uc.ucMsgBox.enmMessageType.Error);

                     return;
                 }
                 //if (rdFYBudgetCAT.Value < rdAvailable.Value)
                 //{
                 //    MsgBox1.AddMessage("Available budget phải nhỏ hơn FY budget  / Available budget must be less than FY budget", uc.ucMsgBox.enmMessageType.Error);

                 //    return;
                 //}
                 //if (txtBudgetowner.Text == "")
                 //{
                 //    MsgBox1.AddMessage("Budget owner không được rỗng  / Budget owner is not empty", uc.ucMsgBox.enmMessageType.Error);

                 //    return;
                 //}
                 if (FileUpload1.HasFile)
                 {
                     try
                     {
                         int vt1 = FileUpload1.FileName.LastIndexOf(".");
                         int vtcanlay = vt1;
                         int len = FileUpload1.FileName.Length;
                         string filetemp = FileUpload1.FileName.Substring(0, vt1);
                         string extention = FileUpload1.FileName.Substring(vtcanlay, len - vtcanlay);
                         //     filename = Session["NewDocNo"].ToString() + "-" + Session["AutoNumber"].ToString();
                         filename = FileUpload1.FileName.Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("+", String.Empty).Replace("-", String.Empty).Replace(" ", String.Empty).Replace("*", String.Empty);
                         string sFolderPath = Server.MapPath("ImagesUpload/" + filename);

                         Random rnd = new Random();
                         filename = filetemp.Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("+", String.Empty).Replace("-", String.Empty).Replace(" ", String.Empty).Replace("*", String.Empty) + rnd.Next().ToString() + extention;

                         // }
                         filename = Regex.Replace(filename, @"\s", ""); //+ DateTime.Now.ToString("yyyyMMdd-HHMMss");
                      
                         sFolderPath = Server.MapPath("ImagesUpload/" + filename);
                         FileUpload1.SaveAs(sFolderPath);

                         // item.FileAttach = filename;

                     }
                     catch (Exception ex)
                     {
                         //hdPathSave.Value = "";
                         //    item.FileAttach = "";
                         Response.Write(ex.Message);
                     }
                 }
                 else
                 {
                     filename = "";
                 }



                 #endregion


                 #region Upadte header
                 Obj = new clsObj();
                 Obj.Parameter = new string[] { "@ASPNo","@DateCreation","@Country_FK","@BU_FK","@Brand_FK","@Category_FK","@Code","@GLCode","@From"
                                                   ,"@To","@Channel_Fk","@Objective","@FYbudgetcategory","@Availablebudget","@ASPFValue","@BudgetBalance","@BudgetDepartment","@Spent","@BudgetBalance_Controller"
                                                 ,"@ASPFValue_Controller","@AttachFile","@UserCreate","@UserEdit","@region","@upcharge","@N1","@BudgetOwnerN1"};
                 Obj.ValueParameter = new object[] { Request.QueryString["Userid"].ToString(), rddateCreation.SelectedDate, Rdcbocountry.SelectedValue, rdcboNganhHang.SelectedValue, "", "", 0, "", RddatetimeFrom.SelectedDate ,
                    RddatetimeTo.SelectedDate,rdChannel.SelectedValue,txtObjective.Text,"0","0",radnumtxtASPFvalue.Text,"0",0,0,0,0,filename,
                         Session["email"]  ,"","" , rdTypeofBudget.SelectedValue,rdNguoiduyet.Text,rdNguoiduyet.SelectedValue   };

                 Obj.SpName = "sp_Update_ASPF";
                 Sql.fNonGetData(Obj);

                 #endregion


                 #region check after Detail
                 foreach (GridDataItem item in RGKienThuc.Items)
                 {
                     TextBox txtDescription = item.FindControl("txtDescription") as TextBox;

                     RadComboBox rdCboBrand = item.FindControl("rdCboBrand") as RadComboBox;
                     RadComboBox rdCboCategory = item.FindControl("rdCboCategory") as RadComboBox;

                     RadNumericTextBox rdnumthanhtienQ1 = item.FindControl("rdnumthanhtienQ1") as RadNumericTextBox;
                     RadNumericTextBox rdnumthanhtienQ2 = item.FindControl("rdnumthanhtienQ2") as RadNumericTextBox;
                     RadNumericTextBox rdnumthanhtienQ3 = item.FindControl("rdnumthanhtienQ3") as RadNumericTextBox;
                     RadNumericTextBox rdnumthanhtienQ4 = item.FindControl("rdnumthanhtienQ4") as RadNumericTextBox;
                     RadNumericTextBox A1 = item.FindControl("A1") as RadNumericTextBox;
                     RadNumericTextBox A2 = item.FindControl("A2") as RadNumericTextBox;
                     RadNumericTextBox A3 = item.FindControl("A3") as RadNumericTextBox;
                     RadNumericTextBox A4 = item.FindControl("A4") as RadNumericTextBox;


                     RadComboBox RadComboBox1 = item.FindControl("RadComboBox1") as RadComboBox;

                     DataTable tbl = cls.GetDataTable("sp_Check_ActivityGroup", new string[] { "@id" }, new object[] { RadComboBox1.SelectedValue });
                     double q1tam = 0, q2tam = 0, q3tam = 0, q4tam = 0;

                     if (tbl.Rows[0]["MKT"].ToString() == "1")
                     {
                         foreach (GridDataItem item2 in RGKienThuc.Items)
                         {

                             RadComboBox RadComboBox1duyet = item2.FindControl("RadComboBox1") as RadComboBox;
                             RadComboBox rdCboBrandduyet = item2.FindControl("rdCboBrand") as RadComboBox;
                             DataTable tblduyet = cls.GetDataTable("sp_Check_ActivityGroup", new string[] { "@id" }, new object[] { RadComboBox1duyet.SelectedValue });

                             if (tbl.Rows[0]["ActivityGroup"].ToString() == tblduyet.Rows[0]["ActivityGroup"].ToString() && rdCboBrandduyet.SelectedValue == rdCboBrand.SelectedValue)
                             {
                                 RadNumericTextBox M1chay = item2.FindControl("M1") as RadNumericTextBox;
                                 RadNumericTextBox M2chay = item2.FindControl("M2") as RadNumericTextBox;
                                 RadNumericTextBox M3chay = item2.FindControl("M3") as RadNumericTextBox;
                                 RadNumericTextBox M4chay = item2.FindControl("M4") as RadNumericTextBox;

                                 RadNumericTextBox M5chay = item2.FindControl("M5") as RadNumericTextBox;
                                 RadNumericTextBox M6chay = item2.FindControl("M6") as RadNumericTextBox;
                                 RadNumericTextBox M7chay = item2.FindControl("M7") as RadNumericTextBox;
                                 RadNumericTextBox M8chay = item2.FindControl("M8") as RadNumericTextBox;

                                 RadNumericTextBox M9chay = item2.FindControl("M9") as RadNumericTextBox;
                                 RadNumericTextBox M10chay = item2.FindControl("M10") as RadNumericTextBox;
                                 RadNumericTextBox M11chay = item2.FindControl("M11") as RadNumericTextBox;
                                 RadNumericTextBox M12chay = item2.FindControl("M12") as RadNumericTextBox;

                                 q1tam = q1tam + (double)(M4chay.Value == null ? 0 : M4chay.Value) + (double)(M5chay.Value == null ? 0 : M5chay.Value) + (double)(M6chay.Value == null ? 0 : M6chay.Value);
                                 q2tam = q2tam + (double)(M7chay.Value == null ? 0 : M7chay.Value) + (double)(M8chay.Value == null ? 0 : M8chay.Value) + (double)(M9chay.Value == null ? 0 : M9chay.Value);
                                 q3tam = q3tam + (double)(M10chay.Value == null ? 0 : M10chay.Value) + (double)(M11chay.Value == null ? 0 : M11chay.Value) + (double)(M12chay.Value == null ? 0 : M12chay.Value);
                                 q4tam = q4tam + (double)(M1chay.Value == null ? 0 : M1chay.Value) + (double)(M2chay.Value == null ? 0 : M2chay.Value) + (double)(M3chay.Value == null ? 0 : M3chay.Value);

                             }


                         }
                     }
                     else
                     {
                         foreach (GridDataItem item2 in RGKienThuc.Items)
                         {

                             RadComboBox RadComboBox1duyet = item2.FindControl("RadComboBox1") as RadComboBox;
                             RadComboBox rdCboBrandduyet = item2.FindControl("rdCboBrand") as RadComboBox;
                             DataTable tblduyet = cls.GetDataTable("sp_Check_ActivityGroup", new string[] { "@id" }, new object[] { RadComboBox1duyet.SelectedValue });

                             if (tbl.Rows[0]["ActivityGroup"].ToString() == tblduyet.Rows[0]["ActivityGroup"].ToString())
                             {
                                 RadNumericTextBox M1chay = item2.FindControl("M1") as RadNumericTextBox;
                                 RadNumericTextBox M2chay = item2.FindControl("M2") as RadNumericTextBox;
                                 RadNumericTextBox M3chay = item2.FindControl("M3") as RadNumericTextBox;
                                 RadNumericTextBox M4chay = item2.FindControl("M4") as RadNumericTextBox;

                                 RadNumericTextBox M5chay = item2.FindControl("M5") as RadNumericTextBox;
                                 RadNumericTextBox M6chay = item2.FindControl("M6") as RadNumericTextBox;
                                 RadNumericTextBox M7chay = item2.FindControl("M7") as RadNumericTextBox;
                                 RadNumericTextBox M8chay = item2.FindControl("M8") as RadNumericTextBox;

                                 RadNumericTextBox M9chay = item2.FindControl("M9") as RadNumericTextBox;
                                 RadNumericTextBox M10chay = item2.FindControl("M10") as RadNumericTextBox;
                                 RadNumericTextBox M11chay = item2.FindControl("M11") as RadNumericTextBox;
                                 RadNumericTextBox M12chay = item2.FindControl("M12") as RadNumericTextBox;

                                 q1tam = q1tam + (double)(M4chay.Value == null ? 0 : M4chay.Value) + (double)(M5chay.Value == null ? 0 : M5chay.Value) + (double)(M6chay.Value == null ? 0 : M6chay.Value);
                                 q2tam = q2tam + (double)(M7chay.Value == null ? 0 : M7chay.Value) + (double)(M8chay.Value == null ? 0 : M8chay.Value) + (double)(M9chay.Value == null ? 0 : M9chay.Value);
                                 q3tam = q3tam + (double)(M10chay.Value == null ? 0 : M10chay.Value) + (double)(M11chay.Value == null ? 0 : M11chay.Value) + (double)(M12chay.Value == null ? 0 : M12chay.Value);
                                 q4tam = q4tam + (double)(M1chay.Value == null ? 0 : M1chay.Value) + (double)(M2chay.Value == null ? 0 : M2chay.Value) + (double)(M3chay.Value == null ? 0 : M3chay.Value);

                             }


                         }
                     }

                     if (txtDescription.Text == "" && (A1.Value != null || A2.Value != null || A3.Value != null || A4.Value != null))
                     {
                         

                         MsgBox1.AddMessage("Diễn giải không được rỗng  / Description is not empty", uc.ucMsgBox.enmMessageType.Error);
                         return;
                     }

                     if (txtDescription.Text != "")
                     {


                         double adada = calculationAvailable(int.Parse(RddatetimeFrom.SelectedDate.Value.Month.ToString()), int.Parse(RddatetimeTo.SelectedDate.Value.Month.ToString()), int.Parse(Rdcbocountry.SelectedValue), int.Parse(hfBudgetowner.Value),
                              int.Parse(RadComboBox1.SelectedValue), int.Parse(rdCboBrand.SelectedValue), int.Parse(rdCboCategory.SelectedValue),
                            q1tam,q2tam,q3tam,q4tam, RddatetimeFrom.SelectedDate.Value);
                         if (adada == 0)
                         {
                             item.BackColor = System.Drawing.Color.Red;
                             item.Font.Bold = true;
                          

                             MsgBox1.AddMessage("Ngân sách không đủ/insufficient budget", uc.ucMsgBox.enmMessageType.Error);

                             Loaddetail_Quater(rdCboBrand, rdCboCategory, A1, A2, A3, A4, RadComboBox1, rdnumthanhtienQ1.Value == null ? 0 : (double)rdnumthanhtienQ1.Value, rdnumthanhtienQ2.Value == null ? 0 : (double)rdnumthanhtienQ2.Value, rdnumthanhtienQ3.Value == null ? 0 : (double)rdnumthanhtienQ3.Value, rdnumthanhtienQ4.Value == null ? 0 : (double)rdnumthanhtienQ4.Value);
    

                             return;
                         }
                         else
                         {

                             item.BackColor = System.Drawing.Color.Green;


                         }

                     }


                 }
                 #endregion

                 #region Update Detail
                 foreach (GridDataItem item in RGKienThuc.Items)
                 {
                     TextBox txtDescription = item.FindControl("txtDescription") as TextBox;

                     RadComboBox rdCboBrand = item.FindControl("rdCboBrand") as RadComboBox;
                     RadComboBox rdCboCategory = item.FindControl("rdCboCategory") as RadComboBox;
                     RadComboBox RDregion = item.FindControl("RDregion") as RadComboBox;


                     RadNumericTextBox rdnumthanhtienQ1 = item.FindControl("rdnumthanhtienQ1") as RadNumericTextBox;
                     RadNumericTextBox rdnumthanhtienQ2 = item.FindControl("rdnumthanhtienQ2") as RadNumericTextBox;
                     RadNumericTextBox rdnumthanhtienQ3 = item.FindControl("rdnumthanhtienQ3") as RadNumericTextBox;
                     RadNumericTextBox rdnumthanhtienQ4 = item.FindControl("rdnumthanhtienQ4") as RadNumericTextBox;
                     RadNumericTextBox M1 = item.FindControl("M1") as RadNumericTextBox;
                     RadNumericTextBox M2 = item.FindControl("M2") as RadNumericTextBox;
                     RadNumericTextBox M3 = item.FindControl("M3") as RadNumericTextBox;
                     RadNumericTextBox M4 = item.FindControl("M4") as RadNumericTextBox;

                     RadNumericTextBox M5 = item.FindControl("M5") as RadNumericTextBox;
                     RadNumericTextBox M6 = item.FindControl("M6") as RadNumericTextBox;
                     RadNumericTextBox M7 = item.FindControl("M7") as RadNumericTextBox;
                     RadNumericTextBox M8 = item.FindControl("M8") as RadNumericTextBox;

                     RadNumericTextBox M9 = item.FindControl("M9") as RadNumericTextBox;
                     RadNumericTextBox M10 = item.FindControl("M10") as RadNumericTextBox;
                     RadNumericTextBox M11 = item.FindControl("M11") as RadNumericTextBox;
                     RadNumericTextBox M12 = item.FindControl("M12") as RadNumericTextBox;

                     RadNumericTextBox A1 = item.FindControl("A1") as RadNumericTextBox;
                     RadNumericTextBox A2 = item.FindControl("A2") as RadNumericTextBox;
                     RadNumericTextBox A3 = item.FindControl("A3") as RadNumericTextBox;
                     RadNumericTextBox A4 = item.FindControl("A4") as RadNumericTextBox;

                     HiddenField HfID = item.FindControl("HfID") as HiddenField;
                     HiddenField hfaspf_fk = item.FindControl("hfaspf_fk") as HiddenField;
                     RadComboBox RadComboBox1 = item.FindControl("RadComboBox1") as RadComboBox;
                    

                     if (txtDescription.Text != "")
                     {

                         RadComboBox rdCboBrandTang = item.FindControl("rdCboBrandTang") as RadComboBox;
                         RadComboBox rdCboCategoryTang = item.FindControl("rdCboCategoryTang") as RadComboBox;
                         HiddenField hfbatbuocchon = item.FindControl("hfbatbuocchon") as HiddenField;
                         if (hfbatbuocchon.Value.ToString() != "" && rdCboBrandTang.SelectedValue.ToString() == "0")
                         {
                             item.BackColor = System.Drawing.Color.Red;
                             item.Font.Bold = true;

                             Sql.fNonGetData(Obj);
                             MsgBox1.AddMessage("Bạn phải chọn nhãn hàng và chủng loại hàng tặng cho các sự kiện này", uc.ucMsgBox.enmMessageType.Error);
                             return;
                         }

                         Obj = new clsObj();
                         Obj.Parameter = new string[] { "@ID", "@ASPF_FK", "@Description", "@Qty", "@Price", "@Amount", "@AccountCoding", "@Nhanhang_fk", "@Category_FK", "@AmountQ1", "@AmountQ2", "@AmountQ3", "@AmountQ4", "@Budget", "@region",
                           "@M1","@M2","@M3","@M4","@M5","@M6","@M7","@M8","@M9","@M10","@M11","@M12","@Nhanhang_fk_Tang","@Category_FK_Tang"};
                         Obj.ValueParameter = new object[] { HfID.Value,Request.QueryString["Userid"].ToString(), txtDescription.Text, 0, 0,0, RadComboBox1.SelectedValue, rdCboBrand.SelectedValue, rdCboCategory.SelectedValue,
                            rdnumthanhtienQ1.Value == null ? 0 : (double)rdnumthanhtienQ1.Value, rdnumthanhtienQ2.Value == null ? 0 : (double)rdnumthanhtienQ2.Value, rdnumthanhtienQ3.Value == null ? 0 : (double)rdnumthanhtienQ3.Value,rdnumthanhtienQ4.Value == null ? 0 : (double)rdnumthanhtienQ4.Value,
                              (A1.Value == null || A1.Value<0 ? 0 : (double)A1.Value)+ (A2.Value == null || A2.Value<0 ? 0 : (double)A2.Value)+ (A3.Value == null || A3.Value<0 ? 0 : (double)A3.Value)+ (A4.Value == null || A4.Value<0 ? 0 : (double)A4.Value)
                              ,RDregion.SelectedValue ,
                             M1.Value == null ? 0 : (double)M1.Value, M2.Value == null ? 0 : (double)M2.Value, M3.Value == null ? 0 : (double)M3.Value, M4.Value == null ? 0 : (double)M4.Value, M5.Value == null ? 0 : (double)M5.Value,
                          M6.Value == null ? 0 : (double)M6.Value, M7.Value == null ? 0 : (double)M7.Value, M8.Value == null ? 0 : (double)M8.Value,
                          M9.Value == null ? 0 : (double)M9.Value, M10.Value == null ? 0 : (double)M10.Value, M11.Value == null ? 0 : (double)M11.Value, M12.Value == null ? 0 : (double)M12.Value
                         ,  rdCboBrandTang.SelectedValue == "0" ? 0 : (int.Parse(rdCboBrandTang.SelectedValue.ToString())), rdCboCategoryTang.SelectedValue == "" ? 0 : (int.Parse(rdCboCategoryTang.SelectedValue.ToString()))  };
                         Obj.SpName = "sp_Update_ASPF_Detail_Bis";
                         Sql.fNonGetData(Obj);



                         //if (Session["ID_ASPF_Detail"].ToString() != "")
                         //{
                         //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Insert successfull');" + Page.ClientScript.GetPostBackEventReference(this, "") + ";", true);
                         //}
                     }


                 }
                 #endregion




                 Obj = new clsObj();
                 Obj.Parameter = new string[] { "@aspfid" };
                 Obj.ValueParameter = new object[] { Request.QueryString["Userid"].ToString() };
                 Obj.SpName = "sp_Insert_Approve_ASPF";
                 Sql.fNonGetData(Obj);
                 //  MsgBox1.AddMessage("Update successfull", uc.ucMsgBox.enmMessageType.Error);

                 DataTable tblto = cls.GetDataTable("sp_Load_content_Bylevel", new string[] { "@docno", "@levelapp" }, new object[] { Request.QueryString["Userid"].ToString(), 2 });
                 if(tblto.Rows.Count>0)
                 {
                     string content = sys.noidung(tblto.Rows[0]["nguoiduyet"].ToString(),tblto.Rows[0]["nguoitao"].ToString(),tblto.Rows[0]["Country"].ToString(),tblto.Rows[0]["BudgetOwner"].ToString(),tblto.Rows[0]["BU"].ToString(),
                  tblto.Rows[0]["Category"].ToString(), tblto.Rows[0]["ASPNo"].ToString(), tblto.Rows[0]["ASPFvalue"].ToString(), tblto.Rows[0]["Objective"].ToString(), tblto.Rows[0]["BudgetDepartment"].ToString(), tblto.Rows[0]["Spent"].ToString(), tblto.Rows[0]["BudgetBalance_Controller"].ToString(), tblto.Rows[0]["ApprovedCode"].ToString() , Request.QueryString["Userid"].ToString());


                     sys.SendMailASPAtt(tblto.Rows[0]["emailto"].ToString(), "Yêu cầu duyệt ASPF (ID :" + Request.QueryString["Userid"].ToString() + ") từ " + tblto.Rows[0]["nguoitao"].ToString() + "/ASPF approval request from " + tblto.Rows[0]["nguoitao"].ToString(), content, tblto.Rows[0]["attachfile"].ToString() == "" ? "" : "~/ImagesUpload/" + tblto.Rows[0]["attachfile"].ToString());



                 }

                 

               //  sys.SendMailASPAtt(


                 Page.RegisterStartupScript("closeWindow", "<script type='text/javascript'>GetRadWindow().close()</script>");
             }
             catch
             {
                 MsgBox1.AddMessage("Update failed. Please try again later", uc.ucMsgBox.enmMessageType.Error);
             }
           
         }


         protected void Button1_Click(object sender, EventArgs e)
         {

             Page.RegisterStartupScript("closeWindow", "<script type='text/javascript'>GetRadWindow().close()</script>");
         }

         protected void Button2_Click(object sender, EventArgs e)
         {
             

         }

         protected void rdChannel_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
         {
             for (int i = 0; i < RGKienThuc.Items.Count; i++)
             {

                 RadComboBox RDregion = (RGKienThuc.Items[i].FindControl("RDregion") as RadComboBox);

                 DataTable tbl1111 = cls.GetDataTable("sp_Load_region", new string[] { "@channel_fk" }, new object[] { rdChannel.SelectedValue });
                 RDregion.ClearSelection();
                 RDregion.DataSource = tbl1111;
                 RDregion.DataBind();
             }
         }

         protected void rdTypeofBudget_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
         {
             loadbudget();
         }

         protected void rdNguoiduyet_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
         {

             // txtBudgetowner.Text = rdNguoiduyet.Text; //.te tbl.Rows[0]["BudgetOwner"].ToString();
             hfBudgetowner.Value = rdNguoiduyet.SelectedValue; //tbl.Rows[0]["ID"].ToString();
             loadbudget();
         }

       

    }

}
