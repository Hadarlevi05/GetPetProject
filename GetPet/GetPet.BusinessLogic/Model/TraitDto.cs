using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model
{
    public class TraitDto
    {
        public string Name { get; set; }

        public List<TraitOptionDto> TraitOptions { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
