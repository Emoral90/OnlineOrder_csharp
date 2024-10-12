using System;
using System.Collections.Generic;

class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    // Check if the address is in the USA
    public bool IsInUSA()
    {
        return country.ToLower() == "usa";
    }

    // Return address as a formatted string
    public string GetAddressString()
    {
        return $"{street}\n{city}, {state}\n{country}";
    }
}

class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    // Check if customer lives in the USA
    public bool IsInUSA()
    {
        return address.IsInUSA();
    }

    // Return customer's name
    public string GetName()
    {
        return name;
    }

    // Get shipping address
    public string GetShippingAddress()
    {
        return address.GetAddressString();
    }
}

class Product
{
    private string name;
    private string productId;
    private double price;
    private int quantity;

    public Product(string name, string productId, double price, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.price = price;
        this.quantity = quantity;
    }

    // Calculate total cost for product
    public double GetTotalCost()
    {
        return price * quantity;
    }

    // Return product details for label
    public string GetPackingLabel()
    {
        return $"{name} (ID: {productId})";
    }
}

class Order
{
    private List<Product> products;
    private Customer customer;

    public Order(Customer customer)
    {
        this.customer = customer;
        products = new List<Product>();
    }

    // Add product to order
    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    // Calculate total cost of order
    public double GetTotalPrice()
    {
        double total = 0;
        foreach (var product in products)
        {
            total += product.GetTotalCost();
        }

        // Add national and international shipping fees
        if (customer.IsInUSA())
        {
            total += 5;
        }
        else
        {
            total += 35;
        }

        return total;
    }

    // Generate packing label
    public string GetPackingLabel()
    {
        string packingLabel = "Packing Label:\n";
        foreach (var product in products)
        {
            packingLabel += $"{product.GetPackingLabel()}\n";
        }
        return packingLabel;
    }

    // Generate shipping label
    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{customer.GetName()}\n{customer.GetShippingAddress()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Creating address objects
        Address address1 = new Address("123 Main St", "Springfield", "IL", "USA");
        Address address2 = new Address("456 Maple Ave", "Toronto", "ON", "Canada");

        // Creating customer objects
        Customer customer1 = new Customer("John Doe", address1);
        Customer customer2 = new Customer("Jane Smith", address2);

        // Creating product objects
        Product product1 = new Product("Laptop", "P001", 999.99, 1);
        Product product2 = new Product("Mouse", "P002", 49.99, 2);
        Product product3 = new Product("Keyboard", "P003", 79.99, 1);

        Product product4 = new Product("Phone", "P004", 699.99, 1);
        Product product5 = new Product("Charger", "P005", 29.99, 2);

        // Creating order objects and adding products
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);
        order1.AddProduct(product3);

        Order order2 = new Order(customer2);
        order2.AddProduct(product4);
        order2.AddProduct(product5);

        // Displaying order details
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalPrice()}\n");

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalPrice()}\n");
    }
}
