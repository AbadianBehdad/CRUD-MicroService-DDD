using Framwork.Domain;
using ShopManagment.Contracts.Product;

namespace ShopManagement.Domain.ProductAgg.Contracts
{
    public interface IProductRepository : IRepository<long, Product>
    {
        //Task AddToDB(CreateProduct product);
        Task<List<ProductViewModel>> GetAllProductsAsync();
        Task<EditProduct> GetDetails(long id);
        Task<List<ProductViewModel>> Search(ProductSearchModel searchModel);
        Task<ProductViewModel> GetProductById(long id);
    }
}
