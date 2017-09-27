using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;

using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Web.SessionState;

/// <summary>
/// Summary description for PrintHelper
/// </summary>
public class PrintHelper
{
    public PrintHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void PrintWebControl(Control ctrl)
    {
        
        PrintWebControl(ctrl, string.Empty);
    }

    public static void PrintWebControl(Control ctrl, string Script)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ctrl is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        if (Script != string.Empty)
        {
            pg.ClientScript.RegisterStartupScript(pg.GetType(), "PrintJavaScript", Script);
        }
        
        string tagstyle = "<head>"+
                            "<style>" +
                            ".lblTitleReport"+
                            "{" +
	                            "border: 1px none #888888;" +
	                            "text-align: center;" +
	                            "font-family: verdana, Arial, Helvetica;" +
                                "font-size: x-large;" +
	                            "font-weight: bold;" +
                                "background-color: #EAEAEA;" +
	                            "color: #4C7A9E;" +
	                            "padding: 2px;" +
	                            "padding-left: 8px;" +
	                            "padding-top: 8px;" +
	                            "padding-bottom: 8px;" +
	                            "margin: 0px;" +
	                            "text-transform:uppercase;" +
                            "}" +
                            "body" +
                            "{" +
	                            "border: 1px none #888888;" +
	                            "text-align: center;" +
                                "font-family: Verdana, Arial,Tahoma;" +
                                "font-size: 8px;" +
	                            "padding: 2px;" +
	                            "padding-left: 8px;" +
	                            "padding-top: 8px;" +
	                            "padding-bottom: 8px;" +
	                            "margin: 0px;" +	                            
                            "}" +
                            "</style>"+
                            "</head>";
                            
        //HtmlHead hd = new HtmlHead(tagstyle);
        //pg.Controls.Add(hd);
        //hd.Attributes.Add("runat", "server");        
        HtmlForm frm = new HtmlForm();
        
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");        
        frm.Controls.Add(ctrl);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
       
        string strHTML = tagstyle; 
        strHTML = strHTML + stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();
    }
    //public static void PrintWebControlNoBorder(Control ctrl, string Script)
    //{
    //    StringWriter stringWrite = new StringWriter();
    //    System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
    //    if (ctrl is WebControl)
    //    {
    //        Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
    //    }
    //    Page pg = new Page();
    //    pg.EnableEventValidation = false;
    //    if (Script != string.Empty)
    //    {
    //        pg.ClientScript.RegisterStartupScript(pg.GetType(), "PrintJavaScript", Script);
    //    }

    //    string tagstyle = "<head>" +
    //                        "<style>" +
    //                        ".lblTitleReport" +
    //                        "{" +
    //                            "border: 0px none #888888;" +
    //                            "text-align: center;" +
    //                            "font-family: verdana, Arial, Helvetica;" +
    //                            "font-size: x-large;" +
    //                            "font-weight: bold;" +
    //                            "background-color: #EAEAEA;" +
    //                            "color: #4C7A9E;" +
    //                            "padding: 2px;" +
    //                            "padding-left: 8px;" +
    //                            "padding-top: 8px;" +
    //                            "padding-bottom: 8px;" +
    //                            "margin: 0px;" +
    //                            "text-transform:uppercase;" +
    //                        "}" +
    //                        "body" +
    //                        "{" +
    //                             "border: 0px none #888888;" +
    //                            "text-align: center;" +
    //                            "font-family: Verdana, Arial,Tahoma;" +
    //                            "font-size: 8px;" +
    //                            "padding: 2px;" +
    //                            "padding-left: 8px;" +
    //                            "padding-top: 8px;" +
    //                            "padding-bottom: 8px;" +
    //                            "margin: 0px;" +
    //                        "}" +
    //                        "</style>" +
    //                        "</head>";

    //    //HtmlHead hd = new HtmlHead(tagstyle);
    //    //pg.Controls.Add(hd);
    //    //hd.Attributes.Add("runat", "server");        
    //    HtmlForm frm = new HtmlForm();
    //    pg.Controls.Add(frm);
    //    frm.Attributes.Add("runat", "server");
    //    frm.Controls.Add(ctrl);
    //    pg.DesignerInitialize();
    //    pg.RenderControl(htmlWrite);
    //    string strHTML = tagstyle;
    //    strHTML = strHTML + stringWrite.ToString();
    //    HttpContext.Current.Response.Clear();
    //    HttpContext.Current.Response.Write(strHTML);
    //    HttpContext.Current.Response.Write("<script>window.print();</script>");
    //    HttpContext.Current.Response.End();
    //}
    public static void PrintHopDong(Control ctrl)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ctrl is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;

        string tagstyle = "";
        //string tagstyle = "<head>" +
        //                    "<style>" +
        //                    ".lblTitleReport" +
        //                    "{" +
        //                        "border: 1px none #888888;" +
        //                        "text-align: center;" +
        //                        "font-family: verdana, Arial, Helvetica;" +
        //                        "font-size: x-large;" +
        //                        "font-weight: bold;" +
        //                        "background-color: #EAEAEA;" +
        //                        "color: #4C7A9E;" +
        //                        "padding: 2px;" +
        //                        "padding-left: 8px;" +
        //                        "padding-top: 8px;" +
        //                        "padding-bottom: 8px;" +
        //                        "margin: 0px;" +
        //                        "text-transform:uppercase;" +
        //                    "}" +
        //                    "body" +
        //                    "{" +
        //                        "border: 1px none #888888;" +
        //                        "text-align: center;" +
        //                        "font-family: Verdana, Arial,Tahoma;" +
        //                        "font-size: 8px;" +
        //                        "padding: 2px;" +
        //                        "padding-left: 8px;" +
        //                        "padding-top: 8px;" +
        //                        "padding-bottom: 8px;" +
        //                        "margin: 0px;" +
        //                    "}" +
        //                    "</style>" +
        //                    "</head>";

         
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ctrl);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = tagstyle;
        strHTML = strHTML + stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();
    }
}
