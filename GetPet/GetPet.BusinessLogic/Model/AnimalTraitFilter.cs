using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model
{
    public class AnimalTraitFilter : BaseFilter
    {
        public int? TraitId { get; set; }

        public int? AnimalTypeId { get; set; }
    }
}
