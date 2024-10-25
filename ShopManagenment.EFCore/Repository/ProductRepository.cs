using Framwork.Infrastructre;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductAgg.Contracts;
using ShopManagment.Contracts.Product;

namespace ShopManagenment.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
    {
        private readonly ShopContext _shopContext;
        public ProductRepository(ShopContext context) : base(context)
        {
            _shopContext = context;
        }

        public async Task<List<ProductViewModel>> GetAllProductsAsync()
        {
            return  _shopContext.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                CreationDate = x.CreationDate.ToString(),

            }).ToList();
        }

        public Task<EditProduct> GetDetails(long id)
        {
            return _shopContext.Products.Select(x => new EditProduct
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                
            }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<ProductViewModel> GetProductById(long id)
        {
            return _shopContext.Products.Select(x=> new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                CreationDate = x.CreationDate.ToString()

            }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<ProductViewModel>> Search(ProductSearchModel searchModel)
        {
            var query = _shopContext.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                CreationDate = x.CreationDate.ToString()
            });
            if (!string.IsNullOrWhiteSpace(searchModel.Name) && searchModel.Name != "string")
                query = query.Where(X => X.Name.Contains(searchModel.Name));
            if(searchModel.Id != 0)
                query = query.Where(x => x.Id == searchModel.Id);
            return query.OrderByDescending(x => x.Id).ToListAsync();

        }
    }
}
