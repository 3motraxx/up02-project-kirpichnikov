using System;

namespace task02s
{
    class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public decimal GetTotalPrice()
        {
            return Price * Quantity;
        }

        public string GetInfo()
        {
            return $"{Name}: {Price} * {Quantity} = {GetTotalPrice()} руб.";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Product product1 = new Product("Кроссовки ", 8500, 3);
            Product product2 = new Product("Ботинки", 15000, 1);
            Product product3 = new Product("Туфли", 12000, 5);

            Console.WriteLine(product1.GetInfo());
            Console.WriteLine(product2.GetInfo());
            Console.WriteLine(product3.GetInfo());
        }
    }
}