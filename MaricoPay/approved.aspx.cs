using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MaricoPay.DB;
namespace MaricoPay
{
    public partial class approved : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                string activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
                string RejectedCode = !string.IsNullOrEmpty(Request.QueryString["RejectedCode"]) ? Request.QueryString["RejectedCode"] : Guid.Empty.ToString();
                if (activationCode != Guid.Empty.ToString())
                {
                    clsSys sys = new clsSys();
                    DBTableDataContext db = new DBTableDataContext();
                    string AppLevel = !string.IsNullOrEmpty(Request.QueryString["AppLevel"]) ? Request.QueryString["AppLevel"] : "";
                    string code = !string.IsNullOrEmpty(Request.QueryString["code"]) ? Request.QueryString["code"] : "";
                    switch (AppLevel)
                    {
                        case "lv1":
                            var model = db.ClaimExpenses.SingleOrDefault(p => p.ApprovedCode1.ToString() == activationCode && p.Code_PK == code);
                            model.Status = 1;
                            model.ApprovedCode1 = null;
                            model.NoteApprover = "Approved via Email";
                            db.SubmitChanges();
                            lbMess.Text = "Approved, Notification of the approval will be emailed to the claimant";
                            //                 string docno = dropApp.SelectedValue;
                            //ChangeStatus(docno, 1, txtAppNote.Text);
                            // LoadClaimApp();
                            //send email
                            sys.SenEmailApproved(code,model.Email,model.AppEmail);

                            break;
                        case "lv2":
                            break;
                        case "lv3":
                            break;
                        case "lv4":
                            break;
                    }
                    db.Dispose();

                }
                else
                {
                    if (RejectedCode != Guid.Empty.ToString())
                    {
                        clsSys sys = new clsSys();
                        DBTableDataContext db = new DBTableDataContext();
                        string AppLevel = !string.IsNullOrEmpty(Request.QueryString["AppLevel"]) ? Request.QueryString["AppLevel"] : "";
                        string code = !string.IsNullOrEmpty(Request.QueryString["code"]) ? Request.QueryString["code"] : "";
                        switch (AppLevel)
                        {
                            case "lv1":
                                var model = db.ClaimExpenses.SingleOrDefault(p => p.ApprovedCode1.ToString() == RejectedCode && p.Code_PK == code);
                                model.Status = 3;
                                model.ApprovedCode1 = null;
                                model.NoteApprover = "Rejected via Email";
                                db.SubmitChanges();
                                lbMess.Text = "Rejected, Notification of the reject will be emailed to the claimant";
                             

                                sys.SenEmailReject(code, "Rejected via Email",model.Email,model.AppEmail);

                                break;
                            case "lv2":
                                break;
                            case "lv3":
                                break;
                            case "lv4":
                                break;
                        }
                        db.Dispose();

                    }
                }
            }
        }
    }
}