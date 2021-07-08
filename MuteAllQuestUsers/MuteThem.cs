using MelonLoader;
using RubyButtonAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRC;

namespace MuteAllQuestUsers
{
    public class MuteThem : MelonMod
    {
        internal static QMToggleButton MuteButton;
        public static bool Mute = false;
        public static bool hasinit;

        public override void OnApplicationStart()
        {
            CheckPlayers().Start();
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

        //CREDITS day helped me with da queue player i love you uwu

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

        //POV you are Kirai Chan or Abbez about to pop a blood vessel because my code is not immaculate

        private static void MuteAllQuest(Player __0)
        {
            if (Mute && __0.IsQuest())
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

        public override void OnLevelWasLoaded(int level)
        {
            if (hasinit != true)
            {
                MuteButton = new QMToggleButton("ShortcutMenu", 5, 2, "MuteQuest", delegate
                {
                    Mute = true;
                }, "OFF", delegate
                {
                    Mute = false;
                }, "Mutes all Quest Users", Color.cyan, Color.cyan, false, false);
                hasinit = true;
            }
        }
    }
}