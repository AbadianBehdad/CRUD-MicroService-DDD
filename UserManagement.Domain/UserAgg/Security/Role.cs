namespace UserManagement.Domain.UserAgg.Security
{
    public class Role
    {
        public int Id { get; set; }
        public string RuleName { get; set; }
        public bool IsActive { get; set; }
    }
}
