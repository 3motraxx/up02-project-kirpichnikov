using System;

namespace task30095
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(
            int id,
            string name,
            string category,
            decimal price,
            int quantity)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
            Quantity = quantity;
        }

        public decimal GetTotalPrice()
        {
            return Price * Quantity;
        }

        public decimal PriceWithDiscountAuto(DateTime? date = null)
        {
            DateTime calculationDate = date ?? DateTime.Now;

            return Database.GetPriceWithDiscount(
                this,
                calculationDate
            );
        }

        public string GetInfo()
        {
            string indicator = Quantity > 5 ? "много" : "мало";

            return $"{Name} ({Category}): " +
                   $"{Price} руб. × {Quantity} = " +
                   $"{GetTotalPrice()} руб. ({indicator})";
        }
    }
}