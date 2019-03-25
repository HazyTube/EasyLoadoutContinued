/*

Author: HazyTube
Name: EasyLoadoutContinued
Released on: LSPDFR and GitHub

*/

using System.Windows.Forms;


namespace EasyLoadoutContinued.Utils
{
    internal static class Global
    {
        internal static class Application
        {
            public static string CurrentVersion { get; set; }
            public static string LatestVersion { get; set; }
            public static LoadoutData DefaultLoadout { get; set; }
            public static bool DebugLogging { get; set; }
            public static string ConfigPath { get; set; }
            public static int LoadoutCount { get; set; }
        }

        internal static class Controls
        {
            public static Keys OpenMenu { get; set; }
            public static Keys OpenMenuModifier { get; set; }
            public static Keys GiveLoadout { get; set; }
            public static Keys GiveLoadoutModifier { get; set; }
        }

        internal static class LoadoutAmmo
        {
            public static short PistolAmmo { get; set; }
            public static short MGAmmo { get; set; }
            public static short ShotgunAmmo { get; set; }
            public static short RifleAmmo { get; set; }
            public static short SniperAmmo { get; set; }
            public static short HeavyAmmo { get; set; }
            public static short ThrowableCount { get; set; }
        }

        internal static class Loadout
        {
            //Loadout Title
            public static string LoadoutTitle { get; set; }

            //Pistols
            public static bool Pistol { get; set; }
            public static bool CombatPistol { get; set; }
            public static bool Pistol50 { get; set; }
            public static bool APPistol { get; set; }
            public static bool HeavyPistol { get; set; }
            public static bool SNSPistol { get; set; }
            public static bool VintagePistol { get; set; }
            public static bool MarksmanPistol { get; set; }
            public static bool HeavyRevolver { get; set; }
            public static bool MachinePistol { get; set; }
            public static bool PistolMK2 { get; set; }
            public static bool SNSPistolMK2 { get; set; }
            public static bool HeavyRevolverMK2 { get; set; }
            public static bool DoubleActionRevolver { get; set; }

            //Machine Guns
            public static bool MicroSMG { get; set; }
            public static bool SMG { get; set; }
            public static bool AssaultSMG { get; set; }
            public static bool TommyGun { get; set; }
            public static bool MG { get; set; }
            public static bool CombatMG { get; set; }
            public static bool CombatPDW { get; set; }
            public static bool MiniSMG { get; set; }
            public static bool SMGMK2 { get; set; }
            public static bool CombatMGMK2 { get; set; }

            //Shotguns
            public static bool PumpShotgun { get; set; }
            public static bool SawedOffShotgun { get; set; }
            public static bool AssaultShotgun { get; set; }
            public static bool BullpupShotgun { get; set; }
            public static bool HeavyShotgun { get; set; }
            public static bool Musket { get; set; }
            public static bool DoubleBarrel { get; set; }
            public static bool AutoShotgun { get; set; }
            public static bool PumpShotgunMK2 { get; set; }

            //Rifles
            public static bool AssaultRifle { get; set; }
            public static bool CarbineRifle { get; set; }
            public static bool AdvancedRifle { get; set; }
            public static bool SpecialCarbine { get; set; }
            public static bool BullpupRifle { get; set; }
            public static bool CompactRifle { get; set; }
            public static bool AssaultRifleMK2 { get; set; }
            public static bool CarbineRifleMK2 { get; set; }
            public static bool SpecialCarbineMK2 { get; set; }
            public static bool BullpupRifleMK2 { get; set; }

            //Attachments
            public static bool AssaultRifleAttachments { get; set; }
            public static bool CarbineRifleAttachments { get; set; }
            public static bool AdvancedRifleAttachments { get; set; }
            public static bool SpecialCarbineAttachments { get; set; }
            public static bool BullpupRifleAttachments { get; set; }
            public static bool CompactRifleAttachments { get; set; }
            public static bool AssaultRifleMK2Attachments { get; set; }
            public static bool CarbineRifleMK2Attachments { get; set; }
            public static bool SpecialCarbineMK2Attachments { get; set; }
            public static bool BullpupRifleMK2Attachments { get; set; }
            public static bool PumpShotgunMK2Attachments { get; set; }

            //Rifle Attachments
            public static bool AssaultRifleMagazine { get; set; }
            public static string AssaultRifleMagazineString { get; set; }
            public static bool AssaultRifleGrip { get; set; }
            public static bool AssaultRifleOptic { get; set; }
            public static bool AssaultRifleFlashlight { get; set; }
            public static bool AssaultRifleMuzzle { get; set; }

            public static bool CarbineRifleMagazine { get; set; }
            public static string CarbineRifleMagazineString { get; set; }
            public static bool CarbineRifleGrip { get; set; }
            public static bool CarbineRifleOptic { get; set; }
            public static bool CarbineRifleFlashlight { get; set; }
            public static bool CarbineRifleMuzzle { get; set; }

            public static bool AdvancedRifleMagazine { get; set; }
            public static string AdvancedRifleMagazineString { get; set; }
            public static bool AdvancedRifleOptic { get; set; }
            public static bool AdvancedRifleFlashlight { get; set; }
            public static bool AdvancedRifleMuzzle { get; set; }

            public static bool SpecialCarbineMagazine { get; set; }
            public static string SpecialCarbineMagazineString { get; set; }
            public static bool SpecialCarbineGrip { get; set; }
            public static bool SpecialCarbineOptic { get; set; }
            public static bool SpecialCarbineFlashlight { get; set; }
            public static bool SpecialCarbineMuzzle { get; set; }

