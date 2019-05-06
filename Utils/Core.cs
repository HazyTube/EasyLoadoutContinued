/*

Developed by: HazyTube
Name: EasyLoadoutContinued
Released on: LSPDFR and GitHub

*/

using Rage;
using RAGENativeUI;
using System.Windows.Forms;
using RAGENativeUI.Elements;
using System.Collections.Generic;

namespace EasyLoadoutContinued.Utils
{
    public static class Core
    {
        private static List<LoadoutData> loadouts;
        private static List<UIMenuCheckboxItem> pLoadouts;
        private static MenuPool pMenuPool;
        private static UIMenu pLoadoutMenu;
        private static UIMenuItem pGiveLoadout;

        public static void RunPlugin()
        {
            Logger.DebugLog("Core Plugin Function Started");
            loadouts = new List<LoadoutData>();
            pLoadouts = new List<UIMenuCheckboxItem>();

            //Initial menu setup
            pMenuPool = new MenuPool();
            pLoadoutMenu = new UIMenu("Easy Loadout Continued", "Choose your active loadout");
            pLoadoutMenu.SetMenuWidthOffset(90);
            pMenuPool.Add(pLoadoutMenu);

            //This is where the user-defined loadout count happens at and is initially loaded
            //We're running through all of the configs and adding them to the loadouts list aswell as adding a menu item for them
            for (int i = 0; i < Globals.Application.LoadoutCount; i++)
            {
                loadouts.Add(new LoadoutData("Loadout" + (i + 1), Settings.GetConfigFile(i + 1)));

                LoadoutConfig.SetConfigPath(Globals.Application.ConfigPath + loadouts[i].LoadoutConfig);
                LoadoutConfig.LoadConfigTitle();
                pLoadouts.Add(new UIMenuCheckboxItem(Globals.Loadout.LoadoutTitle, true, "Set " + Globals.Loadout.LoadoutTitle + " as the active loadout."));
                pLoadoutMenu.AddItem(pLoadouts[i]);
            }

            pLoadoutMenu.AddItem(pGiveLoadout = new UIMenuItem("Give Loadout"));
            pLoadoutMenu.RefreshIndex();
            pLoadoutMenu.OnItemSelect += OnItemSelect;
            pLoadoutMenu.OnCheckboxChange += OnCheckboxChange;

            //Error checking for default loadout, this should allow us to ensure that if an invalid loadout is chosen then it'll default to the first loadout config
            for (int i = 0; i <= Globals.Application.LoadoutCount; i++)
            {
                if (i == Globals.Application.LoadoutCount)
                {
                    Logger.DebugLog(Globals.Application.DefaultLoadout.LoadoutNumber + " Is Not A Valid Loadout. Defaulting to " + loadouts[0].LoadoutNumber + " as default.");
                    LoadoutConfig.SetConfigPath(Globals.Application.ConfigPath + loadouts[0].LoadoutConfig);
                    LoadoutConfig.LoadConfig();
                    UpdateActiveLoadout(0);
                }
                else if (Globals.Application.DefaultLoadout.LoadoutNumber.Equals(loadouts[i].LoadoutNumber))
                {
                    Logger.DebugLog(Globals.Application.DefaultLoadout.LoadoutNumber + " Is A Valid Loadout, Setting It To Load By Default.");
                    LoadoutConfig.SetConfigPath(Globals.Application.ConfigPath + Globals.Application.DefaultLoadout.LoadoutConfig);
                    LoadoutConfig.LoadConfig();
                    UpdateActiveLoadout(i);
                    break;
                }
            }

            //Initial loadout giving for when player goes on duty
            GiveLoadout();

            //Game loop
            while (true)
            {
                GameFiber.Yield();
                //Checking if keybinds for opening menu is pressed. Currently it doesn't check if any other menu is open, so it can overlap with other RageNativeUI menus.
                if (Game.IsKeyDownRightNow(Globals.Controls.OpenMenuModifier) && Game.IsKeyDown(Globals.Controls.OpenMenu) || Globals.Controls.OpenMenuModifier == Keys.None && Game.IsKeyDown(Globals.Controls.OpenMenu))
                {
                    Logger.DebugLog("Menu button pressed, toggling menu status");
                    pLoadoutMenu.Visible = !pLoadoutMenu.Visible;
                }

                if (Game.IsKeyDownRightNow(Globals.Controls.GiveLoadoutModifier) && Game.IsKeyDown(Globals.Controls.GiveLoadout) || Globals.Controls.GiveLoadoutModifier == Keys.None && Game.IsKeyDown(Globals.Controls.GiveLoadout))
                {
                    Logger.DebugLog("Quick Give Bind Pressed, Giving Loadout");
                    GiveLoadout();
                }

                pMenuPool.ProcessMenus();
            }
        }

