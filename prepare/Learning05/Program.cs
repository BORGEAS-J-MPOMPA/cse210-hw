using System;
using System.Collections.Generic;
class Program
{
    static void Main(string[] args)
    {
        // Create customer and address
        Address address1 = new Address("123 Main St", "New York", "NY", "USA");
        Customer customer1 = new Customer("Ludovic Baron", address1);

        Address address2 = new Address("456 Maple Ave", "Toronto", "ON", "Canada");
        Customer customer2 = new Customer("Laurita Smith", address2);

        // Create products
        Product product1 = new Product("Laptop", "LPT123", 1000, 1);
        Product product2 = new Product("Mouse", "MSE456", 25, 2);

        Product product3 = new Product("Monitor", "MON789", 200, 1);
        Product product4 = new Product("Keyboard", "KEY101", 50, 1);

        // Create order1 for customer1
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        // Create order2 for customer2
        Order order2 = new Order(customer2);
        order2.AddProduct(product3);
        order2.AddProduct(product4);

        // Display order1 details
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.GetTotalCost()}");

        Console.WriteLine();

        // Display order2 details
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.GetTotalCost()}");
    }

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

}
