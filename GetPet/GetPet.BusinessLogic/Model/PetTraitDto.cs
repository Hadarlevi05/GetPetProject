using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model
{
    public class PetTraitDto
    {
        public int Id { get; set; }

        public string PetName { get; set; }

        public string TraitName { get; set; }

        public string TraitValue { get; set; }
    }
}
