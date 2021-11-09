using UnityEngine;

namespace KiraiMod.WingAPI.RawUI
{
    public class WingToggle
    {
        private bool state;
        private readonly System.Action<bool> onClick;

        public Wing.BaseWing wing;
        public WingButton button;
        public Color on;
        public Color off;

        public bool State
        {
            get => state;
            set
            {
                if (state == value) return;
                button.text.color = value ? on : off;
                onClick(state = value);
            }
        }

        public WingToggle(Wing.BaseWing wing, string name, Transform parent, int pos, Color on, Color off, bool initial, System.Action<bool> onClick)
        {
            this.wing = wing;

            this.on = on;
            this.off = off;
            this.onClick = onClick;

            button = new WingButton(wing, name, parent, pos, new System.Action(() =>
            {
                State ^= true;
            }));

            button.text.color = initial ? on : off;
        }

        public WingToggle(WingPage page, string name, int index, Color on, Color off, bool initial, System.Action<bool> onClick)
        {
            wing = page.wing;

            this.on = on;
            this.off = off;
            this.onClick = onClick;

            button = new WingButton(page, name, index, new System.Action(() =>
            {
                State ^= true;
            }));

            button.text.color = initial ? on : off;
        }
    }
}