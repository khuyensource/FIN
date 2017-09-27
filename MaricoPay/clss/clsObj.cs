using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace Data
{
    public class clsObj
    {
        int ketQua = 0;
        public int KetQua
        {
            get { return ketQua; }
            set { ketQua = value; }
        }

        int maLoi = 0;
        public int MaLoi
        {
            get { return maLoi; }
            set { maLoi = value; }
        }

        string cnnString = ConfigurationManager.ConnectionStrings["FINConnectionString"].ToString();
        public string CnnString
        {
            get { return cnnString; }
            set { cnnString = value; }
        }

        SqlCommand cm = new SqlCommand();
        public SqlCommand Cm
        {
            get { return cm; }
            set { cm = value; }
        }

        DataSet ds = new DataSet();
        public DataSet Ds
        {
            get { return ds; }
            set { ds = value; }
        }

        DataTable dt = new DataTable();
        public DataTable Dt
        {
            get { return dt; }
            set { dt = value; }
        }

        string spName;
        public string SpName
        {
            get { return spName; }
            set { spName = value; }
        }

        string[] parameter = null;
        public string[] Parameter
        {
            set { parameter = value; }
            get { return parameter; }
        }

        object[] valueParameter;
        public object[] ValueParameter
        {
            set { valueParameter = value; }
            get { return valueParameter; }
        }

        string[] parameterOutput = null;
        public string[] ParameterOutput
        {
            set { parameterOutput = value; }
            get { return parameterOutput; }
        }

        object[] valueOutput;
        public object[] ValueOutput
        {
            set { valueOutput = value; }
            get { return valueOutput; }
        }
    }
}
