using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModelsDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PlayerModel player = new PlayerModel()
            {
                Id = 2,
                FirstName = "LeBron",
                LastName = "James",
                AllStar = true

            };
            return View(player);
        }
    }  }