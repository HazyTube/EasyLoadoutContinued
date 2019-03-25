/*

Author: HazyTube
Name: EasyLoadoutContinued
Released on: LSPDFR and GitHub

*/

using System.Windows.Forms;
using System.Collections.Generic;
using Rage;
using Rage.Native;
using RAGENativeUI;
using RAGENativeUI.Elements;
using LSPD_First_Response.Mod.API;


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
            pLoadoutMenu = new UIMenu("Easy Loadout", "Choose your active loadout");
            pMenuPool.Add(pLoadoutMenu);

            //This is where the user-defined loadout count happens at and is initially loaded
            //We're running through all of the configs and adding them to the loadouts list aswell as adding a menu item for them
            for (int i = 0; i < Global.Application.LoadoutCount; i++)
            {
                loadouts.Add(new LoadoutData("Loadout" + (i + 1), Config.GetConfigFile(i + 1)));

                LoadoutConfig.SetConfigPath(Global.Application.ConfigPath + loadouts[i].LoadoutConfig);
                LoadoutConfig.LoadConfigTitle();
                pLoadouts.Add(new UIMenuCheckboxItem(Global.Loadout.LoadoutTitle, true, "Set " + Global.Loadout.LoadoutTitle + " as the active loadout."));
                pLoadoutMenu.AddItem(pLoadouts[i]);
            }

            pLoadoutMenu.AddItem(pGiveLoadout = new UIMenuItem("Give Loadout"));
            pLoadoutMenu.RefreshIndex();
            pLoadoutMenu.OnItemSelect += OnItemSelect;
            pLoadoutMenu.OnCheckboxChange += OnCheckboxChange;

            //Error checking for default loadout, this should allow us to ensure that if an invalid loadout is chosen then it'll default to the first loadout config
            for (int i = 0; i <= Global.Application.LoadoutCount; i++)
            {
                if (i == Global.Application.LoadoutCount)
                {
                    Logger.DebugLog(Global.Application.DefaultLoadout.LoadoutNumber + " Is Not A Valid Loadout. Defaulting to " + loadouts[0].LoadoutNumber + " as default.");
                    LoadoutConfig.SetConfigPath(Global.Application.ConfigPath + loadouts[0].LoadoutConfig);
                    LoadoutConfig.LoadConfig();
                    UpdateActiveLoadout(0);
                }
                else if (Global.Application.DefaultLoadout.LoadoutNumber.Equals(loadouts[i].LoadoutNumber))
                {
                    Logger.DebugLog(Global.Application.DefaultLoadout.LoadoutNumber + " Is A Valid Loadout, Setting It To Load By Default.");
                    LoadoutConfig.SetConfigPath(Global.Application.ConfigPath + Global.Application.DefaultLoadout.LoadoutConfig);
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
                if (Game.IsKeyDownRightNow(Global.Controls.OpenMenuModifier) && Game.IsKeyDown(Global.Controls.OpenMenu) || Global.Controls.OpenMenuModifier == Keys.None && Game.IsKeyDown(Global.Controls.OpenMenu))
                {
                    Logger.DebugLog("Menu button pressed, toggling menu status");
                    pLoadoutMenu.Visible = !pLoadoutMenu.Visible;
                }

                if (Game.IsKeyDownRightNow(Global.Controls.GiveLoadoutModifier) && Game.IsKeyDown(Global.Controls.GiveLoadout) || Global.Controls.GiveLoadoutModifier == Keys.None && Game.IsKeyDown(Global.Controls.GiveLoadout))
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
                for (int i = 0; i < Global.Application.LoadoutCount; i++)
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
            for (int i = 0; i < Global.Application.LoadoutCount; i++)
                if (i == loadout)
                    pLoadouts[i].Checked = true;
                else
                    pLoadouts[i].Checked = false;

            Logger.DebugLog("Starting Active Loadout Change.");

            //Setting and loading config file
            LoadoutConfig.SetConfigPath(Global.Application.ConfigPath + loadouts[(loadout)].LoadoutConfig);
            LoadoutConfig.LoadConfig();

            Logger.DebugLog("Setting Loadout #" + (loadout + 1) + " as Active Loadout.");
            Logger.DebugLog("Loadout Title: " + Global.Loadout.LoadoutTitle + " FilePath: " + LoadoutConfig.GetConfigPath());
            pLoadouts[loadout].Checked = true;
            pLoadouts[loadout].Text = Global.Loadout.LoadoutTitle;

            //Sending notification of active loadout change
            Notifier.Notify(Global.Loadout.LoadoutTitle + " set as active loadout ~g~Successfully~s~!");
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
            if (Global.Loadout.Pistol)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_PISTOL", Global.LoadoutAmmo.PistolAmmo, false);
                if (Global.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_PISTOL", "COMPONENT_AT_PI_FLSH");
                }
            }
            if (Global.Loadout.CombatPistol)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_COMBATPISTOL", Global.LoadoutAmmo.PistolAmmo, false);
                if (Global.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_COMBATPISTOL", "COMPONENT_AT_PI_FLSH");
                }
            }
            if (Global.Loadout.Pistol50)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_PISTOL50", Global.LoadoutAmmo.PistolAmmo, false);
                if (Global.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_PISTOL50", "COMPONENT_AT_PI_FLSH");
                }
            }
            if (Global.Loadout.APPistol)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_APPISTOL", Global.LoadoutAmmo.PistolAmmo, false);
                if (Global.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_APPISTOL", "COMPONENT_AT_PI_FLSH");
                }
            }
            if (Global.Loadout.HeavyPistol)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_HEAVYPISTOL", Global.LoadoutAmmo.PistolAmmo, false);
                if (Global.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_HEAVYPISTOL", "COMPONENT_AT_PI_FLSH");
                }
            }
            if (Global.Loadout.SNSPistol)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SNSPISTOL", Global.LoadoutAmmo.PistolAmmo, false);
            }
            if (Global.Loadout.VintagePistol)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_VINTAGEPISTOL", Global.LoadoutAmmo.PistolAmmo, false);
            }
            if (Global.Loadout.MarksmanPistol)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MARKSMANPISTOL", Global.LoadoutAmmo.PistolAmmo, false);
            }
            if (Global.Loadout.HeavyRevolver)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_REVOLVER", Global.LoadoutAmmo.PistolAmmo, false);
            }
            if (Global.Loadout.PistolMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_PISTOL_MK2", Global.LoadoutAmmo.PistolAmmo, false);
                if (Global.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_PISTOL_MK2", "COMPONENT_AT_PI_FLSH_02");
                }
            }
            if (Global.Loadout.SNSPistolMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SNSPISTOL_MK2", Global.LoadoutAmmo.PistolAmmo, false);
            }
            if (Global.Loadout.HeavyRevolverMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_REVOLVER_MK2", Global.LoadoutAmmo.PistolAmmo, false);
            }
            if (Global.Loadout.DoubleActionRevolver)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_DOUBLEACTION", Global.LoadoutAmmo.PistolAmmo, false);
            }

            //Machine Guns
            if (Global.Loadout.MicroSMG)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MICROSMG", Global.LoadoutAmmo.MGAmmo, false);
                if (Global.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_MICROSMG", "COMPONENT_AT_PI_FLSH");
                }
            }
            if (Global.Loadout.SMG)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SMG", Global.LoadoutAmmo.MGAmmo, false);
                if (Global.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_SMG", "COMPONENT_AT_AR_FLSH");
                }
            }
            if (Global.Loadout.AssaultSMG)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_ASSAULTSMG", Global.LoadoutAmmo.MGAmmo, false);
                if (Global.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTSMG", "COMPONENT_AT_AR_FLSH");
                }
            }
            if (Global.Loadout.TommyGun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_GUSENBERG", Global.LoadoutAmmo.MGAmmo, false);
            }
            if (Global.Loadout.MG)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MG", Global.LoadoutAmmo.MGAmmo, false);
            }
            if (Global.Loadout.CombatMG)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_COMBATMG", Global.LoadoutAmmo.MGAmmo, false);
            }
            if (Global.Loadout.CombatPDW)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_COMBATPDW", Global.LoadoutAmmo.MGAmmo, false);
                if (Global.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_COMBATPDW", "COMPONENT_AT_AR_FLSH");
                }
            }
            if (Global.Loadout.MiniSMG)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MINISMG", Global.LoadoutAmmo.MGAmmo, false);
            }
            if (Global.Loadout.MachinePistol)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MACHINEPISTOL", Global.LoadoutAmmo.MGAmmo, false);
            }
            if (Global.Loadout.SMGMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SMG_MK2", Global.LoadoutAmmo.MGAmmo, false);
                if (Global.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_SMG_MK2", "COMPONENT_AT_AR_FLSH");
                }
            }
            if (Global.Loadout.CombatMGMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_COMBATMG_MK2", Global.LoadoutAmmo.MGAmmo, false);
            }

            //Shotguns
            if (Global.Loadout.PumpShotgun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_PUMPSHOTGUN", Global.LoadoutAmmo.ShotgunAmmo, false);
                if (Global.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_PUMPSHOTGUN", "COMPONENT_AT_AR_FLSH");
                }
            }
            if (Global.Loadout.SawedOffShotgun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SAWNOFFSHOTGUN", Global.LoadoutAmmo.ShotgunAmmo, false);
            }
            if (Global.Loadout.AssaultShotgun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_ASSAULTSHOTGUN", Global.LoadoutAmmo.ShotgunAmmo, false);
                if (Global.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTSHOTGUN", "COMPONENT_AT_AR_FLSH");
                }
            }
            if (Global.Loadout.BullpupShotgun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_BULLPUPSHOTGUN", Global.LoadoutAmmo.ShotgunAmmo, false);
                if (Global.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPSHOTGUN", "COMPONENT_AT_AR_FLSH");
                }
            }
            if (Global.Loadout.HeavyShotgun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_HEAVYSHOTGUN", Global.LoadoutAmmo.ShotgunAmmo, false);
                if (Global.Loadout.AttachFlashlightToAll)
                {
                    playerPed.Inventory.AddComponentToWeapon("WEAPON_HEAVYSHOTGUN", "COMPONENT_AT_AR_FLSH");
                }
            }
            if (Global.Loadout.Musket)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MUSKET", Global.LoadoutAmmo.ShotgunAmmo, false);
            }
            if (Global.Loadout.DoubleBarrel)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_DBSHOTGUN", Global.LoadoutAmmo.ShotgunAmmo, false);
            }
            if (Global.Loadout.AutoShotgun)
            {
                playerPed.Inventory.GiveNewWeapon(317205821, Global.LoadoutAmmo.ShotgunAmmo, false);
            }


            //Rifles
            if (Global.Loadout.AssaultRifle)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_ASSAULTRIFLE", Global.LoadoutAmmo.RifleAmmo, false);
                if (Global.Loadout.AssaultRifleAttachments)
                {
                    if (Global.Loadout.AssaultRifleMagazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE", Global.Loadout.AssaultRifleMagazineString);
                    if (Global.Loadout.Flashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE", "COMPONENT_AT_AR_FLSH");
                    if (Global.Loadout.AssaultRifleGrip)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE", "COMPONENT_AT_AR_AFGRIP");
                    if (Global.Loadout.AssaultRifleOptic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE", "COMPONENT_AT_SCOPE_MACRO");
                    if (Global.Loadout.AssaultRifleMuzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE", "COMPONENT_AT_AR_SUPP_02");
                }
            }
            if (Global.Loadout.CarbineRifle)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_CARBINERIFLE", Global.LoadoutAmmo.RifleAmmo, false);
                if (Global.Loadout.CarbineRifleAttachments)
                {
                    if (Global.Loadout.CarbineRifleMagazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE", Global.Loadout.CarbineRifleMagazineString);
                    if (Global.Loadout.CarbineRifleFlashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE", "COMPONENT_AT_AR_FLSH");
                    if (Global.Loadout.CarbineRifleGrip)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE", "COMPONENT_AT_AR_AFGRIP");
                    if (Global.Loadout.CarbineRifleOptic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE", "COMPONENT_AT_SCOPE_MEDIUM");
                    if (Global.Loadout.CarbineRifleMuzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE", "COMPONENT_AT_AR_SUPP");
                }
            }
            if (Global.Loadout.AdvancedRifle)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_ADVANCEDRIFLE", Global.LoadoutAmmo.RifleAmmo, false);
                if (Global.Loadout.AdvancedRifleAttachments)
                {
                    if (Global.Loadout.AdvancedRifleMagazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ADVANCEDRIFLE", Global.Loadout.AdvancedRifleMagazineString);
                    if (Global.Loadout.AdvancedRifleFlashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ADVANCEDRIFLE", "COMPONENT_AT_AR_FLSH");
                    if (Global.Loadout.AdvancedRifleOptic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ADVANCEDRIFLE", "COMPONENT_AT_SCOPE_SMALL");
                    if (Global.Loadout.AdvancedRifleMuzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ADVANCEDRIFLE", "COMPONENT_AT_AR_SUPP");
                }
            }
            if (Global.Loadout.SpecialCarbine)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SPECIALCARBINE", Global.LoadoutAmmo.RifleAmmo, false);
                if (Global.Loadout.SpecialCarbineAttachments)
                {
                    if (Global.Loadout.SpecialCarbineMagazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE", Global.Loadout.SpecialCarbineMagazineString);
                    if (Global.Loadout.SpecialCarbineFlashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE", "COMPONENT_AT_AR_FLSH");
                    if (Global.Loadout.SpecialCarbineGrip)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE", "COMPONENT_AT_AR_AFGRIP");
                    if (Global.Loadout.SpecialCarbineOptic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE", "COMPONENT_AT_SCOPE_MEDIUM");
                    if (Global.Loadout.SpecialCarbineMuzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE", "COMPONENT_AT_AR_SUPP_02");
                }
            }
            if (Global.Loadout.BullpupRifle)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_BULLPUPRIFLE", Global.LoadoutAmmo.RifleAmmo, false);
                if (Global.Loadout.BullpupRifleAttachments)
                {
                    if (Global.Loadout.BullpupRifleMagazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE", Global.Loadout.BullpupRifleMagazineString);
                    if (Global.Loadout.BullpupRifleFlashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE", "COMPONENT_AT_AR_FLSH");
                    if (Global.Loadout.BullpupRifleGrip)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE", "COMPONENT_AT_AR_AFGRIP");
                    if (Global.Loadout.BullpupRifleOptic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE", "COMPONENT_AT_SCOPE_SMALL");
                    if (Global.Loadout.BullpupRifleMuzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE", "COMPONENT_AT_AR_SUPP");
                }
            }
            if (Global.Loadout.CompactRifle)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_COMPACTRIFLE", Global.LoadoutAmmo.RifleAmmo, false);
                if (Global.Loadout.CompactRifleAttachments)
                {
                    if (Global.Loadout.CompactRifleMagazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_COMPACTRIFLE", Global.Loadout.CompactRifleMagazineString);
                }
            }
            if (Global.Loadout.AssaultRifleMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_ASSAULTRIFLE_MK2", Global.LoadoutAmmo.RifleAmmo, false);
                if (Global.Loadout.AssaultRifleMK2Attachments)
                {
                    if (Global.Loadout.AssaultRifleMK2Magazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE_MK2", Global.Loadout.AssaultRifleMK2MagazineString);
                    if (Global.Loadout.AssaultRifleMK2Flashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE_MK2", "COMPONENT_AT_AR_FLSH");
                    if (Global.Loadout.AssaultRifleMK2Grip)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE_MK2", "COMPONENT_AT_AR_AFGRIP_02");
                    if (Global.Loadout.AssaultRifleMK2Optic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE_MK2", Global.Loadout.AssaultRifleMK2OpticString);
                    if (Global.Loadout.AssaultRifleMK2Muzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE_MK2", Global.Loadout.AssaultRifleMK2MuzzleString);
                    if (Global.Loadout.AssaultRifleMK2Barrel)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_ASSAULTRIFLE_MK2", Global.Loadout.AssaultRifleMK2BarrelString);
                }
            }
            if (Global.Loadout.CarbineRifleMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_CARBINERIFLE_MK2", Global.LoadoutAmmo.RifleAmmo, false);
                if (Global.Loadout.CarbineRifleMK2Attachments)
                {
                    if (Global.Loadout.CarbineRifleMK2Magazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE_MK2", Global.Loadout.CarbineRifleMK2MagazineString);
                    if (Global.Loadout.CarbineRifleMK2Flashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE_MK2", "COMPONENT_AT_AR_FLSH");
                    if (Global.Loadout.CarbineRifleMK2Grip)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE_MK2", "COMPONENT_AT_AR_AFGRIP_02");
                    if (Global.Loadout.CarbineRifleMK2Optic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE_MK2", Global.Loadout.CarbineRifleMK2OpticString);
                    if (Global.Loadout.CarbineRifleMK2Muzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE_MK2", Global.Loadout.CarbineRifleMK2MuzzleString);
                    if (Global.Loadout.CarbineRifleMK2Barrel)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_CARBINERIFLE_MK2", Global.Loadout.CarbineRifleMK2BarrelString);
                }
            }
            if (Global.Loadout.SpecialCarbineMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SPECIALCARBINE_MK2", Global.LoadoutAmmo.RifleAmmo, false);
                if (Global.Loadout.SpecialCarbineMK2Attachments)
                {
                    if (Global.Loadout.SpecialCarbineMK2Magazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE_MK2", Global.Loadout.SpecialCarbineMK2MagazineString);
                    if (Global.Loadout.SpecialCarbineMK2Flashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE_MK2", "COMPONENT_AT_AR_FLSH");
                    if (Global.Loadout.SpecialCarbineMK2Grip)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE_MK2", "COMPONENT_AT_AR_AFGRIP_02");
                    if (Global.Loadout.SpecialCarbineMK2Optic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE_MK2", Global.Loadout.SpecialCarbineMK2OpticString);
                    if (Global.Loadout.SpecialCarbineMK2Muzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE_MK2", Global.Loadout.SpecialCarbineMK2MuzzleString);
                    if (Global.Loadout.SpecialCarbineMK2Barrel)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_SPECIALCARBINE_MK2", Global.Loadout.SpecialCarbineMK2BarrelString);
                }
            }
            if (Global.Loadout.BullpupRifleMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_BULLPUPRIFLE_MK2", Global.LoadoutAmmo.RifleAmmo, false);
                if (Global.Loadout.BullpupRifleMK2Attachments)
                {
                    if (Global.Loadout.BullpupRifleMK2Magazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE_MK2", Global.Loadout.BullpupRifleMK2MagazineString);
                    if (Global.Loadout.BullpupRifleMK2Flashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE_MK2", "COMPONENT_AT_AR_FLSH");
                    if (Global.Loadout.BullpupRifleMK2Grip)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE_MK2", "COMPONENT_AT_AR_AFGRIP_02");
                    if (Global.Loadout.BullpupRifleMK2Optic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE_MK2", Global.Loadout.BullpupRifleMK2OpticString);
                    if (Global.Loadout.BullpupRifleMK2Muzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE_MK2", Global.Loadout.BullpupRifleMK2MuzzleString);
                    if (Global.Loadout.BullpupRifleMK2Barrel)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_BULLPUPRIFLE_MK2", Global.Loadout.BullpupRifleMK2BarrelString);
                }
            }
            if (Global.Loadout.PumpShotgunMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_PUMPSHOTGUN_MK2", Global.LoadoutAmmo.RifleAmmo, false);
                if (Global.Loadout.PumpShotgunMK2Attachments)
                {
                    if (Global.Loadout.PumpShotgunMK2Magazine)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_PUMPSHOTGUN_MK2", Global.Loadout.PumpShotgunMK2MagazineString);
                    if (Global.Loadout.PumpShotgunMK2Flashlight)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_PUMPSHOTGUN_MK2", "COMPONENT_AT_AR_FLSH");
                    if (Global.Loadout.PumpShotgunMK2Optic)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_PUMPSHOTGUN_MK2", Global.Loadout.PumpShotgunMK2OpticString);
                    if (Global.Loadout.PumpShotgunMK2Muzzle)
                        playerPed.Inventory.AddComponentToWeapon("WEAPON_PUMPSHOTGUN_MK2", Global.Loadout.PumpShotgunMK2MuzzleString);
                }
            }

            //Snipers
            if (Global.Loadout.SniperRifle)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SNIPERRIFLE", Global.LoadoutAmmo.SniperAmmo, false);
            }
            if (Global.Loadout.HeavySniper)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_HEAVYSNIPER", Global.LoadoutAmmo.SniperAmmo, false);
            }
            if (Global.Loadout.MarksmanRifle)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MARKSMANRIFLE", Global.LoadoutAmmo.SniperAmmo, false);
            }
            if (Global.Loadout.HeavySniperMK2)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_HEAVYSNIPER_MK2", Global.LoadoutAmmo.SniperAmmo, false);
            }

            //Heavy Weapons
            if (Global.Loadout.GrenadeLauncher)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_GRENADELAUNCHER", Global.LoadoutAmmo.HeavyAmmo, false);
            }
            if (Global.Loadout.RPG)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_RPG", Global.LoadoutAmmo.HeavyAmmo, false);
            }
            if (Global.Loadout.Minigun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MINIGUN", 10000, false);
            }
            if (Global.Loadout.FireworkLauncher)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_FIREWORK", Global.LoadoutAmmo.HeavyAmmo, false);
            }
            if (Global.Loadout.HomingLauncher)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_HOMINGLAUNCHER", Global.LoadoutAmmo.HeavyAmmo, false);
            }
            if (Global.Loadout.RailGun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_RAILGUN", Global.LoadoutAmmo.HeavyAmmo, false);
            }
            if (Global.Loadout.CompactLauncher)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_COMPACTLAUNCHER", Global.LoadoutAmmo.HeavyAmmo, false);
            }

            //Throwables
            if (Global.Loadout.Flare)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_FLARE", Global.LoadoutAmmo.ThrowableCount, false);
            }
            if (Global.Loadout.BZGas)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_BZGAS", Global.LoadoutAmmo.ThrowableCount, false);
            }
            if (Global.Loadout.TearGas)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SMOKEGRENADE", Global.LoadoutAmmo.ThrowableCount, false);
            }
            if (Global.Loadout.Molotov)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MOLOTOV", Global.LoadoutAmmo.ThrowableCount, false);
            }
            if (Global.Loadout.Grenade)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_GRENADE", Global.LoadoutAmmo.ThrowableCount, false);
            }
            if (Global.Loadout.StickyBomb)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_STICKYBOMB", Global.LoadoutAmmo.ThrowableCount, false);
            }
            if (Global.Loadout.ProximityMine)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_PROXMINE", Global.LoadoutAmmo.ThrowableCount, false);
            }
            if (Global.Loadout.PipeBomb)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_PIPEBOMB", Global.LoadoutAmmo.ThrowableCount, false);
            }
            if (Global.Loadout.Snowball)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SNOWBALL", Global.LoadoutAmmo.ThrowableCount, false);
            }
            if (Global.Loadout.Baseball)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_BALL", Global.LoadoutAmmo.ThrowableCount, false);
            }

            //Melee Weapons
            if (Global.Loadout.Nightstick)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_NIGHTSTICK", 1, false);
            }
            if (Global.Loadout.Knife)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_KNIFE", 1, false);
            }
            if (Global.Loadout.Hammer)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_HAMMER", 1, false);
            }
            if (Global.Loadout.BaseballBat)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_BAT", 1, false);
            }
            if (Global.Loadout.Crowbar)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_CROWBAR", 1, false);
            }
            if (Global.Loadout.GolfClub)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_GOLFCLUB", 1, false);
            }
            if (Global.Loadout.BrokenBottle)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_BOTTLE", 1, false);
            }
            if (Global.Loadout.AntiqueDagger)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_DAGGER", 1, false);
            }
            if (Global.Loadout.Hatchet)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_HATCHET", 1, false);
            }
            if (Global.Loadout.BrassKnuckles)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_KNUCKLE", 1, false);
            }
            if (Global.Loadout.Machete)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_MACHETE", 1, false);
            }
            if (Global.Loadout.Switchblade)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_SWITCHBLADE", 1, false);
            }
            if (Global.Loadout.BattleAxe)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_BATTLEAXE", 1, false);
            }
            if (Global.Loadout.Wrench)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_WRENCH", 1, false);
            }
            if (Global.Loadout.PoolCue)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_POOLCUE", 1, false);
            }

            //Other
            if (Global.Loadout.Taser)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_STUNGUN", 100, false);
            }
            if (Global.Loadout.FlareGun)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_FLAREGUN", Global.LoadoutAmmo.ThrowableCount, false);
            }
            if (Global.Loadout.Flashlight)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_FLASHLIGHT", 1, false);
            }
            if (Global.Loadout.FireExtinguisher)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_FIREEXTINGUISHER", 10000, false);
            }
            if (Global.Loadout.GasCan)
            {
                playerPed.Inventory.GiveNewWeapon("WEAPON_PETROLCAN", 10000, false);
            }

            //Misc
            if (Global.Loadout.BodyArmor)
            {
                playerPed.Armor = 100;
            }

            Logger.DebugLog("Loadout Successfully Processed.");
            Notifier.Notify("(Active: ~g~" + Global.Loadout.LoadoutTitle + "~s~) Loadout Cleared ~g~Successfully~s~!");
        }
    }
}
