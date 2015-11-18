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
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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

            return SqlHelper.CreateInstance().GetDataSet(parameters, "GetUser");
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="engineeringName"></param>
        /// <param name="uses"></param>
        /// <param name="address"></param>
        /// <param name="customerName"></param>
        /// <param name="customerTel"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public int AddProject(string userName, string projectName, string projectUses, string machineType, string projectAddress, string customerName, string customerTel, float price, string projectStatus, string projectType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter = new SqlParameter("username", SqlDbType.VarChar, 20);
            parameter.Value = userName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("projectName", SqlDbType.VarChar, 200);
            parameter.Value = projectName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("projectUses", SqlDbType.VarChar, 6);
            parameter.Value = projectUses;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("machineType", SqlDbType.VarChar, 8);
            parameter.Value = machineType;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("projectAddress", SqlDbType.VarChar, 500);
            parameter.Value = projectAddress;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("customerName", SqlDbType.VarChar, 20);
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

            parameter = new SqlParameter("projectStatus", SqlDbType.VarChar, 20);
            parameter.Value = projectStatus;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("projectType", SqlDbType.VarChar, 8);
            parameter.Value = projectType;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            return Convert.ToInt32(SqlHelper.CreateInstance().ExecuteScalar(parameters, "AddProject"));
        }

        /// <summary>
        /// 获取小区集合
        /// </summary>
        /// <param name="firstLetter"></param>
        /// <returns></returns>
        public DataSet GetBuild(string firstLetter)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter = new SqlParameter("firstLetter", SqlDbType.Char, 1);
            parameter.Value = firstLetter;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            return SqlHelper.CreateInstance().GetDataSet(parameters, "GetBuild");
        }

        public DataSet GetProject(string projectStatus, string customerName, string customerTel, string projectAddress, string projectType, string machineType, string startDate, string endDate)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter parameter = new SqlParameter("projectStatus", SqlDbType.VarChar, 20);
            parameter.Value = projectStatus;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("customerName", SqlDbType.VarChar, 20);
            parameter.Value = customerName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("customerTel", SqlDbType.VarChar, 11);
            parameter.Value = customerTel;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("projectAddress", SqlDbType.VarChar, 500);
            parameter.Value = projectAddress;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("projectType", SqlDbType.VarChar, 8);
            parameter.Value = projectType;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("machineType", SqlDbType.VarChar, 8);
            parameter.Value = machineType;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("startDate", SqlDbType.VarChar, 10);
            parameter.Value = startDate;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("endDate", SqlDbType.VarChar, 10);
            parameter.Value = endDate;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            return SqlHelper.CreateInstance().GetDataSet(parameters, "GetProject");
        }
    }
}
