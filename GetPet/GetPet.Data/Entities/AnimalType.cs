using System.ComponentModel.DataAnnotations;

namespace GetPet.Data.Entities
{
    public class AnimalType : BaseEntity
    {
        [StringLength(400)]
        public string Name { get; set; }
    }
}
