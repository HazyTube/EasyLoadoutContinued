/*

Author: HazyTube
Name: EasyLoadoutContinued
Released on: LSPDFR and GitHub

*/

using Rage;

namespace EasyLoadoutContinued.Utils
{
    static class LoadoutConfig
    {
        private static string filePath;
        private static string[] AssaultRifleMagazines = new string[] { "COMPONENT_ASSAULTRIFLE_CLIP_01", "COMPONENT_ASSAULTRIFLE_CLIP_02", "COMPONENT_ASSAULTRIFLE_CLIP_03" };
        private static string[] CarbineRifleMagazines = new string[] { "COMPONENT_CARBINERIFLE_CLIP_01", "COMPONENT_CARBINERIFLE_CLIP_02", "COMPONENT_CARBINERIFLE_CLIP_03" };
        private static string[] AdvancedRifleMagazines = new string[] { "COMPONENT_ADVANCEDRIFLE_CLIP_01", "COMPONENT_ADVANCEDRIFLE_CLIP_02" };
        private static string[] SpecialCarbineMagazines = new string[] { "COMPONENT_SPECIALCARBINE_CLIP_01", "COMPONENT_SPECIALCARBINE_CLIP_02", "COMPONENT_SPECIALCARBINE_CLIP_03" };
        private static string[] BullpupRifleMagazines = new string[] { "COMPONENT_BULLPUPRIFLE_CLIP_01", "COMPONENT_BULLPUPRIFLE_CLIP_02" };
        private static string[] CompactRifleMagazines = new string[] { "COMPONENT_COMPACTRIFLE_CLIP_01", "COMPONENT_COMPACTRIFLE_CLIP_02", "COMPONENT_COMPACTRIFLE_CLIP_03" };

        //MK2 Variants
        private static string[] AssaultRifleMK2Magazines = new string[] { "COMPONENT_ASSAULTRIFLE_MK2_CLIP_01", "COMPONENT_ASSAULTRIFLE_MK2_CLIP_02", "COMPONENT_ASSAULTRIFLE_MK2_CLIP_ARMORPIERCING", "COMPONENT_ASSAULTRIFLE_MK2_CLIP_FMJ", "COMPONENT_ASSAULTRIFLE_MK2_CLIP_INCENDIARY", "COMPONENT_ASSAULTRIFLE_MK2_CLIP_TRACER" };
        private static string[] AssaultRifleMK2Optics = new string[] { "COMPONENT_AT_SIGHTS", "COMPONENT_AT_SCOPE_MACRO_MK2", "COMPONENT_AT_SCOPE_MEDIUM_MK2" };
        private static string[] AssaultRifleMK2Muzzles = new string[] { "COMPONENT_AT_AR_SUPP_02", "COMPONENT_AT_MUZZLE_01", "COMPONENT_AT_MUZZLE_02", "COMPONENT_AT_MUZZLE_03", "COMPONENT_AT_MUZZLE_04", "COMPONENT_AT_MUZZLE_05", "COMPONENT_AT_MUZZLE_06", "COMPONENT_AT_MUZZLE_07" };
        private static string[] AssaultRifleMK2Barrels = new string[] { "COMPONENT_AT_AR_BARREL_01", "COMPONENT_AT_AR_BARREL_02" };

        private static string[] CarbineRifleMK2Magazines = new string[] { "COMPONENT_CARBINERIFLE_MK2_CLIP_01", "COMPONENT_CARBINERIFLE_MK2_CLIP_02", "COMPONENT_CARBINERIFLE_MK2_CLIP_ARMORPIERCING", "COMPONENT_CARBINERIFLE_MK2_CLIP_FMJ", "COMPONENT_CARBINERIFLE_MK2_CLIP_INCENDIARY", "COMPONENT_CARBINERIFLE_MK2_CLIP_TRACER" };
        private static string[] CarbineRifleMK2Optics = new string[] { "COMPONENT_AT_SIGHTS", "COMPONENT_AT_SCOPE_MACRO_MK2", "COMPONENT_AT_SCOPE_MEDIUM_MK2" };
        private static string[] CarbineRifleMK2Muzzles = new string[] { "COMPONENT_AT_AR_SUPP", "COMPONENT_AT_MUZZLE_01", "COMPONENT_AT_MUZZLE_02", "COMPONENT_AT_MUZZLE_03", "COMPONENT_AT_MUZZLE_04", "COMPONENT_AT_MUZZLE_05", "COMPONENT_AT_MUZZLE_06", "COMPONENT_AT_MUZZLE_07" };
        private static string[] CarbineRifleMK2Barrels = new string[] { "COMPONENT_AT_CR_BARREL_01", "COMPONENT_AT_CR_BARREL_02" };

        private static string[] SpecialCarbineMK2Magazines = new string[] { "COMPONENT_SPECIALCARBINE_MK2_CLIP_01", "COMPONENT_SPECIALCARBINE_MK2_CLIP_02", "COMPONENT_SPECIALCARBINE_MK2_CLIP_TRACER", "COMPONENT_SPECIALCARBINE_MK2_CLIP_INCENDIARY", "COMPONENT_SPECIALCARBINE_MK2_CLIP_ARMORPIERCING", "COMPONENT_SPECIALCARBINE_MK2_CLIP_FMJ" };
        private static string[] SpecialCarbineMK2Optics = new string[] { "COMPONENT_AT_SIGHTS", "COMPONENT_AT_SCOPE_MACRO_MK2", "COMPONENT_AT_SCOPE_MEDIUM_MK2" };
        private static string[] SpecialCarbineMK2Muzzles = new string[] { "COMPONENT_AT_AR_SUPP_02", "COMPONENT_AT_MUZZLE_01", "COMPONENT_AT_MUZZLE_02", "COMPONENT_AT_MUZZLE_03", "COMPONENT_AT_MUZZLE_04", "COMPONENT_AT_MUZZLE_05", "COMPONENT_AT_MUZZLE_06", "COMPONENT_AT_MUZZLE_07" };
        private static string[] SpecialCarbineMK2Barrels = new string[] { "COMPONENT_AT_SC_BARREL_01", "COMPONENT_AT_SC_BARREL_02" };

        private static string[] BullpupRifleMK2Magazines = new string[] { "COMPONENT_BULLPUPRIFLE_MK2_CLIP_01", "COMPONENT_BULLPUPRIFLE_MK2_CLIP_02", "COMPONENT_BULLPUPRIFLE_MK2_CLIP_TRACER", "COMPONENT_BULLPUPRIFLE_MK2_CLIP_INCENDIARY", "COMPONENT_BULLPUPRIFLE_MK2_CLIP_ARMORPIERCING", "COMPONENT_BULLPUPRIFLE_MK2_CLIP_FMJ" };
        private static string[] BullpupRifleMK2Optics = new string[] { "COMPONENT_AT_SIGHTS", "COMPONENT_AT_SCOPE_MACRO_02_MK2", "COMPONENT_AT_SCOPE_SMALL_MK2" };
        private static string[] BullpupRifleMK2Muzzles = new string[] { "COMPONENT_AT_AR_SUPP", "COMPONENT_AT_MUZZLE_01", "COMPONENT_AT_MUZZLE_02", "COMPONENT_AT_MUZZLE_03", "COMPONENT_AT_MUZZLE_04", "COMPONENT_AT_MUZZLE_05", "COMPONENT_AT_MUZZLE_06", "COMPONENT_AT_MUZZLE_07" };
        private static string[] BullpupRifleMK2Barrels = new string[] { "COMPONENT_AT_BP_BARREL_01", "COMPONENT_AT_BP_BARREL_02" };

        private static string[] PumpShotgunMK2Magazines = new string[] { "COMPONENT_PUMPSHOTGUN_MK2_CLIP_01", "COMPONENT_PUMPSHOTGUN_MK2_CLIP_TRACER", "COMPONENT_PUMPSHOTGUN_MK2_CLIP_INCENDIARY", "COMPONENT_PUMPSHOTGUN_MK2_CLIP_HOLLOWPOINT", "COMPONENT_PUMPSHOTGUN_MK2_CLIP_FMJ" };
        private static string[] PumpShotgunMK2Optics = new string[] { "COMPONENT_AT_SIGHTS", "COMPONENT_AT_SCOPE_MACRO_MK2", "COMPONENT_AT_SCOPE_SMALL_MK2" };
        private static string[] PumpShotgunMK2Muzzles = new string[] { "COMPONENT_AT_SR_SUPP_03", "COMPONENT_AT_MUZZLE_08" };

