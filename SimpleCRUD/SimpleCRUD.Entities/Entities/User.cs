using SimpleCRUD.Entities.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCRUD.Entities.Entities
{
    [Table(name: "Users")]
    public class User : FullAuditedEntity<int>
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
