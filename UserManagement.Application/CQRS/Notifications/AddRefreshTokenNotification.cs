using MediatR;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.UserAgg;
using UserManagement.Domain.UserAgg.Security;
using UserManagenet.EFCore;

namespace UserManagement.Application.CQRS.Notifications;

public class AddRefreshTokenNotification : INotification
{
    public string RefreshToken { get; set; }
    public Guid UserId { get; set; }
    public int RefreshTokenTimeout { get; set; }
}
public class AddRefreshTokenNotificationHandler : INotificationHandler<AddRefreshTokenNotification>
{
    private readonly UserContext _userContext;

    public AddRefreshTokenNotificationHandler(UserContext userContext)
    {
        _userContext = userContext;
    }
    public async Task Handle(AddRefreshTokenNotification notification, CancellationToken cancellationToken)
    {
        var UserRefreshTolken = new UserRefreshToken(notification.UserId, notification.RefreshToken,notification.RefreshTokenTimeout);
        var c = await _userContext.UserRefreshTokens.SingleOrDefaultAsync(x => x.UserId == notification.UserId);
        if (c == null)
        {
             await _userContext.UserRefreshTokens.AddAsync(UserRefreshTolken);
        }
        else 
        {
            c.Edit(notification.RefreshToken, notification.RefreshTokenTimeout);
        }
         await _userContext.SaveChangesAsync();
        
    }
}
