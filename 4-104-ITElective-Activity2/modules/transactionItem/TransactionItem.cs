using _4_104_ITElective_Activity2.modules.item;
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
        public required CupSize cupSize { get; set; }
        public required double price { get; set; }

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
        public static CupSize fromString(string str)
        {
            switch (str)
            {
                case "Grande":
                    return CupSize.Grande;
                case "Venti":
                    return CupSize.Venti;
                case "Regular":
                    return CupSize.Regular;
                default:
                    throw new ArgumentException("Invalid cup size string");
            }
        }
        public static string cupSizeToString(CupSize cupSize)
        {
            switch (cupSize)
            {
                case CupSize.Grande:
                    return "Grande";
                case CupSize.Venti:
                    return "Venti";
                case CupSize.Regular:
                    return "Regular";
                default:
                    throw new ArgumentException("Invalid cup size");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public enum CupSize
    {
        Grande,
        Venti,
        Regular
    }
}
