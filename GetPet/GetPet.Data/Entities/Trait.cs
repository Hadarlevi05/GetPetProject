using GetPet.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GetPet.Data.Entities
{
    public class Trait : BaseEntity
    {
        [StringLength(400)]
        public string Name { get; set; }

        public int AnimalTypeId { get; set; }

        public List<TraitOption> TraitOptions { get; set; }

        public TraitType TraitType { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
