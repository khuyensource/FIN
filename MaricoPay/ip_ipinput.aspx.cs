using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Data;
using Telerik.Web.UI;
namespace MaricoPay
{
    public partial class ip_ipinput : clsPhanQuyenCaoCap
    {
        Cclass cls = new Cclass();
        clsObj Obj = new clsObj();
        clsSql Sql = new clsSql();
        clsSys sys = new clsSys();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["email"] != null)
            {

                checktrontrol(radcomboPR.SelectedValue);
                if (!IsPostBack)
                {
                    //  loadluoi();
                    //LoadPR();
                    LoadTrangThai();
                    loaddata();
                  
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        
        }

      

        private void checktrontrol(object IDPR)
        {
            DataTable tbl = cls.GetDataTable("[sp_IP_Checkstatus_IP_Send_NCC]", new string[] { "@idpr" }, new object[] { IDPR });
            if (tbl.Rows.Count > 0)
            {
                Button1.Visible = false;
                Button2.Visible = true;
            }
            else
            {
                Button1.Visible = true;
                Button2.Visible = false;
            }

            DataTable tbl11 = cls.GetDataTable("[sp_IP_Checkstatus_PR_in_IPPRVendor]", new string[] { "@idpr" }, new object[] { IDPR });
            if (tbl11.Rows.Count > 0)
            {
                Button1.Visible = true;
            }
            else
            {
                Button1.Visible = false; 
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
        void fLoadPRDetail(object IPPR)
        {

            DataTable tbl = cls.GetDataTable("[SP_IPload_PRdetail_ByID]", new string[] { "@IDPR" }, new object[] { IPPR });
            //  ViewState["CurrentTable"] = tbl;
            RadGrid1.DataSource = tbl;
            RadGrid1.DataBind();
        }

        protected void radcomboPR_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PRChangeIndex();
        }

        protected void RG_CancelCommand(object source, GridCommandEventArgs e)
        {

            fLoadPRDetail(radcomboPR.SelectedValue);

        }
        protected void RG_UpdateCommand(object source, GridCommandEventArgs e)
        {
           
            fLoadPRDetail(radcomboPR.SelectedValue);
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
                fLoadPRDetail(radcomboPR.SelectedValue);
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
                fLoadPRDetail(radcomboPR.SelectedValue);
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
                fLoadPRDetail(radcomboPR.SelectedValue);
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
                fLoadPRDetail(radcomboPR.SelectedValue);
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
                fLoadPRDetail(radcomboPR.SelectedValue);
            }
        }
        protected void RG_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //string ID = RadGrid1.Items[e.Item.ItemIndex]["IDPRDetail"].Text;
            //Obj = new clsObj();
            //Obj.Parameter = new string[] { "@IDPRDetail" };
            //Obj.ValueParameter = new object[] { ID };
            //Obj.SpName = "spDelete_IPPRDetail";
            //Sql.fNonGetData(Obj);
            //if (Obj.KetQua < 1)
            //{
            //    lbLoi.Text = "<font color='red'>Xóa thất bại. Vui lòng thử lại sau.</font>";
            //}
            //else
            //{
            //    string filename = RadGrid1.Items[e.Item.ItemIndex]["HinhMau"].Text;
            //    if (filename != "")
            //    {
            //        string sFolderPath = Server.MapPath("Upload/IP/" + filename);
            //        if (System.IO.File.Exists(sFolderPath) == true)
            //            System.IO.File.Delete(sFolderPath);
            //    }
            //    lbLoi.Text = "<font color='blue'>Xóa thành công.</font>";
            //}
            //fLoadPRDetail(radcomboPR.SelectedValue);
        }
        protected void RG_ItemCommand(object source, GridCommandEventArgs e)
        {




            switch (e.CommandName)
            {
                case Telerik.Web.UI.RadGrid.EditCommandName:
                    RadGrid1.MasterTableView.IsItemInserted = false;
                    e.Item.OwnerTableView.EditFormSettings.UserControlName = "./uc/ip_ipinput.ascx";


                    fLoadPRDetail(radcomboPR.SelectedValue);
                    break;

            }
            // fLoadPRDetail(radcomboPR.SelectedValue);
        }

        private void loaddata()
        {

            DataTable tblto = cls.GetDataTable("SP_IPload_PRdetail_ByID", new string[] { "@IDPR" }, new object[] { radcomboPR.SelectedValue });
            RadGrid1.DataSource = tblto;
            RadGrid1.MasterTableView.HierarchyDefaultExpanded = true;
        }


