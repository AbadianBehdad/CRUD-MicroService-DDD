using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.CQRS.UserCommandQuary.Command;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("Add")]
        public async Task<SaveUserCommandRespond> Add(SaveUserCommand command) 
        {
            return await _mediator.Send(command);
            
        }

        [HttpPost("UpdateUserName")]
        public async Task<ChangePasswordCommandRespond> UpdateUserName(ChangeUserNameCommand command) 
        {
            return await _mediator.Send(command);
        }
    }
}
