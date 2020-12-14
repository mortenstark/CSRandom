using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSRandom
{
    public class Weapon
    {
        public Type Type { get; set; }
        public string Name { get; set; }

        public int Price { get; set; }
        public Side Side { get; set; }
        public int Key { get; set; }
        public int Percentage { get; set; }

        public string BuyName { get; set; }
    }


    public enum Type
    {
        Pistol = 1,
        Heavy = 2,
        Smg = 3,
        Rifle = 4,
        Equipment = 5,
        Grenade = 6
    }

    public enum Side 
    {
        Terrorist = 1,
        CounterTerrorist = 2,
        Both = 3
    }
}