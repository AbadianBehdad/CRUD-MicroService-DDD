using AutoMapper;
using ShopManagement.Application.CQRS.ProductCommandQuary.Command;
using ShopManagement.Application.CQRS.ProductCommandQuary.Query;
using ShopManagement.Domain.ProductAgg;
using ShopManagment.Contracts.Product;

namespace ShopManagement.Application.AutoMapper
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Product, CreateProduct>().ReverseMap();
            CreateMap<Product, SaveProductCommand>().ReverseMap();
            CreateMap<ProductViewModel, GetProductQueryRespond>().ReverseMap();
            CreateMap<ProductViewModel, GetAllProductQuaryRespond>().ReverseMap();

        }

        
    }
}
