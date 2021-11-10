using KiraiMod.WingAPI;
using KiraiMod.WingAPI.RawUI;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRC;

namespace MuteAllQuestUsers
{
    public class MuteThem : MelonMod
    {
        public static bool Mute = false;

        public override void OnApplicationStart()
        {
            MelonCoroutines.Start(CheckPlayers());
            WingAPI.Initialize();
            MelonCoroutines.Start(WhereDaUI());
        }

        public static IEnumerator CheckPlayers()
        {
            for (; ; )
            {
                if (PlayerExtensions.IsInWorld())
                {
                    yield return QueuePlayerActions(delegate (Player player)
                    {
                        try
                        {
                            MuteAllQuest(player);
                        }
                        catch
                        {
                        }
                    }, 0.1f);
                }
                else
                {
                    yield return new WaitForSeconds(4);
                }
            }
        }

        // CREDITS day helped me with the queue player i love you uwu

        public static IEnumerator QueuePlayerActions(Action<Player> OnPlayerAction, float WaitBetweenPlayer)
        {
            var AllPlayers = PlayerExtensions.GetAllPlayers();
            foreach (var player in AllPlayers)
            {
                if (player != null)
                {
                    OnPlayerAction?.Invoke(player);
                    yield return new WaitForSeconds(WaitBetweenPlayer);
                }
            }
            yield return null;
        }

        private static void MuteAllQuest(Player __0)
        {
            if (Mute && __0.IsQuest()) // this will also mute people who spoof xd if you wanna be a quest user so bad then i guess we'll treat you like one
            {
                if (User.ContainsKey(PlayerExtensions.GetAPIUser(__0).id))
                {
                    if (__0 == null)
                    {
                        User.Remove(__0.GetAPIUser().id);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    User.Add(PlayerExtensions.GetAPIUser(__0).id, new MuteThem());
                    __0.LocalMute();
                }
            }
            else if (__0.IsQuest() && !Mute)
            {
                if (User.ContainsKey(PlayerExtensions.GetAPIUser(__0).id))
                {
                    User.Remove(__0.GetAPIUser().id);
                    __0.LocalUnMute();
                }
            }
        }

        internal static Dictionary<string, MuteThem> User = new Dictionary<string, MuteThem>();

       public static void DAGUI()
        {
            try
            {
                WingAPI.OnWingInit += new System.Action<Wing.BaseWing>(wing123 =>
                {
                    WingPage page123 = wing123.CreatePage("MuteQuest");
                    WingToggle toggle123 = page123.CreateToggle("Mute", 0, UnityEngine.Color.green, UnityEngine.Color.red, false, new System.Action<bool>(state => Mute = state));
                });
            }
            catch (Exception ex)
            {
                MelonLogger.Msg(ex);
            }
        }
        private IEnumerator WhereDaUI()
        {
            while (VRCUiManager.prop_VRCUiManager_0 == null) yield return null;
            DAGUI();
            yield break;
        }
    }
}