using ClanManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClanManager.Controllers
{
    public class ProfileController : Controller
    {

        [HttpPost]
        public ActionResult Index(Character character)
        {
            ViewBag.Character = Data.getAllPlayerDataTrimmed(character.Name);

            return View();
        }

        // GET: Profile/Name
        public ActionResult ProfileOverview(string id)
        {
            if (id != null || id != "")
            {
                ViewBag.Character = Data.getAllPlayerDataTrimmed(id);
                return View();
            }
            else if (id == null || id == "")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Character = Data.getAllPlayerDataTrimmed(id);
                return View();
            }
        }
    }
}