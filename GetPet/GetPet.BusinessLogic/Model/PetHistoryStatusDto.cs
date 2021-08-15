using GetPet.Data.Enums;

namespace GetPet.BusinessLogic.Model
{
    public class PetHistoryStatusDto : BaseDto
    {
        public int PetId { get; set; }        

        public PetStatus Status { get; set; }
        
        public string Remarks { get; set; }
    }
}
