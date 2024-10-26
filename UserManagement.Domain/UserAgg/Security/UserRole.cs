namespace UserManagement.Domain.UserAgg.Security
{
    public class UserRole
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public Guid UserID { get; set; }
    }
}
