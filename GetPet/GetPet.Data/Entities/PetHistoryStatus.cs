using GetPet.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetPet.Data.Entities
{
    public class PetHistoryStatus: BaseEntity
    {
        [ForeignKey("Pet")]
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        
        public PetStatus Status { get; set; }
        
        [StringLength(4000)]
        public string Remarks { get; set; }
    }
}