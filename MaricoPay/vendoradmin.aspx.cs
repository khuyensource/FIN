using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace MaricoPay
{
    public partial class vendoradmin : clsPhanQuyenCaoCap
    {
        Cclass cls = new Cclass();
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btsave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Please fill in Name",uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (txtAdd.Text.Trim()=="")
            {
                MsgBox1.AddMessage("Please fill in Address", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
            if (txtTax.Text.Trim() == "")
            {
                MsgBox1.AddMessage("Please fill in Taxcode", uc.ucMsgBox.enmMessageType.Error);
                return;
            }
           string vendorname= cls.GetString("insert_vendor", new string[] { "@VendorName", "@Active", "@Type", "@Address", "@TaxCode", "@BankAcc", "@BankNum", "@BankName", "@BankCity", "@Tel","@PurOrg","@VendorCode" }
                , new object[] { txtName.Text.Trim(), 1, "SP", txtAdd.Text.Trim(), txtTax.Text.Trim(), txtBanAcc.Text.Trim(), txtBankNo.Text.Trim(), txtBankName.Text.Trim(), txtBankcity.Text.Trim(), txtTel.Text.Trim(), txtPurOrg.Text.Trim(),txtCode.Text.Trim()});
           if (vendorname != "")
           {
               MsgBox1.AddMessage("Cap nhat thanh cong", uc.ucMsgBox.enmMessageType.Success);
           }
           else
           {
               MsgBox1.AddMessage("Cap nhat that bai", uc.ucMsgBox.enmMessageType.Error);
           }

        }

      

        protected void btFind_Click(object sender, EventArgs e)
        {
            string find = "";
            if (txtCode.Text.Trim() != "")
            {
                find = txtCode.Text.Trim();
            }
            else
            {
                find = txtName.Text.Trim();
            }
            DataTable tbl = cls.GetDataTable("sp_findVendors", "@find", find);
            if (tbl.Rows.Count == 1)
            {
                txtCode.Text = cls.cToString(tbl.Rows[0]["VendorCode"]);
                txtName.Text = cls.cToString(tbl.Rows[0]["VendorName"]);
                txtAdd.Text = cls.cToString(tbl.Rows[0]["Address"]);
                txtTel.Text = cls.cToString(tbl.Rows[0]["Tel"]);
                txtTax.Text = cls.cToString(tbl.Rows[0]["TaxCode"]);
                txtBankNo.Text = cls.cToString(tbl.Rows[0]["BankNum"]);
                txtBanAcc.Text = cls.cToString(tbl.Rows[0]["BankAcc"]);
                txtBankName.Text = cls.cToString(tbl.Rows[0]["BankName"]);
                txtBankcity.Text = cls.cToString(tbl.Rows[0]["BankCity"]);
                txtPurOrg.Text = cls.cToString(tbl.Rows[0]["PurOrg"]);
            }
            else
            {
                RadGrid1.DataSource = tbl;
                RadGrid1.DataBind();
            }
        }
    }
}