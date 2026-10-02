using System;
using System.Collections.Generic;

namespace task30095
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = Database.GetAllProducts();

            TestDiscount(products, new DateTime(2026, 9, 15));
            TestDiscount(products, new DateTime(2026, 9, 20));

            Console.ReadLine();
        }

        static void TestDiscount(List<Product> products, DateTime date)
        {
            Console.WriteLine();
            Console.WriteLine("======================================================================");
            Console.WriteLine(
                $"ТЕСТИРОВАНИЕ АЛГОРИТМА СКИДКИ НА {date:yyyy-MM-dd}"
            );
            Console.WriteLine("======================================================================");

            int passed = 0;

            foreach (Product product in products)
            {
                decimal result = product.PriceWithDiscountAuto(date);

                int ordersCount =
                    Database.GetOrdersCountLastMonth(product, date);

                decimal expected;

                if (ordersCount == 0)
                {
                    expected = product.DiscountedPrice();
                }
                else
                {
                    expected = product.Price;
                }

                string status =
                    result == expected ? "✅" : "❌";

                if (result == expected)
                {
                    passed++;
                }

                string comment;

                if (ordersCount == 0)
                {
                    comment = "Заказов нет → 25% скидка";
                }
                else
                {
                    comment = "Заказы есть → скидки нет";
                }

                Console.WriteLine(
                    $"{status} Товар {product.Id} на {date:yyyy-MM-dd}: " +
                    $"{product.Price} → {result:F2} " +
                    $"(ожидалось {expected:F2}) — " +
                    $"{product.Name} — {comment}"
                );
            }

            Console.WriteLine("======================================================================");
            Console.WriteLine(
                $"Пройдено: {passed} / {products.Count}"
            );
        }
    }
}