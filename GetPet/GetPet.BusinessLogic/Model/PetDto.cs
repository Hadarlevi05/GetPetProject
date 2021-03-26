using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model
{
    public class PetDto
    {
        public string Name { get; set; }
        public string AgeInYears { get; set; }
        public string AgeInMonths { get; set; }
        public string Gender { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
