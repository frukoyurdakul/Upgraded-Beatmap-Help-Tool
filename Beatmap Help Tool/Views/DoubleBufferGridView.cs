using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beatmap_Help_Tool.Views
{
    public class DoubleBufferGridView : DataGridView
    {
        public DoubleBufferGridView()
        {
            DoubleBuffered = true;
        }
    }
}
