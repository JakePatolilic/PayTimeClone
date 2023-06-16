using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PayTime;

namespace PayTimeGUI
{
    public partial class CategoryList : UserControl
    {
        public Category category;
        public string name;

        public CategoryList(string name)
        {
            InitializeComponent();
            this.name = name;
            label1.Text = name;
            category = new Category("Name", 0, 0);
        }

        public CategoryList()
        {
            InitializeComponent();
            this.name = "";
            category = new Category("Name", 0, 0);
        }

        public void getCategory(Category c)
        {
            this.category = c;
            label1.Text = category.CategoryName;
        }
    }
}
