namespace ProductCard
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Composition { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }

        public Product(
            int id,
            string name,
            string category,
            string composition,
            decimal price,
            int quantity,
            string imagePath,
            string description = "")
        {
            Id = id;
            Name = name;
            Category = category;
            Composition = composition;
            Price = price;
            Quantity = quantity;
            ImagePath = imagePath;
            Description = description;
        }
    }
}