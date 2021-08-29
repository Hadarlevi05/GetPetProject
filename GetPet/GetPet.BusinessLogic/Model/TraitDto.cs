using System.Collections.Generic;

namespace GetPet.BusinessLogic.Model
{
    public class TraitDto : BaseDto
    {
        public int TraitId { get; set; }

        public string Name { get; set; }

        public List<TraitOptionDto> TraitOptions { get; set; }

        public bool IsBoolean { get; set; }
    }
}
