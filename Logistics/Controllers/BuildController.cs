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
            BuildModel build = new BuildModel();
            firstLetter = string.IsNullOrEmpty(firstLetter) ? string.Empty : firstLetter;
            try
            {
                DataSet dst = ServiceModel.CreateInstance().Client.GetBuild(firstLetter);
                if (dst != null && dst.Tables.Count > 0)
                {
                    foreach (DataRow drow in dst.Tables[0].Rows)
                    {
                        build.FirstLetter.Add(drow[0].ToString());
                    }
                    foreach (DataRow drow in dst.Tables[1].Rows)
                    {
                        build.Build.Add(new BuildInfo() { ID = int.Parse(drow[0].ToString()), Name = drow[1].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View(build);
        }

    }
}
