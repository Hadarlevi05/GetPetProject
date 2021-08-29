using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model.Filters
{
    public class TraitFilter : BaseFilter
    {
        public int? AnimalTypeId { get; set; }

        public string? TraitName { get; set; }

    }
}
