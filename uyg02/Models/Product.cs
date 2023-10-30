namespace uyg02.Models

{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool Status { get; set; }

        public decimal Price { get; set; }
    }
}
