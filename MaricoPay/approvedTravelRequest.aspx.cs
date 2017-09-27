using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MaricoPay.DB;

namespace MaricoPay
{
    public partial class approvedTravelRequest : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                string activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
              //  string RejectedCode = !string.IsNullOrEmpty(Request.QueryString["RejectedCode"]) ? Request.QueryString["RejectedCode"] : Guid.Empty.ToString();
                if (activationCode != Guid.Empty.ToString())
                {
                    clsSys sys = new clsSys();
                    DBTableDataContext db = new DBTableDataContext();
                //    string AppLevel = !string.IsNullOrEmpty(Request.QueryString["AppLevel"]) ? Request.QueryString["AppLevel"] : "";
                    string code = !string.IsNullOrEmpty(Request.QueryString["code"]) ? Request.QueryString["code"] : "";
                    //switch (AppLevel)
                    //{
                    //    case "lv1":
                            var model = db.Approves.SingleOrDefault(p => p.ApprovedCode.ToString() == activationCode && p.Docno == code);
                            model.Status = 1;
                            model.ApprovedCode = null;
                            model.DateApp = DateTime.Now;
                            model.Note = "Approved via Email";
                            db.SubmitChanges();

                            DataTable tbl = cls.GetDataTable("sp_getNextLevel", new string[] { "@Docno", "@username" }, new object[] { code, activationCode });
                            if (tbl.Rows.Count > 0)
                            {
                                //co chuyen len tren
                                //lay thong tin nguoi tren
                                string emailnextlevel = cls.cToString(tbl.Rows[0]["Approval"]);
                               // SenEmailSubmit(docno, emailnextlevel, "");
                              //  MsgBox1.AddMessage("Approved. Notification of the approval (" + code + ") will be emailed to next level and cc to the traveling initiator", uc.ucMsgBox.enmMessageType.Success);
                            }
                            else
                            {

                                //string emailhanhchanh = cls.GetString("sp_getEmailHanhChanh", new string[] { "@Code" }, new object[] { docno });
                                //string cc = txtAppEmail.Text;
                                //if (emailhanhchanh != "")
                                //{
                                //    cc = cc + ";" + emailhanhchanh;
                                //}
                                ////de la nguoi duyet cuoi cung
                                //kq = sys.SendMailASP(txtMyEmail.Text, cc, "Travel request has been Approved", "Travel request  " + docno + " has been approved by " + cls.cToString(tbl.Rows[0]["Username"]));
                                //MsgBox1.AddMessage("Approved. Notification of the approval (" + docno + ") will be emailed to the traveling initiator", uc.ucMsgBox.enmMessageType.Success);
                                //lbMess.Text = "Approved. Notification of the approval (" + code + ") will be emailed to the traveling initiator";
                            }

                            lbMess.Text = "Approved. Notification of the approval (" + code + ") will be emailed to the traveling initiator";
                            //                 string docno = dropApp.SelectedValue;
                            //ChangeStatus(docno, 1, txtAppNote.Text);
                            // LoadClaimApp();
                            //send email
                            //string emailhanhchanh = cls.GetString("sp_getEmailHanhChanh", new string[] { "@Code" }, new object[] { code });
                            //string cc = model.AppEmail;
                            //if (emailhanhchanh != "")
                            //{
                            //    cc = cc + ";" + emailhanhchanh;
                            //}
                            
                            //sys.SendMailASP(model.Email, cc, "Travel request has been Approved", "Travel request  " + code + " has been approved");

                    //        break;
                    //    case "lv2":
                    //        break;
                    //    case "lv3":
                    //        break;
                    //    case "lv4":
                    //        break;
                    //}
                    db.Dispose();

                }
               
            }
        }

        //private void SenEmailSubmit(string code, string to, string appby, string nguoidenghi,string phongban,string mucdich)
        //{
        //    clsSys sys = new clsSys();
        //    // Guid activationCode = Guid.NewGuid();
        //    string activationCode = cls.GetString("sp_getGuiCode", new string[] { "@docno", "@approval" }, new object[] { code, to });
        //    // string to = txtAppEmail.Text;
        //    // string cc = txtMyEmail.Text;
        //    string nguoidenghi = txtName.Text;
        //    string phongban = comboDepartment1.Text;
        //    string mucdich = txtPurpose.Text;
        //    string thoigian = "Từ/From " + raddateFrom.SelectedDate.Value.ToString("dd-MMM-yy") + " Đến/To " + raddateTo.SelectedDate.Value.ToString("dd-MMM-yy");
        //    string phuongtien = chkOto.Checked ? " Oto " : "";
        //    phuongtien = phuongtien + cls.cToString(chkTauHoa.Checked ? " Tàu hỏa " : "");
        //    phuongtien = phuongtien + cls.cToString(chkMayBay.Checked ? " Máy bay " : "");
        //    string html = "";
        //    html = "<table><tr><td>Người đề nghị/Requester: <b>" + nguoidenghi + "</b> Phòng ban/Dept: " + phongban + "</td></tr>";
        //    html = html + "<tr><td>Mục đích công tác/Purpose of business trip: <b>" + mucdich + "</b></td></tr>";
        //    html = html + "<tr><td>Thời gian/Length of days: <b>" + thoigian + "</b></td></tr>";
        //    html = html + "<tr><td>Phương tiện/Transportation mean: <b>" + phuongtien + "</b></td></tr>";
        //    html = html + "</table>";
        //    html = html + "<table  cellpadding=\"2\" cellspacing=\"0\" style=\"width: 100%; border: 1px solid black; border-collapse: collapse; font-size: 12px;\">";
        //    html = html + "<tr style=\"border: 1px solid black; border-collapse: collapse\"><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Nội dung chi phí</br>Content of Expenses </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Đơn giá</br>Unit price </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Số lượng</br>Quantity </td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Tạm ứng</br>Advance</td><td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Thành tiền</br>Amount VNĐ</td></tr>";
        //    double tongtien = 0;
        //    double tamung = 0;
        //    foreach (GridDataItem item in RadGrid1.Items)
        //    {
        //        tongtien = tongtien + cls.cToDouble(item["Cong"].Text);

        //        Label lb = (Label)item.FindControl("Label1");
        //        if (lb.Text.ToLower() == "yes")
        //        {
        //            tamung = tamung + cls.cToDouble(item["Cong"].Text);
        //        }
        //        html = html + "<tr><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["Description"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["SoTien"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["SoLuong"].Text + " </td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + lb.Text + "</td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse;\">" + item["Cong"].Text + "</td></tr>";
        //    }
        //    html = html + "<tr><td colspan=4 align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\">Tổng chi phí/Total Amount:</td><td  style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\"><b>" + cls.FormatNumber(tongtien) + "</b></td></tr>";
        //    html = html + "<tr><td colspan=4 align=\"center\" style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\">Đề nghị tạm ứng/Advance request:</td><td style=\"border: 1px solid black; border-bottom: 0px; border-collapse: collapse; font-weight: bold;\"><b>" + cls.FormatNumber(tamung) + "</b></td></tr>";
        //    html = html + "</table>";
        //    string who = "";
        //    if (appby != "")
        //    {
        //        who = "(has been approved by " + appby + ")";
        //    }
        //    bool kq = sys.SendMailASP(to, "Approval Travel Request", "Dear  Sir/Madam,</br>Please approve Travel Request No " + code + who + ". <a href = '" + Request.Url.AbsoluteUri.Replace("TravelRequest.aspx?type=0", "") + "approvedTravelRequest.aspx?ActivationCode=" + activationCode + "&code=" + code + "'>Click here to approve.</a> Or <a href = '" + Request.Url.AbsoluteUri.Replace("TravelRequest.aspx?type=0", "") + "TravelRequest.aspx?RejectedCode=" + activationCode + "&code=" + code + "'>Click here to Reject.</a></br>" + html + "</br>Best Regards,");
        //    if (kq == true)
        //    {
        //        btSave.Visible = false;
        //        btSubmit.Visible = false;
        //        lbStatus.Text = "Submitted";
        //        lbMess.Text = "Submit success";
        //        MsgBox1.AddMessage("Submit success", uc.ucMsgBox.enmMessageType.Success);
        //    }
        //    else
        //    {
        //        lbStatus.Text = "Saved";
        //        lbMess.Text = "Submit fail";
        //        MsgBox1.AddMessage("Sent email fail, You try again later", uc.ucMsgBox.enmMessageType.Error);
        //    }
        //}

    }
}