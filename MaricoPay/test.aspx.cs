using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Configuration;
namespace MaricoPay
{
    
    public partial class test : System.Web.UI.Page
    {
        Cclass cls = new Cclass();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btcontract_Click(object sender, EventArgs e)
        {

            //string strDoc = @"D:\2016-FC-AS-11-khuyen.tran.doc";
            //string strDocout = @"D:\FC-2016-FC-AS-11-khuyen.tran.docx";
            //string strDocoutf = @"D:\LG-FC-2016-FC-AS-11-khuyen.tran.docx";
            ////  string strDocoutf1 = @"D:\testoutfot1.docx";
            //string pdfout = @"D:\LG-FC-2016-FC-AS-11.pdf";
            //string img1 = @"D:\sy.vo.png";
            //string img2 = @"D:\oanh.hoang.png";
            //string file1 = "LG-2016-HR-AS-42-oanh.hoang.doc";
            //string out1 = "LG-FC-2016-HR-AS-42.pdf";
            ////  string filepath1 = Server.MapPath("Upload/CO/" + file1);
            //// string outpdffilepath1 =Server.MapPath("Upload/CO/" + out1);

            //string filepath1 = @"E:\HostWebIIS\testkhuyen\Upload\CO\" + file1;//day la duong dan tren server
            //string outpdffilepath1 = @"E:\HostWebIIS\testkhuyen\Upload\CO\" + out1;
            ////E:\HostWebIIS\testkhuyen\Upload\CO
            //// object filepath1 = "https://fin.icpvn.com/Upload/CO/" + file1;
            ////  object outpdffilepath1 = "https://fin.icpvn.com/Upload/CO/" + out1;
            //// string outpdffilepath = Server.MapPath("Upload/CO/" + pdffilename);
            ////   ct.AddSinagureFooter(strDoc, img1, strDocout);
            ////  ct.AddSinagureFooter(strDocout, img2, strDocoutf);

            //CContract ct = new CContract();
            //string kq = ct.Doc2Pdf(filepath1, outpdffilepath1); //goi bang dll WordToPDF.dll

            //TextBox1.Text = kq;

            ////    ct.word2PDF3(filepath1, outpdffilepath1);

            //// word2PDF3()
        }
       
      
      
        protected void Button1_Click(object sender, EventArgs e)
        {

          
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

         
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            
        }

        protected void btUnlockFile_Click(object sender, EventArgs e)
        {
            CContract ct = new CContract();
             string strdoc = @"D:\Temp\aaa.doc";
             string strdocout = @"D:\Temp\aaaOut.doc";
            string img1 = @"D:\khuyen.tran.png";
            string pass="legalonly";
            ketquaSign kq = ct.AddSinagureFooterWord1(strdoc, img1, strdocout, 10, 0);
             TextBox1.Text=kq.noidung;
            //string kq=ct.UnLock(strdoc, pass, strdocout);
            //if ( kq== strdocout)
            //{
            //    TextBox1.Text = "OK " + kq;
            //}
            //else
            //{
            //    TextBox1.Text="NO "+kq;
            //}
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            CContract ct = new CContract();
            string strdoc = @"D:\Temp\aaa.pdf";
            string strdocout = @"D:\Temp\aaaOut.pdf";
            string img1 = @"D:\khuyen.tran.png";
            string pass = "legalonly";
            ketquaSign kq = ct.AddSinagureFooterPdf(strdoc, img1, strdocout, 10, 0);
            TextBox1.Text = kq.noidung;
        }
    }
}