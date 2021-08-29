using System;
using System.ComponentModel.DataAnnotations;

namespace GetPet.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime UpdatedTimestamp { get; set; } = DateTime.Now;
        public DateTime CreationTimestamp { get; set; } = DateTime.Now;
    }
}
