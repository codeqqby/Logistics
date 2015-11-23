using Logistics.Filters;
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
        [ActionAuthentication]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionAuthentication]
        public JsonResult GetProject(ProjectModel project)
        {
            if (Session[CookieModel.UserName.ToString()] == null || string.IsNullOrEmpty(Session[CookieModel.UserName.ToString()].ToString()))
            {
                Redirect("Login/Index");
                return null;
            }
            JsonResult json = new JsonResult() { ContentType = "text/html" };
            try
            {
                project.ProjectStatus = string.IsNullOrEmpty(project.ProjectStatus) ? string.Empty : project.ProjectStatus;
                project.CustomerName = string.IsNullOrEmpty(project.CustomerName) ? string.Empty : project.CustomerName;
                project.CustomerTel = string.IsNullOrEmpty(project.CustomerTel) ? string.Empty : project.CustomerTel;
                project.ProjectAddress = string.IsNullOrEmpty(project.ProjectAddress) ? string.Empty : project.ProjectAddress;
                project.ProjectType = string.IsNullOrEmpty(project.ProjectType) ? string.Empty : project.ProjectType;
                project.MachineType = string.IsNullOrEmpty(project.MachineType) ? string.Empty : project.MachineType;
                project.StartDate = string.IsNullOrEmpty(project.StartDate) ? string.Empty : project.StartDate;
                project.EndDate = string.IsNullOrEmpty(project.EndDate) ? string.Empty : project.EndDate;
                int page = 0;
                if (!int.TryParse(HttpContext.Request.Params["page"], out page))
                {
                    page = 1;
                }
                page = page == 0 ? 1 : page;
                int rows = 0;
                if (!int.TryParse(HttpContext.Request.Params["rows"], out rows))
                {
                    rows = 50;
                }
                rows = rows == 0 ? 50 : rows;
                DataSet dst = ServiceModel.CreateInstance().Client.GetProject(Session[CookieModel.UserName.ToString()].ToString(),project.ProjectStatus, project.CustomerName, project.CustomerTel, project.ProjectAddress, project.ProjectType, project.MachineType, project.StartDate, project.EndDate, page, rows);
                if (dst == null) return null;
                if (dst.Tables.Count != 2) return null;
                var data = from row in dst.Tables[0].AsEnumerable()
                           select new ProjectQueryModel()
                           {
                               createtime = row["createtime"].ToString().Trim(),
                               pname = row["pname"].ToString().Trim(),
                               paddress = row["paddress"].ToString().Trim(),
                               price = row["price"].ToString().Trim(),
                               customer = row["customer"].ToString().Trim(),
                               pstatus = row["pstatus"].ToString().Trim(),
                               view = row["view"].ToString().Trim()
                           };
                json.Data = new { total = Convert.ToInt32(dst.Tables[1].Rows[0][0]), rows = data };
            }
            catch { }
            return json;
        }

        [HttpPost]
        [ActionAuthentication]
        public JsonResult ModifyProjectStatus(ProjectModel project)
        {
            if (Session[CookieModel.UserName.ToString()] == null || string.IsNullOrEmpty(Session[CookieModel.UserName.ToString()].ToString()))
            {
                Redirect("Login/Index");
                return null;
            }
            JsonResult json = new JsonResult() { ContentType = "text/html" };
            int result = 0;
            string message = string.Empty;
            try
            {
                project.ProjectStatus = string.IsNullOrEmpty(project.ProjectStatus) ? string.Empty : project.ProjectStatus;
                project.ProjectStatus = project.ProjectStatus == "1" ? "登录成功" : "登录失败";
                result = ServiceModel.CreateInstance().Client.ModifyProjectStatus(Session[CookieModel.UserName.ToString()].ToString(), project.ProjectID, project.ProjectStatus);
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
    }
}
