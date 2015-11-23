using Logistics.Filters;
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
        [ActionAuthentication]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionAuthentication]
        public JsonResult Add(ProjectModel project)
        {
            if (Session[CookieModel.UserName.ToString()] == null || string.IsNullOrEmpty(Session[CookieModel.UserName.ToString()].ToString()))
            {
                Redirect("Login/Index");
                return null;
            }
            JsonResult json = new JsonResult() { ContentType = "text/html" };
            int result = 0;

            string message = ValidateInput(project);
            if (!string.IsNullOrEmpty(message))
            {
                json.Data = new { Result = 0, Message = message };
                return json;
            }
            try
            {
                if (project.ProHouseType == "1")
                {
                    project.ProjectAddress = string.Format("{0}{1}{2}期{3}幢{4}室", project.Address0, project.Address1, project.Address2, project.Address3, project.Address4);
                }
                if (project.ProHouseType == "2")
                {
                    project.ProjectAddress = string.Format("{0}{1}{2}期{3}幢", project.Address0, project.Address1, project.Address2, project.Address3);
                }
                if (project.ProHouseType == "3")
                {
                    project.ProjectAddress = string.Format("{0}{1}镇{2}村{3}", project.Address0, project.Address1, project.Address6, project.Address5);
                }
                project.ProjectName = string.Empty;
                project.ProjectUses = string.Empty;
                project.ProjectType = GetProHouseType(project.ProHouseType);
                project.ProjectStatus = "登录成功";
                result = ServiceModel.CreateInstance().Client.AddProject(Session[CookieModel.UserName.ToString()].ToString(), project.ProjectName, project.ProjectUses, project.MachineType, project.ProjectAddress, project.CustomerName, project.CustomerTel, float.Parse(project.Price), project.ProjectStatus, project.ProjectType);
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

        private string ValidateInput(ProjectModel project)
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(project.MachineType))
            {
                message = "请选择机型方案";
                return message;
            }
            if (project.ProHouseType == "0")
            {
                message = "请选择住房类型";
                return message;
            }
            if (project.ProHouseType == "1")
            {
                if (string.IsNullOrEmpty(project.Address1) ||
                    string.IsNullOrEmpty(project.Address2) ||
                    string.IsNullOrEmpty(project.Address3) ||
                    string.IsNullOrEmpty(project.Address4))
                {
                    message = "项目地址不完整";
                    return message;
                }
            }
            if (project.ProHouseType == "2")
            {
                if (string.IsNullOrEmpty(project.Address1) ||
                    string.IsNullOrEmpty(project.Address2) ||
                    string.IsNullOrEmpty(project.Address3))
                {
                    message = "项目地址不完整";
                    return message;
                }
            }
            if (project.ProHouseType == "3")
            {
                if (string.IsNullOrEmpty(project.Address1) ||
                    string.IsNullOrEmpty(project.Address6) ||
                    string.IsNullOrEmpty(project.Address5))
                {
                    message = "项目地址不完整，自建房请写详细地址！";
                    return message;
                }
            }
            if (string.IsNullOrEmpty(project.CustomerName))
            {
                message = "客户姓名未填写！";
                return message;
            }
            if (string.IsNullOrEmpty(project.CustomerTel))
            {
                message = "联系电话未填写！";
                return message;
            }
            if (!Regex.IsMatch(project.CustomerTel, "[\\d-]+"))
            {
                message = "联系电话的格式不正确！";
                return message;
            }
            if (string.IsNullOrEmpty(project.Price))
            {
                message = "项目初报价金额未填写！";
                return message;
            }
            float price = 0;
            if (!float.TryParse(project.Price, out price))
            {
                message = "项目初报价金额的格式不正确！";
                return message;
            }
            return message;
        }

        private string GetProHouseType(string type)
        {
            if (type == "1") 
            {
                type = "小区";
            }
            else if (type == "2")
            {
                type = "别墅";
            }
            else if (type == "3") 
            {
                type = "自建";
            }
            return string.Format("家装{0}", type);
        }
    }
}
