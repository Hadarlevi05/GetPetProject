﻿using System.ComponentModel.DataAnnotations.Schema;

namespace GetPet.Data.Entities
{
    public class TraitOption : BaseEntity
    {
        [ForeignKey("Trait")]
        public int TraitId { get; set; }
        public Trait Trait { get; set; }

        public string Option { get; set; }
    }
}
