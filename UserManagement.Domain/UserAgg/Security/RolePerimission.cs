using System.Security;

namespace UserManagement.Domain.UserAgg.Security
{
    public class RolePerimission
    {
        public int Id { get; set; }
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }
        public int PerimissionId { get; set; }
        public virtual Perimission Perimission { get; set; }

    }
}
