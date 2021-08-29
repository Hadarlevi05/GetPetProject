using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GetPet.Data.Entities
{
    public class Notification : BaseEntity
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public string PetFilterSerialized { get; set; }
    }
}
