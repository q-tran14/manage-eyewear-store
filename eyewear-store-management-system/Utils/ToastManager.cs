using System;
using System.Windows.Forms;

namespace eyewear_store_management_system.Utils
{
    public static class ToastManager
    {
        public static void ShowToastNotification(string title, string message, string type, Form parentForm)
        {
            if (parentForm.InvokeRequired)
            {
                parentForm.Invoke(new Action(() => ShowToastNotification(title, message, type, parentForm)));
                return;
            }

            ToastForm toast = new ToastForm(title, message, type, parentForm);
            toast.Show();
        }
    }
}
