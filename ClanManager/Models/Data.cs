using CsQuery;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ClanManager.Models
{
    public class Data
    {
        public static Clan selectedClan = new Clan();
        public static List<Character> dynamicCharacterList = new List<Character>();
        private static readonly string DatabaseString = "Data Source=TERROG;Initial catalog=BNSCharacters;Integrated Security=true";

        public static string url = "http://eu-bns.ncsoft.com/ingame/bs/character/profile?c="; //get link

        public static List<string> characternames = new List<string>();

        /// <summary>
        /// Method to read out HTML page and return read data
        /// </summary>
        public static string getHtml(string urlAddress)
        {
            string data = ""; //HTML text
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();
                }
                return data;
            }
            catch
            {
                return data;
            }
        }

        /// <summary>
        /// Method using read HTML page and existing charactername. Will return information for given character and 
        /// trim text
        /// </summary>
        public static Character getAllPlayerDataTrimmed(string name)
        {
            try
            {
                string html = getHtml(url + name);
                CQ dom = CQ.Create(html);
                Character characterData = new Character(name);

                //Read data from HTML page (see CsQuery documentation on how it works)
                characterData.Class = dom[".desc li"].Eq(0).Text().Trim(); //Class
                characterData.Level = dom[".desc li"].Eq(1).Text().Trim(); //Level
                characterData.Server = dom[".desc li"].Eq(2).Text().Trim(); //server
                characterData.Clan = dom[".desc li"].Eq(4).Text().Trim(); //clan
                characterData.Weapon = dom[".wrapWeapon"].Find(".name").Text().Trim(); //Weapon
                characterData.Earring = dom[".earring"].Find(".name").Text().Trim();
                characterData.Necklace = dom[".necklace"].Find(".name").Text().Trim();
                characterData.Bracelet = dom[".bracelet"].Find(".name").Text().Trim();
                characterData.Ring = dom[".ring"].Find(".name").Text().Trim();
                characterData.Belt = dom[".belt"].Find(".name").Text().Trim();
                characterData.Soul = dom[".soul"].Find(".name").Text().Trim();
                characterData.AP = dom[".stat-point"].Eq(1).Text().Trim(); //AP
                characterData.CriticalHit = dom[".stat-point"].Eq(21).Text().Trim();
                characterData.CriticalHitRate = dom[".stat-point"].Eq(24).Text().Trim();
                characterData.CriticalDmg = dom[".stat-point"].Eq(25).Text().Trim();
                characterData.CriticalDmgRate = dom[".stat-point"].Eq(28).Text().Trim();
                characterData.ElementalDmg = ((Convert.ToInt32(dom[".stat-point"].Eq(37).Text().Trim()) +
                    Convert.ToInt32(dom[".stat-point"].Eq(40).Text().Trim())) / 2).ToString(); //sum of both elemental damage types
                characterData.HP = dom[".stat-point"].Eq(45).Text().Trim();
                characterData.Pet = dom[".guard"].Find("name").Text().Trim(); //pet
                characterData.SoulBadge = dom[".singongpae"].Find("name").Text().Trim();
                characterData.Piercing = dom[".stat-point"].Eq(10).Text().Trim(); //defence piercing
                characterData.DPS = Convert.ToInt32(characterData.CalculateDPS(characterData));

                //for reading the avatar I can't read the <img> tag so I just remove data I don't want.
                //there might a method I'm missing though!
                string avatar = dom[".charaterView"].Html();
                if (avatar.Contains("\n\t\t\t<img src=\""))
                {
                    avatar = avatar.Replace("\n\t\t\t<img src=\"", "");
                }

                if (avatar.Contains("\">\n\t\t\t\n\t\t"))
                {
                    avatar = avatar.Replace("\">\n\t\t\t\n\t\t", "");
                }

                characterData.Avatar = avatar;

                return characterData;
            }
            catch { return null; }
        }


        //NEED TO MAKE UPDATE BUTTON TO CHECK DATA FROM DATABASE WITH F2 DATA

        //public static List<string> getPlayerClan(string clanName)
        //{
        //    try
        //    {
        //        characternames = Data.getCharactersFromClan(clanName);
        //        List<string> clanmembers = new List<string>();

        //        foreach (string s in characternames)
        //        {
        //            string html = getHtml(url + s);
        //            CQ dom = CQ.Create(html);
        //            string clanname = dom[".desc li"].Eq(4).Text().Trim(); //clan

        //            if (clanname.ToLower() == clanName.ToLower())
        //                clanmembers.Add(s);
        //        }
        //        return clanmembers;
        //    }
        //    catch { return null; }
        //}

        public static SqlConnection Connection
        {
            get
            {
                SqlConnection connection = new SqlConnection(DatabaseString);
                connection.Open();
                return connection;
            }
        }

        public static List<Character> getCharactersFromClan(string clanname)
        {
            List<Character> returnList = new List<Character>();
            //query to check if searched clan exsists within the database.
            string query = "SELECT name FROM character WHERE LOWER(clan) = @clanname";
            using (SqlConnection conn = Data.Connection)
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add(new SqlParameter("clanname", clanname.ToLower()));

                //conn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        try
                        {
                            //Make this character object using result from query
                            Character databaseCharacter = getAllPlayerDataTrimmed(rdr["Name"].ToString());

                            //Compare database with F2 page and check if still in same clan
                            if (databaseCharacter.Clan.ToLower() == clanname.ToLower())
                            {
                                Data.selectedClan.Members.Add(databaseCharacter);
                                //add to return list if database clan and F2 clan are the same
                                returnList.Add(databaseCharacter);
                            }
                            else
                            {
                                Data.updateClan(databaseCharacter.Name.ToLower(), databaseCharacter.Clan.ToLower());
                            }
                        }
                        catch
                        {
                            //throw new Exception("Error collecting data");
                        }
                    }
                }
            }
            return returnList;
        }
        public static bool updateClan(string name, string newClan)
        {
            string query = @"UPDATE Character 
                            SET Clan = @newClanQuery
                            WHERE LOWER(name) = @nameQuery; ";

            using (SqlConnection conn = Data.Connection)
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("newClanQuery", newClan));
                    cmd.Parameters.Add(new SqlParameter("nameQuery", name));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
        }
    }
}