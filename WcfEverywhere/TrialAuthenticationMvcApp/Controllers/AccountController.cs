using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrialAuthenticationMvcApp.Models;

namespace TrialAuthenticationMvcApp.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM login, string redirectUrl)
        {
            if (BackServiceProxy.ForwardLoginRequestToService(login))
            {
                FormsAuthentication.SetAuthCookie(login.Username, false);
                return Redirect(redirectUrl);
            }
            else
            {
                ModelState.AddModelError("", "Login failed");
                return View();
            }
        }
    }
}