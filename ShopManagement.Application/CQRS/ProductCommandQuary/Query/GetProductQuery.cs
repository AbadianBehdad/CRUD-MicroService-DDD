using AutoMapper;
using MediatR;
using ShopManagement.Domain.ProductAgg.Contracts;

namespace ShopManagement.Application.CQRS.ProductCommandQuary.Query;

public class GetProductQuery : IRequest<GetProductQueryRespond>
{
    public long Id { get; set; }
}
public class GetProductQueryRespond
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long Price { get; set; }
    public string CreationDate { get; set; }
}
public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductQueryRespond>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetProductQueryRespond> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductById(request.Id);
        var result = _mapper.Map<GetProductQueryRespond>(product);

        return result;
    }
}
