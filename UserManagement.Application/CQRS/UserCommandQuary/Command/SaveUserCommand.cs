using MediatR;
using System.Data;
using System.Net;
using UserManagement.Domain.UserAgg;
using UserManagement.Domain.UserAgg.Contracts;
using UserMangement.Utility.Encryption;

namespace UserManagement.Application.CQRS.UserCommandQuary.Command
{
    public class SaveUserCommand : IRequest<SaveUserCommandRespond>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    
    }
    public class SaveUserCommandRespond
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }

    public class SaveUserCommandHandler : IRequestHandler<SaveUserCommand, SaveUserCommandRespond>
    {
        private readonly IUserRepository _repository;
        private readonly EncryptionUtility _encryptionUtility;

        public SaveUserCommandHandler(IUserRepository repository, EncryptionUtility encryptionUtility)
        {
            _repository = repository;
            _encryptionUtility = encryptionUtility;
        }
        public async Task<SaveUserCommandRespond> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            if (await _repository.Exists(x => x.Name == request.Name))
            {
                var res = new SaveUserCommandRespond {Message = "نام کاربری تکراری است"};
                return res;
            }
            var salt = _encryptionUtility.GetNewSalt();
            var hashpassword = _encryptionUtility.GetSHA256(request.Password, salt);
            var user = new User(request.Name, request.Email, hashpassword, salt);
            await _repository.Add(user);
            var result = new SaveUserCommandRespond { Id = user.Id, Message="عملیات با موفقیت به انجام رسید!" };
            return result;
        }
    }
}
