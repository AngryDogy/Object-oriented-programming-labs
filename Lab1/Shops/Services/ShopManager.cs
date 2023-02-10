using System.Diagnostics.CodeAnalysis;
using Shops.Entities;
using Shops.Srvices;

namespace Shops.Services;

public class ShopManager : IShopManager
{
    private const int InputLength = 4;
    private List<Shop> _shops;
    private List<Product> _products;
    private List<Person> _clients;

    public ShopManager()
    {
        _shops = new List<Shop>();
        _products = new List<Product>();
        _clients = new List<Person>();
    }

    public IReadOnlyCollection<Shop> Shops => _shops.AsReadOnly();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
    public IReadOnlyCollection<Person> Clients => _clients.AsReadOnly();
    public Shop AddShop(string name, string country, string city, string street, int houseNumber)
    {
        var shop = new Shop(name, country, city, street, houseNumber);
        _shops.Add(shop);
        return shop;
    }

    public void AddProductToShop(Shop shop, Product product, decimal price, int productsNumber)
    {
        if (shop is null)
        {
            throw new NullReferenceException("A shop is null!");
        }

        if (product is null)
        {
            throw new NullReferenceException("A product is null!");
        }

        if (!ShopExist(shop))
        {
            throw new InvalidOperationException("A service doesn't contain a shop!");
        }

        if (!ProductExist(product))
        {
            throw new InvalidOperationException("A service doesn't contain a product!");
        }

        shop.AddProduct(product, price, productsNumber);
    }

    public void AddListOfProductsToShop(Shop shop, List<Product> products, List<decimal> prices, List<int> productsNumber)
    {
        if (shop is null)
        {
            throw new NullReferenceException("A shop is null!");
        }

        if (products is null)
        {
            throw new NullReferenceException("A product is null!");
        }

        if (!ShopExist(shop))
        {
            throw new InvalidOperationException("A service doesn't contain a shop!");
        }

        if (products.Count != prices.Count || products.Count != productsNumber.Count)
        {
            throw new InvalidOperationException("An invalid list of products!");
        }

        for (int i = 0; i < products.Count; i++)
        {
            if (!ProductExist(products[i]))
            {
                throw new InvalidOperationException("A service doesn't contain a product!");
            }

            shop.AddProduct(products[i], prices[i], productsNumber[i]);
        }
    }

    public void BuyProductFromShop(Person client, Shop shop, Product product, int productsNumber)
    {
        if (shop is null)
        {
            throw new NullReferenceException("A shop is null!");
        }

        if (product is null)
        {
            throw new NullReferenceException("A product is null!");
        }

        if (!ProductExist(product))
        {
            throw new InvalidOperationException("A service doesn't contain a product!");
        }

        if (!ClientExist(client))
        {
            throw new InvalidOperationException("A service doen't contain a client!");
        }

        shop.BuyProduct(client, product, productsNumber);
    }

    public void BuyListOfProductsFromShop(Person client, Shop shop, List<Product> products, List<int> productsNumber)
    {
        if (shop is null)
        {
            throw new NullReferenceException("A shop is null!");
        }

        if (products is null)
        {
            throw new NullReferenceException("A product is null!");
        }

        if (!ClientExist(client))
        {
            throw new InvalidOperationException("A service doen't contain a client!");
        }

        if (client.Money < shop.PriceForProducts(products))
        {
            throw new InvalidOperationException("A client doesn't have enough money!");
        }

        if (products.Count != productsNumber.Count)
        {
            throw new InvalidOperationException("An invalid list of products!");
        }

        for (int i = 0; i < products.Count; i++)
        {
            if (!ProductExist(products[i]))
            {
                throw new InvalidOperationException("A service doesn't contain a product!");
            }

            shop.BuyProduct(client, products[i], productsNumber[i]);
        }
    }

    public Product RegisterProduct(string name)
    {
        var product = new Product(name);
        _products.Add(product);
        return product;
    }

    public void RegisterListOfProducts(List<string> products)
    {
        foreach (string nameProduct in products)
        {
            var product = new Product(nameProduct);
            _products.Add(product);
        }
    }

    public Shop? FindShop(Guid id)
    {
        Shop? shop = _shops.SingleOrDefault(s => s.Id == id);
        return shop;
    }

    public Person? FindClient(Guid id)
    {
        Person? client = _clients.SingleOrDefault(c => c.Id == id);
        return client;
    }

    public Person RegisterClient(string name, decimal money)
    {
        var client = new Person(name, money);
        _clients.Add(client);
        return client;
    }

    public Shop SearchForCheapestPrice(Product product)
    {
        if (product is null)
        {
            throw new NullReferenceException("A product is null!");
        }

        if (!ProductExist(product))
        {
            throw new InvalidOperationException("A product doesn't exist!");
        }

        IEnumerable<Shop> shopWithProduct = _shops.Where(p => p.ProductsNumber.ContainsKey(product)).Where(p => p.ProductsNumber[product] != 0);
        Shop? cheapestShop = shopWithProduct.OrderBy(p => p.ProductsPrices[product]).First();
        return cheapestShop;
    }

    public Shop? SearchForCheapestPriceList(List<Product> products)
    {
        if (products is null)
        {
            throw new NullReferenceException("Products is null!");
        }

        Shop? cheapestShop = _shops.OrderBy(p => p.PriceForProducts(products)).FirstOrDefault();
        return cheapestShop;
    }

    public bool ShopExist(Shop shop) => _shops.Any(x => x.Id == shop.Id);

    public bool ProductExist(Product product) => _products.Any(x => x.Id == product.Id);
    public bool ClientExist(Person client) => _clients.Any(x => x.Id == client.Id);
}