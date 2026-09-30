using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace task3009
{
    public class Database
    {
        private const string ConnectionString =
            "Data Source=db_variant_28.db;Version=3;";

        // Все товары
        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SQLiteConnection connection =
                   new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Товар ORDER BY id";

                using (SQLiteCommand command =
                       new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader =
                       command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product(
                            reader.GetString(2),
                            reader.GetDecimal(4),
                            reader.GetInt32(5)
                        );

                        products.Add(product);
                    }
                }
            }

            return products;
        }

        public static List<Product> GetProductsByCategory(string category)
        {
            List<Product> products = new List<Product>();

            using (SQLiteConnection connection =
                   new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query =
                    "SELECT * FROM Товар WHERE категория = @category";

                using (SQLiteCommand command =
                       new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@category", category);

                    using (SQLiteDataReader reader =
                           command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product(
                                reader.GetString(2),
                                reader.GetDecimal(4),
                                reader.GetInt32(5)
                            );

                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }


        public static List<Product> GetProductsLowStock()
        {
            List<Product> products = new List<Product>();

            using (SQLiteConnection connection =
                   new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query =
                    "SELECT * FROM Товар WHERE количество <= 3";

                using (SQLiteCommand command =
                       new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader =
                       command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product(
                            reader.GetString(2),
                            reader.GetDecimal(4),
                            reader.GetInt32(5)
                        );

                        products.Add(product);
                    }
                }
            }

            return products;
        }
    }
}