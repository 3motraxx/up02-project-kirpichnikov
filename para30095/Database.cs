using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace task30095
{
    public class Database
    {
        private const string ConnectionString =
            "Data Source=db_variant_28.db;Version=3;";

        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SQLiteConnection connection =
                   new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Товар ORDER BY Id";

                using (SQLiteCommand command =
                       new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader =
                       command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product(
                            Convert.ToInt32(reader.GetValue(0)),
                            Convert.ToString(reader.GetValue(1)),
                            Convert.ToString(reader.GetValue(2)),
                            Convert.ToDecimal(reader.GetValue(4)),
                            Convert.ToInt32(reader.GetValue(5))
                        );

                        products.Add(product);
                    }
                }
            }

            return products;
        }

        public static decimal GetPriceWithDiscount(
            Product product,
            DateTime date)
        {
            int ordersCount = GetOrdersCountLastMonth(product, date);

            if (ordersCount == 0)
            {
                return product.Price * 0.75m;
            }

            return product.Price;
        }

        public static int GetOrdersCountLastMonth(
            Product product,
            DateTime date)
        {
            using (SQLiteConnection connection =
                   new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = @"
                    SELECT COUNT(*)
                    FROM Заказ
                    WHERE товар_id = @productId
                      AND дата >= date(@date, 'start of month', '-1 month')
                      AND дата < date(@date, 'start of month')";

                using (SQLiteCommand command =
                       new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@productId",
                        product.Id
                    );

                    command.Parameters.AddWithValue(
                        "@date",
                        date.ToString("yyyy-MM-dd")
                    );

                    return Convert.ToInt32(
                        command.ExecuteScalar()
                    );
                }
            }
        }
    }
}