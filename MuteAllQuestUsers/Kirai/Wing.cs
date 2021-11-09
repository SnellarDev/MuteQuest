using KiraiMod.WingAPI.RawUI;
using System.Collections.Generic;
using UnityEngine;

namespace KiraiMod.WingAPI
{
    public static class Wing
    {
        public static BaseWing Left = new BaseWing();
        public static BaseWing Right = new BaseWing();

        public static class Misc
        {
            public static Transform UserInterface; // UserInterface
            public static Transform QuickMenu; //     UserInterface/Canvas_QuickMenu(Clone)
        }

        public class BaseWing
        {
            public List<WingPage> openedPages = new List<WingPage>();

            internal void Setup(Transform wing)
            {
                Wing = wing;
                WingOpen = wing.Find("Button");
                WingPages = wing.Find("Container/InnerContainer");
                WingMenu = WingPages.Find("WingMenu");
                WingButtons = WingPages.Find("WingMenu/ScrollRect/Viewport/VerticalLayoutGroup");

                ProfilePage = WingPages.Find("Profile");
                ProfileButton = WingButtons.Find("Button_Profile");
            }

            public Transform Wing; //        UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left
            public Transform WingOpen; //    UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Button
            public Transform WingPages; //   UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Container/InnerContainer
            public Transform WingMenu; //    UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Container/InnerContainer/WingMenu
            public Transform WingButtons; // UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup

            public Transform ProfilePage;
            public Transform ProfileButton;
        }
    }
}