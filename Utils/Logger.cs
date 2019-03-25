/*

Author: HazyTube
Name: EasyLoadoutContinued
Released on: LSPDFR and GitHub

*/

using Rage;

namespace EasyLoadoutContinued.Utils
{
    internal static class Logger
    {
        //ONLY INCLUDE PLUGIN NAME
        private const string LogPrefix = "EasyLoadoutContinued";

        //Simple log line
        internal static void Log(string LogLine)
        {
            string log = string.Format("[{0}]: {1}", LogPrefix, LogLine);

            Game.LogTrivial(log);
        }

        //Simple log line that will be ran only if the global setting for debug logging is enabled
        internal static void DebugLog(string LogLine)
        {
            if (Global.Application.DebugLogging)
            {
                string log = string.Format("[{0}][DEBUG]: {1}", LogPrefix, LogLine);

                Game.LogTrivial(log);
            }
        }
    }
}