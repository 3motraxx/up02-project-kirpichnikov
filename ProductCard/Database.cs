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
            object? result = ErrorHandler.SafeCall(
                () =>
                {
                    List<Product> products =
                        new List<Product>();

                    using (SQLiteConnection connection =
                           new SQLiteConnection(
                               ConnectionString))
                    {
                        connection.Open();

                        string query =
                            "SELECT * FROM Товар ORDER BY Id";

                        using (SQLiteCommand command =
                               new SQLiteCommand(
                                   query,
                                   connection))
                        using (SQLiteDataReader reader =
                               command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string imagePath = "";

                                if (reader.FieldCount > 7)
                                {
                                    imagePath =
                                        Convert.ToString(
                                            reader.GetValue(7)
                                        ) ?? "";
                                }

                                string description = "";

                                // Ищем поле "Описание",
                                // если оно есть в БД
                                for (int i = 0;
                                     i < reader.FieldCount;
                                     i++)
                                {
                                    string columnName =
                                        reader.GetName(i);

                                    if (columnName.Equals(
                                        "Описание",
                                        StringComparison.OrdinalIgnoreCase))
                                    {
                                        description =
                                            Convert.ToString(
                                                reader.GetValue(i)
                                            ) ?? "";

                                        break;
                                    }
                                }

                                Product product =
                                    new Product(
                                        Convert.ToInt32(
                                            reader.GetValue(0)
                                        ),

                                        Convert.ToString(
                                            reader.GetValue(1)
                                        ) ?? "",

                                        Convert.ToString(
                                            reader.GetValue(2)
                                        ) ?? "",

                                        Convert.ToString(
                                            reader.GetValue(3)
                                        ) ?? "",

                                        Convert.ToDecimal(
                                            reader.GetValue(4)
                                        ),

                                        Convert.ToInt32(
                                            reader.GetValue(5)
                                        ),

                                        imagePath,

                                        description
                                    );

                                products.Add(product);
                            }
                        }
                    }

                    return products;
                }
            );

            return result as List<Product>
                   ?? new List<Product>();
        }
    }
}