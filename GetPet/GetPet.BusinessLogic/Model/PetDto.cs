using GetPet.Data.Entities;
using GetPet.Data.Enums;
using System;
using System.Collections.Generic;

namespace GetPet.BusinessLogic.Model
{
    public class PetDto : BaseDto
    {        
        
        public string Name { get; set; }

        public string Birthday { get; set; }
        
        public Gender Gender { get; set; }
                
        //public AnimalType AnimalTypeEnum { get; set; } // TODO Later: Fix AnimalType in Mapper (PetProfile)
        public int AnimalTypeId { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public UserDto User { get; set; } // TODO: How to convert it between Pet and PetDTO?

        public int UserId { get; set; }

        public IList<string> Images { get; set; }

        public IDictionary<string, string> Traits { get; set; }

        public IList<Trait> TraitDTOs { get; set; }

        public string AgeInYears { get; set; }

        public PetSource Source { get; set; }
        /// <summary>
        /// if the source is external we should save the link to the original link
        /// </summary>
        public string SourceLink { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}