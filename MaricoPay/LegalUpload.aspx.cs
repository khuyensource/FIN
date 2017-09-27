using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
namespace MaricoPay
{
    public partial class LegalUpload : clsPhanQuyenCaoCap
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.GetPostBackEventReference(this, string.Empty);
            if (this.IsPostBack)
            {
                string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];
                if (eventTarget == "PopUpLoadLegal")
                {
                    LoadContract(radioType.SelectedValue);

                }

            }
            else
            //if (!IsPostBack)
            {
                if (Session["username"] != null /*&& Request.Form["us"] != null*/)
                {
                    LoadContract(0);
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        public bool isShow(object loaicv)
        {
            bool kq = false;
            switch (loaicv.ToString())
            {
                case "Yes":
                    kq = true;
                    break;
                case "No":
                    kq = false;
                    break;
            }
            return kq;
        }
        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            // RadGrid1.Rebind();
            GridEditableItem editedItem;
           // string status = "";
          //  DateTime? pkr;
          //  string content = "";
            switch (e.CommandName)
            {
                  //case "Upload":
                  //  editedItem = (GridEditableItem)e.Item;
                  //  //  string docno1 = editedItem["ContractNo"].Text;
                  //  System.Text.StringBuilder sb = new System.Text.StringBuilder();
                  // // Session["docno"] = editedItem["ContractNo"].Text;
                  //  //Session["EditUpload"] = 0;
                  //  sb.Append("window.open('PopUpLoadLegal.aspx?docno=" + editedItem["ContractNo"].Text + "&EditUpload=0','PrintMe', 'height=300px,width=600px,scrollbars=1');");
                  //  ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "NewClientScript", sb.ToString(), true);
                  //  break;
                  case "Upload":
                    editedItem = (GridEditableItem)e.Item;
                    //  string docno1 = editedItem["ContractNo"].Text;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //Session["EditUpload"] = 1;
                    sb.Append("window.open('PopUpLoadLegal.aspx?docno=" + editedItem["ContractNo"].Text + "&contractno=" + editedItem["ContractNoLegal"].Text + "&date=" + editedItem["StampDate"].Text + "&filenameDoc=" + editedItem["FileDocLegalName"].Text + "&filenamepdf=" + editedItem["FileScanLegalName"].Text + "','PrintMe', 'height=300px,width=600px,scrollbars=1');");
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "NewClientScript", sb.ToString(), true);
                    break;
                
            }
        }
        private void LoadContract(object loai)
        {
            Cclass cls = new Cclass();
            DataTable tbl = cls.GetDataTable("sp_getContralLegalUpload", new string[] { "@loai", "@username" }, new object[] { loai, Session["username"] });
            RadGrid1.DataSource = tbl;
            RadGrid1.DataBind();
        }
        protected void radioType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadContract(radioType.SelectedValue);
        }
    }
}