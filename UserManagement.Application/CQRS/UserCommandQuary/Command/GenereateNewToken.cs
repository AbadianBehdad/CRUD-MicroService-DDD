using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UserManagement.Application.CQRS.Notifications;
using UserManagenet.EFCore;
using UserMangement.Utility.Encryption;
using UserMangement.Utility.Models;

namespace UserManagement.Application.CQRS.UserCommandQuary.Command
{
    public class GenereateNewToken : IRequest<GenerateNewTokenRespone>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

    public class GenerateNewTokenRespone
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }
    }

    public class GenereateNewTokenHandler : IRequestHandler<GenereateNewToken, GenerateNewTokenRespone>
    {
        private readonly UserContext _userContext;
        private readonly IMediator _mediator;
        private readonly Configs _configs;
        private readonly EncryptionUtility _encryptionUtility;

        public GenereateNewTokenHandler(UserContext userContext,IMediator mediator, IOptions<Configs> options,EncryptionUtility encryptionUtility)
        {
            _userContext = userContext;
            _mediator = mediator;
            _configs = options.Value;
            _encryptionUtility = encryptionUtility;
        }
        public async Task<GenerateNewTokenRespone> Handle(GenereateNewToken request, CancellationToken cancellationToken)
        {
            var RefreshToken = await _userContext.UserRefreshTokens.SingleOrDefaultAsync(x=> x.RefreshToken == request.RefreshToken);
            if (RefreshToken == null) return new GenerateNewTokenRespone { Message = "رفرش توکن معتبر نمیباشد" };

            var newToken = _encryptionUtility.GetNewToken(RefreshToken.UserId);
            var newRefreshToken = _encryptionUtility.GetNewRefreshToken();

            var addRefreshToken = new AddRefreshTokenNotification
            {
                RefreshToken = newRefreshToken,
                RefreshTokenTimeout = _configs.RefreshTokenTimeout,
                UserId = RefreshToken.UserId,
            };
            await _mediator.Publish(addRefreshToken);

            var generateNewToken = new GenerateNewTokenRespone
            {
                RefreshToken = newRefreshToken,
                Token = newToken,
                Message = "توکن و رفرش توکن جدید با موفقیت ساخته شد!"
            };
            return generateNewToken;
        }
    }
}
