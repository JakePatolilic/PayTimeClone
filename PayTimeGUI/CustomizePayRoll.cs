using PayTime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace PayTimeGUI
{
    public partial class CustomizePayRoll : Form
    {
        public string TextName;
        public double TextPercent;
        Factor factor;
        public List<Category> Categories;
        public PayRoll payroll;
        private PayRollInfo payRollInfo;
        public CustomizePayRoll()
        {
            TextName = string.Empty;
            TextPercent = 0;
            InitializeComponent();
            payRollInfo = new PayRollInfo(this);
            payRollInfo.ReturnButtonClicked += Return_Clicked;
            Controls.Add(payRollInfo);
            payRollInfo.Visible = false;

            textBox1.TextChanged += TextBox1_Convert;
            textBox2.KeyPress += TextBox2_Convert;
            checkBox1.CheckedChanged += CheckBox1_CheckedChanged;
            checkBox2.CheckedChanged += CheckBox2_CheckedChanged;

            Categories = new List<Category>();
            payroll = new PayRoll("", 0, DateTime.Now);
        }

        private void Entry_ButtonClicked(object sender, EventArgs e)
        {
            CategoryEntry entry = (CategoryEntry)sender;
            flowLayoutPanel1.Controls.Remove(entry);
            entry.Dispose();
        }

        private void Return_Clicked(object? sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.Visible = true;
            }

            payRollInfo.Visible = false;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            foreach (Control control in Controls)
            {
                control.Visible = false;
            }

            payRollInfo.Visible = true;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            foreach (Control control in Controls)
            {
                control.Visible = Visible;
            }
        }

        private void TextBox1_Convert(object? sender, EventArgs e)
        {
            TextName = textBox1.Text;
        }

        private void TextBox2_Convert(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '.' && textBox2.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void CheckBox1_CheckedChanged(object? sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                factor = Factor.Deduction;
            }
        }

        private void CheckBox2_CheckedChanged(object? sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                factor = Factor.Bonus;
                checkBox1.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                errorProvider1.SetError(textBox1, "Category Name is a required field.");
            }
            else
            {
                errorProvider1.SetError(textBox1, string.Empty);
            }

            if (string.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                errorProvider2.SetError(textBox2, "Amount is a required field.");
            }
            else
            {
                errorProvider2.SetError(textBox2, string.Empty);
            }

            if (checkBox1.Checked == false && checkBox2.Checked == false)
            {
                errorProvider3.SetError(checkBox2, "Choose at least 1 type of category.");
            }
            else
            {
                errorProvider3.SetError(checkBox2, string.Empty);
            }
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                return;
            }
            else if (!checkBox1.Checked && !checkBox2.Checked)
                return;
            else
                nextForm();
        }

        private void nextForm()
        {
            if (double.TryParse(textBox2.Text, out double result))
            {
                TextPercent = result;
            }

            Category category = new Category(TextName, factor, TextPercent);
            textBox1.Clear();
            textBox2.Clear();
            checkBox1.Checked = false;
            checkBox2.Checked = false;

            CategoryEntry entry = new CategoryEntry();
            var r = checker(category);
            if (r == false)
                return;
            //Categories.Add(category);
            entry.SetCategoryData(category);

            entry.Location = new Point(0, flowLayoutPanel1.Controls.Count * 50);
            flowLayoutPanel1.Controls.Add(entry);
            entry.deleteButtonClicked += Entry_ButtonClicked;
        }

        public void setPayRollData(PayRoll payRoll)
        {
            this.payroll = payRoll;
        }

        private bool checker(Category category)
        {
            double totalPercent = 0;
            foreach (Category c in Categories)
            {
                if (c.Factor == Factor.Deduction)
                {
                    totalPercent += c.Percent;
                }
                else if (c.Factor == Factor.Bonus)
                {
                    totalPercent -= c.Percent;
                }
            }
            if (category.Factor == Factor.Deduction)
            {
                totalPercent += category.Percent;
            }
            else if (category.Factor == Factor.Bonus)
            {
                totalPercent -= category.Percent;
            }
            if (totalPercent > 100)
                return false;
            else
            {
                this.Categories.Add(category);
                return true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Category c in Categories)
            {
                payroll.AddCategory(c);
            }
            PayRollEntry p = new PayRollEntry();
            PayRollForm pay = new PayRollForm();

            p.SetCategoryData(payroll);

            p.Location = new Point(0, flowLayoutPanel1.Controls.Count * 50);
            pay.flowLayoutPanel1.Controls.Add(p);

            DataBase.AddPayRoll(payroll);
            DataBase.SavePayRoll();
            this.Hide();
            PayRollForm form = new PayRollForm();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PayRollForm form = new PayRollForm();
            form.Show();
            this.Close();
        }
    }
}
