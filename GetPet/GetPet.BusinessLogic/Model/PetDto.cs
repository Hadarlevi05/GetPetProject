using System;
using System.Collections.Generic;

namespace GetPet.BusinessLogic.Model
{
    public class PetDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Birthday { get; set; }
        
        public string Gender { get; set; }
                
        public string AnimalType { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public UserDto User { get; set; }

        public IList<string> Images { get; set; }

        public IDictionary<string, string> Traits { get; set; }

        public string AgeInYears { get; set; }

        public string AgeInMonths { get; set; }       
    }
}