using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gameosoftv2.BusinessLayer;
using Gameosoftv2.ServiceLayer;
using Gameosoftv2.DataLayer;
using System.Web.Security;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gameosoftv2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoginIndex()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginIndex(LoginUserViewModel LoginUserViewModel)
        {
            //Calls the web api
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56189/");
            string reqUri = string.Format("api/LoginWebApi/{0}/{1}", LoginUserViewModel.UserName, LoginUserViewModel.Password);
            HttpResponseMessage response = client.GetAsync(reqUri).Result;

            response.EnsureSuccessStatusCode();

            if (response.Content.ReadAsStringAsync().Result == "true")
            {
                FormsAuthentication.SetAuthCookie(LoginUserViewModel.UserName, false);
                return RedirectToAction("AuthenticatedUserIndex", "AuthenticatedUser");
            }
            return View(LoginUserViewModel);
        }
    }
}