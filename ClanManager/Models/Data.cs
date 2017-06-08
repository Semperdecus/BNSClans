using CsQuery;
using Newtonsoft.Json;
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
        //private static readonly string DatabaseString = "Server=mssql.fhict.local;Database=dbi340015;User Id=dbi340015;Password=qw12QW!@;";
        private static readonly string DatabaseString = "Data Source=TERROG;Initial catalog=BNSCharacters;Integrated Security=true";

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
                string htmlAbilitiesJSON = taskArray[1].Result;
                string htmlMain = taskArray[2].Result;

                CharacterRawJSONData characterDataJSON = JsonConvert.DeserializeObject<CharacterRawJSONData>(htmlAbilitiesJSON);
                double test = characterDataJSON.records.total_ability.abnormal_attack_power_rate;
                string html = htmlEquipment + " " + htmlMain;

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
                characterData.Pet = dom[".guard"].Find("name").Text().Trim(); //pet
                characterData.SoulBadge = dom[".singongpae"].Find("name").Text().Trim();

                characterData.AP = characterDataJSON.records.total_ability.attack_power_value.ToString(); //AP
                characterData.CriticalHit = characterDataJSON.records.total_ability.attack_critical_value.ToString();
                characterData.CriticalHitRate = characterDataJSON.records.total_ability.attack_critical_rate.ToString();
                characterData.CriticalDmg = characterDataJSON.records.total_ability.attack_critical_damage_value.ToString();
                characterData.CriticalDmgRate = characterDataJSON.records.total_ability.attack_critical_damage_rate.ToString();
                characterData.HP = characterDataJSON.records.total_ability.int_max_hp.ToString();

                characterData.Piercing = characterDataJSON.records.total_ability.attack_pierce_value.ToString();

                #region determine which element character is using and add elemental damage
                if (characterData.Class == "Summoner")
                    //earth summoner
                    if (characterDataJSON.records.total_ability.attack_attribute_earth_value > characterDataJSON.records.total_ability.attack_attribute_wind_value)
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_earth_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_earth_rate.ToString();
                        characterData.usingElement = "Earth";
                    }
                    else
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_wind_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_wind_rate.ToString();
                        characterData.usingElement = "Wind";
                    }
                else if (characterData.Class == "Warlock")
                    //void > ice
                    if (characterDataJSON.records.total_ability.attack_attribute_void_value > characterDataJSON.records.total_ability.attack_attribute_ice_value)
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_void_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_void_rate.ToString();
                        characterData.usingElement = "Shadow";
                    }
                    else
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_ice_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_ice_rate.ToString();
                        characterData.usingElement = "Ice";
                    }
                else if (characterData.Class == "Destroyer")
                    //earth summoner
                    if (characterDataJSON.records.total_ability.attack_attribute_earth_value > characterDataJSON.records.total_ability.attack_attribute_void_value)
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_earth_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_earth_rate.ToString();
                        characterData.usingElement = "Earth";
                    }
                    else
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_void_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_void_rate.ToString();
                        characterData.usingElement = "Shadow";
                    }
                else if (characterData.Class == "Assassin")
                    //earth summoner
                    if (characterDataJSON.records.total_ability.attack_attribute_lightning_value > characterDataJSON.records.total_ability.attack_attribute_void_value)
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_lightning_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_lightning_rate.ToString();
                        characterData.usingElement = "Lightning";
                    }
                    else
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_void_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_void_rate.ToString();
                        characterData.usingElement = "Shadow";
                    }
                else if (characterData.Class == "Blade Dancer")
                    //earth summoner
                    if (characterDataJSON.records.total_ability.attack_attribute_lightning_value > characterDataJSON.records.total_ability.attack_attribute_wind_value)
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_lightning_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_lightning_rate.ToString();
                        characterData.usingElement = "Lightning";
                    }
                    else
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_wind_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_wind_rate.ToString();
                        characterData.usingElement = "Wind";
                    }
                else if (characterData.Class == "Blade Master")
                    //earth summoner
                    if (characterDataJSON.records.total_ability.attack_attribute_lightning_value > characterDataJSON.records.total_ability.attack_attribute_fire_value)
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_lightning_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_lightning_rate.ToString();
                        characterData.usingElement = "Lightning";
                    }
                    else
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_fire_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_fire_rate.ToString();
                        characterData.usingElement = "Fire";
                    }
                else if (characterData.Class == "Force Master")
                    //earth summoner
                    if (characterDataJSON.records.total_ability.attack_attribute_ice_value > characterDataJSON.records.total_ability.attack_attribute_fire_value)
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_ice_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_ice_rate.ToString();
                        characterData.usingElement = "Ice";
                    }
                    else
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_fire_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_fire_rate.ToString();
                        characterData.usingElement = "Fire";
                    }
                else if (characterData.Class == "Soul Fighter")
                    //earth summoner
                    if (characterDataJSON.records.total_ability.attack_attribute_ice_value > characterDataJSON.records.total_ability.attack_attribute_earth_value)
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_ice_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_ice_rate.ToString();
                        characterData.usingElement = "Ice";
                    }
                    else
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_earth_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_earth_rate.ToString();
                        characterData.usingElement = "Earth";
                    }
                else if (characterData.Class == "Kung Fu Master")
                    //earth summoner
                    if (characterDataJSON.records.total_ability.attack_attribute_wind_value > characterDataJSON.records.total_ability.attack_attribute_fire_value)
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_wind_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_wind_rate.ToString();
                        characterData.usingElement = "Wind";
                    }
                    else
                    {
                        characterData.ElementalDmg = characterDataJSON.records.total_ability.attack_attribute_fire_value.ToString();
                        characterData.ElementalDmgRate = characterDataJSON.records.total_ability.attack_attribute_fire_rate.ToString();
                        characterData.usingElement = "Fire";
                    }
                #endregion

                characterData.DPS = Convert.ToInt32(characterData.CalculateDPS(characterData));

                
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
                                Data.updateCharacterClan(databaseCharacter.Name.ToLower(), databaseCharacter.Clan.ToLower());
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

        public static bool updateCharacterClan(string name, string newClan)
        {
            string query = @"UPDATE Character 
                            SET Clan = @newClanQuery
                            WHERE LOWER(name) = @nameQuery; ";

            using (SqlConnection conn = Data.Connection)
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (newClan != null && name != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("newClanQuery", newClan));
                        cmd.Parameters.Add(new SqlParameter("nameQuery", name));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return true;
                            }
                            return false;
                        }
                    }
                    else
                        return false;
                }
            }
        }

        public static bool updateClanDetails(string clanName, double averageAP, double averageLevel, double averageScore, 
            int truesoulAmount, int chokmaAmount, int members, int summoners, int warlocks, int soulFighters, int destroyers, 
            int bladeDancers, int bladeMasters, int forceMasters, int kungFuMasters, int assassins, string server)
        {
            bool clanExists = false;

            string clancheckQuery = @"Select * from Clan Where name = @nameQuery";
            using (SqlConnection conn = Data.Connection)
            {
                using (SqlCommand cmd = new SqlCommand(clancheckQuery, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("nameQuery", clanName));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clanExists = true;
                        }
                    }
                }
            }

            if (clanExists == true)
            {
                string query = @"UPDATE Clan 
                    SET averageAP = @averageAPQuery, averageLevel = @averageLevelQuery, averageScore = @averageScoreQuery, 
                    truesoulAmount = @truesoulQuery, chokmaAmount = @chokmaQuery, members = @memberQuery, 
                    summoners = @sumQuery, warlocks = @wlQuery, soulFighters = @sfQuery, destroyers = @desQuery, 
                    bladeDancers = @bdQuery, bladeMasters = @bmQuery, forceMasters = @fmQuery, kungFuMasters = @kfmQuery, 
                    assassins = @sinQuery, serverGroup = @serverQuery
                    WHERE LOWER(name) = @nameQuery";

                using (SqlConnection conn = Data.Connection)
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("nameQuery", clanName));
                        cmd.Parameters.Add(new SqlParameter("averageAPQuery", averageAP));
                        cmd.Parameters.Add(new SqlParameter("averageLevelQuery", averageLevel));
                        cmd.Parameters.Add(new SqlParameter("averageScoreQuery", averageScore));
                        cmd.Parameters.Add(new SqlParameter("truesoulQuery", truesoulAmount));
                        cmd.Parameters.Add(new SqlParameter("chokmaQuery", chokmaAmount));
                        cmd.Parameters.Add(new SqlParameter("memberQuery", members));
                        cmd.Parameters.Add(new SqlParameter("sumQuery", summoners));
                        cmd.Parameters.Add(new SqlParameter("wlQuery", warlocks));
                        cmd.Parameters.Add(new SqlParameter("sfQuery", soulFighters));
                        cmd.Parameters.Add(new SqlParameter("desQuery", destroyers));
                        cmd.Parameters.Add(new SqlParameter("bdQuery", bladeDancers));
                        cmd.Parameters.Add(new SqlParameter("bmQuery", bladeMasters));
                        cmd.Parameters.Add(new SqlParameter("fmQuery", forceMasters));
                        cmd.Parameters.Add(new SqlParameter("kfmQuery", kungFuMasters));
                        cmd.Parameters.Add(new SqlParameter("sinQuery", assassins));
                        cmd.Parameters.Add(new SqlParameter("serverQuery", server));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {                          
                            return true;
                        }
                    }
                }
            }
            else if (clanExists == false)
            {
                if(addClanToDatabase(clanName, averageAP, averageLevel, averageScore, truesoulAmount, chokmaAmount, members, summoners,
                    warlocks, soulFighters, destroyers, bladeDancers, bladeMasters, forceMasters, kungFuMasters, assassins, server))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public static bool addClanToDatabase(string clanName, double averageAP, double averageLevel, double averageScore,
            int truesoulAmount, int chokmaAmount, int members, int summoners, int warlocks, int soulFighters, int destroyers,
            int bladeDancers, int bladeMasters, int forceMasters, int kungFuMasters, int assassins, string server)
        {
            string query = @"INSERT INTO [Clan](name, averageAP, averageLevel, averageScore, truesoulAmount, chokmaAmount, members, summoners,
                            warlocks, soulFighters, destroyers, bladeDancers, bladeMasters, forceMasters, kungFuMasters, assassins, serverGroup) VALUES
                            (@nameQuery, @averageAPQuery, @averageLevelQuery, @averageScoreQuery, @truesoulQuery, @chokmaQuery, @memberQuery, 
                            @sumQuery, @wlQuery, @sfQuery, @desQuery, @bdQuery, @bmQuery, @fmQuery, @kfmQuery, @sinQuery, @serverQuery)";

            using (SqlConnection conn = Data.Connection)
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("nameQuery", clanName));
                    cmd.Parameters.Add(new SqlParameter("averageAPQuery", averageAP));
                    cmd.Parameters.Add(new SqlParameter("averageLevelQuery", averageLevel));
                    cmd.Parameters.Add(new SqlParameter("averageScoreQuery", averageScore));
                    cmd.Parameters.Add(new SqlParameter("truesoulQuery", truesoulAmount));
                    cmd.Parameters.Add(new SqlParameter("chokmaQuery", chokmaAmount));
                    cmd.Parameters.Add(new SqlParameter("memberQuery", members));
                    cmd.Parameters.Add(new SqlParameter("sumQuery", summoners));
                    cmd.Parameters.Add(new SqlParameter("wlQuery", warlocks));
                    cmd.Parameters.Add(new SqlParameter("sfQuery", soulFighters));
                    cmd.Parameters.Add(new SqlParameter("desQuery", destroyers));
                    cmd.Parameters.Add(new SqlParameter("bdQuery", bladeDancers));
                    cmd.Parameters.Add(new SqlParameter("bmQuery", bladeMasters));
                    cmd.Parameters.Add(new SqlParameter("fmQuery", forceMasters));
                    cmd.Parameters.Add(new SqlParameter("kfmQuery", kungFuMasters));
                    cmd.Parameters.Add(new SqlParameter("sinQuery", assassins));
                    cmd.Parameters.Add(new SqlParameter("serverQuery", server));

                    if(members > 0)
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            return true;
                        }
                    }
                    else
                    {
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

        public static List<string> selectTop10Clan(string columnName, string groupNumber, string topX)
        {
            string query = "";
            List<string> returnList = new List<string>();
            if(groupNumber != "")
            {
                query = "select top (" + topX + ") " + columnName + ", name from Clan " +
                "where serverGroup LIKE '%Group " + groupNumber + "%' " +
                "order by " + columnName + " desc";
            }
            else
            {
                query = "select top (" + topX + ") " + columnName + ", name from Clan " +
                "order by " + columnName + " desc";
            }


            using (SqlConnection conn = Data.Connection)
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {

                    
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string result = "";
                                string clanname = reader["name"].ToString();
                                string amount = reader[columnName].ToString();
                                result = clanname + " - " + amount;

                                returnList.Add(result);
                            }

                            return returnList;
                        }

                    }
                    catch
                    {
                        throw new Exception("Error collecting data.");
                    }
                }
            }
        }
    }
}