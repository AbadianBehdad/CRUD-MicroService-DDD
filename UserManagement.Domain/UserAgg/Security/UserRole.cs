namespace UserManagement.Domain.UserAgg.Security
{
    public class UserRole
    {
        public int Id { get; set; }
        public int RoleID { get; set; }
        public Guid UserID { get; set; }
    }
}
