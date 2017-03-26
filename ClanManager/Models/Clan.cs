using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClanManager.Models
{
    public class Clan
    {
        public string Name { get; set; }
        public List<Character> Members { get; set; }

        public Clan(string name, List<Character> members)
        {
            this.Name = name;
            this.Members = members;
        }

        public Clan()
        {

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
            foreach (Character c in members)
            {
                if (c.Level != null && c.Level.Contains("Level 50 • Level ") )
                {
                    c.Level = c.Level.Replace("Level 50 • Level ", "");
                }
                else
                {
                    return result;
                }
                result += Convert.ToDouble(c.Level);
            }

            result = result / members.Count();

            return result;
        }

        public double AverageScore(List<Character> members)
        {
            double result = 0.00;
            foreach (Character c in members)
            {
                result += Convert.ToDouble(c.DPS);
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
            int numberVerifier = 2;
            #region check which group the clan members are located in return result
            if(naGroup1 > 2)
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
                if (c.Weapon.Contains("Raven"))
                {
                    result += 1;
                }
            }

            return result;
        }

        public int TrueSoulAmount(List<Character> members)
        {
            int result = 0;
            foreach (Character c in members)
            {
                if (c.Soul.Contains("True"))
                {
                    result += 1;
                }
            }

            return result;
        }

        public int Hongmoon15Amount(List<Character> members)
        {
            int result = 0;
            foreach (Character c in members)
            {
                if (Convert.ToInt32(c.Level) >= 14)
                {
                    result += 1;
                }
            }

            return result;
        }

        public int UnleashedPetAmount(List<Character> members)
        {
            int result = 0;
            foreach (Character c in members)
            {
                if (c.Pet.Contains("Unleashed"))
                {
                    result += 1;
                }
            }

            return result;
        }
    }
}