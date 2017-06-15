using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace ClanManager.Models
{
    public class Clan
    {
        public string Name { get; set; }
        public List<Character> Members { get; set; }
        public double averageAP { get; set; }
        public double averageLevel { get; set; }
        public double averageScore { get; set; }
        public int truesoulAmount { get; set; }
        public int chokmaAmount { get; set; }
        public int memberAmount { get; set; }
        public int summoners { get; set; }
        public int warlocks { get; set; }
        public int soulFighters { get; set; }
        public int destroyers { get; set; }
        public int bladeDancers { get; set; }
        public int bladeMasters { get; set; }
        public int forceMasters { get; set; }
        public int kungFuMasters { get; set; }
        public int assassins { get; set; }
        public string server { get; set; }

        public Clan(string name, List<Character> members)
        {
            this.Name = name;
            this.Members = members;
        }

        public Clan()
        {

        }


        public Clan(string name, int members, double averageAP, double averageLevel, double averageScore, int truesouls, int chokmas, 
            int summoners, int forceMasters, int destroyers, int bladeMasters, int bladeDancers, int assassins, int kungFuMasters, int soulFighters,
            int warlocks, string server)
        {
            this.Name = name;
            this.memberAmount = members;
            this.averageAP = averageAP;
            this.averageLevel = averageLevel;
            this.averageScore = averageScore;
            this.truesoulAmount = truesouls;
            this.chokmaAmount = chokmas;
            this.summoners = summoners;
            this.forceMasters = forceMasters;
            this.destroyers = destroyers;
            this.bladeDancers = bladeDancers;
            this.bladeMasters = bladeMasters;
            this.assassins = assassins;
            this.kungFuMasters = kungFuMasters;
            this.soulFighters = soulFighters;
            this.warlocks = warlocks;
            this.server = server;
        }
        public double AverageAP(List<Character> members)
        {
            double result = 0.00;
            foreach(Character c in members)
            {
                result += Convert.ToDouble(c.AP);
            }

            result = result / members.Count();

            return result;
        }

        public double AverageHMLevel(List<Character> members)
        {
            //only works on simplified level names 
            double result = 0.00;
            string resultLevel;
            int amountOfMembers = members.Count;
            foreach (Character c in members)
            {
                if (c.Level != null && c.Level.Contains("Level 50 • HongmoonLevel ") )
                {
                    resultLevel = c.Level.Replace("Level 50 • HongmoonLevel ", "");
                }
                else
                {
                    resultLevel = "0";
                }
                try
                {
                    result += Convert.ToDouble(resultLevel);
                }
                catch
                {
                    amountOfMembers -= 1;
                }
            }

            result = result / amountOfMembers;

            return result;
        }

        public double AverageScore(List<Character> members)
        {
            double result = 0.00;
            foreach (Character c in members)
            {
                try
                {
                    result += Convert.ToDouble(c.DPS);
                }
                catch
                {

                }
            }

            result = result / members.Count();

            return result;
        }

        public string ServerGroup(List<Character> members)
        {
            string result = "";
            int naGroup1 = 0;
            int naGroup2 = 0;
            int naGroup3 = 0;
            int naGroup4 = 0;
            int naGroup5 = 0;
            int naGroup6 = 0;

            int euGroup1 = 0;
            int euGroup2 = 0;
            int euGroup3 = 0;
            int euGroup4 = 0;
            int euGroup5 = 0;
            int euGroup6 = 0;

            //see in which server each character is located
            foreach (Character c in members.Take(10))
            {
                #region NA server groups check
                if(c.Server == "Master Hong" || c.Server == "Gunma" || c.Server == "Taywong")
                {
                    naGroup1 += 1;
                }
                if (c.Server == "Mushin" || c.Server == "Old Man Cho")
                {
                    naGroup2 += 1;
                }
                if (c.Server == "Jiwan" || c.Server == "Soha" || c.Server == "Dochun")
                {
                    naGroup3 += 1;
                }
                if (c.Server == "Poharan" || c.Server == "Iksanun")
                {
                    naGroup4 += 1;
                }
                if (c.Server == "Yehara" || c.Server == "Hajoon" || c.Server == "Onmyung")
                {
                    naGroup5 += 1;
                }
                if (c.Server == "Juwol" || c.Server == "Yunwa" || c.Server == "Junghado")
                {
                    naGroup6 += 1;
                }
                #endregion

                #region EU server groups check
                if (c.Server == "Windrest" || c.Server == "Wild Springs" || c.Server == "Highland Gate")
                {
                    euGroup1 += 1;
                }
                if (c.Server == "Cardinal Gates" || c.Server == "Hao District" || c.Server == "Greenhollow" || c.Server == "Spirit’s Rest")
                {
                    euGroup2 += 1;
                }
                if (c.Server == "Starfall Crater" || c.Server == "Ebon Hall" || c.Server == "Angler’s Watch" || c.Server == "Twin Wagons")
                {
                    euGroup3 += 1;
                }
                if (c.Server == "[DE] Frostgipfel" || c.Server == "[DE] Bambusdorf")
                {
                    euGroup4 += 1;
                }
                if (c.Server == "[DE] Windweide" || c.Server == "[DE] Himmelsfarm")
                {
                    euGroup5 += 1;
                }
                if (c.Server == "[FR] Dokumo" || c.Server == "[FR] Ogong" || c.Server == "[FR] Hogdonny")
                {
                    euGroup6 += 1;
                }
                #endregion
            }

            //if there's x or more people in a certain server group return that group. 
            //Prevents problems with mass server changes etc.
            int numberVerifier = 1;
            #region check which group the clan members are located in return result
            if(naGroup1 > numberVerifier)
            {
                result = "[NA]Group 1: Master Hong, Gunma, Taywong";
            }
            else if (naGroup2 > numberVerifier)
            {
                result = "[NA]Group 2: Mushin, Old Man Cho";
            }
            else if(naGroup3 > numberVerifier)
            {
                result = "[NA]Group 3: Jiwan, Soha, Dochun";
            }
            else if(naGroup4 > numberVerifier)
            {
                result = "[NA]Group 4: Poharan, Iksanun";
            }
            else if(naGroup5 > numberVerifier)
            {
                result = "[NA]Group 5: Yehara, Hajoon, Onmyung";
            }
            else if(naGroup6 > numberVerifier)
            {
                result = "[NA]Group 6: Juwol, Yunwa, Junghado";
            }
            else if(euGroup1 > numberVerifier)
            {
                result = "[EU]Group 1: Windrest, Wild Springs, Highland Gate";
            }
            else if (euGroup2 > numberVerifier)
            {
                result = "[EU]Group 2: Cardinal Gates, Hao District, Greenhollow, Spirit’s Rest";
            }
            else if (euGroup3 > numberVerifier)
            {
                result = "[EU]Group 3: Starfall Crater, Ebon Hall, Angler’s Watch, Twin Wagons";
            }
            else if (euGroup4 > numberVerifier)
            {
                result = "[EU]Group 4: [DE] Frostgipfel, [DE] Bambusdorf";
            }
            else if (euGroup5 > numberVerifier)
            {
                result = "[EU]Group 5: [DE] Windweide, [DE] Himmelsfarm";
            }
            else if (euGroup6 > numberVerifier)
            {
                result = "[EU]Group 6: [FR] Dokumo, [FR] Ogong, [FR] Hogdonny";
            }
            else
            {
                result = "";
            }
            #endregion

            return result;
        }

        public int ClassAmount(List<Character> members, string className)
        {
            int result = 0;
            foreach (Character c in members)
            {
                if (c.Class == className)
                {
                    result += 1;
                }
            }

            return result;
        }

        public int ChokmaAmount(List<Character> members)
        {
            int result = 0;
            foreach (Character c in members)
            {
                try
                {
                    if (c.Weapon.Contains("Raven"))
                    {
                        result += 1;
                    }
                }
                catch
                {
                    return result;
                }
            }

            return result;
        }

        public int TrueSoulAmount(List<Character> members)
        {
            int result = 0;
            foreach (Character c in members)
            {
                try
                {
                    if (c.Soul.Contains("True"))
                    {
                        result += 1;
                    }
                }
                catch
                {
                    return result;
                }
            }

            return result;
        }

        public int Hongmoon15Amount(List<Character> members)
        {
            int result = 0;
            foreach (Character c in members)
            {
                try
                {
                    if(c.Level.Contains("Level 50 • HongmoonLevel "))
                    {
                        string s = c.Level.Replace("Level 50 • HongmoonLevel ", "");

                        if (Convert.ToInt32(s) >= 15)
                        {
                            result += 1;
                        }
                    }
                }
                catch
                {

                }
            }

            return result;
        }

        public int UnleashedPetAmount(List<Character> members)
        {
            int result = 0;
            foreach (Character c in members)
            {
                try
                {
                    if (c.Pet.Contains("Unleashed"))
                    {
                        result += 1;
                    }
                }
                catch
                {
                    return result;
                }
            }

            return result;
        }

        public string AvatarHTMLList(List<Character> members)
        {
            string responseHtml = "";
            foreach (Character c in members)
            {
                try
                {
                    if (c.Avatar != null || c.Avatar == "")
                    {
                        responseHtml += "<img src= \"" + c.Avatar + "\" title = \"" + c.Name +
                                    "\" \"height = \"310\" width = \"189\" onerror=\"this.style.display='none'\" /> ";
                        //Below uses a thumbnail if no avatar was uploaded by player
                        //responseHtml += "<img src= \"" + c.Avatar + "\" title = \"" + c.Name +
                        //          "\" \"height = \"310\" width = \"189\" onerror=\"this.src='http://static.ncsoft.com/ingame/bns/character_v2/profile/noImg.png'\" /> ";
                    }
                }
                catch
                {
                    return responseHtml;
                }
            }

            return responseHtml;
        }

        public List<Bitmap> AvatarBmpList(List<Character> members)
        {
            List<Bitmap> responseList = new List<Bitmap>();
            foreach (Character c in members)
            {
                try
                {
                    if (c.Avatar != null || c.Avatar == "")
                    {
                        
                        System.Net.WebRequest request =
                            System.Net.WebRequest.Create(
                            c.Avatar);
                        System.Net.WebResponse response = request.GetResponse();
                        System.IO.Stream responseStream =
                            response.GetResponseStream();
                        Bitmap bmp = new Bitmap(responseStream);
                        responseList.Add(bmp);
                    }
                }
                catch
                {

                }
            }

            return responseList;

        }
    }
}