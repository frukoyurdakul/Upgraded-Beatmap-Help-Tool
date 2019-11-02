using System.Windows.Forms;

namespace Beatmap_Help_Tool.Utils
{
    public static class MessageBoxUtils
    {
        public static DialogResult show(string text)
        {
            return MessageBox.Show(text, "Status", MessageBoxButtons.OK);
        }

        public static DialogResult showWarning(string text)
        {
            return MessageBox.Show(text, "Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult showError(string text)
        {
            return MessageBox.Show(text, "Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult showQuestionYesNo(string text)
        {
            return MessageBox.Show(text, "Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult showQuestionYesNoCancel(string text)
        {
            return MessageBox.Show(text, "Status", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        public static DialogResult show(string text, string title)
        {
            return MessageBox.Show(text, title, MessageBoxButtons.OK);
        }

        public static DialogResult showWarning(string text, string title)
        {
            return MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult showError(string text, string title)
        {
            return MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult showQuestionYesNo(string text, string title)
        {
            return MessageBox.Show(text, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult showQuestionYesNoCancel(string text, string title)
        {
            return MessageBox.Show(text, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }
    }
}
