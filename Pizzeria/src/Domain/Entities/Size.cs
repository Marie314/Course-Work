using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Domain.Entities
{
    [Table("Sizes")]
    public class Size
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
