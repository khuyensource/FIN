using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
using Telerik.Web.UI;
namespace MaricoPay
{
    public partial class TransferBudget : System.Web.UI.Page
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
                LoadDepartment();
                LoadBU(Rdcbocountry_To.SelectedValue, rdcboBU_To);
                LoadBU(Rdcbocountry_From.SelectedValue, rdcboBU_From);
                LoadBrand(rdcboBU_From.SelectedValue, RdBrand_From);
                LoadBrand(rdcboBU_To.SelectedValue, RdBrand_To);
                LoadCAT(RdBrand_From.SelectedValue, RdCat_From);
                LoadCAT(RdBrand_To.SelectedValue, RdCat_To);
                Load_ActiviGroup();
                calculationAvailable_quater(int.Parse(Rdcbocountry_From.SelectedValue), int.Parse(RdDepartment_from.SelectedValue), int.Parse(RdActivityGroup_From.SelectedValue), int.Parse(RdBrand_From.SelectedValue), int.Parse(RdCat_From.SelectedValue), int.Parse(rdquater.SelectedValue), radnumtxtAivailable_From);
                calculationAvailable_quater(int.Parse(Rdcbocountry_To.SelectedValue), int.Parse(RdDepartment_To.SelectedValue), int.Parse(RdActivityGroup_To.SelectedValue), int.Parse(RdBrand_From.SelectedValue), int.Parse(RdCat_To.SelectedValue), int.Parse(rdquater.SelectedValue), radnumtxtAivailable_To);

            }
        }

        private double CalculationCommitment(int Country_FK, int BudgetOwner, int ActivityGroup, int Brand_fk, int Cat_Fk, int Quater, string Upcharge)
        {

            double trave = 0;

            DataTable tbl = cls.GetDataTable("sp_Load_COMMITMENT_ASPF_ActivityGroup", new string[] { "@Country_fk", "@BudgetOwner", "@ActivityGroup1", "@Brand_fk", "@Cat_Fk", "@Quater", "@Upcharge" },
                new object[] { Country_FK, BudgetOwner, ActivityGroup, Brand_fk, Cat_Fk, Quater, Upcharge });
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


        private double Calculationbudget(int Country_FK, int BudgetOwner, int ActivityGroup, int Brand_fk, int Cat_Fk, int Quater, string Upcharge)
        {


            double trave = 0;


            DataTable tbl = cls.GetDataTable("sp_Load_budget_ASPF_ActivityGroup", new string[] { "@Country_fk", "@BudgetOwner", "@ActivityGroup1", "@Brand_fk", "@Cat_Fk", "@Quater" },
                new object[] { Country_FK, BudgetOwner, ActivityGroup, Brand_fk, Cat_Fk, Quater });
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




        private void Load_ActiviGroup()
        {
            DataTable tbl = cls.GetDataTable("sp_Load_ActivityGroup");
            RdActivityGroup_From.DataSource = tbl;
            RdActivityGroup_From.DataBind();

            RdActivityGroup_To.DataSource = tbl;
            RdActivityGroup_To.DataBind();

        }
        private void LoadBU(string Country_fk, RadComboBox combobox)
        {

            DataTable tbl = cls.GetDataTable("sp_Load_BU", new string[] { "@Country_fk" }, new object[] { Country_fk });
            combobox.DataSource = tbl;
            combobox.DataBind();
        }
        private void LoadBrand(string nganhhang, RadComboBox combobox)
        {

            DataTable tbl1 = cls.GetDataTable("sp_Load_Brand_ByBU", new string[] { "@BU" }, new object[] { nganhhang });

            //   DataTable tbl = cls.GetDataTable("sp_Load_AccountCoding_Bis", new string[] { "@id", "@userid" }, new object[] { "", Session["username"] });
            combobox.DataSource = tbl1;
            combobox.DataBind();
        }
        private void LoadCAT(string BrandFK, RadComboBox combobox)
        {
            DataTable tbl122 = cls.GetDataTable("sp_Load_Category_ByBrand", new string[] { "@brand_fk" }, new object[] { BrandFK });
            combobox.DataSource = tbl122;
            combobox.DataBind();
        }
        private void LoadCountry()
        {

            DataTable tbl = cls.GetDataTable("sp_LoadCountry");
            Rdcbocountry_From.DataSource = tbl;
            Rdcbocountry_From.DataBind();
            Rdcbocountry_To.DataSource = tbl;
            Rdcbocountry_To.DataBind();
        }

        private void LoadDepartment()
        {

            DataTable tbl = cls.GetDataTable("sp_Load_budgetOwner");
            RdDepartment_from.DataSource = tbl;
            RdDepartment_from.DataBind();
            RdDepartment_To.DataSource = tbl;
            RdDepartment_To.DataBind();

        }

        private void calculationAvailable_quater(int Country_FK, int BudgetOwner, int ActivityGroup, int Brand_fk, int Cat_Fk, int quater, RadNumericTextBox textbox)
        {
            double trave;
            trave = Calculationbudget(Country_FK, BudgetOwner, ActivityGroup, Brand_fk, Cat_Fk, quater, "Normal") -
                      CalculationCommitment(Country_FK, BudgetOwner, ActivityGroup, Brand_fk, Cat_Fk, quater, "Normal");


            textbox.Value = trave;



        }

        protected void Rdcbocountry_From_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadBU(Rdcbocountry_From.SelectedValue, rdcboBU_From);
            calculationAvailable_quater(int.Parse(Rdcbocountry_From.SelectedValue), int.Parse(RdDepartment_from.SelectedValue), int.Parse(RdActivityGroup_From.SelectedValue), int.Parse(RdBrand_From.SelectedValue), int.Parse(RdCat_From.SelectedValue), int.Parse(rdquater.SelectedValue), radnumtxtAivailable_From);
        }

        protected void Rdcbocountry_To_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadBU(Rdcbocountry_To.SelectedValue, rdcboBU_To);
            calculationAvailable_quater(int.Parse(Rdcbocountry_To.SelectedValue), int.Parse(RdDepartment_To.SelectedValue), int.Parse(RdActivityGroup_To.SelectedValue), int.Parse(RdBrand_From.SelectedValue), int.Parse(RdCat_To.SelectedValue), int.Parse(rdquater.SelectedValue), radnumtxtAivailable_To);

        }

        protected void rdcboBrand_From_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadBrand(rdcboBU_From.SelectedValue, RdBrand_From);
            calculationAvailable_quater(int.Parse(Rdcbocountry_From.SelectedValue), int.Parse(RdDepartment_from.SelectedValue), int.Parse(RdActivityGroup_From.SelectedValue), int.Parse(RdBrand_From.SelectedValue), int.Parse(RdCat_From.SelectedValue), int.Parse(rdquater.SelectedValue), radnumtxtAivailable_From);
            calculationAvailable_quater(int.Parse(Rdcbocountry_To.SelectedValue), int.Parse(RdDepartment_To.SelectedValue), int.Parse(RdActivityGroup_To.SelectedValue), int.Parse(RdBrand_From.SelectedValue), int.Parse(RdCat_To.SelectedValue), int.Parse(rdquater.SelectedValue), radnumtxtAivailable_To);

        }

        protected void rdcboBrand_To_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadBrand(rdcboBU_To.SelectedValue, RdBrand_To);
            calculationAvailable_quater(int.Parse(Rdcbocountry_To.SelectedValue), int.Parse(RdDepartment_To.SelectedValue), int.Parse(RdActivityGroup_To.SelectedValue), int.Parse(RdBrand_From.SelectedValue), int.Parse(RdCat_To.SelectedValue), int.Parse(rdquater.SelectedValue), radnumtxtAivailable_To);

        }

        protected void RdBrand_From_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadCAT(RdBrand_From.SelectedValue, RdCat_From);
            calculationAvailable_quater(int.Parse(Rdcbocountry_From.SelectedValue), int.Parse(RdDepartment_from.SelectedValue), int.Parse(RdActivityGroup_From.SelectedValue), int.Parse(RdBrand_From.SelectedValue), int.Parse(RdCat_From.SelectedValue), int.Parse(rdquater.SelectedValue), radnumtxtAivailable_From);

        }

        protected void RdBrand_To_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadCAT(RdBrand_To.SelectedValue, RdCat_To);
            // Calculationbudget(int.Parse(Rdcbocountry_To.SelectedValue), int.Parse(RdDepartment_To.SelectedValue), int.Parse(RdActivityGroup_To.SelectedValue), int.Parse(RdBrand_From.SelectedValue), int.Parse(RdCat_To.SelectedValue), int.Parse(rdquater.SelectedValue), radnumtxtAivailable_To);

        }

        protected void RdActivityGroup_From_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculationAvailable_quater(int.Parse(Rdcbocountry_From.SelectedValue), int.Parse(RdDepartment_from.SelectedValue), int.Parse(RdActivityGroup_From.SelectedValue), int.Parse(RdBrand_From.SelectedValue), int.Parse(RdCat_From.SelectedValue), int.Parse(rdquater.SelectedValue), radnumtxtAivailable_From);

        }

        protected void RdCat_From_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculationAvailable_quater(int.Parse(Rdcbocountry_From.SelectedValue), int.Parse(RdDepartment_from.SelectedValue), int.Parse(RdActivityGroup_From.SelectedValue), int.Parse(RdBrand_From.SelectedValue), int.Parse(RdCat_From.SelectedValue), int.Parse(rdquater.SelectedValue), radnumtxtAivailable_From);

        }

        protected void RdDepartment_from_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculationAvailable_quater(int.Parse(Rdcbocountry_From.SelectedValue), int.Parse(RdDepartment_from.SelectedValue), int.Parse(RdActivityGroup_From.SelectedValue), int.Parse(RdBrand_From.SelectedValue), int.Parse(RdCat_From.SelectedValue), int.Parse(rdquater.SelectedValue), radnumtxtAivailable_From);

        }

        protected void rdquater_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculationAvailable_quater(int.Parse(Rdcbocountry_From.SelectedValue), int.Parse(RdDepartment_from.SelectedValue), int.Parse(RdActivityGroup_From.SelectedValue), int.Parse(RdBrand_From.SelectedValue), int.Parse(RdCat_From.SelectedValue), int.Parse(rdquater.SelectedValue), radnumtxtAivailable_From);
            calculationAvailable_quater(int.Parse(Rdcbocountry_To.SelectedValue), int.Parse(RdDepartment_To.SelectedValue), int.Parse(RdActivityGroup_To.SelectedValue), int.Parse(RdBrand_From.SelectedValue), int.Parse(RdCat_To.SelectedValue), int.Parse(rdquater.SelectedValue), radnumtxtAivailable_To);

        }

        protected void RdCat_To_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculationAvailable_quater(int.Parse(Rdcbocountry_To.SelectedValue), int.Parse(RdDepartment_To.SelectedValue), int.Parse(RdActivityGroup_To.SelectedValue), int.Parse(RdBrand_From.SelectedValue), int.Parse(RdCat_To.SelectedValue), int.Parse(rdquater.SelectedValue), radnumtxtAivailable_To);

        }

        protected void RdActivityGroup_To_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculationAvailable_quater(int.Parse(Rdcbocountry_To.SelectedValue), int.Parse(RdDepartment_To.SelectedValue), int.Parse(RdActivityGroup_To.SelectedValue), int.Parse(RdBrand_From.SelectedValue), int.Parse(RdCat_To.SelectedValue), int.Parse(rdquater.SelectedValue), radnumtxtAivailable_To);

        }



        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (radnumtxtASPFvalue_From.Value == 0 || radnumtxtASPFvalue_From.Value.ToString() == "")
            {
                MsgBox1.AddMessage("Số tiền cần chuyển phải lớn hơn 0 / Amount to be transferred greater than 0", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (radnumtxtASPFvalue_From.Value > radnumtxtAivailable_From.Value)
            {
                MsgBox1.AddMessage("Số tiền cần chuyển không được lớn hơn ngân sách còn lại / Amount to be transferred must be within the remaining budget", uc.ucMsgBox.enmMessageType.Error);
                return;
            }

            Obj = new clsObj();
            Obj.Parameter = new string[] { "@Usercreate","@Country_from","@Function_from","@Brand_from","@Cat_from", "@ActivityGroup_from","@Available_From","@Value_from",
                 "@Country_to","@Function_to","@Brand_to","@Cat_to","@ActivityGroup_To","@Available_To","@Value_to","@Quater","@Approved" };
            Obj.ValueParameter = new object[] { "minh.vo@marico.com", Rdcbocountry_From.SelectedValue,RdDepartment_from.SelectedValue,RdBrand_From.SelectedValue,RdCat_From.SelectedValue,RdActivityGroup_From.SelectedValue,radnumtxtAivailable_From.Value,radnumtxtASPFvalue_From.Value,
                Rdcbocountry_To.SelectedValue,RdDepartment_To.SelectedValue,RdBrand_To.SelectedValue,RdCat_To.SelectedValue,RdActivityGroup_To.SelectedValue,radnumtxtAivailable_To.Value,radnumtxtASPFvalue_From.Value,rdquater.SelectedValue, 0};


                Obj.ParameterOutput = new string[] { "@IDout" };
                 Obj.ValueOutput = new string[] { "0" };
                 Obj.SpName = "sp_insert_Autotranfer_detail";
                 Sql.fNonGetData_Out(Obj);
                 Session["ID_ATF"] = int.Parse(Obj.ValueOutput[0].ToString());
                 Session["TransferBudget"] = "sp_insert_Autotranfer_detail";

                    Obj = new clsObj();
                     Obj.Parameter = new string[] { "@aspfid" };
                     Obj.ValueParameter = new object[] { Session["ID_ATF"].ToString() };
                     Obj.SpName = "sp_Insert_Approve_Autotranfer_detail";
                     Sql.fNonGetData(Obj);

    

            string myclosescript = "<script language='javascript' type='text/javascript'>CloseWindow11();</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "myclosescript", myclosescript);

            Page.RegisterStartupScript("closeWindow", "<script type='text/javascript'>GetRadWindow().close()</script>");

        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            string myclosescript = "<script language='javascript' type='text/javascript'>CloseWindow11();</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "myclosescript", myclosescript);

            Page.RegisterStartupScript("closeWindow", "<script type='text/javascript'>GetRadWindow().close()</script>");
        }

       
        
    }
}