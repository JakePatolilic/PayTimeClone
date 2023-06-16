using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTime
{
    /// <summary>
    /// An object that accepts data of the criteria that will affect the salaries of the employees.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// A name to identify the Category.
        /// </summary>
        public string? CategoryName { get; set; }

        /// <summary>
        /// The type of category.
        /// Can be taxes, or bonus and etc.
        /// </summary>
        public Factor Factor { get; set; }

        /// <summary>
        /// Percent how much this category will affect that salary.
        /// Deduction if taxes, and addition if bonus and such.
        /// </summary>
        public double Percent { get; set; }

        /// <summary>
        /// Constructor for Category Class.
        /// </summary>
        public Category(string CategoryName, Factor Factor, double Percent)
        {
            this.CategoryName = CategoryName;
            this.Factor = Factor;
            this.Percent = Percent;
        }
    }
}
