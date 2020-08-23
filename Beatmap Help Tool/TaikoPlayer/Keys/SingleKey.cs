using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace Beatmap_Help_Tool.TaikoPlayer.KeyModel
{
    public class SingleKey : KeyboardKey
    {
        private readonly Key key;

        public SingleKey(Key key, OnKeyPressed handler) : base(handler)
        {
            this.key = key;
        }

        public override Key GetKey()
        {
            return key;
        }

        public override bool IsPressed()
        {
            return Keyboard.GetState().IsKeyDown(key);
        }
    }
}
