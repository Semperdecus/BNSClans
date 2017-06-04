using ClanManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClanManager.scripts.handlers
{
    /// <summary>
    /// Summary description for ClanSummaryData
    /// </summary>
    public class ClanSummaryData : IHttpHandler
    {
        Clan selectedclan = Data.selectedClan;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string recievedValue = Convert.ToString(context.Request["value"]);
            string response = "";

            //loop that returns stuff depending on what was recieved
            if (recievedValue == "ap")
            {
                double avgAP = Math.Round(selectedclan.AverageAP(selectedclan.Members), 2);
                selectedclan.averageAP = avgAP;
                response = avgAP.ToString();
            }
            else if (recievedValue == "level")
            {
                double avgLvl = Math.Round(selectedclan.AverageHMLevel(selectedclan.Members), 2);
                selectedclan.averageLevel = avgLvl;
                response = avgLvl.ToString();
            }
            else if (recievedValue == "score")
            {
                double avgScore = Math.Round(selectedclan.AverageScore(selectedclan.Members), 2);
                selectedclan.averageScore = avgScore;
                response = avgScore.ToString();
            }
            else if (recievedValue == "server")
            {
                string server = selectedclan.ServerGroup(selectedclan.Members);
                selectedclan.server = server;
                response = server;
            }
            
            else if (recievedValue == "sin")
            {
                int classCount = selectedclan.ClassAmount(selectedclan.Members, "Assassin");
                selectedclan.assassins = classCount;
                response = classCount.ToString();
            }
            else if (recievedValue == "bd")
            {
                int classCount = selectedclan.ClassAmount(selectedclan.Members, "Blade Dancer");
                selectedclan.bladeDancers = classCount;
                response = classCount.ToString();
            }
            else if (recievedValue == "bm")
            {
                int classCount = selectedclan.ClassAmount(selectedclan.Members, "Blade Master");
                selectedclan.bladeMasters = classCount;
                response = classCount.ToString();
            }
            else if (recievedValue == "des")
            {
                int classCount = selectedclan.ClassAmount(selectedclan.Members, "Destroyer");
                selectedclan.destroyers = classCount;
                response = classCount.ToString();
            }
            else if (recievedValue == "fm")
            {
                int classCount = selectedclan.ClassAmount(selectedclan.Members, "Force Master");
                selectedclan.forceMasters = classCount;
                response = classCount.ToString();
            }
            else if (recievedValue == "kfm")
            {
                int classCount = selectedclan.ClassAmount(selectedclan.Members, "Kung Fu Master");
                selectedclan.kungFuMasters = classCount;
                response = classCount.ToString();
            }
            else if (recievedValue == "sf")
            {
                int classCount = selectedclan.ClassAmount(selectedclan.Members, "Soul Fighter");
                selectedclan.soulFighters = classCount;
                response = classCount.ToString();
            }
            else if (recievedValue == "sum")
            {
                int classCount = selectedclan.ClassAmount(selectedclan.Members, "Summoner");
                selectedclan.summoners = classCount;
                response = classCount.ToString();
            }
            else if (recievedValue == "wl")
            {
                int classCount = selectedclan.ClassAmount(selectedclan.Members, "Warlock");
                selectedclan.warlocks = classCount;
                response = classCount.ToString();
            }
            else if (recievedValue == "maxlevel")
            {
                int maxlevel = selectedclan.Hongmoon15Amount(selectedclan.Members);
                response = maxlevel.ToString();
            }
            else if (recievedValue == "chokma")
            {
                int chokma = selectedclan.ChokmaAmount(selectedclan.Members);
                selectedclan.chokmaAmount = chokma;
                response = chokma.ToString();
            }
            else if (recievedValue == "unleashedpet")
            {
                int unleashedpet = selectedclan.UnleashedPetAmount(selectedclan.Members);
                response = unleashedpet.ToString();
            }
            else if (recievedValue == "truesoul")
            {
                int truesoul = selectedclan.TrueSoulAmount(selectedclan.Members);
                selectedclan.truesoulAmount = truesoul;
                response = truesoul.ToString();
            }
            //returns the amount of F2 pages which couldn't be opened
            else if (recievedValue == "errors")
            {
                if(Website.errorLoadingData > 0)
                {
                    response = "(F2 Page errors: " + Website.errorLoadingData.ToString() + ")";
                }
                else
                {
                    response = "";
                }
            }
            else if (recievedValue == "avatars")
            {
                string avatarsHTML = selectedclan.AvatarHTMLList(selectedclan.Members);
                response = avatarsHTML.ToString();
            }
            else if (recievedValue == "updateDatabase")
            {
                selectedclan.memberAmount = selectedclan.Members.Count();

                if (Data.updateClanDetails(selectedclan.Name, selectedclan.averageAP, selectedclan.averageLevel, selectedclan.averageScore,
                    selectedclan.truesoulAmount, selectedclan.chokmaAmount, selectedclan.memberAmount, selectedclan.summoners, selectedclan.warlocks,
                    selectedclan.soulFighters, selectedclan.destroyers, selectedclan.bladeDancers, selectedclan.bladeMasters, selectedclan.forceMasters,
                    selectedclan.kungFuMasters, selectedclan.assassins, selectedclan.server))
                {
                    response = "Data for " + selectedclan.Name + " has been updated.";
                }
                else
                {
                    response = "Error updating data for " + selectedclan.Name + ".";
                }
            }
            context.Response.Write(response);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}