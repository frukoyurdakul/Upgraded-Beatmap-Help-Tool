using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beatmap_Help_Tool.Models
{
    public class RichStringModel
    {
        public string Text { get; internal set; }

        public Color Color { get; internal set; } 

        public RichStringModel(string text) : this(text, Color.Black)
        {

        }

        public RichStringModel(string text, int color) : this(text, Color.FromArgb(color))
        {

        }

        public RichStringModel(string text, Color color)
        {
            Text = text;
            Color = color;
        }
    }
}
