using System.Windows;
using CaisseApp_MVVM.ViewModels;
using Models;

namespace CaisseApp_MVVM.View;

public partial class SearchWindow : Window
{
    private DB_Helper _dbHelper;
    private ProductViewModel _pvm;
    
    public SearchWindow(ProductViewModel pvm)
    {
        InitializeComponent();
        _pvm = pvm;
        this.DataContext = pvm;
        _dbHelper = new DB_Helper();
    }

    private void SearchProduct(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(ProductName.Text))
            {
                _pvm.Searching(ProductName.Text);
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "", MessageBoxButton.OK);
        }
    }
    
    private void Close_Window_Button(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}