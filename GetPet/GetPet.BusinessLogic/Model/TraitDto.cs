using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model
{
    public class TraitDto : BaseDto

    {
        public int traitId { get; set; }

        public string Name { get; set; }

        public List<TraitOptionDto> TraitOptions { get; set; }

        public bool IsBoolean { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
