using System;
using System.Collections.Generic;

public class Product
{
    private string _name;
    private string _productId;
    private decimal _price;
    private int _quantity;

    public Product(string name, string productId, decimal price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetProductId()
    {
        return _productId;
    }

    public decimal GetTotalCost()
    {
        return _price * _quantity;
    }
}

public class Address
{
    private string _streetAddress;
    private string _city;
    private string _state;
    private string _country;

    public Address(string streetAddress, string city, string state, string country)
    {
        _streetAddress = streetAddress;
        _city = city;
        _state = state;
        _country = country;
    }

    public bool IsInUSA()
    {
        return _country.ToLower() == "usa";
    }

    public string GetFullAddress()
    {
        return $"{_streetAddress}\n{_city}, {_state}\n{_country}";
    }
}

public class Customer
{
    private string _name;
    private Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    public string GetName()
    {
        return _name;
    }

    public bool LivesInUSA()
    {
        return _address.IsInUSA();
    }

    public string GetShippingAddress()
    {
        return _address.GetFullAddress();
    }
}

public class Order
{
    private List<Product> _products = new List<Product>();
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public decimal GetTotalCost()
    {
        decimal total = 0;
        foreach (Product product in _products)
        {
            total += product.GetTotalCost();
        }

        // Add shipping cost
        decimal shippingCost = _customer.LivesInUSA() ? 5 : 35;
        return total + shippingCost;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (Product product in _products)
        {
            label += $"{product.GetName()} (ID: {product.GetProductId()})\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{_customer.GetName()}\n{_customer.GetShippingAddress()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();

        // Create customers and addresses
        Address address1 = new Address("17748", "Paris", "Vincennes", "FR");
        Customer customer1 = new Customer("Ludovic Jean-Pierre Baron", address1);

        Address address2 = new Address("456 Pool Street", "Diata", "Ex-Tele", "Congo");
        Customer customer2 = new Customer("Laurita Smith Makaya", address2);

        // Create a list of customers
        List<Customer> customers = new List<Customer> { customer1, customer2 };

        // Create products
        List<Product> products = new List<Product>
        {
            new Product("Laptop", "LPT123", 1000, 1),
            new Product("Mouse", "MSE456", 25, 2),
            new Product("Monitor", "MON789", 200, 1),
            new Product("Keyboard", "KEY101", 50, 1),
            new Product("Headphones", "HD123", 75, 1),
            new Product("External Hard Drive", "EHD789", 100, 1)
        };

        // Create multiple orders for random customers with random products
        for (int i = 0; i < 2; i++) // Change 2 to any number of orders you want to generate
        {
            int randomCustomerIndex = randomGenerator.Next(customers.Count);
            Customer randomCustomer = customers[randomCustomerIndex];

            // Create a new order for the selected random customer
            Order order = new Order(randomCustomer);

            // Randomly select 2-4 products per order
            int numberOfProducts = randomGenerator.Next(2, 5);
            for (int j = 0; j < numberOfProducts; j++)
            {
                int randomProductIndex = randomGenerator.Next(products.Count);
                order.AddProduct(products[randomProductIndex]);
            }

            // Display order details
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine($"Total Cost: ${order.GetTotalCost()}");
            Console.WriteLine();
        }
    }
}
