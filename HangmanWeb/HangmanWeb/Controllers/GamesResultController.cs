using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HangmanRepository;
using HangmanWeb.Models;

namespace HangmanWeb.Controllers
{
    public class GamesResultController : Controller
    {
        //
        // GET: /GamesResult/
        public ActionResult Report()
        {
            if (System.Web.HttpContext.Current.User == null || !System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            GamesResult gamesResult = Game.GetGamesResult(System.Web.HttpContext.Current.User.Identity.Name);
            return View(gamesResult);
        }
	}
}