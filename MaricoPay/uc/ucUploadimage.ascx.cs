using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
namespace MaricoPay.uc
{
    public partial class ucUploadimage : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdPathSave.Value = "";
            }
        }
        /// <summary>
        /// Get, Set duong dan file da upload
        /// </summary>
        public string PathFile
        {

            get
            {
                return hdPathSave.Value;
            }
            set
            {
                hdPathSave.Value = value;
            }
        }
        /// <summary>
        /// Get, Set file name uploaded
        /// </summary>
        public string FileName
        {

            get
            {
                return hdFileName.Value;
            }
            set
            {
                hdFileName.Value = value;
            }
        }
       /// <summary>
       /// filename: ko co phan mo rong
       /// UPload file
       /// </summary>
       /// <param name="path"></param>
       /// <param name="filename"></param>
        public void uploadimg(string filename)
        {
            hdPathSave.Value = "";
            hdFileName.Value = "";
            if (FileUpload1.HasFile)
            {
                clsControl ctr = new clsControl();
                try
                {
                    // ConfigurationManager.AppSettings["avatarucbihied"].ToString()
                    //string sFolderPath = Server.MapPath(ConfigurationManager.AppSettings["avatar"].ToString());
                    int vt1 = FileUpload1.FileName.LastIndexOf(".");
                    int vtcanlay = vt1;
                    int len = FileUpload1.FileName.Length;
                    string extention = FileUpload1.FileName.Substring(vtcanlay, len - vtcanlay);
                    filename = filename+extention;
                    //HinhBia.SaveAs(Server.MapPath("../" + ConfigurationManager.AppSettings["hinhbia_sanpham"].ToString()).ToString() + ANHBIA);
                    string sFolderPath = Server.MapPath("ImagesUpload/" + filename);
                    if (System.IO.File.Exists(sFolderPath /*+ "/" + filesave*/) == true)
                        System.IO.File.Delete(sFolderPath);
                    //resize
                    HttpPostedFile pf = FileUpload1.PostedFile;

                    System.Drawing.Image bm = System.Drawing.Image.FromStream(pf.InputStream);
                    bm = ctr.ResizeBitmap((Bitmap)bm, 800, 600); /// new width, height
                    bm.Save(sFolderPath);
                    hdPathSave.Value = sFolderPath;
                    hdFileName.Value = filename;
                    //Obj = new clsObj();
                    //Obj.Parameter = new string[] { "@email", "@filename" };
                    //Obj.ValueParameter = new object[] { email, filesave };
                    //Obj.SpName = "sp_updateAvatar";
                    //Sql.fNonGetData(Obj);


                }
                catch (Exception ex)
                {
                    hdPathSave.Value = "";
                    Response.Write(ex.Message);
                }
            }
           
        }
    }
}