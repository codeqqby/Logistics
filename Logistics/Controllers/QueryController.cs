using Logistics.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class QueryController : Controller
    {
        //
        // GET: /Query/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetProject(ProjectModel project)
        {
            JsonResult json = new JsonResult() { ContentType = "text/html" };
            int result = 0;
            string message = string.Empty;
            try
            {
                project.ProjectStatus=string.IsNullOrEmpty(project.ProjectStatus)?string.Empty:project.ProjectStatus;
                project.CustomerName = string.IsNullOrEmpty(project.CustomerName) ? string.Empty : project.CustomerName;
                project.CustomerTel = string.IsNullOrEmpty(project.CustomerTel) ? string.Empty : project.CustomerTel;
                project.ProjectAddress = string.IsNullOrEmpty(project.ProjectAddress) ? string.Empty : project.ProjectAddress;
                project.ProjectType = string.IsNullOrEmpty(project.ProjectType) ? string.Empty : project.ProjectType;
                project.MachineType = string.IsNullOrEmpty(project.MachineType) ? string.Empty : project.MachineType;
                project.StartDate = string.IsNullOrEmpty(project.StartDate) ? string.Empty : project.StartDate;
                project.EndDate = string.IsNullOrEmpty(project.EndDate) ? string.Empty : project.EndDate;
                DataSet dst = ServiceModel.CreateInstance().Client.GetProject(project.ProjectStatus, project.CustomerName, project.CustomerTel, project.ProjectAddress, project.ProjectType, project.MachineType, project.StartDate, project.EndDate);
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
    }
}
