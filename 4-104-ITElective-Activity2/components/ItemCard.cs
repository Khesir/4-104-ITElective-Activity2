using _4_104_ITElective_Activity2.core;
using _4_104_ITElective_Activity2.modules.item;
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
            var dto = new AddItemToCartDTO { Item = item };
            EventBus.Publish(dto);
        }
    }
}
