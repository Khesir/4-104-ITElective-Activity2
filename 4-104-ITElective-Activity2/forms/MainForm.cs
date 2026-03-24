using _4_104_ITElective_Activity2.Components;
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
        private ProductService _service;
        private TransactionItemService _transactionItemService;

        private BindingList<TransactionItem> _transactionItems;
        private CultureInfo phCulture = new CultureInfo("en-PH");

        public MainForm()
        {
            InitializeComponent();

            InitializeTransactionGrid();

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

            // ── REQUIRED to override Windows default header styles ──
            transactionGridView.EnableHeadersVisualStyles = false;

            // ── Header Styling ──────────────────────────────────────
            transactionGridView.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#07302B");
            transactionGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Courier New", 10f, FontStyle.Bold);
            transactionGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            transactionGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            transactionGridView.ColumnHeadersHeight = 42;
            transactionGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // ── Row Styling ─────────────────────────────────────────
            transactionGridView.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#07302B");
            transactionGridView.DefaultCellStyle.ForeColor = Color.White;
            transactionGridView.DefaultCellStyle.Font = new Font("Courier New", 9f);
            transactionGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            transactionGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 213, 79);   // yellow highlight on select
            transactionGridView.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#07302B");

            // ── Alternating Row ─────────────────────────────────────
            transactionGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(10, 55, 50); // slightly lighter green

            // ── Grid Appearance ─────────────────────────────────────
            transactionGridView.BackgroundColor = ColorTranslator.FromHtml("#07302B");
            transactionGridView.BorderStyle = BorderStyle.None;
            transactionGridView.RowHeadersVisible = false;
            transactionGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ── Bottom Border Only (per cell) ───────────────────────
            transactionGridView.CellBorderStyle = DataGridViewCellBorderStyle.None; // disable default borders
            transactionGridView.CellPainting += TransactionGrid_CellPainting;

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
        private void TransactionGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);

            // Skip header row top/side — bottom border only on data rows
            if (e.RowIndex >= 0)
            {
                using (Pen pen = new Pen(Color.FromArgb(255, 213, 79), 1))
                {
                    Point left = new Point(e.CellBounds.Left, e.CellBounds.Bottom - 1);
                    Point right = new Point(e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                    e.Graphics.DrawLine(pen, left, right);
                }
            }

            e.Handled = true;
        }
        private void OnItemsLoaded(LoadItemsResultDTO result)
        {

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

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
