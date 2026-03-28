using _4_104_ITElective_Activity2.core;
using _4_104_ITElective_Activity2.Core.DI;
using _4_104_ITElective_Activity2.Core.Util;
using _4_104_ITElective_Activity2.modules.item;
using _4_104_ITElective_Activity2.modules.transaction;
using _4_104_ITElective_Activity2.Modules.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _4_104_ITElective_Activity2.Forms
{
    public partial class Admin : Form
    {
        private readonly ProductService _productService;
        private readonly TransactionService _transactionService;
        private readonly UserService _userService;

        public Admin()
        {
            InitializeComponent();

            _productService = ServiceLocator.Get<ProductService>();
            _transactionService = ServiceLocator.Get<TransactionService>();
            _userService = ServiceLocator.Get<UserService>();

            // Set initial sidebar title
            SideBarTitle.Text = "Users";
            tabControl1.SelectedTab = UserPage;

            EventBus.Subscribe<ClockTickDTO>(OnClockTick);
        }

        private void OnClockTick(ClockTickDTO dto)
        {
            ClockLabel.Text = dto.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == UserPage)
            {
                // Load users
                SideBarTitle.Text = "Users";
            }
            else if (tabControl1.SelectedTab == productPage)
            {
                // Load products

                SideBarTitle.Text = "Products";
            }
            else if (tabControl1.SelectedTab == TransactionPage)
            {
                // Load transactions

                SideBarTitle.Text = "Transactions";
            }
        }
        private void UpdateSideBarList()
        {

        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            EventBus.Unsubscribe<ClockTickDTO>(OnClockTick);
            base.OnFormClosed(e);
        }

        private void logoutIcon_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                EventBus.Publish(new LogoutSignal { signal=true});
                this.Close();

            }
        }
    }
}