        /* COMMENTING THESE LINES OUT AS THESE WEAPONS ARE NOT SUPPORTED AT THIS TIME
		private static string[] HeavyRevolverMK2Magazines = new string[] { "COMPONENT_REVOLVER_MK2_CLIP_01", "COMPONENT_REVOLVER_MK2_CLIP_FMJ", "COMPONENT_REVOLVER_MK2_CLIP_HOLLOWPOINT", "COMPONENT_REVOLVER_MK2_CLIP_INCENDIARY", "COMPONENT_REVOLVER_MK2_CLIP_TRACER" };
		private static string[] HeavyRevolverMK2Optics = new string[] { "COMPONENT_AT_SIGHTS", "COMPONENT_AT_SCOPE_MACRO_MK2" };

		private static string[] SNSPistolMK2Magazines = new string[] { "COMPONENT_SNSPISTOL_MK2_CLIP_01", "COMPONENT_SNSPISTOL_MK2_CLIP_02", "COMPONENT_SNSPISTOL_MK2_CLIP_TRACER", "COMPONENT_SNSPISTOL_MK2_CLIP_INCENDIARY", "COMPONENT_SNSPISTOL_MK2_CLIP_HOLLOWPOINT", "COMPONENT_SNSPISTOL_MK2_CLIP_FMJ" };
		private static string[] SNSPistolMK2Muzzles = new string[] { "COMPONENT_AT_PI_SUPP_02", "COMPONENT_AT_PI_COMP_02" };

		private static string[] MarksmanRifleMK2Magazines = new string[] { "COMPONENT_MARKSMANRIFLE_MK2_CLIP_01", "COMPONENT_MARKSMANRIFLE_MK2_CLIP_02", "COMPONENT_MARKSMANRIFLE_MK2_CLIP_TRACER", "COMPONENT_MARKSMANRIFLE_MK2_CLIP_INCENDIARY", "COMPONENT_MARKSMANRIFLE_MK2_CLIP_ARMORPIERCING", "COMPONENT_MARKSMANRIFLE_MK2_CLIP_FMJ" };
		private static string[] MarksmanRifleMK2Optics = new string[] { "COMPONENT_AT_SIGHTS", "COMPONENT_AT_SCOPE_MEDIUM_MK2", "COMPONENT_AT_SCOPE_LARGE_FIXED_ZOOM_MK2" };
		private static string[] MarksmanRifleMK2Muzzles = new string[] { "COMPONENT_AT_AR_SUPP", "COMPONENT_AT_MUZZLE_01", "COMPONENT_AT_MUZZLE_02", "COMPONENT_AT_MUZZLE_03", "COMPONENT_AT_MUZZLE_04", "COMPONENT_AT_MUZZLE_05", "COMPONENT_AT_MUZZLE_06", "COMPONENT_AT_MUZZLE_07" };
		private static string[] MarksmanRifleMK2Barrels = new string[] { "COMPONENT_AT_MRFL_BARREL_01", "COMPONENT_AT_MRFL_BARREL_02" };
		*/


        private static InitializationFile initialiseFile(string filepath)
        {
            InitializationFile ini = new InitializationFile(filepath);
            ini.Create();
            return ini;
        }

        public static void SetConfigPath(string path)
        {
            filePath = path + ".ini";
        }

        public static string GetConfigPath()
        {
            return filePath;
        }

        //Pretty useless function that we're calling at initialization so we can load config titles for UI reasons
        public static void LoadConfigTitle()
        {
            Logger.DebugLog("Loading Config Title.");
            InitializationFile settings = initialiseFile(filePath);
            Global.Loadout.LoadoutTitle = settings.ReadString("Loadout", "LoadoutTitle", "ERROR IN CONFIG");
        }

