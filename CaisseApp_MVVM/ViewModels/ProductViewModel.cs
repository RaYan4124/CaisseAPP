using System.Configuration;
using System.Data;
using System.Windows;
using Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Mysqlx.Datatypes;
using Object = Mysqlx.Datatypes.Object;

namespace CaisseApp_MVVM.ViewModels;


public class ProductViewModel : INotifyPropertyChanged
{
    private DB_Helper _db_helper = new DB_Helper();

    private ObservableCollection<Product> _products = new ObservableCollection<Product>();
    private ObservableCollection<Product> _research = new ObservableCollection<Product>();
    
    private Product _selectedProduct;
    private Product _selectedProductFromSearch;
    private string _pad_value;
    private int _new_quantity;
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
    
    public ObservableCollection<Product> Research
    {
        get { return _research; }
        set
        {
            _research = value;
            OnPropertyChanged(nameof(Research));
        }
    }
    
    public Product SelectedProduct
    {
        get => _selectedProduct; 
        set
        {
            _selectedProduct = value; //for setting new product, the intern product take the new value with the twoway biding
            _new_quantity = value.Quantity;
            OnPropertyChanged(nameof(SelectedProduct)); //to notify the vue for the binding
            OnPropertyChanged(nameof(NewQuantity));
        }
    }

    public Product SelectedProductFromSearch
    {
        get => _selectedProductFromSearch;
        set
        {
            _selectedProductFromSearch = value;
            OnPropertyChanged(nameof(SelectedProductFromSearch));
            if (_selectedProductFromSearch != null)
            {
                AddProduct(_selectedProductFromSearch.Id, _selectedProductFromSearch.Name, _selectedProductFromSearch.Price);
                _selectedProductFromSearch = null;
            }
        }
    }
    
    public string PadValue
    {
        get => _pad_value;
        set
        {
            _pad_value = value;
            OnPropertyChanged(nameof(PadValue));
        }
    }

    public int NewQuantity
    {
        get => _new_quantity;
        set
        {
            _new_quantity = value;
            OnPropertyChanged(nameof(NewQuantity));
        }
    }

    public double TotalPrice
    {
        get
        {
            double total = Products.Sum(p => p.TotalPrice);
            return total;
        }

        set { OnPropertyChanged(nameof(TotalPrice)); }
    }
    
        //get{return Products.Sum(p => p.Price);}  get permet de calculer et renvoyer une valeur, tandis que set permet de modifier et d'affecter un nouveau contenue a la propriété
    
    
    public ProductViewModel()
    {
        Products =
            new ObservableCollection<Product>(); //overload the constructor of OC to fill directly the collection with the result of GetProducts().
        Products.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalPrice));
    }

    public void AddProduct(int id, string name, int price)
    {
        var existing = Products.FirstOrDefault(p => p.Id == id);
        int QtnToAdd = String.IsNullOrEmpty(_pad_value) ? 1 : int.Parse(_pad_value);
        if (existing == null)
        {
            Products.Add(new Product(id, name,
                price, QtnToAdd));
        }
        else
        {
            existing.Quantity += QtnToAdd;
        }
        PadValue = string.Empty;
        OnPropertyChanged(nameof(TotalPrice));
    }

    public void ShowProducts()
    {
        Products.Clear(); 
        foreach (var product in _db_helper.GetProducts()) 
        {
            Products.Add(product);
        }

        OnPropertyChanged(nameof(TotalPrice));
    }

    public void Modify_Qtn(int NewQtn)
    {
        SelectedProduct.Quantity = NewQtn;
        OnPropertyChanged(nameof(TotalPrice));
    }

    public void Searching(string s)
    {
        Research.Clear();
        List<Product> res = _db_helper.GetProductByName(s);
        if (res.Any())
        {
            foreach (var p in res)
            {
                Research.Add(p);
            }
        }
    }
    
    public void Remove_Product()
    {
        if (SelectedProduct != null)
        {
            Products.Remove(SelectedProduct);
        }
        OnPropertyChanged(nameof(TotalPrice));
    }
    
    public void NumPad(string s)
    {
        PadValue += s;
    }
    
    public void CorrectionPad()
    {
        PadValue = string.Empty;
    }

    public void IncrementQuantity()
    {
        NewQuantity++;
    }

    public void DecrementQuantity()
    {
        if (NewQuantity > 1)
        {
            NewQuantity--;
        }
        else
        {
            NewQuantity = 1;
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}













