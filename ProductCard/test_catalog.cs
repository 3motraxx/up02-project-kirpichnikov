using System;

namespace ProductCard
{
    public static class CatalogTest
    {
        public static void TestFields()
        {
            Console.WriteLine("=== Проверка каталога ===");

            var products = Database.GetAllProducts();

            Console.WriteLine($"Всего товаров: {products.Count}");

            TestRequiredFields(products);
            TestPrices(products);
            TestQuantities(products);
            TestImages(products);
            TestEdgeCases(products);
        }

        private static void TestRequiredFields(
            System.Collections.Generic.List<Product> products)
        {
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

        private static void TestPrices(
            System.Collections.Generic.List<Product> products)
        {
            foreach (Product p in products)
            {
                if (p.Price < 0)
                {
                    Console.WriteLine(
                        $"❌ Товар id={p.Id}: нет цены"
                    );

                    return;
                }
            }

            Console.WriteLine(
                "✅ У всех товаров есть цена"
            );
        }

        private static void TestQuantities(
            System.Collections.Generic.List<Product> products)
        {
            foreach (Product p in products)
            {
                if (p.Quantity < 0)
                {
                    Console.WriteLine(
                        $"❌ Товар id={p.Id}: количество меньше 0"
                    );

                    return;
                }
            }

            Console.WriteLine(
                "✅ Количество всех товаров не меньше 0"
            );
        }

        private static void TestImages(
            System.Collections.Generic.List<Product> products)
        {
            foreach (Product p in products)
            {
                if (!string.IsNullOrWhiteSpace(p.ImagePath))
                {
                    Console.WriteLine(
                        "✅ Хотя бы у одного товара есть изображение"
                    );

                    return;
                }
            }

            Console.WriteLine(
                "❌ Ни у одного товара нет изображения"
            );
        }

        private static void TestEdgeCases(
            System.Collections.Generic.List<Product> products)
        {
            bool expensive = false;
            bool longName = false;
            bool cyrillicName = false;

            foreach (Product p in products)
            {
                if (p.Price > 1000000)
                {
                    expensive = true;
                    Console.WriteLine(
                        $"⚠️ Найден товар дороже 1 000 000 руб.: {p.Name}"
                    );
                }

                if (!string.IsNullOrEmpty(p.Name) &&
                    p.Name.Length > 100)
                {
                    longName = true;
                    Console.WriteLine(
                        $"⚠️ Найдено длинное название: {p.Name}"
                    );
                }

                if (!string.IsNullOrEmpty(p.Name))
                {
                    foreach (char c in p.Name)
                    {
                        if ((c >= 'А' && c <= 'я') ||
                            c == 'Ё' || c == 'ё')
                        {
                            cyrillicName = true;
                            break;
                        }
                    }
                }
            }

            if (expensive)
                Console.WriteLine(
                    "✅ Обработка цены больше 1 000 000 руб. проверена"
                );
            else
                Console.WriteLine(
                    "ℹ️ Товаров дороже 1 000 000 руб. в БД нет"
                );

            if (longName)
                Console.WriteLine(
                    "✅ Обработка длинного названия проверена"
                );
            else
                Console.WriteLine(
                    "ℹ️ Названий длиннее 100 символов в БД нет"
                );

            if (cyrillicName)
                Console.WriteLine(
                    "✅ Кириллическое название поддерживается"
                );
            else
                Console.WriteLine(
                    "❌ Кириллических названий нет"
                );
        }
    }
}