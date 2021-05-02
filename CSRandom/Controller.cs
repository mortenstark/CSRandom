using System.Collections.Generic;
using System.Linq;

namespace CSRandom
{
    public class Controller
    {
        public static readonly List<Weapon> WEAPONS = CreateWeapons();
        public static int money;
        public static int next;

        public static List<Weapon> BuyWeapons(int _money, Side side, bool topup, bool donation)
        {
            money = _money;
            IEnumerable<Weapon> allWeaponsForType = WEAPONS.Where(w => w.Side == side || w.Side == Side.Both).OrderBy(w => w.Type).ThenBy(x => x.Key);
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
                        weapon = WEAPONS.Where(w => w.BuyName == "m249").FirstOrDefault();
                    }
                    else if (next == 2)
                    {
                        if (side == Side.Terrorist)
                        {
                            weapon = WEAPONS.Where(w => w.BuyName == "g3sg1").FirstOrDefault();
                        }
                        else
                        {
                            weapon = WEAPONS.Where(w => w.BuyName == "scar20").FirstOrDefault();
                        }
                    }
                    else
                    {
                        weapon = WEAPONS.Where(w => w.BuyName == "awp").FirstOrDefault();
                    }
                    selectedWeapons.Add(weapon);
                    money -= weapon.Price;
                }
                else
                {
                    weapon = BuyWeaponType(allWeaponsForType, (Type)next);
                    if (weapon != null)
                        selectedWeapons.Add(weapon);
                }

            }
            if (donation)
            {
                return selectedWeapons;
            }
            int grenadeCount = 0;
            if (selectedWeapons.Count == 0 || topup)
            {
                weapon = BuyWeaponType(allWeaponsForType, Type.Pistol);
                if (weapon != null)
                    selectedWeapons.Add(weapon);
            }
            int percentage = RandomNumber.Between(0, 100);
            List<Weapon> grenades = new List<Weapon>();
            if (percentage >= 25 && percentage < 50)
                grenadeCount = 1;
            else if (percentage >= 50 && percentage < 75)
                grenadeCount = 2;
            else if (percentage >= 75)
                grenadeCount = 3;

            while (grenadeCount > 0)
            {
                var grenade = BuyGrenade(allWeaponsForType, grenades);
                if (grenade == null)
                    break;
                grenades.Add(grenade);
                grenadeCount--;
            }
            selectedWeapons.AddRange(grenades);
            return selectedWeapons;
        }



        public static Weapon BuyGrenade(IEnumerable<Weapon> allWeapons, List<Weapon> grenades)
        {
            Weapon weapon = null;

            var nades = allWeapons.Where(w => w.Type == Type.Grenade && w.Price <= money && !grenades.Contains(w)).ToList();
            if (nades.Count() == 0)
                return weapon;
            else
            {
                next = RandomNumber.Between(0, nades.Count() - 1);
                weapon = nades[next];
                money -= weapon.Price;
            }
            return weapon;
        }

        public static Weapon BuyWeaponType(IEnumerable<Weapon> allWeapons, Type type)
        {
            Weapon weapon = null;
            var weapons = allWeapons.Where(w => w.Type == type).ToList();
            next = RandomNumber.Between(0, weapons.Count() - 1);
            weapon = weapons[next];
            if (weapon.Price > money || weapon.Price == 0)
            {
                weapon = null;
            }
            else
            {
                money -= weapon.Price;
            }
            return weapon;
        }


        public static List<Weapon> CreateWeapons()
        {
            var weapons = new List<Weapon>();
            var weapon = new Weapon();
            weapon.Type = Type.Pistol;
            weapon.Name = "P250";
            weapon.Price = 300;
            weapon.Side = Side.Both;
            weapon.Key = 3;
            weapon.BuyName = "p250";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Pistol;
            weapon.Name = "USP-S";
            weapon.Key = 1;
            weapon.Price = 0;
            weapon.Side = Side.CounterTerrorist;
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Pistol;
            weapon.Name = "Five-Seven";
            weapon.Price = 500;
            weapon.Side = Side.CounterTerrorist;
            weapons.Add(weapon);
            weapon.Key = 4;
            weapon.BuyName = "fn57";

            weapon = new Weapon();
            weapon.Type = Type.Pistol;
            weapon.Name = "Glock-18";
            weapon.Price = 0;
            weapon.Side = Side.Terrorist;
            weapon.Key = 1;
            weapon.BuyName = "glock18";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Pistol;
            weapon.Name = "Tec-9";
            weapon.Price = 500;
            weapon.Side = Side.Terrorist;
            weapon.Key = 4;
            weapon.BuyName = "tec9";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Pistol;
            weapon.Name = "Desert Eagle";
            weapon.Price = 700;
            weapon.Side = Side.Both;
            weapon.Key = 5;
            weapon.BuyName = "deagle";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Heavy;
            weapon.Name = "Nova";
            weapon.Price = 1050;
            weapon.Side = Side.Both;
            weapon.Key = 1;
            weapon.BuyName = "nova";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Heavy;
            weapon.Name = "XM1014";
            weapon.Price = 2000;
            weapon.Side = Side.Both;
            weapon.Key = 2;
            weapon.BuyName = "xm1014";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Heavy;
            weapon.Name = "Sawed Off";
            weapon.Price = 1100;
            weapon.Side = Side.Terrorist;
            weapon.Key = 3;
            weapon.BuyName = "mag7";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Heavy;
            weapon.Name = "Negev";
            weapon.Price = 1700;
            weapon.Side = Side.Both;
            weapon.Key = 5;
            weapon.BuyName = "negev";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Heavy;
            weapon.Name = "MAG-7";
            weapon.Price = 1300;
            weapon.Side = Side.CounterTerrorist;
            weapon.Key = 3;
            weapon.BuyName = "mag7";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Heavy;
            weapon.Name = "M249";
            weapon.Price = 5200;
            weapon.Side = Side.Both;
            weapon.Key = 4;
            weapon.BuyName = "m249";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Smg;
            weapon.Name = "MAC-10";
            weapon.Price = 1050;
            weapon.Side = Side.Terrorist;
            weapon.Key = 1;
            weapon.BuyName = "mac10";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Smg;
            weapon.Name = "MP5-SD";
            weapon.Price = 1500;
            weapon.Side = Side.Both;
            weapon.Key = 2;
            weapon.BuyName = "mp7";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Smg;
            weapon.Name = "UMP-45";
            weapon.Price = 1200;
            weapon.Side = Side.Both;
            weapon.Key = 3;
            weapon.BuyName = "ump45";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Smg;
            weapon.Name = "P90";
            weapon.Price = 2350;
            weapon.Side = Side.Both;
            weapon.Key = 4;
            weapon.BuyName = "p90";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Smg;
            weapon.Name = "MP9";
            weapon.Price = 1250;
            weapon.Side = Side.CounterTerrorist;
            weapon.Key = 1;
            weapon.BuyName = "mp9";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Smg;
            weapon.Name = "PP-Bizon";
            weapon.Price = 1400;
            weapon.Side = Side.Both;
            weapon.Key = 5;
            weapon.BuyName = "bizon";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "Galil-AR";
            weapon.Price = 2000;
            weapon.Side = Side.Terrorist;
            weapon.Key = 1;
            weapon.BuyName = "galilar";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "AK-47";
            weapon.Price = 2700;
            weapon.Side = Side.Terrorist;
            weapon.Key = 2;
            weapon.BuyName = "ak47";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "SSG 08";
            weapon.Price = 1700;
            weapon.Side = Side.Both;
            weapon.Key = 3;
            weapon.BuyName = "ssg08";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "SG 553";
            weapon.Price = 3000;
            weapon.Side = Side.Terrorist;
            weapon.Key = 4;
            weapon.BuyName = "sg556";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "Famas";
            weapon.Price = 2250;
            weapon.Side = Side.CounterTerrorist;
            weapon.Key = 1;
            weapon.BuyName = "famas";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "M4A1-S";
            weapon.Price = 3100;
            weapon.Side = Side.CounterTerrorist;
            weapon.Key = 2;
            weapon.BuyName = "m4a1_silencer";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "AUG";
            weapon.Price = 3300;
            weapon.Side = Side.CounterTerrorist;
            weapon.Key = 4;
            weapon.BuyName = "aug";
            weapons.Add(weapon);


            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "AWP";
            weapon.Price = 4750;
            weapon.Side = Side.Both;
            weapon.Key = 5;
            weapon.BuyName = "awp";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "G3SG1";
            weapon.Price = 5000;
            weapon.Side = Side.Terrorist;
            weapon.Key = 6;
            weapon.BuyName = "g3sg1";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Rifle;
            weapon.Name = "SCAR-20";
            weapon.Price = 5000;
            weapon.Side = Side.CounterTerrorist;
            weapon.Key = 6;
            weapon.BuyName = "scar20";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Grenade;
            weapon.Name = "Molotov";
            weapon.Price = 400;
            weapon.Side = Side.Terrorist;
            weapon.Key = 1;
            weapon.BuyName = "molotov";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Grenade;
            weapon.Name = "Decoy Grenade";
            weapon.Price = 50;
            weapon.Side = Side.Both;
            weapon.Key = 2;
            weapon.BuyName = "decoy";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Grenade;
            weapon.Name = "Flashbang";
            weapon.Price = 200;
            weapon.Side = Side.Both;
            weapon.Key = 3;
            weapon.BuyName = "flashbang";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Grenade;
            weapon.Name = "High Explosive Grenade";
            weapon.Price = 300;
            weapon.Side = Side.Both;
            weapon.Key = 4;
            weapon.BuyName = "hegrenade";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Grenade;
            weapon.Name = "Smoke Grenade";
            weapon.Price = 300;
            weapon.Key = 1;
            weapon.Side = Side.Both;
            weapon.Key = 5;
            weapon.BuyName = "smokegrenade";
            weapons.Add(weapon);

            weapon = new Weapon();
            weapon.Type = Type.Grenade;
            weapon.Name = "Incendiary Grenade";
            weapon.Price = 600;
            weapon.Side = Side.CounterTerrorist;
            weapon.Key = 1;
            weapon.BuyName = "incgrenade";
            weapons.Add(weapon);
            weapon = new Weapon();

            weapon.Type = Type.Equipment;
            weapon.Name = "Zeus x-27";
            weapon.Price = 200;
            weapon.Side = Side.Both;
            weapon.Key = 3;
            weapon.BuyName = "taser23";
            weapons.Add(weapon);
            return weapons;
        }
    }
}