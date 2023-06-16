using PayTime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayTimeGUI
{
    public partial class DataSource : Form
    {
        public PayRoll payroll;
        public DataSource()
        {
            payroll = new PayRoll("", 0, DateTime.Now);
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.KeyPreview = true;

            this.KeyDown += Form1_KeyDown;
        }

        public void getData(PayRoll p)
        {
            this.payroll = p;
            this.payroll.CalculateSalary();
            showData();
            size();
        }

        public void showData()
        {
            foreach (Category c in payroll.Categories)
            {
                DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = c.CategoryName;
                newColumn.Name = c.CategoryName;
                dataGridView1.Columns.Add(newColumn);
            }
            foreach (Employee emp in payroll.Employees)
            {

                List<string> categoryMoneyValues = emp.CategoryMoney.Select(value => value.ToString("F2")).ToList();

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = emp.Name;
                row.Cells[1].Value = emp.TotalTime;
                row.Cells[2].Value = emp.Salary;

                for (int i = 0; i < categoryMoneyValues.Count; i++)
                {
                    row.Cells[i + 3].Value = categoryMoneyValues[i];
                }

                dataGridView1.Rows.Add(row);
            }
        }

        public void size()
        {
            int numberOfCategories = payroll.Categories.Count;
            int numberOfEmployees = payroll.Employees.Count;
            dataGridView1.Width = numberOfCategories * 106;
            dataGridView1.Height = 10 + numberOfEmployees * 27;
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                Paystub stub = new Paystub();
                stub.ReceiveData(payroll);
                stub.Show();
                e.Handled = true;
                this.Close();
            }
        }
    }
}
