using GetPet.Data.Entities;
using System;
using System.Collections.Generic;

namespace GetPet.BusinessLogic.Model
{
    public class PetFilter : BaseFilter
    {
        public IEnumerable<int> AnimalTypes { get; set; }

        public IDictionary<int, IList<int>> TraitValues { get; set; }

        public IList<int> BooleanTraits{ get; set; }

        public DateTime? CreatedSince { get; set; } = null;
    }
}