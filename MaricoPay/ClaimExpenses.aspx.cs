using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Linq;
using MaricoPay.DB;
using System.IO;
using System.Collections.Specialized;
namespace MaricoPay
{
    public partial class ClaimExpenses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             // string sss=  Request.Form["us"];
              if (Session["username"] != null /*&& Request.Form["us"] != null*/)
               // if (Request.QueryString["us"] != null)//click vao avatar
                {
                    
                    hdStatus.Value = "0";
                    //
                    if (Request.QueryString["type"] != null)//click vao avatar
                    {
                        int type = int.Parse(Request.QueryString["type"].ToString());
                        switch (type)
                        {
                            case 0: //tao moi
                                dropApp.Visible = false;
                                dropSaved.Visible = true;
                                radioClaim.Visible = true;
                                LoadMarKet();
                                LoadClaim();
                                GetNewClaim();
                                getClaimDetail("");
                                Session["AutoNumber"] = 0;
                                HiddenColum();
                                break;
                            case 2://approved
                                dropApp.Visible = true;
                                dropSaved.Visible = false;
                                radioClaim.Visible = false;
                                LoadMarKet();
                                hdStatus.Value = "2";
                                LoadClaimApp();
                                HiddenColum();
                                break;
                        }
                        setButton(int.Parse(hdStatus.Value));
                    }
                    else
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                   
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            //else
            //{
            //    // something here

            //    if (!string.IsNullOrEmpty(__EVENTTARGET.Value)) // it should be changed value here...
            //    {
            //        string s = __EVENTTARGET.Value;
            //    }
            //}
        }
        private void LoadMarKet()
        {
            DBTableDataContext ds = new DBTableDataContext();
            var kq = ds.Markets.ToList();//Markets LA TABLE
            dropMarket.DataValueField = "Market_PK";
            dropMarket.DataTextField = "Description";
            dropMarket.DataSource = kq;
            dropMarket.DataBind();
            ds.Dispose();

        }
        private void GetNewClaim()
        {
            string us = Session["username"].ToString();
            DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            var kq = dbs.sp_GetUserInfo(us).ToList();
           // txtMarket.Text = kq[0].Market;
            txtDepartment.Text = kq[0].Department;
            txtName.Text = kq[0].Fullname;
            txtPosition.Text = kq[0].Position;
            txtAppName.Text = kq[0].FullNameRec;
            txtAppEmail.Text = kq[0].EmailRec;
            txtMyEmail.Text = kq[0].Email;
            dropMarket.SelectedValue = kq[0].Market;
            raddateNow.SelectedDate = DateTime.Now;
            lbStatus.Text = kq[0].StatusText;
            hdStatus.Value = kq[0].Status.ToString();
            txtAppNote.Visible = false;
          //  txtPurpose.Text = "";
            radnumAdvncedAmount.Value = 0;
            dbs.Dispose();
            Session["NewDocNo"] = GenalCode();
        }
        private void GetOldClaim(string code)//0 cho phep them; 1: ko cho phep them
        {
           // string us = Session["username"].ToString();
            DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            var kq = dbs.sp_GetUserInfo_ClaimExists(code).ToList();
            // txtMarket.Text = kq[0].Market;
            txtDepartment.Text = kq[0].Department;
            txtName.Text = kq[0].Fullname;
            txtPosition.Text = kq[0].Position;
            txtAppName.Text = kq[0].FullNameRec;
            txtAppEmail.Text = kq[0].EmailRec;
            txtMyEmail.Text = kq[0].Email;
            dropMarket.SelectedValue = kq[0].Market;
            raddateNow.SelectedDate = kq[0].DateRec;
            radioClaim.SelectedValue = kq[0].Type;
          //  txtPurpose.Text = kq[0].Purpose;
          //  raddateFrom.SelectedDate = kq[0].FDate;
          //  raddateTo.SelectedDate = kq[0].TDate;
            radnumAdvncedAmount.Value = (double)kq[0].DaTamUng;
          //  radnumDays.Value =(double) kq[0].NoDays;
            lbStatus.Text = kq[0].StatusText;
            hdStatus.Value = kq[0].Status.ToString();
            txtAppNote.Visible = true;
            txtAppNote.Text = kq[0].NoteApprover;
            dbs.Dispose();
            Session["NewDocNo"] = code;
        }
        private void setButton(int status)
        {
            switch (status)
            {
                case 0: //new
                    btSave.Visible = true;
                    btSubmit.Visible = true;
                    btPrint.Visible = false;
                    txtAppNote.Visible = false;
                    lbReason.Visible = false;
                    btApp.Visible = false;
                    btReject.Visible = false;
                    break;
                case 1://approved
                    btSave.Visible = false;
                    btSubmit.Visible = false;
                    btPrint.Visible = true;
                    txtAppNote.Visible = true;
                    lbReason.Visible = true;
                    txtAppNote.ReadOnly = true;
                    btApp.Visible = false;
                    btReject.Visible = false;
                    break;
                case 2://submitted
                    btSave.Visible = false;
                    btSubmit.Visible = false;
                    btPrint.Visible = false;
                    txtAppNote.Visible = false;
                    lbReason.Visible = false;
                    btApp.Visible = false;
                    btReject.Visible = false;
                    break;
                case 3://rejected
                    btSave.Visible = true;
                    btSubmit.Visible = false;
                    btPrint.Visible = false;
                    txtAppNote.Visible = true;
                    txtAppNote.ReadOnly = true;
                    lbReason.Visible = true;
                    btApp.Visible = false;
                    btReject.Visible = false;
                    break;
                default:
                     btSave.Visible = true;
                    btSubmit.Visible = false;
                    btPrint.Visible = false;
                    txtAppNote.Visible = false;
                    btApp.Visible = false;
                    btReject.Visible = false;
                     lbReason.Visible = false;
                    break;

            }
        }

