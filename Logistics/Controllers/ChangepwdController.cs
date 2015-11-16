using Logistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class ChangepwdController : Controller
    {
        //
        // GET: /Changpwd/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ChangePassword(UserModel user)
        {
            JsonResult json = new JsonResult() { ContentType = "text/html" };
            int result = 0;
            user.UserName = ServiceModel.CreateInstance().UserName;
            string message = ValidateInput(user);
            if (!string.IsNullOrEmpty(message))
            {
                json.Data = new { Result = 0, Message = message };
                return json;
            }
            try
            {
                user.Password = Md5Encrypt.CreateInstance().Encrypt(user.Password);
                user.Password_New = Md5Encrypt.CreateInstance().Encrypt(user.Password_New);
                result = ServiceModel.CreateInstance().Client.ModifyPassword(user.UserName, user.Password, user.Password_New);
                switch (result)
                { 
                    case -1:
                        message = "没有权限";
                        break;
                    case 0:
                        message = "旧密码输入不正确";
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

        private string ValidateInput(UserModel user)
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(user.Password))
            {
                message = "旧密码不能为空";
                return message;
            }
            if (user.Password.Length < 6 || user.Password.Length > 20)
            {
                message = "旧密码为6-20位字符";
                return message;
            }
            if (string.IsNullOrEmpty(user.Password_New))
            {
                message = "新密码不能为空";
                return message;
            }
            if (user.Password_New.Length < 6 || user.Password_New.Length > 20)
            {
                message = "新密码为6-20位字符";
                return message;
            }
            if (string.IsNullOrEmpty(user.Password_Confirm))
            {
                message = "确认密码不能为空";
                return message;
            }
            if (user.Password_Confirm.Length < 6 || user.Password_Confirm.Length > 20)
            {
                message = "确认密码为6-20位字符";
                return message;
            }
            if ( user.Password_New!= user.Password_Confirm)
            {
                message = "新密码和确认密码输入不一致";
                return message;
            }
            return message;
        }
    }
}
