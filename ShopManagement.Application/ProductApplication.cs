using AutoMapper;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductAgg.Contracts;
using ShopManagment.Contracts.Product;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductApplication(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Add(CreateProduct command)
        {
            if (await _repository.Exists(x => x.Name == command.Name))
                return;
            //var product = new Product(command.Name, command.Description, command.Price);
            var product = _mapper.Map<Product>(command);
             await _repository.Add(product);
            
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task Edit(EditProduct command)
        {
            if (!await _repository.Exists(x => x.Id == command.Id))
                return;
            var product = await _repository.Get(command.Id);
            product.Edit(command.Name, command.Description,command.Price);
            await _repository.Save();
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            return await _repository.GetAllProductsAsync();
        }

        public async Task<EditProduct> GetDetailsBy(long id)
        {
            return await _repository.GetDetails(id);
        }

        public Task<List<ProductViewModel>> Search(ProductSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}
