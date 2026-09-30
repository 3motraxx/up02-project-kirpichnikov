using System;

namespace task30095
{
    class Program
    {
        static void Main(string[] args)
        {
            Product p = new Product(
                2,
                "Ботинки Timberland",
                "Ботинки",
                15000,
                3
            );

            DateTime date = new DateTime(2026, 09, 15);

            Console.WriteLine($"Базовая цена: {p.Price}");
            Console.WriteLine(
                $"Со скидкой: {p.PriceWithDiscountAuto(date):F1}"
            );
        }
    }
}