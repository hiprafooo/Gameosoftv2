using Gameosoftv2.BusinessLayer;
using Gameosoftv2.DataLayer;
using Gameosoftv2.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Gameosoftv2.Controllers
{
    public class LoginWebApiController : ApiController
    {

        [Route("api/LoginWebApi/{username}/{password}")]
        public bool Get(string username, string password)
        {
            LoginUserViewModel LoginUserViewModel = new ServiceLayer.LoginUserViewModel()
            {
                UserName = username,
                Password = password
            };
            ILoginUserDataLayer loginUserDataLayer = new LoginUserDataLayer();
            Gameosoftv2.BusinessLayer.LoginUserService LoginUserServiceBusiness = new BusinessLayer.LoginUserService(loginUserDataLayer);
            Gameosoftv2.ServiceLayer.LoginUserService LoginUserService = new ServiceLayer.LoginUserService(LoginUserServiceBusiness);
            LoginUserViewModel = LoginUserService.LoginUser(LoginUserViewModel);
            return LoginUserViewModel.IsUserLoginSucceeded;
        }

        //With params v1
        //[HttpGet] 
        //[Route("api/LoginWebApiController/{paramOne}")]
        //public string Get(int paramOne)
        //{
        //    return paramOne.ToString();
        //}

        //Wihtout params
        //public LoginUserViewModel Get()
        //{
        //    //return "Welcome";
        //    return new LoginUserViewModel
        //    {
        //        IsUserLoginSucceeded = true,
        //        Password = "pass",
        //        UserName = "firstname"
        //    };
        //}

        //
        //public LoginUserViewModel Login(LoginUserViewModel LoginUserViewModel)
        //{
        //    ILoginUserDataLayer loginUserDataLayer = new LoginUserDataLayer();
        //    Gameosoftv2.BusinessLayer.LoginUserService LoginUserServiceBusiness = new BusinessLayer.LoginUserService(loginUserDataLayer);
        //    Gameosoftv2.ServiceLayer.LoginUserService LoginUserService = new ServiceLayer.LoginUserService(LoginUserServiceBusiness);
        //    LoginUserViewModel = LoginUserService.LoginUser(LoginUserViewModel);
        //    return LoginUserViewModel;
        //}
    }
}
