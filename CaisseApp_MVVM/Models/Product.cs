using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{

    public class Product : INotifyPropertyChanged
    {
        [Key] public int Id { get; private set; }
        private string _name;

        private int _price;

        private int _quantity;
        public DateTime AddDate { get; private set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPrice));
            } // a chaque modofication de qtn wpf sais que totalprice a changer et va le maj
        }

        public int TotalPrice => Price * Quantity;

        public Product(int Id, string name, int price, int quantity = 1) //if qtn is not specified, it will be 1
        {
            this.Quantity = quantity;
            this.Id = Id;
            this.Name = name;
            this.Price = price;
            AddDate = DateTime.Now;
        }

        public override string ToString()
        {
            return Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}