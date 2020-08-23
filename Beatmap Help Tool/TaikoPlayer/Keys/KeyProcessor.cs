using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beatmap_Help_Tool.TaikoPlayer.KeyModel;
using OpenTK;
using OpenTK.Input;

namespace Beatmap_Help_Tool.TaikoPlayer
{
    public class KeyProcessor
    {
        public const long keyPressDelay = 1000;
        private readonly ISet<KeyboardKey> keys = new HashSet<KeyboardKey>();

        public KeyProcessor()
        {
            
        }

        public KeyProcessor AssignKey(Key key, OnKeyPressed handler)
        {
            keys.Add(new SingleKey(key, handler));
            return this;
        }

        public KeyProcessor AssignFunctionalKey(Key functionKey, Key actualKey, OnKeyPressed handler)
        {
            keys.Add(new KeyCombination(functionKey, actualKey, handler));
            return this;
        }

        public void OnUpdateFrame()
        {
            foreach (KeyboardKey key in keys)
                key.OnUpdateFrame();
        }

        ~KeyProcessor()
        {
            keys.Clear();
        }
    }
}
