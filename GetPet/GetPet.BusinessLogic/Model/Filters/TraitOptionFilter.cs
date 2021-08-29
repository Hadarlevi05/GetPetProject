using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model.Filters
{
    public class TraitOptionFilter : BaseFilter
    {
        public int? TraitId { get; set; }

        public string Name { get; set; }
    }
}
