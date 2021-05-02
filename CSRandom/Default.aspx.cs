using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSRandom
{
    public partial class _Default : Page
    {

        public void gameTypeCheckedChanged(Object sender, EventArgs e)
        {
            helmetChk.Enabled = helmetChk.Checked = armorChk.Enabled = armorChk.Checked = radioCompetitive.Checked;
        }


        public void GetWeapons_Click(Object sender, EventArgs e)
        {
            Side side = Side.CounterTerrorist;
            if (tRb.Checked)
                side = Side.Terrorist;
            try
            {
                Convert.ToInt32(amountTxt.Text);
            }
            catch { return; }
            var moneyTxt = amountTxt.Text;
            
            List<Weapon> weapons = Controller.GetWeapons(Convert.ToInt32(amountTxt.Text), side, topupCheck.Checked);
            var mainWeapon = weapons.Where(x => x.Type == Type.Heavy || x.Type == Type.Rifle || x.Type == Type.Smg).FirstOrDefault();
            if (mainWeapon != null)
                mainWeaponLbl.Text = mainWeapon.Name;
            else
                mainWeaponLbl.Text = "None";
            var pistol = weapons.Where(x => x.Type == Type.Pistol).FirstOrDefault();
            if (pistol != null)
                pistolLbl.Text = pistol.Name;
            else
                pistolLbl.Text = "None";
            var grenades = weapons.Where(x => x.Type == Type.Grenade);
            if (grenades.Count() > 0)
            {
                grenadeLbl.Text = "";
                foreach (var nade in grenades)
                {
                    grenadeLbl.Text += nade.Name + ", ";
                }
                grenadeLbl.Text.Trim().TrimEnd(',');
            }
            else
                grenadeLbl.Text = "None";
            var keys = "";
            if (mainWeapon != null)
            {
                keys += (int)mainWeapon.Type + ", " + mainWeapon.Key + ", ";
            }
            if (pistol != null)
            {
                keys += (int)pistol.Type + ", " + pistol.Key + ", ";
            }
            if (grenades.Count() > 0)
            {
                keys += (int)grenades.First().Type + ", ";
                foreach (var nade in grenades)
                {
                    keys += nade.Key + ", ";
                }
            }
            keysLbl.Text = keys;
            topupCheck.Checked = false;
        }
    }
}