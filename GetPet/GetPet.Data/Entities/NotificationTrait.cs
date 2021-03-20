using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetPet.Data.Entities
{
    public class NotificationTrait : BaseEntity
    {
        [ForeignKey("Notification")]
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
        
        [ForeignKey("Trait")]
        public int TraitId { get; set; }
        public Trait Trait { get; set; }

        [StringLength(400)]
        public string Value { get; set; }
    }
}