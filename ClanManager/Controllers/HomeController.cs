using ClanManager.Models;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public void ExportAvatars()
        {
            //here be download function
            Data.selectedClan.AvatarBmpList(Data.selectedClan.Members).ToString();

            string avatarsURLs = "";

            foreach(Character c in Data.selectedClan.Members)
            {
                avatarsURLs += c.Avatar + ",";
            }

            avatarsURLs.ToString();
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + compressedFileName + ".zip");
            Response.ContentType = "application/zip";

            using (var zipStream = new ZipOutputStream(Response.OutputStream))
            {
                foreach (Bitmap filePath in Data.selectedClan.AvatarBmpList(Data.selectedClan.Members))
                {
                    //Bitmap[] images = System.IO.File.ReadAllBytes(filePath);

                    //var fileEntry = new ZipEntry(Path.GetFileName(filePath))
                    //{
                    //    Size = images.Length
                    //};

                    //zipStream.PutNextEntry(fileEntry);
                    //zipStream.Write(images, 0, images.Length);
                }

                zipStream.Flush();
                zipStream.Close();
            }


        }
    }
}