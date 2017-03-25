using ClanManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClanManager
{
    /// <summary>
    /// Summary description for LoadMemberCount
    /// </summary>
    public class LoadMemberCount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(Data.selectedClan.Members.Count());
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