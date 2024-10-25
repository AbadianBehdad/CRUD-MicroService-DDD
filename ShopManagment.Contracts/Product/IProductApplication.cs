namespace ShopManagment.Contracts.Product
{
    public interface IProductApplication
    {
        Task Add(CreateProduct command);
        Task Delete(long id);
        Task Edit(EditProduct command);
        Task<EditProduct> GetDetailsBy(long id);
        Task<List<ProductViewModel>> GetAll();
        Task<List<ProductViewModel>> Search(ProductSearchModel searchModel);


    }
}
