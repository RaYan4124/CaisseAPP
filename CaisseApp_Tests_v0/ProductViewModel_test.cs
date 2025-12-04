using CaisseApp_MVVM.ViewModels;
using Models;

namespace CaisseApp_Tests_v0;

public class ProductViewModel_test
{
    public ProductViewModel Pvm_Test = new ProductViewModel();
    public Product p_test = new Product(0000, "p_test", 1);
    public Random rand = new Random();
    
    [Fact]
    public void Modify_Qtn_test()
    {
        int x = rand.Next(50);
        Pvm_Test.SelectedProduct = p_test;
        Pvm_Test.Modify_Qtn(x);
        Assert.Equal(x, Pvm_Test.SelectedProduct.Quantity);
    }

    [Fact]
    public void AddProduct_emptyListCase_test()
    {
        Pvm_Test.Products.Clear();
        Assert.Empty(Pvm_Test.Products);
        Pvm_Test.AddProduct(p_test.Id, p_test.Name, p_test.Price);
        
        Assert.NotEmpty(Pvm_Test.Products);
        Assert.Single(Pvm_Test.Products);
        Assert.Equal(1, Pvm_Test.Products.First().Quantity);
        Assert.Equal(p_test.Name, Pvm_Test.Products.First().Name);
        Assert.Equal(p_test.Id, Pvm_Test.Products.First().Id);
        Assert.Equal(p_test.Price, Pvm_Test.Products.First().Price);
        Assert.Equal(p_test.Price, Pvm_Test.TotalPrice);
    }

    [Fact]
    public void AddProductSearch_notEmptyListCase_test()
    {
        Pvm_Test.Products.Clear();
        Assert.Empty(Pvm_Test.Products);

        p_test.Quantity = 1;
        Pvm_Test.Products.Add(p_test);
        Assert.NotEmpty(Pvm_Test.Products);
        
        Pvm_Test.AddProduct(p_test.Id, p_test.Name, p_test.Price);
        Assert.Equal(2, Pvm_Test.Products.First().Quantity);
        Assert.Equal(p_test.Name, Pvm_Test.Products.First().Name);
        Assert.Equal(p_test.Id, Pvm_Test.Products.First().Id);
        Assert.Equal(p_test.Price, Pvm_Test.Products.First().Price);
        Assert.Equal(p_test.Price * 2, Pvm_Test.TotalPrice);
    }
}