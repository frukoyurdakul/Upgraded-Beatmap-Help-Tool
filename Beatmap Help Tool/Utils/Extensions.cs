﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beatmap_Help_Tool.Utils
{
    public static class Extensions
    {
        public static void Invoke(this Control control, Action action)
        {
            control.Invoke(action);
        }
    }
}
