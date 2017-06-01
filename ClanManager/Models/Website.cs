using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;

namespace ClanManager.Models
{
    public class Website
    {
        Clan selectedClan = Data.selectedClan;

        public Website()
        {

        }

        //error loading data int to give feedback through viewbag when failed to load F2 page
        public static int errorLoadingData { get; set; }

        public bool ZipAvatars()
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        foreach (Character c in selectedClan.Members)
                        {
                            var demoFile = archive.CreateEntry(c.Name + ".png");

                            using (var entryStream = demoFile.Open())
                            {
                                c.AvatarBmp(c).Save(entryStream, ImageFormat.Png);
                            }
                        }
                    }

                    using (var fileStream = new FileStream(@"C:\Users\Terence\Desktop\" + selectedClan.Name + ".zip", FileMode.Create))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.CopyTo(fileStream);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}