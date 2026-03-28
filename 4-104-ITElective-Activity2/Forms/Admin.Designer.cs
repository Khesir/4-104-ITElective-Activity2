namespace _4_104_ITElective_Activity2.Forms
{
    partial class Admin
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
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            label2 = new Label();
            flowLayoutPanel2 = new FlowLayoutPanel();
            AddBtn = new Button();
            EditBtn = new Button();
            DeleteBtn = new Button();
            tableLayoutPanel4 = new TableLayoutPanel();
            ClockLabel = new Label();
            label3 = new Label();
            logoutIcon = new PictureBox();
            splitContainer1 = new SplitContainer();
            tableLayoutPanel3 = new TableLayoutPanel();
            SideBarTitle = new Label();
            SidebarDataList = new FlowLayoutPanel();
            tabControl1 = new TabControl();
            UserPage = new TabPage();
            productPage = new TabPage();
            TransactionPage = new TabPage();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoutIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1113, 694);
            panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(splitContainer1, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 87.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.Size = new Size(1113, 694);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.2574539F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54.7425461F));
            tableLayoutPanel2.Controls.Add(label2, 0, 0);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel2, 0, 1);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 58.18182F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 41.81818F));
            tableLayoutPanel2.Size = new Size(1107, 75);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Left;
            label2.Font = new Font("Courier New", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(98, 43);
            label2.TabIndex = 1;
            label2.Text = "Starbuko";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(AddBtn);
            flowLayoutPanel2.Controls.Add(EditBtn);
            flowLayoutPanel2.Controls.Add(DeleteBtn);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Location = new Point(3, 46);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(495, 26);
            flowLayoutPanel2.TabIndex = 2;
            // 
            // AddBtn
            // 
            AddBtn.Location = new Point(3, 3);
            AddBtn.Name = "AddBtn";
            AddBtn.Size = new Size(75, 23);
            AddBtn.TabIndex = 0;
            AddBtn.Text = "Add";
            AddBtn.UseVisualStyleBackColor = true;
            // 
            // EditBtn
            // 
            EditBtn.Location = new Point(84, 3);
            EditBtn.Name = "EditBtn";
            EditBtn.Size = new Size(75, 23);
            EditBtn.TabIndex = 1;
            EditBtn.Text = "Edit";
            EditBtn.UseVisualStyleBackColor = true;
            // 
            // DeleteBtn
            // 
            DeleteBtn.Location = new Point(165, 3);
            DeleteBtn.Name = "DeleteBtn";
            DeleteBtn.Size = new Size(75, 23);
            DeleteBtn.TabIndex = 2;
            DeleteBtn.Text = "Delete";
            DeleteBtn.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 3;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56.1959648F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43.8040352F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 47F));
            tableLayoutPanel4.Controls.Add(ClockLabel, 1, 0);
            tableLayoutPanel4.Controls.Add(label3, 0, 0);
            tableLayoutPanel4.Controls.Add(logoutIcon, 2, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(504, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(600, 37);
            tableLayoutPanel4.TabIndex = 3;
            // 
            // ClockLabel
            // 
            ClockLabel.AutoSize = true;
            ClockLabel.Dock = DockStyle.Fill;
            ClockLabel.Font = new Font("Courier New", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ClockLabel.ForeColor = Color.White;
            ClockLabel.Location = new Point(313, 0);
            ClockLabel.Name = "ClockLabel";
            ClockLabel.Size = new Size(236, 37);
            ClockLabel.TabIndex = 2;
            ClockLabel.Text = "MM/DD/YY-HH/MM/SS";
            ClockLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Courier New", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(304, 37);
            label3.TabIndex = 1;
            label3.Text = "Logged in as Admin";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // logoutIcon
            // 
            logoutIcon.Cursor = Cursors.Hand;
            logoutIcon.Image = Properties.Resources.logout_8_32;
            logoutIcon.Location = new Point(555, 3);
            logoutIcon.Name = "logoutIcon";
            logoutIcon.Size = new Size(42, 31);
            logoutIcon.TabIndex = 3;
            logoutIcon.TabStop = false;
            logoutIcon.Click += logoutIcon_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 84);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel3);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tabControl1);
            splitContainer1.Size = new Size(1107, 561);
            splitContainer1.SplitterDistance = 369;
            splitContainer1.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(SideBarTitle, 0, 0);
            tableLayoutPanel3.Controls.Add(SidebarDataList, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 7.5329566F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 92.46704F));
            tableLayoutPanel3.Size = new Size(369, 561);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // SideBarTitle
            // 
            SideBarTitle.AutoSize = true;
            SideBarTitle.Dock = DockStyle.Left;
            SideBarTitle.Font = new Font("Courier New", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SideBarTitle.ForeColor = Color.White;
            SideBarTitle.Location = new Point(3, 0);
            SideBarTitle.Name = "SideBarTitle";
            SideBarTitle.Size = new Size(197, 42);
            SideBarTitle.TabIndex = 0;
            SideBarTitle.Text = "Users x Products list";
            SideBarTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SidebarDataList
            // 
            SidebarDataList.Dock = DockStyle.Fill;
            SidebarDataList.Location = new Point(3, 45);
            SidebarDataList.Name = "SidebarDataList";
            SidebarDataList.Size = new Size(363, 513);
            SidebarDataList.TabIndex = 1;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(UserPage);
            tabControl1.Controls.Add(productPage);
            tabControl1.Controls.Add(TransactionPage);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(734, 561);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // UserPage
            // 
            UserPage.BackColor = Color.FromArgb(7, 48, 43);
            UserPage.Location = new Point(4, 24);
            UserPage.Name = "UserPage";
            UserPage.Padding = new Padding(3);
            UserPage.Size = new Size(726, 533);
            UserPage.TabIndex = 0;
            UserPage.Text = "Users";
            // 
            // productPage
            // 
            productPage.BackColor = Color.FromArgb(7, 48, 43);
            productPage.Location = new Point(4, 24);
            productPage.Name = "productPage";
            productPage.Padding = new Padding(3);
            productPage.Size = new Size(726, 533);
            productPage.TabIndex = 1;
            productPage.Text = "Products";
            // 
            // TransactionPage
            // 
            TransactionPage.BackColor = Color.FromArgb(7, 48, 43);
            TransactionPage.Location = new Point(4, 24);
            TransactionPage.Name = "TransactionPage";
            TransactionPage.Padding = new Padding(3);
            TransactionPage.Size = new Size(726, 533);
            TransactionPage.TabIndex = 2;
            TransactionPage.Text = "Transaction";
            // 
            // Admin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 58, 52);
            ClientSize = new Size(1113, 694);
            Controls.Add(panel1);
            Name = "Admin";
            Text = "Admin";
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoutIcon).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel3;
        private Label SideBarTitle;
        private FlowLayoutPanel SidebarDataList;
        private TabControl tabControl1;
        private TabPage UserPage;
        private TabPage productPage;
        private Label label2;
        private FlowLayoutPanel flowLayoutPanel2;
        private Button AddBtn;
        private Button EditBtn;
        private Button DeleteBtn;
        private TableLayoutPanel tableLayoutPanel4;
        private Label ClockLabel;
        private Label label3;
        private PictureBox logoutIcon;
        private TabPage TransactionPage;
    }
}