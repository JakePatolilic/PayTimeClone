using Microsoft.VisualBasic;
using PayTime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace PayTimeGUI
{
    public partial class PayRollEntry : UserControl
    {
        public event EventHandler DeleteButtonClicked;
        public PayRoll payRoll;
        public event EventHandler EntryClicked;

        public PayRollEntry()
        {
            InitializeComponent();
            button2.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.BorderSize = 0;
            payRoll = new PayRoll("", 0, DateTime.Now);

            label1.DoubleClick += Label1_DoubleClick;
            label1.MouseEnter += Label1_MouseEnter;
            label1.MouseLeave += Label1_MouseLeave;
            label2.MouseEnter += Label1_MouseEnter;
            label2.MouseLeave += Label1_MouseLeave;
            label3.MouseEnter += Label3_MouseEnter;
            label3.MouseLeave += Label3_MouseLeave;
            button2.MouseEnter += button_MouseEnter;
            button2.MouseLeave += button_MouseLeave;
            button2.MouseEnter += Label1_MouseEnter;
            button2.MouseLeave += Label1_MouseLeave;
            button1.MouseEnter += button_MouseEnter;
            button1.MouseLeave += button_MouseLeave;
            button1.MouseEnter += Label1_MouseEnter;
            button1.MouseLeave += Label1_MouseLeave;
            this.MouseEnter += entry_MouseEnter;
            this.MouseLeave += entry_MouseLeave;
            this.Click += Entry_Clicked;
            this.Tag = payRoll;
        }

        public void SetCategoryData(PayRoll payroll)
        {
            this.payRoll = payroll;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (payRoll != null)
            {
                label1.Text = payRoll.PayRollName;
                label3.Text = payRoll.TotalPay.ToString();
            }
            else
            {
                label1.Text = string.Empty;
                label3.Text = string.Empty;
            }
        }

        private void entry_MouseEnter(object? sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(27, 32, 41);
        }

        private void entry_MouseLeave(object? sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(36, 45, 60);
        }

        private void button_MouseEnter(object? sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(27, 32, 41);
            button1.BackColor = Color.FromArgb(27, 32, 41);
        }

        private void button_MouseLeave(object? sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(36, 45, 60);
            button1.BackColor = Color.FromArgb(36, 45, 60);
        }

        private void Label1_DoubleClick(object? sender, EventArgs e)
        {
            string newName = Interaction.InputBox("Enter a new name for the label:", "Change Label Name", label1.Text);
            label1.Text = newName;
        }

        private void Label1_MouseEnter(object? sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(27, 32, 41);
        }

        private void Label1_MouseLeave(object? sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(36, 45, 60);
        }

        private void Label3_MouseEnter(object? sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(27, 32, 41);
        }

        private void Label3_MouseLeave(object? sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(36, 45, 60);
        }

        private void button2_Click(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this PayRoll?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeleteButtonClicked?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Entry_Clicked(object? sender, EventArgs e)
        {
            EntryClicked?.Invoke(this, EventArgs.Empty);
        }

        private void label1_Click(object? sender, EventArgs e)
        {
            this.Entry_Clicked(sender, e);
        }

        private void label2_Click_1(object? sender, EventArgs e)
        {
            this.Entry_Clicked(sender, e);
        }

        private void label3_Click(object? sender, EventArgs e)
        {
            this.Entry_Clicked(sender, e);
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            PayrollData p = new PayrollData();
            p.getData(payRoll);
            p.Show();
        }
    }
}
