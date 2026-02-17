using _4_104_ITElective_Activity2.core;
using _4_104_ITElective_Activity2.modules.item;
using _4_104_ITElective_Activity2.modules.transactionItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;

namespace _4_104_ITElective_Activity2.forms
{
    public partial class MainForm : Form
    {
        private ItemService _service;
        private BindingList<TransactionItem> _transactionItems;
        private CultureInfo phCulture = new CultureInfo("en-PH");

        public MainForm()
        {
            InitializeComponent();

            InitializeTransactionGrid();

            _service = new ItemService(new ItemRepository()); // thjis can be improved by cached persistence, but for the sake of simplicity, we will just create a new instance here

            // handle resizing of itemSelector to make sure the userControl inside it will also resize
            itemSelector.Resize += (s, e) =>
            {
                foreach (Control c in itemSelector.Controls)
                {
                    c.Width = itemSelector.ClientSize.Width - 20;
                }
            };
            EventBus.Subscribe<AddItemToCartDTO>(OnItemAdded);
            EventBus.Subscribe<LoadItemsResultDTO>(OnItemsLoaded);

            EventBus.Publish(new LoadItemsRequestDTO());
        }
        public void InitializeTransactionGrid()
        {
            _transactionItems = new BindingList<TransactionItem>();

            transactionGridView.AutoGenerateColumns = false;
            transactionGridView.AllowUserToAddRows = false;
            transactionGridView.Columns.Clear();

            transactionGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(TransactionItem.id),
                HeaderText = "ID",
                DataPropertyName = nameof(TransactionItem.id)
            });

            transactionGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(TransactionItem.name),
                HeaderText = "Item Name",
                DataPropertyName = nameof(TransactionItem.name)
            });

            transactionGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(TransactionItem.quantity),
                HeaderText = "Qty",
                DataPropertyName = nameof(TransactionItem.quantity)
            });

            transactionGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(TransactionItem.price),
                HeaderText = "Price",
                DataPropertyName = nameof(TransactionItem.price)
            });

            transactionGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(TransactionItem.totalPrice),
                HeaderText = "Total",
                DataPropertyName = nameof(TransactionItem.totalPrice),
                ReadOnly = true
            });

            transactionGridView.DataSource = _transactionItems;
        }
        private void OnItemsLoaded(LoadItemsResultDTO result)
        {
            // Load items to ListView
            foreach (var item in result.Items)
            {
                // add userControl compoment to listView
                var card = new ItemCard(item);

                itemSelector.Controls.Add(card);
            }
        }
        private void OnItemAdded(AddItemToCartDTO dto)
        {
            var item = dto.Item;
            var existingItem = _transactionItems.FirstOrDefault(t => t.id == item.id);
            if (existingItem != null)
            {
                existingItem.quantity += 1;
                _transactionItems.ResetBindings(); // Notify the DataGridView to refresh the display
            }
            else
            {
                _transactionItems.Add(new TransactionItem
                {
                    id = _transactionItems.Count + 1,
                    itemId = (int)item.id!,
                    name = item.name,
                    price = item.price,
                    quantity = 1,
                });
            }
            // Update total price for all items
            UpdateTotalPrice();
        }
        private void UpdateTotalPrice()
        {
            var total = _transactionItems.Sum(t => t.GetTotalPrice());
            priceLabel.Text = total.ToString("C", phCulture);

            // Recalculate change automatically
            UpdateChange();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            EventBus.Unsubscribe<AddItemToCartDTO>(OnItemAdded);
            EventBus.Unsubscribe<LoadItemsResultDTO>(OnItemsLoaded);
            base.OnFormClosed(e);
        }

        private void paymentTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsDigit(e.KeyChar)) return;
            if (e.KeyChar == '.' && !paymentTextBox.Text.Contains('.')) return;
            e.Handled = true;
        }

        private void paymentTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateChange();
        }
        private void UpdateChange()
        {
            if (decimal.TryParse(paymentTextBox.Text, NumberStyles.Number, phCulture, out decimal payment) &&
                decimal.TryParse(priceLabel.Text, NumberStyles.Currency, phCulture, out decimal total))
            {
                decimal change = payment - total;
                changeLabel.Text = change.ToString("C", phCulture);
                changeLabel.ForeColor = (change < 0) ? System.Drawing.Color.Yellow : System.Drawing.Color.Yellow;
            }
            else
            {
                changeLabel.Text = (0m).ToString("C", phCulture);
                changeLabel.ForeColor = System.Drawing.Color.Yellow;
            }
        }
    }
}
