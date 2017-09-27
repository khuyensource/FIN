using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
namespace MaricoPay
{
    public partial class AutoClose : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
        clsObj Obj = new clsObj();
        clsSql Sql = new clsSql();
        clsSys sys = new clsSys();
        public DataTable dtdetail;

        protected void Page_Load(object sender, EventArgs e)
        {


            // 


            DataTable tblto11111 = cls.GetDataTable("sp_Check_status_ASPF", new string[] { "@docno", "@ApprovedCode" }, new object[] { Request.QueryString["Userid"].ToString(), Request.QueryString["ActivationCode"].ToString() });

            if (tblto11111.Rows.Count > 0)
            {
                MsgBox1.AddMessage("Bạn đã duyệt/ từ chối Yêu Cầu duyệt Ngân Sách Quảng Cáo Khuyến Mãi này vào ngày" + tblto11111.Rows[0]["DateApp"].ToString() + "  / You had approved/ rejected the ASPF on " + tblto11111.Rows[0]["DateApp"].ToString() + " ", uc.ucMsgBox.enmMessageType.Error);

                return;

                string myclosescript = "<script language='javascript' type='text/javascript'>CloseWindow11();</script>";

                Page.ClientScript.RegisterStartupScript(GetType(), "myclosescript", myclosescript);
               
            }


                    #region Upadte header
                    try
                    {

               //         DataTable tblto111 = cls.GetDataTable("sp_Check_status_ASPF", new string[] { "@docno", "@ApprovedCode" }, new object[] { Request.QueryString["Userid"].ToString(), Request.QueryString["ActivationCode"].ToString() });

                     

                        Obj = new clsObj();
                        Obj.Parameter = new string[] { "@id", "@Userid" };
                        Obj.ValueParameter = new object[] { Request.QueryString["Userid"].ToString(), Request.QueryString["ActivationCode"].ToString() };

                        Obj.SpName = "sp_Approval_ASPF";
                        Sql.fNonGetData(Obj);


                        #region check da duyet het chua 


                        DataTable tblto8881 = cls.GetDataTable("sp_Check_Approval_all", new string[] { "@docno" }, new object[] { Request.QueryString["Userid"].ToString() });

                        if (tblto8881.Rows.Count > 0)
                        {
                            /////////////////// duyet het roi ////////////////


                            DataTable tblto = cls.GetDataTable("sp_Load_content_Bylevel", new string[] { "@docno", "@levelapp" }, new object[] { Request.QueryString["Userid"].ToString(), 10 });
                            if (tblto.Rows.Count > 0)
                            {
                                string content = sys.noidungCungCapASPNo(tblto.Rows[0]["nguoiduyet"].ToString(), tblto.Rows[0]["nguoitao"].ToString(), tblto.Rows[0]["Country"].ToString(), tblto.Rows[0]["BudgetOwner"].ToString(), tblto.Rows[0]["Brand"].ToString(),
                             tblto.Rows[0]["Category"].ToString(), tblto.Rows[0]["ASPNo"].ToString(), tblto.Rows[0]["ASPFvalue"].ToString(), tblto.Rows[0]["Objective"].ToString(), tblto.Rows[0]["BudgetDepartment"].ToString(), tblto.Rows[0]["Spent"].ToString(), tblto.Rows[0]["BudgetBalance_Controller"].ToString(), tblto.Rows[0]["ApprovedCode"].ToString(), Request.QueryString["Userid"].ToString());

                                sys.SendMailASPAtt(tblto.Rows[0]["emailto"].ToString(), "Yêu cầu cung cấp số IO  (ID:" + Request.QueryString["Userid"].ToString() + ") từ " + tblto.Rows[0]["nguoitao"].ToString() + "/Request  provide the number IO (ID:" + Request.QueryString["Userid"].ToString() + ") from " + tblto.Rows[0]["nguoitao"].ToString() , content, tblto.Rows[0]["attachfile"].ToString() == "" ? "" : "~/ImagesUpload/" + tblto.Rows[0]["attachfile"].ToString());


                                //    sys.SendMailASPAtt(tblto.Rows[0]["emailto"].ToString(), tblto.Rows[0]["emailcc"].ToString(), "Phản hồi từ " + tblto.Rows[0]["nguoiduyet"].ToString() + "  xxx  về yêu cầu duyệt ASPF (ID:" + Request.QueryString["Userid"].ToString() + ") /Respond from " + tblto.Rows[0]["nguoiduyet"].ToString() + " for ASPF approval request ", content, tblto.Rows[0]["attachfile"].ToString() == "" ? "" : "~/ImagesUpload/" + tblto.Rows[0]["attachfile"].ToString());

                            }


                        }
                        else
                        {

                            /////////////// chua duyet het ///////////////////////////  'BE82D035-0A8F-4985-B827-353677E78F96'      

                            DataTable tblto888 = cls.GetDataTable("sp_Load_contentApproval_ByUser", new string[] { "@ApprovedCode" }, new object[] { Request.QueryString["ActivationCode"].ToString() });
                            if (tblto888.Rows.Count > 0)
                            {
                                string content1111 = sys.noidungsendApproval(tblto888.Rows[0]["nguoiduyet"].ToString(), tblto888.Rows[0]["nguoitao"].ToString(), tblto888.Rows[0]["Country"].ToString(), tblto888.Rows[0]["BudgetOwner"].ToString(), tblto888.Rows[0]["Brand"].ToString(),
                             tblto888.Rows[0]["Category"].ToString(), tblto888.Rows[0]["ASPNo"].ToString(), tblto888.Rows[0]["ASPFvalue"].ToString(), tblto888.Rows[0]["Objective"].ToString(), tblto888.Rows[0]["BudgetDepartment"].ToString(), tblto888.Rows[0]["Spent"].ToString(), tblto888.Rows[0]["BudgetBalance_Controller"].ToString(), tblto888.Rows[0]["ApprovedCode"].ToString(), Request.QueryString["Userid"].ToString(), tblto888.Rows[0]["attachfile"].ToString());

                                sys.SendMailASPAtt(tblto888.Rows[0]["emailto"].ToString(), "Yêu cầu duyệt ASPF  (ID:" + Request.QueryString["Userid"].ToString() + ") từ " + tblto888.Rows[0]["nguoitao"].ToString() + "/ASPF approval request from " + tblto888.Rows[0]["nguoitao"].ToString(), content1111, tblto888.Rows[0]["attachfile"].ToString() == "" ? "" : "~/ImagesUpload/" + tblto888.Rows[0]["attachfile"].ToString());

                            }  // 


                            DataTable tblto = cls.GetDataTable("sp_Load_content_ByUser", new string[] { "@ApprovedCode" }, new object[] { Request.QueryString["ActivationCode"].ToString() });
                            if (tblto.Rows.Count > 0)
                            {
                                string content = sys.noidungApproval(tblto.Rows[0]["nguoiduyet"].ToString(), tblto.Rows[0]["nguoitao"].ToString(), tblto.Rows[0]["Country"].ToString(), tblto.Rows[0]["BudgetOwner"].ToString(), tblto.Rows[0]["Brand"].ToString(),
                             tblto.Rows[0]["Category"].ToString(), tblto.Rows[0]["ASPNo"].ToString(), tblto.Rows[0]["ASPFvalue"].ToString(), tblto.Rows[0]["Objective"].ToString(), tblto.Rows[0]["BudgetDepartment"].ToString(), tblto.Rows[0]["Spent"].ToString(), tblto.Rows[0]["BudgetBalance_Controller"].ToString(), tblto.Rows[0]["ApprovedCode"].ToString(), Request.QueryString["Userid"].ToString());

                                sys.SendMailASPAtt(tblto.Rows[0]["emailto"].ToString(), tblto.Rows[0]["emailcc"].ToString(), "Phản hồi từ " + tblto.Rows[0]["nguoiduyet"].ToString() + " về yêu cầu duyệt ASPF (ID:" + Request.QueryString["Userid"].ToString() + ") /Respond from " + tblto.Rows[0]["nguoiduyet"].ToString() + " for ASPF approval request ", content, tblto.Rows[0]["attachfile"].ToString() == "" ? "" : "~/ImagesUpload/" + tblto.Rows[0]["attachfile"].ToString());

                            }


                            MsgBox1.AddMessage("Update successfull", uc.ucMsgBox.enmMessageType.Success);

                            //  Response.Write("<script language=javascript>window.close();</script>");
                            //   Page.RegisterStartupScript("closeWindow", "<script type='text/javascript'>GetRadWindow().close()</script>");





                        }
                        #endregion



                    }

            
            finally
            {
                string myclosescript = "<script language='javascript' type='text/javascript'>CloseWindow11();</script>";

                Page.ClientScript.RegisterStartupScript(GetType(), "myclosescript", myclosescript);
            }
             #endregion
        }

        protected void UpdatePanel1_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Thread.Sleep(10);
               // TextBox1.Text = "Finished";
            }
        }

    }
}