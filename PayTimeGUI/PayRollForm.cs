using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PayTime;

namespace PayTimeGUI
{
    public partial class PayRollForm : Form
    {
        public List<PayRoll> payRolls;
        public List<PayRollEntry> payrollEntry;
        public List<EmployeeEntry> employeeEntry;
        public PayRoll pay;
        public List<Employee> employees;
        public int page;
        public bool flag;
        public bool clicked;
        public int num;
        private bool isDragging;
        private Point dragStartPosition;
        private PayRollEntry selectedEntry;

        public PayRollForm()
        {
            pay = new PayRoll("", 0, DateTime.Now);
            payRolls = new List<PayRoll>();
            employeeEntry = new List<EmployeeEntry>();
            employees = new List<Employee>();
            page = 0;
            flag = false;
            clicked = false;
            num = 0;
            payrollEntry = new List<PayRollEntry>();
            InitializeComponent();
            loadPayRoll();
            showPayroll();
            label1.Text = (page + 1).ToString();
            textBox1.TextChanged += TextBoxSearch_TextChanged;
            textBox1.BorderStyle = BorderStyle.None;
            buttonDesign();
            panel2.MouseDown += panel2_MouseDown;
            panel2.MouseMove += panel2_MouseMove;
            panel2.MouseUp += panel2_MouseUp;
        }

        private void panel2_MouseDown(object? sender, MouseEventArgs e)
        {
            isDragging = true;
            dragStartPosition = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove(object? sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = PointToScreen(e.Location);
                Location = new Point(currentPosition.X - dragStartPosition.X, currentPosition.Y - dragStartPosition.Y);
            }
        }

