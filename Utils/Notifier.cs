/*

Author: HazyTube
Name: EasyLoadoutContinued
Released on: LSPDFR and GitHub

*/

using Rage;
using System.Reflection;

namespace EasyLoadoutContinued.Utils
{
    internal static class Notifier
    {
        private const string NotificationPrefix = "EasyLoadoutContinued";

        internal static void Notify(string body)
        {
            string notice = string.Format("~p~[{0}]~s~: {1}", NotificationPrefix, body);
            Game.DisplayNotification(notice);
            Logger.DebugLog("Notification Sent.");
        }

        internal static void NotifySubtitle(string body)
        {
            string subtitle = string.Format("~p~[{0}]~s~: {1}", NotificationPrefix, body);
            Game.DisplaySubtitle(subtitle);
            Logger.DebugLog("Subtitle Sent.");
        }

        internal static void StartUpNotification()
        {
            Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "EasyLoadoutContinued", "~y~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ~g~by HazTybe", "~b~Has been loaded.");
            Logger.DebugLog("Startup Notification Sent.");
        }
    }
}