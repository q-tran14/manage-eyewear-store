using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace eyewear_store_management_system.CustomComponents
{
    public class CustomToggleButton : PictureBox
    {
        // Fields
        private bool isOn = false;
        private Image onImage;
        private Image offImage;

        // Properties
        [Category("Custom Toggle Button")]
        public bool IsOn
        {
            get { return isOn; }
            set
            {
                isOn = value;
                UpdateImage();
                OnToggleChanged(EventArgs.Empty);
            }
        }

        [Category("Custom Toggle Button")]
        public Image OnImage
        {
            get { return onImage; }
            set
            {
                onImage = value;
                if (isOn) this.Image = onImage;
            }
        }

        [Category("Custom Toggle Button")]
        public Image OffImage
        {
            get { return offImage; }
            set
            {
                offImage = value;
                if (!isOn) this.Image = offImage;
            }
        }

        // Events
        public event EventHandler ToggleChanged;

        // Constructor
        public CustomToggleButton()
        {
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Cursor = Cursors.Hand;
        }

        // Methods
        public void Toggle()
        {
            IsOn = !IsOn;
        }

        public void SetStatus(bool status)
        {
            IsOn = status;
        }

        private void UpdateImage()
        {
            this.Image = isOn ? onImage : offImage;
        }

        protected virtual void OnToggleChanged(EventArgs e)
        {
            ToggleChanged?.Invoke(this, e);
        }
    }
}