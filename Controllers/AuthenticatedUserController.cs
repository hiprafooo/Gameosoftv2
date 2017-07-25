using Gameosoftv2.DataLayer;
using Gameosoftv2.PlayService;
using Gameosoftv2.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Gameosoftv2.Controllers
{
    public class AuthenticatedUserController : Controller
    {
        // GET: AuthenticatedUser
        [Authorize]
        public ActionResult AuthenticatedUserIndex()
        {
            //Little dirty coding here. Presentation layer directly accessing DB

            AuthenticatedUserViewModel AuthenticatedUserViewModel = new ServiceLayer.AuthenticatedUserViewModel();
            ApplicationDbContext db = new ApplicationDbContext();

            //Users drop down
            AuthenticatedUserViewModel.Users =  db.Users.Select(a => new SelectListItem
                                                {
                                                    Value=   a.Id,
                                                    Text = a.UserName
                                                }).ToList();

            //Games Drop Down
            AuthenticatedUserViewModel.Games = db.Game.Select(a => new SelectListItem
                {
                    Value = SqlFunctions.StringConvert((double)a.Id),
                    Text = a.GameName
                }).ToList();

            //GridView
            AuthenticatedUserViewModel.UserGames = db.UserGame.Select(a => new UserGameViewModel
            {
                GameName = a.Game.GameName,
                UserName = a.User.UserName,
                HoursPlayed = a.HoursPlayed,
                UserId = a.UserId,
                GameId = a.GameId
            }).ToList();

            return View(AuthenticatedUserViewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AuthenticatedUserIndex(AuthenticatedUserViewModel AuthenticatedUserViewModel)
        {

            //WCF
            PlayServiceClient PlayServiceClient = new PlayService.PlayServiceClient();
            PlayServiceClient
                .InsertUserGameRecord
                (
                Convert.ToInt16(AuthenticatedUserViewModel.GameId),
                AuthenticatedUserViewModel.UserId, 
                AuthenticatedUserViewModel.HoursPlayed
                );

            return RedirectToAction("AuthenticatedUserIndex");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "RegisterUser");
        }
    }
}