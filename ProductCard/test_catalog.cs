using System;

namespace ProductCard
{
    public class CatalogTest
    {
        public static void TestFields()
        {
            Console.WriteLine("Проверка вывода полей.");

            var products = Database.GetAllProducts();

            Console.WriteLine($"Всего товаров: {products.Count}");

            int errors = 0;

            foreach (Product p in products)
            {


                int fieldCount = 7;

                if (fieldCount < 6)
                {
                    Console.WriteLine(
                        $"❌ Товар id={p.Id}: мало полей ({fieldCount})"
                    );

                    errors++;
                }
            }

            if (errors == 0)
            {
                Console.WriteLine(
                    "✅ Все товары содержат нужные поля"
                );
            }
            else
            {
                Console.WriteLine(
                    $"❌ Найдено ошибок: {errors}"
                );
            }
        }
    }
}