/*

Developed by: HazyTube
Name: EasyLoadoutContinued
Released on: LSPDFR and GitHub

*/

using Rage;
using System.Windows.Forms;

namespace EasyLoadoutContinued.Utils
{
    internal static class Settings
    {
        private static InitializationFile initialiseFile(string filepath)
        {
            InitializationFile ini = new InitializationFile(filepath);
            ini.Create();
            return ini;
        }

        public static string GetConfigFile(int count)
        {
            InitializationFile settings = initialiseFile(Globals.Application.ConfigPath + "EasyLoadoutContinued.ini");
            string tmp = settings.ReadString("MultiLoadout", "Loadout" + count);

            //We're just checking if the string is set to nothing because this is an indicator that it wasn't set in the config file thus something is wrong.
            if (tmp == "")
            {
                Logger.Log("Loadout" + count + " is not a valid config file. Please verify configs are valid and exist in " + Globals.Application.ConfigPath + " before reporting this as a potential bug.");
            }

            return tmp;
        }

        public static void LoadSettings()
        {
            InitializationFile settings = initialiseFile(Globals.Application.ConfigPath + "EasyLoadoutContinued.ini");

            Globals.Application.DebugLogging = LoadoutConfig.ToBoolean(settings.ReadString("General", "DebugLogging", "false"));
            Logger.DebugLog("General Config Loading Started.");

            KeysConverter kc = new KeysConverter();

            string OpenMenuKey, OpenMenuModifierKey, GiveLoadoutKey, GiveLoadoutModifierKey, DefaultLoadout, DefaultLoadout2;

            OpenMenuKey = settings.ReadString("Keybindings", "OpenMenu", "F8");
            OpenMenuModifierKey = settings.ReadString("Keybindings", "OpenMenuModifier", "None");
            GiveLoadoutKey = settings.ReadString("Keybindings", "GiveLoadout", "F7");
            GiveLoadoutModifierKey = settings.ReadString("Keybindings", "GiveLoadoutModifier", "None");

            Globals.Controls.OpenMenu = (Keys)kc.ConvertFromString(OpenMenuKey);
            Globals.Controls.OpenMenuModifier = (Keys)kc.ConvertFromString(OpenMenuModifierKey);
            Globals.Controls.GiveLoadout = (Keys)kc.ConvertFromString(GiveLoadoutKey);
            Globals.Controls.GiveLoadoutModifier = (Keys)kc.ConvertFromString(GiveLoadoutModifierKey);


            DefaultLoadout = settings.ReadString("General", "DefaultLoadout", "Loadout1");
            DefaultLoadout2 = settings.ReadString("General", DefaultLoadout, "Loadout1");
            Globals.Application.DefaultLoadout = new LoadoutData(DefaultLoadout, DefaultLoadout2);
            Globals.Application.LoadoutCount = settings.ReadInt32("MultiLoadout", "LoadoutCount", 3);

            //Ammo Count
            Globals.LoadoutAmmo.PistolAmmo = settings.ReadInt16("Ammo", "PistolAmmo", 10000);
            Globals.LoadoutAmmo.MGAmmo = settings.ReadInt16("Ammo", "MGAmmo", 10000);
            Globals.LoadoutAmmo.ShotgunAmmo = settings.ReadInt16("Ammo", "ShotgunAmmo", 10000);
            Globals.LoadoutAmmo.RifleAmmo = settings.ReadInt16("Ammo", "RifleAmmo", 10000);
            Globals.LoadoutAmmo.SniperAmmo = settings.ReadInt16("Ammo", "SniperAmmo", 10000);
            Globals.LoadoutAmmo.HeavyAmmo = settings.ReadInt16("Ammo", "HeavyAmmo", 10000);
            Globals.LoadoutAmmo.ThrowableCount = settings.ReadInt16("Ammo", "ThrowableCount", 10000);

            Logger.DebugLog("General Config Loading Finished.");
            Logger.Log("[GENERAL] DebugLogging is set to " + Globals.Application.DebugLogging);
            Logger.Log("[GENERAL] Default loadout is " + Globals.Application.DefaultLoadout);
            Logger.Log("[KEYBINDINGS] OpenMenu is set to " + Globals.Controls.OpenMenu);
            Logger.Log("[KEYBINDINGS] OpenMenuModifier is set to " + Globals.Controls.OpenMenuModifier);
            Logger.Log("[KEYBINDINGS] GiveLoadout is set to " + Globals.Controls.GiveLoadout);
            Logger.Log("[KEYBINDINGS] GiveLoadoutModifier is set to " + Globals.Controls.GiveLoadoutModifier);
            Game.LogTrivial("-----------------------------------------------------------------------------------------------------");
        }
    }
}