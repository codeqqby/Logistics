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

            return SqlHelper.CreateInstance().GetDataSet(parameters);
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

            return Convert.ToInt32(SqlHelper.CreateInstance().ExecuteScalar(parameters));
        }


        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
