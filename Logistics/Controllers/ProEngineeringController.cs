using Logistics.Models;
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class ProEngineeringController : Controller
    {
        //
        // GET: /Engineering/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(ProEngineeringModel engineering)
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
                result = ServiceModel.CreateInstance().Client.AddProEngineering(ServiceModel.CreateInstance().UserName, engineering.EngineeringName, engineering.Uses, engineering.Address, engineering.CustomerName, engineering.CustomerTel, float.Parse(engineering.Price));
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

        private string ValidateInput(ProEngineeringModel engineering)
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(engineering.EngineeringName))
            {
                message = "工程名称未填写！";
                return message;
            }
            if (engineering.Uses == "项目用途选择")
            {
                message = "项目用途未选择！";
                return message;
            }
            if (engineering.Area2 == "乡镇选择")
            {
                message = "项目地址的乡镇未选择！";
                return message;
            }
            if (string.IsNullOrEmpty(engineering.Area3))
            {
                message = "项目地址的道路未填！";
                return message;
            }
            if (string.IsNullOrEmpty(engineering.Area4))
            {
                message = "项目地址的号码牌、大厦未填！";
                return message;
            }
            if (string.IsNullOrEmpty(engineering.Area5))
            {
                message = "项目地址的楼层未填，如无请填1楼！";
                return message;
            }
            if (string.IsNullOrEmpty(engineering.CustomerName))
            {
                message = "客户姓名未填写！";
                return message;
            }
            if (string.IsNullOrEmpty(engineering.CustomerTel))
            {
                message = "联系电话未填写！";
                return message;
            }
            if (!Regex.IsMatch(engineering.CustomerTel, "[\\d-]+"))
            {
                message = "联系电话的格式不正确！";
                return message;
            }
            if (string.IsNullOrEmpty(engineering.Price))
            {
                message = "项目初报价金额未填写！";
                return message;
            }
            float price = 0;
            if (!float.TryParse(engineering.Price, out price))
            {
                message = "项目初报价金额的格式不正确！";
                return message;
            }
            return message;
        }
    }
}
