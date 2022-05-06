using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Domain.Entities
{
    [Table("Roles")]
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
