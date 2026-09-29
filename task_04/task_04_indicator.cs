using System;

namespace task4
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
            List<Product> catalog = new List<Product>
            {
                new Product("Кроссовки", 8500, 3),
                new Product("Ботинки", 15000, 1),
                new Product("Туфли", 12000, 5),
                new Product("Сандалии", 4500, 8),
                new Product("Кеды", 6000, 2)
            };

            var sortedCatalog = catalog.OrderByDescending(p => p.Quantity > 5).ToList();

            Console.WriteLine("Каталог с индикатором:");

            for (int i = 0; i < sortedCatalog.Count; i++)
            {
                Product product = sortedCatalog[i];

                string status = product.Quantity > 5 ? "много" : "мало";

                Console.WriteLine($"{i + 1}. {product.Name,-10} — {product.Quantity} шт. → {status}");
            }
        }
    }
}