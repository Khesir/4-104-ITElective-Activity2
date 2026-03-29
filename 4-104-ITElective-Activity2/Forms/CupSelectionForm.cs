using _4_104_ITElective_Activity2.modules.transactionItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _4_104_ITElective_Activity2.Forms
{
    public partial class CupSelectionForm : Form
    {
        public CupSize SelectedCupSize { get; private set; } = CupSize.Regular;

        public CupSelectionForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            radioButton1.Checked = true;
            button1.Click += (s, e) =>
            {
                if (radioButton3.Checked)       SelectedCupSize = CupSize.Grande;
                else if (radioButton2.Checked)  SelectedCupSize = CupSize.Venti;
                else                            SelectedCupSize = CupSize.Regular;
                DialogResult = DialogResult.OK;
            };
        }
    }
}
