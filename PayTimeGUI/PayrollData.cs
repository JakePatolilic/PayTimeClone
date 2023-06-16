using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PayTime;

namespace PayTimeGUI
{
    public partial class PayrollData : Form
    {
        public PayRoll payroll;
        public PayrollData()
        {
            payroll = new PayRoll("", 0, DateTime.Now);
            InitializeComponent();
        }

        public void getData(PayRoll p)
        {
            this.payroll = p;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (payroll != null)
            {
                label4.Text = payroll.PayRollName;
                label5.Text = payroll.TotalPay.ToString();
                label6.Text = payroll.PayDate.ToString("yyyy-MM-dd");
            }
            else
            {
                label4.Text = string.Empty;
                label5.Text = string.Empty;
                label6.Text = string.Empty;
            }
            showCategories();
        }

        public void showCategories()
        {
            foreach (Category c in payroll.Categories)
            {
                CategoryInfo entry = new CategoryInfo();
                entry.SetCategoryData(c);
                flowLayoutPanel1.Controls.Add(entry);
            }
        }
    }
}
