using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model.Filters
{
    public class AnimalTraitFilter :  BaseFilter
    {
        public int? AnimalTypeId { get; set; }

        public string AnimalTypeName { get; set; }
    }
}