        //protected void raddateTo_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        //{
        //    TinhNgay();
        //}

        //protected void raddateFrom_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        //{
        //    TinhNgay();
        //}
        //protected void raddateTo_Sales_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        //{
        //    TinhNgaySales();
        //}

        //protected void raddateFrom_Sales_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        //{
        //    TinhNgaySales();
        //}
        //private void TinhNgay()
        //{
        //    if (raddateTo.SelectedDate != null && raddateFrom.SelectedDate != null)
        //    {
        //        radnumDays.Value =(raddateTo.SelectedDate.Value - raddateFrom.SelectedDate.Value).TotalDays+1;
        //    }
        //}
        private double TinhNgaySales()
        {
            double kq = 0;
            clsSys sys = new clsSys();
            if (raddateTo_Sales.SelectedDate != null && raddateFrom_Sales.SelectedDate != null)
            {

                kq = (raddateTo_Sales.SelectedDate.Value - raddateFrom_Sales.SelectedDate.Value).TotalDays + 1;
            }
            return kq;
        }
        private void LoadClaim()
        {
            DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            var kq = dbs.sp_getClaimSaved(Session["username"].ToString(), radioClaim.SelectedValue);//sp_getClaimSaved la store co 2 tham so dau vao
            dropSaved.DataSource = kq;
            dropSaved.DataValueField = "Values";
            dropSaved.DataTextField = "Text";
            dropSaved.DataBind();
            dbs.Dispose();
        }
        private void LoadClaimApp()
        {
            DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            var kq = dbs.sp_getClaimApproved(Session["username"].ToString());//sp_getClaimSaved la store co 2 tham so dau vao
            dropApp.DataSource = kq;
            dropApp.DataValueField = "Values";
            dropApp.DataTextField = "Text";
            dropApp.DataBind();
            dbs.Dispose();
        }
        private string GenalCode()
        {
            //dd-MM-yy-Automumber
       //     DBTableDataContext dbs = new DBTableDataContext();
            DBStoreDataContext dbs = new DBStoreDataContext();
            clsSys sys = new clsSys();
            string code = Session["username"].ToString()+"-"+raddateNow.SelectedDate.Value.ToString("dd-MM-yy") +"-";
            var kq = dbs.sp_GenaCode(code).ToList();
            dbs.Dispose();
            int rows = 0;
            if (kq.Count > 0)
            {
                rows =sys.cToInt(kq[0].Column1);
            }
           rows = rows + 1;
           code = code + rows.ToString();
            
            return code;
        }
        private void FillTable()
        {
           
            List<sp_getClaimDetailResult> tbl = (List<sp_getClaimDetailResult>)Session["ClaimDetail"];
            string temp="";
            int rowspan=0;
           string tbtmp = "";
            string nowitem = "";
            int bienchay=0;
            Literal1.Text = "";
            string html = "";
           
            string tr = "";
            foreach (sp_getClaimDetailResult item in tbl)
            {
             //   html = "<tr>";
                bienchay++;
                if (item.TotalVN != 0)
                {
                    nowitem = item.FDate.ToString("dd-MMM-yy") + item.TDate.ToString("dd-MMM-yy") +  item.Purpose;
                    if (nowitem ==temp|| temp=="")
                    {
                        rowspan++;
                        //merge rows
                        
                        tbtmp="<tr><td rowspan=\"" + rowspan + "\" style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.FDate.ToString("dd-MMM-yy") + "</td>"
                        +"<td  rowspan=\"" + rowspan + "\" style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.TDate.ToString("dd-MMM-yy") + "</td>"
                        +"<td rowspan=\"" + rowspan + "\"  style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Purpose + "</td>";

                        temp = nowitem;
                        if (rowspan > 1)
                            tr = "<tr>";
                        else
                            tr = "";
                        html = html+tr+"<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Date.ToString("dd-MMM-yy") + "</td>"
                                      + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>"
                                       + "<a  href='#' onclick=\"javascript:window.open('/popVAT.aspx?cp=" + item.CompanyName + "&pv=" + item.Province + "&vatcode=" + item.VATCode + "&vatamount=" + item.VATAmount + "','VAT','width=500,height=150')\">" + item.No + "</a></td>"
                                        + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Notation + "</td>";

                        html = html
                       + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Description + "</td>";
                        if (radioClaim.SelectedValue.ToLower() != "domestic")//trong nuoc
                        {
                            html = html + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.Currency + "-" + String.Format("{0:0,0}", item.Rate) + "</td>"
                         + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.Amount) + "</td>";
                        }
                        html = html
                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.TotalVN) + "</td>"
                       + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>"
                          + "<a  href='#' onclick=\"javascript:window.open('/ImagesUpload/" + item.PictureURL + "','mywindowtitle','width=800,height=600')\"><img  id='popupImage' src='/ImagesUpload/" + item.PictureURL + "' alt='' style='width:80px;height:60px;'></a></td>"
                      + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'><input type='button' value='Delete' onclick='deleteRow(this)'></td></tr>";

                    }
                    else
                    {
                        Literal1.Text = Literal1.Text + tbtmp+html;
                      //  html3=tbtmp+html2;
                      html = "";
                      //  Literal1.Text = Literal1.Text + html;
                        temp = item.FDate.ToString("dd-MMM-yy") + item.TDate.ToString("dd-MMM-yy") + item.Purpose;
                        rowspan = 1;
                        tbtmp = "<tr><td rowspan=\"" + rowspan + "\" style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.FDate.ToString("dd-MMM-yy") + "</td>"
                      + "<td  rowspan=\"" + rowspan + "\" style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.TDate.ToString("dd-MMM-yy") + "</td>"
                      + "<td rowspan=\"" + rowspan + "\"  style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Purpose + "</td>";
                        if (rowspan > 1)
                            tr = "<tr>";
                        else
                            tr = "";
                        html = html+tr+"<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Date.ToString("dd-MMM-yy") + "</td>"
                                      + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>"
                                       + "<a  href='#' onclick=\"javascript:window.open('/popVAT.aspx?cp=" + item.CompanyName + "&pv=" + item.Province + "&vatcode=" + item.VATCode + "&vatamount=" + item.VATAmount + "','VAT','width=500,height=150')\">" + item.No + "</a></td>"
                                        + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Notation + "</td>";

                        html = html
                       + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>" + item.Description + "</td>";
                        if (radioClaim.SelectedValue.ToLower() != "domestic")//trong nuoc
                        {
                            html = html + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + item.Currency + "-" + String.Format("{0:0,0}", item.Rate) + "</td>"
                         + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.Amount) + "</td>";
                        }
                        html = html
                          + "<td style='color: #000000; border:1px solid black; border-collapse:collapse; text-align:right;'>" + String.Format("{0:0,0}", item.TotalVN) + "</td>"
                       + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'>"
                          + "<a  href='#' onclick=\"javascript:window.open('/ImagesUpload/" + item.PictureURL + "','mywindowtitle','width=800,height=600')\"><img  id='popupImage' src='/ImagesUpload/" + item.PictureURL + "' alt='' style='width:80px;height:60px;'></a></td>"
                      + "<td style='color: #000000; border:1px solid black; border-collapse:collapse;'><input type='button' value='Delete' onclick='deleteRow(this)'></td></tr>";
        
                    }
          
