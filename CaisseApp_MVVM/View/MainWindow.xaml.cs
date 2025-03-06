using System.Windows;
using System.Windows.Controls;
using CaisseApp_MVVM.ViewModels;
using System.IO;

namespace CaisseApp_MVVM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window //mainWindow Herite from Window class, that mean that it's a WPF window, Partial beceaus this the definition of this class is shared between many files(MainWindow.xaml and MainWindow.xaml.cs)
    {
        private ProductViewModel pvm;
        public MainWindow()
        {
            InitializeComponent();
            pvm = new ProductViewModel();
            this.DataContext = pvm;
        }
        
        private void Add_DB_Button_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow addProductWindow = new AddProductWindow();
            addProductWindow.ShowDialog(); //showdialog open the window 
        }

        private void Show_DB_Button_Click(object sender, RoutedEventArgs e)
        {
            pvm.ShowProducts();
        }

        private void ShowScanWindox(object sender, RoutedEventArgs e)
        {
            ScannWindow scannWindow = new ScannWindow(pvm);
            scannWindow.ShowDialog();
        }
        
        private void PrintTicket(object sender, RoutedEventArgs e)
        {
            if (pvm.Products.Count == 0)
            {
                MessageBox.Show("Aucun produit n'a été scanné");
                return;
            }

            string filePath = "C:\\Users\\ADC\\Desktop\\ticket.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine("========= Ticket de Caisse =========");
                    for (int i = 0; i < pvm.Products.Count; i++)
                    {
                        string line = $"{i + 1}. {pvm.Products[i].Name.PadRight(20)} {pvm.Products[i].Price.ToString("0.00")} €";  //PadRight add space to the right of the string to align the price, for exemple if the name do 5 chars, it will add 15 space to align the price.
                        sw.WriteLine(line);
                    }
                    sw.WriteLine("-------------------------------------");
                    sw.WriteLine($"TOTAL : {pvm.TotalPrice.ToString("0.00")} €");
                    sw.WriteLine("=====================================");
                }

                MessageBox.Show($"Ticket imprimé avec succès !\nFichier : {filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'impression du ticket : {ex.Message}");
            }
        }

    }
}