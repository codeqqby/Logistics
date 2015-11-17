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

            //this.connectionString = "server=127.0.0.1;database=logistics;user id=sa;password=sa";
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

        public DataSet GetDataSet(List<SqlParameter> parameters,string procedureName)
        {
            DataSet dst = null;
            try
            {
                using (SqlConnection con = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;
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

        public object ExecuteScalar(List<SqlParameter> parameters, string procedureName)
        {
            object result = null;
            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedureName;
                cmd.Parameters.AddRange(parameters.ToArray());
                try
                {
                    con.Open();
                    result = cmd.ExecuteScalar();
                }
                catch { }
                finally
                {
                    con.Close();
                }
            }
            return result;
        }
    }
}