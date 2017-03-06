using ClanManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClanManager.Controllers
{
    public class ClanController : Controller
    {
        // GET: Clan
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Clan clan)
        {
            clan.Members = Data.getCharactersFromClan(clan.Name);
            Data.selectedClan = new Clan(clan.Name, clan.Members);
            return View();
        }
    }
}