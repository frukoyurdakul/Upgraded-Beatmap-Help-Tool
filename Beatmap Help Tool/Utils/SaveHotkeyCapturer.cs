using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Beatmap_Help_Tool.Utils
{
    public class SaveHotkeyCapturer
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int WM_HOTKEY = 0x0312;

        private ISaveHotkeyListener Listener;
        private Form Form;

        private const int id = 1;

        public SaveHotkeyCapturer(Form form, ISaveHotkeyListener listener)
        {
            Listener = listener;
            Form = form;
            form.Disposed += Form_Disposed;

            if (!RegisterHotKey(form.Handle, id, (int)ModifierKeys.Control, (int)Keys.S))
                throw new ArgumentException("Could not register hotkey.");
        }

        private void Form_Disposed(object sender, EventArgs e)
        {
            UnregisterHotKey(Form.Handle, id);
        }

        public void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                // Invoke the event to notify the parent.
                Listener.onSaveHotkey();
            }
        }

        public interface ISaveHotkeyListener
        {
            void onSaveHotkey();
        }
    }
}
