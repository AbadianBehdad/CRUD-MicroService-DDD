using MediatR;
using Microsoft.Extensions.Options;
using UserManagement.Application.CQRS.Notifications;
using UserManagement.Domain.UserAgg.Contracts;
using UserMangement.Utility.Encryption;
using UserMangement.Utility.Models;

namespace UserManagement.Application.CQRS.UserCommandQuary.Command;

public class LoginCommand : IRequest<LoginCommandRespond>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
public class LoginCommandRespond
{
    public string UserName { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public int ExpireTime { get; set; }
    public string Message { get; set; }
}
public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandRespond>
{
    private readonly IUserRepository _userRepository;
    private readonly EncryptionUtility _encryptionUtility;
    private readonly IMediator _mediator;
    private readonly Configs _configs;

    public LoginCommandHandler(IUserRepository userRepository, EncryptionUtility encryptionUtility, IOptions<Configs> options, IMediator mediator)
    {
        _userRepository = userRepository;
        _encryptionUtility = encryptionUtility;
        _mediator = mediator;
        _configs = options.Value;
    }
    public async Task<LoginCommandRespond> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.Exists(x => x.Name == request.UserName)) return new LoginCommandRespond { Message = "یوزر با این نام کاربری وجود ندارد" };
        var user = await _userRepository.GetBy(request.UserName);
        var hashPassword = _encryptionUtility.GetSHA256(request.Password, user.PasswordSalt);
        if (user.Password != hashPassword) return new LoginCommandRespond { Message = "پسورد اشتباه است" };

        user.UpdateLastLogin();
        await _userRepository.Save();
        var token = _encryptionUtility.GetNewToken(user.Id);
        var refreshToken = _encryptionUtility.GetNewRefreshToken();
        var addRefreshToken = new AddRefreshTokenNotification
        {
            RefreshToken = refreshToken,
            RefreshTokenTimeout = _configs.RefreshTokenTimeout,
            UserId = user.Id,
        };
        await _mediator.Publish(addRefreshToken);


        var respone = new LoginCommandRespond
        {
            UserName = user.Name,
            Token = token,
            RefreshToken = refreshToken,
            Message = "عملیات با موفقیت به انجام رسید",
            ExpireTime = _configs.TokenTimeout,
        };
        return respone;
         
    }
}
