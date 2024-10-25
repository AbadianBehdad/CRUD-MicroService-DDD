using MediatR;
using UserManagement.Domain.UserAgg.Contracts;

namespace UserManagement.Application.CQRS.UserCommandQuary.Command
{
    public class ChangeUserNameCommand : IRequest<ChangePasswordCommandRespond>
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
    public class ChangePasswordCommandRespond
    {
        public string Message { get; set; }
    }
    public class ChangePasswordCommandHandler : IRequestHandler<ChangeUserNameCommand, ChangePasswordCommandRespond>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(IUserRepository userRepository)
        {
              _userRepository = userRepository;
        }

        public async Task<ChangePasswordCommandRespond> Handle(ChangeUserNameCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetBy(request.OldName);
            if (user == null)
            {
                return new ChangePasswordCommandRespond { Message = "یوزر با این نام یافت نشد!" };

            }
            user.ChangeUserName(request.NewName);
            await _userRepository.Save();
            return new ChangePasswordCommandRespond { Message = "عملیات با موفقیت انجام شد" };
        }
    }
}
