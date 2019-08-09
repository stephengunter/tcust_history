using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.DAL;

namespace TzuChiBackend.Controllers
{
    public class AdminAccountController : Controller
    {
        IAccountViewManagement gAccountViewManagement = new AccountViewManagementImpl();
        IAdminManagement gAdminManagement = new AdminManagementImpl();
        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOff();
            }

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                model.Password = gAdminManagement.DesEncrypt(model.Password);//將密碼加密
                if (Membership.ValidateUser(model.Account, model.Password))
                {
                    gAccountViewManagement.LoginRecord(model, GetIP());      //記錄IP和登入時間
                    FormsAuthentication.RedirectFromLoginPage(model.Account, model.RememberMe);
                }

                ModelState.AddModelError("LogOnError", "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        } // Login()

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        } // LogOff()


        /// <summary>
        /// 取得IP
        /// </summary>
        private string GetIP()
        {
            string trueIP = string.Empty;
            //先取得是否有經過代理伺服器
            string ipForwared = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            string ipRemote = string.IsNullOrEmpty(Request.ServerVariables["REMOTE_ADDR"]) ? string.Empty : Request.ServerVariables["REMOTE_ADDR"].Trim();
            IPAddress ipAddress;

            if (!string.IsNullOrEmpty(ipForwared))
            {
                //將取得的 IP 字串存入陣列
                trueIP = (from s in ipForwared.Split(',')
                          where IPAddress.TryParse(s.Trim(), out ipAddress)
                          select s.Trim()).SingleOrDefault();
                trueIP = trueIP ?? string.Empty;
            } // if ( !string.IsNullOrEmpty( ipForwared ) )

            else if (!string.IsNullOrEmpty(ipRemote))
            {
                //沒經過代理伺服器，直接使用 ServerVariables["REMOTE_ADDR"]
                //並經過 CheckIP( ) 的驗證
                if (IPAddress.TryParse(ipRemote, out ipAddress))
                    trueIP = ipAddress.ToString();

            } // else if ( !string.IsNullOrEmpty( ipRemote ) )

            return trueIP;

        } // GetIP()
    }
}