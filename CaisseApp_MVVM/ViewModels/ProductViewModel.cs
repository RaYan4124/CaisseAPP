using System.Configuration;
using System.Data;
using System.Windows;
using Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CaisseApp_MVVM.ViewModels;


public class ProductViewModel : INotifyPropertyChanged
{
    private DB_Helper _db_helper = new DB_Helper();

    private ObservableCollection<Product> _products = new ObservableCollection<Product>();

    public ObservableCollection<Product> Products
    {
        get { return _products; }
        set
        {
            if (_products != value)
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
                OnPropertyChanged(nameof(TotalPrice)); //notify that TotalPrice must be recalculed
            }

        }
    } // collection that notify and declanche an event whene something is changed in the collection to update automatically the user interface.


    public double TotalPrice
    {
        get
        {
            double total = Products.Sum(p => p.Price);
            Console.WriteLine($"TotalPrice mis à jour : {total}");
            return total;
        }

        set { OnPropertyChanged(nameof(TotalPrice)); }
    }

    /*{
        get{return Products.Sum(p => p.Price);}  get permet de calculer et renvoyer une valeur, tandis que set permet de modifier et d'affecter un nouveau contenue a la propriété

    }*/
    public ProductViewModel()
    {
        Products =
            new ObservableCollection<Product>(); //overload the constructor of OC to fill directly the collection with the result of GetProducts().
        Products.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalPrice));
    }

    public void AddProduct(int id, string name, int price)
    {
        Products.Add(new Product(id, name,
            price)); //create new product with parameters values and push it in the collection.
        OnPropertyChanged(nameof(TotalPrice));
    }

    public void ShowProducts()
    {
        Products.Clear(); //clear the collection
        foreach (var product in _db_helper.GetProducts()) //foreach product on the db 
        {
            Products.Add(product); //add the product on the collection
        }

        OnPropertyChanged(nameof(TotalPrice));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
