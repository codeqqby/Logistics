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
        private string outputName = "result";

        #region 用户
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
        /// 查询所有用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataSet GetAllUser(string userName, string name)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter = new SqlParameter("username", SqlDbType.VarChar, 20);
            parameter.Value = userName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("name", SqlDbType.VarChar, 20);
            parameter.Value = name;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            return SqlHelper.CreateInstance().GetDataSet(parameters, "GetAllUser");
        }

        /// <summary>
        /// 查询管理员
        /// </summary>
        /// <returns></returns>
        public DataSet GetCurrentUser(string userName)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
           
            SqlParameter parameter = new SqlParameter("username", SqlDbType.VarChar, 20);
            parameter.Value = userName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            return SqlHelper.CreateInstance().GetDataSet(parameters, "GetCurrentUser");
        }

        public int AddUser(string userName, string name, string realName, string phone, byte isAdmin)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter = new SqlParameter("username", SqlDbType.VarChar, 20);
            parameter.Value = userName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("name", SqlDbType.VarChar, 20);
            parameter.Value = name;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("realName", SqlDbType.VarChar, 20);
            parameter.Value = realName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("phone", SqlDbType.VarChar, 20);
            parameter.Value = phone;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("isAdmin", SqlDbType.Bit);
            parameter.Value = isAdmin;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter(outputName, SqlDbType.Int);
            parameter.Direction = ParameterDirection.Output;
            parameters.Add(parameter);

            return Convert.ToInt32(SqlHelper.CreateInstance().ExecuteNonQuery(parameters, "AddUser", outputName));
        }

        public int ModifyUser(string userName,int userID, string name, string realName,string phone, byte isAdmin)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter = new SqlParameter("username", SqlDbType.VarChar, 20);
            parameter.Value = userName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("userID", SqlDbType.Int);
            parameter.Value = userID;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("name", SqlDbType.VarChar, 20);
            parameter.Value = name;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("realName", SqlDbType.VarChar, 20);
            parameter.Value = realName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("phone", SqlDbType.VarChar, 20);
            parameter.Value = phone;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("isAdmin", SqlDbType.Bit);
            parameter.Value = isAdmin;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter(outputName, SqlDbType.Int);
            parameter.Direction = ParameterDirection.Output;
            parameters.Add(parameter);

            return Convert.ToInt32(SqlHelper.CreateInstance().ExecuteNonQuery(parameters, "ModifyUser", outputName));
        }

        public int DeleteUser(string userName, int userID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter = new SqlParameter("username", SqlDbType.VarChar, 20);
            parameter.Value = userName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("userID", SqlDbType.Int);
            parameter.Value = userID;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter(outputName, SqlDbType.Int);
            parameter.Direction = ParameterDirection.Output;
            parameters.Add(parameter);

            return Convert.ToInt32(SqlHelper.CreateInstance().ExecuteNonQuery(parameters, "DeleteUser", outputName));
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

            parameter = new SqlParameter(outputName, SqlDbType.Int);
            parameter.Direction = ParameterDirection.Output;
            parameters.Add(parameter);

            return Convert.ToInt32(SqlHelper.CreateInstance().ExecuteNonQuery(parameters, "ModifyPassword", outputName));
        }
        #endregion

        #region 项目
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

            parameter = new SqlParameter(outputName, SqlDbType.Int);
            parameter.Direction = ParameterDirection.Output;
            parameters.Add(parameter);

            return Convert.ToInt32(SqlHelper.CreateInstance().ExecuteNonQuery(parameters, "AddProject",outputName));
        }

        /// <summary>
        /// 修改项目登录状态
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="projectID"></param>
        /// <param name="projectStatus"></param>
        /// <returns></returns>
        public int ModifyProjectStatus(string userName, int projectID, string projectStatus)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            
            SqlParameter parameter = new SqlParameter("username", SqlDbType.VarChar, 20);
            parameter.Value = userName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("projectID", SqlDbType.Int);
            parameter.Value = projectID;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("projectStatus", SqlDbType.VarChar, 20);
            parameter.Value = projectStatus;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter(outputName, SqlDbType.Int);
            parameter.Direction = ParameterDirection.Output;
            parameters.Add(parameter);

            return Convert.ToInt32(SqlHelper.CreateInstance().ExecuteNonQuery(parameters, "ModifyProjectStatus", outputName));
        }

        /// <summary>
        /// 查询项目集合
        /// </summary>
        /// <param name="projectStatus"></param>
        /// <param name="customerName"></param>
        /// <param name="customerTel"></param>
        /// <param name="projectAddress"></param>
        /// <param name="projectType"></param>
        /// <param name="machineType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public DataSet GetProject(string userName, string projectStatus, string customerName, string customerTel, string projectAddress, string projectType, string machineType, string startDate, string endDate, int page, int rows)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter parameter = new SqlParameter("username", SqlDbType.VarChar, 20);
            parameter.Value = userName;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("projectStatus", SqlDbType.VarChar, 20);
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

            parameter = new SqlParameter("page", SqlDbType.Int);
            parameter.Value = page;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            parameter = new SqlParameter("rows", SqlDbType.Int);
            parameter.Value = rows;
            parameter.Direction = ParameterDirection.Input;
            parameters.Add(parameter);

            return SqlHelper.CreateInstance().GetDataSet(parameters, "GetProject");
        }
        #endregion

        #region 小区
        /// <summary>
        /// 查询小区集合
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
        #endregion
    }
}
