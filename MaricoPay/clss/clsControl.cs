using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Specialized;
using System.Drawing;
public class clsControl
{
    #region Biến
    private string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };
    #endregion
    public Control fFindControlRecursive(Control control, string id)
    {
        if (control == null) return null;
        Control ctrl = control.FindControl(id);
        if (ctrl == null)
        {
            foreach (Control child in control.Controls)
            {
                ctrl = fFindControlRecursive(child, id);
                if (ctrl != null) break;
            }
        }
        return ctrl;
    }
    public string fGioiHanSoTu(string Doan, int SoTu)
    {
        string[] Str = Doan.Split(' ');
        if (Str.Length > SoTu)
        {
            Doan = "";
            for (int i = 0; i < SoTu; i++)
            {
                Doan += ' ' + Str[i];
            }
            Doan += "...";
        }
        return Doan;
    }
    public string fRemoveSign4VietnameseString(string Str)
    {
        for (int i = 1; i < VietnameseSigns.Length; i++)
        {
            for (int j = 0; j < VietnameseSigns[i].Length; j++)
                Str = Str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
        }
        return Str;
    }
    public bool fKiemTraFileHinh(string DuoiHinh)
    {
        if (DuoiHinh == ".jpg" || DuoiHinh == ".jpeg" || DuoiHinh == ".gif" || DuoiHinh == ".png")
        {
            return true;
        }
        return false;
    }
    public string fTaoMa(string Value)
    {
        return fRemoveSign4VietnameseString(Value).Replace(" ", "_");
    }
    public string fMaHoaPassWord(string Value)
    {
        byte[] Encode = Encoding.UTF8.GetBytes(Value);
        MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
        byte[] EnPass = MD5.ComputeHash(Encode);
        return BitConverter.ToString(EnPass);
    }
    public string fLang(Literal Text)
    {
        System.Web.SessionState.HttpSessionState Session = HttpContext.Current.Session;
        if (Session["LangFrontend"] == null)
            Session["LangFrontend"] = "vi";
        return Text.Text = (string)HttpContext.GetGlobalResourceObject("~/lang/"+Session["LangFrontend"].ToString(), Text.ID);
    }
    public void fFindLiteral(Control control)
    {
        foreach (Control child in control.Controls)
        {
            if (child is Literal)
            {
                Literal ctr = child as Literal;
                ctr.Text = fLang(ctr);
            }
        }
    }
    public string GetResource(string vl)
    {
        
        System.Web.SessionState.HttpSessionState Session = HttpContext.Current.Session;
        return (string)HttpContext.GetGlobalResourceObject(Session["LangFrontend"].ToString(), vl);
    }
    public Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
    {
        Bitmap result = new Bitmap(nWidth, nHeight);
        using (Graphics g = Graphics.FromImage((System.Drawing.Image)result))
            g.DrawImage(b, 0, 0, nWidth, nHeight);
        return result;
    }
    public void ResizeImage(string OriginalFile, string NewFile, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
    {
        System.Drawing.Image FullsizeImage = System.Drawing.Image.FromFile(OriginalFile);

        // Prevent using images internal thumbnail
        FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
        FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

        if (OnlyResizeIfWider)
        {
            if (FullsizeImage.Width <= NewWidth)
            {
                NewWidth = FullsizeImage.Width;
                //FullsizeImage.Width = NewWidth;
            }
        }

        int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;
        if (NewHeight > MaxHeight)
        {
            // Resize with height instead
            NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
            NewHeight = MaxHeight;
        }

        System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

        // Clear handle to original file so that we can overwrite it if necessary
        FullsizeImage.Dispose();

        // Save resized picture
        NewImage.Save(NewFile);
    }

    /// <summary>
/// POST data and Redirect to the specified url using the specified page.
/// </summary>
/// <param name="page">The page which will be the referrer page.</param>
/// <param name="destinationUrl">The destination Url to which
/// the post and redirection is occuring.</param>
/// <param name="data">The data should be posted.</param>
/// <Author>Samer Abu Rabie</Author>

public void RedirectAndPOST(Page page, string destinationUrl, 
                                   NameValueCollection data)
{
//Prepare the Posting form
string strForm = PreparePOSTForm(destinationUrl, data);
//Add a literal control the specified page holding 
//the Post Form, this is to submit the Posting form with the request.
page.Controls.Add(new LiteralControl(strForm));
}
    /// <summary>
/// This method prepares an Html form which holds all data
/// in hidden field in the addetion to form submitting script.
/// </summary>
/// <param name="url">The destination Url to which the post and redirection
/// will occur, the Url can be in the same App or ouside the App.</param>
/// <param name="data">A collection of data that
/// will be posted to the destination Url.</param>
/// <returns>Returns a string representation of the Posting form.</returns>
/// <Author>Samer Abu Rabie</Author>

private static String PreparePOSTForm(string url, NameValueCollection data)
{
    //Set a name for the form
    string formID = "PostForm";
    //Build the form using the specified data to be posted.
    StringBuilder strForm = new StringBuilder();
    strForm.Append("<form id=\"" + formID + "\" name=\"" + 
                   formID + "\" action=\"" + url + 
                   "\" method=\"POST\">");

    foreach (string key in data)
    {
        strForm.Append("<input type=\"hidden\" name=\"" + key + 
                       "\" value=\"" + data[key] + "\">");
    }

    strForm.Append("</form>");
    //Build the JavaScript which will do the Posting operation.
    StringBuilder strScript = new StringBuilder();
    strScript.Append("<script language='javascript'>");
    strScript.Append("var v" + formID + " = document." + 
                     formID + ";");
    strScript.Append("v" + formID + ".submit();");
    strScript.Append("</script>");
    //Return the form and the script concatenated.
    //(The order is important, Form then JavaScript)
    return strForm.ToString() + strScript.ToString();
}
    
}
