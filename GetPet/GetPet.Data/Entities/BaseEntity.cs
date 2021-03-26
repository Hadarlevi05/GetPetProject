using System;
using System.ComponentModel.DataAnnotations;

namespace GetPet.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public DateTime CreationTimestamp { get; set; }
    }
}
