using KiraiMod.WingAPI.RawUI;
using UnityEngine;

namespace KiraiMod.WingAPI
{
    public static class Extensions
    {
        public static WingPage CreatePage(this Wing.BaseWing page, string name) => new WingPage(page, name);
        public static WingPage CreateNestedPage(this WingPage page, string name, int index) => new WingPage(page, name, index);
        public static WingButton CreateButton(this WingPage page, string name, int index, System.Action onClick) => new WingButton(page, name, index, onClick);
        public static WingToggle CreateToggle(this WingPage page, string name, int index, Color on, Color off, bool initial, System.Action<bool> onClick) => new WingToggle(page, name, index, on, off, initial, onClick);
    }
}