/*

Author: HazyTube
Name: EasyLoadoutContinued
Released on: LSPDFR and GitHub

*/

using Rage;
using System;
using System.Net;

namespace EasyLoadoutContinued.Utils
{

    internal static class Updater
    {
        private static readonly WebClient wc = new WebClient();

        public static int CheckUpdate()
        {
            string response = null;

            try
            {
                Logger.DebugLog("Fetching latest plugin version from GitHub");
                response = wc.DownloadStringTaskAsync(new Uri("https://raw.githubusercontent.com/HazyTube/EasyLoadoutContinued/master/LatestVersion")).Result;
            }
            catch (Exception)
            {
                Game.LogTrivial("Checking version of plugin 'EasyLoadoutContinued' Failed");
            }

            //If we get a null respone then the download failed and we just return -2 and inform user of failing the download
            if (string.IsNullOrWhiteSpace(response))
            {
                return -2;
            }

            Global.Application.LatestVersion = response;

            Version current = new Version(Global.Application.CurrentVersion);
            Version latest = new Version(Global.Application.LatestVersion);

            //This is where we're checking the results
            //If the plugin is newer than what's being reported then we'll return 1 (This will just log the issue, no notification)
            //If the plugin is older than what's being reported then we'll return -1(This Logs aswell as displays a notification)
            //If the plugin is the same version as what's being reported than we'll return 0 (This logs & displays notification that it loaded successfully)
            if (current.CompareTo(latest) > 0)
            {
                return 1;
            }
            else if (current.CompareTo(latest) < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
