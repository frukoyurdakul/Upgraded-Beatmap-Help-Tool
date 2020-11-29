using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beatmap_Help_Tool.Forms
{
    public partial class InconsistencyResultForm : Form
    {
        public InconsistencyResultForm()
        {
            InitializeComponent();
        }

        public InconsistencyResultForm(string text)
        {
            InitializeComponent();
            webBrowser.DocumentText = text;
        }
    }
}
