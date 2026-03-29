using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _4_104_ITElective_Activity2.Components
{
    public partial class ProductCard : UserControl
    {
        private double price;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public double Price
        {
            get => price;
            set             {
                price = value;
                productPrice.Text = price.ToString("C");
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Title
        {
            get => productName.Text;
            set => productName.Text = value;
        }
        private string _imagePath;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Image
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                if (!string.IsNullOrEmpty(value))
                    pictureBox1.Load(value);
                else
                    pictureBox1.Image = null;
            }
        }

        public ProductCard()
        {
            InitializeComponent();
        }

        public void RegisterClickHandler(EventHandler handler)
        {
            AddClickEvent(this, handler);
        }

        private void AddClickEvent(Control parent, EventHandler handler)
        {
            parent.Click += handler;

            foreach (Control child in parent.Controls)
            {
                AddClickEvent(child, handler);
            }
        }
    }
}
