using System.Windows;
using Models;

namespace CaisseApp_MVVM.View;

public partial class AddProductWindow : Window
{
    
    private DB_Helper _db_helper;
    public AddProductWindow()
    {
        InitializeComponent(); //load interface that is define on XAML
        _db_helper = new DB_Helper();
    }
    
    private void Add_DB_Button_Click(object sender, RoutedEventArgs e)       //add product to db
    {
        try
        {
            if (string.IsNullOrEmpty(ProductName.Text) || string.IsNullOrEmpty(ProductPrice.Text) ||  string.IsNullOrEmpty(ProductPrice.Text))
            {
                MessageBox.Show("Veillez Remplire Les Champs Associer", " ", MessageBoxButton.OK);
                return;
            }

            if (!int.TryParse(ProductPrice.Text, out int price)) //trying to convert string to int and stock the result on price var, this methode return true if the operation success
            {
                MessageBox.Show("Veillez indiqué un format de prix correcte", " ", MessageBoxButton.OK);
                return;
            }
           
            if(!int.TryParse(ProductReference.Text, out int reference))
            {
                MessageBox.Show("Veillez indiqué un format de reference correcte", " ", MessageBoxButton.OK);
                return;
            }
            
            _db_helper.AddProduct(reference, ProductName.Text, price);
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void Close_Window_Button(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}