using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
//using Spire.Doc;
//using Spire.Doc.Documents;
//using Spire.Doc.Fields;
using System.Drawing;
//using WordToPDF;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Word;
using Ionic.Zip;

using System.Net;
using System.Threading;

    public class CContract
    {
        //public void AddSinagureHeader(string filedoc,string fileimage,string outputfile)
        //{
        //     //Load Document

        //    Spire.Doc.Document doc = new Spire.Doc.Document();

        //    doc.LoadFromFile(filedoc);

 

        //    //Header Paragraph

        //    Spire.Doc.HeaderFooter header = doc.Sections[0].HeadersFooters.Header;

        //    Spire.Doc.Documents.Paragraph para = header.AddParagraph();

        //    para.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;

 

        //    //Header Image

        //    Spire.Doc.Fields.DocPicture pic = para.AppendPicture(System.Drawing.Image.FromFile(fileimage));

        //    pic.Height = 22;

        //    pic.Width = 30;

        //    pic.TextWrappingStyle = Spire.Doc.Documents.TextWrappingStyle.Inline;

 

        //    //Header Text

        //    Spire.Doc.Fields.TextRange tr=para.AppendText("Microsoft Technology");

        //    tr.CharacterFormat.FontName = "Impact";

        //    tr.CharacterFormat.FontSize = 12;

        //    tr.CharacterFormat.TextColor = System.Drawing.Color.DarkBlue;

 

        //    //Save and Launch

        //    doc.SaveToFile(outputfile, Spire.Doc.FileFormat.Docx);

        //  //  System.Diagnostics.Process.Start(outputfile);
        //}
        ///// <summary>
        ///// pdfoutput=true ==> xuat ra file pdf; false thi xuat file docx
        ///// </summary>
        ///// <param name="filedoc"></param>
        ///// <param name="fileimage"></param>
        ///// <param name="outputfile"></param>
        ///// <param name="isFirstAdd"></param>
        ///// <param name="pdfoutput"></param>
        ///// <returns></returns>
        //private static void doc_UpdateFields(object sender, IFieldsEventArgs args)
        //{
        //    if (args is AskFieldEventArgs)
        //    {
        //        AskFieldEventArgs askArgs = args as AskFieldEventArgs;
        //        Console.WriteLine(askArgs.PromptText);
        //        string s = Console.ReadLine();
        //        askArgs.ResponseText = s;
        //    }
        //}
        //public string AddSinagureFooter(string filedoc, string fileimage, string outputfile)
        //{
        //    string kq = "OK";
        //    //Load Document
        // //   System.Globalization.CultureInfo cc = Thread.CurrentThread.CurrentCulture;
        //  //  Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
        //    Spire.Doc.Document doc = new Spire.Doc.Document();

        //    try
        //    {
        //        doc.LoadFromFile(filedoc);
        //    }
        //    catch {
        //        try
        //        {
        //            doc.LoadFromFile(filedoc,FileFormat.Docx);
        //        }
        //        catch { }
            
            
        //    }
        //  //  doc.UpdateFields += new UpdateFieldsHandler(doc_UpdateFields);
        //  //  doc.IsUpdateFields = true;

          
        //    //WebClient webClient = new WebClient();

        //    //MemoryStream ms = new MemoryStream(webClient.DownloadData(filedoc));
          
        //    //    doc.LoadFromStream(ms, FileFormat.Docx);
           
            
        //    doc.Comments.Clear();
           
        //    doc.EmbedSystemFonts = true;
        //    doc.AcceptChanges();
        //    doc.TrackChanges = false;
        // //   doc.ReplaceFirst
        //    //Header Paragraph
        //    int ssc = doc.Sections.Count;
        //    for (int i = 0; i < ssc; i++)
        //    {

        //        //first page header-footer
        //      //  Spire.Doc.HeaderFooter footer = doc.Sections[i].HeadersFooters.Footer;
        //        Spire.Doc.HeaderFooter firstPagefooter = doc.Sections[i].HeadersFooters.FirstPageFooter;
        //        // int soft = footer.Paragraphs.Count;
        //        if (firstPagefooter.Paragraphs.Count <= 0)//chua co footer
        //        {
        //            Spire.Doc.Documents.Paragraph para = firstPagefooter.AddParagraph();

        //            para.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;

        //            //Header Image

        //            Spire.Doc.Fields.DocPicture pic = para.AppendPicture(System.Drawing.Image.FromFile(fileimage));

        //            pic.Height = 22;

        //            pic.Width = 30;

        //        }
        //        else//da co footer thi them vao footer cuoi cung
        //        {
        //            Spire.Doc.Documents.Paragraph para = firstPagefooter.Paragraphs[firstPagefooter.Paragraphs.Count - 1];
        //            para.AppendText("   ");
        //            Spire.Doc.Fields.DocPicture pic = para.AppendPicture(System.Drawing.Image.FromFile(fileimage));

        //            pic.Height = 22;

        //            pic.Width = 30;
        //        }

        //        //Khac first page header-footer
        //        Spire.Doc.HeaderFooter footer = doc.Sections[i].HeadersFooters.Footer;
        //                     // int soft = footer.Paragraphs.Count;
        //        if (footer.Paragraphs.Count <= 0)//chua co footer
        //        {
        //            Spire.Doc.Documents.Paragraph para = footer.AddParagraph();

        //            para.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;

        //            //Header Image

        //            Spire.Doc.Fields.DocPicture pic = para.AppendPicture(System.Drawing.Image.FromFile(fileimage));

        //            pic.Height = 22;

        //            pic.Width = 30;

        //        }
        //        else//da co footer thi them vao footer cuoi cung
        //        {
        //            Spire.Doc.Documents.Paragraph para = footer.Paragraphs[footer.Paragraphs.Count - 1];
        //            para.AppendText("   ");
        //            Spire.Doc.Fields.DocPicture pic = para.AppendPicture(System.Drawing.Image.FromFile(fileimage));

        //            pic.Height = 22;

        //            pic.Width = 30;
        //        }
                
        //    }

        //    //Save and Launch
        //    if (System.IO.File.Exists(outputfile) == true)
        //    {
        //        System.IO.File.Delete(outputfile);

        //    }
        //    try
        //    {
                
        //            doc.SaveToFile(outputfile, Spire.Doc.FileFormat.Docx);
                    
             
        //    }
        //    catch (Exception e)
        //    {
        //        kq = e.Message;
        //    }
        //   // Thread.CurrentThread.CurrentCulture = cc;
        //    return kq;

        // //   System.Diagnostics.Process.Start(outputfile);
        //}
      //public struct ketquaSign
      //  {
      //      public bool bketqua;
      //      public string noidung;

      //  };
      
      public ketquaSign AddSinagureFooterWord1(string filedoc, string fileimage, string outputfile, float left, float top)
        {
            ketquaSign kq;
            kq.bketqua = false;
            kq.noidung = "";
            bool unlocked = false;
            Microsoft.Office.Interop.Word.Application oWord;
            //try
            //{
            //    oWord.Quit(false);
            //}
            //catch { }
            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false; //to avoid displaying the Word Application
            object strDocName = filedoc;
            object objBool = false;
            object objBoolTrue = true;
            object objNull = System.Reflection.Missing.Value;
            object FileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatUnicodeText;
            Cclass cls = new Cclass();
            string ext = cls.getExtention(strDocName.ToString());
            object outputfileunlock = strDocName.ToString().Replace(ext, "_Unlocked" + ext);
            //  Microsoft.Office.Interop.Word.Document oMyDoc=new Microsoft.Office.Interop.Word.Document();
            Microsoft.Office.Interop.Word.Document oMyDoc = null;
            WdProtectionType proctectedtype = Microsoft.Office.Interop.Word.WdProtectionType.wdNoProtection;
            object szPassword = "legalonly1";
            try
            {
                oMyDoc = oWord.Documents.Open(ref strDocName, ref objBool, ref objBool, ref objBool, ref objNull,
                         ref objNull, true, ref objNull, ref objNull, ref objNull, ref objNull, true, ref objNull, ref objNull, ref objNull, ref objNull);
                proctectedtype = oMyDoc.ProtectionType;
                if (proctectedtype != Microsoft.Office.Interop.Word.WdProtectionType.wdNoProtection)
                {
                    try
                    {
                        oMyDoc.Unprotect(szPassword);
                        oMyDoc.SaveAs2(outputfileunlock, objNull, objNull, objNull, objNull, objNull, objNull, true, objNull, objNull, objNull, true, objNull, objNull, objNull, objNull, objNull);
                        oMyDoc.Close(false);
                        oMyDoc = null;
                        oMyDoc = oWord.Documents.Open(ref outputfileunlock, ref objBool, ref objBool, ref objBool, ref objNull,
                              ref objNull, ref objBoolTrue, ref objNull, ref objNull, ref objNull, ref objNull, ref objBoolTrue, ref objNull, ref objNull, ref objNull, ref objNull);
                        unlocked = true;
                    }
                    catch
                    {
                    
                    }
                }
                
                oMyDoc.Activate();
                //Microsoft.Office.Interop.Word.WdProtectionType pr = oMyDoc.ProtectionType;
                //spr = pr.ToString();
            }
            catch (Exception e1)
            {
                kq.bketqua = false;
                kq.noidung = e1.Message;
            }


            if (oMyDoc.ProtectionType == Microsoft.Office.Interop.Word.WdProtectionType.wdNoProtection)
            {
                foreach (Microsoft.Office.Interop.Word.Section wordSection in oMyDoc.Sections)
                {
                    try
                    {
                        Microsoft.Office.Interop.Word.HeaderFooter HeaderfooterRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterFirstPage];
                        //if (HeaderfooterRange.IsHeader)
                        //{
                        Microsoft.Office.Interop.Word.Shape oshape1 = HeaderfooterRange.Shapes.AddPicture(fileimage, objNull, objNull, left, top, 30, 22, objNull);
                        // }
                    }
                    catch { }
                    try
                    {
                        Microsoft.Office.Interop.Word.HeaderFooter footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary];
                        //if (footerRange.IsHeader)
                        //{
                        Microsoft.Office.Interop.Word.Shape oshape = footerRange.Shapes.AddPicture(fileimage, objNull, objNull, left, top, 30, 22, objNull);
                        //}
                    }
                    catch { }
                }
                try
                {
                    //accapt all change
                    try
                    {
                        oMyDoc.AcceptAllRevisions();
                    }
                    catch { }
                    //delete display comment
                    try
                    {
                        oMyDoc.DeleteAllCommentsShown();
                    }
                    catch { }
                    try
                    {
                        oMyDoc.DeleteAllComments();
                    }
                    catch { }
                    try
                    {
                        oMyDoc.TrackRevisions = false;
                    }
                    catch { }
                    try
                    {

                        oMyDoc.TrackFormatting = false;
                    }
                    catch { }
                    if (unlocked == true)
                    {
                        //can Lock lai file vi file goc da bi lock

                        oMyDoc.Protect(proctectedtype, ref objBool, ref szPassword, ref objBool, ref objBoolTrue);
                        oMyDoc.SaveAs2(outputfile, objNull, objNull, objNull, objNull, objNull, objNull, true, objNull, objNull, objNull, true, objNull, objNull, objNull, objNull, objNull);
                  // outputfileunlock
                        oMyDoc.Close(false);
                        oWord.Quit(false);
                        System.IO.File.Delete(outputfileunlock.ToString());
                    }
                    else
                    {
                        oMyDoc.SaveAs2(outputfile, objNull, objNull, objNull, objNull, objNull, objNull, true, objNull, objNull, objNull, true, objNull, objNull, objNull, objNull, objNull);
                        oMyDoc.Close(false);
                        oWord.Quit(false);
                    }
                        // kq = "OK";
                    kq.bketqua = true;
                    kq.noidung = "OK";
                    //oMyDoc.Close(false);
                    //oWord.Quit(false);

                }
                catch (Exception e)
                {
                    oMyDoc.Close(false);
                    oWord.Quit(false);
                    //kq = e.Message;
                    kq.bketqua = false;
                    kq.noidung = e.Message;
                }
            }
            else
            {
              //  UnLock()
               
              //  oMyDoc.SaveAs2(outputfile, objNull, objNull, objNull, objNull, objNull, objNull, true, objNull, objNull, objNull, true, objNull, objNull, objNull, objNull, objNull);
                kq.bketqua = false;
                kq.noidung = "File has been protected "+proctectedtype.ToString();
                oMyDoc.Close(false);
                oWord.Quit(false);
            }

            return kq;
        } 
        public string AddSinagureFooterWord(string filedoc, string fileimage, string outputfile,float left,float top)
        {
            string kq = "FALSE";
            Microsoft.Office.Interop.Word.Application oWord;
            //try
            //{
            //    oWord.Quit(false);
            //}
            //catch { }
            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false; //to avoid displaying the Word Application
            object strDocName = filedoc;
            object objBool = false;
            object objNull = System.Reflection.Missing.Value;
            object FileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatUnicodeText;
       
           //  Microsoft.Office.Interop.Word.Document oMyDoc=new Microsoft.Office.Interop.Word.Document();
             Microsoft.Office.Interop.Word.Document oMyDoc = null;
           
             oMyDoc = oWord.Documents.Open(ref strDocName, ref objBool, ref objBool, ref objBool, ref objNull,
                      ref objNull, true, ref objNull, ref objNull, ref objNull, ref objNull, true, ref objNull, ref objNull, ref objNull, ref objNull);
             oMyDoc.Activate();
             Microsoft.Office.Interop.Word.WdProtectionType pr = oMyDoc.ProtectionType;
             string spr = pr.ToString();
             //if (spr.ToLower() != "wdnoprotection")
             //{
             //    object objpass = "123456";
             //    try
             //    {
             //        oMyDoc.Unprotect(ref objpass);
             //        pr = oMyDoc.ProtectionType;
             //        spr = pr.ToString();
             //    }
             //    catch { }
             //}

             if (oMyDoc.HasPassword == false && spr.ToLower()== "wdnoprotection")
             {
                 foreach (Microsoft.Office.Interop.Word.Section wordSection in oMyDoc.Sections)
                 {
                                       
                         try
                         {
                             Microsoft.Office.Interop.Word.HeaderFooter HeaderfooterRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterFirstPage];
                             //if (HeaderfooterRange.IsHeader)
                             //{
                                 Microsoft.Office.Interop.Word.Shape oshape1 = HeaderfooterRange.Shapes.AddPicture(fileimage, objNull, objNull, left, top, 30, 22, objNull);
                            // }
                         }
                         catch { }
                         try
                         {
                             Microsoft.Office.Interop.Word.HeaderFooter footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary];
                             //if (footerRange.IsHeader)
                             //{
                                 Microsoft.Office.Interop.Word.Shape oshape = footerRange.Shapes.AddPicture(fileimage, objNull, objNull, left, top, 30, 22, objNull);
                             //}
                         }
                         catch { }
                     
                 }
                 try
                 {
                     //accapt all change
                     try
                     {
                         oMyDoc.AcceptAllRevisions();

                     }
                     catch { }
                     //delete display comment
                     try
                     {
                         oMyDoc.DeleteAllCommentsShown();

                     }
                     catch { }
                     try
                     {

                         oMyDoc.DeleteAllComments();

                     }
                     catch { }
                     try
                     {

                         oMyDoc.TrackRevisions = false;
                     }
                     catch { }
                     try
                     {

                         oMyDoc.TrackFormatting = false;
                     }
                     catch { }
                     oMyDoc.SaveAs2(outputfile, objNull, objNull, objNull, objNull, objNull, objNull, true, objNull, objNull, objNull, true, objNull, objNull, objNull, objNull, objNull);
                     kq = "OK";
                     oMyDoc.Close(false);
                     oWord.Quit(false);

                 }
                 catch (Exception e)
                 {
                     oMyDoc.Close(false);
                     oWord.Quit(false);
                     kq = e.Message;
                 }
             }
             else
             {
                // kq = "Password has been protected";
                 oMyDoc.SaveAs2(outputfile, objNull, objNull, objNull, objNull, objNull, objNull, true, objNull, objNull, objNull, true, objNull, objNull, objNull, objNull, objNull);
                 kq = "OK";
                 oMyDoc.Close(false);
                 oWord.Quit(false);
             }
           
            return kq;
        } 
        public string Doc2PdfSave(string pathdocfile, string pathpdfout)
        {
            string kq = "OK";
            try
            {
              //  byte[] docfile = ReadFile(pathdocfile);


                object oMissing = System.Reflection.Missing.Value;
              // System.IO.File.WriteAllBytes(pathpdfout, docfile);
                Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document wordDocument = appWord.Documents.Open(pathdocfile, oMissing, true, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);

               

                //Tim chu de xoa
                Microsoft.Office.Interop.Word.Paragraphs paragraphs = null;
                paragraphs = wordDocument.Paragraphs;
                if (paragraphs.First.Range.Text.ToLower().Trim() == "evaluation warning: the document was created with spire.doc for .net.")
                {
                    wordDocument.Paragraphs.First.Range.Delete(oMissing, oMissing);
                }

                try
                {
                    wordDocument.TrackFormatting = false;
                   
                   
                }
                catch { }
                try
                {
                    wordDocument.DeleteAllCommentsShown();

                }
                catch { }
                try
                {
                   // wordDocument.TrackFormatting = false;
                    wordDocument.AcceptAllRevisions();
                    
                }
                catch { }
                try
                {
                    // wordDocument.TrackFormatting = false;
                    wordDocument.AcceptAllRevisionsShown();

                }
                catch { }
                wordDocument.SaveAs2(pathpdfout, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF, oMissing, oMissing, oMissing, oMissing, oMissing, true, oMissing, oMissing,oMissing,true, oMissing, oMissing, oMissing, oMissing, oMissing);

             object saveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
             ((Microsoft.Office.Interop.Word._Document)wordDocument).Close(ref saveChanges, ref oMissing, ref oMissing);
             wordDocument = null;
             ((Microsoft.Office.Interop.Word._Application)appWord).Quit(ref oMissing, ref oMissing, ref oMissing);
             appWord = null;
            }
            catch (Exception e)
            {

                kq = e.Message;
                Exception realerror = e;
                while (realerror.InnerException != null)
                    kq += realerror.InnerException;

            }
            return kq;
        }
     //   Them 20 Jan 2017
       private Microsoft.Office.Interop.Word.ApplicationClass MSdoc;
        object Unknown = Type.Missing;
        public string word2PDF(object Source, object Target)
        {   //Creating the instance of Word Application  
            string kq = "OK";
           // if (MSdoc == null) MSdoc = new Microsoft.Office.Interop.Word.ApplicationClass();

            Microsoft.Office.Interop.Word.Application oWord;
             oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false; //to avoid displaying the Word Application
            Microsoft.Office.Interop.Word.Document oMyDoc = null;
            try
            {
                //MSdoc.Visible = false;

                oMyDoc=oWord.Documents.Open(ref Source, ref Unknown,
                     ref Unknown, ref Unknown, ref Unknown,
                     ref Unknown, ref Unknown, ref Unknown,
                     ref Unknown, ref Unknown, ref Unknown,
                     ref Unknown, ref Unknown, ref Unknown, ref Unknown, ref Unknown);
               // MSdoc.Application.Visible = false;
               // MSdoc.WindowState = Microsoft.Office.Interop.Word.WdWindowState.wdWindowStateMinimize;
                oMyDoc.Activate();
                object format = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;
                oMyDoc.SaveAs2(ref Target, ref format,
                        ref Unknown, ref Unknown, ref Unknown,
                        ref Unknown, ref Unknown, ref Unknown,
                        ref Unknown, ref Unknown, ref Unknown,
                        ref Unknown, ref Unknown, ref Unknown,
                       ref Unknown, ref Unknown);
                //MSdoc.ActiveDocument.SaveAs(ref Target, ref format,
                //        ref Unknown, ref Unknown, ref Unknown,
                //        ref Unknown, ref Unknown, ref Unknown,
                //        ref Unknown, ref Unknown, ref Unknown,
                //        ref Unknown, ref Unknown, ref Unknown,
                //       ref Unknown, ref Unknown);
            }
            catch (Exception e)
            {
               // MessageBox.Show(e.Message);
                kq = e.Message;
            }
            finally
            {
                //if (MSdoc != null)
                //{
                //    MSdoc.Documents.Close(false, ref Unknown, ref Unknown);
                   
                //    //WordDoc.Application.Quit(ref Unknown, ref Unknown, ref Unknown); 
                //}
                //// for closing the application
                //MSdoc.Quit(false, ref Unknown, ref Unknown);

                oMyDoc.Close(false);
                oWord.Quit(false);
            }
            return kq;
        }
        //Them ngay 20 Jan 2017
        public string AddSinagureFooterWord2(object Source, string fileimage, object Target, float left, float top)
        {
            string kq = "OK";
            if (MSdoc == null) MSdoc = new Microsoft.Office.Interop.Word.ApplicationClass();

            try
            {
                MSdoc.Visible = false;

                MSdoc.Documents.Open(ref Source, ref Unknown,
                     ref Unknown, ref Unknown, ref Unknown,
                     ref Unknown, ref Unknown, ref Unknown,
                     ref Unknown, ref Unknown, ref Unknown,
                     ref Unknown, ref Unknown, ref Unknown, ref Unknown, ref Unknown);
                MSdoc.Application.Visible = false;
                MSdoc.WindowState = Microsoft.Office.Interop.Word.WdWindowState.wdWindowStateMinimize;

                object format = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatUnicodeText;

                foreach (Microsoft.Office.Interop.Word.Section wordSection in MSdoc.ActiveDocument.Sections)
                {
                    Microsoft.Office.Interop.Word.HeaderFooter footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary];
                    Microsoft.Office.Interop.Word.Shape oshape = footerRange.Shapes.AddPicture(fileimage, ref Unknown, ref Unknown, left, top, 30, 22, ref Unknown);

                }
                try
                {
                    MSdoc.ActiveDocument.AcceptAllRevisions();

                }
                catch { }
                //delete display comment
                try
                {
                    MSdoc.ActiveDocument.DeleteAllCommentsShown();

                }
                catch { }
                try
                {

                    MSdoc.ActiveDocument.DeleteAllComments();

                }
                catch { }
                try
                {

                    MSdoc.ActiveDocument.TrackRevisions = false;
                }
                catch { }
                try
                {

                    MSdoc.ActiveDocument.TrackFormatting = false;
                }
                catch { }

                MSdoc.ActiveDocument.SaveAs(ref Target, ref format,
                        ref Unknown, ref Unknown, ref Unknown,
                        ref Unknown, ref Unknown, ref Unknown,
                        ref Unknown, ref Unknown, ref Unknown,
                        ref Unknown, ref Unknown, ref Unknown,
                       ref Unknown, ref Unknown);
            }
            catch (Exception e)
            {
                // MessageBox.Show(e.Message);
                kq = e.Message;
            }
            finally
            {
                if (MSdoc != null)
                {
                    MSdoc.Documents.Close(false, ref Unknown, ref Unknown);
                    //WordDoc.Application.Quit(ref Unknown, ref Unknown, ref Unknown); 
                }
                // for closing the application
                MSdoc.Quit(false, ref Unknown, ref Unknown);
            }
            return kq;
        }
        public string UnLock(object fileToOpen, object szPassword, object outputfile)
        {
            Microsoft.Office.Interop.Word.Application oWord;
            object objMiss = System.Reflection.Missing.Value;
            oWord = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document oMyDoc = null;
            // objApp = new Word.Application();
            oMyDoc = oWord.Documents.Open(ref fileToOpen, ref objMiss, ref objMiss, ref objMiss, ref objMiss,
                ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss,
                ref objMiss, ref objMiss, ref objMiss, ref objMiss, ref objMiss);

            if (oMyDoc.ProtectionType != Microsoft.Office.Interop.Word.WdProtectionType.wdNoProtection)
            {
                oMyDoc.Unprotect(ref szPassword);
                oMyDoc.SaveAs2(outputfile, objMiss, objMiss, objMiss, objMiss, objMiss, objMiss, true, objMiss, objMiss, objMiss, true, objMiss, objMiss, objMiss, objMiss, objMiss);
                //  oMyDoc.Save();
                //MessageBox.Show("Word document UnProtected successfully !", "Word Protect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                oMyDoc.Close(false);
                oWord.Quit(false);
                return outputfile.ToString();
            }
            else
            {
                oMyDoc.Close(false);
                oWord.Quit(false);
                return fileToOpen.ToString();
                // MessageBox.Show("Selected word document is not protected !", "Word Protect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool Extract(string zipfile, string ouputfolder)
        {
            bool kq = false;
           
            try
            {
                using (ZipFile zip = ZipFile.Read(zipfile))
                {
                    foreach (ZipEntry e in zip)
                    {
                        e.Extract(ouputfolder);
                    }
                    //duy chuyen tat ca file ra thu muc goc
                    DirSearch(ouputfolder, ouputfolder);

                    //xoa cac thu muc sau khi da di chuyen file
                    foreach (var directory in Directory.GetDirectories(ouputfolder))
                    {
                        //processDirectory(directory);
                        if (Directory.GetFiles(directory).Length == 0 &&  Directory.GetDirectories(directory).Length == 0)
                        {
                            try
                            {
                                Directory.Delete(directory, false);
                            }
                            catch { }
                        }
                    }

                }
                kq = true;
            }
            catch
            {
                kq = false;
            }
            return kq;
        }
        public bool Archive(string inputfolder, string ouputzipfile)
           
        {
            bool kq = false;
            if (System.IO.File.Exists(ouputzipfile))
            {
                try
                {
                    System.IO.File.Delete(ouputzipfile);
                }
                catch { }
            }
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.UseUnicodeAsNecessary = true;  // utf-8
                    zip.AddDirectory(inputfolder);
                 //   zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                    zip.Save(ouputzipfile);
                    
                }
                kq = true;
            }
            catch
            {
                kq = false;
            }
            return kq;
        }
        private void DirSearch(string sDir,string disfolder)
        {
            Cclass cls =new Cclass();
           
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                  //  files.Add(f);
                    string disfile=disfolder+"/"+cls.getFileName(f);
                    if(f.ToLower()!=disfile.ToLower())
                    {
                        System.IO.File.Move(f, disfile);
                    }
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                   DirSearch(d, disfolder);
                }
            }
            catch
            {
                
            }

           
        }
    }
