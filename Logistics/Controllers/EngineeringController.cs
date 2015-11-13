using Logistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class EngineeringController : Controller
    {
        //
        // GET: /Engineering/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(EngineeringModel engineering)
        {
            JsonResult json = new JsonResult() { ContentType = "text/html" };
            int result = 0;

            string message = ValidateInput(engineering);
            if (!string.IsNullOrEmpty(message))
            {
                json.Data = new { Result = 0, Message = message };
                return json;
            }
            try
            {

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

        private string ValidateInput(EngineeringModel engineering)
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(engineering.EngineeringName))
            {
                message = "请填写工程名称";
                return message;
            }
            if (string.IsNullOrEmpty(engineering.Uses))
            {
                message = "请填写项目用途";
                return message;
            }
            if (string.IsNullOrEmpty(engineering.Address))
            {
                message = "请填写项目地址";
                return message;
            }
            if (string.IsNullOrEmpty(engineering.CustomerName))
            {
                message = "请填写客户姓名";
                return message;
            }
            if (string.IsNullOrEmpty(engineering.CustomerTel))
            {
                message = "请填写联系电话";
                return message;
            }
            if (string.IsNullOrEmpty(engineering.Price))
            {
                message = "请填写项目初报价金额";
                return message;
            }
            if (Regex.IsMatch(engineering.Price, "^[0-9]+([.]{1}[0-9]+){0,1}$"))
            {
                message = "项目初报价金额的格式不正确";
                return message;
            }
            return message;
        }
    }
}
