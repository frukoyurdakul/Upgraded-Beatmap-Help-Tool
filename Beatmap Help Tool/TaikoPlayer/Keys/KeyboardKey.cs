using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.TaikoPlayer.KeyModel
{
    public abstract class KeyboardKey
    {
        private long lastPressedTime = 0;
        private bool wasPressed = false, changed = false;

        public abstract Key GetKey();
        public abstract bool IsPressed();

        private readonly OnKeyPressed handler;

        public KeyboardKey(OnKeyPressed handler)
        {
            this.handler = handler;
        }

        public long GetLastPressedTime()
        {
            return lastPressedTime;
        }

        public void CallDelegate()
        {
            handler();
        }

        public void OnUpdateFrame()
        {
            changed = wasPressed != IsPressed();
            if (changed)
            {
                if (IsPressed())
                {
                    lastPressedTime = Environment.TickCount;
                    CallDelegate();
                    wasPressed = true;
                }
                else
                {
                    lastPressedTime = 0;
                    wasPressed = false;
                }
            }
            else if (IsPressed() && Environment.TickCount > lastPressedTime + KeyProcessor.keyPressDelay)
            {
                CallDelegate();
            }
        }
    }
}
