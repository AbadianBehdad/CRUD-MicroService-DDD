using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopManagement.Application.CQRS.ProductCommandQuary.Command;
using ShopManagement.Application.CQRS.ProductCommandQuary.Query;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CQRSProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CQRSProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddProduct")]
        public async Task<SaveProductCommandResponde> Create(SaveProductCommand command) 
        {
            return await _mediator.Send(command);
            

        }
        [HttpGet("Get")]
        public async Task<GetProductQueryRespond> Get([FromQuery]GetProductQuery command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("GetAll")]
        public async Task<List<GetAllProductQuaryRespond>> GetAll()
        {
            return await _mediator.Send(new GetAllProductQuary());
        }
    }
}
