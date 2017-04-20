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

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string recievedValue = Convert.ToString(context.Request["value"]);
            string response = "";

            //loop that returns stuff depending on what was recieved
            if (recievedValue == "ap")
            {
                double avgAP = Math.Round(Data.selectedClan.AverageAP(Data.selectedClan.Members), 2);
                response = avgAP.ToString();
            }
            else if (recievedValue == "level")
            {
                double avgLvl = Math.Round(Data.selectedClan.AverageHMLevel(Data.selectedClan.Members), 2);
                response = avgLvl.ToString();
            }
            else if (recievedValue == "score")
            {
                double avgScore = Math.Round(Data.selectedClan.AverageScore(Data.selectedClan.Members), 2);
                response = avgScore.ToString();
            }
            else if (recievedValue == "server")
            {
                string server = Data.selectedClan.ServerGroup(Data.selectedClan.Members);
                response = server;
            }
            
            else if (recievedValue == "sin")
            {
                int classCount = Data.selectedClan.ClassAmount(Data.selectedClan.Members, "Assassin");
                response = classCount.ToString();
            }
            else if (recievedValue == "bd")
            {
                int classCount = Data.selectedClan.ClassAmount(Data.selectedClan.Members, "Blade Dancer");
                response = classCount.ToString();
            }
            else if (recievedValue == "bm")
            {
                int classCount = Data.selectedClan.ClassAmount(Data.selectedClan.Members, "Blade Master");
                response = classCount.ToString();
            }
            else if (recievedValue == "des")
            {
                int classCount = Data.selectedClan.ClassAmount(Data.selectedClan.Members, "Destroyer");
                response = classCount.ToString();
            }
            else if (recievedValue == "fm")
            {
                int classCount = Data.selectedClan.ClassAmount(Data.selectedClan.Members, "Force Master");
                response = classCount.ToString();
            }
            else if (recievedValue == "kfm")
            {
                int classCount = Data.selectedClan.ClassAmount(Data.selectedClan.Members, "Kung Fu Master");
                response = classCount.ToString();
            }
            else if (recievedValue == "sf")
            {
                int classCount = Data.selectedClan.ClassAmount(Data.selectedClan.Members, "Soul Fighter");
                response = classCount.ToString();
            }
            else if (recievedValue == "sum")
            {
                int classCount = Data.selectedClan.ClassAmount(Data.selectedClan.Members, "Summoner");
                response = classCount.ToString();
            }
            else if (recievedValue == "wl")
            {
                int classCount = Data.selectedClan.ClassAmount(Data.selectedClan.Members, "Warlock");
                response = classCount.ToString();
            }
            else if (recievedValue == "maxlevel")
            {
                int maxlevel = Data.selectedClan.Hongmoon15Amount(Data.selectedClan.Members);
                response = maxlevel.ToString();
            }
            else if (recievedValue == "chokma")
            {
                int chokma = Data.selectedClan.ChokmaAmount(Data.selectedClan.Members);
                response = chokma.ToString();
            }
            else if (recievedValue == "unleashedpet")
            {
                int unleashedpet = Data.selectedClan.UnleashedPetAmount(Data.selectedClan.Members);
                response = unleashedpet.ToString();
            }
            else if (recievedValue == "truesoul")
            {
                int truesoul = Data.selectedClan.TrueSoulAmount(Data.selectedClan.Members);
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