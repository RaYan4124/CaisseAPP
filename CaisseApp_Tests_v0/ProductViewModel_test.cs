using CaisseApp_MVVM.ViewModels;
using Models;

namespace CaisseApp_Tests_v0;

public class ProductViewModel_test
{
    public ProductViewModel Pvm_Test = new ProductViewModel();
    public DB_Helper db_test = new DB_Helper();
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
    public void AddProduct_notEmptyListCase_test()
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

    [Fact]
    public void Remove_Product_test()
    {
        Pvm_Test.Products.Clear();
        Assert.Empty(Pvm_Test.Products);
        
        int x = rand.Next(5);
        for (int i = 1; i <= 5; i++)
        {
            Pvm_Test.Products.Add(new Product(i, $"{i}-test", 10));
        }
        
        Assert.NotEmpty(Pvm_Test.Products);
        Assert.Equal(50, Pvm_Test.TotalPrice);

        for (int i = 1; i <= 5; i++)
        {
            var first = Pvm_Test.Products.First();
            Pvm_Test.SelectedProduct = first;
            Pvm_Test.Remove_Product();
            Assert.DoesNotContain(first, Pvm_Test.Products);
            Assert.Equal(5 - i ,Pvm_Test.Products.Count);
            Assert.Equal(Pvm_Test.Products.Count * 10, Pvm_Test.TotalPrice);
        }
        Assert.Empty(Pvm_Test.Products);
        Assert.Equal(0, Pvm_Test.TotalPrice);
        
        //trying removing for empty list
        Pvm_Test.Remove_Product();
        Assert.Empty(Pvm_Test.Products);
        Assert.Equal(0, Pvm_Test.TotalPrice);
    }

    [Fact]
    public void Searching_test()
    {
        Pvm_Test.Research.Clear();
        Assert.Empty(Pvm_Test.Research);
        try
        {
            db_test.AddProduct(999999999, "99999", 99999);
            Pvm_Test.Searching("99999");
        
            Assert.NotEmpty(Pvm_Test.Research);
            Assert.Single(Pvm_Test.Research);
            Assert.Equal(999999999, Pvm_Test.Research.First().Id);
        }
        finally
        {
            db_test.DeleteProduct(999999999);
        } 
    }
}