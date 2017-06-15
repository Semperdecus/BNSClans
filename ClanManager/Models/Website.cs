using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
                            catch
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

        public string GetUntilOrEmpty(string text, string stopAt)
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }

        public void BulkInsertMillionNames(string rawText)
        {
            string[] namesArray = rawText.Split(
                new string[] { "\r\n" }, StringSplitOptions.None);
            int resultAdded = 0;
            int resultnotAdded = 0;

            List<string> databaseNames = Data.getAllNamesFromDatabase();
            int threadSize = namesArray.Length / 5000;
            
            List<string> list_names = new List<string>(namesArray);
            Parallel.ForEach(list_names, name =>
            {
                if (!databaseNames.Contains(name))
                {
                    Character newMember = Data.getAllPlayerDataTrimmed(name);
                    if (newMember != null)
                    {
                        //try to add player
                        if (Data.addMember(newMember.Name, newMember.Clan) == true)
                        {
                            resultAdded += 1;
                        }
                        //if player is already in database update their clan
                        else if (Data.addMember(newMember.Name, newMember.Clan) == false)
                        {
                            if (Data.updateCharacterClan(newMember.Name, newMember.Clan) == true)
                            {
                                resultAdded += 1;
                            }
                            else
                            {
                                resultnotAdded += 1;
                            }
                        }
                        else
                        {
                            resultnotAdded += 1;
                        }
                    }
                    else
                    {
                        resultnotAdded += 1;
                    }
                }
            });
        }
    }
}