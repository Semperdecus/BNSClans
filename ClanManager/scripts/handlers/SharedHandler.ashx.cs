using ClanManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClanManager.scripts.handlers
{
    /// <summary>
    /// Summary description for SharedHandler
    /// </summary>
    public class SharedHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            List<Character> emptylist = new List<Character>();
            emptylist.Add(new Character());
            Data.selectedClan = new Clan("", emptylist);

            context.Response.Write("Succes");
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