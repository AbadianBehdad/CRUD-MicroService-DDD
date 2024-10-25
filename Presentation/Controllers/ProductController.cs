using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopManagment.Contracts.Product;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;

        public ProductController(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        [HttpPost("Add")]
        public async Task AddProduct(CreateProduct command)
        {
            await _productApplication.Add(command);

        }


        [HttpGet("GetALl")]
        public async Task<List<ProductViewModel>> GetAll() 
        {
            return await _productApplication.GetAll();
        }

        [HttpPost("Edit")]
        public async Task EditProduct(EditProduct command)
        {
            await _productApplication.Edit(command);

        }
        [HttpPost("Search")]
        public async Task<List<ProductViewModel>> Search(ProductSearchModel searchModel)
        {
            return await _productApplication.Search(searchModel);
        }

    }
}
