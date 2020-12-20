using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace CSRandom
{
    public class Controller
    {
        public static List<Weapon> Weapons = new List<Weapon>();
        public static int money;
        public static int next;

        public static List<Weapon> GetWeapons(int _money, Side side, bool topup)
        {
            money = _money;
            if (Weapons.Count == 0)
            {
                CreateWeapons();
            }
            IOrderedEnumerable<Weapon> allWeapons = Weapons.Where(w => w.Side == side || w.Side == Side.Both).OrderBy(w => w.Type).ThenBy(x => x.Key);
            List<Weapon> selectedWeapons = new List<Weapon>();
            next = RandomNumber.Between(2, 4);
            Weapon weapon = new Weapon();
            if (!topup)
            {
                if (money >= 5200)
                {
                    next = RandomNumber.Between(1, 3);
                    if (next == 1)
                    {
                        weapon = new Weapon();
                        weapon.Type = Type.Heavy;
                        weapon.Name = "M249";
                        weapon.Price = 5200;
                        weapon.Side = Side.Both;
                        weapon.Key = 4;
                        weapon.BuyName = "m249";
                        Weapons.Add(weapon);
                    }
                    else if (next == 2)
                    {
                        if (side == Side.Terrorist)
                        {
                            weapon = new Weapon();
                            weapon.Type = Type.Rifle;
                            weapon.Name = "G3SG1";
                            weapon.Price = 5000;
                            weapon.Side = Side.Terrorist;
                            weapon.Key = 6;
                            weapon.BuyName = "g3sg1";
                            Weapons.Add(weapon);
                        }
                        else
                        {
                            weapon = new Weapon();
                            weapon.Type = Type.Rifle;
                            weapon.Name = "SCAR-20";
                            weapon.Price = 5000;
                            weapon.Side = Side.CounterTerrorist;
                            weapon.Key = 6;
                            weapon.BuyName = "scar20";
                            Weapons.Add(weapon);
                        }
                    }
                    else
                    {
                        weapon = new Weapon();
                        weapon.Type = Type.Rifle;
                        weapon.Name = "AWP";
                        weapon.Price = 4750;
                        weapon.Side = Side.Both;
                        weapon.Key = 5;
                        weapon.BuyName = "awp";
                    }
                    selectedWeapons.Add(weapon);
                    money -= weapon.Price;
                }
                else
                {
                    weapon = GetWeaponType(allWeapons, (Type)next, selectedWeapons);
                    if (weapon != null)
                        selectedWeapons.Add(weapon);
                }

            }
            int grenadeCount = 0;
            if (selectedWeapons.Count == 0 || topup)
            {
                weapon = GetWeaponType(allWeapons, Type.Pistol, selectedWeapons);
                if (weapon != null)
                    selectedWeapons.Add(weapon);
            }
            int percentage = RandomNumber.Between(0, 100);
            if (percentage >= 25 && percentage < 50)
                grenadeCount = 1;
            else if (percentage >= 50 && percentage < 75)
                grenadeCount = 2;
            else if (percentage >= 75)
                grenadeCount = 3;

            while (grenadeCount > 0)
            {
                weapon = GetWeaponType(allWeapons, Type.Grenade, selectedWeapons);
                if (weapon == null)
                    break;
                selectedWeapons.Add(weapon);
                grenadeCount--;
            }
            WriteToFile(selectedWeapons);
            return selectedWeapons;
        }

        public static List<Weapon> GetNadesAndGun(int _money, Side side)
        {
            money = _money;
            IOrderedEnumerable<Weapon> allWeapons = Weapons.Where(w => w.Side == side || w.Side == Side.Both).OrderBy(w => w.Type).ThenBy(x => x.Key);
            List<Weapon> selectedWeapons = new List<Weapon>();
            next = RandomNumber.Between(2, 4);
            Weapon weapon = GetWeaponType(allWeapons, (Type)next, selectedWeapons);
            if (weapon != null)
                selectedWeapons.Add(weapon);

            int grenadeCount = 0;
            if (weapon == null)
            {
                weapon = GetWeaponType(allWeapons, Type.Pistol, selectedWeapons);
                if (weapon != null)
                    selectedWeapons.Add(weapon);
            }
            int percentage = RandomNumber.Between(0, 100);
            if (percentage >= 25 && percentage < 50)
                grenadeCount = 1;
            else if (percentage >= 50 && percentage < 75)
                grenadeCount = 2;
            else if (percentage >= 75)
                grenadeCount = 3;

            while (grenadeCount > 0)
            {
                weapon = GetWeaponType(allWeapons, Type.Grenade, selectedWeapons);
                if (weapon == null)
                    break;
                selectedWeapons.Add(weapon);
                grenadeCount--;
            }
            WriteToFile(selectedWeapons);
            return selectedWeapons;
        }

        private static void WriteToFile(List<Weapon> selectedWeapons)
        {
            StringBuilder txt = new StringBuilder();
            foreach (var w in selectedWeapons)
            {
                txt.Append("buy " + w.BuyName + "; ");
            }
            //            File.WriteAllText(@"\\DESKTOP-5QJ7FUL\c$\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive\csgo\cfg\buytest2.cfg", txt.ToString());
        }

        public static Weapon GetWeaponType(IEnumerable<Weapon> allWeapons, Type type, List<Weapon> grenades)
        {
            Weapon weapon = null;

            switch (type)
            {
                case Type.Grenade:
                    var nades = allWeapons.Except(grenades).Where(w => w.Type == Type.Grenade && w.Price <= money).ToList();
                    if (nades.Count() == 0)
                        break;
                    else
                    {
                        next = RandomNumber.Between(0, nades.Count() - 1);
                        weapon = nades[next];
                        money -= weapon.Price;
                    }
                    break;
                default:
                    var weapons = allWeapons.Where(w => w.Type == type).ToList();
                    next = RandomNumber.Between(0, weapons.Count() - 1);
                    weapon = weapons[next];
                    if (weapon.Price > money || weapon.Price == 0)
                    {
                        weapon = null;
                        break;
                    }
                    money -= weapon.Price;
                    break;
            }

            return weapon;
        }


        public static void CreateWeapons()
        {
            var weapon = new Weapon();
            weapon.Type = Type.Pistol;
            weapon.Name = "P250";
            weapon.Price = 300;
            weapon.Side = Side.Both;
            weapon.Key = 3;
            weapon.BuyName = "p250";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Pistol;
            weapon.Name = "Glock-18";
            weapon.Price = 0;
            weapon.Side = Side.Terrorist;
            weapon.Key = 1;
            weapon.BuyName = "glock18";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Pistol;
            weapon.Name = "Tec-9";
            weapon.Price = 500;
            weapon.Side = Side.Terrorist;
            weapon.Key = 4;
            weapon.BuyName = "tec9";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Pistol;
            weapon.Name = "Desert Eagle";
            weapon.Price = 700;
            weapon.Side = Side.Both;
            weapon.Key = 5;
            weapon.BuyName = "deagle";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Heavy;
            weapon.Name = "Nova";
            weapon.Price = 1050;
            weapon.Side = Side.Both;
            weapon.Key = 1;
            weapon.BuyName = "nova";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Heavy;
            weapon.Name = "XM1014";
            weapon.Price = 2000;
            weapon.Side = Side.Both;
            weapon.Key = 2;
            weapon.BuyName = "xm1014";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Heavy;
            weapon.Name = "Sawed Off";
            weapon.Price = 1100;
            weapon.Side = Side.Terrorist;
            weapon.Key = 3;
            weapon.BuyName = "mag7";
            Weapons.Add(weapon);

            //weapon = new Weapon();
            //weapon.Type = Type.Heavy;
            //weapon.Name = "M249";
            //weapon.Price = 5200;
            //weapon.Side = Side.Both;
            //weapon.Key = 4;
            //weapon.BuyName = "m249";
            //Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Heavy;
            weapon.Name = "Negev";
            weapon.Price = 1700;
            weapon.Side = Side.Both;
            weapon.Key = 5;
            weapon.BuyName = "negev";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Smg;
            weapon.Name = "MAC-10";
            weapon.Price = 1050;
            weapon.Side = Side.Terrorist;
            weapon.Key = 1;
            weapon.BuyName = "mac10";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Smg;
            weapon.Name = "MP5-SD";
            weapon.Price = 1500;
            weapon.Side = Side.Both;
            weapon.Key = 2;
            weapon.BuyName = "mp7";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Smg;
            weapon.Name = "UMP-45";
            weapon.Price = 1200;
            weapon.Side = Side.Both;
            weapon.Key = 3;
            weapon.BuyName = "ump45";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Smg;
            weapon.Name = "P90";
            weapon.Price = 2350;
            weapon.Side = Side.Both;
            weapon.Key = 4;
            weapon.BuyName = "p90";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Smg;
            weapon.Name = "PP-Bizon";
            weapon.Price = 1400;
            weapon.Side = Side.Both;
            weapon.Key = 5;
            weapon.BuyName = "bizon";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "Galil-AR";
            weapon.Price = 2000;
            weapon.Side = Side.Terrorist;
            weapon.Key = 1;
            weapon.BuyName = "galilar";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "AK-47";
            weapon.Price = 2700;
            weapon.Side = Side.Terrorist;
            weapon.Key = 2;
            weapon.BuyName = "ak47";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "SSG 08";
            weapon.Price = 1700;
            weapon.Side = Side.Both;
            weapon.Key = 3;
            weapon.BuyName = "ssg08";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "SG 553";
            weapon.Price = 3000;
            weapon.Side = Side.Terrorist;
            weapon.Key = 4;
            weapon.BuyName = "sg556";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "AWP";
            weapon.Price = 4750;
            weapon.Side = Side.Both;
            weapon.Key = 5;
            weapon.BuyName = "awp";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "G3SG1";
            weapon.Price = 5000;
            weapon.Side = Side.Terrorist;
            weapon.Key = 6;
            weapon.BuyName = "g3sg1";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Grenade;
            weapon.Name = "Molotov";
            weapon.Price = 400;
            weapon.Side = Side.Terrorist;
            weapon.Key = 1;
            weapon.BuyName = "molotov";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Grenade;
            weapon.Name = "Decoy Grenade";
            weapon.Price = 50;
            weapon.Side = Side.Both;
            weapon.Key = 2;
            weapon.BuyName = "decoy";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Grenade;
            weapon.Name = "Flashbang";
            weapon.Price = 200;
            weapon.Side = Side.Both;
            weapon.Key = 3;
            weapon.BuyName = "flashbang";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Grenade;
            weapon.Name = "High Explosive Grenade";
            weapon.Price = 300;
            weapon.Side = Side.Both;
            weapon.Key = 4;
            weapon.BuyName = "hegrenade";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Grenade;
            weapon.Name = "Smoke Grenade";
            weapon.Price = 300;
            weapon.Key = 1;
            weapon.Side = Side.Both;
            weapon.Key = 5;
            weapon.BuyName = "smokegrenade";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Pistol;
            weapon.Name = "USP-S";
            weapon.Key = 1;
            weapon.Price = 0;
            weapon.Side = Side.CounterTerrorist;
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Pistol;
            weapon.Name = "Five-Seven";
            weapon.Price = 500;
            weapon.Side = Side.CounterTerrorist;
            Weapons.Add(weapon);
            weapon.Key = 4;
            weapon.BuyName = "fn57";
            weapon = new Weapon();

            weapon.Type = Type.Heavy;
            weapon.Name = "MAG-7";
            weapon.Price = 1300;
            weapon.Side = Side.CounterTerrorist;
            weapon.Key = 3;
            weapon.BuyName = "mag7";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Smg;
            weapon.Name = "MP9";
            weapon.Price = 1250;
            weapon.Side = Side.CounterTerrorist;
            weapon.Key = 1;
            weapon.BuyName = "mp9";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "Famas";
            weapon.Price = 2250;
            weapon.Side = Side.CounterTerrorist;
            weapon.Key = 1;
            weapon.BuyName = "famas";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "M4A1-S";
            weapon.Price = 3100;
            weapon.Side = Side.CounterTerrorist;
            weapon.Key = 2;
            weapon.BuyName = "m4a1_silencer";
            Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "AUG";
            weapon.Price = 3300;
            weapon.Side = Side.CounterTerrorist;
            weapon.Key = 4;
            weapon.BuyName = "aug";
            Weapons.Add(weapon);

            //weapon = new Weapon();
            //weapon.Type = Type.Rifle;
            //weapon.Name = "SCAR-20";
            //weapon.Price = 5000;
            //weapon.Side = Side.CounterTerrorist;
            //weapon.Key = 6;
            //weapon.BuyName = "scar20";
            //Weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Grenade;
            weapon.Name = "Incendiary Grenade";
            weapon.Price = 600;
            weapon.Side = Side.CounterTerrorist;
            weapon.Key = 1;
            weapon.BuyName = "incgrenade";
            Weapons.Add(weapon);
            weapon = new Weapon();

            weapon.Type = Type.Equipment;
            weapon.Name = "Zeus x-27";
            weapon.Price = 200;
            weapon.Side = Side.Both;
            weapon.Key = 3;
            weapon.BuyName = "taser23";
            Weapons.Add(weapon);
        }
    }
}