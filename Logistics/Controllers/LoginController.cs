using Logistics.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View(new UserModel());
        }

        [HttpPost]
        public ActionResult Index(UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            if (!ValidateInput(user))
            {
                return View(user);
            }
            LogisticsService.Service1Client client = new LogisticsService.Service1Client();
            string password = new Md5Crypto().GetMd5(user.Password);
            DataSet dst = client.UserLogin(user.UserName, password);
            if (dst == null || dst.Tables.Count == 0)
            {
                ViewBag.ErrorMessage = "用户名或密码错误";
                return View(user);
            }
            HttpCookie cookie = new HttpCookie("User");
            cookie.Values.Add("UserName", user.UserName);
            cookie.Values.Add("Password", password);
            Response.Cookies.Add(cookie);

            return Content("OK");
        }

        private bool ValidateInput(UserModel user)
        {
            if (string.IsNullOrEmpty(user.UserName))
            {
               ViewBag.ErrorMessage = "用户名不能为空";
                return false;
            }
            if (user.UserName.Length < 4 || user.UserName.Length > 20)
            {
                ViewBag.ErrorMessage = "用户名为4-20位字符";
                return false;
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                ViewBag.ErrorMessage = "密码不能为空";
                return false;
            }
            if (user.Password.Length < 6 || user.Password.Length > 20)
            {
                ViewBag.ErrorMessage = "密码为6-20位字符";
                return false;
            }
            if (string.IsNullOrEmpty(user.Code))
            {
                ViewBag.ErrorMessage = "请输入验证码";
                return false;
            }
            if (Session["VerificationCode"] == null || string.IsNullOrEmpty(Session["VerificationCode"].ToString()))
            {
                ViewBag.ErrorMessage = "请输入验证码";
                return false;
            }
            else if(Session["VerificationCode"].ToString() != user.Code.ToUpper())
            {
                ViewBag.ErrorMessage = "验证码输入不正确";
                return false;
            }
            return true;
        }

        public FileContentResult GetVerificationCode()
        {
            VerificationCode code = new VerificationCode();
            code.GetVerificationCode(6);
            Session["VerificationCode"] = code.Code;
            return File(code.CodeBuffer, @"image/jpeg");
        }

    }
}