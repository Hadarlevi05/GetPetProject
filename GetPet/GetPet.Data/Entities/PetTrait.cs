using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetPet.Data.Entities
{
    public class PetTrait : BaseEntity
    {
        [ForeignKey("Pet")]
        public int PetId { get; set; }
        public Pet Pet { get; set; }

        [ForeignKey("Trait")]
        public int TraitId { get; set; }
        public Trait Trait { get; set; }

        public int? TraitOptionId { get; set; }
        
        [StringLength(400)]
        public string Description { get; set; }

    }
}
