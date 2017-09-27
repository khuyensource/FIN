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
    public partial class RejectOnline : System.Web.UI.Page
    {

        Cclass cls = new Cclass();
        clsObj Obj = new clsObj();
        clsSql Sql = new clsSql();
        clsSys sys = new clsSys();
        public DataTable dtdetail;
        protected void Page_Load(object sender, EventArgs e)
        {


            //if (Session["email"] != null)
            //{

                if (!IsPostBack)
                {

                    //LoadCountry();
                    //LoadBU();
                    //LoadBrand_BU(rdcboNganhHang.SelectedValue);
                    //Load_Category_ByBrand(rdCboBrand.SelectedValue);
                    // Load_AccountCoding("");
                    // Load_AccountCoding_Detail(Rdcode.SelectedValue);
                  //  Load_Channel();

                    // LoadASPF_Detail("");
                    //  LoadInfoUser();


                    //  ShowEmptyGrid(5);
                   // LoadData();
                   // loadcontrol();
                    loadReason();
                }
               // btnSave.Visible = false;
               // btnSubmit.Visible = false;
               // Button1.Visible = false;
            //}
            //else
            //{
            //    Response.Redirect("~/Login.aspx");
            //}

        }
        void loadcontrol()
        {
            DataTable tbl = cls.GetDataTable("Sp_LoadStatus_approval", new string[] { "@ID", "@user" },
                new object[] { Request.QueryString["Userid"].ToString(), Request.QueryString["ActivationCode"].ToString() });  

            if (tbl.Rows.Count > 0)
            {
               // btnSave.Visible = false;
               // btnSubmit.Visible = false;
                //  btnLuu.Visible = true;
            }
            else
            {


              //  btnSave.Visible = true;
              //  btnSubmit.Visible = true;
                //   btnLuu.Visible = true;
            }
        }

      

      
        void loadReason()
        {
            DataTable tbl = cls.GetDataTable("sp_Load_ASPF_Reason");
            RdcboReason.DataSource = tbl;
            RdcboReason.DataBind();
        }


      
      

        private double calculateAmount(double Qty, double Price)
        {
            return Qty * Price;
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
          //  RGKienThuc.DataSource = dtdetail;
           // RGKienThuc.DataBind();


        }


      

        protected void RGKienThuc_DeleteCommand(object source, GridCommandEventArgs e)
        {

        }
        protected void RGKienThuc_ItemCommand(object source, GridCommandEventArgs e)
        {
        }
       


        //private void Load_AccountCoding(string id)
        //{

        //    DataTable tbl = cls.GetDataTable("sp_Load_AccountCoding", new string[] { "@id" }, new object[] { id });
        //    Rdcode.DataSource = tbl;
        //    Rdcode.DataBind();

        //}

       


        //private void Load_AccountCoding_Detail(string id)
        //{

        //    DataTable tbl = cls.GetDataTable("sp_Load_AccountCoding", new string[] { "@id" }, new object[] { id });
        //    if (tbl.Rows.Count > 0)
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
         //   Load_AccountCoding_Detail(Rdcode.SelectedValue);
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
              //  RGKienThuc.DataSource = dtdetail;
            }
        }

        protected void rdFYBudgetCAT_TextChanged(object sender, EventArgs e)
        {
           // calculateBudgetBlance();
        }

        protected void rdAvailable_TextChanged(object sender, EventArgs e)
        {
          //  calculateBudgetBlance();
        }

        protected void radnumtxtASPFvalue_TextChanged(object sender, EventArgs e)
        {
            //calculateBudgetBlance();
           // calculateBudgetBlanceController();
        }


        protected void rdnuumsoluong_TextChanged(object sender, EventArgs e)
        {

            //  Calculation();

        }

     

        protected void rdnumdongia_TextChanged(object sender, EventArgs e)
        {

            // Calculation();

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
                Obj.Parameter = new string[] { "@id", "@Userid" };
                Obj.ValueParameter = new object[] { Request.QueryString["Userid"].ToString(), Session["email"]  };

                Obj.SpName = "sp_Reject_ASPF";
                Sql.fNonGetData(Obj);

                //btnSave.Visible = false;


                DataTable tblto = cls.GetDataTable("sp_Load_content_ByUser", new string[] { "@ApprovedCode" }, new object[] { Request.QueryString["ActivationCode"].ToString() });
                if (tblto.Rows.Count > 0)
                {
                    string content = sys.noidungReject(tblto.Rows[0]["nguoiduyet"].ToString(), tblto.Rows[0]["nguoitao"].ToString(), tblto.Rows[0]["Country"].ToString(), tblto.Rows[0]["BudgetOwner"].ToString(), tblto.Rows[0]["Brand"].ToString(),
                 tblto.Rows[0]["Category"].ToString(), tblto.Rows[0]["ASPNo"].ToString(), tblto.Rows[0]["ASPFvalue"].ToString(), tblto.Rows[0]["Objective"].ToString(), tblto.Rows[0]["BudgetDepartment"].ToString(), tblto.Rows[0]["Spent"].ToString(), tblto.Rows[0]["BudgetBalance_Controller"].ToString(), tblto.Rows[0]["ApprovedCode"].ToString(), Request.QueryString["Userid"].ToString(), RdcboReason.Text + txtNote.Text);

                    sys.SendMailASPAtt(tblto.Rows[0]["emailto"].ToString(), tblto.Rows[0]["emailcc"].ToString(), "Phản hồi từ " + tblto.Rows[0]["nguoiduyet"].ToString() + " về yêu cầu duyệt ASPF (ID:" + Request.QueryString["Userid"].ToString() + ") /Respond from " + tblto.Rows[0]["nguoiduyet"].ToString() + " for ASPF approval request ", content, tblto.Rows[0]["attachfile"].ToString() == "" ? "" : "~/ImagesUpload/" + tblto.Rows[0]["attachfile"].ToString());

                }



                Page.RegisterStartupScript("closeWindow", "<script type='text/javascript'>GetRadWindow().close()</script>");

            }
            catch
            {
                MsgBox1.AddMessage("Update failed. Please try again later", uc.ucMsgBox.enmMessageType.Error);
            }








        }

    

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["CreateASPF"] = "Update successfull";
            Page.RegisterStartupScript("closeWindow", "<script type='text/javascript'>GetRadWindow().close()</script>");

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            DataTable tblto11111 = cls.GetDataTable("sp_Check_status_ASPF", new string[] { "@docno", "@ApprovedCode" }, new object[] { Request.QueryString["Userid"].ToString(), Request.QueryString["ActivationCode"].ToString() });

            if (tblto11111.Rows.Count > 0)  
            {
                MsgBox1.AddMessage("Bạn đã duyệt/ từ chối Yêu Cầu duyệt Ngân Sách Quảng Cáo Khuyến Mãi này vào ngày" + tblto11111.Rows[0]["DateApp"].ToString() + "  / You had approved/ rejected the ASPF on " + tblto11111.Rows[0]["DateApp"].ToString() +  " " , uc.ucMsgBox.enmMessageType.Error);

                

                string myclosescript = "<script language='javascript' type='text/javascript'>CloseWindow11();</script>";

                Page.ClientScript.RegisterStartupScript(GetType(), "myclosescript", myclosescript);
                return;
            }

            DataTable tblto11111333 = cls.GetDataTable("sp_Check_status_Reject_ASPF", new string[] { "@docno", "@ApprovedCode" }, new object[] { Request.QueryString["Userid"].ToString(), Request.QueryString["ActivationCode"].ToString() });

            if (tblto11111333.Rows.Count > 0)
            {
                MsgBox1.AddMessage("Bạn đã duyệt/ từ chối Yêu Cầu duyệt Ngân Sách Quảng Cáo Khuyến Mãi này vào ngày" + tblto11111.Rows[0]["DateReject"].ToString() + "  / You had approved/ rejected the ASPF on " + tblto11111.Rows[0]["DateReject"].ToString() + " ", uc.ucMsgBox.enmMessageType.Error);



                string myclosescript = "<script language='javascript' type='text/javascript'>CloseWindow11();</script>";

                Page.ClientScript.RegisterStartupScript(GetType(), "myclosescript", myclosescript);
                return;
            }

            



            if (RdcboReason.SelectedValue == "0")
            {
                MsgBox1.AddMessage("Lý do không được rỗng / Reason not be empty ", uc.ucMsgBox.enmMessageType.Error);

                return;
            }





            try
            {
                Obj = new clsObj();
                Obj.Parameter = new string[] { "@id", "@Userid" };
                Obj.ValueParameter = new object[] { Request.QueryString["Userid"].ToString(), Request.QueryString["ActivationCode"].ToString() };

                Obj.SpName = "sp_Reject_ASPF";
                Sql.fNonGetData(Obj);

                //btnSave.Visible = false;


                string cc = "";
                string cctemp = "";
                DataTable tblcc = cls.GetDataTable("sp_Get_emailCC_Reject_ASPF", new string[] { "@id", "@Userid" }, new object[] { Request.QueryString["Userid"].ToString(), Request.QueryString["ActivationCode"].ToString() });
                if (tblcc.Rows.Count > 0)
                {
                    for (int i = 0; i < tblcc.Rows.Count; i++)
                    {
                        cctemp = tblcc.Rows[i][0].ToString();
                        cc = cc + cctemp + ";";

                    }
                }

                DataTable tblto = cls.GetDataTable("sp_Load_content_ByUser", new string[] { "@ApprovedCode" }, new object[] { Request.QueryString["ActivationCode"].ToString() });
                if (tblto.Rows.Count > 0)
                {
                    string content = sys.noidungReject(tblto.Rows[0]["nguoiduyet"].ToString(), tblto.Rows[0]["nguoitao"].ToString(), tblto.Rows[0]["Country"].ToString(), tblto.Rows[0]["BudgetOwner"].ToString(), tblto.Rows[0]["Brand"].ToString(),
                 tblto.Rows[0]["Category"].ToString(), tblto.Rows[0]["ASPNo"].ToString(), tblto.Rows[0]["ASPFvalue"].ToString(), tblto.Rows[0]["Objective"].ToString(), tblto.Rows[0]["BudgetDepartment"].ToString(), tblto.Rows[0]["Spent"].ToString(), tblto.Rows[0]["BudgetBalance_Controller"].ToString(), tblto.Rows[0]["ApprovedCode"].ToString(), Request.QueryString["Userid"].ToString(), RdcboReason.Text + txtNote.Text);

                    sys.SendMailASPAtt(tblto.Rows[0]["emailto"].ToString(), cc, "Phản hồi từ " + tblto.Rows[0]["nguoiduyet"].ToString() + " về yêu cầu duyệt ASPF (ID:" + Request.QueryString["Userid"].ToString() + ") /Respond from " + tblto.Rows[0]["nguoiduyet"].ToString() + " for ASPF approval request ", content, tblto.Rows[0]["attachfile"].ToString() == "" ? "" : "~/ImagesUpload/" + tblto.Rows[0]["attachfile"].ToString());

                }

                MsgBox1.AddMessage("Update successfull", uc.ucMsgBox.enmMessageType.Success);



            }
            catch
            {
                MsgBox1.AddMessage("Update failed. Please try again later", uc.ucMsgBox.enmMessageType.Error);
            }
            finally
            {
                string myclosescript = "<script language='javascript' type='text/javascript'>CloseWindow11();</script>";

                Page.ClientScript.RegisterStartupScript(GetType(), "myclosescript", myclosescript);

            }
        }

       

       
    }
}