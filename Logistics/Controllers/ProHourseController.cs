using Logistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class ProHourseController : Controller
    {
        //
        // GET: /Hourse/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(ProHourseModel hourse)
        {
            JsonResult json = new JsonResult() { ContentType = "text/html" };
            int result = 0;

            string message = ValidateInput(hourse);
            if (!string.IsNullOrEmpty(message))
            {
                json.Data = new { Result = 0, Message = message };
                return json;
            }
            try
            {
                if (hourse.ProHouseType == "1")
                {
                    hourse.Address = string.Format("{0}{1}{2}期{3}幢{4}室", hourse.Address0, hourse.Address1, hourse.Address2, hourse.Address3, hourse.Address4);
                }
                if (hourse.ProHouseType == "2")
                {
                    hourse.Address = string.Format("{0}{1}{2}期{3}幢", hourse.Address0, hourse.Address1, hourse.Address2, hourse.Address3);
                }
                if (hourse.ProHouseType == "3")
                {
                    hourse.Address = string.Format("{0}{1}镇{2}村{3}", hourse.Address0, hourse.Address1, hourse.Address6, hourse.Address5);
                }
                result = ServiceModel.CreateInstance().Client.AddProHourse(ServiceModel.CreateInstance().UserName, hourse.MachineType, hourse.Address, hourse.CustomerName, hourse.CustomerTel, float.Parse(hourse.Price));
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

        private string ValidateInput(ProHourseModel hourse)
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(hourse.MachineType))
            {
                message = "请选择机型方案";
                return message;
            }
            if (hourse.ProHouseType == "0")
            {
                message = "请选择住房类型";
                return message;
            }
            if (hourse.ProHouseType == "1")
            {
                if (string.IsNullOrEmpty(hourse.Address1) ||
                    string.IsNullOrEmpty(hourse.Address2) ||
                    string.IsNullOrEmpty(hourse.Address3) ||
                    string.IsNullOrEmpty(hourse.Address4))
                {
                    message = "项目地址不完整";
                    return message;
                }
            }
            if (hourse.ProHouseType == "2")
            {
                if (string.IsNullOrEmpty(hourse.Address1) ||
                    string.IsNullOrEmpty(hourse.Address2) ||
                    string.IsNullOrEmpty(hourse.Address3))
                {
                    message = "项目地址不完整";
                    return message;
                }
            }
            if (hourse.ProHouseType == "3")
            {
                if (string.IsNullOrEmpty(hourse.Address1) ||
                    string.IsNullOrEmpty(hourse.Address6) ||
                    string.IsNullOrEmpty(hourse.Address5))
                {
                    message = "项目地址不完整，自建房请写详细地址！";
                    return message;
                }
            }
            if (string.IsNullOrEmpty(hourse.CustomerName))
            {
                message = "客户姓名未填写！";
                return message;
            }
            if (string.IsNullOrEmpty(hourse.CustomerTel))
            {
                message = "联系电话未填写！";
                return message;
            }
            if (!Regex.IsMatch(hourse.CustomerTel, "[\\d-]+"))
            {
                message = "联系电话的格式不正确！";
                return message;
            }
            if (string.IsNullOrEmpty(hourse.Price))
            {
                message = "项目初报价金额未填写！";
                return message;
            }
            float price = 0;
            if (!float.TryParse(hourse.Price, out price))
            {
                message = "项目初报价金额的格式不正确！";
                return message;
            }
            return message;
        }
    }
}
