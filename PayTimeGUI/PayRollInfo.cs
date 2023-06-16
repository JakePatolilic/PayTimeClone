using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PayTime;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static PayTimeGUI.PayRollInfo;

namespace PayTimeGUI
{
    public partial class PayRollInfo : UserControl
    {
        public string Name;
        public double Amount;
        public DateTime Date;
        CustomizePayRoll customForm;
        public event EventHandler ReturnButtonClicked;

        public PayRollInfo(CustomizePayRoll parent)
        {
            InitializeComponent();
            textBox1.KeyPress += textBox1_KeyPress;
            textBox1.TextChanged += TextBox1_TextChanged;
            textBox2.KeyPress += TextBox2_TextChanged;
            customForm = parent;
            panel1.Paint += Panel1_Modify;
            dateTimePicker1.ValueChanged += date;
        }

        private void date(object? sender, EventArgs e)
        {
            this.Date = dateTimePicker1.Value;
        }

        private void TextBox1_TextChanged(object? sender, EventArgs e)
        {
            this.Name = textBox1.Text;
        }
        private void textBox1_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (textBox1.Text.Length >= 15 && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void TextBox2_TextChanged(object? sender, KeyPressEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                errorProvider1.SetError(textBox1, "Payroll Name is a required field.");
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

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                return;
            }

            if (double.TryParse(textBox2.Text, out double result))
            {
                Amount = result;
            }
            PayRoll payRoll = new PayRoll(Name, Amount, Date);
            customForm.setPayRollData(payRoll);
            ReturnButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void Panel1_Modify(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            int cornerRadius = 20;

            using (GraphicsPath path = new GraphicsPath())
            {
                RectangleF rect = new RectangleF(0, 0, panel.Width, panel.Height);

                Color borderColor = Color.DarkBlue; 

                path.AddArc(rect.X, rect.Y, cornerRadius, cornerRadius, 180, 90); // Top-left corner
                path.AddArc(rect.Width - cornerRadius, rect.Y, cornerRadius, cornerRadius, 270, 90); // Top-right corner
                path.AddArc(rect.Width - cornerRadius, rect.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90); // Bottom-right corner
                path.AddArc(rect.X, rect.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                path.CloseAllFigures();

                panel.Region = new Region(path);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PayRollForm p = new PayRollForm();
            p.Show();
            this.Hide();
            customForm.Close();
        }
    }
}
