using System;
using System.Drawing;
using System.Windows.Forms;

namespace Beatmap_Help_Tool.Views
{
    public class PlaceHolderTextBox : TextBox
    {
        bool isPlaceHolder = true;
        string _placeHolderText;
        public string PlaceHolderText
        {
            get { return _placeHolderText; }
            set
            {
                _placeHolderText = value;
                setPlaceholder();
            }
        }

        public new string Text
        {
            get => isPlaceHolder ? string.Empty : base.Text;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    removePlaceHolder();
                base.Text = value;
                if (string.IsNullOrEmpty(value))
                    setPlaceholder();
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (DesignMode)
                base.Text = "";
            else
                setPlaceholder();
        }

        //when the control loses focus, the placeholder is shown
        private void setPlaceholder()
        {
            if (IsHandleCreated && string.IsNullOrEmpty(base.Text))
            {
                base.Text = PlaceHolderText;
                ForeColor = Color.FromArgb(80, 80, 80);
                Font = new Font(Font, FontStyle.Regular);
                isPlaceHolder = true;
            }
        }

        //when the control is focused, the placeholder is removed
        private void removePlaceHolder()
        {
            if (isPlaceHolder)
            {
                base.Text = "";
                ForeColor = SystemColors.WindowText;
                Font = new Font(Font, FontStyle.Regular);
                isPlaceHolder = false;
            }
        }

        private Form findFormParent()
        {
            Control control = this;
            while (!(control.Parent is Form))
                control = control.Parent;
            return (Form)control;
        }

        public PlaceHolderTextBox()
        {
            GotFocus += removePlaceHolder;
            LostFocus += setPlaceholder;
        }

        private void setPlaceholder(object sender, EventArgs e)
        {
            setPlaceholder();
        }

        private void removePlaceHolder(object sender, EventArgs e)
        {
            removePlaceHolder();
        }
    }
}
