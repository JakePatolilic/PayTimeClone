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
    public partial class EmployeeEntry : UserControl
    {
        public Employee employee;
        private PayRollForm parentForm;
        public event EventHandler<Employee> EmployeeDataUpdated;
        public event EventHandler EmpUpdate;
        public event EventHandler EmployeeDeleted;
        public bool flag;
        public string EmployeeName
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public TimeOnly TimeIn
        {
            get { return TimeOnly.FromDateTime(dateTimePicker1.Value); }
            set { dateTimePicker1.Value = DateTime.Today.Add(value.ToTimeSpan()); }
        }

        public TimeOnly TimeOut
        {
            get { return TimeOnly.FromDateTime(dateTimePicker2.Value); }
            set { dateTimePicker2.Value = DateTime.Today.Add(value.ToTimeSpan()); }
        }

        public EmployeeEntry()
        {
            InitializeComponent();
            employee = new Employee("", "", WorkerType.DockWorker);
            parentForm = new PayRollForm();
            flag = false;

            textBox1.TextChanged += TextBox1_Convert;
            dateTimePicker1.ValueChanged += DateTimePicker1_ValueChanged;
            dateTimePicker2.ValueChanged += DateTimePicker2_ValueChanged;
        }

        public void SetEmployeeData(Employee emp)
        {
            this.employee = emp;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (employee != null)
            {
                textBox1.Text = employee.Name;
                dateTimePicker1.Value = DateTime.Today.Add(employee.TimeIn.ToTimeSpan());
                dateTimePicker2.Value = DateTime.Today.Add(employee.TimeOut.ToTimeSpan());
            }
            else
            {
                textBox1.Text = string.Empty;
                dateTimePicker1.Value = DateTime.Today;
                dateTimePicker2.Value = DateTime.Today;
            }
        }

        private void TextBox1_Convert(object? sender, EventArgs e)
        {
            if (textBox1.Text != employee.Name)
            {
                this.flag = true;
            }
            if (parentForm != null && parentForm.pay != null && employee != null)
            {
                employee.Name = textBox1.Text;
                EmployeeDataUpdated?.Invoke(this, employee);
            }
        }

        private void DateTimePicker1_ValueChanged(object? sender, EventArgs e)
        {
            if (TimeOnly.FromDateTime(dateTimePicker1.Value) != employee.TimeIn)
            {
                this.flag = true;
            }
            if (employee != null)
            {
                employee.TimeIn = TimeIn;
            }
        }

        private void DateTimePicker2_ValueChanged(object? sender, EventArgs e)
        {
            if (TimeOnly.FromDateTime(dateTimePicker2.Value) != employee.TimeOut)
            {
                this.flag = true;
            }
            if (employee != null)
            {
                employee.TimeOut = TimeOut;
            }
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            EmployeeDeleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
