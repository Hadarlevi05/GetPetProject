using System.ComponentModel.DataAnnotations;

namespace GetPet.Data.Entities
{
    public class Trait : BaseEntity
    {
        [StringLength(400)]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
