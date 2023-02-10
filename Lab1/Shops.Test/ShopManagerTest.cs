using Shops.Entities;
using Shops.Services;
using Shops.Srvices;
using Xunit;
using Xunit.Abstractions;

namespace Shops.Test;

public class ShopManagerTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ShopManagerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void AddProductToShop()
    {
        var manager = new ShopManager();
        Shop shop = manager.AddShop("Ikea", "Russia", "SPB", "dibenko", 1);
        Product product = manager.RegisterProduct("table");
        shop.AddProduct(product, 1000, 10);
        Assert.True(shop.ProductsNumber[product] == 10);
    }

    [Fact]
    public void ChangeProductPrice()
    {
        var manager = new ShopManager();
        Shop shop = manager.AddShop("Ikea", "Russia", "SPB", "dibenko", 1);
        Product product = manager.RegisterProduct("table");
        shop.AddProduct(product, 1000, 10);
        shop.ChangePrice(product, 1500);
        Assert.True(shop.ProductsPrices[product] == 1500);
    }

    [Fact]
    public void SearchForCheapestProduct()
    {
        var manager = new ShopManager();
        Shop shop1 = manager.AddShop("Ikea", "Russia", "SPB", "dibenko", 1);
        Shop shop2 = manager.AddShop("CSKA", "Russia", "Petrozavodsk", "Drevlynka", 5);
        Shop shop3 = manager.AddShop("Lerya", "Russia", "SPB", "Vaska", 12);
        Product product = manager.RegisterProduct("table");
        shop1.AddProduct(product, 1500, 10);
        shop2.AddProduct(product, 800, 7);
        shop3.AddProduct(product, 1300, 15);
        Shop cheapestShop = manager.SearchForCheapestPrice(product);
        _testOutputHelper.WriteLine(cheapestShop.ToString());
        Assert.True(cheapestShop.ToString() == "CSKA Russia, Petrozavodsk, Drevlynka, 5");
    }

    [Fact]
    public void BuyProduct()
    {
        var manager = new ShopManager();
        Shop shop = manager.AddShop("Ikea", "Russia", "SPB", "dibenko", 1);
        Product product = manager.RegisterProduct("table");
        shop.AddProduct(product, 1000, 10);
        Person client = manager.RegisterClient("Vlad", 10000);
        manager.BuyProductFromShop(client, shop, product, 10);
        Assert.True(client.Money == 0 && shop.ProductsNumber[product] == 0);
    }
}