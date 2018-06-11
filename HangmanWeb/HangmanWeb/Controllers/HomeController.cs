using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HangmanWeb.Models;

namespace HangmanWeb.Controllers
{
    public class HomeController : Controller
    {
        private string word;

        public ActionResult NewGame()
        {
            if (System.Web.HttpContext.Current.User == null || !System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            List<SelectListItem> items = new List<SelectListItem>();
            List<KeyValuePair<int, string>> categories = Game.GetCategories();
            foreach (KeyValuePair<int, string> pair in categories)
            {
                items.Add(new SelectListItem() { Text = pair.Value, Value = pair.Key.ToString()});
            }

            ViewBag.Categories = items;
            return View();
        }


        [HttpGet]
        public ActionResult Word(string Categories)
        {
            Game game = new Game(int.Parse(Categories), System.Web.HttpContext.Current.User.Identity.Name);
            Session["Game"] = game;
            ViewBag.ImagePath = @"..\..\Images\err0.gif";
            return View(game);
        }


        public ViewResult CharClick(char c)
        {
            Game game = (Game)Session["Game"];
            game.ProcessChar(c);
            ViewBag.ImagePath = String.Format(@"..\..\Images\err{0}.gif", game.WrongCharsCount);
            return View("Word", game);
        }
    }
}