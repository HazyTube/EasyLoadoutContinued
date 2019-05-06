/*

Developed by: HazyTube
Name: EasyLoadoutContinued
Released on: LSPDFR and GitHub

*/

namespace EasyLoadoutContinued.Utils
{
    class LoadoutData
    {
        public string LoadoutNumber { get; set; }
        public string LoadoutConfig { get; set; }

        public LoadoutData(string num, string config)
        {
            LoadoutNumber = num;
            LoadoutConfig = config;
        }
    }
}
