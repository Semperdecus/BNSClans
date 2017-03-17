using ClanManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;

namespace ClanManager
{
    /// <summary>
    /// Summary description for LoadData
    /// </summary>
    public class LoadData : IHttpHandler
    {
        private bool write = false;
        private string row = "";

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response; 
            response.BufferOutput = false; 
            response.ContentType = "text/html";


            //Data.selectedClan.Members.Clear();
            

            //query to check if searched clan exists within the database.
            string query = "SELECT name FROM character WHERE LOWER(clan) = @clanname";
            using (SqlConnection conn = Data.Connection)
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add(new SqlParameter("clanname", Data.selectedClan.Name.ToLower()));

                //conn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read() && !write)
                    {
                        try
                        {
                            //Make this character object using result from query
                            Character databaseCharacter = Data.getAllPlayerDataTrimmed(rdr["Name"].ToString());

                            //Compare database with F2 page and check if still in same clan
                            if (databaseCharacter.Clan.ToLower() == Data.selectedClan.Name.ToLower())
                            {
                                Data.selectedClan.Members.Add(databaseCharacter);

                                //write row to page
                                row = "<table class=\"table\"><tr><td> " +
                                                    databaseCharacter.Name +
                                                @"</td>
                                                <td> " +
                                                    databaseCharacter.Clan +
                                                @"</td> 
                                                <td> " +
                                                    databaseCharacter.Level +
                                                @"</td>
                                                <td> " +
                                                    databaseCharacter.HP +
                                                @"</td> 
                                                <td> " +
                                                    databaseCharacter.AP +
                                                @"</td>
                                                <td> " +
                                                    databaseCharacter.CriticalHit +
                                                @"</td>
                                                <td> " +
                                                    databaseCharacter.DPS.ToString() +
                                                @"</td></tr></table>";
                                response.Write(row);
                                response.Flush();
                                response.Clear();
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
            if(write == true)
            {
                write = false;
            }      
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}