using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Data
{
    public class clsSql
    {
        public void fNonGetData_Out(clsObj Data)
        {
            clsDB Db = new clsDB();
            Data.Cm.CommandType = CommandType.StoredProcedure;
            Data.Cm.CommandText = Data.SpName;
            Db.AddParameters(Data);//Bien thuong
            Db.AddParametersOutput(Data);//Bien Output
            Db.NonGetData(Data);
            Db.ReturnValueOutput(Data);//Lay gia tri bien Output            
        }
        public void fNonGetData_OnlyOut(clsObj Data)
        {
            clsDB Db = new clsDB();
            Data.Cm.CommandType = CommandType.StoredProcedure;
            Data.Cm.CommandText = Data.SpName;
            Db.AddParametersOutput(Data);//Bien Output
            Db.NonGetData(Data);
            Db.ReturnValueOutput(Data);//Lay gia tri bien Output            
        }
        public void fNonGetData(clsObj Data)
        {
            clsDB Db = new clsDB();
            Data.Cm.CommandType = CommandType.StoredProcedure;
            Data.Cm.CommandText = Data.SpName;
            Db.AddParameters(Data);//Bien thuong
            Db.NonGetData(Data);
        }
        public void fGetData(clsObj Data)
        {
            clsDB Db = new clsDB();
            Data.Cm.CommandType = CommandType.StoredProcedure;
            Data.Cm.CommandText = Data.SpName;
            Data.Cm.CommandTimeout = 5000;
            Db.AddParameters(Data);//Bien thuong
            Db.GetData(Data);
        }
        public void fGetDataSet(clsObj Data)
        {
            clsDB Db = new clsDB();
            Data.Cm.CommandType = CommandType.StoredProcedure;
            Data.Cm.CommandText = Data.SpName;
            Db.AddParameters(Data);//Bien thuong
            Db.GetDataSet(Data);
        }
    }
}