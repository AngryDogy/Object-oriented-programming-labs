using Shops.Entities;

namespace Shops.Srvices;

public interface IShopManager
{
    IReadOnlyCollection<Shop> Shops { get; }
    IReadOnlyCollection<Product> Products { get; }
    IReadOnlyCollection<Person> Clients { get; }
    Shop AddShop(string name, string country, string city, string street, int houseNumber);
    void AddProductToShop(Shop shop, Product product, decimal price, int productsNumber);
    void AddListOfProductsToShop(Shop shop, List<Product> products, List<decimal> prices, List<int> productsNumber);
    void BuyProductFromShop(Person client, Shop shop, Product product, int productsNumber);
    void BuyListOfProductsFromShop(Person client, Shop shop, List<Product> products, List<int> productsNumber);
    Product RegisterProduct(string name);
    void RegisterListOfProducts(List<string> products);
    Shop? FindShop(Guid id);
    Person? FindClient(Guid id);
    Person RegisterClient(string name, decimal money);
    Shop? SearchForCheapestPrice(Product product);
    public Shop? SearchForCheapestPriceList(List<Product> products);
    bool ShopExist(Shop shop);
    bool ProductExist(Product product);
    bool ClientExist(Person client);
}