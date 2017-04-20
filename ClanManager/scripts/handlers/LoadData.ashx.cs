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
    /// LoadData reads the data for characters in a clan. Using data from an external website.
    /// Params: Clanname
    /// </summary>
    public class LoadData : IHttpHandler
    {
        private string row = "";

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response; 

            //the number returned from database to see how many clan members are in the clan. 
            //Passed on through script.js since for loop is happening in there so we need this var in both places.
            int a = Convert.ToInt32(context.Request["value"]);

            //this number keeps track of current row that needs to be processed.
            Data.rowNumber += 1;
            //query to check clan and number to return. 
            string query = @"SELECT * FROM (
                                SELECT ROW_NUMBER() OVER (ORDER BY name) AS RowNr, *
                                FROM character
	                            WHERE LOWER(clan) = @clanname
                            ) sub
                            WHERE sub.RowNr = @rowNumber";

            using (SqlConnection conn = Data.Connection)
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add(new SqlParameter("clanname", Data.selectedClan.Name.ToLower()));
                cmd.Parameters.Add(new SqlParameter("rowNumber", a));

                //conn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        try
                        {
                            //Make this character object using result from query
                            Character databaseCharacter = Data.getAllPlayerDataTrimmed(rdr["Name"].ToString());

                            //Compare database with F2 page and check if still in same clan
                            if (databaseCharacter.Clan.ToLower() == Data.selectedClan.Name.ToLower())
                            {
                                Data.selectedClan.Members.Add(databaseCharacter);

                                //write row number correctly
                                string returnRowNumber = "";
                                if (a < 10)
                                {
                                    returnRowNumber = "0" + a;
                                }
                                else
                                {
                                    returnRowNumber = a.ToString();
                                }
                                //write row to page
                                row = "<tr class=\"tablerow\"><td class=\"number sorter-false\"> " +
                                                "</td><td> " +
                                                    "<a href=\"https://www.bnstree.com/character/eu/" + databaseCharacter.Name + "\">" + databaseCharacter.Name + "</a>" +
                                                    //"<a href=\"/Profile/" + databaseCharacter.Name + "\">" + databaseCharacter.Name + "</a>" +
                                                @"</td>
                                                <td> " +
                                                    databaseCharacter.Class +
                                                 @"</td>
                                                <td> " +
                                                    databaseCharacter.AP +
                                                "</td>" +
                                                "<td class=\"nodisplay\"> " +
                                                    databaseCharacter.HP +
                                                @"</td>
                                                <td> " +
                                                    databaseCharacter.GetSimplifiedLevel(databaseCharacter.Level) +
                                                @"</td> 
                                                <td> " +
                                                    databaseCharacter.GetSimplifiedWeaponName(databaseCharacter.Weapon) +
                                                @"</td>
                                                <td> " +
                                                    databaseCharacter.GetSimplifiedSoulName(databaseCharacter.Soul) +
                                                "</td>" +
                                                "<td class=\"nodisplay\"> " +
                                                    databaseCharacter.Pet +
                                                "</td>" +
                                                "<td class=\"nodisplay\"> " +
                                                    databaseCharacter.Earring +
                                                "</td>" +
                                                "<td class=\"nodisplay\"> " +
                                                    databaseCharacter.Necklace +
                                                "</td>" +
                                                "<td class=\"nodisplay\"> " +
                                                    databaseCharacter.Bracelet +
                                                "</td>" +
                                                "<td class=\"nodisplay\"> " +
                                                    databaseCharacter.Ring +
                                                "</td>" +
                                                "<td class=\"nodisplay\"> " +
                                                    databaseCharacter.Belt +
                                                "</td>" + "<td class=\"nodisplay\"> " +
                                                    databaseCharacter.SoulBadge +
                                                "</td>" + "<td class=\"nodisplay\"> " +
                                                    databaseCharacter.CriticalHit +
                                                "</td>" + "<td class=\"nodisplay\"> " +
                                                    databaseCharacter.CriticalHitRate +
                                                "</td>" + "<td class=\"nodisplay\"> " +
                                                    databaseCharacter.CriticalDmg +
                                                "</td>" + "<td class=\"nodisplay\"> " +
                                                    databaseCharacter.CriticalDmgRate +
                                                "</td>" + "<td class=\"nodisplay\"> " +
                                                    databaseCharacter.ElementalDmg +
                                                "</td>" + "<td class=\"nodisplay\"> " +
                                                    databaseCharacter.ElementalDmgRate +
                                                "</td>" + "<td class=\"nodisplay\"> " +
                                                    databaseCharacter.Server +
                                                "</td>" +
                                                "<td> " +
                                                    databaseCharacter.DPS.ToString() +
                                                @"</td></tr>";
                                response.Write(row);
                            }
                            else
                            {
                                Data.updateClan(databaseCharacter.Name.ToLower(), databaseCharacter.Clan.ToLower());
                            }
                        }
                        catch
                        {
                            Website.errorLoadingData += 1;

                            row = "<tr class=\"tablerow\"><td class=\"number sorter-false\"> </td><td> " + rdr["Name"].ToString() + " </td>" +
                                "<td> </td><td> </td><td> </td><td> </td><td> </td><td> </td></tr>";

                            response.Write(row);
                        }
                    }
                }
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