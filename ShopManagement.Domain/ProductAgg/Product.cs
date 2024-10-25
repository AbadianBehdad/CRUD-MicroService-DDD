using Framwork.Domain;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product : EntityBase<long>
    {

        public string Name { get; private set; }
        public string? Description { get; private set; }
        public long Price { get; private set; }

        public Product(string name, string? description, long price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
        public void Edit(string name, string? description, long price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }
    
}
