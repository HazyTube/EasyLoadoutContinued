/*

Developed by: HazyTube
Name: EasyLoadoutContinued
Released on: LSPDFR and GitHub

*/

using EasyLoadoutContinued.Utils;
using Rage;

namespace EasyLoadoutContinued
{
    internal static class Logger
    {
        private const string LogPrefix = "EasyLoadoutContinued";

        internal static void Log(string LogLine)
        {
            if (Globals.Application.IsPluginInBeta == true)
            {
                string log = string.Format("[{0}][BETA]: {1}", LogPrefix, LogLine);

                Game.LogTrivial(log);
            }
            else if (Globals.Application.IsPluginInBeta == false)
            {
                string log = string.Format("[{0}]: {1}", LogPrefix, LogLine);

                Game.LogTrivial(log);
            }
        }

        internal static void DebugLog(string LogLine)
        {
            if (Globals.Application.DebugLogging)
            {
                string log = string.Format("[{0}][DEBUG]: {1}", LogPrefix, LogLine);

                Game.LogTrivial(log);
            }
        }
    }
}