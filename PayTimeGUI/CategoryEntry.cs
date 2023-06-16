using Krypton.Toolkit;
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
    public partial class CategoryEntry : UserControl
    {
        public Category category;
        public event EventHandler deleteButtonClicked;
        public CategoryEntry()
        {
            InitializeComponent();
            category = new Category("", 0, 0);
            button1.Click += button1_Click;
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
                label2.Text = category.Percent.ToString();
            }
            else
            {
                label1.Text = string.Empty;
                label2.Text = string.Empty;
            }
        }

        public void delButtonClicked()
        {
            deleteButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            delButtonClicked();
        }
    }
}
