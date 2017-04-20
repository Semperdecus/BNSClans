using ClanManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClanManager.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<Character> emptylist = new List<Character>();
            emptylist.Add(new Character());

            Data.selectedClan = new Clan("", emptylist);
            return View();
        }

        [HttpPost]
        public ActionResult Index(Clan clan)
        {
            List<Character> emptylist = new List<Character>();
            emptylist.Add(new Character());
            Data.selectedClan = new Clan(clan.Name, emptylist);
            emptylist.Clear();
            return RedirectToAction("clanoverview", "Clan", new { id = clan.Name });
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Clans()
        {
            return View();
        }
    }
}