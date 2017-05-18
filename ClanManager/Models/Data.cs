using CsQuery;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ClanManager.Models
{
    public class Data
    {
        public static int rowNumber = 0;
        public static Clan selectedClan = new Clan();
        private static readonly string DatabaseString = "Server=mssql.fhict.local;Database=dbi340015;User Id=dbi340015;Password=qw12QW!@;";
        //private static readonly string DatabaseString = "Data Source=TERROG;Initial catalog=BNSCharacters;Integrated Security=true";

        private static string urlMain = "http://eu-bns.ncsoft.com/ingame/bs/character/profile?c="; //get link
        private static string urlEquipment = "http://eu-bns.ncsoft.com/ingame/bs/character/data/equipments?c="; //equipment link
        private static string urlAbilities = "http://eu-bns.ncsoft.com/ingame/bs/character/data/abilities.json?c="; //abilities link
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
                Task<string>[] taskArray = {
                    Task<string>.Factory.StartNew(() => getHtml(urlEquipment + name)),
                    Task<string>.Factory.StartNew(() => getHtml(urlAbilities + name)),
                    Task<string>.Factory.StartNew(() => getHtml(urlMain + name))
                };
                string htmlEquipment = taskArray[0].Result;
                string htmlAbilities = taskArray[1].Result;
                string htmlMain = taskArray[2].Result;

                string html = htmlEquipment + " " + htmlAbilities + " " + htmlMain;

                CQ domfull = CQ.CreateDocument(html);
                CQ dom = domfull.Render(DomRenderingOptions.RemoveComments);

                Character characterData = new Character(name);

                //Read data from HTML page (see CsQuery documentation on how it works)
                characterData.Class = dom[".desc li"].Eq(0).Text().Trim(); //Class
                characterData.Level = dom[".desc li"].Eq(1).Text().Trim(); //Level
                characterData.Server = dom[".desc li"].Eq(2).Text().Trim(); //server
                characterData.Clan = dom[".desc li"].Eq(4).Text().Trim(); //clan
                characterData.Weapon = dom[".wrapWeapon"].Find(".name").Text().Replace("Weapon", "").Trim(); //Weapon
                characterData.Earring = dom[".earring"].Find(".name").Text().Replace("Earring", "").Trim();
                characterData.Necklace = dom[".necklace"].Find(".name").Text().Replace("Necklace", "").Trim();
                characterData.Bracelet = dom[".bracelet"].Find(".name").Text().Replace("Bracelet", "").Trim();
                characterData.Ring = dom[".ring"].Find(".name").Text().Replace("Ring", "").Trim();
                characterData.Belt = dom[".belt"].Find(".name").Text().Replace("Belt", "").Trim();
                characterData.Soul = dom[".soul"].Find(".name").Text().Replace("Soul", "").Trim();
                if(dom[".stat-point"].HasData() == true)
                {
                    characterData.AP = dom[".stat-point"].Eq(1).Text().Trim(); //AP
                    characterData.CriticalHit = dom[".stat-point"].Eq(21).Text().Trim();
                    characterData.CriticalHitRate = dom[".stat-point"].Eq(24).Text().Trim();
                    characterData.CriticalDmg = dom[".stat-point"].Eq(25).Text().Trim();
                    characterData.CriticalDmgRate = dom[".stat-point"].Eq(28).Text().Trim();
                    characterData.HP = dom[".stat-point"].Eq(45).Text().Trim();
                    characterData.Pet = dom[".guard"].Find("name").Text().Trim(); //pet
                    characterData.SoulBadge = dom[".singongpae"].Find("name").Text().Trim();
                    characterData.Piercing = dom[".stat-point"].Eq(10).Text().Trim(); //defence piercing
                    characterData.DPS = Convert.ToInt32(characterData.CalculateDPS(characterData));
                }
                
                //for reading the avatar I can't read the <img> tag so I just trim data I don't want. (Trim method won't work here)
                //there might a method I'm missing though!
                string avatar = dom[".charaterView"].Html().Trim();
                if (avatar.Contains("<img src=\""))
                {
                    avatar = avatar.Replace("<img src=\"", "");
                }
                if (avatar.Contains("\" onerror=\"this.src='http://static.ncsoft.com/ingame/bns/character_v2/profile/noImg.png'\" alt>"))
                {
                    avatar = avatar.Replace("\" onerror=\"this.src='http://static.ncsoft.com/ingame/bns/character_v2/profile/noImg.png'\" alt>", "");
                }

                characterData.Avatar = avatar;

                return characterData;
            }
            catch { return new Character(name); }
        }

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

        public static bool addMember(string name, string clanname)
        {
            string query = @"insert into [Character](name, clan) VALUES (@nameQuery , @clanQuery)";

            using (SqlConnection conn = Data.Connection)
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("nameQuery", name));
                    cmd.Parameters.Add(new SqlParameter("clanQuery", clanname));

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            return true;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }
    }
}