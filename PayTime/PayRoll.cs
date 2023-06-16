using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PayTime
{
    /// <summary>
    /// A class that accepts the data and customization from the user.
    /// </summary>
    public class PayRoll
    {
        /// <summary>
        /// A simple name to identify the payroll.
        /// </summary>
        public string PayRollName { get; set; }

        /// <summary>
        /// The date payroll is created
        /// </summary>
        public DateTime PayDate { get; set; }

        /// <summary>
        /// A collection of the employees that is working under the company.
        /// </summary>
        public List<Employee> Employees { get; set; }

        /// <summary>
        /// The criteria on how the emplyees should be paid
        /// Example: Taxes, Pag-IBIG, pay/hr.
        /// </summary>
        public List<Category> Categories { get; set; }

        /// <summary>
        /// The payment that the whole work.
        /// </summary>
        public double TotalPay { get; set; }

        /// <summary>
        /// Constructor for PayRoll Class.
        /// </summary>
        /// <param name="payRollName"></param>
        public PayRoll(string payRollName, double totalPay, DateTime date)
        {
            this.PayRollName = payRollName;
            this.Employees = new List<Employee>();
            this.Categories = new List<Category>();
            this.TotalPay = totalPay;
            PayDate = date;
        }

        /// <summary>
        /// Adds an object of type Category
        /// </summary>
        /// <param name="category"></param>
        public bool AddCategory(Category category)
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

        /// <summary>
        /// Adds an object of type Employee
        /// </summary>
        /// <param name="employee"></param>
        public void AddEmployee(Employee employee)
        {
            this.Employees.Add(employee);
        }

        /// <summary>
        /// Removes an object of type Category
        /// </summary>
        /// <param name="category"></param>
        public void RemoveCategory(Category category)
        {
            this.Categories.Remove(category);
        }

        /// <summary>
        /// Removes an object of type Employee
        /// </summary>
        /// <param name="category"></param>
        public void RemoveEmployee(Employee employee)
        {
            this.Employees.Remove(employee);
        }

        /// <summary>
        /// Calculate the Salary base on the categories that were added.
        /// Calculate total hrs of the employees, then divide it to totalPay, before calculating it with the categories.
        /// </summary>
        public void CalculateSalary()
        {
            
            double totalHrs = 0;
            double tempPay;
            double tempTotalPay = this.TotalPay;
            foreach (Employee employee in Employees)
            {
                TimeSpan duration = employee.TimeOut - employee.TimeIn;
                totalHrs += duration.TotalMinutes / 60;    
            }
            tempTotalPay /= totalHrs;

            foreach(Employee employee in Employees)
            {
                employee.CategoryMoney.Clear();
                TimeSpan duration = employee.TimeOut - employee.TimeIn;
                totalHrs = duration.TotalMinutes / 60;
                employee.TotalTime = totalHrs;
                employee.Salary = tempTotalPay * totalHrs;
                double tempSalary = 0;
                foreach (Category category in Categories)
                {
                    tempPay = employee.Salary * (category.Percent / 100);
                    if (category.Factor == Factor.Deduction)
                    {
                        tempSalary -= tempPay;
                        //employee.Salary -= tempPay;
                        employee.CategoryMoney.Add(tempPay);
                    }
                    else if(category.Factor == Factor.Bonus)
                    {
                        tempSalary += tempPay;
                        employee.CategoryMoney.Add(tempPay);
                        //employee.Salary += tempPay;
                    }
                }
                employee.Salary += tempSalary;
            }
        }

        /*public void EmployeeCategoryMoney()
        {
            foreach(Category c in Categories)
            {
                foreach(Employee emp in Employees)
                {
                    emp.Categories.Add(c);
                }
            }
        }*/

        public override string ToString()
        {
            string result = this.PayRollName + "\n";
            foreach(Employee emp in this.Employees)
            {
                result += "-" + emp.Name + "= " + emp.Salary + "total work time: " + emp.TotalTime + "\n";
            }
            return result;
        }
    }
}
