using System.Configuration;
using System.Data;
using System.Windows;
using Models;
using MySql.Data.MySqlClient;

namespace CaisseApp_MVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            

            //MessageBox.Show("L'application démarre !", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            Product hb = new Product(99875, "Slim", 50);
            DB_Helper db_helper = new DB_Helper();
            //db_helper.GetProducts();

            //db_helper.AddProduct(9985, "Selecto", 50);
            //db_helper.AlreadyExist(hb);
            //db_helper.DeleteProduct(99585);


        }
    }

}