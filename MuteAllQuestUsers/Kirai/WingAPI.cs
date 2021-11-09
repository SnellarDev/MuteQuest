using MelonLoader;
using System;
using System.Collections;
using UnityEngine;
using static KiraiMod.WingAPI.Wing;

namespace KiraiMod.WingAPI
{
    public class WingAPI : MelonMod
    {
        public static Action<BaseWing> OnWingInit = new Action<BaseWing>(_ => { });

        private static bool hasInitialized = false;
        public static void Initialize()
        {
            if (hasInitialized) return;
            hasInitialized = true;

            MelonCoroutines.Start(FindUI());
        }

        private static IEnumerator FindUI()
        {
            while ((Misc.UserInterface = GameObject.Find("UserInterface")?.transform) is null)
                yield return null;

            while ((Misc.QuickMenu = Misc.UserInterface.Find("Canvas_QuickMenu(Clone)")) is null)
                yield return null;

            Left.Setup(Misc.QuickMenu.Find("Container/Window/Wing_Left"));
            Right.Setup(Misc.QuickMenu.Find("Container/Window/Wing_Right"));

            Left.WingOpen.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(new Action(() => Init_L()));
            Right.WingOpen.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(new Action(() => Init_R()));
        }

        private static Action Init_L = new Action(() =>
        {
            Init_L = new Action(() => { });
            MelonLogger.Msg("Creating Left Wing UI");
            //GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emoji").gameObject.SetActive(false);
            //GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes").gameObject.SetActive(false);
            //GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Avatars").gameObject.SetActive(false);
            //GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Friends").gameObject.SetActive(false);
            OnWingInit(Left);
            //GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Profile").gameObject.SetActive(false);
        });

        private static Action Init_R = new Action(() =>
        {
            Init_R = new Action(() => { });
            MelonLogger.Msg("Creating Right Wing UI");
            OnWingInit(Right);
        });
    }
}