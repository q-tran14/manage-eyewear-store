using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eyewear_store_management_system.Utils
{
    public partial class BaseForm : Form
    {
        public MainForm parentForm;
        public BaseForm(MainForm parent)
        {
            this.parentForm = parent;
            InitializeComponent();
        }
    }
}