        private void panel2_MouseUp(object? sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void buttonDesign()
        {
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 0;
            button8.FlatStyle = FlatStyle.Flat;
            button8.FlatAppearance.BorderSize = 0;

            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
        }

        public void ReceivePayRollData(PayRoll payroll)
        {
            this.pay = payroll;
        }

        private void loadEmployees()
        {
            flowLayoutPanel2.Controls.Clear();
            foreach (Employee e in pay.Employees)
            {
                EmployeeEntry entry = new EmployeeEntry();
                entry.SetEmployeeData(e);
                employeeEntry.Add(entry);
                flowLayoutPanel2.Controls.Add(entry);
                entry.EmployeeDeleted += empDeleteButtonClicked;
            }
        }

        private void loadPayRoll()
        {
            DataBase.LoadPayRoll();
            foreach (PayRoll p in DataBase.PayRolls)
            {
                payRolls.Add(p);
            }
            payRolls = payRolls.AsEnumerable().Reverse().ToList();
            foreach (PayRoll p in payRolls)
            {
                PayRollEntry entry = new PayRollEntry();
                entry.SetCategoryData(p);
                entry.EntryClicked += userControl_Click;
                payrollEntry.Add(entry);
                entry.DeleteButtonClicked += delButtonClicked;
            }
        }

        private void showPayroll()
        {
            int startIndex = page * 5;

            var entriesToDisplay = payrollEntry.Skip(startIndex).Take(5);

            foreach (var entry in entriesToDisplay)
            {
                flowLayoutPanel1.Controls.Add(entry);
            }
        }

        public void delButtonClicked(object sender, EventArgs e)
        {
            PayRollEntry entry = (PayRollEntry)sender;
            flowLayoutPanel1.Controls.Remove(entry);
            entry.Dispose();
            DataBase.deletePayRoll(entry.payRoll);
        }

        public void empDeleteButtonClicked(object sender, EventArgs e)
        {
            EmployeeEntry entry = (EmployeeEntry)sender;
            flowLayoutPanel2.Controls.Remove(entry);
            entry.Dispose();
            pay.RemoveEmployee(entry.employee);
            flag = true;
        }

        public void userControl_Click(object sender, EventArgs e)
        {
            if (this.flag == true)
            {
                DialogResult result = MessageBox.Show("Do you want to switch payroll without saving?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    clicked = true;
                    PayRollEntry payRollEntry = (PayRollEntry)sender;
                    PayRoll payroll = payRollEntry.payRoll;
                    label5.Text = payroll.PayRollName;
                    selectedEntry = payRollEntry;
                    pay = payroll;
                    loadEmployees();
                    this.flag = false;
                    return;
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
            if (num < 1)
            {
                clicked = true;
                PayRollEntry payRollEntry = (PayRollEntry)sender;
                PayRoll payroll = payRollEntry.payRoll;
                label5.Text = payroll.PayRollName;
                selectedEntry = payRollEntry;
                pay = payroll;
                loadEmployees();
                this.num++;
                return;
            }
            foreach (EmployeeEntry ee in employeeEntry)
            {
                if (ee.flag == true)
                {
                    DialogResult result = MessageBox.Show("Do you want to switch payroll without saving?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        clicked = true;
                        PayRollEntry payRollEntry = (PayRollEntry)sender;
                        PayRoll payroll = payRollEntry.payRoll;
                        label5.Text = payroll.PayRollName;
                        selectedEntry = payRollEntry;
                        pay = payroll;
                        loadEmployees();
                        ee.flag = false;
                        return;
                    }
                    else if (result == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            if (this.flag == false)
            {
                clicked = true;
                PayRollEntry payRollEntry = (PayRollEntry)sender;
                PayRoll payroll = payRollEntry.payRoll;
                label5.Text = payroll.PayRollName;
                selectedEntry = payRollEntry;
                pay = payroll;
                loadEmployees();
                return;
            }
        }

        private void CustomizePayRoll_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CustomizePayRoll customizePayRoll = new CustomizePayRoll();
            customizePayRoll.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (clicked == true)
            {
                Employee emp = new Employee("Name", "Id", WorkerType.DockWorker);
                EmployeeEntry entry = new EmployeeEntry();
                flowLayoutPanel2.Controls.Add(entry);
                pay.Employees.Add(emp);
                entry.SetEmployeeData(emp);
                entry.EmployeeDeleted += empDeleteButtonClicked;
                flag = true;
            }
            return;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool unsavedChanges = false;

            foreach (EmployeeEntry ee in employeeEntry)
            {
                if (ee.flag)
                {
                    unsavedChanges = true;
                    break;
                }
            }

            if (this.flag == true)
            {
                unsavedChanges = true;
            }

            if (unsavedChanges == true)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to leave without saving?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            if (payRolls.Contains(pay))
            {
                Application.Exit();
                return;
            }

            payRolls = payRolls.AsEnumerable().Reverse().ToList();
            for (int i = 0; i < DataBase.PayRolls.Count; i++)
            {
                if (payRolls[i] != DataBase.PayRolls[i])
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to leave without saving?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        Application.Exit();
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            Application.Exit();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            double count = payrollEntry.Count / 5;
            double realCount = Math.Ceiling(count);
            if (page < realCount)
            {
                page++;
                label1.Text = (page + 1).ToString();
            }
            showPayroll();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            if (page != 0)
            {
                page--;
                label1.Text = (page + 1).ToString();
            }

            showPayroll();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataBase.updatePayroll(pay);
            flag = false;
            foreach (EmployeeEntry ee in employeeEntry)
            {
                ee.flag = false;
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Paystub stub = new Paystub();
            stub.ReceiveData(pay);
            stub.Show();
        }

        private void TextBoxSearch_TextChanged(object? sender, EventArgs e)
        {
            string searchString = textBox1.Text.ToLower();
            flowLayoutPanel1.Controls.Clear();

            if (textBox1.Text == "")
            {
                showPayroll();
            }
            else
            {
                foreach (PayRoll p in payRolls)
                {
                    if (p.PayRollName.ToLower().Contains(searchString))
                    {
                        PayRollEntry entry = new PayRollEntry();
                        entry.SetCategoryData(p);
                        entry.EntryClicked += userControl_Click;
                        entry.DeleteButtonClicked += delButtonClicked;
                        flowLayoutPanel1.Controls.Add(entry);
                    }

                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
