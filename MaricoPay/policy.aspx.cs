using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using Telerik.Web.UI;
namespace MaricoPay
{
    public partial class policy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                  string folder = !string.IsNullOrEmpty(Request.QueryString["type"]) ? Request.QueryString["type"] : "";
                  if (folder == "")
                  {
                      Response.Redirect("~/Login.aspx");
                      return;
                  }
                lbTitle.Text = folder;
                    string[] filePaths = Directory.GetFiles(Server.MapPath("~/Policy/" + folder));
                List<ListItem> files = new List<ListItem>();
                foreach (string filePath in filePaths)
                {
                    string filename = Path.GetFileName(filePath);
                  //  string disname
                    int vt = filename.LastIndexOf(".");
                    string disname=filename;
                    if(vt>0)
                    {
                        disname = filename.Substring(0, vt);
                   }

                    string fileurl = "~/Policy/"+ folder + "/" + filename;// filePath;// @"http://" + Request.Url.Authority + "/Policy/" + folder + "/" + filename;
                    files.Add(new ListItem(disname, fileurl));
                }
                RadGrid1.DataSource = files;
                RadGrid1.DataBind();
               
            }
        }
        public void viewebook(string pdf_filepath)
        {
            string FilePath = pdf_filepath;//Server.MapPath(pdf_filepath);
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(FilePath);
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
                Response.Write("<script>window.open(\"policy.aspx\",\"myframe\");</script>");

            }

        }
        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = (GridEditableItem)e.Item;

            switch (e.CommandName)
            {
                case "Open":
                    string fileurl = editedItem["Value"].Text;
                    myframe.Attributes["src"] ="pdfshow.ashx?pdf="+ fileurl;
                    //<iframe src="MapHandler.ashx?pdf=text.pdf" width="200" height="200"></iframe>
                   // viewebook(fileurl);
                    break;
            }
        }
    }
}