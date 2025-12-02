using CaisseApp_MVVM.ViewModels;
using Models;

namespace CaisseApp_Tests_v0;

public class ProductViewModel_test
{
    public ProductViewModel Pvm_Test = new ProductViewModel();
    public Product p_test = new Product(0000, "p_test", 1);
    public Random rand = new Random();

    [Fact]
    public void AddProduct_test()
    {
        Pvm_Test.AddProduct(1000, "prod_pvm_test", 100);
        Assert.NotEmpty(Pvm_Test.Products);
        Assert.Single(Pvm_Test.Products);
        Assert.Equal("prod_pvm_test", Pvm_Test.Products[0].Name);
        Assert.Equal(100, Pvm_Test.Products[0].Price);
        Assert.Equal(1000, Pvm_Test.Products[0].Id);
    }
    
    [Fact]
    public void Modify_Qtn_test()
    {
        int x = rand.Next(50);
        Pvm_Test.SelectedProduct = p_test;
        Pvm_Test.Modify_Qtn(x);
        Assert.Equal(x, Pvm_Test.SelectedProduct.Quantity);
    }
}