        private void PRChangeIndex()
        {
            fLoadPRDetail(radcomboPR.SelectedValue);
        }

        
     
   
        

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (radcomboPR.SelectedValue == "")
            {
                MsgBox1.AddMessage("Bạn phải chọn PR", uc.ucMsgBox.enmMessageType.Error);
            }
            else
            {
                #region send email  
                DataTable tblto = cls.GetDataTable("SP_IP_Load_Linkcode_BYIDPR", new string[] { "@IDPR" }, new object[] { radcomboPR.SelectedValue });
                //if (tblto.Rows.Count > 0)
                //{
                 //   string content = sys.noidungReject(tblto.Rows[0]["nguoiduyet"].ToString(), tblto.Rows[0]["nguoitao"].ToString(), tblto.Rows[0]["Country"].ToString(), tblto.Rows[0]["BudgetOwner"].ToString(), tblto.Rows[0]["Brand"].ToString(),
                 //tblto.Rows[0]["Category"].ToString(), tblto.Rows[0]["ASPNo"].ToString(), tblto.Rows[0]["ASPFvalue"].ToString(), tblto.Rows[0]["Objective"].ToString(), tblto.Rows[0]["BudgetDepartment"].ToString(), tblto.Rows[0]["Spent"].ToString(), tblto.Rows[0]["BudgetBalance_Controller"].ToString(), tblto.Rows[0]["ApprovedCode"].ToString(), Request.QueryString["Userid"].ToString(), RdcboReason.Text + txtNote.Text);
                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                for( int i=0;i<tblto.Rows.Count;i++)
                {
                    string noidunggoi = " Thân gởi " + tblto.Rows[i]["NguoiLienHe"].ToString() + ", Nhà cung cấp/Công ty : " + tblto.Rows[i]["TenNCC"].ToString() + " " +
                        " </br>" +
                        "Xin vui lòng  nhấn vào  <a href = '" + strUrl + "/IP_NCCInput.aspx?linkcode=" + tblto.Rows[i]["linkcode"] + "&IDPR=" + radcomboPR.SelectedValue + "'>đây</a>  để điền chi tiết của báo giá." +
                         " </br>" +
                          "Vui lòng hoàn thành bảng báo giá chậm nhất ngày " + tblto.Rows[i]["NgayGuiYCBaoGia"].ToString() + "" +
                      
                          " </br>" +
                            " </br>" +
                       " Dear " + tblto.Rows[i]["NguoiLienHe"].ToString() + ", Suppliers/company " + tblto.Rows[i]["TenNCC"].ToString() + " " +
                        " </br>" +
                        " Please click   <a href = '" + strUrl + "/IP_NCCInput.aspx?linkcode=" + tblto.Rows[i]["linkcode"] + "&IDPR=" + radcomboPR.SelectedValue + "'>here </a>  fill in detail quotation ." +
                         " </br>" +
                            "Please complete the quotation by " + tblto.Rows[i]["NgayGuiYCBaoGia"].ToString() + "" +
                            " </br>" +
                               " </br>" +
                         "Trân trọng /Best regards,";
                  
                     // tblto.Rows[i]["Emailvendor"].ToString() 
                    if (sys.SendMailASP(tblto.Rows[i]["Email"].ToString(), tblto.Rows[i]["emailIP"].ToString(), "Vendor’s quotation request/Yêu cầu báo giá của nhà cung cấp- PR number :" + radcomboPR.SelectedValue + " - " +tblto.Rows[i]["noidung"].ToString() + "", noidunggoi))
                    {
                        #region Update vao bang IPPRVendor  IP_UpdateIPPRVendor_SendEmail
                        Obj = new clsObj();
                        Obj.Parameter = new string[] { "@EmailIPApprove", "@LinkCode" }; //Session["Email"]
                        Obj.ValueParameter = new object[] {Session["Email"], tblto.Rows[i]["linkcode"].ToString() };

                        //  Obj.ValueOutput = new string[] { "0" };
                        Obj.SpName = "IP_UpdateIPPRVendor_SendEmail";
                        Sql.fNonGetData(Obj);
                        //    Session["ID_ASPF"] = int.Parse(Obj.ValueOutput[0].ToString());
                        #endregion
                    }
                }
                MsgBox1.AddMessage("Đã cập nhật thành công/Update successfull", uc.ucMsgBox.enmMessageType.Success);
                checktrontrol(radcomboPR.SelectedValue);
                #endregion
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
        private void LoadTrangThai()
        {
            DataTable tbl = cls.GetDataTable("sp_IPGetTrangThai");
            radcomboTrangThai.DataSource = tbl;
            radcomboTrangThai.DataBind();
        }
        protected void radcomboTrangThai_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            radcomboPR.Items.Clear();

            DataTable tbl = cls.GetDataTable("[sp_LoadIPPRActiveSubmited_Minh]", new string[] { "@Username", "@trangthai" }, new object[] { Session["Username"].ToString(), radcomboTrangThai.SelectedValue });
            radcomboPR.DataSource = tbl;
            radcomboPR.DataBind();

            if (radcomboPR.Items.Count > 0)
            {
                radcomboPR.SelectedIndex = 0;
             
                radcomboPR.Text = radcomboPR.SelectedValue;
                PRChangeIndex();
                PRChangeIndex();
            }
            else
            {
                radcomboPR.SelectedIndex = -1;
                radcomboPR.Text = "";

                RadGrid1.DataSource = null;
                RadGrid1.DataBind();
            }
            


         

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (radcomboPR.SelectedValue == "")
            {
                MsgBox1.AddMessage("Bạn phải chọn PR", uc.ucMsgBox.enmMessageType.Error);
            }
            else
            {
                #region send email
                DataTable tblto = cls.GetDataTable("SP_IP_Load_Linkcode_BYIDPR", new string[] { "@IDPR" }, new object[] { radcomboPR.SelectedValue });
                //if (tblto.Rows.Count > 0)
                //{
                //   string content = sys.noidungReject(tblto.Rows[0]["nguoiduyet"].ToString(), tblto.Rows[0]["nguoitao"].ToString(), tblto.Rows[0]["Country"].ToString(), tblto.Rows[0]["BudgetOwner"].ToString(), tblto.Rows[0]["Brand"].ToString(),
                //tblto.Rows[0]["Category"].ToString(), tblto.Rows[0]["ASPNo"].ToString(), tblto.Rows[0]["ASPFvalue"].ToString(), tblto.Rows[0]["Objective"].ToString(), tblto.Rows[0]["BudgetDepartment"].ToString(), tblto.Rows[0]["Spent"].ToString(), tblto.Rows[0]["BudgetBalance_Controller"].ToString(), tblto.Rows[0]["ApprovedCode"].ToString(), Request.QueryString["Userid"].ToString(), RdcboReason.Text + txtNote.Text);
                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                for (int i = 0; i < tblto.Rows.Count; i++)
                {
                    string noidunggoi = " Thân gởi " + tblto.Rows[i]["NguoiLienHe"].ToString() + ", Nhà cung cấp/Công ty : " + tblto.Rows[i]["TenNCC"].ToString() + " " +
                       " </br>" +
                       "Xin vui lòng  nhấn vào  <a href = '" + strUrl + "/IP_NCCInput.aspx?linkcode=" + tblto.Rows[i]["linkcode"] + "&IDPR=" + radcomboPR.SelectedValue + "'>đây</a>  để điền chi tiết của báo giá." +
                        " </br>" +
                         "Vui lòng hoàn thành bảng báo giá chậm nhất ngày " + tblto.Rows[i]["NgayGuiYCBaoGia"].ToString() + "" +

                         " </br>" +
                           " </br>" +
                      " Dear " + tblto.Rows[i]["NguoiLienHe"].ToString() + ", Suppliers/company " + tblto.Rows[i]["TenNCC"].ToString() + " " +
                       " </br>" +
                       " Please click   <a href = '" + strUrl + "/IP_NCCInput.aspx?linkcode=" + tblto.Rows[i]["linkcode"] + "&IDPR=" + radcomboPR.SelectedValue + "'>here </a>  fill in detail quotation ." +
                        " </br>" +
                           "Please complete the quotation by " + tblto.Rows[i]["NgayGuiYCBaoGia"].ToString() + "" +
                           " </br>" +
                             " </br>" +
                        "Trân trọng /Best regards,";

                    // tblto.Rows[i]["Emailvendor"].ToString() 
                    if (sys.SendMailASP(tblto.Rows[i]["Email"].ToString(), tblto.Rows[i]["emailIP"].ToString(), "Vendor’s quotation request/Yêu cầu báo giá của nhà cung cấp - PR number :" + radcomboPR.SelectedValue + "-" + tblto.Rows[i]["noidung"] + "", noidunggoi))
                    {
                        #region Update vao bang IPPRVendor  IP_UpdateIPPRVendor_SendEmail
                        Obj = new clsObj();
                        Obj.Parameter = new string[] { "@EmailIPApprove", "@LinkCode" }; //Session["Email"]
                        Obj.ValueParameter = new object[] { Session["Email"], tblto.Rows[i]["linkcode"].ToString() };

                        //  Obj.ValueOutput = new string[] { "0" };
                        Obj.SpName = "IP_UpdateIPPRVendor_SendEmail";
                        Sql.fNonGetData(Obj);
                        //    Session["ID_ASPF"] = int.Parse(Obj.ValueOutput[0].ToString());
                        #endregion
                    }
                }
                MsgBox1.AddMessage("Đã cập nhật thành công/Update successfull", uc.ucMsgBox.enmMessageType.Success);
                checktrontrol(radcomboPR.SelectedValue);
                #endregion
            }

        }

    }
}
