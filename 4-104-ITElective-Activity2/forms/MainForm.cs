using _4_104_ITElective_Activity2.core;
using _4_104_ITElective_Activity2.modules.item;
using _4_104_ITElective_Activity2.modules.transaction;
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
        // Depenencies
        // Later will be hold under core di manager
        private ItemService _service;
        private TransactionItemService _transactionItemService;

        private BindingList<TransactionItem> _transactionItems;
        private CultureInfo phCulture = new CultureInfo("en-PH");

        public MainForm()
        {
            InitializeComponent();

            InitializeTransactionGrid();

            _service = new ItemService(new ItemRepository()); // thjis can be improved by cached persistence, but for the sake of simplicity, we will just create a new instance here
            _transactionItemService = new TransactionItemService(new TransactionItemRepository());
            // handle resizing of itemSelector to make sure the userControl inside it will also resize
            itemSelector.Resize += (s, e) =>
            {
                foreach (Control c in itemSelector.Controls)
                {
                    c.Width = itemSelector.ClientSize.Width - 20;
                }
            };
            EventBus.Subscribe<UpdateTransactionItemDTO>(OnItemAdded);
            EventBus.Subscribe<LoadItemsResultDTO>(OnItemsLoaded);

            EventBus.Publish(new LoadItemsRequestDTO());
        }
        public void InitializeTransactionGrid()
        {
            _transactionItems = new BindingList<TransactionItem>();

            transactionGridView.AutoGenerateColumns = false;
            transactionGridView.AllowUserToAddRows = false;
            transactionGridView.Columns.Clear();

            //transactionGridView.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    Name = nameof(TransactionItem.id),
            //    HeaderText = "ID",
            //    DataPropertyName = nameof(TransactionItem.id)
            //});

            transactionGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(TransactionItem.name),
                HeaderText = "Item Name",
                DataPropertyName = nameof(TransactionItem.name),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            });
            transactionGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(TransactionItem.cupSize),
                HeaderText = "Size",
                DataPropertyName = nameof(TransactionItem.cupSize),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            });

            transactionGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(TransactionItem.quantity),
                HeaderText = "Qty",
                DataPropertyName = nameof(TransactionItem.quantity),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            });

            transactionGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(TransactionItem.price),
                HeaderText = "Price",
                DataPropertyName = nameof(TransactionItem.price),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            });

            transactionGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(TransactionItem.totalPrice),
                HeaderText = "Total",
                DataPropertyName = nameof(TransactionItem.totalPrice),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
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
        private void OnItemAdded(UpdateTransactionItemDTO dto)
        {
            _transactionItems.Clear();

            foreach (var item in dto.TransactionItem)
            {
                _transactionItems.Add(item);
            }
            UpdateTotalPrice();
        }

        private void UpdateTransactionGrid()
        {
            transactionGridView.Refresh();
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
            EventBus.Unsubscribe<UpdateTransactionItemDTO>(OnItemAdded);
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

        private void button1_Click(object sender, EventArgs e)
        {
            paymentTextBox.Text = "";
            EventBus.Publish(new CreatedNewTransactionDTO());
        }
    }
}
