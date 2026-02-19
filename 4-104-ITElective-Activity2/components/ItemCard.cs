using _4_104_ITElective_Activity2.core;
using _4_104_ITElective_Activity2.modules.item;
using _4_104_ITElective_Activity2.modules.transactionItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _4_104_ITElective_Activity2
{
    public partial class ItemCard : UserControl
    {
        // Expose usercontrol components as properties for easier access
        // Remove item to decouple the card from the item module,
        // but for simplicity we will just keep a reference to
        // the item here

        private Item item;
        public ItemCard(Item item)
        {
            InitializeComponent();
            AddClickEvent(this, ItemCard_Click); // recirsively add click event to all child controls so that the whole card is clickable
            this.item = item;
            // Set UI elements based on item properties
            string photoPath = Path.Combine(
               AppDomain.CurrentDomain.BaseDirectory,
               "data",
               "StarbukoImages",
               "StarbukoImages",
               item.imagePath
           );

            pictureBox1.Image = Image.FromFile(photoPath);
            productPrice.Text = $"₱{item.price}";
            productName.Text = item.name;
        }
        
        private void AddClickEvent(Control parent, EventHandler handler)
        {
            parent.Click += handler;

            foreach (Control child in parent.Controls)
            {
                AddClickEvent(child, handler);
            }
        }
        private void ItemCard_Click(object sender, EventArgs e)
        {
            var resullt = MessageBox.Show(
                "Choose Cup Size:\n"
                + "Yes - Grande (+20)\n"
                + "No - Venti (+30)\n"
                + "Cancel - Regular\n",
                "Select Cup Size", MessageBoxButtons.YesNoCancel);
            var cup_size = resullt switch
            {
                DialogResult.Yes => "Grande",
                DialogResult.No => "Venti",
                DialogResult.Cancel => "Regular",
                _ => "Grande"
            };
            // Sends an event if we create new transactionItem
            var entity = new TransactionItem
            {
                itemId = (int)item.id!,
                name = item.name,
                quantity = 1,
                price = item.price + (cup_size == "Grande" ? 20 : cup_size == "Venti" ? 30 : 0),
                cupSize = TransactionItem.fromString(cup_size),
            };
            entity.UpdateTotalPrice();
            var transactionItemDTO = new AddedTransactionItemDTO { TransactionItem = entity };
            EventBus.Publish(transactionItemDTO);
        }
    }
}