        public static void LoadConfig()
        {
            Logger.DebugLog("Config Loading Started");

            InitializationFile settings = initialiseFile(filePath);

            //Title
            Global.Loadout.LoadoutTitle = settings.ReadString("Loadout", "LoadoutTitle", "ERROR IN CONFIG");

            //Pistols
            Global.Loadout.Pistol = ToBoolean(settings.ReadString("Loadout", "Pistol", "false"));
            Global.Loadout.CombatPistol = ToBoolean(settings.ReadString("Loadout", "CombatPistol", "true"));
            Global.Loadout.Pistol50 = ToBoolean(settings.ReadString("Loadout", "Pistol50", "false"));
            Global.Loadout.APPistol = ToBoolean(settings.ReadString("Loadout", "APPistol", "false"));
            Global.Loadout.HeavyPistol = ToBoolean(settings.ReadString("Loadout", "HeavyPistol", "false"));
            Global.Loadout.SNSPistol = ToBoolean(settings.ReadString("Loadout", "SNSPistol", "false"));
            Global.Loadout.VintagePistol = ToBoolean(settings.ReadString("Loadout", "VintagePistol", "false"));
            Global.Loadout.MarksmanPistol = ToBoolean(settings.ReadString("Loadout", "MarksmanPistol", "false"));
            Global.Loadout.HeavyRevolver = ToBoolean(settings.ReadString("Loadout", "HeavyRevolver", "false"));
            Global.Loadout.PistolMK2 = ToBoolean(settings.ReadString("Loadout", "PistolMK2", "false"));
            Global.Loadout.SNSPistolMK2 = ToBoolean(settings.ReadString("Loadout", "SNSPistolMK2", "false"));
            Global.Loadout.HeavyRevolverMK2 = ToBoolean(settings.ReadString("Loadout", "HeavyRevolverMK2", "false"));
            Global.Loadout.DoubleActionRevolver = ToBoolean(settings.ReadString("Loadout", "DoubleActionRevolver", "false"));

            //Machine Guns
            Global.Loadout.MicroSMG = ToBoolean(settings.ReadString("Loadout", "MicroSMG", "false"));
            Global.Loadout.SMG = ToBoolean(settings.ReadString("Loadout", "SMG", "false"));
            Global.Loadout.AssaultSMG = ToBoolean(settings.ReadString("Loadout", "AssaultSMG", "false"));
            Global.Loadout.TommyGun = ToBoolean(settings.ReadString("Loadout", "TommyGun", "false"));
            Global.Loadout.MG = ToBoolean(settings.ReadString("Loadout", "MG", "false"));
            Global.Loadout.CombatMG = ToBoolean(settings.ReadString("Loadout", "CombatMG", "false"));
            Global.Loadout.CombatPDW = ToBoolean(settings.ReadString("Loadout", "CombatPDW", "false"));
            Global.Loadout.MiniSMG = ToBoolean(settings.ReadString("Loadout", "MiniSMG", "false"));
            Global.Loadout.MachinePistol = ToBoolean(settings.ReadString("Loadout", "MachinePistol", "false"));
            Global.Loadout.SMGMK2 = ToBoolean(settings.ReadString("Loadout", "SMGMK2", "false"));
            Global.Loadout.CombatMGMK2 = ToBoolean(settings.ReadString("Loadout", "CombatMGMK2", "false"));

            //Shotguns
            Global.Loadout.PumpShotgun = ToBoolean(settings.ReadString("Loadout", "PumpShotgun", "true"));
            Global.Loadout.SawedOffShotgun = ToBoolean(settings.ReadString("Loadout", "SawedOffShotgun", "false"));
            Global.Loadout.AssaultShotgun = ToBoolean(settings.ReadString("Loadout", "AssaultShotgun", "false"));
            Global.Loadout.BullpupShotgun = ToBoolean(settings.ReadString("Loadout", "BullpupShotgun", "false"));
            Global.Loadout.HeavyShotgun = ToBoolean(settings.ReadString("Loadout", "HeavyShotgun", "false"));
            Global.Loadout.Musket = ToBoolean(settings.ReadString("Loadout", "Musket", "false"));
            Global.Loadout.DoubleBarrel = ToBoolean(settings.ReadString("Loadout", "DoubleBarrel", "false"));
            Global.Loadout.AutoShotgun = ToBoolean(settings.ReadString("Loadout", "AutoShotgun", "false"));
            Global.Loadout.PumpShotgunMK2 = ToBoolean(settings.ReadString("Loadout", "PumpShotgunMK2", "false"));

            //Rifles
            Global.Loadout.AssaultRifle = ToBoolean(settings.ReadString("Loadout", "AssaultRifle", "false"));
            Global.Loadout.CarbineRifle = ToBoolean(settings.ReadString("Loadout", "CarbineRifle", "false"));
            Global.Loadout.AdvancedRifle = ToBoolean(settings.ReadString("Loadout", "AdvancedRifle", "false"));
            Global.Loadout.SpecialCarbine = ToBoolean(settings.ReadString("Loadout", "SpecialCarbine"));
            Global.Loadout.BullpupRifle = ToBoolean(settings.ReadString("Loadout", "BullpupRifle", "false"));
            Global.Loadout.CompactRifle = ToBoolean(settings.ReadString("Loadout", "CompactRifle", "false"));
            Global.Loadout.AssaultRifleMK2 = ToBoolean(settings.ReadString("Loadout", "AssaultRifleMK2", "false"));
            Global.Loadout.CarbineRifleMK2 = ToBoolean(settings.ReadString("Loadout", "CarbineRifleMK2", "false"));
            Global.Loadout.SpecialCarbineMK2 = ToBoolean(settings.ReadString("Loadout", "SpecialCarbineMK2", "false"));
            Global.Loadout.CarbineRifleMK2 = ToBoolean(settings.ReadString("Loadout", "CarbineRifleMK2", "false"));

            //Snipers
            Global.Loadout.SniperRifle = ToBoolean(settings.ReadString("Loadout", "SniperRifle", "false"));
            Global.Loadout.HeavySniper = ToBoolean(settings.ReadString("Loadout", "HeavySniper", "false"));
            Global.Loadout.MarksmanRifle = ToBoolean(settings.ReadString("Loadout", "MarksmanRifle", "false"));
            Global.Loadout.HeavySniperMK2 = ToBoolean(settings.ReadString("Loadout", "HeavySniperMK2", "false"));

            //Heavy Weapons
            Global.Loadout.GrenadeLauncher = ToBoolean(settings.ReadString("Loadout", "GrenadeLauncher", "false"));
            Global.Loadout.RPG = ToBoolean(settings.ReadString("Loadout", "RPG", "false"));
            Global.Loadout.Minigun = ToBoolean(settings.ReadString("Loadout", "Minigun", "false"));
            Global.Loadout.FireworkLauncher = ToBoolean(settings.ReadString("Loadout", "FireworkLauncher", "false"));
            Global.Loadout.HomingLauncher = ToBoolean(settings.ReadString("Loadout", "HomingLauncher", "false"));
            Global.Loadout.RailGun = ToBoolean(settings.ReadString("Loadout", "RailGun", "false"));
            Global.Loadout.CompactLauncher = ToBoolean(settings.ReadString("Loadout", "CompactLauncher", "false"));

            //Throwables
            Global.Loadout.Flare = ToBoolean(settings.ReadString("Loadout", "Flare", "true"));
            Global.Loadout.BZGas = ToBoolean(settings.ReadString("Loadout", "BZGas", "false"));
            Global.Loadout.TearGas = ToBoolean(settings.ReadString("Loadout", "TearGas", "false"));
            Global.Loadout.Molotov = ToBoolean(settings.ReadString("Loadout", "Molotov", "false"));
            Global.Loadout.Grenade = ToBoolean(settings.ReadString("Loadout", "Grenade", "false"));
            Global.Loadout.StickyBomb = ToBoolean(settings.ReadString("Loadout", "StickyBomb", "false"));
            Global.Loadout.ProximityMine = ToBoolean(settings.ReadString("Loadout", "ProximityMine", "false"));
            Global.Loadout.PipeBomb = ToBoolean(settings.ReadString("Loadout", "PipeBomb", "false"));
            Global.Loadout.Snowball = ToBoolean(settings.ReadString("Loadout", "Snowball", "false"));
            Global.Loadout.Baseball = ToBoolean(settings.ReadString("Loadout", "Baseball", "false"));

            //Melee Weapons
            Global.Loadout.Nightstick = ToBoolean(settings.ReadString("Loadout", "Nightstick", "true"));
            Global.Loadout.Knife = ToBoolean(settings.ReadString("Loadout", "Knife", "false"));
            Global.Loadout.Hammer = ToBoolean(settings.ReadString("Loadout", "Hammer", "false"));
            Global.Loadout.BaseballBat = ToBoolean(settings.ReadString("Loadout", "BaseballBat", "false"));
            Global.Loadout.Crowbar = ToBoolean(settings.ReadString("Loadout", "Crowbar", "false"));
            Global.Loadout.GolfClub = ToBoolean(settings.ReadString("Loadout", "GolfClub", "false"));
            Global.Loadout.BrokenBottle = ToBoolean(settings.ReadString("Loadout", "BrokenBottle", "false"));
            Global.Loadout.AntiqueDagger = ToBoolean(settings.ReadString("Loadout", "AntiqueDagger", "false"));
            Global.Loadout.Hatchet = ToBoolean(settings.ReadString("Loadout", "Hatchet", "false"));
            Global.Loadout.BrassKnuckles = ToBoolean(settings.ReadString("Loadout", "BrassKnuckles", "false"));
            Global.Loadout.Machete = ToBoolean(settings.ReadString("Loadout", "Machete", "false"));
            Global.Loadout.Switchblade = ToBoolean(settings.ReadString("Loadout", "Switchblade", "false"));
            Global.Loadout.BattleAxe = ToBoolean(settings.ReadString("Loadout", "BattleAxe", "false"));
            Global.Loadout.Wrench = ToBoolean(settings.ReadString("Loadout", "Wrench", "false"));
            Global.Loadout.PoolCue = ToBoolean(settings.ReadString("Loadout", "PoolCue", "false"));

            //Other
            Global.Loadout.Taser = ToBoolean(settings.ReadString("Loadout", "Taser", "true"));
            Global.Loadout.FlareGun = ToBoolean(settings.ReadString("Loadout", "FlareGun", "false"));
            Global.Loadout.Flashlight = ToBoolean(settings.ReadString("Loadout", "Flashlight", "true"));
            Global.Loadout.FireExtinguisher = ToBoolean(settings.ReadString("Loadout", "FireExtinguisher", "true"));
            Global.Loadout.GasCan = ToBoolean(settings.ReadString("Loadout", "GasCan", "false"));

            //Misc
            Global.Loadout.BodyArmor = ToBoolean(settings.ReadString("Loadout", "BodyArmor", "true"));
            Global.Loadout.AttachFlashlightToAll = ToBoolean(settings.ReadString("Loadout", "AttachFlashlightToAll", "false"));

            //Attachments
            Global.Loadout.AssaultRifleAttachments = ToBoolean(settings.ReadString("WeaponAttachments", "AssaultRifleAttachments", "false"));
            Global.Loadout.CarbineRifleAttachments = ToBoolean(settings.ReadString("WeaponAttachments", "CarbineRifleAttachments", "false"));
            Global.Loadout.AdvancedRifleAttachments = ToBoolean(settings.ReadString("WeaponAttachments", "AdvancedRifleAttachments", "false"));
            Global.Loadout.SpecialCarbineAttachments = ToBoolean(settings.ReadString("WeaponAttachments", "SpecialCarbineAttachements", "false"));
            Global.Loadout.BullpupRifleAttachments = ToBoolean(settings.ReadString("WeaponAttachments", "BullpupRifleAttachments", "false"));
            Global.Loadout.CompactRifleAttachments = ToBoolean(settings.ReadString("WeaponAttachments", "CompactRifleAttachments", "false"));
            Global.Loadout.AssaultRifleMK2Attachments = ToBoolean(settings.ReadString("WeaponAttachments", "AssaultRifleMK2Attachments", "false"));
            Global.Loadout.CarbineRifleMK2Attachments = ToBoolean(settings.ReadString("WeaponAttachments", "CarbineRifleMK2Attachments", "false"));
            Global.Loadout.SpecialCarbineMK2Attachments = ToBoolean(settings.ReadString("WeaponAttachments", "SpecialCarbineMK2Attachments", "false"));
            Global.Loadout.BullpupRifleMK2Attachments = ToBoolean(settings.ReadString("WeaponAttachments", "BullpupRifleMK2Attachments", "false"));
            Global.Loadout.PumpShotgunMK2Attachments = ToBoolean(settings.ReadString("WeaponAttachments", "PumpShotgunMK2Attachments", "false"));

            ///All config stuff for attachments and component strings are loaded here aswell as sent to be validated as to if they're valid or not
            if (Global.Loadout.AssaultRifle)
            {
                if (Global.Loadout.AssaultRifleAttachments)
                {
                    Global.Loadout.AssaultRifleMagazine = ToBoolean(settings.ReadString("AssaultRifleAttachments", "Magazine", "false"));
                    Global.Loadout.AssaultRifleGrip = ToBoolean(settings.ReadString("AssaultRifleAttachments", "Grip", "true"));
                    Global.Loadout.AssaultRifleOptic = ToBoolean(settings.ReadString("AssaultRifleAttachments", "Optic", "true"));
                    Global.Loadout.AssaultRifleFlashlight = ToBoolean(settings.ReadString("AssaultRifleAttachments", "Flashlight", "true"));
                    Global.Loadout.AssaultRifleMuzzle = ToBoolean(settings.ReadString("AssaultRifleAttachments", "Suppressor", "false"));
                    Global.Loadout.AssaultRifleMagazineString = settings.ReadString("AssaultRifleComponentStrings", "Magazine", "COMPONENT_ASSAULTRIFLE_CLIP_02");
                    ValidateComponentStrings(1);
                }
            }

            if (Global.Loadout.CarbineRifle)
            {
                if (Global.Loadout.CarbineRifleAttachments)
                {
                    Global.Loadout.CarbineRifleMagazine = ToBoolean(settings.ReadString("CarbineRifleAttachments", "Magazine", "false"));
                    Global.Loadout.CarbineRifleGrip = ToBoolean(settings.ReadString("CarbineRifleAttachments", "Grip", "true"));
                    Global.Loadout.CarbineRifleOptic = ToBoolean(settings.ReadString("CarbineRifleAttachments", "Optic", "true"));
                    Global.Loadout.CarbineRifleFlashlight = ToBoolean(settings.ReadString("CarbineRifleAttachments", "Flashlight", "true"));
                    Global.Loadout.CarbineRifleMuzzle = ToBoolean(settings.ReadString("CarbineRifleAttachments", "Suppressor", "false"));
                    Global.Loadout.CarbineRifleMagazineString = settings.ReadString("CarbineRifleComponentStrings", "Magazine", "COMPONENT_CARBINERIFLE_CLIP_02");
                    ValidateComponentStrings(2);
                }
            }

            if (Global.Loadout.AdvancedRifle)
            {
                if (Global.Loadout.AdvancedRifleAttachments)
                {
                    Global.Loadout.AdvancedRifleMagazine = ToBoolean(settings.ReadString("AdvancedRifleAttachments", "Magazine", "false"));
                    Global.Loadout.AdvancedRifleOptic = ToBoolean(settings.ReadString("AdvancedRifleAttachments", "Optic", "true"));
                    Global.Loadout.AdvancedRifleFlashlight = ToBoolean(settings.ReadString("AdvancedRifleAttachments", "Flashlight", "true"));
                    Global.Loadout.AdvancedRifleMuzzle = ToBoolean(settings.ReadString("AdvancedRifleAttachments", "Suppressor", "false"));
                    Global.Loadout.AdvancedRifleMagazineString = settings.ReadString("AdvancedRifleComponentStrings", "Magazine", "COMPONENT_ADVANCEDRIFLE_CLIP_02");
                    ValidateComponentStrings(3);
                }
            }

            if (Global.Loadout.SpecialCarbine)
            {
                if (Global.Loadout.SpecialCarbineAttachments)
                {
                    Global.Loadout.SpecialCarbineMagazine = ToBoolean(settings.ReadString("SpecialCarbineAttachments", "Magazine", "false"));
                    Global.Loadout.SpecialCarbineGrip = ToBoolean(settings.ReadString("SpecialCarbineAttachments", "Grip", "true"));
                    Global.Loadout.SpecialCarbineOptic = ToBoolean(settings.ReadString("SpecialCarbineAttachments", "Optic", "true"));
                    Global.Loadout.SpecialCarbineFlashlight = ToBoolean(settings.ReadString("SpecialCarbineAttachments", "Flashlight", "true"));
                    Global.Loadout.SpecialCarbineMuzzle = ToBoolean(settings.ReadString("SpecialCarbineAttachments", "Suppressor", "false"));
                    Global.Loadout.SpecialCarbineMagazineString = settings.ReadString("SpecialCarbineComponentStrings", "Magazine", "COMPONENT_SPECIALCARBINE_CLIP_02");
                    ValidateComponentStrings(4);
                }
            }

            if (Global.Loadout.BullpupRifle)
            {
                if (Global.Loadout.BullpupRifleAttachments)
                {
                    Global.Loadout.BullpupRifleMagazine = ToBoolean(settings.ReadString("BullpupRifleAttachments", "Magazine", "false"));
                    Global.Loadout.BullpupRifleGrip = ToBoolean(settings.ReadString("BullpupRifleAttachments", "Grip", "true"));
                    Global.Loadout.BullpupRifleOptic = ToBoolean(settings.ReadString("BullpupRifleAttachments", "Optic", "true"));
                    Global.Loadout.BullpupRifleFlashlight = ToBoolean(settings.ReadString("BullpupRifleAttachments", "Flashlight", "true"));
                    Global.Loadout.BullpupRifleMuzzle = ToBoolean(settings.ReadString("BullpupRifleAttachments", "Suppressor", "false"));
                    Global.Loadout.BullpupRifleMagazineString = settings.ReadString("BullpupRifleComponentStrings", "Magazine", "COMPONENT_BULLPUPRIFLE_CLIP_02");
                    ValidateComponentStrings(5);
                }
            }

            if (Global.Loadout.CompactRifle)
            {
                if (Global.Loadout.CompactRifleAttachments)
                {
                    Global.Loadout.CompactRifleMagazine = ToBoolean(settings.ReadString("CompactRifleAttachments", "Magazine", "false"));
                    Global.Loadout.CompactRifleMagazineString = settings.ReadString("CompactRifleComponentStrings", "Magazine", "COMPONENT_COMPACTRIFLE_CLIP_02");
                    ValidateComponentStrings(6);
                }
            }

            if (Global.Loadout.AssaultRifleMK2)
            {
                if (Global.Loadout.AssaultRifleMK2Attachments)
                {
                    Global.Loadout.AssaultRifleMK2Magazine = ToBoolean(settings.ReadString("AssaultRifleMK2Attachments", "Magazine", "false"));
                    Global.Loadout.AssaultRifleMK2Grip = ToBoolean(settings.ReadString("AssaultRifleMK2Attachments", "Grip", "true"));
                    Global.Loadout.AssaultRifleMK2Optic = ToBoolean(settings.ReadString("AssaultRifleMK2Attachments", "Optic", "true"));
                    Global.Loadout.AssaultRifleMK2Flashlight = ToBoolean(settings.ReadString("AssaultRifleMK2Attachments", "Flashlight", "true"));
                    Global.Loadout.AssaultRifleMK2Muzzle = ToBoolean(settings.ReadString("AssaultRifleMK2Attachments", "Muzzle", "false"));
                    Global.Loadout.AssaultRifleMK2Barrel = ToBoolean(settings.ReadString("AssaultRifleMK2Attachments", "Barrel", "false"));
                    Global.Loadout.AssaultRifleMK2MagazineString = settings.ReadString("AssaultRifleMK2ComponentStrings", "Magazine", "COMPONENT_ASSAULTRIFLE_MK2_CLIP_02");
                    Global.Loadout.AssaultRifleMK2OpticString = settings.ReadString("AssaultRifleMK2ComponentStrings", "Optic", "COMPONENT_AT_SCOPE_MACRO_MK2");
                    Global.Loadout.AssaultRifleMK2MuzzleString = settings.ReadString("AssaultRifleMK2ComponentStrings", "Muzzle", "COMPONENT_AT_MUZZLE_01");
                    Global.Loadout.AssaultRifleMK2BarrelString = settings.ReadString("AssaultRifleMK2ComponentStrings", "Barrel", "COMPONENT_AT_AR_BARREL_01");
                    ValidateComponentStrings(7);
                }
            }

            if (Global.Loadout.CarbineRifleMK2)
            {
                if (Global.Loadout.CarbineRifleMK2Attachments)
                {
                    Global.Loadout.CarbineRifleMK2Magazine = ToBoolean(settings.ReadString("CarbineRifleMK2Attachments", "Magazine", "false"));
                    Global.Loadout.CarbineRifleMK2Grip = ToBoolean(settings.ReadString("CarbineRifleMK2Attachments", "Grip", "true"));
                    Global.Loadout.CarbineRifleMK2Optic = ToBoolean(settings.ReadString("CarbineRifleMK2Attachments", "Optic", "true"));
                    Global.Loadout.CarbineRifleMK2Flashlight = ToBoolean(settings.ReadString("CarbineRifleMK2Attachments", "Flashlight", "true"));
                    Global.Loadout.CarbineRifleMK2Muzzle = ToBoolean(settings.ReadString("CarbineRifleMK2Attachments", "Muzzle", "false"));
                    Global.Loadout.CarbineRifleMK2Barrel = ToBoolean(settings.ReadString("CarbineRifleMK2Attachments", "Barrel", "false"));
                    Global.Loadout.CarbineRifleMK2MagazineString = settings.ReadString("CarbineRifleMK2ComponentStrings", "Magazine", "COMPONENT_CARBINERIFLE_MK2_CLIP_02");
                    Global.Loadout.CarbineRifleMK2OpticString = settings.ReadString("CarbineRifleMK2ComponentStrings", "Optic", "COMPONENT_AT_SCOPE_MACRO_MK2");
                    Global.Loadout.CarbineRifleMK2MuzzleString = settings.ReadString("CarbineRifleMK2ComponentStrings", "Muzzle", "COMPONENT_AT_MUZZLE_01");
                    Global.Loadout.CarbineRifleMK2BarrelString = settings.ReadString("CarbineRifleMK2ComponentStrings", "Barrel", "COMPONENT_AT_CR_BARREL_01");
                    ValidateComponentStrings(8);
                }
            }

            if (Global.Loadout.SpecialCarbineMK2)
            {
                if (Global.Loadout.SpecialCarbineMK2Attachments)
                {
                    Global.Loadout.SpecialCarbineMK2Magazine = ToBoolean(settings.ReadString("SpecialCarbineMK2Attachments", "Magazine", "false"));
                    Global.Loadout.SpecialCarbineMK2Grip = ToBoolean(settings.ReadString("SpecialCarbineMK2Attachments", "Grip", "true"));
                    Global.Loadout.SpecialCarbineMK2Optic = ToBoolean(settings.ReadString("SpecialCarbineMK2Attachments", "Optic", "true"));
                    Global.Loadout.SpecialCarbineMK2Flashlight = ToBoolean(settings.ReadString("SpecialCarbineMK2Attachments", "Flashlight", "true"));
                    Global.Loadout.SpecialCarbineMK2Muzzle = ToBoolean(settings.ReadString("SpecialCarbineMK2Attachments", "Muzzle", "false"));
                    Global.Loadout.SpecialCarbineMK2Barrel = ToBoolean(settings.ReadString("SpecialCarbineMK2Attachments", "Barrel", "false"));
                    Global.Loadout.SpecialCarbineMK2MagazineString = settings.ReadString("SpecialCarbineMK2ComponentStrings", "Magazine", "COMPONENT_SPECIALCARBINE_MK2_CLIP_02");
                    Global.Loadout.SpecialCarbineMK2OpticString = settings.ReadString("SpecialCarbineMK2ComponentStrings", "Optic", "COMPONENT_AT_SCOPE_MACRO_MK2");
                    Global.Loadout.SpecialCarbineMK2MuzzleString = settings.ReadString("SpecialCarbineMK2ComponentStrings", "Muzzle", "COMPONENT_AT_MUZZLE_01");
                    Global.Loadout.SpecialCarbineMK2BarrelString = settings.ReadString("SpecialCarbineMK2ComponentStrings", "Barrel", "COMPONENT_AT_SC_BARREL_01");
                    ValidateComponentStrings(9);
                }
            }

            if (Global.Loadout.BullpupRifleMK2)
            {
                if (Global.Loadout.BullpupRifleMK2Attachments)
                {
                    Global.Loadout.BullpupRifleMK2Magazine = ToBoolean(settings.ReadString("BullpupRifleMK2Attachments", "Magazine", "false"));
                    Global.Loadout.BullpupRifleMK2Grip = ToBoolean(settings.ReadString("BullpupRifleMK2Attachments", "Grip", "true"));
                    Global.Loadout.BullpupRifleMK2Optic = ToBoolean(settings.ReadString("BullpupRifleMK2Attachments", "Optic", "true"));
                    Global.Loadout.BullpupRifleMK2Flashlight = ToBoolean(settings.ReadString("BullpupRifleMK2Attachments", "Flashlight", "true"));
                    Global.Loadout.BullpupRifleMK2Muzzle = ToBoolean(settings.ReadString("BullpupRifleMK2Attachments", "Muzzle", "false"));
                    Global.Loadout.BullpupRifleMK2Barrel = ToBoolean(settings.ReadString("BullpupRifleMK2Attachments", "Barrel", "false"));
                    Global.Loadout.BullpupRifleMK2MagazineString = settings.ReadString("BullpupRifleMK2ComponentStrings", "Magazine", "COMPONENT_BULLPUPRIFLE_MK2_CLIP_02");
                    Global.Loadout.BullpupRifleMK2OpticString = settings.ReadString("BullpupRifleMK2ComponentStrings", "Optic", "COMPONENT_AT_SCOPE_MACRO_02_MK2");
                    Global.Loadout.BullpupRifleMK2MuzzleString = settings.ReadString("BullpupRifleMK2ComponentStrings", "Muzzle", "COMPONENT_AT_MUZZLE_01");
                    Global.Loadout.BullpupRifleMK2BarrelString = settings.ReadString("BullpupRifleMK2ComponentStrings", "Barrel", "COMPONENT_AT_BP_BARREL_01");
                    ValidateComponentStrings(10);
                }
            }

            if (Global.Loadout.PumpShotgunMK2)
            {
                if (Global.Loadout.PumpShotgunMK2Attachments)
                {
                    Global.Loadout.PumpShotgunMK2Magazine = ToBoolean(settings.ReadString("PumpShotgunMK2Attachments", "Magazine", "false"));
                    Global.Loadout.PumpShotgunMK2Optic = ToBoolean(settings.ReadString("PumpShotgunMK2Attachments", "Optic", "true"));
                    Global.Loadout.PumpShotgunMK2Flashlight = ToBoolean(settings.ReadString("PumpShotgunMK2Attachments", "Flashlight", "true"));
                    Global.Loadout.PumpShotgunMK2Muzzle = ToBoolean(settings.ReadString("PumpShotgunMK2Attachments", "Muzzle", "false"));
                    Global.Loadout.PumpShotgunMK2MagazineString = settings.ReadString("PumpShotgunMK2ComponentStrings", "Magazine", "COMPONENT_PUMPSHOTGUN_MK2_CLIP_01");
                    Global.Loadout.PumpShotgunMK2OpticString = settings.ReadString("PumpShotgunMK2ComponentStrings", "Optic", "COMPONENT_AT_SIGHTS");
                    Global.Loadout.PumpShotgunMK2MuzzleString = settings.ReadString("PumpShotgunMK2ComponentStrings", "Barrel", "COMPONENT_AT_MUZZLE_08");
                    ValidateComponentStrings(11);
                }
            }

            Logger.DebugLog("Loadout Config Finished.");
        }