                    if (bienchay == tbl.Count)//dong cuoi
                    {
                       // html = tbtmp + html;
                        Literal1.Text = Literal1.Text + tbtmp + html;
                    }
                  
                }
            }
            
        }
        private bool SaveParent(string code)
        {
            try
            {
                using (var dbs = new DBTableDataContext())
                {
                    //ClaimExpense LA TABLE
                    var model = new ClaimExpense { Code_PK = code, Type = radioClaim.SelectedValue, DateRec = raddateNow.SelectedDate.Value, UserName = Session["username"].ToString(), Approver = txtAppName.Text, AppEmail = txtAppEmail.Text, Status = 0, /*FDate = raddateFrom.SelectedDate.Value, TDate = raddateTo.SelectedDate.Value, NoDays = (int)radnumDays.Value, Purpose = txtPurpose.Text,*/ Market = dropMarket.SelectedValue, Department = txtDepartment.Text, Position = txtPosition.Text, FullName = txtName.Text, DaTamUng =(decimal)radnumAdvncedAmount.Value, Tra_ThuChenhLech = 0, DocTot = 0, NoteApprover = "", Email=txtMyEmail.Text };
                    dbs.ClaimExpenses.InsertOnSubmit(model);
                    dbs.SubmitChanges();

                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool UpdateParent(string code)
        {
            try
            {
                using (var db = new DBTableDataContext())
                {
                    //ClaimExpense LA TABLE

                    var model = db.ClaimExpenses.SingleOrDefault(o => o.Code_PK == code);
                      model.DateRec = raddateNow.SelectedDate.Value;
                    model.UserName = Session["username"].ToString();
                    model.Approver = txtAppName.Text;
                    model.AppEmail = txtAppEmail.Text;
                    model.Status = 0;
                  //  model.FDate = raddateFrom.SelectedDate.Value;
                  //  model.TDate = raddateTo.SelectedDate.Value;
                  //  model.NoDays = (int)radnumDays.Value;
                   // model.Purpose = txtPurpose.Text;
                    model.Market = dropMarket.SelectedValue;
                    model.Department = txtDepartment.Text;
                    model.Position = txtPosition.Text;
                    model.FullName = txtName.Text;
                    model.DaTamUng =(decimal)radnumAdvncedAmount.Value;
                 //   model.Tra_ThuChenhLech = 0;
                  //  model.DocTot = 0;
                  //  model.NoteApprover = "";
                    model.Email=txtMyEmail.Text;
                    
                        db.SubmitChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void UpdateDetail(string code)
        {
            //delete
             using (var db = new DBTableDataContext())
                        {
                            db.ClaimExpensesDetails.DeleteAllOnSubmit(db.ClaimExpensesDetails.Where(o => o.Code_FK == code));
                            db.SubmitChanges();
                        }
      //insert
             SaveDetail(code);
        }
        private void updateDocTot(string code)
        {
            DBStoreDataContext dbs = new DBStoreDataContext();
            dbs.sp_UpdateDocTotal(code);
            dbs.SubmitChanges();
            dbs.Dispose();
        }
        private void SaveDetail(string code)
        {
            DBTableDataContext dbs = new DBTableDataContext();
            try
            {
                List<sp_getClaimDetailResult> tbl = (List<sp_getClaimDetailResult>)Session["ClaimDetail"];
                foreach (sp_getClaimDetailResult item in tbl)
                {
                    if (item.TotalVN > 0)
                    {
                        var model = new ClaimExpensesDetail { Code_FK = code, Date = item.Date, No = item.No, Notation = item.Notation, Charges_FK = item.Charges_FK, Currency = item.Currency, Rate = item.Rate, Amount = item.Amount, TotalVN = item.TotalVN, PictureURL = item.PictureURL,CompanyName=item.CompanyName,Province=item.Province,VATCode=item.VATCode,VATAmount=item.VATAmount,FDate=item.FDate,TDate=item.TDate,NoDays=item.NoDays,Purpose=item.Purpose };
                        dbs.ClaimExpensesDetails.InsertOnSubmit(model);
                    }
                }
                // dbs.ClaimExpensesDetails.InsertAllOnSubmit(tbl);
                dbs.SubmitChanges();
                LoadSum(code);
                lbMess.Text = "Save success";
                
            }
            catch {

                    #region Delete file
                    //string fileToDelete = Server.MapPath("~/" + Lib.clsConfig.Ads + FileName).ToString();
                    //if (File.Exists(fileToDelete) && FileName != "NoPhoto.jpg")
                    //{
                    //    File.Delete(fileToDelete);
                    //}
                    #endregion
                    var model1 = dbs.ClaimExpenses.SingleOrDefault(p => p.Code_PK == code);
                    dbs.ClaimExpenses.DeleteOnSubmit(model1);
                    dbs.SubmitChanges();
                    lbMess.Text = "Save Error";
            }
            dbs.Dispose();
        }
        protected void btAdd_Click(object sender, EventArgs e)
        {
            if (raddateFrom_Sales.IsEmpty)
            {
                MsgBox1.AddMessage("Please fill date in from date", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (raddateTo_Sales.IsEmpty)
            {
                MsgBox1.AddMessage("Please fill date in to date", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (raddateTo_Sales.SelectedDate.Value < raddateFrom_Sales.SelectedDate.Value)
            {
                MsgBox1.AddMessage("To date must be greater or equal from date", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            double nodays = TinhNgaySales();
            if (txtPurpose_sales.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Please fill text in Purpose", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            clsSys sys = new clsSys();
            List<sp_getClaimDetailResult> tbl = (List<sp_getClaimDetailResult>)Session["ClaimDetail"];
            sp_getClaimDetailResult item = new sp_getClaimDetailResult();
            //them Code_FK
            item.Code_FK=Session["NewDocNo"].ToString();
            item.Date=raddateInvoice.SelectedDate.Value;
            item.No=txtNoInvoice.Text;
            item.Notation=txtNotation.Text;
            item.Charges_FK=combocharges1.Values;
            item.Description = combocharges1.Text;
            item.CompanyName = sys.cToString(Session["Company"]);
            item.Province = sys.cToString(Session["Province"]);
            item.VATCode = sys.cToString(Session["VAT"]);
            item.VATAmount = sys.cToDuoble(Session["VATAmount"]);
            item.FDate = raddateFrom_Sales.SelectedDate.Value;
            item.TDate = raddateTo_Sales.SelectedDate.Value;
            item.Purpose = txtPurpose_sales.Text.Trim();
            if (combocharges1.Values.ToLower() == "ho")
            {
                item.NoDays = sys.cToInt(nodays - 1);
                
            }
            else
            {
                item.NoDays = sys.cToInt(nodays);
            }
            if (radioClaim.SelectedValue.ToLower() == "domestic")
            {
                item.Currency = "VND";
                item.Rate = 1;
            }
            else
            {
                item.Currency = comboCurrence1.CurrText;
                item.Rate = double.Parse(comboCurrence1.RateText);
            }
            item.Amount=(decimal) radnumAmount.Value;
            item.TotalVN = (decimal)radnuTotalVND.Value;
            Session["AutoNumber"] = (int)Session["AutoNumber"] + 1;
            imgUpload1.uploadimg(Session["NewDocNo"].ToString() + "-"+Session["AutoNumber"].ToString());
            item.PictureURL = imgUpload1.FileName;
            tbl.Add(item);
            Session["ClaimDetail"] = tbl;
        Session["Company"]="";
         Session["Province"]="";
          Session["VAT"]="";
          Session["VATAmount"]=0;
            //if (radioClaim.SelectedValue.ToLower() == "domestic")
            FillTable();
            raddateInvoice.Focus();
        }
        private void getClaimDetail(string code)
        {
            DB.DBStoreDataContext dbs = new DB.DBStoreDataContext();
            List<sp_getClaimDetailResult> kq = dbs.sp_getClaimDetail(code,false).OrderBy(o => o.ID).ToList();//sp_getClaimDetail LA STORE
            Session["ClaimDetail"] = kq;
            Session["AutoNumber"] = kq.Count;
            dbs.Dispose();
            
        }
        protected void dropSaved_SelectedIndexChanged(object sender, EventArgs e)
        {
            Literal1.Text = "";
            getClaimDetail(dropSaved.SelectedValue);
            if (dropSaved.SelectedValue == "0")
            {
                GetNewClaim();
              //  btApp.Visible = false;
                gridSum.Visible = false;
                radioClaim.Enabled = true;
            }
            else 
            {
                GetOldClaim(dropSaved.SelectedValue);
                HiddenColum();
                radioClaim.Enabled = false;
                FillTable();
                gridSum.Visible = true;
                LoadSum(dropSaved.SelectedValue);
              
            }
            setButton(int.Parse(hdStatus.Value));
           // btApp.Visible = false;
        }
        protected void dropApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            Literal1.Text = "";
            getClaimDetail(dropApp.SelectedValue);
            if (dropApp.SelectedValue == "0")
            {
                // GetNewClaim();
                btApp.Visible = false;
                btReject.Visible = false;
                gridSum.Visible = false;
            }
            else
            {
                btApp.Visible = true;
                btReject.Visible = true;
                txtAppNote.ReadOnly = false;
                txtAppNote.Visible = true;
                lbReason.Visible = true;
                btSave.Visible = false;
                btSubmit.Visible = false;
                GetOldClaim(dropApp.SelectedValue);
                HiddenColum();
                FillTable();
                gridSum.Visible = true;
                LoadSum(dropApp.SelectedValue);
            }
        }
        private void LoadSum(string code)
        {
            DBStoreDataContext dbs = new DBStoreDataContext();
            var model = dbs.sp_LoadTotalClaim4Doc(code).ToList();
            if (model.Count > 3)
            {
                gridSum.DataSource = model;
                gridSum.DataBind();
                gridSum.Rows[gridSum.Rows.Count - 1].Font.Bold = true;
                gridSum.Rows[gridSum.Rows.Count - 2].Font.Bold = true;
                gridSum.Rows[gridSum.Rows.Count - 3].Font.Bold = true;
                //gridSum.Columns[0].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                //gridSum.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                //gridSum.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                
            }
            dbs.Dispose();
        }
        protected void btSave_Click(object sender, EventArgs e)
        {
            //if (txtPurpose.Text.Trim() == "")
            //{
            //    MsgBox1.AddMessage("Please fill Purpose",uc.ucMsgBox.enmMessageType.Error);
            //    txtPurpose.Focus();
            //}
            //else
            //{
                if (dropSaved.SelectedValue == "0")//tao moi
                {
                    string code = Session["NewDocNo"].ToString();
                    if (SaveParent(code) == true)
                    {
                        SaveDetail(code);
                        updateDocTot(code);
                        LoadSum(code);
                        btSubmit.Visible = true;
                    }
                }
                else//update
                {
                    if (UpdateParent(dropSaved.SelectedValue) == true)
                    {
                        UpdateDetail(dropSaved.SelectedValue);
                        updateDocTot(dropSaved.SelectedValue);
                        LoadSum(dropSaved.SelectedValue);
                        btSubmit.Visible = true;
                    }
                }
           // }
        }
        protected void hdRowDelete_ValueChanged(object sender, EventArgs e)
        {
            int s = int.Parse(__EVENTTARGET.Value);
            //html =3 ==> tbl=0
            List<sp_getClaimDetailResult> tbl = (List<sp_getClaimDetailResult>)Session["ClaimDetail"];
            int ix = s - 3;
            //#region Delete file
            //string photo = tbl[ix].PictureURL;
            //string fileToDelete = Server.MapPath("~/ImagesUpload/" + photo).ToString();
            //if (File.Exists(fileToDelete) && photo != "NoPhoto.jpg")
            //    {
            //        File.Delete(fileToDelete);
            //    }
            //    #endregion
            tbl.RemoveAt(ix);
            Session["ClaimDetail"]=tbl;
             FillTable();
             __EVENTTARGET.Value = "0";
        }

       
        private void ChangeStatus(string code,int status,string note)
        {
            using (var db = new DBTableDataContext())
            {
                var model = db.ClaimExpenses.SingleOrDefault(p => p.Code_PK == code);
                model.Status = status;
                model.NoteApprover = note;
                db.SubmitChanges();
            }
        }
        private void SenEmailSubmit(string code)
        {
            clsSys sys = new clsSys();
            string to = "khuyentt@icpvn.com"; //txtAppEmail.Text;
            string cc = "khuyentt@beaute-cos.com"; //txtMyEmail.Text;
            sys.SendMailASP(to, cc, "Approved Claim", "Please approve Claim No "+code);

        }
        private void SenEmailApproved(string code)
        {
            clsSys sys = new clsSys();
            string to = "khuyentt@icpvn.com";// txtMyEmail.Text;
            string cc = "khuyentt@beaute-cos.com";// txtAppEmail.Text;
            sys.SendMailASP(to, cc, "Claim has been Approved", "Claim " + code+" has been approved");

        }
        private void SenEmailReject(string code,string resion)
        {
            clsSys sys = new clsSys();
            string to = "khuyentt@icpvn.com";// txtMyEmail.Text;
            string cc = "khuyentt@beaute-cos.com";// txtAppEmail.Text;
            sys.SendMailASP(to, cc, "Claim has been Rejected", "Claim " + code + " has been rejected with resion "+resion);

        }
        protected void btSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dropSaved.SelectedValue == "0")//tao moi
                {
                    ChangeStatus(Session["NewDocNo"].ToString(), 2, "");
                    //send email nguoi approve anh cc me
                    SenEmailSubmit(Session["NewDocNo"].ToString());
                    btSave.Visible = false;
                    btSubmit.Visible = false;
                }
                else
                {
                    ChangeStatus(dropSaved.SelectedValue, 2, "");
                    //send email nguoi approve anh cc me
                    SenEmailSubmit(dropSaved.SelectedValue);
                    btSave.Visible = false;
                    btSubmit.Visible = false;
                }
             //   Submitted 	
                lbStatus.Text = "Submitted";
                lbMess.Text = "Submit success";
            }
            catch { }
            
        }

        protected void btApp_Click(object sender, EventArgs e)
        {
            string docno = dropApp.SelectedValue;
            ChangeStatus(docno, 1, txtAppNote.Text);
             LoadClaimApp();
            //send email
             SenEmailApproved(docno);
             btAdd.Visible = false;
             btReject.Visible = false;
        }

        protected void btReject_Click(object sender, EventArgs e)
        {
           
            if (txtAppNote.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Please input Reason Rejected", uc.ucMsgBox.enmMessageType.Error);
                txtAppNote.Focus();
            }
            else
            {
                string docno = dropApp.SelectedValue;
                ChangeStatus(docno, 3, txtAppNote.Text);
               
                LoadClaimApp();
                //send email
                SenEmailReject(docno, txtAppNote.Text);
                btAdd.Visible = false;
                btReject.Visible = false;
            }
        }
        private void HiddenColum()
        {
            if (radioClaim.SelectedValue.ToLower() == "domestic")//trong nuoc
            {
                
                comboCurrence1.Visible = false;
              
               
                radnumAmount.Visible = false;
                tdAmountCurr.Visible = false;
               
                tdCurr.Visible = false;
                tdcomboCurr.Visible = false;
                tdradnumAmount.Visible = false;

                tdCurr1.Visible = false;
                tdRate.Visible = false;
            }
            else
            {
                comboCurrence1.Visible = true;
                radnumAmount.Visible = true;
                tdCurr.Visible = true;
                tdAmountCurr.Visible = true;
                tdcomboCurr.Visible = true;
                tdradnumAmount.Visible = true;

                tdCurr1.Visible = true;
                tdRate.Visible = true;
            }
        }
        protected void radioClaim_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadClaim();
            HiddenColum();
        }

        protected void btPrint_Click(object sender, EventArgs e)
        {
            //PrintHelper.PrintWebControl(pnlPrintClaimExpenses);
            //Printed();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            Session["docno"] = dropSaved.SelectedValue;
            sb.Append("window.open('PrintClaim.aspx','PrintMe', 'height=900px,width=1024px,scrollbars=1');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "NewClientScript", sb.ToString(), true);
        }
        private void Printed()
        {
            //Control ctrl = (Control)Session["ctrl"];
            //if (ctrl != null)
            //    PrintHelper.PrintWebControl(ctrl);
            //try
            //{
            //    if (Session["MaHDPrinte"] != null)
            //    {
            //        BWInCacNghiepVuKho objNVK = new BWInCacNghiepVuKho();
            //        DataSet ds = new DataSet();
            //        //Trung Tam PP
            //        ds = objNVK.GetTrungTamPP(ref error, Session["Sitecode"].ToString());
            //        lblTrungTamPP.Text = ds.Tables[0].Rows[0]["sitename"].ToString();
            //        lblDiaChi.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            //        lblTelFax.Text = ds.Tables[0].Rows[0]["TelFax"].ToString();

            //        BWNhapHoaDonBanHang obj = new BWNhapHoaDonBanHang();
            //        DataTable dtHoDon = obj.dsInfoHoaDonChuaIn(ref error, Session["MaHDPrinte"].ToString()).Tables[0];
            //        if (dtHoDon.Rows[0]["so"].ToString() == "3")
            //        {
            //            lblTitle.Text = "Lệnh Xuất Hàng (Có Chiết Khấu)";
            //        }
            //        else
            //        {
            //            lblTitle.Text = "Lệnh Xuất Hàng";
            //        }
            //        lblTenTVV.Text = dtHoDon.Rows[0]["MaTVV"].ToString() + "_" + dtHoDon.Rows[0]["TenTVV"].ToString();
            //        lblDienThoai.Text = dtHoDon.Rows[0]["DienThoai"].ToString();
            //        lblSoDonHang.Text = dtHoDon.Rows[0]["MaHD"].ToString();
            //        lblNgayDonHang.Text = dtHoDon.Rows[0]["NgayHD"].ToString();
            //        lbGhiChu.Text = dtHoDon.Rows[0]["PayNo"].ToString();
            //        double dTienChuaCK = double.Parse(dtHoDon.Rows[0]["TienHD"].ToString());

            //        lblTienChuaChietKhau.Text = String.Format("{0:###,###}", dTienChuaCK);

            //        //PhieuThu
            //        lblTenTVVPhieuThu.Text = dtHoDon.Rows[0]["TenTVV"].ToString();
            //        lblSoTien.Text = "";

            //        DataTable dt = obj.dsChiTietHoaDonPrint(ref error, Session["MaHDPrinte"].ToString()).Tables[0];
            //        gvListChiTietHoaDon.DataSource = dt;
            //        gvListChiTietHoaDon.DataBind();
            //        HienThiGiaNTD();
            //        double dTienKhuyenMaiBangTien = 0;
            //        double dTongTienNTD = 0;
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            if (dt.Rows[i]["ThanhTien"].ToString() != "")
            //            {
            //                double dTienTemp = double.Parse(dt.Rows[i]["ThanhTien"].ToString());
            //                if (dTienTemp < 0)
            //                {
            //                    dTienKhuyenMaiBangTien = dTienKhuyenMaiBangTien + dTienTemp;
            //                }
            //            }
            //            dTongTienNTD = dTongTienNTD + cls.cToDouble(dt.Rows[i]["ThanhTienNTD"]);
            //        }
            //        lblTienConPhaiThanhToan.Text = String.Format("{0:###,###}", dTienChuaCK);
            //        lblKhuyenMaiBangTien.Text = String.Format("{0:###,###}", System.Math.Abs(dTienKhuyenMaiBangTien));
            //        lblSoTien.Text = String.Format("{0:###,###}", dTienChuaCK);
            //        //lblSoTien.Text = String.Format("{0:###,###}", dTienChuaCK + dTienKhuyenMaiBangTien);
            //        lblSoTienNTD.Text = String.Format("{0:###,###}", dTongTienNTD);

            //        PrintHelper.PrintWebControl(pnlPrintHoaDon);
            //    }
            //    else
            //    {
            //        lblChuKyKeToan.Text = "Looix";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    lblChuKyKeToan.Text = error + " Lỗi: " + ex;
            //    return;
            //}
        }

        protected void radnumAmount_TextChanged(object sender, EventArgs e)
        {
            clsSys sys = new clsSys();
            radnuTotalVND.Value = radnumAmount.Value * sys.cToDuoble(comboCurrence1.RateText);
            radnuTotalVND.Focus();
        }

        protected void btExpand_Click(object sender, EventArgs e)
        {
            dvSum.Visible = !dvSum.Visible;
            if (dvSum.Visible)
            {
                btExpand.Text = "Collapse";
            }
            else
            {
                btExpand.Text = "Expand";
            }
        }
}
}