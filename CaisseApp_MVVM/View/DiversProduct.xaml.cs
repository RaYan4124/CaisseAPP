using System.Windows;
using System.Windows.Controls;
using CaisseApp_MVVM.ViewModels;

namespace CaisseApp_MVVM.View;

public partial class DiversProduct : Window
{
    private ProductViewModel _pvm;
    public DiversProduct(ProductViewModel pvm)
    {
        InitializeComponent();
        _pvm = pvm;
        this.DataContext = pvm;
    }

    private void NumPadDivers(object sender, RoutedEventArgs routedEventArgs)
    {
        Button btn = sender as Button;
        _pvm.NumPadDivers(btn.Content.ToString());
    }

    private void divers_product(object sender, RoutedEventArgs routedEventArgs)
    {
        int price;
        if (String.IsNullOrEmpty(divers_price.Text) || !int.TryParse(divers_price.Text,out price))
        {
            MessageBox.Show("Veillez Remplire Correctement Le Champs", " ", MessageBoxButton.OK);
            return;
        }

        _pvm.DiversProduct(price);
        divers_window_closed(sender, routedEventArgs);
    }

    private void divers_window_closed(object sender, EventArgs eventArgs)
    {
        _pvm.DiversWindowClosed();
        this.Close();
    }
}