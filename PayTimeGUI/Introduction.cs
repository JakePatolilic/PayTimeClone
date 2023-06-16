using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayTimeGUI
{
    public partial class Introduction : Form
    {
        private System.Windows.Forms.Timer timer;
        public Introduction()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            timer.Stop();
            PayRollForm form = new PayRollForm();
            form.Show();
            this.Hide();
        }
    }
}
