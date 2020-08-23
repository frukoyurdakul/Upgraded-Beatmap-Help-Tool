using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.TaikoPlayer.KeyModel
{
    public class KeyCombination : KeyboardKey
    {
        private readonly Key functionKey, actualKey;

        public KeyCombination(Key functionKey, Key actualKey, OnKeyPressed handler) : base(handler)
        {
            this.functionKey = functionKey;
            this.actualKey = actualKey;
        }

        public override Key GetKey()
        {
            return actualKey;
        }

        public override bool IsPressed()
        {
            KeyboardState state = Keyboard.GetState();
            return state.IsKeyDown(functionKey) && state.IsKeyDown(actualKey);
        }
    }
}
