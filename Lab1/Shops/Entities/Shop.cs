using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private const int Zero = 0;
    private Dictionary<Product, decimal> _productsPrices;
    private Dictionary<Product, int> _productsNumber;
    public Shop(string name, string country, string city, string street, int houseNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("A name is invalid!");
        }

        var address = new Address(country, city, street, houseNumber);
        Name = name;
        Address = address;
        Id = Guid.NewGuid();
        _productsPrices = new Dictionary<Product, decimal>();
        _productsNumber = new Dictionary<Product, int>();
    }

    public string Name { get; }
    public Address Address { get; }
    public Guid Id { get; }
    public IReadOnlyDictionary<Product, decimal> ProductsPrices => _productsPrices;
    public IReadOnlyDictionary<Product, int> ProductsNumber => _productsNumber;

    public void AddProduct(Product product, decimal price, int productsNumber)
    {
        if (product is null)
        {
            throw new NullReferenceException("A product is null!");
        }

        if (_productsPrices.ContainsKey(product))
        {
            throw new ShopInvalidOperationException("A shop already has this product!");
        }

        if (price < Zero)
        {
            throw new InvalidOperationException("A price is negative");
        }

        if (productsNumber < Zero)
        {
            throw new InvalidOperationException("A productNumber is negative");
        }

        _productsPrices[product] = price;
        _productsNumber[product] = productsNumber;
    }

    public void BuyProduct(Person person, Product product, int number)
    {
        if (person is null)
        {
            throw new NullReferenceException("A person is null");
        }

        if (product is null)
        {
            throw new NullReferenceException("A product is null");
        }

        if (!_productsNumber.ContainsKey(product))
        {
            throw new ShopInvalidOperationException("A shop doens't contain the product");
        }

        if (number < Zero)
        {
            throw new InvalidOperationException("A number is negative");
        }

        decimal price = number * _productsPrices[product];
        if (person.Money >= price && _productsNumber[product] >= number)
        {
            person.Pay(price);
            _productsNumber[product] -= number;
        }
        else
        {
            if (_productsNumber[product] < number)
            {
                throw new ShopInvalidOperationException("A shop doesn't have enough products");
            }
            else
            {
                throw new ShopInvalidOperationException("A person doens't have enough money!");
            }
        }
    }

    public void ChangeNumber(Product product, int newNumber)
    {
        if (product is null)
        {
            throw new NullReferenceException("A product is null!");
        }

        if (!_productsPrices.ContainsKey(product))
        {
            throw new ShopInvalidOperationException("A shop doens't contain the product");
        }

        if (newNumber < Zero)
        {
            throw new InvalidOperationException("newNumber is negative");
        }

        _productsNumber[product] = newNumber;
    }

    public void ChangePrice(Product product, decimal newPrice)
    {
        if (product is null)
        {
            throw new NullReferenceException("A product is null!");
        }

        if (!_productsPrices.ContainsKey(product))
        {
            throw new ShopInvalidOperationException("A shop doens't contain the product");
        }

        if (newPrice < Zero)
        {
            throw new InvalidOperationException("newPrice is negative");
        }

        _productsPrices[product] = newPrice;
    }

    public decimal PriceForProducts(List<Product> products)
    {
        decimal sum = 0;
        foreach (Product product in products)
        {
            if (!_productsPrices.ContainsKey(product))
            {
                throw new InvalidOperationException("A shop doens't contain the product!");
            }

            sum = sum + _productsPrices[product];
        }

        return sum;
    }

    public override string ToString()
    {
        return Name.ToString() + " " + Address.ToString();
    }
}