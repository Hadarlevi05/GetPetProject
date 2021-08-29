namespace GetPet.BusinessLogic.Model
{
    public class TraitOptionDto : BaseDto
    {
        public int TraitId { get; set; }

        public TraitDto Trait { get; set; }

        public string Option { get; set; }
    }
}
