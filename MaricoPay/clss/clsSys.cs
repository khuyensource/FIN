using System;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Net;
//using Data;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MaricoPay.DB;
public class clsSys
{
    Cclass cls = new Cclass();
    public string mahoaS(string input)
    {
        MD5 md5Hasher = MD5.Create();
        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
        StringBuilder sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x0"));
        }
        return sBuilder.ToString();
    }
    /// <summary>
    /// Get so luong wow đã nhận
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
   
    private string BytesToString(byte[] Bytes)
    {
        MemoryStream MS = new MemoryStream(Bytes);
        StreamReader SR = new StreamReader(MS);
        string S = SR.ReadToEnd();
        SR.Close();
        return S;
    }

    public string ToUnicode(string S)
    {
        return BytesToString(new UnicodeEncoding().GetBytes(S));
    }
    public bool CheckEmailLogin(string email, string pass)
    {

        bool kq = false;//
        try
        {
            string from = email;// ConfigurationManager.AppSettings["fromemail"].ToString();
            string usemail = email;// ConfigurationManager.AppSettings["useremail"].ToString();
            string smtpserver = ConfigurationManager.AppSettings["smtp"].ToString();
            string smtpport = ConfigurationManager.AppSettings["smtpport"].ToString();
            string ssl = ConfigurationManager.AppSettings["ssl"].ToString();
           // string pass = ConfigurationManager.AppSettings["pass"].ToString();
            //SmtpClient smtpclient = new SmtpClient();
            //System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
            // MailAddress mailAddress = new MailAddress(radcombowho.SelectedValue, radcombowho.Text, System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress("notification@icpvn.com");//, System.Text.Encoding.Unicode
            MailAddress afrom = new MailAddress(from, "Fin system");//, System.Text.Encoding.Unicode
            MailMessage message = new MailMessage();
            message.From = afrom;
            message.To.Add(to);
            message.IsBodyHtml = true;
            message.Subject = "check login";
            message.Body = "Login fin system";

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(usemail, pass);
            client.Port = cls.cToInt(smtpport); // You can use Port 25 if 587 is blocked (mine is!)
            client.Host = smtpserver;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = bool.Parse(ssl);
          //  client.Timeout = 2000000;

            // client.Timeout = 0;

            client.Send(message);
            kq = true;
        }
        catch
        {
            kq = false;
        }
        return kq;
    }
    public bool SendMailASP(string sto,string sub, string content)
    {
        
        bool kq = false;//
        try
        {
        string from = ConfigurationManager.AppSettings["fromemail"].ToString();
        string usemail = ConfigurationManager.AppSettings["useremail"].ToString();
        string smtpserver = ConfigurationManager.AppSettings["smtp"].ToString();
        string smtpport = ConfigurationManager.AppSettings["smtpport"].ToString();
        string ssl = ConfigurationManager.AppSettings["ssl"].ToString();
        string pass = ConfigurationManager.AppSettings["pass"].ToString();
        //SmtpClient smtpclient = new SmtpClient();
        //System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
        // MailAddress mailAddress = new MailAddress(radcombowho.SelectedValue, radcombowho.Text, System.Text.Encoding.UTF8);
        MailAddress to = new MailAddress(sto);//, System.Text.Encoding.Unicode
        MailAddress afrom = new MailAddress(from, "Fin system");//, System.Text.Encoding.Unicode
        MailMessage message = new MailMessage();
        MailAddress bcc = new MailAddress(from);
        message.From = afrom;
        message.To.Add(to);
        message.Bcc.Add(bcc);
        message.IsBodyHtml = true;
       // message.BodyEncoding = System.Text.Encoding.UTF8;
        message.Subject = sub;
        message.Body = content;


        SmtpClient client = new SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential(usemail, pass);
        client.Port = cls.cToInt(smtpport); // You can use Port 25 if 587 is blocked (mine is!)
        client.Host = smtpserver;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = bool.Parse(ssl);

       // SmtpClient client = new SmtpClient(smtpserver, Convert.ToInt16(smtpport));
       // // client.UseDefaultCredentials = true;
       // client.Credentials = new System.Net.NetworkCredential(usemail, pass);
       //// System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpUName, smtpUNamePwd);
          //  // client.Credentials = credentials;
          //  client.UseDefaultCredentials = false;  //<-- Set This Line After Credentials
        //client.EnableSsl = bool.Parse(ssl);
        //client.Timeout = 50000;

        //// client.Timeout = 0;
        
            client.Send(message);
            kq = true;
        }
        catch
        {
            kq = false;
        }
        return kq;
    }
    public bool SendMailASPAtt(string sto, string sub, string content, string PathFileAttached)
    {

        bool kq = false;//
        try
        {
            string from = ConfigurationManager.AppSettings["fromemail"].ToString();
            string usemail = ConfigurationManager.AppSettings["useremail"].ToString();
            string smtpserver = ConfigurationManager.AppSettings["smtp"].ToString();
            string smtpport = ConfigurationManager.AppSettings["smtpport"].ToString();
            string ssl = ConfigurationManager.AppSettings["ssl"].ToString();
            string pass = ConfigurationManager.AppSettings["pass"].ToString();
            //SmtpClient smtpclient = new SmtpClient();
            //System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
            // MailAddress mailAddress = new MailAddress(radcombowho.SelectedValue, radcombowho.Text, System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress(sto);//, System.Text.Encoding.Unicode
            MailAddress afrom = new MailAddress(from, "Fin System");//, System.Text.Encoding.Unicode
            MailMessage message = new MailMessage(afrom, to);
            MailAddress bcc = new MailAddress(from);
            message.To.Add(bcc);
            message.Bcc.Add(bcc);
            message.From = afrom;
            //if (PathFileAttached != "")
            //{
            //   int vt= PathFileAttached.LastIndexOf("\\");
            //    string filename=PathFileAttached;
            //    if (vt >= 0)
            //    {
            //        filename = PathFileAttached.Substring(vt);
            //    }
            //    System.IO.FileStream fStream;
            //    fStream = System.IO.File.OpenRead(PathFileAttached);
            //    Attachment at = new Attachment(fStream, filename);
            //    message.Attachments.Add(at);
            //}
            message.IsBodyHtml = true;
            // message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = sub;
            message.Body = content;

            //SmtpClient client = new SmtpClient(smtpserver, Convert.ToInt16(smtpport));
            //// client.UseDefaultCredentials = true;
            //client.Credentials = new System.Net.NetworkCredential(usemail, pass);
            //client.EnableSsl = bool.Parse(ssl);
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(usemail, pass);
            client.Port = cls.cToInt(smtpport); // You can use Port 25 if 587 is blocked (mine is!)
            client.Host = smtpserver;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = bool.Parse(ssl);
          //  client.Timeout = 2000000;

          

            client.Send(message);
            kq = true;
        }
        catch
        {
            kq = false;
        }
        return kq;
    }
    public bool SendMailASP(string sto, string cc, string sub, string content)
    {
        bool kq = false;//
        try
        {
            string from = ConfigurationManager.AppSettings["fromemail"].ToString();
            string usemail = ConfigurationManager.AppSettings["useremail"].ToString();
            string smtpserver = ConfigurationManager.AppSettings["smtp"].ToString();
            string smtpport = ConfigurationManager.AppSettings["smtpport"].ToString();
            string ssl = ConfigurationManager.AppSettings["ssl"].ToString();
            string pass = ConfigurationManager.AppSettings["pass"].ToString();
            //SmtpClient smtpclient = new SmtpClient();
            //System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
            // MailAddress mailAddress = new MailAddress(radcombowho.SelectedValue, radcombowho.Text, System.Text.Encoding.UTF8);
       //     MailAddress to = new MailAddress(sto, sto);//, System.Text.Encoding.Unicode
            MailAddress afrom = new MailAddress(from, "Fin System");//, System.Text.Encoding.Unicode
           // MailMessage message = new MailMessage(afrom, to);
            MailMessage message = new MailMessage();
            // System.Net.Mail.MailAddress
            message.IsBodyHtml = true;
            //   message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = sub;
            message.Body = content;
            message.From = afrom;
            string ch = ";";
            int vt = cc.IndexOf(",");
            if (vt >= 0)
                ch = ",";

            //add cc nhieu nguoi
            foreach (var curr_address in cc.Split(new[] { ch }, StringSplitOptions.RemoveEmptyEntries))
            {
                MailAddress copy = new MailAddress(curr_address);
                // TO_addressList.Add(mytoAddress);
                message.CC.Add(copy);
            }
            //add to nhieu nguoi
            ch = ";";
            vt = sto.IndexOf(",");
            if (vt >= 0)
                ch = ",";
            foreach (var address in sto.Split(new[] { ch }, StringSplitOptions.RemoveEmptyEntries))
            {
                MailAddress to = new MailAddress(address);
                message.To.Add(to);
            }
            MailAddress bcc = new MailAddress(from);
            message.Bcc.Add(bcc);
            //MailAddress copy = new MailAddress(cc);
            //message.CC.Add(copy);
            message.SubjectEncoding = System.Text.Encoding.Default;
           
            //SmtpClient client = new SmtpClient(smtpserver, Convert.ToInt16(smtpport));
            //// client.UseDefaultCredentials = true;
            //client.Credentials = new System.Net.NetworkCredential(usemail, pass);

            //// client.UseDefaultCredentials = true;
            //// client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            //client.EnableSsl = bool.Parse(ssl);
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(usemail, pass);
            client.Port = cls.cToInt(smtpport); // You can use Port 25 if 587 is blocked (mine is!)
            client.Host = smtpserver;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = bool.Parse(ssl);
          //  client.Timeout = 2000000;
            client.Send(message);
            kq = true;
        }
        catch
        {
            kq = false;
        }
        return kq;
    }
    /// <summary>
    /// Gưi email cho nhieu nguoi nhan va nhieu nguoi cc dinh kem file
    /// </summary>
    /// <param name="sto"></param>
    /// <param name="cc"></param>
    /// <param name="sub"></param>
    /// <param name="content"></param>
    /// <param name="PathFileAttached"></param>
    /// <returns></returns>
    public bool SendMailASPAtt(string sto, string cc, string sub, string content, string PathFileAttached)
    {

        bool kq = false;//
        try
        {
            string from = ConfigurationManager.AppSettings["fromemail"].ToString();
            string usemail = ConfigurationManager.AppSettings["useremail"].ToString();
            string smtpserver = ConfigurationManager.AppSettings["smtp"].ToString();
            string smtpport = ConfigurationManager.AppSettings["smtpport"].ToString();
            string ssl = ConfigurationManager.AppSettings["ssl"].ToString();
            string pass = ConfigurationManager.AppSettings["pass"].ToString();
            //SmtpClient smtpclient = new SmtpClient();
            //System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
            // MailAddress mailAddress = new MailAddress(radcombowho.SelectedValue, radcombowho.Text, System.Text.Encoding.UTF8);
          //  MailAddress to = new MailAddress(sto, sto);//, System.Text.Encoding.Unicode
            MailAddress afrom = new MailAddress(from, "Fin System");//, System.Text.Encoding.Unicode
          //  MailMessage message = new MailMessage(afrom, to);
            MailMessage message = new MailMessage();
          //  if (PathFileAttached != "")
          //  {
          //     // FileStream fStream = new FileStream(PathFileAttached, FileMode.Open, FileAccess.Read);
          //     // StreamReader s = new StreamReader(fStream);
               

              
                  
               
          //      int vt1 = PathFileAttached.LastIndexOf("\\");
          //      string filename = PathFileAttached;
          //      if (vt1 >= 0)
          //      {
          //          filename = PathFileAttached.Substring(vt1+1);
          //      }
          //    //  System.IO.FileStream fStream;
          //   //   fStream = System.IO.File.OpenRead(PathFileAttached);
          //   //   fStream = new FileStream(PathFileAttached, FileMode.Open, FileAccess.Read);


          //      ////cach download ve client roi gui email
          //      //var PathToTheFileInTheServer = PathFileAttached;
          //      //var sourceFileInfo = new System.IO.FileInfo(PathToTheFileInTheServer);
          //      string pathtemp = "D:\\" + filename;
          //     // sourceFileInfo.CopyTo(pathtemp);


          ////   Download(pathtemp,PathFileAttached);
          //     // DownLoadFileFromRemoteLocation(PathFileAttached, pathtemp);
          //     // MemoryStream memoryStream = new MemoryStream();
          //   //   var fStream = new System.IO.MemoryStream(ReadByteArryFromFile(PathFileAttached));
          //     // Attachment at = new Attachment(fStream, filename);
          //      //Get some binary data
          //     byte[] data = ReadByteArryFromFile(PathFileAttached);

          //      //save the data to a memory stream
          //   MemoryStream memoryStream = new MemoryStream(data);
          //  //    Attachment at = new Attachment(memoryStream, filename, MediaTypeNames.Application.Octet);
          //      Attachment at = new Attachment(memoryStream, filename);
          //  //  Attachment at = new Attachment(pathtemp);
          //      message.Attachments.Add(at);
          //  }
            message.IsBodyHtml = true;
            // message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = sub;
            message.Body = content;
            message.From = afrom;
            string ch = ";";
            int vt = cc.IndexOf(",");
            if (vt >= 0)
                ch = ",";

            //add cc nhieu nguoi
            foreach (var curr_address in cc.Split(new[] { ch }, StringSplitOptions.RemoveEmptyEntries))
            {
                MailAddress copy = new MailAddress(curr_address);
                // TO_addressList.Add(mytoAddress);
                message.CC.Add(copy);
            }
            //add to nhieu nguoi
             ch = ";";
             vt = sto.IndexOf(",");
            if (vt >= 0)
                ch = ",";
            foreach (var address in sto.Split(new[] { ch }, StringSplitOptions.RemoveEmptyEntries))
            {
                MailAddress to = new MailAddress(address);
                message.To.Add(to);
            }
            MailAddress bcc = new MailAddress(from);
            message.Bcc.Add(bcc);
           // SmtpClient client = new SmtpClient(smtpserver, Convert.ToInt16(smtpport));
           // //// client.UseDefaultCredentials = true;
           //client.Credentials = new System.Net.NetworkCredential(usemail, pass);
           //// client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
           // //// client.Timeout = 0;
           // client.EnableSsl = bool.Parse(ssl);
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(usemail, pass);
            client.Port = cls.cToInt(smtpport); // You can use Port 25 if 587 is blocked (mine is!)
            client.Host = smtpserver;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = bool.Parse(ssl);
          //  client.Timeout = 2000000;
           

            client.Send(message);
            kq = true;
        }
        catch
        {
            kq = false;
        }
        return kq;
    }
    //public void Download(string sFileName, string sFilePath)
    //{
    //    HttpContext.Current.Response.ContentType = "APPLICATION/OCTET-STREAM";
    //    String Header = "Attachment; Filename=" + sFileName;
    //    HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
    //  //  System.IO.FileInfo Dfile = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(sFilePath));
    //    System.IO.FileInfo Dfile = new System.IO.FileInfo(sFilePath);
    //    HttpContext.Current.Response.WriteFile(Dfile.FullName);
    //    HttpContext.Current.Response.End();
    //}
    public void Download(string filesave, string FName)
    {
      
        //Stream stream;
        string path = FName;
        System.IO.FileInfo file = new System.IO.FileInfo(path);
        if (file.Exists)
        {
          //  // Clear the content of the response
          //  HttpContext.Current.Response.ClearContent();
          //  // LINE1: Add the file name and attachment, which will force the open/cance/save dialog to show, to the header
          //  HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
          //  // Add the file size into the response header
          //  HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());

          //  // Set the ContentType
          //  HttpContext.Current.Response.ContentType = "application/octet-stream";// ReturnExtension(file.Extension.ToLower());

          //  // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
          //  HttpContext.Current.Response.TransmitFile(file.FullName);
          //  HttpContext.Current.Response.Flush();
          ////  stream = HttpContext.Current.Response.OutputStream;
         
          //  // End the response
          //  HttpContext.Current.Response.End();

          //  HttpContext.Current.Response.ClearHeaders();
         //   HttpContext.Current.Response.ClearContent();
         //   HttpContext.Current.Response.AppendHeader("Content-Length", file.Length.ToString());
         //   HttpContext.Current.Response.ContentType = ReturnExtension(file.Extension.ToLower());
          //  HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + file.Name);
         //  HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(path));
           try
           {
               byte[] data = ReadByteArryFromFile(path);

               //save the data to a memory stream
               MemoryStream memoryStream = new MemoryStream(data);
               SaveFile(filesave, memoryStream);
           }
           catch { }
           finally
           {
               HttpContext.Current.Response.Clear();
           }
          //  HttpContext.Current.Response.End();
        }
      
    }
    private byte[] ReadByteArryFromFile(string destPath)
    {
        byte[] buff = null;
        FileStream fs = new FileStream(destPath, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        long numBytes = new FileInfo(destPath).Length;
        buff = br.ReadBytes((int)numBytes);
        return buff;
    }
    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
            case ".docx":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
            case ".xlsx":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }
    //private void DownLoadFileFromRemoteLocation(string downloadFileLocation, string downloadedFileSaveLocation)
    //{
    //    string serviceUrl = string.Format("{0}/RetrieveFile?Path={1}", System.ServiceModel.Web.FileManagerServiceUrl, downloadFileLocation);
    //    var request = WebRequest.Create(serviceUrl);
    //    request.UseDefaultCredentials = true;
    //    request.PreAuthenticate = true;
    //    request.Credentials = CredentialCache.DefaultCredentials;

    //    try
    //    {
    //        using (var response = request.GetResponse())
    //        {
    //            using (var fileStream = response.GetResponseStream())
    //            {
    //                if (fileStream == null)
    //                {
    //                    MessageBox.Show("File not recieved");
    //                    return;
    //                }

    //                CreateDirectoryForSaveLocation(downloadedFileSaveLocation);
    //                SaveFile(downloadedFileSaveLocation, fileStream);
    //            }
    //        }

    //        MessageBox.Show("File downloaded and copied");

    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show("File could not be downloaded or saved. Message :" + ex.Message);
    //    }

    //}

    private static void SaveFile(string downloadedFileSaveLocation, Stream fileStream)
    {
        using (var file = File.Create(downloadedFileSaveLocation))
        {
            fileStream.CopyTo(file);
        }
    }

    private void CreateDirectoryForSaveLocation(string downloadedFileSaveLocation)
    {
        var fileInfo = new FileInfo(downloadedFileSaveLocation);
        if (fileInfo.DirectoryName == null) throw new Exception("Save location directory could not be determined");
        Directory.CreateDirectory(fileInfo.DirectoryName);
    }  
    
    
    public void SenEmailApproved(string code,string to,string cc)
    {
       // clsSys sys = new clsSys();
       // string to = "khuyentt@icpvn.com";// txtMyEmail.Text;
       // string cc = "khuyentt@beaute-cos.com";// txtAppEmail.Text;
        SendMailASP(to, cc, "Claim has been Approved", "Claim " + code + " has been approved");

    }
    public void SenEmailReject(string code, string resion,string to,string cc)
    {
     //   clsSys sys = new clsSys();
       

      //  string to = "khuyentt@icpvn.com";// txtMyEmail.Text;
       // string cc = "khuyentt@beaute-cos.com";// txtAppEmail.Text;
        SendMailASP(to, cc, "Claim has been Rejected", "Claim " + code + " has been rejected with reason " + resion);

    }
    #region NoiDungEmailMinh
    public string noidungsendApproval(string nguoiduyet, string nguoiyeucau, string quocgia, string bophan, string nhanhang, string nghanhhang, string soaspf, string sotien, string noidung, string ngansachcanam, string dachitieu, string conlai, string activationCode, string code, string duongdan)
    {

        String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
        String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
        string neucoduongdan = "";

        if (duongdan != "")
        {
            
            neucoduongdan = "  Vui lòng nhấn vào  <a href = \"" + strUrl + "/ImagesUpload/" + duongdan + "\"  >đây</a> để xem file đính kèm  " +
                  " <br/>  ";
        }
        else
        {
            neucoduongdan = "";
        }
        string havepath = "";

        if (duongdan != "")
        {
           
            havepath = "  Please click here  <a href = \"" + strUrl + "/ImagesUpload/" + duongdan + "\"  >đây</a> see attach file " +
                  " <br/>  ";
        }
        else
        {
            havepath = "";
        }


        string noidung111 =
           "Kính gởi ông/bà / Dear Sir/Madam  :" + nguoiduyet +
          " <br/>" +
          " Bạn có một yêu cầu duyệt ASPF từ / You have a ASPF approval request from  :" + nguoiyeucau + ",<br/>" +
          "<p>" +
          " Nội dung như sau /Contents are as follows  :";


        DataTable tbl = cls.GetDataTable("sp_Load_ASPF_Report", new string[] { "@ID" }, new object[] { code });

        DataTable tbl1 = cls.GetDataTable("sp_Load_ASPF_Detail_RP", new string[] { "@aspf_fk" }, new object[] { code });

        DataTable tbl11 = cls.GetDataTable("sp_Load_ASPF_Revise", new string[] { "@Revise" }, new object[] { code });


        noidung111 = noidung111 +
        "<table cellspacing=\"0\" style=\" border: 1px solid black; border-collapse: collapse;\">" +
      " <tr style=\"border: 1px solid black; border-collapse: collapse\">" +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">ID Number</td>" +
         " <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + code + "</td> " +
      " </tr> " +
      " <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Quốc gia /Country</td>" +
          "<td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + quocgia + "</td>" +
     "  </tr>" +
     "  <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Budget owner</td>" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + bophan + "</td> " +
    "   </tr>" +
      " <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
           "<td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Ngành hàng/ Bussiness Unit </td>" +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + nghanhhang + "</td> " +
    "   </tr> " +
      " <tr>" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" colspan=\"2\">  " +
             "  <table> " +
              "     <tr  style=\"border: 1px solid black; border-collapse: collapse\"> " +
                       "<td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" +
                         "  Ngày thực hiện từ ngày / From :  " +
                    "   </td>" +
                      "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + DateTime.Parse(tbl.Rows[0]["from"].ToString()).ToString("dd/MM/yyyy") + "</td> " +

                      "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >" +
                          "  Đến ngày / To :" +
                     "  </td>" +
                       "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + DateTime.Parse(tbl.Rows[0]["to"].ToString()).ToString("dd/MM/yyyy") + "</td> " +

                 "  </tr>" +
               "</table> " +

         "  </td>" +

      " </tr>" +
     "  <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Áp dụng cho kênh / Channel</td> " +
          "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + tbl.Rows[0]["Channel"].ToString() + "</td>" +
      " </tr>" +
     "  <tr  style=\"border: 1px solid black; border-collapse: collapse\"> " +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Mục tiêu / Objective</td>" +
          "<td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + noidung + "</td>" +
    "   </tr> " +
     "  <tr style=\"border: 1px solid black; border-collapse: collapse\">" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">ASPF value (VND)</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + cls.FormatNumber(sotien) + "</td>" +
    "   </tr> " +
     "  <tr style=\"border: 1px solid black; border-collapse: collapse\">" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >File đính kèm/ Attach file</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + neucoduongdan + "</td>" +
    "   </tr> " +
       "</table> " +

       "</br>" +

   "<table cellspacing=\"0\" style=\" border: 1px solid black; border-collapse: collapse; \" >" +
   "    <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
       "    <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Số ASPF/IO Number</td> " +
        "   <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Vùng/Region</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Mã chi phí/Code</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Nhãn hàng/Brand</td> " +
        "   <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Chủng Loại/Category</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >Diễn giải/Description</td> " +
                "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >MaterialGroup free</td> " +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Thành tiền/ Amount (VND)</td> " +
        "   <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >Ngân sách còn lại/ Budget available(VND)</td>" +
     "  </tr> ";
        string noidung333 = " ";
        for (int i = 0; i < tbl1.Rows.Count; i++)
        {
            noidung333 = noidung333 + "   <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
      "     <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + tbl1.Rows[i]["IOnumber"].ToString() + "</td> " +
       "    <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["Region"].ToString() + " </td> " +
       "    <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["Item"].ToString() + " </td>  " +
        "   <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + tbl1.Rows[i]["Brand"].ToString() + " </td> " +
       "    <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["Category"].ToString() + " </td> " +
      "     <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["Description"].ToString() + " </td> " +
                 "     <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["MaterialGroup"].ToString() + " </td> " +
       "    <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + cls.FormatNumber(tbl1.Rows[i]["Amount"].ToString()) + "</td> " +
      "     <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + cls.FormatNumber(tbl1.Rows[i]["Budget"].ToString()) + " </td> " +
      " </tr>";

        }

        string noidung444 = "";
       
        if (tbl11.Rows.Count > 0)
        {
            
            for (int i = 0; i < tbl11.Rows.Count; i++)
            {
                noidung444 = noidung444 + "IO này thay đổi từ  <a href = '" + strUrl + "/ASPFDetail.aspx?Userid=" + tbl11.Rows[i]["ID"].ToString() + "'>IO</a>  " +
                  " <br/> ";
            }
        }
        //    " </table> " +
      
        noidung111 = noidung111 + noidung333 + " </table> " + "</br>"
            + noidung444 +
            "  Vui lòng nhấn vào  <a href = \"" + strUrl + "/AutoClose.aspx?ActivationCode=" + activationCode + "&Userid=" + code + "\"  onclick=\"window.open(this.href,'mywin','left=300,top=300,width=500,height=300,toolbar=1,resizable=0'); return false;\"  >đây</a> để duyệt hoặc nhấn vào <a href = '" + strUrl + "/RejectOnline.aspx?ActivationCode=" + activationCode + "&Userid=" + code + "&Reject=" + code + "'>đây</a> để từ chối " +

         " . Muốn xem chi tiết nội dung ASPF, vui lòng nhấn vào <a href = '" + strUrl + "/ApprovalOnline.aspx?ActivationCode=" + activationCode + "&Userid=" + code + "'>đây</a>  " +
            " <br/> " +


           "  Please click  here to   <a href = '" + strUrl + "/AutoClose.aspx?ActivationCode=" + activationCode + "&Userid=" + code + "'>here</a> to approve , click here <a href = '" + strUrl + "/RejectOnline.aspx?ActivationCode=" + activationCode + "&Userid=" + code + "&Reject=" + code + "'>here</a>  to reject " +

            ". Please click <a href = '" + strUrl + "/ApprovalOnline.aspx?ActivationCode=" + activationCode + "&Userid=" + code + "'>here</a> for details of ASPF <br/> " +
          "   Best regards, <br/> ";



        return noidung111;
    }

    public string noidung(string nguoiduyet, string nguoiyeucau, string quocgia, string bophan, string nhanhang, string nghanhhang, string soaspf, string sotien, string noidung, string ngansachcanam, string dachitieu, string conlai, string activationCode, string code)
    {

        string neucoduongdan = "";



        string noidung111 =
           "Kính gởi ông/bà / Dear Sir/Madam  :" + nguoiduyet +
          " <br/>" +
          " Bạn có một yêu cầu duyệt ASPF từ / You have a ASPF approval request from : " + nguoiyeucau + ",<br/>" +
          "<p>" +
          " Nội dung như sau:" +

           "<br/>";

        DataTable tbl = cls.GetDataTable("sp_Load_ASPF_Report", new string[] { "@ID" }, new object[] { code });

        DataTable tbl1 = cls.GetDataTable("sp_Load_ASPF_Detail_RP", new string[] { "@aspf_fk" }, new object[] { code });


        noidung111 = noidung111 +
        "<table cellspacing=\"0\" style=\" border: 1px solid black; border-collapse: collapse;\">" +
      " <tr style=\"border: 1px solid black; border-collapse: collapse\">" +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">ID Number</td>" +
         " <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + code + "</td> " +
      " </tr> " +
      " <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Quốc gia /Country</td>" +
          "<td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + quocgia + "</td>" +
     "  </tr>" +
     "  <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Budget owner</td>" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + bophan + "</td> " +
    "   </tr>" +
      " <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
           "<td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Ngành hàng/ Bussiness Unit </td>" +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + nhanhang + "</td> " +
    "   </tr> " +
      " <tr>" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" colspan=\"2\">  " +
             "  <table> " +
              "     <tr  style=\"border: 0px; border-collapse: collapse\"> " +
                       "<td align=\"center\" style=\"border: 0px; border-collapse: collapse; font-weight: bold;\">" +
                         "  Ngày thực hiện từ ngày / From :  " +
                    "   </td>" +
                      "  <td align=\"center\" style=\"border: 0px; border-collapse: collapse; font-weight: bold;\">" + DateTime.Parse(tbl.Rows[0]["from"].ToString()).ToString("dd/MM/yyyy") + "</td> " +

                      "  <td align=\"center\" style=\"border: 0px; border-collapse: collapse; font-weight: bold;\" >" +
                          "  Đến ngày / To :" +
                     "  </td>" +
                       "  <td align=\"center\" style=\"border:0px; border-collapse: collapse; font-weight: bold;\">" + DateTime.Parse(tbl.Rows[0]["to"].ToString()).ToString("dd/MM/yyyy") + "</td> " +

                 "  </tr>" +
               "</table> " +

         "  </td>" +

      " </tr>" +
     "  <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Áp dụng cho kênh / Channel</td> " +
          "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + tbl.Rows[0]["Channel"].ToString() + "</td>" +
      " </tr>" +
     "  <tr  style=\"border: 1px solid black; border-collapse: collapse\"> " +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Mục tiêu / Objective</td>" +
          "<td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + noidung + "</td>" +
    "   </tr> " +
     "  <tr style=\"border: 1px solid black; border-collapse: collapse\">" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">ASPF value (VND)</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + cls.FormatNumber(sotien) + "</td>" +
    "   </tr> " +
     "  <tr style=\"border: 1px solid black; border-collapse: collapse\">" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >File đính kèm/ Attach file</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + neucoduongdan + "</td>" +
    "   </tr> " +
       "</table> " +

       "</br>" +

   "<table cellspacing=\"0\" style=\" border: 1px solid black; border-collapse: collapse; \" >" +
   "    <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
       "    <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Số ASPF/IO Number</td> " +
        "   <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Vùng/Region</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Mã chi phí/Code</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Nhãn hàng/Brand</td> " +
        "   <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Chủng Loại/Category</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >Diễn giải/Description</td> " +
          "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >MaterialGroup free</td> " +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Thành tiền/ Amount (VND)</td> " +
        "   <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >Ngân sách còn lại/ Budget available(VND)</td>" +
     "  </tr> ";
        string noidung333 = " ";
        for (int i = 0; i < tbl1.Rows.Count; i++)
        {
            noidung333 = noidung333 + "   <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
      "     <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + tbl1.Rows[i]["IOnumber"].ToString() + "</td> " +
       "    <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["Region"].ToString() + " </td> " +
       "    <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["Item"].ToString() + " </td>  " +
        "   <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + tbl1.Rows[i]["Brand"].ToString() + " </td> " +
       "    <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["Category"].ToString() + " </td> " +
      "     <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["Description"].ToString() + " </td> " +
           "     <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["MaterialGroup"].ToString() + " </td> " +
       "    <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + cls.FormatNumber(tbl1.Rows[i]["Amount"].ToString()) + "</td> " +
      "     <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + cls.FormatNumber(tbl1.Rows[i]["Budget"].ToString()) + " </td> " +
      " </tr>";

        }
        DataTable tbl11 = cls.GetDataTable("sp_Load_ASPF_Revise", new string[] { "@Revise" }, new object[] { code });
        string noidung444 = "";
        String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
        String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
        if (tbl11.Rows.Count > 0)
        {
           
            for (int i = 0; i < tbl11.Rows.Count; i++)
            {
                noidung444 = noidung444 + "IO này thay đổi từ  <a href = '" + strUrl + "/ASPFDetail.aspx?Userid=" + tbl11.Rows[i]["ID"].ToString() + "'>IO</a>  " +
                  " <br/> ";
            }
        }

        noidung111 = noidung111 + noidung333 + " </table> " + "</br>" + noidung444 +
            "  Vui lòng nhấn vào  <a href = '" + strUrl + "/CheckingOnline.aspx?ActivationCode=" + activationCode + "&code=" + code + "'>đây</a> để duyệt hoặc nhấn vào <a href = '" + strUrl + "/CheckingOnline.aspx?ActivationCode=" + activationCode + "&code=" + code + "&Reject=" + code + "'>đây</a> để từ chối " +

          ".Muốn xem chi tiết nội dung ASPF, vui lòng nhấn vào <a href = '" + strUrl + "/ApprovalOnline.aspx?ActivationCode=" + activationCode + "&Userid=" + code + "'>đây</a>  " +
             " <br/> " +

    "  Please click   <a href = '" + strUrl + "/CheckingOnline.aspx?ActivationCode=" + activationCode + "&code=" + code + "'>here</a> to approve nhấn vào <a href = '" + strUrl + "/CheckingOnline.aspx?ActivationCode=" + activationCode + "&code=" + code + "&Reject=" + code + "'>here</a>  to reject " +

    "  Please click here to approve or  <br/> " +
     " Please click <a href = '" + strUrl + "/ApprovalOnline.aspx?ActivationCode=" + activationCode + "&Userid=" + code + "'>here</a> for details of ASPF <br/> " +
   "   Best regards, <br/> ";



        return noidung111;
    }

    public string noidungCungCapASPNo(string nguoiduyet, string nguoiyeucau, string quocgia, string bophan, string nhanhang, string nghanhhang, string soaspf, string sotien, string noidung, string ngansachcanam, string dachitieu, string conlai, string activationCode, string code)
    {


        string neucoduongdan = "";



        string noidung111 =
           "Kính gởi ông/bà / Dear Sir/Madam :" + nguoiduyet +
          " <br/>" +
          " Bạn có một yêu cầu cung cấp số IO từ  / You have a  request provider number IO from : " + nguoiyeucau + ",<br/>" +
          "<p>" +
          " Nội dung như sau:" +

           "<br/>";

        DataTable tbl = cls.GetDataTable("sp_Load_ASPF_Report", new string[] { "@ID" }, new object[] { code });

        DataTable tbl1 = cls.GetDataTable("sp_Load_ASPF_Detail_RP", new string[] { "@aspf_fk" }, new object[] { code });


        noidung111 = noidung111 +
        "<table cellspacing=\"0\" style=\" border: 1px solid black; border-collapse: collapse;\">" +
      " <tr style=\"border: 1px solid black; border-collapse: collapse\">" +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">ID Number</td>" +
         " <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + code + "</td> " +
      " </tr> " +
      " <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Quốc gia /Country</td>" +
          "<td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + quocgia + "</td>" +
     "  </tr>" +
     "  <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Budget owner</td>" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + bophan + "</td> " +
    "   </tr>" +
      " <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
           "<td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Ngành hàng/ Bussiness Unit </td>" +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + nhanhang + "</td> " +
    "   </tr> " +
      " <tr>" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" colspan=\"2\">  " +
             "  <table> " +
              "     <tr  style=\"border: 0px; border-collapse: collapse\"> " +
                       "<td align=\"center\" style=\"border: 0px; border-collapse: collapse; font-weight: bold;\">" +
                         "  Ngày thực hiện từ ngày / From :  " +
                    "   </td>" +
                      "  <td align=\"center\" style=\"border: 0px; border-collapse: collapse; font-weight: bold;\">" + DateTime.Parse(tbl.Rows[0]["From"].ToString()).ToString("dd/MM/yyyy") + "</td> " +

                      "  <td align=\"center\" style=\"border: 0px; border-collapse: collapse; font-weight: bold;\" >" +
                          "  Đến ngày / To :" +
                     "  </td>" +
                       "  <td align=\"center\" style=\"border:0px; border-collapse: collapse; font-weight: bold;\">" + DateTime.Parse(tbl.Rows[0]["to"].ToString()).ToString("dd/MM/yyyy") + "</td> " +

                 "  </tr>" +
               "</table> " +

         "  </td>" +

      " </tr>" +
     "  <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Áp dụng cho kênh / Channel</td> " +
          "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + tbl.Rows[0]["Channel"].ToString() + "</td>" +
      " </tr>" +
     "  <tr  style=\"border: 1px solid black; border-collapse: collapse\"> " +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Mục tiêu / Objective</td>" +
          "<td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + noidung + "</td>" +
    "   </tr> " +
     "  <tr style=\"border: 1px solid black; border-collapse: collapse\">" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">ASPF value (VND)</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + cls.FormatNumber(sotien) + "</td>" +
    "   </tr> " +
     "  <tr style=\"border: 1px solid black; border-collapse: collapse\">" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >File đính kèm/ Attach file</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + neucoduongdan + "</td>" +
    "   </tr> " +
       "</table> " +

       "</br>" +

   "<table cellspacing=\"0\" style=\" border: 1px solid black; border-collapse: collapse; \" >" +
   "    <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
       "    <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Số ASPF/IO Number</td> " +
        "   <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Vùng/Region</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Mã chi phí/Code</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Nhãn hàng/Brand</td> " +
        "   <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Chủng Loại/Category</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >Diễn giải/Description</td> " +
            "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >MaterialGroup free</td> " +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Thành tiền/ Amount (VND)</td> " +
        "   <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >Ngân sách còn lại/ Budget available(VND)</td>" +
     "  </tr> ";
        string noidung333 = " ";
        for (int i = 0; i < tbl1.Rows.Count; i++)
        {
            noidung333 = noidung333 + "   <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
      "     <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + tbl1.Rows[i]["IOnumber"].ToString() + "</td> " +
       "    <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["Region"].ToString() + " </td> " +
       "    <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["Item"].ToString() + " </td>  " +
        "   <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + tbl1.Rows[i]["Brand"].ToString() + " </td> " +
       "    <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["Category"].ToString() + " </td> " +
      "     <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["Description"].ToString() + " </td> " +
     "     <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["MaterialGroup"].ToString() + " </td> " +
       "    <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + cls.FormatNumber(tbl1.Rows[i]["Amount"].ToString()) + "</td> " +
      "     <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + cls.FormatNumber(tbl1.Rows[i]["Budget"].ToString()) + " </td> " +
      " </tr>";

        }
        String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
        String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
        noidung111 = noidung111 + noidung333 + " </table> " + "</br>" +
         "  Vui lòng nhấn vào  <a href = '" + strUrl + "/ProviderASPNo.aspx?ActivationCode=" + activationCode + "&code=" + code + "'>đây</a> để duyệt " +

          ".Muốn xem chi tiết nội dung ASPF, vui lòng nhấn vào <a href = '" + strUrl + "/ApprovalOnline.aspx?ActivationCode=" + activationCode + "&Userid=" + code + "'>đây</a>  " +
             " <br/> " +
            //          " Trân trọng <br/> " +

             "  Please click   <a href = '" + strUrl + "/ProviderASPNo.aspx?ActivationCode=" + activationCode + "&code=" + code + "'>here</a> to approve nhấn vào <a href = '" + strUrl + "/CheckingOnline.aspx?ActivationCode=" + activationCode + "&code=" + code + "&Reject=" + code + "'>here</a>  to reject " +

    "  Please click here to approve or  <br/> " +
     " Please click <a href = '" + strUrl + "/ApprovalOnline.aspx?ActivationCode=" + activationCode + "&Userid=" + code + "'>here</a> for details of ASPF <br/> " +
   "   Best regards, <br/> ";



        return noidung111;
    }

    public string noidungApproval(string nguoiduyet, string nguoiyeucau, string quocgia, string bophan, string nhanhang, string nghanhhang, string soaspf, string sotien, string noidung, string ngansachcanam, string dachitieu, string conlai, string activationCode, string code)
    {
        string neucoduongdan = "";
        string noidung111 =
           "Kính gởi ông/bà / Dear Sir/Madam :" + nguoiyeucau +
          " <br/>" +
          " Yêu cầu duyệt ASPF đã được duyệt bởi /  Your ASPF approval request was approved by : " + nguoiduyet + ",<br/>" +
          "<p>" +
          " Nội dung như sau /Contents are as follows :" +
           "<br/>" +

           "<br/>";

        DataTable tbl = cls.GetDataTable("sp_Load_ASPF_Report", new string[] { "@ID" }, new object[] { code });

        DataTable tbl1 = cls.GetDataTable("sp_Load_ASPF_Detail_RP", new string[] { "@aspf_fk" }, new object[] { code });


        noidung111 = noidung111 +
        "<table cellspacing=\"0\" style=\" border: 1px solid black; border-collapse: collapse;\">" +
      " <tr style=\"border: 1px solid black; border-collapse: collapse\">" +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">ID Number</td>" +
         " <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + code + "</td> " +
      " </tr> " +
      " <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Quốc gia /Country</td>" +
          "<td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + quocgia + "</td>" +
     "  </tr>" +
     "  <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Budget owner</td>" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + bophan + "</td> " +
    "   </tr>" +
      " <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
           "<td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Ngành hàng/ Bussiness Unit </td>" +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + nghanhhang + "</td> " +
    "   </tr> " +
      " <tr>" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" colspan=\"2\">  " +
             "  <table> " +
              "     <tr  style=\"border: 1px solid black; border-collapse: collapse\"> " +
                       "<td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" +
                         "  Ngày thực hiện từ ngày / From :  " +
                    "   </td>" +
                      "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + DateTime.Parse(tbl.Rows[0]["from"].ToString()).ToString("dd/MM/yyyy") + "</td> " +

                      "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >" +
                          "  Đến ngày / To :" +
                     "  </td>" +
                       "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + DateTime.Parse(tbl.Rows[0]["To"].ToString()).ToString("dd/MM/yyyy") + "</td> " +

                 "  </tr>" +
               "</table> " +

         "  </td>" +

      " </tr>" +
     "  <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Áp dụng cho kênh / Channel</td> " +
          "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + tbl.Rows[0]["Channel"].ToString() + "</td>" +
      " </tr>" +
     "  <tr  style=\"border: 1px solid black; border-collapse: collapse\"> " +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Mục tiêu / Objective</td>" +
          "<td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + noidung + "</td>" +
    "   </tr> " +
     "  <tr style=\"border: 1px solid black; border-collapse: collapse\">" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">ASPF value (VND)</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + cls.FormatNumber(sotien) + "</td>" +
    "   </tr> " +
     "  <tr style=\"border: 1px solid black; border-collapse: collapse\">" +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >File đính kèm/ Attach file</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + neucoduongdan + "</td>" +
    "   </tr> " +
       "</table> " +

       "</br>" +

   "<table cellspacing=\"0\" style=\" border: 1px solid black; border-collapse: collapse; \" >" +
   "    <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
       "    <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Số ASPF/IO Number</td> " +
        "   <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Vùng/Region</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Mã chi phí/Code</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Nhãn hàng/Brand</td> " +
        "   <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Chủng Loại/Category</td> " +
         "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >Diễn giải/Description</td> " +
           "  <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >MaterialGroup free</td> " +
          " <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">Thành tiền/ Amount (VND)</td> " +
        "   <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\" >Ngân sách còn lại/ Budget available(VND)</td>" +
     "  </tr> ";
        string noidung333 = " ";
        for (int i = 0; i < tbl1.Rows.Count; i++)
        {
            noidung333 = noidung333 + "   <tr  style=\"border: 1px solid black; border-collapse: collapse\">" +
      "     <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + tbl1.Rows[i]["IOnumber"].ToString() + "</td> " +
       "    <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["Region"].ToString() + " </td> " +
       "    <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["Item"].ToString() + " </td>  " +
        "   <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\">" + tbl1.Rows[i]["Brand"].ToString() + " </td> " +
       "    <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["Category"].ToString() + " </td> " +
      "     <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["Description"].ToString() + " </td> " +
         "     <td  align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + tbl1.Rows[i]["MaterialGroup"].ToString() + " </td> " +
       "    <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + cls.FormatNumber(tbl1.Rows[i]["Amount"].ToString()) + "</td> " +
      "     <td align=\"center\" style=\"border: 1px solid black; border-collapse: collapse; font-weight: bold;\"> " + cls.FormatNumber(tbl1.Rows[i]["Budget"].ToString()) + " </td> " +
      " </tr>";

        }

        noidung111 = noidung111 + noidung333 + " </table> " + "</br>" +

       "Vui lòng liên lạc /  Please contact   " + nguoiduyet + " để biết thêm chi tiết /for further details  " +
          " <br/> " +
          " Trân trọng /Best regards, <br/> " +

        //" ---------------------------------------------------------------------------------------------------------------------------------------------------------- <br/>" +

        //  " Dear Sir/Madam  " + nguoiyeucau + " <br/>   " +
            // "  Your ASPF approval request was approved by " + nguoiduyet + " <br/> " +
            //    "<p>" +
            //  " Contents are as follows : <br/> " +
            //  " Country: " + quocgia + " <br/> " +
            // "  Department: " + bophan + " <br/> " +
            //  " Brand: " + nhanhang + " <br/> " +
            // "  Category: " + nghanhhang + " <br/> " +
            // "  ASPF number: " + soaspf + " <br/> " +
            // "  Amount: " + cls.FormatNumber(sotien) + " <br/> " +
            // "  Ojective: " + noidung + " <br/> " +
            //    "<p>" +
            // "  Remark: <br/> " +
            // "  FY Budget: " + cls.FormatNumber(ngansachcanam) + " <br/> " +
            //"   Spent: " + cls.FormatNumber(dachitieu) + " <br/> " +
            // "  Balance: " + cls.FormatNumber(conlai) + " <br/> " +
            //    "<p>" +

        //  " Please contact Contents are as follows : " + nguoiduyet + " for further details. " +
        "   Best regards, <br/> ";



        return noidung111;
    }

    public string noidungRevise(string nguoiduyet, string nguoiyeucau, string quocgia, string bophan, string nhanhang, string nghanhhang, string soaspf, string sotien, string noidung, string ngansachcanam, string dachitieu, string conlai, string activationCode, string code, string sotientruoc)
    {

        string noidung111 =
           "Kính gởi ông/bà :" + nguoiyeucau +
          " <br/>" +
          "Số ASPF  :" + soaspf + " đã được thay đổi với nội dung như sau :" +
          " <br/>" +

          " Số tiền trước khi điều chỉnh : " + cls.FormatNumber(sotientruoc) +
         "  <br/>" +
         " Số tiền  sau khi điều chỉnh : " + cls.FormatNumber(sotien) +

          " <br/>" +
         "  Ghi chú:" +
          " Ngân sách cả năm: " + cls.FormatNumber(ngansachcanam) +
          " <br/>" +
          " Đã chi tiêu: " + cls.FormatNumber(dachitieu) +
          " <br/>  " +
          " Còn lại: " + cls.FormatNumber(conlai) +
          " <br/>" +
             "<p>" +
       "Vui lòng liên lạc " + nguoiyeucau + " để biết thêm chi tiết " +
          " <br/> " +
          " Trân trọng <br/> " +

        " ---------------------------------------------------------------------------------------------------------------------------------------------------------- <br/>" +

          " Dear Sir/Madam : " + nguoiyeucau + " <br/>   " +
          " ASPF No  :" + soaspf + "  has been changed as follows :" +
          " <br/>" +
            "<p>" +

         "  Amount before adjustment : " + cls.FormatNumber(sotientruoc) + " <br/> " +
         "  <br/>" +
         " Amount after adjustment : " + cls.FormatNumber(sotien) + " <br/> " +
         "  Remark: <br/> " +
         "  FY Budget: " + cls.FormatNumber(ngansachcanam) + " <br/> " +
        "   Spent: " + cls.FormatNumber(dachitieu) + " <br/> " +
         "  Balance: " + cls.FormatNumber(conlai) + " <br/> " +
            "<p>" +

          " Please contact " + nguoiyeucau + " for further details. " +
        "   Best regards, <br/> ";



        return noidung111;
    }

    public string noidungReject(string nguoiduyet, string nguoiyeucau, string quocgia, string bophan, string nhanhang, string nghanhhang, string soaspf, string sotien, string noidung, string ngansachcanam, string dachitieu, string conlai, string activationCode, string code, string lido)
    {

        string noidung111 =
           "Kính gởi ông/bà :" + nguoiyeucau +
          " <br/>" +
          " Yêu cầu duyệt ASPF từ " + nguoiyeucau + " đã bị từ chối bởi  " + nguoiduyet + ",<br/>" +
          "<p>" +
          " Nội dung như sau:" +
           "<br/>" +
          " Quốc gia:" + quocgia +
           " <br/>" +
           " Bộ phận:  " + bophan +
          " <br/>" +
          " Nhãn hàng: " + nhanhang +
         "  <br/>" +
          " Ngành hàng:" + nghanhhang +
          " <br/>" +
          " Số ASPF: " + soaspf +
         "  <br/>" +
          " Số tiền: " + cls.FormatNumber(sotien) +
         "  <br/>" +
          " Nội dung: " + noidung +
             "<p>" +
          " <br/>" +
         "  Ghi chú:" +
          " Ngân sách cả năm: " + cls.FormatNumber(ngansachcanam) +
          " <br/>" +
          " Đã chi tiêu: " + cls.FormatNumber(dachitieu) +
          " <br/>  " +
          " Còn lại: " + cls.FormatNumber(conlai) +
          " <br/>" +
             "<p>" +
               " Lý do bị từ chối : " + lido +
               "<p>" +
       "Vui lòng liên lạc " + nguoiduyet + " để biết thêm chi tiết " +
          " <br/> " +
          " Trân trọng <br/> " +

        " ---------------------------------------------------------------------------------------------------------------------------------------------------------- <br/>" +

          " Dear Sir/Madam  " + nguoiyeucau + " <br/>   " +
         "  Your ASPF approval request was rejected  by " + nguoiduyet + " <br/> " +
            "<p>" +
          " Contents are as follows : <br/> " +
          " Country: " + quocgia + " <br/> " +
         "  Department: " + bophan + " <br/> " +
          " Brand: " + nhanhang + " <br/> " +
         "  Category: " + nghanhhang + " <br/> " +
         "  ASPF number: " + soaspf + " <br/> " +
         "  Amount: " + cls.FormatNumber(sotien) + " <br/> " +
         "  Ojective: " + noidung + " <br/> " +
            "<p>" +
         "  Remark: <br/> " +
         "  FY Budget: " + cls.FormatNumber(ngansachcanam) + " <br/> " +
        "   Spent: " + cls.FormatNumber(dachitieu) + " <br/> " +
         "  Balance: " + cls.FormatNumber(conlai) + " <br/> " +
            "<p>" +
             "<p>" +
               "Reason reject : " + lido +
               "<p>" +
          " Please contact " + nguoiduyet + " for further details. " +
        "   Best regards, <br/> ";



        return noidung111;
    }


    #endregion
    public bool CheckEmail(string EmailAddress)
    {
        //string strPattern = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
        //if (System.Text.RegularExpressions.Regex.IsMatch(EmailAddress, strPattern))
        //{
        string email = ConfigurationManager.AppSettings["email"].ToString();
        int vt = EmailAddress.LastIndexOf("@");
        if (vt <= 0)
        {
            return false;
        }
       string[] exemail = email.Split('|');
        string duoiemail = EmailAddress.Substring(vt);
        int i = 0;
        while (i < exemail.Length && duoiemail.Trim().ToLower()!=exemail.GetValue(i).ToString().Trim().ToLower())
        {
            i++;
        }
      //  int emailhople = email.IndexOf(duoiemail);
        if (i < exemail.Length)
        {
            return true;
        }
        else
        {
            return false;
        }

        //}
        //return false;
    }
    public double cToDuoble(object obj)
    {
        double kq = 0;
        try
        {
            kq = Convert.ToDouble(obj);
        }
        catch
        {
            kq = 0;
        }
        return kq;
    }
    public string cToString(object obj)
    {
        string kq = "";
        try
        {
            kq = obj.ToString();
        }
        catch
        {
            kq = "";
        }
        return kq;
    }
    public int cToInt(object obj)
    {
        int kq = 0;
        try
        {
            kq = int.Parse(obj.ToString());
        }
        catch
        {
            kq = 0;
        }
        return kq;
    }
    /// <summary>
    /// insert log; statuskey: login, logout, resetpass, changepass
    /// active: 1 thanh cong, 0 that bai
    /// </summary>
    /// <param name="email"></param>
    /// <param name="statuskey"></param>
    /// <param name="active"></param>
    public void insertLog(string email, string statuskey, bool active)
    {
        //clsObj Obj;
        //clsSql Sql = new clsSql();
        //Obj = new clsObj();
        //Obj.Parameter = new string[] { "@email", "@ipadd", "@LanIP", "@computername", "@userloginwindow", "@statuskey", "@active" };
      

        string userloginWindow;
        // Get UserHostAddress property.

        HttpRequest currentRequest = HttpContext.Current.Request;
        string ipAddress = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (ipAddress == null || ipAddress.ToLower() == "unknown")
            ipAddress = currentRequest.ServerVariables["REMOTE_ADDR"];

        userloginWindow = System.Environment.UserName;
        string LanIP = GetLanIPAddress();
        // ip=HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        string com = System.Environment.MachineName;//.Net.Dns.GetHostName();
        //Obj.ValueParameter = new object[] { email, ipAddress, LanIP, com, userloginWindow, statuskey, active };//dag nhap thanh cong
        //Obj.SpName = "sp_insertLog";
        //Sql.fNonGetData(Obj);
        //DBTableDataContext ds = new DBTableDataContext();
        DateTime gd=DateTime.Now;
      
        using (var db = new DBTableDataContext())
        {
            var model = new log { email = email, ipadd = ipAddress, LanIP = LanIP, Computername = com, userloginwindow = userloginWindow, statuskey = statuskey, Active = active, whenlog = gd };
            db.logs.InsertOnSubmit(model);
            db.SubmitChanges();
        }
       // cls.bThem(new string[] { }, new object[] { });
    }
    //Get Lan Connected IP address method
    public string GetLanIPAddress()
    {
        //Get the Host Name
        string stringHostName = System.Net.Dns.GetHostName();
        //Get The Ip Host Entry
        System.Net.IPHostEntry ipHostEntries = System.Net.Dns.GetHostEntry(stringHostName);
        //Get The Ip Address From The Ip Host Entry Address List
        System.Net.IPAddress[] arrIpAddress = ipHostEntries.AddressList;
        return arrIpAddress[arrIpAddress.Length - 1].ToString();
    }
    

        public string RemoveSign4VietnameseString(string ip_str_change)
        {
            Regex v_reg_regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string v_str_FormD = ip_str_change.Normalize(NormalizationForm.FormD);
            return v_reg_regex.Replace(v_str_FormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }


        //private void SendActivationEmail(int userId)
        //{
        //    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //    string activationCode = Guid.NewGuid().ToString();
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("INSERT INTO UserActivation VALUES(@UserId, @ActivationCode)"))
        //        {
        //            using (SqlDataAdapter sda = new SqlDataAdapter())
        //            {
        //                cmd.CommandType = CommandType.Text;
        //                cmd.Parameters.AddWithValue("@UserId", userId);
        //                cmd.Parameters.AddWithValue("@ActivationCode", activationCode);
        //                cmd.Connection = con;
        //                con.Open();
        //                cmd.ExecuteNonQuery();
        //                con.Close();
        //            }
        //        }
        //    }
        //    using (MailMessage mm = new MailMessage("sender@gmail.com", txtEmail.Text))
        //    {
        //        mm.Subject = "Account Activation";
        //        string body = "Hello " + txtUsername.Text.Trim() + ",";
        //        body += "<br /><br />Please click the following link to activate your account";
        //        body += "<br /><a href = '" +HttpContext.Current.Request.Url.AbsoluteUri.Replace("CS.aspx", "CS_Activation.aspx?ActivationCode=" + activationCode) + "'>Click here to activate your account.</a>";
        //        body += "<br /><br />Thanks";
        //        mm.Body = body;
        //        mm.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.EnableSsl = true;
        //        NetworkCredential NetworkCred = new NetworkCredential("sender@gmail.com", "<password>");
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = 587;
        //        smtp.Send(mm);
        //    }
        //}

   
}