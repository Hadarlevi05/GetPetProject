using GetPet.Data.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace GetPet.BusinessLogic.Model
{
    public class PetDto : BaseDto
    {        
        
        public string Name { get; set; }

        public DateTime Birthday { get; set; }
        
        public Gender Gender { get; set; }
                
        public int AnimalTypeId { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public UserDto User { get; set; } 

        public IList<string> Images { get; set; }

        //public IFormFile formFiles { get; set; }

        public IDictionary<string, string> Traits { get; set; }

        public IDictionary<string, string> BooleanTraits { get; set; }

        public IList<PetHistoryStatusDto> PetHistoryStatuses { get; set; }

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