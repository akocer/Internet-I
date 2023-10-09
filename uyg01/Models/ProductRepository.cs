namespace uyg01.Models
{
    public class ProductRepository
    {
        public static List<Product> products = new List<Product> {
            new Product() {Id=1,Name="Kalem", Description="Kalem açıklama", Status=true },
            new Product() {Id=2,Name="Defter", Description="Defter açıklama", Status=true },
            new Product() {Id=3,Name="Silgi", Description="Silgi açıklama", Status=false },
            new Product() {Id=4,Name="Kitap", Description="Kitap açıklama", Status=false },
            new Product() {Id=5,Name="Boya", Description="Boya açıklama", Status=false }
        };

        public List<Product> GetList()
        {
            return products;
        }
        public Product GetById(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);
            return product;
        }

        public void Add(Product model)
        {
            Random rn = new Random();
            int id = rn.Next(1000);
            model.Id = id;
            products.Add(model);
        }

        public void Update(Product model)
        {
            var product = GetById(model.Id);
            if (product != null)
            {
                var index = products.FindIndex(x => x.Id == product.Id);
                product.Name = model.Name;
                product.Description = model.Description;
                product.Status = model.Status;
                products[index] = product;
            }

        }
        public void Remove(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                products.Remove(product);
            }
        }

    }

}
