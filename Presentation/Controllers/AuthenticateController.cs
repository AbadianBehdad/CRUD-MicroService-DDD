
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.CQRS.UserCommandQuary.Command;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticateController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Login")]
        public async Task<LoginCommandRespond> Post(LoginCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("GenerateNewToken")]
        public async Task<GenerateNewTokenRespone> NewToken(GenereateNewToken command)
        {
            return await _mediator.Send(command);
        }
        
    }
}
