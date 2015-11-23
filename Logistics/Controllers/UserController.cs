using Logistics.Filters;
using Logistics.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        [ActionAuthentication]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionAuthentication]
        public JsonResult Query(UserModel user)
        {
            JsonResult json = new JsonResult() { ContentType = "text/html" };
            try
            {
                user.UserName = string.IsNullOrEmpty(user.UserName) ? string.Empty : user.UserName;
                DataSet dst = ServiceModel.CreateInstance().Client.GetAllUser(ServiceModel.CreateInstance().UserName,user.UserName);
                if (dst == null) return null;
                if (dst.Tables.Count != 1) return null;
                if (Convert.ToInt32(dst.Tables[0].Rows[0][0]) == -1) return null;
                var data = from row in dst.Tables[0].AsEnumerable()
                           select new UserQueryModel()
                           {
                               id = Convert.ToInt32(row["id"]),
                               uname = row["uname"].ToString().Trim(),
                               rname = row["rname"].ToString().Trim(),
                               phone = row["phone"].ToString().Trim(),
                               isadmin = Convert.ToBoolean( row["isadmin"])?"是":"否"
                           };
                json.Data = new { total = Convert.ToInt32(dst.Tables[0].Rows[0][0]), rows = data };
            }
            catch { }
            return json;
        }

        private string ValidateInput(UserModel user)
        {
            string message = string.Empty;
            if (user.UserName.Length < 4 || user.UserName.Length > 20)
            {
                message = "用户名为4-20位字符";
                return message;
            }
            if (user.RealName.Length < 4 || user.RealName.Length > 20)
            {
                message = "真实姓名为4-20位字符";
                return message;
            }
            if (user.Phone.Length < 11 || user.Phone.Length > 20)
            {
                message = "联系电话为11-20位字符";
                return message;
            }
            if (!Regex.IsMatch(user.Phone, "[\\d-]+"))
            {
                message = "联系电话的格式不正确！";
                return message;
            }
            return message;
        }

        [HttpPost]
        [ActionAuthentication]
        public JsonResult Add(UserModel user)
        {
            JsonResult json = new JsonResult() { ContentType = "text/html" };
            int result = 0;
            string message = ValidateInput(user);
            if (!string.IsNullOrEmpty(message))
            {
                json.Data = new { Result = result, Message = message };
                return json;
            }
            try
            {
                user.Password = string.IsNullOrEmpty(user.Password) ? "123456" : user.Password;
                user.Password = Md5Encrypt.CreateInstance().Encrypt(user.Password);
                result = ServiceModel.CreateInstance().Client.AddUser(ServiceModel.CreateInstance().UserName, user.UserName, user.RealName, user.Phone, user.IsAdmin);
                switch (result)
                {
                    case -1:
                        message = "没有权限";
                        break;
                    case 0:
                        message = "添加失败";
                        break;
                    case 1:
                        message = "添加成功";
                        break;
                }
            }
            catch (Exception ex)
            {
                result = 0;
                message = ex.Message;
            }
            json.Data = new { Result = result, Message = message };
            return json;
        }

        [HttpPost]
        [ActionAuthentication]
        public JsonResult Modify(UserModel user)
        {
            JsonResult json = new JsonResult() { ContentType = "text/html" };
            int result = 0;
            string message = ValidateInput(user);
            if (!string.IsNullOrEmpty(message))
            {
                json.Data = new { Result = result, Message = message };
                return json;
            }
            try
            {
                result = ServiceModel.CreateInstance().Client.ModifyUser(ServiceModel.CreateInstance().UserName, user.UserID,user.UserName,user.RealName,user.Phone,user.IsAdmin);
                switch (result)
                {
                    case -1:
                        message = "没有权限";
                        break;
                    case 0:
                        message = "修改失败";
                        break;
                    case 1:
                        message = "修改成功";
                        break;
                }
            }
            catch (Exception ex)
            {
                result = 0;
                message = ex.Message;
            }
            json.Data = new { Result = result, Message = message };
            return json;
        }

        [HttpPost]
        [ActionAuthentication]
        public JsonResult Delete(UserModel user)
        {
            JsonResult json = new JsonResult() { ContentType = "text/html" };
            int result = 0;
            string message = string.Empty;
            try
            {
                result = ServiceModel.CreateInstance().Client.DeleteUser(ServiceModel.CreateInstance().UserName, user.UserID);
                switch (result)
                {
                    case -1:
                        message = "没有权限";
                        break;
                    case 0:
                        message = "删除失败";
                        break;
                    case 1:
                        message = "删除成功";
                        break;
                }
            }
            catch (Exception ex)
            {
                result = 0;
                message = ex.Message;
            }
            json.Data = new { Result = result, Message = message };
            return json;
        }

        [HttpPost]
        [ActionAuthentication]
        public JsonResult Current(UserModel user)
        {
            JsonResult json = new JsonResult() { ContentType = "text/html" };
            if (string.IsNullOrEmpty(ServiceModel.CreateInstance().CurrentUser))
            {
                try
                {
                    DataSet dst = ServiceModel.CreateInstance().Client.GetCurrentUser(ServiceModel.CreateInstance().UserName);
                    if (dst == null) return null;
                    if (dst.Tables.Count != 2) return null;
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow drow in dst.Tables[1].Rows)
                    {
                        sb.Append(drow[0].ToString());
                        sb.Append("  ");
                    }
                    ServiceModel.CreateInstance().CurrentUser = string.Format("登录用户:{0}&nbsp;&nbsp;&nbsp;", dst.Tables[0].Rows[0][0].ToString());
                    ServiceModel.CreateInstance().CurrentAdmin = string.Format("系统管理员:{0}", sb.ToString().Trim());
                }
                catch { }
            }
            json.Data = new { current = ServiceModel.CreateInstance().CurrentUser, admin = ServiceModel.CreateInstance().CurrentAdmin };
            return json;
        }

    }
}
