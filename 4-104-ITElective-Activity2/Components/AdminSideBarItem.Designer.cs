namespace _4_104_ITElective_Activity2.Components
{
    partial class AdminSideBarItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            BarDesc = new Label();
            BarTitle = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(BarDesc, 0, 1);
            tableLayoutPanel1.Controls.Add(BarTitle, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(233, 86);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // BarDesc
            // 
            BarDesc.AutoSize = true;
            BarDesc.Dock = DockStyle.Fill;
            BarDesc.Font = new Font("Courier New", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BarDesc.ForeColor = Color.White;
            BarDesc.Location = new Point(3, 43);
            BarDesc.Name = "BarDesc";
            BarDesc.Size = new Size(227, 43);
            BarDesc.TabIndex = 2;
            BarDesc.Text = "₱ 180.00";
            BarDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // BarTitle
            // 
            BarTitle.AutoSize = true;
            BarTitle.Dock = DockStyle.Fill;
            BarTitle.Font = new Font("Courier New", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BarTitle.ForeColor = Color.White;
            BarTitle.Location = new Point(3, 0);
            BarTitle.Name = "BarTitle";
            BarTitle.Size = new Size(227, 43);
            BarTitle.TabIndex = 1;
            BarTitle.Text = "Creamy Pure Matcha Latte";
            BarTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AdminSideBarItem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 63, 47);
            Controls.Add(tableLayoutPanel1);
            Name = "AdminSideBarItem";
            Size = new Size(233, 86);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label BarTitle;
        private Label BarDesc;
    }
}
