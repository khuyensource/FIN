using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
using Telerik.Web.UI;

namespace MaricoPay.uc
{
    public partial class UC_IP_NCCInput : System.Web.UI.UserControl
    {
        Cclass cls = new Cclass();
        clsObj Obj = new clsObj();
        clsSql Sql = new clsSql();
        clsSys sys = new clsSys();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public bool fBool(object value)
        {
            if (value.ToString() == "" || value.ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        void loaddatadaluu(object idpr, object  idprDetail )
        {
            DataTable tbl = cls.GetDataTable("sp_IP_Load_IPPRVendor_NCCSaved", new string[] { "@idpr", "@idprDetail","@linkcode" }, new object[] { idpr, idprDetail,HfLinkcode.Value });
            if (tbl.Rows.Count > 0)
            {
                Rdnumdongia.Text = tbl.Rows[0]["dongia"].ToString();
                RadNumericTextBox1.Text = tbl.Rows[0]["ThanhTien"].ToString();
                rddatethoigianlammau.SelectedDate = DateTime.Parse(tbl.Rows[0]["NgayLamMau"].ToString());
                rddatethoigiansanxuat.SelectedDate = DateTime.Parse(tbl.Rows[0]["NgaySX"].ToString() );
                rdngaygiaohang.SelectedDate = DateTime.Parse(tbl.Rows[0]["NgayGiao"].ToString()); 
                txtGhichu.Text = tbl.Rows[0]["GhiChuVendor"].ToString();
                RadNumericTextBox4.Text = tbl.Rows[0]["HanThanhToan"].ToString();
              //  Rdnumdongia.Value = tbl.Rows[0]["dongia"].ToString();

            }

        }
        public string fGet(object IDPRDetail, object IDPR, object IDMaterial, object soluong, object linkcode, object DaNhanBaoGia)
        {
            HFIDPR.Value = IDPR.ToString();
            HFIDPRDetail.Value = IDPRDetail.ToString();
            HfIDMaterial.Value = IDMaterial.ToString();
            hfSoluong.Value = soluong.ToString();
        
            HfLinkcode.Value = linkcode.ToString();
            HfDaNhanBaoGia.Value = DaNhanBaoGia.ToString();
            loaddatadaluu(HFIDPR.Value, HFIDPRDetail.Value);
            return "";
        }
        

        protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                if (HfDaNhanBaoGia.Value.ToString() == "1" || cls.cToBool(HfDaNhanBaoGia.Value) )
                {
                   
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Báo giá đã được gởi đi không chỉnh sửa được');", true);
                    return;
                }
                else
                {
                    // string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });

                    //  Label1.Text = computer_name[0];
                    if (CheckBox1.Checked)
                    {
                        Obj = new clsObj();
                        Obj.Parameter = new string[] { "@NguoiGuiBaoGia", "@dongia", "@ThanhTien", "@NgayLamMau", "@NgaySX","@NgayGiao","@HanThanhToan",
                     "@HinhMau","@GhiChuVendor","@idpr","@idprDetail","@linkcode" };
                        Obj.ValueParameter = new object[] {"", Rdnumdongia.Value, RadNumericTextBox1.Value, rddatethoigianlammau.SelectedDate.Value,rddatethoigiansanxuat.SelectedDate.Value,
                rdngaygiaohang.SelectedDate.Value, RadNumericTextBox4.Value,radupload(RadAsyncUpload1,HFIDPR.Value),txtGhichu.Text,HFIDPR.Value,HFIDPRDetail.Value, HfLinkcode.Value };
                        Obj.SpName = "sp_IP_Update_NCCBaogia_All";
                        // DataTable tableName = new DataTable();
                        Sql.fNonGetData(Obj);

                        // Label1.Text = Label1.Text + " tui dang o day de  chay cai store nayf : sp_IP_Update_NCCBaogia_All";
                    }
                    else
                    {
                        Obj = new clsObj();
                        Obj.Parameter = new string[] { "@NguoiGuiBaoGia", "@dongia", "@ThanhTien", "@NgayLamMau", "@NgaySX","@NgayGiao","@HanThanhToan",
              "@HinhMau","@GhiChuVendor","@idpr","@idprDetail","@linkcode" };
                        Obj.ValueParameter = new object[] { "", Rdnumdongia.Value, RadNumericTextBox1.Value, rddatethoigianlammau.SelectedDate.Value,rddatethoigiansanxuat.SelectedDate.Value,
                rdngaygiaohang.SelectedDate.Value, RadNumericTextBox4.Value,radupload(RadAsyncUpload1,HFIDPR.Value),txtGhichu.Text,HFIDPR.Value,HFIDPRDetail.Value,
                     HfLinkcode.Value };
                        Obj.SpName = "sp_IP_Update_NCCBaogia";
                        // DataTable tableName = new DataTable();
                        Sql.fNonGetData(Obj);

                        //  Label1.Text = Label1.Text + " tui dang o day de chay chay cai store nayf : sp_IP_Update_NCCBaogia";
                    }
                }
            }
            catch (  Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), ex.Message, true);
            }
            finally
            {
                if (HfDaNhanBaoGia.Value.ToString() == "0" || !cls.cToBool(HfDaNhanBaoGia.Value))
                {
               
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Đã lưu thành công/Saved successfully');", true);
                }
            }

        }

        private string radupload(RadAsyncUpload up, object docno)
        {
            string kq = "";
            // if(   RadUpload1.UploadedFiles.Count>0)
            if (up.UploadedFiles.Count > 0)
            {
                try
                {

                    //int vt1 = up.FileName.LastIndexOf(".");
                    //  int vtcanlay = vt1;
                    //  int len = up..FileName.Length;
                    string extention = up.UploadedFiles[0].GetExtension();
                    string filename = "";
                    filename = cls.cToString(docno).Replace('/', '-');
                    Random getrandom = new Random();
                    filename = filename +  getrandom.Next().ToString() + extention;
                    //HinhBia.SaveAs(Server.MapPath("../" + ConfigurationManager.AppSettings["hinhbia_sanpham"].ToString()).ToString() + ANHBIA);
                    string sFolderPath = Server.MapPath("Upload/IP/" + filename);
                    //  string sFullPath = sFolderPath + filename;
                    if (System.IO.File.Exists(sFolderPath) == true)
                        System.IO.File.Delete(sFolderPath);
                    //resize
                    //  HttpPostedFile pf = FileUpload1.PostedFile;
                    // up.SaveAs(sFolderPath);
                    // kq = filename;
                    try
                    {
                        // up.SaveAs(sFolderPath);

                        up.UploadedFiles[0].SaveAs(sFolderPath);
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

        protected void Rdnumdongia_TextChanged(object sender, EventArgs e)
        {
            RadNumericTextBox1.Text = (cls.cToDecimal(hfSoluong.Value) * cls.cToDecimal(Rdnumdongia.Value)).ToString();
        }

    }
}