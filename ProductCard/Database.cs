using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace ProductCard
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
                            Convert.ToString(reader.GetValue(1)) ?? "",
                            Convert.ToString(reader.GetValue(2)) ?? "",
                            Convert.ToString(reader.GetValue(3)) ?? "",
                            Convert.ToDecimal(reader.GetValue(4)),
                            Convert.ToInt32(reader.GetValue(5)),
                            reader.FieldCount > 7
                                ? Convert.ToString(reader.GetValue(7)) ?? ""
                                : ""
                        );

                        products.Add(product);
                    }
                }
            }

            return products;
        }
    }
}