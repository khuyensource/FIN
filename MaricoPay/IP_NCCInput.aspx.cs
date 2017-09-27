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
    public partial class IP_NCCInput : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
        clsObj Obj = new clsObj();
        clsSql Sql = new clsSql();
        clsSys sys = new clsSys();

        protected void Page_Load(object sender, EventArgs e)
        {
            checktrontrol(Request.QueryString["linkcode"].ToString());
            checkngay(Request.QueryString["idpr"].ToString(), Request.QueryString["linkcode"].ToString());
            if (!IsPostBack)
            {
                fLoadPRDetail(Request.QueryString["idpr"].ToString(), Request.QueryString["linkcode"].ToString());
            }
        }

        public bool fBool(object value)
        {
            if (value.ToString() == "")
            {
                return false;
            }
            return bool.Parse(value.ToString());
        }
        protected void RG_CancelCommand(object source, GridCommandEventArgs e)
        {

            fLoadPRDetail(Request.QueryString["idpr"].ToString(), Request.QueryString["linkcode"].ToString());

        }
        void fLoadPRDetail(object IPPR,object linkcode)
        {

            DataTable tbl = cls.GetDataTable("[SP_IPload_PRdetail_ByIDPR_Linkcode]", new string[] { "@IDPR", "@linkcode" }, new object[] { IPPR, linkcode });
            //  ViewState["CurrentTable"] = tbl;
            RadGrid1.DataSource = tbl;
            RadGrid1.DataBind();
        }

        void checkngay(object IPPR, object linkcode)
        {

            DataTable tbl = cls.GetDataTable("[SP_IPcheckngay_ByIDPR_Linkcode]", new string[] { "@IDPR", "@linkcode" }, new object[] { IPPR, linkcode });
            //  ViewState["CurrentTable"] = tbl;
            if (tbl.Rows.Count > 0)
            {
                if (tbl.Rows[0]["checkngay"].ToString() == "1")
                {
                    Label1.Text = "<span class=\"auto-style1\"> Bảng báo giá của bạn đã hết hạn / Your quotation has expired</span>";
                }
              

               
            }
        }


        protected void RG_UpdateCommand(object source, GridCommandEventArgs e)
        {
           
            fLoadPRDetail(Request.QueryString["idpr"].ToString(), Request.QueryString["linkcode"].ToString());
        }
        protected void RG_InsertCommand(object source, GridCommandEventArgs e)
        {
            
        }
        protected void RG_EditCommand(object source, GridCommandEventArgs e)
        {
            if (ViewState["CurrentTable"] != null)
            {
                RadGrid1.DataSource = (DataTable)ViewState["CurrentTable"];
                RadGrid1.DataBind();
            }
            else
            {
                fLoadPRDetail(Request.QueryString["idpr"].ToString(), Request.QueryString["linkcode"].ToString());
            }
        }
        protected void RG_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            if (ViewState["CurrentTable"] != null)
            {
                RadGrid1.DataSource = (DataTable)ViewState["CurrentTable"];
                RadGrid1.DataBind();
            }
            else
            {
                fLoadPRDetail(Request.QueryString["idpr"].ToString(), Request.QueryString["linkcode"].ToString());
            }
        }
        protected void RG_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            if (ViewState["CurrentTable"] != null)
            {
                RadGrid1.DataSource = (DataTable)ViewState["CurrentTable"];
                RadGrid1.DataBind();
            }
            else
            {
                fLoadPRDetail(Request.QueryString["idpr"].ToString(), Request.QueryString["linkcode"].ToString());
            }
        }
        protected void RG_GroupsChanging(object source, GridGroupsChangingEventArgs e)
        {
            if (ViewState["CurrentTable"] != null)
            {
                RadGrid1.DataSource = (DataTable)ViewState["CurrentTable"];
                RadGrid1.DataBind();
            }
            else
            {
                fLoadPRDetail(Request.QueryString["idpr"].ToString(), Request.QueryString["linkcode"].ToString());
            }
        }
        protected void RG_SortCommand(object source, GridSortCommandEventArgs e)
        {
            if (ViewState["CurrentTable"] != null)
            {
                RadGrid1.DataSource = (DataTable)ViewState["CurrentTable"];
                RadGrid1.DataBind();
            }
            else
            {
                fLoadPRDetail(Request.QueryString["idpr"].ToString(), Request.QueryString["linkcode"].ToString());
            }
        }
        protected void RG_DeleteCommand(object source, GridCommandEventArgs e)
        {
           
        }
        protected void RG_ItemCommand(object source, GridCommandEventArgs e)
        {


            switch (e.CommandName)
            {
                case Telerik.Web.UI.RadGrid.EditCommandName:
                    RadGrid1.MasterTableView.IsItemInserted = false;
                    e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/UC_IP_NCCInput.ascx";


                    fLoadPRDetail(Request.QueryString["idpr"].ToString(), Request.QueryString["linkcode"].ToString());
                    break;

            }
            // fLoadPRDetail(Request.QueryString["idpr"].ToString(), Request.QueryString["linkcode"].ToString());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            DataTable tbl1 = cls.GetDataTable("[sp_IP_CheckExists_NCC_Send_IP]", new string[] { "@linkcode" }, new object[] { Request.QueryString["linkcode"].ToString() });
                         if (tbl1.Rows[0]["dongia"].ToString() == "" || tbl1.Rows[0]["dongia"].ToString() == "")
                         {
                             ucMsgBox.AddMessage("Bạn chưa cập nhật chi tiết bảng báo giá/You have not updated your quote details", uc.ucMsgBox.enmMessageType.Success);
                         }
                         else

                         {
                             #region send email

                             DataTable tblto = cls.GetDataTable("sp_IP_Load_Info_NCC_Send_IP", new string[] { "@LinkCode" }, new object[] { Request.QueryString["linkcode"].ToString() });
                             //if (tblto.Rows.Count > 0)
                             //{
                             //   string content = sys.noidungReject(tblto.Rows[0]["nguoiduyet"].ToString(), tblto.Rows[0]["nguoitao"].ToString(), tblto.Rows[0]["Country"].ToString(), tblto.Rows[0]["BudgetOwner"].ToString(), tblto.Rows[0]["Brand"].ToString(),
                             //tblto.Rows[0]["Category"].ToString(), tblto.Rows[0]["ASPNo"].ToString(), tblto.Rows[0]["ASPFvalue"].ToString(), tblto.Rows[0]["Objective"].ToString(), tblto.Rows[0]["BudgetDepartment"].ToString(), tblto.Rows[0]["Spent"].ToString(), tblto.Rows[0]["BudgetBalance_Controller"].ToString(), tblto.Rows[0]["ApprovedCode"].ToString(), Request.QueryString["Userid"].ToString(), RdcboReason.Text + txtNote.Text);
                             //   for (int i = 0; i < tblto.Rows.Count; i++)
                             //    {
                             // string noidunggoi = "Nhà Cung Cấp "+tblto.Rows[0]["tenNCC"].ToString()+" đã gởi báo giá cho chi tiết của PR số "+Request.QueryString["idpr"].ToString()+" để xem chi tiết của báo giá vui lòng đăng nhập hệ thống IP";
                             //String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                             //String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                             string noidunggoi = " Thân gởi IP team  " +
                                       " </br>" +
                                       "Xin vui lòng  nhấn vào  <a href = \"https://fin.maricosea.com\"> đây</a>  để xem chi tiết của báo giá." +
                                        " </br>" +
                                      " Dear IP team  " +
                                       " </br>" +
                                       " Please click   <a href =  \"https://fin.maricosea.com\">here</a>   view in detail quotation ." +
                                        " </br>" +
                                        "Trân trọng /Best regards,";

                             if (sys.SendMailASP(tblto.Rows[0]["emailIP"].ToString(), tblto.Rows[0]["Email"].ToString(), "Vendor’s quotation Notification- PR Number" + tblto.Rows[0]["IDPR"].ToString() + " - " + tblto.Rows[0]["noidung"].ToString() + "", noidunggoi))
                             {
                                 #region Update vao bang IPPRVendor  của NCC  IP_UpdateIPPRVendor_NCC_SendEmail_IP
                                 Obj = new clsObj();
                                 Obj.Parameter = new string[] { "@LinkCode" }; //Session["Email"]
                                 Obj.ValueParameter = new object[] { Request.QueryString["linkcode"].ToString() };

                                 //  Obj.ValueOutput = new string[] { "0" };
                                 Obj.SpName = "IP_UpdateIPPRVendor_NCC_SendEmail_IP";
                                 Sql.fNonGetData(Obj);
                                 //    Session["ID_ASPF"] = int.Parse(Obj.ValueOutput[0].ToString());
                                 #endregion
                             }

                             //  }

                             ucMsgBox.AddMessage("Đã cập nhật thành công/Update successfull", uc.ucMsgBox.enmMessageType.Success);
                             checktrontrol(Request.QueryString["linkcode"].ToString());

                             #endregion

                         }
        }
        private void checktrontrol(object linkcode)
        {
            DataTable tbl = cls.GetDataTable("[sp_IP_Checkstatus_NCC_Send_IP]", new string[] { "@linkcode" }, new object[] { linkcode });
            
            if (tbl.Rows.Count > 0)
            {
                Button1.Visible = false;
            }
            else
            {
                Button1.Visible = true;
            }

             DataTable tbl1 = cls.GetDataTable("[sp_IP_CheckExists_NCC_Send_IP]", new string[] { "@linkcode" }, new object[] { linkcode });
             if (tbl1.Rows[0]["dongia"].ToString() != "" && tbl1.Rows[0]["Thanhtien"].ToString() != "" && tbl1.Rows[0]["DaNhanBaoGia"].ToString() == "True")
            {
                Label1.Text = "<span class=\"auto-style1\">Bảng báo giá của bạn đã được gởi đi / Your quotation has been successfully submitted </span>";
            }
           


        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {

                //   GridDataItem dataItem = (GridDataItem)e.Item;

                GridDataItem item = (GridDataItem)e.Item;
                if (item["tomau"].Text != "&nbsp;")

                //   if ((!string.IsNullOrEmpty(myCell.Text) || myCell.Text != "&nbsp;" || myCell.Text != ""))
                {
                    // dataItem.BackColor = System.Drawing.Color.Red;  
                    item.ForeColor = System.Drawing.Color.Red;
                    item.Font.Bold = true;
                }

            }
        }


    }
}