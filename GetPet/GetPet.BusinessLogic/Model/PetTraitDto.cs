using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model
{
    public class PetTraitDto : BaseDto
    {        
        public string PetName { get; set; }

        public string TraitName { get; set; }

        public string TraitValue { get; set; }
    }
}
