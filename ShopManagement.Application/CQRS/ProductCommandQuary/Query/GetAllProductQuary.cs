using AutoMapper;
using MediatR;
using ShopManagement.Domain.ProductAgg.Contracts;
namespace ShopManagement.Application.CQRS.ProductCommandQuary.Query;

public class GetAllProductQuary : IRequest<List<GetAllProductQuaryRespond>>
{
        
}
public class GetAllProductQuaryRespond
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long Price { get; set; }
    public string CreationDate { get; set; }
}

public class GetAllProductQuaryHandler : IRequestHandler<GetAllProductQuary, List<GetAllProductQuaryRespond>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductQuaryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<List<GetAllProductQuaryRespond>> Handle(GetAllProductQuary request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAllProductsAsync();
        var result = _mapper.Map<List<GetAllProductQuaryRespond>>(product);
        return result;
    }
}