            public static bool BullpupRifleMagazine { get; set; }
            public static string BullpupRifleMagazineString { get; set; }
            public static bool BullpupRifleGrip { get; set; }
            public static bool BullpupRifleOptic { get; set; }
            public static bool BullpupRifleFlashlight { get; set; }
            public static bool BullpupRifleMuzzle { get; set; }

            public static bool CompactRifleMagazine { get; set; }
            public static string CompactRifleMagazineString { get; set; }

            public static bool AssaultRifleMK2Magazine { get; set; }
            public static bool AssaultRifleMK2Grip { get; set; }
            public static bool AssaultRifleMK2Optic { get; set; }
            public static bool AssaultRifleMK2Flashlight { get; set; }
            public static bool AssaultRifleMK2Barrel { get; set; }
            public static bool AssaultRifleMK2Muzzle { get; set; }
            public static string AssaultRifleMK2MagazineString { get; set; }
            public static string AssaultRifleMK2OpticString { get; set; }
            public static string AssaultRifleMK2BarrelString { get; set; }
            public static string AssaultRifleMK2MuzzleString { get; set; }

            public static bool CarbineRifleMK2Magazine { get; set; }
            public static bool CarbineRifleMK2Grip { get; set; }
            public static bool CarbineRifleMK2Optic { get; set; }
            public static bool CarbineRifleMK2Flashlight { get; set; }
            public static bool CarbineRifleMK2Barrel { get; set; }
            public static bool CarbineRifleMK2Muzzle { get; set; }
            public static string CarbineRifleMK2MagazineString { get; set; }
            public static string CarbineRifleMK2OpticString { get; set; }
            public static string CarbineRifleMK2BarrelString { get; set; }
            public static string CarbineRifleMK2MuzzleString { get; set; }

            public static bool SpecialCarbineMK2Magazine { get; set; }
            public static bool SpecialCarbineMK2Grip { get; set; }
            public static bool SpecialCarbineMK2Optic { get; set; }
            public static bool SpecialCarbineMK2Flashlight { get; set; }
            public static bool SpecialCarbineMK2Barrel { get; set; }
            public static bool SpecialCarbineMK2Muzzle { get; set; }
            public static string SpecialCarbineMK2MagazineString { get; set; }
            public static string SpecialCarbineMK2OpticString { get; set; }
            public static string SpecialCarbineMK2BarrelString { get; set; }
            public static string SpecialCarbineMK2MuzzleString { get; set; }

            public static bool BullpupRifleMK2Magazine { get; set; }
            public static bool BullpupRifleMK2Grip { get; set; }
            public static bool BullpupRifleMK2Optic { get; set; }
            public static bool BullpupRifleMK2Flashlight { get; set; }
            public static bool BullpupRifleMK2Barrel { get; set; }
            public static bool BullpupRifleMK2Muzzle { get; set; }
            public static string BullpupRifleMK2MagazineString { get; set; }
            public static string BullpupRifleMK2OpticString { get; set; }
            public static string BullpupRifleMK2BarrelString { get; set; }
            public static string BullpupRifleMK2MuzzleString { get; set; }

            public static bool PumpShotgunMK2Magazine { get; set; }
            public static bool PumpShotgunMK2Optic { get; set; }
            public static bool PumpShotgunMK2Flashlight { get; set; }
            public static bool PumpShotgunMK2Muzzle { get; set; }
            public static string PumpShotgunMK2MagazineString { get; set; }
            public static string PumpShotgunMK2OpticString { get; set; }
            public static string PumpShotgunMK2MuzzleString { get; set; }


            //Snipers
            public static bool SniperRifle { get; set; }
            public static bool HeavySniper { get; set; }
            public static bool MarksmanRifle { get; set; }
            public static bool HeavySniperMK2 { get; set; }
            public static bool MarksmanRifleMK2 { get; set; }

            //Heavy Weapons
            public static bool GrenadeLauncher { get; set; }
            public static bool RPG { get; set; }
            public static bool Minigun { get; set; }
            public static bool FireworkLauncher { get; set; }
            public static bool HomingLauncher { get; set; }
            public static bool RailGun { get; set; }
            public static bool CompactLauncher { get; set; }

            //Throwables
            public static bool Flare { get; set; }
            public static bool BZGas { get; set; }
            public static bool TearGas { get; set; }
            public static bool Molotov { get; set; }
            public static bool Grenade { get; set; }
            public static bool StickyBomb { get; set; }
            public static bool ProximityMine { get; set; }
            public static bool PipeBomb { get; set; }
            public static bool Snowball { get; set; }
            public static bool Baseball { get; set; }

            //Melee Weapons
            public static bool Nightstick { get; set; }
            public static bool Knife { get; set; }
            public static bool Hammer { get; set; }
            public static bool BaseballBat { get; set; }
            public static bool Crowbar { get; set; }
            public static bool GolfClub { get; set; }
            public static bool BrokenBottle { get; set; }
            public static bool AntiqueDagger { get; set; }
            public static bool Hatchet { get; set; }
            public static bool BrassKnuckles { get; set; }
            public static bool Machete { get; set; }
            public static bool Switchblade { get; set; }
            public static bool BattleAxe { get; set; }
            public static bool Wrench { get; set; }
            public static bool PoolCue { get; set; }

            //Other
            public static bool Taser { get; set; }
            public static bool FlareGun { get; set; }
            public static bool Flashlight { get; set; }
            public static bool FireExtinguisher { get; set; }
            public static bool GasCan { get; set; }

            //Misc
            public static bool BodyArmor { get; set; }
            public static bool AttachFlashlightToAll { get; set; }
        }
    }
}
