using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ClanManager.Models
{
    public class Character
    {
        public string Name { get; set; }
        public string Rank { get; set; }
        public string Class { get; set; }
        public string AP { get; set; }
        public string HP { get; set; }
        public string Level { get; set; }
        public string Weapon { get; set; }
        public string Earring { get; set; }
        public string Necklace { get; set; }
        public string Bracelet { get; set; }
        public string Ring { get; set; }
        public string Belt { get; set; }
        public string Soul { get; set; }
        public string Avatar { get; set; }
        public string Pet { get; set; }
        public string SoulBadge { get; set; }
        public string CriticalHit { get; set; }
        public string CriticalHitRate { get; set; }
        public string CriticalDmg { get; set; }
        public string CriticalDmgRate { get; set; }
        public string ElementalDmg { get; set; }
        public string ElementalDmgRate { get; set; }
        public string Clan { get; set; }
        public string Server { get; set; }
        public string Piercing { get; set; }
        public int DPS { get; set; }

        public Character(string name, string rank, string eclass, string ap, string hp, string level,
            string weapon, string earring, string necklace, string bracelet, string ring,
            string belt, string soul, string avatar, string pet, string soulbadge, string criticalhit,
            string criticalhitrate, string criticaldmg, string criticaldmgrate, string elementaldmg,
            string elementaldmgrate, string clan, string server, string piercing)
        {
            this.Name = name;
            this.Rank = rank;
            this.Class = eclass;
            this.AP = ap;
            this.HP = hp;
            this.Level = level;
            this.Weapon = weapon;
            this.Earring = earring;
            this.Necklace = necklace;
            this.Bracelet = bracelet;
            this.Ring = ring;
            this.Belt = belt;
            this.Soul = soul;
            this.Avatar = avatar;
            this.Pet = pet;
            this.SoulBadge = soulbadge;
            this.CriticalHit = criticalhit;
            this.CriticalHitRate = criticalhitrate;
            this.CriticalDmg = criticaldmg;
            this.CriticalDmgRate = criticaldmgrate;
            this.ElementalDmg = elementaldmg;
            this.Clan = clan;
            this.Server = server;
            this.Piercing = piercing;

        }

        public Character(string name)
        {
            this.Name = name;
        }

        public decimal CalculateDPS(Character c)
        {
            //DPS calculation from here http://www.freedomplays.com/blade-soul-level-50-weapon-accessory-choice-v3/3/
            decimal piercePercentage = decimal.Parse(Piercing.TrimEnd(new char[] { '%', ' ' })) / 100M;
            decimal critDmgPercentage = decimal.Parse(CriticalDmgRate.TrimEnd(new char[] { '%', ' ' })) / 100M;
            decimal critHitPercentage = decimal.Parse(CriticalHitRate.TrimEnd(new char[] { '%', ' ' })) / 100M;

            decimal elementDamageRate = (Convert.ToInt32(ElementalDmg) / (Convert.ToInt32(24.722) + 
                Convert.ToInt32(0.003421) * Convert.ToInt32(ElementalDmg)))+100;
            decimal meanBoardDamage = (375 / 100 * Convert.ToInt32(AP) + 50) * (elementDamageRate/100);

            decimal meanNonCritDamage = meanBoardDamage * ((100 + piercePercentage)/100);
            decimal meanCritDamage = meanNonCritDamage * critDmgPercentage;

            decimal result = meanNonCritDamage * (1 - critHitPercentage) + 
                Convert.ToInt32(meanCritDamage) * (critHitPercentage);

            return result;
        }
        public Character()
        {

        }

        //this method trims unneeded information from weapon names (eg. Razor, staff, sword, ... )
        public string GetSimplifiedWeaponName(string weaponFullName)
        {
            string simplifiedWeaponName = "";
            string[] weaponTypes = new string[] { " Razor", " Staff", " Lynblade", " Bracers", " Bangle", " Axe", " Sword", " Gauntlet", " Dagger" };
            foreach (string x in weaponTypes)
            {
                if (weaponFullName.Contains(x))
                {
                    simplifiedWeaponName = weaponFullName.Replace(x, "");
                }
            }
            return simplifiedWeaponName;
        }

        public string GetSimplifiedSoulName(string soulFullName)
        {
            string simplifiedSoulName = "";

            if(soulFullName.Contains("Awakened Hongmoon Critical Energy"))
            {
                simplifiedSoulName = soulFullName.Replace(" Hongmoon Critical", "");
            }
            else if(soulFullName.Contains("True Hongmoon Energy"))
            {
                simplifiedSoulName = soulFullName.Replace(" Hongmoon", "");
            }
            else
            {
                simplifiedSoulName = soulFullName;
            }
            return simplifiedSoulName;
        }

        public string GetSimplifiedLevel(string levelFullName)
        {
            string simplifiedLevelName = "";

            if (levelFullName.Contains("Level 50 • HongmoonLevel "))
            {
                simplifiedLevelName = levelFullName.Replace("Level 50 • HongmoonLevel ", "HM ");
            }
            else
            {
                simplifiedLevelName = levelFullName;
            }
            return simplifiedLevelName;
        }

        public Bitmap AvatarBmp(Character c)
        {
            Bitmap responseBmp = new Bitmap(378, 620);
            try
            {
                if (c.Avatar != null || c.Avatar == "")
                {
                    System.Net.WebRequest request =
                        System.Net.WebRequest.Create(
                        c.Avatar);
                    System.Net.WebResponse response = request.GetResponse();
                    System.IO.Stream responseStream =
                        response.GetResponseStream();
                    Bitmap bmp = new Bitmap(responseStream);
                    HttpContext.Current.Response.Flush();

                    responseBmp = bmp;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
            return responseBmp;

        }
    }
}