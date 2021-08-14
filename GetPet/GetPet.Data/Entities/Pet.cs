using GetPet.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetPet.Data.Entities
{
    public class Pet : BaseEntity
    {
        [StringLength(400)]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public PetStatus Status { get; set; }
        public string Description { get; set; }

        [ForeignKey("AnimalType")]
        public int AnimalTypeId { get; set; }
        public AnimalType AnimalType { get; set; }
                        
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public IList<MetaFileLink> MetaFileLinks { get; set; }
        public IList<PetTrait> PetTraits { get; set; }
        public IList<PetHistoryStatus> PetHistoryStatuses { get; set; }        

        public PetSource Source { get; set; }
        /// <summary>
        /// if the source is external we should save the link to the original link
        /// </summary>
        public string SourceLink { get; set; }
        public string ExternalId { get; set; }
    }
}