using SampleServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrialAuthenticationMvcApp.Infrastructure;

namespace TrialAuthenticationMvcApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            BackServiceProxy proxy = new BackServiceProxy();
            var serviceResult = proxy.DoSomething(new SomeParameters { Parameter = 99 });
            return View(serviceResult);
        }
    }
}