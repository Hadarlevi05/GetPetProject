using System.ComponentModel.DataAnnotations.Schema;

namespace GetPet.Data.Entities
{
    public class EmailHistory : BaseEntity
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
