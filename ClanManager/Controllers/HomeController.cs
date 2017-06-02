using ClanManager.Models;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ClanManager.Controllers
{
    public class HomeController : Controller
    {
        private Website website;

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

        public void ExportAvatars()
        {
            website = new Website();

            if (website.ZipAvatars() == true)
            {
                Response.Write("Successfully added " + website.succesZipped + " avatars, unsuccessfully added " + website.unsuccesZipped + " avatars." + Environment.NewLine +
                    "Saved to location: " + website.filePath);
                //return File(website.zippedFileStream, "application/zip");
            }
            else
            {
                Response.Write("Error, try again later");
                //return RedirectToAction("clanoverview", "Clan", new { id = Data.selectedClan.Name });
            }

        }

    }
}