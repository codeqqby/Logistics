using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LogisticsWCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    public class Service1 : IService1
    {
        public DataSet UserLogin(string userName, string password)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter = new SqlParameter("username", SqlDbType.VarChar, 20);
            parameter.Value = userName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("password", SqlDbType.VarChar, 32);
            parameter.Value = password;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            return SqlHelper.CreateInstance().GetDataSet(parameters,"GetUser");
        }

        public int ModifyPassword(string userName, string password, string newPassword)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter = new SqlParameter("username", SqlDbType.VarChar, 20);
            parameter.Value = userName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("password", SqlDbType.VarChar, 32);
            parameter.Value = password;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("newpassword", SqlDbType.VarChar, 32);
            parameter.Value = newPassword;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            return Convert.ToInt32(SqlHelper.CreateInstance().ExecuteScalar(parameters, "ModifyPassword"));
        }

        public int AddEngineering(string userName, string engineeringName, string uses, string address, string customerName, string customerTel, float price)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter = new SqlParameter("username", SqlDbType.VarChar, 20);
            parameter.Value = userName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("engineeringName", SqlDbType.VarChar, 250);
            parameter.Value = engineeringName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("uses", SqlDbType.VarChar, 10);
            parameter.Value = uses;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("address", SqlDbType.VarChar, 500);
            parameter.Value = address;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("customerName", SqlDbType.VarChar, 10);
            parameter.Value = customerName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("customerTel", SqlDbType.VarChar, 11);
            parameter.Value = customerTel;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("price", SqlDbType.Float);
            parameter.Value = price;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            return Convert.ToInt32(SqlHelper.CreateInstance().ExecuteScalar(parameters, "AddEngineering"));
        }

        public DataSet GetFirstLetter()
        {
            return SqlHelper.CreateInstance().GetDataSet(new List<SqlParameter>(), "GetFirstLetter");
        }

        public DataSet GetBuild(string firstLetter)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter = new SqlParameter("firstLetter", SqlDbType.Char, 1);
            parameter.Value = firstLetter;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            return SqlHelper.CreateInstance().GetDataSet(parameters, "GetBuild");
        }
    }
}
