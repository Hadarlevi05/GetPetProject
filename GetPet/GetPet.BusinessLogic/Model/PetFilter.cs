using GetPet.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model
{
    public class PetFilter
    {
        public IEnumerable<AnimalType> AnimalTypes { get; set; }

        public IDictionary<int, string> TraitValues { get; set; }





    }
}
