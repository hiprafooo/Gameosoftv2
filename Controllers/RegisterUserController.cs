using Gameosoftv2.BusinessLayer;
using Gameosoftv2.ServiceLayer;
using Gameosoftv2.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Security;

namespace Gameosoftv2.Controllers
{
    public class RegisterUserController : Controller
    {
        // GET: RegisterUser
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(RegisterUserViewModel registerUserViewModel)
        {
            IRegisterUserDataLayer registerUserDataLayer = new RegisterUserDataLayer();
            Gameosoftv2.BusinessLayer.RegisterUserService RegisterUserServiceBusiness = new BusinessLayer.RegisterUserService(registerUserDataLayer);

            Gameosoftv2.ServiceLayer.RegisterUserService RegisterUserService = new Gameosoftv2.ServiceLayer.RegisterUserService(RegisterUserServiceBusiness);
            registerUserViewModel = RegisterUserService.RegisterUser(registerUserViewModel);
            if (registerUserViewModel.IsRegisterUserSucceeded)
            {
                //var userStore = new UserStore<IdentityUser>();
                //var manager = new UserManager<IdentityUser>(userStore);
                //var user = new IdentityUser() { UserName = registerUserViewModel.UserName  };

                //var authenticationManager = HttpContext.GetOwinContext().Authentication;
                //var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                //authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
                //return RedirectToAction("AuthenticatedUserIndex", "AuthenticatedUser");
                //registerUserViewModel.ClaimsIdentity

                FormsAuthentication.SetAuthCookie(registerUserViewModel.UserName, false);
                return RedirectToAction("AuthenticatedUserIndex", "AuthenticatedUser");
            }
            return View(registerUserViewModel);
        }
    }
}