        //Simple function that we're using to convert a string to a bool (Thanks RPH for not error checking in ReadBoolean!)
        public static bool ToBoolean(string convert)
        {
            switch (convert.ToLower())
            {
                //Putting as many different possible variants of true/false that could be made so we don't have errors.
                //If none match, well than we just simply default to returning false
                case "true":
                    return true;
                case "ture":
                case "teur":
                case "treu":
                case "rtue":
                case "rteu":
                    Notifier.Notify("There was an error in your config file, check logs for more info.");
                    Logger.Log("You have a typo in your config file. True was spelt: " + convert + " in " + filePath + ".");
                    return true;
                case "false":
                    return false;
                case "flase":
                case "fasle":
                case "fales":
                    Notifier.Notify("There was an error in your config file, check logs for more info.");
                    Logger.Log("You have a typo in your config file. False was spelt: " + convert + " in " + filePath + ".");
                    return false;
                default:
                    Notifier.Notify("There was an error in your config file, check logs for more info.");
                    Logger.Log("There was an error parsing " + filePath + ", please check for a misspelt true/false definition. (Culprit: " + convert + ")");
                    return false;
            }
        }

        //Function that validates component strings. If the strings aren't in the arrays defined at the top of this file than we log an error and revert to a hard-coded value to prevent crashing
        private static void ValidateComponentStrings(int index)
        {
            Logger.DebugLog("Component String Validation Started.");

            bool IsError = false;
            switch (index)
            {
                case 1:
                    if (Global.Loadout.AssaultRifleMagazine)
                    {
                        for (int i = 0; i < AssaultRifleMagazines.Length; i++)
                        {
                            if (Global.Loadout.AssaultRifleMagazineString.Equals(AssaultRifleMagazines[i]))
                                i = AssaultRifleMagazines.Length + 1;
                            else if (!Global.Loadout.AssaultRifleMagazineString.Equals(AssaultRifleMagazines[i]) && i == AssaultRifleMagazines.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.AssaultRifleMagazineString + " for Assault Rifle Extended Mag is invalid, defaulting to COMPONENT_ASSAULTRIFLE_CLIP_02!");
                                Global.Loadout.AssaultRifleMagazineString = "COMPONENT_ASSAULTRIFLE_CLIP_02";
                                IsError = true;
                            }
                        }
                    }
                    break;
                case 2:
                    if (Global.Loadout.CarbineRifleMagazine)
                    {
                        for (int i = 0; i < CarbineRifleMagazines.Length; i++)
                        {
                            if (Global.Loadout.CarbineRifleMagazineString.Equals(CarbineRifleMagazines[i]))
                                i = CarbineRifleMagazines.Length + 1;
                            else if (!Global.Loadout.CarbineRifleMagazineString.Equals(CarbineRifleMagazines[i]) && i == CarbineRifleMagazines.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.CarbineRifleMagazineString + " for Carbine Rifle Extended Mag is invalid, defaulting to COMPONENT_CARBINERIFLE_CLIP_02!");
                                Global.Loadout.CarbineRifleMagazineString = "COMPONENT_CARBINERIFLE_CLIP_02";
                                IsError = true;
                            }
                        }
                    }
                    break;
                case 3:
                    if (Global.Loadout.AdvancedRifleMagazine)
                    {
                        for (int i = 0; i < AdvancedRifleMagazines.Length; i++)
                        {
                            if (Global.Loadout.AdvancedRifleMagazineString.Equals(AdvancedRifleMagazines[i]))
                                i = AdvancedRifleMagazines.Length + 1;
                            else if (!Global.Loadout.AdvancedRifleMagazineString.Equals(AdvancedRifleMagazines[i]) && i == AdvancedRifleMagazines.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.AdvancedRifleMagazineString + " for Advanced Rifle Extended Mag is invalid, defaulting to COMPONENT_ADVANCEDRIFLE_CLIP_02!");
                                Global.Loadout.AdvancedRifleMagazineString = "COMPONENT_ADVANCEDRIFLE_CLIP_02";
                                IsError = true;
                            }
                        }
                    }
                    break;
                case 4:
                    if (Global.Loadout.SpecialCarbineMagazine)
                    {
                        for (int i = 0; i < SpecialCarbineMagazines.Length; i++)
                        {
                            if (Global.Loadout.SpecialCarbineMagazineString.Equals(SpecialCarbineMagazines[i]))
                                i = SpecialCarbineMagazines.Length + 1;
                            else if (!Global.Loadout.SpecialCarbineMagazineString.Equals(SpecialCarbineMagazines[i]) && i == SpecialCarbineMagazines.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.SpecialCarbineMagazineString + " for Special Carbine Extended Mag is invalid, defaulting to COMPONENT_SPECIALCARBINE_CLIP_02!");
                                Global.Loadout.SpecialCarbineMagazineString = "COMPONENT_SPECIALCARBINE_CLIP_02";
                                IsError = true;
                            }
                        }
                    }
                    break;
                case 5:
                    if (Global.Loadout.BullpupRifleMagazine)
                    {
                        for (int i = 0; i < BullpupRifleMagazines.Length; i++)
                        {
                            if (Global.Loadout.BullpupRifleMagazineString.Equals(BullpupRifleMagazines[i]))
                                i = BullpupRifleMagazines.Length + 1;
                            else if (!Global.Loadout.BullpupRifleMagazineString.Equals(BullpupRifleMagazines[i]) && i == BullpupRifleMagazines.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.BullpupRifleMagazineString + " for Bullpup Rifle Extended Mag is invalid, defaulting to COMPONENT_BULLPUPRIFLE_CLIP_02!");
                                Global.Loadout.BullpupRifleMagazineString = "COMPONENT_BULLPUPRIFLE_CLIP_02";
                                IsError = true;
                            }
                        }
                    }
                    break;
                case 6:
                    if (Global.Loadout.CompactRifleMagazine)
                    {
                        for (int i = 0; i < CompactRifleMagazines.Length; i++)
                        {
                            if (Global.Loadout.CompactRifleMagazineString.Equals(CompactRifleMagazines[i]))
                                i = CompactRifleMagazines.Length + 1;
                            else if (!Global.Loadout.CompactRifleMagazineString.Equals(CompactRifleMagazines[i]) && i == CompactRifleMagazines.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.CompactRifleMagazineString + " for Compact Rifle Extended Mag is invalid, defaulting to COMPONENT_COMPACTRIFLE_CLIP_02!");
                                Global.Loadout.CompactRifleMagazineString = "COMPONENT_COMPACTRIFLE_CLIP_02";
                                IsError = true;
                            }
                        }
                    }
                    break;
                case 7:
                    if (Global.Loadout.AssaultRifleMK2Magazine)
                    {
                        for (int i = 0; i < AssaultRifleMK2Magazines.Length; i++)
                        {
                            if (Global.Loadout.AssaultRifleMK2MagazineString.Equals(AssaultRifleMK2Magazines[i]))
                                i = AssaultRifleMK2Magazines.Length + 1;
                            else if (!Global.Loadout.AssaultRifleMK2MagazineString.Equals(AssaultRifleMK2Magazines[i]) && i == AssaultRifleMK2Magazines.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.AssaultRifleMK2MagazineString + " for Assault Rifle MK2 Extended Mag is invalid, defaulting to COMPONENT_ASSAULTRIFLE_MK2_CLIP_02!");
                                Global.Loadout.AssaultRifleMK2MagazineString = "COMPONENT_ASSAULTRIFLE_MK2_CLIP_02";
                                IsError = true;
                            }
                        }
                    }

                    if (Global.Loadout.AssaultRifleMK2Optic)
                    {
                        for (int i = 0; i < AssaultRifleMK2Optics.Length; i++)
                        {
                            if (Global.Loadout.AssaultRifleMK2OpticString.Equals(AssaultRifleMK2Optics[i]))
                                i = AssaultRifleMK2Optics.Length + 1;
                            else if (!Global.Loadout.AssaultRifleMK2OpticString.Equals(AssaultRifleMK2Optics[i]) && i == AssaultRifleMK2Optics.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.AssaultRifleMK2OpticString + " for Assault Rifle MK2 Optic is invalid, defaulting to COMPONENT_AT_SCOPE_MACRO_MK2!");
                                Global.Loadout.AssaultRifleMK2OpticString = "COMPONENT_AT_SCOPE_MACRO_MK2";
                                IsError = true;
                            }
                        }
                    }

                    if (Global.Loadout.AssaultRifleMK2Muzzle)
                    {
                        for (int i = 0; i < AssaultRifleMK2Muzzles.Length; i++)
                        {
                            if (Global.Loadout.AssaultRifleMK2MuzzleString.Equals(AssaultRifleMK2Muzzles[i]))
                                i = AssaultRifleMK2Muzzles.Length + 1;
                            else if (!Global.Loadout.AssaultRifleMK2MuzzleString.Equals(AssaultRifleMK2Muzzles[i]) && i == AssaultRifleMK2Muzzles.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.AssaultRifleMK2MuzzleString + " for Assault Rifle MK2 Muzzle is invalid, defaulting to COMPONENT_AT_MUZZLE_01!");
                                Global.Loadout.AssaultRifleMK2MuzzleString = "COMPONENT_AT_MUZZLE_01";
                                IsError = true;
                            }
                        }
                    }

                    if (Global.Loadout.AssaultRifleMK2Barrel)
                    {
                        for (int i = 0; i < AssaultRifleMK2Barrels.Length; i++)
                        {
                            if (Global.Loadout.AssaultRifleMK2BarrelString.Equals(AssaultRifleMK2Barrels[i]))
                                i = AssaultRifleMK2Barrels.Length + 1;
                            else if (!Global.Loadout.AssaultRifleMK2BarrelString.Equals(AssaultRifleMK2Barrels[i]) && i == AssaultRifleMK2Barrels.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.AssaultRifleMK2BarrelString + " for Assault Rifle MK2 Barrel is invalid, defaulting to COMPONENT_AT_AR_BARREL_01!");
                                Global.Loadout.AssaultRifleMK2BarrelString = "COMPONENT_AT_AR_BARREL_01";
                                IsError = true;
                            }
                        }
                    }
                    break;
                case 8:
                    if (Global.Loadout.CarbineRifleMK2Magazine)
                    {
                        for (int i = 0; i < CarbineRifleMK2Magazines.Length; i++)
                        {
                            if (Global.Loadout.CarbineRifleMK2MagazineString.Equals(CarbineRifleMK2Magazines[i]))
                                i = CarbineRifleMK2Magazines.Length + 1;
                            else if (!Global.Loadout.CarbineRifleMK2MagazineString.Equals(CarbineRifleMK2Magazines[i]) && i == CarbineRifleMK2Magazines.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.CarbineRifleMK2MagazineString + " for Carbine Rifle MK2 Extended Mag is invalid, defaulting to COMPONENT_CARBINERIFLE_MK2_CLIP_02!");
                                Global.Loadout.CarbineRifleMK2MagazineString = "COMPONENT_CARBINERIFLE_MK2_CLIP_02";
                                IsError = true;
                            }
                        }
                    }

                    if (Global.Loadout.CarbineRifleMK2Optic)
                    {
                        for (int i = 0; i < CarbineRifleMK2Optics.Length; i++)
                        {
                            if (Global.Loadout.CarbineRifleMK2OpticString.Equals(CarbineRifleMK2Optics[i]))
                                i = CarbineRifleMK2Optics.Length + 1;
                            else if (!Global.Loadout.CarbineRifleMK2OpticString.Equals(CarbineRifleMK2Optics[i]) && i == CarbineRifleMK2Optics.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.CarbineRifleMK2OpticString + " for Carbine Rifle MK2 Optic is invalid, defaulting to COMPONENT_AT_SCOPE_MACRO_MK2!");
                                Global.Loadout.CarbineRifleMK2OpticString = "COMPONENT_AT_SCOPE_MACRO_MK2";
                                IsError = true;
                            }
                        }
                    }

                    if (Global.Loadout.CarbineRifleMK2Muzzle)
                    {
                        for (int i = 0; i < CarbineRifleMK2Muzzles.Length; i++)
                        {
                            if (Global.Loadout.CarbineRifleMK2MuzzleString.Equals(CarbineRifleMK2Muzzles[i]))
                                i = CarbineRifleMK2Muzzles.Length + 1;
                            else if (!Global.Loadout.CarbineRifleMK2MuzzleString.Equals(CarbineRifleMK2Muzzles[i]) && i == CarbineRifleMK2Muzzles.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.CarbineRifleMK2MuzzleString + " for Carbine Rifle MK2 Muzzle is invalid, defaulting to COMPONENT_AT_MUZZLE_01!");
                                Global.Loadout.CarbineRifleMK2MuzzleString = "COMPONENT_AT_MUZZLE_01";
                                IsError = true;
                            }
                        }
                    }

                    if (Global.Loadout.CarbineRifleMK2Barrel)
                    {
                        for (int i = 0; i < CarbineRifleMK2Barrels.Length; i++)
                        {
                            if (Global.Loadout.CarbineRifleMK2BarrelString.Equals(CarbineRifleMK2Barrels[i]))
                                i = CarbineRifleMK2Barrels.Length + 1;
                            else if (!Global.Loadout.CarbineRifleMK2BarrelString.Equals(CarbineRifleMK2Barrels[i]) && i == CarbineRifleMK2Barrels.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.CarbineRifleMK2BarrelString + " for Carbine Rifle MK2 Barrel is invalid, defaulting to COMPONENT_AT_CR_BARREL_01!");
                                Global.Loadout.CarbineRifleMK2BarrelString = "COMPONENT_AT_CR_BARREL_01";
                                IsError = true;
                            }
                        }
                    }
                    break;
                case 9:
                    if (Global.Loadout.SpecialCarbineMK2Magazine)
                    {
                        for (int i = 0; i < SpecialCarbineMK2Magazines.Length; i++)
                        {
                            if (Global.Loadout.SpecialCarbineMK2MagazineString.Equals(SpecialCarbineMK2Magazines[i]))
                                i = SpecialCarbineMK2Magazines.Length + 1;
                            else if (!Global.Loadout.SpecialCarbineMK2MagazineString.Equals(SpecialCarbineMK2Magazines[i]) && i == SpecialCarbineMK2Magazines.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.SpecialCarbineMK2MagazineString + " for Special Carbine MK2 Extended Mag is invalid, defaulting to COMPONENT_SPECIALCARBINE_MK2_CLIP_02!");
                                Global.Loadout.SpecialCarbineMK2MagazineString = "COMPONENT_SPECIALCARBINE_MK2_CLIP_02";
                                IsError = true;
                            }
                        }
                    }

                    if (Global.Loadout.SpecialCarbineMK2Optic)
                    {
                        for (int i = 0; i < SpecialCarbineMK2Optics.Length; i++)
                        {
                            if (Global.Loadout.SpecialCarbineMK2OpticString.Equals(SpecialCarbineMK2Optics[i]))
                                i = SpecialCarbineMK2Optics.Length + 1;
                            else if (!Global.Loadout.SpecialCarbineMK2OpticString.Equals(SpecialCarbineMK2Optics[i]) && i == SpecialCarbineMK2Optics.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.SpecialCarbineMK2OpticString + " for Special Carbine MK2 Optic is invalid, defaulting to COMPONENT_AT_SCOPE_MACRO_MK2!");
                                Global.Loadout.SpecialCarbineMK2OpticString = "COMPONENT_AT_SCOPE_MACRO_MK2";
                                IsError = true;
                            }
                        }
                    }

                    if (Global.Loadout.SpecialCarbineMK2Muzzle)
                    {
                        for (int i = 0; i < SpecialCarbineMK2Muzzles.Length; i++)
                        {
                            if (Global.Loadout.SpecialCarbineMK2MuzzleString.Equals(SpecialCarbineMK2Muzzles[i]))
                                i = SpecialCarbineMK2Muzzles.Length + 1;
                            else if (!Global.Loadout.SpecialCarbineMK2MuzzleString.Equals(SpecialCarbineMK2Muzzles[i]) && i == SpecialCarbineMK2Muzzles.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.SpecialCarbineMK2MuzzleString + " for Special Carbine MK2 Muzzle is invalid, defaulting to COMPONENT_AT_MUZZLE_01!");
                                Global.Loadout.SpecialCarbineMK2MuzzleString = "COMPONENT_AT_MUZZLE_01";
                                IsError = true;
                            }
                        }
                    }

                    if (Global.Loadout.SpecialCarbineMK2Barrel)
                    {
                        for (int i = 0; i < SpecialCarbineMK2Barrels.Length; i++)
                        {
                            if (Global.Loadout.SpecialCarbineMK2BarrelString.Equals(SpecialCarbineMK2Barrels[i]))
                                i = SpecialCarbineMK2Barrels.Length + 1;
                            else if (!Global.Loadout.SpecialCarbineMK2BarrelString.Equals(SpecialCarbineMK2Barrels[i]) && i == SpecialCarbineMK2Barrels.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.SpecialCarbineMK2BarrelString + " for Special Carbine MK2 Barrel is invalid, defaulting to COMPONENT_AT_SC_BARREL_01!");
                                Global.Loadout.SpecialCarbineMK2BarrelString = "COMPONENT_AT_SC_BARREL_01";
                                IsError = true;
                            }
                        }
                    }
                    break;
                case 10:
                    if (Global.Loadout.BullpupRifleMK2Magazine)
                    {
                        for (int i = 0; i < BullpupRifleMK2Magazines.Length; i++)
                        {
                            if (Global.Loadout.BullpupRifleMK2MagazineString.Equals(BullpupRifleMK2Magazines[i]))
                                i = BullpupRifleMK2Magazines.Length + 1;
                            else if (!Global.Loadout.BullpupRifleMK2MagazineString.Equals(BullpupRifleMK2Magazines[i]) && i == BullpupRifleMK2Magazines.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.BullpupRifleMK2MagazineString + " for Bullpup Rifle MK2 Extended Mag is invalid, defaulting to COMPONENT_BULLPUPRIFLE_MK2_CLIP_02!");
                                Global.Loadout.BullpupRifleMK2MagazineString = "COMPONENT_BULLPUPRIFLE_MK2_CLIP_02";
                                IsError = true;
                            }
                        }
                    }

                    if (Global.Loadout.BullpupRifleMK2Optic)
                    {
                        for (int i = 0; i < BullpupRifleMK2Optics.Length; i++)
                        {
                            if (Global.Loadout.BullpupRifleMK2OpticString.Equals(BullpupRifleMK2Optics[i]))
                                i = BullpupRifleMK2Optics.Length + 1;
                            else if (!Global.Loadout.BullpupRifleMK2OpticString.Equals(BullpupRifleMK2Optics[i]) && i == BullpupRifleMK2Optics.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.BullpupRifleMK2OpticString + " for Bullpup Rifle MK2 Optic is invalid, defaulting to COMPONENT_AT_SCOPE_MACRO_02_MK2!");
                                Global.Loadout.BullpupRifleMK2OpticString = "COMPONENT_AT_SCOPE_MACRO_02_MK2";
                                IsError = true;
                            }
                        }
                    }

                    if (Global.Loadout.BullpupRifleMK2Muzzle)
                    {
                        for (int i = 0; i < BullpupRifleMK2Muzzles.Length; i++)
                        {
                            if (Global.Loadout.BullpupRifleMK2MuzzleString.Equals(BullpupRifleMK2Muzzles[i]))
                                i = BullpupRifleMK2Muzzles.Length + 1;
                            else if (!Global.Loadout.BullpupRifleMK2MuzzleString.Equals(BullpupRifleMK2Muzzles[i]) && i == BullpupRifleMK2Muzzles.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.BullpupRifleMK2MuzzleString + " for Bullpup Rifle MK2 Muzzle is invalid, defaulting to COMPONENT_AT_MUZZLE_01!");
                                Global.Loadout.BullpupRifleMK2MuzzleString = "COMPONENT_AT_MUZZLE_01";
                                IsError = true;
                            }
                        }
                    }

                    if (Global.Loadout.BullpupRifleMK2Barrel)
                    {
                        for (int i = 0; i < BullpupRifleMK2Barrels.Length; i++)
                        {
                            if (Global.Loadout.BullpupRifleMK2BarrelString.Equals(BullpupRifleMK2Barrels[i]))
                                i = BullpupRifleMK2Barrels.Length + 1;
                            else if (!Global.Loadout.BullpupRifleMK2BarrelString.Equals(BullpupRifleMK2Barrels[i]) && i == BullpupRifleMK2Barrels.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.BullpupRifleMK2BarrelString + " for Bullpup Rifle MK2 Barrel is invalid, defaulting to COMPONENT_AT_BP_BARREL_01!");
                                Global.Loadout.BullpupRifleMK2BarrelString = "COMPONENT_AT_BP_BARREL_01";
                                IsError = true;
                            }
                        }
                    }
                    break;
                case 11:
                    if (Global.Loadout.PumpShotgunMK2Magazine)
                    {
                        for (int i = 0; i < PumpShotgunMK2Magazines.Length; i++)
                        {
                            if (Global.Loadout.PumpShotgunMK2MagazineString.Equals(PumpShotgunMK2Magazines[i]))
                                i = PumpShotgunMK2Magazines.Length + 1;
                            else if (!Global.Loadout.PumpShotgunMK2MagazineString.Equals(PumpShotgunMK2Magazines[i]) && i == PumpShotgunMK2Magazines.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.PumpShotgunMK2MagazineString + " for Pump Shotgun MK2 Extended Mag is invalid, defaulting to COMPONENT_PUMPSHOTGUN_MK2_CLIP_01!");
                                Global.Loadout.PumpShotgunMK2MagazineString = "COMPONENT_PUMPSHOTGUN_MK2_CLIP_01";
                                IsError = true;
                            }
                        }
                    }

                    if (Global.Loadout.PumpShotgunMK2Optic)
                    {
                        for (int i = 0; i < PumpShotgunMK2Optics.Length; i++)
                        {
                            if (Global.Loadout.PumpShotgunMK2OpticString.Equals(PumpShotgunMK2Optics[i]))
                                i = PumpShotgunMK2Optics.Length + 1;
                            else if (!Global.Loadout.PumpShotgunMK2OpticString.Equals(PumpShotgunMK2Optics[i]) && i == PumpShotgunMK2Optics.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.PumpShotgunMK2OpticString + " for Pump Shotgun MK2 Extended Mag is invalid, defaulting to COMPONENT_AT_SIGHTS!");
                                Global.Loadout.PumpShotgunMK2OpticString = "COMPONENT_AT_SIGHTS";
                                IsError = true;
                            }
                        }
                    }

                    if (Global.Loadout.PumpShotgunMK2Muzzle)
                    {
                        for (int i = 0; i < PumpShotgunMK2Muzzles.Length; i++)
                        {
                            if (Global.Loadout.PumpShotgunMK2MuzzleString.Equals(PumpShotgunMK2Muzzles[i]))
                                i = PumpShotgunMK2Muzzles.Length + 1;
                            else if (!Global.Loadout.PumpShotgunMK2MuzzleString.Equals(PumpShotgunMK2Muzzles[i]) && i == PumpShotgunMK2Muzzles.Length - 1)
                            {
                                Logger.Log("Component String " + Global.Loadout.PumpShotgunMK2MuzzleString + " for Pump Shotgun MK2 Extended Mag is invalid, defaulting to COMPONENT_AT_MUZZLE_08!");
                                Global.Loadout.PumpShotgunMK2MuzzleString = "COMPONENT_AT_MUZZLE_08";
                                IsError = true;
                            }
                        }
                    }
                    break;
            }
            if (IsError)
            {
                Notifier.Notify("~r~[ERROR] ~s~There was an error with your weapon component strings, check your logs for exact information!");
            }


            Logger.DebugLog("Component String Validation Finished.");
        }
    }
}