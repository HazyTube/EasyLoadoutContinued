/*

Developed by: HazyTube
Name: EasyLoadoutContinued
Released on: LSPDFR and GitHub

*/

using Rage;
using System.Reflection;
using EasyLoadoutContinued.Utils;

namespace EasyLoadoutContinued
{
    internal static class Notifier
    {
        private const string NotificationPrefix = "Easy Loadout Continued";

        internal static void DisplayNotification(string Subtitle, string Body)
        {
            Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", NotificationPrefix, $"~b~{Subtitle}~s~",
                Body);
            Logger.DebugLog("Notification Displayed");
        }

        internal static void StartUpNotification()
        {
            Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", NotificationPrefix,
                "~y~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ~o~by HazyTube",
                "~b~Has been loaded.");
            Logger.DebugLog("Startup Notification Sent.");
        }

        internal static void StartUpNotificationOutdated()
        {
            Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", NotificationPrefix,
                "~y~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ~o~by HazyTube",
                "~r~Plugin is out of date! Please update the plugin.");
            Logger.DebugLog("Startup Notification (Outdated) Sent.");
        }

        internal static void StartUpNotificationBeta()
        {
            Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", NotificationPrefix,
                "~y~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() +
                Globals.Application.CurrentBetaVersion + "~s~ ~o~by HazyTube",
                "~b~Has been loaded.~s~ \nPlugin is in beta!");
            Logger.DebugLog("Startup Notification (Beta) Sent.");
        }

        internal static void StartUpNotificationBetaOutdated()
        {
            Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", NotificationPrefix,
                "~y~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() +
                Globals.Application.CurrentBetaVersion + "~s~ ~o~by HazyTube",
                $"~r~Plugin is out of date!~s~ \nPlease download the latest beta version from GitHub. \nLatest Version: {Globals.Application.LatestVersion}{Globals.Application.LatestBetaVersion}");
            Logger.DebugLog("Startup Notification (Beta Outdated) Sent.");
        }
    }
}