using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using MaricoPay.DB;
namespace MaricoPay
{
    public partial class admin : clsPhanQuyenCaoCap
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTravel();
                LoadExpenses();
            }
        }
        public bool isShowSendEmail(object statususer)
        {
            bool kq = false;
            switch (statususer.ToString())
            {
                case "1":
                    kq = false;
                    break;
                case "0":
                    kq = true;
                    break;
                case "3":
                    kq = false;
                    break;
                default:
                    kq = false;
                    break;
            }
            return kq;
        }
        private void LoadTravel()
        {
            DataTable tbl = cls.GetDataTable("sp_getAllTravelApproved");
            dropTravel.DataSource = tbl;
            dropTravel.DataBind();
        }
        private void LoadExpenses()
        {
            DataTable tbl = cls.GetDataTable("sp_getAllExpensesApproved");
            dropExpenses.DataSource = tbl;
            dropExpenses.DataBind();
        }

        protected void btViewTravel_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
           
           string type= cls.GetString("sp_getDocTypeFromDocNo", new string[] { "@docno" }, new object[] { dropTravel.SelectedValue });
           if (type.ToUpper() == "OFFICE")
            {
                Session["docno"] = dropTravel.SelectedValue;
                Session["TravelDetail"] = null;
                sb.Append("window.open('PrintTravelRequest.aspx','PrintMe', 'height=900px,width=1024px,scrollbars=1');");
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "NewClientScript", sb.ToString(), true);
            }
            else
            {
                sb.Append("window.open('PrintTravelRequestSales.aspx?type=2&docno=" + dropTravel.SelectedValue + "','PrintMe', 'height=900px,width=1024px,scrollbars=1');");
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "NewClientScript", sb.ToString(), true);
            }
        }

        protected void btviewExpenses_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
           
            string type = cls.GetString("sp_getDocTypeFromDocNo", new string[] { "@docno" }, new object[] { dropExpenses.SelectedValue });
            if (type.ToUpper() == "OFFICE")
            {
                Session["docno"] = dropExpenses.SelectedValue;
                Session["ClaimDetailPrint"] = null;
                sb.Append("window.open('PrintClaimOffice.aspx','PrintMe', 'height=900px,width=1024px,scrollbars=1');");
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "NewClientScript", sb.ToString(), true);
            }
            else
            {
               
                sb.Append("window.open('PrintClaimSales.aspx?type=3&docno=" + dropExpenses.SelectedValue + "','PrintMe', 'height=900px,width=1024px,scrollbars=1');");
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "NewClientScript", sb.ToString(), true);
            }
        }

        protected void btDeleteTravel_Click(object sender, EventArgs e)
        {
            if (cls.bXoa(new string[] { "@docno" }, new object[] { dropTravel.SelectedValue }, "sp_deleteTravelApproved") == true)
            {
                LoadTravel();
               
                MsgBox1.AddMessage("Deleted successfully", uc.ucMsgBox.enmMessageType.Success);

            }
            else
            {
                MsgBox1.AddMessage("Deleted fail", uc.ucMsgBox.enmMessageType.Error);
            }
        }

        protected void btDeleteExpenses_Click(object sender, EventArgs e)
        {
            if (cls.bXoa(new string[] { "@docno" }, new object[] { dropExpenses.SelectedValue }, "sp_deleteExpensesApproved") == true)
            {
                LoadExpenses();
                MsgBox1.AddMessage("Deleted successfully", uc.ucMsgBox.enmMessageType.Success);

            }
            else
            {
                MsgBox1.AddMessage("Deleted fail", uc.ucMsgBox.enmMessageType.Error);
            }
        }

        protected void dropTravel_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tblstatus = cls.GetDataTable("sp_getStatusDocnoAdminTravel", new string[] { "@docno" }, new object[] { dropTravel.SelectedValue });
            RadGrid1.DataSource = tblstatus;
            RadGrid1.DataBind();
            string st = cls.GetString0("sp_getStatusFinal", new string[] { "@docno" }, new object[] { dropTravel.SelectedValue });
            if (st == "1")
            {
                btDeleteTravel.Enabled = true;
            }
            else
            {
                btDeleteTravel.Enabled = false;
            }
        }

        protected void dropExpenses_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tblstatus = cls.GetDataTable("sp_getStatusDocnoAdminExpenses", new string[] { "@docno" }, new object[] { dropExpenses.SelectedValue });
            RadGrid2.DataSource = tblstatus;
            RadGrid2.DataBind();
            string st = cls.GetString0("sp_getStatusFinal", new string[] { "@docno" }, new object[] { dropExpenses.SelectedValue });
            if (st == "1")
            {
                btDeleteExpenses.Enabled = true;
            }
            else
            {
                btDeleteExpenses.Enabled = false;
            }
        }

        private void SenEmailSubmitExpenses(string code, string to, string appby, string activationCode, string nguoidenghi, string phongban, string mucdich, string datamung,string tungay,string denngay,string type)
        {
            clsSys sys = new clsSys();
            if (type.ToUpper() == "OFFICE")
            {
                string thoigian = "Từ/From " + tungay + " Đến/To " + denngay;
                string html = "";
                html = "<table><tr><td>Người đề nghị/Requester: <b>" + nguoidenghi + "</b> Phòng ban/Dept: " + phongban + "</td></tr>";
                html = html + "<tr><td>Nội dung thanh toán/Purpose of business trip: <b>" + mucdich + "</b></td></tr>";
                html = html + "<tr><td>Thời gian/Length of days: <b>" + thoigian + "</b></td></tr>";
                html = html + "<tr><td>Đã tạm ứng/Advanced: <b>" + datamung + " VNĐ</b></td></tr>";
                html = html + "</table>";
                html = html + "<table cellpadding=\"2\" cellspacing=\"0\" style=\"width: 100%; border: 1px solid black; border-collapse: collapse; font-size: 12px;\">";
                html = html + "<thead>";
                html = html + "<tr style=\"border: 1px solid black; border-collapse: collapse\">";
                html = html + "<th align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"  rowspan=\"2\">STT<br />No</th>";
                html = html + "<th align=\"center\" colspan=\"2\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Hóa đơn/Invoice</th>";
                html = html + "<th rowspan=\"2\" align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Chi tiết chi phí<br />Detail of Expenses</th>";
                html = html + "<th align=\"center\" rowspan=\"2\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Người tham gia<br /> Participant </th>";
                html = html + "<th align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\" rowspan=\"2\">Tiền tệ<br /> Currency </th>";
                html = html + " <th align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\" rowspan=\"2\"> Nguyên tệ<br /> Amount</th>";
                html = html + "<th align=\"center\" rowspan=\"2\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\">Tỉ giá<br /> Rate (VND)</th>";
                html = html + "<th align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\" rowspan=\"2\"> Thành tiền VND<br /> Amount</th>";
                html = html + "<th align=\"center\" rowspan=\"2\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\"> GL</th>";
                html = html + "<th align=\"center\" rowspan=\"2\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\"> IO</th>";
                html = html + "</tr>";
                html = html + "<tr style=\"border: 1px solid black; border-collapse: collapse\">";
                html = html + "<th align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> Ngày/Date</th>";
                html = html + "<th align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Số/No</th>";
                html = html + "</tr>";
                html = html + " </thead>";
                html = html + " <tbody>";
                DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
                List<sp_getClaimDetailResult> kq1 = dbs.sp_getClaimDetail(code, true).ToList();//sp_getClaimDetail LA STORE
                Session["ClaimDetailPrint"] = kq1;
                dbs.Dispose();
                string kq = FillTableemail(cls.cToDouble(datamung));
                html = html + kq;

                html = html + "</tbody></table>";

                string who = "";
                if (appby != "")
                {
                    who = "(has been approved by " + appby + ")";
                }
                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                bool kq2 = sys.SendMailASP(to, "Approved Expense Claim", "Dear Sir/Madam,<br/><br/>Please approve Claim number " + code + who + ". <a href = '" + strUrl + "/ClaimExpensesOffice.aspx?ActivationCode=" + activationCode + "&code=" + code + "'>Click here to approve.</a> or </br><a href = '" + strUrl + "/ClaimExpensesOffice.aspx?RejectedCode=" + activationCode.ToString() + "&code=" + code + "'>Click here to Reject.</a></br></br>" + html + "</br>Best Regards,");

                if (kq2 == true)
                {
                    MsgBox1.AddMessage("Submitted successfully!", uc.ucMsgBox.enmMessageType.Success);
                }
                else
                {
                    // cls.bXoa(new string[] { "@Docno", "@username" }, new object[] { code, to }, "sp_DeleteApproveByEmail");

                    MsgBox1.AddMessage("Failed to send, Please try again!", uc.ucMsgBox.enmMessageType.Error);
                }
            }
            else//sales
            {
                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                cls.SenEmailSubmitClaimWorkingPlan(code, to, appby, activationCode, nguoidenghi, phongban, mucdich, tungay, denngay, strUrl);
            }

        }
        private string FillTableemail(double tamung)
        {
            string kq = "";

            List<sp_getClaimDetailResult> tbl = (List<sp_getClaimDetailResult>)Session["ClaimDetailPrint"];

            double tongtien = 0;
            int stt = 0;
            foreach (sp_getClaimDetailResult item in tbl)
            {

                if (item.TotalVN != 0)
                {
                    if (item.Date == new DateTime(2000, 1, 1))
                    {
                        //dong subtotal

                        kq = kq + "<tr><td colspan=8 style='color: #000000; font-weight: bold; text-align:left; border:1px solid black; border-collapse:collapse;'>Subtotal</td>";

                        kq = kq
                          + "<td colspan=3 style='color: #000000; font-weight: bold; border:1px solid black; border-collapse:collapse; text-align:left;'>" + String.Format("{0:0,0}", item.TotalVN) + "</td>"
                          + "</tr>";
                    }
                    else
                    {
                        stt++;
                        tongtien = tongtien + cls.cToDouble(item.TotalVN);
                        kq = kq + "<tr><td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + cls.cToString0(stt) + "</td><td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Date.ToString("dd-MMM-yy") + "</td>"
                                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>"
                                           + "<a  href='#' onclick=\"javascript:window.open('/popVAT.aspx?cp=" + item.CompanyName + "&pv=" + item.Province + "&vatcode=" + item.VATCode + "&vatamount=" + item.VATAmount + "','VAT','width=500,height=150')\">" + item.No + "</a></td>";


                        kq = kq
                           + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.DetailExpenses + "</td>"
                            + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Participant + "</td>";

                        kq = kq + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.Currency + "</td>"
                     + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.Amount) + "</td>"
                    + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.Rate) + "</td>";

                        kq = kq
                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.TotalVN) + "</td>"
                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.GL + "</td>"
                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.IO + "</td>"
                          + "</tr>";
                    }


                }
            }
            //tinh tong tien
            kq = kq + "<tr><td colspan=8 style='color: #000000; font-weight: bold; text-align:right;'>Tổng tiền VND/Total Amount:</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + cls.FormatNumber(tongtien) + "</td><td colspan=2></td></tr>";
            kq = kq + "<tr><td colspan=8 style='color: #000000; font-weight: bold; text-align:right;'>Đã tạm ứng VND/Advanced Amount:</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + cls.FormatNumber(tamung) + "</td><td colspan=2></td></tr>";
            kq = kq + "<tr><td colspan=8 style='color: #000000; font-weight: bold; text-align:right;'>Chênh lệch VND/Pay back(+)/Reimbursemet(-):</td><td style='color: #000000; font-weight: bold; text-align:right;'>" + cls.FormatNumber(tongtien - tamung) + "</td><td colspan=2></td></tr>";
            return kq;
        }
        protected void RadGrid2_ItemCommand(object sender, GridCommandEventArgs e)
        {
            // RadGrid1.Rebind();
            GridEditableItem editedItem;
          
            switch (e.CommandName)
            {
                case "Email":
                   editedItem = (GridEditableItem)e.Item;
//                    status = editedItem["status"].Text;
                  
                    string docno = editedItem["Docno"].Text;
                      string to = editedItem["approval"].Text;
                     // to = "khuyentt@icpvn.com";
                      string activecode = editedItem["ApprovedCode"].Text;
                      string nguoidenghi = editedItem["Username"].Text;
                      string phongban = editedItem["Department"].Text;
                      string mucdich = editedItem["Purpose"].Text;
                      string datamung = editedItem["DaTamUng"].Text;
                      string tungay =editedItem["FDate"].Text;
                      string denngay = editedItem["TDate"].Text;
                      string type = editedItem["Type"].Text;
                     SenEmailSubmitExpenses(docno, to, "", activecode, nguoidenghi, phongban, mucdich, datamung, tungay, denngay,type);
                    break;
                case "Reject":
                   editedItem = (GridEditableItem)e.Item;
//                    status = editedItem["status"].Text;
                  
                    string docno1 = editedItem["Docno"].Text;
                      string to1 = editedItem["approval"].Text;
                      if (cls.bCapNhat(new string[] { "@docno", "@approval", "@note", "@status" }, new object[] { docno1, to1, "Admin Rejected", 3 }, "sp_UpdateStatusDocno") == true)
                      {
                          MsgBox1.AddMessage("Rejected successfully", uc.ucMsgBox.enmMessageType.Success);
                      }
                      else
                      {
                          MsgBox1.AddMessage("Can not reject", uc.ucMsgBox.enmMessageType.Error);
                      }
                     //gui email den nguoi yeu cau va nguoi duyet
                     
                    break;
               
             
            }
        }
        private void SenEmailSubmitTravel(string code, string to, string appby, string activationCode, string nguoidenghi, string phongban, string mucdich,string noiden,string lotrinh, string tungay, string denngay,bool oto,bool tauhoa,bool maybay,bool datve,bool datphong,string khac,string type)
        {
            clsSys sys = new clsSys();
            if (type.ToUpper() == "OFFICE")
            {
                string thoigian = "Từ/From " + tungay + " Đến/To " + denngay;
                string phuongtien = oto ? " Oto / Car " : "";
                phuongtien = phuongtien + cls.cToString(tauhoa ? " Tàu hỏa / Train " : "");
                phuongtien = phuongtien + cls.cToString(maybay ? " Máy bay / Flight " : "");
                string thuxep = datve ? " Mua vé máy bay / Returned air ticket; " : "";
                thuxep = thuxep + cls.cToString(datphong ? " Đặt khách sạn / Hotel booking; " : "");
                thuxep = thuxep + khac;
                string html = "";
                html = "<table><tr><td>Người đề nghị/Requester: <b>" + nguoidenghi + "</b> Phòng ban/Dept: " + phongban + "</td></tr>";
                html = html + "<tr><td>Nơi đến/Destination: <b>" + noiden + "</b></td></tr>";
                html = html + "<tr><td>Lộ trình/Itinerary: <b>" + lotrinh + "</b></td></tr>";
                html = html + "<tr><td>Mục đích công tác/Purpose of business trip: <b>" + mucdich + "</b></td></tr>";
                html = html + "<tr><td>Thời gian/Length of days: <b>" + thoigian + "</b></td></tr>";


                html = html + "<tr><td>Phương tiện/Transportation mean: <b>" + phuongtien + "</b></td></tr>";
                html = html + "<tr><td>Đề nghị hành chánh thu xếp/Request admin to arrange: <b>" + thuxep + "</b></td></tr>";
                html = html + "</table>";
                html = html + "<table  cellpadding=\"2\" cellspacing=\"0\" style=\"width: 100%; border: 1px solid black; border-collapse: collapse; font-size: 12px;\">";
                html = html + "<tr style=\"border: 1px solid black; border-collapse: collapse\"><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Nội dung chi phí</br>Content of Expenses </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Đơn giá</br>Unit price </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Số lượng</br>Quantity </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Tạm ứng</br>Advance</td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Thành tiền</br>Amount VNĐ</td></tr>";
                double tongtien = 0;
                double tamung = 0;
                DataTable traveldetail = cls.GetDataTable("sp_getTravelDetail", "@Code", code);
                foreach (DataRow item in traveldetail.Rows)
                {
                    tongtien = tongtien + cls.cToDouble(item["Cong"]);
                    //IsTamUng

                    if (cls.cToString(item["IsTamUng"]).ToLower() == "yes")
                    {
                        tamung = tamung + cls.cToDouble(item["Cong"]);
                    }
                    html = html + "<tr><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + cls.cToString(item["Description"]) + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + cls.cToString0(item["SoTien"]) + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + cls.cToString0(item["SoLuong"]) + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + cls.cToString(item["IsTamUng"]) + "</td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + cls.cToString0(item["Cong"]) + "</td></tr>";
                }
                html = html + "<tr><td colspan=4 align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\">Tổng chi phí/Total Amount:</td><td  style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\"><b>" + cls.FormatNumber(tongtien) + "</b></td></tr>";
                html = html + "<tr><td colspan=4 align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\">Đề nghị tạm ứng/Advance request:</td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\"><b>" + cls.FormatNumber(tamung) + "</b></td></tr>";
                html = html + "</table>";
                string who = "";
                if (appby != "")
                {
                    who = "(has been approved by " + appby + ")";
                }
                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                bool kq = sys.SendMailASP(to, "Approve Travel Request", "Dear  Sir/Madam,</br></br>Please approve Travel Request number " + code + who + ". <a href = '" + strUrl + "/TravelRequest.aspx?ActivationCode=" + activationCode + "&code=" + code + "'>Click here to approve.</a> Or <a href = '" + strUrl + "/TravelRequest.aspx?RejectedCode=" + activationCode + "&code=" + code + "'>Click here to Reject.</a></br>" + html + "</br>Best Regards,");
                if (kq == true)
                {

                    MsgBox1.AddMessage("Submitted successfully!", uc.ucMsgBox.enmMessageType.Success);
                }
                else
                {

                    MsgBox1.AddMessage("Failed to send, Please try again!", uc.ucMsgBox.enmMessageType.Error);
                }
            }
            else//SALES
            {
                DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
                List<sp_getTravelDetailSalesResult> kq = dbs.sp_getTravelDetailSales(code).OrderBy(o => o.FDate).ToList();//sp_getClaimDetail LA STORE
                if (code == "0" || code == "")
                {
                    kq.RemoveAt(0);
                }
                
                RadGrid radgrid1 = new RadGrid();
                radgrid1.DataSource = kq;
                radgrid1.DataBind();
                if (cls.SenEmailSubmitWorkingPlan(code, to, appby, activationCode, nguoidenghi, phongban, mucdich, tungay, denngay, radgrid1, Request.Url.Authority) == true)
                {
                    MsgBox1.AddMessage("Submitted successfully!", uc.ucMsgBox.enmMessageType.Success);
                }
                else
                {

                    MsgBox1.AddMessage("Failed to send, Please try again!", uc.ucMsgBox.enmMessageType.Error);
                }
            }
        }
        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            // RadGrid1.Rebind();
            GridEditableItem editedItem;
            
            switch (e.CommandName)
            {
                case "Email":
                    editedItem = (GridEditableItem)e.Item;
                    //                    status = editedItem["status"].Text;

                    string docno = editedItem["Docno"].Text;
                    string to = editedItem["approval"].Text;
                  //  to = "khuyentt@icpvn.com";
                    string activecode = editedItem["ApprovedCode"].Text;
                    string nguoidenghi = editedItem["Username"].Text;
                    string phongban = editedItem["Department"].Text;
                    string mucdich = editedItem["Purpose"].Text;
                   // string datamung = editedItem["DaTamUng"].Text;
                    string tungay = editedItem["FDate"].Text;
                    string denngay = editedItem["TDate"].Text;
                    string noiden = editedItem["Destination"].Text;
                    string lotrinh = editedItem["Itenerary"].Text;
                    bool Oto =cls.cToBool(editedItem["Oto"].Text);
                     bool Tauhoa =cls.cToBool(editedItem["Tauhoa"].Text);
                     bool Maybay =cls.cToBool(editedItem["Maybay"].Text);
                     bool DatVe = cls.cToBool(editedItem["DatVe"].Text);
                     bool DatPhong = cls.cToBool(editedItem["DatPhong"].Text);
                     string Khac = editedItem["Khac"].Text;
                     string type = editedItem["Type"].Text;
                     SenEmailSubmitTravel(docno, to, "", activecode, nguoidenghi, phongban, mucdich, noiden, lotrinh, tungay, denngay, Oto, Tauhoa, Maybay, DatVe, DatPhong, Khac, type);
                    break;
                case "Reject":
                    editedItem = (GridEditableItem)e.Item;
                    //                    status = editedItem["status"].Text;

                    string docno1 = editedItem["Docno"].Text;
                    string to1 = editedItem["approval"].Text;
                    if (cls.bCapNhat(new string[] { "@docno", "@approval", "@note", "@status" }, new object[] { docno1, to1, "Admin Rejected", 3 }, "sp_UpdateStatusDocno") == true)
                    {
                        MsgBox1.AddMessage("Rejected successfully", uc.ucMsgBox.enmMessageType.Success);
                    }
                    else
                    {
                        MsgBox1.AddMessage("Can not reject", uc.ucMsgBox.enmMessageType.Error);
                    }
                    //gui email den nguoi yeu cau va nguoi duyet

                    break;
               

            }
        }
    }
}