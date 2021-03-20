using System.ComponentModel.DataAnnotations.Schema;

namespace GetPet.Data.Entities
{
    public class AnimalTrait : BaseEntity
    {
        [ForeignKey("Trait")]
        public int TraitId { get; set; }
        public Trait Trait { get; set; }

        [ForeignKey("AnimalType")]
        public int AnimalTypeId { get; set; }
        public AnimalType AnimalType { get; set; }
    }
}