        public static void OnCheckboxChange(UIMenu sender, UIMenuCheckboxItem checkbox, bool isChecked)
        {
            //Ensuring UI that had the update is ours..
            if (sender == pLoadoutMenu)
            {
                //Then we're checking the checkboxes were updated...
                for (int i = 0; i < Globals.Application.LoadoutCount; i++)
                {
                    if (checkbox == pLoadouts[i])
                    {
                        UpdateActiveLoadout(i);
                    }
                }
            }
            else
                return;
        }

        private static void UpdateActiveLoadout(int loadout)
        {
            //Quick logic to uncheck non-active loadouts
            for (int i = 0; i < Globals.Application.LoadoutCount; i++)
                if (i == loadout)
                    pLoadouts[i].Checked = true;
                else
                    pLoadouts[i].Checked = false;

            Logger.DebugLog("Starting Active Loadout Change.");

            //Setting and loading config file
            LoadoutConfig.SetConfigPath(Globals.Application.ConfigPath + loadouts[(loadout)].LoadoutConfig);
            LoadoutConfig.LoadConfig();

            Logger.DebugLog("Setting Loadout #" + (loadout + 1) + " as Active Loadout.");
            Logger.DebugLog("Loadout Title: " + Globals.Loadout.LoadoutTitle + " FilePath: " + LoadoutConfig.GetConfigPath());
            pLoadouts[loadout].Checked = true;
            pLoadouts[loadout].Text = Globals.Loadout.LoadoutTitle;

            //Sending notification of active loadout change
            Notifier.DisplayNotification($"Loadout", $"{Globals.Loadout.LoadoutTitle} set as active loadout ~g~successfully~s~!");
            Logger.DebugLog("Active Loadout Changed");
        }

        public static void OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if (sender == pLoadoutMenu)
            {
                if (selectedItem == pGiveLoadout)
                {
                    Logger.DebugLog("Give Loadout Menu Option Selected, Giving Loadout.");
                    GiveLoadout();
                }
            }
            else
            {
                return;
            }
        }

