using ClanManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ClanManager
{
    /// <summary>
    /// Summary description for ReturnRows
    /// </summary>
    public class ReturnRows : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            response.BufferOutput = false;
            response.ContentType = "text/html";

            int ReturnRows = 0;

            //query to check if searched clan exists within the database.
            string query = "SELECT Count(*) AS Rows FROM character WHERE LOWER(clan) = @clanname";
            using (SqlConnection conn = Data.Connection)
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add(new SqlParameter("clanname", Data.selectedClan.Name.ToLower()));

                //conn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        try
                        {
                            if (rdr.HasRows)
                            {
                                ReturnRows = Convert.ToInt32(rdr["Rows"]);
                            }
                            else
                            {
                                ReturnRows = 0;
                            }
                        }
                        catch
                        {
                            //throw new Exception("Error collecting data");
                        }
                    }
                }
            }
            response.Write(ReturnRows);
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