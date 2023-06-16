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

namespace PayTimeGUI
{
    public partial class CategoryInfo : UserControl
    {
        public Category category;
        public CategoryInfo()
        {
            category = new Category("", 0, 0);
            InitializeComponent();
        }

        public void SetCategoryData(Category category)
        {
            this.category = category;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (category != null)
            {
                label1.Text = category.CategoryName;
                label2.Text = category.Percent.ToString() + "%";
            }
            else
            {
                label1.Text = string.Empty;
                label2.Text = string.Empty;
            }
        }
    }
}
