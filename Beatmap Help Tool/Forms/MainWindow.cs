using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beatmap_Help_Tool
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        #region Mouse focus functions
        private void processSender(object sender, EventArgs e)
        {
            if (sender is Control)
            {
                (sender as Control).Focus();
            }
        }
        #endregion
    }
}
