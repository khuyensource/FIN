using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Data;
namespace MaricoPay
{
    /// <summary>
    /// Summary description for pdfshow
    /// </summary>
    public class pdfshow : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            CreateImage(context);
        }
        private void CreateImage(HttpContext context)
        {
            if (context.Request.QueryString["pdf"] != null)
            {
                string strPdf = context.Request.QueryString["pdf"].ToString();

                try
                {
                    Cclass cls = new Cclass();
                    string ext = cls.getExtention(strPdf);
                    switch (ext.ToLower())
                    {
                        case ".pdf":
                            FileStream fs = null;
                            BinaryReader br = null;
                            byte[] data = null;
                            fs = new FileStream(context.Server.MapPath(strPdf), FileMode.Open, FileAccess.Read, FileShare.Read);
                            br = new BinaryReader(fs, System.Text.Encoding.Default);
                            data = new byte[Convert.ToInt32(fs.Length)];
                            br.Read(data, 0, data.Length);
                            context.Response.Clear();
                            context.Response.ContentType = "application/pdf";
                            context.Response.BinaryWrite(data);
                            context.Response.End();
                            context.Response.BinaryWrite(data);
                            context.Response.End();
                            fs.Close();
                            fs.Dispose();
                            br.Close();
                            data = null;
                            break;
                        case ".doc":
                            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                            response.ClearContent();
                            response.Clear();
                            response.ContentType = "text/plain";
                            response.AddHeader("Content-Disposition", "attachment; filename=" + strPdf + ";");
                            response.TransmitFile(context.Server.MapPath(strPdf));
                            response.Flush();
                            response.End();
                            break;
                        case ".docx":
                            System.Web.HttpResponse response1 = System.Web.HttpContext.Current.Response;
                            response1.ClearContent();
                            response1.Clear();
                            response1.ContentType = "text/plain";
                            response1.AddHeader("Content-Disposition", "attachment; filename=" + strPdf + ";");
                            response1.TransmitFile(context.Server.MapPath(strPdf));
                            response1.Flush();
                            response1.End();
                            break;
                        default:
                             System.Web.HttpResponse response2 = System.Web.HttpContext.Current.Response;
                            response2.ClearContent();
                            response2.Clear();
                           // context.Response.ContentType = "application/force-download";
                            response2.TransmitFile(context.Server.MapPath(strPdf));
                            response2.Flush();
                            response2.End();
                            break;
                    }

                }
                catch (Exception ex)
                {
                    context.Response.Write(ex.Message);
                }
                //finally
                //{
                //    fs.Close();
                //    fs.Dispose();
                //    br.Close();
                //    data = null;
                //}
            }
            else
            {
                context.Response.Write("Wrong path");

            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}