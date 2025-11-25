using System.Windows;
using CaisseApp_MVVM.ViewModels;

namespace CaisseApp_MVVM.View;

public partial class ModifyQuantity : Window
{
    private ProductViewModel _pvm;
    public ModifyQuantity(ProductViewModel pvm)
    {
        InitializeComponent();
        _pvm = pvm;
        this.DataContext = pvm;    //datacontext est l'objet ou les binding de la fenetre vont chercher, la pour tout les binding de la fenetre modifyquantity les information vont etre chercher dans _pvm
    }
    

    private void _ModifyQtn(Object sender, RoutedEventArgs e)
    {
        try
        {
            int _newqtn;
            if (string.IsNullOrEmpty(Newqtn.Text) || string.IsNullOrWhiteSpace(Newqtn.Text))
            {
                MessageBox.Show("Veillez Remplire Correctement Le Champs", " ", MessageBoxButton.OK);
                return;
            }else 

            if (!int.TryParse(Newqtn.Text, out _newqtn) || _newqtn <= 0)
            {
                MessageBox.Show("Veillez Remplire Le Champs avec la bonne valeur", " ", MessageBoxButton.OK);
                return;
            }
            
            _pvm.Modify_Qtn(_newqtn);
            
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, " ", MessageBoxButton.OK);
        }
    }

    private void Close_Window_Button(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

}