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
        public int succesZipped { get; set; }
        public int unsuccesZipped { get; set; }
        public FileStream zippedFileStream { get; set; }
        public string filePath { get; set; }
        public Website()
        {

        }

        //error loading data int to give feedback through viewbag when failed to load F2 page
        public static int errorLoadingData { get; set; }

        public bool ZipAvatars()
        {
            string downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads);
            succesZipped = 0;
            unsuccesZipped = 0;
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        foreach (Character c in selectedClan.Members)
                        {
                            try
                            {
                                var demoFile = archive.CreateEntry(c.Name + ".png");

                                using (var entryStream = demoFile.Open())
                                {
                                    c.AvatarBmp(c).Save(entryStream, ImageFormat.Png);
                                    succesZipped += 1;
                                }
                            }
                            catch(Exception e)
                            {
                                unsuccesZipped += 1;
                                //mostly catches 404 not found errors and sometime enumeration errors
                            }
                        }
                    }
                    using (var fileStream = new FileStream(downloadsPath + "\\" + selectedClan.Name + ".zip", FileMode.Create))
                    {
                        
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.CopyTo(fileStream);
                        zippedFileStream = fileStream;
                        filePath = downloadsPath + "\\" + selectedClan.Name + ".zip";
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }
        }
    }
}