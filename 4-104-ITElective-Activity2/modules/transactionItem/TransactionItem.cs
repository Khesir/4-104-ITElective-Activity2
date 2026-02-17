using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace _4_104_ITElective_Activity2.modules.transactionItem
{
    public class TransactionItem
    {
        private int _quantity;
        private double _totalPrice;

        public int? id { get; set; }
        
        // reference to item id, not actual id
        public required int itemId { get; set; }
        public required string name { get; set; }
        public double price { get; set; }

        public int quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(quantity));
                    // auto-update totalPrice
                    totalPrice = _quantity * price;
                }
            }
        }

        public double totalPrice
        {
            get => _totalPrice;
            private set
            {
                if (_totalPrice != value)
                {
                    _totalPrice = value;
                    OnPropertyChanged(nameof(totalPrice));
                }
            }
        }

        public double GetTotalPrice()
        {
            return price * quantity;
        }
        public void UpdateTotalPrice()
        {
            totalPrice = GetTotalPrice();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
