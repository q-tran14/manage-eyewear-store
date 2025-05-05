using eyewear_store_management_system.Utils;
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
        public bool isBlack = true;
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
                if (isOn ) this.Image = onImage;
            }
        }

        [Category("Custom Toggle Button")]
        public Image OffImage
        {
            get { return offImage; }
            set
            {
                offImage = value;
                if (!isOn ) this.Image = offImage;
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
            Image r = isOn ? onImage : offImage;

            if (r == null)
            {
                this.Image = r; // hoặc gán một ảnh mặc định nếu muốn
                return;
            }

            Bitmap fallbackBitmap;
            try
            {
                fallbackBitmap = new Bitmap(r);
            }
            catch
            {
                this.Image = r;
                return;
            }

            if (this.Enabled)
                r = UtilityImage.AdjustImage(fallbackBitmap, exposure: 0f);
            else
                r = UtilityImage.AdjustImage(fallbackBitmap, exposure: 0.5f);

            this.Image = r;
        }

        protected virtual void OnToggleChanged(EventArgs e)
        {
            ToggleChanged?.Invoke(this, e);
        }
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            UpdateImage();
        }

        public void DarkImage()
        {
            if (this.Image is Bitmap bitmap)
            {
                this.Image = UtilityImage.AdjustImage(bitmap, exposure: 0f);
            }
        }

        public void GrayImage()
        {
            if (this.Image is Bitmap bitmap)
            {
                this.Image = UtilityImage.AdjustImage(bitmap, exposure: 0.5f);
            }
        }
    }
}