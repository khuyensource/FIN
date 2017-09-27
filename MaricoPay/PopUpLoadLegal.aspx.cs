using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaricoPay
{
    public partial class PopUpLoadLegal : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
          //  Session["docno"] ;
            if (!IsPostBack)
            {
                //EditUpload
              
                //string EditUpload = !string.IsNullOrEmpty(Request.QueryString["EditUpload"]) ? Request.QueryString["EditUpload"] : "0";
                //if (cls.cToInt(EditUpload) == 0)//tao moi
                //{

                //    txtID.Text = !string.IsNullOrEmpty(Request.QueryString["docno"]) ? Request.QueryString["docno"] : "";
                //    radDateContract.SelectedDate = DateTime.Now;
                //}
                //else//sua
                //{
                    string docno = !string.IsNullOrEmpty(Request.QueryString["docno"]) ? Request.QueryString["docno"] : "";
                    string contractno = !string.IsNullOrEmpty(Request.QueryString["contractno"]) ? Request.QueryString["contractno"] : "";
                    string date = !string.IsNullOrEmpty(Request.QueryString["date"]) ? Request.QueryString["date"] : Guid.Empty.ToString();
                    Session["filenameDoc"] = !string.IsNullOrEmpty(Request.QueryString["filenameDoc"]) ? Request.QueryString["filenameDoc"] :"";
                    Session["filenamepdf"] = !string.IsNullOrEmpty(Request.QueryString["filenamepdf"]) ? Request.QueryString["filenamepdf"] : "";
                    txtID.Text = docno;
                    if (contractno == "")//them
                    {
                        radDateContract.SelectedDate = DateTime.Now;
                    }
                    else//sua
                    {
                        txtcontractnumber.Text = contractno;
                        radDateContract.SelectedDate = cls.cToDateTime(date);
                    }
                //}
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Please select contract", uc.ucMsgBox.enmMessageType.Error);
                Response.Write("<script language=javascript>window.close();</script>");
                return;
            }
            if (txtcontractnumber.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Please fillin Contract number", uc.ucMsgBox.enmMessageType.Error);
                txtcontractnumber.Focus();
                return;
            }
            string filenameDoc=cls.cToString(Session["filenameDoc"]);
            string filenamepdf = cls.cToString(Session["filenamepdf"]);
            if (filenameDoc != "")
            {
                string sFolderPathDocDelete1 = Server.MapPath("Upload/CO/Legal/" + filenameDoc);
                if (System.IO.File.Exists(sFolderPathDocDelete1) == true)
                    System.IO.File.Delete(sFolderPathDocDelete1);
            }
            if (filenamepdf != "")
            {
                string sFolderPathDocDelete2 = Server.MapPath("Upload/CO/Legal/" + filenamepdf);
                if (System.IO.File.Exists(sFolderPathDocDelete2) == true)
                    System.IO.File.Delete(sFolderPathDocDelete2);
            }
            string docfile = "";
            //if (fileUpDoc.HasFile == false)
            //{
            //    MsgBox1.AddMessage("Please select doc file to upload", uc.ucMsgBox.enmMessageType.Error);
            //    return;
            //}
            //else
                if (fileUpDoc.HasFile == true)
            {
                int vt1 = fileUpDoc.FileName.LastIndexOf(".");
                int vtcanlay = vt1;
                int len = fileUpDoc.FileName.Length;
                string extention = fileUpDoc.FileName.Substring(vtcanlay, len - vtcanlay);
                if (extention.ToLower() != ".doc" || extention.ToLower() != ".docx")
                {
                    MsgBox1.AddMessage("Please select doc or docx file only", uc.ucMsgBox.enmMessageType.Error);
                    return;
                }
                else
                {
                    docfile = upload(fileUpDoc, txtcontractnumber.Text.Trim());
                }
            }
            if (fileUpPdf.HasFile == false)
            {
                MsgBox1.AddMessage("Please select pdf file to upload", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            else
            {
                int vt1 = fileUpPdf.FileName.LastIndexOf(".");
                int vtcanlay = vt1;
                int len = fileUpPdf.FileName.Length;
                string extention = fileUpPdf.FileName.Substring(vtcanlay, len - vtcanlay);
                if (extention.ToLower() != ".pdf")
                {
                    MsgBox1.AddMessage("Please select pdf file only", uc.ucMsgBox.enmMessageType.Error);
                    return;
                }
            }
            //string docfile = upload(fileUpDoc, txtcontractnumber.Text.Trim());
            string pdffile = upload(fileUpPdf, txtcontractnumber.Text.Trim());
            if (/*docfile != "" &&*/ pdffile != "")
            {
               // Cclass cls = new Cclass();
                cls.bCapNhat(new string[] { "@Docno", "@ContractNoLegal", "@fileDocLegal", "@FileScanLegal", "@LegalUploadUser", "@StampDate" }, new object[] { txtID.Text, txtcontractnumber.Text.Trim(), docfile, pdffile, Session["username"], radDateContract.SelectedDate.Value }, "sp_updateLegalUpload");
                Response.Write("<script language=javascript> window.opener.__doPostBack('PopUpLoadLegal', '');window.close();</script>");
            }
        }
        private string upload(FileUpload up, string docno)
        {
            string kq = "";
            // if(   RadUpload1.UploadedFiles.Count>0)
            if (up.HasFile)
            {
                try
                {
                    Cclass cls = new Cclass();
                    int vt1 = up.FileName.LastIndexOf(".");
                    int vtcanlay = vt1;
                    int len = up.FileName.Length;
                    string extention = up.FileName.Substring(vtcanlay, len - vtcanlay);
                    string filename = "";
                    filename = docno.Replace('/', '-');
                    filename = filename + '-' + cls.cToString(Session["username"]) + extention;
                    //HinhBia.SaveAs(Server.MapPath("../" + ConfigurationManager.AppSettings["hinhbia_sanpham"].ToString()).ToString() + ANHBIA);
                    string sFolderPath = Server.MapPath("Upload/CO/Legal/" + filename);
                    if (System.IO.File.Exists(sFolderPath) == true)
                        System.IO.File.Delete(sFolderPath);
                    //resize
                    //  HttpPostedFile pf = FileUpload1.PostedFile;
                    // up.SaveAs(sFolderPath);
                    // kq = filename;
                    try
                    {
                        up.SaveAs(sFolderPath);
                        kq = filename;
                    }
                    catch
                    {
                        kq = "";

                    }

                }
                catch
                {
                    kq = "";
                }
            }
            else
            {
                kq = "";
            }
            return kq;
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>window.close();</script>");
        }

        protected void btremoveDoc_Click(object sender, EventArgs e)
        {
            btremoveDoc.Attributes.Add("onclick", "return false;");
            fileUpDoc.Attributes.Clear();
            
        }

        
    }
}