        public static void GiveLoadout()
        {
            Ped playerPed = Game.LocalPlayer.Character;

            Logger.DebugLog("Removing Weapons.");
            Rage.Native.NativeFunction.Natives.REMOVE_ALL_PED_WEAPONS(playerPed, true);

            Logger.DebugLog("Processing Loadout.");

            //Pistols
            if (Globals.Loadout.Pistol)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_PISTOL", Globals.LoadoutAmmo.PistolAmmo, false);
                if (Globals.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_PISTOL", "COMPONENT_AT_PI_FLSH");
                }
            }
            if (Globals.Loadout.CombatPistol)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_COMBATPISTOL", Globals.LoadoutAmmo.PistolAmmo, false);
                if (Globals.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_COMBATPISTOL", "COMPONENT_AT_PI_FLSH");
                }
            }
            if (Globals.Loadout.Pistol50)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_PISTOL50", Globals.LoadoutAmmo.PistolAmmo, false);
                if (Globals.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_PISTOL50", "COMPONENT_AT_PI_FLSH");
                }
            }
            if (Globals.Loadout.APPistol)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_APPISTOL", Globals.LoadoutAmmo.PistolAmmo, false);
                if (Globals.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_APPISTOL", "COMPONENT_AT_PI_FLSH");
                }
            }
            if (Globals.Loadout.HeavyPistol)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_HEAVYPISTOL", Globals.LoadoutAmmo.PistolAmmo, false);
                if (Globals.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_HEAVYPISTOL", "COMPONENT_AT_PI_FLSH");
                }
            }
            if (Globals.Loadout.SNSPistol)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SNSPISTOL", Globals.LoadoutAmmo.PistolAmmo, false);
            }
            if (Globals.Loadout.VintagePistol)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_VINTAGEPISTOL", Globals.LoadoutAmmo.PistolAmmo, false);
            }
            if (Globals.Loadout.MarksmanPistol)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MARKSMANPISTOL", Globals.LoadoutAmmo.PistolAmmo, false);
            }
            if (Globals.Loadout.HeavyRevolver)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_REVOLVER", Globals.LoadoutAmmo.PistolAmmo, false);
            }
            if (Globals.Loadout.PistolMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_PISTOL_MK2", Globals.LoadoutAmmo.PistolAmmo, false);
                if (Globals.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_PISTOL_MK2", "COMPONENT_AT_PI_FLSH_02");
                }
            }
            if (Globals.Loadout.SNSPistolMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SNSPISTOL_MK2", Globals.LoadoutAmmo.PistolAmmo, false);
            }
            if (Globals.Loadout.HeavyRevolverMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_REVOLVER_MK2", Globals.LoadoutAmmo.PistolAmmo, false);
            }
            if (Globals.Loadout.DoubleActionRevolver)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_DOUBLEACTION", Globals.LoadoutAmmo.PistolAmmo, false);
            }

            //Machine Guns
            if (Globals.Loadout.MicroSMG)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MICROSMG", Globals.LoadoutAmmo.MGAmmo, false);
                if (Globals.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_MICROSMG", "COMPONENT_AT_PI_FLSH");
                }
            }
            if (Globals.Loadout.SMG)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SMG", Globals.LoadoutAmmo.MGAmmo, false);
                if (Globals.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_SMG", "COMPONENT_AT_AR_FLSH");
                }
            }
            if (Globals.Loadout.AssaultSMG)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_ASSAULTSMG", Globals.LoadoutAmmo.MGAmmo, false);
                if (Globals.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTSMG", "COMPONENT_AT_AR_FLSH");
                }
            }
            if (Globals.Loadout.TommyGun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_GUSENBERG", Globals.LoadoutAmmo.MGAmmo, false);
            }
            if (Globals.Loadout.MG)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MG", Globals.LoadoutAmmo.MGAmmo, false);
            }
            if (Globals.Loadout.CombatMG)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_COMBATMG", Globals.LoadoutAmmo.MGAmmo, false);
            }
            if (Globals.Loadout.CombatPDW)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_COMBATPDW", Globals.LoadoutAmmo.MGAmmo, false);
                if (Globals.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_COMBATPDW", "COMPONENT_AT_AR_FLSH");
                }
            }
            if (Globals.Loadout.MiniSMG)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MINISMG", Globals.LoadoutAmmo.MGAmmo, false);
            }
            if (Globals.Loadout.MachinePistol)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MACHINEPISTOL", Globals.LoadoutAmmo.MGAmmo, false);
            }
            if (Globals.Loadout.SMGMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SMG_MK2", Globals.LoadoutAmmo.MGAmmo, false);
                if (Globals.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_SMG_MK2", "COMPONENT_AT_AR_FLSH");
                }
            }
            if (Globals.Loadout.CombatMGMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_COMBATMG_MK2", Globals.LoadoutAmmo.MGAmmo, false);
            }

            //Shotguns
            if (Globals.Loadout.PumpShotgun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_PUMPSHOTGUN", Globals.LoadoutAmmo.ShotgunAmmo, false);
                if (Globals.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_PUMPSHOTGUN", "COMPONENT_AT_AR_FLSH");
                }
            }
            if (Globals.Loadout.SawedOffShotgun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SAWNOFFSHOTGUN", Globals.LoadoutAmmo.ShotgunAmmo, false);
            }
            if (Globals.Loadout.AssaultShotgun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_ASSAULTSHOTGUN", Globals.LoadoutAmmo.ShotgunAmmo, false);
                if (Globals.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTSHOTGUN", "COMPONENT_AT_AR_FLSH");
                }
            }
            if (Globals.Loadout.BullpupShotgun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_BULLPUPSHOTGUN", Globals.LoadoutAmmo.ShotgunAmmo, false);
                if (Globals.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPSHOTGUN", "COMPONENT_AT_AR_FLSH");
                }
            }
            if (Globals.Loadout.HeavyShotgun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_HEAVYSHOTGUN", Globals.LoadoutAmmo.ShotgunAmmo, false);
                if (Globals.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_HEAVYSHOTGUN", "COMPONENT_AT_AR_FLSH");
                }
            }
            if (Globals.Loadout.Musket)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MUSKET", Globals.LoadoutAmmo.ShotgunAmmo, false);
            }
            if (Globals.Loadout.DoubleBarrel)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_DBSHOTGUN", Globals.LoadoutAmmo.ShotgunAmmo, false);
            }
            if (Globals.Loadout.AutoShotgun)
            {
                playerPed.Inventory.GiveNewWeapon(317205821, Globals.LoadoutAmmo.ShotgunAmmo, false);
            }


            //Rifles
            if (Globals.Loadout.AssaultRifle)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_ASSAULTRIFLE", Globals.LoadoutAmmo.RifleAmmo, false);
                if (Globals.Loadout.AssaultRifleAttachments)
                {
                    if (Globals.Loadout.AssaultRifleMagazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE", Globals.Loadout.AssaultRifleMagazineString);
                    if (Globals.Loadout.Flashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE", "COMPONENT_AT_AR_FLSH");
                    if (Globals.Loadout.AssaultRifleGrip)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE", "COMPONENT_AT_AR_AFGRIP");
                    if (Globals.Loadout.AssaultRifleOptic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE", "COMPONENT_AT_SCOPE_MACRO");
                    if (Globals.Loadout.AssaultRifleMuzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE", "COMPONENT_AT_AR_SUPP_02");
                }
            }
            if (Globals.Loadout.CarbineRifle)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_CARBINERIFLE", Globals.LoadoutAmmo.RifleAmmo, false);
                if (Globals.Loadout.CarbineRifleAttachments)
                {
                    if (Globals.Loadout.CarbineRifleMagazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE", Globals.Loadout.CarbineRifleMagazineString);
                    if (Globals.Loadout.CarbineRifleFlashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE", "COMPONENT_AT_AR_FLSH");
                    if (Globals.Loadout.CarbineRifleGrip)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE", "COMPONENT_AT_AR_AFGRIP");
                    if (Globals.Loadout.CarbineRifleOptic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE", "COMPONENT_AT_SCOPE_MEDIUM");
                    if (Globals.Loadout.CarbineRifleMuzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE", "COMPONENT_AT_AR_SUPP");
                }
            }
            if (Globals.Loadout.AdvancedRifle)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_ADVANCEDRIFLE", Globals.LoadoutAmmo.RifleAmmo, false);
                if (Globals.Loadout.AdvancedRifleAttachments)
                {
                    if (Globals.Loadout.AdvancedRifleMagazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ADVANCEDRIFLE", Globals.Loadout.AdvancedRifleMagazineString);
                    if (Globals.Loadout.AdvancedRifleFlashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ADVANCEDRIFLE", "COMPONENT_AT_AR_FLSH");
                    if (Globals.Loadout.AdvancedRifleOptic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ADVANCEDRIFLE", "COMPONENT_AT_SCOPE_SMALL");
                    if (Globals.Loadout.AdvancedRifleMuzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ADVANCEDRIFLE", "COMPONENT_AT_AR_SUPP");
                }
            }
            if (Globals.Loadout.SpecialCarbine)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SPECIALCARBINE", Globals.LoadoutAmmo.RifleAmmo, false);
                if (Globals.Loadout.SpecialCarbineAttachments)
                {
                    if (Globals.Loadout.SpecialCarbineMagazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE", Globals.Loadout.SpecialCarbineMagazineString);
                    if (Globals.Loadout.SpecialCarbineFlashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE", "COMPONENT_AT_AR_FLSH");
                    if (Globals.Loadout.SpecialCarbineGrip)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE", "COMPONENT_AT_AR_AFGRIP");
                    if (Globals.Loadout.SpecialCarbineOptic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE", "COMPONENT_AT_SCOPE_MEDIUM");
                    if (Globals.Loadout.SpecialCarbineMuzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE", "COMPONENT_AT_AR_SUPP_02");
                }
            }
            if (Globals.Loadout.BullpupRifle)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_BULLPUPRIFLE", Globals.LoadoutAmmo.RifleAmmo, false);
                if (Globals.Loadout.BullpupRifleAttachments)
                {
                    if (Globals.Loadout.BullpupRifleMagazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE", Globals.Loadout.BullpupRifleMagazineString);
                    if (Globals.Loadout.BullpupRifleFlashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE", "COMPONENT_AT_AR_FLSH");
                    if (Globals.Loadout.BullpupRifleGrip)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE", "COMPONENT_AT_AR_AFGRIP");
                    if (Globals.Loadout.BullpupRifleOptic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE", "COMPONENT_AT_SCOPE_SMALL");
                    if (Globals.Loadout.BullpupRifleMuzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE", "COMPONENT_AT_AR_SUPP");
                }
            }
            if (Globals.Loadout.CompactRifle)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_COMPACTRIFLE", Globals.LoadoutAmmo.RifleAmmo, false);
                if (Globals.Loadout.CompactRifleAttachments)
                {
                    if (Globals.Loadout.CompactRifleMagazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_COMPACTRIFLE", Globals.Loadout.CompactRifleMagazineString);
                }
            }
            if (Globals.Loadout.AssaultRifleMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_ASSAULTRIFLE_MK2", Globals.LoadoutAmmo.RifleAmmo, false);
                if (Globals.Loadout.AssaultRifleMK2Attachments)
                {
                    if (Globals.Loadout.AssaultRifleMK2Magazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE_MK2", Globals.Loadout.AssaultRifleMK2MagazineString);
                    if (Globals.Loadout.AssaultRifleMK2Flashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE_MK2", "COMPONENT_AT_AR_FLSH");
                    if (Globals.Loadout.AssaultRifleMK2Grip)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE_MK2", "COMPONENT_AT_AR_AFGRIP_02");
                    if (Globals.Loadout.AssaultRifleMK2Optic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE_MK2", Globals.Loadout.AssaultRifleMK2OpticString);
                    if (Globals.Loadout.AssaultRifleMK2Muzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE_MK2", Globals.Loadout.AssaultRifleMK2MuzzleString);
                    if (Globals.Loadout.AssaultRifleMK2Barrel)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE_MK2", Globals.Loadout.AssaultRifleMK2BarrelString);
                }
            }
            if (Globals.Loadout.CarbineRifleMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_CARBINERIFLE_MK2", Globals.LoadoutAmmo.RifleAmmo, false);
                if (Globals.Loadout.CarbineRifleMK2Attachments)
                {
                    if (Globals.Loadout.CarbineRifleMK2Magazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE_MK2", Globals.Loadout.CarbineRifleMK2MagazineString);
                    if (Globals.Loadout.CarbineRifleMK2Flashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE_MK2", "COMPONENT_AT_AR_FLSH");
                    if (Globals.Loadout.CarbineRifleMK2Grip)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE_MK2", "COMPONENT_AT_AR_AFGRIP_02");
                    if (Globals.Loadout.CarbineRifleMK2Optic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE_MK2", Globals.Loadout.CarbineRifleMK2OpticString);
                    if (Globals.Loadout.CarbineRifleMK2Muzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE_MK2", Globals.Loadout.CarbineRifleMK2MuzzleString);
                    if (Globals.Loadout.CarbineRifleMK2Barrel)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE_MK2", Globals.Loadout.CarbineRifleMK2BarrelString);
                }
            }
            if (Globals.Loadout.SpecialCarbineMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SPECIALCARBINE_MK2", Globals.LoadoutAmmo.RifleAmmo, false);
                if (Globals.Loadout.SpecialCarbineMK2Attachments)
                {
                    if (Globals.Loadout.SpecialCarbineMK2Magazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE_MK2", Globals.Loadout.SpecialCarbineMK2MagazineString);
                    if (Globals.Loadout.SpecialCarbineMK2Flashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE_MK2", "COMPONENT_AT_AR_FLSH");
                    if (Globals.Loadout.SpecialCarbineMK2Grip)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE_MK2", "COMPONENT_AT_AR_AFGRIP_02");
                    if (Globals.Loadout.SpecialCarbineMK2Optic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE_MK2", Globals.Loadout.SpecialCarbineMK2OpticString);
                    if (Globals.Loadout.SpecialCarbineMK2Muzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE_MK2", Globals.Loadout.SpecialCarbineMK2MuzzleString);
                    if (Globals.Loadout.SpecialCarbineMK2Barrel)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE_MK2", Globals.Loadout.SpecialCarbineMK2BarrelString);
                }
            }
            if (Globals.Loadout.BullpupRifleMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_BULLPUPRIFLE_MK2", Globals.LoadoutAmmo.RifleAmmo, false);
                if (Globals.Loadout.BullpupRifleMK2Attachments)
                {
                    if (Globals.Loadout.BullpupRifleMK2Magazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE_MK2", Globals.Loadout.BullpupRifleMK2MagazineString);
                    if (Globals.Loadout.BullpupRifleMK2Flashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE_MK2", "COMPONENT_AT_AR_FLSH");
                    if (Globals.Loadout.BullpupRifleMK2Grip)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE_MK2", "COMPONENT_AT_AR_AFGRIP_02");
                    if (Globals.Loadout.BullpupRifleMK2Optic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE_MK2", Globals.Loadout.BullpupRifleMK2OpticString);
                    if (Globals.Loadout.BullpupRifleMK2Muzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE_MK2", Globals.Loadout.BullpupRifleMK2MuzzleString);
                    if (Globals.Loadout.BullpupRifleMK2Barrel)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE_MK2", Globals.Loadout.BullpupRifleMK2BarrelString);
                }
            }
            if (Globals.Loadout.PumpShotgunMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_PUMPSHOTGUN_MK2", Globals.LoadoutAmmo.RifleAmmo, false);
                if (Globals.Loadout.PumpShotgunMK2Attachments)
                {
                    if (Globals.Loadout.PumpShotgunMK2Magazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_PUMPSHOTGUN_MK2", Globals.Loadout.PumpShotgunMK2MagazineString);
                    if (Globals.Loadout.PumpShotgunMK2Flashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_PUMPSHOTGUN_MK2", "COMPONENT_AT_AR_FLSH");
                    if (Globals.Loadout.PumpShotgunMK2Optic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_PUMPSHOTGUN_MK2", Globals.Loadout.PumpShotgunMK2OpticString);
                    if (Globals.Loadout.PumpShotgunMK2Muzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_PUMPSHOTGUN_MK2", Globals.Loadout.PumpShotgunMK2MuzzleString);
                }
            }

            //Snipers
            if (Globals.Loadout.SniperRifle)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SNIPERRIFLE", Globals.LoadoutAmmo.SniperAmmo, false);
            }
            if (Globals.Loadout.HeavySniper)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_HEAVYSNIPER", Globals.LoadoutAmmo.SniperAmmo, false);
            }
            if (Globals.Loadout.MarksmanRifle)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MARKSMANRIFLE", Globals.LoadoutAmmo.SniperAmmo, false);
            }
            if (Globals.Loadout.HeavySniperMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_HEAVYSNIPER_MK2", Globals.LoadoutAmmo.SniperAmmo, false);
            }

            //Heavy Weapons
            if (Globals.Loadout.GrenadeLauncher)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_GRENADELAUNCHER", Globals.LoadoutAmmo.HeavyAmmo, false);
            }
            if (Globals.Loadout.RPG)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_RPG", Globals.LoadoutAmmo.HeavyAmmo, false);
            }
            if (Globals.Loadout.Minigun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MINIGUN", 10000, false);
            }
            if (Globals.Loadout.FireworkLauncher)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_FIREWORK", Globals.LoadoutAmmo.HeavyAmmo, false);
            }
            if (Globals.Loadout.HomingLauncher)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_HOMINGLAUNCHER", Globals.LoadoutAmmo.HeavyAmmo, false);
            }
            if (Globals.Loadout.RailGun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_RAILGUN", Globals.LoadoutAmmo.HeavyAmmo, false);
            }
            if (Globals.Loadout.CompactLauncher)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_COMPACTLAUNCHER", Globals.LoadoutAmmo.HeavyAmmo, false);
            }

            //Throwables
            if (Globals.Loadout.Flare)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_FLARE", Globals.LoadoutAmmo.ThrowableCount, false);
            }
            if (Globals.Loadout.BZGas)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_BZGAS", Globals.LoadoutAmmo.ThrowableCount, false);
            }
            if (Globals.Loadout.TearGas)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SMOKEGRENADE", Globals.LoadoutAmmo.ThrowableCount, false);
            }
            if (Globals.Loadout.Molotov)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MOLOTOV", Globals.LoadoutAmmo.ThrowableCount, false);
            }
            if (Globals.Loadout.Grenade)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_GRENADE", Globals.LoadoutAmmo.ThrowableCount, false);
            }
            if (Globals.Loadout.StickyBomb)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_STICKYBOMB", Globals.LoadoutAmmo.ThrowableCount, false);
            }
            if (Globals.Loadout.ProximityMine)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_PROXMINE", Globals.LoadoutAmmo.ThrowableCount, false);
            }
            if (Globals.Loadout.PipeBomb)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_PIPEBOMB", Globals.LoadoutAmmo.ThrowableCount, false);
            }
            if (Globals.Loadout.Snowball)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SNOWBALL", Globals.LoadoutAmmo.ThrowableCount, false);
            }
            if (Globals.Loadout.Baseball)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_BALL", Globals.LoadoutAmmo.ThrowableCount, false);
            }

            //Melee Weapons
            if (Globals.Loadout.Nightstick)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_NIGHTSTICK", 1, false);
            }
            if (Globals.Loadout.Knife)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_KNIFE", 1, false);
            }
            if (Globals.Loadout.Hammer)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_HAMMER", 1, false);
            }
            if (Globals.Loadout.BaseballBat)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_BAT", 1, false);
            }
            if (Globals.Loadout.Crowbar)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_CROWBAR", 1, false);
            }
            if (Globals.Loadout.GolfClub)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_GOLFCLUB", 1, false);
            }
            if (Globals.Loadout.BrokenBottle)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_BOTTLE", 1, false);
            }
            if (Globals.Loadout.AntiqueDagger)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_DAGGER", 1, false);
            }
            if (Globals.Loadout.Hatchet)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_HATCHET", 1, false);
            }
            if (Globals.Loadout.BrassKnuckles)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_KNUCKLE", 1, false);
            }
            if (Globals.Loadout.Machete)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MACHETE", 1, false);
            }
            if (Globals.Loadout.Switchblade)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SWITCHBLADE", 1, false);
            }
            if (Globals.Loadout.BattleAxe)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_BATTLEAXE", 1, false);
            }
            if (Globals.Loadout.Wrench)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_WRENCH", 1, false);
            }
            if (Globals.Loadout.PoolCue)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_POOLCUE", 1, false);
            }

            //Other
            if (Globals.Loadout.Taser)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_STUNGUN", 100, false);
            }
            if (Globals.Loadout.FlareGun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_FLAREGUN", Globals.LoadoutAmmo.ThrowableCount, false);
            }
            if (Globals.Loadout.Flashlight)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_FLASHLIGHT", 1, false);
            }
            if (Globals.Loadout.FireExtinguisher)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_FIREEXTINGUISHER", 10000, false);
            }
            if (Globals.Loadout.GasCan)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_PETROLCAN", 10000, false);
            }

            //Misc
            if (Globals.Loadout.BodyArmor)
            {
                playerPed.Armor = 100;
            }

            Logger.DebugLog("Loadout Successfully Processed.");
            Notifier.DisplayNotification("Loadout", $"Active Loadout: ~g~{Globals.Loadout.LoadoutTitle}~s~ \nLoadout Cleared ~g~Successfully~s~!");
        }
    }
}
