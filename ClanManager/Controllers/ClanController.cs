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
            if(Data.selectedClan == null || Data.selectedClan.Name == "")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(Character newmembers)
        {
            string rawText = newmembers.Name;

            int resultAdded = 0;
            int resultnotAdded = 0;

            string[] partsFromString = rawText.Split(
                new string[] { "\r\n" }, StringSplitOptions.None);
            foreach(string s in partsFromString)
            {
                Character newMember = Data.getAllPlayerDataTrimmed(s);
                if (newMember != null)
                {
                    //this method might throw
                    if (Data.addMember(newMember.Name, newMember.Clan) == true && newMember.Clan.ToLower() == Data.selectedClan.Name.ToLower())
                    {
                        resultAdded += 1;
                    }
                    else
                    {
                        resultnotAdded += 1;
                    }
                }
                else
                {
                    resultnotAdded += 1;
                }
            }
            return View();
        }
    }
}