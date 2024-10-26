namespace UserManagement.Domain.UserAgg.Security;

public class UserRefreshToken
{
    public UserRefreshToken(Guid userId, string refreshToken, int refreshTokenTimeout)
    {
        UserId = userId;

        RefreshToken = refreshToken;
        RefreshTokenTimeout = refreshTokenTimeout;
        CreationDate = DateTime.Now;
        IsValid = true;
    }
    public void Invalid()
    {
        IsValid = false;
    }
    public void Edit(string refreshToken, int refreshTokenTimeout)
    {
        RefreshToken = refreshToken;
        RefreshTokenTimeout = refreshTokenTimeout;
        CreationDate = DateTime.Now;
        IsValid = true;
    }

    public long Id { get; private set; }
    public Guid UserId { get; private set; }
    public virtual User User { get; private set; }
    public string RefreshToken { get; private set; }
    public int RefreshTokenTimeout { get; private set; }

    public DateTime CreationDate { get; private set; }
    public bool IsValid { get; private set; }


}
