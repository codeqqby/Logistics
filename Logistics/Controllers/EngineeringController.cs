using Logistics.Models;
using System;
using System.Text.RegularExpressions;
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
                engineering.Address = string.Format("{0}市{1}(乡、镇、街道){2}(路、街){3}(号、大厦){4}楼{5}", engineering.Area1, engineering.Area2, engineering.Area3, engineering.Area4, engineering.Area5, engineering.Area6);
                result = ServiceModel.CreateInstance().Client.AddEngineering(ServiceModel.CreateInstance().UserName, engineering.EngineeringName, engineering.Uses, engineering.Address, engineering.CustomerName, engineering.CustomerTel, float.Parse(engineering.Price));
                switch (result)
                {
                    case -1:
                        message = "没有权限";
                        break;
                    case 0:
                        message = "登录项目失败";
                        break;
                    case 1:
                        message = "登录项目成功";
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
            if (engineering.Uses=="0")
            {
                message = "请选择项目用途";
                return message;
            }
            if (engineering.Area1=="0")
            {
                message = "请填写项目地址";
                return message;
            }
            if (engineering.Area2 == "0")
            {
                message = "请选择乡镇";
                return message;
            }
            if (string.IsNullOrEmpty(engineering.Area3))
            {
                message = "请填写路、街";
                return message;
            }
            if (string.IsNullOrEmpty(engineering.Area4))
            {
                message = "请填写号、大厦";
                return message;
            }
            if (string.IsNullOrEmpty(engineering.Area5))
            {
                message = "请填写楼";
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
            if (!Regex.IsMatch(engineering.CustomerTel, "[\\d-]+"))
            {
                message = "联系电话的格式不正确";
                return message;
            }
            if (string.IsNullOrEmpty(engineering.Price))
            {
                message = "请填写项目初报价金额";
                return message;
            }
            float price = 0;
            if (!float.TryParse(engineering.Price, out price))
            {
                message = "项目初报价金额的格式不正确";
                return message;
            }
            return message;
        }
    }
}
