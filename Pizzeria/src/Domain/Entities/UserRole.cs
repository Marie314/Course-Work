using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Domain.Entities
{
    [Table("UserRoles")]
    public class UserRole
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }
    }
}
