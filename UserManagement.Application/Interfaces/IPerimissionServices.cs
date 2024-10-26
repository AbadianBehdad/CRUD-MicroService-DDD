namespace UserManagement.Application.Interfaces
{
    public interface IPerimissionServices
    {
        public Task<bool> CheckPerimission(Guid userId, string perimission);
    }
}
