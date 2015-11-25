using Logistics.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Logistics.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies.Get(CookieModel.Logistics_User_Cookie.ToString());
            if (cookie == null)
            {
                return View(new UserModel());
            }
            if (cookie[CookieModel.UserName.ToString()] != null && cookie[CookieModel.Password.ToString()] != null)
            {
                UserModel user = new UserModel()
                {
                    UserName = cookie[CookieModel.UserName.ToString()],
                    Password = DESEncrypt.CreateInstance().Decrypt(cookie[CookieModel.Password.ToString()]),
                    RememberMe = true
                };
                DataSet dst = ServiceModel.CreateInstance().Client.UserLogin(user.UserName, user.Password);
                if (dst == null || dst.Tables.Count == 0)
                {
                    ViewBag.ErrorMessage = "用户名或密码错误";
                    return View(user);
                }
                user.Password = cookie[CookieModel.Password.ToString()];
                AddHttpContextItems(user);
                Session[CookieModel.UserName.ToString()] = user.UserName;
                return RedirectToAction("Index","Home");
            }
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
            string password = Md5Encrypt.CreateInstance().Encrypt(user.Password);
            DataSet dst = ServiceModel.CreateInstance().Client.UserLogin(user.UserName, password);
            if (dst == null || dst.Tables.Count == 0)
            {
                ViewBag.ErrorMessage = "用户名或密码错误";
                return View(user);
            }
            if (user.RememberMe)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, true, FormsAuthentication.FormsCookiePath);
                FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddTicks(FormsAuthentication.Timeout.Ticks), false, JsonConvert.SerializeObject(user));
                string hashTicket = FormsAuthentication.Encrypt(Ticket);

                HttpCookie cookie = new HttpCookie(CookieModel.Logistics_User_Cookie.ToString(), hashTicket);
                cookie[CookieModel.UserName.ToString()] = user.UserName;
                password = DESEncrypt.CreateInstance().Encrypt(password);
                cookie[CookieModel.Password.ToString()] = password;
                cookie.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(cookie);
            }
            else
            {
                HttpCookie cookie = new HttpCookie(CookieModel.Logistics_User_Cookie.ToString());
                cookie.Expires = DateTime.Now.AddMonths(-1);
                Request.Cookies.Add(cookie);
                cookie[CookieModel.UserName.ToString()] = null;
                cookie[CookieModel.Password.ToString()] = null;
                Response.Cookies.Add(cookie);
            }
            user.Password = DESEncrypt.CreateInstance().Encrypt(Md5Encrypt.CreateInstance().Encrypt(user.Password));
            AddHttpContextItems(user);
            Session[CookieModel.UserName.ToString()] = user.UserName;
            return RedirectToAction("Index", "Home");
        }

        private bool ValidateInput(UserModel user)
        {
            if (string.IsNullOrEmpty(user.UserName))
            {
               ViewBag.ErrorMessage = "用户名不能为空";
                return false;
            }
            if (user.UserName.Length < 2 || user.UserName.Length > 10)
            {
                ViewBag.ErrorMessage = "用户名为2-10位字符";
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

        private void AddHttpContextItems(UserModel user)
        {
            if (HttpContext.Session[CookieModel.UserName.ToString()] == null || HttpContext.Session[CookieModel.Password.ToString()] == null)
            {
                HttpContext.Session[CookieModel.UserName.ToString()] = user.UserName;
                HttpContext.Session[CookieModel.Password.ToString()] = user.Password;
            }
            HttpContext.Session.Timeout = 120;
        }

        public FileContentResult GetVerificationCode()
        {
            VerificationCode code = new VerificationCode();
            code.GetVerificationCode(6);
            Session["VerificationCode"] = code.Code;
            return File(code.CodeBuffer, @"image/jpeg");
        }

        public ActionResult Loginout()
        {
            HttpCookie cookie = Request.Cookies.Get(CookieModel.Logistics_User_Cookie.ToString());
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddHours(-1);
                Response.Cookies.Add(cookie);
            }
            Session[CookieModel.UserName.ToString()] = null;
            Session[CookieModel.Password.ToString()] = null;
            Session[CookieModel.UserName.ToString()] = null;
            Session[CookieModel.CurrentUser.ToString()] = null;
            Session[CookieModel.CurrentAdmin.ToString()] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}