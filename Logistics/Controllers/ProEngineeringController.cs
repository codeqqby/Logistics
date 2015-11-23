using Logistics.Filters;
using Logistics.Models;
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class ProEngineeringController : Controller
    {
        //
        // GET: /project/
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
                project.ProjectAddress = string.Format("{0}市{1}(乡、镇、街道){2}(路、街){3}(号、大厦){4}楼{5}", project.Area1, project.Area2, project.Area3, project.Area4, project.Area5, project.Area6);
                project.MachineType = string.Empty;
                project.ProjectType = "工程";
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
            if (string.IsNullOrEmpty(project.ProjectName))
            {
                message = "工程名称未填写！";
                return message;
            }
            if (project.ProjectUses == "项目用途选择")
            {
                message = "项目用途未选择！";
                return message;
            }
            if (project.Area2 == "乡镇选择")
            {
                message = "项目地址的乡镇未选择！";
                return message;
            }
            if (string.IsNullOrEmpty(project.Area3))
            {
                message = "项目地址的道路未填！";
                return message;
            }
            if (string.IsNullOrEmpty(project.Area4))
            {
                message = "项目地址的号码牌、大厦未填！";
                return message;
            }
            if (string.IsNullOrEmpty(project.Area5))
            {
                message = "项目地址的楼层未填，如无请填1楼！";
                return message;
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
    }
}
