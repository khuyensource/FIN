using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel.Syndication;
using System.Xml;
namespace MaricoPay.uc
{
    public partial class ucCurr : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string URLString = "https://www.vietcombank.com.vn/exchangerates/ExrateXML.aspx";

                    System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();//xml doc used for xml parsing

                    xdoc.Load(URLString);//loading XML in xml doc

                    //Lấy dữ liệu theo Tag DataTime để lấy ngày giờ cập nhật
                    string ngaycapnhat = "Ngày cập nhật: " + xdoc.GetElementsByTagName("DateTime")[0].InnerText;
                    //  int i = 0; //đếm số node

                    //duyệt danh sách các node Exrate
                    ListItem item1 = new ListItem();
                    item1.Text = "VND-Vietnam dong";
                    item1.Value = "VND";
                    dropCurr.Items.Add(item1);
                    foreach (XmlNode node in xdoc.GetElementsByTagName("Exrate"))
                    {
                        ListItem item = new ListItem();
                        item.Text = node.Attributes[0].Value + "-" + node.Attributes[1].Value;
                        item.Value = node.Attributes[0].Value;
                        dropCurr.Items.Add(item);
                        //var tigiabuy = node.Attributes[2].Value;
                        // var tigiastran = node.Attributes[3].Value;
                        // var tigiasell = node.Attributes[4].Value;
                    }
                   
                  //  dropCurr.SelectedIndex = dropCurr.Items.Count - 1;
                    dropCurr.SelectedValue="VND";
                    //string[] kq = dropCurr.SelectedValue.Split('|');
                    //txtTiGia.Text = kq[1].ToString();
                }
                catch {
                    Cclass cls = new Cclass();
                    System.Data.DataTable tbl = cls.GetDataTable("sp_getCurrency1");
                    dropCurr.DataSource = tbl;
                    dropCurr.DataTextField = "Text";
                    dropCurr.DataValueField = "Value";
                    dropCurr.DataBind();
                    dropCurr.SelectedValue = "VND";
                
                }
            }
        }
        /// <summary>
        /// get, set text Currence (Ma tien te)
        /// </summary>
        public string CurrText
        {

            get
            {
                return dropCurr.SelectedItem.Text.Substring(0,3);
            }
            set
            {
             //   dropCurr.SelectedItem.Text = value;
                dropCurr.SelectedIndex = dropCurr.Items.IndexOf(dropCurr.Items.FindByText(value));
            }
        }
        /// <summary>
        /// get,set text full Currence (Ma tien te - ten tien te)
        /// </summary>
        public string CurrTextFull
        {

            get
            {
                return dropCurr.SelectedItem.Text;
            }
            set
            {
                //dropCurr.SelectedItem.Text = value;
                //dropCurr.SelectedItem.Value = value;
                // dropCurr.Items.Contains(new ListItem(value))
                dropCurr.SelectedIndex = dropCurr.Items.IndexOf(dropCurr.Items.FindByText(value));
            }
        }
        /// <summary>
        /// get, set values currence (Matiente|tigiaban)
        /// </summary>
        public string CurrValues
        {
            get
            {
                return dropCurr.SelectedValue;
            }
            set
            {
                dropCurr.SelectedValue = value;
            }
        }
        
        /// <summary>
        /// Set visible
        /// </summary>
        public override bool Visible
        {

           set
            {
                dropCurr.Visible = value;
               

            }
        }
       
    }
}