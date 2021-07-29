using System.ComponentModel.DataAnnotations;

namespace GetPet.Data.Entities
{
    public class Organization: BaseEntity
    {
        [StringLength(400)]
        public string Name { get; set; }
        
        [StringLength(400)]
        public string Email { get; set; }

        [StringLength(400)]
        public string PhoneNumber { get; set; }
    }
}