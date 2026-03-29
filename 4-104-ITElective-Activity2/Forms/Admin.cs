using _4_104_ITElective_Activity2.Components;
using _4_104_ITElective_Activity2.core;
using _4_104_ITElective_Activity2.Core;
using _4_104_ITElective_Activity2.Core.DI;
using _4_104_ITElective_Activity2.Core.Storage;
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

        private User? _selectedUser;
        private Product? _selectedProduct;

        public Admin()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            _productService                 = ServiceLocator.Get<ProductService>();
            _transactionService             = ServiceLocator.Get<TransactionService>();
            _userService                    = ServiceLocator.Get<UserService>();

            productLayoutPanel.Visible      = false;
            transactionLayoutPanel.Visible  = false;
            userLayoutPanel.Visible         = false;

            EventBus.Subscribe<ClockTickDTO>(OnClockTick);

            // Trigger initial sidebar load
            tabControl1_SelectedIndexChanged(this, EventArgs.Empty);

            label3.Text = $"Welcome, {AppStore.Get<UserSession>()?.Username}!";
        }

        private void OnClockTick(ClockTickDTO dto)
        {
            ClockLabel.Text = dto.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
        }


        private void SetSidebarLoading(bool isLoading)
        {
            TabControlPanel.Enabled = !isLoading;
            SidebarDataList.Controls.Clear();
            if (isLoading)
                SidebarDataList.Controls.Add(new AdminSideBarItem { Title = "Loading...", Description = "" });
        }

        private void SetCrudButtonsEnabled(bool enabled)
        {
            addBtn.Enabled    = enabled;
            EditBtn.Enabled   = enabled;
            DeleteBtn.Enabled = enabled;
        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControlPanel.SelectedTab == TransactionPage)
                SetCrudButtonsEnabled(false);
            else
                SetCrudButtonsEnabled(true);

            if (TabControlPanel.SelectedTab == UserPage)
            {
                SideBarTitle.Text = "Users";
                SetSidebarLoading(true);
                List<User> users = await _userService.GetAllUsersAsync();
                SetSidebarLoading(false);
                updateQuickActionBtn(true);

                Console.WriteLine($"Loaded {users.Count} users");
                if (users.Count == 0)
                {
                    SidebarDataList.Controls.Add(new AdminSideBarItem { Title = "No user found", Description = "" });
                    userLayoutPanel.Visible = false;
                }
                else
                {
                    foreach (var user in users)
                    {
                        var userControl = new AdminSideBarItem { Title = $"Name: {user.Username}", Description = $"Role: {user.Role}" };
                        userControl.RegisterClickHandler((s, args) => updateSelectedTabUser(user));
                        SidebarDataList.Controls.Add(userControl);
                    }

                    updateSelectedTabUser(users[0]);
                    userLayoutPanel.Visible = true;
                }
            }
            else if (TabControlPanel.SelectedTab == productPage)
            {
                SideBarTitle.Text = "Products";
                SetSidebarLoading(true);
                List<Product> products = await _productService.GetAllProductsAsync();
                SetSidebarLoading(false);
                updateQuickActionBtn(true);

                if (products.Count == 0)
                {
                    SidebarDataList.Controls.Add(new AdminSideBarItem { Title = "No product found", Description = "" });
                    productLayoutPanel.Visible = false;
                }
                else
                {
                    foreach (var product in products)
                    {
                        var userControl = new AdminSideBarItem { Title = product.name, Description = $"Price: {product.price}" };
                        userControl.RegisterClickHandler((s, args) => updateSelectedTabProduct(product));
                        SidebarDataList.Controls.Add(userControl);
                    }
                    updateSelectedTabProduct(products[0]);
                    productLayoutPanel.Visible = true;
                }
            }
            else if (TabControlPanel.SelectedTab == TransactionPage)
            {
                SideBarTitle.Text = "Transactions";
                SetSidebarLoading(true);
                List<Transaction> transactions = await _transactionService.GetAllTransactionsAsync();
                SetSidebarLoading(false);
                updateQuickActionBtn(false);
                if (transactions.Count == 0)
                {
                    SidebarDataList.Controls.Add(new AdminSideBarItem { Title = "No transaction found", Description = "" });
                    transactionLayoutPanel.Visible = false;
                }
                else
                {
                    foreach (var transaction in transactions)
                    {
                        var userControl = new AdminSideBarItem { Title = transaction.createdAt.ToString("MM/dd/yyyy hh:mm:ss tt"), Description = $"Total Amount: ₱{transaction.totalAmount}" };
                        userControl.RegisterClickHandler((s, args) => updateSelectedTabTransaction(transaction));
                        SidebarDataList.Controls.Add(userControl);
                    }
                    updateSelectedTabTransaction(transactions[0]);
                    transactionLayoutPanel.Visible = true;
                }
            }
        }
        private void updateQuickActionBtn(bool flag)
        {
            addBtn.Enabled    = flag;
            EditBtn.Enabled   = flag;
            DeleteBtn.Enabled = flag;
        }
        private async void updateSelectedTabUser(User user)
        {
            _selectedUser = user;
            userIdLabel.Text                = user.Id.ToString();
            usernameLabel.Text              = user.Username;
            userRoleLabel.Text              = user.Role;

            userGenderLabel.Text            = user.Gender           != null ? user.Gender           : "Not set";
            userContactLabel.Text           = user.Contact          != null ? user.Contact          : "Not set";
            userPermanentAddressLabel.Text  = user.PermanentAddress != null ? user.PermanentAddress : "Not set";
            userCurrentAddressLabel.Text    = user.CurrentAddress   != null ? user.CurrentAddress   : "Not set";

            userTotalTransactionLabel.Text  = "Loading...";
            userPictureBox.Image            = Properties.Resources.images__2_;

            int txCount = await _transactionService.GetTransactionCountByUserAsync(user.Id);
            userTotalTransactionLabel.Text = txCount.ToString();

            // Section 2 General Information
            var nameParts = new List<string>();

            if (!string.IsNullOrWhiteSpace(user.LastName))
                nameParts.Add(user.LastName);

            var firstMiddle = string.Join(" ", new[] { user.MiddleName, user.FirstName }
                                            .Where(s => !string.IsNullOrWhiteSpace(s)));

            if (!string.IsNullOrWhiteSpace(firstMiddle))
                nameParts.Add(firstMiddle);

            // Set full name label or "Not set" if everything is empty
            userFullnameLabel.Text = nameParts.Count > 0
                ? string.Join(", ", nameParts)
                : "Not set";


            string? imageUrl = await _userService.GetProfileImageUrlAsync(user.ImagePath);
            if (imageUrl != null)
                userPictureBox.Load(imageUrl);
            else
                userPictureBox.Image = Properties.Resources.images__2_;
        }

        private async void updateSelectedTabProduct(Product product)
        {
            _selectedProduct = product;
            productIdLabel.Text             = product.id.ToString();
            productNameLabel.Text           = product.name;
            productPriceLabel.Text          = product.price.ToString("C2");
            productAvailabilityLabel.Text   = product.isAvailable ? "Available" : "Not Available";
            productPictureBox.Image            = Properties.Resources.images__2_;

            // Update the picture box in the product details tab with the image of the selected product.
            string? imageUrl = await _productService.GetProductImageUrlAsync(product.imagePath);
            if (imageUrl != null)
                productPictureBox.Load(imageUrl);
            else
                productPictureBox.Image = Properties.Resources.images__2_;
        }
        private async void updateSelectedTabTransaction(Transaction transaction)
        {
            transactionIdLabel.Text    = $"ID: {transaction.id}";
            transactionTitleLabel.Text = "Loading...";
            transactionItemDataGrid.DataSource   = null;

            var items = await _transactionService.GetItemsByTransactionIdAsync((int)transaction.id!);

            transactionTitleLabel.Text = $"Transaction Items ({items.Count})";
            transactionItemDataGrid.DataSource         = items;
            transactionItemDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (transactionItemDataGrid.Columns["id"]     != null) transactionItemDataGrid.Columns["id"].Visible     = false;
            if (transactionItemDataGrid.Columns["itemId"] != null) transactionItemDataGrid.Columns["itemId"].Visible = false;
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

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (TabControlPanel.SelectedTab == UserPage)
            {
                using var form = new UserForm(FormMode.Add);
                if (form.ShowDialog() == DialogResult.OK)
                    tabControl1_SelectedIndexChanged(this, EventArgs.Empty);
            }
            else if (TabControlPanel.SelectedTab == productPage)
            {
                using var form = new ProductForm(FormMode.Add);
                if (form.ShowDialog() == DialogResult.OK)
                    tabControl1_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (TabControlPanel.SelectedTab == UserPage)
            {
                using var form = new UserForm(FormMode.Edit, _selectedUser);
                if (form.ShowDialog() == DialogResult.OK)
                    tabControl1_SelectedIndexChanged(this, EventArgs.Empty);
            }
            else if (TabControlPanel.SelectedTab == productPage)
            {
                using var form = new ProductForm(FormMode.Edit, _selectedProduct);
                if (form.ShowDialog() == DialogResult.OK)
                    tabControl1_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        private async void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (TabControlPanel.SelectedTab == UserPage)
            {
                if (_selectedUser == null) return;
                var confirm = MessageBox.Show(
                    $"Are you sure you want to delete user \"{_selectedUser.Username}\"?",
                    "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;

                await _userService.DeleteUserAsync(_selectedUser.Id);
                _selectedUser = null;
                tabControl1_SelectedIndexChanged(this, EventArgs.Empty);
            }
            else if (TabControlPanel.SelectedTab == productPage)
            {
                if (_selectedProduct == null) return;
                var confirm = MessageBox.Show(
                    $"Are you sure you want to delete product \"{_selectedProduct.name}\"?",
                    "Delete Product", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;

                await _productService.DeleteProductAsync(_selectedProduct.id!.Value);
                _selectedProduct = null;
                tabControl1_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

    }
}
