using HarmonyLib;
using KiraiMod.WingAPI;
using KiraiMod.WingAPI.RawUI;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VRC;

namespace MuteAllQuestUsers
{
    public class MuteThem : MelonMod
    {
        public override void OnApplicationStart()
        {
            WingAPI.Initialize();
            MelonCoroutines.Start(WhereDaUI());
            MethodInfo[] array = (from m in typeof(NetworkManager).GetMethods()
                                  where m.Name.Contains("Method_Public_Void_Player_") && !m.Name.Contains("PDM")
                                  select m).ToArray();
            try { HarmonyInstance.Patch(AccessTools.Method(typeof(NetworkManager), array[0].Name, null, null), GetPatch("OnPlayerJoined")); } catch (Exception e) { MelonLogger.Error($"Error Patching OnPlayerJoined => {e.Message}"); }
        }

        private static bool OnPlayerJoined(ref VRC.Player __0)
        {
            try
            {
                if(Mute)
                MuteCheck(__0);
            }
            catch { }
            return true;
        }

        private static void MuteCheck(Player __0)
        {
           if (Mute && __0.IsQuest() && __0.IsInVR())
               __0.LocalMute();
           else if(__0.IsQuest() && __0.IsInVR())
               __0.LocalUnMute();
        }
        private static void MuteCheckAll()
        {
            foreach(var player in PlayerExtensions.AllPlayers)
                MuteCheck(player);
        }

       public static void DAGUI()
        {
            try
            {
                WingAPI.OnWingInit += new System.Action<Wing.BaseWing>(wing123 =>
                {
                    WingPage page123 = wing123.CreatePage("MuteQuest");
                    WingToggle toggle123 = page123.CreateToggle("Mute", 0, UnityEngine.Color.green, UnityEngine.Color.red, false, (bool toggled) =>
                    {
                        if (toggled)
                        {
                            Mute = true;
                            MuteCheckAll();
                        }
                        else
                        {
                            Mute = false;
                            MuteCheckAll();
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                MelonLogger.Error(ex);
            }
        }

        private IEnumerator WhereDaUI()
        {
            while (VRCUiManager.prop_VRCUiManager_0 == null) yield return null;
            DAGUI();
            yield break;
        }

        private static HarmonyMethod GetPatch(string name)
        {
            return new HarmonyMethod(typeof(MuteThem).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));
        }

        public static bool Mute = false;
    }
}