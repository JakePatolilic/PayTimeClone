namespace PayTimeGUI
{
    partial class DataSource
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            Employee = new DataGridViewTextBoxColumn();
            Hours = new DataGridViewTextBoxColumn();
            Salary = new DataGridViewTextBoxColumn();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Employee, Hours, Salary });
            dataGridView1.Location = new Point(21, 208);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(346, 48);
            dataGridView1.TabIndex = 0;
            // 
            // Employee
            // 
            Employee.HeaderText = "Employee";
            Employee.Name = "Employee";
            Employee.ReadOnly = true;
            // 
            // Hours
            // 
            Hours.HeaderText = "Hours";
            Hours.Name = "Hours";
            Hours.ReadOnly = true;
            // 
            // Salary
            // 
            Salary.HeaderText = "Salary";
            Salary.Name = "Salary";
            Salary.ReadOnly = true;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(927, 51);
            label7.Name = "label7";
            label7.Size = new Size(78, 32);
            label7.TabIndex = 23;
            label7.Text = "label7";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(858, 52);
            label6.Name = "label6";
            label6.Size = new Size(104, 33);
            label6.TabIndex = 22;
            label6.Text = "Date:";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label5.Location = new Point(463, 9);
            label5.Name = "label5";
            label5.Size = new Size(181, 55);
            label5.TabIndex = 21;
            label5.Text = "Pay Stub";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(10, 85);
            label4.Name = "label4";
            label4.Size = new Size(114, 32);
            label4.TabIndex = 20;
            label4.Text = "Amount:";
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(10, 54);
            label3.Name = "label3";
            label3.Size = new Size(100, 31);
            label3.TabIndex = 19;
            label3.Text = "Payroll:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(141, 85);
            label2.Name = "label2";
            label2.Size = new Size(78, 32);
            label2.TabIndex = 18;
            label2.Text = "label2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(141, 53);
            label1.Name = "label1";
            label1.Size = new Size(89, 32);
            label1.TabIndex = 17;
            label1.Text = "PayRoll";
            // 
            // DataSource
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new Size(1084, 811);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Name = "DataSource";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DataSource";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Employee;
        private DataGridViewTextBoxColumn Hours;
        private DataGridViewTextBoxColumn Salary;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}