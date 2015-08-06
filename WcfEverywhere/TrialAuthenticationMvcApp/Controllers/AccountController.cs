using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrialAuthenticationMvcApp.Models;
using TrialAuthenticationMvcApp.Infrastructure;

namespace TrialAuthenticationMvcApp.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login, string redirectUrl)
        {
            BackServiceProxy proxy = new BackServiceProxy();
            if (proxy.ForwardLoginRequestToService(login))
            {
                FormsAuthentication.SetAuthCookie(login.Username, false);
                return Redirect(redirectUrl ?? FormsAuthentication.DefaultUrl);
            }
           else
            {
                ModelState.AddModelError("", "Login failed");
                return View();
            }
        }
    }
}