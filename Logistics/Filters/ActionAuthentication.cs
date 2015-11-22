using Logistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Logistics.Filters
{
    public class ActionAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session[CookieModel.UserName.ToString()] == null ||
                filterContext.HttpContext.Session[CookieModel.Password.ToString()] == null)
            {
                filterContext.Result = new RedirectResult("Login/Index");
            }
            base.OnActionExecuting(filterContext);
        }

    }
}