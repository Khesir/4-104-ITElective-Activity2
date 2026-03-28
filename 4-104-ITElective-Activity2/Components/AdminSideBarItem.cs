using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _4_104_ITElective_Activity2.Components
{
    public partial class AdminSideBarItem : UserControl
    {
        private string id;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Id
        {
            get => id;
            set => id = value;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Title
        {
            get => BarTitle.Text;
            set => BarTitle.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Description
        {
            get => BarDesc.Text;
            set => BarDesc.Text = value;
        }

        public AdminSideBarItem()
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
