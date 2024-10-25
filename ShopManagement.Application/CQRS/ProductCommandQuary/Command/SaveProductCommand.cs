using AutoMapper;
using MediatR;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductAgg.Contracts;

namespace ShopManagement.Application.CQRS.ProductCommandQuary.Command
{
    public class SaveProductCommand : IRequest<SaveProductCommandResponde>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
    }
    public class SaveProductCommandResponde
    {
        public long Id { get; set; }
        public string? Message { get; set; }
    }

    public class SaveProductHandler : IRequestHandler<SaveProductCommand, SaveProductCommandResponde>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public SaveProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<SaveProductCommandResponde> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            if (await _productRepository.Exists(x => x.Name == request.Name))
                return new SaveProductCommandResponde { Id = 0, Message = "Duplicated Name" };
            //var product = new Product(request.Name, request.Description, request.Price);
            var product = _mapper.Map<Product>(request);
            await _productRepository.Add(product);
            return new SaveProductCommandResponde { Id = product.Id, Message = "Product sucessfully add" };

        }
    }
}
