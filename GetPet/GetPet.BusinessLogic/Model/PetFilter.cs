using GetPet.Data.Entities;
using System;
using System.Collections.Generic;

namespace GetPet.BusinessLogic.Model
{
    public class PetFilter : BaseFilter
    {
        public IEnumerable<AnimalType> AnimalTypes { get; set; }

        public IDictionary<int, string> TraitValues { get; set; }

        public DateTime? CreatedSince { get; set; } = null;
    }
}