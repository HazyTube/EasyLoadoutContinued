/*

Author: HazyTube
Name: EasyLoadoutContinued
Released on: LSPDFR and GitHub

*/

using Rage;
using LSPD_First_Response.Mod.API;
using EasyLoadoutContinued.Utils;

[assembly: Rage.Attributes.Plugin("EasyLoadoutContinued", Author = "HazyTube", Description = "Loads specified loadout when going on duty aswell as having a keybind that you can press to give you the loadout anytime.")]

namespace EasyLoadoutContinued
{
    public class Main : Plugin
    {

        public override void Initialize()
        {
            Functions.OnOnDutyStateChanged += DutyStateChange;
            Global.Application.CurrentVersion = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
            Global.Application.ConfigPath = "Plugins/LSPDFR/EasyLoadoutContinued/Configs/";

        }

        public void DutyStateChange(bool OnDuty)
        {
            int versionStatus = Updater.CheckUpdate();
            if (versionStatus == -1)
            {
                Notifier.Notify("Plugin is out of date! (Current Version: ~r~" + Global.Application.CurrentVersion + " ~s~) - (Latest Version: ~g~" + Global.Application.LatestVersion + "~s~) Please update the plugin!");
                Logger.Log("Plugin is out of date. (Current Version: " + Global.Application.CurrentVersion + ") - (Latest Version: " + Global.Application.LatestVersion + ")");
            }
            else if (versionStatus == -2)
            {
                Logger.Log("There was an issue checking plugin versions, the plugin may be out of date!");
            }
            else if (versionStatus == 1)
            {
                Logger.Log("Current version of plugin is higher than the version reported on the official GitHub, this could be an error that you may want to report!");
            }
            else
            {
                Notifier.StartUpNotification();
                Logger.Log("Plugin Version v" + Global.Application.CurrentVersion + " loaded successfully");
            }

            //Loading general config
            Config.LoadConfig();

            StartPlugin();
        }

        private static void StartPlugin()
        {
            GameFiber.StartNew(delegate { Core.RunPlugin(); });
        }

        public override void Finally()
        {
            Logger.Log("Plugin unloaded successfully");
        }
    }
}
