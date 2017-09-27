using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


public class clsLang : System.Web.UI.UserControl
{
    clsControl Ctrl = new clsControl();
    protected override void OnInit(EventArgs e)
    {
        Ctrl.fFindLiteral(this);
    }
}
public class clsLangpage : System.Web.UI.Page
{
    clsControl Ctrl = new clsControl();
    protected override void OnInit(EventArgs e)
    {
        Ctrl.fFindLiteral(this);
    }
}