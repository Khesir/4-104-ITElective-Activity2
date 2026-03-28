using _4_104_ITElective_Activity2.Components;
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

            EventBus.Subscribe<ClockTickDTO>(OnClockTick);

            // Trigger initial sidebar load
            tabControl1_SelectedIndexChanged(this, EventArgs.Empty);


        }

        private void OnClockTick(ClockTickDTO dto)
        {
            ClockLabel.Text = dto.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SidebarDataList.Controls.Clear();

            if (tabControl1.SelectedTab == UserPage)
            {
                // Load users
                SideBarTitle.Text = "Users";
                List<User> users = _userService.GetAllUsers();
                Console.WriteLine($"Loaded {users.Count} users");
                if (users.Count == 0)
                {
                    var userControl = new AdminSideBarItem { Title = "No user found", Description = "" };
                    SidebarDataList.Controls.Add(userControl);
                }
                else
                {
                    foreach (var user in users)
                    {
                        var userControl = new AdminSideBarItem { Title = $"Name: {user.Username}", Description = $"Role: {user.Role}" };

                        userControl.RegisterClickHandler((s, args) => { }); // Todo update the tab
                        SidebarDataList.Controls.Add(userControl);
                    }
                }

            }
            else if (tabControl1.SelectedTab == productPage)
            {
                // Load products
                SideBarTitle.Text = "Products";
                List<Product> products = _productService.GetAllProducts();
                if (products.Count == 0)
                {
                    var userControl = new AdminSideBarItem { Title = "No product found", Description = "" };
                    SidebarDataList.Controls.Add(userControl);
                }
                else
                {
                    foreach (var product in products)
                    {
                        var userControl = new AdminSideBarItem { Title = product.name, Description = $"Price: {product.price.ToString()}" };

                        userControl.RegisterClickHandler((s, args) => { }); // Todo update the tab
                        SidebarDataList.Controls.Add(userControl);
                    }
                }
            }
            else if (tabControl1.SelectedTab == TransactionPage)
            {
                // Load transactions
                SideBarTitle.Text = "Transactions";
                List<Transaction> transactions = _transactionService.GetAllTransactions();
                if (transactions.Count == 0)
                {
                    var userControl = new AdminSideBarItem { Title = "No transaction found", Description = "" };
                    SidebarDataList.Controls.Add(userControl);
                }
                else
                {
                    foreach (var transaction in transactions)
                    {
                        var userControl = new AdminSideBarItem { Title = transaction.createdAt.ToString("MM/dd/yyyy hh:mm:ss tt"), Description = $"Total Amount: {transaction.totalAmount.ToString()}" };
                        userControl.RegisterClickHandler((s, args) => { }); // Todo update the tab
                        SidebarDataList.Controls.Add(userControl);
                    }
                }

            }
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
                EventBus.Publish(new LogoutSignal { signal = true });
                this.Close();

            }
        }

        private void tableLayoutPanel13_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
