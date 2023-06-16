using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTime
{
    /// <summary>
    /// Class that accepts the data of the Employees.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Name of the employee.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Identifiction of the employee.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The salary of the employee.
        /// Field that accepts the result of the computed salary in the PayRoll class.
        /// </summary>
        public double Salary { get; set; }

        /// <summary>
        /// The type of worker.
        /// Affects how much the employee is paid.
        /// </summary>
        public WorkerType WorkerType { get; set; }

        /// <summary>
        /// The time when the worker starts working.
        /// </summary>
        public TimeOnly TimeIn { get; set; }

        /// <summary>
        /// The time when the worker stops working
        /// </summary>
        public TimeOnly TimeOut { get; set; }

        /// <summary>
        /// gets the total time the Employee is working.
        /// </summary>
        public double TotalTime { get; set; }

        public List<double> CategoryMoney { get; set; }

        /// <summary>
        /// Constructor of the Employee class.
        /// </summary>
        public Employee(string Name, string Id, WorkerType WorkerType)
        {
            this.Name = Name;
            this.Id = Id;
            this.WorkerType = WorkerType;
            CategoryMoney = new List<double>();
        }
    }
}
