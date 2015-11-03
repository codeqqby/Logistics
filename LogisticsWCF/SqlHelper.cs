using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LogisticsWCF
{
    public class SqlHelper
    {
        private static SqlHelper uniqueInstance;
        private static readonly object locker = new object();
        private string connectionString;

        private SqlHelper()
        {
            string constring = ConfigurationManager.AppSettings["sqlcon"];
            this.connectionString = DESEncrypt.CreateInstance().Decrypt(constring);
        }

        public static SqlHelper CreateInstance()
        {
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new SqlHelper();
                    }
                }
            }
            return uniqueInstance;
        }

        public DataSet GetDataSet(List<SqlParameter> parameters)
        {
            DataSet dst = null;
            try
            {
                using (SqlConnection con = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GetUser";
                    cmd.Parameters.AddRange(parameters.ToArray());
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    con.Open();
                    dst = new DataSet();
                    sda.Fill(dst);
                    con.Close();
                }
            }
            catch { }
            return dst;
        }
    }
}