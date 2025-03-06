using CaisseApp_MVVM;
using Models;

namespace CaisseApp_Tests_v0;

public class DB_Helper_Test
{
    DB_Helper db_helper_test = new DB_Helper();

    [Fact]
    public void GetElementById_Test()
    {
        int IdTest = 2222;

        Product prod_test = db_helper_test.GetProductById(IdTest);

        Assert.NotNull(prod_test);
        Assert.Equal(prod_test.Id, IdTest);
    }

    [Fact]
    public void AddProduct_Test()
    {
        int TestId = 3333;
        string TestName = "testProduct";
        int TestPrice = 10;

        db_helper_test.AddProduct(TestId, TestName, TestPrice);
        Product TestProd = db_helper_test.GetProductById(TestId);

        Assert.NotNull(TestProd);
        Assert.Equal(TestProd.Id, TestId);
        Assert.Equal(TestName, TestProd.Name);
        Assert.Equal(TestPrice, TestProd.Price);
    }

    [Fact]
    public void DeleteProduct_Test()
    {
        int TestId = 3333;
        //string TestName = "testProduct";
        //int TestPrice = 10;

        db_helper_test.DeleteProduct(TestId);
        Product TestProd = db_helper_test.GetProductById(TestId);

        Assert.Null(TestProd);
    }
}