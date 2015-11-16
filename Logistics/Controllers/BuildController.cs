using Logistics.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class BuildController : Controller
    {
        //
        // GET: /Build/
        public ActionResult Index(string firstLetter)
        {
            ServiceModel.CreateInstance().FirstLetter = firstLetter == null ? string.Empty : firstLetter;
            return View();
        }

        [HttpPost]
        public JsonResult GetFirstLetter()
        {
            JsonResult json = new JsonResult() { ContentType = "text/html" };
            string data = string.Empty;
            try
            {
                DataSet dst = ServiceModel.CreateInstance().Client.GetBuild(ServiceModel.CreateInstance().FirstLetter);
                if (dst != null && dst.Tables.Count > 0)
                {
                    data=JsonConvert.SerializeObject(dst.Tables[0]);
                }
            }
            catch (Exception ex)
            {
               
            }
            json.Data = data;
            return json;
        }

    }
}
