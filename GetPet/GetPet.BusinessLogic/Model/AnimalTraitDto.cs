using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model
{
    public class AnimalTraitDto
    {
        public int Id { get; set; }

        public int TraitId { get; set; }

        public string TraitName { get; set; }

        public int AnimalTypeId { get; set; }

        public string AnimalTypeName { get; set; }
    }
}
