using CaisseApp_MVVM.ViewModels;

namespace CaisseApp_Tests_v0;

public class ProductViewModel_test
{
    public ProductViewModel Pvm_Test = new ProductViewModel();

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
}