using System.Windows;
using System.Windows.Controls;
using Models;
using CaisseApp_MVVM.ViewModels;

namespace CaisseApp_MVVM.View;

public partial class ScannWindow : Window
{
    private DB_Helper _dbHelper;
    private ProductViewModel _pvm;
    public ScannWindow(ProductViewModel pvm)
    {
        InitializeComponent();
        _dbHelper = new DB_Helper();
        _pvm = pvm;
        this.DataContext = _pvm;
    }

    private void ScannProduct(object sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(ProductRef.Text) || string.IsNullOrWhiteSpace((ProductRef.Text)))
            {
                MessageBox.Show("Veillez Remplire Correctement Le Champs", " ", MessageBoxButton.OK);
                return;
            }

            if (!int.TryParse(ProductRef.Text, out int productId))
            {
                MessageBox.Show("Veillez indiqué un format de reference correcte", " ", MessageBoxButton.OK);
                return;
            }

            
            Product product = _dbHelper.GetProductById(productId);
            

            if (product != null)
            {   
                _pvm.AddProduct(product.Id, product.Name, product.Price);
                _pvm.TotalPrice = _pvm.TotalPrice;

            }
            else
            {
                MessageBox.Show("Aucun element n'a été trouvé");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, " ", MessageBoxButton.OK);
        }
        finally
        {
            ProductRef.Clear();
        }
    }
    
    private void Close_Window_Button(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}