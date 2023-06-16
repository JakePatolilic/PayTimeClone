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
using PayTime;

namespace PayTimeGUI
{
    public partial class Paystub : Form
    {
        public PayRoll payroll;
        public List<Category> categories;
        public Paystub()
        {
            InitializeComponent();
            payroll = new PayRoll("Name", 0, DateTime.Now);
            categories = new List<Category>();
            flowLayoutPanel4.WrapContents = false;
            flowLayoutPanel2.WrapContents = false;
            flowLayoutPanel3.WrapContents = false;
            this.KeyPreview = true;

            this.KeyDown += Form1_KeyDown;
        }

        public void ReceiveData(PayRoll pay)
        {
            this.payroll = pay;
            foreach (Category c in payroll.Categories)
            {
                categories.Add(c);
            }
            listCategories();
            listEmployees();
            label1.Text = payroll.PayRollName;
            label2.Text = payroll.TotalPay.ToString();
            label7.Text = payroll.PayDate.ToString("yyyy-MM-dd");
        }

        private void listCategories()
        {
            flowLayoutPanel1.Controls.Clear();

            CategoryList entry = new CategoryList("Employee");
            entry.Location = new Point(0, flowLayoutPanel1.Controls.Count * 1);
            entry.label1.Font = new Font("Arial", 12, FontStyle.Bold);
            flowLayoutPanel1.Controls.Add(entry);

            entry = new CategoryList("Total Hours");
            entry.Location = new Point(0, flowLayoutPanel1.Controls.Count * 1);
            entry.label1.Font = new Font("Arial", 12, FontStyle.Bold);
            flowLayoutPanel1.Controls.Add(entry);

            entry = new CategoryList("Salary");
            entry.Location = new Point(0, flowLayoutPanel1.Controls.Count * 1);
            entry.label1.Font = new Font("Arial", 12, FontStyle.Bold);
            flowLayoutPanel1.Controls.Add(entry);

            foreach (Category c in categories)
            {
                entry = new CategoryList();
                entry.label1.Font = new Font("Arial", 12, FontStyle.Bold);
                entry.getCategory(c);
                entry.Location = new Point(0, flowLayoutPanel1.Controls.Count * 1);
                flowLayoutPanel1.Controls.Add(entry);
            }
        }

        private void listEmployees()
        {
            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel3.Controls.Clear();
            foreach (FlowLayoutPanel f in flowLayoutPanel4.Controls)
            {
                flowLayoutPanel4.Controls.Remove(f);
                f.Dispose();
            }
            flowLayoutPanel4.Controls.Clear();

            payroll.CalculateSalary();
            foreach (Employee e in payroll.Employees)
            {
                CategoryList entry = new CategoryList(e.Name);
                entry.Location = new Point(0, flowLayoutPanel2.Controls.Count * 3);
                flowLayoutPanel2.Controls.Add(entry);
            }
            foreach (Employee e in payroll.Employees)
            {
                CategoryList entry = new CategoryList(e.Name);
                entry.Location = new Point(0, flowLayoutPanel3.Controls.Count * 3);
                flowLayoutPanel3.Controls.Add(entry);
            }

            foreach (Employee e in payroll.Employees)
            {
                FlowLayoutPanel panel = new FlowLayoutPanel();
                panel.FlowDirection = FlowDirection.LeftToRight;

                panel.AutoSize = false;
                panel.Size = new Size(100 * (e.CategoryMoney.Count + 1), 27);

                foreach (double money in e.CategoryMoney)
                {
                    CategoryList entry = new CategoryList(money.ToString("F2"));
                    panel.Controls.Add(entry);
                }

                flowLayoutPanel4.Controls.Add(panel);
            }

            foreach (Employee e in payroll.Employees)
            {
                CategoryList entry = new CategoryList(e.Salary.ToString());
                entry.Location = new Point(0, flowLayoutPanel5.Controls.Count * 3);
                flowLayoutPanel5.Controls.Add(entry);
            }
        }

        private void Paystub_Load(object sender, EventArgs e)
        {
            int numberOfCategories = payroll.Categories.Count;
            int numberOfEmployees = payroll.Employees.Count;
            int lineLength = 330 + (numberOfCategories * 106);
            int verticalLength = (numberOfEmployees * 33);

            panel1.Width = lineLength;
            flowLayoutPanel4.Width = lineLength;
            flowLayoutPanel1.Width = lineLength;
            flowLayoutPanel2.Height = verticalLength;
            flowLayoutPanel3.Height = verticalLength;
            flowLayoutPanel4.Height = verticalLength;
            flowLayoutPanel5.Height = verticalLength;
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                DataSource data = new DataSource();
                data.getData(payroll);
                data.Show();

                e.Handled = true;
                this.Close();
            }
        }
    }
}
