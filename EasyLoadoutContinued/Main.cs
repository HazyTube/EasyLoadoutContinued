/*

Developed by: HazyTube
Name: EasyLoadoutContinued
Released on: LSPDFR and GitHub

*/

using Rage;
using System.IO;
using System.Reflection;
using EasyLoadoutContinued.Utils;
using LSPD_First_Response.Mod.API;

[assembly: Rage.Attributes.Plugin("EasyLoadoutContinued", Author = "HazyTube", Description = "Loads specified loadout when going on duty aswell as having a keybind that you can press to give you the loadout anytime.")]

namespace EasyLoadoutContinued
{
    public class Main : Plugin
    {
        public override void Initialize()
        {
            //IMPORTANT! This will set if the plugin is in beta or not, and also sets the beta version
            Globals.Application.IsPluginInBeta = false;
            Globals.Application.CurrentBetaVersion = "-b0";
            //------------------------------------------------------------------------------------------

            Globals.Application.PluginName = "EasyLoadoutContinued";
            
            Functions.OnOnDutyStateChanged += DutyStateChange;
            
            if (Globals.Application.IsPluginInBeta)
            {
                Logger.Log($"{Globals.Application.PluginName} {Assembly.GetExecutingAssembly().GetName().Version.ToString()}{Globals.Application.CurrentBetaVersion} has been  initialized.");
            }
            else
            {
                Logger.Log($"{Globals.Application.PluginName} {Assembly.GetExecutingAssembly().GetName().Version.ToString()} has been  initialized.");
            }

            //This sets the currentversion
            Globals.Application.CurrentVersion = $"{Assembly.GetExecutingAssembly().GetName().Version}";

            Globals.Application.ConfigPath = "Plugins/LSPDFR/EasyLoadoutContinued/";
            
            Globals.Application.ConfigFileName = "EasyLoadoutContinued.ini";
        }

        public void DutyStateChange(bool OnDuty)
        {
            if (OnDuty)
            {
                Game.LogTrivial($"--------------------------------------{Globals.Application.PluginName} startup log--------------------------------------");

                if (!Globals.Application.IsPluginInBeta)
                {
                    int versionStatus = Updater.CheckUpdate();

                    if (versionStatus == -1)
                    {
                        Notifier.StartUpNotificationOutdated();
                        Logger.Log($"Plugin is out of date. (Current Version: {Globals.Application.CurrentVersion}) - (Latest Version: {Globals.Application.LatestVersion})");
                    }
                    else if (versionStatus == -2)
                    {
                        Logger.Log("There was an issue checking plugin versions, the plugin may be out of date!");
                    }
                    else if (versionStatus == 1)
                    {
                        Logger.Log("Current version of plugin is higher than the version reported on the official GitHub, this could be an error that you may want to report!");
                        Notifier.StartUpNotification();
                    }
                    else
                    {
                        Notifier.StartUpNotification();
                        Logger.Log($"Plugin version v{Globals.Application.CurrentVersion} loaded succesfully");
                    }
                }

                if (Globals.Application.IsPluginInBeta)
                {
                    int betaVersionStatus = Updater.CheckBetaUpdate();
                    int versionStatus = Updater.CheckUpdate();

                    if (betaVersionStatus == -1 || versionStatus == -1)
                    {
                        Notifier.StartUpNotificationBetaOutdated();
                        Logger.Log($"Plugin is out of date.");
                        Logger.Log($"(Current Beta Version: {Globals.Application.CurrentVersion}{Globals.Application.CurrentBetaVersion})");
                        Logger.Log($"(Latest Beta Version: {Globals.Application.LatestVersion}{Globals.Application.LatestBetaVersion})");
                    }
                    else if (betaVersionStatus == -2 || betaVersionStatus == 0 || versionStatus == -2)
                    {
                        Logger.Log("There was an issue checking plugin versions, the plugin may be out of date!");
                    }
                    else if (betaVersionStatus == 1 || versionStatus == 1)
                    {
                        Notifier.StartUpNotificationBeta();
                        Logger.Log($"Plugin Version v{Globals.Application.CurrentVersion}{Globals.Application.CurrentBetaVersion} loaded successfully");
                        Logger.Log("Plugin is in beta!");
                    }
                }

                if (File.Exists(Globals.Application.ConfigPath + Globals.Application.ConfigFileName))
                {
                    Settings.LoadSettings();
                }
                else
                {
                    Logger.Log($"[ERROR] Config file {Globals.Application.ConfigFileName} does not exist, please check if the file is located in {Globals.Application.ConfigPath} before reporting this as a bug");
                    Logger.Log("[WARNING] The plugin will use the default keybindings now");
                }
                
                StartPlugin();
            }
        }

        private static void StartPlugin()
        {
            GameFiber.StartNew(delegate { Core.RunPlugin(); });
        }

        public override void Finally()
        {
            if (Globals.Application.IsPluginInBeta)
            {
                Logger.Log($"EasyLoadoutContinued {Globals.Application.CurrentVersion}{Globals.Application.CurrentBetaVersion} has been unloaded");
            }
            else
            {
                Logger.Log($"EasyLoadoutContinued {Globals.Application.CurrentVersion} has been unloaded");
            }
        }
    }
}
