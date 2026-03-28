using _4_104_ITElective_Activity2.modules.transactionItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace _4_104_ITElective_Activity2.modules.transaction
{
    public class Transaction : INotifyPropertyChanged
    {
        private double _totalAmount;

        public int? id { get; set; }
        public int? userId { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public List<TransactionItem> items { get; set; } = new();

        public double totalAmount
        {
            get => _totalAmount;
            private set
            {
                if (_totalAmount != value)
                {
                    _totalAmount = value;
                    OnPropertyChanged(nameof(totalAmount));
                }
            }
        }

        public void AddItem(TransactionItem item)
        {
            items.Add(item);
            RecalculateTotal();
        }

        public void RemoveItem(TransactionItem item)
        {
            items.Remove(item);
            RecalculateTotal();
        }

        public void RecalculateTotal()
        {
            totalAmount = items.Sum(i => i.totalPrice);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
