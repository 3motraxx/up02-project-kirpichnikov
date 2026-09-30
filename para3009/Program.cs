using System;
using System.Collections.Generic;
using System.Linq;

namespace task3009
{
    public class Product
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
        public decimal PriceWithDiscount(decimal sale)
        {
            return Price * (1 - sale / 100);
        }
    }


    public class Catalog
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Все товары:");

            List<Product> products = Database.GetAllProducts();

            Console.WriteLine($"КАТАЛОГ ({products.Count} товаров)");
            Console.WriteLine(new string('=', 70));

            foreach (Product product in products)
            {
                string highlight = product.Quantity <= 3 ? "!!!!" : "  ";

                Console.WriteLine($"{highlight} {product.GetInfo()}");
            }

            Console.WriteLine(new string('=', 70));


            Console.WriteLine("\n2. Товары категории Розы:");

            List<Product> roses = Database.GetProductsByCategory("Розы");

            Console.WriteLine($"КАТАЛОГ ({roses.Count} товаров)");
            Console.WriteLine(new string('=', 70));

            foreach (Product product in roses)
            {
                string highlight = product.Quantity <= 3 ? "" : "";

                Console.WriteLine($"{highlight} {product.GetInfo()}");
            }

            Console.WriteLine(new string('=', 70));


            Console.WriteLine("\n3. Товары с низким остатком (≤3):");

            List<Product> lowStock = Database.GetProductsLowStock();

            Console.WriteLine($"КАТАЛОГ ({lowStock.Count} товаров)");
            Console.WriteLine(new string('=', 70));

            foreach (Product product in lowStock)
            {
                Console.WriteLine($"{product.GetInfo()}");
            }

            Console.WriteLine(new string('=', 70));
        }
    }
}



