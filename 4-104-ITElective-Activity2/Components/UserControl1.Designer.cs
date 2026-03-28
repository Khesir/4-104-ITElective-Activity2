namespace _4_104_ITElective_Activity2.Components
{
    partial class UserControl1
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
            productName = new Label();
            productPrice = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(productPrice, 0, 1);
            tableLayoutPanel1.Controls.Add(productName, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(233, 86);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // productName
            // 
            productName.AutoSize = true;
            productName.Dock = DockStyle.Fill;
            productName.Font = new Font("Courier New", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            productName.ForeColor = Color.White;
            productName.Location = new Point(3, 0);
            productName.Name = "productName";
            productName.Size = new Size(227, 43);
            productName.TabIndex = 1;
            productName.Text = "Creamy Pure Matcha Latte";
            productName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // productPrice
            // 
            productPrice.AutoSize = true;
            productPrice.Dock = DockStyle.Fill;
            productPrice.Font = new Font("Courier New", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            productPrice.ForeColor = Color.White;
            productPrice.Location = new Point(3, 43);
            productPrice.Name = "productPrice";
            productPrice.Size = new Size(227, 43);
            productPrice.TabIndex = 2;
            productPrice.Text = "₱ 180.00";
            productPrice.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // UserControl1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 63, 47);
            Controls.Add(tableLayoutPanel1);
            Name = "UserControl1";
            Size = new Size(233, 86);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label productName;
        private Label productPrice;
    }
}
