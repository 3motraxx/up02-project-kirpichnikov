using System;

namespace task3
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

    public class Catalog
    {
        static void Main(string[] args)
        {
            Product product1 = new Product("Кроссовки", 8500, 3);
            Product product2 = new Product("Ботинки", 15000, 1);
            Product product3 = new Product("Туфли", 12000, 5);
            Product product4 = new Product("Сандалии", 4500, 8);
            Product product5 = new Product("Кеды", 6000, 2);

            Console.WriteLine("Каталог товаров:");
            Console.WriteLine($"1. {product1.GetInfo()}");
            Console.WriteLine($"2. {product2.GetInfo()}");
            Console.WriteLine($"3. {product3.GetInfo()}");
            Console.WriteLine($"4. {product4.GetInfo()}");
            Console.WriteLine($"5. {product5.GetInfo()}");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"Итого: {product1.GetTotalPrice() + product2.GetTotalPrice() + product3.GetTotalPrice() + product4.GetTotalPrice() + product5.GetTotalPrice()}   руб.");
        }
    }
}